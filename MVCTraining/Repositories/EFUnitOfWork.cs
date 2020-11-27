using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVCTraining.Models;

namespace MVCTraining.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; set; }

        public EFUnitOfWork()
        {
            Context = new TrackSpendingModel(); //建立連線
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}