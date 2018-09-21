using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using WebApi.Core;

namespace WebApi.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;

        public IEnumerable<Department> GetAll(string searchKeyword, int pageNumber, int pageSize)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                var sql = @"SELECT * FROM tblDepartment WHERE DepartmentName LIKE N'%" + searchKeyword + "%' OR Location LIKE N'%" + searchKeyword + "%' OR DepartmentHead LIKE N'%" + searchKeyword + "%'";
                // 返回所有的departments 
                return db.Query<Department>(sql)
                         .Skip(pageSize * (pageNumber - 1))
                         .Take(pageSize);
            }
        }

        public Department Get(int id, string relatedEntity = "")
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                if (!string.IsNullOrEmpty(relatedEntity) && string.Compare("Employees", "Employees", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // 找到指定的department
                    var department = db.QueryFirst<Department>("SELECT * FROM tblDepartment WHERE Id = @Id", new { Id = id });
                    // 找到属于该department的 所有 employees
                    var employees = db.Query<Employee>("SELECT * FROM tblEmployee WHERE DepartmentId = @Id", new { Id = id }).AsList();
                    department.Employees = employees;
                    return department;
                }
                return db.QueryFirst<Department>("SELECT * FROM tblDepartment WHERE Id = @Id", new { Id = id });
            }
        }

        public int Insert(Department department)
        {
            var sql = @"INSERT INTO tblDepartment(DepartmentName, Location, DepartmentHead) VALUES(@departmentName, @location, @departmentHead); SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                return db.QueryFirst<int>(sql, new { departmentName = department.DepartmentName, location = department.Location, departmentHead = department.DepartmentHead });
            }
        }

        public int Update(Department department)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                return db.Execute("UPDATE tblDepartment SET DepartmentName=@departmentName, Location=@location, DepartmentHeaD=@departmentHead WHERE Id=@Id",
                    new { departmentName = department.DepartmentName, location = department.Location, departmentHead = department.DepartmentHead, Id = department.Id });
            }
        }

        public int Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                return db.Execute("DELETE tblDepartment WHERE Id=@Id", new { Id = id });
            }
        }
    }
}
