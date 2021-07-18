using AutoMapper;
using DevBase.Domain.DTO.Cadastros;
using DevBase.Domain.Entidades.Cadastros;
using DevBase.Services.DTO;
using DevBase.Services.Interfaces.Cadastros;
using DevBase.Services.Util.Exceptions;
using DevBase.Services.Util.ExtensionMethods;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevBase.Api.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : ControllerApiBase
    {
        private readonly IDesenvolvedorService _desenvolvedorService;
        private readonly IMapper _mapper;

        public DevelopersController(IDesenvolvedorService desenvolvedorService, IMapper mapper)
        {
            _desenvolvedorService = desenvolvedorService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Inserir(DesenvolvedorDto desenvolvedorDto)
        {
            try
            {
                var response = await _desenvolvedorService.CriarDesenvolvedor(desenvolvedorDto);
                if (response.Sucesso)
                {
                    var dto = _mapper.Map<Desenvolvedor, DesenvolvedorDto>(response.Data);
                    return CreatedAtAction(nameof(Inserir), new { Id = response.Data.Id }, dto);
                }
                return BadRequest(response.Mensagem);
            }
            catch (ValidationException vex)
            {
                return BadRequest(vex.Errors.ToListValidationFailureString());
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PagedResponseDto<DesenvolvedorDto>>>> Consultar([FromQuery] PaginationFilterDeveloperDto filtro)
        {
            try
            {
                return filtro.Validar()
                    ? await ConsultarPorFiltro(filtro)
                    : await ConsultarTodos();

            }
            catch (PaginationFilterDeveloperException pex)
            {
                return BadRequest($"O filtro informado é inválido: {pex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

        private async Task<ActionResult> ConsultarTodos()
        {
            var response = await _desenvolvedorService.RetornarTodosDesenvolvedores();
            var desenvolvedoresDto = _mapper.Map<IList<Desenvolvedor>, IList<DesenvolvedorDto>>(response.Data.ToList());
            return Ok(desenvolvedoresDto);
        }

        private async Task<ActionResult> ConsultarPorFiltro(PaginationFilterDeveloperDto filtro)
        {
            var response = await _desenvolvedorService.ListarPorFiltroPaginado(filtro);
            if (response.Sucesso)
            {
                var desenvolvedoresDto = _mapper.Map<IList<Desenvolvedor>, IList<DesenvolvedorDto>>(response.Data.ToList());
                return Ok(new PagedResponseDto<IList<DesenvolvedorDto>>(desenvolvedoresDto, response.NumeroDaPagina, response.QuantidadeRegistrosPorPagina, response.TotalRegistros));
            }

            return NotFound(response.Mensagem);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DesenvolvedorDto>> Atualizar(int id, DesenvolvedorDto desenvolvedor)
        {
            try
            {
                var response = await _desenvolvedorService.AtualizarDesenvolvedor(id, desenvolvedor);

                if (response.Sucesso)
                {
                    var dev = _mapper.Map<DesenvolvedorDto>(response.Data);
                    return Ok(dev);
                }

                return NotFound(response.Mensagem);

            }
            catch (ValidationException vex)
            {
                return BadRequest(vex.Errors.ToListValidationFailureString());
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DesenvolvedorDto>> Excluir(int id)
        {
            try
            {
                var response = await _desenvolvedorService.ExcluirDesenvolvedor(id);

                if (response.Sucesso)
                    return NoContent();

                return BadRequest(response.Mensagem);

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DesenvolvedorDto>> ConsultarPorId(int id)
        {
            try
            {
                var response = await _desenvolvedorService.ConsultarPorId(id);
                if (response.Sucesso)
                {
                    var dev = _mapper.Map<DesenvolvedorDto>(response.Data);
                    return Ok(dev);
                }

                return NotFound(response.Mensagem);

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }
    }
}