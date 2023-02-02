using BookStoreWebGentle.Models;

namespace BookStoreWebGentle.Repository
{
    public interface IUserRepository
    {
        UserDTO GetUser(SignInModel signInModel);

    }
}