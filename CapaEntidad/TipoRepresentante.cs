using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad {
    public class TipoRepresentante : IDisposable
    {
        private int _Id_TipoRepresentante = -1;
        private string _TipoRepresentante_Descripcion = null;
        private int _Id_Emp = -1;
        private bool _TipoRepresentante_activo = false;
        private CapaEntidad.CatTerritorializacion _Territorializacion = null;

        public TipoRepresentante() {
            _Id_TipoRepresentante = -1;
            _TipoRepresentante_Descripcion = null;
            _Id_Emp = -1;
            _TipoRepresentante_activo = false;
            _Territorializacion = null;
        }

        public TipoRepresentante(int Id_TipoRepresentante,
                                      string TipoRepresentante_Descripcion,
                                      int Id_Emp, 
                                      bool TipoRepresentante_activo,
                                      CapaEntidad.CatTerritorializacion Territorializacion)
        {
            _Id_TipoRepresentante = Id_TipoRepresentante;
            _TipoRepresentante_Descripcion = TipoRepresentante_Descripcion;
            _Id_Emp = Id_Emp;
            _TipoRepresentante_activo = TipoRepresentante_activo;
            _Territorializacion = Territorializacion;
        }

        public int Id_TipoRepresentante {
            get { return _Id_TipoRepresentante; }
            set { _Id_TipoRepresentante = value;  }
        }

        public string TipoRepresentante_Descripcion {
            get { return _TipoRepresentante_Descripcion; }
            set { _TipoRepresentante_Descripcion = value; }
        }

        public int Id_Emp {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        public bool TipoRepresentante_activo {
            get { return _TipoRepresentante_activo; }
            set { _TipoRepresentante_activo = value; }
        }

        public CapaEntidad.CatTerritorializacion Territorializacion {
            get { return _Territorializacion; }
            set { _Territorializacion = value; }
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

        ~TipoRepresentante() {
            Dispose(false);
        }
        #endregion
    }
}
