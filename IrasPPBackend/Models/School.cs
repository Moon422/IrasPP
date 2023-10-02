using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IrasPPBackend.Models;

[Table("Schools_T")]
public class School
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(256)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(256)]
    public string SchoolCode { get; set; } = string.Empty;

    public IList<SchoolAdmin> SchoolAdmins { get; set; }
    public IList<DeanOfSchool> DeansOfSchool { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; }
}
