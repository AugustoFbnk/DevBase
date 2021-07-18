using AutoMapper;
using DevBase.Api.Controllers.Cadastros;
using DevBase.Api.Test.ServicesMock.Cadastros;
using DevBase.Domain.DTO.Cadastros;
using DevBase.Domain.Mapper.Cadastros;
using DevBase.Services.DTO;
using DevBase.Services.Interfaces.Cadastros;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DevBase.Api.Test.Controllers.Cadastros
{
    [TestFixture]
    public class DevelopersControllerTest
    {

        private IMapper _mapper;
        private IDesenvolvedorService _desenvolvedorServiceSucesso;
        private IDesenvolvedorService _desenvolvedorServiceFalha;

        [SetUp]
        public void SetUp()
        {
            _mapper = CriarMapper();
            _desenvolvedorServiceSucesso = new DesenvolvedorServiceSucessoMock();
            _desenvolvedorServiceFalha = new DesenvolvedorServiceFalhaMock();
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
        public void Deve_retornar_201_created_ao_tentar_inserir()
        {
            var controller = new DevelopersController(_desenvolvedorServiceSucesso, _mapper);
            var resposta = controller.Inserir(new DesenvolvedorDto()).Result as CreatedAtActionResult;

            Assert.AreEqual(201, resposta.StatusCode);
        }

        [Test]
        public async Task Deve_retornar_400_bad_request_ao_tentar_inserir()
        {
            var controller = new DevelopersController(_desenvolvedorServiceFalha, _mapper);
            var resposta = await controller.Inserir(new DesenvolvedorDto());
            var badRequest = resposta as BadRequestObjectResult;

            Assert.AreEqual(400, badRequest.StatusCode);
        }

        [Test]
        public void Deve_retornar_200_ok_ao_tentar_consultar()
        {
            var controller = new DevelopersController(_desenvolvedorServiceSucesso, _mapper);
            var resposta = controller.Consultar(new PaginationFilterDeveloperDto()).Result.Result as OkObjectResult;

            Assert.AreEqual(200, resposta.StatusCode);
        }

        [Test]
        public void Deve_retornar_404_not_found_ao_tentar_consultar()
        {
            var controller = new DevelopersController(_desenvolvedorServiceFalha, _mapper);
            var filtro = new PaginationFilterDeveloperDto
            {
                NumeroDaPagina = 1,
                QuantidadeDeRegistros = 10
            };

            var resposta = controller.Consultar(filtro).Result.Result as NotFoundObjectResult;

            Assert.AreEqual(404, resposta.StatusCode);
        }

        [Test]
        public void Deve_retornar_200_ok_ao_tentar_atualizar()
        {
            var controller = new DevelopersController(_desenvolvedorServiceSucesso, _mapper);
            var resposta = controller.Atualizar(1, new DesenvolvedorDto()).Result.Result as OkObjectResult;

            Assert.AreEqual(200, resposta.StatusCode);
        }

        [Test]
        public void Deve_retornar_404_not_found_ao_tentar_atualizar()
        {
            var controller = new DevelopersController(_desenvolvedorServiceFalha, _mapper);

            var resposta = controller.Atualizar(1, new DesenvolvedorDto()).Result.Result;
            var notFound = resposta as NotFoundObjectResult;

            Assert.AreEqual(404, notFound.StatusCode);
        }

        [Test]
        public void Deve_retornar_204_no_content_ao_tentar_excluir()
        {
            var controller = new DevelopersController(_desenvolvedorServiceSucesso, _mapper);

            var resposta = controller.Excluir(1).Result.Result;
            var noContent = resposta as NoContentResult;

            Assert.AreEqual(204, noContent.StatusCode);
        }

        [Test]
        public void Deve_retornar_400_bad_request_ao_tentar_excluir()
        {
            var controller = new DevelopersController(_desenvolvedorServiceFalha, _mapper);

            var resposta = controller.Excluir(1).Result.Result;
            var badRequest = resposta as BadRequestObjectResult;

            Assert.AreEqual(400, badRequest.StatusCode);
        }

        [Test]
        public void Deve_retornar_200_ok_ao_consultar_por_id()
        {
            var controller = new DevelopersController(_desenvolvedorServiceSucesso, _mapper);
            var resposta = controller.ConsultarPorId(1).Result.Result as OkObjectResult;

            Assert.AreEqual(200, resposta.StatusCode);
        }

        [Test]
        public void Deve_retornar_404_not_found_ao_consultar_por_id()
        {
            var controller = new DevelopersController(_desenvolvedorServiceFalha, _mapper);

            var resposta = controller.Atualizar(1, new DesenvolvedorDto()).Result.Result;
            var notFound = resposta as NotFoundObjectResult;

            Assert.AreEqual(404, notFound.StatusCode);
        }
    }
}
