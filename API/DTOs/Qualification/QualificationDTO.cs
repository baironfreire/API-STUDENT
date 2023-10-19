using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs.Qualification
{
    public class QualificationDTO
    {
        public int Id { get; set; }
        public string qualificationName { get; set; }

    }
}
