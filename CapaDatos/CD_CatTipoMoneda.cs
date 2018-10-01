using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;


namespace CapaDatos
{
    public class CD_CatTipoMoneda
    {
        public void ConsultaTipoMoneda(TipoMoneda tipoMoneda, string Conexion, int id_Emp, ref List<TipoMoneda> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoMoneda_Consulta", ref dr, Parametros, Valores);
                
                while (dr.Read())
                {
                    tipoMoneda = new TipoMoneda();
                    tipoMoneda.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    tipoMoneda.Id_Mon = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Mon")));
                    tipoMoneda.Mon_Abrev = dr.GetValue(dr.GetOrdinal("Mon_Abrev")).ToString();
                    tipoMoneda.Mon_TipCambio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Mon_TipCambio")));
                    tipoMoneda.Mon_Descripcion = dr.GetValue(dr.GetOrdinal("Mon_Descripcion")).ToString();
                    tipoMoneda.Mon_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Mon_Activo")));
                    tipoMoneda.Mon_ActivoStr = dr.GetValue(dr.GetOrdinal("Mon_ActivoStr")).ToString();
                    List.Add(tipoMoneda);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTipoMoneda(TipoMoneda tipoMoneda, ICD_Contexto icdCtx, int id_Emp, ref List<TipoMoneda> List)
        {
            try
            {
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { id_Emp };

                SqlCommand sqlcmd = CD_Datos.GenerarSqlCommand("spCatTipoMoneda_Consulta", ref dr, Parametros, Valores, icdCtx);

                while (dr.Read())
                {
                    tipoMoneda = new TipoMoneda();
                    tipoMoneda.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    tipoMoneda.Id_Mon = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Mon")));
                    tipoMoneda.Mon_Abrev = dr.GetValue(dr.GetOrdinal("Mon_Abrev")).ToString();
                    tipoMoneda.Mon_TipCambio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Mon_TipCambio")));
                    tipoMoneda.Mon_Descripcion = dr.GetValue(dr.GetOrdinal("Mon_Descripcion")).ToString();
                    tipoMoneda.Mon_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Mon_Activo")));
                    tipoMoneda.Mon_ActivoStr = dr.GetValue(dr.GetOrdinal("Mon_ActivoStr")).ToString();
                    List.Add(tipoMoneda);
                }

                sqlcmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTipoMonedaIndividual(ref TipoMoneda tipoMoneda, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp"
                                         , "@Id_Mon" };
                object[] Valores = { 
                                        tipoMoneda.Id_Emp
                                        ,tipoMoneda.Id_Mon
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoMoneda_ConsultaMoneda", ref dr, Parametros, Valores);

                if(dr.HasRows)
                {
                    dr.Read();
                    
                    tipoMoneda.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    tipoMoneda.Id_Mon = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Mon")));
                    tipoMoneda.Mon_Abrev = dr.GetValue(dr.GetOrdinal("Mon_Abrev")).ToString();
                    tipoMoneda.Mon_TipCambio = Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Mon_TipCambio")));
                    tipoMoneda.Mon_Descripcion = dr.GetValue(dr.GetOrdinal("Mon_Descripcion")).ToString();
                    tipoMoneda.Mon_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Mon_Activo")));
                    tipoMoneda.Mon_ActivoStr = dr.GetValue(dr.GetOrdinal("Mon_ActivoStr")).ToString();
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarTipoMoneda(TipoMoneda tipoMoneda, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
                                        "@Id_Mon", 
                                        "@Mon_Abrev", 
                                        "@Mon_TipCambio", 
	                                    "@Mon_Descripcion", 
	                                    "@Mon_Activo",
                                      };
                object[] Valores = { 
                                        tipoMoneda.Id_Emp
                                        ,tipoMoneda.Id_Mon
                                        ,tipoMoneda.Mon_Abrev
                                        ,tipoMoneda.Mon_TipCambio
                                        ,tipoMoneda.Mon_Descripcion
                                        ,tipoMoneda.Mon_Activo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoMoneda_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarTipoMoneda(TipoMoneda tipoMoneda, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
                                        "@Id_Mon", 
                                        "@Mon_Abrev", 
                                        "@Mon_TipCambio", 
	                                    "@Mon_Descripcion", 
	                                    "@Mon_Activo",
                                      };
                object[] Valores = { 
                                        tipoMoneda.Id_Emp
                                        ,tipoMoneda.Id_Mon
                                        ,tipoMoneda.Mon_Abrev
                                        ,tipoMoneda.Mon_TipCambio
                                        ,tipoMoneda.Mon_Descripcion
                                        ,tipoMoneda.Mon_Activo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoMoneda_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
