using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MikeRosoft.Data;
using MikeRosoft.Models;
using MikeRosoft.Models.BanViewModels;

namespace MikeRosoft.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class BansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bans
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Bans.ToListAsync());
            return View(_context.Bans.Include(b => b.GetAdmin).Where(a => a.GetAdmin.Equals(User.Identity)));
        }

        // GET: Bans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ban = await _context.Bans
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ban == null)
            {
                return NotFound();
            }

            return View(ban);
        }

        // GET: Bans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,GetAdminId,BanTime")] Ban ban)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ban);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ban);
        }

        // GET: Bans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ban = await _context.Bans.FindAsync(id);
            if (ban == null)
            {
                return NotFound();
            }
            return View(ban);
        }

        // POST: Bans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,GetAdminId,BanTime")] Ban ban)
        {
            if (id != ban.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ban);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BanExists(ban.ID))
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
            return View(ban);
        }

        // GET: Bans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ban = await _context.Bans
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ban == null)
            {
                return NotFound();
            }

            return View(ban);
        }

        // POST: Bans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ban = await _context.Bans.FindAsync(id);
            _context.Bans.Remove(ban);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BanExists(int id)
        {
            return _context.Bans.Any(e => e.ID == id);
        }

        //Get method
        public IActionResult SelectUsersToBan(string NameSelected, string sur1Selected, string sur2Selected, string IdSelected)
        {
            SelectUsersToBanViewModel selectUsers = new SelectUsersToBanViewModel();
            //Search for users where no banForUser has an end date later than today
            selectUsers.UserList = _context.Users.Include(user => user.BanRecord).Where(user=> !user.BanRecord.Any(banforuser=>banforuser.End.Date > DateTime.Now));

            //filter by name
            if (NameSelected != null)
            {
                selectUsers.UserList = selectUsers.UserList.Where(u => u.Name.Contains(NameSelected));
            }
            
            //filter by 1st surname
            if (NameSelected != null)
            {
                selectUsers.UserList = selectUsers.UserList.Where(u => u.FirstSurname.Contains(sur1Selected));
            }
            
            //filter by 2nd surname
            if (NameSelected != null)
            {
                selectUsers.UserList = selectUsers.UserList.Where(u => u.SecondSurname.Contains(sur2Selected));
            }
            
            //filter by ID
            if (NameSelected != null)
            {
                selectUsers.UserList = selectUsers.UserList.Where(u => u.Id.Contains(IdSelected));
            }

            //Populate user list
            selectUsers.UserList.ToList();
            return View(selectUsers);
        }
    }
}
