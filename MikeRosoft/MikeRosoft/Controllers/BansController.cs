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
    [Authorize(Roles = "Admin")]
    public class BansController : Controller
    {
        private int banCounter = 0; 
        private int banForUserCounter = 0;

        public int nextBanCounter()
        {
            this.banCounter++;
            return this.banCounter;
        }

        public int nextBanForUserCounter()
        {
            this.banForUserCounter++;
            return this.banForUserCounter;
        }

        private readonly ApplicationDbContext _context;

        public BansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bans
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bans.ToListAsync());
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
        public IActionResult Create(SelectedUsersToBanViewModel model)
        {
            CreateBanViewModel BanToCreate = new CreateBanViewModel();
            //Initialize fields of the viewmodel
            BanToCreate.UserIds = model.IdsToAdd;
            //This causes an exception if no users are selected (the array is null and has no size)
            if(model.IdsToAdd != null) BanToCreate.infoAboutUser = new string[BanToCreate.UserIds.Length];

            //Fill the lest of BanTypes in the SelectList
            BanToCreate.BanTypesAvailable = new SelectList(_context.BanTypes.Select(g => g.TypeName).ToList());
            //Get default duration for each type (Exception if 0)
            if(_context.BanTypes.Select(g => g.TypeName).ToList().Count != 0)
            BanToCreate.defaultDuration = new TimeSpan[_context.BanTypes.Select(g => g.TypeName).ToList().Count];

            for (int i=0; i< BanToCreate.BanTypesAvailable.Count();i++)
            {
                BanToCreate.defaultDuration[i] = _context.BanTypes.Select(g => g.DefaultDuration).ToList().ElementAt(i);
            }

            if (model.IdsToAdd == null)
                ModelState.AddModelError("NoUsersSelected", "You should select at least a user to be banned, please");
            else
            {
                //Have some info regarding the user in the viewmodel
                for (int i = 0; i < model.IdsToAdd.Length; i++)
                {
                    var user = _context.Users.First(u => u.Id.Equals(model.IdsToAdd[i]));
                    BanToCreate.infoAboutUser[i] = user.Name + " " + user.FirstSurname + " (" + user.DNI + ")";
                }
            }
           
            return View(BanToCreate);
        }


        // POST: Bans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBanViewModel cm, IList<BanForUser> bfus)
        {
            //Create ban
            Ban ban = new Ban
            {
                //GetAdmin = _context.Admins.OfType<Admin>().FirstOrDefault<Admin>(u => u.Name.Equals(User.Identity.Name)),
                BanTime = DateTime.UtcNow,
                ID = this.nextBanCounter()
            };

            _context.Bans.Add(ban);
            //Fill the list of BanForUsers
            for (int i = 0; i<cm.UserIds.Count(); i++)
            {
                //Create a BanForUser for each selected  Id
                BanForUser bfu = new BanForUser
                {
                    //Relationship with ban
                    GetBan = ban,

                    //Relationship with BanType
                    GetBanType = _context.Users.OfType<BanType>().FirstOrDefault<BanType>(u => u.TypeName.Equals(cm.banTypeName)),

                    //Relationship with User
                    GetUser = _context.Users.OfType<User>().FirstOrDefault<User>(u => u.Id.Equals(cm.UserIds[i])),

                    //Start and end dates
                    Start = Convert.ToDateTime(cm.StartDate[i]),
                    End = Convert.ToDateTime(cm.EndDate[i]),

                    //Additional comments
                    AdditionalComment = cm.AdditionalComment[i]

                };
                _context.BanForUsers.Add(bfu);
                
            }

            await _context.SaveChangesAsync();
            //Details of the transaction
            return RedirectToAction("Details", new { id = ban.ID });
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
        public IActionResult SelectUsersToBan(string NameSelected, string userSurname1, string userSurname2, string userDNI)
        {
            SelectUsersToBanViewModel selectUsers = new SelectUsersToBanViewModel();
            //Search for users where no banForUser has an end date later than today
            selectUsers.Users = _context.Users.Include(user => user.BanRecord).Where(user => !user.BanRecord.Any(banforuser => banforuser.End.Date > DateTime.Now)).ToList();
            //filter by name
            if (NameSelected != null)
            {
                selectUsers.Users = selectUsers.Users.Where(u => u.Name.Contains(NameSelected));
            }

            //filter by 1st surname
            if (userSurname1 != null)
            {
                selectUsers.Users = selectUsers.Users.Where(u => u.FirstSurname.Contains(userSurname1));
            }

            //filter by 2nd surname
            if (userSurname2 != null)
            {
                selectUsers.Users = selectUsers.Users.Where(u => u.SecondSurname.Contains(userSurname2));
            }

            //filter by DNI
            if (userDNI != null)
            {
                selectUsers.Users = selectUsers.Users.Where(u => u.DNI.Contains(userDNI));
            }

            //Populate user list
            selectUsers.Users.ToList();
            return View(selectUsers);
        }

        //Post method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectUsersToBan(SelectedUsersToBanViewModel model)
        {
            if (model.IdsToAdd != null)
            {
                return RedirectToAction("Create", model);
            }
            else
            {
                //If no users are selected
                ModelState.AddModelError(string.Empty, "You must select at least one user");
                SelectUsersToBanViewModel select = new SelectUsersToBanViewModel();
                select.Users = _context.Users.Include(user => user.BanRecord).Where(user => !user.BanRecord.Any(banforuser => banforuser.End.Date > DateTime.Now));

                return View(select);
            }
        }
    }
}
