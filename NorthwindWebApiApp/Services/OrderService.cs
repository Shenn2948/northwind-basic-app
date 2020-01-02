using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NorthwindModel;

using NorthwindWebApiApp.Configuration;
using NorthwindWebApiApp.Controllers;
using NorthwindWebApiApp.Models;

namespace NorthwindWebApiApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrdersController> logger;

        private readonly NorthwindEntities entities;

        public OrderService(IOptions<NorthwindServiceConfiguration> northwindServiceConfiguration, ILogger<OrdersController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            var uri = northwindServiceConfiguration == null ? throw new ArgumentNullException(nameof(northwindServiceConfiguration)) : northwindServiceConfiguration.Value.Uri;
            this.entities = new NorthwindEntities(uri);
        }

        public async Task<IEnumerable<BriefOrderModel>> GetOrdersAsync()
        {
            this.logger.LogDebug($"Getting data from ${this.entities.BaseUri.AbsoluteUri}.");

            var orderTaskFactory = new TaskFactory<IEnumerable<Order>>();

            var orders = await orderTaskFactory.FromAsync(this.entities.Orders.BeginExecute(null, null), iar => this.entities.Orders.EndExecute(iar));

            return orders.Select(o => new BriefOrderModel { OrderId = o.OrderID, OrderDate = o.OrderDate, RequiredDate = o.RequiredDate, }).ToArray();
        }

        public async Task<IEnumerable<BriefOrderVersion2Model>> GetExtendedOrdersAsync()
        {
            this.logger.LogDebug($"Getting data from ${this.entities.BaseUri.AbsoluteUri}.");

            var orderTaskFactory = new TaskFactory<IEnumerable<Order>>();

            var orders = await orderTaskFactory.FromAsync(this.entities.Orders.BeginExecute(null, null), iar => this.entities.Orders.EndExecute(iar));

            return orders.Select(o => new BriefOrderVersion2Model { CustomerId = o.CustomerID, EmployeeId = o.EmployeeID }).ToArray();
        }

        public async Task<FullOrderModel> GetOrderAsync(int orderId)
        {
            this.logger.LogDebug($"Getting data from ${this.entities.BaseUri.AbsoluteUri}.");

            var orderQueryTaskFactory = new TaskFactory<IEnumerable<Orders_Qry>>();
            var query = this.entities.Orders_Qries.AddQueryOption("$filter", $"OrderID eq {orderId}");

            var orders = (await orderQueryTaskFactory.FromAsync(query.BeginExecute(null, null), iar => query.EndExecute(iar))).ToArray();

            var order = orders.FirstOrDefault();

            if (order == null)
            {
                return null;
            }

            return new FullOrderModel
                   {
                       OrderId = order.OrderID,
                       CustomerId = order.CustomerID,
                       EmployeeId = order.EmployeeID,
                       OrderDate = order.OrderDate,
                       RequiredDate = order.RequiredDate,
                       ShipVia = order.ShipVia,
                   };
        }
    }
}
