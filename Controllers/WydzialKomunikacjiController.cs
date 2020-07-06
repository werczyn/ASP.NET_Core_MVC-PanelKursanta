using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPBProjekt.Data;
using IPBProjekt.Models;
using Microsoft.AspNetCore.Authorization;

namespace IPBProjekt.Controllers

{
    [Authorize(Roles = "WydzialKomunikacji")]
    public class WydzialKomunikacjiController : Controller
    {
        private readonly AppData _context;

        public WydzialKomunikacjiController(AppData context)
        {
            _context = context;
        }

        // GET: WydzialKomunikacji

        public async Task<IActionResult> Index(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            ViewBag.idWydzialu = id;
            return View(await _context.Dokumenty
                .Join(_context.Kursants,
                d=>d.IdOsoba,
                k=>k.IdOsoba,
                (dokument, kursant) => new Dokument { Kursant = kursant,
                    IdDokument=dokument.IdDokument,
                    CzySprawdzony=dokument.CzySprawdzony,
                    CzyPrzyjety = dokument.CzyPrzyjety,
                    IdWydzialKomunikacji = dokument.IdWydzialKomunikacji,
                CzyWyslany = dokument.CzyWyslany})
                .Where(d => d.IdWydzialKomunikacji == id).ToListAsync());
        }

        public async Task<IActionResult> WyswietlDokument(int? idOsoba, int? idWydzialu)
        {
            ViewBag.idWydzialu = idWydzialu;
            return View(await _context.Dokumenty.Include(d => d.Kursant).Include(d => d.WydzialKomunikacji).SingleOrDefaultAsync(d => d.IdOsoba == idOsoba && d.IdWydzialKomunikacji == idWydzialu));
        }

        public async Task<IActionResult> Zatwierdz(int? idOsoba, int? idWydzialu)
        {
            var dokument = await _context.Dokumenty.Include(d => d.Kursant).Include(d => d.WydzialKomunikacji).SingleOrDefaultAsync(d => d.IdOsoba == idOsoba && d.IdWydzialKomunikacji == idWydzialu);

            var result = DateTime.Now - dokument.Kursant.DataUrodzenia;
            DateTime zeroTime = new DateTime(1, 1, 1);
            int lata = (zeroTime + result).Year - 1;

            if (lata<18)
            {
                dokument.CzySprawdzony = true;
                dokument.CzyPrzyjety = false;
            }
            else
            {
                dokument.CzySprawdzony = true;
                dokument.CzyPrzyjety = true;
                var kursant = dokument.Kursant;
                kursant.NumerKursanta = kursant.DataUrodzenia.Year + kursant.DataUrodzenia.Month + kursant.DataUrodzenia.Day;
                _context.Update(kursant);
            }
            _context.Update(dokument);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new {@id = idWydzialu });
        }

        public async Task<IActionResult> Odrzuc(int? idOsoba, int? idWydzialu)
        {
            var dokument = await _context.Dokumenty.Where(d => d.IdOsoba == idOsoba && d.IdWydzialKomunikacji == idWydzialu).SingleOrDefaultAsync();
            dokument.CzyPrzyjety = false;
            dokument.CzySprawdzony = true;
            _context.Update(dokument);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { @id = idWydzialu });
        }

        // GET: WydzialKomunikacji/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wydzialKomunikacji = await _context.WydzialKomunikacji
                .FirstOrDefaultAsync(m => m.NumerWydzialu == id);
            if (wydzialKomunikacji == null)
            {
                return NotFound();
            }

            return View(wydzialKomunikacji);
        }

        // GET: WydzialKomunikacji/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WydzialKomunikacji/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumerWydzialu,Miasto,Ulica,NumerMieszkania")] WydzialKomunikacji wydzialKomunikacji)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wydzialKomunikacji);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wydzialKomunikacji);
        }

        // GET: WydzialKomunikacji/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wydzialKomunikacji = await _context.WydzialKomunikacji.FindAsync(id);
            if (wydzialKomunikacji == null)
            {
                return NotFound();
            }
            return View(wydzialKomunikacji);
        }

        // POST: WydzialKomunikacji/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumerWydzialu,Miasto,Ulica,NumerMieszkania")] WydzialKomunikacji wydzialKomunikacji)
        {
            if (id != wydzialKomunikacji.NumerWydzialu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wydzialKomunikacji);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WydzialKomunikacjiExists(wydzialKomunikacji.NumerWydzialu))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(wydzialKomunikacji);
        }

        // GET: WydzialKomunikacji/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wydzialKomunikacji = await _context.WydzialKomunikacji
                .FirstOrDefaultAsync(m => m.NumerWydzialu == id);
            if (wydzialKomunikacji == null)
            {
                return NotFound();
            }

            return View(wydzialKomunikacji);
        }

        // POST: WydzialKomunikacji/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wydzialKomunikacji = await _context.WydzialKomunikacji.FindAsync(id);
            _context.WydzialKomunikacji.Remove(wydzialKomunikacji);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WydzialKomunikacjiExists(int id)
        {
            return _context.WydzialKomunikacji.Any(e => e.NumerWydzialu == id);
        }
    }
}
