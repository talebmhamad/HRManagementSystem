using HRManagementSystem.Data.DTOs;

namespace HRManagementSystem.Services.Interfaces
{
    public interface IContractService
    {
        Task<bool> AddContract(ContractDto contract);
        Task<bool> UpdateContract(ContractDto contract);
        Task<List<ContractDto>> GetAll();
        Task<ContractDto?> GetContractByid(int id);
        Task<bool> HasContract(int employeeId);

        Task<int> CountExpiringContracts();
    }
}
