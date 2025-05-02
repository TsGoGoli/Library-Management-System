using LibraryManagementApp.DataAccess.Repositories;
using LibraryManagementApp.Models;

namespace LibraryManagementApp.BusinessLogic.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public void AddBook(Book book)
        {
            if (book.PublicationYear > DateTime.Now.Year)
                throw new ArgumentException("Invalid publication year");

            _bookRepository.Add(book);
        }

        public IEnumerable<Book> SearchBooks(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Enumerable.Empty<Book>();

            return _bookRepository.Search(searchTerm);
        }
    }
}

