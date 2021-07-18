using DevBase.Services.DTO;

namespace DevBase.Services
{
    public static class GenericResponses<T>
    {
        public static ResponseDto<T> RetornarRegistroNaoEncontradoParaId(int id, string mensagem = null)
        {
            return new ResponseDto<T>()
            {
                Mensagem = mensagem ?? $"Nenhum registro encontrado para o id: {id}",
                Sucesso = false
            };
        }

        public static ResponseDto<T> RetornarRegistroAtualizadoComSucesso(T data, string mensagem = null)
        {
            return new ResponseDto<T>(data)
            {
                Mensagem = mensagem ?? $"Registro atualizado com sucesso",
                Sucesso = true
            };
        }

        public static ResponseDto<T> RetornarRegistroExcluidoComSucesso(int id, string mensagem = null)
        {
            return new ResponseDto<T>
            {
                Mensagem = mensagem ?? $"Registro de id {id} excluido com sucesso",
                Sucesso = true
            };
        }

        public static ResponseDto<T> RetornarResponseSucesso(T data, string mensagem = null)
        {
            return new ResponseDto<T>(data)
            {
                Mensagem = mensagem,
                Sucesso = true
            };
        }

        public static ResponseDto<T> RetornarRegistroIncluidoComSucesso(T data, string mensagem = null)
        {
            return new ResponseDto<T>(data)
            {
                Mensagem = mensagem ?? "Registro incluído com sucesso!",
                Sucesso = true
            };
        }

        public static PagedResponseDto<T> RetornarRegistroEncontrado(T data, string mensagem = null)
        {
            return new PagedResponseDto<T>(data)
            {
                Mensagem = mensagem,
                Sucesso = true
            };
        }

        public static PagedResponseDto<T> RetornarRegistroEncontrado(T data, int? numeroDaPagina, int? quantidadeRegistrosPorPagina, int? totalRegistros, string mensagem = null)
        {
            return new PagedResponseDto<T>(data, numeroDaPagina, quantidadeRegistrosPorPagina, totalRegistros)
            {
                Mensagem = mensagem,
                Sucesso = true
            };
        }

        public static PagedResponseDto<T> RetornarNenhuRegistroEncontradoParaOFiltro(string mensagem = null)
        {
            return new PagedResponseDto<T>
            {
                Mensagem = mensagem ?? "Nenhum registro encontrado para o filtro informado",
                Sucesso = false
            };
        }
    }
}
