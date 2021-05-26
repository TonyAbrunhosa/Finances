using FinancasAPI.Domain.Contracts.IRepository;
using FinancasAPI.Domain.Entities;
using FinancasAPI.Domain.EntitiesOutput;
using FinancasAPI.Infra.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasAPI.Infra.Repository
{
    public class OperationsRepository: IOperationsRepository
    {
        public string Path { get; set; } = System.AppDomain.CurrentDomain.BaseDirectory.ToString();

        private readonly CommunicationJson _communicationJson;

        public OperationsRepository(CommunicationJson communicationJson)
        {
            _communicationJson = communicationJson;
        }

        public async Task<IEnumerable<Operations>> GetOperations()
        {
            return await Task.Run(() => 
            {
                return JsonConvert.DeserializeObject<IEnumerable<Operations>>(_communicationJson.ReadFile(Path));
            });
        }

        public async Task<IEnumerable<OperationsOutput>> GetOperationsByAtivo()
        {
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<IEnumerable<Operations>>(_communicationJson.ReadFile(Path))
                .GroupBy(x=> x.Ativo)
                .Select(t => new OperationsOutput
                {
                    Ativo = t.Key,
                    Amount = t.Sum(x=>x.Amount),
                    AveragePrice = t.Sum(x=>x.Amount * x.Price)
                });
            });
        }
        public async Task<IEnumerable<OperationsOutput>> GetOperationsByTypeOperations()
        {
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<IEnumerable<Operations>>(_communicationJson.ReadFile(Path))
                .GroupBy(x => x.TypeOperation)
                .Select(t => new OperationsOutput
                {
                    TypeOperation = t.Key,
                    Amount = t.Sum(x => x.Amount),
                    AveragePrice = t.Sum(x => x.Amount * x.Price)
                });
            });
        }
        public async Task<IEnumerable<OperationsOutput>> GetOperationsByAccount()
        {
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<IEnumerable<Operations>>(_communicationJson.ReadFile(Path))
                .GroupBy(x => x.Account)
                .Select(t => new OperationsOutput
                {
                    Account = t.Key,
                    Amount = t.Sum(x => x.Amount),
                    AveragePrice = t.Sum(x => x.Amount * x.Price)
                });
            });
        }

        public async Task<IEnumerable<AtivosOutput>> GetTotalByAtivos()
        {
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<IEnumerable<Operations>>(_communicationJson.ReadFile(Path))
                .GroupBy(x => new { x.Ativo, x.TypeOperation })
                .Select(t => new AtivosOutput
                {
                    Ativo = t.Key.Ativo,
                    TypeOperation = t.Key.TypeOperation,
                    BuyPrice = t.Sum(x=> x.Price),
                    SalePrice = t.Sum(x=>x.Price)
                });
            });
        }
    }
}
