using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;

namespace CapaNegocios
{
    /// <summary>
    /// Representa el contrato para manejar transacciones a nivel de reglas de negocio
    /// </summary>
    public interface IBusinessTransaction
        : IDisposable
    {
        /// <summary>
        /// Actualiza las entidades del contexto, sin empujar los cambios a la base de datos. Útil cuando quieres que el contexto actualice los campos identidad ;)
        /// </summary>
        void Save();

        /// <summary>
        /// Empuja los cambios hechos a nivel de capa de datos a la fuente de datos
        /// </summary>
        void Commit();

        /// <summary>
        /// Marca el inicio de un bloque de trabajo a nivel de capa de datos.
        /// </summary>
        void Begin();

        /// <summary>
        /// Representa el contrato a la conexión física y todas sus operaciones a nivel de capa de acceso y manipulación de datos
        /// </summary>
        ICD_Contexto DataContext
        {
            get;
        }
    }
}
