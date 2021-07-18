using System;

namespace DevBase.Domain.Entidades.Cadastros
{
    public class Desenvolvedor : EntityBase
    {
        public string Nome { get; set; }
        public Sexo Sexo { get; set; }
        public int Idade { get; set; }
        public string Hobby { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
