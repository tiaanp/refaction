using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Domain.Contracts
{
    public interface IRefactorMeProvider
    {
        IProductRepository Products { get; }

        IProductOptionRepository ProductOptions { get; }
    }
}
