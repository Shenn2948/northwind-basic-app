using System;

namespace NorthwindWebApiApp.Services
{
    public class BriefOrderDescription
    {
        public int OrderId { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }
    }
}
