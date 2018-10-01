using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;


namespace CapaDatos
{
    public class CD_ProductoPrecios
    {
        public void ConsultaListaProductoPrecios(ProductoPrecios productoPrecios, string Conexion, ref List<ProductoPrecios> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd" };
                object[] Valores = { productoPrecios.Id_Emp, productoPrecios.Id_Cd, productoPrecios.Id_Prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecios_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    productoPrecios = new ProductoPrecios();
                    productoPrecios.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    productoPrecios.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    productoPrecios.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    productoPrecios.Id_Pre = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pre")));
                    productoPrecios.Prd_Actual = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Actual"))) ? false : dr.GetBoolean(dr.GetOrdinal("Prd_Actual"));
                    productoPrecios.Prd_FechaInicio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_FechaInicio"))) ? (object)null : dr.GetDateTime(dr.GetOrdinal("Prd_FechaInicio"));
                    productoPrecios.Prd_FechaFin = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_FechaFin"))) ? (object)null : dr.GetDateTime(dr.GetOrdinal("Prd_FechaFin"));
                    productoPrecios.Prd_PreDescripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_PreDescripcion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Prd_PreDescripcion"));
                    productoPrecios.Pre_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pre_Descripcion"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Pre_Descripcion"));
                    productoPrecios.Prd_Pesos = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Pesos"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Pesos")));

                    List.Add(productoPrecios);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarProductoPrecios(ProductoPrecios productoPrecios, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
                                        "@Id_Cd", 
	                                    "@Id_Prd", 
                                        "@Id_Pre",
                                        "@Prd_Actual",
                                        "@Prd_FechaInicio",
                                        "@Prd_FechaFin",
                                        "@Prd_PreDescripcion",
                                        "@Prd_Pesos"
                                      };
                object[] Valores = { 
                                        productoPrecios.Id_Emp
                                        ,productoPrecios.Id_Cd
                                        ,productoPrecios.Id_Prd
                                        ,productoPrecios.Id_Pre
                                        ,productoPrecios.Prd_Actual
                                        ,productoPrecios.Prd_FechaInicio
                                        ,productoPrecios.Prd_FechaFin
                                        ,productoPrecios.Prd_PreDescripcion
                                        ,productoPrecios.Prd_Pesos
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecios_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarProductoPrecios(ProductoPrecios productoPrecios, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
                                        "@Id_Cd", 
	                                    "@Id_Prd", 
                                        "@Id_Pre",
                                        "@Prd_Actual",
                                        "@Prd_FechaInicio",
                                        "@Prd_FechaFin",
                                        "@Prd_PreDescripcion",
                                        "@Prd_Pesos"
                                      };
                object[] Valores = { 
                                        productoPrecios.Id_Emp
                                        ,productoPrecios.Id_Cd
                                        ,productoPrecios.Id_Prd
                                        ,productoPrecios.Id_Pre
                                        ,productoPrecios.Prd_Actual
                                        ,productoPrecios.Prd_FechaInicio
                                        ,productoPrecios.Prd_FechaFin
                                        ,productoPrecios.Prd_PreDescripcion
                                        ,productoPrecios.Prd_Pesos
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecios_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaProductoPrecioAAA(ref float precio, Sesion sesion, int Id_Prd, ref int Id_Pre)
        {
            //ric
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPreciosAAA_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Pesos"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Pesos")));
                    Id_Pre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pre"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pre")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Versión que utiliza un contexto de conexión a la fuente de datos.
        /// </summary>
        /// <param name="precio"></param>
        /// <param name="sesion"></param>
        /// <param name="Id_Prd"></param>
        /// <param name="Id_Pre"></param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        public void ConsultaListaProductoPrecioAAA(ref float precio, Sesion sesion, int Id_Prd, ref int Id_Pre, ICD_Contexto icdCtx)
        {
            //ric
            try
            {
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Prd };

                SqlCommand sqlcmd = CD_Datos.GenerarSqlCommand("spProductoPreciosAAA_Consulta", ref dr, Parametros, Valores, icdCtx);


                while (dr.Read())
                {
                    precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Pesos"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Pesos")));
                    Id_Pre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pre"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pre")));
                }

                sqlcmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaProductoPrecioAAA(ref float precio, Sesion sesion, int Id_Prd)
        {
            //ric
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPreciosAAA_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Pesos"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Pesos")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaProductoPrecioAAA(ref float precio, int Id_Emp, int Id_Cd, int Id_Prd, string Conexion)
        {
            //ric
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPreciosAAA_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Pesos"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Pesos")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Versión que acepta un contexto de conexión a la fuente de datos
        /// </summary>
        /// <param name="precio"></param>
        /// <param name="Id_Emp"></param>
        /// <param name="Id_Cd"></param>
        /// <param name="Id_Prd"></param>
        /// <param name="idcCtx">Contexto de conexión a la fuente de datos</param>
        public void ConsultaListaProductoPrecioAAA(ref float precio, int Id_Emp, int Id_Cd, int Id_Prd, ICD_Contexto idcCtx)
        {
            //ric
            try
            {
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Prd };

                SqlCommand sqlcmd = CD_Datos.GenerarSqlCommand("spProductoPreciosAAA_Consulta", ref dr, Parametros, Valores, idcCtx);


                while (dr.Read())
                {
                    precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Pesos"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Pesos")));
                }

                sqlcmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaProductoPrecioPUBLICO(ref float precio, Sesion sesion, int Id_Prd)
        {
            //ric
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPreciosPUBLICO_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Pesos"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Pesos")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Consulta el precio publico de un articulo. Recibe ProductoPrecios (Id_Emp, Id_Cd, Id_Prd)
        /// </summary>
        /// <param name="productoPrecios"></param>
        /// <param name="Conexion"></param>
        /// <param name="precio"></param>
        public void ConsultaProductoPrecio_Publico(ProductoPrecios productoPrecios, string Conexion, ref double precio, DateTime Fecha_Actual)
        {//rm Consulta el precio publico de un articulo
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd", "@Fecha_Actual" };
                object[] Valores = { productoPrecios.Id_Emp, productoPrecios.Id_Cd, productoPrecios.Id_Prd, Fecha_Actual };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecio_ConsultarPrePub", ref dr, Parametros, Valores);

                precio = -1;
                while (dr.Read())
                {
                    precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Pesos"))) ? -1 : dr.GetDouble(dr.GetOrdinal("Prd_Pesos"));
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultarPrecios(ref double precioAAA, ref double precioLista, Sesion sesion, int Id_Cte, int Id_Prd)
        {

            
           
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Prd" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd, Id_Cte, Id_Prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecios_Consultar", ref dr, Parametros, Valores);


                if (dr.HasRows)
                {
                    dr.Read();
                    precioAAA = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PAAA"))) ? 0 : dr.GetDouble(dr.GetOrdinal("PAAA"));
                    precioLista = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PLISTA"))) ? 0 : dr.GetDouble(dr.GetOrdinal("PLISTA"));
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarPrecios(ref double precioAAA, ref double precioLista, Sesion sesion, int Id_Prd)
        {



            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Prd" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd, null, Id_Prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecios_Consultar", ref dr, Parametros, Valores);


                if (dr.HasRows)
                {
                    dr.Read();
                    precioAAA = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PAAA"))) ? 0 : dr.GetDouble(dr.GetOrdinal("PAAA"));
                    precioLista = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PLISTA"))) ? 0 : dr.GetDouble(dr.GetOrdinal("PLISTA"));
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Versión que acepta una transacción de la capa de negocio
        /// </summary>
        /// <param name="precioAAA"></param>
        /// <param name="precioLista"></param>
        /// <param name="sesion"></param>
        /// <param name="Id_Prd"></param>
        /// <param name="icdCtx">Transacción de la capa de negocio</param>
        public void ConsultarPrecios(ref double precioAAA, ref double precioLista, Sesion sesion, int Id_Prd, ICD_Contexto icdCtx)
        {
            try
            {
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Prd" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd, null, Id_Prd };

                SqlCommand sqlcmd = CD_Datos.GenerarSqlCommand("spProductoPrecios_Consultar", ref dr, Parametros, Valores, icdCtx);


                if (dr.HasRows)
                {
                    dr.Read();
                    precioAAA = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PAAA"))) ? 0 : dr.GetDouble(dr.GetOrdinal("PAAA"));
                    precioLista = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PLISTA"))) ? 0 : dr.GetDouble(dr.GetOrdinal("PLISTA"));
                }

                dr.Close();
                sqlcmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
