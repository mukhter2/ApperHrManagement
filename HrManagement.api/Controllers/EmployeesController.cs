using HrManagement.Models.Model;
using HrManagement.core.Service;

using HrManagement.context.efcore;
using Microsoft.AspNetCore.Mvc;
using HrManagement.paging;
using HrManagement.interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HrManagement.db.Controllers
{
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _db;
        private readonly IUriService uriService;

        public EmployeesController(EF_DataContext eF_DataContext, IUriService uriService)
        {
            _db = new EmployeeService(eF_DataContext,uriService);
             this.uriService= uriService;
        }
        // GET: api/<EmployeesController>
        [HttpGet]
        [Route("api/[controller]/GetEmployees")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<EmployeeModel> data = _db.GetEmployees();
                if (!data.Any())
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }


        [HttpGet]
        [Route("api/[controller]/GetEmployees/pageNumber={pageNumber}&pageSize={pageSize}")]
        public IActionResult GetPaging(int pageNumber,int pageSize)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                var validFilter = new PaginationFilter(pageNumber, pageSize);

                Task<PageResponse<List<Employee>>> data = _db.GetEmployeePaging(validFilter, HttpContext);
                
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }


        // GET api/<EmployeesController>/5
        [HttpGet]
        [Route("api/[controller]/GetEmployeeById/{id}")]
        public IActionResult Get(string id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                EmployeeModel data = _db.GetEmployeesById(id);
                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/<EmployeesController>
        [HttpPost]
        [Route("api/[controller]/SaveEmployee")]
        public IActionResult Post([FromBody] EmployeeModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveEmployees(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/<EmployeesController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateEmployee")]

        public IActionResult Put([FromBody] EmployeeModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveEmployees(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        [Route("api/[controller]/DeleteEmployee/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.DeleteEmployee(id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
