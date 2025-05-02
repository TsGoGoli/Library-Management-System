
namespace LibraryManagementApp.Models
{
    public class LoanDetails
    {
        public int LoanId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}