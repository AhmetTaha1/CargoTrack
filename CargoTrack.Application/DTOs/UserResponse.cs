using CargoTrack.Domain.Entities;

namespace CargoTrack.Application.DTOs;

public class UserResponse
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}