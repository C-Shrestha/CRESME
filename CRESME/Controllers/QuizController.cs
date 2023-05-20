using CRESME.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;

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


        public QuizController(ApplicationDbContext context, IWebHostEnvironment environment = null)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult Create(Quiz quiz) 
        {
            //checkboxes only send output if they are checked(default is "on"), otherwise null
            if (Request.Form["feedback"] == "on") 
            {
                quiz.FeedBackEnabled = "true";
            }
            else {
                quiz.FeedBackEnabled = "false";
            }

            if (Request.Form["publish"] == "on")
            {
                quiz.Published = "true";
            }
            else
            {
                quiz.Published = "false";
            }
            
            //might want to limit this to just the day or just the hour
            quiz.DateCreated = DateTime.Now;
            
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                Request.Form.Files["fileUpload"].CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                    quiz.HistoryA = worksheet.Cells[1, 1].Value.ToString().Trim();
                    quiz.HistoryB = worksheet.Cells[1, 2].Value.ToString().Trim();
                    quiz.HistoryC = worksheet.Cells[1, 3].Value.ToString().Trim();
                    quiz.HistoryD = worksheet.Cells[1, 4].Value.ToString().Trim();

                    quiz.PhysicalA = worksheet.Cells[2, 1].Value.ToString().Trim();
                    quiz.PhysicalB = worksheet.Cells[2, 2].Value.ToString().Trim();
                    quiz.PhysicalC = worksheet.Cells[2, 3].Value.ToString().Trim();
                    quiz.PhysicalD = worksheet.Cells[2, 4].Value.ToString().Trim();
                    
                    quiz.DiagnosticA = worksheet.Cells[3, 1].Value.ToString().Trim();
                    quiz.DiagnosticB = worksheet.Cells[3, 2].Value.ToString().Trim();
                    quiz.DiagnosticC = worksheet.Cells[3, 3].Value.ToString().Trim();
                    quiz.DiagnosticD = worksheet.Cells[3, 4].Value.ToString().Trim();
                    
                    quiz.DiagnosisA = worksheet.Cells[4, 1].Value.ToString().Trim();
                    quiz.DiagnosisB = worksheet.Cells[4, 2].Value.ToString().Trim();
                    quiz.DiagnosisC = worksheet.Cells[4, 3].Value.ToString().Trim();
                    quiz.DiagnosisD = worksheet.Cells[4, 4].Value.ToString().Trim();

                    quiz.KeyWordsA = worksheet.Cells[5, 1].Value.ToString().Trim();
                    quiz.KeyWordsB = worksheet.Cells[5, 2].Value.ToString().Trim();
                    quiz.KeyWordsC = worksheet.Cells[5, 3].Value.ToString().Trim();
                    quiz.KeyWordsD = worksheet.Cells[5, 4].Value.ToString().Trim();

                    quiz.FeedBackA = worksheet.Cells[6, 1].Value.ToString().Trim();
                    quiz.FeedBackB = worksheet.Cells[6, 2].Value.ToString().Trim();
                    quiz.FeedBackC = worksheet.Cells[6, 3].Value.ToString().Trim();
                    quiz.FeedBackD = worksheet.Cells[6, 4].Value.ToString().Trim();
                }
            }

            quiz.NumImages = Request.Form.Files.Count - 1; //image count = fileuploads - excel upload

            switch (quiz.NumImages) {
                case 10:
                    quiz.Image10 = UploadImagetoFile(Request.Form.Files["Image10"]);
                    goto case 9;
                case 9:
                    quiz.Image9 = UploadImagetoFile(Request.Form.Files["Image9"]);
                    goto case 8;
                case 8:
                    quiz.Image8 = UploadImagetoFile(Request.Form.Files["Image8"]);
                    goto case 7;
                case 7:
                    quiz.Image7 = UploadImagetoFile(Request.Form.Files["Image7"]);
                    goto case 6;
                case 6:
                    quiz.Image6 = UploadImagetoFile(Request.Form.Files["Image6"]);
                    goto case 5;
                case 5:
                    quiz.Image5 = UploadImagetoFile(Request.Form.Files["Image5"]);
                    goto case 4;
                case 4:
                    quiz.Image4 = UploadImagetoFile(Request.Form.Files["Image4"]);
                    goto case 3;
                case 3:
                    quiz.Image3 = UploadImagetoFile(Request.Form.Files["Image3"]);
                    goto case 2;
                case 2:
                    quiz.Image2 = UploadImagetoFile(Request.Form.Files["Image2"]);
                    goto case 1;
                case 1:
                    quiz.Image1 = UploadImagetoFile(Request.Form.Files["Image1"]);
                    break;
                default: //no images or more than 10 images somehow
                    break;
            }
           


            _context.Add(quiz);
            _context.SaveChanges();


            return View("CreateQuiz");
        }

        public string UploadImagetoFile(IFormFile ImageUpload) {
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
