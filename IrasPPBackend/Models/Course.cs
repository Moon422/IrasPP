using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IrasPPBackend.Models;

[Table("Courses_T")]
public class Course
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(256)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int CourseCode { get; set; }

    [Required]
    public long ProgramId { get; set; }
    public Program Program { get; set; }

    public IList<CourseCoordinator> Coordinators { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; }
}
