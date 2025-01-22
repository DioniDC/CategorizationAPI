namespace categorizationapi.Models
{
    public class LogProduto
    {
        public string? NomeCliente { get; set; }
        public string? CnpjCliente { get; set; }
        public DateTime DataLog { get; set; }
        public string? CodigoProduto { get; set; }
        public string? Descricao { get; set; }
        public string? CodigoBarras { get; set; }
        public string? SubgrupoAntes { get; set; }
        public string? SubgrupoDepois { get; set; }
    }
}
