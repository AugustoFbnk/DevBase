using DevBase.Domain.Entidades.Cadastros;
using System;
using System.Linq;

namespace DevBase.Infra.Data.Interfaces.Repositorios.Cadastros
{
    public interface IDesenvolvedorRepositorio : IGenericRepository<Desenvolvedor>
    {
        IQueryable<Desenvolvedor> GetByFiltro(DateTime? dataNascimento, char? sexo, string hobby, string nome);
    }
}
