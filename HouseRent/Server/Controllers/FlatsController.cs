using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HouseRent.Shared.DTO;
using HouseRent.Shared.Models;

namespace HouseRent.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatsController : ControllerBase
    {
        private readonly FlatDbContext _context;
        private readonly IWebHostEnvironment env;
        public FlatsController(FlatDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flat>>> GetFlats()
        {
            if (_context.Flats == null)
            {
                return NotFound();
            }
            return await _context.Flats.ToListAsync();
        }

        // GET: api/Flats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flat>> GetFlat(int id)
        {
            if (_context.Flats == null)
            {
                return NotFound();
            }
            var flat = await _context.Flats.FindAsync(id);

            if (flat == null)
            {
                return NotFound();
            }

            return flat;
        }

        // PUT: api/Flats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlat(int id, Flat flat)
        {
            if (id != flat.FlatID)
            {
                return BadRequest();
            }

            _context.Entry(flat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlatExists(id))
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

        // POST: api/Flats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Flat>> PostFlat(Flat flat)
        {
            if (_context.Flats == null)
            {
                return Problem("Entity set 'FlatDbContext.Flats'  is null.");
            }
            _context.Flats.Add(flat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlat", new { id = flat.FlatID }, flat);
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
        // DELETE: api/Flats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlat(int id)
        {
            if (_context.Flats == null)
            {
                return NotFound();
            }
            var flat = await _context.Flats.FindAsync(id);
            if (_context.RentItems.Any(x => x.FlatID == id)) return BadRequest("Entity has child data");
            if (flat == null)
            {
                return NotFound();
            }

            _context.Flats.Remove(flat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool  FlatExists(int id)
        {
            return (_context.Flats?.Any(e => e.FlatID == id)).GetValueOrDefault();
        }
    }
}
