using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad {
    public class CatTerritorializacion : IDisposable
    {
        private int _Id_Territorializacion = -1;
        private string _Territorializacion = null;
        private string _Descripcion = null;

        public CatTerritorializacion() {
            _Id_Territorializacion = -1;
            _Territorializacion = null;
            _Descripcion = null;
        }

        public CatTerritorializacion(int Id_Territorializacion,
                                       string Territorializacion,
                                       string Descripcion)
        {
            _Id_Territorializacion = Id_Territorializacion;
            _Territorializacion = Territorializacion;
            _Descripcion = Descripcion;
        }

        public int Id_Territorializacion {
            get { return _Id_Territorializacion;  }
            set { _Id_Territorializacion = value; }
        }

        public string Territorializacion {
            get { return _Territorializacion; }
            set { _Territorializacion = value; }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        #region "IDispose"
        // Flag: Has Dispose already been called? 
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers. 
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing) {
            if (disposed) return;

            if (disposing)
            {
                // Free any other managed objects here. 
                //
            }

            // Free any unmanaged objects here. 
            //
            disposed = true;
        }

        ~CatTerritorializacion()
        {
            Dispose(false);
        }
        #endregion
    }
}
