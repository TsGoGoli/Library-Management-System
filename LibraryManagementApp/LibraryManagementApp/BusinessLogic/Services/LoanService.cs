using LibraryManagementApp.DataAccess.Repositories;
using LibraryManagementApp.Models;

namespace LibraryManagementApp.BusinessLogic.Services
{
    public class LoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;

        public LoanService(
            ILoanRepository loanRepository,
            IBookRepository bookRepository,
            IMemberRepository memberRepository)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
        }

        public void BorrowBook(int bookId, int memberId)
        {
            var book = _bookRepository.GetById(bookId);
            if (book == null) throw new ArgumentException("Invalid Book ID");

            var member = _memberRepository.GetById(memberId);
            if (member == null) throw new ArgumentException("Invalid Member ID");

            if (_loanRepository.GetActiveLoanByBookId(bookId) != null)
                throw new InvalidOperationException("Book is already borrowed");

            var loan = new Loan
            {
                BookId = bookId,
                MemberId = memberId,
                DueDate = DateTime.Today.AddDays(14)
            };

            _loanRepository.Borrow(loan);
        }

        public void ReturnBook(int bookId)
        {
            var activeLoan = _loanRepository.GetActiveLoanByBookId(bookId);
            if (activeLoan == null)
                throw new InvalidOperationException("No active loan for this book");

            _loanRepository.Return(activeLoan.LoanId);
        }

        public IEnumerable<LoanDetails> GetActiveLoans()
        {
            return _loanRepository.GetActiveLoans();
        }
    }
}