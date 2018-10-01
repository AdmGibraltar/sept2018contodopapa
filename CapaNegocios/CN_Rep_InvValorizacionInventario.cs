using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;using CapaEntidad;

namespace CapaNegocios
{
    public class CN_Rep_InvValorizacionInventario
    {
        public void ConsultaValorizacion(ValorizacionInventario valorizacion, string Conexion, ref List<Producto> List)
        {
            try
            {
                CD_Rep_InvValorizacionInventario claseCapaDatos = new CD_Rep_InvValorizacionInventario();
                claseCapaDatos.ConsultaValorizacion(valorizacion, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
