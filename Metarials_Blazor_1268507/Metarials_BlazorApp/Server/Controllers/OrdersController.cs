using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Metarials_BlazorApp.Shared.Models;
using Metarials_BlazorApp.Shared.DTO;

namespace Metarials_BlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ClientDbContext _context;

        public OrdersController(ClientDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            return await _context.Orders.ToListAsync();
        }
        [HttpGet("DTO")]
        public async Task<ActionResult<IEnumerable<OrderViewDTO>>> GetOrderDTOs()
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            return await _context.Orders
                .Include(o => o.Clients)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Metarials)
                .Select(o =>
                    new OrderViewDTO
                    {
                        OrderID = o.OrderID,
                        OrderDate = o.OrderDate,
                        DeliveryDate = o.DeliveryDate,
                        ClientName = o.Clients.ClientName,
                        Status = o.Status,
                        ItemCount = o.OrderItems.Count,
                        OrderValue = o.OrderItems.Sum(x => x.Metarials.Price * x.Quantity)

                    })
                .ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;

        }
        [HttpGet("DTO/{id}")]
        public async Task<ActionResult<OrderViewDTO>> GetOrderViewDTO(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.Include(o => o.Clients)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Metarials).FirstOrDefaultAsync(o => o.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            return new OrderViewDTO
            {
                OrderID = order.OrderID,
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                ClientName = order.Clients.ClientName,
                Status = order.Status,
                ItemCount = order.OrderItems.Count,
                OrderValue = order.OrderItems.Sum(x => x.Metarials.Price * x.Quantity)

            };
        }
        [HttpGet("Items/{id}")]
        public async Task<ActionResult<IEnumerable<OrderItemViewDTO>>> GetOrderItems(int id)
        {
            if (_context.OrderItems == null)
            {
                return NotFound();
            }
            var orderitems = await _context.OrderItems.Include(x => x.Metarials).Where(oi => oi.OrderID == id).ToListAsync();

            if (orderitems == null)
            {
                return NotFound();
            }

            return orderitems.Select(oi => new OrderItemViewDTO { OrderID = oi.OrderID, MetarialName = oi.Metarials.MetarialName, Price = oi.Metarials.Price, Quantity = oi.Quantity }).ToList();
        }
        [HttpGet("OI/{id}")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItemsOf(int id)
        {
            if (_context.OrderItems == null)
            {
                return NotFound();
            }
            var orderitems = await _context.OrderItems.Where(oi => oi.OrderID == id).ToListAsync();

            if (orderitems == null)
            {
                return NotFound();
            }

            return orderitems;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderID)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPut("DTO/{id}")]
        public async Task<IActionResult> PutOrderWithOrderItem(int id, OrderEditDTO order)
        {
            if (id != order.OrderID)
            {
                return BadRequest();
            }
            var existing = await _context.Orders.Include(x => x.OrderItems).FirstAsync(o => o.OrderID == id);
            _context.OrderItems.RemoveRange(existing.OrderItems);
            existing.OrderID = order.OrderID;
            existing.OrderDate = order.OrderDate;
            existing.DeliveryDate = order.DeliveryDate;
            existing.ClientID = order.ClientID;
            existing.Status = order.Status;
            foreach (var item in order.OrderItems)
            {
                _context.OrderItems.Add(new OrderItem { OrderID = order.OrderID, MetarialID = item.MetarialID, Quantity = item.Quantity });
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.InnerException?.Message);

            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
          if (_context.Orders == null)
          {
              return Problem("Entity set 'ClientDbContext.Orders'  is null.");
          }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderID }, order);
        }
        [HttpPost("DTO")]
        public async Task<ActionResult<Order>> PostOrderDTO(OrderDTO dto)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'ClientDbContext.Orders'  is null.");
            }
            var order = new Order {  ClientID= dto.ClientID, OrderDate=dto.OrderDate, DeliveryDate=dto.DeliveryDate, Status=dto.Status };
            foreach(var oi in dto.OrderItems)
            {
                order.OrderItems.Add(new OrderItem { MetarialID = oi.MetarialID, Quantity = oi.Quantity });
            }
            _context.Orders.Add(order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //throw ex.InnerException;
            }
           

            return order;
        }
        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderID == id)).GetValueOrDefault();
        }
    }
}
