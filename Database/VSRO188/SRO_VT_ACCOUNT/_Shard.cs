﻿namespace Database.VSRO188.SRO_VT_ACCOUNT;

public class _Shard
{
    public short nID { get; set; }

    public byte nFarmID { get; set; }

    public byte nContentID { get; set; }

    public string szName { get; set; } = null!;

    public string szDesc { get; set; } = null!;

    public string szDBConfig { get; set; } = null!;

    public short nMaxUser { get; set; }

    public short nStartupServerID { get; set; }

    public byte nStatus { get; set; }

    public byte nCurrentUserRatio { get; set; }
}