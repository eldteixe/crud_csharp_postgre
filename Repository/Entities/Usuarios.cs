using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Usuarios
    {
        public virtual int id { get; set; }

        public virtual string nome { get; set; }

        public virtual string login { get; set; }

        public virtual string senha { get; set; }

        public virtual char status { get; set; }
    }
}
