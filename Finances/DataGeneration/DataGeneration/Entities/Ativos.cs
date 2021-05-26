using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGeneration.Entities
{
    public class Ativos
    {
        public Ativos(string aTIVOS, decimal vALOR)
        {
            ATIVOS = aTIVOS;
            VALOR = vALOR;
        }

        public string ATIVOS { get; set; }
        public decimal VALOR { get; set; }        
    }
}
