using System;

namespace DevBase.Domain.DTO.Cadastros
{
    public class DesenvolvedorDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public char? Sexo { get; set; }
        public int? Idade { get; set; }
        public string Hobby { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
