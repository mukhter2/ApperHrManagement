using HrManagement.Models.Model;
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
        LeaveModel GetLeavesById(string id);
        void SaveLeaves(LeaveModel LeaveModel);
        void DeleteLeave(string id);

    }
}
