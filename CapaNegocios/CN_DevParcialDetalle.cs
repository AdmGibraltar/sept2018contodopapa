using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_DevParcialDetalle
    {
        public void InsertarDevParcial(Sesion Sesion, DevParcial_Detalle devparcial, List<DevParcial_Detalle> devparcialList, ref int verificador)
        {
            try
            {
                new CD_DevParcial_Detalle().InsertarDevParcial(Sesion, devparcial, devparcialList, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void InsertarDevParcialDetalle(Sesion Sesion, List<DevParcial_Detalle> devparcialList, ref int verificador)
        //{
        //    try
        //    {
        //        new CD_DevParcial_Detalle().InsertarDevParcialDetalle(Sesion, devparcialList, ref verificador);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void ConsultaDevParcialDetalleFactura(Sesion Sesion, int factura, int devolucion, ref List<DevParcial_Detalle> List)
        {
            try
            {
                new CD_DevParcial_Detalle().ConsultaDevParcialDetalleFactura(Sesion, factura, devolucion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DevParcial_DetalleFactura> ConsultaDetalleFactura(Sesion Sesion, int factura, int id)
        {
            try
            {
                List<DevParcial_DetalleFactura> List = new List<DevParcial_DetalleFactura>();
                new CD_DevParcial_Detalle().ConsultaDetalleFactura(Sesion, factura, id, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFacturas(Sesion Sesion, int factura, ref DevParcial_DetalleFactura List)
        {
            try
            {
                new CD_DevParcial_Detalle().ConsultaFacturas(Sesion, factura, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void EliminarDevParcial(Sesion Sesion, DevParcial devParcial, ref int verificador)
        {
            try
            {
                CD_DevParcial_Lista cddevParcial = new CD_DevParcial_Lista();
                cddevParcial.EliminarDevParcialLista(Sesion, devParcial, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarEncabezadoImprimir(Sesion sesion, DevParcial devParcial, ref DevParcial devParcial2)
        {
            try
            {
                CD_DevParcial_Lista cddevParcial = new CD_DevParcial_Lista();
                cddevParcial.ConsultarEncabezadoImprimir(sesion, devParcial, ref devParcial2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarDevParcialImpresion(DevParcial_Detalle devParcial, string Conexion, ref int verificador)
        {
            try
            {
                CD_DevParcial_Lista cddevParcial = new CD_DevParcial_Lista();
                cddevParcial.ActualizarDevParcialImpresion(devParcial, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }             
    }
}
