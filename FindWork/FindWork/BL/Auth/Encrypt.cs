using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace FindWork.BL.Auth;

public class Encrypt : IEncrypt
{
    public string HashPassword(string password, byte[] salt) =>
        Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, 5000, 64));
}