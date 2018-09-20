using System.ComponentModel.DataAnnotations;

namespace WebApi.Web.Models
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public int DepartmentId { get; set; } // FK
    }
}