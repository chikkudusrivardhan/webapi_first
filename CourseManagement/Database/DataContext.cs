using CourseManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Student> Student => Set<Student>();
        public DbSet<Course> Course => Set<Course>();
        public DbSet<Users> Users => Set<Users>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
            .HasOne(s => s.Users)
            .WithOne(u => u.Student)
            .HasForeignKey<Student>(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            // User → Course (1–1)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Users)
                .WithMany(u => u.OwnedCoursers)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-many Student ↔ Course
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseStudent",
                    j => j
                        .HasOne<Course>()
                        .WithMany()
                        .HasForeignKey("CoursesCourseId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<Student>()
                        .WithMany()
                        .HasForeignKey("StudentsStudentId")
                        .OnDelete(DeleteBehavior.Restrict)
                );

            modelBuilder.Entity<Users>().HasData(
                new Users { UserId = 12, 
                    UserName = "Admin",
                    Age = 21,
                    Role = Roles.Admin,
                    PasswordHash = 
                    Convert
                    .FromBase64String("lfcn8aksyLGtYJffRInB5jJZ9BieHsQPuWyLdk2Uapc="),
                    PasswordSalt = 
                    Convert
                    .FromBase64String("UagU6j7DohmKWMlKKN4jSvP4BanbSbuTnFs6nPEH+T5jzGVqZi8e3oTvsDssOFEOjaVrehoFvtA6XBrm2lAbOA==")
                }
            );
        }
    }
}
