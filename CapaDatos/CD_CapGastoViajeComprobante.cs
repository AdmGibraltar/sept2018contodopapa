using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CapGastoViajeComprobante
    {
        public void InsertarGastoViajeComprobante(GastoViajeComprobante gastoViajeComprobante, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd",
                                        "@Id_GV",
                                        "@Id_GVComprobante",
                                        "@GVComprobante_Fecha",
                                        "@Id_GVComprobanteTipo",
                                        "@GVComprobante_ConComprobante",
                                        "@GVComprobante_Xml",
                                        "@GVComprobante_XmlStream",
                                        "@GVComprobante_Pdf",
                                        "@GVComprobante_Observaciones",
                                        "@GVComprobante_Importe",
                                        "@GVSerie",
	                                    "@GVFolio",
	                                    "@GV_Cuenta",
	                                    "@GV_Cc",
	                                    "@GV_CuentaPago",
                                        "@GV_Numero",
                                        "@GV_SubCuenta",
                                        "@GV_SubSubCuenta"
                                      };

                 object[] Valores = { 
                                       gastoViajeComprobante.Id_Emp,
                                       gastoViajeComprobante.Id_Cd,
                                       gastoViajeComprobante.Id_GV,
                                       gastoViajeComprobante.Id_GVComprobante,
                                       gastoViajeComprobante.GVComprobante_Fecha,
                                       gastoViajeComprobante.Id_GVComprobanteTipo,
                                       gastoViajeComprobante.GVComprobante_ConComprobante,
                                       gastoViajeComprobante.GVComprobante_Xml,
                                       gastoViajeComprobante.GVComprobante_XmlStream,
                                       gastoViajeComprobante.GVComprobante_Pdf,
                                       gastoViajeComprobante.GVComprobante_Observaciones,
                                       gastoViajeComprobante.GVComprobante_Importe,
                                       gastoViajeComprobante.GVComprobante_Serie,
                                       gastoViajeComprobante.GVComprobante_Folio,
                                       gastoViajeComprobante.GVComprobante_GV_Cuenta,
                                       gastoViajeComprobante.GVComprobante_GV_Cc,
                                       gastoViajeComprobante.GVComprobante_GV_CuentaPago,
                                       gastoViajeComprobante.GVComprobante_GV_Numero,
                                       gastoViajeComprobante.GVComprobante_GV_SubCuenta,
                                       gastoViajeComprobante.GVComprobante_GV_SubSubCuenta
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGastoViajeComprobante_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarGastoViajeComprobante(GastoViajeComprobante gastoViajeComprobante, string Conexion, ref int verificador)
        {
            try
            {
                
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_GV", "@Id_GVComprobante" };
                object[] Valores = { 
                                       gastoViajeComprobante.Id_Emp, 
                                       gastoViajeComprobante.Id_Cd,
                                       gastoViajeComprobante.Id_GV,
                                       gastoViajeComprobante.Id_GVComprobante
 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGastoViajeComprobante_Eliminar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaGastoViajeComprobante(GastoViajeComprobante gastoViajeComprobante, string Conexion, ref List<GastoViajeComprobante> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_GV" };
                object[] Valores = { 
                                       gastoViajeComprobante.Id_Emp, 
                                       gastoViajeComprobante.Id_Cd,
                                       gastoViajeComprobante.Id_GV
 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGastoViajeComprobante_Lista", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    gastoViajeComprobante = new GastoViajeComprobante();
                    gastoViajeComprobante.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    gastoViajeComprobante.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    gastoViajeComprobante.Id_GV = (int)dr.GetValue(dr.GetOrdinal("Id_GV"));
                    gastoViajeComprobante.Id_GVComprobante = (int)dr.GetValue(dr.GetOrdinal("Id_GVComprobante"));
                    gastoViajeComprobante.GVComprobante_Fecha = DateTime.Parse(dr["GVComprobante_Fecha"].ToString());
                    gastoViajeComprobante.Id_GVComprobanteTipo = Int32.Parse(dr["Id_GVComprobanteTipo"].ToString());
                    //JFCV  la columna de descripción ya no se va a mostrar en el grid
                    //gastoViajeComprobante.GVComprobanteTipo_Descripcion = dr["GVComprobanteTipo_Descripcion"].ToString();
                    gastoViajeComprobante.GVComprobante_ConComprobante = Boolean.Parse(dr["GVComprobante_ConComprobante"].ToString());
                    gastoViajeComprobante.GVComprobante_ConComprobanteDescripcion = dr["GVComprobante_ConComprobanteDescripcion"].ToString();
                    gastoViajeComprobante.GVComprobante_Xml = dr["GVComprobante_Xml"].ToString();
                    gastoViajeComprobante.GVComprobante_XmlStream = dr["GVComprobante_XmlStream"] == System.DBNull.Value ? null : (byte[])(dr["GVComprobante_XmlStream"]);
                    gastoViajeComprobante.GVComprobante_Pdf = dr["GVComprobante_Pdf"] == System.DBNull.Value ? null : (byte[])(dr["GVComprobante_Pdf"]);
                    gastoViajeComprobante.GVComprobante_Observaciones = dr["GVComprobante_Observaciones"].ToString();
                    gastoViajeComprobante.GVComprobante_Importe = decimal.Parse(dr["GVComprobante_Importe"].ToString());
                    gastoViajeComprobante.GVComprobante_GV_CuentaPago = dr["GV_CuentaPago"].ToString();
                    gastoViajeComprobante.GVComprobante_GV_Cuenta = dr["GV_Cuenta"].ToString();
                    gastoViajeComprobante.GVComprobante_GV_Cc = dr["GV_Cc"].ToString();
                    gastoViajeComprobante.GVComprobante_GV_Numero = dr["GV_Numero"].ToString();
                    gastoViajeComprobante.GVComprobante_GV_SubCuenta = dr["GV_SubCuenta"].ToString();
                    gastoViajeComprobante.GVComprobante_GV_SubSubCuenta = dr["GV_SubSubCuenta"].ToString();
                    gastoViajeComprobante.GVComprobante_Serie = dr["GVComprobante_Serie"].ToString();
                    gastoViajeComprobante.GVComprobante_Folio = dr["GVComprobante_Folio"].ToString();
                    list.Add(gastoViajeComprobante);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaGastoViajeComprobante(GastoViajeComprobante gastoViajeComprobante, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_GV", "@Id_GVComprobante" };
                object[] Valores = { 
                                       gastoViajeComprobante.Id_Emp, 
                                       gastoViajeComprobante.Id_Cd,
                                       gastoViajeComprobante.Id_GV,
                                       gastoViajeComprobante.Id_GVComprobante
 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGastoViajeComprobante_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    gastoViajeComprobante.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    gastoViajeComprobante.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    gastoViajeComprobante.Id_GV = (int)dr.GetValue(dr.GetOrdinal("Id_GV"));
                    gastoViajeComprobante.Id_GVComprobante = (int)dr.GetValue(dr.GetOrdinal("Id_GVComprobante"));
                    gastoViajeComprobante.GVComprobante_Fecha = DateTime.Parse(dr["GVComprobante_Fecha"].ToString());
                    gastoViajeComprobante.Id_GVComprobanteTipo = Int32.Parse(dr["Id_GVComprobanteTipo"].ToString());
                    //JFCV  la columna de descripción ya no se va a mostrar en el grid
                    //gastoViajeComprobante.GVComprobanteTipo_Descripcion = dr["GVComprobanteTipo_Descripcion"].ToString();
                    gastoViajeComprobante.GVComprobante_ConComprobante = Boolean.Parse(dr["GVComprobante_ConComprobante"].ToString());
                    gastoViajeComprobante.GVComprobante_ConComprobanteDescripcion = dr["GVComprobante_ConComprobanteDescripcion"].ToString();
                    gastoViajeComprobante.GVComprobante_Xml = dr["GVComprobante_Xml"].ToString();
                    gastoViajeComprobante.GVComprobante_XmlStream = dr["GVComprobante_XmlStream"] == System.DBNull.Value ? null : (byte[])(dr["GVComprobante_XmlStream"]);
                    gastoViajeComprobante.GVComprobante_Pdf = dr["GVComprobante_Pdf"] == System.DBNull.Value ? null : (byte[])(dr["GVComprobante_Pdf"]);
                    gastoViajeComprobante.GVComprobante_Observaciones = dr["GVComprobante_Observaciones"].ToString();
                    gastoViajeComprobante.GVComprobante_Importe = decimal.Parse(dr["GVComprobante_Importe"].ToString());
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
