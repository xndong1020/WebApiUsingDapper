using AutoMapper;
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

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        // GET api/Department/Get
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_service.GetDepartments());
        }

        // GET api/Department/Get/id
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(_service.GetDepartment(id, "Employees"));
        }

        // POST api/Department/POST
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(DepartmentDTO departmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var department = Mapper.Map<Department>(departmentDto);
            department.Id = _service.InsertDepartment(department);
            return Created("/api/Department/Get/" + department.Id, department);
        }

        // PUT api/Department/PUT
        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(DepartmentDTO departmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var department = Mapper.Map<Department>(departmentDto);
            var result = _service.UpdateDepartment(department);

            if (result == 0)
                return NotFound();

            return Ok();
        }

        // DELETE api/Department/Delete/id
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var result = _service.DeleteDepartment(id);

            if (result == 0)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
