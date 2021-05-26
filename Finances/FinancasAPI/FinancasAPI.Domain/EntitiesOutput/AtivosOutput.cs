using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasAPI.Domain.EntitiesOutput
{
    public class AtivosOutput
    {
        public string Ativo { get; set; }
        public string TypeOperation { get; set; }
        public decimal SalePrice { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal Total { get; set; }
    }
}
