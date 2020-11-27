using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MVCTraining.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// db context
        /// </summary>
        DbContext Context { get; set; }
        /// <summary>
        /// save change
        /// </summary>
        void Save();
    }
}
