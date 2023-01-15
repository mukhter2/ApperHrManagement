using HrManagement.context.efcore;
using HrManagement.Models.Model;
using HrManagement.paging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagement.interfaces.Services
{
    public interface ILeaveService
    {
        List<LeaveModel> GetLeaves();
        Task<PageResponse<List<Leave>>> GetLeavePaging(PaginationFilter filter, HttpContext httpContext);
        Task<PageResponse<List<Leave>>> GetLeaveSearch(string value, PaginationFilter filter, HttpContext httpContext);
        LeaveModel GetLeavesById(string id);
        void SaveLeaves(LeaveModel LeaveModel);
        void DeleteLeave(string id);

    }
}
