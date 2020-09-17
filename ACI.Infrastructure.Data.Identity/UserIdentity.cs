using ACI.Domain.Entities;
using ACI.Infrastructure.CrossCutting.Identity.Contracts;
using ACI.Infrastructure.CrossCutting.Logging;
using ACI.Infrastructure.Data.Identity.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ACI.Infrastructure.Data.Identity
{
    public class UserIdentity : IUserIdentity
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public UserIdentity(IConfiguration configuration)
        {
            this._configuration = configuration;
            _connectionString = _configuration.GetConnectionString("connection_db");
        }

        public async Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken)
        {
            int i = -1;
            try
            {
                SqlParameter[] parameters =
            {
                new SqlParameter{ ParameterName="@Id", Value=user.Id },
                new SqlParameter{ ParameterName="@FirstName", Value=user.FirstName },
                new SqlParameter{ ParameterName="@LastName", Value=user.LastName },
                new SqlParameter{ ParameterName="@Email", Value=user.Email },
                new SqlParameter{ ParameterName="@NormalizedEmail", Value=user.NormalizedEmail },
                new SqlParameter{ ParameterName="@PasswordHash", Value=user.PasswordHash },
                new SqlParameter{ ParameterName="@BirthDate", Value=user.BirthDate }
            };

                i = await GenericSqlMethods.ExecuteNonQueryAsync(_connectionString, "UspCreateUser", parameters);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message, ex.StackTrace);
            }
            return (i > 0) ? IdentityResult.Success : IdentityResult.Failed();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        public async Task<UserEntity> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            UserEntity user = null;
            SqlParameter[] parameters = { new SqlParameter { ParameterName = "@email", Value = normalizedEmail } };
            SqlDataReader reader = await GenericSqlMethods.ExecuteReaderAsync(_connectionString, "UspFindUserByEmail", parameters);
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        user = new UserEntity
                            (
                                reader.GetGuid(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                reader.GetString(reader.GetOrdinal("Email")),
                                reader.GetString(reader.GetOrdinal("NormalizedEmail")),
                                reader.GetBoolean(reader.GetOrdinal("EmailConfirmed")),
                                reader.GetString(reader.GetOrdinal("PasswordHash")),
                                reader.GetDateTime(reader.GetOrdinal("BirthDate")),
                                reader.GetDateTime(reader.GetOrdinal("RegistrationDate"))
                            );
                    }
                    reader.Close();
                }
                return user;
            }
        }

        public async Task<UserEntity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            UserEntity user = null;
            SqlParameter[] parameters = { new SqlParameter { ParameterName = "@email", Value = normalizedUserName } };
            SqlDataReader reader = await GenericSqlMethods.ExecuteReaderAsync(_connectionString, "UspFindUserByEmail", parameters);
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        user = new UserEntity
                            (
                                reader.GetGuid(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                reader.GetString(reader.GetOrdinal("Email")),
                                reader.GetString(reader.GetOrdinal("NormalizedEmail")),
                                reader.GetBoolean(reader.GetOrdinal("EmailConfirmed")),
                                reader.GetString(reader.GetOrdinal("PasswordHash")),
                                reader.GetDateTime(reader.GetOrdinal("BirthDate")),
                                reader.GetDateTime(reader.GetOrdinal("RegistrationDate"))
                            );
                    }
                    reader.Close();
                }
                return user;
            }
        }

        public Task<string> GetEmailAsync(UserEntity user, CancellationToken cancellationToken)
        {
            var result = user.Email;
            return Task.FromResult(result);
        }

        public Task<bool> GetEmailConfirmedAsync(UserEntity user, CancellationToken cancellationToken)
        {
            var result = user.EmailConfirmed;
            return Task.FromResult(result);
        }

        public Task<string> GetNormalizedEmailAsync(UserEntity user, CancellationToken cancellationToken)
        {
            var result = user.NormalizedEmail;
            return Task.FromResult(result);
        }

        public Task<string> GetNormalizedUserNameAsync(UserEntity user, CancellationToken cancellationToken)
        {
            var result = user.NormalizedEmail;
            return Task.FromResult(result);
        }

        public Task<string> GetPasswordHashAsync(UserEntity user, CancellationToken cancellationToken)
        {
            var result = user.PasswordHash;
            return Task.FromResult(result);
        }

        public Task<bool> HasPasswordAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }


        public Task SetEmailAsync(UserEntity user, string email, CancellationToken cancellationToken)
        {
            user.SetEmail(email);
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(UserEntity user, bool confirmed, CancellationToken cancellationToken)
        {
            user.SetEmailConfirmed(confirmed);
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(UserEntity user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.SetEmail(normalizedEmail);
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(UserEntity user, string normalizedName, CancellationToken cancellationToken)
        {
            user.SetEmail(normalizedName);
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(UserEntity user, string passwordHash, CancellationToken cancellationToken)
        {
            user.SetPasswordHash(passwordHash);
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(UserEntity user, string userName, CancellationToken cancellationToken)
        {
            user.SetEmail(userName);
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
        {
            int i = -1;
            try
            {
                SqlParameter[] parameters =
            {
                new SqlParameter{ ParameterName="@Id", Value=user.Id },
                new SqlParameter{ ParameterName="@FirstName", Value=user.FirstName },
                new SqlParameter{ ParameterName="@LastName", Value=user.LastName },
                new SqlParameter{ ParameterName="@Email", Value=user.Email },
                new SqlParameter{ ParameterName="@NormalizedEmail", Value=user.NormalizedEmail },
                new SqlParameter{ ParameterName="@EmailConfirmed", Value=user.EmailConfirmed },
                new SqlParameter{ ParameterName="@PasswordHash", Value=user.PasswordHash },
                new SqlParameter{ ParameterName="@BirthDate", Value=user.BirthDate }
            };

                i = await GenericSqlMethods.ExecuteNonQueryAsync(_connectionString, "UspUpdateUser", parameters);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message, ex.StackTrace);
            }
            return (i > 0) ? IdentityResult.Success : IdentityResult.Failed();
        }


        public Task<string> GetUserNameAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<string> GetUserIdAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }




        #region
        /*
         *  Only for the demo these methods were not implemented.
            However, every interface must implement all the methods it has.
            Don't forget that please.
         */
        public Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
