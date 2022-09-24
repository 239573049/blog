using Blog.Module;
using Microsoft.EntityFrameworkCore;

namespace Blog.EntityFrameworkCore;

public class BlogDbContext : DbContext
{
    public DbSet<BlogComments> BlogComments { get; set; }

    public DbSet<BlogLikes> BlogLikes { get; set; }

    public DbSet<Blogs> Blogs { get; set; }

    public DbSet<BlogTypes> BlogTypes { get; set; }

    public DbSet<Users> Users { get; set; }


    public BlogDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        // 显示更详细的异常信息
        optionsBuilder.EnableDetailedErrors();
#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Blogs>(x =>
        {
            x.ToTable("Blogs");

            x.HasComment("博客文章");

            x.HasKey(x => x.Id);

            x.HasIndex(x => x.Id);
            x.HasIndex(x => x.TypeId);
            x.HasIndex(x => x.AuthorId);
            x.HasIndex(x => x.Title);

        });

        modelBuilder.Entity<Users>(x =>
        {
            x.ToTable("Users");

            x.HasComment("用户表");

            x.HasKey(x => x.Id);

            x.HasIndex(x => x.Id);
            x.HasIndex(x => x.Username).IsUnique();

        });

        modelBuilder.Entity<BlogTypes>(x =>
        {
            x.ToTable("BlogTypes");

            x.HasComment("博客类型");

            x.HasKey(x => x.Id);

            x.HasIndex(x => x.Id);
            x.HasIndex(x => x.Name);
        });

        modelBuilder.Entity<BlogLikes>(x =>
        {
            x.ToTable("BlogLikes");

            x.HasComment("博客浏览量");

            x.HasKey(x => x.Id);

            x.HasIndex(x => x.Id);

        });

        modelBuilder.Entity<BlogComments>(x =>
        {
            x.ToTable("BlogComments");

            x.HasComment("博客评论");

            x.HasKey(x => x.Id);
            x.HasIndex(x => x.Id);
        });
    }
}
