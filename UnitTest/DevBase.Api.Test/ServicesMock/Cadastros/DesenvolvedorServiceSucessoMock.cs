using DevBase.Util.ExtensionMethods;
using DevBase.Domain.DTO.Cadastros;
using DevBase.Domain.Entidades.Cadastros;
using DevBase.Services;
using DevBase.Services.DTO;
using DevBase.Services.Interfaces.Cadastros;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBase.Api.Test.ServicesMock.Cadastros
{
    public class DesenvolvedorServiceSucessoMock : IDesenvolvedorService
    {
        public async Task<ResponseDto<Desenvolvedor>> AtualizarDesenvolvedor(int id, DesenvolvedorDto desenvolvedor)
        {
            return GenericResponses<Desenvolvedor>.RetornarRegistroAtualizadoComSucesso(new Desenvolvedor());
        }

        public async Task<ResponseDto<Desenvolvedor>> ConsultarPorId(int id)
        {
            return GenericResponses<Desenvolvedor>.RetornarRegistroEncontrado(new Desenvolvedor());
        }

        public async Task<ResponseDto<Desenvolvedor>> CriarDesenvolvedor(DesenvolvedorDto desenvolvedor)
        {
            var dev = new Desenvolvedor
            {
                Id = 1,
                Nome = desenvolvedor.Nome,
                DataNascimento = desenvolvedor.DataNascimento,
                Hobby = desenvolvedor.Hobby,
                Idade = desenvolvedor.DataNascimento.CalcularIdade(),
                Sexo = desenvolvedor.Sexo.ToCharEnum<Sexo>()
            };
            return GenericResponses<Desenvolvedor>.RetornarRegistroIncluidoComSucesso(dev);
        }

        public async Task<ResponseDto<Desenvolvedor>> ExcluirDesenvolvedor(int id)
        {
            return GenericResponses<Desenvolvedor>.RetornarRegistroExcluidoComSucesso(id);
        }

        public Task<PagedResponseDto<IEnumerable<Desenvolvedor>>> ListarPorFiltroPaginado(PaginationFilterDeveloperDto filtro)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseDto<IEnumerable<Desenvolvedor>>> RetornarTodosDesenvolvedores()
        {
            return GenericResponses<IEnumerable<Desenvolvedor>>.RetornarResponseSucesso(new List<Desenvolvedor>());
        }
    }
}
