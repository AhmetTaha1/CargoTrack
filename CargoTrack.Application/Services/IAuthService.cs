using CargoTrack.Application.DTOs;

namespace CargoTrack.Application.Services;


public interface IAuthService
{
    Task<UserResponse> RegisterAsync(RegisterRequest request);

}