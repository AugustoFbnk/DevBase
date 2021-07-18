using DevBase.Domain.Entidades.Cadastros;
using System.Linq;

namespace DevBase.Infra.Data.Interfaces.Repositorios.Cadastros
{
    public interface IDesenvolvedorRepositorio : IGenericRepository<Desenvolvedor>
    {
        IQueryable<Desenvolvedor> GetByFiltro(int? idade, char? sexo, string hobby, string nome);
    }
}
