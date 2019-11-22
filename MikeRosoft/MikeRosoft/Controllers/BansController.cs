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
        public IActionResult Create(SelectedUsersToBanViewModel selectedUsers)
        {
            CreateBanViewModel BanViewModel = new CreateBanViewModel();
            //Initialize fields of the viewmodel (prevents null exception)
            BanViewModel.UserIds = selectedUsers.IdsToAdd;

            //BanViewModel.infoAboutUser = new List<string>();
            BanViewModel.BansForUsers = new List<BanForUser>();


            //Fill the lest of BanTypes in the SelectList
            BanViewModel.BanTypesAvailable = new SelectList(_context.BanTypes.Select(g => g.TypeName).ToList());
            BanViewModel.defaultDuration = new List<TimeSpan>(_context.BanTypes.Select(g => g.DefaultDuration).ToList());
            

            if (selectedUsers.IdsToAdd == null)
                ModelState.AddModelError("NoUsersSelected", "You should select at least a user to be banned, please");
            else
            {
                //Have some info regarding the user in the viewmodel
                foreach (string id in selectedUsers.IdsToAdd)
                {
                    var user = _context.Users.First(u => u.Id.Equals(id));
                   
                    //Fill info about the user
                    BanViewModel.infoAboutUser.Add(user.Name + " " + user.FirstSurname + " (" + user.DNI + ")");

                    //Fill bans for users
                    BanViewModel.BansForUsers.Add( new BanForUser { GetUser=user /*ID= nextBanForUserCounter()*/ });
                }

                var admin = _context.Admins.First(u => u.UserName.Equals(User.Identity.Name));
                BanViewModel.adminId = admin.Id;

            }

            return View(BanViewModel);
        }


        // POST: Bans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(CreateBanViewModel cm, IList<BanForUser> BansForUsers)
        {
            //Create ban
            Ban ban = new Ban
            {
                GetAdmin = _context.Admins.First(u => u.UserName.Equals(User.Identity.Name)),
                GetAdminId = _context.Admins.First(u => u.UserName.Equals(User.Identity.Name)).Id,
                BanTime = DateTime.UtcNow,
                GetBanForUsers = new List<BanForUser>()
            };

            //Add this ban to the database
            _context.Bans.Add(ban);

            //Fill information in BanForUser list (Needed: Ban, BanType relationships)
            foreach(BanForUser bfu in cm.BansForUsers)
            //for(int i = 0; i< cm.BansForUsers.Count;i++)
            {
                int num = 0;
                //Relationship with Ban
                //BansForUsers[i].GetBan = ban;
                bfu.GetBan = ban;

                //Relationship with BanType (the view only picks up the type name)
                //BansForUsers[i].GetBanType = await _context.BanTypes.SingleOrDefaultAsync(b => b.TypeName.Equals(cm.banTypeName[num]));
                bfu.GetBanType = await _context.BanTypes.SingleOrDefaultAsync(b => b.TypeName.Equals(cm.banTypeName[num]));

                //Add this BanforUser to the database
                //_context.BanForUsers.Add(cm.BansForUsers[i]);
                _context.BanForUsers.Add(bfu);
                num++;
            }

            //Update database
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
