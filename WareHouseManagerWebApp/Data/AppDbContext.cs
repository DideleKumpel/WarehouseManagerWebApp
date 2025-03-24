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
        public DbSet<employeeModel> Employees { get; set; }
        public DbSet<taskModel> Tasks { get; set; }
        public DbSet<locationModel> Locations { get; set; }
        public DbSet<productModel> Products { get; set; }
        public DbSet<rampModel> Ramps { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<employeeModel>().ToTable("worker"); // table name in MySQL
            modelBuilder.Entity<employeeModel>().HasKey(u => u.Id); // worker_id
            // column maping
            modelBuilder.Entity<employeeModel>()
                .Property(u => u.Id)
                .HasColumnName("worker_id");
            modelBuilder.Entity<employeeModel>()
                .Property(u => u.Name)
                .HasColumnName("name").IsRequired().HasMaxLength(50);
            modelBuilder.Entity<employeeModel>()
                .Property(u => u.Lastname)
                .HasColumnName("lastname").IsRequired().HasMaxLength(50);
            modelBuilder.Entity<employeeModel>()
                .Property(u => u.Role)
                .HasColumnName("role").IsRequired();

            modelBuilder.Entity<userModel>().ToTable("user"); // Table 'user' in MySQL
            modelBuilder.Entity<userModel>().HasKey(u => u.Id); 

            modelBuilder.Entity<userModel>()
                .Property(u => u.Id)
                .HasColumnName("user_id"); 
            modelBuilder.Entity<userModel>()
                .Property(u => u.Username)
                .HasColumnName("login").IsRequired().HasMaxLength(50);
            modelBuilder.Entity<userModel>()
                .Property(u => u.Password)
                .HasColumnName("password").IsRequired().HasMaxLength(50);
            modelBuilder.Entity<userModel>()
                .Property(u => u.EmployeeId)
                .HasColumnName("worker_id").IsRequired();
            // Relacion between 'user' and 'employee'
           modelBuilder.Entity<userModel>()
                .HasOne(u => u.Employee)
                .WithOne() // 1:1
                .HasForeignKey<userModel>(u => u.EmployeeId); // EmployeeId foreign key

            modelBuilder.Entity<rampModel>().ToTable("ramps");   //table name in MySQL
            modelBuilder.Entity<rampModel>().HasKey(r => r.Name); // location_id
            // column maping
            modelBuilder.Entity<rampModel>()
                .Property(r => r.Name)
                .HasColumnName("ramp_name").IsRequired().HasMaxLength(50);

            modelBuilder.Entity<productModel>().ToTable("products");   //table name in MySQL
            modelBuilder.Entity<productModel>().HasKey(p => p.Barcode); // product_id
            // column maping
            modelBuilder.Entity<productModel>()
                .Property(p => p.Barcode)
                .HasColumnName("products_id").IsRequired().HasMaxLength(50);
            modelBuilder.Entity<productModel>()
                .Property(p => p.Name)
                .HasColumnName("productname").IsRequired().HasMaxLength(100);
            modelBuilder.Entity<productModel>()
                .Property(p => p.Weight)
                .HasColumnName("weight").IsRequired();
            modelBuilder.Entity<productModel>()
                .Property(p => p.Category)
                .HasColumnName("category").IsRequired().HasMaxLength(100);
            modelBuilder.Entity<productModel>()
                .Property(p => p.Description)
                .HasColumnName("description").IsRequired().HasMaxLength(350);

            modelBuilder.Entity<locationModel>().ToTable("locations");   //table name in MySQL
            modelBuilder.Entity<locationModel>().HasKey(l => l.Id); // location_id
            // column maping
            modelBuilder.Entity<locationModel>()
                .Property(l => l.Id)
                .HasColumnName("locations_id");
            modelBuilder.Entity<locationModel>()
                .Property(l => l.Shelf)
                .HasColumnName("shelf").IsRequired().HasMaxLength(10);
            modelBuilder.Entity<locationModel>()
                .Property(l => l.Row)
                .HasColumnName("Row").IsRequired().HasMaxLength(10);
            modelBuilder.Entity<locationModel>()
                .Property(l => l.Level)
                .HasColumnName("Level").IsRequired().HasMaxLength(10);
            modelBuilder.Entity<locationModel>()
                .Property(l => l.MaxCapacity)
                .HasColumnName("maxcapacity").IsRequired();
            modelBuilder.Entity<locationModel>()
                .Property(l => l.ItemBarcode)
                .HasColumnName("products_products_id").IsRequired(false);

            //Relaction in db
            modelBuilder.Entity<locationModel>()
                .HasOne(l => l.Product) // relaction to ProductModel
                .WithMany() // 1:n
                .HasForeignKey(l => l.ItemBarcode);

            modelBuilder.Entity<taskModel>().ToTable("tasks");   //table name in MySQL
            modelBuilder.Entity<taskModel>().HasKey(t => t.Id);
            // column maping
            modelBuilder.Entity<taskModel>()
                .Property(t => t.Id)
                .HasColumnName("tasks_id");
            modelBuilder.Entity<taskModel>()
                .Property(t => t.Type)
                .HasColumnName("type").IsRequired().HasConversion<string>();
            modelBuilder.Entity<taskModel>()
                .Property(t => t.Status)
                .HasColumnName("status").IsRequired().HasConversion<string>();
            modelBuilder.Entity<taskModel>()
                .Property(t => t.UploadDate)
                .HasColumnName("upload_dateTime").IsRequired();
            modelBuilder.Entity<taskModel>()
                .Property(t => t.FinishDate)
                .HasColumnName("finish_dateTime").IsRequired(false);
            modelBuilder.Entity<taskModel>()
                .Property(t => t.RampName)
                .HasColumnName("ramp_name").IsRequired().HasMaxLength(50);
            modelBuilder.Entity<taskModel>()
                .Property(t => t.EmployeeId)
                .HasColumnName("worker_worker_id").IsRequired(false);
            modelBuilder.Entity<taskModel>()
                .Property(t => t.LocationId)
                .HasColumnName("locations_locations_id").IsRequired();
            modelBuilder.Entity<taskModel>()
                .Property(t => t.ProductBarcode)
                .HasColumnName("products_products_id").IsRequired();
            // Relaction to EmployeeModel
            modelBuilder.Entity<taskModel>()
                .HasOne(t => t.Ramp)
                .WithMany() // 1:n
                .HasForeignKey(t => t.RampName);
            modelBuilder.Entity<taskModel>()
                .HasOne(t => t.Employee)
                .WithMany() // 1:n
                .HasForeignKey(t => t.EmployeeId);
            modelBuilder.Entity<taskModel>()
                .HasOne(t => t.Location)
                .WithMany() // 1:n
                .HasForeignKey(t => t.LocationId);
            modelBuilder.Entity<taskModel>()
                .HasOne(t => t.Product)
                .WithMany() // 1:n
                .HasForeignKey(t => t.ProductBarcode);

        }

    }
}
