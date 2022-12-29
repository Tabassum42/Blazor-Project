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
    public class MetarialsController : ControllerBase
    {
        private readonly ClientDbContext _context;
        private readonly IWebHostEnvironment env;
        public MetarialsController(ClientDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: api/Metarials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Metarials>>> GetMetarials()
        {
          if (_context.Metarials == null)
          {
              return NotFound();
          }
            return await _context.Metarials.ToListAsync();
        }

        // GET: api/Metarials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Metarials>> GetMetarials(int id)
        {
          if (_context.Metarials == null)
          {
              return NotFound();
          }
            var metarials = await _context.Metarials.FindAsync(id);

            if (metarials == null)
            {
                return NotFound();
            }

            return metarials;
        }

        // PUT: api/Metarials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetarials(int id, Metarials metarials)
        {
            if (id != metarials.MetarialID)
            {
                return BadRequest();
            }

            _context.Entry(metarials).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetarialsExists(id))
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

        // POST: api/Metarials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Metarials>> PostMetarials(Metarials metarials)
        {
          if (_context.Metarials == null)
          {
              return Problem("Entity set 'ClientDbContext.Metarials'  is null.");
          }
            _context.Metarials.Add(metarials);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMetarials", new { id = metarials.MetarialID }, metarials);
        }
        [HttpPost("Upload")]
        public async Task<ImageUploadResponse> Upload(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName);
            var randomFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            var storedFileName = randomFileName + ext;
            using FileStream fs = new FileStream(Path.Combine(env.WebRootPath, "Uploads", storedFileName), FileMode.Create);
            await file.CopyToAsync(fs);
            fs.Close();
            return new ImageUploadResponse { FileName = file.FileName, StoredFileName = storedFileName };
        }

        // DELETE: api/Metarials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetarials(int id)
        {
            if (_context.Metarials == null)
            {
                return NotFound();
            }
            var metarials = await _context.Metarials.FindAsync(id);
            if (metarials == null)
            {
                return NotFound();
            }

            _context.Metarials.Remove(metarials);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MetarialsExists(int id)
        {
            return (_context.Metarials?.Any(e => e.MetarialID == id)).GetValueOrDefault();
        }
    }
}
