using SilkroadSecurityAPI.Message;

namespace PacketLibrary.VSRO188.Agent.Client;

public class CLIENT_CONSIGNMENT_LIST : Packet
{
    public CLIENT_CONSIGNMENT_LIST() : base(0x750E)
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

        Reset();

        return this;
    }

    public static Packet of()
    {
        return new CLIENT_CONSIGNMENT_LIST();
    }
}