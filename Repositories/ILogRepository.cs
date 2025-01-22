using System.Threading.Tasks;
using categorizationapi.Models;

namespace categorizationapi.Repositories
{
    public interface ILogRepository
    {
        Task GravarLog(LogProduto logProduto);
    }
}