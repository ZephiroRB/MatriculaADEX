﻿using Matricula.Core.Interfaces;
using Matricula.Infrastructure.Data;

namespace Matricula.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MatriculaContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(MatriculaContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }
        public async Task Delete(Guid id)
        {
            T entity = await GetById(id);
            _entities.Remove(entity);
        }
    }
}
