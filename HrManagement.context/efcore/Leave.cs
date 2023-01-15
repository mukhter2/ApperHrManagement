using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrManagement.context.efcore    
{
    [Table("Leave")]
    public class Leave
    {
        [Key,Required]
        public string Id { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string LeaveType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }



    }
}
