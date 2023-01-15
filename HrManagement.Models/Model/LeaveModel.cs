namespace HrManagement.Models.Model
{
    public class LeaveModel
    {
        public string Id { get; set; }
        public string LeaveType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EmployeeId { get; set; } = string.Empty;
    }
}
