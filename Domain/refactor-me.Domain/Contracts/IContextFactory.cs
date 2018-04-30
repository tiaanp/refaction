using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Domain.Contracts
{
    public interface IContextFactory
    {

        DbContext Connect();
        /// <summary>
        ///		Create connection.
        /// </summary>
        /// <returns>
        ///		A new <see cref="Task{DbContext}"/> instance.
        /// </returns>
        Task<DbContext> ConnectAsync();
    }
}
