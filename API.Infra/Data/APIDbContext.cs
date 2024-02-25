using Microsoft.EntityFrameworkCore;

namespace API.Business.Model
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options): base(options) { 
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<TipoEmprestimo> TipoEmprestimos { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estado>().HasData(
            new Estado { Id = 1, Nome = "Acre", Sigla = "AC" },
            new Estado { Id = 2, Nome = "Alagoas", Sigla = "AL" },
            new Estado { Id = 3, Nome = "Amapá", Sigla = "AP" },
            new Estado { Id = 4, Nome = "Amazonas", Sigla = "AM" },
            new Estado { Id = 5, Nome = "Bahia", Sigla = "BA" },
            new Estado { Id = 6, Nome = "Ceará", Sigla = "CE" },
            new Estado { Id = 7, Nome = "Distrito Federal", Sigla = "DF" },
            new Estado { Id = 8, Nome = "Espírito Santo", Sigla = "ES" },
            new Estado { Id = 9, Nome = "Goiás", Sigla = "GO" },
            new Estado { Id = 10, Nome = "Maranhão", Sigla = "MA" },
            new Estado { Id = 11, Nome = "Mato Grosso", Sigla = "MT" },
            new Estado { Id = 12, Nome = "Mato Grosso do Sul", Sigla = "MS" },
            new Estado { Id = 13, Nome = "Minas Gerais", Sigla = "MG" },
            new Estado { Id = 14, Nome = "Pará", Sigla = "PA" },
            new Estado { Id = 15, Nome = "Paraíba", Sigla = "PB" },
            new Estado { Id = 16, Nome = "Paraná", Sigla = "PR" },
            new Estado { Id = 17, Nome = "Pernambuco", Sigla = "PE" },
            new Estado { Id = 18, Nome = "Piauí", Sigla = "PI" },
            new Estado { Id = 19, Nome = "Rio de Janeiro", Sigla = "RJ" },
            new Estado { Id = 20, Nome = "Rio Grande do Norte", Sigla = "RN" },
            new Estado { Id = 21, Nome = "Rio Grande do Sul", Sigla = "RS" },
            new Estado { Id = 22, Nome = "Rondônia", Sigla = "RO" },
            new Estado { Id = 23, Nome = "Roraima", Sigla = "RR" },
            new Estado { Id = 24, Nome = "Santa Catarina", Sigla = "SC" },
            new Estado { Id = 25, Nome = "São Paulo", Sigla = "SP" },
            new Estado { Id = 26, Nome = "Sergipe", Sigla = "SE" },
            new Estado { Id = 27, Nome = "Tocantins", Sigla = "TO" }
            );

            modelBuilder.Entity<TipoEmprestimo>().HasData(
            new TipoEmprestimo { Id = 1, Nome = "Crédito Direto", JurosMensal = 2, ParaPJ = false },
            new TipoEmprestimo { Id = 2, Nome = "Crédito Consignado", JurosMensal = 1, ParaPJ = false },
            new TipoEmprestimo { Id = 3, Nome = "Crédito Pessoa Jurídica", JurosMensal = 5, ParaPJ = true },
            new TipoEmprestimo { Id = 4, Nome = "Crédito Pessoa Física", JurosMensal = 3, ParaPJ = false },
            new TipoEmprestimo { Id = 5, Nome = "Crédito Imobiliário", JurosMensal = 9, ParaPJ = false }
            );
        }
    } 
}
