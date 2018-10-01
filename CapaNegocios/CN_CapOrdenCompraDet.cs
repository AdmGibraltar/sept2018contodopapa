using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapOrdenCompraDet
    {
        public void spCapOrdCompraDetalle_Consulta_Entradas_Partida(OrdenCompraDet ordenCompraDet, string Conexion, ref int Ord_Cantidad)
        {
            try
            {
                CD_CapOrdenCompraDet claseCapaDatos = new CD_CapOrdenCompraDet();
                claseCapaDatos.spCapOrdCompraDetalle_Consulta_Entradas_Partida(ordenCompraDet, Conexion, ref Ord_Cantidad);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void spCapOrdCompraDetalle_Consulta_Entradas(OrdenCompraDet ordenCompraDet, string Conexion, ref List<OrdenCompraDet> List)
        {
            try
            {
                CD_CapOrdenCompraDet claseCapaDatos = new CD_CapOrdenCompraDet();
                claseCapaDatos.spCapOrdCompraDetalle_Consulta_Entradas(ordenCompraDet, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaOrdenCompraDetalle_Lista(OrdenCompraDet ordenCompraDet, string Conexion, ref List<OrdenCompraDet> List)
        {
            try
            {
                CD_CapOrdenCompraDet claseCapaDatos = new CD_CapOrdenCompraDet();
                claseCapaDatos.ConsultaOrdenCompraDetalle_Lista(ordenCompraDet, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
