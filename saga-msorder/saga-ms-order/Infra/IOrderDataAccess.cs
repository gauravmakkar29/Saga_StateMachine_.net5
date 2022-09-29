using Saga_BlockSeat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saga_BlockSeat.Infra
{
    public interface IOrderDataAccess
    {
        List<OrderModel> GetAllOrder();
        
        void SaveOrder(OrderModel order);

        OrderModel GetOrder(Guid orderId);
        bool DeleteOrder(Guid orderId);
    }
}
