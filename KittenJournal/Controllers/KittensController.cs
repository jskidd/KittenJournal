using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KittenJournal.DAL;
using KittenJournal.Models;
using KittenJournal.Models.ViewModels;

namespace KittenJournal
{
    public class KittensController : Controller
    {
        private readonly AppDbContext _context;

        public KittensController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Kittens
        public async Task<IActionResult> Index()
        {
            List<KittenViewModel> vm = new List<KittenViewModel>();

            foreach (var kitten in _context.Kittens.ToList())
            {
                vm.Add(new KittenViewModel
                {
                    Kitten = kitten,
                    Foster = _context.Fosters.Find(kitten.FosterId)
                });
            }

            return View(vm.ToList());
        }

        // GET: Kittens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitten = await _context.Kittens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kitten == null)
            {
                return NotFound();
            }

            return View(new KittenViewModel()
            {
                Kitten = kitten,
                Foster = _context.Fosters.Where(f => f.Id == kitten.FosterId).FirstOrDefault(),
                Feedings = await _context.Feedings.Where(f => f.KittenId == kitten.Id).OrderByDescending(f => f.Timestamp).ToListAsync()
            }); ;
        }

        // GET: Kittens/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Fosters = await _context.Fosters.ToListAsync();
            return View();
        }

        // POST: Kittens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CurrentWeight,Sex,FosterId")] Kitten kitten)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kitten);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kitten);
        }

        // GET: Kittens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitten = await _context.Kittens.FindAsync(id);
            if (kitten == null)
            {
                return NotFound();
            }

            CreateEditKittenViewModel vm = new CreateEditKittenViewModel()
            {
                kitten = kitten,
                FostersList = _context.Fosters.ToList()
            };

            return View(vm);
        }

        // POST: Kittens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CurrentWeight,Sex,FosterId")] Kitten kitten)
        {
            if (id != kitten.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kitten);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KittenExists(kitten.Id))
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
            return View(kitten);
        }

        // GET: Kittens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitten = await _context.Kittens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kitten == null)
            {
                return NotFound();
            }

            return View(kitten);
        }

        // POST: Kittens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kitten = await _context.Kittens.FindAsync(id);
            _context.Kittens.Remove(kitten);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KittenExists(int id)
        {
            return _context.Kittens.Any(e => e.Id == id);
        }
    }
}
