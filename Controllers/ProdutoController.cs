using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using categorizationapi.Services;
using categorizationapi.Models;

namespace categorizationapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IDepartamentalizacaoService _departamentalizacaoService;
        private readonly IProdutoService _produtoService;
        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(IDepartamentalizacaoService departamentalizacaoService, 
                                 IProdutoService produtoService, 
                                 ILogger<ProdutoController> logger)
        {
            _departamentalizacaoService = departamentalizacaoService ?? throw new ArgumentNullException(nameof(departamentalizacaoService));
            _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("processar")]
        public async Task<IActionResult> ProcessarProdutos([FromBody] List<Produto> produtos)
        {
            if (produtos == null || produtos.Count == 0)
            {
                _logger.LogWarning("Lista de produtos vazia recebida.");
                return BadRequest(new { message = "A lista de produtos não pode estar vazia." });
            }

            _logger.LogInformation($"Processando {produtos.Count} produtos.");
            var produtosProcessados = await _departamentalizacaoService.ProcessarListaDeProdutos(produtos);
            
            if (produtosProcessados == null || produtosProcessados.Count == 0)
            {
                _logger.LogWarning("Nenhum produto processado.");
                return NotFound(new { message = "Nenhum produto foi processado." });
            }

            return Ok(produtosProcessados);
        }

        [HttpPost("atualizar-departamentalizacao")]
        public IActionResult AtualizarDepartamentalizacao([FromBody] Classe novaDepartamentalizacao)
        {
            if (novaDepartamentalizacao == null)
            {
                _logger.LogWarning("Tentativa de atualizar departamentalização com dados nulos.");
                return BadRequest(new { message = "Os dados de departamentalização não podem estar vazios." });
            }

            _departamentalizacaoService.AtualizarDepartamentalizacao(novaDepartamentalizacao);
            _logger.LogInformation("Departamentalização atualizada com sucesso.");
            return Ok(new { message = "Departamentalização atualizada com sucesso." });
        }

        [HttpPost("verificar-produtos")]
        public async Task<IActionResult> VerificarProdutos([FromBody] List<ProdutoDto> produtos)
        {
            if (produtos == null || produtos.Count == 0)
            {
                _logger.LogWarning("Lista de produtos vazia recebida.");
                return BadRequest(new { message = "A lista de produtos não pode estar vazia." });
            }

            _logger.LogInformation($"Verificando {produtos.Count} produtos.");
            var resultado = await _produtoService.VerificarEAtualizarProdutos(produtos);
            
            if (resultado == null || resultado.Count == 0)
            {
                _logger.LogWarning("Nenhum produto verificado ou atualizado.");
                return NotFound(new { message = "Nenhum produto foi verificado ou atualizado." });
            }

            return Ok(resultado);
        }
    }
}
