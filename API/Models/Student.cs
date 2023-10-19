using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public partial class Student
{
    [Key]
    public int StudentId { get; set; }

    [Required]
    [StringLength(50)]
    public string? Name { get; set; }

    public int? Age { get; set; }

    [StringLength(50)]
    public string? Address { get; set; }

    public List<Qualification> Qualifications { get; set; }

}
