using DevBase.Domain.DTO.Cadastros;
using DevBase.Domain.Entidades.Cadastros;
using DevBase.Services;
using DevBase.Services.DTO;
using DevBase.Services.Interfaces.Cadastros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBase.Api.Test.ServicesMock.Cadastros
{
    public class DesenvolvedorServiceFalhaMock : IDesenvolvedorService
    {
        public async Task<ResponseDto<Desenvolvedor>> AtualizarDesenvolvedor(int id, DesenvolvedorDto desenvolvedor)
        {
            return GenericResponses<Desenvolvedor>.RetornarRegistroNaoEncontradoParaId(id);
        }

        public async Task<ResponseDto<Desenvolvedor>> ConsultarPorId(int id)
        {
            return GenericResponses<Desenvolvedor>.RetornarRegistroNaoEncontradoParaId(id);
        }

        public async Task<ResponseDto<Desenvolvedor>> CriarDesenvolvedor(DesenvolvedorDto desenvolvedor)
        {
            return new ResponseDto<Desenvolvedor>
            {
                Sucesso = false
            };
        }

        public async Task<ResponseDto<Desenvolvedor>> ExcluirDesenvolvedor(int id)
        {
            return GenericResponses<Desenvolvedor>.RetornarRegistroNaoEncontradoParaId(id);
        }

        public async Task<PagedResponseDto<IEnumerable<Desenvolvedor>>> ListarPorFiltroPaginado(PaginationFilterDeveloperDto filtro)
        {
            return GenericResponses<IEnumerable<Desenvolvedor>>.RetornarNenhuRegistroEncontradoParaOFiltro();
        }

        public Task<ResponseDto<IEnumerable<Desenvolvedor>>> RetornarTodosDesenvolvedores()
        {
            throw new NotImplementedException();
        }
    }
}
