using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapPagoElectronico
    {
        public void InsertarPagoElectronico(PagoElectronico pagoElectronico, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapPagoElectronico().InsertarPagoElectronico(pagoElectronico, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarPagoElectronico(PagoElectronico pagoElectronico, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapPagoElectronico().ModificarPagoElectronico(pagoElectronico, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutorizarPagoElectronico(PagoElectronico pagoElectronico, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapPagoElectronico().AutorizarPagoElectronico(pagoElectronico, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutorizarPagoElectronico(PagoElectronico pagoElectronico, ref int verificador, ref dbAccess oDB)
        {
            try
            {
                new CD_CapPagoElectronico().AutorizarPagoElectronico(pagoElectronico, ref verificador, ref oDB);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //JFCV 18 dic 2015 agregar rechazo 
        public void RechazarPagoElectronico(PagoElectronico pagoElectronico, ref int verificador, ref dbAccess oDB)
        {
            try
            {
                new CD_CapPagoElectronico().RechazarPagoElectronico(pagoElectronico, ref verificador, ref oDB);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelarPagoElectronico(PagoElectronico pagoElectronico, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapPagoElectronico().CancelarPagoElectronico(pagoElectronico, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPagoElectronico(PagoElectronico pagoElectronico, string Conexion, ref List<PagoElectronico> list)
        {
            try
            {
                new CD_CapPagoElectronico().ConsultaPagoElectronico(pagoElectronico, Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPagoElectronicoAdmin(PagoElectronico pagoElectronico, string Conexion, ref List<PagoElectronico> list)
        {
            try
            {
                new CD_CapPagoElectronico().ConsultaPagoElectronicoAdmin(pagoElectronico, Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPagoElectronico(PagoElectronico pagoElectronico, string Conexion)
        {
            try
            {
                new CD_CapPagoElectronico().ConsultaPagoElectronico(pagoElectronico, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //jfcv 17 enero 2016 agregue porque al autorizar se revolvia con los archivos reales y el de soporte 
        public void ConsultaPagoElectronicoAutorizacion(PagoElectronico pagoElectronico, string Conexion)
        {
            try
            {
                new CD_CapPagoElectronico().ConsultaPagoElectronicoAutorizacion(pagoElectronico, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     

        public void ConsultaPagoElectronicoArchivos(PagoElectronico pagoElectronico, string Conexion, ref List<PagoElectronico> list)
        {
            try
            {
                new CD_CapPagoElectronico().ConsultaPagoElectronico(pagoElectronico, Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ConsultaEmpRFC(int id_Emp, string Conexion)
        {
            string Result = null;
            try
            {
                Result = (new CD_CapPagoElectronico()).ConsultaEmpRFC(id_Emp, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Result;
        }

        public void EliminarPagoElectronicoArchivos(PagoElectronico pagoElectronico, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapPagoElectronico().EliminarPagoElectronicoArchivos(pagoElectronico, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //JFCV 13 Ene 2015 Cambiar Estatus a Pago Electrónico
        public void CambiarEstatusPagoElectronico(PagoElectronico pagoElectronico, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapPagoElectronico().CambiarEstatusPagoElectronico(pagoElectronico, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
