using ShipShop.Application.Models;
using ShipShop.Core.Entities;
using ShipShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderModel>> GetAll()
        {
            var orders = await _orderRepository.GetAll();
            List<OrderModel> result = new List<OrderModel>();
            result=orders.Select(x=>new OrderModel
            {
                OrderNumber=x.Id,
                CustomerName=x.Customer.FullName,
                ShippingAddress = x.ShippingAddress,
                OrderDate =x.CreatedOn.ToShortDateString(),
                DeliveryDate=x.DeliveryDate.ToString(), 
                Notes=x.Note,
                Feedback=x.Feedback,
                OrderStatus=x.LookupItem.Value,
                CustomerPhone=x.Phone,
                

            }).ToList();
            return result;
        }
        public async Task<OrderModel> GetById(int id)
        {
            var order = await _orderRepository.GetById(id);
            if (order == null)
                return null;
            OrderModel result = new OrderModel();
           result.OrderNumber=id;
            result.CustomerName=order.CustomerName;
            result.CustomerPhone=order.Phone;
            result.OrderDate = order.CreatedOn.ToString();
            result.ShippingAddress=order.ShippingAddress;
            result.DeliveryDate = order.DeliveryDate.ToString();
            result.OrderStatus = order.LookupItem.Value;
            result.Feedback=order.Feedback;
            result.Notes = order.Note;

            return result;
        }

        public async Task Delete(int id)
        {
            var order = await _orderRepository.GetById(id);
            if (order == null)
                throw new Exception($"No Order With The Given Id {id}");
            await _orderRepository.Delete(id);
        }
    }
}
