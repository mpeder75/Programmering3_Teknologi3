using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Api.Services
{
    public interface IGithubService
    {
        Task<string> GetGithubUser(string username);
    }
}
