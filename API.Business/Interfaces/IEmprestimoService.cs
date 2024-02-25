using API.Business.Model;

namespace API.Business.Interfaces
{
    public interface IEmprestimoService
    {
        public Task<List<Emprestimo>> GetAll();
        public Task<Result> Add(Emprestimo emprestimo);
        public Task<Emprestimo> Get(int id);
        public Task<bool> Update(Emprestimo emprestimo);
        public Task<bool> Delete(int id);
    }
}
