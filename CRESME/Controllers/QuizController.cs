﻿using CRESME.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using NuGet.DependencyResolver;


namespace CRESME.Controllers
{
    public class QuizController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _environment;

        public QuizController(ApplicationDbContext context, IWebHostEnvironment environment = null)
        {
            _context = context;
            _environment = environment;
        }

        // Authorize makes the page availabe to only to specified Roles
        [Authorize(Roles = "Admin, Instructor")]
        public IActionResult CreateQuiz()
        {
            return View(_context.Users.ToList());
        }

        [Authorize(Roles = "Admin, Instructor, Student")]
        public IActionResult DisplayQuizzes()
        {
            return View(_context.Quiz.ToList());
        }

        [Authorize(Roles = "Admin, Instructor, Student")]
        public IActionResult TakeQuiz(int QuizID = -1)
        {
           
            Quiz quiz;
            if (QuizID != -1)
            {
                quiz = _context.Quiz.Find(QuizID);
                throw new Exception("Unable to find correct Quiz ID");
            }
            else
            {
                quiz = new Quiz();
            }
            return View(quiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult Create(Quiz quiz) //closedxml update
        {
            //checkboxes only send output if they are checked(default is "on"), otherwise null
            if (Request.Form["Feedback"] == "1")
            {
                quiz.FeedBackEnabled = "Yes";
            }
            else
            {
                quiz.FeedBackEnabled = "No";
            }

            if (Request.Form["Publish"] == "1")
            {
                quiz.Published = "Yes";
            }
            else
            {
                quiz.Published = "No";
            }

            quiz.DateCreated = DateTime.Now;
         
            //Reading Excel File upload for quiz info
            if (Request.Form.Files["ExcelFileUpload"] != null)
            {
                try
                {
                    using (var stream = new MemoryStream())
                    {
                        Request.Form.Files["ExcelFileUpload"].CopyToAsync(stream);
                        using (XLWorkbook package = new XLWorkbook(stream))
                        {
                            IXLWorksheet worksheet = package.Worksheets.First();
                            int rowcount = worksheet.RowsUsed().Count();
                            quiz.HistoryA = worksheet.Cell(1, 1).Value.ToString().Trim();
                            quiz.HistoryB = worksheet.Cell(1, 2).Value.ToString().Trim();
                            quiz.HistoryC = worksheet.Cell(1, 3).Value.ToString().Trim();
                            quiz.HistoryD = worksheet.Cell(1, 4).Value.ToString().Trim();

                            quiz.PhysicalA = worksheet.Cell(2, 1).Value.ToString().Trim();
                            quiz.PhysicalB = worksheet.Cell(2, 2).Value.ToString().Trim();
                            quiz.PhysicalC = worksheet.Cell(2, 3).Value.ToString().Trim();
                            quiz.PhysicalD = worksheet.Cell(2, 4).Value.ToString().Trim();

                            quiz.DiagnosticA = worksheet.Cell(3, 1).Value.ToString().Trim();
                            quiz.DiagnosticB = worksheet.Cell(3, 2).Value.ToString().Trim();
                            quiz.DiagnosticC = worksheet.Cell(3, 3).Value.ToString().Trim();
                            quiz.DiagnosticD = worksheet.Cell(3, 4).Value.ToString().Trim();

                            quiz.DiagnosisKeyWordsA = worksheet.Cell(4, 1).Value.ToString().Trim();
                            quiz.DiagnosisKeyWordsB = worksheet.Cell(4, 2).Value.ToString().Trim();
                            quiz.DiagnosisKeyWordsC = worksheet.Cell(4, 3).Value.ToString().Trim();
                            quiz.DiagnosisKeyWordsD = worksheet.Cell(4, 4).Value.ToString().Trim();
                            if (quiz.FeedBackEnabled == "Yes") {
                                quiz.FeedBackA = worksheet.Cell(5, 1).Value.ToString().Trim();
                                quiz.FeedBackB = worksheet.Cell(5, 2).Value.ToString().Trim();
                                quiz.FeedBackC = worksheet.Cell(5, 3).Value.ToString().Trim();
                                quiz.FeedBackD = worksheet.Cell(5, 4).Value.ToString().Trim();
                            }
                            if (quiz.NumColumns == 5)
                            {
                                quiz.HistoryE = worksheet.Cell(1, 5).Value.ToString().Trim();
                                quiz.PhysicalE = worksheet.Cell(2, 5).Value.ToString().Trim();
                                quiz.DiagnosticE = worksheet.Cell(3, 5).Value.ToString().Trim();
                                quiz.DiagnosisKeyWordsE = worksheet.Cell(4, 5).Value.ToString().Trim();
                                quiz.FeedBackE = worksheet.Cell(5, 5).Value.ToString().Trim();
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    throw new Exception("Could not read Excel File.");
                }
            }

            if (Request.Form.Files["Legend"] != null) {
                quiz.Legend = UploadImagetoFile(Request.Form.Files["Legend"]);
            }
            

            //checks for image upload and image position input, only saves image if both are present
            if (Request.Form.Files["imageFile0"] != null & Request.Form["ImagePos0"].Count>0)
            {
                quiz.Image0 = UploadImagetoFile(Request.Form.Files["imageFile0"]);
                quiz.ImagePos0 = Request.Form["ImagePos0"];

            }
            else
            {
                quiz.Image0 = null;
                quiz.ImagePos0 = null;
            }

            if (Request.Form.Files["imageFile1"] != null & Request.Form["ImagePos1"].Count > 0)
            {
                quiz.Image1 = UploadImagetoFile(Request.Form.Files["imageFile1"]);
                quiz.ImagePos1 = Request.Form["ImagePos1"];
            }
            else
            {
                quiz.Image1 = null;
                quiz.ImagePos1 = null;
            }

            if (Request.Form.Files["imageFile2"] != null & Request.Form["ImagePos2"].Count > 0)
            {
                quiz.Image2 = UploadImagetoFile(Request.Form.Files["imageFile2"]);
                quiz.ImagePos2 = Request.Form["ImagePos2"];
            }
            else
            {
                quiz.Image2 = null;
                quiz.ImagePos2 = null;
            }

            if (Request.Form.Files["imageFile3"] != null & Request.Form["ImagePos3"].Count > 0)
            {
                quiz.Image3 = UploadImagetoFile(Request.Form.Files["imageFile3"]);
                quiz.ImagePos3 = Request.Form["ImagePos3"];
            }
            else
            {
                quiz.Image3 = null;
                quiz.ImagePos3 = null;
            }

            if (Request.Form.Files["imageFile4"] != null & Request.Form["ImagePos4"].Count > 0)
            {
                quiz.Image4 = UploadImagetoFile(Request.Form.Files["imageFile4"]);
                quiz.ImagePos4 = Request.Form["ImagePos4"];
            }
            else
            {
                quiz.Image4 = null;
                quiz.ImagePos4 = null;
            }

            if (Request.Form.Files["imageFile5"] != null & Request.Form["ImagePos5"].Count > 0)
            {
                quiz.Image5 = UploadImagetoFile(Request.Form.Files["imageFile5"]);
                quiz.ImagePos5 = Request.Form["ImagePos5"];
            }
            else
            {
                quiz.Image5 = null;
                quiz.ImagePos5 = null;
            }

            if (Request.Form.Files["imageFile6"] != null & Request.Form["ImagePos6"].Count > 0)
            {
                quiz.Image6 = UploadImagetoFile(Request.Form.Files["imageFile6"]);
                quiz.ImagePos6 = Request.Form["ImagePos6"];
            }
            else
            {
                quiz.Image6 = null;
                quiz.ImagePos6 = null;
            }

            if (Request.Form.Files["imageFile7"] != null & Request.Form["ImagePos7"].Count > 0)
            {
                quiz.Image7 = UploadImagetoFile(Request.Form.Files["imageFile7"]);
                quiz.ImagePos7 = Request.Form["ImagePos7"];
            }
            else
            {
                quiz.Image7 = null;
                quiz.ImagePos7 = null;
            }

            if (Request.Form.Files["imageFile8"] != null & Request.Form["ImagePos8"].Count > 0)
            {
                quiz.Image8 = UploadImagetoFile(Request.Form.Files["imageFile8"]);
                quiz.ImagePos8 = Request.Form["ImagePos8"];
            }
            else
            {
                quiz.Image8 = null;
                quiz.ImagePos8 = null;
            }

            if (Request.Form.Files["imageFile9"] != null & Request.Form["ImagePos9"].Count > 0)
            {
                quiz.Image9 = UploadImagetoFile(Request.Form.Files["imageFile9"]);
                quiz.ImagePos9 = Request.Form["ImagePos9"];
            }
            else
            {
                quiz.Image9 = null;
                quiz.ImagePos9 = null;
            }


            if (ModelState.IsValid)
            {
                _context.Add(quiz);
                _context.SaveChanges();
                ViewBag.Result = "Successfully created CRESME.";
            }
            else {
                ViewBag.Result = "Failed to create CRESME.";
            }

            return View("CreateQuiz", _context.Users.ToList());
        }

        public string UploadImagetoFile(IFormFile ImageUpload) {
            try
            {
                string RootPath = this._environment.WebRootPath;
                string ImageName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string ImageGuidExtension = Guid.NewGuid().ToString() + Path.GetExtension(ImageUpload.FileName); //GUID ensures that the image file path is unique
                string newImageName = ImageName + ImageGuidExtension;

                System.IO.Directory.CreateDirectory(RootPath + "/uploadedImages/"); //will create uploadedImages folder if doesnt exist, doesnt do anything if folder exists
                string savepath = Path.Combine(RootPath + "/uploadedImages/", newImageName);
                using (var filestream = new FileStream(savepath, FileMode.Create))
                {
                    ImageUpload.CopyTo(filestream);
                }

                return "/uploadedImages/" + newImageName;
            }
            catch (Exception error) {
                throw new Exception("could not upload image successfully");
            }
        }

        public ActionResult SubmitAttempt(Attempt attempt) {

            return View("DisplayQuizzes");
        }

    }
}
