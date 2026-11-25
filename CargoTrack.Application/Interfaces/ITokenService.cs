

using CargoTrack.Domain.Entities;

namespace CargoTrack.Application.Interfaces;


public interface ITokenService
{
    string CreateToken(User user);
}