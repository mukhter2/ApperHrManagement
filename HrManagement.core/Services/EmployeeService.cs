using HrManagement.Models.Model;
using HrManagement.context.efcore;
using HrManagement.interfaces.Services;
using HrManagement.paging;
using Microsoft.EntityFrameworkCore;
using HrManagement.core.Helpers;
using Microsoft.AspNetCore.Http;
using System.Web;

using HrManagement.core.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using Microsoft.Extensions.DependencyInjection;
using System.Xml.Linq;

namespace HrManagement.core.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EF_DataContext _context;
        private readonly IUriService uriService;

        public EmployeeService(EF_DataContext context, IUriService uriService)
        {
            _context = context;
            this.uriService = uriService;
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <returns></returns>
        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> response = new();
            var dataList = _context.Employees.ToList();
            dataList.ForEach(row => response.Add(new EmployeeModel()
            {
                FirstName = row.FirstName,
                MiddleName = row.MiddleName,
                LastName = row.LastName,
                DateOfBirth = row.DateOfBirth,
                JoiningDate = row.JoiningDate,
                Designation=row.Designation,
                Department=row.Department,
                Id=row.Id
            }));
            return response;
        }
        public async Task<PageResponse<List<Employee>>> GetEmployeeSearch(string value, PaginationFilter filter, HttpContext httpContext)
        {
            List<EmployeeModel> response = new();
            var route = httpContext.Request.Path.Value;

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            //Students.Where(x => x.Name.Contains(name)).ToList();
            var dataList = _context.Employees.Where( x => x.Id.Contains(value) || x.Designation.Contains(value) || x.Department.Contains(value) || x.DateOfBirth.ToString().Contains(value) || x.FirstName.Contains(value) || x.MiddleName.Contains(value) || x.LastName.Contains(value) || x.JoiningDate.ToString().Contains(value)).OrderByDescending(q => q.Id)
        .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
        .Take(validFilter.PageSize)
        .ToList(); 
            dataList.ForEach(row => response.Add(new EmployeeModel()
            {
                FirstName = row.FirstName,
                MiddleName = row.MiddleName,
                LastName = row.LastName,
                DateOfBirth = row.DateOfBirth,
                JoiningDate = row.JoiningDate,
                Designation = row.Designation,
                Department = row.Department,
                Id = row.Id
            }));

            var totalRecords = _context.Employees.Count();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Employee>(dataList, validFilter, totalRecords, uriService, route);

            return pagedReponse;
        }
        public async Task<PageResponse<List<Employee>>> GetEmployeePaging(PaginationFilter filter, HttpContext httpContext)
        {
            List<EmployeeModel> response = new();
            
           var route = httpContext.Request.Path.Value;

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);


            /*            var dataList = _context.Employees.ToList();
            */
            var dataList = _context.Employees
                .OrderByDescending(q => q.Id)
        .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
        .Take(validFilter.PageSize)
        .ToList();
            
            dataList.ForEach(row => response.Add(new EmployeeModel()
            {
                FirstName = row.FirstName,
                MiddleName = row.MiddleName,
                LastName = row.LastName,
                DateOfBirth = row.DateOfBirth,
                JoiningDate = row.JoiningDate,
                Designation = row.Designation,
                Department = row.Department,
                Id = row.Id
            }));

            var totalRecords = _context.Employees.Count();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Employee>(dataList, validFilter, totalRecords, uriService,route);


            return pagedReponse;
        }
        public EmployeeModel GetEmployeesById(string id)
        {
            EmployeeModel response = new EmployeeModel();
            var row = _context.Employees.Where(d => d.Id.Equals(id)).FirstOrDefault();
            if (row == null){
                return null;
            }else
            {
                return new EmployeeModel()
                {
                    FirstName = row.FirstName,
                    MiddleName = row.MiddleName,
                    LastName = row.LastName,
                    DateOfBirth = row.DateOfBirth,
                    JoiningDate = row.JoiningDate,
                    Designation = row.Designation,
                    Department = row.Department,
                    Id = row.Id
                };
            }
            
        }
        /// <summary>
        /// It serves the POST/PUT/PATCH
        /// </summary>
        public void SaveEmployees(EmployeeModel employeeModel)
        {
            Employee dbTable = new();
            dbTable = _context.Employees.Where(d => d.Id.Equals(employeeModel.Id))
                    .FirstOrDefault();
            if (dbTable != null)
                {
                    dbTable.FirstName = employeeModel.FirstName;
                    dbTable.MiddleName = employeeModel.MiddleName;
                    dbTable.LastName = employeeModel.LastName;
                    dbTable.DateOfBirth = employeeModel.DateOfBirth;
                    dbTable.JoiningDate = employeeModel.JoiningDate;
                    dbTable.Designation = employeeModel.Designation;
                    dbTable.Department = employeeModel.Department;
                }else
            {
                //POST
                dbTable = new()
                {
                    Id = employeeModel.Id,
                    FirstName = employeeModel.FirstName,
                    MiddleName = employeeModel.MiddleName,
                    LastName = employeeModel.LastName,
                    DateOfBirth = employeeModel.DateOfBirth,
                    JoiningDate = employeeModel.JoiningDate,
                    Designation = employeeModel.Designation,
                    Department = employeeModel.Department
                };
                _context.Employees.Add(dbTable);
            }
            _context.SaveChanges();

        }
        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="id"></param>
        public void DeleteEmployee(string id)
        {
            var order = _context.Employees.Where(d => d.Id.Equals(id)).FirstOrDefault();
            if (order != null)
            {
                _context.Employees.Remove(order);
                _context.SaveChanges();
            }
        }

    }
}

