using System.Collections.Generic;
using System.Threading.Tasks;
using categorizationapi.Models;

namespace categorizationapi.Services
{
    public interface IDepartamentalizacaoService
    {
        Task<List<Produto>> ProcessarListaDeProdutos(List<Produto> produtos);
        void AtualizarDepartamentalizacao(Classe novaDepartamentalizacao);
    }
}
