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
    public DbSet<ViceChancellor> ViceChancellors { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<HeadOfDepartment> HeadOfDepartments { get; set; }
    public DbSet<DeanOfSchool> DeanOfSchools { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }

    public IrasDbContext(DbContextOptions options) : base(options)
    {
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
                    ft => Enum.Parse<FacultySpecialRoles>(ft)
                );
            }
        );

        modelBuilder.Entity<Course>(
            entity =>
            {
                entity.HasMany(c => c.CourseCoordinators)
                .WithMany(f => f.CoursesCoordinated)
                .UsingEntity<CourseCoordinator>(
                    r => r.HasOne(
                            cc => cc.Faculty
                        ).WithMany()
                        .HasForeignKey(
                            cc => cc.FacultyId
                        ).IsRequired(),
                    l => l.HasOne(
                            cc => cc.Course
                        ).WithMany()
                        .HasForeignKey(
                            cc => cc.CourseId
                        ).IsRequired()
                );
            }
        );

        modelBuilder.Entity<Department>(
            entity =>
            {
                entity.HasMany(
                    d => d.HeadOfDepartments
                ).WithMany(
                    f => f.DepartmentsHeaded
                ).UsingEntity<HeadOfDepartment>(
                    r => r.HasOne(
                        hod => hod.Faculty
                    ).WithMany()
                    .HasForeignKey(hod => hod.FacultyId)
                    .IsRequired(),
                    l => l.HasOne(
                        hod => hod.Department
                    ).WithMany()
                    .HasForeignKey(hod => hod.DepartmentId)
                    .IsRequired()
                );
            }
        );

        modelBuilder.Entity<School>(
            entity =>
            {
                entity.HasMany(
                    s => s.DeanOfSchools
                ).WithMany(
                    f => f.SchoolsDeaned
                ).UsingEntity<DeanOfSchool>(
                    r => r.HasOne(dos => dos.Faculty)
                    .WithMany()
                    .HasForeignKey(dos => dos.FacultyId)
                    .IsRequired(),
                    l => l.HasOne(dos => dos.School)
                    .WithMany()
                    .HasForeignKey(dos => dos.SchoolId)
                    .IsRequired()
                );
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
