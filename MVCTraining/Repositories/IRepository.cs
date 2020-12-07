using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTraining.Repositories
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// unit of work
        /// </summary>
        IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// get all data
        /// </summary>
        /// <returns>IQueryable<T></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// 新增一個entity
        /// </summary>
        /// <param name="entity"></param>
        void Create(T entity);

        /// <summary>
        /// 修改一個entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// save change
        /// </summary>
        void Commit();
    }
}
