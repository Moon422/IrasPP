using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IrasPPBackend.Models;

[Table("Departments_T")]
public class Department
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(256)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public long SchoolId { get; set; }
    public School School { get; set; }

    public IList<Faculty> Faculties { get; set; }
    public IList<Faculty> HeadOfDepartments { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; }
}
