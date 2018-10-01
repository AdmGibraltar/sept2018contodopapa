using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Linq.Expressions;

namespace CapaDatos
{
    public interface ICD_Contexto : IDisposable
    {
        void Commit();

        CD_ITransaction BeginTransaction();

        void ReloadEntity<E, P>(E entity, Expression<Func<E, P>> navigationProperty)
            where P : class
            where E : class;
    }

    public interface ICD_Contexto<T>
        : ICD_Contexto where T : DbContext
    {
        T Contexto
        {
            get;
            set;
        }
    }
}
