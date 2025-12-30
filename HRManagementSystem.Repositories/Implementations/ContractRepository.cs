using HRManagementSystem.Data;
using HRManagementSystem.Data.Entities;
using HRManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Repositories.Implementations
{
    public class ContractRepository : IContractRepository
    {
        private readonly HRDbContext _context;

        public ContractRepository(HRDbContext _context)
        {
            this._context = _context;
        }

        public async Task<Contract> Create(Contract contract)
        {
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
           return contract;
        }

        public async Task<List<Contract>> GetAll()
        {
           return await _context.Contracts.Include(c => c.Employee).ToListAsync();
        }

        public async Task<Contract?> GetContractByid(int id)
        {
            return await _context.Contracts.Include(c => c.Employee).FirstOrDefaultAsync(c => c.ContractId == id);
        }

        public async Task<Contract> Update(Contract contract)
        {
            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();
            return contract;
        }

        public async Task<bool> HasContract(int employeeId)
        {
            return await _context.Contracts
                .AnyAsync(c => c.EmployeeId == employeeId && c.IsActive);
        }
    }
}
