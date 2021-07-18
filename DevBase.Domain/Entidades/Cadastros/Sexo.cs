using System.Runtime.Serialization;

namespace DevBase.Domain.Entidades.Cadastros
{
    public enum Sexo
    {
        [EnumMember(Value = "M")]
        Masculino,
        [EnumMember(Value = "F")]
        Feminino
    }
}
