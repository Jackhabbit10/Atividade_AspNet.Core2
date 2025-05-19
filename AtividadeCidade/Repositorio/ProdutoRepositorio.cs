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

        public bool Editar(Produto produto)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("Update tb_produto set cod_prod=@codigo, nome_prod=@nome, descricao_prod=@descricao, preco_prod=@preco, quantidade_prod=@quantidade where cod_prod=@codigo;", conexao);

                    cmd.Parameters.Add("@codigo", MySqlDbType.Int32).Value = produto.cod_prod;

                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.nome_prod;
                    
                    cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.descricao_prod;
                    
                    cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.preco_prod;
                    
                    cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidade_prod;
                   
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                        return linhasAfetadas > 0; 
                }
            }
            catch (MySqlException ex)
            {
                
                Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
                return false; 

            }
        }

        public Produto ObterProduto(int id)
        {
            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                // Cria um novo comando SQL para selecionar um registro da tabela 'cliente' com base no código
                MySqlCommand cmd = new MySqlCommand("SELECT * from tb_produto where cod_prod=@codigo ", conexao);

                // Adiciona um parâmetro para o código a ser buscado, definindo seu tipo e valor
                cmd.Parameters.AddWithValue("@codigo", id);

                // Cria um adaptador de dados (não utilizado diretamente para ExecuteReader)
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                // Declara um leitor de dados do MySQL
                MySqlDataReader dr;
                // Cria um novo objeto Cliente para armazenar os resultados
                Produto produto = new Produto();

                /* Executa o comando SQL e retorna um objeto MySqlDataReader para ler os resultados
                CommandBehavior.CloseConnection garante que a conexão seja fechada quando o DataReader for fechado*/

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                // Lê os resultados linha por linha
                while (dr.Read())
                {
                    
                    produto.cod_prod = Convert.ToInt32(dr["cod_prod"]);
                    produto.nome_prod = (string)(dr["nome_prod"]); 
                    produto.descricao_prod = (string)(dr["descricao_prod"]);
                    produto.preco_prod = (decimal)(dr["preco_prod"]);
                    produto.quantidade_prod = (Int32)(dr["quantidade_prod"]);
                }
                // Retorna o objeto Cliente encontrado (ou um objeto com valores padrão se não encontrado)
                return produto;
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
