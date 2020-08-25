using ACI.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ACI.Infrastructure.CrossCutting.Identity.Contracts
{
    public interface IUserIdentity : IUserStore<UserEntity>, IUserPasswordStore<UserEntity>, IUserEmailStore<UserEntity>
    {
    }
}
