using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace CapaModelo
{
    /// <summary>
    /// Interface que declara una referencia al contexto de datos desde una entidad.
    /// </summary>
    public interface IDBContextHolder
    {
        /// <summary>
        /// Referencia por composición al contexto de la fuente de datos
        /// </summary>
        DbContext Context
        {
            get;
            set;
        }
    }
}
