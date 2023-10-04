using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HouseRent.Shared.DTO;
using HouseRent.Shared.Models;

namespace HouseRent.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        private readonly FlatDbContext _context;

        public RentsController(FlatDbContext context)
        {
            _context = context;
        }

        // GET: api/Rents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rent>>> GetRents()
        {
            if (_context.Rents == null)
            {
                return NotFound();
            }
            return await _context.Rents.ToListAsync();
        }
        [HttpGet("DTO")]
        public async Task<ActionResult<IEnumerable<RentViewDTO>>> GetRentDTOs()
        {
            if (_context.Rents == null)
            {
                return NotFound();
            }
            return await _context.Rents
                .Include(o => o.Tenant)
                .Include(o => o.RentItems).ThenInclude(oi => oi.Flat)
                .Select(o =>
                    new RentViewDTO
                    {
                        RentID = o.RentID,
                        RentDate = o.RentDate,
                        BookedDate = o.BookedDate,
                        TenantName = o.Tenant.TenantName,
                        Status = o.Status,
                        ItemCount = o.RentItems.Count,
                        RentValue = o.RentItems.Sum(x => x.Flat.Price * x.Quantity)

                    })
                .ToListAsync();
        }
        // GET: api/Rents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rent>> GetRent(int id)
        {
            if (_context.Rents == null)
            {
                return NotFound();
            }
            var rent = await _context.Rents.FindAsync(id);

            if (rent == null)
            {
                return NotFound();
            }

            return rent;
        }
        [HttpGet("DTO/{id}")]
        public async Task<ActionResult<RentViewDTO>> GetRentViewDTO(int id)
        {
            if (_context.Rents == null)
            {
                return NotFound();
            }
            var rent = await _context.Rents.Include(o => o.Tenant)
                .Include(o => o.RentItems).ThenInclude(oi => oi.Flat).FirstOrDefaultAsync(o => o.RentID == id);

            if (rent == null)
            {
                return NotFound();
            }

            return new RentViewDTO
            {
                
                RentID = rent.RentID,
                RentDate = rent.RentDate,
                BookedDate = rent.BookedDate,
                TenantName = rent.Tenant.TenantName,
                Status = rent.Status,
                ItemCount = rent.RentItems.Count,
                RentValue = rent.RentItems.Sum(x => x.Flat.Price * x.Quantity)

            };
        }
        [HttpGet("Items/{id}")]
        public async Task<ActionResult<IEnumerable<RentItemViewDTO>>> GetRentItems(int id)
        {
            if (_context.RentItems == null)
            {
                return NotFound();
            }
            var rentitems = await _context.RentItems.Include(x => x.Flat).Where(oi => oi.RentID == id).ToListAsync();

            if (rentitems == null)
            {
                return NotFound();
            }

            return rentitems.Select(oi => new RentItemViewDTO { RentID = oi.RentID, FlatName = oi.Flat.FlatName, Price = oi.Flat.Price, Quantity = oi.Quantity }).ToList();
        }
        [HttpGet("OI/{id}")]
        public async Task<ActionResult<IEnumerable<RentItem>>> GetRentItemsOf(int id)
        {
            if (_context.RentItems == null)
            {
                return NotFound();
            }
            var rentitems = await _context.RentItems.Where(oi => oi.RentID == id).ToListAsync();

            if (rentitems == null)
            {
                return NotFound();
            }

            return rentitems;
        }
        // PUT: api/Rents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Rent rent)
        {
            if (id != rent.RentID)
            {
                return BadRequest();
            }

            _context.Entry(rent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentExists(id))
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

        public async Task<IActionResult> PutRentWithOrderItem(int id, RentEditDTO rent)
        {
            if (id != rent.RentID)
            {
                return BadRequest();
            }
            var existing = await _context.Rents.Include(x => x.RentItems).FirstAsync(o => o.RentID == id);
            _context.RentItems.RemoveRange(existing.RentItems);
            existing.RentID = rent.RentID;
            existing.RentDate = rent.RentDate;
            existing.BookedDate = rent.BookedDate;
            existing.TenantID = rent.TenantID;
            existing.Status = rent.Status;
            foreach (var item in rent.RentItems)
            {
                _context.RentItems.Add(new RentItem { RentID = rent.RentID, FlatID = item.FlatID, Quantity = item.Quantity });
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
        // POST: api/Rents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rent>> PostRent(Rent rent)
        {
            if (_context.Rents == null)
            {
                return Problem("Entity set 'FlatDbContext.Rents'  is null.");
            }
            _context.Rents.Add(rent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRent", new { id = rent.RentID }, rent);
        }

        [HttpPost("DTO")]
        public async Task<ActionResult<Rent>> PostRentDTO(RentDTO dto)
        {
            if (_context.Rents == null)
            {
                return Problem("Entity set 'FlatDbContext.Rents'  is null.");
            }
            var rent = new Rent { TenantID = dto.TenantID, RentDate = dto.RentDate, BookedDate = dto.BookedDate, Status = dto.Status };
            foreach (var oi in dto.RentItems)
            {
                rent.RentItems.Add(new RentItem { FlatID = oi.FlatID, Quantity = oi.Quantity });
            }
            _context.Rents.Add(rent);
            await _context.SaveChangesAsync();

            return rent;
        }
        // DELETE: api/Rents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRent(int id)
        {
            if (_context.Rents == null)
            {
                return NotFound();
            }
            var rent = await _context.Rents.FindAsync(id);
            if (rent == null)
            {
                return NotFound();
            }

            _context.Rents.Remove(rent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentExists(int id)
        {
            return (_context.Rents?.Any(e => e.RentID == id)).GetValueOrDefault();
        }
    }
}
