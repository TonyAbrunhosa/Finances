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

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            switch (button.Content)
            {
                case "Todas Operações":
                    dtgOperations.ItemsSource = AcessoApi.Get<Operation>("Operation");
                    ColAtivo.Visibility = Visibility.Visible;
                    ColDateTimeOperation.Visibility = Visibility.Visible;
                    ColTypeOperation.Visibility = Visibility.Visible;
                    ColAmount.Visibility = Visibility.Visible;
                    ColPrice.Visibility = Visibility.Visible;
                    ColAccount.Visibility = Visibility.Visible;
                    ColAveragePrice.Visibility = Visibility.Collapsed;
                    break;
                case "Operações por Ativos":
                    dtgOperations.ItemsSource = AcessoApi.Get<Operation>("Operation/GetByAtivo");
                    ColAtivo.Visibility = Visibility.Visible;
                    ColDateTimeOperation.Visibility = Visibility.Collapsed;                    
                    ColTypeOperation.Visibility = Visibility.Collapsed;
                    ColAmount.Visibility = Visibility.Visible;
                    ColPrice.Visibility = Visibility.Collapsed;
                    ColAccount.Visibility = Visibility.Collapsed;
                    ColAveragePrice.Visibility = Visibility.Visible;
                    break;
                case "Operações por Conta":
                    dtgOperations.ItemsSource = AcessoApi.Get<Operation>("Operation/GetByAccount");
                    ColAtivo.Visibility = Visibility.Collapsed;
                    ColDateTimeOperation.Visibility = Visibility.Collapsed;
                    ColTypeOperation.Visibility = Visibility.Collapsed;
                    ColAmount.Visibility = Visibility.Visible;
                    ColPrice.Visibility = Visibility.Collapsed;
                    ColAccount.Visibility = Visibility.Visible;
                    ColAveragePrice.Visibility = Visibility.Visible;
                    break;
                case "Operações por tipo de operação":
                    dtgOperations.ItemsSource = AcessoApi.Get<Operation>("Operation/GetByTypeOperations");
                    ColAtivo.Visibility = Visibility.Collapsed;
                    ColDateTimeOperation.Visibility = Visibility.Collapsed;
                    ColTypeOperation.Visibility = Visibility.Visible;
                    ColAmount.Visibility = Visibility.Visible;
                    ColPrice.Visibility = Visibility.Collapsed;
                    ColAccount.Visibility = Visibility.Collapsed;
                    ColAveragePrice.Visibility = Visibility.Visible;
                    break;
                //case "":
                //    dtgOperations.ItemsSource = AcessoApi.Get<Operation>("Operation");
                //    break;
                default:
                    break;
            }
        }
    }
}
