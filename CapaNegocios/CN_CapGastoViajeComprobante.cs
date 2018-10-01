using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapGastoViajeComprobante
    {
        public void InsertarGastoViajeComprobante(GastoViajeComprobante gastoViajeComprobante, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapGastoViajeComprobante().InsertarGastoViajeComprobante(gastoViajeComprobante, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaGastoViajeComprobante(GastoViajeComprobante gastoViajeComprobante, string Conexion, ref List<GastoViajeComprobante> list)
        {
            try
            {
                new CD_CapGastoViajeComprobante().ConsultaGastoViajeComprobante(gastoViajeComprobante, Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaGastoViajeComprobante(GastoViajeComprobante gastoViajeComprobante, string Conexion)
        {
            try
            {
                new CD_CapGastoViajeComprobante().ConsultaGastoViajeComprobante(gastoViajeComprobante, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarGastoViajeComprobante(GastoViajeComprobante gastoViajeComprobante, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapGastoViajeComprobante().EliminarGastoViajeComprobante(gastoViajeComprobante, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
