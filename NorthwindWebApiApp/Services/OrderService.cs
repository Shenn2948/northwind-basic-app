using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NorthwindWebApiApp.Models;

namespace NorthwindWebApiApp.Services
{
  public class OrderService : IOrderService
  {
    private readonly NorthwindModel.NorthwindEntities entities;

    public OrderService(IOptions<Configuration.NorthwindServiceConfiguration> northwindServiceConfiguration)
    {
      var uri = northwindServiceConfiguration == null ? throw new ArgumentNullException(nameof(northwindServiceConfiguration)) : northwindServiceConfiguration.Value.Uri;
      this.entities = new NorthwindModel.NorthwindEntities(uri);
    }

    public async Task<IEnumerable<BriefOrderModel>> GetOrdersAsync()
    {
      var orderTaskFactory = new TaskFactory<IEnumerable<NorthwindModel.Order>>();

      var orders = await orderTaskFactory.FromAsync(
          this.entities.Orders.BeginExecute(null, null),
          iar => this.entities.Orders.EndExecute(iar));

      return orders.Select(o => new BriefOrderModel
      {
        OrderId = o.OrderID,
        OrderDate = o.OrderDate,
        RequiredDate = o.RequiredDate,
      }).ToArray();
    }

    public async Task<FullOrderModel> GetOrderAsync(int orderId)
    {
      var orderQueryTaskFactory = new TaskFactory<IEnumerable<NorthwindModel.Orders_Qry>>();
      var query = this.entities.Orders_Qries.AddQueryOption("$filter", $"OrderID eq {orderId}");

      var orders = (await orderQueryTaskFactory.FromAsync(
          query.BeginExecute(null, null),
          iar => query.EndExecute(iar))).ToArray();

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
