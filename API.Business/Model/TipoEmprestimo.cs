using System.ComponentModel.DataAnnotations;

namespace API.Business.Model
{
    public class TipoEmprestimo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome Obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Prencha pelo menos 3 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Taxa de Juros Mensal Obrigatória")]
        public decimal JurosMensal { get; set; }

        [Required(ErrorMessage = "Informe se é exclusivo para PJ")]
        public bool ParaPJ { get; set; }

        public ICollection<Emprestimo> Emprestimos { get; set; }
    } 
}
