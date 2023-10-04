using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HouseRent.Shared.DTO;
using HouseRent.Shared.Models;

namespace HouseRent.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly FlatDbContext _context;

        public TenantsController(FlatDbContext context)
        {
            _context = context;
        }

        // GET: api/Tenants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tenant>>> GetTenants()
        {
            if (_context.Tenants == null)
            {
                return NotFound();
            }
            return await _context.Tenants.ToListAsync();
        }
        [HttpGet("DTO")]
        public async Task<ActionResult<IEnumerable<TenantDTO>>> GetTenantDTOs()
        {
            if (_context.Tenants == null)
            {
                return NotFound();
            }
            return await _context
                .Tenants.Include(x => x.Rents)
                .Select(c => new TenantDTO
                {
                    TenantID = c.TenantID,
                    TenantName = c.TenantName,
                    Address = c.Address,
                    Email = c.Email,
                    CanDelete = !c.Rents.Any()
                })
                .ToListAsync();
        }
        // GET: api/Tenants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tenant>> GetTenant(int id)
        {
            if (_context.Tenants == null)
            {
                return NotFound();
            }
            var customer = await _context.Tenants.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Tenants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTenant(int id, Tenant tenant)
        {
            if (id != tenant.TenantID)
            {
                return BadRequest();
            }

            _context.Entry(tenant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TenantExists(id))
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

        // POST: api/Tenants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tenant>> PostTenant(Tenant tenant)
        {
            if (_context.Tenants == null)
            {
                return Problem("Entity set 'FlatDbContext.Tenants'  is null.");
            }
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTenant", new { id = tenant.TenantID }, tenant);
        }

        // DELETE: api/Tenants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTenant(int id)
        {
            if (_context.Tenants == null)
            {
                return NotFound();
            }
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }

            _context.Tenants.Remove(tenant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TenantExists(int id)
        {
            return (_context.Tenants?.Any(e => e.TenantID == id)).GetValueOrDefault();
        }
    }
}
