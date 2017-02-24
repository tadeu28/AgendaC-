using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploDAO.DB.Model
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public string Email{ get; set; }
        public IList<Telefone> Telefones { get; set; }

        public Pessoa()
        {
            this.Telefones = new List<Telefone>();
        }

    }
}
