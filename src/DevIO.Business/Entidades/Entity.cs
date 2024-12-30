using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Entidades
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid(); //sempre um id novo
            
        }

        public Guid Id { get; set; }



    }
}
