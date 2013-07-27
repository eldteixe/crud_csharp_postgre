using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Telefones
    {
        public virtual int id { get; set; }

        public virtual int id_contatos { get; set; }

        public virtual int telefone { get; set; }

        public virtual string email { get; set; }

        public virtual string site { get; set; }
    }
}
