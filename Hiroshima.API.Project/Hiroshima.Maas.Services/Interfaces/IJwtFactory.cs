using Hiroshima.Maas.Services.Utility.Helper;
using System.Threading.Tasks;
namespace Hiroshima.Maas.Services.Interfaces
{
    public interface IJwtFactory
    {
        Task<Token> GenerateEncodedToken(string id, string userName, string role);
    }
}
