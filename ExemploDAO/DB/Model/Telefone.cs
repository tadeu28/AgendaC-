using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploDAO.DB.Model
{
    public class Telefone
    {
        public int Id { get; set; }
        public string DDD { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public int IdPessoa { get; set; }
    }
}
