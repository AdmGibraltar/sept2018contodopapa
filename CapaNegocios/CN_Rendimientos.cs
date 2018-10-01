using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;


namespace CapaNegocios
{
    public class CN_Rendimientos
    {

        public void InsertarRendimientos(Sesion sesion , string Conexion, string sessionID, ref Factura factura, ref int verificador)
        {
            try
            {
                CD_Rendimientos claseCapaDatos = new CD_Rendimientos();
                claseCapaDatos.InsertarRendimientos(sesion, Conexion, sessionID, ref factura, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarRendimientosRemisiones(Sesion sesion, string Conexion, string sessionID, ref int Id_Rem, ref int verificador)
        {
            try
            {
                CD_Rendimientos claseCapaDatos = new CD_Rendimientos();
                claseCapaDatos.InsertarRendimientosRemisiones(sesion, Conexion, sessionID, ref Id_Rem, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void InsertarRendimientosPedidos(Sesion sesion, string Conexion, string sessionID, ref Pedido pedido, ref int verificador)
        {
            try
            {
                CD_Rendimientos claseCapaDatos = new CD_Rendimientos();
                claseCapaDatos.InsertarRendimientosPedidos(sesion, Conexion, sessionID, ref pedido, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarRendimientosLogin(Sesion sesion, string Conexion, string sessionID, string actividad,  ref int verificador)
        {
            try
            {
                CD_Rendimientos claseCapaDatos = new CD_Rendimientos();
                claseCapaDatos.InsertarRendimientosLogin(sesion, Conexion, sessionID, actividad,  ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
