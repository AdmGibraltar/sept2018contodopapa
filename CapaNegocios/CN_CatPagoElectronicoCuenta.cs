using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatPagoElectronicoCuenta
    {
        public void InsertarCuenta(PagoElectronicoCuenta cuenta, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CatPagoElectronicoCuenta().InsertarCuenta(cuenta, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCuenta(PagoElectronicoCuenta cuenta, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CatPagoElectronicoCuenta().ModificarCuenta(cuenta, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCuenta(PagoElectronicoCuenta cuenta, string Conexion, ref List<PagoElectronicoCuenta> list)
        {
            try
            {
                new CD_CatPagoElectronicoCuenta().ConsultaCuenta(cuenta, Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCuenta(PagoElectronicoCuenta cuenta, string Conexion)
        {
            try
            {
                new CD_CatPagoElectronicoCuenta().ConsultaCuenta(cuenta, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
