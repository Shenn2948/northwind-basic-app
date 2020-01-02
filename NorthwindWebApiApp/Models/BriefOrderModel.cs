using System;

namespace NorthwindWebApiApp.Models
{
  public class BriefOrderModel
  {
    public int OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? RequiredDate { get; set; }
  }
}
