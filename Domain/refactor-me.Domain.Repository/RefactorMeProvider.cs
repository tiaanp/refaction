using refactor_me.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace refactor_me.Domain.Repository
{
   public class RefactorMeProvider : IRefactorMeProvider, IContextFactory
    {
        public IProductOptionRepository ProductOptions
        {
            get
            {
                return
                   this._ProductOptions ??
                       (this._ProductOptions = new ProductOptionRepository(this));
            }
        }


        public IProductRepository Products
        {
            get
            {
                return
                    this._Products ??
                        (this._Products = new ProductRepository(this));
            }
        }

        public async Task<DbContext> ConnectAsync()
        {
            var response = new RefactorMeContext();

            response.Database.CommandTimeout = 600;

            return await
                Task
                    .FromResult(response).ConfigureAwait(false);
        }

        public DbContext Connect()
        {
            var response = new RefactorMeContext();
            
            response.Database.CommandTimeout = 600;
            return response;
        }

        private IProductRepository _Products;
        private IProductOptionRepository _ProductOptions;

    }
}
