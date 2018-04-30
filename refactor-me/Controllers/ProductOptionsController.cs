using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using refactor_me.Domain.Contracts;
using refactor_me.Domain.Models;
using System.Threading.Tasks;
using refactor_me.Models;
using refactor_me.Validators;
using FluentValidation.WebApi;

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
             AllowAnonymous]
        public ProductOptionDTO Create(ProductOptionDTO option)
        {
            var productOptionValidator = new ProductOptionValidator();
            var results = productOptionValidator.Validate(option);

            if (!results.IsValid)
            {
                results.AddToModelState(ModelState, "ProductOption");
            }


            var dto = new ProductOption
            {
                Description = option.Description,
                Name = option.Name,
                ProductId = option.ProductId
            };
            base._RefactorMeProvider
                          .ProductOptions.AddAsync(dto);
            option.Id = dto.Id;
            return option;
        }


        [method:
             HttpPut,
             AllowAnonymous]
        public void Update(ProductOptionDTO option)
        {
            var productOptionValidator = new ProductOptionValidator();
            var results = productOptionValidator.Validate(option);

            if (!results.IsValid)
            {
                results.AddToModelState(ModelState, "ProductOption");
            }


            var dto = new ProductOption
            {
                Description = option.Description,
                Name = option.Name,
                ProductId = option.ProductId
            };

            base._RefactorMeProvider
                              .ProductOptions.Edit(dto);
        }

        [method:
             HttpDelete,
             AllowAnonymous,
             Route("{id}")]
        public void Delete(Guid id)
         => base._RefactorMeProvider
                            .ProductOptions.Delete(id);
    }
}
