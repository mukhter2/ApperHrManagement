using HrManagement.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagement.interfaces.Services
{
    public interface IEmployeeService
    {
        List<EmployeeModel> GetEmployees();
        EmployeeModel GetEmployeesById(string id);
        void SaveEmployees(EmployeeModel employeeModel);
        void DeleteEmployee(string id);

    }
}
