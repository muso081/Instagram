using Instagram.Domain.Entities;

namespace Instagram.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(long userId);
        Task<ICollection<User>> GetByUsernameAsync(string username);
        void Update(User user);
        void Delete(User user);
        Task<int> SaveChangesAsync();
    }
}
