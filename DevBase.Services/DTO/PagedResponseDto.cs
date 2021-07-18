namespace DevBase.Services.DTO
{
    public class PagedResponseDto<T> : ResponseDto<T>
    {
        public int? NumeroDaPagina { get; set; }
        public int? QuantidadeRegistrosPorPagina { get; set; }
        public int? TotalRegistros { get; set; }
        public PagedResponseDto()
        {

        }

        public PagedResponseDto(T data)
        {
            Data = data;
        }

        public PagedResponseDto(T data, int? numeroDaPagina, int? quantidadeRegistrosPorPagina, int? totalRegistros)
        {
            NumeroDaPagina = numeroDaPagina;
            QuantidadeRegistrosPorPagina = quantidadeRegistrosPorPagina;
            TotalRegistros = totalRegistros;
            Data = data;
            Mensagem = null;
            Sucesso = true;
            Erros = null;
        }
    }
}
