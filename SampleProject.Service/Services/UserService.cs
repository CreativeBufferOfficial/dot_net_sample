using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SampleProject.Common.Infrastructure;
using SampleProject.Common.Infrastructure.Helper;
using SampleProject.Common.Infrastructure.Models.Entities;
using SampleProject.Common.Infrastructure.Models.RequestModels.User;
using SampleProject.Common.Infrastructure.Models.ResponseModels;
using SampleProject.Data.Interfaces;
using SampleProject.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlowFishEncrypt = BCrypt.Net.BCrypt;

namespace SampleProject.Service.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// parameterized constructor for Dependency injection 
        /// </summary>
        /// <param name="appsettings"></param>
        /// <param name="mapper"></param>
        /// <param name="userRepository"></param>
        public UserService(IOptions<AppSettings> appsettings, IMapper mapper, IUserRepository userRepository)
        {
            _appSettings = appsettings.Value;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        /// <summary>
        /// create a new user, if not already exists
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns>
        /// returns the newly generated user guid if successfully created
        /// </returns>
        public async Task<ServiceResult<Guid>> CreateUser(CreateUserRequestModel model, int userId)
        {
            var serviceResult = new ServiceResult<Guid>();

            var exists = await _userRepository.CheckUserExistence(model.Email);

            if (exists)
                serviceResult.SetError("User with the provided email already exists");
            else
            {
                var userModel = _mapper.Map<UserModel>(model);
                userModel.CreatedBy = userId;
                userModel.PasswordHash = BlowFishEncrypt.HashPassword(model.Password);

                var result = await _userRepository.CreateUser(userModel);

                if (result > 0)
                    serviceResult.SetSuccess(userModel.UserGuid, "User created successfully");
                else
                    serviceResult.SetError("Failed to create user.");
            }

            return serviceResult;
        }

        /// <summary>
        /// authenticate user based on credentials provided
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// returns the JWT token along with the expiry time if the user is valid
        /// </returns>
        public async Task<ServiceResult<LoginResponseModel>> AuthenticateUser(LoginRequestModel model)
        {
            var serviceResult = new ServiceResult<LoginResponseModel>();

            // get user details by email
            var userDetails = await _userRepository.GetUserDetailsByEmail(model.Email);

            if (userDetails == null || !BlowFishEncrypt.Verify(model.Password, userDetails.PasswordHash))
                serviceResult.SetError("Invalid user name or password");

            if (!userDetails.IsActive)
                serviceResult.SetError("Account is not active yet");

            // generate JWT token
            var tokenDetails = GetAuthorizationToken(userDetails);

            if (tokenDetails != null)
                serviceResult.SetSuccess(tokenDetails, "Login successful");
            else
                serviceResult.SetError("Some error occured. Please contact administrator");

            return serviceResult;
        }

        /// <summary>
        /// get all users list
        /// </summary>
        /// <param name="showDeleted"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<UserViewModel>>> GetAllUsers(bool? showDeleted)
        {
            var serviceResult = new ServiceResult<IEnumerable<UserViewModel>>();

            serviceResult.SetData(await _userRepository.GetAllUsers(showDeleted));

            return serviceResult;
        }

        /// <summary>
        /// update an existing user
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns>
        /// returns whether successfully updated or not
        /// </returns>
        public async Task<ServiceResult<bool>> UpdateUser(UpdateUserRequestModel model, int userId)
        {
            var serviceResult = new ServiceResult<bool>();

            // authenticate user in the database
            var userDetails = await _userRepository.GetUserDetailsByGuid(model.UserGuid);

            if (userDetails == null)
                serviceResult.SetError("No user found with provided details");
            else
            {
                var userModel = _mapper.Map<UserModel>(model);
                userModel.ModifiedBy = userId;
                var result = await _userRepository.UpdateUser(userModel);

                if (result > 0)
                    serviceResult.SetSuccess(result > 0, "User updated successfully");
                else
                    serviceResult.SetError("Failed to update user.");
            }

            return serviceResult;
        }

        /// <summary>
        /// get user details by user guid
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public async Task<ServiceResult<UserViewModel>> GetUserDetailsByGuid(Guid userGuid)
        {
            var serviceResult = new ServiceResult<UserViewModel>();

            var userDetails = await _userRepository.GetUserDetailsByGuid(userGuid);

            if (userDetails == null)
                serviceResult.SetError("No user found with provided details");
            else
                serviceResult.SetData(userDetails);

            return serviceResult;
        }

        /// <summary>
        /// soft delete an existing user
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="userId"></param>
        /// <returns>
        /// returns whether successfully deleted or not
        /// </returns>
        public async Task<ServiceResult<bool>> DeleteUser(Guid userGuid, int userId)
        {
            var serviceResult = new ServiceResult<bool>();

            var result = await _userRepository.DeleteUser(new UserModel { ModifiedBy = userId, ModifiedDateUtc = DateTime.UtcNow, UserGuid = userGuid });

            if (result > 0)
                serviceResult.SetSuccess(result > 0, "User deleted successfully");
            else
                serviceResult.SetError("Failed to delete user.");

            return serviceResult;
        }

        #region Private methods

        /// <summary>
        /// generate JWT token based on the user details provided
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private LoginResponseModel? GetAuthorizationToken(UserViewModel model)
        {
            var symmetricKey = Encoding.ASCII.GetBytes(_appSettings.JwtSymmatricKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDesriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                                    new[]
                                        {
                                            new Claim(GlobalConstants.TokenClaims.Id, model.UserId.ToString()),
                                            new Claim(GlobalConstants.TokenClaims.Guid, model.UserGuid.ToString()),
                                            new Claim(GlobalConstants.TokenClaims.Name, model.Name),
                                            new Claim(GlobalConstants.TokenClaims.Email, model.Email),
                                            new Claim(GlobalConstants.TokenClaims.PhoneNumber, model.PhoneNumber ?? string.Empty),
                                            new Claim(GlobalConstants.TokenClaims.IsActive, model.IsActive.ToString()),
                                            new Claim(GlobalConstants.TokenClaims.RoleId, model.RoleId.ToString()),
                                            new Claim(GlobalConstants.TokenClaims.RoleName, model.RoleName),
                                            new Claim(ClaimTypes.Role, model.RoleName),
                                            new Claim(ClaimTypes.Name, model.Name),
                                        }),
                Audience = _appSettings.Audience,
                Issuer = _appSettings.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.TokenExpirationTimeInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDesriptor);

            return token != null
                        ? new LoginResponseModel { Expiration = token.ValidTo, Token = new JwtSecurityTokenHandler().WriteToken(token) }
                        : null;
        }

        #endregion
    }
}
