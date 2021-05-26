using FinancasAPI.Domain.Contracts;
using FinancasAPI.Domain.Contracts.IRepository;
using FinancasAPI.Domain.Contracts.IService;
using FinancasAPI.Domain.Entities;
using FinancasAPI.Domain.EntitiesOutput;
using System.Linq;
using System.Threading.Tasks;

namespace FinancasAPI.Application.Service
{
    public class OperationsService : IOperationsService
    {
        private readonly IOperationsRepository _operationRepository;

        public OperationsService(IOperationsRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public async Task<ICommandResult> GetOperations()
        {
            return new CommandResult(true,"Success", await _operationRepository.GetOperations());
        }

        public async Task<ICommandResult> GetOperationsByAccount()
        {
            return new CommandResult(true, "Success", await _operationRepository.GetOperationsByAccount());
        }

        public async Task<ICommandResult> GetOperationsByAtivo()
        {
            return new CommandResult(true, "Success", await _operationRepository.GetOperationsByAtivo());
        }

        public async Task<ICommandResult> GetOperationsByTypeOperations()
        {
            return new CommandResult(true, "Success", await _operationRepository.GetOperationsByTypeOperations());
        }

        public async Task<ICommandResult> GetTotalByAtivos()
        {
            var output = (await _operationRepository.GetTotalByAtivos()).ToList();

            output = output
                .GroupBy(x => x.Ativo)
                .Select(c=> new AtivosOutput
                {
                    Ativo = c.Key,
                    BuyPrice = c.Sum(x=>x.BuyPrice),
                    SalePrice = c.Sum(x => x.SalePrice),
                    Total = c.Sum(x => x.BuyPrice) - c.Sum(x => x.SalePrice)
                }).ToList();

            return new CommandResult(true, "Success", output);
        }
    }
}
