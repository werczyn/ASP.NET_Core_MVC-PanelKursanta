using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPBProjekt.Data;
using IPBProjekt.Models;
using System.Text.RegularExpressions;

namespace IPBProjekt.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class WydzialKomunikacjiController : ControllerBase
    {
        private readonly AppData _context;

        public WydzialKomunikacjiController(AppData context)
        {
            _context = context;
        }

        // GET: api/WydzialKomunikacji
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WydzialKomunikacji>>> GetWydzialKomunikacji()
        {
            return await _context.WydzialKomunikacji.Include(wk=>wk.Dokumenty).Include(wk=>wk.Uzytkownicy).ToListAsync();
        }

        // GET: api/WydzialKomunikacji/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WydzialKomunikacji>> GetWydzialKomunikacji(int id)
        {
            var wydzialKomunikacji = await _context.WydzialKomunikacji.Include(wk => wk.Dokumenty).SingleOrDefaultAsync(wk=>wk.NumerWydzialu == id);

            if (wydzialKomunikacji == null)
            {
                return NotFound();
            }

            return wydzialKomunikacji;
        }

        [HttpGet("miasto/{miasto}")]
        public async Task<ActionResult<IEnumerable<WydzialKomunikacji>>> GetWydzialKomunikacji(string miasto)
        {
            return await _context.WydzialKomunikacji.Include(wk => wk.Dokumenty).Include(wk => wk.Uzytkownicy).Where(wk=>wk.Miasto.ToLower() ==  miasto).ToListAsync();
        }

        [HttpGet("search/{fraza}")]
        public async Task<ActionResult<IEnumerable<WydzialKomunikacji>>> GetWydzialKomunikacjiSearch(string fraza)
        {
            Regex regex = new Regex(@"^.*" + fraza + ".*$");
            var wydzialy = await _context.WydzialKomunikacji.Include(wk => wk.Dokumenty).ToListAsync();
            return wydzialy.Where(
                wk => regex.IsMatch(wk.Miasto.ToLower()) || 
                regex.IsMatch(wk.Ulica.ToLower())
            ).ToList();
        }

        // PUT: api/WydzialKomunikacji/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWydzialKomunikacji(int id, WydzialKomunikacji wydzialKomunikacji)
        {
            if (id != wydzialKomunikacji.NumerWydzialu)
            {
                return BadRequest();
            }

            _context.Entry(wydzialKomunikacji).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WydzialKomunikacjiExists(id))
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

        // POST: api/WydzialKomunikacji
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WydzialKomunikacji>> PostWydzialKomunikacji(WydzialKomunikacji wydzialKomunikacji)
        {
            _context.WydzialKomunikacji.Add(wydzialKomunikacji);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWydzialKomunikacji", new { id = wydzialKomunikacji.NumerWydzialu }, wydzialKomunikacji);
        }

        // DELETE: api/WydzialKomunikacji/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WydzialKomunikacji>> DeleteWydzialKomunikacji(int id)
        {
            var wydzialKomunikacji = await _context.WydzialKomunikacji.FindAsync(id);
            if (wydzialKomunikacji == null)
            {
                return NotFound();
            }

            _context.WydzialKomunikacji.Remove(wydzialKomunikacji);
            await _context.SaveChangesAsync();

            return wydzialKomunikacji;
        }

        private bool WydzialKomunikacjiExists(int id)
        {
            return _context.WydzialKomunikacji.Any(e => e.NumerWydzialu == id);
        }
    }
}
