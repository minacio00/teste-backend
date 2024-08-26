using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using test_backend.Data.DTOs;
using test_backend.Models;
using test_backend.Services;

namespace test_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    // GET: api/Order
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadOrderDTO>>> GetOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    // GET: api/Order/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    // POST: api/Order
    [HttpPost]
    public async Task<ActionResult<Order>> PostOrder(CreateOrderDTO createOrderDTO)
    {
        try
        {
            var order = _mapper.Map<Order>(createOrderDTO);
            var createdOrder = await _orderService.CreateOrderAsync(createOrderDTO);
            var orderDto = _mapper.Map<ReadOrderDTO>(createdOrder);

            return CreatedAtAction(nameof(GetOrder), new { id = orderDto.Id }, orderDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/Order/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(int id, Order order)
    {
        if (id != order.Id)
        {
            return BadRequest();
        }

        var updatedOrder = await _orderService.UpdateOrderAsync(order);

        if (updatedOrder == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/Order/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var success = await _orderService.DeleteOrderAsync(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
