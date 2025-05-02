using LibraryManagementApp.Models;

namespace LibraryManagementApp.DataAccess.Repositories
{
    public interface IBookRepository
    {
        void Add(Book book);
        Book GetById(int id);
        IEnumerable<Book> GetAll();
        IEnumerable<Book> Search(string searchTerm);
    }
}


