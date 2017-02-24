using ExemploDAO.DB.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploDAO.DB.Repository
{
    public class PessoaRepository : RepositoryBase
    {

        public PessoaRepository(MySqlConnection conexao) : base(conexao)
        { }

        public bool Update(Pessoa pessoa)
        {
            try
            {
                this._conexao.Open();
                var sql = "UPDATE `cronometragem`.`pessoa` " +
                          "  SET " +                          
                          "  `nome` = @nome, " +
                          "  `data` = @data, " +
                          "  `email` = @email " +
                          "   WHERE `id` = @id;";
                
                var cmd = new MySqlCommand(sql, this._conexao);

                cmd.Parameters.AddWithValue("@id", pessoa.Id);
                cmd.Parameters.AddWithValue("@nome", pessoa.Nome);
                cmd.Parameters.AddWithValue("@data", pessoa.Data);
                cmd.Parameters.AddWithValue("@email", pessoa.Email);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Não foi possível atualizar a pessoa.", ex);
            }
            finally
            {
                this._conexao.Close();
            }
        }

        public List<Pessoa> Select()
        {            
            try {
                List<Pessoa> pessoas = new List<Pessoa>();
                var sql = "SELECT * from pessoa";
                var dataSet = new DataSet();
                var query = new MySqlDataAdapter(sql, ConfigDB.StringConexao);
                query.Fill(dataSet);
                foreach (var item in dataSet.Tables[0].AsEnumerable().ToList())
                {
                    var pessoa = new Pessoa
                    {
                        Id = Convert.ToInt32(item["id"]),
                        Nome = Convert.ToString(item["nome"]),
                        Data = Convert.ToDateTime(item["data"]),
                        Email = Convert.ToString(item["email"])
                    };

                    ConfigDB.Instance.TelefoneRepository.Select(pessoa);

                    pessoas.Add(pessoa);
                }
                return pessoas;
            }
            catch (Exception ex)
            {
                throw new Exception("Não Foi possível buscar pessoa", ex);
            }
            finally
            {

            }                    
            
        }

        public bool Insert(Pessoa pessoa)
        {
            try
            {
                this._conexao.Open();
                var sql = "insert into pessoa(nome, data, email) VALUES (@nome, @data, @email)";
                var cmd = new MySqlCommand(sql, this._conexao);

                cmd.Parameters.AddWithValue("@nome", pessoa.Nome);
                cmd.Parameters.AddWithValue("@data", pessoa.Data);
                cmd.Parameters.AddWithValue("@email", pessoa.Email);

                cmd.ExecuteNonQuery();
                pessoa.Id = Convert.ToInt32(cmd.LastInsertedId);
                return true;
            }
            catch (Exception ex) {
                throw new Exception("Não foi possivel inserir pessoa.", ex);
            }
            finally
            {
                this._conexao.Close();
            }
        }

        
    }
}
