using Dapper;
using LibraryManagementApp.Models;
using Microsoft.Data.SqlClient;

namespace LibraryManagementApp.DataAccess.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly string _connectionString;

        public LoanRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Borrow(Loan loan)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute(
                @"INSERT INTO Loans 
                (BookId, MemberId, DueDate) 
                VALUES 
                (@BookId, @MemberId, @DueDate)",
                new
                {
                    loan.BookId,
                    loan.MemberId,
                    DueDate = loan.DueDate.Date
                });
        }

        public void Return(int loanId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute(
                @"UPDATE Loans 
                SET ReturnDate = GETDATE() 
                WHERE LoanId = @LoanId",
                new { LoanId = loanId });
        }

        public IEnumerable<LoanDetails> GetActiveLoans()
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<LoanDetails>(
                @"SELECT l.LoanId, b.Title, m.FirstName, m.LastName, l.BorrowDate, l.DueDate 
                FROM Loans l
                JOIN Books b ON l.BookId = b.BookId
                JOIN Members m ON l.MemberId = m.MemberId
                WHERE l.ReturnDate IS NULL");
        }

        public Loan GetActiveLoanByBookId(int bookId)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QueryFirstOrDefault<Loan>(
                @"SELECT * FROM Loans 
                WHERE BookId = @BookId 
                AND ReturnDate IS NULL",
                new { BookId = bookId });
        }
    }
}

