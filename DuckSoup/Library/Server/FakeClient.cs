﻿using System;
using System.Net.Sockets;
using API.Database.DuckSoup;
using API.Session;
using PacketLibrary.Handler;
using Serilog;
using SilkroadSecurityAPI;
using SilkroadSecurityAPI.Message;
using TcpClient = NetCoreServer.TcpClient;

namespace DuckSoup.Library.Server;

/// <summary>
///     Basically the connection to the Server, the proxy spawns a client that connects to the Silkroad Server
/// </summary>
public class FakeClient : TcpClient
{
    public FakeClient(FakeServer fakeServer, Service service) : base(service.RemoteMachine_Machine.Address,
        service.RemotePort)
    {
        ServerSecurity = Utility.GetSecurity(service.SecurityType);
        FakeServer = fakeServer;
    }

    public ISession? Session { get; internal set; }
    internal ISecurity ServerSecurity { get; }
    private FakeServer FakeServer { get; }

    protected override void OnConnected()
    {
        Log.Debug($"FakeRemoteClient connected a new session with Id {Id}");
    }

    protected override void OnDisconnected()
    {
        if (Session == null)
        {
            return;
        }

        Session.Disconnect();
        Log.Debug($"FakeRemoteClient disconnected a session with Id {Id}");
    }

    protected override void OnError(SocketError error)
    {
        if (Session == null)
        {
            return;
        }

        Session.Disconnect();
        Log.Debug($"FakeRemoteClient caught an error with code {error}");
    }

    // Receive from Server
    // S -> P -> C
    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        if (Session == null)
        {
            return;
        }
        
        string message = string.Empty;
        try
        {
            ServerSecurity.Recv(buffer, (int)offset, (int)size);

            var receivedPackets = ServerSecurity.TransferIncoming();

            if (receivedPackets == null || receivedPackets.Count == 0) return;

            foreach (var packet in receivedPackets)
            {
                
                var packetType = packet.Encrypted ? "[E]" : packet.Massive ? "[M]" : "";
                message = $"[S -> P] {packetType} Packet: 0x{packet.MsgId:X} - {Id}";
                Log.Verbose(message);

                if (packet.MsgId == 0x5000 || packet.MsgId == 0x9000) continue;

                var packetResult = FakeServer.PacketHandler.HandleServer(packet, Session).Result;

                switch (packetResult.ResultType)
                {
                    case PacketResultType.Block:
                        // TODO :: Temporary for testing purp.
                        // Session.SendToClient(packetResult);
                        Log.Debug($"Server Packet: 0x{packet.MsgId:X} is perhaps not on whitelist!");
                        break;
                    case PacketResultType.Disconnect:
                        // Console.WriteLine($"Packet: 0x{packet.MsgId:X} is on blacklist!");
                        Session.Disconnect();
                        return;
                    case PacketResultType.Nothing:
                        Session.SendToClient(packetResult);
                        break;
                    default:
                        Log.Error("FakeClient - Unknown ResultType");
                        break;
                }
            }

            Session.TransferToClient();
        }
        catch (Exception exception)
        {
            Session.GetData(Data.CharInfo, out ICharInfo? charInfo, null);
            Session.GetData(Data.CharId, out int charId, -1);
            Log.Error("FakeClient Recv | 0x{0:X} | Name: {1} | Id: {2} | ServerType: {3} ", message, (charInfo != null? charInfo.CharName : "null"), charId, FakeServer.Service.ServerType);
            Log.Error("FakeClient Recv | SSAClientId: {1} | Current: {2} | Last: {3} ", Session.GetServerSecurity().GetId(), Session.GetServerSecurity().GetCurrentLockState(), Session.GetServerSecurity().GetLastLockState());
            Log.Error("FakeClient Recv | SSAServerId: {1} | Current: {2} | Last: {3} ", Session.GetClientSecurity().GetId(), Session.GetClientSecurity().GetCurrentLockState(), Session.GetClientSecurity().GetLastLockState());
            Log.Error("FakeClient Recv | {0}", exception.Message);
            Log.Error("FakeClient Recv | {0}", exception.StackTrace);
            Log.Error("FakeClient Recv | {0}", exception.InnerException);
            Log.Error("FakeClient Recv | {0}", exception.Data);
            Session.Disconnect();
        }
    }

    public void Send(Packet packet, bool transfer = false)
    {
        try
        {
            ServerSecurity.Send(packet);

            if (transfer) Transfer();
        }
        catch (Exception exception)
        {
            Log.Error("FakeClient:124 | ID: {0} ", this.Id);
            Log.Error("FakeClient:124 | {0}", exception.Message);
            Log.Error("FakeClient:124 | {0}", exception.StackTrace);
            Log.Error("FakeClient:124 | {0}", exception.InnerException);
            Log.Error("FakeClient:124 | {0}", exception.Data);
            this.Disconnect();
        }
    }

    public void Transfer()
    {
        try
        {
            ServerSecurity.TransferOutgoing(this);
        }
        catch (Exception exception)
        {
            Log.Error("FakeClient:143 | ID: {0} ", this.Id);
            Log.Error("FakeClient:143 | {0}", exception.Message);
            Log.Error("FakeClient:143 | {0}", exception.StackTrace);
            Log.Error("FakeClient:143 | {0}", exception.InnerException);
            Log.Error("FakeClient:143 | {0}", exception.Data);
            this.Disconnect();
        }
    }
}