using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Repositories.Interfaces;
using HRManagementSystem.Services.Interfaces;
using HRManagementSystem.Services.Mappers;

namespace HRManagementSystem.Services.Implementations
{
    public class ContractService :IContractService
    {
        private readonly IContractRepository _contractRepository;

        public ContractService(IContractRepository _contractRepository)
        {
            this._contractRepository = _contractRepository;
        }

        public async Task<bool> AddContract(ContractDto contract)
        {
            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract), "Contract cannot be null");
            }

            var hasContract = await _contractRepository.HasContract(contract.EmployeeId);
            if (hasContract)
            {
                return false; 
            }

            var contractEntity = ContractMapper.ToEntity(contract);

            await _contractRepository.Create(contractEntity);

            return true;
        }

        public async Task<bool> UpdateContract(ContractDto contract)
        {
            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract), "Contract cannot be null");
            }
            var contractEntity = ContractMapper.ToEntity(contract);

            await _contractRepository.Update(contractEntity);

            return true;
        }

        public async Task<List<ContractDto>> GetAll()
        {
            var contractEntities = await _contractRepository.GetAll();
            return contractEntities.Select(ContractMapper.ToDto).Where(dto => dto != null).ToList()!;
        }
        public async Task<ContractDto?> GetContractByid(int id)
        {
            var contractEntity = await _contractRepository.GetContractByid(id);
            return ContractMapper.ToDto(contractEntity!);
        }
        public async Task<bool> HasContract(int employeeId)
        {
            return await _contractRepository.HasContract(employeeId);

        }

        public async Task<int> CountExpiringContracts()
        {
            return await _contractRepository.CountExpiringContracts();
        }

    }
}
