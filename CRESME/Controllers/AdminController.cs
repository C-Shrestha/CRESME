using ClosedXML.Excel;
using CRESME.Constants;
using CRESME.Data;
using CRESME.Models;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;

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


        [HttpPost]
        [Route("/Admin/ImportExcel")]
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
        [Route("/Admin/DeleteAll")]
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
        /*public IActionResult ExportExcel()*/
        /*public async Task<IActionResult> ExportExcel()*/

        /*public IActionResult ExportExcel()
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
            string excelname = $"ListofUsers.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);
        }*/



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
        public IActionResult EditQuiz(int QuizId)
        {

            return View(_context.Quiz.Find(QuizId));

        }

        

        [HttpPost]
        [Route("/Admin/UpdateQuiz")]
        public async Task<IActionResult> UpdateQuiz(int QuizId, string QuizName, string Block, string Course, string Term, DateTime DateCreated, DateTime StartDate, DateTime EndDate)
        {
            

            var quiz = await _context.Quiz.FindAsync(QuizId);
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
        [Route("/Admin/DeleteQuiz")]
        public async Task<IActionResult> DeleteQuiz(int QuizId)
        {
            
            var quiz =  _context.Quiz.Find(QuizId);

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



        /*--------------------------All the functions below here are listed for Students to take Quiz----------------------------------*/


        // View to show the list of assigned quizes for logged in student 

        public async Task<IActionResult> AssignedQuizes()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(currentUserId);

            var quizes = _context.Quiz
                        .FromSqlInterpolated($"select * from Quiz where Course = {user.Course} and Block = {user.Block} and Term = {user.Term}")
                        .ToList();

            return View(quizes);

        }


        public async Task<IActionResult> InstructorQuizesView()
        {

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(currentUserId);

            var quizes = _context.Quiz
                        .FromSqlInterpolated($"select * from Quiz where Course = {user.Course}")
                        .ToList();

            return View(quizes);

        }




        [HttpPost]
        [Route("/Admin/ExportUsersToExcel")]
        public IActionResult ExportUsersToExcel()
        {
            // Create a new Excel workbook
            using (XLWorkbook workbook = new XLWorkbook())
            {

                var userList = _context.Users
                        .FromSqlInterpolated($"select * from AspNetUsers")
                        .ToList();

                // Add a worksheet to the workbook
                var worksheet = workbook.Worksheets.Add("Users");

                // Set the column headers
                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "UserName";
                worksheet.Cell(1, 3).Value = "NormalizedUserName";
                worksheet.Cell(1, 4).Value = "Email";
                worksheet.Cell(1, 5).Value = "NormalizedEmail";
                worksheet.Cell(1, 6).Value = "EmailConfirmed";
                worksheet.Cell(1, 7).Value = "PasswordHash";
                worksheet.Cell(1, 8).Value = "SecurityStamp";
                worksheet.Cell(1, 9).Value = "ConcurrencyStamp";
                worksheet.Cell(1, 10).Value = "PhoneNumber";
                worksheet.Cell(1, 11).Value = "PhoneNumberConfirmed";
                worksheet.Cell(1, 12).Value = "TwoFactorEnabled";
                worksheet.Cell(1, 13).Value = "LockoutEnd";
                worksheet.Cell(1, 14).Value = "LockoutEnabled";
                worksheet.Cell(1, 15).Value = "AccessFailedCount";
                worksheet.Cell(1, 16).Value = "Name";
                worksheet.Cell(1, 17).Value = "Role";
                worksheet.Cell(1, 18).Value = "Block";
                worksheet.Cell(1, 19).Value = "Course";
                worksheet.Cell(1, 20).Value = "Term";
                




                // Set the row values
                for (int i = 0; i < userList.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = userList[i].Id;
                    worksheet.Cell(i + 2, 2).Value = userList[i].UserName;
                    worksheet.Cell(i + 2, 3).Value = userList[i].NormalizedUserName;
                    worksheet.Cell(i + 2, 4).Value = userList[i].Email;
                    worksheet.Cell(i + 2, 5).Value = userList[i].NormalizedEmail;
                    worksheet.Cell(i + 2, 6).Value = userList[i].EmailConfirmed;
                    worksheet.Cell(i + 2, 7).Value = userList[i].PasswordHash;
                    worksheet.Cell(i + 2, 8).Value = userList[i].SecurityStamp;
                    worksheet.Cell(i + 2, 9).Value = userList[i].ConcurrencyStamp;
                    worksheet.Cell(i + 2, 10).Value = userList[i].PhoneNumber;
                    worksheet.Cell(i + 2, 11).Value = userList[i].PhoneNumberConfirmed;
                    worksheet.Cell(i + 2, 12).Value = userList[i].TwoFactorEnabled;
                    worksheet.Cell(i + 2, 13).Value = "";
                    worksheet.Cell(i + 2, 14).Value = userList[i].LockoutEnabled;
                    worksheet.Cell(i + 2, 15).Value = userList[i].AccessFailedCount;
                    worksheet.Cell(i + 2, 16).Value = userList[i].Name;
                    worksheet.Cell(i + 2, 17).Value = userList[i].Role;
                    worksheet.Cell(i + 2, 18).Value = userList[i].Block;
                    worksheet.Cell(i + 2, 19).Value = userList[i].Course;
                    worksheet.Cell(i + 2, 20).Value = userList[i].Term;
                    

                }

                /*// Save the workbook to the provided file path
                workbook.SaveAs(filePath);*/

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "List_of_Users.xlsx");
            }
        }



        public IActionResult ExportQuizesToExcel()
        {
            // Create a new Excel workbook
            using (XLWorkbook workbook = new XLWorkbook())
            {

                var userList = _context.Quiz
                        .FromSqlInterpolated($"select * from Quiz")
                        .ToList();

                // Add a worksheet to the workbook
                var worksheet = workbook.Worksheets.Add("Quizes");

                // Set the column headers
                worksheet.Cell(1, 1).Value = "QuizId";
                worksheet.Cell(1, 2).Value = "QuizName";
                worksheet.Cell(1, 3).Value = "NumColumns";
                worksheet.Cell(1, 4).Value = "DateCreated";
                worksheet.Cell(1, 5).Value = "StartDate";
                worksheet.Cell(1, 6).Value = "EndDate";
                worksheet.Cell(1, 7).Value = "Published";
                worksheet.Cell(1, 8).Value = "FeedBackEnabled";
                worksheet.Cell(1, 9).Value = "NIDAssignment";
                worksheet.Cell(1, 10).Value = "Term";
                worksheet.Cell(1, 11).Value = "Course";
                worksheet.Cell(1, 12).Value = "Block";
                worksheet.Cell(1, 13).Value = "PatientIntro";
                worksheet.Cell(1, 14).Value = "HistoryA";
                worksheet.Cell(1, 15).Value = "HistoryB";
                worksheet.Cell(1, 16).Value = "HistoryC";
                worksheet.Cell(1, 17).Value = "HistoryD";
                worksheet.Cell(1, 18).Value = "HistoryE";
                worksheet.Cell(1, 19).Value = "PhysicalA";
                worksheet.Cell(1, 20).Value = "PhysicalB";
                worksheet.Cell(1, 21).Value = "PhysicalC";
                worksheet.Cell(1, 22).Value = "PhysicalD";
                worksheet.Cell(1, 23).Value = "PhysicalE";
                worksheet.Cell(1, 24).Value = "DiagnosticA";
                worksheet.Cell(1, 25).Value = "DiagnosticB";
                worksheet.Cell(1, 26).Value = "DiagnosticC";
                worksheet.Cell(1, 27).Value = "DiagnosticD";
                worksheet.Cell(1, 28).Value = "DiagnosticE";
                worksheet.Cell(1, 29).Value = "DiagnosisKeyWordsA";
                worksheet.Cell(1, 30).Value = "DiagnosisKeyWordsB";
                worksheet.Cell(1, 31).Value = "DiagnosisKeyWordsC";
                worksheet.Cell(1, 32).Value = "DiagnosisKeyWordsD";
                worksheet.Cell(1, 33).Value = "DiagnosisKeyWordsE";
                worksheet.Cell(1, 34).Value = "FeedBackA";
                worksheet.Cell(1, 35).Value = "FeedBackB";
                worksheet.Cell(1, 36).Value = "FeedBackC";
                worksheet.Cell(1, 37).Value = "FeedBackD";
                worksheet.Cell(1, 38).Value = "FeedBackE";
                worksheet.Cell(1, 39).Value = "Image0";
                worksheet.Cell(1, 40).Value = "Image1";
                worksheet.Cell(1, 41).Value = "Image2";
                worksheet.Cell(1, 42).Value = "Image3";
                worksheet.Cell(1, 43).Value = "Image4";
                worksheet.Cell(1, 44).Value = "Image5";
                worksheet.Cell(1, 45).Value = "Image6";
                worksheet.Cell(1, 46).Value = "Image7";
                worksheet.Cell(1, 47).Value = "Image8";
                worksheet.Cell(1, 48).Value = "Image9";
                worksheet.Cell(1, 49).Value = "ImagePos0";
                worksheet.Cell(1, 50).Value = "ImagePos1";
                worksheet.Cell(1, 51).Value = "ImagePos2";
                worksheet.Cell(1, 52).Value = "ImagePos3";
                worksheet.Cell(1, 53).Value = "ImagePos4";
                worksheet.Cell(1, 54).Value = "ImagePos5";
                worksheet.Cell(1, 55).Value = "ImagePos6";
                worksheet.Cell(1, 56).Value = "ImagePos7";
                worksheet.Cell(1, 57).Value = "ImagePos8";
                worksheet.Cell(1, 58).Value = "ImagePos9";
                

                // Set the row values
                for (int i = 0; i < userList.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = userList[i].QuizId;
                    worksheet.Cell(i + 2, 2).Value = userList[i].QuizName;
                    worksheet.Cell(i + 2, 3).Value = userList[i].NumColumns;
                    worksheet.Cell(i + 2, 4).Value = userList[i].DateCreated;
                    worksheet.Cell(i + 2, 5).Value = userList[i].StartDate;
                    worksheet.Cell(i + 2, 6).Value = userList[i].EndDate;
                    worksheet.Cell(i + 2, 7).Value = userList[i].Published;
                    worksheet.Cell(i + 2, 8).Value = userList[i].FeedBackEnabled;
                    worksheet.Cell(i + 2, 9).Value = userList[i].NIDAssignment;
                    worksheet.Cell(i + 2, 10).Value = userList[i].Term;
                    worksheet.Cell(i + 2, 11).Value = userList[i].Course;
                    worksheet.Cell(i + 2, 12).Value = userList[i].Block;
                    worksheet.Cell(i + 2, 13).Value = userList[i].PatientIntro;
                    worksheet.Cell(i + 2, 14).Value = userList[i].HistoryA;
                    worksheet.Cell(i + 2, 15).Value = userList[i].HistoryB;
                    worksheet.Cell(i + 2, 16).Value = userList[i].HistoryC;
                    worksheet.Cell(i + 2, 17).Value = userList[i].HistoryD;
                    worksheet.Cell(i + 2, 18).Value = userList[i].HistoryE;
                    worksheet.Cell(i + 2, 19).Value = userList[i].PhysicalA;
                    worksheet.Cell(i + 2, 20).Value = userList[i].PhysicalB;
                    worksheet.Cell(i + 2, 21).Value = userList[i].PhysicalC;
                    worksheet.Cell(i + 2, 22).Value = userList[i].PhysicalD;
                    worksheet.Cell(i + 2, 23).Value = userList[i].PhysicalE;
                    worksheet.Cell(i + 2, 24).Value = userList[i].DiagnosticA;
                    worksheet.Cell(i + 2, 25).Value = userList[i].DiagnosticB;
                    worksheet.Cell(i + 2, 26).Value = userList[i].DiagnosticC;
                    worksheet.Cell(i + 2, 27).Value = userList[i].DiagnosticD;
                    worksheet.Cell(i + 2, 28).Value = userList[i].DiagnosticE;
                    worksheet.Cell(i + 2, 29).Value = userList[i].DiagnosisKeyWordsA;
                    worksheet.Cell(i + 2, 30).Value = userList[i].DiagnosisKeyWordsB;
                    worksheet.Cell(i + 2, 31).Value = userList[i].DiagnosisKeyWordsC;
                    worksheet.Cell(i + 2, 32).Value = userList[i].DiagnosisKeyWordsD;
                    worksheet.Cell(i + 2, 33).Value = userList[i].DiagnosisKeyWordsE;
                    worksheet.Cell(i + 2, 34).Value = userList[i].FeedBackA;
                    worksheet.Cell(i + 2, 35).Value = userList[i].FeedBackB;
                    worksheet.Cell(i + 2, 36).Value = userList[i].FeedBackC;
                    worksheet.Cell(i + 2, 37).Value = userList[i].FeedBackD;
                    worksheet.Cell(i + 2, 38).Value = userList[i].FeedBackE;
                    worksheet.Cell(i + 2, 39).Value = userList[i].Image0;
                    worksheet.Cell(i + 2, 40).Value = userList[i].Image1;
                    worksheet.Cell(i + 2, 41).Value = userList[i].Image2;
                    worksheet.Cell(i + 2, 42).Value = userList[i].Image3;
                    worksheet.Cell(i + 2, 43).Value = userList[i].Image4;
                    worksheet.Cell(i + 2, 44).Value = userList[i].Image5;
                    worksheet.Cell(i + 2, 45).Value = userList[i].Image6;
                    worksheet.Cell(i + 2, 46).Value = userList[i].Image7;
                    worksheet.Cell(i + 2, 47).Value = userList[i].Image8;
                    worksheet.Cell(i + 2, 48).Value = userList[i].Image9;
                    worksheet.Cell(i + 2, 49).Value = userList[i].ImagePos0;
                    worksheet.Cell(i + 2, 50).Value = userList[i].ImagePos1;
                    worksheet.Cell(i + 2, 51).Value = userList[i].ImagePos2;
                    worksheet.Cell(i + 2, 52).Value = userList[i].ImagePos3;
                    worksheet.Cell(i + 2, 53).Value = userList[i].ImagePos4;
                    worksheet.Cell(i + 2, 54).Value = userList[i].ImagePos5;
                    worksheet.Cell(i + 2, 55).Value = userList[i].ImagePos6;
                    worksheet.Cell(i + 2, 56).Value = userList[i].ImagePos7;
                    worksheet.Cell(i + 2, 57).Value = userList[i].ImagePos8;
                    worksheet.Cell(i + 2, 58).Value = userList[i].ImagePos9;
                    

                }

                /*// Save the workbook to the provided file path
                workbook.SaveAs(filePath);*/

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "List_of_Quizes.xlsx");

            }

        }



        //POST:Details
        public async Task<IActionResult> QuizDetails(Quiz quiz)
        {
 
            var quizes = _context.Attempt
                        .FromSqlInterpolated($"select * from Attempts where QuizName = {quiz.QuizName}")
                        .ToList();

            return View(quizes.ToList());


        }

        [HttpPost]
        public IActionResult QuizDetailsToExcel(Attempt formData)
        {
            // Create a new Excel workbook
            using (XLWorkbook workbook = new XLWorkbook())
            {
                var userList = _context.Attempt
                        .FromSqlInterpolated($"select * from Attempts where QuizName = {formData.QuizName}")
                        .ToList();

                // Add a worksheet to the workbook
                var worksheet = workbook.Worksheets.Add("Quizes");

                // Set the column headers
                worksheet.Cell(1, 1).Value = "AttemptId";
                worksheet.Cell(1, 2).Value = "StudentNID";
                worksheet.Cell(1, 3).Value = "StudentName";
                worksheet.Cell(1, 4).Value = "QuizName";
                worksheet.Cell(1, 5).Value = "Score";
                worksheet.Cell(1, 6).Value = "NumColumns";
                worksheet.Cell(1, 7).Value = "StartTime";
                worksheet.Cell(1, 8).Value = "EndTime";
                worksheet.Cell(1, 9).Value = "Term";
                worksheet.Cell(1, 10).Value = "Course";
                worksheet.Cell(1, 11).Value = "Block";
                worksheet.Cell(1, 12).Value = "PhysicalAnswerA";
                worksheet.Cell(1, 13).Value = "PhysicalAnswerB";
                worksheet.Cell(1, 14).Value = "PhysicalAnswerC";
                worksheet.Cell(1, 15).Value = "PhysicalAnswerD";
                worksheet.Cell(1, 16).Value = "PhysicalAnswerE";
                worksheet.Cell(1, 17).Value = "DiagnosticAnswerA";
                worksheet.Cell(1, 18).Value = "DiagnosticAnswerB";
                worksheet.Cell(1, 19).Value = "DiagnosticAnswerC";
                worksheet.Cell(1, 20).Value = "DiagnosticAnswerD";
                worksheet.Cell(1, 21).Value = "DiagnosticAnswerE";
                worksheet.Cell(1, 22).Value = "FreeResponseA";
                worksheet.Cell(1, 23).Value = "FreeResponseB";
                worksheet.Cell(1, 24).Value = "FreeResponseC";
                worksheet.Cell(1, 25).Value = "FreeResponseD";
                worksheet.Cell(1, 26).Value = "FreeResponseE";
                worksheet.Cell(1, 27).Value = "NumImage0Clicks";
                worksheet.Cell(1, 28).Value = "NumImage1Clicks";
                worksheet.Cell(1, 29).Value = "NumImage2Clicks";
                worksheet.Cell(1, 30).Value = "NumImage3Clicks";
                worksheet.Cell(1, 31).Value = "NumImage4Clicks";
                worksheet.Cell(1, 32).Value = "NumImage5Clicks";
                worksheet.Cell(1, 33).Value = "NumImage6Clicks";
                worksheet.Cell(1, 34).Value = "NumImage7Clicks";
                worksheet.Cell(1, 35).Value = "NumImage8Clicks";
                worksheet.Cell(1, 36).Value = "NumImage9Clicks";



                // Set the row values
                for (int i = 0; i < userList.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = userList[i].AttemptId;
                    worksheet.Cell(i + 2, 2).Value = userList[i].StudentNID;
                    worksheet.Cell(i + 2, 3).Value = userList[i].StudentName;
                    worksheet.Cell(i + 2, 4).Value = userList[i].QuizName;
                    worksheet.Cell(i + 2, 5).Value = userList[i].Score;
                    worksheet.Cell(i + 2, 6).Value = userList[i].NumColumns;
                    worksheet.Cell(i + 2, 7).Value = userList[i].StartTime;
                    worksheet.Cell(i + 2, 8).Value = userList[i].EndTime;
                    worksheet.Cell(i + 2, 9).Value = userList[i].Term;
                    worksheet.Cell(i + 2, 10).Value = userList[i].Course;
                    worksheet.Cell(i + 2, 11).Value = userList[i].Block;
                    worksheet.Cell(i + 2, 12).Value = userList[i].PhysicalAnswerA;
                    worksheet.Cell(i + 2, 13).Value = userList[i].PhysicalAnswerB;
                    worksheet.Cell(i + 2, 14).Value = userList[i].PhysicalAnswerC;
                    worksheet.Cell(i + 2, 15).Value = userList[i].PhysicalAnswerD;
                    worksheet.Cell(i + 2, 16).Value = userList[i].PhysicalAnswerE;
                    worksheet.Cell(i + 2, 17).Value = userList[i].DiagnosticAnswerA;
                    worksheet.Cell(i + 2, 18).Value = userList[i].DiagnosticAnswerB;
                    worksheet.Cell(i + 2, 19).Value = userList[i].DiagnosticAnswerC;
                    worksheet.Cell(i + 2, 20).Value = userList[i].DiagnosticAnswerD;
                    worksheet.Cell(i + 2, 21).Value = userList[i].DiagnosticAnswerE;
                    worksheet.Cell(i + 2, 22).Value = userList[i].FreeResponseA;
                    worksheet.Cell(i + 2, 23).Value = userList[i].FreeResponseB;
                    worksheet.Cell(i + 2, 24).Value = userList[i].FreeResponseC;
                    worksheet.Cell(i + 2, 25).Value = userList[i].FreeResponseD;
                    worksheet.Cell(i + 2, 26).Value = userList[i].FreeResponseE;
                    worksheet.Cell(i + 2, 27).Value = userList[i].NumImage0Clicks;
                    worksheet.Cell(i + 2, 28).Value = userList[i].NumImage1Clicks;
                    worksheet.Cell(i + 2, 29).Value = userList[i].NumImage2Clicks;
                    worksheet.Cell(i + 2, 30).Value = userList[i].NumImage3Clicks;
                    worksheet.Cell(i + 2, 31).Value = userList[i].NumImage4Clicks;
                    worksheet.Cell(i + 2, 32).Value = userList[i].NumImage5Clicks;
                    worksheet.Cell(i + 2, 33).Value = userList[i].NumImage6Clicks;
                    worksheet.Cell(i + 2, 34).Value = userList[i].NumImage7Clicks;
                    worksheet.Cell(i + 2, 35).Value = userList[i].NumImage8Clicks;
                    worksheet.Cell(i + 2, 36).Value = userList[i].NumImage9Clicks;


                }

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                string filename = $"Quiz Data: {formData.QuizName} {DateTime.Now:MM/dd/yyy}.xlsx";
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",filename);

            }

        }


        public IActionResult ExportQuizFromDetails(Attempt formData)
        {
            // Create a new Excel workbook
            using (XLWorkbook workbook = new XLWorkbook())
            {

                var userList = _context.Quiz
                        .FromSqlInterpolated($"select * from Quiz where QuizName = {formData.QuizName}")
                        .ToList();

                // Add a worksheet to the workbook
                var worksheet = workbook.Worksheets.Add("Quizes");

               
               
                
                worksheet.Cell(1, 1).Value = userList[0].HistoryA;
                worksheet.Cell(1, 2).Value = userList[0].HistoryB;
                worksheet.Cell(1, 3).Value = userList[0].HistoryC;
                worksheet.Cell(1, 4).Value = userList[0].HistoryD;
                worksheet.Cell(1, 5).Value = userList[0].HistoryE;
                worksheet.Cell(2, 1).Value = userList[0].PhysicalA;
                worksheet.Cell(2, 2).Value = userList[0].PhysicalB;
                worksheet.Cell(2, 3).Value = userList[0].PhysicalC;
                worksheet.Cell(2, 4).Value = userList[0].PhysicalD;
                worksheet.Cell(2, 5).Value = userList[0].PhysicalE;
                worksheet.Cell(3, 1).Value = userList[0].DiagnosticA;
                worksheet.Cell(3, 2).Value = userList[0].DiagnosticB;
                worksheet.Cell(3, 3).Value = userList[0].DiagnosticC;
                worksheet.Cell(3, 4).Value = userList[0].DiagnosticD;
                worksheet.Cell(3, 5).Value = userList[0].DiagnosticE;
                worksheet.Cell(4, 1).Value = userList[0].DiagnosisKeyWordsA;
                worksheet.Cell(4, 2).Value = userList[0].DiagnosisKeyWordsB;
                worksheet.Cell(4, 3).Value = userList[0].DiagnosisKeyWordsC;
                worksheet.Cell(4, 4).Value = userList[0].DiagnosisKeyWordsD;
                worksheet.Cell(4, 5).Value = userList[0].DiagnosisKeyWordsE;
                worksheet.Cell(5, 1).Value = userList[0].FeedBackA;
                worksheet.Cell(5, 2).Value = userList[0].FeedBackB;
                worksheet.Cell(5, 3).Value = userList[0].FeedBackC;
                worksheet.Cell(5, 4).Value = userList[0].FeedBackD;
                worksheet.Cell(5, 5).Value = userList[0].FeedBackE;
                    
                   
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                string filename = $" Quiz: {formData.QuizName} {DateTime.Now:MM/dd/yyy}.xlsx";
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);

            }

        }


        public static int StudentCount()
        {
            return 0; 
        }











    }// end Admin Controller

}// CRESME.Controller
