using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
	public DbSet<Product> Products { get; set; }
	public DbSet<Category> Categories { get; set;}
	
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}
	
	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<Product>().HasKey(p => p.Code);
		builder.Entity<Product>().Property(p => p.Code).HasMaxLength(3).IsRequired();
		builder.Entity<Product>().Property(p => p.Name).HasMaxLength(255).IsRequired();
		builder.Entity<Product>().Property(p => p.Description).HasMaxLength(255).IsRequired(false);
		builder.Entity<Category>().ToTable("Categories");
	}
}
