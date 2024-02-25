using System.ComponentModel.DataAnnotations;

namespace API.Business.Model
{
    public class Estado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sigla { get; set; }

        public ICollection<Cliente> Clientes { get; set; }
    }
}
