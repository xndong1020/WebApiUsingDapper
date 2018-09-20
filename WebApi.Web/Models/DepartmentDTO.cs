using System.ComponentModel.DataAnnotations;

namespace WebApi.Web.Models
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string DepartmentHead { get; set; }
    }
}