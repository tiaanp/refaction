using System;
using System.Net;
using System.Web.Http;
using AutoMapper;
using refactor_me.Domain.Contracts;
using refactor_me.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using refactor_me.Infrastructure.Extentions;
using System.Net.Http;
using refactor_me.Models;
using refactor_me.Validators;
using FluentValidation.WebApi;

namespace refactor_me.Controllers
{
    [Route("api/[controller]/[action]"),
     AllowAnonymous]
    public class ProductsController : BaseController
    {
        public ProductsController(IRefactorMeProvider refactorMeProvider)
            : base(refactorMeProvider)
        {
        }

        [method:
            HttpGet,
            AllowAnonymous]
        public async Task<IEnumerable<Product>> GetAll()
            => await base._RefactorMeProvider.Products.GetAllAsync();

        [method:
            HttpGet,
            AllowAnonymous]
        public async Task<IEnumerable<Product>> SearchByName(string name)
            => await base._RefactorMeProvider.Products.GetEntitiesAsync(product => product.Name.Contains(name));

        [method:
             HttpGet,
             AllowAnonymous]
        public async Task<Product> GetProduct(Guid id)
            => await base._RefactorMeProvider.Products.GetEntityAsync(product => product.Id == id);

        [method:
            HttpPost,
             AllowAnonymous]
        public ProductDTO Create(ProductDTO product)
        {
            var productValidator = new ProductValidator();
            var results = productValidator.Validate(product);

            var dto = new Product
            {
                DeliveryPrice = product.DeliveryPrice,
                Price = product.Price,
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,

            };

            if (!results.IsValid)
            {
                
                results.AddToModelState(ModelState, "Product");
                
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState));
            }
            base._RefactorMeProvider.Products.Add(dto);

            product.Id = dto.Id;
            return product;
        }

        [method:
             HttpPut,
             AllowAnonymous]
        public void Update(ProductDTO product)
        {
            var productValidator = new ProductValidator();
            var results = productValidator.Validate(product);

            if (!results.IsValid)
            {
                results.AddToModelState(ModelState, "Product");
                //throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState));
            }
            var dto = new Product
            {
                DeliveryPrice = product.DeliveryPrice,
                Price = product.Price,
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,

            };

            base._RefactorMeProvider.Products.Edit(dto);
        }


        [method:
             HttpDelete,
             AllowAnonymous]
        public void Delete(Guid id)
            => base._RefactorMeProvider.Products.DeleteAsync(id);


    }
}
