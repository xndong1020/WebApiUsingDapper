using System.Collections.Generic;
using WebApi.Core;

namespace WebApi.Repository
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(string searchKeyword, int pageNumber, int pageSize);
        Department Get(int id, string relatedEntity = "");
        int Insert(Department department);
        int Update(Department department);
        int Delete(int id);
    }
}
