

using API.Business.Interfaces;
using API.Business.Model;
using Microsoft.EntityFrameworkCore;


namespace API.Service.Services
{
    public class ClienteService : IClienteService
    {
        private readonly APIDbContext _dbContext;

        public ClienteService(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Add(Cliente cliente)
        {
            try
            {
                _dbContext.Clientes.Add(cliente);
                await _dbContext.SaveChangesAsync();

                return cliente.Id > 0 ? true: false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<List<Cliente>> GetAll()
        {
            var clientes = await _dbContext.Clientes.ToListAsync();
            return clientes.OrderByDescending(x => x.Nome).ToList();
        }

        public async Task<Cliente> Get(int id)
        {
            return await _dbContext.Clientes.FindAsync(id);
        }

        public async Task<bool> Update(Cliente cliente)
        {
            _dbContext.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var cliente = await _dbContext.Clientes.FindAsync(id);
            if (cliente == null) return false;

            try
            {
                _dbContext.Clientes.Remove(cliente);
                await _dbContext.SaveChangesAsync();

                return (await _dbContext.Clientes.FindAsync(id)) == null? true: false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
    }
}
