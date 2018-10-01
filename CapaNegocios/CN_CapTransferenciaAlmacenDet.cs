using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapTransferenciaAlmacenDet
    {
        public void ConsultaTransferenciaAlmacenDetalle_Lista(TransferenciaAlmacen transferenciaAlmacen, string Conexion, ref List<TransferenciaAlmacenDet> List)
        {
            try
            {
                CD_CapTransferenciaAlmacenDet claseCapaDatos = new CD_CapTransferenciaAlmacenDet();
                claseCapaDatos.ConsultaTransferenciaAlmacenDetalle_Lista(transferenciaAlmacen, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
