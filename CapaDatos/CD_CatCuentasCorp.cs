using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatCuentasCorp
    {
        public void ConsultaCuentasCorp(int Id_Emp, string Conexion, ref List<CuentasCorp> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCuentasCorp_Consulta", ref dr, Parametros, Valores);

                CuentasCorp segmento;
                while (dr.Read())
                {
                    segmento = new CuentasCorp();
                    segmento.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    segmento.Id_Cc = (int)dr.GetValue(dr.GetOrdinal("Id_Cc"));
                    segmento.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Cc_Descripcion"));
                    segmento.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cc_Activo")));
                    if (Convert.ToBoolean(segmento.Estatus))
                    {
                        segmento.EstatusStr = "Activo";
                    }
                    else
                    {
                        segmento.EstatusStr = "Inactivo";
                    }
                    List.Add(segmento);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCuentasCorp(CuentasCorp CuentasCorp, string Conexion, ref int verificador)
        {
            try
            {
                
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Cc",
	                                    "@Cc_Descripcion", 
                                        "@Cc_Activo"
                                      };
                object[] Valores = { 
                                        CuentasCorp.Id_Emp,
                                        CuentasCorp.Id_Cc,
                                        CuentasCorp.Descripcion,
                                        CuentasCorp.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCuentasCorp_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCuentasCorp(CuentasCorp CuentasCorp, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Cc",
	                                    "@Cc_Descripcion", 
                                        "@Cc_Activo"
                                      };
                object[] Valores = { 
                                        CuentasCorp.Id_Emp,
                                        CuentasCorp.Id_Cc,
                                        CuentasCorp.Descripcion,
                                        CuentasCorp.Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCuentasCorp_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
