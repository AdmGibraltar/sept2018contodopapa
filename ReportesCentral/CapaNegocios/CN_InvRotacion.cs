using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;
using System.Collections;
using System.Data;

namespace CapaNegocios
{
    public class CN_InvRotacion
    {
        public void Consulta(InvRotacion rotacion, string Conexion, ref List<InvRotacion> List)
        {
            try
            {
                CD_InvRotacion claseCapaDatos = new CD_InvRotacion();
                claseCapaDatos.Consulta(rotacion, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
