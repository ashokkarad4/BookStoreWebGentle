using BookStoreWebGentle.Models;

namespace BookStoreWebGentle.Services
{
    public interface ITokenService
    {
       string BuildToken(string key, string issuer, UserDTO user);
        bool IsTokenValid(string key, string issuer, string token);

    }
}