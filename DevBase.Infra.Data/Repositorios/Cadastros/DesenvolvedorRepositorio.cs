using DevBase.Util.ExtensionMethods;
using DevBase.Domain.Entidades.Cadastros;
using DevBase.Infra.Data.Contextos;
using DevBase.Infra.Data.Interfaces.Repositorios.Cadastros;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace DevBase.Infra.Data.Repositorios.Cadastros
{
    public class DesenvolvedorRepositorio : GenericRepository<Desenvolvedor>, IDesenvolvedorRepositorio
    {
        public DesenvolvedorRepositorio(DevBaseContext dataContext) : base(dataContext)
        {
        }

        public IQueryable<Desenvolvedor> GetByFiltro(DateTime? dataNascimento, char? sexo, string hobby, string nome)
        {
            return GetDataContext().Set<Desenvolvedor>()
                .Where(x => x.DataNascimento == (dataNascimento ?? x.DataNascimento)
                         && x.Nome.Contains(!string.IsNullOrEmpty(nome) ? nome : x.Nome)
                         && x.Sexo == (sexo != null ? sexo.ToCharEnum<Sexo>() : x.Sexo)
                         && x.Hobby.Contains(!string.IsNullOrEmpty(hobby) ? hobby : x.Hobby)).AsNoTracking();
        }
    }
}
