using ExericioTela.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExericioTela.Repositorios
{
    public class PessoaRepositorio
    {
        private const string ConnectionString = "Data Source=localhost;Initial Catalog=Sexatura;Integrated Security=False;User Id=tiago;Password=24122412;";

        public void InserirPessoa(Pessoa pessoa)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                string sql = $@"
        INSERT INTO [dbo].[Pessoas]
                   ([Sexo]
                   ,[Altura])
             VALUES
                   ('{pessoa.Sexo}'
                   ,{pessoa.Altura.ToString().Replace(",", ".")})
        ";
                command.CommandText = sql; //define o texto do comando
                int qtd = command.ExecuteNonQuery(); //retorna linhas afetadas
                Console.WriteLine("Linhas afetadas:" + qtd);

            }

        }

        public List<Pessoa> LoadPessoas()
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                string sql = $@"
        SELECT  Id
                ,Sexo
                ,Altura
        FROM Pessoas;
        ";
                command.CommandText = sql;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string sexo = reader.GetString(1);
                    double altura = reader.GetDouble(2);

                    pessoas.Add(new Pessoa()
                    {
                        Id = id,
                        Sexo = sexo,
                        Altura = altura,
                    });
                }

            }
            return pessoas;
        }
        public void RemoverPessoa(Pessoa pessoa)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                string sql = $@"
            DELETE 
            FROM [dbo].[Pessoas] 
            WHERE Id = {pessoa.Id}
            ";
                command.CommandText = sql;
                int qtd = command.ExecuteNonQuery();
                Console.WriteLine("Linhas afetadas:" + qtd);
            }
        }

        public void Removertudo()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                string sql = $@"
                DELETE 
                FROM [dbo].[Pessoas]
                ";
                command.CommandText = sql;
                int qtd = command.ExecuteNonQuery();
                Console.WriteLine("Linhas afetadas:" + qtd);
            }
        }
    }
}
