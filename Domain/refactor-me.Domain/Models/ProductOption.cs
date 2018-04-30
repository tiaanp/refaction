using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Domain.Models
{
    public class ProductOption : DataEntity
    {
        [property:
            ForeignKey("Product")
        ]

        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
