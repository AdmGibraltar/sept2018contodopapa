using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_ProSeguimientoPrd
    {
        /// <summary>
        /// Metodo que busca los datos necesarios en la base de datos referentes al 
        /// seguimiento de productos en entrega a sucursal
        /// </summary>
        /// <param name="producto">Entidad del producto</param>
        /// <param name="conexion">Cadena de concexion a la base de datos</param>
        /// <param name="lista">Lista que contendra los resultados</param>
        /// <param name="Id_Emp">Id de la empresa</param>
        /// <param name="Id_Prd">Id del producto a buscar</param>
        public void BuscaProSeguimientoPrd(ref Producto producto, string conexion, ref List<Producto> lista, ref int validador)
        {
            try
            {
                SqlDataReader sdr = null;
                CapaDatos.CD_Datos CDDatos = new CapaDatos.CD_Datos(conexion);
                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Prd",
                                          "@Id_Cd"
                                      };
                object[] valores = { 
                                       producto.Id_Emp,
                                       producto.Id_Prd ==-1?(object)null:producto.Id_Prd,
                                       producto.Id_Cd
                                   };

                SqlCommand sqlcmd = CDDatos.GenerarSqlCommand("spProSeguimientoPrd", ref sdr, parametros, valores);

                while (sdr.Read())
                {
                    producto = new Producto();
                    producto.Id_Emp = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Emp")));
                    producto.Id_Pinv = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Pinv")));
                    producto.Id_Prd = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Prd")));
                    producto.Prd_Descripcion = sdr.GetValue(sdr.GetOrdinal("Prd_Descripcion")).ToString();
                    producto.Prd_Presentacion = sdr.GetValue(sdr.GetOrdinal("Prd_Presentacion")).ToString();
                    producto.Prd_InvInicial = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Prd_InvInicial")));
                    producto.Prd_InvFinal = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Prd_InvFinal")));
                    producto.Prd_Asignado = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Prd_Asignado")));
                    producto.Prd_Ordenado = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Prd_Ordenado")));
                    producto.Prd_Transito = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Prd_Transito")));
                    producto.TieneComentarios = sdr.GetValue(sdr.GetOrdinal("Comentarios")).ToString();
                    lista.Add(producto);
                    validador = 1;
                }
                CDDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo utilizado para llenar el grid del formulario de observaciones de seguimieto
        /// </summary>
        /// <param name="segPrd">Entidad del seguimiento de productos</param>
        /// <param name="listaPrd">Lista que contendra los resultados de la operacion</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        public void LlenaGridSeguimiento(SeguimientoProductos segPrd, ref List<SeguimientoProductos> listaPrd, string conexion)
        {
            try
            {
                SqlDataReader sdr = null;
                CapaDatos.CD_Datos CDDatos = new CapaDatos.CD_Datos(conexion);

                string[] parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Prd"
                                      };
                object[] valores = { 
                                       segPrd.Id_Emp,
                                       segPrd.Id_Cd,
                                       segPrd.Id_Prd
                                   };

                SqlCommand sqlcmd = CDDatos.GenerarSqlCommand("spProseguimientoPrd_BuscaObservaciones", ref sdr, parametros, valores);

                while (sdr.Read())
                {
                    segPrd = new SeguimientoProductos();

                    segPrd.Id_Emp = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Emp")));
                    segPrd.Id_Cd = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Cd")));
                    segPrd.Id_Prd = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Prd")));
                    segPrd.Id_SegPrd = Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("ID_SegPrd")));
                    segPrd.Seg_fecha = Convert.ToDateTime(sdr.GetValue(sdr.GetOrdinal("Seg_fecha")));
                    segPrd.Seg_Comentarios = sdr.GetValue(sdr.GetOrdinal("Seg_Comentarios")).ToString();

                    listaPrd.Add(segPrd);
                }

                CDDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que inserta observaciones en la tabla de CapSegProd
        /// </summary>
        /// <param name="SegPrd">Entidad de los seguimeintos a productos</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Variable que confirma la operacon si regresa con valor mayor a 0</param>
        public void GuardaObservaciones(SeguimientoProductos SegPrd, string conexion, ref int verificador)
        {
            CD_Datos CDDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CDDatos = new CD_Datos(conexion);
                CDDatos.StartTrans();

                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Prd",
                                          "@Seg_fecha",
                                          "@Seg_Comentarios"
                                      };
                object[] valores = { 
                                       SegPrd.Id_Emp,
                                       SegPrd.Id_Cd,
                                       SegPrd.Id_Prd,
                                       SegPrd.Seg_fecha,
                                       SegPrd.Seg_Comentarios
                                   };

                sqlcmd = CDDatos.GenerarSqlCommand("spProSeguimientoPrd_Insertar", ref verificador, parametros, valores);

                if (verificador > 0)
                {
                    CDDatos.CommitTrans();
                    CDDatos.LimpiarSqlcommand(ref sqlcmd);
                }
                else
                {
                    CDDatos.RollBackTrans();
                }
            }
            catch (Exception ex)
            {
                CDDatos.RollBackTrans();
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que modifica las observaciones en la tabla de CapSegProd
        /// </summary>
        /// <param name="SegPrd">Entidad de los seguimeintos a productos</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Variable que confirma la operacon si regresa con valor mayor a 0</param>
        public void ModificaObservaciones(SeguimientoProductos SegPrd, string conexion, ref int verificador)
        {
            CD_Datos CDDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CDDatos = new CD_Datos(conexion);
                CDDatos.StartTrans();

                string[] parametros = { 
                                          "@Id_SegPrd",
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Prd",
                                          "@Seg_fecha",
                                          "@Seg_Comentarios"
                                      };
                object[] valores = { 
                                       SegPrd.Id_SegPrd,
                                       SegPrd.Id_Emp,
                                       SegPrd.Id_Cd,
                                       SegPrd.Id_Prd,
                                       SegPrd.Seg_fecha,
                                       SegPrd.Seg_Comentarios
                                   };

                sqlcmd = CDDatos.GenerarSqlCommand("spProSeguimientoPrd_Modificar", ref verificador, parametros, valores);

                if (verificador > 0)
                {
                    CDDatos.CommitTrans();
                    CDDatos.LimpiarSqlcommand(ref sqlcmd);
                }
                else
                {
                    CDDatos.RollBackTrans();
                }
            }
            catch (Exception ex)
            {
                CDDatos.RollBackTrans();
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que borra registros de la tabla CapSegProd
        /// </summary>
        /// <param name="SegPrd">Entidad de los seguimeintos a productos</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Variable que confirma la operacon si regresa con valor mayor a 0</param>
        public void EliminaObservaciones(SeguimientoProductos SegPrd, string conexion, ref int verificador)
        {
            try
            {
                CD_Datos CDDatos = new CD_Datos(conexion);

                string[] parametros = {
                                          "@Id_SegPrd",
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Prd"
                                      };
                object[] valores = { 
                                       SegPrd.Id_SegPrd,
                                       SegPrd.Id_Emp,
                                       SegPrd.Id_Cd,
                                       SegPrd.Id_Prd
                                   };

                SqlCommand sqlcmd = CDDatos.GenerarSqlCommand("spProSeguimientoPrd_Eliminar", ref verificador, parametros, valores);
                CDDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
