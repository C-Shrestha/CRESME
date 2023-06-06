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

        [Required]
        public string? StudentNID { get; set; } = "";

        public string? StudentName { get; set; } = "";

        [Required]
        [BindProperty]
        public int QuizID { get; set; }

        public int? Score { get; set; }

        public int NumColumns { get; set; }

        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime EndTime { get; set; }
       
        public string? Term { get; set; } = "";

        public string? Course { get; set; } = "";
         
        public string? Block { get; set; } = ""; // comma seperated course blocks

        public string? PhysicalAnswerA { get; set; } = "";
        public string? PhysicalAnswerB { get; set; } = "";
        public string? PhysicalAnswerC { get; set; } = "";
        public string? PhysicalAnswerD { get; set; } = "";
        public string? PhysicalAnswerE { get; set; } = "";

        public string? DiagnosticAnswerA { get; set; } = "";
        public string? DiagnosticAnswerB { get; set; } = "";
        public string? DiagnosticAnswerC { get; set; } = "";
        public string? DiagnosticAnswerD { get; set; } = "";
        public string? DiagnosticAnswerE { get; set; } = "";

        public string? FreeResponseA { get; set; } = "";
        public string? FreeResponseB { get; set; } = "";
        public string? FreeResponseC { get; set; } = "";
        public string? FreeResponseD { get; set; } = "";
        public string? FreeResponseE { get; set; } = "";

        public string? NumImage0Clicks { get; set; } 
        public string? NumImage1Clicks { get; set; } 
        public string? NumImage2Clicks { get; set; } 
        public string? NumImage3Clicks { get; set; } 
        public string? NumImage4Clicks { get; set; } 
        public string? NumImage5Clicks { get; set; } 
        public string? NumImage6Clicks { get; set; } 
        public string? NumImage7Clicks { get; set; } 
        public string? NumImage8Clicks { get; set; } 
        public string? NumImage9Clicks { get; set; } 


    }
}
