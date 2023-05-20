using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRESME.Data
{
    [Table("Quiz")]
    public class Quiz
    {
        [Required]
        [Key]
        [BindProperty]
        public string QuizName { get; set; } = "";

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime EndDate { get; set; }

        public string Published { get; set; } = "";

        public string FeedBackEnabled { get; set; } = "";

        [BindProperty]
        public string? NIDAssignment { get; set; } = ""; // comma seperated NID

        [BindProperty]
        public string? CourseAssignment { get; set; } = "";
        
        [BindProperty]
        public string? BlockAssignment { get; set; } = ""; // comma seperated course blocks

        public int? NumImages { get; set; }

        public string? PatientIntro { get; set; } = ""; //might be the same as QuizName, check with Melissa

        public string? HistoryA { get; set; } = "";
        public string? HistoryB { get; set; } = "";
        public string? HistoryC { get; set; } = "";
        public string? HistoryD { get; set; } = "";

        public string? PhysicalA { get; set; } = "";
        public string? PhysicalB { get; set; } = "";
        public string? PhysicalC { get; set; } = "";
        public string? PhysicalD { get; set; } = "";

        public string? DiagnosticA { get; set; } = "";
        public string? DiagnosticB { get; set; } = "";
        public string? DiagnosticC { get; set; } = "";
        public string? DiagnosticD { get; set; } = "";

        public string? DiagnosisA { get; set; } = "";
        public string? DiagnosisB { get; set; } = "";
        public string? DiagnosisC { get; set; } = "";
        public string? DiagnosisD { get; set; } = "";

        //all keywords comma seperated
        public string? KeyWordsA { get; set; } = "";
        public string? KeyWordsB { get; set; } = "";
        public string? KeyWordsC { get; set; } = "";
        public string? KeyWordsD { get; set; } = "";

        public string? FeedBackA { get; set; } = "";
        public string? FeedBackB { get; set; } = "";
        public string? FeedBackC { get; set; } = "";
        public string? FeedBackD { get; set; } = "";

        public string? Image1 { get; set; } = "";
        public string? Image2 { get; set; } = "";
        public string? Image3 { get; set; } = "";
        public string? Image4 { get; set; } = "";
        public string? Image5 { get; set; } = "";
        public string? Image6 { get; set; } = "";
        public string? Image7 { get; set; } = "";
        public string? Image8 { get; set; } = "";
        public string? Image9 { get; set; } = "";
        public string? Image10 { get; set; } = "";

        public string? Image1Pos { get; set; } = "";
        public string? Image2Pos { get; set; } = "";
        public string? Image3Pos { get; set; } = "";
        public string? Image4Pos { get; set; } = "";
        public string? Image5Pos { get; set; } = "";
        public string? Image6Pos { get; set; } = "";
        public string? Image7Pos { get; set; } = "";
        public string? Image8Pos { get; set; } = "";
        public string? Image9Pos { get; set; } = "";
        public string? Image10Pos { get; set; } = "";
    }
}
