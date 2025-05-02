using LibraryManagementApp.Models;

namespace LibraryManagementApp.DataAccess.Repositories
{
    public interface IMemberRepository
    {
        void Add(Member member);
        Member GetById(int memberId);
        IEnumerable<Member> GetAll();
    }
}

