using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPBProjekt.Data;
using IPBProjekt.Models;

namespace IPBProjekt.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DokumentController : ControllerBase
    {
        private readonly AppData _context;

        public DokumentController(AppData context)
        {
            _context = context;
        }

        // GET: api/Dokument
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dokument>>> GetDokumenty()
        {
            return await _context.Dokumenty.Include(d=>d.Kursant).Include(d=>d.WydzialKomunikacji).ToListAsync();
        }

        // GET: api/Dokument/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dokument>> GetDokument(int id)
        {
            var dokument = await _context.Dokumenty.Include(d => d.Kursant).Include(d => d.WydzialKomunikacji).SingleOrDefaultAsync(d=>d.IdDokument == id);

            if (dokument == null)
            {
                return NotFound();
            }

            return dokument;
        }

        [HttpGet("kursant/{id}")]
        public async Task<ActionResult<IEnumerable<Dokument>>> GetDocumentsFromKursant(int id)
        {
            var dokument = await _context.Dokumenty.Include(d => d.Kursant).Include(d => d.WydzialKomunikacji).Where(d => d.IdOsoba == id).ToListAsync();
            return dokument;
        }

        [HttpGet("wydzialKomunikacji/{id}")]
        public async Task<ActionResult<IEnumerable<Dokument>>> GetDocumentsToWydzialKomunikacji(int id)
        {
            var dokument = await _context.Dokumenty.Include(d => d.Kursant).Include(d => d.WydzialKomunikacji).Where(d => d.IdWydzialKomunikacji == id).ToListAsync();
            return dokument;
        }

        [HttpGet("sprawdzony")]
        public async Task<ActionResult<IEnumerable<Dokument>>> GetSprawdzoneDokumenty()
        {
            var dokument = await _context.Dokumenty.Include(d => d.Kursant).Include(d => d.WydzialKomunikacji).Where(d => d.CzySprawdzony).ToListAsync();
            return dokument;
        }

        [HttpGet("przyjety")]
        public async Task<ActionResult<IEnumerable<Dokument>>> GetPrzyjeteDokumenty()
        {
            var dokument = await _context.Dokumenty.Include(d => d.Kursant).Include(d => d.WydzialKomunikacji).Where(d => d.CzyPrzyjety).ToListAsync();
            return dokument;
        }

        // PUT: api/Dokument/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDokument(int id, Dokument dokument)
        {
            if (id != dokument.IdDokument)
            {
                return BadRequest();
            }

            _context.Entry(dokument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DokumentExists(id))
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

        // POST: api/Dokument
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Dokument>> PostDokument(Dokument dokument)
        {
            _context.Dokumenty.Add(dokument);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDokument", new { id = dokument.IdDokument }, dokument);
        }

        // DELETE: api/Dokument/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dokument>> DeleteDokument(int id)
        {
            var dokument = await _context.Dokumenty.FindAsync(id);
            if (dokument == null)
            {
                return NotFound();
            }

            _context.Dokumenty.Remove(dokument);
            await _context.SaveChangesAsync();

            return dokument;
        }

        private bool DokumentExists(int id)
        {
            return _context.Dokumenty.Any(e => e.IdDokument == id);
        }
    }
}
