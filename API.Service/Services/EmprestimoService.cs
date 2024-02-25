

using API.Business.Interfaces;
using API.Business.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Drawing;


namespace API.Service.Services
{
    public class EmprestimoService : IEmprestimoService
    {
        private readonly APIDbContext _dbContext;

        public EmprestimoService(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Add(Emprestimo emprestimo)
        {
            try
            {
                var cliente = await _dbContext.Clientes.FindAsync(emprestimo.ClienteId);
                if (cliente == null)
                    return new Result(false, "Cliente não encontrado", null);
                var tipoEmprestimo = await _dbContext.TipoEmprestimos.FindAsync(emprestimo.TipoEmprestimoId);
                if (tipoEmprestimo == null) 
                    return new Result(false, "Tipo Emprestimo não encontrado", null);


                if (!ValidaValorSolicitado(emprestimo.ValorSolicitado)) 
                    return new Result(false, "RECUSADO: Valor solicitado acima do máximo permitido", "Empréstimo máximo de R$1000000,00");
                if (!ValidaParcelaMinimas(emprestimo.QtParcelas)) 
                    return new Result(false, "RECUSADO: Quantidade de parcelas inválido", "A quantidade de parcelas desejadas deve estar em 5 e 72 parcelas");
                if(!ValidaPrimeiroVencimento(emprestimo.PrimeiroVencimento))
                    return new Result(false, "RECUSADO: Primeiro vencimento inválido", "A data do primeiro vencimento deve ter pelo menos 15 dias e no máximo 40 dias a partir da data atual");
                if ((cliente.IsPJ && !tipoEmprestimo.ParaPJ) || (!cliente.IsPJ && tipoEmprestimo.ParaPJ))
                    return new Result(false, "RECUSADO: Cliente PF ou PJ deve solicitar empréstimo para seu perfil", "Cliente PJ nao pode solicitar empréstimo PF ou PF solicitar emprestimo de PJ");
                if (cliente.IsPJ && emprestimo.ValorSolicitado<15000)
                    return new Result(false, "RECUSADO: Abaixo do valor mínimo para cliente PJ", "Cliente PJ devem solicitar pelo menos R$15000,00");

                _dbContext.Emprestimos.Add(emprestimo);
                await _dbContext.SaveChangesAsync();

                List<Parcela> parcelas;
                if (emprestimo.Id > 0)
                {
                    parcelas = GerarParcelasComJurosCompostosMensais(emprestimo.Id, emprestimo.PrimeiroVencimento, emprestimo.ValorSolicitado, emprestimo.QtParcelas, tipoEmprestimo.JurosMensal/100);
                    _dbContext.Parcelas.AddRange(parcelas);
                    await _dbContext.SaveChangesAsync();
                }
                else
                    return new Result(true, "ERRO de sistema!", null);

                return emprestimo.Id > 0 ? 
                    new Result(true, "APROVADO! Valor total com juros: " + parcelas.Sum(x => x.ValorParcela) + " Valor dos juros: " + (parcelas.Sum(x => x.ValorParcela) - emprestimo.ValorSolicitado), "Parcela 1: "+parcelas.First().ValorParcela+" Demais parcelas: "+parcelas.Last().ValorParcela):
                    new Result(false, "ERRO: Não foi possível inserir", null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<List<Emprestimo>> GetAll()
        {
            var emprestimos = await _dbContext.Emprestimos.ToListAsync();
            return emprestimos.OrderByDescending(x => x.DataHora).ToList();
        }

        public async Task<Emprestimo> Get(int id)
        {
            return await _dbContext.Emprestimos.FindAsync(id);
        }

        public async Task<bool> Update(Emprestimo emprestimo)
        {
            _dbContext.Entry(emprestimo).State = EntityState.Modified;

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
            var emprestimo = await _dbContext.Emprestimos.FindAsync(id);
            if (emprestimo == null) return false;

            try
            {
                _dbContext.Emprestimos.Remove(emprestimo);
                await _dbContext.SaveChangesAsync();

                return (await _dbContext.Emprestimos.FindAsync(id)) == null ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        private bool ValidaPrimeiroVencimento(DateTime dataRecebida)
        {
            TimeSpan diferenca = dataRecebida - DateTime.Today;

            if (diferenca.TotalDays >= 15 && diferenca.TotalDays <= 40)
                return true;
            else
                return false;

        }
        private bool ValidaParcelaMinimas(int qtParcelas)
        {
            if (qtParcelas>=5 && qtParcelas<=72)
                return true;
            else
                return false;
        }
        private bool ValidaValorSolicitado(decimal valorSolicitado)
        {
            if (valorSolicitado <= 1000000)
                return true;
            else
                return false;
        }
        
        public List<Parcela> GerarParcelasComJurosCompostosMensais(int emprestimoId, DateTime dataInicial, decimal valorEmprestimo, int numeroParcelas, decimal taxaJurosMensal)
        {
            var parcelas = new List<Parcela>();

            decimal valorParcelaTemp = CalcularValorParcela(valorEmprestimo, numeroParcelas, taxaJurosMensal);
            var diasParaPrimeiraParcela = (dataInicial.Date - DateTime.Today.Date).Days;
            var primeiraParcela = (valorParcelaTemp / 30) * diasParaPrimeiraParcela;
            parcelas.Add(new Parcela(emprestimoId, primeiraParcela, dataInicial));

            numeroParcelas--;
            decimal valorParcela = CalcularValorParcela(valorEmprestimo-primeiraParcela, numeroParcelas, taxaJurosMensal);
            var vencimento = dataInicial.Date.AddMonths(1);
            for (int i = 0; i < numeroParcelas; i++)
            {
                parcelas.Add(new Parcela(emprestimoId, valorParcela, vencimento));
            }
            
            return parcelas;
        }

        private decimal CalcularValorParcela(decimal valorEmprestimo, int numeroParcelas, decimal taxaJurosMensal)
        {
            decimal valorParcela = valorEmprestimo * (decimal)Math.Pow((double)(1 + taxaJurosMensal), numeroParcelas) * (taxaJurosMensal) / ((decimal)Math.Pow((double)(1 + taxaJurosMensal), numeroParcelas) - 1);
            return valorParcela;
        }

    }
}
