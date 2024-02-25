using System.ComponentModel.DataAnnotations;

namespace API.Business.Model
{
    public class Cliente
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Nome Obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Prencha pelo menos 3 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Estado Obrigatório")]
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }

        public bool IsPJ { get; set; }

        [Required(ErrorMessage = "CPF ou CNPJ obrigatório")]
        [StringLength(18, MinimumLength = 11, ErrorMessage = "Preencha com CPF ou CNPJ do Cliente")]
        public string CadNacional { get; set; }

        public ICollection<Emprestimo> Emprestimo { get; set; }
    } 
}
