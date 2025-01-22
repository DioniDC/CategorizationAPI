using categorizationapi.Models;

namespace categorizationapi.Repositories
{
    public interface IDepartamentalizacaoRepository
    {
        string LoadDepartamentalizacaoJson();
        void SaveDepartamentalizacao(Classe novaDepartamentalizacao);
        List<Produto> ParseResponse(string jsonResponse, List<Produto> produtosOriginais);
    }
}
