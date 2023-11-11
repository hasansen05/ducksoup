using SilkroadSecurityAPI.Message;

namespace PacketLibrary.VSRO188.Agent.Client;

public class CLIENT_COS_UPDATE_RIDESTATE_REQUEST : Packet
{
    public CLIENT_COS_UPDATE_RIDESTATE_REQUEST() : base(0x70CB)
    {
    }

    public override PacketDirection FromDirection => PacketDirection.Client;

    public override PacketDirection ToDirection => PacketDirection.Server;


    public override async Task Read()
    {
        //throw new NotImplementedException();
    }

    public override async Task<Packet> Build()
    {
        //throw new NotImplementedException();

        //Reset();

        return this;
    }

    public static Packet of()
    {
        return new CLIENT_COS_UPDATE_RIDESTATE_REQUEST();
    }
}