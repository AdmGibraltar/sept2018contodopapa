using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_CatClienteProd
    {
        public void ConsultaClienteProd(ClienteProd clienteprod, string Conexion, ref List<CapaEntidad.ClienteProd> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { clienteprod.Id_Emp, clienteprod.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteProd_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    clienteprod = new ClienteProd();
                    clienteprod.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    clienteprod.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    clienteprod.Id_Clp = (string)dr.GetValue(dr.GetOrdinal("Id_Clp"));
                    clienteprod.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    clienteprod.Id_Prd = (int)dr.GetValue(dr.GetOrdinal("Id_Prd"));
                    clienteprod.Clp_descripcion = (string)dr.GetValue(dr.GetOrdinal("Clp_descripcion"));
                    clienteprod.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Clp_Activo")));
                    if (Convert.ToBoolean(clienteprod.Estatus))
                    {
                        clienteprod.EstatusStr = "Activo";
                    }
                    else
                    {
                        clienteprod.EstatusStr = "Inactivo";
                    }
                    List.Add(clienteprod);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                FacturaDet facturaDet = null;
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Prd_Lista" };
                object[] Valores = { id_Emp, id_Cd, id_Cte, lista_Id_prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteProd_FacturaEspecial_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    facturaDet = new FacturaDet();

                    facturaDet.Id_Emp = id_Emp;
                    facturaDet.Id_Cd = id_Cd;
                    facturaDet.Id_Fac = 0;
                    facturaDet.Id_FacDet = 0;
                    facturaDet.Id_CteExt = id_Cte;
                    facturaDet.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    facturaDet.Fac_Cant = 0; //depende de la factura original
                    facturaDet.Fac_Precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_Pesos"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Clp_Pesos")));

                    //datos del producto de la orden de compra
                    facturaDet.Producto = new Producto();
                    facturaDet.Producto.Id_PrdEsp  = Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_PrdEsp")));
                    facturaDet.Producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    facturaDet.Producto.Id_Emp = id_Emp;
                    facturaDet.Producto.Id_Cd = id_Cd;
                    facturaDet.Producto.Prd_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_descripcion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_descripcion")).ToString();
                    facturaDet.Producto.Prd_DescripcionEspecial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_descripcionEspecial"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_descripcionEspecial")).ToString();
                    facturaDet.Producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_Presentacion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_Presentacion")).ToString();
                    facturaDet.Producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_unidades"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_unidades")).ToString();
                    facturaDet.Clp_Release = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_Release"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_Release")).ToString();
                    facturaDet.Producto.Prd_ClaveProdServ = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_ClaveProdServ"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Prd_ClaveProdServ")).ToString();
                    facturaDet.Producto.Prd_ClaveUnidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_ClaveUnidad"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Prd_ClaveUnidad")).ToString();


                    listaFacturaProductos.Add(facturaDet);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                RemisionDet remisionDet = null;
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Prd_Lista" };
                object[] Valores = { id_Emp, id_Cd, id_Cte, lista_Id_prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteProd_FacturaEspecial_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    remisionDet = new RemisionDet();

                    remisionDet.Id_Emp = id_Emp;
                    remisionDet.Id_Cd = id_Cd;
                    remisionDet.Id_Rem = 0;
                    remisionDet.Id_RemDet = 0;
                    remisionDet.Id_CteExt = id_Cte;
                    remisionDet.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    remisionDet.Rem_Cant = 0; //depende de la factura original
                    remisionDet.Rem_Precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_Pesos"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Clp_Pesos")));

                    //datos del producto de la orden de compra
                    remisionDet.Producto = new Producto();
                    remisionDet.Producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    remisionDet.Producto.Id_PrdEsp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_PrdEsp"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Id_PrdEsp")).ToString();
                    remisionDet.Producto.Id_Emp = id_Emp;
                    remisionDet.Producto.Id_Cd = id_Cd;
                    remisionDet.Producto.Prd_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_descripcion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_descripcion")).ToString();
                    remisionDet.Producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_Presentacion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_Presentacion")).ToString();
                    remisionDet.Producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_unidades"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_unidades")).ToString();
                    remisionDet.Clp_Release = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_Release"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_Release")).ToString();
                    remisionDet.Producto.Prd_DescripcionEspecial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_descripcionespecial"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_descripcionespecial")).ToString();
                    listaRemisionProductos.Add(remisionDet);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                NotaCargoDet facturaDet = null;
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Prd_Lista" };
                object[] Valores = { id_Emp, id_Cd, id_Cte, lista_Id_prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteProd_FacturaEspecial_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    facturaDet = new NotaCargoDet();

                    facturaDet.Id_Emp = id_Emp;
                    facturaDet.Id_Cd = id_Cd;
                    facturaDet.Id_Nca = 0;
                    facturaDet.Id_NcaDet = 0;
                    facturaDet.Id_CteExt = id_Cte;
                    facturaDet.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    facturaDet.Nca_Cant = 0; //depende de la factura original
                    facturaDet.Nca_Precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_Pesos"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Clp_Pesos")));

                    //datos del producto de la orden de compra
                    facturaDet.Producto = new Producto();
                    facturaDet.Producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    facturaDet.Producto.Id_Emp = id_Emp;
                    facturaDet.Producto.Id_Cd = id_Cd;
                    facturaDet.Producto.Id_PrdEsp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_PrdEsp"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Id_PrdEsp")).ToString();
                    facturaDet.Producto.Prd_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_descripcion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_descripcion")).ToString();
                    facturaDet.Producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_Presentacion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_Presentacion")).ToString();
                    facturaDet.Producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_unidades"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_unidades")).ToString();
                    facturaDet.Producto.Prd_DescripcionEspecial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_descripcionespecial"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_descripcionespecial")).ToString();
                    facturaDet.Clp_Release = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_Release"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_Release")).ToString();

                    listaFacturaProductos.Add(facturaDet);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                NotaCreditoDet facturaDet = null;
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Prd_Lista" };
                object[] Valores = { id_Emp, id_Cd, id_Cte, lista_Id_prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteProd_FacturaEspecial_Consulta", ref dr, Parametros, Valores);
                
                while (dr.Read())
                {
                    facturaDet = new NotaCreditoDet();

                    facturaDet.Id_Emp = id_Emp;
                    facturaDet.Id_Cd = id_Cd;
                    facturaDet.Id_Ncr = 0;
                    facturaDet.Id_NcrDet = 0;
                    facturaDet.Id_CteExt = id_Cte;
                    facturaDet.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    facturaDet.Ncr_Cant = 0; //depende de la factura original
                    facturaDet.Ncr_Precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_Pesos"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Clp_Pesos")));

                    //datos del producto de la orden de compra
                    facturaDet.Producto = new Producto();
                    facturaDet.Producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    facturaDet.Producto.Id_Emp = id_Emp;
                    facturaDet.Producto.Id_Cd = id_Cd;
                    facturaDet.Producto.Id_PrdEsp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_PrdEsp"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Id_PrdEsp")).ToString();
                    facturaDet.Producto.Prd_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_descripcion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_descripcion")).ToString();
                    facturaDet.Producto.Prd_DescripcionEspecial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_descripcionEspecial"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_descripcionEspecial")).ToString();
                    facturaDet.Producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_Presentacion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_Presentacion")).ToString();

                    facturaDet.Producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_unidades"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_unidades")).ToString();
                    facturaDet.Clp_Release = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_Release"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Clp_Release")).ToString();

                    listaFacturaProductos.Add(facturaDet);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
		                                "@Id_Cd", 
		                                "@Id_Clp", 
		                                "@Id_Cte", 
		                                "@Id_Prd", 
		                                "@Clp_descripcion",
                                        "@Clp_Presentacion",
                                        "@Clp_Unidades",
                                        "@Clp_Cantidad",
                                        "@Clp_Activo"
                                      };
                object[] Valores = { 
                                        clienteprod.Id_Emp,
                                        clienteprod.Id_Cd,
                                        clienteprod.Id_Clp,
                                        clienteprod.Id_Cte,
                                        clienteprod.Id_Prd,
                                        clienteprod.Clp_descripcion,
                                        clienteprod.Clp_Presentacion,
                                        clienteprod.Unidades,
                                        clienteprod.CantFact,
                                        clienteprod.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteProd_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
		                                "@Id_Cd", 
		                                "@Id_Clp", 
		                                "@Id_Cte", 
		                                "@Id_Prd", 
		                                "@Clp_descripcion", 
                                        "@Clp_Presentacion",
                                        "@Clp_Unidades",
                                        "@Clp_Cantidad",
                                        "@Clp_Activo"
                                      };
                object[] Valores = { 
                                        clienteprod.Id_Emp,
                                        clienteprod.Id_Cd,
                                        clienteprod.Id_Clp,
                                        clienteprod.Id_Cte,
                                        clienteprod.Id_Prd,
                                        clienteprod.Clp_descripcion,
                                        clienteprod.Clp_Presentacion,
                                        clienteprod.Unidades,
                                        clienteprod.CantFact,
                                        clienteprod.Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteProd_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza descripcion de productos de factura especial
        /// </summary>
        /// <param name="listaFacturaProductos">lista de productos (partidas) de la factura especial</param>
        /// <param name="Conexion">cadena de conexión</param>
        /// <param name="verificador">verificador</param>
        /// <param name="soloDescripcion">Indica si se actualiza o no solo la descripcion del producto</param>
        public void ModificarClienteProdFacturaEspecial(List<FacturaDet> listaFacturaProductos, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlCommand sqlcmd = null;
            try
            {
                CapaDatos.StartTrans();

                // -----------------------------------------------------------------
                // Parametros de  detalle de factura epsecial y Cliente-Producto
                // -----------------------------------------------------------------
                string[] ParametrosCLienteProducto = { 
	                                    "@Id_Emp", 
		                                "@Id_Cd", 
		                                "@Id_Clp", 
		                                "@Id_Cte", 
		                                "@Id_Prd", 
		                                "@Clp_descripcion", 
		                                "@Clp_unidades", 
		                                "@Clp_FecUltVta", 
		                                "@Clp_Presentacion",
                                        "@Clp_Release",
		                                "@Clp_Cantidad", 
                                        "@Clp_InvFin", 
                                        "@Clp_Asignado", 
                                        "@Clp_Activo",
                                        "@Clp_Precio"
                                      };
                //Se unen partidas con el mismo producto, la descripcion de junta con separadores "|"
                for (int i = 0; i < listaFacturaProductos.Count; i++)
                {
                    FacturaDet remisionDet = listaFacturaProductos[i];
                    for (int j = i + 1; j < listaFacturaProductos.Count; j++)
                    {
                        FacturaDet remisionDet2 = listaFacturaProductos[j];
                        if (remisionDet.Id_Prd == remisionDet2.Id_Prd)
                        {
                            if (remisionDet2.Producto.Prd_DescripcionEspecial != "")
                            {
                                remisionDet.Producto.Prd_DescripcionEspecial = string.Concat(remisionDet.Producto.Prd_DescripcionEspecial, "|", remisionDet2.Producto.Prd_DescripcionEspecial);
                            }
                            listaFacturaProductos.RemoveAt(j);
                            j--;

                        }
                    }
                }
                foreach (FacturaDet remisionDet in listaFacturaProductos)
                {

                    // --------------------------------------
                    // Insertar detalle de Cliente-Producto
                    // --------------------------------------
                    object[] ValoresClienteProducto = { 
                                        remisionDet.Id_Emp
                                        ,remisionDet.Id_Cd
                                        ,remisionDet.Producto.Id_PrdEsp //0 //@Id_Clp: en el SP de genera el maximo consecutivo si es nuevo, y si ya existe solo se actualiza
                                        ,remisionDet.Id_CteExt //cliente de datos generales de la remision original
                                        ,remisionDet.Id_Prd
                                        ,remisionDet.Producto.Prd_DescripcionEspecial
                                        ,remisionDet.Producto.Prd_UniNe
                                        ,DateTime.Now
                                        ,remisionDet.Producto.Prd_Presentacion
                                        ,remisionDet.Clp_Release == null ? "" : remisionDet.Clp_Release
                                        ,remisionDet.Fac_Cant //cantidad remisionada, se suma si el cliente-producto ya existe
                                        ,0
                                        ,0
                                        ,true
                                        ,remisionDet.Fac_Precio
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteProdDescripcion_FacturaEspecial_Modificar", ref verificador, ParametrosCLienteProducto, ValoresClienteProducto);
                }
                CapaDatos.CommitTrans();
                if (sqlcmd != null)
                {
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza descripcion de productos de nota de cargo especial
        /// </summary>
        /// <param name="listaFacturaProductos">lista de productos (partidas) de la nota cargo especial</param>
        /// <param name="Conexion">cadena de conexión</param>
        /// <param name="verificador">verificador</param>
        /// <param name="soloDescripcion">Indica si se actualiza o no solo la descripcion del producto</param>
        public void ModificarClienteProdNCargpEspecial(List<NotaCargoDet> listaFacturaProductos, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlCommand sqlcmd = null;
            try
            {
                CapaDatos.StartTrans();

                // -----------------------------------------------------------------
                // Parametros de  detalle de factura epsecial y Cliente-Producto
                // -----------------------------------------------------------------
                string[] ParametrosCLienteProducto = { 
	                                    "@Id_Emp", 
		                                "@Id_Cd", 
		                                "@Id_Clp", 
		                                "@Id_Cte", 
		                                "@Id_Prd", 
		                                "@Clp_descripcion", 
		                                "@Clp_unidades", 
		                                "@Clp_FecUltVta", 
		                                "@Clp_Presentacion",
                                        "@Clp_Release",
		                                "@Clp_Cantidad", 
                                        "@Clp_InvFin", 
                                        "@Clp_Asignado", 
                                        "@Clp_Activo",
                                        "@Clp_Precio"
                                      };

                //Se unen partidas con el mismo producto, la descripcion de junta con separadores "|"
                for (int i = 0; i < listaFacturaProductos.Count; i++)
                {
                    NotaCargoDet facturaDet = listaFacturaProductos[i];
                    for (int j = i + 1; j < listaFacturaProductos.Count; j++)
                    {
                        NotaCargoDet facturaDet2 = listaFacturaProductos[j];
                        if (facturaDet.Id_Prd == facturaDet2.Id_Prd)
                        {
                            facturaDet.Producto.Prd_DescripcionEspecial = string.Concat(facturaDet.Producto.Prd_DescripcionEspecial, "|", facturaDet2.Producto.Prd_DescripcionEspecial);
                            listaFacturaProductos.RemoveAt(j);
                            j--;
                        }
                    }
                }

                foreach (NotaCargoDet facturaDet in listaFacturaProductos)
                {

                    // --------------------------------------
                    // Insertar detalle de Cliente-Producto
                    // --------------------------------------
                    object[] ValoresClienteProducto = { 
                                        facturaDet.Id_Emp
                                        ,facturaDet.Id_Cd
                                        ,0 //@Id_Clp: en el SP de genera el maximo consecutivo si es nuevo, y si ya existe solo se actualiza
                                        ,facturaDet.Id_CteExt //cliente de datos generales de la factura original
                                        ,facturaDet.Id_Prd
                                        ,facturaDet.Producto.Prd_DescripcionEspecial
                                        ,facturaDet.Producto.Prd_UniNe
                                        ,DateTime.Now
                                        ,facturaDet.Producto.Prd_Presentacion
                                        ,facturaDet.Clp_Release == null ? "" :facturaDet.Clp_Release 
                                        ,facturaDet.Nca_Cant //cantidad facturada, se suma si el cliente-producto ya existe
                                        ,0
                                        ,0
                                        ,true
                                        ,facturaDet.Nca_Precio
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteProdDescripcion_FacturaEspecial_Modificar", ref verificador, ParametrosCLienteProducto, ValoresClienteProducto);
                }


                CapaDatos.CommitTrans();
                if (sqlcmd != null)
                {
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarClienteProdNCreditoEspecial(List<NotaCreditoDet> listaFacturaProductos, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlCommand sqlcmd = null;
            try
            {
                CapaDatos.StartTrans();
                // -----------------------------------------------------------------
                // Parametros de  detalle de factura epsecial y Cliente-Producto
                // -----------------------------------------------------------------
                string[] ParametrosCLienteProducto = { 
	                                    "@Id_Emp", 
		                                "@Id_Cd", 
		                                "@Id_Clp", 
		                                "@Id_Cte", 
		                                "@Id_Prd", 
		                                "@Clp_descripcion", 
		                                "@Clp_unidades", 
		                                "@Clp_FecUltVta", 
		                                "@Clp_Presentacion",
                                        "@Clp_Release",
		                                "@Clp_Cantidad", 
                                        "@Clp_InvFin", 
                                        "@Clp_Asignado", 
                                        "@Clp_Activo",
                                        "@Clp_Precio"
                                      };

                //Se unen partidas con el mismo producto, la descripcion de junta con separadores "|"
                for (int i = 0; i < listaFacturaProductos.Count; i++)
                {
                    NotaCreditoDet facturaDet = listaFacturaProductos[i];
                    for (int j = i + 1; j < listaFacturaProductos.Count; j++)
                    {
                        NotaCreditoDet facturaDet2 = listaFacturaProductos[j];
                        if (facturaDet.Id_Prd == facturaDet2.Id_Prd)
                        {
                            facturaDet.Producto.Prd_DescripcionEspecial = string.Concat(facturaDet.Producto.Prd_DescripcionEspecial, "|", facturaDet2.Producto.Prd_DescripcionEspecial);
                            listaFacturaProductos.RemoveAt(j);
                            j--;
                        }
                    }
                }

                foreach (NotaCreditoDet facturaDet in listaFacturaProductos)
                {
                    // --------------------------------------
                    // Insertar detalle de Cliente-Producto
                    // --------------------------------------
                    object[] ValoresClienteProducto = { 
                                        facturaDet.Id_Emp
                                        ,facturaDet.Id_Cd
                                        ,facturaDet.Producto.Id_PrdEsp//0 //@Id_Clp: en el SP de genera el maximo consecutivo si es nuevo, y si ya existe solo se actualiza
                                        ,facturaDet.Id_CteExt //cliente de datos generales de la factura original
                                        ,facturaDet.Id_Prd
                                        ,facturaDet.Producto.Prd_DescripcionEspecial
                                        ,facturaDet.Producto.Prd_UniNe
                                        ,DateTime.Now
                                        ,facturaDet.Producto.Prd_Presentacion
                                        ,facturaDet.Clp_Release == null ? "":facturaDet.Clp_Release
                                        ,facturaDet.Ncr_Cant //cantidad facturada, se suma si el cliente-producto ya existe
                                        ,0
                                        ,0
                                        ,true
                                        ,facturaDet.Ncr_Precio
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteProdDescripcion_FacturaEspecial_Modificar", ref verificador, ParametrosCLienteProducto, ValoresClienteProducto);
                }
                CapaDatos.CommitTrans();
                if (sqlcmd != null)
                {
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarClienteProdRemisionEspecial(List<RemisionDet> listaRemisionProductos, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlCommand sqlcmd = null;
            try
            {
                CapaDatos.StartTrans();

                // -----------------------------------------------------------------
                // Parametros de  detalle de factura epsecial y Cliente-Producto
                // -----------------------------------------------------------------
                string[] ParametrosCLienteProducto = { 
	                                    "@Id_Emp", 
		                                "@Id_Cd", 
		                                "@Id_Clp", 
		                                "@Id_Cte", 
		                                "@Id_Prd", 
		                                "@Clp_descripcion", 
		                                "@Clp_unidades", 
		                                "@Clp_FecUltVta", 
		                                "@Clp_Presentacion",
                                        "@Clp_Release",
		                                "@Clp_Cantidad", 
                                        "@Clp_InvFin", 
                                        "@Clp_Asignado", 
                                        "@Clp_Activo",
                                        "@Clp_Precio"
                                      };
                //Se unen partidas con el mismo producto, la descripcion de junta con separadores "|"
                for (int i = 0; i < listaRemisionProductos.Count; i++)
                {
                    RemisionDet remisionDet = listaRemisionProductos[i];
                    for (int j = i + 1; j < listaRemisionProductos.Count; j++)
                    {
                        RemisionDet remisionDet2 = listaRemisionProductos[j];
                        if (remisionDet.Id_Prd == remisionDet2.Id_Prd)
                        {
                            remisionDet.Producto.Prd_DescripcionEspecial = string.Concat(remisionDet.Producto.Prd_DescripcionEspecial, "|", remisionDet2.Producto.Prd_DescripcionEspecial);
                            listaRemisionProductos.RemoveAt(j);
                            j--;
                        }
                    }
                }
                foreach (RemisionDet remisionDet in listaRemisionProductos)
                {

                    // --------------------------------------
                    // Insertar detalle de Cliente-Producto
                    // --------------------------------------
                    object[] ValoresClienteProducto = { 
                                        remisionDet.Id_Emp
                                        ,remisionDet.Id_Cd
                                        ,0 //@Id_Clp: en el SP de genera el maximo consecutivo si es nuevo, y si ya existe solo se actualiza
                                        ,remisionDet.Id_CteExt //cliente de datos generales de la remision original
                                        ,remisionDet.Id_Prd
                                        ,remisionDet.Producto.Prd_DescripcionEspecial
                                        ,remisionDet.Producto.Prd_UniNe
                                        ,DateTime.Now
                                        ,remisionDet.Producto.Prd_Presentacion
                                        ,remisionDet.Clp_Release == null ? "" : remisionDet.Clp_Release
                                        ,remisionDet.Rem_Cant //cantidad remisionada, se suma si el cliente-producto ya existe
                                        ,0
                                        ,0
                                        ,true
                                        ,remisionDet.Rem_Precio
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteProdDescripcion_FacturaEspecial_Modificar", ref verificador, ParametrosCLienteProducto, ValoresClienteProducto);
                }
                CapaDatos.CommitTrans();
                if (sqlcmd != null)
                {
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza o da de alta encabezado y productos de factura especial
        /// </summary>
        /// <param name="facturaEsp">Encabezado de factura especial</param>
        /// <param name="listaFacturaProductos">lista de productos (partidas) de la factura especial</param>
        /// <param name="Conexion">cadena de conexión</param>
        /// <param name="verificador">verificador</param>
        /// <param name="actualizar">Booleano True = modificacion de factura especial, False = Nueva factura especial </param>
        public void ModificarFacturaEspecial(ref FacturaEspecial facturaEsp, ref List<FacturaDet> listaFacturaProductos, string Conexion, ref int verificador, bool actualizar)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlCommand sqlcmd = null;
            try
            {
                CapaDatos.StartTrans();

                // --------------------------
                // Insertar factura epsecial
                // --------------------------
                string[] ParametrosFacturaEspecial = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                        ,"@Id_Ter"
                                        ,"@FacEsp_Fecha"
                                        ,"@FacEsp_Importe"
                                        ,"@FacEsp_SubTotal"
                                        ,"@FacEsp_ImporteIva"
                                        ,"@FacEsp_Total"
                                        ,"@actualizar"
                                      };
                object[] ValoresFacturaEspecial = { 
                                        facturaEsp.Id_Emp
                                        ,facturaEsp.Id_Cd
                                        ,facturaEsp.Id_Fac
                                        ,facturaEsp.Id_Ter
                                        ,facturaEsp.FacEsp_Fecha
                                        ,facturaEsp.FacEsp_Importe
                                        ,facturaEsp.FacEsp_SubTotal
                                        ,facturaEsp.FacEsp_ImporteIva
                                        ,facturaEsp.FacEsp_Total
                                        ,actualizar
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaEspecial_Insertar", ref verificador, ParametrosFacturaEspecial, ValoresFacturaEspecial);

                // -----------------------------------------------------------------
                // Parametros de  detalle de factura epsecial y Cliente-Producto
                // -----------------------------------------------------------------
                string[] ParametrosFacturaEspecialDetalle = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Fac"
                                        ,"@Id_FacDet"
                                        ,"@Id_Prd"
                                        ,"@Id_PrdEsp"
                                        ,"@FacEsp_Descripcion"
                                        ,"@FacEsp_Presentacion"
                                        ,"@FacEsp_Unidades"
                                        ,"@FacEsp_Release"
                                        ,"@Fac_Cant"
                                        ,"@Fac_Precio"
                                      };               
                int cont = 0;
                foreach (FacturaDet facturaDet in listaFacturaProductos)
                {
                    // --------------------------------------
                    // Insertar detalle de factura epsecial
                    // --------------------------------------
                    object[] ValoresFacturaEspecialDetalle = { 
                                                        facturaDet.Id_Emp
                                                        ,facturaDet.Id_Cd
                                                        ,facturaEsp.Id_Fac
                                                        ,cont
                                                        ,facturaDet.Id_Prd
                                                        ,facturaDet.Producto.Id_PrdEsp
                                                        ,facturaDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries).Length > 0 ? facturaDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries)[0] : ""
                                                        ,facturaDet.Producto.Prd_Presentacion
                                                        ,facturaDet.Producto.Prd_UniNe
                                                        ,facturaDet.Clp_Release
                                                        ,facturaDet.Fac_Cant //cantidad facturada
                                                        ,facturaDet.Fac_Precio
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaEspecialDetalle_Insertar"
                        , ref verificador, ParametrosFacturaEspecialDetalle, ValoresFacturaEspecialDetalle);
                    cont++; //Aumenta contador de partida
                }
                CapaDatos.CommitTrans();
                if (sqlcmd != null)
                {
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza o da de alta encabezado y productos de factura especial
        /// </summary>
        /// <param name="facturaEsp">Encabezado de factura especial</param>
        /// <param name="listaFacturaProductos">lista de productos (partidas) de la factura especial</param>
        /// <param name="Conexion">cadena de conexión</param>
        /// <param name="verificador">verificador</param>
        /// <param name="actualizar">Booleano True = modificacion de factura especial, False = Nueva factura especial </param>
        public void ModificarNCargoEspecial(ref FacturaEspecial facturaEsp, ref List<NotaCargoDet> listaFacturaProductos, string Conexion, ref int verificador, int actualizar)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlCommand sqlcmd = null;
            try
            {
                CapaDatos.StartTrans();

                // --------------------------
                // Insertar nota cargo especial
                // --------------------------
                string[] ParametrosNCargoEspecial = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Nca"
                                        ,"@Id_Ter"
                                        ,"@NcaEsp_Fecha"
                                        ,"@NcaEsp_Importe"
                                        ,"@NcaEsp_SubTotal"
                                        ,"@NcaEsp_ImporteIva"
                                        ,"@NcaEsp_Total"
                                        ,"@actualizar"
                                      };
                object[] ValoresNCargoEspecial = { 
                                        facturaEsp.Id_Emp
                                        ,facturaEsp.Id_Cd
                                        ,facturaEsp.Id_Fac
                                        ,facturaEsp.Id_Ter
                                        ,facturaEsp.FacEsp_Fecha
                                        ,facturaEsp.FacEsp_Importe
                                        ,facturaEsp.FacEsp_SubTotal
                                        ,facturaEsp.FacEsp_ImporteIva
                                        ,facturaEsp.FacEsp_Total
                                        ,actualizar
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCargoEspecial_Insertar", ref verificador, ParametrosNCargoEspecial, ValoresNCargoEspecial);



                // -----------------------------------------------------------------
                // Parametros de  detalle de factura epsecial y Cliente-Producto
                // -----------------------------------------------------------------
                string[] ParametrosNCargoEspecialDetalle = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Nca"
                                        ,"@Id_NcaDet"
                                        ,"@Id_Prd"
                                        ,"@NcaEsp_Descripcion"
                                        ,"@NcaEsp_Presentacion"
                                        ,"@NcaEsp_Unidades"
                                        ,"@NcaEsp_Release"
                                        ,"@Nca_Importe"
                                        ,"@Id_PrdEsp"
                                      };
                string[] ParametrosCLienteProducto = { 
	                                    "@Id_Emp", 
		                                "@Id_Cd", 
		                                "@Id_Clp", 
		                                "@Id_Cte", 
		                                "@Id_Prd", 
		                                "@Clp_descripcion", 
		                                "@Clp_unidades", 
		                                "@Clp_FecUltVta", 
		                                "@Clp_Presentacion",
                                        "@Clp_Release",
		                                "@Clp_Cantidad", 
                                        "@Clp_InvFin", 
                                        "@Clp_Asignado", 
                                        "@Clp_Activo",
                                        "@Clp_Precio"
                                      };
                int cont = 0;
                foreach (NotaCargoDet facturaDet in listaFacturaProductos)
                {
                    // --------------------------------------
                    // Insertar detalle de nota cargo epsecial
                    // --------------------------------------
                    object[] ValoresFacturaEspecialDetalle = { 
                                                        facturaDet.Id_Emp
                                                        ,facturaDet.Id_Cd
                                                        ,facturaEsp.Id_Fac
                                                        ,cont
                                                        ,facturaDet.Id_Prd
                                                        ,facturaDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries).Length > 0 ? facturaDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries)[0] : ""
                                                        ,facturaDet.Producto.Prd_Presentacion
                                                        ,facturaDet.Producto.Prd_UniNe
                                                        ,facturaDet.Clp_Release == null ? "" : facturaDet.Clp_Release
                                                        ,facturaDet.Nca_Importe
                                                        ,facturaDet.Producto.Id_PrdEsp
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCargoEspecialDetalle_Insertar"
                        , ref verificador, ParametrosNCargoEspecialDetalle, ValoresFacturaEspecialDetalle);
                    cont++; //Aumenta contador de partida
                }
                CapaDatos.CommitTrans();
                if (sqlcmd != null)
                {
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarNCreditoEspecial(ref FacturaEspecial facturaEsp, ref List<NotaCreditoDet> listaFacturaProductos, string Conexion, ref int verificador, int actualizar)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlCommand sqlcmd = null;
            try
            {
                CapaDatos.StartTrans();
                // --------------------------
                // Insertar nota cargo especial
                // --------------------------
                string[] ParametrosNCargoEspecial = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Ncr"
                                        ,"@Id_Ter"
                                        ,"@NcrEsp_Fecha"
                                        ,"@NcrEsp_Importe"
                                        ,"@NcrEsp_SubTotal"
                                        ,"@NcrEsp_ImporteIva"
                                        ,"@NcrEsp_Total"
                                        ,"@acualizar"
                                      };
                object[] ValoresNCargoEspecial = { 
                                        facturaEsp.Id_Emp
                                        ,facturaEsp.Id_Cd
                                        ,facturaEsp.Id_Fac
                                        ,facturaEsp.Id_Ter
                                        ,facturaEsp.FacEsp_Fecha
                                        ,facturaEsp.FacEsp_Importe
                                        ,facturaEsp.FacEsp_SubTotal
                                        ,facturaEsp.FacEsp_ImporteIva
                                        ,facturaEsp.FacEsp_Total
                                        ,actualizar
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCreditoEspecial_Insertar", ref verificador, ParametrosNCargoEspecial, ValoresNCargoEspecial);

                // -----------------------------------------------------------------
                // Parametros de  detalle de factura especial y Cliente-Producto
                // -----------------------------------------------------------------
                string[] ParametrosNCreditoEspecialDetalle = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Ncr"
                                        ,"@Id_NcrDet"
                                        ,"@Id_Prd"
                                        ,"@NcrEsp_Descripcion"
                                        ,"@NcrEsp_Presentacion"
                                        ,"@NcrEsp_Unidades"
                                        ,"@NcrEsp_Release"
                                        ,"@Ncr_Importe",
                                        "@Id_PrdEsp",
                                        "@Id_NcrSerie"
                                      };
                string[] ParametrosCLienteProducto = { 
	                                    "@Id_Emp", 
		                                "@Id_Cd", 
		                                "@Id_Clp", 
		                                "@Id_Cte", 
		                                "@Id_Prd", 
		                                "@Clp_descripcion", 
		                                "@Clp_unidades", 
		                                "@Clp_FecUltVta", 
		                                "@Clp_Presentacion",
                                        "@Clp_Release",
		                                "@Clp_Cantidad", 
                                        "@Clp_InvFin", 
                                        "@Clp_Asignado", 
                                        "@Clp_Activo",
                                        "@Clp_Precio"
                                      };
                int cont = 0;
                foreach (NotaCreditoDet facturaDet in listaFacturaProductos)
                {
                    // --------------------------------------
                    // Insertar detalle de nota credito epsecial
                    // --------------------------------------
                    if (facturaDet.Producto.Prd_DescripcionEspecial == null)
                    {
                        facturaDet.Producto.Prd_DescripcionEspecial = "";
                    }
                    object[] ValoresFacturaEspecialDetalle = { 
                                                        facturaDet.Id_Emp
                                                        ,facturaDet.Id_Cd
                                                        ,facturaEsp.Id_Fac
                                                        ,cont
                                                        ,facturaDet.Id_Prd
                                                        ,facturaDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries).Length > 0 ? facturaDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries)[0] : ""
                                                        ,facturaDet.Producto.Prd_Presentacion
                                                        ,facturaDet.Producto.Prd_UniNe
                                                        ,facturaDet.Clp_Release
                                                        ,facturaDet.Ncr_Importe
                                                       ,facturaDet.Producto.Id_PrdEsp
                                                       ,facturaEsp.Id_FacSerie
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapNCreditoEspecialDetalle_Insertar"
                        , ref verificador, ParametrosNCreditoEspecialDetalle, ValoresFacturaEspecialDetalle);

                    cont++; //Aumenta contador de partida
                }

                CapaDatos.CommitTrans();
                if (sqlcmd != null)
                {
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarRemisionEspecial(ref FacturaEspecial facturaEsp, ref List<RemisionDet> listaRemisionProductos, string Conexion, ref int verificador, int actualizar)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            SqlCommand sqlcmd = null;
            try
            {
                CapaDatos.StartTrans();

                // --------------------------
                // Insertar remision especial
                // --------------------------
                string[] ParametrosRemisionEspecial = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Rem"
                                        ,"@Id_Ter"
                                        ,"@RemEsp_Fecha"
                                        ,"@RemEsp_Importe"
                                        ,"@RemEsp_SubTotal"
                                        ,"@RemEsp_ImporteIva"
                                        ,"@RemEsp_Total"
                                        ,"@actualizar"
                                      };
                object[] ValoresRemisionEspecial = { 
                                        facturaEsp.Id_Emp
                                        ,facturaEsp.Id_Cd
                                        ,facturaEsp.Id_Fac
                                        ,facturaEsp.Id_Ter
                                        ,facturaEsp.FacEsp_Fecha
                                        ,facturaEsp.FacEsp_Importe
                                        ,facturaEsp.FacEsp_SubTotal
                                        ,facturaEsp.FacEsp_ImporteIva
                                        ,facturaEsp.FacEsp_Total
                                        ,actualizar
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemisionEspecial_Insertar", ref verificador, ParametrosRemisionEspecial, ValoresRemisionEspecial);

                // -----------------------------------------------------------------
                // Parametros de  detalle de factura epsecial y Cliente-Producto
                // -----------------------------------------------------------------
                string[] ParametrosRemisionEspecialDetalle = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Rem"
                                        ,"@Id_RemDet"
                                        ,"@Id_Prd"
                                        ,"@RemEsp_Descripcion"
                                        ,"@RemEsp_Presentacion"
                                        ,"@RemEsp_Unidades"
                                        ,"@RemEsp_Release"
                                        ,"@Rem_Cant"
                                        ,"@Rem_Precio"
                                        ,"@Id_PrdEsp"
                                      };

                int cont = 0;
                foreach (RemisionDet remisionDet in listaRemisionProductos)
                {
                    // --------------------------------------
                    // Insertar detalle de nota remision especial
                    // --------------------------------------
                    object[] ValoresFacturaEspecialDetalle = { 
                                                        remisionDet.Id_Emp
                                                        ,remisionDet.Id_Cd
                                                        ,facturaEsp.Id_Fac
                                                        ,cont
                                                        ,remisionDet.Id_Prd
                                                       ,remisionDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries).Length > 0 ? remisionDet.Producto.Prd_DescripcionEspecial.Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries)[0] : ""
                                                        ,remisionDet.Producto.Prd_Presentacion
                                                        ,remisionDet.Producto.Prd_UniNe
                                                        ,remisionDet.Clp_Release == null ? "": remisionDet.Clp_Release
                                                        ,remisionDet.Rem_Cant //cantidad remisionada

                                                        ,remisionDet.Rem_Precio
                                                        ,remisionDet.Producto.Id_PrdEsp
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemisionEspecialDetalle_Insertar"
                        , ref verificador, ParametrosRemisionEspecialDetalle, ValoresFacturaEspecialDetalle);
                    cont++; //Aumenta contador de partida
                }
                CapaDatos.CommitTrans();
                if (sqlcmd != null)
                {
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultarClienteDet(ClienteProd clienteprod, string Conexion, ref System.Data.DataTable dt)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Prd" };
                object[] Valores = { clienteprod.Id_Emp, clienteprod.Id_Cd, clienteprod.Id_Cte, clienteprod.Id_Prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteProdDet_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {

                    if (dr.GetValue(dr.GetOrdinal("Id_ClpDet")) != DBNull.Value)
                    {
                        dt.Rows.Add(new object[] {
                        dr.GetValue(dr.GetOrdinal("Id_ClpDet")),
                        dr.GetValue(dr.GetOrdinal("Id_Precio")),
                        dr.GetValue(dr.GetOrdinal("PrecioStr")),
                        Precio((int)dr.GetValue(dr.GetOrdinal("Id_Precio"))),
                        dr.GetValue(dr.GetOrdinal("Clp_Pesos"))
                    });
                    }
                    clienteprod.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Clp_Activo")));
                    clienteprod.Clp_descripcion = dr.GetValue(dr.GetOrdinal("Clp_descripcion")).ToString();
                    clienteprod.Id_Clp = dr.GetValue(dr.GetOrdinal("Id_Clp")).ToString();
                    clienteprod.Unidades = dr.GetValue(dr.GetOrdinal("Clp_unidades")).ToString();
                    clienteprod.Clp_Presentacion = dr.GetValue(dr.GetOrdinal("Clp_Presentacion")).ToString();
                    clienteprod.CantFact = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CantFact")));
                    clienteprod.Clp_FecUltVta = dr.IsDBNull(dr.GetOrdinal("Clp_FecUltVta")) ? (DateTime?)null : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Clp_FecUltVta")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private object Precio(int p)
        {
            switch (p)
            {
                case 0: return "Publico";
                case 1: return "Distribuidor";
                case 2: return "Estándar";
                default: return "";
            }
        }

        public void InsertarClienteProdDet(ClienteProd clienteprod, System.Data.DataTable dt, string Conexion)
        {

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            int verificador = 0;
            try
            {

                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
                                        "@Id_Cte",
	                                    "@Id_Clp", 
                                        "@Id_ClpDet",
                                        "@Id_Prd",
	                                    "@Id_Precio", 
	                                    "@Clp_Pesos",
                                        "@Accion"
                                      };

                object[] Valores = null;
                SqlCommand sqlcmd = null;

                if (dt.Rows.Count > 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        Valores = new object[] { 
                                        clienteprod.Id_Emp,
                                        clienteprod.Id_Cd,
                                        clienteprod.Id_Cte,
                                        clienteprod.Id_Clp,
                                        x,
                                        clienteprod.Id_Prd,
                                        dt.Rows.Count>0? dt.Rows[x]["Tprecio"]:0,
                                        dt.Rows.Count>0?dt.Rows[x]["Pesos"]:0,
                                        x
                                   };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteProdDet_Insertar", ref verificador, Parametros, Valores);
                    }
                }
                else
                {
                    Valores = new object[] { 
                                        clienteprod.Id_Emp,
                                        clienteprod.Id_Cd,
                                        clienteprod.Id_Cte,
                                        clienteprod.Id_Clp,
                                        0,
                                        clienteprod.Id_Prd,
                                        0,
                                        0,
                                        0
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteProdDet_Insertar", ref verificador, Parametros, Valores);
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }

        }

        public void ClienteProductoPrecioPublico_Consultar(ref ClienteProd clienteprod, string Conexion, ref float precioPublico)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Prd", "@Id_Vap" };
                object[] Valores = { clienteprod.Id_Emp, clienteprod.Id_Cd, clienteprod.Id_Cte, clienteprod.Id_Prd, clienteprod.Id_Vap };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPrecioEspecialCatalogoProductoCliente_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    precioPublico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("precioPublico"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("precioPublico")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp"
                                          ,"@Id_Cd"
                                          ,"@Id_Cte"
                                          ,"@Id_Prd"
                                      };

                object[] Valores = { 
                                       clienteprod.Id_Emp
                                       ,clienteprod.Id_Cd // id_cd_ver
                                       ,clienteprod.Id_Cte
                                       ,clienteprod.Id_Prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteProducto_ConsultaPrecios", ref dr, Parametros, Valores);

                precio = -1;
                while (dr.Read())
                {
                    precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_Pesos"))) ? -1 : dr.GetDouble(dr.GetOrdinal("Clp_Pesos"));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
        public void ConsultaClienteProdPrecioEspecialibt(ClienteProd clienteprod, ICD_Contexto Conexion, ref double precio)
        {//rm
            try
            {
                SqlDataReader dr = null;
                //CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp"
                                          ,"@Id_Cd"
                                          ,"@Id_Cte"
                                          ,"@Id_Prd"
                                      };

                object[] Valores = { 
                                       clienteprod.Id_Emp
                                       ,clienteprod.Id_Cd // id_cd_ver
                                       ,clienteprod.Id_Cte
                                       ,clienteprod.Id_Prd
                                   };

                SqlCommand sqlcmd = CD_Datos.GenerarSqlCommand("spCatClienteProducto_ConsultaPrecios", ref dr, Parametros, Valores, Conexion);

                precio = -1;
                while (dr.Read())
                {
                    precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Clp_Pesos"))) ? -1 : dr.GetDouble(dr.GetOrdinal("Clp_Pesos"));
                }

                sqlcmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
