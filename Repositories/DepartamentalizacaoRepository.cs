using Newtonsoft.Json;
using System.IO;
using categorizationapi.Models;
using Microsoft.Extensions.Logging;

namespace categorizationapi.Repositories
{
    public class DepartamentalizacaoRepository : IDepartamentalizacaoRepository
    {
        private readonly string _filePath = "departamentalizacao.json";
        private readonly ILogger<DepartamentalizacaoRepository> _logger;

        public DepartamentalizacaoRepository(ILogger<DepartamentalizacaoRepository> logger)
        {
            _logger = logger;
        }

        public string LoadDepartamentalizacaoJson()
        {
            if (!File.Exists(_filePath))
            {
                _logger.LogWarning("Arquivo departamentalizacao.json não encontrado. Criando um novo.");
                var defaultData = new Classe { Codigo = "100", Nome = "Mercado Central", Departamentos = new List<Departamento>() };
                SaveDepartamentalizacao(defaultData);
            }
            return File.ReadAllText(_filePath);
        }

        public void SaveDepartamentalizacao(Classe novaDepartamentalizacao)
        {
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(novaDepartamentalizacao, Formatting.Indented));
        }

        public List<Produto> ParseResponse(string jsonResponse, List<Produto> produtosOriginais)
        {
            var parsedResponse = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            string rawText = (string)parsedResponse.choices[0].message.content;
            
            try
            {
                var produtosProcessados = JsonConvert.DeserializeObject<List<Produto>>(rawText);
                return produtosProcessados ?? produtosOriginais;
            }
            catch (JsonSerializationException)
            {
                _logger.LogWarning("O JSON não estava no formato esperado de lista, tentando analisar como objeto com chave.");
                var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, List<Produto>>>(rawText);
                if (jsonObject != null && jsonObject.Count > 0)
                {
                    foreach (var key in jsonObject.Keys)
                    {
                        if (jsonObject[key] != null && jsonObject[key].Count > 0)
                        {
                            return jsonObject[key];
                        }
                    }
                }
            }

            return produtosOriginais;
        }
    }
}