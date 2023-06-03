using ClosedXML.Excel;
using CRESME.Constants;
using CRESME.Data;
using CRESME.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Data;
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


        /*        //remove later
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

        */


        // GET: Users
        public async Task<IActionResult> ListUsers()
        {
            return _context.Users != null ?
                        View(await _userManager.Users.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Test'  is null.");
        }

        public async Task<List<ApplicationUser>> ImportExcel(IFormFile file)

        {
            var list = new List<ApplicationUser>();
            using (var stream = new MemoryStream())
            {
                
                await file.CopyToAsync(stream);

                /*if (stream.Length <= 0)
                {
                    return list;
                }
*/
                using (XLWorkbook workbook = new XLWorkbook(stream))
                
                {                    
                    IXLWorksheet worksheet = workbook.Worksheets.First();
                        
                    int rowCount = worksheet.RowsUsed().Count();

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var PasswordHash = worksheet.Cell(row, 7).Value.ToString();
                        

                        var assignRole = worksheet.Cell(row, 17).Value.ToString().Trim();


                        var user = new ApplicationUser
                        {

                            UserName = worksheet.Cell(row, 2).Value.ToString().Trim(),
                            NormalizedUserName = worksheet.Cell(row, 3).Value.ToString().Trim(),
                            Email = worksheet.Cell(row, 4).Value.ToString().Trim(),
                            NormalizedEmail = worksheet.Cell(row, 5).Value.ToString().Trim(),
                            EmailConfirmed = true,
                            PhoneNumber = worksheet.Cell(row, 10).Value.ToString().Trim(),
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            LockoutEnd = null,
                            LockoutEnabled = true,
                            AccessFailedCount = 0,
                            Name = worksheet.Cell(row, 16).Value.ToString().Trim(),
                            Role = worksheet.Cell(row, 17).Value.ToString().Trim(),
                            Block = worksheet.Cell(row, 18).Value.ToString().Trim(),
                            Course = worksheet.Cell(row, 19).Value.ToString().Trim(),
                            Term = worksheet.Cell(row, 20).Value.ToString().Trim()




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

            RedirectToAction("ListUsers");

            return list;


        } // importToExcel



        /*public async Task<IActionResult> EditUsers([Bind("Id, UserName,NormalizedUserName, Email,NormalizedEmail, EmailConfirmed,PasswordHash, SecurityStamp, ConcurrencyStamp PhoneNumber,PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd,LockoutEnabled, AccessFailedCount,Name, Role")] ApplicationUser test)*/


        // GET: Tests/Create
        public IActionResult EditUsers(String id)
        {
            //return View();
            return View(_context.Users.Find(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string Id, string Name, string UserName, string Block, string Course, string Term)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                user.UserName = UserName;
                user.Email = UserName; 
                user.Name = Name;
                user.Block = Block;
                user.Course = Course;
                user.Term = Term;

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("ListUsers");


            }
            else
                ModelState.AddModelError("", "User Not Found");

            return RedirectToAction("CreateAccounts");
        }



        // GET: Tests/Creates
        public IActionResult CreateUsers()
        {
            return View();
        }


        // POST: Tests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string PasswordHash, string Role, string UserName, string Name, string Block, string Course, string Term)
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
                Block = Block,
                Course = Course,
                Term = Term


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



    /*    //Export Excel Data REMOVE LATER
        

        public IActionResult ExportToExcel()
        {
            db dbop = new db();
            DataSet ds = dbop.Getrecord(); 
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells.LoadFromDataTable(ds.Tables[0], true);
                package.Save(); 
            }

            stream.Position = 0;
            string excelname = $"StudentGrades.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname); 
        }
*/

       // Export List of Users for Admin
        public IActionResult ExportExcel()
        {
            db dbop = new db();
            DataSet ds = dbop.Getrecord();
            var stream = new MemoryStream();

            using (XLWorkbook workbook = new XLWorkbook())
            {
                

                foreach (DataTable dataTable in ds.Tables)
                {
                    // Add a worksheet for each DataTable in the DataSet
                    IXLWorksheet worksheet = workbook.Worksheets.Add(dataTable.TableName);

                    // Write column headers
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = dataTable.Columns[i].ColumnName;
                    }

                    // Write data rows
                    for (int row = 0; row < dataTable.Rows.Count; row++)
                    {
                        for (int col = 0; col < dataTable.Columns.Count; col++)
                        {
                            worksheet.Cell(row + 2, col + 1).Value = dataTable.Rows[row][col].ToString();
                        }
                    }
                }

                
                workbook.SaveAs(stream);
            }

            stream.Position = 0;
            string excelname = $"StudentGrades.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);
        }

        

        /*--------------------------All the functions below here are listed for Quizes----------------------------------*/




        // GET: List All Quizes for Admin account 
        public async Task<IActionResult> ListAllQuizes()
        {
            return _context.Users != null ?

                        /*View(await _userManager.Users.ToListAsync()) :*/

                        View(await _context.Quiz.ToListAsync()) :

                        Problem("Entity set 'ApplicationDbContext.Test'  is null.");
        }

        // GET: Wdit Quiz
        public IActionResult EditQuiz(string QuizName)
        {

            return View(_context.Quiz.Find(QuizName));

        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuiz(string QuizName, string Block, string Course, string Term, DateTime DateCreated, DateTime StartDate, DateTime EndDate)
        {
            

            var quiz = await _context.Quiz.FindAsync(QuizName);
            if (quiz != null)
            {
                quiz.QuizName = QuizName;
                quiz.Block = Block;
                quiz.Course = Course;
                quiz.Term = Term;
                quiz.DateCreated = DateCreated;
                quiz.StartDate = StartDate;
                quiz.EndDate = EndDate;



                /*IdentityResult result = await _userManager.UpdateAsync(user);

                IdentityResult result = await _context.Quiz.Update(quiz); */


                 _context.SaveChanges();

                return RedirectToAction("ListAllQuizes");

            }
            else
                ModelState.AddModelError("", "User Not Found");

            return RedirectToAction("CreateAccounts");
        }

        //POST:Delete
        [HttpPost]
        public async Task<IActionResult> DeleteQuiz(string QuizName)
        {
            

            var quiz =  _context.Quiz.Find(QuizName);


            if (quiz != null)
            {
                _context.Remove(quiz);
                _context.SaveChanges();

                return RedirectToAction("ListAllQuizes");

            }
            

            return RedirectToAction("CreateAccounts");
        }


        //POST:DeleteAll
        [HttpPost]
        public async Task<IActionResult> DeleteAllQuizes()
        {

            var quizes = await _context.Quiz.ToListAsync();
            if (quizes != null)
            {

                foreach (var item in quizes)
                {
                   
                   _context.Remove(item);
                    

                }

                _context.SaveChanges();


            }

            /* else
             {
                 ModelState.AddModelError("", "User Not Found");
             }*/

            return RedirectToAction("ListUsers");

        }







        // Export all Quizes for Admins
        public IActionResult ExportAllQuiz()
        {
            dbQuiz dbop = new dbQuiz();
            DataSet ds = dbop.Getrecord();
            var stream = new MemoryStream();

            using (XLWorkbook workbook = new XLWorkbook())
            {


                foreach (DataTable dataTable in ds.Tables)
                {
                    // Add a worksheet for each DataTable in the DataSet
                    IXLWorksheet worksheet = workbook.Worksheets.Add(dataTable.TableName);

                    // Write column headers
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = dataTable.Columns[i].ColumnName;
                    }

                    // Write data rows
                    for (int row = 0; row < dataTable.Rows.Count; row++)
                    {
                        for (int col = 0; col < dataTable.Columns.Count; col++)
                        {
                            worksheet.Cell(row + 2, col + 1).Value = dataTable.Rows[row][col].ToString();
                        }
                    }
                }


                workbook.SaveAs(stream);
            }

            stream.Position = 0;
            string excelname = $"List_of_Quizes.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);
        }







    }// end Admin Controller

}// CRESME.Controller
