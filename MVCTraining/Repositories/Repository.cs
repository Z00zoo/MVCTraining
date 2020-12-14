using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Linq.Expressions;

namespace MVCTraining.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public IUnitOfWork UnitOfWork {get; set;}

        private DbSet<T> _ObjectSet;

        private DbSet<T> ObjectSet
        {
            get 
            {
                if (_ObjectSet == null)
                {
                    _ObjectSet = UnitOfWork.Context.Set<T>();
                }
                return _ObjectSet;
            }
        }

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IQueryable<T> GetAll()
        {
            return ObjectSet;
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> filter)
        {
            return ObjectSet.Where(filter);
        }

        public T GetSingle(Expression<Func<T, bool>> filter)
        {
            return ObjectSet.SingleOrDefault(filter);
        }

        public void Create(T entity)
        {
            ObjectSet.Add(entity);
        }

        public void Commit()
        {
            UnitOfWork.Save();
        }

    }
}