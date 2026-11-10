using CargoTrack.Domain.Entities;

namespace CargoTrack.Application.Interfaces;


public interface IUserRepository
{
Task<bool> EmailExistsAsync(string email);
void Add(User user);
Task SaveChangesAsync();

}
