﻿namespace Database.VSRO188.SRO_VT_SHARD;

public class Tab_RefAISkill
{
    public int TacticsID { get; set; }

    public string SkillCodeName { get; set; } = null!;

    public byte ExcuteConditionType { get; set; }

    public int? ExcuteConditionData { get; set; }

    public int? Option { get; set; }
}