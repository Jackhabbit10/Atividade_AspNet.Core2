using MySql.Data.MySqlClient;
using AtividadeCidade.Models;
using System.Data;


namespace AtividadeCidade.Repositorio
{
    public class UsuarioRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");
        public Usuario ObterUsuario(string email)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("SELECT * FROM tb_usuario WHERE email_usu = @email", conexao);
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Usuario usuario = null;
                    if (dr.Read())
                    {

                        usuario = new Usuario
                        {
                            cod_usu = Convert.ToInt32(dr["cod_usu"]),
                            nome_usu = dr["nome_usu"].ToString(),
                            email_usu = dr["email_usu"].ToString(),
                            senha_usu = dr["senha_usu"].ToString()
                        };
                    }

                    return usuario;
                }
            }
        }
    }
}