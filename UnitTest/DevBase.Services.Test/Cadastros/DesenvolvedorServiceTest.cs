using AutoMapper;
using DevBase.Util.ExtensionMethods;
using DevBase.Domain.DTO.Cadastros;
using DevBase.Domain.Entidades.Cadastros;
using DevBase.Domain.Mapper.Cadastros;
using DevBase.Infra.Data.Interfaces.Repositorios.Cadastros;
using DevBase.Services.Cadastros;
using DevBase.Services.DTO;
using DevBase.Services.Test.Environments.Cadastros.DesenvolvedorEnvironments;
using DevBase.Services.Test.RepositorioMock.Cadastros.DesenvolvedorRepositorioMocks;
using DevBase.Services.Util.DtoValidator;
using FluentValidation;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevBase.Services.Test.Cadastros
{
    [TestFixture]
    public class DesenvolvedorServiceTest
    {
        private IDesenvolvedorRepositorio _desenvolvedorRepositorio;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _mapper = CriarMapper();
            _desenvolvedorRepositorio = new DesenvolvedorRepositorioMock();
            DesenvolvedorEnvironment.ConfigurarEnvironment(_desenvolvedorRepositorio);
        }

        private static Mapper CriarMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DesenvolvedorProfileMap>();
            });
            return new Mapper(mapperConfiguration);
        }

        [Test]
        public async Task Deve_retornar_todos_os_desenvolvedores()
        {
            var service = new DesenvolvedorService(_desenvolvedorRepositorio, _mapper, new DesenvolvedorDtoValidator());
            var desenvolvedores = await service.RetornarTodosDesenvolvedores();
            var data = desenvolvedores.Data;
            Assert.AreEqual(12, data.Count());
        }

        [Test]
        public async Task Deve_consultar_desenvolvedor_por_id()
        {
            var idValido = _desenvolvedorRepositorio.GetAll().Where(x => x.Id > 0).FirstOrDefault().Id;
            var service = new DesenvolvedorService(_desenvolvedorRepositorio, _mapper, new DesenvolvedorDtoValidator());
            var resposta = await service.ConsultarPorId(idValido);
            Assert.AreEqual(idValido, resposta.Data.Id);
            Assert.IsTrue(resposta.Sucesso);
        }

        [Test]
        public async Task Deve_falhar_ao_consultar_desenvolvedor_por_id_invalido()
        {
            var idInvalido = 0;
            var service = new DesenvolvedorService(_desenvolvedorRepositorio, _mapper, new DesenvolvedorDtoValidator());
            var resposta = await service.ConsultarPorId(idInvalido);
            Assert.IsFalse(resposta.Sucesso);
            Assert.AreEqual($"Nenhum registro encontrado para o id: {idInvalido}", resposta.Mensagem);
        }

        [Test]
        public void Deve_lancar_excecao_quando_tentar_criar_dev_com_sexo_invalido()
        {
            var novoDev = new DesenvolvedorDto
            {
                DataNascimento = new DateTime(1990, 01, 01),
                Hobby = string.Empty,
                Nome = "Novo Dev",
                Sexo = 'K'
            };

            var service = new DesenvolvedorService(_desenvolvedorRepositorio, _mapper, new DesenvolvedorDtoValidator());
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await service.CriarDesenvolvedor(novoDev));
            Assert.AreEqual(@"Validation failed: 
 -- Sexo: O valor informado para o sexo é inválido! Valores válidos: F para feminino, M para Masculino Severity: Error", ex.Message);
        }

        [Test]
        public void Deve_lancar_excecao_quando_tentar_criar_dev_informando_a_idade()
        {
            var novoDev = new DesenvolvedorDto
            {
                DataNascimento = new DateTime(1990, 01, 01),
                Hobby = string.Empty,
                Nome = "Novo Dev",
                Sexo = 'M',
                Idade = new DateTime(1990, 01, 01).CalcularIdade()
            };

            var service = new DesenvolvedorService(_desenvolvedorRepositorio, _mapper, new DesenvolvedorDtoValidator());
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await service.CriarDesenvolvedor(novoDev));
            Assert.AreEqual(@"Validation failed: 
 -- Idade: A idade não deve ser informada. O campo é gerado com base na data de nascimento. Severity: Error", ex.Message);
        }

        [Test]
        public void Deve_lancar_excecao_quando_tentar_criar_dev_sem_informar_data_de_nascimento()
        {
            var novoDev = new DesenvolvedorDto
            {
                Hobby = string.Empty,
                Nome = "Novo Dev",
                Sexo = 'M',
            };

            var service = new DesenvolvedorService(_desenvolvedorRepositorio, _mapper, new DesenvolvedorDtoValidator());
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await service.CriarDesenvolvedor(novoDev));
            Assert.AreEqual(@"Validation failed: 
 -- DataNascimento: A data de nascimento não foi informada! Severity: Error", ex.Message);
        }

        [Test]
        public void Deve_lancar_excecao_quando_tentar_criar_dev_sem_informar_o_nome()
        {
            var novoDev = new DesenvolvedorDto
            {
                DataNascimento = new DateTime(1990, 01, 01),
                Hobby = string.Empty,
                Sexo = 'M',
            };

            var service = new DesenvolvedorService(_desenvolvedorRepositorio, _mapper, new DesenvolvedorDtoValidator());
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await service.CriarDesenvolvedor(novoDev));
            Assert.AreEqual(@"Validation failed: 
 -- Nome: 'Nome' não pode ser nulo. Severity: Error
 -- Nome: O nome não foi informado! Severity: Error", ex.Message);
        }

        [Test]
        public async Task Deve_trazer_apenas_o_maximo_de_registros_permitido_quando_filtrado_paginado()
        {
            var filtro = new PaginationFilterDeveloperDto
            {
                NumeroDaPagina = 1,
                QuantidadeDeRegistros = 10
            };
            var service = new DesenvolvedorService(_desenvolvedorRepositorio, _mapper, new DesenvolvedorDtoValidator());
            var registros = await service.ListarPorFiltroPaginado(filtro);

            Assert.AreEqual(10, registros.Data.Count());
        }

        [Test]
        public async Task Deve_retornar_mensagem_nenhum_registro_encontrado_para_o_filtrado_informado()
        {
            DesenvolvedorEnvironment.ConfigurarEnvironment(_desenvolvedorRepositorio);
            var filtro = new PaginationFilterDeveloperDto
            {
                NumeroDaPagina = 1,
                QuantidadeDeRegistros = 10,
                Nome = "DEV NÃO EXISTE"
            };
            var service = new DesenvolvedorService(_desenvolvedorRepositorio, _mapper, new DesenvolvedorDtoValidator());
            var resposta = await service.ListarPorFiltroPaginado(filtro);
            Assert.IsFalse(resposta.Sucesso);
            Assert.AreEqual("Nenhum registro encontrado para o filtro informado", resposta.Mensagem);
        }

        [Test]
        public async Task Deve_criar_desenvolvedor()
        {
            var novoDev = new DesenvolvedorDto
            {
                DataNascimento = new DateTime(1990, 01, 01),
                Hobby = string.Empty,
                Nome = "Novo Dev",
                Sexo = 'M'
            };

            var resposta = await CriarDesenvolvedor(novoDev);
            Assert.AreEqual("Registro incluído com sucesso!", resposta.Mensagem);
            Assert.IsTrue(resposta.Sucesso);

            var devCriado = _desenvolvedorRepositorio.GetAll().Where(x => x.Nome == "Novo Dev").ToList();
            Assert.AreEqual(1, devCriado.Count());
        }

        [Test]
        public async Task Deve_alterar_desenvolvedor()
        {
            var novoDev = new DesenvolvedorDto
            {
                DataNascimento = new DateTime(1990, 01, 01),
                Hobby = string.Empty,
                Nome = "Novo Dev - Teste alteracao",
                Sexo = 'M'
            };

            var devCriado = await CriarDesenvolvedor(novoDev);
            novoDev.Nome = "Novo Dev Alterado";

            var service = new DesenvolvedorService(_desenvolvedorRepositorio, _mapper, new DesenvolvedorDtoValidator());
            var devAlterado = await service.AtualizarDesenvolvedor(devCriado.Data.Id, novoDev);
            Assert.AreEqual("Registro atualizado com sucesso", devAlterado.Mensagem);
            Assert.IsTrue(devAlterado.Sucesso);

            var devAlteradoEntity = _desenvolvedorRepositorio.GetAll().Where(x => x.Nome == "Novo Dev Alterado").ToList();
            Assert.AreEqual(1, devAlteradoEntity.Count());
        }

        [Test]
        public async Task Deve_falhar_ao_tentar_alterar_desenvolvedor_nao_existente_na_base()
        {
            var dev = new DesenvolvedorDto
            {
                DataNascimento = new DateTime(1990, 01, 01),
                Hobby = string.Empty,
                Nome = "Novo Dev - Teste alteracao",
                Sexo = 'M'
            };

            var idInvalido = 0;
            var service = new DesenvolvedorService(_desenvolvedorRepositorio, _mapper, new DesenvolvedorDtoValidator());
            var resposta = await service.AtualizarDesenvolvedor(idInvalido, dev);

            Assert.AreEqual($"Nenhum registro encontrado para o id: {idInvalido}", resposta.Mensagem);
            Assert.IsFalse(resposta.Sucesso);
        }

        [Test]
        public async Task Deve_excluir_desenvolvedor()
        {
            var novoDev = new DesenvolvedorDto
            {
                DataNascimento = new DateTime(1990, 01, 01),
                Hobby = string.Empty,
                Nome = "Novo Dev - Teste exclusão",
                Sexo = 'M'
            };

            var devCriado = await CriarDesenvolvedor(novoDev);
            var service = new DesenvolvedorService(_desenvolvedorRepositorio, _mapper, new DesenvolvedorDtoValidator());
            var resposta = await service.ExcluirDesenvolvedor(devCriado.Data.Id);

            Assert.AreEqual($"Registro de id {devCriado.Data.Id} excluido com sucesso", resposta.Mensagem);
            Assert.IsTrue(resposta.Sucesso);
        }

        [Test]
        public async Task Deve_falhar_ao_tentar_excluir_desenvolvedor_nao_existente_na_base()
        {
            var idInvalido = 0;
            var service = new DesenvolvedorService(_desenvolvedorRepositorio, _mapper, new DesenvolvedorDtoValidator());
            var resposta = await service.ExcluirDesenvolvedor(idInvalido);

            Assert.AreEqual($"Nenhum registro encontrado para o id: {idInvalido}", resposta.Mensagem);
            Assert.IsFalse(resposta.Sucesso);
        }

        private async Task<ResponseDto<Desenvolvedor>> CriarDesenvolvedor(DesenvolvedorDto novoDev)
        {
            var service = new DesenvolvedorService(_desenvolvedorRepositorio, _mapper, new DesenvolvedorDtoValidator());
            return await service.CriarDesenvolvedor(novoDev);

        }
    }
}
