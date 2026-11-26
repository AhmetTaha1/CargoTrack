using CargoTrack.Application.DTOs;
using CargoTrack.Application.Exceptions;
using CargoTrack.Application.Interfaces;
using CargoTrack.Domain.Entities;

namespace CargoTrack.Application.Services;


public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;


    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<UserResponse> RegisterAsync(RegisterRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        if (await _userRepository.EmailExistsAsync(email))
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

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var user = await _userRepository.GetByEmailAsync(email);

        if (user is null || !_passwordHasher.Verify(user, request.Password))
            throw new UnauthorizedException("E-posta veya şifre hatalı");

        return new LoginResponse {Token=_tokenService.CreateToken(user)};









    }






}