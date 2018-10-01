using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatTipoPrecio
    {
        public void ConsultaTipoPrecio(TipoPrecio tipoPrecio, string Conexion, int id_Emp, ref List<TipoPrecio> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoPrecio_Consulta", ref dr, Parametros, Valores);
                
                while (dr.Read())
                {
                    tipoPrecio = new TipoPrecio();
                    tipoPrecio.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    tipoPrecio.Id_Pre = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pre")));
                    tipoPrecio.Pre_Tipo = Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Pre_Tipo")));
                    tipoPrecio.Pre_TipoStr = dr.GetValue(dr.GetOrdinal("Pre_TipoStr")).ToString();
                    tipoPrecio.Pre_Descripcion = dr.GetValue(dr.GetOrdinal("Pre_Descripcion")).ToString();
                    tipoPrecio.Pre_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Pre_Activo")));
                    tipoPrecio.Pre_ActivoStr = dr.GetValue(dr.GetOrdinal("Pre_ActivoStr")).ToString();
                    List.Add(tipoPrecio);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarTipoPrecio(TipoPrecio tipoPrecio, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
                                        "@Id_Pre", 
	                                    "@Pre_Descripcion", 
                                        "@Pre_Tipo", 
	                                    "@Pre_Activo",
                                      };
                object[] Valores = { 
                                        tipoPrecio.Id_Emp
                                        ,tipoPrecio.Id_Pre
                                        ,tipoPrecio.Pre_Descripcion
                                        ,tipoPrecio.Pre_Tipo
                                        ,tipoPrecio.Pre_Activo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoPrecio_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarTipoPrecio(TipoPrecio tipoPrecio, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
                                        "@Id_Pre", 
	                                    "@Pre_Descripcion", 
                                        "@Pre_Tipo", 
	                                    "@Pre_Activo",
                                      };
                object[] Valores = { 
                                        tipoPrecio.Id_Emp
                                        ,tipoPrecio.Id_Pre
                                        ,tipoPrecio.Pre_Descripcion
                                        ,tipoPrecio.Pre_Tipo
                                        ,tipoPrecio.Pre_Activo
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoPrecio_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
