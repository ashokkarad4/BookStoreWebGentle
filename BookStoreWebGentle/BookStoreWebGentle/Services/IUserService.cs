namespace BookStoreWebGentle.Services
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}