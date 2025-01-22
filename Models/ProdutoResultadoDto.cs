public class ProdutoResultadoDto
{
    public int? CodigoProduto { get; set; }  // Permite valores nulos
    public string Descricao { get; set; } = string.Empty;
    public int? Subgrupo { get; set; }  // Permite valores nulos
}
