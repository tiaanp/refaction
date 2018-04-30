using refactor_me.Domain.BaseRepository;
using refactor_me.Domain.Contracts;
using refactor_me.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Domain.Repository
{
    internal class ProductRepository : EntityRepository<Product>, IProductRepository
    {

        #region Constructors

        /// <summary>
        ///		Creates a new  ProductRepository instance.
        /// </summary>
      
        internal ProductRepository(IContextFactory contextFactory)
			: base(contextFactory) {
        }

        #endregion

        #region EntityRepository<ProductRepository> Implementation

        protected override void DataTransfer(Product original, Product modified)
        {
            base.DataTransfer(original, modified);

            original.DeliveryPrice = modified.DeliveryPrice;
            original.Description = modified.Description;
            original.Name = modified.Name;
            original.Price = modified.Price;
            // Validates parameters and updates the VersionName properties.

            


        }

        #endregion
    }
}
