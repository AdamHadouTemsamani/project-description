namespace Data;

public class DBContext : DbContext
{
    public DbSet<DBAuthor> Authors => Set<DBAuthor>();
    public DbSet<DBCommit> Commits => Set<DBCommit>();

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
        

    }

}
