using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CatMensaje
    {
        public CatMensaje ConsultarPorLlave(string llave, string cadenaConexionEF)
        {
            CatMensaje resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var mensajes = (from m in ctx.CatMensajes
                                where m.CatMen_Llave == llave
                                select m).ToList();
                if (mensajes.Count > 0)
                {
                    resultado = mensajes[0];
                }
            }
            return resultado;
        }
    }
}
