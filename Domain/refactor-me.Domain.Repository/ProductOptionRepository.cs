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
    internal class ProductOptionRepository : EntityRepository<ProductOption>, IProductOptionRepository
    {

        #region Constructors

        /// <summary>
        ///		Creates a new  ProductOptionRepository instance.
        /// </summary>

        internal ProductOptionRepository(IContextFactory contextFactory)
			: base(contextFactory) {
        }

        #endregion

        #region EntityRepository Implementation

        protected override void DataTransfer(ProductOption original, ProductOption modified)
        {
            // Validates parameters and updates the Version properties.
            base.DataTransfer(original, modified);

            original.Description = modified.Description;
            original.Name = modified.Name;

          

        }

        #endregion
    }
}
