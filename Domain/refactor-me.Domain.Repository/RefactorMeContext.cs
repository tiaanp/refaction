using refactor_me.Domain.Models;
using refactor_me.Domain.Repository.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Domain.Repository
{
    internal class RefactorMeContextMigration : IDbContextFactory<RefactorMeContext>
    {
        public RefactorMeContext Create()
        {
            
            return new RefactorMeContext();
        }
    }

    internal class RefactorMeContext : DbContext
    {
        internal RefactorMeContext()
            : base("RefactorConnectionString")
        {

            base.Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RefactorMeContext, Configuration>());
        }
        #region DbContext Implementation

        internal DbSet<Product> Products { get; set; }
        internal DbSet<ProductOption> ProductOptions { get; set; }



        //This is required for auto migrations
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        #endregion

    }
}