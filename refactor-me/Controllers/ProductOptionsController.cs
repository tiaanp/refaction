using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using refactor_me.Domain.Contracts;
using refactor_me.Domain.Models;
using System.Threading.Tasks;

namespace refactor_me.Controllers
{
    [RoutePrefix("api/[controller]/[action]")]
    public class ProductOptionsController : BaseController
    {
        public ProductOptionsController(IRefactorMeProvider refactorMeProvider)
            : base(refactorMeProvider)
        {
        }


        [method:
           HttpGet,
           AllowAnonymous]
        public async Task<IEnumerable<ProductOption>> GetAll()
            => await base._RefactorMeProvider.ProductOptions.GetAllAsync();



        [method:
            HttpGet,
            AllowAnonymous,
            Route("{productId}")]
        public async Task<IEnumerable<ProductOption>> ByProductId(Guid productId)
            => await base._RefactorMeProvider.ProductOptions.GetEntitiesAsync(productOption => productOption.ProductId == productId);


        [method:
            HttpGet,
            AllowAnonymous,
            Route("{id}")]
        public async Task<ProductOption> ById(Guid id)
            => await base._RefactorMeProvider
                            .ProductOptions
                            .GetEntityAsync(productOption =>
                                productOption.Id == id);

        [method:
             HttpPost,
             AllowAnonymous ]
        public void Create(ProductOption option)
            => base._RefactorMeProvider
                            .ProductOptions.AddAsync(option);

        [method:
             HttpPut,
             AllowAnonymous]
        public void Update(ProductOption option)
        =>  base._RefactorMeProvider
                            .ProductOptions.Edit(option);

        [method:
             HttpDelete,
             AllowAnonymous,
             Route("{id}")]
        public void Delete(Guid id)
         => base._RefactorMeProvider
                            .ProductOptions.Delete(id);
    }
}
