using HRManagementSystem.Data.Entities;

namespace HRManagementSystem.Repositories.Interfaces
{
    public interface IContractRepository
    {
        Task<Contract> Create(Contract contract);
        Task<Contract> Update(Contract contract);
        Task<List<Contract>> GetAll();
        Task<Contract?> GetContractByid(int id);

        Task<bool> HasContract(int employeeId);

        Task<int> CountExpiringContracts();



    }
}
