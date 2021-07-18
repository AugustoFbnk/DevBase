using DevBase.Domain.DTO.Cadastros;
using FluentValidation;

namespace DevBase.Services.Util.DtoValidator
{
    public class DesenvolvedorDtoValidator : AbstractValidator<DesenvolvedorDto>
    {
        public DesenvolvedorDtoValidator()
        {
            RuleFor(x => x.Sexo).Must(VerificarSexoInformadoValido).WithMessage("O valor informado para o sexo é inválido! Valores válidos: F para feminino, M para Masculino");
            RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage("O nome não foi informado!");
            RuleFor(x => x.DataNascimento).NotNull().NotEmpty().WithMessage("A data de nascimento não foi informada!");
            RuleFor(x => x.Idade).Null().WithMessage("A idade não deve ser informada. O campo é gerado com base na data de nascimento.");
        }

        private bool VerificarSexoInformadoValido(char? sexo)
        {
            return sexo == 'F' || sexo == 'M';
        }
    }
}
