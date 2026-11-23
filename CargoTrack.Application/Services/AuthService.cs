using CargoTrack.Application.DTOs;
using CargoTrack.Application.Exceptions;
using CargoTrack.Application.Interfaces;
using CargoTrack.Domain.Entities;

namespace CargoTrack.Application.Services;


public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;


    public AuthService(IUserRepository userRepository,IPasswordHasher passwordHasher)
    {
        _passwordHasher=passwordHasher;
        _userRepository=userRepository;
    }

    public async Task<UserResponse> RegisterAsync(RegisterRequest request)
    {
        var email=request.Email.Trim().ToLowerInvariant();

        if(await _userRepository.EmailExistsAsync(email))
            throw new ConflictException("Bu e-posta zaten kayıtlı");

    var user = new User
    {
        Email = email,
        Role = UserRole.Customer
    };

    user.PasswordHash = _passwordHasher.Hash(user, request.Password);

    
    _userRepository.Add(user);
    await _userRepository.SaveChangesAsync();

    return new UserResponse
    {
        Id = user.Id,
        Email = user.Email,
        Role = user.Role
    };

    }






}