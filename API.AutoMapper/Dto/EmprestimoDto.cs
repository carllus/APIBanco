namespace API.AutoMapper.Dto
{
    public class EmprestimoDto
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public DateTime PrimeiroVencimento { get; set; }
        public int ClienteId { get; set; }
        public int TipoEmprestimoId { get; set; }
        public float ValorSolicitado { get; set; }
        public int QtParcelas { get; set; }
    }
}
