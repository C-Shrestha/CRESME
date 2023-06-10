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
        public string QuizID { get; set; }

        public string? UserID { get; set; } 

        public int? Score { get; set; }
      
        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [BindProperty]
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

        [BindProperty] 
        public string? NumImage0Clicks { get; set; }
        [BindProperty] 
        public string? NumImage1Clicks { get; set; }
        [BindProperty] 
        public string? NumImage2Clicks { get; set; }
        [BindProperty] 
        public string? NumImage3Clicks { get; set; }
        [BindProperty] 
        public string? NumImage4Clicks { get; set; }
        [BindProperty] 
        public string? NumImage5Clicks { get; set; }
        [BindProperty] 
        public string? NumImage6Clicks { get; set; }
        [BindProperty] 
        public string? NumImage7Clicks { get; set; }
        [BindProperty] 
        public string? NumImage8Clicks { get; set; }
        [BindProperty] 
        public string? NumImage9Clicks { get; set; } 


    }
}
