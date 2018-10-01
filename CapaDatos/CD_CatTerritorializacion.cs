using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatTerritorializacion : IDisposable
    {
        private string _strCNX = null;

        public CD_CatTerritorializacion() {
            _strCNX = null;
        }

        public CD_CatTerritorializacion(string ConnectionString) {
            _strCNX = ConnectionString;
        }

        /// <summary>
        /// Trae la Territorializacion de la tabla CatTerritorializacion
        /// </summary>
        /// <param name="Id_Territorializacion">El ID de la Territorializacion o 0 (cero) para todas</param>
        /// <returns>Lista de resultado Territorializacion (List&lt;CapaEntidad.CatTerritorializacion&gt;)</returns>
        public List<CapaEntidad.CatTerritorializacion> ConsultaTerritorializaciones(int Id_Territorializacion) {
            return this.ConsultaTerritorializaciones(Id_Territorializacion, _strCNX);
        }

        /// <summary>
        /// Trae la Territorializacion de la tabla CatTerritorializacion
        /// </summary>
        /// <param name="Id_Territorializacion">El ID de la Territorializacion o 0 (cero) para todas</param>
        /// <param name="ConnectionString">La cadena de conexion</param>
        /// <returns>Una lista del resultado Territorializacion (List&lt;CapaEntidad.CatTerritorializacion&gt;)</returns>
        public List<CapaEntidad.CatTerritorializacion> ConsultaTerritorializaciones(int Id_Territorializacion, string ConnectionString) {
            _strCNX = ConnectionString == null ? _strCNX : ConnectionString;
            CD_Datos cd_datos = new CD_Datos(_strCNX);
            SqlDataReader dr = null;
            List<CapaEntidad.CatTerritorializacion> Result = null;

            try {
                if (_strCNX == null) { throw new Exception("La cadena de conexion es nula"); }

                string[] Parametros = { "@Id_Territorializacion" };
                object[] Valores = { Id_Territorializacion };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("CatTerritorializacion_Consulta", ref dr, Parametros, Valores);

                Result = new List<CapaEntidad.CatTerritorializacion>();
                while (dr.Read()) {
                    using (CapaEntidad.CatTerritorializacion r = new CapaEntidad.CatTerritorializacion()) {
                        r.Id_Territorializacion = Convert.ToInt32(dr["Id_Territorializacion"]);
		                r.Territorializacion = Convert.ToString(dr["Territorializacion"]);
		                r.Descripcion = Convert.ToString(dr["Descripcion"]);
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

        ~CD_CatTerritorializacion()
        {
            Dispose(false);
        }
        #endregion
    }
}
