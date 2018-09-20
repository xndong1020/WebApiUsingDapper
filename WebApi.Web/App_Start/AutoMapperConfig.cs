using AutoMapper;
using System.Collections.Generic;
using WebApi.Core;

namespace WebApi.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // 设置AutoMapper 的类型转换
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Department, Models.DepartmentDTO>();
                cfg.CreateMap<Employee, Models.EmployeeDTO>();
                cfg.CreateMap<IList<Department>, IList<Models.DepartmentDTO>>();
                cfg.CreateMap<IList<Employee>, IList<Models.EmployeeDTO>>();
                cfg.CreateMap<Models.DepartmentDTO, Department>();
                cfg.CreateMap<Models.EmployeeDTO, Employee>();
            });
        }
    }
}