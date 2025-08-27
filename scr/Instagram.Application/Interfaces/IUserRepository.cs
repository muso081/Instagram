using Instagram.Domain.Entities;
using static Instagram.Domain.Entities.User;

namespace Instagram.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(long userId);
        Task<User> GetByUsernameAsync(string username);
        Task<ICollection<User>> SelectAllAsync();
        Task<User> GetByEmailAsync(string email);
        void Update(User user);
        void Delete(User user);
        Task<int> SaveChangesAsync();
        Task<long> InsertAsync(User user);
        Task UpdateUserRole(long userId, UserRole role);

    }
}
