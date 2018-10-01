using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_FacturasRutaEntrega
    {
        public void ConsultaFacturasEntrega(Sesion sesion, FacturaEntregaRuta facturafiltro, ref List<FacturaEntregaRuta> List)
        {
            try
            {
                CD_FacturasRutaEntrega factura = new CD_FacturasRutaEntrega();
                factura.ConsultaFacturasRutaEntrega(sesion, facturafiltro, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFacturaRutaEntrega(Sesion sesion, FacturaEntregaRuta factura, ref int Verificador)
        {
            try
            {
                CD_FacturasRutaEntrega facturas = new CD_FacturasRutaEntrega();
                facturas.ModificarFacturaRutaEntrega(sesion, factura, ref Verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
