using CRESME.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.DependencyResolver;
using System.Security.Claims;

namespace CRESME.Controllers
{
    public class AttemptController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _environment;

        public AttemptController(ApplicationDbContext context, IWebHostEnvironment environment = null)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitAttempt(Attempt attempt) {
            
            Quiz ParentQuiz = new Quiz();
            
            string PhysicalAnswer1 = "";
            string PhysicalAnswer2 = "";
            string PhysicalAnswer3 = "";
            string PhysicalAnswer4 = "";
            string PhysicalAnswer5 = "";

            string DiagnosticAnswer1 = "";
            string DiagnosticAnswer2 = "";
            string DiagnosticAnswer3 = "";
            string DiagnosticAnswer4 = "";
            string DiagnosticAnswer5 = "";

            List<string> DiagnosisAnswerKey1 = new List<string>();
            List<string> DiagnosisAnswerKey2 = new List<string>();
            List<string> DiagnosisAnswerKey3 = new List<string>();
            List<string> DiagnosisAnswerKey4 = new List<string>();
            List<string> DiagnosisAnswerKey5 = new List<string>();


            attempt.QuizID =Int32.Parse(Request.Form["QuizStringID"]);
            if (attempt.QuizID != null)
            {
                attempt.QuizID = Int32.Parse(Request.Form["QuizStringID"]);
                ParentQuiz = _context.Quiz.Find(attempt.QuizID); //gets Quiz model data
                if (ParentQuiz == null)
                {
                    ParentQuiz = new Quiz();
                }
            }
            else
            {
                //return quiz id null error
            }
            
            

            //  ASsIGNING KEYS
            //assigns correct database answers as keys for physical and diagnostic answers, and constructs the free response answer key 

            //COLUMN ONE
            AssignColumnKeys(ParentQuiz, "column1", PhysicalAnswer1, DiagnosticAnswer1, DiagnosisAnswerKey1);
            //COLUMN TWO
            AssignColumnKeys(ParentQuiz, "column2", PhysicalAnswer2, DiagnosticAnswer2, DiagnosisAnswerKey2);
            //COLUMN THREE
            AssignColumnKeys(ParentQuiz, "column3", PhysicalAnswer3, DiagnosticAnswer3, DiagnosisAnswerKey3);
            //COLUMN FOUR
            AssignColumnKeys(ParentQuiz, "column4", PhysicalAnswer4, DiagnosticAnswer4, DiagnosisAnswerKey4);
            //COLUMN FIVE
            if (ParentQuiz.NumColumns == 5) {
                AssignColumnKeys(ParentQuiz, "column5", PhysicalAnswer5, DiagnosticAnswer5, DiagnosisAnswerKey5);
            }
            //  SCORING 
            attempt.Score = 0;
            //COLUMN ONE
            foreach(string key in DiagnosisAnswerKey1){
                if (attempt.FreeResponseA.Contains(key)) {
                    attempt.Score += 3;
                    if (attempt.PhysicalAnswerA == PhysicalAnswer1) {
                        attempt.Score += 1;
                    }
                    if (attempt.DiagnosticAnswerA == DiagnosticAnswer1)
                    {
                        attempt.Score += 1;
                    }
                    else
                    {
                        attempt.Score -= 3;
                    }
                    break;
                }
            }
            //COLUMN TWO
            foreach (string key in DiagnosisAnswerKey2)
            {
                if (attempt.FreeResponseB.Contains(key))
                {
                    attempt.Score += 3;
                    if (attempt.PhysicalAnswerB == PhysicalAnswer2)
                    {
                        attempt.Score += 1;
                    }
                    if (attempt.DiagnosticAnswerB == DiagnosticAnswer2)
                    {
                        attempt.Score += 1;
                    }
                    else
                    {
                        attempt.Score -= 3;
                    }
                    break;
                }
            }
            //COLUMN THREE
            foreach (string key in DiagnosisAnswerKey3)
            {
                if (attempt.FreeResponseC.Contains(key))
                {
                    attempt.Score += 3;
                    if (attempt.PhysicalAnswerC == PhysicalAnswer3)
                    {
                        attempt.Score += 1;
                    }
                    if (attempt.DiagnosticAnswerC == DiagnosticAnswer3)
                    {
                        attempt.Score += 1;
                    }
                    else
                    {
                        attempt.Score -= 3;
                    }
                    break;
                }
            }
            //COLUMN FOUR
            foreach (string key in DiagnosisAnswerKey4)
            {
                if (attempt.FreeResponseD.Contains(key))
                {
                    attempt.Score += 3;
                    if (attempt.PhysicalAnswerD == PhysicalAnswer4)
                    {
                        attempt.Score += 1;
                    }
                    if (attempt.DiagnosticAnswerD == DiagnosticAnswer4)
                    {
                        attempt.Score += 1;
                    }
                    else
                    {
                        attempt.Score -= 3;
                    }
                    break;
                }
            }
            //COLUMN FIVE
            foreach (string key in DiagnosisAnswerKey5)
            {
                if (attempt.FreeResponseE.Contains(key))
                {
                    attempt.Score += 3;
                    if (attempt.PhysicalAnswerE == PhysicalAnswer5)
                    {
                        attempt.Score += 1;
                    }
                    if (attempt.DiagnosticAnswerE == DiagnosticAnswer5)
                    {
                        attempt.Score += 1;
                    }
                    else {
                        attempt.Score -= 3;
                    }
                    break;
                }
            }

            //student info
            //var CurrentUserID = User.FindFirst(ClaimTypes.NameIdentifier);
            //var CurrentStudent = _context.Users.Find(CurrentUserID.ToString());
            //attempt.StudentID = CurrentStudent.Id;
          

            //if (ModelState.IsValid && ParentQuiz.FeedBackEnabled!="Yes")
            //{
                _context.Add(attempt);
                _context.SaveChanges();
            //}
           return View("_LoginPartial");
        }



