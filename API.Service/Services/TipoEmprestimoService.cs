

using API.Business.Interfaces;
using API.Business.Model;
using Microsoft.EntityFrameworkCore;


namespace API.Service.Services
{
    public class TipoEmprestimoService : ITipoEmprestimoService
    {
        private readonly APIDbContext _dbContext;

        public TipoEmprestimoService(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Add(TipoEmprestimo tipoEmprestimo)
        {
            try
            {
                _dbContext.TipoEmprestimos.Add(tipoEmprestimo);
                await _dbContext.SaveChangesAsync();

                return tipoEmprestimo.Id > 0 ? true: false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<List<TipoEmprestimo>> GetAll()
        {
            var tipoEmprestimos = await _dbContext.TipoEmprestimos.ToListAsync();
            return tipoEmprestimos.ToList();
        }

        public async Task<TipoEmprestimo> Get(int id)
        {
            return await _dbContext.TipoEmprestimos.FindAsync(id);
        }

        public async Task<bool> Update(TipoEmprestimo tipoEmprestimo)
        {
            _dbContext.Entry(tipoEmprestimo).State = EntityState.Modified;

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
            var tipoEmprestimo = await _dbContext.TipoEmprestimos.FindAsync(id);
            if (tipoEmprestimo == null) return false;

            try
            {
                _dbContext.TipoEmprestimos.Remove(tipoEmprestimo);
                await _dbContext.SaveChangesAsync();

                return (await _dbContext.TipoEmprestimos.FindAsync(id)) == null? true: false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
    }
}
