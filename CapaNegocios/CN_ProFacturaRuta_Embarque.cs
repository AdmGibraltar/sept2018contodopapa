using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_ProFacturaRuta_Embarque
    {
        /// <summary>
        /// Metodo que busca los datos de los embarques en la base de datos
        /// </summary>
        /// <param name="embarques">Entidad de los embarques</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="lista">Lista que se llena con el resultado de la operacion</param>
        /// <param name="Id_Emp">Id de la empresa</param>
        /// <param name="Id_Cd">Id de la ciudad</param>
        /// <param name="Id_Emb">Id del embarque</param>
        /// <param name="Fac_Fecha_Ini">Fecha de inicio para el periodo de busqueda</param>
        /// <param name="Fac_Fecha_Fin">Fecha de fin para el periodo de busqueda</param> 
        public void BuscarProFacturaRutaEmbarque(Embarques embarques, string conexion, ref List<Embarques> lista,
            int Id_Emp, int Id_Cd, int Id_Emb, DateTime Fac_Fecha_Ini, DateTime Fac_Fecha_Fin)
        {
            try
            {
                CD_ProFacturaRuta_Embarque CDProFacturaRutaEmbarque = new CD_ProFacturaRuta_Embarque();

                CDProFacturaRutaEmbarque.BuscarProFacturaRutaEmbarque(embarques, conexion, ref lista, 
                    Id_Emp, Id_Cd, Id_Emb, Fac_Fecha_Ini, Fac_Fecha_Fin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que trae datos especificos de algun embarque
        /// </summary>
        /// <param name="embarques">Entidad de los embarques</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        public void ConsultaProFacturaRutaEmbarque(ref Embarques embarques, string conexion)
        {
            try
            {
                CD_ProFacturaRuta_Embarque CDProFacturaRutaEmbarque = new CD_ProFacturaRuta_Embarque();
                CDProFacturaRutaEmbarque.ConsultaProFacturaRutaEmbarque(ref embarques, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
