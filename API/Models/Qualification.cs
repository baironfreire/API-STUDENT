
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public partial class Qualification
{
    [Key]
    public int QualificationsId { get; set; }

    public int StudentId { get; set; }

    [StringLength(255)]
    public string? QualificationName { get; set; }
    [ForeignKey("StudentId")]
    public Student Student { get; set; }


}
