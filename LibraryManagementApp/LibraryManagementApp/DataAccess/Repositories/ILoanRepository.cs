using LibraryManagementApp.Models;

namespace LibraryManagementApp.DataAccess.Repositories
{
    public interface ILoanRepository
    {
        void Borrow(Loan loan);
        void Return(int loanId);
        IEnumerable<LoanDetails> GetActiveLoans();
        Loan GetActiveLoanByBookId(int bookId);
    }
}

