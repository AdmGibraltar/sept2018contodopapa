using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace CapaDatos
{
    /// <summary>
    /// Implementación específica de una transacción usando DBContextTransaction como maquinaria interna
    /// </summary>
    public class CD_DBCtxTransaction
        : CD_ITransaction
    {
        public CD_DBCtxTransaction(DbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }

        protected DbContextTransaction _transaction = null;
    }
}
