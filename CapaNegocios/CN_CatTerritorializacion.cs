using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaNegocios
{
    public class CN_CatTerritorializacion: IDisposable
    {
        private string strCNX = null; 
        public CN_CatTerritorializacion() {
            strCNX = null;
        }

        public CN_CatTerritorializacion(string ConectionString) {
            strCNX = ConectionString;
        }

        public CapaEntidad.CatTerritorializacion ConsultaTipoRepresentante(int Id_Territorializacion) {
            CapaEntidad.CatTerritorializacion Result = null;

            try {
                Result = this.ConsultaTipoRepresentante(Id_Territorializacion, strCNX);
            }
            catch (Exception ex) {
                throw ex;
            }

            return Result;
        }

        public CapaEntidad.CatTerritorializacion ConsultaTipoRepresentante(int Id_Territorializacion, string ConectionString)
        {
            CapaEntidad.CatTerritorializacion Result = null;
            try {
                using (CapaDatos.CD_CatTerritorializacion CT = new CapaDatos.CD_CatTerritorializacion(ConectionString)) { 
                    Result = CT.ConsultaTerritorializaciones(Id_Territorializacion)[0];
                }
            } catch (Exception ex) {
                throw ex;
            }
            
            return Result;
        }

        /// <summary>
        /// Trae la Territorializacion de la tabla CatTerritorializacion
        /// </summary>
        /// <param name="Id_Territorializacion">El ID de Territorializacion o 0 (cero) Para traer todos</param>
        /// <returns>Lista de resultado Territorializacion (List&lt;CapaEntidad.CatTerritorializacion&gt;)</returns>
        public List<CapaEntidad.CatTerritorializacion> ConsultaTipoRepresentantes(int Id_Territorializacion) {
            List<CapaEntidad.CatTerritorializacion> Result = null;

            try {
                Result = this.ConsultaTipoRepresentantes(Id_Territorializacion, strCNX);
            }
            catch (Exception ex) {
                throw ex;
            }

            return Result;
        }

        /// <summary>
        /// Trae la Territorializacion de la tabla CatTerritorializacion
        /// </summary>
        /// <param name="Id_Territorializacion">El ID de Territorializacion o 0 (cero) Para traer todos</param>
        /// <param name="ConectionString">Cadena de conexion</param>
        /// <returns>Lista de resultado Territorializacion (List&lt;CapaEntidad.CatTerritorializacion&gt;)</returns>
        public List<CapaEntidad.CatTerritorializacion> ConsultaTipoRepresentantes(int Id_Territorializacion, string ConectionString)
        {
            List<CapaEntidad.CatTerritorializacion> Result = new List<CapaEntidad.CatTerritorializacion>();
            try {
                using (CapaDatos.CD_CatTerritorializacion CT = new CapaDatos.CD_CatTerritorializacion(ConectionString)) { 
                    Result = CT.ConsultaTerritorializaciones(Id_Territorializacion);
                }
            } catch (Exception ex) {
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

        ~CN_CatTerritorializacion()
        {
            Dispose(false);
        }
        #endregion
    }
}
