using BookStoreWebGentle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWebGentle.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly List<UserDTO> users = new List<UserDTO>();

        public UserRepository()
        {
            users.Add(new UserDTO
            {
                UserName = "joydipkanjilal",
                Password = "joydip123",
                Role = "manager"
            });
            users.Add(new UserDTO
            {
                UserName = "michaelsanders",
                Password = "michael321",
                Role = "developer"
            });
            users.Add(new UserDTO
            {
                UserName = "stephensmith",
                Password = "stephen123",
                Role = "tester"
            });
            users.Add(new UserDTO
            {
                UserName = "rodpaddock",
                Password = "rod123",
                Role = "admin"
            });
            users.Add(new UserDTO
            {
                UserName = "rexwills",
                Password = "rex321",
                Role = "admin"
            });
        }
        public UserDTO GetUser(SignInModel signInModel)
        {
            return users.Where(x => x.UserName.ToLower() == signInModel.UserName.ToLower()
                && x.Password == signInModel.Password).FirstOrDefault();
        }
    }

}
