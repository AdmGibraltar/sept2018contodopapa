using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
   public class CN_CatCuentasCorp
    {
        public void ConsultaCuentasCorp(int Id_Emp, string Conexion, ref List<CuentasCorp> List)
        {
            try
            {
                CD_CatCuentasCorp claseCapaDatos = new CD_CatCuentasCorp();
                claseCapaDatos.ConsultaCuentasCorp(Id_Emp, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCuentasCorp(CuentasCorp segmento, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCuentasCorp claseCapaDatos = new CD_CatCuentasCorp();
                claseCapaDatos.InsertarCuentasCorp(segmento, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCuentasCorp(CuentasCorp segmento, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCuentasCorp claseCapaDatos = new CD_CatCuentasCorp();
                claseCapaDatos.ModificarCuentasCorp(segmento, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
