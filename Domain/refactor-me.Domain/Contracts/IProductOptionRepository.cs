using refactor_me.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Domain.Contracts
{
    public interface IProductOptionRepository : IEntityRepository<ProductOption>
    {
    }
}
