using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo.SIANCentral;
using System.Linq.Expressions;

namespace CapaDatos
{
    internal class CD_ContextoSIANCentral
        : ICD_Contexto<SIANCentralEntities1>
    {
        /// <summary>
        /// Accesor de la implementación específica del contexto de datos para la aplicación
        /// </summary>
        public SIANCentralEntities1 Contexto
        {
            get;
            set;
        }

        /// <summary>
        /// Llama a [Dispose] del contexto
        /// </summary>
        public void Dispose()
        {
            Contexto.Dispose();
        }

        /// <summary>
        /// Empuja los datos hechos en el contexto a la fuente de datos
        /// </summary>
        public void Commit()
        {
            Contexto.SaveChanges();
        }

        /// <summary>
        /// Crea y marca el inicio de una transacción
        /// </summary>
        /// <returns></returns>
        public CD_ITransaction BeginTransaction()
        {
            return new CD_DBCtxTransaction(Contexto.Database.BeginTransaction());
        }


        public void ReloadEntity<E, P>(E entity, Expression<Func<E, P>> navigationProperty)
            where P : class
            where E : class
        {
            Contexto.Entry(entity).Reference(navigationProperty);
        }
    }
}
