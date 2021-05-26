using FinancasDesktop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinancasDesktop.Views.UC.Queries
{
    /// <summary>
    /// Interação lógica para UCQueyOperations.xam
    /// </summary>
    public partial class UCQueyOperations : UserControl
    {
        public UCQueyOperations()
        {
            InitializeComponent();
        }

        private void btnQuryOperation_Click(object sender, RoutedEventArgs e)
        {
            List<Operation> operations = new List<Operation>();
            Operation operation = new Operation();
            operation.DateTimeOperation = DateTime.Now;
            operation.Ativo = "PETR4";
            operation.TypeOperation = "C";
            operation.Amount = 100;
            operation.Price = 25.31m;
            operation.Account = 2154;
            operations.Add(operation);

            dtgOperations.ItemsSource = operations;
        }

        private void btnQuryOperationR_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
