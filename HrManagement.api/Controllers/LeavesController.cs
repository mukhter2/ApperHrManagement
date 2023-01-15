using HrManagement.Models.Model;
using HrManagement.core.Service;

using HrManagement.paging;
using HrManagement.context.efcore;
using Microsoft.AspNetCore.Mvc;
using HrManagement.interfaces.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaveManagement.db.Controllers
{
    [ApiController]
    public class LeavesController : ControllerBase
    {
        private readonly LeaveService _db;
        private readonly IUriService uriService;

        public LeavesController(EF_DataContext eF_DataContext,IUriService uriService)
        {
            _db = new LeaveService(eF_DataContext,uriService);
            this.uriService = uriService;
        }
        // GET: api/<LeavesController>
        [HttpGet]
        [Route("api/[controller]/GetLeaves")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<LeaveModel> data = _db.GetLeaves();
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
        [Route("api/[controller]/GetLeaves/pageNumber={pageNumber}&pageSize={pageSize}")]
        public IActionResult GetPaging(int pageNumber, int pageSize)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                var validFilter = new PaginationFilter(pageNumber, pageSize);

                Task<PageResponse<List<Leave>>> data = _db.GetLeavePaging(validFilter, HttpContext);
                
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        // GET api/<LeavesController>/5
        [HttpGet]
        [Route("api/[controller]/GetLeaveById/{id}")]
        public IActionResult Get(string id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                LeaveModel data = _db.GetLeavesById(id);
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

        // POST api/<LeavesController>
        [HttpPost]
        [Route("api/[controller]/SaveLeave")]
        public IActionResult Post([FromBody] LeaveModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveLeaves(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/<LeavesController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateLeave")]

        public IActionResult Put([FromBody] LeaveModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveLeaves(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/<LeavesController>/5
        [HttpDelete("{id}")]
        [Route("api/[controller]/DeleteLeave/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.DeleteLeave(id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
