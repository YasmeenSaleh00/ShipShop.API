using ShipShop.Application.Commands;
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
        private readonly ICartRepository _cartRepository;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
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
                TotalPrice=x.TotalPrice,
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
           result.OrderNumber=order.Id;
            result.CustomerName=order.CustomerName;
            result.CustomerPhone=order.Phone;
            result.OrderDate = order.CreatedOn.ToString();
            result.ShippingAddress=order.ShippingAddress;
            result.DeliveryDate = order.DeliveryDate.ToString();
            result.OrderStatus = order.LookupItem.Value;
            result.TotalPrice= order.TotalPrice;
            result.Items = order.Items.Select(x=>new OrderItemModel
            {
                ProductName = x.Product.Name,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice
            }).ToList();    
        
          
            result.Notes = order.Note;

            return result;
        }

        public async Task<List<OrderModel>> GetAllOrdersByCustomerId(int customerId)
        {
            var orders = await _orderRepository.GetAllOrdersByCustomerId(customerId);
            List<OrderModel> result = new List<OrderModel>();
            result = orders.Select(x => new OrderModel
            {
                OrderNumber = x.Id,
                CustomerName = x.Customer.FullName,
                ShippingAddress = x.ShippingAddress,
                OrderDate = x.CreatedOn.ToShortDateString(),
                DeliveryDate = x.DeliveryDate.ToString(),
                Notes = x.Note,
                TotalPrice = x.TotalPrice,

                OrderStatus = x.LookupItem.Value,
                CustomerPhone = x.Phone,
                Items = x.Items.Select(i => new OrderItemModel
                {
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),



            }).ToList();
            return result;
        }

        public async Task Delete(int id)
        {
            var order = await _orderRepository.GetById(id);
            if (order == null)
                throw new Exception($"No Order With The Given Id {id}");
            await _orderRepository.Delete(id);
        }
        public async Task<int> CreateOrder(OrderCommand command , int customerId)
        {
            var cart = await _cartRepository.GetCartByCustomerAsync(customerId);
            if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
                throw new Exception("Cart is empty or not found.");

            var order = new Order
            {
                CustomerId = cart.CustomerId,
                CustomerName = command.CustomerName,
                Phone = command.Phone,
                ShippingAddress = command.ShippingAddress,
                DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(3)),
                Note = command.Note,
                CartId = cart.Id,
                OrderStatusId = 5, 
                TotalPrice = cart.CartItems.Sum(item => item.Quantity * item.Product.Price),
                Items = cart.CartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Product.Price
                }).ToList()
            };

            var orderId = await _orderRepository.Add(order);

            await _cartRepository.ClearCartAsync(cart.Id); 

            return orderId;
        }

    }
}
