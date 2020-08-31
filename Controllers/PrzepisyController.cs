using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeastBook_final.Data;
using FeastBook_final.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Collections;

namespace FeastBook_final.Controllers
{
    [Authorize]
    public class PrzepisyController : Controller
    {
        private ApplicationDbContext _context;

        public PrzepisyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Przepisy
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? rating, string id)
        {
            var applicationDbContext = _context.Przepisy.Include(p => p.Kategoria);

            /*IQueryable<Kategoria> kategoriaQuery = from p in _context.Przepisy
                                                orderby p.Kategoria.Nazwa
                                                select p.Kategoria;*/
            var przepisy = from p in _context.Przepisy
                           select p;
            if (!string.IsNullOrEmpty(searchString))
            {
                przepisy = przepisy.Where(s => s.Nazwa.Contains(searchString) || s.Hasztag.Contains(searchString));
            }
            if (rating.HasValue)
            {
                przepisy = przepisy.Where(s => s.Ocena.Equals(rating));
            }
            if (!string.IsNullOrEmpty(id))
            {
                przepisy = przepisy.Where(s => s.Kategoria.Nazwa.Equals(id));
                ViewBag.Id = id;
            }

            /*if (!string.IsNullOrEmpty(wyszukanePrzepisy))
            {
                przepisy = przepisy.Where(x => x.Kategoria.Nazwa.ToString() == wyszukanePrzepisy);
            }

            var wyszukiwaniePrzepisowVM = new WyszukiwaniePrzepisowViewModel
            {
                Kategorie = new SelectList(await kategoriaQuery.Distinct().ToListAsync()),
                Przepisy = await przepisy.ToListAsync()
            };*/

            return View(await przepisy.ToListAsync());
        }
        /*
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Przepisy.Include(p => p.Kategoria);
            return View(await applicationDbContext.ToListAsync());
        }*/

        // GET: Przepisy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przepis = await _context.Przepisy
                .Include(p => p.Kategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (przepis == null)
            {
                return NotFound();
            }

            return View(przepis);
        }

        // GET: Przepisy/Create
        public IActionResult Create()
        {
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa");
            return View();
        }

        // POST: Przepisy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Hasztag,Ocena,Image,Image2,Image3,Tresc,KategoriaId")] Przepis przepis, List<IFormFile> Image, List<IFormFile> Image2, List<IFormFile> Image3)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in Image)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            przepis.Image = stream.ToArray();
                        }
                    }
                }
                foreach (var item in Image2)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            przepis.Image2 = stream.ToArray();
                        }
                    }
                }
                foreach (var item in Image3)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            przepis.Image3 = stream.ToArray();
                        }
                    }
                }
                _context.Add(przepis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", przepis.KategoriaId);
            return View(przepis);
        }

        // GET: Przepisy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przepis = await _context.Przepisy.FindAsync(id);
            if (przepis == null)
            {
                return NotFound();
            }
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", przepis.KategoriaId);
            return View(przepis);
        }

        // POST: Przepisy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Hasztag,Ocena,Image,Image2,Image3,Tresc,KategoriaId")] Przepis przepis, List<IFormFile> Image, List<IFormFile> Image2, List<IFormFile> Image3)
        {
            if (id != przepis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var item in Image)
                    {
                        if (item.Length > 0)
                        {
                            using (var stream = new MemoryStream())
                            {
                                await item.CopyToAsync(stream);
                                przepis.Image = stream.ToArray();
                            }
                        } else
                        {
                            var przepisEdytowany = await _context.Przepisy.FindAsync(id);
                            przepis.Image = przepisEdytowany.Image; 
                        }
                    }
                    foreach (var item in Image2)
                    {
                        if (item.Length > 0)
                        {
                            using (var stream = new MemoryStream())
                            {
                                await item.CopyToAsync(stream);
                                przepis.Image2 = stream.ToArray();
                            }
                        }
                        else
                        {
                            var przepisEdytowany = await _context.Przepisy.FindAsync(id);
                            przepis.Image2 = przepisEdytowany.Image2;
                        }
                    }
                    foreach (var item in Image3)
                    {
                        if (item.Length > 0)
                        {
                            using (var stream = new MemoryStream())
                            {
                                await item.CopyToAsync(stream);
                                przepis.Image3 = stream.ToArray();
                            }
                        }
                        /*else
                        {
                            var przepisEdytowany = await _context.Przepisy.FindAsync(id);
                            przepis.Image3 = przepisEdytowany.Image3;
                        }*/
                    }
                    _context.Update(przepis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrzepisExists(przepis.Id))
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
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", przepis.KategoriaId);
            return View(przepis);
        }

        // GET: Przepisy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przepis = await _context.Przepisy
                .Include(p => p.Kategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (przepis == null)
            {
                return NotFound();
            }

            return View(przepis);
        }

        // POST: Przepisy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var przepis = await _context.Przepisy.FindAsync(id);
            _context.Przepisy.Remove(przepis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool PrzepisExists(int id)
        {
            return _context.Przepisy.Any(e => e.Id == id);
        }
    }
}
