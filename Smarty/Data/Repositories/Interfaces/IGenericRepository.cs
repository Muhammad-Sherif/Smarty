using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Smarty.Data.Repositories.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		T FindByKey(params object[] keyValues);
		public Task<T> FindByKeyAsync(params object[] keyValues);
		public T FirstOrDefault(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] navigationProperties);
		public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] navigationProperties);

		IEnumerable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
		public Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties);

		IEnumerable<T> GetAll();
		public Task<IEnumerable<T>> GetAllAsync();

		IEnumerable<T> FindByCriteria(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] navigationProperties);
		Task<IEnumerable<T>> FindByCriteriaAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] navigationProperties);

		int Count();
		int Count(Expression<Func<T, bool>> criteria);
		Task<int> CountAsync();
		Task<int> CountAsync(Expression<Func<T, bool>> criteria);
		T Add(T entity);
		Task<T> AddAsync(T entity);
		IEnumerable<T> AddRange(IEnumerable<T> entities);
		Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
		void Delete(T entity);
		void DeleteRange(IEnumerable<T> entities);
		T Update(T entity);
		IEnumerable<T> UpdateRange(IEnumerable<T> entities);

		void Attach(T entity);
		void AttachRange(IEnumerable<T> entities);

	}
}
