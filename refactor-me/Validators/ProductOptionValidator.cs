using FluentValidation;
using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.Validators
{
    public class ProductOptionValidator : AbstractValidator<ProductOptionDTO>
    {
        public ProductOptionValidator()
        {
            RuleFor(productOption => productOption.ProductId).NotNull();
            RuleFor(productOption => productOption.Name).NotNull();
        }
    }
}