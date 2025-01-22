namespace categorizationapi.Repositories
{
    public interface IProdutoRepository
    {
        Task<string?> ObterCodigoBarra(List<string> codigosBarras);
        Task<ProdutoDto?> ObterProdutoPorCodigo(string codigoBarra);
        Task AtualizarProduto(ProdutoDto produto);
    }
}
