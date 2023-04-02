namespace FindWork.BL.Auth;

public interface IEncrypt
{
    string HashPassword(string password, byte[] salt);
}