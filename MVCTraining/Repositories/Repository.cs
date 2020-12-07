using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVCTraining.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public IUnitOfWork UnitOfWork {get; set;}

        private DbSet<T> _Objectset;

        private DbSet<T> Objectset
        {
            get 
            {
                if (_Objectset == null)
                {
                    _Objectset = UnitOfWork.Context.Set<T>();
                }
                return _Objectset;
            }
        }

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IQueryable<T> GetAll()
        {
            return Objectset;
        }

        public void Create(T entity)
        {
            Objectset.Add(entity);
        }

        public void Update(T entity)
        {

            Objectset.Attach(entity);
            UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
        }

        public void Commit()
        {
            UnitOfWork.Save();
        }
    }
}