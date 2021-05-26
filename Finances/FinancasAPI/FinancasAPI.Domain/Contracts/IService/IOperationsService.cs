using System.Threading.Tasks;

namespace FinancasAPI.Domain.Contracts.IService
{
    public interface IOperationsService
    {
        Task<ICommandResult> GetOperations();
        Task<ICommandResult> GetOperationsByAtivo();
        Task<ICommandResult> GetOperationsByTypeOperations();
        Task<ICommandResult> GetOperationsByAccount();
        Task<ICommandResult> GetTotalByAtivos();
    }
}
