using BookStoreWebGentle.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;


namespace BookStoreWebGentle.Repository
{

        public class AccountRepository : IAccountRepository
        {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AccountRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
            public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
            {
            var user = new ApplicationUser()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                PhoneNumber=userModel.PhoneNumber,
              
                DateOfBirth=userModel.DateOfBirth,
                Email = userModel.Email,
                UserName = userModel.Email

            };
           var result=await _userManager.CreateAsync(user, userModel.Password);
            return result;
        }

        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            return await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, true);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
