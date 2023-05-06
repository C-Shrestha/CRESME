using CRESME.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult Create(IFormCollection formValues) 
        {
            System.Diagnostics.Debug.WriteLine("Creating Quiz");

            Quiz quiz = new Quiz();
            
            quiz.QuizName= formValues["QuizName"];

            //checkboxes only send output if they are checked(default is "on"), otherwise null
            if (formValues["feedback"] == "On") {
                quiz.FeedBackEnabled = true;
            }
            else {
                quiz.FeedBackEnabled = false;
            }

            if (formValues["publish"] == "On")
            {
                quiz.isPublished = true;
            }
            else
            {
                quiz.isPublished = false;
            }
            
            //might be a better way to convert string to date format
            quiz.StartDate = DateTime.Parse(formValues["startdate"]);
            quiz.EndDate = DateTime.Parse(formValues["enddate"]);

            //might want to limit this to just the day or just the hour
            quiz.DateCreated = DateTime.Now;

            quiz.NIDAssignment = formValues["assignNID"];
            quiz.YearAssignment = formValues["assignYear"];
            quiz.CourseAssignment = formValues["assignCourse"];
            quiz.BlockAssignment = formValues["assignBlock"];

            _context.Add(quiz);
            _context.SaveChanges();

            System.Diagnostics.Debug.WriteLine("Finished Creating Quiz");

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
