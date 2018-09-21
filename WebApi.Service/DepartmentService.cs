using System;
using System.Collections.Generic;
using WebApi.Core;
using WebApi.Repository;

namespace WebApi.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repo;

        public DepartmentService(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Department> GetDepartments(string searchKeyword, int pageNumber, int pageSize)
        {
            return _repo.GetAll(searchKeyword, pageNumber, pageSize);
        }

        public Department GetDepartment(int id, string relatedEntity)
        {
            return _repo.Get(id, relatedEntity);
        }

        public int InsertDepartment(Department department)
        {
            return _repo.Insert(department);
        }

        public int UpdateDepartment(Department department)
        {
           return  _repo.Update(department);
        }

        public int DeleteDepartment(int id)
        {
            return _repo.Delete(id);
        }
    }
}
