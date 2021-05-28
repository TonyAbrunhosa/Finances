using ClosedXML.Excel;
using FinancasDesktop.Classes;
using FinancasDesktop.Conexao;
using FinancasDesktop.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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
            CommandResult commandResult = new CommandResult();

            switch (button.Content)
            {
                case "Todas Operações":
                    startRequest = DateTime.Now.TimeOfDay;
                    commandResult = AcessoApi.Get<CommandResult>("Operation");
                    CreateLogTimeResponse(button.Content.ToString());
                    DeserializeObject(commandResult);
                    break;
                case "Operações por Ativos":
                    startRequest = DateTime.Now.TimeOfDay;
                    commandResult = AcessoApi.Get<CommandResult>("Operation/GetByAtivo");
                    CreateLogTimeResponse(button.Content.ToString());
                    DeserializeObject(commandResult);
                    break;
                case "Operações por Conta":
                    startRequest = DateTime.Now.TimeOfDay;
                    commandResult = AcessoApi.Get<CommandResult>("Operation/GetByAccount");
                    CreateLogTimeResponse(button.Content.ToString());
                    DeserializeObject(commandResult);
                    break;
                case "Operações por tipo de operação":
                    startRequest = DateTime.Now.TimeOfDay;
                    commandResult = AcessoApi.Get<CommandResult>("Operation/GetByTypeOperations");
                    CreateLogTimeResponse(button.Content.ToString());
                    DeserializeObject(commandResult);
                    break;
                case "Exportar":
                    ExportExcel();
                    break;
            }            
        }

        private void DeserializeObject(CommandResult commandResult)
        {
            List<Operation> operations = new List<Operation>();

            if (commandResult.Success)
            {
                operations = JsonConvert.DeserializeObject<List<Operation>>(commandResult.Data.ToString());
                dtgOperations.ItemsSource = operations;
                EnableColumns(operations);
                MessageBox.Show("Consulta realizada com sucesso.", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ocorreu um erro ao realizar a sua consulta.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportExcel()
        {
            try
            {
                if ((dtgOperations?.Items?.Count ?? 0) == 0)
                    MessageBox.Show("Realize uma consulta antes de Exporta", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                    saveFileDialog1.Filter = "Pasta de trabalho do Excel (.xlsx) |*.xlsx|CSV UTF-8 (Delimitado por vírgula) .csv |*.csv";
                    saveFileDialog1.Title = "Salvar Excel";
                    saveFileDialog1.ShowDialog();
                    if (saveFileDialog1.FileName != "")
                    {
                        if (saveFileDialog1.FileName.Contains(".xlsx"))
                            CreateExcelXlsx(saveFileDialog1.FileName);
                        else if (saveFileDialog1.FileName.Contains(".csv"))
                            CreateExcelCsv(saveFileDialog1.FileName);

                        MessageBox.Show($"Excel gerado com sucesso no diretório informado.\n{saveFileDialog1.FileName} ", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o Excel", "Atenção", MessageBoxButton.OK, MessageBoxImage.Error);
                LogEx.GerarLog(ex);
            }
        }

        private void CreateExcelCsv(string fileName)
        {
            using (StreamWriter stream = new StreamWriter(fileName,true,Encoding.GetEncoding("iso-8859-1")))
            {
                string colluns = "";
                

                for (int i = 0; i < 7; i++)
                {
                    colluns = colluns +
                         (dtgOperations.Columns[i].Visibility == Visibility.Visible ? 
                            dtgOperations.Columns[i].Header.ToString() +
                                (i == 7 ? "" : ";") : "");
                }

                stream.WriteLine(colluns);

                foreach (var item in dtgOperations.ItemsSource as List<Operation>)
                {
                    string rows = string.Empty;

                    if (ColAccount.Visibility == Visibility.Visible)
                        rows = item.Account.ToString() + ";";
                    if (ColDateTimeOperation.Visibility == Visibility.Visible)
                        rows = rows + item.DateTimeOperation.ToString() + ";";
                    if (ColAtivo.Visibility == Visibility.Visible)
                        rows = rows + item.Ativo.ToString() + ";";
                    if (ColTypeOperation.Visibility == Visibility.Visible)
                        rows = rows + item.TypeOperation.ToString() == "C" ? "COMPRA" : "VENDA" + ";";
                    if (ColAmount.Visibility == Visibility.Visible)
                        rows = rows + item.Amount.ToString() + ";";
                    if (ColPrice.Visibility == Visibility.Visible)
                        rows = rows + item.Price.ToString() + ";";
                    if (ColAveragePrice.Visibility == Visibility.Visible)
                        rows = rows + item.AveragePrice.ToString() + ";";

                    stream.WriteLine(rows);
                }                
            }            
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

        private void CreateExcelXlsx(string fileName)
        {
            List<Operation> operacoes = dtgOperations.ItemsSource as List<Operation>;
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var workbook = new XLWorkbook();
            workbook.AddWorksheet("OPERAÇÕES");
            var ws = workbook.Worksheet("OPERAÇÕES");
            int row = 2;
            int position = 0;

            foreach (var item in dtgOperations.Columns)
            {
                if(item.Visibility == Visibility)
                {
                    var word = Convert.ToString(alphabet[position]);
                    ws.Cell(word + (1).ToString()).Value = item.Header.ToString();
                    position++;                    
                }
                
            }            

            foreach (var c in operacoes)
            {
                position = 0;
                if(ColAccount.Visibility == Visibility.Visible)
                    ws.Cell(Convert.ToString(alphabet[position++]) + row.ToString()).Value = c.Account;
                if (ColDateTimeOperation.Visibility == Visibility.Visible)
                    ws.Cell(Convert.ToString(alphabet[position++]) + row.ToString()).Value = c.DateTimeOperation;
                if (ColAtivo.Visibility == Visibility.Visible)
                    ws.Cell(Convert.ToString(alphabet[position++]) + row.ToString()).Value = c.Ativo;
                if (ColTypeOperation.Visibility == Visibility.Visible)
                    ws.Cell(Convert.ToString(alphabet[position++]) + row.ToString()).Value = c.TypeOperation == "C" ? "COMPRA" : "VENDA";
                if (ColAmount.Visibility == Visibility.Visible)
                    ws.Cell(Convert.ToString(alphabet[position++]) + row.ToString()).Value = c.Amount;
                if (ColPrice.Visibility == Visibility.Visible)
                    ws.Cell(Convert.ToString(alphabet[position++]) + row.ToString()).Value = c.Price;
                if (ColAveragePrice.Visibility == Visibility.Visible)
                    ws.Cell(Convert.ToString(alphabet[position++]) + row.ToString()).Value = c.AveragePrice;
                row++;
            }

            workbook.SaveAs(fileName);
        }       
    }
    
}
