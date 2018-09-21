using AutoMapper;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Core;
using WebApi.Service;
using WebApi.Web.Models;

namespace WebApi.Web.Controllers
{
    [RoutePrefix("api/v1/Department")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentService _service;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        // GET api/Department/Get
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_service.GetDepartments());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return StatusCode(HttpStatusCode.ServiceUnavailable);
            }
        }

        // GET api/Department/Get
        [Route("")]
        [ActionName("Get")]
        [HttpGet]
        public IHttpActionResult GetWithPaging(int? pageNumber, int pageSize = 2)
        {
            try
            {
                IList<Department> departments;
                if (pageNumber.HasValue)
                {
                    departments = _service.GetDepartments()
                                          .Skip(pageSize * (pageNumber.Value - 1))
                                          .Take(pageSize)
                                          .ToList();

                }
                else
                    departments = _service.GetDepartments().ToList();

                return Ok(departments);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return StatusCode(HttpStatusCode.ServiceUnavailable);
            }

        }

        // GET api/Department/Get/id
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_service.GetDepartment(id, "Employees"));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return StatusCode(HttpStatusCode.ServiceUnavailable);
            }
        }

        // POST api/Department/POST
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(DepartmentDTO departmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var department = Mapper.Map<Department>(departmentDto);
                department.Id = _service.InsertDepartment(department);
                return Created("/api/Department/Get/" + department.Id, department);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return StatusCode(HttpStatusCode.ServiceUnavailable);
            }
        }

        // PUT api/Department/PUT
        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(DepartmentDTO departmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var department = Mapper.Map<Department>(departmentDto);
                var result = _service.UpdateDepartment(department);

                if (result == 0)
                    return NotFound();

                return Ok();
            }
            catch (Exception e)
            {
                logger.Error(e);
                return StatusCode(HttpStatusCode.ServiceUnavailable);
            }
        }

        // DELETE api/Department/Delete/id
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _service.DeleteDepartment(id);

                if (result == 0)
                    return NotFound();

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return StatusCode(HttpStatusCode.ServiceUnavailable);
            }
        }
    }
}
