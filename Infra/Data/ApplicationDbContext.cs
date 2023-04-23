using Flunt.Notifications;
using IWantApp.Domain.Products;
using IWantApp.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IWantApp.Infra.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Category { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :  base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) 
    {
        base.OnModelCreating(builder);

        builder.Ignore<Notification>();
        builder.Entity<Product>()
            .Property(p => p.Name).IsRequired();

        builder.Entity<Product>()
            .Property(p => p.Description).HasMaxLength(255);

        builder.Entity<Category>()
            .Property(p => p.Name).IsRequired();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>()
            .HaveMaxLength(100);
     
    }
}
