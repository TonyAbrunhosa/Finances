using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasAPI.Domain.Entities
{
    public class Operations
    {
        public DateTime DateTimeOperation { get; set; }
        public string Ativo { get; set; }
        public string TypeOperation { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public int Account { get; set; }
    }
}
