using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPBProjekt.Data;
using IPBProjekt.Models;
using Newtonsoft.Json;

namespace IPBProjekt.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class KursantController : ControllerBase
    {
        private readonly AppData _context;

        public KursantController(AppData context)
        {
            _context = context;
        }

        // GET: api/Kursant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kursant>>> GetKursants()
        {
            return await _context.Kursants.Include(k=>k.Dokument).Include(k=>k.Uzytkownik).ToListAsync();
        }

        // GET: api/Kursant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kursant>> GetKursant(int id)
        {
            var kursant = await _context.Kursants.Include(k => k.Dokument).Include(k => k.Uzytkownik).SingleOrDefaultAsync(k=>k.IdOsoba == id);

            if (kursant == null)
            {
                return NotFound();
            }

            return kursant;
        }

        [HttpGet("info/przyjety")]
        public async Task<ActionResult<IEnumerable<Kursant>>> GetKursantPrzyjety()
        {
            return await _context.Kursants.Include(k => k.Dokument).Include(k => k.Uzytkownik).Where(k=>k.Dokument.CzyPrzyjety).ToListAsync();
        }

        [HttpGet("info/sprawdzony")]
        public async Task<ActionResult<IEnumerable<Kursant>>> GetKursantSprawdzony()
        {
            return await _context.Kursants.Include(k => k.Dokument).Include(k => k.Uzytkownik).Where(k => k.Dokument.CzySprawdzony).ToListAsync();
        }

        [HttpGet("info/zamlody")]
        public async Task<ActionResult<IEnumerable<Kursant>>> GetKursantZaMlody()
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            var kursanci = await _context.Kursants.Include(k => k.Dokument).Include(k => k.Uzytkownik).ToListAsync();
            var zaMlodzi = new List<Kursant>();
            foreach (var item in kursanci)
            {
                var result = DateTime.Now - item.DataUrodzenia;
                int lata = (zeroTime + result).Year - 1;
                if (lata < 18)
                {
                    zaMlodzi.Add(item);
                }
            }
            return zaMlodzi;
        }

        [HttpGet("info/pkk")]
        public async Task<ActionResult<IEnumerable<Kursant>>> GetKursantsZPKK()
        {
            return await _context.Kursants.Include(k => k.Dokument).Include(k => k.Uzytkownik).Where(k => k.NumerKursanta.HasValue).ToListAsync();
        }

        // PUT: api/Kursant/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKursant(int id, Kursant kursant)
        {
            if (id != kursant.IdOsoba)
            {
                return BadRequest();
            }

            _context.Entry(kursant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KursantExists(id))
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

        // POST: api/Kursant
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Kursant>> PostKursant(Kursant kursant)
        {
            _context.Kursants.Add(kursant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKursant", new { id = kursant.IdOsoba }, kursant);
        }

        // DELETE: api/Kursant/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Kursant>> DeleteKursant(int id)
        {
            var kursant = await _context.Kursants.FindAsync(id);
            if (kursant == null)
            {
                return NotFound();
            }

            _context.Kursants.Remove(kursant);
            await _context.SaveChangesAsync();

            return kursant;
        }

        private bool KursantExists(int id)
        {
            return _context.Kursants.Any(e => e.IdOsoba == id);
        }
    }
}
