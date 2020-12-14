using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVCTraining.Models;
using NLog;
using System.Data.Entity.Infrastructure;

namespace MVCTraining.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public DbContext Context { get; set; }

        public EFUnitOfWork()
        {
            Context = new TrackSpendingModel(); //建立連線
        }

        public void Save()
        {
            try
            {
                logger.Debug($"HasChanges = {Context.ChangeTracker.HasChanges()}");
                Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                logger.Debug(ex.Message);
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}