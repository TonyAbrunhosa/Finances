using System;

namespace FinancasDesktop.Entities
{
    public class Operation
    {
        public DateTime DateTimeOperation { get; set; }
        public string Ativo { get; set; }
        public string TypeOperation { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public int Account { get; set; }
    }
}
