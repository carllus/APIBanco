using System.ComponentModel.DataAnnotations;

namespace API.Business.Model
{
    public class Emprestimo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataHora { get; set; }
        [Required]
        public DateTime PrimeiroVencimento { get; set; }

        [Required(ErrorMessage = "Informe o cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Informe o Tipo de Empréstimo")]
        public int TipoEmprestimoId { get; set; }
        public TipoEmprestimo TipoEmprestimo { get; set; }

        [Required(ErrorMessage = "Informe o valor solicitado")]
        public decimal ValorSolicitado { get; set; }
        [Required]
        public int QtParcelas { get; set; }

        public ICollection<Parcela> Parcelas { get; set; }
    }
}
