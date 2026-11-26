using Microsoft.EntityFrameworkCore;
using CargoTrack.Domain.Entities;
using CargoTrack.Infrastructure.Data;

using CargoTrack.Application.Interfaces;

namespace CargoTrack.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{

    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context=context;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u=>u.Email==email);
    }
    public void Add(User user)
    {
        _context.Users.Add(user);
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u=>u.Email==email);
    }
    







}