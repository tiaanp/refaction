using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Domain.Models
{
    /// <summary>
	///		base Entity to provide a comman base over all super classes
	/// </summary>
	public abstract class DataEntity 
    {

        #region Instance Properties

        [property:
            Key,
            DatabaseGenerated(DatabaseGeneratedOption.Identity)
        ]
        public virtual Guid Id { get; set; }

       

        #endregion
    }
}
