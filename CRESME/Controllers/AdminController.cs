using CRESME.Constants;
using CRESME.Data;
using CRESME.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Diagnostics;

namespace CRESME.Controllers
{
    public class AdminController : Controller
    {


        // Authorize makes the page availabe to only to specified Roles
        [Authorize(Roles = "Admin, Instructor")]
        public IActionResult CreateAccounts()
        {
            return View();
        }



        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;



        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<List<ApplicationUser>> Import(IFormFile file)
        {
            var list = new List<ApplicationUser>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;



                    for (int row = 2; row <= rowcount; row++)
                    {
                        var PasswordHash = worksheet.Cells[row, 7].Value.ToString();

                        var assignRole = worksheet.Cells[row, 17].Value.ToString().Trim();


                        var user = new ApplicationUser
                        {
                            
                            UserName = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            NormalizedUserName = worksheet.Cells[row, 3].Value.ToString().Trim(),
                            Email = worksheet.Cells[row, 4].Value.ToString().Trim(),
                            NormalizedEmail = worksheet.Cells[row, 5].Value.ToString().Trim(),
                            EmailConfirmed = true,
                            PhoneNumber = worksheet.Cells[row, 10].Value.ToString().Trim(),
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            LockoutEnd = null,
                            LockoutEnabled = true,
                            AccessFailedCount = 0,
                            Name = worksheet.Cells[row, 16].Value.ToString().Trim(),
                            Role = worksheet.Cells[row, 17].Value.ToString().Trim()

                        };
                        var result = await _userManager.CreateAsync(user, PasswordHash);

                        // assign roles
                        if (assignRole == "Instructor")
                        {
                            await _userManager.AddToRoleAsync(user, Roles.Instructor.ToString());
                        }

                        if (assignRole == "Student")
                        {
                            await _userManager.AddToRoleAsync(user, Roles.Student.ToString());
                        }



                    }


                }
            }

            _context.Users.AddRange(list);


            _context.SaveChanges();



            return list;

        } // import



        // GET: Users
        public async Task<IActionResult> ListUsers()
        {
            return _context.Users != null ?
                        View(await _userManager.Users.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Test'  is null.");
        }


        /*public async Task<IActionResult> EditUsers([Bind("Id, UserName,NormalizedUserName, Email,NormalizedEmail, EmailConfirmed,PasswordHash, SecurityStamp, ConcurrencyStamp PhoneNumber,PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd,LockoutEnabled, AccessFailedCount,Name, Role")] ApplicationUser test)*/


        // GET: Tests/Create
        public IActionResult EditUsers(String id)
        {
            //return View();
            return View(_context.Users.Find(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string Id, string Name, string UserName)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                user.UserName = UserName;
                user.Email = UserName; 
                user.Name = Name;

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("ListUsers");


            }
            else
                ModelState.AddModelError("", "User Not Found");

            return RedirectToAction("CreateAccounts");
        }



        // GET: Tests/Create
        public IActionResult CreateUsers()
        {
            return View();
        }


        // POST: Tests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string PasswordHash, string Role, string UserName, string Name)
        {

  
            if (UserName == null)
            {
                return RedirectToAction("ListUsers");
            }


            ApplicationUser user = new ApplicationUser
            {

                UserName = UserName,
                Name = Name,

                NormalizedUserName = UserName.ToUpper(),
                Email = UserName,
                NormalizedEmail = UserName.ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Role = Role,


            };


            /*IdentityResult result =*/
            await _userManager.CreateAsync(user, PasswordHash);

            // assign roles
            if (Role == "Instructor")
            {
                await _userManager.AddToRoleAsync(user, Roles.Instructor.ToString());
            }

            if (Role == "Student")
            {
                await _userManager.AddToRoleAsync(user, Roles.Student.ToString());
            }

            _context.SaveChanges();



            return RedirectToAction("ListUsers");
        }


        //POST:Delete
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("ListUsers");


            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }

            return RedirectToAction("CreateAccounts");
        }

        //POST:DeleteAll
        [HttpPost]
        public async Task<IActionResult> DeleteAll()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users != null)
            {

                foreach (var item in users)
                {
                    if (item.UserName != "admin@gmail.com")
                    {
                        await _userManager.DeleteAsync(item);
                    }

                }


            }

            /* else
             {
                 ModelState.AddModelError("", "User Not Found");
             }*/

            return RedirectToAction("ListUsers");

        }


    }// end Admin Controller

}// CRESME.Controller
