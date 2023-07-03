using ClosedXML.Excel;
using CRESME.Constants;
using CRESME.Data;
using CRESME.Models;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.IO;
using System.IO.Compression;
using System.Security.Policy;
using NuGet.Versioning;
using System.Configuration;

namespace CRESME.Controllers
{
    public class AdminController : Controller
    {

        //private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;


        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
            _signInManager = signInManager;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Authorize makes the page availabe to only to specified Roles
        [Authorize(Roles = "Admin")]
        public IActionResult CreateAccounts()
        {
            return View();
        }


        /*returns a list of users in the database.*/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListUsers()
        {
            return _context.Users != null ?
                        View(await _userManager.Users.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Test'  is null.");
        }



        /*[HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/ImportExcel")]
        *//*Takes in a properly formated Excel file and create a users (both instructors and studnets).
         Since we used Identity, we opted to keep the UserName Column.
         So, NID(excel upload) == Username (Database) == Email(Database). They are same value.
         *//*
        public async Task<IActionResult> ImportExcel(IFormFile file)

        {
            var list = new List<ApplicationUser>();
            using (var stream = new MemoryStream())
            {

                await file.CopyToAsync(stream);

                using (XLWorkbook workbook = new XLWorkbook(stream))

                {
                    IXLWorksheet worksheet = workbook.Worksheets.First();

                    int rowCount = worksheet.RowsUsed().Count();

                    for (int row = 2; row <= rowCount; row++)
                    {
                        //storing variables for password and for assigning roles later
                        var PasswordHash = worksheet.Cell(row, 3).Value.ToString().Trim();
                        var assignRole = worksheet.Cell(row, 4).Value.ToString().Trim();
                        var nid = worksheet.Cell(row, 1).Value.ToString().Trim();


                        //creating a new user object
                        //columns created by Identity and not used in this app are labeled "false" or null or empty
                        var user = new ApplicationUser
                        {

                            UserName = nid,
                            NormalizedUserName = nid,
                            Email = nid,
                            NormalizedEmail = nid,
                            EmailConfirmed = true,
                            PhoneNumber = "",
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            LockoutEnd = null,
                            LockoutEnabled = true,
                            AccessFailedCount = 0,
                            Name = worksheet.Cell(row, 2).Value.ToString().Trim(),
                            Role = assignRole,
                            Block = worksheet.Cell(row, 5).Value.ToString().Trim(),
                            Course = worksheet.Cell(row, 6).Value.ToString().Trim(),
                            Term = worksheet.Cell(row, 7).Value.ToString().Trim()

                        };

                        //creating a new user
                        var result = await _userManager.CreateAsync(user, PasswordHash);

                        //assigning roles to the user
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

            TempData["AlertMessage"] = "Users created sucessfully!";

            return RedirectToAction("ListUsers");

        } // importToExcel*/



        /*Returns the view for Editing a user*/
        [Authorize(Roles = "Admin")]
        public IActionResult EditUsers(String id)
        {
            //return View();
            return View(_context.Users.Find(id));

        }

        /*Located in the EditUsers.cshtml.This function will edit the user's data*/
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(string Id, string Name, string UserName, string Block, string Course, string Term)
        {
            //find the user to be updated
            var user = await _userManager.FindByIdAsync(Id);

            // update the user data
            if (user != null)
            {
                user.UserName = UserName.Trim();
                user.Email = UserName.Trim(); 
                user.Name = Name.Trim();
                user.Block = Block.Trim();
                user.Course = Course.Trim();
                user.Term = Term.Trim();

                // check if the update was sucessful
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["AlertMessage"] = "User updated sucessfully!";
                    return RedirectToAction("ListUsers");

                }
            }
            else
                ModelState.AddModelError("", "User Not Found");

            return RedirectToAction("CreateAccounts");
        }



        /*Located in ListUsers.cshtml.Returns the view for creating a new user*/
        [Authorize(Roles = "Admin")]
        public IActionResult CreateUsers()
        {
            return View();
        }


        /*Located in CreateUsers.cshtml. This fucntion creates a new user.
         Since we used Identity, we opted to keep the UserName Column.
         So, NID(excel upload) == Username (Database) == Email(Database). They are same value.
         */
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(string PasswordHash, string Role, string UserName, string Name, string Block, string Course, string Term)
        {

  
            if (UserName == null)
            {
                return RedirectToAction("ListUsers");
            }

            // create a new user object
            ApplicationUser user = new ApplicationUser
            {

                UserName = UserName.Trim(),
                Name = Name.Trim(),
                NormalizedUserName = UserName.ToUpper().Trim(),
                Email = UserName.Trim(),
                NormalizedEmail = UserName.ToUpper().Trim(),
                EmailConfirmed = true,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Role = Role.Trim(),
                Block = Block.Trim(),
                Course = Course.Trim(),
                Term = Term.Trim()

            };


            //create user in the database
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

            TempData["AlertMessage"] = "User created sucessfully!";

            return RedirectToAction("ListUsers");
        }


        /*Deletes a user based on the passsed ID*/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            // find the user object in Database
            var user = await _userManager.FindByIdAsync(id);
            

            if (user != null)
            {
                if (user.Role == "Instructor")
                {
                    //find users NID == Username
                    var nid = user.UserName;

                    // in quiz table, whereever the instrustor was assigned a quiz, change that to empty string
                    var matchingRows = _context.Quiz.Where(q => q.InstructorID == nid);

                    foreach (var row in matchingRows)
                    {
                        row.InstructorID = "";
                    }

                    _context.SaveChanges();


                }
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["AlertMessage"] = "User deleted sucessfully!";
                    return RedirectToAction("ListUsers");
                }

            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }

