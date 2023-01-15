using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrManagement.context.efcore
{
    [Table("Employee")]
    public class Employee
    {
        [Key, Required]
        public string Id { get; set; } =string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Designation { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;

    }


}
 