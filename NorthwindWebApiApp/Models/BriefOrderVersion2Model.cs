namespace NorthwindWebApiApp.Models
{
    public class BriefOrderVersion2Model : BriefOrderModel
    {
        public string CustomerId { get; set; }

        public int? EmployeeId { get; set; }
    }
}