            return RedirectToAction("CreateAccounts");
        }



        /*Located in ListUsers.cshtml. Deletes all Users in the database except for the Admin.*/
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users != null)
            {

                foreach (var item in users)
                {
                    if (item.Role != "Admin")
                    {
                        await _userManager.DeleteAsync(item);
                    }

                }


            }
            TempData["AlertMessage"] = "All users deleted sucessfully!";
            return RedirectToAction("ListUsers");

        }


        /*Returns all the quizes in the database.*/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListAllQuizes()
        {
            return _context.Users != null ?

                        View(await _context.Quiz.ToListAsync()) :

                        Problem("Entity set 'ApplicationDbContext.Test'  is null.");
        }

        /*Located in ListAllQuizes.cshtml.Rerurns a view to edit a CRESME.*/
        [Authorize(Roles = "Admin, Instructor")]
        public IActionResult EditQuiz(int QuizId)
        {

            return View(_context.Quiz.Find(QuizId));

        }



        /*Located in ListAllQuizes.cshtml. Delete a CRESME*/
        [HttpPost]
        [Route("/Admin/DeleteQuiz")]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> DeleteQuiz(int QuizId)
        {
            
            var quiz =  _context.Quiz.Find(QuizId);


            if (quiz != null)
            {
                //deletes images 0 - 9 and legend image for each quiz if they are not null
                string path;
                FileInfo imagefile;
                if (quiz.Legend != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Legend);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image0 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image0);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image1 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image1);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image2 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image2);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image3 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image3);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image4 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image4);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image5 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image5);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image6 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image6);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image7 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image7);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image8 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image8);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image9 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image9);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                

                _context.Remove(quiz);
                _context.SaveChanges();
                TempData["AlertMessage"] = "CRESME deleted sucessfully!";
                return RedirectToAction("ListAllQuizes");

            }
            

            return RedirectToAction("CreateAccounts");
        }


        /*Located in ListAllQuizes.cshtml.Delete all quizes.*/
        [HttpPost]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> DeleteAllQuizes()
        {
            var quizes = await _context.Quiz.ToListAsync();
            if (quizes != null)
            {
                foreach (var item in quizes)
                {
                    DeleteQuiz(item.QuizId);
                }
                TempData["AlertMessage"] = "All CRESME deleted sucessfully!";
                _context.SaveChanges();

            }

            return RedirectToAction("ListUsers");

        }


        /*Located at "Your CRESMES" tab. Returns a list of CRESMES created by the current instructor or created for to the instructor by the Admin. 
         In Database, an instuctor can have only one course. But in reality instuctor might teach many courses.
         So, this fucntion checks CRESMES created by Admin for instuctor and also CRESMES created by instructors themselves.
         */
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> InstructorQuizesView()
        {

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(currentUserId);

            // list of CRESMES created by the Admin + Instructor by CRESME name
            //Here, some CRESMES might be created by Admin so will have Admin's InstructorID in Quiz. To find all CRESMES,
            //we need to check for CRESMES created by instructors themselves also in second query on the bottom
            var quizes1 = _context.Quiz
                        .FromSqlInterpolated($"select * from Quiz where Course = {user.Course}")
                        .ToList();

            // list of CRESMES created by instructor themselves
            var quizes2 = _context.Quiz
                        .FromSqlInterpolated($"select * from Quiz where InstructorID = {user.UserName}")
                        .ToList();

            // adding the two CRESMES together. Final list contains CRESMES created by both Admin and Instructor themselves. 
            quizes1.AddRange(quizes2);


            return View(quizes1.Distinct());

        }



        /*Located in ListAllUsers.cshtml. Returns all the users in database. */
        [HttpPost]
        [Route("/Admin/ExportUsersToExcel")]
        [Authorize(Roles = "Admin")]
        public IActionResult ExportUsersToExcel()
        {
            // Create a new Excel workbook
            using (XLWorkbook workbook = new XLWorkbook())
            {
                //get a list of all users in database
                var userList = _context.Users
                        .FromSqlInterpolated($"select * from AspNetUsers")
                        .ToList();

                // Add a worksheet to the workbook
                var worksheet = workbook.Worksheets.Add("Users");

                // Set the column headers
                
                worksheet.Cell(1, 1).Value = "NID";     
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "PasswordHash";
                worksheet.Cell(1, 4).Value = "Role";
                worksheet.Cell(1, 5).Value = "Block";
                worksheet.Cell(1, 6).Value = "Course";
                worksheet.Cell(1, 7).Value = "Term";

                // Set the row values
                for (int i = 0; i < userList.Count; i++)
                {
                    //NID == UserName == Email. They are all same. We are using UserName to retrive NID. 
                    worksheet.Cell(i + 2, 1).Value = userList[i].UserName;       
                    worksheet.Cell(i + 2, 2).Value = userList[i].Name;

                    //PasswordHash left empty for security purposes. 
                    worksheet.Cell(i + 2, 3).Value = "";
                    worksheet.Cell(i + 2, 4).Value = userList[i].Role;
                    worksheet.Cell(i + 2, 5).Value = userList[i].Block;
                    worksheet.Cell(i + 2, 6).Value = userList[i].Course;
                    worksheet.Cell(i + 2, 7).Value = userList[i].Term;


                }

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "List_of_Users.xlsx");
            }
        }



        /*Located in ListAllQuizes.cshtml. Exports list of quizes and their metadata.
         This does not export InstructorID column for security purpose. 
         */
        [Authorize(Roles = "Admin, Instructor")]
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
                worksheet.Cell(1, 59).Value = "InstructorID";


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
                    worksheet.Cell(i + 2, 59).Value = userList[i].InstructorID;
                    


                }

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "List_of_Quizes.xlsx");

            }

        }

        //POST:Details
        //Returns a list of attempts for a particular quiz.
        //If quiz has not been taken by any student yet, then returns a List of Attempts with one object contaning the QuizName
        //QuizName is required for Excel Generation in Quiz Details page. So, we create a new attempt object with that quiz name and return it.  
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> QuizDetails(Quiz quiz)
        {
 
            var quizes = _context.Attempt
                        .FromSqlInterpolated($"select * from Attempts where QuizName = {quiz.QuizName}")
                        .ToList();

            //if no studnet has taken the quiz yet, return a list of Attempts with QuizName for QuizDetails page
            if (quizes.Count() == 0)
            {
                List<Attempt> temp = new List<Attempt>();
                Attempt attempt = new Attempt();
                attempt.QuizName = quiz.QuizName;
                temp.Add(attempt);
                return View(temp);
            }

            return View(quizes.ToList());

        }

        /*Located in QuizDetails.csthml. Export CRESME data about the particular CRESME being viewed currently. Includes students attempts metadata about the CRESME*/
        [HttpPost]
        [Authorize(Roles = "Admin, Instructor")]
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

                worksheet.Cell(1, 37).Value = "QuizID";
                worksheet.Cell(1, 38).Value = "PatientIntro";
                


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



                    worksheet.Cell(i + 2, 37).Value = userList[i].QuizID;
                    worksheet.Cell(i + 2, 38).Value = userList[i].PatientIntro;
                    


                }

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                string filename = $"Quiz Metadata: {formData.QuizName} {DateTime.Now:MM/dd/yyy}.xlsx";
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",filename);

            }

        }

        //located in QuizDetails.cshtml page. exports images for a quiz including legend
        public IActionResult ExportQuizImages(string quizname) {
            
            Quiz quiz = _context.Quiz.SingleOrDefault(q => q.QuizName == quizname);
            string RootPath = this._environment.WebRootPath;

            var zipName = $"{quiz.QuizName}-images.zip";
            using (MemoryStream ms = new MemoryStream())
            {
                //required: using System.IO.Compression;  
                using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    ZipArchiveEntry entry;
                    FileStream fileStream;
                    Stream entryStream;
                    FileInfo fileInfo;
                    string path;

                    if (quiz.Legend != null)
                    {
                        path = Path.Combine(this._environment.WebRootPath + quiz.Legend);
                        fileInfo = new FileInfo(path);
                        entry = zip.CreateEntry(Path.GetFileName(quiz.Legend));

                        using (fileStream = fileInfo.OpenRead())
                        using (entryStream = entry.Open())
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }


                    if (quiz.Image0 != null)
                    {
                        path = Path.Combine(this._environment.WebRootPath + quiz.Image0);
                        fileInfo = new FileInfo(path);
                        entry = zip.CreateEntry(Path.GetFileName(quiz.Image0));

                        using (fileStream = fileInfo.OpenRead())
                        using (entryStream = entry.Open())
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }
                    if (quiz.Image1 != null)
                    {
                        path = Path.Combine(this._environment.WebRootPath + quiz.Image1);
                        fileInfo = new FileInfo(path);
                        entry = zip.CreateEntry(Path.GetFileName(quiz.Image1));

                        using (fileStream = fileInfo.OpenRead())
                        using (entryStream = entry.Open())
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }
                    if (quiz.Image2 != null)
                    {
                        path = Path.Combine(this._environment.WebRootPath + quiz.Image2);
                        fileInfo = new FileInfo(path);
                        entry = zip.CreateEntry(Path.GetFileName(quiz.Image2));

                        using (fileStream = fileInfo.OpenRead())
                        using (entryStream = entry.Open())
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }
                    if (quiz.Image3 != null)
                    {
                        path = Path.Combine(this._environment.WebRootPath + quiz.Image3);
                        fileInfo = new FileInfo(path);
                        entry = zip.CreateEntry(Path.GetFileName(quiz.Image3));

                        using (fileStream = fileInfo.OpenRead())
                        using (entryStream = entry.Open())
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }
                    if (quiz.Image4 != null)
                    {
                        path = Path.Combine(this._environment.WebRootPath + quiz.Image4);
                        fileInfo = new FileInfo(path);
                        entry = zip.CreateEntry(Path.GetFileName(quiz.Image4));

                        using (fileStream = fileInfo.OpenRead())
                        using (entryStream = entry.Open())
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }
                    if (quiz.Image5 != null)
                    {
                        path = Path.Combine(this._environment.WebRootPath + quiz.Image5);
                        fileInfo = new FileInfo(path);
                        entry = zip.CreateEntry(Path.GetFileName(quiz.Image5));

                        using (fileStream = fileInfo.OpenRead())
                        using (entryStream = entry.Open())
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }
                    if (quiz.Image6 != null)
                    {
                        path = Path.Combine(this._environment.WebRootPath + quiz.Image6);
                        fileInfo = new FileInfo(path);
                        entry = zip.CreateEntry(Path.GetFileName(quiz.Image6));

                        using (fileStream = fileInfo.OpenRead())
                        using (entryStream = entry.Open())
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }
                    if (quiz.Image7 != null)
                    {
                        path = Path.Combine(this._environment.WebRootPath + quiz.Image7);
                        fileInfo = new FileInfo(path);
                        entry = zip.CreateEntry(Path.GetFileName(quiz.Image7));

                        using (fileStream = fileInfo.OpenRead())
                        using (entryStream = entry.Open())
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }
                    if (quiz.Image8 != null)
                    {
                        path = Path.Combine(this._environment.WebRootPath + quiz.Image8);
                        fileInfo = new FileInfo(path);
                        entry = zip.CreateEntry(Path.GetFileName(quiz.Image8));

                        using (fileStream = fileInfo.OpenRead())
                        using (entryStream = entry.Open())
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }
                    if (quiz.Image9 != null)
                    {
                        path = Path.Combine(this._environment.WebRootPath + quiz.Image9);
                        fileInfo = new FileInfo(path);
                        entry = zip.CreateEntry(Path.GetFileName(quiz.Image9));

                        using (fileStream = fileInfo.OpenRead())
                        using (entryStream = entry.Open())
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }




                }
                return File(ms.ToArray(), "application/zip", zipName);
            }
        }

        /*Located in QuizDetails.cshtml page. Exports the currently viewed CRESME. The excel file is formatted and can be used to create another CRESME*/
        [Authorize(Roles = "Admin, Instructor")]
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

        [Authorize(Roles = "Admin, Instructor")]
        public static int StudentCount()
        {
            return 0; 
        }


        //return view with a Term input box that will delete all students with specified Term
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteByTermStudentsView()
        {
            
            return View();

        }

        // deletes all students with the specified Term
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public  IActionResult DeleteByTermStudents(ApplicationUser user)
        {

            var rowsToDelete = _context.Users.Where(e => e.Term == user.Term && e.Role == "Student");
            _context.Users.RemoveRange(rowsToDelete);
            _context.SaveChanges();

            TempData["AlertMessage"] = "Student in the term deleted!";

            return RedirectToAction("ListUsers");

        }

        //return view with a Term input box that will delete all instructors with specified Term
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteByTermInstructorView()
        {

            return View();

        }

        // deletes all instructors with the specified Term
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteByTermInstructors(ApplicationUser user)
        {
            
            var rowsToDelete = _context.Users.Where(e => e.Term == user.Term && e.Role == "Instructor");


            var term = user.Term;
            var matchingUsers = _context.Users.Where(user => user.Term == term);

            // change quizes with deleted instructors to empty string for InstructorID. 
            foreach (var users in matchingUsers)
            {
                //find users NID == Username
                var nid = users.UserName;

                // in quiz table, whereever the instrustor was assigned a quiz, change that to empty string
                var matchingRows = _context.Quiz.Where(q => q.InstructorID == nid);

                foreach (var row in matchingRows)
                {
                    row.InstructorID = "";
                }

                
            }
            
            //remove users
            _context.Users.RemoveRange(rowsToDelete);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Instructor in the term deleted!";

            return RedirectToAction("ListUsers");

        }

        //return view with a Block input box that will delete all students with specified BLock
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteByBlockStudentsView()
        {

            return View();

        }

        // deletes all students with the specified Block
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteByBlockStudents(ApplicationUser user)
        {

            var rowsToDelete = _context.Users.Where(e => e.Block == user.Block && e.Role == "Student");
            _context.Users.RemoveRange(rowsToDelete);
            _context.SaveChanges();

            TempData["AlertMessage"] = "Student in the block deleted!";

            return RedirectToAction("ListUsers");

        }

        //return view with a Block input box that will delete all instructors with specified Block
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteByBlockInstructorView()
        {

            return View();

        }

        // deletes all instructors with the specified Block
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteByBlockInstructors(ApplicationUser user)
        {

            var rowsToDelete = _context.Users.Where(e => e.Block == user.Block && e.Role == "Instructor");

            var block = user.Block;
            var matchingUsers = _context.Users.Where(user => user.Block == block);

            // change quizes with deleted instructors to empty string for InstructorID. 
            foreach (var users in matchingUsers)
            {
                //find users NID == Username
                var nid = users.UserName;

                // in quiz table, whereever the instrustor was assigned a quiz, change that to empty string
                var matchingRows = _context.Quiz.Where(q => q.InstructorID == nid);

                foreach (var row in matchingRows)
                {
                    row.InstructorID = "";
                }


            }




            _context.Users.RemoveRange(rowsToDelete);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Instructor in the term deleted!";
            return RedirectToAction("ListUsers");

        }

        //POST:Delete all attempts
        [HttpPost]
        public async Task<IActionResult> DeleteAllAttempts()
        {

            var attempts = await _context.Attempt.ToListAsync();
            if (attempts != null)
            {

                foreach (var item in attempts)
                {

                    _context.Remove(item);

                }

                _context.SaveChanges();


            }
            return RedirectToAction("ListUsers");

        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEntireDatabase()
        {
            // delete all quizes
            await DeleteAllQuizes();

            //delete all attempts
            await DeleteAllAttempts();

            //delete all users
            await DeleteAll();

            //remove all images from the database, redundant as DeleteAllQuizes call DeleteQuiz, which removes images for each quiz
            
            string RootPath = this._environment.WebRootPath;
            string dirpath = RootPath + "/uploadedImages/";
            System.IO.DirectoryInfo dir = new DirectoryInfo(dirpath);
            foreach (FileInfo file in dir.GetFiles()) { 
                file.Delete();
            }            
           

            TempData["AlertMessage"] = "Entire database has been deleted!";

            return RedirectToAction("ListUsers"); 
        }


        //return view with a Block input box that will delete all students with specified BLock
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteByCourseStudentsView()
        {

            return View();

        }

        // deletes all students with the specified Block
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteByCourseStudents(ApplicationUser user)
        {

            var rowsToDelete = _context.Users.Where(e => e.Course == user.Course && e.Role == "Student");

            _context.Users.RemoveRange(rowsToDelete);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Student in the course deleted!";
            return RedirectToAction("ListUsers");

        }

        //return view with a Block input box that will delete all instructors with specified Block
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteByCourseInstructorView()
        {

            return View();

        }

        // deletes all instructors with the specified Block
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteByCourseInstructors(ApplicationUser user)
        {

            var rowsToDelete = _context.Users.Where(e => e.Course == user.Course && e.Role == "Instructor");

            var course = user.Course;
            var matchingUsers = _context.Users.Where(user => user.Course == course);

            // change quizes with deleted instructors to empty string for InstructorID. 
            foreach (var users in matchingUsers)
            {
                //find users NID == Username
                var nid = users.UserName;

                // in quiz table, whereever the instrustor was assigned a quiz, change that to empty string
                var matchingRows = _context.Quiz.Where(q => q.InstructorID == nid);

                foreach (var row in matchingRows)
                {
                    row.InstructorID = "";
                }


            }

            _context.Users.RemoveRange(rowsToDelete);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Instructor in the course deleted!";
            return RedirectToAction("ListUsers");

        }



        /*compares two list, one of Quiz and one of Attempts. 
         * comapres and returns a list of quizes that have not been attempted yet by the students.*/
        public static List<Quiz> CompareLists(List<Quiz> list1, List<Attempt> list2)
        {
            // Retrieve the IDs from list2
            var idsList2 = list2.Select(obj => obj.QuizID);

            // Filter the objects in list1 based on the IDs not present in list2
            var result = list1.Where(obj => !idsList2.Contains(obj.QuizId)).ToList();

            return result;
        }




        /*Located in AssignedQuizes.cshtml. Returns a list of CRESMES assigned to a particular student which have not been taken yet.
         CRESMES returned are also Published by the user. But Feedback is "No" i.e. not practice CRESMES. 
         *//*
        [Authorize(Roles = "Admin, Instructor,Student")]
        public async Task<IActionResult> AssignedQuizes()
        {
            //get the current studnets object
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(currentUserId);
            List<Quiz> quizes = new List<Quiz>();

            // list of CRESMES assinged to the user that are NOT practice(FeedbackEnabled) CRESMES
            var assignedQuizes = _context.Quiz
                        .FromSqlInterpolated($"select * from Quiz where Course = {user.Course} and Block = {user.Block} and Term = {user.Term} and FeedBackEnabled = {"No"} and Published = {"Yes"}")
                        .ToList();

            // list of quizes already taken by the user
            var takenQuizes = _context.Attempt
                        .FromSqlInterpolated($"select * from Attempts where StudentID = {user.Id}")
                        .ToList();

            // if no quizes have been assingend to the student yet, return an empty list of quizes 
            if(assignedQuizes.Count() == 0)
            {
                return View(quizes); 
            }
            else
            {
                // if no quizes have been taken yet by the student, return the list with all quizes assigned to the student
                if(takenQuizes.Count()  == 0)
                {
                    return View(assignedQuizes);
                }
                else
                {
                    // some quizes have been assigned and some quizes have been taken already by the studnet. 
                    // look and find the quizes yet to be taken. 

                    // Compare the lists
                    List<Quiz> result = CompareLists(assignedQuizes, takenQuizes);

                    // if result is empty, return an empty list of quizes
                    if (result.Count() == 0) 
                    {
                        return View(new List<Quiz>());
                    }
                    else
                    {
                        return View(result.Distinct()); 
                    }
                }

            }



        } // end assigned quizes*/




        /*Located in PracticeQuizes.cshtml. Returns a list of CRESMES assigned to student for practice.*//*
        [Authorize(Roles = "Admin, Instructor,Student")]
        public async Task<IActionResult> PracticeQuizes()
        {
          
            //get the current studnets object
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(currentUserId);

            // list of practice CRESME assinged to the student that are published and feedback is enabled.
            var feedbackQuizes = _context.Quiz
                        .FromSqlInterpolated($"select * from Quiz where FeedBackEnabled = {"Yes"} and Published = {"Yes"}")
                        .ToList();

            return View(feedbackQuizes);



        }*/




        /*Located in PastQuizes.cshtml.cshtml. Returns a list of quizes already completed by a particular student.*//*
        [Authorize(Roles = "Admin, Instructor,Student")]
        public async Task<IActionResult> PastQuizes()
        {
            

            //get the current studnets object
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(currentUserId);

            // list of quizes already taken by the user
            var TakenQuizes = _context.Attempt
                        .FromSqlInterpolated($"select * from Attempts where StudentID = {user.Id}")
                        .ToList();

            return View(TakenQuizes); 



        } // end past quizes*/







        /*Located in nav-link "ALL CRESMES"
         * Returns all the quizes in the database.
         Function is same as ListAllQuizs Function.
         Difference is in the View Page, the Quizees are only viewable, not editable. 
         */
            [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> AllCresmeInstructors()
        {
            return _context.Users != null ?

                        View(await _context.Quiz.ToListAsync()) :

                        Problem("Entity set 'ApplicationDbContext.Test'  is null.");
        }


        /*//POST:Details
        //Located in InstructorQuizView Page. 
        //Returns a list of attempts for a particular quiz.
        //If quiz has not been taken by any student yet, then returns a List of Attempts with one object contaning the QuizName
        //QuizName is required for Excel Generation in Quiz Details page. So, we create a new attempt object with that quiz name and return it.  
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> InstructorQuizDetails(Quiz quiz)
        {

            var quizes = _context.Attempt
                        .FromSqlInterpolated($"select * from Attempts where QuizName = {quiz.QuizName}")
                        .ToList();

            //if no studnet has taken the quiz yet, return a list of Attempts with QuizName for QuizDetails page
            if (quizes.Count() == 0)
            {
                List<Attempt> temp = new List<Attempt>();
                Attempt attempt = new Attempt();
                attempt.QuizName = quiz.QuizName;
                temp.Add(attempt);
                return View(temp);
            }

            return View(quizes.ToList());

        }*/



        /*Located in ListAllQuizes.cshtml.Rerurns a view to edit a CRESME.*/
        [Authorize(Roles = "Admin, Instructor")]
        public IActionResult InstructorEditQuiz(int QuizId)
        {

            return View(_context.Quiz.Find(QuizId));

        }

        /*Located in EditQuiz.cshtml. Updates a CRESME*/
        [HttpPost]    
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> InstructorUpdateQuiz(int QuizId, string QuizName, string Block, string Course, string Term, DateTime DateCreated, DateTime StartDate, DateTime EndDate, string PatientIntro, string Published, string FeedBackEnabled, string InstructorID)
        {

            // find the quiz to be updated
            var quiz = await _context.Quiz.FindAsync(QuizId);
            //find current user
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(currentUserId);

            //update based on the new values
            if (quiz != null)
            {
                quiz.QuizName = QuizName.Trim();
                quiz.Block = Block.Trim();
                quiz.Course = Course.Trim();
                quiz.Term = Term.Trim();
                quiz.DateCreated = DateCreated;
                quiz.StartDate = StartDate;
                quiz.EndDate = EndDate;
                quiz.PatientIntro = PatientIntro.Trim();
                if (InstructorID != null)
                {
                    quiz.InstructorID = InstructorID.Trim();
                }
                

                //checkboxes is checked and changed to correct format for database entry
                if (FeedBackEnabled == "1")
                {
                    quiz.FeedBackEnabled = "Yes";
                }
                else
                {
                    quiz.FeedBackEnabled = "No";
                }

                if (Published == "1")
                {
                    quiz.Published = "Yes";
                }
                else
                {
                    quiz.Published = "No";
                }



                _context.SaveChanges();
                TempData["AlertMessage"] = "CRESME updated sucessfully!";
                if (user.Role == "Admin")
                {
                    return RedirectToAction("ListAllQuizes");
                }
                else
                {
                    return RedirectToAction("InstructorQuizesView");
                }
                

            }
            else
            {
                if (user.Role == "Admin")
                {
                    return RedirectToAction("ListAllQuizes");
                }
                else
                {
                    return RedirectToAction("InstructorQuizesView");
                }

            }
                

            
        }



        /*Located in InstructorQuizView.cshtml. Delete a CRESME*/
        [HttpPost]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> InstructorDeleteQuiz(int QuizId)
        {

            var quiz = _context.Quiz.Find(QuizId);


            if (quiz != null)
            {
                //deletes images 0 - 9 and legend image for each quiz if they are not null
                string path;
                FileInfo imagefile;
                if (quiz.Legend != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Legend);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image0 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image0);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image1 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image1);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image2 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image2);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image3 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image3);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image4 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image4);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image5 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image5);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image6 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image6);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image7 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image7);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image8 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image8);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }
                if (quiz.Image9 != null)
                {
                    path = Path.Combine(this._environment.WebRootPath + quiz.Image9);
                    imagefile = new FileInfo(path);
                    if (imagefile.Exists)
                    {
                        imagefile.Delete();
                    }
                }


                _context.Remove(quiz);
                _context.SaveChanges();
                TempData["AlertMessage"] = "CRESME deleted sucessfully!";
                return RedirectToAction("InstructorQuizesView");

            }


            return RedirectToAction("InstructorQuizesView");
        }


        /*-----------------------------------------Multiple Users--------------------------------------------------*/



        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Admin/ImportExcel")]
        /*Takes in a properly formated Excel file and create a users(both instructors and studnets).
         Since we used Identity, we opted to keep the UserName Column.
         So, NID(excel upload) == Username (Database) == Email(Database). They are same value.*/


        public async Task<IActionResult> ImportExcel(IFormFile file)
        //public async IActionResult ImportExcel(IFormFile file)
        {
            var newUsersList = new List<ApplicationUser>();

            using (var stream = new MemoryStream())
            {

                await file.CopyToAsync(stream);

                using (XLWorkbook workbook = new XLWorkbook(stream))

                {
                    IXLWorksheet worksheet = workbook.Worksheets.First();

                    int rowCount = worksheet.RowsUsed().Count();

                    for (int row = 2; row <= rowCount; row++)
                    {

                        var password = GenerateRandomPassword();

                        //var password = worksheet.Cell(row, 7).Value.ToString().Trim();


                        var nid = worksheet.Cell(row, 1).Value.ToString().Trim();
                        var name = worksheet.Cell(row, 2).Value.ToString().Trim();
                        var role = worksheet.Cell(row, 3).Value.ToString().Trim();
                        var block = worksheet.Cell(row, 4).Value.ToString().Trim();
                        var course = worksheet.Cell(row, 5).Value.ToString().Trim();
                        var term = worksheet.Cell(row, 6).Value.ToString().Trim();

                        // if nid does not exit, then create a new account with that nid. Store in list to export to user. 
                        var nidExits = _userManager.Users.Where(e => e.UserName == nid).FirstOrDefault();

                        // if nid exits, change nid data in DB
                        if (nidExits != null)
                        {
                            nidExits.Block = string.Join(", ", nidExits.Block, block).Trim();
                            nidExits.Course = string.Join(", ", nidExits.Course, course).Trim();
                            nidExits.Term = string.Join(", ", nidExits.Term, term).Trim();
                        }

                        // if nid does not not exit, create new user and add in newUsersList
                        else
                        {

                            //creating a new user object
                            //columns created by Identity and not used in this app are labeled "false" or null or empty
                            var user = new ApplicationUser
                            {
                                UserName = nid,
                                NormalizedUserName = nid,
                                Email = nid,
                                NormalizedEmail = nid,
                                EmailConfirmed = true,
                                PhoneNumber = "",
                                PhoneNumberConfirmed = false,
                                TwoFactorEnabled = false,
                                LockoutEnd = null,
                                LockoutEnabled = true,
                                AccessFailedCount = 0,
                                Name = name,
                                Role = role,
                                Block = block,
                                Course = course,
                                Term = term

                            };

                            //creating a new user
                            var result = await _userManager.CreateAsync(user, password);

                            //assigning roles to the user
                            if (role == "Instructor")
                            {
                                await _userManager.AddToRoleAsync(user, Roles.Instructor.ToString());
                            }

                            if (role == "Student")
                            {
                                await _userManager.AddToRoleAsync(user, Roles.Student.ToString());
                            }


                            // adding the newly user to the newUsersList to export as excel
                            //before adding to excel, change the password to actual value so that can be sent to the students. 

                            var newUser = new ApplicationUser
                            {
                                UserName = nid,
                                NormalizedUserName = nid,
                                Email = nid,
                                NormalizedEmail = nid,
                                EmailConfirmed = true,
                                PhoneNumber = "",
                                PhoneNumberConfirmed = false,
                                TwoFactorEnabled = false,
                                LockoutEnd = null,
                                LockoutEnabled = true,
                                AccessFailedCount = 0,
                                Name = name,
                                Role = role,
                                Block = block,
                                Course = course,
                                Term = term, 
                                PasswordHash = password


                            };


                            newUsersList.Add(newUser);

                        }


                    }// end for loop



                }
            }

            //_context.Users.AddRange(list);

            _context.SaveChanges();

            TempData["AlertMessage"] = "Users created sucessfully!";
            return ExportExcelForMail(newUsersList);

            //return RedirectToAction("ListUsers");

        } // importToExcel


        /// <summary>
        /// Generates a Random Password
        /// respecting the given strength requirements.
        /// </summary>
        /// <param name="opts">A valid PasswordOptions object
        /// containing the password strength requirements.</param>
        /// <returns>A random password</returns>
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }



        /*Located in ListAllUsers.cshtml. Returns all the users in database. */
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ExportExcelForMail(List<ApplicationUser> userList)
        {
            // Create a new Excel workbook
            using (XLWorkbook workbook = new XLWorkbook())
            {

                // Add a worksheet to the workbook
                var worksheet = workbook.Worksheets.Add("Users");

                // Set the column headers

                worksheet.Cell(1, 1).Value = "NID";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Password";
                worksheet.Cell(1, 4).Value = "Role";
                //worksheet.Cell(1, 5).Value = "Block";
                //worksheet.Cell(1, 6).Value = "Course";
               // worksheet.Cell(1, 7).Value = "Term";

                // Set the row values
                for (int i = 0; i < userList.Count; i++)
                {
                    //NID == UserName == Email. They are all same. We are using UserName to retrive NID. 
                    worksheet.Cell(i + 2, 1).Value = userList[i].UserName;
                    worksheet.Cell(i + 2, 2).Value = userList[i].Name;
                    worksheet.Cell(i + 2, 3).Value = userList[i].PasswordHash;
                    worksheet.Cell(i + 2, 4).Value = userList[i].Role;
                    //worksheet.Cell(i + 2, 5).Value = userList[i].Block;
                    //worksheet.Cell(i + 2, 6).Value = userList[i].Course;
                    //worksheet.Cell(i + 2, 7).Value = userList[i].Term;


                }

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CRESME Users Mailing List.xlsx");
            }
        }



        /*Located in AssignedQuizes.cshtml. Returns a list of CRESMES assigned to a particular student which have not been taken yet.
         CRESMES returned are also Published by the user. But Feedback is "No" i.e. not practice CRESMES. 
         */
        [Authorize(Roles = "Admin, Instructor,Student")]
        public async Task<IActionResult> AssignedQuizes()
        {
            //get the current studnets object
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(currentUserId);
            List<Quiz> quizes = new List<Quiz>();
            var blockList = user.Block.Split(",");
            var courseList = user.Course.Split(",");
            var termList = user.Term.Split(",");


            for (int i = 0; i < blockList.Count(); i++)
            {
                // list of CRESMES assinged to the user that are NOT practice(FeedbackEnabled) CRESMES
                var assignedQuizes = _context.Quiz
                            .FromSqlInterpolated($"select * from Quiz where Course = {courseList[i].Trim()} and Block = {blockList[i].Trim()} and Term = {termList[i].Trim()} and FeedBackEnabled = {"No"} and Published = {"Yes"}")
                            .ToList();
                // list of quizes already taken by the user
                var takenQuizes = _context.Attempt
                            .FromSqlInterpolated($"select * from Attempts where StudentID = {user.Id}")
                            .ToList();

                // if no quizes have been assingend to the student yet, return an empty list of quizes 
                if (assignedQuizes.Count() == 0)
                {
                    //return View(quizes);
                    ;
                }
                else
                {
                    // if no quizes have been taken yet by the student, return the list with all quizes assigned to the student
                    if (takenQuizes.Count() == 0)
                    {
                        //return View(assignedQuizes);
                        quizes.AddRange(assignedQuizes); 
                    }
                    else
                    {
                        // some quizes have been assigned and some quizes have been taken already by the studnet. 
                        // look and find the quizes yet to be taken. 

                        // Compare the lists
                        List<Quiz> result = CompareLists(assignedQuizes, takenQuizes);

                        // if result is empty, return an empty list of quizes
                        if (result.Count() == 0)
                        {
                            //return View(new List<Quiz>());
                            ;
                        }
                        else
                        {
                            //return View(result.Distinct());
                            quizes.AddRange(result); 
                        }
                    }

             
                
                }




            }//for loop 


            return View(quizes.Distinct());
            

        } // end assigned quizes



        /*Located in PastQuizes.cshtml.cshtml. Returns a list of quizes already completed by a particular student.*/
        [Authorize(Roles = "Admin, Instructor,Student")]
        public async Task<IActionResult> PastQuizes()
        {
            //get the current studnets object
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(currentUserId);

            // list of quizes already taken by the user
            var TakenQuizes = _context.Attempt
                        .FromSqlInterpolated($"select * from Attempts where StudentID = {user.Id}")
                        .ToList();

            return View(TakenQuizes);



        } // end past quizes



        /*Located in PracticeQuizes.cshtml. Returns a list of CRESMES assigned to student for practice.*/
        [Authorize(Roles = "Admin, Instructor,Student")]
        public async Task<IActionResult> PracticeQuizes()
        {

            //get the current studnets object
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(currentUserId);

            // list of practice CRESME assinged to the student that are published and feedback is enabled.
            var feedbackQuizes = _context.Quiz
                        .FromSqlInterpolated($"select * from Quiz where FeedBackEnabled = {"Yes"} and Published = {"Yes"}")
                        .ToList();

            return View(feedbackQuizes);



        }

        /*returns a list of users in the database.*/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> StudentDetails(string id)
        {
            return _context.Users != null ?
                        View(await _userManager.FindByIdAsync(id)) :
                        Problem("Entity set 'ApplicationDbContext.Test'  is null.");
        }




        /*Deletes a user based on the passsed ID*/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(int index, string id)
        {
            /*//get the current studnets object
            //var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(student.Id);

            // user is the object passed which contains the course which must be deleted from the student.
            var course = student.Course;
            
            // check if the student has more than one course
            if (user.Course.Split(",").Count() == 1)
            {
                // if the student is enrolled in only one course, delete the entire student.
                //_userManager.DeleteAsync(user);
                //TempData["Success"] = "Course deleted from user sucessfully! Since student was only enrolled in one course, student was alos deleted.";

            }
            // else, if studentt has more than one course, delete the specified course*/




            /* if (user != null)
             {

                 IdentityResult result = await _userManager.DeleteAsync(user);
                 if (result.Succeeded)
                 {
                     TempData["Success"] = "Course deleted from user sucessfully!";
                     return RedirectToAction("ListUsers");
                 }
                 else
                 {
                     TempData["AlertMessage"] = "Failed to delete Course!";
                     return RedirectToAction("ListUsers");

                 }

             }
             else
             {
                 ModelState.AddModelError("", "User Not Found");
                 return RedirectToAction("ListUsers");

             }*/


            //else student is null
            //return error message
            if (id == null)
            {
                TempData["AlertMessage"] = "Failed to delete Course! Student does not exist!";
                return Redirect(Request.Headers["Referer"].ToString());

            }
            //if student in not null
            else
            {
                
                //get the student with that user ID
                //var user = _context.Users.SingleOrDefault(user => user.Id == student.Id);
                
                //get user data to update
                var user = await _userManager.FindByIdAsync(id);

                /*//get the couse to be removed along with respective block and term
                var courseRemove = student.Course;
                var blockRemove = student.Block;
                var termRemove = student.Term;*/

                /*// list of course a studnet is currently enrolled in
                var courseList = user.Course; 

                //remove the course
                string updatedList = RemoveStringFromList(courseList, courseRemove);

                // add back the new course list
                user.Course = updatedList; 

                await _userManager.UpdateAsync(user);*/

                // remove the block, course, term at "index" for student
                user.Block = RemoveStringAtIndex(user.Block, index);
                user.Course = RemoveStringAtIndex(user.Course, index); 
                user.Term = RemoveStringAtIndex(user.Term, index);

                await _userManager.UpdateAsync(user);

                TempData["Success"] = "Course removed from user sucessfully!";
                return Redirect(Request.Headers["Referer"].ToString());


            }

        }

        


        public static string RemoveStringFromList(string inputString, string stringToRemove)
        {
            // Split the input string into an array of strings
            string[] stringArray = inputString.Split(", ");
            

            // Convert the array into a list
            List<string> stringList = stringArray.ToList();

            // Remove the desired string from the list
            stringList.Remove(stringToRemove.Trim());

            // Join the updated list elements back into a string
            string updatedString = string.Join(",", stringList);

            return updatedString;
        }

        public static string RemoveStringAtIndex(string inputString, int index)
        {
            // Split the input string into an array of strings
            string[] stringArray = inputString.Split(", ");

            // Convert the array into a list
            List<string> stringList = stringArray.ToList();

            // remove element at the index
            stringList.RemoveAt(index);

            //convert list to a single string
            string updatedString = string.Join(",", stringList);

            return updatedString; 
            
            
        }





    }// end Admin Controller

}// CRESME.Controller
