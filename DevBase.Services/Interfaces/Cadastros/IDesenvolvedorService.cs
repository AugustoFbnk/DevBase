using DevBase.Domain.DTO.Cadastros;
using DevBase.Domain.Entidades.Cadastros;
using DevBase.Services.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBase.Services.Interfaces.Cadastros
{
    public interface IDesenvolvedorService
    {
        Task<ResponseDto<IEnumerable<Desenvolvedor>>> RetornarTodosDesenvolvedores();

        Task<ResponseDto<Desenvolvedor>> CriarDesenvolvedor(DesenvolvedorDto desenvolvedor);

        Task<PagedResponseDto<IEnumerable<Desenvolvedor>>> ListarPorFiltroPaginado(PaginationFilterDeveloperDto filtro);

        Task<ResponseDto<Desenvolvedor>> ConsultarPorId(int id);

        Task<ResponseDto<Desenvolvedor>> AtualizarDesenvolvedor(int id, DesenvolvedorDto desenvolvedor);

        Task<ResponseDto<Desenvolvedor>> ExcluirDesenvolvedor(int id);
    }
}
