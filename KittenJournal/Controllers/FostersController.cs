using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KittenJournal.DAL;
using KittenJournal.Models;
using KittenJournal.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using KittenJournal.Models.Identity;
using Microsoft.AspNetCore.Authorization;

namespace KittenJournal
{
    public class FostersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<KittenJournalUser> _userManager;

        public FostersController(AppDbContext context, UserManager<KittenJournalUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Fosters
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> Index()
        {
            List<FosterViewModel> fosters = new List<FosterViewModel>();
            foreach (var f in (await _context.Fosters.ToListAsync()))
            {
                FosterViewModel fvm = new FosterViewModel()
                {
                    foster = f,
                    kittens = _context.Kittens.Where(k => k.FosterId == f.Id)
                };
                fosters.Add(fvm);
            }
            return View(fosters.ToList());
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Index(string SearchString)
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                return View();
            }

            List<FosterViewModel> fosters = new List<FosterViewModel>();
            foreach (var f in (await _context.Fosters.Where(f => f.Name.Contains(SearchString)).ToListAsync()))
            {
                FosterViewModel fvm = new FosterViewModel()
                {
                    foster = f,
                    kittens = _context.Kittens.Where(k => k.FosterId == f.Id)
                };
                fosters.Add(fvm);
            }
            return View(fosters.ToList());
        }

        // GET: Fosters/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foster = await _context.Fosters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foster == null)
            {
                return NotFound();
            }

            FosterViewModel fvm = new FosterViewModel()
            {
                foster = foster,
                kittens = _context.Kittens.Where(k => k.FosterId == foster.Id)
            };

            return View(fvm);
        }

        // GET: Fosters/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fosters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,City,State,ZipCode,Email,PhoneNumber,Password,ConfirmPassword")] CreateEditFosterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Foster foster = new Foster()
                {
                    Name = vm.Name,
                    Address = vm.Address,
                    City = vm.City,
                    State = vm.State,
                    ZipCode = vm.ZipCode,
                    Email = vm.Email,
                    PhoneNumber = vm.PhoneNumber
                };
                _context.Add(foster);
                await _context.SaveChangesAsync();

                KittenJournalUser user = new KittenJournalUser()
                {
                    UserName = vm.Email,
                    Email = vm.Email,
                    PhoneNumber = vm.PhoneNumber,
                    FosterId = foster.Id,
                };

                IdentityResult result = await _userManager.CreateAsync(user, vm.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View();
                }

                await _userManager.AddToRoleAsync(user, "Foster");

                return RedirectToAction(nameof(Index));

            }
            return View(vm);
        }

        // GET: Fosters/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foster = await _context.Fosters.FindAsync(id);
            if (foster == null)
            {
                return NotFound();
            }

            return View(foster);
        }

        // POST: Fosters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,City,State,ZipCode,Email,PhoneNumber")] Foster foster)
        {
            if (id != foster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    KittenJournalUser user = _userManager.Users.Where(u => u.FosterId == foster.Id).FirstOrDefault();

                    user.Email = foster.Email;
                    user.UserName = foster.Email;
                    user.PhoneNumber = foster.PhoneNumber;

                    await _userManager.UpdateAsync(user);

                    _context.Update(foster);
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FosterExists(foster.Id))
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
            return View(foster);
        }

        // GET: Fosters/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foster = await _context.Fosters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foster == null)
            {
                return NotFound();
            }

            return View(foster);
        }

        // POST: Fosters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foster = await _context.Fosters.FindAsync(id);
            foreach(var k in _context.Kittens.Where(k => k.FosterId == foster.Id)) {
                k.FosterId = null;
            }
            _context.Fosters.Remove(foster);
            await _context.SaveChangesAsync();

            KittenJournalUser user = await _userManager.FindByEmailAsync(foster.Email);
            
            if (user != null) {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction(nameof(Index));
        }
        private bool FosterExists(int id)
        {
            return _context.Fosters.Any(e => e.Id == id);
        }
    }
}
