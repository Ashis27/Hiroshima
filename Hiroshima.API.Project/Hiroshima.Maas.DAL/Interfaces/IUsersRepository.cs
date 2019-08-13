using System.Linq;
using System.Threading.Tasks;
using Hiroshima.Maas.DL.Entities;
using Microsoft.AspNetCore.Identity;

namespace Hiroshima.Maas.DAL.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        IQueryable<AppUser> Get();
       // AppUser GetByEmail(string email);
        Task<IdentityResult> Create(AppUser user, string password);
        Task<IdentityResult> Delete(AppUser user);
        Task<IdentityResult> Update(AppUser user);
        UserManager<AppUser> GetUserManager();
    }
}