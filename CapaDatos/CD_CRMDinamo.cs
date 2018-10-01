using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CRMDinamo
    {

        public List<CapaEntidad.EntradaCDReporteDinamo> spRepCRMC_onsolidadoRik(int TipoCd, int Anio, int Mes, int TipoRik, int Id_U, string Conexion)
        {
            List<CapaEntidad.EntradaCDReporteDinamo> lst = new List<CapaEntidad.EntradaCDReporteDinamo>();

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@TipoCd", 
                                          "@Anio",
                                          "@Mes",
                                          "@TipoRik",
                                          "@Id_U"
                                      };

                object[] Valores = { 
                                       TipoCd  , 
                                       Anio,
                                       Mes ,
                                       TipoRik,
                                       120 // TODO: Modificar por Id_U                                       
                                   };
                
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepCRMC_onsolidadoRik", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CapaEntidad.EntradaCDReporteDinamo Obj = new CapaEntidad.EntradaCDReporteDinamo();
                    //CDI
                    Obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));                    
                    Obj.Cd_Nombre = dr.GetValue(dr.GetOrdinal("cd_Nombre")).ToString();
                    Obj.CD_Zona = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Zona")));
                    Obj.ZonaNombre = dr.GetValue(dr.GetOrdinal("ZonaNombre")).ToString();                    
                    Obj.Zona = dr.GetValue(dr.GetOrdinal("Zona")).ToString();
                    Obj.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    Obj.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    Obj.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Representante")).ToString();
                    Obj.EsZona = false;
                    if (Obj.Id_Cd == 0)
                    {
                        Obj.EsZona = true;                        
                        Obj.Rik_Nombre = Obj.ZonaNombre;
                        Obj.Cd_Nombre = "";
                    }
                    Obj.ProyectosIngresados_Num = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("NpA"))); // - Num Proy
                    Obj.ProyectosIngresados_Importe = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MpA")));

                    Obj.ProyectosPromocion_Importe = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MontoProm")));
                    Obj.ProyectosPromocion_Cumplimiento = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorcentajeCierre")));

                    Obj.ProyectosCierre_Monto = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorcentajeProy")));
                    Obj.ProyectosCierre_Cumplimiento = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MontoCerr")));

                    Obj.ProyectosCancelados_Num = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("NpC")));
                    Obj.ProyectosCancelados_Importe = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MpC")));
                    Obj.EntradasFrecuencia = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Frecuencia")));                                   
                    lst.Add(Obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                lst = null;
            }
            return lst;
        }

        public List<CapaEntidad.EntradaCDReporteDinamo> spRepCRM_Consolidado(int TipoCd, int Anio, int Mes, int TipoRik, int Id_U, string Conexion) 
        {
            List<CapaEntidad.EntradaCDReporteDinamo> lst = new List<CapaEntidad.EntradaCDReporteDinamo>();

           try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@TipoCd", 
                                          "@Anio",
                                          "@Mes",
                                          "@TipoRik",
                                          "@Id_U"
                                      };

                object[] Valores = { 
                                       TipoCd  , 
                                       Anio,
                                       Mes ,
                                       TipoRik,
                                       120 // TODO: Modificar por Id_U                                       
                                   };

                                                                 
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepCRM_Consolidado", ref dr, Parametros, Valores);
                               
                while (dr.Read())
                {
                    CapaEntidad.EntradaCDReporteDinamo Obj = new CapaEntidad.EntradaCDReporteDinamo();

                    //Obj.EsZona = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Zona")));                    
                    Obj.ZonaNombre= dr.IsDBNull(dr.GetOrdinal("ZonaNombre")) ? "" : dr.GetValue(dr.GetOrdinal("ZonaNombre")).ToString();
                
                    Obj.CD_Zona = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Zona")));
                    Obj.Zona = dr.IsDBNull(dr.GetOrdinal("Zona")) ? "" : dr.GetValue(dr.GetOrdinal("Zona")).ToString();                    
    
                    //CDI
                    Obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Obj.Cd_Nombre = dr.IsDBNull(dr.GetOrdinal("cd_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("cd_Nombre")).ToString();

                    Obj.EsZona = false;
                    if (Obj.Id_Cd == 0)
                    {
                        Obj.EsZona = true;
                        Obj.Cd_Nombre = Obj.ZonaNombre;
                    }                                        
                    //Proyectos Ingresados En el Mes
                    Obj.ProyectosIngresados_Num = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("NpA"))); // - Num Proyectos                    
                    Obj.ProyectosIngresados_Importe = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MpA"))); // - Importe proy.

                    //Proyectos Promocion                    
                    Obj.ProyectosPromocion_Monto = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MontoProm")));// - Monto Proy
                    Obj.ProyectosPromocion_Cumplimiento = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorcentajeProy")));// - Cumplimiento
                    Obj.ProyectosPromocion_Plantilla = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Plantilla")));// -Plantilla
                    
                    //Cierre 
                    Obj.ProyectosCierre_Monto = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MontoCerr"))); // - Monto Cierre
                    Obj.ProyectosCierre_Cumplimiento = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorcentajeCierre"))); // - Cumplimiento
                    Obj.ProyectosCierre_Plantilla = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Plantilla"))); // - Plantilla
                    
                    //Cancelados 
                    Obj.ProyectosCancelados_Num = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("NpC"))); // - Num Proy
                    Obj.ProyectosCancelados_Importe = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MpC"))); // - Importe Proy
                    
                    //Entradas / Frecuencia
                    Obj.EntradasFrecuencia = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Frecuencia"))); // - Frecuencia

                    lst.Add(Obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);               
            }
            catch (Exception ex)
            {
                //throw ex;
                lst = null;
            }
           return lst;
    }

        public List<CapaEntidad.EntradaCDReporteDinamo> spRepCRM_ConsolidadoTrimestral(int TipoCd, int Anio, int Mes, int TipoRik, int Id_U, string Conexion)
        {
            // Listado Promedio 2 Mese *SI* , Listado RIKs *NO* 

            List<CapaEntidad.EntradaCDReporteDinamo> lst = new List<CapaEntidad.EntradaCDReporteDinamo>();

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@TipoCd", 
                                          "@Anio",
                                          "@Mes",
                                          "@TipoRik",
                                          "@Id_U"
                                      };

                object[] Valores = { 
                                       TipoCd  , 
                                       Anio,
                                       Mes ,
                                       TipoRik,
                                       120 // TODO: Modificar por Id_U                                       
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SPRepCrmConsolidado_Trimestral", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CapaEntidad.EntradaCDReporteDinamo Obj = new CapaEntidad.EntradaCDReporteDinamo();

                    //Obj.EsZona = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Zona")));                    
                    Obj.ZonaNombre = dr.IsDBNull(dr.GetOrdinal("ZonaNombre")) ? "" : dr.GetValue(dr.GetOrdinal("ZonaNombre")).ToString();

                    Obj.CD_Zona = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Zona")));
                    Obj.Zona = dr.IsDBNull(dr.GetOrdinal("Zona")) ? "" : dr.GetValue(dr.GetOrdinal("Zona")).ToString();

                    //CDI
                    Obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Obj.Cd_Nombre = dr.IsDBNull(dr.GetOrdinal("cd_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("cd_Nombre")).ToString();

                    Obj.EsZona = false;
                    if (Obj.Id_Cd == 0)
                    {
                        Obj.EsZona = true;
                        Obj.Cd_Nombre = Obj.ZonaNombre;
                        //Obj.Cd_Nombre = "AAS";
                    }
                    //Proyectos Ingresados En el Mes
                    Obj.ProyectosIngresados_Num = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("NpA"))); // - Num Proyectos                    
                    Obj.ProyectosIngresados_Importe = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MpA"))); // - Importe proy.

                    //Proyectos Promocion                    
                    Obj.ProyectosPromocion_Monto = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MontoProm")));// - Monto Proy
                    Obj.ProyectosPromocion_Cumplimiento = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorcentajeProy")));// - Cumplimiento
                    Obj.ProyectosPromocion_Plantilla = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Plantilla")));// -Plantilla

                    //Cierre 
                    Obj.ProyectosCierre_Monto = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MontoCerr"))); // - Monto Cierre
                    Obj.ProyectosCierre_Cumplimiento = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorcentajeCierre"))); // - Cumplimiento
                    Obj.ProyectosCierre_Plantilla = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Plantilla"))); // - Plantilla

                    //Cancelados 
                    Obj.ProyectosCancelados_Num = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("NpC"))); // - Num Proy
                    Obj.ProyectosCancelados_Importe = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MpC"))); // - Importe Proy

                    //Entradas / Frecuencia
                    Obj.EntradasFrecuencia = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Frecuencia"))); // - Frecuencia

                    lst.Add(Obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                lst = null;
            }
            return lst;
        }

    }
}
