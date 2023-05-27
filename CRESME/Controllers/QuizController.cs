using CRESME.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CRESME.Controllers
{
    public class QuizController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _environment;

        // Authorize makes the page availabe to only to specified Roles
        [Authorize(Roles = "Admin, Instructor")]
        public IActionResult CreateQuiz()
        {
            return View();
        }

        public IActionResult TakeQuiz() {
            Quiz quiz = _context.Quiz.Find("test");
            return View(quiz);
        }

        public QuizController(ApplicationDbContext context, IWebHostEnvironment environment = null)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult Create(Quiz quiz) //closedxml update
        {
            //checkboxes only send output if they are checked(default is "on"), otherwise null
            if (Request.Form["Feedback"] == "1")
            {
                quiz.FeedBackEnabled = "true";
            }
            else
            {
                quiz.FeedBackEnabled = "false";
            }

            if (Request.Form["Publish"] == "1")
            {
                quiz.Published = "true";
            }
            else
            {
                quiz.Published = "false";
            }

            //might want to limit this to just the day or just the hour
            quiz.DateCreated = DateTime.Now;


         
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

                            quiz.DiagnosisA = worksheet.Cell(4, 1).Value.ToString().Trim();
                            quiz.DiagnosisB = worksheet.Cell(4, 2).Value.ToString().Trim();
                            quiz.DiagnosisC = worksheet.Cell(4, 3).Value.ToString().Trim();
                            quiz.DiagnosisD = worksheet.Cell(4, 4).Value.ToString().Trim();

                            quiz.KeyWordsA = worksheet.Cell(5, 1).Value.ToString().Trim();
                            quiz.KeyWordsB = worksheet.Cell(5, 2).Value.ToString().Trim();
                            quiz.KeyWordsC = worksheet.Cell(5, 3).Value.ToString().Trim();
                            quiz.KeyWordsD = worksheet.Cell(5, 4).Value.ToString().Trim();

                            quiz.FeedBackA = worksheet.Cell(6, 1).Value.ToString().Trim();
                            quiz.FeedBackB = worksheet.Cell(6, 2).Value.ToString().Trim();
                            quiz.FeedBackC = worksheet.Cell(6, 3).Value.ToString().Trim();
                            quiz.FeedBackD = worksheet.Cell(6, 4).Value.ToString().Trim();
                        }
                    }
                }
                catch (Exception error)
                {
                    //this should redirect to an error page
                }
            }


            if (Request.Form.Files["imageFile0"] != null & Request.Form["ImagePos0"].Count>0)
            {
                quiz.Image1 = UploadImagetoFile(Request.Form.Files["imageFile0"]);
                quiz.Image1Pos = Request.Form["ImagePos0"];
            }
            else
            {
                quiz.Image1 = null;
            }

            if (Request.Form.Files["imageFile1"] != null)
            {
                quiz.Image2 = UploadImagetoFile(Request.Form.Files["imageFile1"]);
            }
            else
            {
                quiz.Image2 = null;
            }

            if (Request.Form.Files["imageFile2"] != null)
            {
                quiz.Image3 = UploadImagetoFile(Request.Form.Files["imageFile2"]);
            }
            else
            {
                quiz.Image3 = null;
            }

            if (Request.Form.Files["imageFile3"] != null)
            {
                quiz.Image4 = UploadImagetoFile(Request.Form.Files["imageFile3"]);
            }
            else
            {
                quiz.Image4 = null;
            }

            if (Request.Form.Files["imageFile4"] != null)
            {
                quiz.Image5 = UploadImagetoFile(Request.Form.Files["imageFile4"]);
            }
            else
            {
                quiz.Image5 = null;
            }

            if (Request.Form.Files["imageFile5"] != null)
            {
                quiz.Image6 = UploadImagetoFile(Request.Form.Files["imageFile5"]);
            }
            else
            {
                quiz.Image6 = null;
            }

            if (Request.Form.Files["imageFile6"] != null)
            {
                quiz.Image7 = UploadImagetoFile(Request.Form.Files["imageFile6"]);
            }
            else
            {
                quiz.Image7 = null;
            }

            if (Request.Form.Files["imageFile7"] != null)
            {
                quiz.Image8 = UploadImagetoFile(Request.Form.Files["imageFile7"]);
            }
            else
            {
                quiz.Image8 = null;
            }


            if (ModelState.IsValid)
            {
                _context.Add(quiz);
                _context.SaveChanges();
            }

            return View("CreateQuiz");
        }

        public string UploadImagetoFile(IFormFile ImageUpload) { //make this async
            string RootPath = this._environment.WebRootPath;
            string ImageName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
            string ImageGuidExtension = Guid.NewGuid().ToString() + Path.GetExtension(ImageUpload.FileName); //GUID ensures that the image file path is unique
            string newImageName = ImageName + ImageGuidExtension;
            string savepath = Path.Combine(RootPath + "/images/", newImageName);
            
            using (var filestream = new FileStream(savepath, FileMode.Create))
            {
                ImageUpload.CopyTo(filestream);
            }

                return "/images/" + newImageName;
        }

    }
}
