using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_RemisionesEntrega
    {
        public void ConsultaProRemisionEntrega(int Id_Emp, int Id_Cd, string Conexion, RemisionesEntrega remisionfiltro, ref List<RemisionesEntrega> List)
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
                                       remisionfiltro.Filtro_Nombre  == "" ? (object)null : remisionfiltro.Filtro_Nombre ,
                                       remisionfiltro.Filtro_Id_Cte  == "" ? (object)null : remisionfiltro.Filtro_Id_Cte ,
                                       remisionfiltro.Filtro_Id_Cte2  == "" ? (object)null : remisionfiltro.Filtro_Id_Cte2,
                                       remisionfiltro.Filtro_FecIni  == "" ? (object)null : remisionfiltro.Filtro_FecIni ,
                                       remisionfiltro.Filtro_FecFin  == "" ? (object)null : remisionfiltro.Filtro_FecFin 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProRemisionesEntrega_Consulta", ref dr, Parametros, Valores);

                RemisionesEntrega remisionesEntrega;
                while (dr.Read())
                {
                    remisionesEntrega = new RemisionesEntrega();
                    remisionesEntrega.Id_Rem = (int)dr.GetValue(dr.GetOrdinal("Id_Rem"));
                    remisionesEntrega.Tipo = (string)dr.GetValue(dr.GetOrdinal("Rem_Tipo"));
                    remisionesEntrega.Estatus = (string)dr.GetValue(dr.GetOrdinal("Rem_Estatus"));
                    remisionesEntrega.Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Rem_Fecha"));
                    remisionesEntrega.Numero = (int)dr.GetValue(dr.GetOrdinal("Id_Rem"));
                    remisionesEntrega.Pedido = (int)dr.GetValue(dr.GetOrdinal("Id_Ped"));
                    remisionesEntrega.Cliente = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    remisionesEntrega.Num_Cliente = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    remisionesEntrega.Fecha2 = !string.IsNullOrEmpty(dr.GetValue(dr.GetOrdinal("Rem_FechaEntrega")).ToString()) ? (DateTime)dr.GetValue(dr.GetOrdinal("Rem_FechaEntrega")) : DateTime.Now;
                    List.Add(remisionesEntrega);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarProRemisionEntrega(int Id_Emp, int Id_Cd, int Id_U, RemisionesEntrega remision, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                         "@Id_Emp",        
                                         "@Id_Cd",   
                                         "@Id_U",
                                         "@Id_Rem",
                                         "@Id_Ped"
                                      };
                object[] Valores = {                                       
                                       Id_Emp,
                                       Id_Cd,       
                                       Id_U,
                                       remision.Id_Rem,
                                       remision.Pedido                                                                        
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProRemisionesEntrega_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AutorizarRemision(ref Remision Remision, string Conexion, string Estatus)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { 
                                           "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Rem"
                                          ,"@Id_Cte" 
                                          ,"@Estatus"
                                      };
                object[] Valores = { 
                                        Remision.Id_Emp 
                                       ,Remision.Id_Cd
                                       ,Remision.Id_Rem
                                       ,100
                                       ,Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRem_Autorizar", ref dr, Parametros, Valores);
                Remision.Id_Emp = 0;
                Remision.Id_Cd = 0;
                Remision.Id_Rem = 0;
                if (dr.HasRows)
                {
                    dr.Read();


                    Remision.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")).ToString());
                    Remision.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")).ToString());
                    Remision.Id_Rem = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rem")).ToString());
                    Remision.Rem_Tipo = dr.GetValue(dr.GetOrdinal("Rem_Tipo")).ToString();
                    //rem.Rem_Fecha = Convert.ToDateTime(("Rem_Fecha").ToString());
                    Remision.Id_Tm = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tm")).ToString());
                    Remision.Id_Ped = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ped")).ToString());
                    Remision.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")).ToString());
                    Remision.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")).ToString());
                    Remision.Rem_Calle = dr.GetValue(dr.GetOrdinal("Rem_Calle")).ToString();
                    Remision.Rem_Numero = dr.GetValue(dr.GetOrdinal("Rem_Numero")).ToString();
                    Remision.Rem_Cp = dr.GetValue(dr.GetOrdinal("Rem_Cp")).ToString();
                    Remision.Rem_Colonia = dr.GetValue(dr.GetOrdinal("Rem_Colonia")).ToString();
                    Remision.Rem_Municipio = dr.GetValue(dr.GetOrdinal("Rem_Municipio")).ToString();
                    Remision.Rem_Ciudad = dr.GetValue(dr.GetOrdinal("Rem_Ciudad")).ToString();
                    Remision.Rem_Estado = dr.GetValue(dr.GetOrdinal("Rem_Estado")).ToString();
                    Remision.Rem_Rfc = dr.GetValue(dr.GetOrdinal("Rem_Rfc")).ToString();
                    Remision.Rem_Telefono = dr.GetValue(dr.GetOrdinal("Rem_Telefono")).ToString();
                    Remision.Rem_Contacto = dr.GetValue(dr.GetOrdinal("Rem_Contacto")).ToString();
                    Remision.Rem_Conducto = dr.GetValue(dr.GetOrdinal("Rem_Conducto")).ToString();
                    Remision.Rem_Guia = dr.GetValue(dr.GetOrdinal("Rem_Guia")).ToString();
                    //remision.Rem_FechaEntrega = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Rem_FechaEntrega")).ToString());
                    //remision.Rem_HoraEntrega = dr.GetValue(dr.GetOrdinal("Rem_HoraEntrega")).ToString();
                    Remision.Rem_Nota = dr.GetValue(dr.GetOrdinal("Rem_Nota")).ToString();
                    Remision.Rem_Subtotal = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Rem_Subtotal")).ToString());
                    Remision.Rem_Iva = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Rem_Iva")).ToString());
                    Remision.Rem_Total = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Rem_Total")).ToString());
                    Remision.Rem_Estatus = dr.GetValue(dr.GetOrdinal("Rem_Estatus")).ToString();
                    Remision.ZonIva = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ZonIva")).ToString());
                    Remision.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")).ToString());
                    Remision.TMITipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TMITipo")).ToString());
                    Remision.TMINombre = dr.GetValue(dr.GetOrdinal("TMINombre")).ToString();
                    //remision.RemPedCli = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RemPedCli")).ToString());
                    //remision.RemCtto = dr.GetValue(dr.GetOrdinal("RemCtto")).ToString();
                    //remision.RemFVig = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("RemFVig")).ToString());
                    //remision.Rem_ManAut = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_ManAut")).ToString());
                    //remision.Id_Vap = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Vap")).ToString());
                    //remision.Rem_PDF = dr.GetValue(dr.GetOrdinal("Rem_PDF")).ToString();
                    //remision.Rem_OrdCompra = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_OrdCompra")).ToString());
                    //remision.Rem_FechaHr = dr.GetValue(dr.GetOrdinal("Rem_FechaHr")).ToString();
                    //remision.Rem_CteCuentaNAcional = dr.GetValue(dr.GetOrdinal("Rem_CteCuentaNAcional")).ToString();
                    //remision.Rem_CteCuentaContNacional = dr.GetValue(dr.GetOrdinal("Rem_CteCuentaContNacional")).ToString();
                    //remision.Rem_FecCan = dr.GetValue(dr.GetOrdinal("Rem_FecCan")).ToString();
                    //remision.Id_UCancelo = dr.GetValue(dr.GetOrdinal("Id_UCancelo")).ToString();
                    //remision.Id_TG = dr.GetValue(dr.GetOrdinal("Id_TG")).ToString();
                    Remision.UCorreo = dr.GetValue(dr.GetOrdinal("U_Correo")).ToString();
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RechazarRemision(Remision Rem, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { 
                                           "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Rem"
                                          ,"@Id_Cte"                                  
                                      };
                object[] Valores = { 
                                        Rem.Id_Emp 
                                       ,Rem.Id_Cd
                                       ,Rem.Id_Rem
                                       ,100
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRem_Rechazar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
