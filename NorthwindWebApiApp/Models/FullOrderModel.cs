namespace NorthwindWebApiApp.Models
{
  public class FullOrderModel : BriefOrderModel
  {
    public string CustomerId { get; set; }

    public int? EmployeeId { get; set; }

    public int? ShipVia { get; set; }
  }
}
