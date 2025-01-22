using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using categorizationapi.Models;
using categorizationapi.Repositories;

namespace categorizationapi.Services
{
    public class DepartamentalizacaoService : IDepartamentalizacaoService
    {
        private readonly IDepartamentalizacaoRepository _departamentalizacaoRepository;
        private readonly HttpClient _httpClient;
        private readonly ILogger<DepartamentalizacaoService> _logger;
        private const string ApiUrl = "https://api.openai.com/v1/chat/completions";
        private readonly string _apiKey = "sua-chave-aqui";
        public DepartamentalizacaoService(HttpClient httpClient, ILogger<DepartamentalizacaoService> logger, IDepartamentalizacaoRepository repository, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _departamentalizacaoRepository = repository;
        
            var apiKey = configuration["OpenAI:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("Chave da API não configurada corretamente.");
            }
        
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<List<Produto>> ProcessarListaDeProdutos(List<Produto> produtos)
        {
            _logger.LogInformation($"Processando {produtos.Count} produtos.");

            var estruturaMercadologica = _departamentalizacaoRepository.LoadDepartamentalizacaoJson();
            var produtosJson = JsonConvert.SerializeObject(produtos, Formatting.None);

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "Você é um assistente especializado em categorização de produtos de mercado. Responda apenas com JSON puro" },
                    new { role = "user", content = $"Aqui está a estrutura mercadológica:\n{estruturaMercadologica}" },
                    new { role = "user", content = $"Preencha o campo 'Subgrupo' neste JSON com o 'Codigo' do subgrupo:\n{produtosJson}" }
                },
                max_tokens = 2000
            };

            var jsonContent = JsonConvert.SerializeObject(requestBody);
            _logger.LogInformation($"Enviando requisição ao ChatGPT com payload de {jsonContent.Length} caracteres.");

            try
            {
                var response = await _httpClient.PostAsync(ApiUrl, new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Resposta da API OpenAI recebida com sucesso.");

                return _departamentalizacaoRepository.ParseResponse(content, produtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao conectar com a API OpenAI: {ex.Message}");
                return produtos;
            }
        }

        public void AtualizarDepartamentalizacao(Classe novaDepartamentalizacao)
        {
            _departamentalizacaoRepository.SaveDepartamentalizacao(novaDepartamentalizacao);
            _logger.LogInformation("Arquivo departamentalizacao.json atualizado com sucesso.");
        }
    }
}
