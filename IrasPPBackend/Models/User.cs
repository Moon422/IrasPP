using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
public enum FacultyType
{
    FACULTY,
    COURSE_COORDINATOR,
    FACULTY_AND_COURSE_COORDINATOR = FACULTY | COURSE_COORDINATOR,
    HEAD_OF_DEPARTMENT,
    FACULTY_AND_HEAD_OF_DEPARTMENT = FACULTY | HEAD_OF_DEPARTMENT,
    FACULTY_AND_COURSE_COORDINATOR_AND_HEAD_OF_DEPARTMENT = FACULTY | COURSE_COORDINATOR | HEAD_OF_DEPARTMENT,
    DEAN_OF_SCHOOL,
    FACULTY_AND_DEAN_OF_SCHOOL = FACULTY | DEAN_OF_SCHOOL,
    FACULTY_AND_COURSE_COORDINATOR_AND_DEAN_OF_SCHOOL = FACULTY | COURSE_COORDINATOR | DEAN_OF_SCHOOL,
    FACULTY_AND_COURSE_COORDINATOR_AND_HEAD_OF_DEPARTMENT_AND_DEAN_OF_SCHOOL = FACULTY | COURSE_COORDINATOR | HEAD_OF_DEPARTMENT | DEAN_OF_SCHOOL
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
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; }
}

public class Admin : User
{

}

public class SchoolAdmin : User
{
    [Required]
    public long SchoolId { get; set; }
    public School School { get; set; }
}

public class ViceChancellor : User
{

}

public class Faculty : User
{
    [Required]
    public long FacultyToDepartmentId { get; set; }
    public Department FacultyToDepartment { get; set; }

    [Required]
    public FacultyType FacultyType { get; set; }

    [Required]
    public bool IsActive { get; set; }

}

public class HeadOfDepartment
{
    [Required]
    public long DepartmentId { get; set; }
    public Department Department { get; set; }

    [Required]
    public long FacultyId { get; set; }
    public Faculty Faculty { get; set; }

    [Required]
    public bool IsCurrent { get; set; }

    public DateTime? HeadTill { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; }
}

public class DeanOfSchool
{
    [Required]
    public long SchoolId { get; set; }
    public School School { get; set; }

    [Required]
    public long FacultyId { get; set; }
    public Faculty Faculty { get; set; }

    [Required]
    public bool IsCurrent { get; set; }

    public DateTime? DeanTill { get; set; }

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
