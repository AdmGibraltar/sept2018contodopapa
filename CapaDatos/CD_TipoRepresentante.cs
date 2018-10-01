using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CapaDatos {
    public class CD_TipoRepresentante : IDisposable {
        string _strCNX = null;

        public CD_TipoRepresentante() {
            _strCNX = null;
        }

        public CD_TipoRepresentante(string ConectionString) {
            _strCNX = ConectionString;
        }

        /// <summary>
        /// Consulta el Tipo de Representante de CatTipoRepresentante
        /// </summary>
        /// <param name="Id_TipoRepresentante">Id_TipoRepresentante o = (Cero) Para todos</param>
        /// <returns>Una lista del resultado TipoRepresentante (List&lt;CapaEntidad.TipoRepresentante&gt;)</returns>
        public List<CapaEntidad.TipoRepresentante> ConsultaTipoRepresentante(int Id_TipoRepresentante) {
            return this.ConsultaTipoRepresentante(Id_TipoRepresentante, null);
        }

        /// <summary>
        /// Consulta el Tipo de Representante de CatTipoRepresentante
        /// </summary>
        /// <param name="Id_TipoRepresentante">Id_TipoRepresentante o = (Cero) Para todos</param>
        /// <param name="ConectionString">La cadena de conexion</param>
        /// <returns>Una lista del resultado TipoRepresentante (List&lt;CapaEntidad.TipoRepresentante&gt;)</returns>
        public List<CapaEntidad.TipoRepresentante> ConsultaTipoRepresentante(int Id_TipoRepresentante, string ConectionString) {
            List<CapaEntidad.TipoRepresentante> Result = null;

            try {
                Result = new List<CapaEntidad.TipoRepresentante>();
                _strCNX = ConectionString == null ? _strCNX : ConectionString;

                if (_strCNX == null) { throw new Exception("Cadena de conexion nula"); }

                CD_Datos cd_datos = new CD_Datos(_strCNX);
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_TipoRepresentante" };
                object[] Valores = { Id_TipoRepresentante };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatTipoRepresentante_Consulta", ref dr, Parametros, Valores);

                
                while (dr.Read()) {
                    using (CapaEntidad.TipoRepresentante r = new CapaEntidad.TipoRepresentante()) {
                        r.Id_TipoRepresentante = Convert.ToInt32(dr["Id_TipoRepresentante"]);
                        r.TipoRepresentante_Descripcion = Convert.ToString(dr["TipoRepresentante_Descripcion"]);
                        r.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                        r.TipoRepresentante_activo = Convert.ToBoolean(dr["TipoRepresentante_activo"]);
                        
                        r.Territorializacion = new CapaEntidad.CatTerritorializacion(Convert.ToInt32(dr["Id_Territorializacion"]), null, null);

                        Result.Add(r);
                    }
                    
                }

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex) {

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

        ~CD_TipoRepresentante()
        {
            Dispose(false);
        }
        #endregion
    }
}
