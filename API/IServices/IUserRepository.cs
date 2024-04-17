using API.Data;
using API.DTOs;
using API.Entities;

namespace API.IServices
{
    public interface IUserRepository
    {
        void Update(UserApp user);
        Task<bool> SaveAllASync();
        Task<IEnumerable<UserApp>> GetUsersASync();
        Task<UserApp> GetUserById(int id);
        Task<UserApp> GetUserByUsernameAsync(string username);
        Task<IEnumerable<MemberDTO>> GetMembersAsync();
        Task<MemberDTO> GetMemberAsync(string username);
    }
}
