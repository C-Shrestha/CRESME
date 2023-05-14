using CRESME.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;


namespace CRESME.Controllers
{
    public class QuizController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Authorize makes the page availabe to only to specified Roles
        [Authorize(Roles = "Admin, Instructor")]
        public IActionResult CreateQuiz()
        {
            return View();
        }


        public QuizController(ApplicationDbContext context) {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult Create(Quiz quiz) 
        {
            //checkboxes only send output if they are checked(default is "on"), otherwise null
            if (Request.Form["feedback"] == "on") {
                quiz.FeedBackEnabled = "true";
            }
            else {
                quiz.FeedBackEnabled = "false";
            }

            if (Request.Form["publish"] == "on")
            {
                quiz.isPublished = "true";
            }
            else
            {
                quiz.isPublished = "false";
            }
            
            //might want to limit this to just the day or just the hour
            quiz.DateCreated = DateTime.Now;

            quiz.NIDAssignment = Request.Form["NIDAssignment"];
            quiz.CourseAssignment = Request.Form["CourseAssignment"];
            quiz.BlockAssignment = Request.Form["BlockAssignment"];
            
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var stream = new MemoryStream())
            {
                Request.Form.Files[0].CopyToAsync(stream);
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
            
            _context.Add(quiz);
            _context.SaveChanges();


            return View("CreateQuiz");
        }


        /*
        [HttpPost]

        public IActionResult Index(IFormFile file, [FromServices] IWebHostEnvironment hostEnvironment)
        {
            string fileName = $"{hostEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            var quizzes = this.GetQuizList(file.FileName);
            return Index(quizzes);
        }
        private List<Quiz> GetQuizList(string fName)
        {
            List<Quiz> quizzes = new List<Quiz>();
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        quizzes.Add(new Quiz()
                        {
                            History = reader.GetValue(0).ToString(),
                            Physical = reader.GetValue(1).ToString(), //maybe add .trim()??
                            Diagnostic = reader.GetValue(2).ToString(),
                            Diagnosis = reader.GetValue(3).ToString()
                        });
                        System.Diagnostics.Debug.Print(reader.GetValue(0).ToString());
                        System.Diagnostics.Debug.Print(quizzes[0].History);
                    }
                }
            }

            return quizzes;
        }
        */


    }
}
