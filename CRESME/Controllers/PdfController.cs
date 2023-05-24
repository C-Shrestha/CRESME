using CRESME.Data;
using CRESME.Models;
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



        // GET: Users
        public async Task<IActionResult> GetAll()
        {
            return _context.Users != null ?
                        View(await _userManager.Users.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Test'  is null.");
        }

        /*[Route("quiz")]*/
        public async Task<IActionResult> GeneratePDF()
        {

            using (var stringWriter = new StringWriter())
            {
                var viewResult = _compositeViewEngine.FindView(ControllerContext, "GetAll", false);
                if (viewResult == null)
                {
                    throw new ArgumentException("View Cannot be Found");

                }
                var model = _userManager.Users;
                /*var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());*/

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
        }



        /*[Route("website")]*/
        public async Task<IActionResult> WebsiteAsync()
        {
            // Imported package "Select.HtmlToPdf.NetCore" from  SelectPDF
            // Free version (this one) can only process up to 5 pages(?), should work for our purposes
            // Easy, but probably not viable as I assume UCF wouldn't want to use third party packages for security purposes.


            /* Account for different devices
            var mobileView = new HtmlToPdf();
            mobileView.Options.WebPageWidth = 480;

            var tabletView = new HtmlToPdf();
            tabletView.Options.WebPageHeight = 1024;

            // Generates the PDF from a given url
            var pdf = mobileView.ConvertUrl("https://www.roundthecode.com/");
            pdf.Append(tabletView.ConvertUrl("https://www.roundthecode.com/"));
            pdf.Append(desktopView.ConvertUrl("https://www.roundthecode.com/"));
             */






            // Generates a PDF from an HTML page and displays a preview.
            var desktopView = new HtmlToPdf();
            desktopView.Options.WebPageWidth = 1920;
            var pdf = desktopView.ConvertUrl("https://localhost:7103/Quiz/GetAll");
            var pdfBytes = pdf.Save();

            return File(pdfBytes, "application/pdf");
        }


        //Showing Students Graades
        // GET: Tests/Create
        public IActionResult Grades()
        {
            //return View();
            
            return View();

        }






    }
}
