using System;
using System.Threading.Tasks;

namespace ACI.Infrastructure.CrossCutting.Identity.Contracts
{
    public interface IEmailSender : IDisposable
    {
        Task<bool> SendAsync(string to, string body);
    }
}
