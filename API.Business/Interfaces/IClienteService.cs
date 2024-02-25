using API.Business.Model;

namespace API.Business.Interfaces
{
    public interface IClienteService
    {
        public Task<List<Cliente>> GetAll();
        public Task<bool> Add(Cliente cliente);
        public Task<Cliente> Get(int id);
        public Task<bool> Update(Cliente cliente);
        public Task<bool> Delete(int id);
    }
}
