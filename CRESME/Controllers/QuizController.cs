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

            //read multiple choice and insert value into ColumnAmount

                
            //checkboxes only send output if they are checked(default is "on"), otherwise null
            if (Request.Form["Feedback"] == "1") 
            {
                quiz.FeedBackEnabled = "true";
            }
            else {
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
            
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (Request.Form.Files["ExcelFileUpload"] != null) {
                try
                {
                    using (var stream = new MemoryStream())
                    {
                        Request.Form.Files["ExcelFileUpload"].CopyToAsync(stream);
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
                }
                catch (Exception error) { 
                    //this should redirect to an error page
                }
            }
            
            quiz.NumImages = Request.Form.Files.Count - 1; //image count = fileuploads - excel upload


            if (Request.Form.Files["imageFile0"] != null) {
                quiz.imageFile0 = UploadImagetoFile(Request.Form.Files["imageFile0"]);
            }
            else {
                quiz.imageFile0 = null;
            }

            if (Request.Form.Files["imageFile1"] != null)
            {
                quiz.imageFile1 = UploadImagetoFile(Request.Form.Files["imageFile1"]);
            }
            else
            {
                quiz.imageFile1 = null;
            }

            if (Request.Form.Files["imageFile2"] != null)
            {
                quiz.imageFile2 = UploadImagetoFile(Request.Form.Files["imageFile2"]);
            }
            else
            {
                quiz.imageFile2 = null;
            }

            if (Request.Form.Files["imageFile3"] != null)
            {
                quiz.imageFile3 = UploadImagetoFile(Request.Form.Files["imageFile3"]);
            }
            else
            {
                quiz.imageFile3 = null;
            }

            if (Request.Form.Files["imageFile4"] != null)
            {
                quiz.imageFile4 = UploadImagetoFile(Request.Form.Files["imageFile4"]);
            }
            else
            {
                quiz.imageFile4 = null;
            }

            if (Request.Form.Files["imageFile5"] != null)
            {
                quiz.imageFile5 = UploadImagetoFile(Request.Form.Files["imageFile5"]);
            }
            else
            {
                quiz.imageFile5 = null;
            }

            if (Request.Form.Files["imageFile6"] != null)
            {
                quiz.imageFile6 = UploadImagetoFile(Request.Form.Files["imageFile6"]);
            }
            else
            {
                quiz.imageFile6 = null;
            }

            if (Request.Form.Files["imageFile7"] != null)
            {
                quiz.imageFile7 = UploadImagetoFile(Request.Form.Files["imageFile7"]);
            }
            else
            {
                quiz.imageFile7 = null;
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
