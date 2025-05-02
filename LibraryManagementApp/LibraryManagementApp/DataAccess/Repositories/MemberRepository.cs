using Dapper;
using LibraryManagementApp.Models;
using Microsoft.Data.SqlClient;

namespace LibraryManagementApp.DataAccess.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly string _connectionString;

        public MemberRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Member member)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute(
                @"INSERT INTO Members 
                (FirstName, LastName, Email, PhoneNumber) 
                VALUES 
                (@FirstName, @LastName, @Email, @PhoneNumber)",
                member);
        }

        public Member GetById(int memberId)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QueryFirstOrDefault<Member>(
                "SELECT * FROM Members WHERE MemberId = @MemberId",
                new { MemberId = memberId });
        }

        public IEnumerable<Member> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Member>("SELECT * FROM Members");
        }
    }
}

