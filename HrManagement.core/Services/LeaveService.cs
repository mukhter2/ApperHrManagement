using HrManagement.context.efcore;
using HrManagement.core.Helpers;
using HrManagement.interfaces.Services;
using HrManagement.Models.Model;
using HrManagement.paging;
using Microsoft.AspNetCore.Http;

namespace HrManagement.core.Service
{
    public class LeaveService : ILeaveService
    {
        private EF_DataContext _context;
        private readonly IUriService uriService;

        public LeaveService(EF_DataContext context, IUriService uriService)
        {
            _context = context;
            this.uriService = uriService;
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <returns></returns>
        public List<LeaveModel> GetLeaves()
        {
            List<LeaveModel> response = new List<LeaveModel>();
            var dataList = _context.Leaves.ToList();
            dataList.ForEach(row => response.Add(new LeaveModel()
            {
                Id=row.Id,
                EmployeeId = row.EmployeeId,
                LeaveType=row.LeaveType,
                Description=row.Description,
                StartDate=row.StartDate,
                EndDate=row.EndDate,

            }));
            return response;
        }


        public async Task<PageResponse<List<Leave>>> GetLeavePaging(PaginationFilter filter, HttpContext httpContext)
        {
            List<LeaveModel> response = new();
            var route = httpContext.Request.Path.Value;

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var dataList = _context.Leaves
                .OrderByDescending(q => q.Id)
        .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
        .Take(validFilter.PageSize)
        .ToList();

            dataList.ForEach(row => response.Add(new LeaveModel()
            {
                Id = row.Id,
                EmployeeId = row.EmployeeId,
                LeaveType = row.LeaveType,
                Description = row.Description,
                StartDate = row.StartDate,
                EndDate = row.EndDate,

            }));
            var totalRecords = _context.Employees.Count();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Leave>(dataList, validFilter, totalRecords, uriService, route);


            return pagedReponse;
        }
        public LeaveModel GetLeavesById(string id)
        {
            LeaveModel response = new LeaveModel();
            var row = _context.Leaves.Where(d => d.Id.Equals(id)).FirstOrDefault();
            if (row == null)
            {
                return null;
            }
            else
            {
                return new LeaveModel()
                {
                    EmployeeId = row.EmployeeId,
                    LeaveType = row.LeaveType,
                    Description = row.Description,
                    StartDate = row.StartDate,
                    EndDate = row.EndDate,
                    Id = row.Id
                };

            } 
            
        }
        /// <summary>
        /// It serves the POST/PUT/PATCH
        /// </summary>
        public void SaveLeaves(LeaveModel LeaveModel)
        {
            Leave dbTable = new();
            dbTable = _context.Leaves.Where(d => d.Id.Equals(LeaveModel.Id))
                    .FirstOrDefault();
            if (dbTable != null)
            {
                dbTable.EmployeeId = LeaveModel.EmployeeId;
                dbTable.LeaveType = LeaveModel.LeaveType;
                dbTable.Description = LeaveModel.Description;
                dbTable.StartDate = LeaveModel.StartDate;
                dbTable.EndDate = LeaveModel.EndDate;
            }
            else
            {
                //POST
                dbTable = new()
                {
                    Id = LeaveModel.Id,
                    EmployeeId = LeaveModel.EmployeeId,
                    LeaveType = LeaveModel.LeaveType,
                    Description = LeaveModel.Description,
                    StartDate = LeaveModel.StartDate,
                    EndDate = LeaveModel.EndDate
                };
                _context.Leaves.Add(dbTable);

            }
            _context.SaveChanges();
            
            
        }
        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="id"></param>
        public void DeleteLeave(string id)
        {
            var order = _context.Leaves.Where(d => d.Id.Equals(id)).FirstOrDefault();
            if (order != null)
            {
                _context.Leaves.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}

