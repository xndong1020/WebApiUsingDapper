namespace WebApi.Core
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; } // FK
    }
}
