using FinancasDesktop.Classes;
using FinancasDesktop.Conexao;
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

        public TimeSpan startRequest { get; private set; }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            List<Operation> operations = new List<Operation>();

            switch (button.Content)
            {
                case "Todas Operações":
                    startRequest = DateTime.Now.TimeOfDay;
                    operations = AcessoApi.Get<Operation>("Operation");
                    CreateLogTimeResponse(button.Content.ToString());
                    break;
                case "Operações por Ativos":
                    startRequest = DateTime.Now.TimeOfDay;
                    operations = AcessoApi.Get<Operation>("Operation/GetByAtivo");
                    CreateLogTimeResponse(button.Content.ToString());
                    break;
                case "Operações por Conta":
                    startRequest = DateTime.Now.TimeOfDay;
                    operations = AcessoApi.Get<Operation>("Operation/GetByAccount");
                    CreateLogTimeResponse(button.Content.ToString());
                    break;
                case "Operações por tipo de operação":
                    startRequest = DateTime.Now.TimeOfDay;
                    operations = AcessoApi.Get<Operation>("Operation/GetByTypeOperations");
                    CreateLogTimeResponse(button.Content.ToString());

                    break;
            }

            dtgOperations.ItemsSource = operations;
            EnableColumns(operations);
        }

        private void CreateLogTimeResponse(string nameButton)
        {
            lblTimeRequest.Content = (DateTime.Now.TimeOfDay - startRequest).ToString();
            LogEx.GeraLogSimples($"TEMPO DE RETORNO DA API PARA A CONSULTA: '{nameButton}' => {lblTimeRequest.Content}", "LogRequests");
        }

        private void EnableColumns(List<Operation> operations)
        {
            ColAtivo.Visibility = operations.Any(x => !string.IsNullOrEmpty(x.Ativo)) ? Visibility.Visible : Visibility.Collapsed;
            ColAveragePrice.Visibility = operations.Any(x => (x?.AveragePrice ?? 0) > 0) ? Visibility.Visible : Visibility.Collapsed;
            ColAccount.Visibility = operations.Any(x => (x?.Account ?? 0) > 0) ? Visibility.Visible : Visibility.Collapsed;
            ColPrice.Visibility = operations.Any(x => (x?.Price ?? 0) > 0) ? Visibility.Visible : Visibility.Collapsed;
            ColAmount.Visibility = operations.Any(x => (x?.Amount ?? 0) > 0) ? Visibility.Visible : Visibility.Collapsed;
            ColTypeOperation.Visibility = operations.Any(x => !string.IsNullOrEmpty(x.TypeOperation)) ? Visibility.Visible : Visibility.Collapsed;
            ColDateTimeOperation.Visibility = operations.Any(x => x.DateTimeOperation.HasValue ) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
