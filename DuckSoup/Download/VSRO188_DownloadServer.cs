using System;
using API;
using API.Database.DuckSoup;
using API.ServiceFactory;
using DuckSoup.Library.Server;
using PacketLibrary.Handler;
using Serilog;

namespace DuckSoup.Download;

public class VSRO188_DownloadServer : FakeServer
{
    private readonly ISharedObjects _sharedObjects;

    public VSRO188_DownloadServer(Service service) : base(service)
    {
        _sharedObjects = ServiceFactory.Load<ISharedObjects>(typeof(ISharedObjects));
    }

    public override void AddSession(ISession session)
    {
        try {
            base.AddSession(session);
            _sharedObjects.DownloadSessions.Add(session);
        }
        catch (Exception exception)
        {
            Log.Error("{0}", exception.ToString());
        }
    }

    public override void RemoveSession(ISession session)
    {
        try
        {
            base.RemoveSession(session);
            if (_sharedObjects.DownloadSessions.Contains(session)) _sharedObjects.DownloadSessions.Remove(session);
        }
        catch (Exception exception)
        {
            Log.Error("{0}", exception.ToString());
        }
    }
}