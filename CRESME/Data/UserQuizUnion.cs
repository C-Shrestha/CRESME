namespace CRESME.Data
{
    public class UserQuizUnion
    {
        public IEnumerable<ApplicationUser> users { get; set; }
        
        public Quiz quiz { get; set; }
    }
}
