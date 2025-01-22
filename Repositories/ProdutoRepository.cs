using categorizationapi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace categorizationapi.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        public async Task<string?> ObterCodigoBarra(List<string> codigosBarras)
        {
            // Implementação simulada
            return await Task.FromResult(codigosBarras.FirstOrDefault());
        }

        public async Task<ProdutoDto?> ObterProdutoPorCodigo(string codigoBarra)
        {
            return await Task.FromResult(new ProdutoDto { CodigoProduto = 1, Descricao = "Produto Teste", Subgrupo = 10 });
        }

        public async Task AtualizarProduto(ProdutoDto produto)
        {
            await Task.CompletedTask;
        }
    }
}
