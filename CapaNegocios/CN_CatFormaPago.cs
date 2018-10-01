using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatFormaPago
    {
        public void ConsultaFormaPago(int Id_Emp, string Conexion, ref List<FormaPago> List)
        {
            try
            {
                CD_CatFormaPago claseCapaDatos = new CD_CatFormaPago();
                claseCapaDatos.ConsultaFormaPago(Id_Emp, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarFormaPago(FormaPago segmento, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatFormaPago claseCapaDatos = new CD_CatFormaPago();
                claseCapaDatos.InsertarFormaPago(segmento, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFormaPago(FormaPago segmento, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatFormaPago claseCapaDatos = new CD_CatFormaPago();
                claseCapaDatos.ModificarFormaPago(segmento, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFormaPago(ref FormaPago fpago, string Conexion)
        {
            try
            {
                CD_CatFormaPago claseCapaDatos = new CD_CatFormaPago();
                claseCapaDatos.ConsultaFormaPago(ref fpago, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
