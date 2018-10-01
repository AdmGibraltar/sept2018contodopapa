using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatFormaPago
    {
        public void ConsultaFormaPago(int Id_Emp, string Conexion, ref List<FormaPago> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatFormaPago_Consulta", ref dr, Parametros, Valores);

                FormaPago segmento;
                while (dr.Read())
                {
                    segmento = new FormaPago();
                    segmento.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    segmento.Id_Fpa = (int)dr.GetValue(dr.GetOrdinal("Id_Fpa"));
                    segmento.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Fpa_Descripcion"));
                    segmento.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Fpa_Activo")));
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

        public void InsertarFormaPago(FormaPago FormaPago, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Fpa",
	                                    "@Fpa_Descripcion", 
                                        "@Fpa_Activo"
                                      };
                object[] Valores = { 
                                        FormaPago.Id_Emp,
                                        FormaPago.Id_Fpa,
                                        FormaPago.Descripcion,
                                        FormaPago.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatFormaPago_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFormaPago(FormaPago FormaPago, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Fpa",
	                                    "@Fpa_Descripcion", 
                                        "@Fpa_Activo"
                                      };
                object[] Valores = { 
                                        FormaPago.Id_Emp,
                                        FormaPago.Id_Fpa,
                                        FormaPago.Descripcion,
                                        FormaPago.Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatFormaPago_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFormaPago(ref FormaPago fpago, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Fpa"
                                      };
                object[] Valores = { 
                                        fpago.Id_Emp,
                                        fpago.Id_Fpa
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatFormaPago_Consulta", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    fpago.Descripcion = dr.GetValue(dr.GetOrdinal("Fpa_Descripcion")).ToString();
                }
                else
                {
                    fpago.Descripcion = "";
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
