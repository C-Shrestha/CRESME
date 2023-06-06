using CRESME.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.DependencyResolver;


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

  
        public IActionResult TakeQuiz(int quizid) {
            Quiz quiz;
            if (quizid != null) {
                quiz = _context.Quiz.Find(quizid);
            }
            else {
                quiz = new Quiz();         
            }
            return View(quiz);            
        }

        public ActionResult SubmitAttempt(Attempt attempt) {
            
            Quiz ParentQuiz = new Quiz();
            ApplicationUser CurrentStudent = new ApplicationUser();
            
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

            if (attempt.StudentNID != null)         
            {
                CurrentStudent = _context.Users.Find(attempt.StudentNID); //gets current student model data
                if (CurrentStudent == null)
                {
                    //return student could not be found
                }
                else if(){ 
                    //redirect to download pdf without uploading attempt? ask Melissa
                }
            }
            else
            {
                //return student nid null error
            }
            attempt.StudentName = CurrentStudent.Name;

            if (attempt.QuizID != null)
            {
                ParentQuiz = _context.Quiz.Find(attempt.QuizID); //gets Quiz model data
                if (ParentQuiz == null)
                {
                    //return quiz context null error
                }
            }
            else
            {
                //return id null error
            }


            //  ASsIGNING KEYS
            //assigns correct database answers as keys for physical and diagnostic answers, and constructs the free response answer key 

            //COLUMN ONE
            AssignColumnKeys("column1", attempt.QuizID, PhysicalAnswer1, DiagnosticAnswer1, DiagnosisAnswerKey1);
            //COLUMN TWO
            AssignColumnKeys("column2", attempt.QuizID, PhysicalAnswer2, DiagnosticAnswer2, DiagnosisAnswerKey2);
            //COLUMN THREE
            AssignColumnKeys("column3", attempt.QuizID, PhysicalAnswer3, DiagnosticAnswer3, DiagnosisAnswerKey3);
            //COLUMN FOUR
            AssignColumnKeys("column4", attempt.QuizID, PhysicalAnswer4, DiagnosticAnswer4, DiagnosisAnswerKey4);
            //COLUMN FIVE
            AssignColumnKeys("column5", attempt.QuizID, PhysicalAnswer5, DiagnosticAnswer5, DiagnosisAnswerKey5);

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

            if (ModelState.IsValid)
            {
                _context.Add(attempt);
                _context.SaveChanges();
            }
            return View("TakeQuiz", ParentQuiz);
        }



        public void AssignColumnKeys(string columnID, int QuizID, string PhysicalAnswer, string DiagnosticAnswer, List<string> DiagnosisAnswerKey) {
            if (Request.Form[columnID] == 1)
            {
                AssignKeysA(QuizID, PhysicalAnswer, DiagnosticAnswer, DiagnosisAnswerKey);
            }
            else if (Request.Form[columnID] == 2)
            {
                AssignKeysB(QuizID, PhysicalAnswer, DiagnosticAnswer, DiagnosisAnswerKey);
            }
            else if (Request.Form[columnID] == 3)
            {
                AssignKeysC(QuizID, PhysicalAnswer, DiagnosticAnswer, DiagnosisAnswerKey);
            }
            else if (Request.Form[columnID] == 4)
            {
                AssignKeysD(QuizID, PhysicalAnswer, DiagnosticAnswer, DiagnosisAnswerKey);
            }
            else if (Request.Form[columnID] == 5)
            {
                AssignKeysE(QuizID, PhysicalAnswer, DiagnosticAnswer, DiagnosisAnswerKey);
            }
            else
            {
                //return answer column could not be read
            }
        }




        public void AssignKeysA(int QuizID,string PhysicalAnswer, string DiagnosticAnswer, List<string> DiagnosisAnswerKey) { 
            PhysicalAnswer = _context.Quiz.Find(QuizID).PhysicalA;
            DiagnosticAnswer = _context.Quiz.Find(QuizID).DiagnosticA;
            DiagnosisAnswerKey = new List<string>(_context.Quiz.Find(QuizID)       //finds Quiz
                                    .DiagnosisKeyWordsA.Split(',')                 //finds and splits free response key
                                    .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces        
        }

        public void AssignKeysB(int QuizID, string PhysicalAnswer, string DiagnosticAnswer, List<string> DiagnosisAnswerKey)
        {
            PhysicalAnswer = _context.Quiz.Find(QuizID).PhysicalB;
            DiagnosticAnswer = _context.Quiz.Find(QuizID).DiagnosticB;
            DiagnosisAnswerKey = new List<string>(_context.Quiz.Find(QuizID)       //finds Quiz
                                    .DiagnosisKeyWordsB.Split(',')                 //finds and splits free response key
                                    .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces        
        }

        public void AssignKeysC(int QuizID, string PhysicalAnswer, string DiagnosticAnswer, List<string> DiagnosisAnswerKey)
        {
            PhysicalAnswer = _context.Quiz.Find(QuizID).PhysicalC;
            DiagnosticAnswer = _context.Quiz.Find(QuizID).DiagnosticC;
            DiagnosisAnswerKey = new List<string>(_context.Quiz.Find(QuizID)       //finds Quiz
                                    .DiagnosisKeyWordsC.Split(',')                 //finds and splits free response key
                                    .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces        
        }

        public void AssignKeysD(int QuizID, string PhysicalAnswer, string DiagnosticAnswer, List<string> DiagnosisAnswerKey)
        {
            PhysicalAnswer = _context.Quiz.Find(QuizID).PhysicalD;
            DiagnosticAnswer = _context.Quiz.Find(QuizID).DiagnosticD;
            DiagnosisAnswerKey = new List<string>(_context.Quiz.Find(QuizID)       //finds Quiz
                                    .DiagnosisKeyWordsD.Split(',')                 //finds and splits free response key
                                    .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces        
        }

        public void AssignKeysE(int QuizID, string PhysicalAnswer, string DiagnosticAnswer, List<string> DiagnosisAnswerKey)
        {
            PhysicalAnswer = _context.Quiz.Find(QuizID).PhysicalE;
            DiagnosticAnswer = _context.Quiz.Find(QuizID).DiagnosticE;
            DiagnosisAnswerKey = new List<string>(_context.Quiz.Find(QuizID)       //finds Quiz
                                    .DiagnosisKeyWordsE.Split(',')                 //finds and splits free response key
                                    .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces        
        }
    }
}
