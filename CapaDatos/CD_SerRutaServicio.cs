using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_SerRutaServicio
    {
        public void ConsultaDatosReporte(Sesion sesion, InvExcesoInventario inventario, ref List<InvExcesoInventario> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spReporteExcesoInventario", ref dr, Parametros, Valores);

                InvExcesoInventario excesoinventario;
                while (dr.Read())
                {
                    excesoinventario = new InvExcesoInventario();
                    List.Add(excesoinventario);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


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
    }
}
