namespace NorthwindWebApiApp.Services
{
    public class BriefOrderVersion2Description : BriefOrderDescription
    {
        public string CustomerId { get; set; }

        public int? EmployeeId { get; set; }
    }
}
