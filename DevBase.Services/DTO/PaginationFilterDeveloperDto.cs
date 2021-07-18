using DevBase.Services.Util.Exceptions;
using System;

namespace DevBase.Services.DTO
{
    public class PaginationFilterDeveloperDto
    {
        public int? NumeroDaPagina { get; set; }
        public int? QuantidadeDeRegistros { get; set; }
        public int? Idade { get; set; }
        public string Nome { get; set; }
        public char? Sexo { get; set; }
        public string Hobby { get; set; }
        public DateTime DataNascimento { get; set; }

        public PaginationFilterDeveloperDto()
        {
        }
        public PaginationFilterDeveloperDto(int? numeroDaPagina,
                                            int? quantidadeDeRegistros,
                                            int? idade = null,
                                            string nome = null,
                                            char? sexo = null,
                                            string hobby = null)
        {
            NumeroDaPagina = numeroDaPagina;
            QuantidadeDeRegistros = quantidadeDeRegistros;
            Idade = idade;
            Nome = nome;
            Sexo = sexo;
            Hobby = hobby;
        }

        public bool Validar()
        {
            LancarExcecaoCasoCamposInvalidos();
            return ValidarFlitro();
        }

        private bool ValidarFlitro()
        {
            if (NumeroDaPagina.HasValue && QuantidadeDeRegistros.HasValue)
            {
                NormalizarFiltro();
                return true;
            }

            return false;
        }

        private void NormalizarFiltro()
        {
            NumeroDaPagina = NumeroDaPagina < 1 ? 1 : NumeroDaPagina;
            QuantidadeDeRegistros = QuantidadeDeRegistros > 10 ? 10 : QuantidadeDeRegistros;
        }

        private void LancarExcecaoCasoCamposInvalidos()
        {
            if (NumeroDaPagina.HasValue && !QuantidadeDeRegistros.HasValue)
            {
                throw new PaginationFilterDeveloperException("Quando o número da página for informado é obrigatório informar a quantidade de registros por página");
            }
            if (!NumeroDaPagina.HasValue && QuantidadeDeRegistros.HasValue)
            {
                throw new PaginationFilterDeveloperException("Quando a quantidade de registros por página for informada é obrigatório informar o número da página");
            }
        }
    }
}
