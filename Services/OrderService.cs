using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using test_backend.Data;
using test_backend.Data.DTOs;
using test_backend.Models;

namespace test_backend.Services
{
    public class OrderService : IOrderService
    {
        private readonly TestBackendContext _context;
        private readonly IMapper _mapper;

        public OrderService(TestBackendContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadOrderDTO>> GetAllOrdersAsync()
        {
             var orders = await _context.Orders
                .Include(o => o.ProductOrders)
                .ThenInclude(po => po.Product)
                .Where(o => o.DeletedAt == null)
                .ToListAsync();

            // Map orders to ReadOrderDTO
            var orderDtos = _mapper.Map<IEnumerable<ReadOrderDTO>>(orders);

            return orderDtos;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.ProductOrders)
                .ThenInclude(po => po.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> CreateOrderAsync(CreateOrderDTO orderDto)
        {
            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                Status = "Em preparação",
                TotalAmount = 0 //
            };

            foreach (var productQuantity in orderDto.Products)
            {
                var product = await _context.Products.FindAsync(productQuantity.ProductId);
                if (product == null || product.StockQuantity < productQuantity.Quantity)
                {
                    throw new Exception("Produto não disponível, ou estoque insuficiente.");
                }

                order.ProductOrders.Add(new ProductOrder
                {
                    ProductId = productQuantity.ProductId,
                    Quantity = productQuantity.Quantity,
                    Product = product
                });

                order.TotalAmount += (product.Price ?? 0) * productQuantity.Quantity;
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            order.DeletedAt = DateTime.UtcNow;
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
