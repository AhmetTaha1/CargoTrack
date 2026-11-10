using CargoTrack.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using CargoTrack.Application.Interfaces;


namespace CargoTrack.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{

    private readonly PasswordHasher<User> _hasher=new();

    public string Hash(User user,string password)
    {
        return _hasher.HashPassword(user,password);
    }

    public bool Verify(User user, string password)
    {
        var result=_hasher.VerifyHashedPassword(user,user.PasswordHash,password);
        return result !=PasswordVerificationResult.Failed;

    }





    

}

