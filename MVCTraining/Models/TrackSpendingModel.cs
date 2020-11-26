using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MVCTraining.Models
{
    public partial class TrackSpendingModel : DbContext
    {
        public TrackSpendingModel()
            : base("name=TrackSpendingModel")
        {
        }

        public virtual DbSet<AccountBook> AccountBook { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
