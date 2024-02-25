using API.Business.Model;
using System.ComponentModel.DataAnnotations;

namespace API.AutoMapper.Dto
{
    public class TipoEmprestimoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public float JurosMensal { get; set; }
        public bool ParaPJ { get; set; }
    }
}
