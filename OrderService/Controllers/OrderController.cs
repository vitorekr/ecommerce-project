using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Repositories;
using OrderService.MessageBus;
using OrderService.Events;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderRepository _repository;
    private readonly EventBusRabbitMQ _eventBus;

    public OrderController(OrderRepository repository)
    {
        _repository = repository;
        _eventBus = new EventBusRabbitMQ();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _repository.GetAllAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null)
            return NotFound();
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Order order)
    {
        await _repository.AddAsync(order);

        // Publica o evento no RabbitMQ
        var orderEvent = new OrderCreatedEvent
        {
            OrderId = order.Id,
            CustomerName = order.CustomerName,
            TotalPrice = order.TotalPrice
        };

        _eventBus.Publish(orderEvent, "order_created_queue");

        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Order order)
    {
        if (id != order.Id)
            return BadRequest();

        await _repository.UpdateAsync(order);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
