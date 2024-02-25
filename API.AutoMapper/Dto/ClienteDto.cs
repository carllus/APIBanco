using API.Business.Model;
using System.ComponentModel.DataAnnotations;

namespace API.AutoMapper.Dto
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int EstadoId { get; set; }
        public bool IsPJ { get; set; }
        public string CadNacional { get; set; }
    }
}
