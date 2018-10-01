using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;
using System.Collections;

namespace CapaNegocios
{
    public class CN_CapPedidoVtaInst
    {
        public void Lista(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Lista(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Lista(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List, string modalidadVenta)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Lista(pedido, Conexion, ref List, modalidadVenta);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ListaFacturacion(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ListaFacturacion(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Consultar(ref PedidoVtaInst pedido, string Conexion, ref int verificador, ref Clientes cc)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Consultar(ref pedido, Conexion, ref verificador, ref cc);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultarDet(PedidoVtaInst pedido, ref  List<PedidoVtaInst> List, ref System.Data.DataTable dt, string Conexion, int? idTG)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarDet(pedido, ref List, Conexion, ref dt, idTG);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarDet_Resto(PedidoVtaInst pedido, ref  List<PedidoVtaInst> List, ref System.Data.DataTable dt, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarDet_Resto(pedido, ref List, Conexion, ref dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta el detalle del resto de productos de un pedido en captura de garantía.
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="List"></param>
        /// <param name="Conexion"></param>
        /// <param name="dt"></param>
        /// <param name="Id_TG">Identificador del tipo de garantía</param>
        public void ConsultarDet_Resto(PedidoVtaInst pedido, ref  List<PedidoVtaInst> List, ref System.Data.DataTable dt, string Conexion, int Id_TG)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarDet_Resto(pedido, ref List, Conexion, ref dt, Id_TG);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarDetAcys(PedidoVtaInst pedido, ref  List<PedidoVtaInst> List, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst cd_pv = new CD_CapPedidoVtaInst();
                cd_pv.ConsultarDetAcys(pedido, ref List, Conexion);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultarPedidoExistente(PedidoVtaInst pvi, int Id_Prd, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarPedidoExistente(pvi, Id_Prd, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /*
        public void Insertar(PedidoVtaInst pedido, DataTable dt, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Insertar(pedido, dt, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        */
        public void Insertar(PedidoVtaInst pedido, DataTable dt, string Conexion, ref int verificador, int? idTG, int? Id_AcsVersion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Insertar(pedido, dt, Conexion, ref verificador, idTG, Id_AcsVersion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Cancelar(PedidoVtaInst pedido, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Cancelar(pedido, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RechazarPedidoVI(PedidoVtaInst pedido, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.RechazarPedidoVI(pedido, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(PedidoVtaInst pedido, DataTable dt, string Conexion, int captado, ref int verificador, ArrayList eliminados)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.Modificar(pedido, dt, Conexion, captado, ref verificador, eliminados);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultarAAAEspecial(int Id_Emp, int Id_Cd, double Id_Cte, string Id_prd, ref int verificador, string Conexion)
        {
            try
            {
                CD_CapPedidoVtaInst claseCapaDatos = new CD_CapPedidoVtaInst();
                claseCapaDatos.ConsultarAAAEspecial(Id_Emp, Id_Cd, Id_Cte, Id_prd, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
