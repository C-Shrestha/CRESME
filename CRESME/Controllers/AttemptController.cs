using CRESME.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.DependencyResolver;
using System.Security.Claims;
using System.Globalization;
using iText.Html2pdf;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using DocumentFormat.OpenXml.EMMA;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CRESME.Controllers
{
    public class AttemptController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _environment;
        ICompositeViewEngine _compositeViewEngine;

        public AttemptController(ApplicationDbContext context, ICompositeViewEngine compositeViewEngine, IWebHostEnvironment environment = null)
        {
            _context = context;
            _environment = environment;
            _compositeViewEngine = compositeViewEngine;
        }


        /*Returns a view with a PDF template format that will be submitted to webcourses.*/
        [Authorize(Roles = "Student, Admin, Instructor")]
        public IActionResult TestAttempt(Attempt attempt)
        {
            return View(attempt);
        }

        /*Generated a PDF based on "PrintAttempt.cshtml view with student CRESME data to be submited to webcourses
         * The "TestAttempt.csthml and PrintAttempt.cshtml are similar only differnce being PrintAtempt.cshtml does not have the "Create PDF" button.
         * TestAttempt -> Create PDF -> PrintAttempt -> PDF Download "*/
        [Authorize(Roles = "Admin, Instructor, Student")]
        public async Task<IActionResult> GenerateAttemptPDF(Attempt attempt)
        {
            // using string Swriter to convert view with Model data into HTML string. 
            using (var stringWriter = new StringWriter())
            {
                // finds the view, PrintAttempt.cshtml, that will be used to generate PDF. 
                var viewResult = _compositeViewEngine.FindView(ControllerContext, "PrintAttempt", false);
                if (viewResult == null)
                {
                    throw new ArgumentException("View Cannot be Found");

                }
                var model = attempt;

                // Dictionary is created with the Attempt model data added to the view
                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                { Model = model };

                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    viewDictionary,
                    TempData,
                    stringWriter,
                    new HtmlHelperOptions()
                    );

                await viewResult.View.RenderAsync(viewContext);


                // using the conver to PDF function to create a PDF stream
                byte[] pdfBytes;
                using (MemoryStream outputStream = new MemoryStream())
                {
                    HtmlConverter.ConvertToPdf(stringWriter.ToString(), outputStream);
                    pdfBytes = outputStream.ToArray();
                }


                string filename = $"{attempt.QuizName} {DateTime.Now:MM/dd/yyy}.pdf";

                // return PDF for as download file
                return File(pdfBytes, "application/pdf", filename);


            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitAttempt(Attempt attempt) {

            Quiz ParentQuiz = new Quiz();

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
                attempt.Course = ParentQuiz.Course;
                attempt.Block = ParentQuiz.Block;
                attempt.Term = ParentQuiz.Term;
            }
            else
            {
                throw new Exception("Quiz ID is null in attempt submission");
            }



            //  ASsIGNING KEYS
            //assigns correct database answers as keys for physical and diagnostic answers, and constructs the free response answer key 

            //COLUMN ONE
            AssignColumnKeys(ParentQuiz, "column1", ref DiagnosisAnswerKey1);
            //COLUMN TWO
            AssignColumnKeys(ParentQuiz, "column2", ref DiagnosisAnswerKey2);
            //COLUMN THREE
            AssignColumnKeys(ParentQuiz, "column3", ref DiagnosisAnswerKey3);
            //COLUMN FOUR
            AssignColumnKeys(ParentQuiz, "column4", ref DiagnosisAnswerKey4);
            //COLUMN FIVE
            if (ParentQuiz.NumColumns == 5) {
                AssignColumnKeys(ParentQuiz, "column5", ref DiagnosisAnswerKey5);
            }


            //  SCORING 
            attempt.Score = 0;


            //Determing duplicate free response answers
            List<string> StudentResponses = new List<string> { attempt.FreeResponseA, attempt.FreeResponseB, attempt.FreeResponseC, attempt.FreeResponseD};
            if (attempt.NumColumns == 5)
            {
                StudentResponses.Add(attempt.FreeResponseE);
            }
            int AnswerACount = 0;
            int AnswerBCount = 0;
            int AnswerCCount = 0;
            int AnswerDCount = 0;
            int AnswerECount = 0;
            foreach (string response in StudentResponses)
            {
                foreach (string key in DiagnosisAnswerKey1)
                {
                    if (response.Contains(key))
                    {
                        AnswerACount++;
                        break;
                    }
                }
                foreach (string key in DiagnosisAnswerKey2)
                {
                    if (response.Contains(key))
                    {
                        AnswerBCount++;
                        break;
                    }
                }
                foreach (string key in DiagnosisAnswerKey3)
                {
                    if (response.Contains(key))
                    {
                        AnswerCCount++;
                        break;
                    }
                }
                foreach (string key in DiagnosisAnswerKey4)
                {
                    if (response.Contains(key))
                    {
                        AnswerDCount++;
                        break;
                    }
                }
                if (attempt.NumColumns==5) {
                    foreach (string key in DiagnosisAnswerKey5)
                    {
                        if (response.Contains(key))
                        {
                            AnswerECount++;
                            break;
                        }
                    }
                }
            }
            //COLUMN ONE
            attempt.ColumnAGrade = GradeColumn(attempt.FreeResponseA, attempt.PhysicalAnswerA, attempt.DiagnosticAnswerA, attempt.NumColumns, AnswerACount, AnswerBCount, AnswerCCount, AnswerDCount, AnswerECount,
                               "Column1", "Column2", "Column3", "Column4", "Column5",
                                DiagnosisAnswerKey1, DiagnosisAnswerKey2, DiagnosisAnswerKey3, DiagnosisAnswerKey4, DiagnosisAnswerKey5);


            //COLUMN TWO
            attempt.ColumnBGrade = GradeColumn(attempt.FreeResponseB, attempt.PhysicalAnswerB, attempt.DiagnosticAnswerB, attempt.NumColumns, AnswerBCount, AnswerACount, AnswerCCount, AnswerDCount, AnswerECount,
                           "Column2", "Column1", "Column3", "Column4", "Column5",
                            DiagnosisAnswerKey2, DiagnosisAnswerKey1, DiagnosisAnswerKey3, DiagnosisAnswerKey4, DiagnosisAnswerKey5);


            //COLUMN THREE
            attempt.ColumnCGrade = GradeColumn(attempt.FreeResponseC, attempt.PhysicalAnswerC, attempt.DiagnosticAnswerC, attempt.NumColumns, AnswerCCount, AnswerBCount, AnswerACount, AnswerDCount, AnswerECount,
                           "Column3", "Column2", "Column1", "Column4", "Column5",
                            DiagnosisAnswerKey3, DiagnosisAnswerKey2, DiagnosisAnswerKey1, DiagnosisAnswerKey4, DiagnosisAnswerKey5);


            //COLUMN FOUR
            attempt.ColumnDGrade = GradeColumn(attempt.FreeResponseD, attempt.PhysicalAnswerD, attempt.DiagnosticAnswerD, attempt.NumColumns, AnswerDCount, AnswerBCount, AnswerCCount, AnswerACount, AnswerECount,
                           "Column4", "Column2", "Column3", "Column1", "Column5",
                            DiagnosisAnswerKey4, DiagnosisAnswerKey2, DiagnosisAnswerKey3, DiagnosisAnswerKey1, DiagnosisAnswerKey5);


            //COLUMN FIVE
            if (attempt.NumColumns==5) {
                attempt.ColumnEGrade = GradeColumn(attempt.FreeResponseE, attempt.PhysicalAnswerE, attempt.DiagnosticAnswerE, attempt.NumColumns, AnswerECount, AnswerBCount, AnswerCCount, AnswerDCount, AnswerACount,
                               "Column5", "Column2", "Column3", "Column4", "Column1",
                                DiagnosisAnswerKey5, DiagnosisAnswerKey2, DiagnosisAnswerKey3, DiagnosisAnswerKey4, DiagnosisAnswerKey1);
            }

            attempt.Score = attempt.ColumnAGrade + attempt.ColumnBGrade + attempt.ColumnCGrade + attempt.ColumnDGrade + attempt.ColumnEGrade;

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
            if (ParentQuiz.Image0 != "")
            {
                attempt.Image0Name = ParentQuiz.Image0.Substring(52);
            }
            if (ParentQuiz.Image1 != "")
            {
                attempt.Image1Name = ParentQuiz.Image1.Substring(52);
            }
            if (ParentQuiz.Image2 != "")
            {
                attempt.Image2Name = ParentQuiz.Image2.Substring(52);
            }
            if (ParentQuiz.Image3 != "")
            {
                attempt.Image3Name = ParentQuiz.Image3.Substring(52);
            }
            if (ParentQuiz.Image4 != "")
            {
                attempt.Image4Name = ParentQuiz.Image4.Substring(52);
            }
            if (ParentQuiz.Image5 != "")
            {
                attempt.Image5Name = ParentQuiz.Image5.Substring(52);
            }
            if (ParentQuiz.Image6 != "")
            {
                attempt.Image6Name = ParentQuiz.Image6.Substring(52);
            }
            if (ParentQuiz.Image7 != "")
            {
                attempt.Image7Name = ParentQuiz.Image7.Substring(52);
            }
            if (ParentQuiz.Image8 != "")
            {
                attempt.Image8Name = ParentQuiz.Image8.Substring(52);
            }
            if (ParentQuiz.Image9 != "")
            {
                attempt.Image9Name = ParentQuiz.Image9.Substring(52);
            }
           

            //student info
            var CurrentStudent = _context.Users.SingleOrDefault( user => user.UserName == User.Identity.Name);
            attempt.StudentID = CurrentStudent.Id;
            attempt.StudentNID = CurrentStudent.UserName;
            attempt.StudentName = CurrentStudent.Name;
            if (ModelState.IsValid && User.IsInRole("Student"))
            {
                _context.Add(attempt);
                _context.SaveChanges();
            }
            //TempData["Success"] = "CRESME submitted sucessfully!";
            return RedirectToAction("TestAttempt", attempt);
        }



        public void AssignColumnKeys(Quiz ParentQuiz, string columnID, ref List<string> DiagnosisAnswerKey)
        {     
            if (Request.Form[columnID] == "1")
            {
                DiagnosisAnswerKey = new List<string>(ParentQuiz                        
                                        .DiagnosisKeyWordsA.Split(',')                 //finds and splits free response key
                                        .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces   
            }
            else if (Request.Form[columnID] == "2")
            {
                DiagnosisAnswerKey = new List<string>(ParentQuiz
                                        .DiagnosisKeyWordsB.Split(',')                 //finds and splits free response key
                                        .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces 
            }
            else if (Request.Form[columnID] == "3")
            {
                DiagnosisAnswerKey = new List<string>(ParentQuiz
                                        .DiagnosisKeyWordsC.Split(',')                 //finds and splits free response key
                                        .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces 
            }
            else if (Request.Form[columnID] == "4")
            {
                DiagnosisAnswerKey = new List<string>(ParentQuiz
                                        .DiagnosisKeyWordsD.Split(',')                 //finds and splits free response key
                                        .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces 
            }
            else if (Request.Form[columnID] == "5")
            {
                DiagnosisAnswerKey = new List<string>(ParentQuiz
                                        .DiagnosisKeyWordsE.Split(',')                 //finds and splits free response key
                                        .Select(x => x.Trim()).ToList());              //Trims leading and trailing spaces 
            }
            else
            {
                throw new Exception("Unrecognized column id when assigning keys.");
            }
            DiagnosisAnswerKey.RemoveAll(s => s == "");
        }

        int GradeColumn( string FreeResponse, string PhysicalAnswer, string DiagnosticAnswer, int? NumColumns, int OneCount, int TwoCount, int ThreeCount, int FourCount, int FiveCount,
                               string C1, string C2, string C3, string C4, string C5, 
                               List<string> DiagnosisAnswerKey1, List<string> DiagnosisAnswerKey2, List<string> DiagnosisAnswerKey3, List<string> DiagnosisAnswerKey4, List<string> DiagnosisAnswerKey5) {

            int ColumnGrade = 0;
            

            if (FreeResponse != null)
            {

                foreach (string key in DiagnosisAnswerKey1)
                {
                    if (FreeResponse.Contains(key))
                    {
                        ColumnGrade += 3;
                        if (PhysicalAnswer == Request.Form[C1] && DiagnosticAnswer == Request.Form[C1])
                        {
                            ColumnGrade += 2;
                        }
                        else if ((PhysicalAnswer == Request.Form[C1] || DiagnosticAnswer == Request.Form[C1]))
                        {
                            ColumnGrade += 1;
                        }
                        else if (OneCount > 1)
                        {
                            ColumnGrade -= 3;
                        }
                        break;
                    }
                }

                foreach (string key in DiagnosisAnswerKey2)
                {
                    if (FreeResponse.Contains(key) && (PhysicalAnswer == Request.Form[C2] || DiagnosticAnswer == Request.Form[C2]))
                    {
                        ColumnGrade += 3;
                        if (PhysicalAnswer == Request.Form[C2] && DiagnosticAnswer == Request.Form[C2])
                        {
                            ColumnGrade += 1;
                        }
                        else if (TwoCount > 1)
                        {
                            ColumnGrade -= 3;
                        }
                        break;
                    }
                }

                foreach (string key in DiagnosisAnswerKey3)
                {
                    if (FreeResponse.Contains(key) && (PhysicalAnswer == Request.Form[C3] || DiagnosticAnswer == Request.Form[C3]))
                    {
                        ColumnGrade += 3;
                        if (PhysicalAnswer == Request.Form[C3] && DiagnosticAnswer == Request.Form[C3])
                        {
                            ColumnGrade += 1;
                        }
                        else if (ThreeCount > 1)
                        {
                            ColumnGrade -= 3;
                        }
                        break;
                    }
                }

                foreach (string key in DiagnosisAnswerKey4)
                {
                    if (FreeResponse.Contains(key) && (PhysicalAnswer == Request.Form[C4] || DiagnosticAnswer == Request.Form[C4]))
                    {
                        ColumnGrade += 3;
                        if (PhysicalAnswer == Request.Form[C4] && DiagnosticAnswer == Request.Form[C4])
                        {
                            ColumnGrade += 1;
                        }
                        else if (FourCount > 1)
                        {
                            ColumnGrade -= 3;
                        }
                        break;
                    }
                }

                if (NumColumns == 5)
                {
                    foreach (string key in DiagnosisAnswerKey5)
                    {
                        if (FreeResponse.Contains(key) && (PhysicalAnswer == Request.Form[C5] || DiagnosticAnswer == Request.Form[C5]))
                        {
                            ColumnGrade += 3;
                            if (PhysicalAnswer == Request.Form[C5] && DiagnosticAnswer == Request.Form[C5])
                            {
                                ColumnGrade += 1;
                            }
                            else if (FiveCount > 1) {
                                ColumnGrade -= 3;
                            }
                            break;
                        }
                    }
                }

            }

            return ColumnGrade;
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
