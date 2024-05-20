using Microsoft.EntityFrameworkCore;

namespace ConnectDatabase.Models;

using Microsoft.EntityFrameworkCore.ChangeTracking;

public class ApiDbContext:DbContext 
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
        
    }
    public DbSet<Users> users { get; set; }

    public override EntityEntry<TEntity> Add<TEntity>(TEntity entity) => base.Add(entity);
}