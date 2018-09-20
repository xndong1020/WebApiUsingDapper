using System.Collections.Generic;

namespace WebApi.Core
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string Location { get; set; }
        public string DepartmentHead { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
