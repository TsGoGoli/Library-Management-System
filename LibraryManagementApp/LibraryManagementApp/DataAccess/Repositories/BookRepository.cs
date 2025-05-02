using Dapper;
using LibraryManagementApp.Models;
using Microsoft.Data.SqlClient;

namespace LibraryManagementApp.DataAccess.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;

        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Book book)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute(
                "INSERT INTO Books (Title, Author, ISBN, PublicationYear) VALUES (@Title, @Author, @ISBN, @PublicationYear)",
                book);
        }

        public Book GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QueryFirstOrDefault<Book>(
                "SELECT * FROM Books WHERE BookId = @Id",
                new { Id = id });
        }

        public IEnumerable<Book> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Book>("SELECT * FROM Books");
        }

        public IEnumerable<Book> Search(string searchTerm)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Book>(
                "SELECT * FROM Books WHERE Title LIKE '%' + @searchTerm + '%' OR Author LIKE '%' + @searchTerm + '%'",
                new { searchTerm });
        }
    }
}

