using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasAPI.Domain.EntitiesOutput
{
    public class OperationsOutput
    {
        public string Ativo { get; set; }
        public string TypeOperation { get; set; }
        public int Account { get; set; }
        public decimal Amount { get; set; }
        public decimal AveragePrice { get; set; }
    }
}
