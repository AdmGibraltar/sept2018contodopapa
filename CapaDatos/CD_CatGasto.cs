using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatGasto
    {
        public void InsertarGasto(Gasto gasto, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
              
                string[] Parametros = { 
	                                    "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Anio", 
	                                    "@Mes", 
	                                    "@VarFlet",
                                        "@VarFletPagado",
                                        "@VarFletDevolucion",
                                        "@FijGenerales",
                                        "@FijAdministracion",
                                        "@FijOcupacion",
                                        "@FijAlmacen",
                                        "@FijServicio",
                                        "@FijCobranza",
                                        "@UCS"
                                      };
                object[] Valores = { 
                                        gasto.Id_Emp,
                                        gasto.Id_Cd,
                                        gasto.Año,
                                        gasto.Mes,
                                        gasto.VarFlet,
                                        gasto.VarFletPagado,
                                        gasto.VarFletDevolucion,
                                        gasto.FijGenerales,
                                        gasto.FijAdministracion,
                                        gasto.FijOcupacion,
                                        gasto.FijAlmacen,
                                        gasto.FijServicio,
                                        gasto.FijCobranza,
                                        gasto.UCS
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatGasto_Insertar", ref verificador, Parametros, Valores);

                 
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                 
                throw ex;
            }
        }

        public void ConsultarGasto(ref Gasto gasto, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Anio","@Mes" };
                object[] Valores = { gasto.Id_Emp, gasto.Id_Cd, gasto.Año, gasto.Mes };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatGasto_Consultar", ref dr, Parametros, Valores);

                gasto = new Gasto();
               
                if (dr.HasRows)
                {
                    dr.Read();
                    
                    gasto.Año = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Gas_Anio")));
                    gasto.Mes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Gas_Mes")));
                    gasto.VarFlet = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Gas_VarFlet")));
                    gasto.VarFletPagado = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Gas_VarFletPagado")));
                    gasto.VarFletDevolucion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Gas_VarFletDevolucion")));
                    gasto.FijGenerales = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Gas_FijGenerales")));
                    gasto.FijAdministracion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Gas_FijAdministracion")));
                    gasto.FijOcupacion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Gas_FijOcupacion")));
                    gasto.FijAlmacen = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Gas_FijAlmacen")));
                    gasto.FijServicio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Gas_FijServicio")));
                    gasto.FijCobranza = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Gas_FijCobranza")));
                    gasto.UCS = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Gas_UCS")));
                   
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
