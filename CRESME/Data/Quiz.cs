using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRESME.Data
{
    [Table("Quiz")]
    public class Quiz
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuizId { get; set; }

        public string InstructorID { get; set; } = "";

        [Required]
        [BindProperty]
        public string QuizName { get; set; } = "";

        [Required]
        [BindProperty]
        public int NumColumns { get; set; } = 4;

        
        public DateTime DateCreated { get; set; }

        
        [BindProperty]
        public DateTime StartDate { get; set; }

        
        [BindProperty]
        public DateTime EndDate { get; set; }

        public string Published { get; set; } = "Yes";

        public string FeedBackEnabled { get; set; } = "No";

        public string ShuffleEnabled { get; set; } = "No";

        [BindProperty]
        public string? NIDAssignment { get; set; } = ""; // comma seperated NID

        [BindProperty]
        public string? AuthorNames { get; set; } = "";

        [BindProperty]
        public string? Term { get; set; } = "";

        [BindProperty]
        public string? Course { get; set; } = "";
        
        [BindProperty]
        public string? Block { get; set; } = ""; // comma seperated course blocks


        [BindProperty]
        public string? PatientIntro { get; set; } = ""; 

        public string? Legend { get; set; } = "";

        public string? CoverImage { get; set; }

        public string? ExcelName { get; set; }

        public string? HistoryA { get; set; } = "";
        public string? HistoryB { get; set; } = "";
        public string? HistoryC { get; set; } = "";
        public string? HistoryD { get; set; } = "";
        public string? HistoryE { get; set; } = "";


        public string? PhysicalA { get; set; } = "";
        public string? PhysicalB { get; set; } = "";
        public string? PhysicalC { get; set; } = "";
        public string? PhysicalD { get; set; } = "";
        public string? PhysicalE { get; set; } = "";

        public string? DiagnosticA { get; set; } = "";
        public string? DiagnosticB { get; set; } = "";
        public string? DiagnosticC { get; set; } = "";
        public string? DiagnosticD { get; set; } = "";
        public string? DiagnosticE { get; set; } = "";

        //all keywords comma seperated
        public string? DiagnosisKeyWordsA { get; set; } = "";
        public string? DiagnosisKeyWordsB { get; set; } = "";
        public string? DiagnosisKeyWordsC { get; set; } = "";
        public string? DiagnosisKeyWordsD { get; set; } = "";
        public string? DiagnosisKeyWordsE { get; set; } = "";

        public string? FeedBackA { get; set; } = "";
        public string? FeedBackB { get; set; } = "";
        public string? FeedBackC { get; set; } = "";
        public string? FeedBackD { get; set; } = "";
        public string? FeedBackE { get; set; } = "";

        public string? Image0 { get; set; } = "";
        public string? Image1 { get; set; } = "";
        public string? Image2 { get; set; } = "";
        public string? Image3 { get; set; } = "";
        public string? Image4 { get; set; } = "";
        public string? Image5 { get; set; } = "";
        public string? Image6 { get; set; } = "";
        public string? Image7 { get; set; } = "";
        public string? Image8 { get; set; } = "";
        public string? Image9 { get; set; } = "";

        public int ImageCount { get; set; } = 0;
        
        public string? ImagePos0 { get; set; } = "";
        public string? ImagePos1 { get; set; } = "";
        public string? ImagePos2 { get; set; } = "";
        public string? ImagePos3 { get; set; } = "";
        public string? ImagePos4 { get; set; } = "";
        public string? ImagePos5 { get; set; } = "";
        public string? ImagePos6 { get; set; } = "";
        public string? ImagePos7 { get; set; } = "";
        public string? ImagePos8 { get; set; } = "";
        public string? ImagePos9 { get; set; } = "";

        [BindProperty]
        public string? Image0Alt { get; set; } = "";
        [BindProperty]
        public string? Image1Alt { get; set; } = "";
        [BindProperty]
        public string? Image2Alt { get; set; } = "";
        [BindProperty]
        public string? Image3Alt { get; set; } = "";
        [BindProperty]
        public string? Image4Alt { get; set; } = "";
        [BindProperty]
        public string? Image5Alt { get; set; } = "";
        [BindProperty]
        public string? Image6Alt { get; set; } = "";
        [BindProperty]
        public string? Image7Alt { get; set; } = "";
        [BindProperty]
        public string? Image8Alt { get; set; } = "";
        [BindProperty]
        public string? Image9Alt { get; set; } = "";

    }
}
