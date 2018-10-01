using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaNegocios {
    public class CN_TipoRepresentante : IDisposable {
        string _strCNX = null;

        public CN_TipoRepresentante() {
            _strCNX = null;
        }

        public CN_TipoRepresentante(string ConnectionString) {
            _strCNX = ConnectionString;
        }

        public CapaEntidad.TipoRepresentante ConsultaTipoRepresentante(int Id_TipoRepresentante) {
            return this.ConsultaTipoRepresentante(Id_TipoRepresentante, null);
        }

        public CapaEntidad.TipoRepresentante ConsultaTipoRepresentante(int Id_TipoRepresentante, string ConnectionString) {
            CapaEntidad.TipoRepresentante Result = null;

            try {
                _strCNX = ConnectionString == null ? _strCNX : ConnectionString;

                using (CapaDatos.CD_TipoRepresentante TR = new CapaDatos.CD_TipoRepresentante(_strCNX)) {
                    Result = TR.ConsultaTipoRepresentante(Id_TipoRepresentante)[0];

                    using (CapaNegocios.CN_CatTerritorializacion CT = new CapaNegocios.CN_CatTerritorializacion(_strCNX)) {
                        Result.Territorializacion = CT.ConsultaTipoRepresentante(Result.Territorializacion.Id_Territorializacion);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            }

            return Result;
        }

        public List<CapaEntidad.TipoRepresentante> ConsultaTipoRepresentantes(int Id_TipoRepresentante) {
            return this.ConsultaTipoRepresentantes(Id_TipoRepresentante, null);
        }

        public List<CapaEntidad.TipoRepresentante> ConsultaTipoRepresentantes(int Id_TipoRepresentante, string ConnectionString)
        {
            List<CapaEntidad.TipoRepresentante> Result = null;

            try
            {
                _strCNX = ConnectionString == null ? _strCNX : ConnectionString;
                Result = new List<CapaEntidad.TipoRepresentante>();

                using (CapaDatos.CD_TipoRepresentante TR = new CapaDatos.CD_TipoRepresentante(_strCNX))
                {
                    Result = TR.ConsultaTipoRepresentante(Id_TipoRepresentante);
                    using (CapaNegocios.CN_CatTerritorializacion CT = new CapaNegocios.CN_CatTerritorializacion(_strCNX)) {
                        for (int i = 0; i<=(Result.Count-1); i++) {
                            Result[i].Territorializacion = CT.ConsultaTipoRepresentante(Result[i].Territorializacion.Id_Territorializacion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Result;
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

        ~CN_TipoRepresentante() {
            Dispose(false);
        }
        #endregion
    }
}
