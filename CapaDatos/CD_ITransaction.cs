using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaDatos
{
    /// <summary>
    /// Representa una transacción usable a nivel de la capa de acceso a datos
    /// </summary>
    public interface CD_ITransaction
        : IDisposable
    {
        /// <summary>
        /// Empuja los cambios hechos en el contexto de datos a la base de datos
        /// </summary>
        void Commit();
    }
}