        public void AssignColumnKeys(Quiz ParentQuiz, string columnID, string PhysicalAnswer, string DiagnosticAnswer, List<string> DiagnosisAnswerKey) {
            if (Request.Form[columnID] == 1)
            {
                PhysicalAnswer = "1";
                DiagnosticAnswer = "1";
                DiagnosisAnswerKey = new List<string>(ParentQuiz                        
                                        .DiagnosisKeyWordsA.Split(',')                 //finds and splits free response key
                                        .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces   
            }
            else if (Request.Form[columnID] == 2)
            {
                PhysicalAnswer = "2";
                DiagnosticAnswer = "2";
                DiagnosisAnswerKey = new List<string>(ParentQuiz
                                        .DiagnosisKeyWordsA.Split(',')                 //finds and splits free response key
                                        .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces 
            }
            else if (Request.Form[columnID] == 3)
            {
                PhysicalAnswer = "3";
                DiagnosticAnswer = "3";
                DiagnosisAnswerKey = new List<string>(ParentQuiz
                                        .DiagnosisKeyWordsA.Split(',')                 //finds and splits free response key
                                        .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces 
            }
            else if (Request.Form[columnID] == 4)
            {
                PhysicalAnswer = "4";
                DiagnosticAnswer = "4";
                DiagnosisAnswerKey = new List<string>(ParentQuiz
                                        .DiagnosisKeyWordsA.Split(',')                 //finds and splits free response key
                                        .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces 
            }
            else if (Request.Form[columnID] == 5)
            {
                PhysicalAnswer = "5";
                DiagnosticAnswer = "5";
                DiagnosisAnswerKey = new List<string>(ParentQuiz
                                        .DiagnosisKeyWordsA.Split(',')                 //finds and splits free response key
                                        .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces 
            }
            else
            {
                //return answer column could not be read
            }
        }





        
    }
}
