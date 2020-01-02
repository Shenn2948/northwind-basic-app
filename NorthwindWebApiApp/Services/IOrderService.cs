using System.Collections.Generic;
using System.Threading.Tasks;

using NorthwindWebApiApp.Models;

namespace NorthwindWebApiApp.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<BriefOrderModel>> GetOrdersAsync();

        Task<FullOrderModel> GetOrderAsync(int orderId);

        Task<IEnumerable<BriefOrderVersion2Model>> GetExtendedOrdersAsync();
    }
}
