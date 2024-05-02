using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace IAAI_ARM64.Data
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
        public virtual DbSet<Models.KnowledgeBase> KnowledgeBase { get; set; }
        public virtual DbSet<Models.About> About { get; set; }
        public virtual DbSet<Models.Master> Master { get; set; }
        public virtual DbSet<Models.ContactUs> ContactUs { get; set; }
        public virtual DbSet<Models.Members> Members { get; set; }
        public virtual DbSet<Models.ServiceHistory> ServiceHistory { get; set; }
    }
}
