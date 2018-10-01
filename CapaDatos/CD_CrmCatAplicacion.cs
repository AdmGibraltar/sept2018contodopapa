using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CrmCatAplicacion
    {
        public void Lista(CapaEntidad.Aplicacion aplicacion, ref List<CapaEntidad.Aplicacion> List, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Sol" };
                object[] Valores = { aplicacion.Id_Emp, aplicacion.Id_Sol };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMAplicaciones_Lista", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    aplicacion = new CapaEntidad.Aplicacion();
                    aplicacion.Id_Apl = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Apl")));
                    aplicacion.Apl_Descripcion = dr.GetValue(dr.GetOrdinal("Apl_Descripcion")).ToString();
                    aplicacion.Apl_Potencial = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Apl_Potencial")));
                    List.Add(aplicacion);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(CapaEntidad.Aplicacion aplicacion, ref int verificador, string Conexion)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Sol", "@Apl_Descripcion", "@Apl_Potencial" };
                object[] Valores = { aplicacion.Id_Emp, aplicacion.Id_Sol, aplicacion.Apl_Descripcion, aplicacion.Apl_Potencial };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMAplicaciones_Insertar", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(CapaEntidad.Aplicacion aplicacion, ref int verificador, string Conexion)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Apl" };
                object[] Valores = { aplicacion.Id_Emp, aplicacion.Id_Apl };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMAplicaciones_Eliminar", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(CapaEntidad.Aplicacion aplicacion, ref int verificador, string Conexion)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Apl",
                                          "@Apl_Descripcion",
                                          "@Apl_Potencial",
                                      };
                object[] Valores = { 
                                       aplicacion.Id_Emp, 
                                       aplicacion.Id_Apl,
                                       aplicacion.Apl_Descripcion,
                                       aplicacion.Apl_Potencial
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCatAplicaciones_Modificar", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
