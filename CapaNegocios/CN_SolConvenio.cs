using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;


namespace CapaNegocios
{
    public class CN_SolConvenio
    {
        public void ProPrecioConv_SolicitudLista(SolConvenio conv, ref List<SolConvenio> List, string Conexion)
        {
            CD_SolConvenio cd_conv = new CD_SolConvenio();
            cd_conv.ProPrecioConv_SolicitudLista(conv, ref List, Conexion);
        }
    }
}
