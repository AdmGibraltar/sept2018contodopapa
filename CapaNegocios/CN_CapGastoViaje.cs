using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapGastoViaje
    {
        public void InsertarGastoViaje(GastoViaje gastoViaje, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapGastoViaje().InsertarGastoViaje(gastoViaje, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutorizarGastoViaje(GastoViaje gastoViaje, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapGastoViaje().AutorizarGastoViaje(gastoViaje, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnviarGastoViaje(GastoViaje gastoViaje, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapGastoViaje().EnviarGastoViaje(gastoViaje, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 

        public void RegistrarGastoViaje(GastoViaje gastoViaje, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapGastoViaje().RegistrarGastoViaje(gastoViaje, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaGastoViaje(GastoViaje gastoViaje, string Conexion, ref List<GastoViaje> list)
        {
            try
            {
                new CD_CapGastoViaje().ConsultaGastoViaje(gastoViaje, Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaGastoViaje(GastoViaje gastoViaje, string Conexion)
        {
            try
            {
                new CD_CapGastoViaje().ConsultaGastoViaje(gastoViaje, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarGastoViaje(GastoViaje gastoViaje, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapGastoViaje().ModificarGastoViaje(gastoViaje, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarEstatusGastoViaje(GastoViaje gastoViaje, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapGastoViaje().ModificarEstatusGastoViaje(gastoViaje, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RechazarGastoViaje(GastoViaje gastoViaje, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapGastoViaje().RechazarGastoViaje(gastoViaje, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        



    }
}
