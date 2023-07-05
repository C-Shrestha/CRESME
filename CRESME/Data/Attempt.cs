using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRESME.Data
{
    [Table("Attempts")]
    public class Attempt
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttemptId { get; set; }
     
        [BindProperty]
        public int? QuizID { get; set; }

        public string? QuizName { get; set; }

        public int? NumColumns { get; set; }

        public string? PatientIntro { get; set; }

        public string? StudentNID { get; set; }
        
        public string? StudentID { get; set; } 

        public string? StudentName { get; set; }
        
        public string? Term { get; set; }
        public string? Course { get; set; }
        public string? Block { get; set; }

        public int? Score { get; set; }
      
        [DataType(DataType.Date)]
       
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }



        [BindProperty]
        public string? PhysicalAnswerA { get; set; } = "";
        [BindProperty]
        public string? PhysicalAnswerB { get; set; } = "";
        [BindProperty]
        public string? PhysicalAnswerC { get; set; } = "";
        [BindProperty]
        public string? PhysicalAnswerD { get; set; } = "";
        [BindProperty] 
        public string? PhysicalAnswerE { get; set; } = "";

        [BindProperty] 
        public string? DiagnosticAnswerA { get; set; } = "";
        [BindProperty] 
        public string? DiagnosticAnswerB { get; set; } = "";
        [BindProperty] 
        public string? DiagnosticAnswerC { get; set; } = "";
        [BindProperty] 
        public string? DiagnosticAnswerD { get; set; } = "";
        [BindProperty] 
        public string? DiagnosticAnswerE { get; set; } = "";

        [BindProperty] 
        public string? FreeResponseA { get; set; } = "";
        [BindProperty] 
        public string? FreeResponseB { get; set; } = "";
        [BindProperty] 
        public string? FreeResponseC { get; set; } = "";
        [BindProperty] 
        public string? FreeResponseD { get; set; } = "";
        [BindProperty] 
        public string? FreeResponseE { get; set; } = "";

        public string? NumImage0Clicks { get; set; } = "0";
        public string? NumImage1Clicks { get; set; } = "0";
        public string? NumImage2Clicks { get; set; } = "0";
        public string? NumImage3Clicks { get; set; } = "0";
        public string? NumImage4Clicks { get; set; } = "0";
        public string? NumImage5Clicks { get; set; } = "0";
        public string? NumImage6Clicks { get; set; } = "0";
        public string? NumImage7Clicks { get; set; } = "0";
        public string? NumImage8Clicks { get; set; } = "0";
        public string? NumImage9Clicks { get; set; } = "0";

        public string? Image0Name { get; set; }
        public string? Image1Name { get; set; }
        public string? Image2Name { get; set; }
        public string? Image3Name { get; set; }
        public string? Image4Name { get; set; }
        public string? Image5Name { get; set; }
        public string? Image6Name { get; set; }
        public string? Image7Name { get; set; }
        public string? Image8Name { get; set; }
        public string? Image9Name { get; set; }


        public string? NumLegendClicks { get; set; } = "0";

        public string? NumLabValueClicks { get; set; } = "0";


    }
}
