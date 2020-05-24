using Authorization.Models;

namespace Authorization.Repository.Implementation
{
    using System.Threading.Tasks;
    using Interface;
    using System;
    using Authorization.Db.Repository.Interface;
    using Authorization.Entities.Entities;
    using Microsoft.Extensions.Logging;
    
    public class UserRepository : IUserRepository
    {
        #region Global Variables
        private readonly IMongoRepository<User> _userDbService;

        private readonly ILogger<UserRepository> _logger;
        #endregion
        
        #region Constructor
        public UserRepository(IMongoRepository<User> userDbService, ILogger<UserRepository> logger)
        {
            _userDbService = userDbService;
            _logger = logger;
        }
        #endregion
        
        #region Implmentations
        public async Task<User> LoginAsync(string userName, string password)
        {
            try
            {
                var user = await _userDbService.FindOneAsync(identity =>
                    identity.Name.ToLower() == userName.ToLower()
                    && identity.Password.Equals(password));

                if (user == null)
                {
                    throw new UnauthorizedAccessException();
                }

                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred while Logging in the user");
                throw;
            }
        }

        public async Task<User> RegisterUser(Login loginDetails)
        {
            try
            {
                var user = await _userDbService.FindOneAsync(identity =>
                    identity.Name.ToLower() == loginDetails.UserName.ToLower() &&
                    identity.Password == loginDetails.Password);
                if (user != null)
                {
                    throw new InvalidOperationException("User Already Exists");
                }

                var insertedEntity = await _userDbService.InsertOneAsync(new User
                {
                    Name = loginDetails.UserName,
                    Password = loginDetails.Password
                });
                return insertedEntity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}