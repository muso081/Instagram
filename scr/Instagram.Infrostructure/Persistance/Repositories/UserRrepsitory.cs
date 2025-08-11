using Instagram.Application.Interfaces;
using Instagram.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.Persistance.Repositories;

public class UserRrepsitory : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRrepsitory(AppDbContext context)
    {
        _context = context;
    }

    public void Delete(User user)
    {
        _context.Users.Remove(user);
    }

    public async Task<User> GetByIdAsync(long userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user == null)
        {
            throw new Exception($"User Not found with Id {userId}");
        }
        return user;
    }

    public async Task<ICollection<User>> GetByUsernameAsync(string username)
    {
        var users = await _context.Users.Where(u => u.Username.Contains(username)).ToListAsync();
        return users;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }
}
