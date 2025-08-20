using Instagram.Domain.Entities;

namespace Instagram.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(long userId);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        void Update(User user);
        void Delete(User user);
        Task<int> SaveChangesAsync();
        Task<long> InsertAsync(User user);
      
    }
}
