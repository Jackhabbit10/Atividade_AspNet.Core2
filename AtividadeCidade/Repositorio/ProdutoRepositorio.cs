using MySql.Data.MySqlClient;
using AtividadeCidade.Models;
using System.Data;
using MySqlX.XDevAPI;

namespace AtividadeCidade.Repositorio
{

    public class ProdutoRepositorio(IConfiguration configuration)
    {

        private readonly string _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");

        public void Cadastrar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into tb_produto values (null, @nome, @descricao, @preco, @quantidade)", conexao);
             
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.nome_prod;

                cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.descricao_prod;

                cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.preco_prod;

                cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidade_prod;

                cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }

        public IEnumerable<Produto>TodosProdutos()
        {
            List<Produto> Produtolist = new List<Produto>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from tb_produto", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Produtolist.Add(
                                new Produto
                                {
                                    cod_prod = Convert.ToInt32(dr["cod_prod"]),
                                    nome_prod = ((string)dr["nome_prod"]),
                                    descricao_prod = ((string)dr["descricao_prod"]),
                                    preco_prod = Convert.ToDecimal(dr["preco_prod"]),
                                    quantidade_prod = Convert.ToInt32(dr["quantidade_prod"]),
                                });
                }
                return Produtolist;
            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {

                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tb_produto where cod_prod=@codigo", conexao);
                cmd.Parameters.AddWithValue("@codigo", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
