using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_ProFactura_Embarque
    {
        /// <summary>
        /// Busca las facturas con estatus I
        /// </summary>
        /// <param name="factura">Entidad de las facturas</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="lista">Lista donde se vaciaran los datos obtenidos</param>
        /// <param name="Id_Emp">Id de la empresa</param>
        /// <param name="Id_Cd">Id de la ciudad</param>
        /// <param name="nombreCliente">Nombre del cliente</param>
        /// <param name="Id_Cte">Id del cliente</param>
        /// <param name="Fac_Fecha_Inicio">Fecha de inicio del periodo</param>
        /// <param name="Fac_Fecha_Fin">Fecha de fin del periodo</param>
        public void BuscaFacturaEmbarque(Factura factura, string conexion, ref List<Factura> lista,
            int Id_Emp, int Id_Cd, string nombreCliente, int Id_Cte, DateTime Fac_Fecha_Inicio,
            DateTime Fac_Fecha_Fin)
        {
            try
            {
                CD_ProFactura_Embarque CDProFactura_Embarque = new CD_ProFactura_Embarque();
                CDProFactura_Embarque.BuscaFacturaEmbarque(factura, conexion, ref lista, Id_Emp,
                    Id_Cd, nombreCliente, Id_Cte, Fac_Fecha_Inicio, Fac_Fecha_Fin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que actualiza el estatus de las facturas
        /// </summary>
        /// <param name="factura">Entidad de la factura</param>
        /// <param name="dt">DataTable donde se vaciaran los resultados obtenidos</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Indica si se pudo o no realizar la operacion</param>
        public void CambiaEstatusFacturaEmbarque(Factura factura, DataTable dt, string conexion, ref int verificador)
        {
            try
            {
                CD_ProFactura_Embarque CDProFacturaEmbarque = new CD_ProFactura_Embarque();

                CDProFacturaEmbarque.CambiaEstatusFacturaEmbarque(factura, dt, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
