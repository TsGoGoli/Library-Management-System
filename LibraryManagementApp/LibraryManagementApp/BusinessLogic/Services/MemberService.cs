using LibraryManagementApp.DataAccess.Repositories;
using LibraryManagementApp.Models;

namespace LibraryManagementApp.BusinessLogic.Services
{
    public class MemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public void RegisterMember(Member member)
        {
            if (string.IsNullOrWhiteSpace(member.Email))
                throw new ArgumentException("Email is required");

            _memberRepository.Add(member);
        }

        public Member GetMember(int memberId)
        {
            return _memberRepository.GetById(memberId);
        }
    }
}

