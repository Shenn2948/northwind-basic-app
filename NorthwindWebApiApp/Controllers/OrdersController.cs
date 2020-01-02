using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindWebApiApp.Models;
using NorthwindWebApiApp.Services;

namespace NorthwindWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BriefOrderModel>>> GetOrders()
        {
            return this.Ok(await this.orderService.GetOrdersAsync());
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<FullOrderModel>> GetOrder(int orderId)
        {
            return this.Ok(await this.orderService.GetOrderAsync(orderId));
        }
    }
}