using Microsoft.EntityFrameworkCore;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.SmartyDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Smarty.Data.Repositories.Implementations
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
        protected SmartyDbContext _context;
		public GenericRepository(SmartyDbContext context)
		{
            _context = context;
		}

		public IEnumerable<T> FindAll()
		{
            return _context.Set<T>().ToList();

        }

		public async Task<IEnumerable<T>> FindAllAsync()
		{
            return await _context.Set<T>().ToListAsync();

        }
        public IEnumerable<T> FindAll(params Expression<Func<T, object>>[] navigationProperties )
        {

            IQueryable<T> query = _context.Set<T>();
            query = navigationProperties.Aggregate(query, (current, navigationProperty) => current.Include(navigationProperty));
            return query.ToList();
        }

        public async Task<IEnumerable<T>> FindAllAsync(params Expression<Func<T,object>>[] navigationProperties )
        {
            IQueryable<T> query = _context.Set<T>();
            query = navigationProperties.Aggregate(query, (current, navigationProperty) => current.Include(navigationProperty));
            return await query.ToListAsync();
		}


        public T FindById(int? id)
        {
            return _context.Set<T>().Find(id);
        }
		


        public async Task<T> FindByIdAsync(int? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

		public T FirstOrDefault(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] navigationProperties)
		{
            IQueryable<T> query = _context.Set<T>();
            query = navigationProperties.Aggregate(query, (current, navigationProperty) => current.Include(navigationProperty));
            return query.FirstOrDefault(criteria);
        }

		public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] navigationProperties)
		{
            IQueryable<T> query = _context.Set<T>();
            query = navigationProperties.Aggregate(query, (current, navigationProperty) => current.Include(navigationProperty));
            return await query.FirstOrDefaultAsync(criteria);
        }
        
        public IEnumerable<T> Find(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = navigationProperties.Aggregate(query, (current, navigationProperty) => current.Include(navigationProperty));
            return query.Where(criteria).ToList();
        }


        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            query = navigationProperties.Aggregate(query, (current, navigationProperty) => current.Include(navigationProperty));

            return await query.Where(criteria).ToListAsync();
        }

        


        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AttachRange(entities);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Count(criteria);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().CountAsync(criteria);
        }

		public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
		{
            _context.Set<T>().UpdateRange(entities);
            return entities;


        }

	}
}
