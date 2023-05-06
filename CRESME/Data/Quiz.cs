using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRESME.Data
{
    [Table("Quiz")]
    public class Quiz
    {
        [Required]
        [Key]   
        public string QuizName { get; set; } = "";

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public bool isPublished { get; set; }

        public bool FeedBackEnabled { get; set; }

        public string? NIDAssignment { get; set; } = ""; // comma seperated NID
        public string? YearAssignment { get; set; } = "";
        public string? CourseAssignment { get; set; } = "";
        public string? BlockAssignment { get; set; } = "";

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
    }
}
