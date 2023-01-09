using Microsoft.EntityFrameworkCore;
using SoftGen.Domain.Entities;

namespace SoftGen.Repository.Context
{
    public class SoftGenApplicationDbContext : DbContext
    {
        public SoftGenApplicationDbContext(DbContextOptions<SoftGenApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers{ get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(options =>
            {
                options.HasKey(x => x.Id);
                options.Property(x => x.FirstName).HasMaxLength(100);
                options.HasMany(x => x.Courses).WithOne(x => x.Student).HasForeignKey(x => x.StudentId);
            });
            modelBuilder.Entity<Teacher>(options =>
            {
                options.HasKey(x => x.Id);
                options.Property(x => x.FirstName).HasMaxLength(100);
                options.HasMany(x => x.Courses).WithOne(x => x.Teacher).HasForeignKey(x => x.TeacherId);
            });
            modelBuilder.Entity<Course>(options =>
            {
                options.HasKey(x => x.Id);
                options.Property(x => x.CourseName).HasMaxLength(100);
            });
            modelBuilder.Entity<StudentCourse>(options => {
                options.HasKey(x => x.Id);
            });
            modelBuilder.Entity<TeacherCourse>(options => {
                options.HasKey(x => x.Id);
            });
            modelBuilder.Entity<User>(options => {
                options.HasKey(x => x.Id);
                options.HasOne(x => x.Student).WithOne(x => x.User).HasForeignKey<Student>(x => x.UserId);
                options.HasOne(x => x.Teacher).WithOne(x => x.User).HasForeignKey<Teacher>(x => x.UserId);
            });

        }
    }
}
