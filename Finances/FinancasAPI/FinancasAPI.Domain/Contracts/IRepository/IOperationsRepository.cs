using FinancasAPI.Domain.Entities;
using FinancasAPI.Domain.EntitiesOutput;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasAPI.Domain.Contracts.IRepository
{
    public interface IOperationsRepository
    {
        Task<IEnumerable<Operations>> GetOperations();
        Task<IEnumerable<OperationsOutput>> GetOperationsByAtivo();
        Task<IEnumerable<OperationsOutput>> GetOperationsByTypeOperations();
        Task<IEnumerable<OperationsOutput>> GetOperationsByAccount();
        Task<IEnumerable<AtivosOutput>> GetTotalByAtivos();
    }
}
