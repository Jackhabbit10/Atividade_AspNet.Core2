
namespace AtividadeCidade.Models
{
    public class Produto
    {
        public int? cod_prod { get; set; }
        public string? nome_prod { get; set; }
        public string? descricao_prod { get; set; }
        public decimal? preco_prod { get; set; }
        public int quantidade_prod { get; set; }

        public List<Produto>? ListaProdutos { get; set; }
    }
}