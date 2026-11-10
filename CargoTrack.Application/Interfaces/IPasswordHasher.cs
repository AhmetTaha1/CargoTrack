using CargoTrack.Domain.Entities;

namespace CargoTrack.Application.Interfaces;


public interface IPasswordHasher
{
    
    string Hash(User user,string password);
    bool Verify(User user,string password);



}