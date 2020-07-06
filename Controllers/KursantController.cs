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
using Microsoft.EntityFrameworkCore.Internal;

namespace IPBProjekt.Controllers
{
    [Authorize(Roles = "Kursant")]
    public class KursantController : Controller
    {
        private readonly AppData _context;

        public KursantController(AppData context)
        {
            _context = context;
        }

        // GET: Kursant
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.idOsoba = id;

            return View(await _context.WydzialKomunikacji.ToListAsync());
        }

        public async Task<IActionResult> WyslijDokument(int? idWydzialu, int? idOsoba)
        {
            if (idWydzialu == null || idOsoba == null)
            {
                return NotFound();
            }

            if (!_context.Dokumenty.Where(d=>d.IdOsoba == idOsoba && d.CzyWyslany == true).Any())
            {
                Dokument dokument = new Dokument { IdOsoba = idOsoba, IdWydzialKomunikacji = idWydzialu, CzyWyslany = true };
                await _context.AddAsync(dokument);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Details), new { @id = idOsoba });
        }

        public async Task<IActionResult> WyswietlDokumenty(int? idOsoba)
        {
            if (idOsoba == null)
            {
                return NotFound();
            }
            return View(await _context.Dokumenty.Include(d=>d.WydzialKomunikacji).Where(d=>d.IdOsoba == idOsoba).ToListAsync());
        }

        public async Task<IActionResult> UsunDokument(int? id, int? idOsoba)
        {
            var dokument = await _context.Dokumenty.FindAsync(id);
            _context.Remove(dokument);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { @id = idOsoba });
        }

        // GET: Kursant/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kursant = await _context.Kursants
                .FirstOrDefaultAsync(m => m.IdOsoba == id);
            if (kursant == null)
            {
                return NotFound();
            }

            return View(kursant);
        }

        // GET: Kursant/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kursant = await _context.Kursants.FindAsync(id);
            if (kursant == null)
            {
                return NotFound();
            }
            return View(kursant);
        }

        // POST: Kursant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumerKursanta,EmailAdress,DataUrodzenia,IdOsoba,Imie,Nazwisko")] Kursant kursant)
        {
            if (id != kursant.IdOsoba)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kursant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KursantExists(kursant.IdOsoba))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details),new { @id = id });
            }
            return View(kursant);
        }

        private bool KursantExists(int id)
        {
            return _context.Kursants.Any(e => e.IdOsoba == id);
        }
    }
}
