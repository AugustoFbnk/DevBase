using AutoMapper;
using DevBase.Util.ExtensionMethods;
using DevBase.Domain.DTO.Cadastros;
using DevBase.Domain.Entidades.Cadastros;
using DevBase.Infra.Data.Interfaces.Repositorios.Cadastros;
using DevBase.Services.DTO;
using DevBase.Services.Interfaces.Cadastros;
using DevBase.Util.ExtensionMethods;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevBase.Services.Cadastros
{
    public class DesenvolvedorService : IDesenvolvedorService
    {
        private readonly IMapper _mapper;
        private readonly IDesenvolvedorRepositorio _desenvolvedorRepositorio;
        private readonly IValidator<DesenvolvedorDto> _desenvolvedorValidator;

        public DesenvolvedorService(IDesenvolvedorRepositorio desenvolvedorRepositorio, IMapper mapper, IValidator<DesenvolvedorDto> desenvolvedorValidator)
        {
            _desenvolvedorRepositorio = desenvolvedorRepositorio;
            _mapper = mapper;
            _desenvolvedorValidator = desenvolvedorValidator;
        }

        public async Task<ResponseDto<IEnumerable<Desenvolvedor>>> RetornarTodosDesenvolvedores()
        {
            var devs = await _desenvolvedorRepositorio.GetAll().ToListAsyncSafe();
            return GenericResponses<IEnumerable<Desenvolvedor>>.RetornarResponseSucesso(devs);
        }

        public async Task<ResponseDto<Desenvolvedor>> CriarDesenvolvedor(DesenvolvedorDto desenvolvedorDto)
        {
            _desenvolvedorValidator.ValidateAndThrow(desenvolvedorDto);

            var desenvolvedor = _mapper.Map<DesenvolvedorDto, Desenvolvedor>(desenvolvedorDto);

            await _desenvolvedorRepositorio.Create(desenvolvedor);
            return GenericResponses<Desenvolvedor>.RetornarRegistroIncluidoComSucesso(desenvolvedor);
        }

        public async Task<PagedResponseDto<IEnumerable<Desenvolvedor>>> ListarPorFiltroPaginado(PaginationFilterDeveloperDto filtro)
        {
            var quantidadeDeRegistros = filtro.QuantidadeDeRegistros.TryToInt();
            var numeroDaPagina = filtro.NumeroDaPagina.TryToInt();

            var devs = _desenvolvedorRepositorio.GetByFiltro(filtro.Idade, filtro.Sexo, filtro.Hobby, filtro.Nome);

            var totalRegistros = devs.Count();

            var listaPaginada = await PaginarLista(quantidadeDeRegistros, numeroDaPagina, devs);

            if (!listaPaginada.Any())
                return GenericResponses<IEnumerable<Desenvolvedor>>.RetornarNenhuRegistroEncontradoParaOFiltro();

            return GenericResponses<IEnumerable<Desenvolvedor>>.RetornarRegistroEncontrado(listaPaginada, numeroDaPagina, quantidadeDeRegistros, totalRegistros);
        }

        private Task<List<Desenvolvedor>> PaginarLista(int quantidadeDeRegistros, int numeroDaPagina, IQueryable<Desenvolvedor> devs)
        {
            return devs.Skip((numeroDaPagina - 1) * quantidadeDeRegistros)
                       .Take(quantidadeDeRegistros)
                       .ToListAsyncSafe();
        }

        public async Task<ResponseDto<Desenvolvedor>> ConsultarPorId(int id)
        {
            var entidade = await _desenvolvedorRepositorio.GetById(id);
            if (entidade == null)
                return GenericResponses<Desenvolvedor>.RetornarRegistroNaoEncontradoParaId(id);

            return GenericResponses<Desenvolvedor>.RetornarRegistroEncontrado(entidade);

        }

        public async Task<ResponseDto<Desenvolvedor>> AtualizarDesenvolvedor(int id, DesenvolvedorDto desenvolvedorDto)
        {
            _desenvolvedorValidator.ValidateAndThrow(desenvolvedorDto);

            var entidade = await _desenvolvedorRepositorio.GetById(id);
            if (entidade == null)
                return GenericResponses<Desenvolvedor>.RetornarRegistroNaoEncontradoParaId(id);

            await AtualizarDesenvolvedor(desenvolvedorDto, entidade);

            return GenericResponses<Desenvolvedor>.RetornarRegistroAtualizadoComSucesso(entidade);

        }

        private async Task AtualizarDesenvolvedor(DesenvolvedorDto desenvolvedor, Desenvolvedor entidade)
        {
            entidade.Hobby = desenvolvedor.Hobby;
            entidade.DataNascimento = desenvolvedor.DataNascimento;
            entidade.Idade = desenvolvedor.DataNascimento.CalcularIdade();
            entidade.Nome = desenvolvedor.Nome;
            entidade.Sexo = desenvolvedor.Sexo.ToCharEnum<Sexo>();
            await _desenvolvedorRepositorio.Update(entidade);
        }

        public async Task<ResponseDto<Desenvolvedor>> ExcluirDesenvolvedor(int id)
        {
            var entidade = await _desenvolvedorRepositorio.GetById(id);
            if (entidade == null)
                return GenericResponses<Desenvolvedor>.RetornarRegistroNaoEncontradoParaId(id);

            await _desenvolvedorRepositorio.Delete(entidade);
            return GenericResponses<Desenvolvedor>.RetornarRegistroExcluidoComSucesso(id);
        }
    }
}
