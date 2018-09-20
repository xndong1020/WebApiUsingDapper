using System.Collections.Generic;
using WebApi.Core;

namespace WebApi.Service
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartment(int id, string relatedEntity);
        int InsertDepartment(Department user);
        int UpdateDepartment(Department user);
        int DeleteDepartment(int id);
    }
}
