using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Diagnostics;
using CRESME.Data;
using CRESME.Models;
using CRESME.Constants;
using System.Xml.Linq;

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
                            //PasswordHash = worksheet.Cells[row, 7].Value.ToString().Trim(),
                            UserName = worksheet.Cells[row, 2].Value.ToString().Trim(),

                            NormalizedUserName = worksheet.Cells[row, 3].Value.ToString().Trim(),
                            Email = worksheet.Cells[row, 4].Value.ToString().Trim(),
                            NormalizedEmail = worksheet.Cells[row, 5].Value.ToString().Trim(),
                            EmailConfirmed = true,

                            //////SecurityStamp = worksheet.Cells[row, 8].Value.ToString().Trim(),
                            //////ConcurrencyStamp = worksheet.Cells[row, 9].Value.ToString().Trim(),
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

       


        /*// GET: 
        public async Task<IActionResult> EditUsers(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var test = await _context.Users.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            return View(await _userManager.Users.ToListAsync());
        }*/

        // POST: Tests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUsers(string id, [Bind("UserName,NormalizedUserName, Email,NormalizedEmail, EmailConfirmed, PhoneNumber,PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd,LockoutEnabled, AccessFailedCount,Role ")] ApplicationUser test)
        {
            if (id != test.UserName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _context.Update(test);
                await _context.SaveChangesAsync();
                /*try
                {
                    _context.Update(test);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExists(test.Course))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }*/
                return RedirectToAction(nameof(Index));
            }
            return View(test);
        }


        // GET: Tests/Create
        public IActionResult CreateUsers()
        {
            return View();
        }

        // GET: Tests/Create
        public IActionResult EditUsers()
        {
            return View();
        }

        // POST: Tests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUsers([Bind("UserName,NormalizedUserName, Email,NormalizedEmail, EmailConfirmed, PhoneNumber,PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd,LockoutEnabled, AccessFailedCount,Role ")] ApplicationUser newUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newUser);
        }

    } // end Admin Controller
}
