using LOND.API.Interfaces;
using LOND.API.Models;
using LOND.API.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace LOND.API.Services
{
    public class UserService : IUserService
    {
        private readonly ILondRepository<LondUser, int> _signUpRepository;
        public UserService(ILondRepository<LondUser, int> londRepository)
        {
            _signUpRepository = londRepository;
        }

        // Implement sign-in and sign-up methods here

        public async Task ClassicSignUpAsync(SignUpObject SIO)
        {
            try
            {

                if (SIO != null)
                {
                    if (string.IsNullOrWhiteSpace(SIO.FirstName))
                    {
                        throw new ArgumentException("FirstName cannot be null or empty");
                    }

                    if (string.IsNullOrWhiteSpace(SIO.LastName))
                    {
                        throw new ArgumentException("LastName cannot be null or empty");
                    }

                    if (string.IsNullOrWhiteSpace(SIO.Email))
                    {
                        throw new ArgumentException("Email cannot be null or empty");
                    }

                    if (string.IsNullOrWhiteSpace(SIO.Password))
                    {
                        throw new ArgumentException("Password cannot be null or empty");
                    }
                    var hasher = new PasswordHasher<object>();
                    string hashedPassword = hasher.HashPassword(null, SIO.Password);
                    LondUser newUser = new LondUser
                    {
                        FirstName = SIO.FirstName,
                        LastName = SIO.LastName,
                        Email = SIO.Email,
                        PasswordHash = hashedPassword,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await _signUpRepository.InsertAsync(newUser);



                }
                else
                {
                    throw new ArgumentNullException("SignUpObject cannot be null");
                }
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException($"An error occurred during ClassicSignUpAsync => Error: {ex}");
            }
        }

        public async Task ClassicSignInAsync(SignInObject signInObject)
        {
            try
            {
                if (signInObject != null)
                {
                    if (string.IsNullOrWhiteSpace(signInObject.Email))
                    {
                        throw new ArgumentException("Email cannot be null or empty");
                    }
                    if (string.IsNullOrWhiteSpace(signInObject.Password))
                    {
                        throw new ArgumentException("Password cannot be null or empty");
                    }
                    // Retrieve user by email
                    var users = await _signUpRepository.ReturnTable();
                    var user = users.FirstOrDefault(u => u.Email == signInObject.Email);
                    if (user == null)
                    {
                        throw new UnauthorizedAccessException("Invalid email or password");
                    }
                    // Verify password
                    var hasher = new PasswordHasher<object>();
                    var result = hasher.VerifyHashedPassword(null, user.PasswordHash, signInObject.Password);
                    if (result == PasswordVerificationResult.Failed)
                    {
                        throw new UnauthorizedAccessException("Invalid email or password");
                    }
                    // Successful sign-in logic here (e.g., generate JWT token)
                }
                else
                {
                    throw new ArgumentNullException("SignInObject cannot be null");
                }
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException($"An error occurred during ClassicSignInAsync => Error: {ex}");
            }
        }
    }
}
