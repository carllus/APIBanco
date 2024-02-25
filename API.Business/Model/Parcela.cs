using System.ComponentModel.DataAnnotations;

namespace API.Business.Model
{
    public class Parcela
    {
        public Parcela(int EmprestimoId, decimal ValorParcela, DateTime Vencimento)
        {
            Id = 0;
            this.EmprestimoId = EmprestimoId;
            this.ValorParcela = ValorParcela;
            this.Vencimento = Vencimento;
            DataPagamento = null;
            IsPago = false;
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmprestimoId { get; set; }
        public Emprestimo Emprestimo { get; set; }

        [Required]
        public decimal ValorParcela { get; set; }

        [Required]
        public DateTime Vencimento { get; set; }

        [Required]
        public bool IsPago { get; set; }

        public DateTime? DataPagamento { get; set; }
    }
}
