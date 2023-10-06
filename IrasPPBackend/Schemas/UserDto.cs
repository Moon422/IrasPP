using System;
using System.ComponentModel.DataAnnotations;
using IrasPPBackend.Models;

namespace IrasPPBackend.Schemas;

public interface IModelConvertible<T>
{
    public T CreateModel();
}

public abstract class UserDto
{
    [Required]
    [MaxLength(30)]
    public string UserId { get; set; }

    [Required]
    [MaxLength(256)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(256)]
    public string LastName { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [MaxLength]
    public string? HouseAddress { get; set; }

    [Required]
    [MaxLength(256)]
    public string Email { get; set; }

    [Required]
    [MaxLength(14)]
    [MinLength(14)]
    public string PhoneNumber { get; set; }

    [Required]
    public UserType UserType { get; set; }
}

public class AdminCreateDto : UserDto, IModelConvertible<Admin>
{
    public Admin CreateModel()
    {
        return new Admin
        {
            FirstName = this.FirstName,
            LastName = this.LastName,
            DateOfBirth = this.DateOfBirth,
            HouseAddress = this.HouseAddress,
            Email = this.Email,
            PhoneNumber = this.PhoneNumber,
            UserType = this.UserType,
            UpdatedAt = DateTime.UtcNow
        };
    }
}

public class AdminShowDto : UserDto
{
    [Required]
    public long Id { get; set; }
}

public abstract class SchoolAdminDto : UserDto
{
    [Required]
    public long SchoolId { get; set; }
}

public class SchoolAdminCreateDto : SchoolAdminDto, IModelConvertible<SchoolAdmin>
{
    public SchoolAdmin CreateModel()
    {
        return new SchoolAdmin
        {
            UserId = this.UserId,
            FirstName = this.FirstName,
            LastName = this.LastName,
            DateOfBirth = this.DateOfBirth,
            HouseAddress = this.HouseAddress,
            Email = this.Email,
            PhoneNumber = this.PhoneNumber,
            UserType = this.UserType,
            SchoolId = this.SchoolId,
            UpdatedAt = DateTime.UtcNow
        };
    }
}

public class SchoolAdminShowDto : SchoolAdminDto
{
    [Required]
    public long Id { get; set; }
}

public abstract class ViceChancellorDto : UserDto
{
    [Required]
    public bool IsCurrent { get; set; }
}

public class ViceChancellorCreateDto : ViceChancellorDto, IModelConvertible<ViceChancellor>
{
    public ViceChancellor CreateModel()
    {
        return new ViceChancellor()
        {
            UserId = this.UserId,
            FirstName = this.FirstName,
            LastName = this.LastName,
            DateOfBirth = this.DateOfBirth,
            HouseAddress = this.HouseAddress,
            Email = this.Email,
            PhoneNumber = this.PhoneNumber,
            UserType = this.UserType,
            IsCurrent = this.IsCurrent,
            UpdatedAt = DateTime.UtcNow
        };
    }
}

public class ViceChancellorShowDto : ViceChancellorDto
{
    [Required]
    public long Id { get; set; }
}

public abstract class FacultyDto : UserDto
{
    [Required]
    public long DepartmentId { get; set; }

    [Required]
    public FacultyRoles FacultyRoles { get; set; }

    [Required]
    public bool IsActive { get; set; }
}

public class FacultyCreateDto : FacultyDto, IModelConvertible<Faculty>
{
    public Faculty CreateModel()
    {
        return new Faculty()
        {
            UserId = this.UserId,
            FirstName = this.FirstName,
            LastName = this.LastName,
            DateOfBirth = this.DateOfBirth,
            HouseAddress = this.HouseAddress,
            Email = this.Email,
            PhoneNumber = this.PhoneNumber,
            UserType = this.UserType,
            DepartmentId = this.DepartmentId,
            FacultyRoles = this.FacultyRoles,
            IsActive = this.IsActive,
            UpdatedAt = DateTime.UtcNow
        };
    }
}

public class FacultyShowDto : FacultyDto
{
    [Required]
    public long Id { get; set; }
}

public abstract class StudentDto : UserDto
{
    [Required]
    public long ProgramId { get; set; }

    [Required]
    public bool IsActive { get; set; }
}

public class StudentCreateDto : StudentDto, IModelConvertible<Student>
{
    public Student CreateModel()
    {
        return new Student()
        {
            UserId = this.UserId,
            FirstName = this.FirstName,
            LastName = this.LastName,
            DateOfBirth = this.DateOfBirth,
            HouseAddress = this.HouseAddress,
            Email = this.Email,
            PhoneNumber = this.PhoneNumber,
            UserType = this.UserType,
            ProgramId = this.ProgramId,
            IsActive = this.IsActive,
            UpdatedAt = DateTime.UtcNow
        };
    }
}

public class StudentShowDto : StudentDto
{
    [Required]
    public long Id { get; set; }
}

public abstract class HeadOfDepartmentDto
{
    [Required]
    public long DepartmentId { get; set; }

    [Required]
    public long FacultyId { get; set; }

    [Required]
    public bool IsCurrent { get; set; }

    [Required]
    public DateTime HeadTill { get; set; }
}

public class HeadOfDepartmentCreateDto : HeadOfDepartmentDto, IModelConvertible<HeadOfDepartment>
{
    public HeadOfDepartment CreateModel()
    {
        return new HeadOfDepartment()
        {
            DepartmentId = this.DepartmentId,
            FacultyId = this.FacultyId,
            IsCurrent = IsCurrent,
            HeadTill = this.HeadTill,
            UpdatedAt = DateTime.UtcNow
        };
    }
}

public class HeadOfDepartmentShowDto : HeadOfDepartmentDto
{
    [Key]
    public long Id { get; set; }
}

public abstract class DeanOfSchoolDto
{
    [Required]
    public long SchoolId { get; set; }

    [Required]
    public long FacultyId { get; set; }

    [Required]
    public bool IsCurrent { get; set; }

    [Required]
    public DateTime DeanTill { get; set; }
}

public class DeanOfSchoolCreateDto : DeanOfSchoolDto, IModelConvertible<DeanOfSchool>
{
    public DeanOfSchool CreateModel()
    {
        return new DeanOfSchool()
        {
            SchoolId = this.SchoolId,
            FacultyId = this.FacultyId,
            IsCurrent = this.IsCurrent,
            DeanTill = this.DeanTill,
            UpdatedAt = DateTime.UtcNow
        };
    }
}

public class DeanOfSchoolShowDto : DeanOfSchoolDto
{
    [Key]
    public long Id { get; set; }
}

public abstract class CourseCoordinatorDto
{
    [Required]
    public long CourseId { get; set; }

    [Required]
    public long FacultyId { get; set; }

    [Required]
    public bool IsCurrent { get; set; }

    [Required]
    public DateTime CourseCoordinatorTill { get; set; }
}

public class CourseCoordinatorCreateDto : CourseCoordinatorDto, IModelConvertible<CourseCoordinator>
{
    public CourseCoordinator CreateModel()
    {
        return new CourseCoordinator()
        {
            CourseId = this.CourseId,
            FacultyId = this.FacultyId,
            IsCurrent = this.IsCurrent,
            CourseCoordinatorTill = this.CourseCoordinatorTill,
            UpdatedAt = DateTime.UtcNow,
        };
    }
}

public class CourseCoordinatorShowDto : CourseCoordinatorDto
{
    [Required]
    public long Id { get; set; }
}
