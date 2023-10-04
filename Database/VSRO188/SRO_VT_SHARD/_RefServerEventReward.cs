﻿namespace Database.VSRO188.SRO_VT_SHARD;

public class _RefServerEventReward
{
    public byte Service { get; set; }

    public int RewardID { get; set; }

    public int OwnerServerEventID { get; set; }

    public int RefRewardID { get; set; }

    public byte Quantity { get; set; }

    public byte RewardClass { get; set; }

    public byte MasterReward { get; set; }
}