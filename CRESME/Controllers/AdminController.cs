using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Diagnostics;
using CRESME.Data;
using CRESME.Models;
using CRESME.Constants;

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

                    /*
                    for (int row = 2; row <= rowcount; row++)
                    {
                        list.Add(new ApplicationUser
                        {
                            Id = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            
                            UserName = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            PasswordHash = worksheet.Cells[row, 7].Value.ToString().Trim(),
                            NormalizedUserName = worksheet.Cells[row, 3].Value.ToString().Trim(),
                            Email = worksheet.Cells[row, 4].Value.ToString().Trim(),
                            NormalizedEmail = worksheet.Cells[row, 5].Value.ToString().Trim(),
                            EmailConfirmed = true,
                        
                            //SecurityStamp = worksheet.Cells[row, 8].Value.ToString().Trim(),
                            //ConcurrencyStamp = worksheet.Cells[row, 9].Value.ToString().Trim(),
                            PhoneNumber = worksheet.Cells[row, 10].Value.ToString().Trim(),
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            LockoutEnd = null,
                            LockoutEnabled = true,
                            AccessFailedCount = 0

                            
                            PasswordHash = worksheet.Cells[row, 7].Value.ToString().Trim(),
                            SecurityStamp = worksheet.Cells[row, 8].Value.ToString().Trim(),
                            ConcurrencyStamp = worksheet.Cells[row, 9].Value.ToString().Trim(),
                            PhoneNumber = worksheet.Cells[row, 10].Value.ToString().Trim(),
                            PhoneNumberConfirmed = (bool)worksheet.Cells[row, 11].Value,
                            TwoFactorEnabled = (bool)worksheet.Cells[row, 12].Value,
                            LockoutEnd = null,
                            LockoutEnabled = true,
                            AccessFailedCount = 0
                            


                        });
                    }

                    */


                    for (int row = 2; row <= rowcount; row++)
                    {
                        var PasswordHash = worksheet.Cells[row, 7].Value.ToString();


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
                            AccessFailedCount = 0
                        };
                        var result = await _userManager.CreateAsync(user, PasswordHash);
                        await _userManager.AddToRoleAsync(user, Roles.Instructor.ToString());


                    }


                }
            }

            _context.Users.AddRange(list);


            _context.SaveChanges();



            return list;

        } // import







    } // end Admin Controller
}
