using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using categorizationapi.Repositories;
using categorizationapi.Models;

namespace categorizationapi.Services;
public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly ILogRepository _logRepository;

    public ProdutoService(IProdutoRepository produtoRepository, ILogRepository logRepository)
    {
        _produtoRepository = produtoRepository;
        _logRepository = logRepository;
    }

    public async Task<List<ProdutoResultadoDto>> VerificarEAtualizarProdutos(List<ProdutoDto> produtos)
    {
        var resultados = new List<ProdutoResultadoDto>();

        foreach (var produto in produtos)
        {
            var codigoBarraExistente = await _produtoRepository.ObterCodigoBarra(produto.CodigosBarras);

            if (codigoBarraExistente != null)
            {
                var produtoBanco = await _produtoRepository.ObterProdutoPorCodigo(codigoBarraExistente);

                if (produtoBanco != null)
                {
                    if (produtoBanco.Subgrupo != produto.Subgrupo)
                    {
                        await _logRepository.GravarLog(new LogProduto
                        {
                            NomeCliente = produto.NomeCliente,
                            CnpjCliente = produto.CnpjCliente,
                            DataLog = DateTime.UtcNow,
                            CodigoProduto = produtoBanco.CodigoProduto?.ToString(),
                            Descricao = produtoBanco.Descricao,
                            CodigoBarras = codigoBarraExistente,
                            SubgrupoAntes = produtoBanco.Subgrupo?.ToString(),
                            SubgrupoDepois = produto.Subgrupo?.ToString()
                        });

                        produtoBanco.Subgrupo = produto.Subgrupo;
                        await _produtoRepository.AtualizarProduto(produtoBanco);
                    }

                    resultados.Add(new ProdutoResultadoDto
                    {
                        CodigoProduto = produtoBanco.CodigoProduto ?? 0,
                        Descricao = produtoBanco.Descricao ?? string.Empty,
                        Subgrupo = produtoBanco.Subgrupo ?? 0 
                    });
                }
            }
        }
        return resultados;
    }
}
