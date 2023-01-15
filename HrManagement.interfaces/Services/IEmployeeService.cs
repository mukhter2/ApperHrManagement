using HrManagement.Models.Model;
using HrManagement.paging;
using Microsoft.AspNetCore.Http;
using HrManagement.context.efcore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HrManagement.interfaces.Services
{
    public interface IEmployeeService
    {
        List<EmployeeModel> GetEmployees();
        Task<PageResponse<List<Employee>>> GetEmployeeSearch(string value, PaginationFilter filter, HttpContext httpContext);
        Task<PageResponse<List<Employee>>> GetEmployeePaging(PaginationFilter filter, HttpContext httpContext);
        EmployeeModel GetEmployeesById(string id);
        void SaveEmployees(EmployeeModel employeeModel);
        void DeleteEmployee(string id);

    }
}
