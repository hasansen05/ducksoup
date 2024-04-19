using System.Collections.Concurrent;
using System.Linq.Expressions;
using Database.VSRO188.SRO_VT_ACCOUNT;
using Database.VSRO188.SRO_VT_SHARD;
using Microsoft.EntityFrameworkCore;

namespace Database.VSRO188;

public static class Cache
{
    private static readonly ConcurrentDictionary<int, _RefObjCommon> RefObjCommons = new();
    private static readonly ConcurrentDictionary<int, _RefObjItem> RefObjItems = new();
    private static readonly ConcurrentDictionary<int, _RefObjChar> RefObjChars = new();
    private static readonly ConcurrentDictionary<int, _RefSkill> RefSkills = new();
    private static readonly ConcurrentDictionary<int, _RefRegion> RefRegions = new();
    private static readonly ConcurrentDictionary<int, _RefQuest> RefQuests = new();
    private static readonly ConcurrentDictionary<int, _RefQuestReward> RefQuestRewards = new();
    private static readonly ConcurrentDictionary<int, _RefQuestRewardItem> RefQuestRewardItems = new();
    private static readonly ConcurrentDictionary<byte, _RefLevel> RefLevels = new();

    private static readonly ConcurrentDictionary<int, _Notice> Notices = new();

    public static async Task<_RefObjCommon?> GetRefObjCommonAsync(int id)
    {
        if (RefObjCommons.TryGetValue(id, out var value))
        {
            return value;
        }

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefObjCommons.FindAsync(id);
        
        if (value != null)
        {
            value = RefObjCommons.GetOrAdd(id, value);
        }

        return value;
    }

    public static async Task<_RefObjCommon?> GetRefObjCommonAsync(Expression<Func<_RefObjCommon, bool>> predicate)
    {
        var value = RefObjCommons.Values.AsQueryable().FirstOrDefault(predicate.Compile());

        if (value != null) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefObjCommons.FirstOrDefaultAsync(predicate);

        if (value != null)
        {
            value = RefObjCommons.GetOrAdd(value.ID, value);
        }

        return value;
    }

    public static async Task<_RefObjItem?> GetRefObjItemAsync(int id)
    {
        if (RefObjItems.TryGetValue(id, out var value)) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefObjItems.FindAsync(id);
        if (value != null)
        {
            value = RefObjItems.GetOrAdd(id, value);
        }

        return value;
    }

    public static async Task<_RefObjItem?> GetRefObjItemAsync(Expression<Func<_RefObjItem, bool>> predicate)
    {
        var value = RefObjItems.Values.AsQueryable().FirstOrDefault(predicate.Compile());

        if (value != null) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefObjItems.FirstOrDefaultAsync(predicate);
        if (value != null)
        {
            value = RefObjItems.GetOrAdd(value.ID, value);
        }

        return value;
    }

    public static async Task<_RefObjChar?> GetRefObjCharAsync(int id)
    {
        if (RefObjChars.TryGetValue(id, out var value)) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefObjChars.FindAsync(id);
        if (value != null)
        {
            value = RefObjChars.GetOrAdd(id, value);
        }

        return value;
    }

    public static async Task<_RefObjChar?> GetRefObjCharAsync(Expression<Func<_RefObjChar, bool>> predicate)
    {
        var value = RefObjChars.Values.AsQueryable().FirstOrDefault(predicate.Compile());

        if (value != null) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefObjChars.FirstOrDefaultAsync(predicate);
        if (value != null)
        {
            value = RefObjChars.GetOrAdd(value.ID, value);
        }

        return value;
    }

    public static async Task<_RefSkill?> GetRefSkillAsync(int id)
    {
        if (RefSkills.TryGetValue(id, out var value)) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefSkills.FindAsync(id);
        if (value != null)
        {
            value = RefSkills.GetOrAdd(id, value);
        }

        return value;
    }

    public static async Task<_RefSkill?> GetRefSkillAsync(Expression<Func<_RefSkill, bool>> predicate)
    {
        var value = RefSkills.Values.AsQueryable().FirstOrDefault(predicate.Compile());

        if (value != null) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefSkills.FirstOrDefaultAsync(predicate);
        if (value != null)
        {
            value = RefSkills.GetOrAdd(value.ID, value);
        }

        return value;
    }

    public static async Task<_RefRegion?> GetRefRegionAsync(int regionId)
    {
        if (RefRegions.TryGetValue(regionId, out var value)) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefRegions.FindAsync(regionId);
        if (value != null)
        {
            value = RefRegions.GetOrAdd(regionId, value);
        }
        
        return value;
    }

