using test_backend.Data.DTOs;
using test_backend.Models;

namespace test_backend.Services;

public interface IOrderService
{
    Task<IEnumerable<ReadOrderDTO>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task<Order> CreateOrderAsync(CreateOrderDTO order);
    Task<Order> UpdateOrderAsync(Order order);
    Task<bool> DeleteOrderAsync(int id);
}