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
            List<Leave> response = new();
            var route = httpContext.Request.Path.Value;

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var dataList = _context.Leaves
                .OrderByDescending(q => q.Id)
        .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
        .Take(validFilter.PageSize)
        .ToList();

            dataList.ForEach(row => response.Add(new Leave()
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
        public async Task<PageResponse<List<Leave>>> GetLeaveSearch(string value, PaginationFilter filter, HttpContext httpContext)
        {
            List<LeaveModel> response = new();
            var route = httpContext.Request.Path.Value;

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            //Students.Where(x => x.Name.Contains(name)).ToList();
            var dataList = _context.Leaves.Where(x => x.Id.Contains(value) || x.LeaveType.Contains(value) || x.EmployeeId.Contains(value) || x.StartDate.ToString().Contains(value) || x.Description.Contains(value) || x.EndDate.ToString().Contains(value)).OrderByDescending(q => q.Id)
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
        public void SaveLeaves(LeaveModel leaveModel)
        {
            Leave dbTable = new();
            dbTable = _context.Leaves.Where(d => d.Id.Equals(leaveModel.Id))
                    .FirstOrDefault();
            if (dbTable != null)
            {
                dbTable.EmployeeId = leaveModel.EmployeeId;
                dbTable.LeaveType = leaveModel.LeaveType;
                dbTable.Description = leaveModel.Description;
                dbTable.StartDate = leaveModel.StartDate;
                dbTable.EndDate = leaveModel.EndDate;
            }
            else
            {
                //POST
                dbTable = new()
                {
                    Id = leaveModel.Id,
                    EmployeeId = leaveModel.EmployeeId,
                    LeaveType = leaveModel.LeaveType,
                    Description = leaveModel.Description,
                    StartDate = leaveModel.StartDate,
                    EndDate = leaveModel.EndDate
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

