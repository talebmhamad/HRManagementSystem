using HRManagementSystem.Data;
using HRManagementSystem.Data.Entities;
using HRManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Repositories.Implementations
{
    public class ContractRepositry : IContractRepository
    {
        private readonly HRDbContext _context;

        public ContractRepositry(HRDbContext _context)
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
           return await _context.Contracts.ToListAsync();
        }

        public async Task<Contract?> GetContractByid(int id)
        {
            return await _context.Contracts.FindAsync(id);
        }

        public async Task<Contract> Update(Contract contract)
        {
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
            return contract;
        }
    }
}
