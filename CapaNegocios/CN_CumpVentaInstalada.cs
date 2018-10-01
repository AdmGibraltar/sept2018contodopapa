using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CumpVentaInstalada
    {  
        public string ConsultaNombreEmpresa(Sesion sesion)
        {
            string empresa = string.Empty;
            try
            {
                CD_CumpVentaInstalada claseCapaDatos = new CD_CumpVentaInstalada();
                empresa = claseCapaDatos.ConsultaNombreEmpresa(sesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return empresa;
        }

        public string ConsultaNombreSucursal(Sesion sesion)
        {
            string sucursal = string.Empty;
            try
            {
                CD_CumpVentaInstalada claseCapaDatos = new CD_CumpVentaInstalada();
                sucursal = claseCapaDatos.ConsultaNombreSucursal(sesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sucursal;
        }

        public void ConsultaTotalesVenta(Sesion sesion, CumpVentaInstalada venta, ref CumpVentaInstalada ventaInstalada)
        {
            try
            {
                CD_CumpVentaInstalada claseCapaDatos = new CD_CumpVentaInstalada();
                claseCapaDatos.ConsultaTotalesVenta(sesion, venta, ref ventaInstalada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
