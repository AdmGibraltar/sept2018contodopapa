using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CatClienteProd
    {

        public void ConsultaClienteProd(ClienteProd clienteprod, string Conexion, ref List<CapaEntidad.ClienteProd> List)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ConsultaClienteProd(clienteprod, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteProd_FacturaEspecial(ref List<FacturaDet> listaFacturaProductos, string Conexion, int id_Emp, int id_Cd, int id_Cte, string lista_Id_prd)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ConsultaClienteProd_FacturaEspecial(ref listaFacturaProductos, Conexion, id_Emp, id_Cd, id_Cte, lista_Id_prd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteProd_NCargoEspecial(ref List<NotaCargoDet> listaFacturaProductos, string Conexion, int id_Emp, int id_Cd, int id_Cte, string lista_Id_prd)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ConsultaClienteProd_NCargoEspecial(ref listaFacturaProductos, Conexion, id_Emp, id_Cd, id_Cte, lista_Id_prd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaClienteProd_NCreditoEspecial(ref List<NotaCreditoDet> listaFacturaProductos, string Conexion, int id_Emp, int id_Cd, int id_Cte, string lista_Id_prd)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ConsultaClienteProd_NCreditoEspecial(ref listaFacturaProductos, Conexion, id_Emp, id_Cd, id_Cte, lista_Id_prd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaClienteProd_RemisionEspecial(ref List<RemisionDet> listaRemisionProductos, string Conexion, int id_Emp, int id_Cd, int id_Cte, string lista_Id_prd)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ConsultaClienteProd_RemisionEspecial(ref listaRemisionProductos, Conexion, id_Emp, id_Cd, id_Cte, lista_Id_prd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarClienteProd(ClienteProd clienteprod, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.InsertarClienteProd(clienteprod, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarClienteProd(ClienteProd clienteprod, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ModificarClienteProd(clienteprod, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteProdDet(ClienteProd clientedet, string Conexion, ref System.Data.DataTable dt)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ConsultarClienteDet(clientedet, Conexion, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarClienteProdDet(ClienteProd clienteprod, DataTable dt, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.InsertarClienteProdDet(clienteprod, dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarClienteProdFacturaEspecial(List<FacturaDet> listaFacturaProductos, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ModificarClienteProdFacturaEspecial(listaFacturaProductos, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarClienteProdNCargoEspecial(List<NotaCargoDet> listaNCargoProductos, string Conexion, ref int verificador)
        {
            try
            {       
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ModificarClienteProdNCargpEspecial(listaNCargoProductos, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarClienteProdNCreditoEspecial(  List<NotaCreditoDet> listaNCargoProductos, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ModificarClienteProdNCreditoEspecial(  listaNCargoProductos, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarClienteProdRemisionEspecial(  List<RemisionDet> listaRemisionProductos, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ModificarClienteProdRemisionEspecial(  listaRemisionProductos, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFacturaEspecial(ref FacturaEspecial facturaEsp, ref List<FacturaDet> listaFacturaProductos, string Conexion, ref int verificador, bool actualizar)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ModificarFacturaEspecial(ref facturaEsp, ref listaFacturaProductos, Conexion, ref verificador, actualizar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarNCargoEspecial(ref FacturaEspecial facturaEsp, ref List<NotaCargoDet> listaFacturaProductos, string Conexion, ref int verificador, int actualizar)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ModificarNCargoEspecial(ref facturaEsp, ref listaFacturaProductos, Conexion, ref verificador, actualizar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarNCreditoEspecial(ref FacturaEspecial facturaEsp, ref List<NotaCreditoDet> listaFacturaProductos, string Conexion, ref int verificador, int actualizar)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ModificarNCreditoEspecial(ref facturaEsp, ref listaFacturaProductos, Conexion, ref verificador, actualizar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarRemisionEspecial(ref FacturaEspecial facturaEsp, ref List<RemisionDet> listaRemisionProductos, string Conexion, ref int verificador, int actualizar)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ModificarRemisionEspecial(ref facturaEsp, ref listaRemisionProductos, Conexion, ref verificador, actualizar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarClienteProdDet(ClienteProd clienteprod, DataTable dt, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.InsertarClienteProdDet(clienteprod, dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ClienteProductoPrecioPublico_Consultar(ref ClienteProd clienteprod, string Conexion, ref float precioPublico)
        {
            try
            {
                CD_CatClienteProd claseCapaDatos = new CD_CatClienteProd();
                claseCapaDatos.ClienteProductoPrecioPublico_Consultar(ref clienteprod, Conexion, ref precioPublico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Recibe ClienteProd (id_emp,id_cd,id_cte,id_prd) y regresa el precio 
        /// especial(CatClienteProdDet.Id_Precio=2). Regresa -1 en caso de que no exista.
        /// </summary>
        /// <param name="clienteprod"></param>
        /// <param name="Conexion"></param>
        /// <param name="precio"></param>
        public void ConsultaClienteProdPrecioEspecial(ClienteProd clienteprod, string Conexion, ref double precio)
        {//rm
            try
            {
                new CD_CatClienteProd().ConsultaClienteProdPrecioEspecial(clienteprod, Conexion, ref precio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         
        /// <summary>
        /// Recibe ClienteProd (id_emp,id_cd,id_cte,id_prd) y regresa el precio 
        /// especial(CatClienteProdDet.Id_Precio=2). Regresa -1 en caso de que no exista.
        /// </summary>
        /// <param name="clienteprod"></param>
        /// <param name="Conexion"></param>
        /// <param name="precio"></param>
        /// <param name="ibt">Transacción de la capa de negocio</param>

        public void ConsultaClienteProdPrecioEspecialibt(ClienteProd clienteprod, ref double precio, IBusinessTransaction ibt)
        {//rm
            try
            {
                new CD_CatClienteProd().ConsultaClienteProdPrecioEspecialibt(clienteprod, ibt.DataContext, ref precio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
