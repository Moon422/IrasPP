using System;
using IrasPPBackend.Models;
using Microsoft.EntityFrameworkCore;

using DeptProgram = IrasPPBackend.Models.Program;

namespace IrasPPBackend.Services;

public class IrasDbContext : DbContext
{
    public DbSet<School> Schools { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<DeptProgram> Programs { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<SchoolAdmin> SchoolAdmins { get; set; }
    public DbSet<ViceChancellor> ViceChancellors { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<HeadOfDepartment> HeadOfDepartments { get; set; }
    public DbSet<DeanOfSchool> DeanOfSchools { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseCoordinator> CourseCoordinators { get; set; }
    public DbSet<Auth> Auths { get; set; }

    public IrasDbContext(DbContextOptions options) : base(options)
    {
        SaveChangesFailed += (s, e) => throw new InvalidOperationException("Failed to save to database.");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(
            entity =>
            {
                entity.Property(
                    u => u.UserType
                ).HasConversion(
                    ut => ut.ToString(),
                    ut => Enum.Parse<UserType>(ut)
                );

                entity.HasDiscriminator(
                    u => u.UserType
                ).HasValue<Admin>(
                    UserType.UNIVERSITY_ADMIN
                ).HasValue<ViceChancellor>(
                    UserType.VICE_CHANCELLOR
                ).HasValue<SchoolAdmin>(
                    UserType.SCHOOL_ADMIN
                ).HasValue<Faculty>(
                    UserType.FACULTY
                ).HasValue<Student>(
                    UserType.STUDENT
                );
            }
        );

        modelBuilder.Entity<Faculty>(
            entity =>
            {
                entity.Property(
                    f => f.FacultyRoles
                ).HasConversion(
                    ft => ft.ToString(),
                    ft => Enum.Parse<FacultyRoles>(ft)
                );
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
