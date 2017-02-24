using ExemploDAO.DB.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExemploDAO.DB
{
    public class ConfigDB
    {
        public static string StringConexao = "Persist Security Info=False;" + "server=localhost;" + "database=cronometragem;" + "uid=root;" + "pwd=aluno";

        private static ConfigDB _instance = null;
        private MySqlConnection _conexao;

        private ConfigDB()
        {
            try
            {
                this._conexao = new MySqlConnection(StringConexao);
                if (this.Conectar())
                {
                    this.PessoaRepository = new PessoaRepository(this._conexao);
                    this.TelefoneRepository = new TelefoneRepository(this._conexao);
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool Conectar()
        {
            try
            {
                this._conexao.Open();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao conectar no MySQL.", ex);
            }
            finally {
                this._conexao.Close();
            }
        }

        public PessoaRepository PessoaRepository { get; set; }

        public TelefoneRepository TelefoneRepository { get; set; }

        public static ConfigDB Instance{ get {
                if (_instance == null)
                    _instance = new ConfigDB();
                return _instance;
            } private set { }
        }
    }
}
