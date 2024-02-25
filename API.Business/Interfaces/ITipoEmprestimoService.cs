using API.Business.Model;

namespace API.Business.Interfaces
{
    public interface ITipoEmprestimoService
    {
        public Task<List<TipoEmprestimo>> GetAll();
        public Task<bool> Add(TipoEmprestimo tipoEmprestimo);
        public Task<TipoEmprestimo> Get(int id);
        public Task<bool> Update(TipoEmprestimo tipoEmprestimo);
        public Task<bool> Delete(int id);
    }
}
