using ACI.Application.Identity.DTOs;
using Microsoft.AspNetCore.Identity;

namespace ACI.Application.Identity.Contracts.Services
{
    public interface IUserIdentityAppService: IUserStore<UserDTO>, IUserPasswordStore<UserDTO>, IUserEmailStore<UserDTO>
    {
    }
}
