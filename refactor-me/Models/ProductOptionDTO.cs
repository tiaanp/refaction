using FluentValidation;
using FluentValidation.Attributes;
using refactor_me.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.Models
{
    [Validator(typeof(ProductOptionValidator))]
    public class ProductOptionDTO 
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public object Id { get; internal set; }
    }
}