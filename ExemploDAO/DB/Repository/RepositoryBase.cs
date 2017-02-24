using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploDAO.DB.Repository
{
    public class RepositoryBase
    {
        protected MySqlConnection _conexao = null;

        public RepositoryBase(MySqlConnection conexao)
        {
            this._conexao = conexao;
        }

        public bool Delete(string tabela, int id)
        {
            try {
                this._conexao.Open();
                var sql = "delete from " + tabela + " where id = @id ";
                var cmd = new MySqlCommand(sql, this._conexao);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                return true;
            } catch (Exception ex) {
                throw new Exception("Não foi possivel excluir o registro da tabela " + tabela, ex);
            }
            finally
            {
                this._conexao.Close();
            }
        }
    }
}
