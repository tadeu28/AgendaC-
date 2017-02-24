using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ExemploDAO.DB.Model;
using System.Data;

namespace ExemploDAO.DB.Repository
{
    public class TelefoneRepository: RepositoryBase
    {
        public TelefoneRepository(MySqlConnection conexao) : base(conexao)
        {
        }

        public List<Telefone> Select()
        {
            try
            {
                var telefones = new List<Telefone>();

                var sql = "SELECT * from telefone";
                var dataSet = new DataSet();
                var query = new MySqlDataAdapter(sql, ConfigDB.StringConexao);
                query.Fill(dataSet);
                foreach (var item in dataSet.Tables[0].AsEnumerable().ToList())
                {
                    var tel = new Telefone
                    {
                        Id = Convert.ToInt32(item["id"]),
                        DDD = Convert.ToString(item["ddd"]),
                        Numero = Convert.ToString(item["ddd"]),
                        Tipo = Convert.ToString(item["tipo"]),
                        IdPessoa = Convert.ToInt32(item["id_pessoa"])
                    };
                    telefones.Add(tel);
                }
                return telefones;
            }
            catch (Exception ex)
            {
                throw new Exception("Não Foi possível buscar pessoa", ex);
            }
            finally
            {
                this._conexao.Close();
            }
        }

        public void Select(Pessoa pessoa)
        {
            try
            {
                var telefones = this.Select();

                //Expressão LAMBDA
                pessoa.Telefones =
                    telefones.Where(w => w.IdPessoa == pessoa.Id).ToList();

                //Expressao LINQ
                //pessoa.Telefones = (from t in telefones
                //                    where t.IdPessoa == pessoa.Id
                //                    select t).ToList();

                //filtro usando expressoes Lambda
                //telefones.ForEach(f =>
                //{
                //    if(f.IdPessoa == pessoa.Id)
                //    {
                //        pessoa.Telefones.Add(f);
                //    }
                //});

                //Codigo de filtro usando foreach - C# Rulez
                //foreach(var tel in telefones)
                //{
                //    if (tel.IdPessoa == pessoa.Id)
                //    {
                //        pessoa.Telefones.Add(tel);
                //    }
                //}

                //Codigo de filtro usando for
                //for(var i = 0; i< telefones.Count(); i++)
                //{
                //    if(telefones[i].IdPessoa == pessoa.Id)
                //    {
                //        pessoa.Telefones.Add(telefones[i]);
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível obter telefones da pessoa " + pessoa.Nome, ex);
            }
            finally
            {
                this._conexao.Close();
            }
        }

        public bool Insert (Telefone telefone)
        {
            try
            {
                this._conexao.Open();
                var sql = "INSERT INTO `cronometragem`.`telefone` (`ddd`, `numero`, `tipo`, id_pessoa) VALUES (@ddd, @numero, @tipo, @id_pessoa)";
                var cmd = new MySqlCommand(sql, this._conexao);
                cmd.Parameters.AddWithValue("@ddd", telefone.DDD);
                cmd.Parameters.AddWithValue("@numero", telefone.Numero);
                cmd.Parameters.AddWithValue("@tipo", telefone.Tipo);
                cmd.Parameters.AddWithValue("@id_pessoa", telefone.IdPessoa);
                cmd.ExecuteNonQuery();
                telefone.Id = Convert.ToInt32(cmd.LastInsertedId);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel inserir pessoa.", ex);
            }
            finally
            {
                this._conexao.Close();
            }
        }

    }
}
