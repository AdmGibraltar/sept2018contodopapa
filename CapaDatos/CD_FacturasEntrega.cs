using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_FacturasEntrega
    {
        public void ConsultaProFacturaEntrega(int Id_Emp, int Id_Cd, string Conexion, FacturaEntrega facturasfiltro, ref List<FacturaEntrega> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Filtro_Nombre",
                                          "@Filtro_CteIni",
                                          "@Filtro_CteFin",
                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       facturasfiltro.Filtro_Nombre  == "" ? (object)null : facturasfiltro.Filtro_Nombre ,
                                       facturasfiltro.Filtro_Id_Cte  == "" ? (object)null : facturasfiltro.Filtro_Id_Cte ,
                                       facturasfiltro.Filtro_Id_Cte2  == "" ? (object)null : facturasfiltro.Filtro_Id_Cte2,
                                       facturasfiltro.Filtro_FecIni,
                                       facturasfiltro.Filtro_FecFin 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProFacturaEntrega_Consulta", ref dr, Parametros, Valores);

                FacturaEntrega facturaEntrega;
                while (dr.Read())
                {
                    facturaEntrega = new FacturaEntrega();
                    facturaEntrega.Id_Fac = (int)dr.GetValue(dr.GetOrdinal("Id_Fac"));
                    facturaEntrega.Estatus = (string)dr.GetValue(dr.GetOrdinal("Fac_Estatus"));
                    facturaEntrega.Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Fac_Fecha"));
                    facturaEntrega.Numero = dr.GetValue(dr.GetOrdinal("Id_FacSerie")).ToString();
                    facturaEntrega.Pedido = (int)dr.GetValue(dr.GetOrdinal("Id_Ped"));
                    facturaEntrega.Cliente = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    facturaEntrega.Num_Cliente = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    List.Add(facturaEntrega);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarProFacturaEntrega(int Id_Emp, int Id_Cd, int Id_U, FacturaEntrega facturas, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                         "@Id_Emp",        
                                         "@Id_Cd",   
                                         "@Id_U",
                                         "@Id_Fac",
                                         "@Id_Ped",
                                     };
                object[] Valores = {                                       
                                       Id_Emp,
                                       Id_Cd,       
                                       Id_U,
                                       facturas.Id_Fac,
                                       facturas.Pedido                                
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProFacturaEntrega_Modificar", ref verificador, Parametros, Valores);
                verificador = 1;
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarProFacturaEntrega(int Id_Emp, int Id_Cd, int Id_U, FacturaEntrega facturas, string Conexion, ref int verificador, string DbName)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                         "@Id_Emp",        
                                         "@Id_Cd",   
                                         "@Id_U",
                                         "@Id_Fac",
                                         "@Id_Ped",
                                         "@db"
                                      };
                object[] Valores = {                                       
                                       Id_Emp,
                                       Id_Cd,       
                                       Id_U,
                                       facturas.Id_Fac,
                                       facturas.Pedido,
                                       DbName                                 
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProFacturaEntrega_Modificar", ref verificador, Parametros, Valores);
                verificador = 1;
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarProFacturaEntregaCob(int Id_Emp, int Id_Cd, int Id_U, FacturaAlmacenCobro facturas, string Conexion, ref int verificador, string dbname)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                CapaDatos.StartTrans();
                SqlCommand sqlcmd = new SqlCommand();
                string[] Parametros = { 
                                         "@Id_Emp",        
                                         "@Id_Cd",   
                                         "@Id_U",
                                         "@Id_Fac",
                                         "@Id_Ped",
                                         "@db"
                                      };


                foreach (FacturaAlmacenCobroDet FacturaAlmacenCobroDet in facturas.ListaFacturaAlmacenCobroDet)
                {

                    object[] Valores = {                                       
                                       FacturaAlmacenCobroDet.Id_Emp,
                                       FacturaAlmacenCobroDet.Id_Cd,       
                                       Id_U,
                                       FacturaAlmacenCobroDet.Fac_Doc,
                                       -1,
                                       dbname                     
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spProFacturaEntrega_Modificar", ref verificador, Parametros, Valores);

                }
                verificador = 1;
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }


        //public void AutorizaBajaFactura(ref Factura fac, string conexion, string estatus)
        //{
        //    try
        //    {
        //        CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
        //         SqlDataReader dr = null;
        //         string[] Parametros = { 
        //                                   "@Id_Emp" 
        //                                  ,"@Id_Cd"
        //                                  ,"@Id_Fac"
        //                                  ,"@Fac_Estatus"
        //                              };
        //         object[] Valores = { 
        //                              fac.Id_Emp,
        //                              fac.Id_Cd,
        //                              fac.Id_Fac,
        //                              estatus
                                    
        //                           };

        //         SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFac_Autorizar", ref dr, Parametros, Valores);
        //         if (dr.HasRows)
        //         {
        //             dr.Read();
        //         }

        //         CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public void CambiarEstatus(Sesion sesion, int Id_Fac, string Fac_Estatus)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                SqlDataReader dr = null;
                string[] Parametros = { 
                                           "@Id_Emp"
                                          ,"@Id_Cd"
                                          ,"@Id_Fac"
                                          ,"@Fac_Estatus"
                                      };
                object[] Valores = { 
                                           sesion.Id_Emp,
                                           sesion.Id_Cd_Ver,
                                           Id_Fac,
                                           Fac_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("Sp_cambiaestatusfactura", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
