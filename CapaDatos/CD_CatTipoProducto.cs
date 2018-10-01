using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CapaEntidad;
using System.Data.SqlClient;


namespace CapaDatos
{
    public class CD_CatTipoProducto
    {
        public void ConsultaTipoProducto(TipoProducto tipoProducto, string Conexion, int id_Emp, ref List<TipoProducto> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoProducto_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    tipoProducto = new TipoProducto();
                    tipoProducto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    tipoProducto.Id_Ptp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ptp")));
                    tipoProducto.Ptp_Descripcion = dr.GetValue(dr.GetOrdinal("Ptp_Descripcion")).ToString();
                    tipoProducto.Ptp_Tipo = dr.GetValue(dr.GetOrdinal("Ptp_Tipo")).ToString();
                    tipoProducto.Ptp_Tipo_Str = dr.GetValue(dr.GetOrdinal("Ptp_Tipo_Str")).ToString();
                    tipoProducto.Ptp_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ptp_Activo")));
                    tipoProducto.Ptp_ActivoStr = dr.GetValue(dr.GetOrdinal("Ptp_ActivoStr")).ToString();
                    List.Add(tipoProducto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarTipoProducto(TipoProducto tipoProducto, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
                                        "@Id_Ptp", 
	                                    "@Ptp_Descripcion", 
                                        "@Ptp_Tipo", 
	                                    "@Ptp_Activo",
                                      };
                object[] Valores = { 
                                        tipoProducto.Id_Emp
                                        ,tipoProducto.Id_Ptp
                                        ,tipoProducto.Ptp_Descripcion
                                        ,tipoProducto.Ptp_Tipo
                                        ,tipoProducto.Ptp_Activo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoProducto_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarTipoProducto(TipoProducto tipoProducto, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
                                        "@Id_Ptp", 
	                                    "@Ptp_Descripcion", 
                                        "@Ptp_Tipo", 
	                                    "@Ptp_Activo",
                                      };
                object[] Valores = { 
                                        tipoProducto.Id_Emp
                                        ,tipoProducto.Id_Ptp
                                        ,tipoProducto.Ptp_Descripcion
                                        ,tipoProducto.Ptp_Tipo
                                        ,tipoProducto.Ptp_Activo
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoProducto_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
