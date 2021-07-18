using DevBase.Util.ExtensionMethods;
using DevBase.Domain.Entidades.Cadastros;
using DevBase.Infra.Data.Interfaces.Repositorios.Cadastros;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevBase.Services.Test.RepositorioMock.Cadastros.DesenvolvedorRepositorioMocks
{
    public class DesenvolvedorRepositorioMock : IDesenvolvedorRepositorio
    {
        private readonly List<Desenvolvedor> _registros = new List<Desenvolvedor>();

        public async Task Create(Desenvolvedor entity)
        {
            entity.Id = _registros.Count + 1;
            _registros.Add(entity);
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(Desenvolvedor entity)
        {
            _registros.Remove(entity);
        }

        public IQueryable<Desenvolvedor> GetAll()
        {
            return _registros.AsQueryable();
        }

        public IQueryable<Desenvolvedor> GetByFiltro(int? idade, char? sexo, string hobby, string nome)
        {
            return _registros.Where(x => x.Idade == (idade ?? x.Idade)
                         && x.Nome.Contains(!string.IsNullOrEmpty(nome) ? nome : x.Nome)
                         && x.Sexo == (sexo != null ? sexo.ToCharEnum<Sexo>() : x.Sexo)
                         && x.Hobby.Contains(!string.IsNullOrEmpty(hobby) ? hobby : x.Hobby)).AsQueryable();
        }

        public async Task<Desenvolvedor> GetById(int id)
        {
            return _registros.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task Update(Desenvolvedor entity)
        {
            var dev = _registros.Where(x => x.Id == entity.Id).FirstOrDefault();
            dev.Nome = entity.Nome;
            dev.DataNascimento = entity.DataNascimento;
            dev.Sexo = entity.Sexo;
            dev.Idade = entity.DataNascimento.CalcularIdade();
        }
    }
}
