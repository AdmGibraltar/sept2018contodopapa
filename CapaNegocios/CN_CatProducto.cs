using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CapaEntidad;
using CapaDatos;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CatProducto
    {
        public void ConsultaProducto_OrdenCompra(ref Producto producto, string Conexion, int id_Ord, int id_Prd, int id_Emp, int id_Cd)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProducto_OrdenCompra(ref producto, Conexion, id_Ord, id_Prd, id_Emp, id_Cd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Versión que acepta una transacción de la capa de negocio
        /// </summary>
        /// <param name="Id_Prd"></param>
        /// <param name="Conexion"></param>
        /// <param name="EsPapel"></param>
        /// <param name="Prd_PesosConTecnico"></param>
        /// <param name="Prd_Mes"></param>
        /// <param name="Prd_PesosAAA"></param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        public void CatProducto_Informacion_VP(int Id_Prd, string Conexion, ref string EsPapel, ref double Prd_PesosConTecnico, ref Int32 Prd_Mes, ref double Prd_PesosAAA, IBusinessTransaction ibt)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.CatProducto_Informacion_VP(Id_Prd, Conexion, ref EsPapel, ref Prd_PesosConTecnico, ref Prd_Mes, ref Prd_PesosAAA, ibt.DataContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CatProducto_Informacion_VP(int Id_Prd, string Conexion, ref string EsPapel, ref double Prd_PesosConTecnico, ref Int32 Prd_Mes, ref double Prd_PesosAAA)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.CatProducto_Informacion_VP(Id_Prd, Conexion, ref EsPapel, ref Prd_PesosConTecnico, ref Prd_Mes, ref Prd_PesosAAA);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaProducto(ref Producto producto, string Conexion, int id_Emp, int id_Cd, string filtro, ref List<Producto> list, object Activo)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaListaProducto(ref producto, Conexion, id_Emp, id_Cd, filtro, ref list, Activo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaListaProductoSpo(ref Producto producto, string Conexion, int id_Emp, int id_Cd, string filtro, ref List<Producto> list, object Activo)
        {//rm lista de productos sistema de propietarios
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaListaProductoSpo(ref producto, Conexion, id_Emp, id_Cd, filtro, ref list, Activo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaListaProductoFacturacion(ref Producto producto, string Conexion, int id_Emp, int id_Cd, int id_Ter, string filtro, ref List<Producto> List, object Activo)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaListaProductoFacturacion(ref producto, Conexion, id_Emp, id_Cd, id_Ter, filtro, ref List, Activo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaProducto(ref Producto producto, string Conexion, int id_Emp, int id_Cd, int id_Cd_Ver, int id_Prd, int ValidaInv)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProducto(ref producto, Conexion, id_Emp, id_Cd, id_Cd_Ver, id_Prd, ValidaInv);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        // 13 Jul 2018 RFH
        //
        public void Consulta_Producto(ref Producto producto, string Conexion, int id_Emp, int id_Cd, int id_Cd_Ver, int id_Prd, int ValidaInv)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.Consulta_Producto(ref producto, Conexion, id_Emp, id_Cd, id_Cd_Ver, id_Prd, ValidaInv);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaProductoInventario(ref int verificador, string Conexion, int id_Emp, int id_Cd, int Id_Es, int Es_Naturaleza, int Id_EsDet)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProductoInventario(ref verificador, Conexion, id_Emp, id_Cd, Id_Es, Es_Naturaleza, Id_EsDet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaProducto(ref Producto producto, string Conexion, int id_Emp, int id_Cd, int id_Cd_Ver, int id_Prd, bool catalogo)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProducto(ref producto, Conexion, id_Emp, id_Cd, id_Cd_Ver, id_Prd, catalogo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaProducto(ref Producto producto, string Conexion, int id_Emp, int Id_Cd, int id_Prd)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProducto(ref producto, Conexion, id_Emp, Id_Cd, id_Prd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarProducto(Producto producto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.InsertarProducto(producto, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarProducto(Producto producto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ModificarProducto(producto, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProductoCte(ref Producto producto, string Conexion, int cliente)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProductoCte(ref producto, Conexion, cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaProducto_Descripcion(int Id_Prd, ref string Prd_Descripcion, string Conexion)
        {
            try
            {
                CD_CatProducto cd_prd = new CD_CatProducto();
                cd_prd.ConsultaProducto_Descripcion(Id_Prd, ref Prd_Descripcion, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public void ConsultarMaxLocal(int Id_Cd, int Id_Emp, string Conexion, ref int max)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultarMaxLocal(Id_Cd, Id_Emp, ref max, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProducto_Disponible(int empresa, int Cd, string Prd, ref List<int> Actuales, string Conexion)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProducto_Disponible(empresa, Cd, Prd, ref Actuales, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProductoAsig_Admin(Producto prd, string Conexion, ref List<Producto> List)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProductoAsig_Admin(prd, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaAsignPedxPrd(ProductoDet prdDet, string Conexion, ref List<ProductoDet> List)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaAsignPedxPrd(prdDet, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AsignarPedXPrd(Pedido pedido, List<ProductoDet> list, string Conexion, ref int verificador, int asignable, int Id_Prd)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.AsignarPedXPrd(pedido, Conexion, list, ref verificador, asignable, Id_Prd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public void ConsultaProductoCte_Lista(Producto prd, string Conexion, int Id_Cte, int Id_Acs, ref List<Producto> list)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaListaProducto(prd, Conexion, Id_Cte, Id_Acs, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultarVentas(ref Producto pr, int Id_Cte, string Conexion)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultarVentas(pr, Conexion, Id_Cte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaProductoAgrupador(Producto prd, int Id_Acs, string Conexion, ref List<Comun> list)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaListaProducto(prd, Id_Acs, Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaBuscar(Producto prd, string Conexion, ref List<Comun> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaBuscar(prd, Conexion, ref List, FiltroId, FiltroDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProductos(ref Producto producto, string Conexion, int id_Emp, int Id_Cd, int id_Prd, ref int productoNuevo)
        {
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                claseCapaDatos.ConsultaProductos(ref producto, Conexion, id_Emp, Id_Cd, id_Prd, ref productoNuevo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Determina si el producto es "no facturable"
        /// </summary>
        /// <param name="p">Producto</param>
        /// <param name="conexion">Cadena de conexión</param>
        /// <returns>true en caso de que el producto sea no facturable; false en caso contrario</returns>
        public bool EsProductoNoFacturable(Producto p, string conexion)
        {
            bool noFacturable = false;
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                noFacturable = claseCapaDatos.EsProductoNoFacturable(p.Id_Emp, p.Id_Cd, p.Id_Prd, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return noFacturable;
        }

        /// <summary>
        /// Determina si el producto es "no facturable"
        /// </summary>
        /// <param name="p">Producto</param>
        /// <param name="conexion">Cadena de conexión</param>
        /// <returns>true en caso de que el producto sea no facturable; false en caso contrario</returns>
        public bool EsProductoNoFacturable(int idEmp, int idCd, int idPrd, string conexion)
        {
            bool noFacturable = false;
            try
            {
                CD_CatProducto claseCapaDatos = new CD_CatProducto();
                noFacturable = claseCapaDatos.EsProductoNoFacturable(idEmp, idCd, idPrd, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return noFacturable;
        }

        /// <summary>
        /// Devuelve la instancia de la entidad [CatProducto], dado el identificador de producto.
        /// </summary>
        /// <param name="s">Sesion del usuario en operación</param>
        /// <param name="idPrd">Identificador del producto</param>
        /// <returns>CatProducto. Instancia de la entidad [CatProducto] en caso de que el producto exista; null en caso contrario.</returns>
        public CatProducto ObtenerPorId(Sesion s, int idPrd)
        {
            CD_CatProducto cdCatProducto = new CD_CatProducto();
            return cdCatProducto.ConsultarPorId(s.Id_Emp, s.Id_Cd, idPrd, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Devuelve la instancia de la entidad [CatProducto], dado el identificador de producto.
        /// </summary>
        /// <param name="s">Sesion del usuario en operación</param>
        /// <param name="idPrd">Identificador del producto</param>
        /// <returns>CatProducto. Instancia de la entidad [CatProducto] en caso de que el producto exista; null en caso contrario.</returns>
        public CatProducto ObtenerPorId(Sesion s, int idPrd, IBusinessTransaction ibt)
        {
            CD_CatProducto cdCatProducto = new CD_CatProducto();
            return cdCatProducto.ConsultarPorId(s.Id_Emp, s.Id_Cd, idPrd, ibt.DataContext);
        }
    }
}