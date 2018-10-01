using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_DevParcial_Lista
    {
        public void ConsultaDevParcialLista(int Id_Emp, int Id_Cd, string Conexion, DevParcial devParcialfiltro, ref List<DevParcial> List)
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
                                          "@Filtro_FecFin",
                                          "@Filtro_Estatus",
                                          "@Filtro_DevIni",
                                          "@Filtro_DevFin"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       devParcialfiltro.Filtro_Nombre  == "" ? (object)null : devParcialfiltro.Filtro_Nombre ,
                                       devParcialfiltro.Filtro_Id_Cte  == "" ? (object)null : devParcialfiltro.Filtro_Id_Cte ,
                                       devParcialfiltro.Filtro_Id_Cte2  == "" ? (object)null : devParcialfiltro.Filtro_Id_Cte2,
                                       devParcialfiltro.Filtro_FecIni  == "" ? (object)null : devParcialfiltro.Filtro_FecIni,
                                       devParcialfiltro.Filtro_FecFin  == "" ? (object)null : devParcialfiltro.Filtro_FecFin, 
                                       devParcialfiltro.Filtro_Estatus == "" ? (object)null : devParcialfiltro.Filtro_Estatus, 
                                       devParcialfiltro.Filtro_Id_Devini == "" ? (object)null : devParcialfiltro.Filtro_Id_Devini,
                                       devParcialfiltro.Filtro_Id_Devfin == "" ? (object)null : devParcialfiltro.Filtro_Id_Devfin
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapDevParcial2_Consulta", ref dr, Parametros, Valores);

                DevParcial devParcial;
                while (dr.Read())
                {
                    devParcial = new DevParcial();
                    devParcial.Id_Nca = (int)dr.GetValue(dr.GetOrdinal("Id_Dev"));
                    devParcial.Id_Nca2 = (int)dr.GetValue(dr.GetOrdinal("Id_Ncr"));
                    devParcial.Es_Estatus = (string)dr.GetValue(dr.GetOrdinal("Dev_EstatusId"));
                    devParcial.Estatus = (string)dr.GetValue(dr.GetOrdinal("Dev_Estatus"));
                    devParcial.Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Dev_Fecha"));
                    devParcial.Tipo = (int)dr.GetValue(dr.GetOrdinal("Dev_Tipo"));                   
                    devParcial.Cliente = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    devParcial.Num_Cliente = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    devParcial.Nca_Subtotal = (double)dr.GetValue(dr.GetOrdinal("Dev_Subtotal"));
                    devParcial.Nca_Iva = (double)dr.GetValue(dr.GetOrdinal("Dev_Iva"));
                    devParcial.Nca_Total = (double)dr.GetValue(dr.GetOrdinal("Dev_Total"));
                    devParcial.Nca_Pagos = (string)dr.GetValue(dr.GetOrdinal("Fac_Pagado"));
                    devParcial.Factura = (int)dr.GetValue(dr.GetOrdinal("Dev_Factura"));                   
                    List.Add(devParcial);            
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarDevParcialLista(Sesion sesion, DevParcial devParcial, ref int verificador)
        {
            try
            {                
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = {    
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Fac",
                                          "@Id_Dev",
                                          "@id_Ncr",
                                          "@idCliente",                                                               
                                          "@Dev_Subtotal",
                                          "@Dev_Iva",
                                          "@Dev_Importe",
                                          "@Id_UCancelo"

                                      };
                object[] Valores = { 
                                       sesion.Id_Emp, 
                                       sesion.Id_Cd_Ver,
                                       devParcial.Factura,
                                       devParcial.Id_Nca,
                                       devParcial.Id_Nca2,
                                       devParcial.Num_Cliente,
                                       devParcial.Nca_Subtotal, 
                                       devParcial.Nca_Iva,                                       
                                       devParcial.Nca_Total,
                                       devParcial.Id_U
                                   };
                
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapDevParcial_Eliminar", ref verificador, Parametros, Valores);               
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                
                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Dev",
                                          "@Id_Fac"
                                      };

                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       devParcial.Id_Nca.ToString(),
                                       devParcial.Factura.ToString()                                       
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapDevParcialLista_Impresion", ref dr, parametros, Valores);                
                
                 while (dr.Read())
                 {                    
                     devParcial2.TipoMov = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Dev_Tipo"))) ? "" : dr.GetString(dr.GetOrdinal("Dev_Tipo"));
                     devParcial2.Estatus = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Dev_Estatus"))) ? "" : dr.GetString(dr.GetOrdinal("Dev_Estatus"));
                     devParcial2.Id_Nca = dr.GetInt32(dr.GetOrdinal("Id_Ncr"));
                     devParcial2.Id_Nca2 = dr.GetInt32(dr.GetOrdinal("Id_Dev"));
                     devParcial2.Fecha = dr.GetDateTime(dr.GetOrdinal("Dev_Fecha"));
                     devParcial2.Factura = dr.GetInt32(dr.GetOrdinal("Dev_Factura"));
                     devParcial2.Num_Cliente = dr.GetInt32(dr.GetOrdinal("Id_Cte"));
                     devParcial2.Id_Ter = dr.GetInt32(dr.GetOrdinal("Id_Ter"));
                     devParcial2.Id_Rik = dr.GetInt32(dr.GetOrdinal("Id_Rik"));
                     devParcial2.Cliente = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_NomComercial"))) ? "" : dr.GetString(dr.GetOrdinal("Cte_NomComercial"));
                     devParcial2.Cte_FacCalle1 = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Direccion"))) ? "" : dr.GetString(dr.GetOrdinal("Direccion"));                     
                     devParcial2.Nca_Subtotal = dr.GetDouble(dr.GetOrdinal("Dev_SubTotal"));
                     devParcial2.Nca_Iva = dr.GetDouble(dr.GetOrdinal("Dev_Iva"));
                     devParcial2.Nca_Total = dr.GetDouble(dr.GetOrdinal("Dev_Total"));
                     devParcial2.DatoFactura = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_FacSerie"))) ? "" : dr.GetString(dr.GetOrdinal("Id_FacSerie"));
                     break;      
                 }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarDevParcialImpresion(DevParcial_Detalle devParcial, string conexion, ref int verificador)
        {//Actualiza el status a (I) de una Devolucion Parcial
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = {    
                                          "@Id_Emp", 
                                          "@Id_Cd",                                          
                                          "@Id_Dev"
                                      };
                object[] Valores = {                                                                          
                                       devParcial.Territorio,//id_Emp
                                       devParcial.TipoDev,//id_Cd
                                       devParcial.TipoMovimiento//id_Dev
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapDevParcialActualizar_Impresion", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
