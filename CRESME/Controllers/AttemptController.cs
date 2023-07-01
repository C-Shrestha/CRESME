using CRESME.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.DependencyResolver;
using System.Security.Claims;
using System.Globalization;

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


            attempt.EndTime = DateTime.Now;

            attempt.QuizID = Int32.Parse(Request.Form["QuizStringID"]);
            if (attempt.QuizID != null)
            {
                attempt.QuizID = Int32.Parse(Request.Form["QuizStringID"]);
                ParentQuiz = _context.Quiz.Find(attempt.QuizID); //gets Quiz model data
                if (ParentQuiz == null)
                {
                    ParentQuiz = new Quiz();
                }
                attempt.QuizName = ParentQuiz.QuizName;
                attempt.PatientIntro = ParentQuiz.PatientIntro;
                attempt.NumColumns = ParentQuiz.NumColumns;
            }
            else
            {
                throw new Exception("Quiz ID is null in attempt submission");
            }



            //  ASsIGNING KEYS
            //assigns correct database answers as keys for physical and diagnostic answers, and constructs the free response answer key 

            //COLUMN ONE
            AssignColumnKeys(ParentQuiz, "column1", ref PhysicalAnswer1, ref DiagnosticAnswer1, ref DiagnosisAnswerKey1);
            //COLUMN TWO
            AssignColumnKeys(ParentQuiz, "column2", ref PhysicalAnswer2, ref DiagnosticAnswer2, ref DiagnosisAnswerKey2);
            //COLUMN THREE
            AssignColumnKeys(ParentQuiz, "column3", ref PhysicalAnswer3, ref DiagnosticAnswer3, ref DiagnosisAnswerKey3);
            //COLUMN FOUR
            AssignColumnKeys(ParentQuiz, "column4", ref PhysicalAnswer4, ref DiagnosticAnswer4, ref DiagnosisAnswerKey4);
            //COLUMN FIVE
            if (ParentQuiz.NumColumns == 5) {
                AssignColumnKeys(ParentQuiz, "column5", ref PhysicalAnswer5, ref DiagnosticAnswer5, ref DiagnosisAnswerKey5);
            }
            //  SCORING 
            attempt.Score = 0;
            //COLUMN ONE
            if (attempt.FreeResponseA != null)
            {
                foreach (string key in DiagnosisAnswerKey1)
                {
                    if (attempt.FreeResponseA.Contains(key) && (attempt.PhysicalAnswerA == PhysicalAnswer1 || attempt.DiagnosticAnswerA == DiagnosticAnswer1))
                    {
                        attempt.Score += 3;
                        if (attempt.PhysicalAnswerA == PhysicalAnswer1)
                        {
                            attempt.Score += 1;
                        }
                        if (attempt.DiagnosticAnswerA == DiagnosticAnswer1)
                        {
                            attempt.Score += 1;
                        }
                        break;
                    }
                }
            }
            //COLUMN TWO
            if (attempt.FreeResponseB != null)
            {
                foreach (string key in DiagnosisAnswerKey2)
                {
                    if (attempt.FreeResponseB.Contains(key) && (attempt.PhysicalAnswerB == PhysicalAnswer2 || attempt.DiagnosticAnswerB == DiagnosticAnswer2))
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

                        break;
                    }
                }
            }
            //COLUMN THREE
            if (attempt.FreeResponseC != null)
            {
                foreach (string key in DiagnosisAnswerKey3)
                {
                    if (attempt.FreeResponseC.Contains(key) && (attempt.PhysicalAnswerC == PhysicalAnswer3 || attempt.DiagnosticAnswerC == DiagnosticAnswer3))
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

                        break;
                    }
                }
            }
            //COLUMN FOUR
            if (attempt.FreeResponseD != null)
            {
                foreach (string key in DiagnosisAnswerKey4)
                {
                    if (attempt.FreeResponseD.Contains(key) && (attempt.PhysicalAnswerD == PhysicalAnswer4 || attempt.DiagnosticAnswerD == DiagnosticAnswer4))
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

                        break;
                    }
                }
            }
            //COLUMN FIVE
            if (attempt.FreeResponseE != null)
            {
                foreach (string key in DiagnosisAnswerKey5)
                {
                    if (attempt.FreeResponseE.Contains(key) && (attempt.PhysicalAnswerE == PhysicalAnswer5 || attempt.DiagnosticAnswerE == DiagnosticAnswer5))
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

                        break;
                    }
                }
            }

            //  UNSHUFFLING COLUMNS
            string[] OriginalAttemptAnswers = { attempt.PhysicalAnswerA, attempt.PhysicalAnswerB, attempt.PhysicalAnswerC, attempt.PhysicalAnswerD, attempt.PhysicalAnswerE,
                                                attempt.DiagnosticAnswerA, attempt.DiagnosticAnswerB, attempt.DiagnosticAnswerC, attempt.DiagnosticAnswerD, attempt.DiagnosticAnswerE,
                                                attempt.FreeResponseA, attempt.FreeResponseB, attempt.FreeResponseC, attempt.FreeResponseD, attempt.FreeResponseE};
            //COLUMN ONE
            (attempt.PhysicalAnswerA, attempt.DiagnosticAnswerA, attempt.FreeResponseA) = UnshuffleColumn("1", OriginalAttemptAnswers);
            //COLUMN TWO
            (attempt.PhysicalAnswerB, attempt.DiagnosticAnswerB, attempt.FreeResponseB) = UnshuffleColumn("2", OriginalAttemptAnswers);
            //COLUMN THREE
            (attempt.PhysicalAnswerC, attempt.DiagnosticAnswerC, attempt.FreeResponseC) = UnshuffleColumn("3", OriginalAttemptAnswers);
            //COLUMN FOUR
            (attempt.PhysicalAnswerD, attempt.DiagnosticAnswerD, attempt.FreeResponseD) = UnshuffleColumn("4", OriginalAttemptAnswers);
            //COLUMN FIVE
            if (ParentQuiz.NumColumns == 5)
            {
                (attempt.PhysicalAnswerE, attempt.DiagnosticAnswerE, attempt.FreeResponseE) = UnshuffleColumn("5", OriginalAttemptAnswers);
            }

            
            //CRESME meta data
            if (ParentQuiz.Image0 != null) {
                attempt.Image0Name = ParentQuiz.Image0[16..^40];
            }
            if (ParentQuiz.Image1 != null)
            {
                attempt.Image1Name = ParentQuiz.Image1[16..^40];
            }
            if (ParentQuiz.Image2 != null)
            {
                attempt.Image2Name = ParentQuiz.Image2[16..^40];
            }
            if (ParentQuiz.Image3 != null)
            {
                attempt.Image3Name = ParentQuiz.Image3[16..^40];
            }
            if (ParentQuiz.Image4 != null)
            {
                attempt.Image4Name = ParentQuiz.Image4[16..^40];
            }
            if (ParentQuiz.Image5 != null)
            {
                attempt.Image5Name = ParentQuiz.Image5[16..^40];
            }
            if (ParentQuiz.Image6 != null)
            {
                attempt.Image6Name = ParentQuiz.Image6[16..^40];
            }
            if (ParentQuiz.Image7 != null)
            {
                attempt.Image7Name = ParentQuiz.Image7[16..^40];
            }
            if (ParentQuiz.Image8 != null)
            {
                attempt.Image8Name = ParentQuiz.Image8[16..^40];
            }
            if (ParentQuiz.Image9 != null)
            {
                attempt.Image9Name = ParentQuiz.Image9[16..^40];
            }
           

            //student info
            var CurrentStudent = _context.Users.SingleOrDefault( user => user.UserName == User.Identity.Name);
            attempt.StudentID = CurrentStudent.Id;
            attempt.StudentNID = CurrentStudent.UserName;
            attempt.StudentName = CurrentStudent.Name;
            attempt.Course = CurrentStudent.Course;
            attempt.Block = CurrentStudent.Block;
            attempt.Term = CurrentStudent.Term;
            if (ModelState.IsValid && User.IsInRole("Student"))
            {
                _context.Add(attempt);
                _context.SaveChanges();
            }
           return View("_LoginPartial");
        }



        public void AssignColumnKeys(Quiz ParentQuiz, string columnID, ref string PhysicalAnswer, ref string DiagnosticAnswer, ref List<string> DiagnosisAnswerKey)
        {     
            if (Request.Form[columnID] == "1")
            {
                PhysicalAnswer = "1";
                DiagnosticAnswer = "1";
                DiagnosisAnswerKey = new List<string>(ParentQuiz                        
                                        .DiagnosisKeyWordsA.Split(',')                 //finds and splits free response key
                                        .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces   
            }
            else if (Request.Form[columnID] == "2")
            {
                PhysicalAnswer = "2";
                DiagnosticAnswer = "2";
                DiagnosisAnswerKey = new List<string>(ParentQuiz
                                        .DiagnosisKeyWordsB.Split(',')                 //finds and splits free response key
                                        .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces 
            }
            else if (Request.Form[columnID] == "3")
            {
                PhysicalAnswer = "3";
                DiagnosticAnswer = "3";
                DiagnosisAnswerKey = new List<string>(ParentQuiz
                                        .DiagnosisKeyWordsC.Split(',')                 //finds and splits free response key
                                        .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces 
            }
            else if (Request.Form[columnID] == "4")
            {
                PhysicalAnswer = "4";
                DiagnosticAnswer = "4";
                DiagnosisAnswerKey = new List<string>(ParentQuiz
                                        .DiagnosisKeyWordsD.Split(',')                 //finds and splits free response key
                                        .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces 
            }
            else if (Request.Form[columnID] == "5")
            {
                PhysicalAnswer = "5";
                DiagnosticAnswer = "5";
                DiagnosisAnswerKey = new List<string>(ParentQuiz
                                        .DiagnosisKeyWordsE.Split(',')                 //finds and splits free response key
                                        .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces 
            }
            else
            {
                PhysicalAnswer = "error";
            }
        }

        (string, string, string) UnshuffleColumn(string ColumnID, string[] attemptAnswers) {
            if (Request.Form["Column1"] == ColumnID)
            {
                return (attemptAnswers[0], attemptAnswers[5], attemptAnswers[10]);
            }
            else if (Request.Form["Column2"] == ColumnID)
            {
                return (attemptAnswers[1], attemptAnswers[6], attemptAnswers[11]);
            }
            else if (Request.Form["Column3"] == ColumnID)
            {
                return (attemptAnswers[2], attemptAnswers[7], attemptAnswers[12]);
            }
            else if (Request.Form["Column4"] == ColumnID)
            {
                return (attemptAnswers[3], attemptAnswers[8], attemptAnswers[13]);
            }
            else if (Request.Form["Column5"] == ColumnID)
            {
                return (attemptAnswers[4], attemptAnswers[9], attemptAnswers[14]);
            }
            else {
                throw new Exception("Unshuffling column error");
                return ("column error","column error","column error");
            }
        }





    }
}
