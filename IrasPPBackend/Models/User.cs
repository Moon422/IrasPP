using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IrasPPBackend.Schemas;

namespace IrasPPBackend.Models;

public enum UserType
{
    UNIVERSITY_ADMIN,
    SCHOOL_ADMIN,
    VICE_CHANCELLOR,
    FACULTY,
    STUDENT,
}

[Flags]
public enum FacultyRoles
{
    FACULTY,
    COURSE_COORDINATOR,
    HEAD_OF_DEPARTMENT,
    COURSE_COORDINATOR_AND_HEAD_OF_DEPARTMENT = COURSE_COORDINATOR | HEAD_OF_DEPARTMENT,
    DEAN_OF_SCHOOL,
    COURSE_COORDINATOR_AND_DEAN_OF_SCHOOL = COURSE_COORDINATOR | DEAN_OF_SCHOOL,
    HEAD_OF_DEPARTMENT_AND_DEAN_OF_SCHOOL = HEAD_OF_DEPARTMENT | DEAN_OF_SCHOOL,
    COURSE_COORDINATOR_AND_HEAD_OF_DEPARTMENT_AND_DEAN_OF_SCHOOL = COURSE_COORDINATOR | HEAD_OF_DEPARTMENT | DEAN_OF_SCHOOL
}

public interface IDtoConvertible<TDto>
{
    public TDto CreateDto();
}

[Table("Users_T")]
public abstract class User
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string UserId { get; set; }

    [Required]
    [MaxLength(256)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(256)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }

    [MaxLength(512)]
    public string? HouseAddress { get; set; }

    [Required]
    [MaxLength(256)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public UserType UserType { get; set; }

    [Required]
    public long AuthId { get; set; }
    public Auth Auth { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; }
}

public class Admin : User, IDtoConvertible<AdminShowDto>
{
    public AdminShowDto CreateDto()
    {
        return new AdminShowDto
        {
            Id = this.Id,
            UserId = this.UserId,
            FirstName = this.FirstName,
            LastName = this.LastName,
            DateOfBirth = this.DateOfBirth,
            HouseAddress = this.HouseAddress,
            Email = this.Email,
            PhoneNumber = this.PhoneNumber,
            UserType = this.UserType
        };
    }
}

public class SchoolAdmin : User, IDtoConvertible<SchoolAdminShowDto>
{
    [Required]
    public long SchoolId { get; set; }
    public School School { get; set; }

    public SchoolAdminShowDto CreateDto()
    {
        return new SchoolAdminShowDto()
        {
            Id = this.Id,
            UserId = this.UserId,
            FirstName = this.FirstName,
            LastName = this.LastName,
            DateOfBirth = this.DateOfBirth,
            Email = this.Email,
            HouseAddress = this.HouseAddress,
            PhoneNumber = this.PhoneNumber,
            SchoolId = this.SchoolId,
            UserType = this.UserType
        };
    }
}

public class ViceChancellor : User, IDtoConvertible<ViceChancellorShowDto>
{
    public bool IsCurrent { get; set; }

    public ViceChancellorShowDto CreateDto()
    {
        return new ViceChancellorShowDto()
        {
            Id = this.Id,
            UserId = this.UserId,
            FirstName = this.FirstName,
            LastName = this.LastName,
            DateOfBirth = this.DateOfBirth,
            Email = this.Email,
            PhoneNumber = this.PhoneNumber,
            HouseAddress = this.HouseAddress,
            IsCurrent = this.IsCurrent,
            UserType = this.UserType
        };
    }
}

public class Faculty : User, IDtoConvertible<FacultyShowDto>
{
    [Required]
    public long DepartmentId { get; set; }
    public Department Department { get; set; }

    public IList<CourseCoordinator> CoursesCoordinated { get; set; }
    public IList<HeadOfDepartment> DepartmentsHeaded { get; set; }
    public IList<DeanOfSchool> SchoolsDeaned { get; set; }

    [Required]
    public FacultyRoles FacultyRoles { get; set; }

    [Required]
    public bool IsActive { get; set; }

    public FacultyShowDto CreateDto()
    {
        return new FacultyShowDto()
        {
            Id = this.Id,
            UserId = this.UserId,
            FirstName = this.FirstName,
            LastName = this.LastName,
            DateOfBirth = this.DateOfBirth,
            DepartmentId = this.DepartmentId,
            Email = this.Email,
            FacultyRoles = this.FacultyRoles,
            HouseAddress = this.HouseAddress,
            IsActive = this.IsActive,
            PhoneNumber = this.PhoneNumber,
            UserType = this.UserType
        };
    }
}

[Table("HeadOfDepartments_T")]
public class HeadOfDepartment : IDtoConvertible<HeadOfDepartmentShowDto>
{
    [Key]
    public long Id { get; set; }

    [Required]
    public long DepartmentId { get; set; }
    public Department Department { get; set; }

    [Required]
    public long FacultyId { get; set; }
    public Faculty Faculty { get; set; }

    [Required]
    public bool IsCurrent { get; set; }

    [Required]
    public DateTime HeadTill { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; }

    public HeadOfDepartmentShowDto CreateDto()
    {
        return new HeadOfDepartmentShowDto()
        {
            Id = this.Id,
            DepartmentId = this.DepartmentId,
            FacultyId = this.FacultyId,
            IsCurrent = this.IsCurrent,
            HeadTill = this.HeadTill
        };
    }
}

[Table("DeanOfSchools_T")]
public class DeanOfSchool : IDtoConvertible<DeanOfSchoolShowDto>
{
    [Key]
    public long Id { get; set; }

    [Required]
    public long SchoolId { get; set; }
    public School School { get; set; }

    [Required]
    public long FacultyId { get; set; }
    public Faculty Faculty { get; set; }

    [Required]
    public bool IsCurrent { get; set; }

    [Required]
    public DateTime DeanTill { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; }

    public DeanOfSchoolShowDto CreateDto()
    {
        return new DeanOfSchoolShowDto()
        {
            Id = this.Id,
            SchoolId = this.SchoolId,
            FacultyId = this.FacultyId,
            IsCurrent = this.IsCurrent,
            DeanTill = this.DeanTill
        };
    }
}

[Table("CourseCoordinators_T")]
public class CourseCoordinator
{
    [Key]
    public long Id { get; set; }

    [Required]
    public long CourseId { get; set; }
    public Course Course { get; set; }

    [Required]
    public long FacultyId { get; set; }
    public Faculty Faculty { get; set; }

    [Required]
    public bool IsCurrent { get; set; }

    [Required]
    public DateTime CourseCoordinatorTill { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; }
}

public class Student : User
{
    [Required]
    public long ProgramId { get; set; }
    public Program Program { get; set; }

    [Required]
    public bool IsActive { get; set; }
}
