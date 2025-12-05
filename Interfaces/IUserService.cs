using LOND.API.Models;

namespace LOND.API.Interfaces
{
    public interface IUserService
    {
        public Task ClassicSignUpAsync(SignUpObject SIO);
        public Task ClassicSignInAsync(SignInObject signInObject);
    }
}
