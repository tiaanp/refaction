using FluentValidation;
using refactor_me.Domain.Models;
using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.Validators
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public ProductValidator()
        {
            RuleFor(productOption => productOption.Name).NotNull();
            RuleFor(productOption => productOption.Price).NotNull().GreaterThan(0);
            RuleFor(productOption => productOption.DeliveryPrice).NotNull().GreaterThan(0);

        }
    }
}