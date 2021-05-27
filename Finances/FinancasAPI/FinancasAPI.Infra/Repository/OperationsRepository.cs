using FinancasAPI.Domain.Contracts.IRepository;
using FinancasAPI.Domain.Entities;
using FinancasAPI.Domain.EntitiesOutput;
using FinancasAPI.Infra.Base;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinancasAPI.Infra.Repository
{
    public class OperationsRepository: IOperationsRepository
    {
        public string Path { get; set; } = System.AppDomain.CurrentDomain.BaseDirectory.ToString();

        private readonly CommunicationJson _communicationJson;
        private readonly IMemoryCache _memCache;

        public OperationsRepository(CommunicationJson communicationJson, IMemoryCache memCache)
        {
            _communicationJson = communicationJson;
            _memCache = memCache;
        }

        public async Task<IEnumerable<Operations>> GetOperations()
        {
            var operatios = await _memCache.GetOrCreateAsync("ExecGetOperations", entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(20);
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120);
                entry.SetPriority(CacheItemPriority.High);

                Thread.Sleep(3000);
                return ExecGetOperations();
            });

            return operatios;           
        }
        private async Task<IEnumerable<Operations>> ExecGetOperations()
        {
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<IEnumerable<Operations>>(_communicationJson.ReadFile(Path));
            });
        }
        public async Task<IEnumerable<OperationsOutput>> GetOperationsByAtivo()
        {
            var operatios = await _memCache.GetOrCreateAsync("ExecGetOperationsByAtivo", entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(20);
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120);
                entry.SetPriority(CacheItemPriority.High);

                Thread.Sleep(3000);
                return ExecGetOperationsByAtivo();
            });

            return operatios;
        }
        private async Task<IEnumerable<OperationsOutput>> ExecGetOperationsByAtivo()
        {
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<IEnumerable<Operations>>(_communicationJson.ReadFile(Path))
                .GroupBy(x => x.Ativo)
                .Select(t => new OperationsOutput
                {
                    Ativo = t.Key,
                    Amount = t.Sum(x => x.Amount),
                    AveragePrice = t.Sum(x => x.Amount * x.Price)
                });
            });
        }
        public async Task<IEnumerable<OperationsOutput>> GetOperationsByTypeOperations()
        {
            var operatios = await _memCache.GetOrCreateAsync("ExecGetOperationsByTypeOperations", entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(20);
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120);
                entry.SetPriority(CacheItemPriority.High);

                Thread.Sleep(3000);
                return ExecGetOperationsByTypeOperations();
            });

            return operatios;
        }
        private async Task<IEnumerable<OperationsOutput>> ExecGetOperationsByTypeOperations()
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
            var operatios = await _memCache.GetOrCreateAsync("ExecGetOperationsByAccount", entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(20);
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120);
                entry.SetPriority(CacheItemPriority.High);

                Thread.Sleep(3000);
                return ExecGetOperationsByAccount();
            });

            return operatios;
        }
        private async Task<IEnumerable<OperationsOutput>> ExecGetOperationsByAccount()
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
                    BuyPrice = t.Key.TypeOperation == "C" ? t.Sum(x=> x.Price) : 0,
                    SalePrice = t.Key.TypeOperation == "V" ? t.Sum(x => x.Price) : 0
                });
            });
        }
    }
}
