
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Student
{
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public int? Age { get; set; }

        public string? Address { get; set; }

    }
}
