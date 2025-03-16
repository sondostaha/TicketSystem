using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;
using TicketSystem.Data.Models;


namespace TicketSystem.Data
{
    public class AppDbContext :IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Branches> Branches { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Departments>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            builder.Entity<Branches>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<Tickets>()
                .Property(c => c.ProgressIndicators)
                .HasConversion(new EnumToStringConverter<Progress>());
            builder.Entity<Tickets>()
               .Property(c => c.Status)
               .HasConversion(new EnumToStringConverter<Status>());
            //builder.Entity<Tickets>()
            //    .Property(b => b.Status)
            //    .HasDefaultValue(Status.Created);
            builder.Entity<Tickets>()
                .Property(v => v.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            builder.Entity<Tickets>()
            .HasOne(x => x.Users)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Departments>()
               .HasData(new Departments()
               {
                   Id = 1,
                   Title = "Programming",
                   CreatedAt = DateTime.Now,
               }, new Departments()
               {
                   Id = 2,
                   Title = "Engineering",
                   CreatedAt = DateTime.Now,
               }, new Departments()
               {
                   Id = 3,
                   Title = "CallCenter",
                   CreatedAt = DateTime.Now,
               });
            builder.Entity<Branches>()
                //.Property(x => x.Title)
                .HasIndex(x => x.Title)
                .IsUnique();
            builder.Entity<Branches>()
                .HasData(
                new Branches() { Id = 1, Title = "Cirnckle1", Address = "6th of october" },
                new Branches() { Id = 2, Title = "Cirnckle2", Address = "Faiyum" }

                );
            var adminId = Guid.NewGuid().ToString();
            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole
                {
                    Id = adminId,
                    Name = "Admin",
                    NormalizedName = "admin",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Director",
                    NormalizedName = "director",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Employee",
                    NormalizedName = "employee",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                }
                );
            var hasher = new PasswordHasher<Users>();
            var user_id = Guid.NewGuid().ToString();
            builder.Entity<Users>()
                .HasData(new Users
                {
                    Id = user_id,
                    UserName = "sondos",
                    NormalizedUserName ="Sondos",
                    Email = "sondos@gmail.com",
                    NormalizedEmail ="Sondos@gmail.com",
                    AssocBranch = 1,
                    DepartmentId = 1,
                    PasswordHash = hasher.HashPassword(null,"123456Ss$"),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                });
            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>()
                {
                    RoleId = adminId,
                    UserId = user_id,
                });

        }
    }
}
    