    public static async Task<_RefRegion?> GetRefRegionAsync(Expression<Func<_RefRegion, bool>> predicate)
    {
        var value = RefRegions.Values.AsQueryable().FirstOrDefault(predicate.Compile());

        if (value != null) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefRegions.FirstOrDefaultAsync(predicate);

        if (value != null)
        {
            value = RefRegions.GetOrAdd(value.wRegionID, value);
        }

        return value;
    }

    public static async Task<_RefQuest?> GetRefQuestAsync(int id)
    {
        if (RefQuests.TryGetValue(id, out var value)) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefQuests.FindAsync(id);
        if (value != null)
        {
            value = RefQuests.GetOrAdd(id, value);
        }

        return value;
    }

    public static async Task<_RefQuest?> GetRefQuestAsync(Expression<Func<_RefQuest, bool>> predicate)
    {
        var value = RefQuests.Values.AsQueryable().FirstOrDefault(predicate.Compile());

        if (value != null) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefQuests.FirstOrDefaultAsync(predicate);
        if (value != null)
        {
            value = RefQuests.GetOrAdd(value.ID, value);
        }

        return value;
    }

    public static async Task<_RefQuestReward?> GetRefQuestRewardAsync(int questId)
    {
        if (RefQuestRewards.TryGetValue(questId, out var value)) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefQuestRewards.FindAsync(questId);
        if (value != null)
        {
            value = RefQuestRewards.GetOrAdd(questId, value);
        }

        return value;
    }

    public static async Task<_RefQuestReward?> GetRefQuestRewardAsync(Expression<Func<_RefQuestReward, bool>> predicate)
    {
        var value = RefQuestRewards.Values.AsQueryable().FirstOrDefault(predicate.Compile());

        if (value != null) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefQuestRewards.FirstOrDefaultAsync(predicate);
        if (value != null)
        {
            value = RefQuestRewards.GetOrAdd(value.QuestID, value);
        }

        return value;
    }

    public static async Task<_RefQuestRewardItem?> GetRefQuestRewardItemAsync(int questId)
    {
        if (RefQuestRewardItems.TryGetValue(questId, out var value)) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefQuestRewardItems.FindAsync(questId);
        if (value != null)
        {
            value = RefQuestRewardItems.GetOrAdd(questId, value);
        }

        return value;
    }

    public static async Task<_RefQuestRewardItem?> GetRefQuestRewardItemAsync(
        Expression<Func<_RefQuestRewardItem, bool>> predicate)
    {
        var value = RefQuestRewardItems.Values.AsQueryable().FirstOrDefault(predicate.Compile());

        if (value != null) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefQuestRewardItems.FirstOrDefaultAsync(predicate);
        if (value != null)
        {
            value = RefQuestRewardItems.GetOrAdd(value.QuestID, value);
        }

        return value;
    }

    public static async Task<_RefLevel?> GetRefLevelAsync(byte level)
    {
        if (RefLevels.TryGetValue(level, out var value)) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefLevels.FindAsync(level);
        if (value != null)
        {
            value = RefLevels.GetOrAdd(level, value);
        }

        return value;
    }

    public static async Task<_RefLevel?> GetRefLevelAsync(Expression<Func<_RefLevel, bool>> predicate)
    {
        var value = RefLevels.Values.AsQueryable().FirstOrDefault(predicate.Compile());

        if (value != null) return value;

        await using var db = new Context.SRO_VT_SHARD();
        value = await db._RefLevels.FirstOrDefaultAsync(predicate);
        if (value != null)
        {
            value = RefLevels.GetOrAdd(value.Lvl, value);
        }

        return value;
    }

    public static async Task<_Notice?> GetNoticeAsync(int id)
    {
        if (Notices.TryGetValue(id, out var value)) return value;

        await using var db = new Context.SRO_VT_ACCOUNT();
        value = await db._Notices.FindAsync(id);
        if (value != null)
        {
            value = Notices.GetOrAdd(id, value);
        }

        return value;
    }

    public static async Task<_Notice?> GetNoticeAsync(Expression<Func<_Notice, bool>> predicate)
    {
        var value = Notices.Values.AsQueryable().FirstOrDefault(predicate.Compile());

        if (value != null) return value;

        await using var db = new Context.SRO_VT_ACCOUNT();
        value = await db._Notices.FirstOrDefaultAsync(predicate);
        if (value != null)
        {
            value = Notices.GetOrAdd(value.ID, value);
        }

        return value;
    }
    
    public static async Task<_Char?> GetCharAsync(int charId)
    {
        await using var db = new Context.SRO_VT_SHARD();
        return await db._Chars.FirstOrDefaultAsync(c => c.CharID == charId);
    }

    public static async Task<_Char?> GetCharAsync(Expression<Func<_Char, bool>> predicate)
    {
        await using var db = new Context.SRO_VT_SHARD();
        return await db._Chars.FirstOrDefaultAsync(predicate);
    }
}