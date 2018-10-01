using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CapPedido
    {

        public void InsertarPedido(CapaEntidad.Pedido pedido, DataTable dt, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.InsertarPedido(pedido, dt, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedido(ref Pedido pedido, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedido(ref pedido, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void PedidosPendientes_ConsultaReporte(Pedido pedido, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_CapPedido cd_ped = new CD_CapPedido();
                cd_ped.PedidosPendientes_ConsultaReporte(pedido, ref dt, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool ConsultaPedidoFacturacion(ref Pedido pedido, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                return claseCapaDatos.ConsultaPedidoFacturacion(ref pedido, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ConsultaPedidoTieneUnidadesRemisionadas(ref Pedido pedido, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                return claseCapaDatos.ConsultaPedidoTieneUnidadesRemisionadas(ref pedido, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarTotalPedidosCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                new CD_CapPedido().ConsultarTotalPedidosCentroDist(ref verificador, Id_Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarPedido(Pedido pedido, DataTable dt, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ModificarPedido(pedido, dt, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoDet(Pedido pedido, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoDet(pedido, Conexion, ref dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoDetDisp(Pedido pedido, ref DataTable dt, int? facturando, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoDetDisp(pedido, Conexion, facturando, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Baja(Pedido ped, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.Baja(ped, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Imprimir(Pedido ped, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.Imprimir(ped, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedido(Pedido pedido, string Conexion, ref List<Pedido> List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedido(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoAsig_Admin(Pedido pedido, string Conexion, ref List<Pedido> List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoAsig_Admin(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoAsig(Pedido pedido, string Conexion, ref List<PedidoDet> List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoAsig(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AsignarPrdXPed(Pedido pedido, List<PedidoDet> list, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.AsignarPrdXPed(pedido, list, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoAutorizacion_Lista(Pedido pedido, string Conexion, ref List<Pedido> List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoAutorizacion_Lista(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Autorizar(Pedido pedido, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.Autorizar(pedido, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarEncabezado_RepFacPedidosPendientes(Sesion sesion, ref string Emp_Nombre, ref string Cd_Nombre, ref string U_Nombre)
        {
            try
            {
                new CD_CapPedido().ConsultarEncabezado_RepFacPedidosPendientes(sesion, ref Emp_Nombre, ref Cd_Nombre, ref U_Nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoDisp(Pedido pedido, int prd, string Conexion, ref int disponible_pedido)
        {
            try
            {
                new CD_CapPedido().ConsultaPedidoDisp(pedido, prd, Conexion, ref disponible_pedido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoCancelacion(PedidoDet pedido, string Conexion, ref List<PedidoDet> list)
        {
            try
            {
                new CD_CapPedido().ConsultaPedidoCancelacion(pedido, Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BajaParcial(Pedido pedido, List<PedidoDet> list, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapPedido().BajaParcial(pedido, list, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AsignarRuta(int id_Ped, string sector, string ruta, int secuencia, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapPedido().AsignarRuta(id_Ped, sector, ruta, secuencia, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
