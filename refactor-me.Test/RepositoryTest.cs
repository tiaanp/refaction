using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.Domain.Contracts;
using refactor_me.Domain.Repository;
using refactor_me.Domain.Models;
using refactor_me.Infrastructure.Extentions;

namespace refactor_me.Test
{
    [TestClass]
    public class RepositoryTest
    {

        [TestMethod]
        public void Add()
        {
            IRefactorMeProvider repo = new RefactorMeProvider();

            var productToInsert = new Product
            {
                DeliveryPrice = 255,
                Description = "S10",
                Name = "Samsung",
                Price = 234
            };

            repo.Products.Add(productToInsert);

            var productToEdit = repo.Products.GetEntityAsync(e => e.Id == productToInsert.Id).Result;

            productToEdit.Price = 258;

            repo.Products.Edit(productToEdit);

            repo.Products.Delete(productToEdit.Id);

           var response = repo.Products.Any(product => product.Id == productToEdit.Id);


        }

        [TestMethod]
        public void GetEntities()
        {
            
            IRefactorMeProvider repo = new RefactorMeProvider();
           
        }

        [TestMethod]
        public void GetAll()
        {
            IRefactorMeProvider repo = new RefactorMeProvider();
            var product = repo.Products.GetAllAsync();
        }


       
    }
}
