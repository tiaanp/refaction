using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.Controllers;
using refactor_me.Domain.Repository;
using System.Linq;
using System.Threading.Tasks;
using refactor_me.Domain.Models;
using refactor_me.Validators;
using refactor_me.Models;
using System.Net.Http;

namespace refactor_me.Test
{
    [TestClass]
    public class TestProductController
    {
        ProductDTO _Product;
        [TestMethod]
        public void Product()
        {

            var refactorMeProvider = new RefactorMeProvider();
             
            var controller = new ProductsController(refactorMeProvider);

            //Adding A Product to DB
            var product = controller.Create(new ProductDTO
            {
                Name = "Samsung Galaxy S7",
                Description = "Newest mobile product from Samsung.",
                Price = 1024.99m,
                DeliveryPrice = 16.99m,
            });

           

            // _Product = (ProductDTO)product;

            GetAll();
            Update();
            Delete();
        }


      
        public void GetAll()
        {


            var refactorMeProvider = new RefactorMeProvider();

            var controller = new ProductsController(refactorMeProvider);


            var addedproduct = controller.GetProduct(_Product.Id).Result;

            Assert.AreEqual(addedproduct.Id, _Product.Id);
        }
  
        public void Update()
        {
            var refactorMeProvider = new RefactorMeProvider();

            var controller = new ProductsController(refactorMeProvider);

            _Product.Name = "Samsung Galaxy S8";

            controller.Update(_Product);

            var updatedProduct = controller.GetProduct(_Product.Id).Result;

            Assert.AreEqual(updatedProduct.Name, "Samsung Galaxy S8 Test");
        }

      
        public void Delete()
        {
            var refactorMeProvider = new RefactorMeProvider();

            var controller = new ProductsController(refactorMeProvider);

            controller.Delete(_Product.Id);

            var deletedProduct = controller.GetProduct(_Product.Id).Result;

            Assert.AreEqual(deletedProduct, null);

        }
    }
}
