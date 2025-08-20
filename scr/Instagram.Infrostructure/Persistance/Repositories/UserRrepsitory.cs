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

    public async Task<User> GetByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            throw new Exception($"User Not found with Email {email}");
        }
        return user;
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

    public async Task<User> GetByUsernameAsync(string username)
    {
        var users = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (users == null)
        {
            throw new Exception($"User Not found with Username {username}");
        }
        return users;
    }

    public async Task<long> InsertAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user.UserId;

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
