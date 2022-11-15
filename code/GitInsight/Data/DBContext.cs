namespace Data;

public class DBContext : DbContext
{
    public DbSet<DBAuthor> Authors => Set<DBAuthor>();
    public DbSet<DBCommit> Commits => Set<DBCommit>();
    public DbSet<DBRepository> Repositories => Set<DBRepository>();

    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DBAuthor>()
                    .Property(i => i.Name)
                    .HasMaxLength(50);
        
        modelBuilder.Entity<DBCommit>()
                    .Property(i => i.Date);

        modelBuilder.Entity<DBRepository>()
                    .Property(i => i.Path)
                    .HasMaxLength(120);

        modelBuilder.Entity<DBRepository>()
                    .Property(i => i.Name)
                    .HasMaxLength(50);

        modelBuilder.Entity<DBCommit>()
                    .HasOne(i => i.Author).WithMany(i => i.Commits);

        modelBuilder.Entity<DBRepository>()
                    .HasMany(i => i.Commits).WithOne(i => i.BelongsTo);

                    
                    
    }

}
