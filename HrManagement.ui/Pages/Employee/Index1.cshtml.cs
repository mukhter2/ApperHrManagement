using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HrManagement.Models.Model;

namespace HrManagement.ui.Employee
{
    public class Index1Model :  PageModel
    {
        public void OnGet()
        {
            ViewData["heading"] = "Welcome to ASP.NET Core Razor Pages !!";

        }
    }
}
