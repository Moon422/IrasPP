using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IrasPPBackend.Models;

[Table("Programs_T")]
public class Program
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(256)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(256)]
    public string ShortCode { get; set; } = string.Empty;

    [Required]
    public long DepartmentId { get; set; }
    public Department Department { get; set; }

    public IList<Student> Students { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; }
}
