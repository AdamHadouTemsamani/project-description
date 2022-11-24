namespace Data;

public class DBContext : DbContext
{
    public DbSet<DBCommit> Commits => Set<DBCommit>();
    public DbSet<DBRepository> Repositories => Set<DBRepository>();

    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
                        
        modelBuilder.Entity<DBCommit>()
                    .Property(i => i.Date);

        modelBuilder.Entity<DBRepository>()
                    .Property(i => i.Path)
                    .HasMaxLength(120);

        modelBuilder.Entity<DBRepository>()
                    .Property(i => i.Name)
                    .HasMaxLength(50);

        modelBuilder.Entity<DBCommit>
                    (c => { c.HasKey(i => new { i.RepositoryId, i.Id}); });

        modelBuilder.Entity<DBRepository>
                    (i => { i.HasKey(i => i.Id); i.Property(i => i.LatestCommit).IsRequired(); });
                    
    }

}
