using Microsoft.EntityFrameworkCore;
using WareHouseManagerWebApp.Model;

namespace WareHouseManagerWebApp.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<userModel> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<userModel>().ToTable("worker"); // table name in MySQL
            modelBuilder.Entity<userModel>().HasKey(u => u.Id); // worker_id
            // column maping
            modelBuilder.Entity<userModel>()
                .Property(u => u.Id)
                .HasColumnName("worker_id");
            modelBuilder.Entity<userModel>()
                .Property(u => u.Username)
                .HasColumnName("login");
            modelBuilder.Entity<userModel>().Property(u => u.Password)
                .HasColumnName("password");
            modelBuilder.Entity<userModel>().Property(u => u.Name)
                .HasColumnName("name");
            modelBuilder.Entity<userModel>().Property(u => u.Lastname)
                .HasColumnName("lastname");
            modelBuilder.Entity<userModel>().Property(u => u.Role)
                .HasColumnName("role");
        }

    }
}
