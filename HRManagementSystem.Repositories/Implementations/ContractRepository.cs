using HRManagementSystem.Data;
using HRManagementSystem.Data.Entities;
using HRManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRManagementSystem.Repositories.Implementations
{
    public class ContractRepository : IContractRepository
    {
        private readonly HRDbContext _context;
        private readonly string _connectionString;
        public ContractRepository(HRDbContext _context, IConfiguration configuration)
        {
            this._context = _context;
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("DefaultConnection string is not configured.");
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

        public async Task<int> CountExpiringContracts()
        {
            int count = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM Contracts where IsActive = 0 ";
                    var result = await command.ExecuteScalarAsync();
                    if (result != null) { count = Convert.ToInt16(result); }
                    ;
                    return count;
                }
            }
        }
    }
}
