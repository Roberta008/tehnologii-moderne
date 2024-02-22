using Microsoft.EntityFrameworkCore;
using ProiectPrezente.Models.Classes;
using ProiectPrezente.Models.Users;

namespace ProiectPrezente.Database
{
    // Prin intermediul constructorului, se primesc opțiunile de configurare a contextului de bază
    public class DatabaseContext(DbContextOptions<DatabaseContext> contextOptions) : DbContext(contextOptions)
    {
        // Setul de date pentru entitatea Student
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassTeacher> ClassTeachers { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Class Teachers
            modelBuilder.Entity<ClassTeacher>().HasKey
            (
                keyExpression: classTeacherEntity => new { classTeacherEntity.ClassID, classTeacherEntity.ProfessorID }
            );
            modelBuilder.Entity<ClassTeacher>()
                .HasOne(navigationExpression: classTeacherEntity => classTeacherEntity.Class)
                    .WithMany(navigationExpression: classEntity => classEntity.Teachers)
                .HasForeignKey(foreignKeyExpression: classTeacherEntity => classTeacherEntity.ClassID)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
            modelBuilder.Entity<ClassTeacher>()
                .HasOne(navigationExpression: classTeacherEntity => classTeacherEntity.Professor)
                    .WithMany(navigationExpression: userEntity => userEntity.ClassesTaught)
                .HasForeignKey(foreignKeyExpression: classTeacherEntity => classTeacherEntity.ProfessorID)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

            // Enrollments
            modelBuilder.Entity<Enrollment>().HasKey
            (
                keyExpression: enrollmentEntity => new { enrollmentEntity.StudentID, enrollmentEntity.ClassID }
            );
            modelBuilder.Entity<Enrollment>()
                .HasOne(navigationExpression: enrollmentEntity => enrollmentEntity.Student)
                    .WithMany(navigationExpression: userEntity => userEntity.Enrollments)
                .HasForeignKey(foreignKeyExpression: enrollmentEntity => enrollmentEntity.StudentID)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade)
                .IsRequired();
            modelBuilder.Entity<Enrollment>()
                .HasOne(navigationExpression: enrollmentEntity => enrollmentEntity.Class)
                .WithMany(navigationExpression: classEntity => classEntity.Enrollments)
                .HasForeignKey(foreignKeyExpression: enrollmentEntity => enrollmentEntity.ClassID)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
