using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_CapTransferenciaAlmacen
    {
        public void ConsultaTransferenciaAlmacen(ref TransferenciaAlmacen TransferenciaAlmacen, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Trans" };
                object[] Valores = { TransferenciaAlmacen.Id_Emp, TransferenciaAlmacen.Id_Cd, TransferenciaAlmacen.Id_Trans };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spTransferencia_Consulta", ref dr, Parametros, Valores);
                if (dr.Read())
                {
                    TransferenciaAlmacen.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    TransferenciaAlmacen.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    TransferenciaAlmacen.Id_Trans = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Trans")));
                    TransferenciaAlmacen.Id_CdOrigen = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_CdOrigen"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CdOrigen")));
                    TransferenciaAlmacen.Id_CdOrigenStr = dr.GetValue(dr.GetOrdinal("Id_CdOrigenStr")).ToString();
                    TransferenciaAlmacen.Trans_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Trans_Fecha")));
                    TransferenciaAlmacen.Id_UOrigen = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_UOrigen")));
                    TransferenciaAlmacen.U_NombreOrigen = dr.GetValue(dr.GetOrdinal("U_NombreOrigen")).ToString();
                    TransferenciaAlmacen.TransNota = dr.GetValue(dr.GetOrdinal("TransNota")).ToString();
                    TransferenciaAlmacen.Id_RemOrigen = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_RemOrigen")));
                 

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTransferenciaAlmacen_Lista(TransferenciaAlmacen TransferenciaAlmacen, string Conexion, ref List<TransferenciaAlmacen> List
            , int Id_Trans_inicio
            , int Id_Trans_fin
            , DateTime Trans_Fecha_inicio
            , DateTime Trans_Fecha_fin
            , string Trans_Estatus)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                          "@Id_Emp"
                                          , "@Id_Cd"
                                          , "@Id_U" 
                                          , "@Id_Trans_inicio"
                                          , "@Id_Trans_fin"
                                          , "@Trans_Fecha_inicio"
                                          , "@Trans_Fecha_fin"
                                          , "@Trans_Estatus"
                                      };
                object[] Valores = { 
                                       TransferenciaAlmacen.Id_Emp
                                       , TransferenciaAlmacen.Id_Cd
                                       , TransferenciaAlmacen.Id_U == -1 ? (object)null : TransferenciaAlmacen.Id_U 
                                       , Id_Trans_inicio == -1 ? (object)null : Id_Trans_inicio
                                       , Id_Trans_fin == -1 ? (object)null : Id_Trans_fin
                                       , Trans_Fecha_inicio == DateTime.MinValue ? (object)null : Trans_Fecha_inicio
                                       , Trans_Fecha_fin == DateTime.MinValue ? (object)null : Trans_Fecha_fin
                                       , Trans_Estatus == "-1" || Trans_Estatus == string.Empty ? (object)null : Trans_Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapTransferencia_Consulta", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    TransferenciaAlmacen = new TransferenciaAlmacen();
                    TransferenciaAlmacen.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    TransferenciaAlmacen.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    TransferenciaAlmacen.Id_Trans = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Trans")));
                    TransferenciaAlmacen.Id_CdOrigen = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_CdOrigen"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CdOrigen")));
                    TransferenciaAlmacen.Id_CdOrigenStr = dr.GetValue(dr.GetOrdinal("Id_CdOrigenStr")).ToString();
                    TransferenciaAlmacen.Trans_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Trans_Fecha")));
                    TransferenciaAlmacen.Id_UOrigen = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_UOrigen")));
                    TransferenciaAlmacen.U_NombreOrigen = dr.GetValue(dr.GetOrdinal("U_NombreOrigen")).ToString();
                    TransferenciaAlmacen.TransNota = dr.GetValue(dr.GetOrdinal("TransNota")).ToString();
                    TransferenciaAlmacen.Id_RemOrigen = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_RemOrigen")));
                    TransferenciaAlmacen.Trans_Estatus = dr.GetValue(dr.GetOrdinal("Trans_Estatus")).ToString();
                    TransferenciaAlmacen.Trans_EstatusStr = dr.GetValue(dr.GetOrdinal("Trans_EstatusStr")).ToString();
                   

                    List.Add(TransferenciaAlmacen);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public void GeneraTransferenciaAlmacenAutomatica(string Conexion, ref DataTable dt, string nombreTabla, int Id_Emp, int Id_Cd_Ver, int proveedor, int Id_prd_RI, int Id_prd_RF, bool Prd_Transito_Aplica, int? validador)
        {
            try
            {               
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Emp"
                                          ,"@Id_Cd_Ver"
                                          ,"@proveedor"
                                          ,"@Id_prd_RI"
                                          ,"@Id_prd_RF"
                                          ,"@Prd_Transito_Aplica" 
                                          ,"@Evaluacion"
                                      };
                object[] Valores = { Id_Emp
                                       ,Id_Cd_Ver
                                       ,proveedor
                                       ,Id_prd_RI <= 0 ? (object)null : Id_prd_RI
                                       ,Id_prd_RF <= 0 ? (object)null : Id_prd_RF
                                       ,Prd_Transito_Aplica 
                                       ,validador == null ? (object)null : validador
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProOrdCompra_GenMax_Partidas_Generar", "tabla", ref dt, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void GeneraTransferenciaAlmacenAutomatica_Lista(TransferenciaAlmacenDet TransferenciaAlmacen, Sesion sesion, int proveedor, int Id_prd_RI, int Id_prd_RF, bool Prd_Transito_Aplica, ref List<TransferenciaAlmacenDet> List)        
        //{
        //    try
        //    {
        //        SqlDataReader dr = null;
        //        CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
        //       string[] Parametros = { "@Id_Emp"
        //                                  ,"@Id_Cd_Ver"
        //                                  ,"@proveedor"
        //                                  ,"@Id_prd_RI"
        //                                  ,"@Id_prd_RF"
        //                                  ,"@Prd_Transito_Aplica" 
        //                              };
        //        object[] Valores = { sesion.Id_Emp
        //                               ,sesion.Id_Cd_Ver
        //                               ,proveedor
        //                               ,Id_prd_RI <= 0 ? (object)null : Id_prd_RI
        //                               ,Id_prd_RF <= 0 ? (object)null : Id_prd_RF
        //                               ,Prd_Transito_Aplica 
        //                           };                                  
        //            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProOrdCompra_GenMax_Partidas_Generar", ref dr, Parametros, Valores);
        //        while (dr.Read())
        //        {
        //            TransferenciaAlmacen = new TransferenciaAlmacenDet();
        //            TransferenciaAlmacen.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
        //            TransferenciaAlmacen.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
        //            TransferenciaAlmacen.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
        //            TransferenciaAlmacen.Trans_Cantidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniEmp"))) ? 1 : (float)(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                  
        //            List.Add(TransferenciaAlmacen);
        //        }
        //        CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}        
        /*
        public void InsertarTransferenciaAlmacen(ref TransferenciaAlmacen TransferenciaAlmacen, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
                                        "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ord", 
                                        "@Id_Pvd",
                                        "@Id_U",
                                        "@Trans_Fecha",
                                        "@Trans_Tipo",
                                        "@Trans_Notas",
                                        "@Trans_Estatus"
                                      };
                object[] Valores = { 
                                        TransferenciaAlmacen.Id_Emp
                                        ,TransferenciaAlmacen.Id_Cd
                                        ,TransferenciaAlmacen.Id_Ord
                                        ,TransferenciaAlmacen.Id_Pvd == -1 ? (object)null : TransferenciaAlmacen.Id_Pvd
                                        ,TransferenciaAlmacen.Id_U
                                        ,TransferenciaAlmacen.Trans_Fecha
                                        ,TransferenciaAlmacen.Trans_Tipo
                                        ,TransferenciaAlmacen.Trans_Notas
                                        ,TransferenciaAlmacen.Trans_Estatus
                                   };
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompra_Insertar", ref verificador, Parametros, Valores);
                TransferenciaAlmacen.Id_Ord = verificador; //clave de orden de compra

                string[] ParametrosDet = { 
                                        "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ord", 
                                        "@Id_OrdDet",
                                        "@Id_Prd",
                                        "@Trans_Cantidad",
                                        "@Trans_CantidadGen"
                                      };
                int i = 1;
                foreach (TransferenciaAlmacenDet TransferenciaAlmacenDet in TransferenciaAlmacen.ListTransferenciaAlmacen)
                {
                    TransferenciaAlmacenDet.Id_OrdDet = i;
                    object[] ValoresDet = { 
                                        TransferenciaAlmacenDet.Id_Emp
                                        ,TransferenciaAlmacenDet.Id_Cd
                                        ,TransferenciaAlmacen.Id_Ord //Id de orden de la tabla de encabezado
                                        ,TransferenciaAlmacenDet.Id_OrdDet
                                        ,TransferenciaAlmacenDet.Id_Prd
                                        ,TransferenciaAlmacenDet.Trans_Cantidad
                                        ,TransferenciaAlmacenDet.Trans_CantidadGen
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompraDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
                    i += 1;
                }
                verificador = TransferenciaAlmacen.Id_Ord;
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarTransferenciaAlmacen(TransferenciaAlmacen TransferenciaAlmacen, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
                                        "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ord", 
                                        "@Id_Pvd",
                                        "@Id_U",
                                        "@Trans_Fecha",
                                        "@Trans_Tipo",
                                        "@Trans_Notas",
                                        "@Trans_Estatus"
                                      };
                object[] Valores = { 
                                        TransferenciaAlmacen.Id_Emp
                                        ,TransferenciaAlmacen.Id_Cd
                                        ,TransferenciaAlmacen.Id_Ord
                                        ,TransferenciaAlmacen.Id_Pvd == -1 ? (object)null : TransferenciaAlmacen.Id_Pvd
                                        ,TransferenciaAlmacen.Id_U
                                        ,TransferenciaAlmacen.Trans_Fecha
                                        ,TransferenciaAlmacen.Trans_Tipo
                                        ,TransferenciaAlmacen.Trans_Notas
                                        ,TransferenciaAlmacen.Trans_Estatus
                                   };
                // -----------------------------------------------------------------
                // Actualizar encabezado de la orden de compra
                // -----------------------------------------------------------------
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompra_Modificar", ref verificador, Parametros, Valores);                
                // -----------------------------------------------------------------
                // Eliminar el detalle de la orden de compra para insertar el nuevo
                // -----------------------------------------------------------------
                string[] ParametrosEliminar = { 
                                        "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ord", 
                                      };
                object[] ValoresEliminar = { 
                                        TransferenciaAlmacen.Id_Emp
                                        ,TransferenciaAlmacen.Id_Cd
                                        ,TransferenciaAlmacen.Id_Ord
                                   };
                sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompraDetalle_Eliminar", ref verificador, ParametrosEliminar, ValoresEliminar);
                  
                //parametros de detalle de la orden de compra
                string[] ParametrosDet = { 
                                        "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ord", 
                                        "@Id_OrdDet",
                                        "@Id_Prd",
                                        "@Trans_Cantidad",
                                        "@Trans_CantidadGen"
                                      };
                int i = 1;
                foreach (TransferenciaAlmacenDet TransferenciaAlmacenDet in TransferenciaAlmacen.ListTransferenciaAlmacen)
                {
                    TransferenciaAlmacenDet.Id_OrdDet = i;
                    object[] ValoresDet = { 
                                        TransferenciaAlmacenDet.Id_Emp
                                        ,TransferenciaAlmacenDet.Id_Cd
                                        ,TransferenciaAlmacen.Id_Ord //Id de orden de la tabla de encabezado
                                        ,TransferenciaAlmacenDet.Id_OrdDet
                                        ,TransferenciaAlmacenDet.Id_Prd
                                        ,TransferenciaAlmacenDet.Trans_Cantidad
                                        ,TransferenciaAlmacenDet.Trans_CantidadGen
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompraDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
                    i += 1;
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

        public void ModificarTransferenciaAlmacen_Estatus(TransferenciaAlmacen TransferenciaAlmacen, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
                                        "@Id_Emp" 
                                        ,"@Id_Cd"
	                                    ,"@Id_Ord"
                                        ,"@Trans_Estatus"
                                      };
                object[] Valores = { 
                                        TransferenciaAlmacen.Id_Emp
                                        ,TransferenciaAlmacen.Id_Cd
                                        ,TransferenciaAlmacen.Id_Ord
                                        ,TransferenciaAlmacen.Trans_Estatus
                                   };
                // -----------------------------------------------------------------
                // Actualizar encabezado de la orden de compra
                // -----------------------------------------------------------------
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompra_ModificarEstatus", ref verificador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ModificarTransferenciaAlmacen_EstatusEmision(TransferenciaAlmacen TransferenciaAlmacen, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
                                        "@Id_Emp" 
                                        ,"@Id_Cd"
	                                    ,"@Id_Ord"
                                        ,"@Trans_EstatusEmision"
                                        ,"@Evento"
                                      };
                object[] Valores = { 
                                        TransferenciaAlmacen.Id_Emp
                                        ,TransferenciaAlmacen.Id_Cd
                                        ,TransferenciaAlmacen.Id_Ord
                                        ,TransferenciaAlmacen.Trans_EstatusEmision
                                        ,TransferenciaAlmacen.Trans_EstatusEmisionStr
                                   };
                // -----------------------------------------------------------------
                // Actualizar encabezado de la orden de compra
                // -----------------------------------------------------------------
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompra_ModificarEstatusEmision", ref verificador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void EliminarTransferenciaAlmacen(TransferenciaAlmacen TransferenciaAlmacen, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Ord"
                                      };
                object[] Valores = { 
                                       TransferenciaAlmacen.Id_Emp
                                       ,TransferenciaAlmacen.Id_Cd
                                       ,TransferenciaAlmacen.Id_Ord
                                   };
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompra_eliminar", ref verificador, Parametros, Valores);
                //TransferenciaAlmacen.Id_Ord = verificador; //identity de orden de compra 
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }*/
    }
}
