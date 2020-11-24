using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MVCTraining.Repositories
{
    public partial class TrackSpendingRepository : DbContext
    {
        public TrackSpendingRepository()
            : base("name=HomeWorkDB")
        {
        }

        public virtual DbSet<AccountBook> AccountBook { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
