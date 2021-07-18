using DevBase.Domain.Entidades;
using DevBase.Infra.Data.Contextos;
using DevBase.Infra.Data.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevBase.Infra.Data.Repositorios
{
    public class GenericRepository<TEntity> : EntityBase, IGenericRepository<TEntity> where TEntity : EntityBase
    {

        private readonly DevBaseContext _context;

        public GenericRepository(DevBaseContext dataContext)
        {
            _context = dataContext;
        }

        public DevBaseContext GetDataContext()
        {
            return _context;
        }

        public async Task Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
