using System.ComponentModel.DataAnnotations;
using API.DTOs.Qualification;

namespace API.DTOs.Student
{
    public class StudentsQualificationsDTO
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public int? Age { get; set; }

        public string? Address { get; set; }

        public List<QualificationDTO> Qualifications { get; set; }
    }
}
