using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using CapaEntidad;

using System.Data;

namespace CapaDatos
{
    public class CD_CrmInformes
    {
        public void ConsultarUENSegmentosTerritoriosSucursal(int Id_Emp, int Id_Cd, ref DataSet ds, string Conexion)
        {
            try
            {
                //SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCrmInformes_UENSegmentosTerritorios_Consultar", ref ds, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarUENS(int Id_Emp, int Id_Cd, int Id_Rik, ref DataTable dt, string Conexion)
        {
            try
            {
                //SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Rik"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd
                                       ,Id_Rik
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRikUen_Consulta", "tabla", ref dt, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarSegmentos(int Id_Emp, int Id_Cd, int Id_Rik, int Id_Uen, ref DataTable dt, string Conexion)
        {
            try
            {
                //SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Rik"
                                          ,"@Id_Uen"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd
                                       ,Id_Rik
                                       ,Id_Uen
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmentosRik_Consulta", "tabla", ref dt, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarAreasSegmento(bool activo, int Id_Emp, int Id_Seg, ref DataTable dt, string Conexion)
        {
            try
            {
                //SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id1" 
                                          ,"@Id2"
                                          ,"@Id3"
                                      };
                object[] Valores = { 
                                       activo
                                       ,Id_Emp
                                       ,Id_Seg
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAreaSegmento_Crm", "tabla", ref dt, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarTerritoriosRik(int Id_Emp, int Id_Cd, int Id_Rik, int Id_Seg, ref DataTable dt, string Conexion)
        {
            try
            {
                //SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Rik"
                                          ,"@Id_Seg"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd
                                       ,Id_Rik
                                       ,Id_Seg
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRikTerr_Crm_Consulta", "tabla", ref dt, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarSolucionesArea(int Id_Emp, int Id_Area, ref DataTable dt, string Conexion)
        {
            try
            {
                //SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Area"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Area
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSolucionesArea_Crm_Consulta", "tabla", ref dt, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarAplicacionesSoluciones(int Id_Emp, int Id_Solucion, ref DataTable dt, string Conexion)
        {
            try
            {
                //SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Solucion"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Solucion
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spAplicacionesSolucion_Crm_Consulta", "tabla", ref dt, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void GenerarControlPromocion_Limpieza(int Id_Emp, int Id_Cd, string Id_Rik, int IntConsulta, double monto1, double monto2,  ref DataSet ds, string Conexion)
        //{
        //    try
        //    {
        //        CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
        //        string[] Parametros = { 
        //                                  "@Id_Emp" 
        //                                  ,"@Id_Cd"
        //                                  ,"@Id_Rik"
        //                                  ,"@IntConsulta"
        //                                  ,"@Monto1"
        //                                  ,"@Monto2"
        //                              };
        //        object[] Valores = { 
        //                               Id_Emp
        //                               ,Id_Cd
        //                               ,Id_Rik
        //                               ,IntConsulta
        //                               ,monto1
        //                               ,monto2
        //                           };

        //        SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ControlPromocion_LimpiezaSalud", ref ds, Parametros, Valores);

        //        CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void GenerarControlPromocion_LimpiezaAplicacion(int Id_Emp, int Id_Cd, int Id_U, string Id_Rik, int periodo, int IntConsulta, string monto1, string monto2, bool Pnuevo,ref DataSet ds, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_U"
                                          ,"@Id_Rik"
                                          ,"@Periodo"
                                          ,"@IntConsulta"
                                          ,"@Monto1"
                                          ,"@Monto2"
                                          ,"@Nuevo"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd
                                       ,Id_U
                                       ,Id_Rik == "-1" ? (object)null: Id_Rik
                                       ,periodo
                                       ,IntConsulta
                                       ,!string.IsNullOrEmpty(monto1)? Convert.ToDouble(monto1) : (object)null
                                       ,!string.IsNullOrEmpty(monto2)? Convert.ToDouble(monto2) : (object)null
                                       ,Pnuevo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ControlPromocion_LimpiezaAplicacion", ref ds, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GenerarControlPromocion(int Id_Emp, int Id_Cd, int Id_U, string Id_Rik, int periodo, int IntConsulta, string monto1, string monto2, ref DataSet ds, string Conexion)
        {
            try
            {
                //SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_U"
                                          ,"@Id_Rik"
                                          ,"@Periodo"
                                          ,"@IntConsulta"
                                          ,"@Monto1"
                                          ,"@Monto2"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd
                                       ,Id_U
                                       ,Id_Rik == "-1" ? (object)null: Id_Rik
                                       ,periodo
                                       ,IntConsulta
                                       ,!string.IsNullOrEmpty(monto1)? Convert.ToDouble(monto1) : (object)null
                                       ,!string.IsNullOrEmpty(monto2)? Convert.ToDouble(monto2) : (object)null
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ControlPromocion", ref ds, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void spCRM_ControlPromocion_GteSegmento(int Id_Emp, int Id_Cd, int periodo, string Id_Rik, int IntConsulta, int Id_GteSeg, ref DataSet ds, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Rik"
                                          ,"@Periodo"
                                          ,"@IntConsulta"
                                          ,"@Id_GteSeg"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd
                                       ,Id_Rik
                                       ,periodo
                                       ,IntConsulta
                                       ,Id_GteSeg
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ControlPromocion_GteSegmento", ref ds, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void spCRM_ControlPromocion_DII(int Id_Emp, int Id_Cd, int periodo, string Id_Rik, ref DataSet ds, string Conexion)
        {
            try
            {
                //SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Rik"
                                          ,"@Periodo"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd
                                       ,Id_Rik
                                       ,periodo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ControlPromocion_DII", ref ds, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void spCRM_ControlEntrada(int Id_Emp, int Id_Cd, int periodo, string Id_Rik, ref DataSet ds, string Conexion)
        {
            try
            {
                //SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Rik"
                                          ,"@Periodo"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd
                                       ,Id_Rik
                                       ,periodo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ControlEntrada", ref ds, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void spCRM_Campana(int Id_Emp, int Id_Cd, int periodo, string Id_Rik, ref DataSet ds, string Conexion)
        {
            try
            {
                //SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Rik"
                                          ,"@Periodo"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd
                                       ,Id_Rik
                                       ,periodo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCampana_Consultar", ref ds, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GenerarCierreMes(int Id_Emp, int Id_Cd, int Id_U, string Id_Rik, int periodo, int IntConsulta, string monto1, string monto2, bool Pnuevo, ref DataSet ds, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_U"
                                          ,"@Id_Rik"
                                          ,"@Periodo"
                                          ,"@IntConsulta"
                                          ,"@Monto1"
                                          ,"@Monto2"
                                          ,"@Nuevo"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd
                                       ,Id_U
                                       ,Id_Rik == "-1" ? (object)null: Id_Rik
                                       ,periodo
                                       ,IntConsulta
                                       ,!string.IsNullOrEmpty(monto1)? Convert.ToDouble(monto1) : (object)null
                                       ,!string.IsNullOrEmpty(monto2)? Convert.ToDouble(monto2) : (object)null
                                       ,Pnuevo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_CierreMes", ref ds, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void spCRM_ControlPromocion_DIINumero(int Id_Emp, int Id_Cd, int periodo, string Id_Rik, ref DataSet ds, string Conexion)
        {
            try
            {
                //SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Rik"
                                          ,"@Periodo"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd
                                       ,Id_Rik
                                       ,periodo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ControlPromocion_DIINumero", ref ds, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
