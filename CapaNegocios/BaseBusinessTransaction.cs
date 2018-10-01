using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    /// <summary>
    /// Implementación base del contrato de transacciones a nivel de reglas de negocio.
    /// </summary>
    public abstract class BaseBusinessTransaction
        : IBusinessTransaction
    {
        public virtual void Commit()
        {
            _cdTransaction.Commit();
            DataContext.Commit();
        }

        public virtual ICD_Contexto DataContext
        {
            get
            {
                return _DataContext;
            }
        }

        protected ICD_Contexto _DataContext=null;


        public void Begin()
        {
            //¿regresa una transacción a nivel de capa de datos?
            if(_cdTransaction!=null)
                _cdTransaction.Dispose();
            _cdTransaction = DataContext.BeginTransaction();
        }

        public void Dispose()
        {
            try
            {
                _cdTransaction.Dispose();
            }
            catch (Exception ex)
            {
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

            try
            {
                _DataContext.Dispose();
            }
            catch (Exception ex)
            {
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        protected CD_ITransaction _cdTransaction = null;

        public void Save()
        {
            DataContext.Commit();
        }
    }
}
