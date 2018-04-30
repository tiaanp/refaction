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
        public  Product Create(Product product)
        {
            if(product.Name.IsNullOrEmpty())
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState));
            }
            base._RefactorMeProvider.Products.Add(product);

            return  product;
        }

        [method: 
             HttpPut,
             AllowAnonymous]
        public  void Update(Product product)
             =>  base._RefactorMeProvider.Products.Edit(product);

        [method: 
             HttpDelete,
             AllowAnonymous]
        public void Delete(Guid id)
            => base._RefactorMeProvider.Products.DeleteAsync(id);


    }
}
