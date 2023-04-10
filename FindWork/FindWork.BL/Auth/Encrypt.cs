using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace FindWork.BL.Auth;

public class Encrypt : IEncrypt
{
    public string HashPassword(string password, string salt) =>
        Convert.ToBase64String(KeyDerivation.Pbkdf2(password, Encoding.ASCII.GetBytes(salt),
            KeyDerivationPrf.HMACSHA512, 5000, 64));
}