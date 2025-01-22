public class ProdutoDto
{
    public int? CodigoProduto { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public int? Subgrupo { get; set; } 
    public string Ncm { get; set; } = string.Empty;
    public string Cest { get; set; } = string.Empty;
    public List<string> CodigosBarras { get; set; } = new();
    public string NomeCliente { get; set; } = string.Empty;
    public string CnpjCliente { get; set; } = string.Empty;
}
