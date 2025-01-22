using System.Collections.Generic;
using System.Threading.Tasks;
using categorizationapi.Models;
using categorizationapi.Services;

namespace categorizationapi.Services;
public interface IProdutoService
{
    Task<List<ProdutoResultadoDto>> VerificarEAtualizarProdutos(List<ProdutoDto> produtos);
}