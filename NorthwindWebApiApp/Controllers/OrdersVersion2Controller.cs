using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using NorthwindWebApiApp.Models;
using NorthwindWebApiApp.Services;

namespace NorthwindWebApiApp.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/orders")]
    public class OrdersVersion2Controller : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersVersion2Controller(IOrderService orderService)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BriefOrderVersion2Model>>> GetOrders()
        {
            return this.Ok(await this.orderService.GetExtendedOrdersAsync());
        }
    }
}
