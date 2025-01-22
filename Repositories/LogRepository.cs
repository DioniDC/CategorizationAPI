using categorizationapi.Models;
using System.Threading.Tasks;

namespace categorizationapi.Repositories
{
    public class LogRepository : ILogRepository
    {
        public async Task GravarLog(LogProduto logProduto)
        {
            // Simulação de gravação de log
            Console.WriteLine($"Log registrado: {logProduto.CodigoProduto} de {logProduto.SubgrupoAntes} para {logProduto.SubgrupoDepois}");
            await Task.CompletedTask;
        }
    }
}
