using Microsoft.EntityFrameworkCore;

namespace FusionStackBackEnd.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
         
       

        public DbSet<Product> products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
           new Role { Id = 1, Name = "Admin" },
           new Role { Id = 2, Name = "Manager" },
           new Role { Id = 3, Name = "Clerk" },
           new Role { Id = 4, Name = "User" }
       );
            modelBuilder.Entity<User>().HasData(
          new User { Id=1,Email="prashantsawant@gmail.com",Name="Prashant Sawant",Password= BCrypt.Net.BCrypt.HashPassword("prashant"),RoleId =1,Phone="9834859931"}
         
      );



            modelBuilder.Entity<User>()
            .HasOne(r => r.Role) 
            .WithMany(u => u.Users)
            .HasForeignKey(u => u.RoleId);
        }






    }

}
