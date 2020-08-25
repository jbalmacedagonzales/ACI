using ACI.Application.Identity.Contracts.Services;
using ACI.Application.Identity.DTOs;
using ACI.Application.Identity.Services.Helpers;
using ACI.Domain.Entities;
using ACI.Infrastructure.CrossCutting.Identity.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ACI.Application.Identity.Services
{
    public class UserIdentityAppService : IUserIdentityAppService
    {
        private readonly IUserIdentity _userIdentity;
        public UserIdentityAppService(IUserIdentity userIdentity)
        {
            this._userIdentity = userIdentity;
        }

        public async Task<IdentityResult> CreateAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var userEntity = new UserEntity(user.Id, user.FirstName, user.LastName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.BirthDate, user.RegistrationDate);
            return (!userEntity.HasMinimunAge())
                ? IdentityResult.Failed(new IdentityError() { Description="The minimun age must be 18 or more" })
                : await _userIdentity.CreateAsync(userEntity, cancellationToken);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _userIdentity.Dispose();
        }

        public async Task<UserDTO> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var entity = await _userIdentity.FindByEmailAsync(normalizedEmail, cancellationToken);
            return GenericMapper<UserEntity, UserDTO>.ToDto(entity);
        }

        public async Task<UserDTO> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var entity = await _userIdentity.FindByNameAsync(normalizedUserName, cancellationToken);
            return GenericMapper<UserEntity, UserDTO>.ToDto(entity);
        }

        public async Task<string> GetEmailAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var userEntity = new UserEntity(user.Id, user.FirstName, user.LastName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.BirthDate, user.RegistrationDate);
            return await _userIdentity.GetEmailAsync(userEntity, cancellationToken);
        }

        public async Task<bool> GetEmailConfirmedAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var userEntity = new UserEntity(user.Id, user.FirstName, user.LastName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.BirthDate, user.RegistrationDate);
            return await _userIdentity.GetEmailConfirmedAsync(userEntity, cancellationToken);
        }

        public async Task<string> GetNormalizedEmailAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var userEntity = new UserEntity(user.Id, user.FirstName, user.LastName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.BirthDate, user.RegistrationDate);
            return await _userIdentity.GetNormalizedEmailAsync(userEntity, cancellationToken);
        }

        public async Task<string> GetNormalizedUserNameAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var userEntity = new UserEntity(user.Id, user.FirstName, user.LastName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.BirthDate, user.RegistrationDate);
            return await _userIdentity.GetNormalizedUserNameAsync(userEntity, cancellationToken);
        }

        public async Task<string> GetPasswordHashAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var userEntity = new UserEntity(user.Id, user.FirstName, user.LastName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.BirthDate, user.RegistrationDate);
            return await _userIdentity.GetPasswordHashAsync(userEntity, cancellationToken);
        }

        public async Task<string> GetUserIdAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var userEntity = new UserEntity(user.Id, user.FirstName, user.LastName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.BirthDate, user.RegistrationDate);
            return await _userIdentity.GetUserIdAsync(userEntity, cancellationToken);
        }

        public async Task<string> GetUserNameAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var userEntity = new UserEntity(user.Id, user.FirstName, user.LastName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.BirthDate, user.RegistrationDate);
            return await _userIdentity.GetUserNameAsync(userEntity, cancellationToken);
        }

        public async Task<bool> HasPasswordAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var userEntity = new UserEntity(user.Id, user.FirstName, user.LastName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.BirthDate, user.RegistrationDate);
            return await _userIdentity.HasPasswordAsync(userEntity, cancellationToken);
        }

        public Task SetEmailAsync(UserDTO user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            var userEntity = new UserEntity(user.Id, user.FirstName, user.LastName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.BirthDate, user.RegistrationDate);
            return _userIdentity.SetEmailAsync(userEntity, email, cancellationToken);
        }

        public Task SetEmailConfirmedAsync(UserDTO user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            var userEntity = new UserEntity(user.Id, user.FirstName, user.LastName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.BirthDate, user.RegistrationDate);
            return _userIdentity.SetEmailConfirmedAsync(userEntity, confirmed, cancellationToken);
        }

        public Task SetNormalizedEmailAsync(UserDTO user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            var userEntity = new UserEntity(user.Id, user.FirstName, user.LastName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.BirthDate, user.RegistrationDate);
            return _userIdentity.SetNormalizedEmailAsync(userEntity, normalizedEmail, cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(UserDTO user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedName;
            var userEntity = new UserEntity(user.Id, user.FirstName, user.LastName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.BirthDate, user.RegistrationDate);
            return _userIdentity.SetNormalizedUserNameAsync(userEntity, normalizedName, cancellationToken);
        }

        public Task SetPasswordHashAsync(UserDTO user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            var userEntity = new UserEntity(user.Id, user.FirstName, user.LastName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.BirthDate, user.RegistrationDate);
            return _userIdentity.SetPasswordHashAsync(userEntity, passwordHash, cancellationToken);
        }

        public Task SetUserNameAsync(UserDTO user, string userName, CancellationToken cancellationToken)
        {
            user.Email = userName;
            var userEntity = new UserEntity(user.Id, user.FirstName, user.LastName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.BirthDate, user.RegistrationDate);
            return _userIdentity.SetUserNameAsync(userEntity, userName, cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var userEntity = new UserEntity(user.Id, user.FirstName, user.LastName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.BirthDate, user.RegistrationDate);
            return await _userIdentity.UpdateAsync(userEntity, cancellationToken);
        }


        #region 
        public Task<UserDTO> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(UserDTO user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
