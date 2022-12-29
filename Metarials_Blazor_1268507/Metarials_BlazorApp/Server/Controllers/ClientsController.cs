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
    public class ClientsController : ControllerBase
    {
        private readonly ClientDbContext _context;

        public ClientsController(ClientDbContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clients>>> GetClients()
        {
          if (_context.Clients == null)
          {
              return NotFound();
          }
            return await _context.Clients.ToListAsync();
        }
        [HttpGet("DTO")]
        public async Task<ActionResult<IEnumerable<ClientsDTO>>> GetClientDTOs()
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            return await _context
                .Clients.Include(x => x.Orders)
                .Select(c => new ClientsDTO
                {
                    ClientID = c.ClientID,
                    ClientName = c.ClientName,
                    Address = c.Address,
                    Email = c.Email,
                    CanDelete = !c.Orders.Any()
                })
                .ToListAsync();
        }
        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clients>> GetClients(int id)
        {
          if (_context.Clients == null)
          {
              return NotFound();
          }
            var clients = await _context.Clients.FindAsync(id);

            if (clients == null)
            {
                return NotFound();
            }

            return clients;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClients(int id, Clients clients)
        {
            if (id != clients.ClientID)
            {
                return BadRequest();
            }

            _context.Entry(clients).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientsExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Clients>> PostClients(Clients clients)
        {
          if (_context.Clients == null)
          {
              return Problem("Entity set 'ClientDbContext.Clients'  is null.");
          }
            _context.Clients.Add(clients);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClients", new { id = clients.ClientID }, clients);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClients(int id)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            var clients = await _context.Clients.FindAsync(id);
            if (clients == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(clients);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientsExists(int id)
        {
            return (_context.Clients?.Any(e => e.ClientID == id)).GetValueOrDefault();
        }
    }
}
