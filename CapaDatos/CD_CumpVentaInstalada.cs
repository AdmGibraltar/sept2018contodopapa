using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CumpVentaInstalada
    {        
        public string ConsultaNombreEmpresa(Sesion sesion)
        {
            string empresa = string.Empty;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { sesion.Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spNombreEmpresa", ref dr, Parametros, Valores);
                                
                while (dr.Read())
                {                  
                    empresa = (string)dr.GetValue(dr.GetOrdinal("Emp_Nombre"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return empresa;
        }

        public string ConsultaNombreSucursal(Sesion sesion)
        {
            string sucursal = string.Empty;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spNombreSucursal", ref dr, Parametros, Valores);
                                
                while (dr.Read())
                {                  
                    sucursal = (string)dr.GetValue(dr.GetOrdinal("Cd_Nombre"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sucursal;
        }

        public void ConsultaTotalesVenta(Sesion sesion, CumpVentaInstalada venta, ref CumpVentaInstalada ventaInstalada)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);  
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Formato", "@Semana", "@RIK", "@Territorio", "@Producto", "@Nivel", "@Detalle" };
                object[] Valores = { sesion.Id_Emp, venta.Id_cd, 6, venta.Semana, venta.Rik, venta.Territorio, venta.Producto, venta.Nivel, venta.Detalle };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spReporteCumpVentaInstalada", ref dr, Parametros, Valores);
                                           
                while (dr.Read())
                {                   
                    ventaInstalada = new CumpVentaInstalada();
                    ventaInstalada.TotalFacturado = (double)dr.GetValue(dr.GetOrdinal("Total1"));
                    ventaInstalada.TotalNca = (double)dr.GetValue(dr.GetOrdinal("Total2"));
                    ventaInstalada.TotalNcr = (double)dr.GetValue(dr.GetOrdinal("Total3"));
                    ventaInstalada.TotalDevParciales = (double)dr.GetValue(dr.GetOrdinal("Total4"));
                    ventaInstalada.VtaDirecta1 = (double)dr.GetValue(dr.GetOrdinal("Total5"));
                    ventaInstalada.VIF1 = (double)dr.GetValue(dr.GetOrdinal("Total6"));
                    ventaInstalada.VIFFueraPeriodo1 = (double)dr.GetValue(dr.GetOrdinal("Total7"));
                    ventaInstalada.VINueva1 = (double)dr.GetValue(dr.GetOrdinal("Total8"));
                    ventaInstalada.VtaEsporadica1 = (double)dr.GetValue(dr.GetOrdinal("Total9"));
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
