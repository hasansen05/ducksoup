using Microsoft.EntityFrameworkCore;

namespace Database;

public class DuckContext : DbContext
{
    public static Dictionary<Type, string> ConnectionStrings = new();

    protected DuckContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (ConnectionStrings.ContainsKey(GetType())) optionsBuilder.UseSqlServer(ConnectionStrings[GetType()]).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    public bool CanConnect()
    {
        using var db = new DuckContext();
        return db.Database.CanConnect();
    }
}