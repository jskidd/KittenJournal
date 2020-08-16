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
    public class FeedingsController : Controller
    {
        private readonly AppDbContext _context;

        public FeedingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Feedings
        public async Task<IActionResult> Index()
        {
            List<FeedingViewModel> vm = new List<FeedingViewModel>();
            foreach (var f in (await _context.Feedings.OrderByDescending(f => f.Timestamp).ToListAsync()))
            {
                vm.Add(new FeedingViewModel() { 
                    Feeding = f,
                    Kitten =  _context.Kittens.Where(k => k.Id ==  f.KittenId).FirstOrDefault()
                });
            }
            return View(vm);
        }

        // GET: Feedings/Create
        public IActionResult Create(int? kittenId)
        {
            ViewBag.KittenId = kittenId;
            return View();
        }

        // POST: Feedings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartingWeight,EndWeight,Timestamp,KittenId")] Feeding feeding)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feeding);

                Kitten kitten = _context.Kittens.Where(k => k.Id == feeding.KittenId).FirstOrDefault();
                kitten.CurrentWeight = feeding.EndWeight;
                _context.Update(kitten);

                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Kittens", new { id = kitten.Id });
            }
            return View(feeding);
        }

        // GET: Feedings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeding = await _context.Feedings.FindAsync(id);
            if (feeding == null)
            {
                return NotFound();
            }
            ViewBag.KittenId = feeding.KittenId;
            ViewBag.KittenName = _context.Kittens.Where(k => k.Id == feeding.KittenId).FirstOrDefault().Name;
            return View(feeding);
        }

        // POST: Feedings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartingWeight,EndWeight,Timestamp,KittenId")] Feeding feeding)
        {
            if (id != feeding.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feeding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedingExists(feeding.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details","Kittens",new { id = _context.Kittens.Where(k => k.Id == feeding.KittenId).FirstOrDefault().Id });
            }
            return View(feeding);
        }

        // GET: Feedings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeding = await _context.Feedings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feeding == null)
            {
                return NotFound();
            }
            ViewBag.KittenName = _context.Kittens.Where(k => k.Id == feeding.KittenId).FirstOrDefault().Name;
            return View(feeding);
        }

        // POST: Feedings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feeding = await _context.Feedings.FindAsync(id);
            _context.Feedings.Remove(feeding);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details","Kittens",new { id = _context.Kittens.Where(k => k.Id == feeding.KittenId).FirstOrDefault().Id });
        }

        private bool FeedingExists(int id)
        {
            return _context.Feedings.Any(e => e.Id == id);
        }
    }
}
