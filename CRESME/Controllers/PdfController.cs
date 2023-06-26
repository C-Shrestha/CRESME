using CRESME.Data;
using CRESME.Models;
using iText.Html2pdf;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using SelectPdf;
using System.Diagnostics;

namespace CRESME.Controllers
{
    /*[Route("pdf")]*/
    public class PdfController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        ICompositeViewEngine _compositeViewEngine;



        public PdfController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICompositeViewEngine compositeViewEngine)
        {
            _context = context;
            _userManager = userManager;
            _compositeViewEngine = compositeViewEngine;

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        /*// GET: Users
        public async Task<IActionResult> GetAll()
        {
            return _context.Users != null ?
                        View(await _userManager.Users.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Test'  is null.");
        }*/

        /*[Route("quiz")]*/
        /*public async Task<IActionResult> GeneratePDF()
        {

            using (var stringWriter = new StringWriter())
            {
                var viewResult = _compositeViewEngine.FindView(ControllerContext, "GetAll", false);
                if (viewResult == null)
                {
                    throw new ArgumentException("View Cannot be Found");

                }
                var model = _userManager.Users;
                *//*var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());*//* 

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
                

                var htmlToPdf = new HtmlToPdf(1000, 1414);
                htmlToPdf.Options.DrawBackground = true;


                var pdf = htmlToPdf.ConvertHtmlString(stringWriter.ToString());

                var pdfBytes = pdf.Save();



                return File(pdfBytes, "application/pdf");


            }
        }*/



        /*[Route("website")]
         This requires a website and is not used in this website currently.
         */
        /*public async Task<IActionResult> WebsiteAsync()
        {
            // Imported package "Select.HtmlToPdf.NetCore" from  SelectPDF
            // Free version (this one) can only process up to 5 pages(?), should work for our purposes
            // Easy, but probably not viable as I assume UCF wouldn't want to use third party packages for security purposes.


            *//* Account for different devices
            var mobileView = new HtmlToPdf();
            mobileView.Options.WebPageWidth = 480;

            var tabletView = new HtmlToPdf();
            tabletView.Options.WebPageHeight = 1024;

            // Generates the PDF from a given url
            var pdf = mobileView.ConvertUrl("https://www.roundthecode.com/");
            pdf.Append(tabletView.ConvertUrl("https://www.roundthecode.com/"));
            pdf.Append(desktopView.ConvertUrl("https://www.roundthecode.com/"));
             *//*



            // Generates a PDF from an HTML page and displays a preview.
            *//*var desktopView = new HtmlToPdf();
            desktopView.Options.WebPageWidth = 1920;
            var pdf = desktopView.ConvertUrl("https://localhost:7103/Quiz/GetAll");
            var pdfBytes = pdf.Save();

            return File(pdfBytes, "application/pdf");*//*
        }*/


        /*//Showing Students Graades
        // GET: Tests/Create
        public IActionResult Grades()
        {
            //return View();
            
            return View();

        }*/




        /*Returns a view with a PDF template format that will be submitted to webcourses.*/
        [Authorize(Roles = "Admin, Instructor")]
        public IActionResult TestAttempt()
        {
            return View(_context.Attempt.FirstOrDefault());

        }



        /*Generated a PDF based on "PrintAttempt.cshtml view with student CRESME data to be submited to webcourses
         * The "TestAttempt.csthml and PrintAttempt.cshtml are similar only differnce being PrintAtempt.cshtml does not have the "Create PDF" button.
         * TestAttempt -> Create PDF -> PrintAttempt -> PDF Download "*/
        /*[Authorize(Roles = "Admin, Instructor, Student")]
        public async Task<IActionResult> GenerateAttemptPDF(Attempt attempt)
        {

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

                // convert PDF to HTML
                var htmlToPdf = new HtmlToPdf(1000, 1414);
                htmlToPdf.Options.DrawBackground = true;

                var pdf = htmlToPdf.ConvertHtmlString(stringWriter.ToString());

                var pdfBytes = pdf.Save();

                string filename = $"{attempt.QuizName} {DateTime.Now:MM/dd/yyy}.pdf";

                // return PDF for as download file
                return File(pdfBytes, "application/pdf", filename);


            }
        }*/


        /*Generated a PDF based on "PrintAttempt.cshtml view with student CRESME data to be submited to webcourses
         * The "TestAttempt.csthml and PrintAttempt.cshtml are similar only differnce being PrintAtempt.cshtml does not have the "Create PDF" button.
         * TestAttempt -> Create PDF -> PrintAttempt -> PDF Download "*/

        [Authorize(Roles = "Admin, Instructor, Student")]
        public  async Task<IActionResult> GenerateAttemptPDF(Attempt attempt)
        {

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

                
                /*// convert PDF to HTML
                var htmlToPdf = new HtmlToPdf(1000, 1414);
                htmlToPdf.Options.DrawBackground = true;

                var pdf = htmlToPdf.ConvertHtmlString(stringWriter.ToString());*/

                FileStream pdfStream =  null;
                string html = stringWriter.ToString();
                ConverterProperties converterProperties = new ConverterProperties();

                HtmlConverter.ConvertToPdf(html, pdfStream, converterProperties);


                var pdfBytes = ConvertStreamToBytes(pdfStream);

                string filename = $"{attempt.QuizName} {DateTime.Now:MM/dd/yyy}.pdf";

                // return PDF for as download file
                return File(pdfBytes, "application/pdf", filename);


            }

           





        }

        public byte[] ConvertStreamToBytes(FileStream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }




        /*public static void converter()
        {
            using (FileStream htmlSource = File.Open("input.html"))
            using (FileStream pdfDest = File.Open("output.pdf", FileMode.Create))
            {
                ConverterProperties converterProperties = new ConverterProperties();
                HtmlConverter.ConvertToPdf(htmlSource, pdfDest, converterProperties);
            }
        }*/

        public void ConvertHtmlToPdf(string htmlContent, string outputPath)
        {
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(htmlContent, outputStream);
            }
        }

        














    }
}
