using DataGeneration.Configuration;
using DataGeneration.Entities;
using FinancasDesktop.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DataGeneration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Inicio....");            
            GenereteData genereteData = new GenereteData();
            Console.WriteLine("##########");
            CreateDadosOperations(genereteData.ListarAccount(), genereteData.ListarAtivos());
            Console.WriteLine("Dados Gerados com sucesso....");
            Console.WriteLine("3...");
            Thread.Sleep(1000);
            Console.WriteLine("2...");
            Thread.Sleep(1000);
            Console.WriteLine("1...");
            Thread.Sleep(1000);
            Console.WriteLine("Fim!");
            Thread.Sleep(1000);
        }

        private static void CreateDadosOperations(List<int> accounts, List<Ativos> ativos)
        {            
            List<Operation> operations = new List<Operation>();
            
            for (int i = 0; i < 20000; i++)
            {
                var randomAccount = new Random().Next(17);
                var randomAtivos = new Random().Next(17);
                var amount = new Random().Next(200);
                decimal baseSum = new Random().Next(20);
                decimal price = ativos[randomAtivos].VALOR;
                decimal percentage = ((baseSum / 100) * price);
                var baseTypeOperation = new Random().Next(2);
                var day = new Random().Next(150);

                Operation operation = new Operation
                {
                    Ativo = ativos[randomAtivos].ATIVOS.ToString(),
                    Price = baseSum % 2 == 0 ? price + percentage : price - percentage,
                    Account = accounts[randomAccount],
                    TypeOperation = baseTypeOperation == 1 ? "C" : "V",
                    Amount = amount,
                    DateTimeOperation = DateTime.Now.AddDays(-day)
                };

                operations.Add(operation);
            }

            CreateJson(operations);
        }

        private static void CreateJson(List<Operation> operations)
        {
            var path = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "DataOperations";

            if (!File.Exists($"{path}.json"))
                File.Create($"{path}.json").Close();

            var fullPath = new FileInfo($"{path}.json").FullName.ToString();
            var objectJson = JsonConvert.SerializeObject(operations).ToString();

            File.WriteAllText(fullPath,  objectJson);
        }
    }
}
