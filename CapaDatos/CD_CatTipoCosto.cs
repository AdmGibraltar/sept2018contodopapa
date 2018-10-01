using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatTipoCosto
    {
        public void ConsultaTipoCosto(TipoCosto tipoCosto, string Conexion, int id_Emp, ref List<TipoCosto> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoCosto_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    tipoCosto = new TipoCosto();
                    tipoCosto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    tipoCosto.Id_Tco = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tco")));
                    tipoCosto.Tco_Descripcion = dr.GetValue(dr.GetOrdinal("Tco_Descripcion")).ToString();
                    tipoCosto.Tco_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Tco_Activo")));
                    tipoCosto.Tco_ActivoStr = dr.GetValue(dr.GetOrdinal("Tco_ActivoStr")).ToString();
                    List.Add(tipoCosto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarTipoCosto(TipoCosto tipoCosto, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp", 
                                        "@Id_Tco", 
	                                    "@Tco_Descripcion", 
	                                    "@Tco_Activo",
                                      };
                object[] Valores = { 
                                        tipoCosto.Id_Emp
                                        ,tipoCosto.Id_Tco
                                        ,tipoCosto.Tco_Descripcion
                                        ,tipoCosto.Tco_Activo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoCosto_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarTipoCosto(TipoCosto tipoCosto, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
                                        "@Id_Tco", 
	                                    "@Tco_Descripcion", 
	                                    "@Tco_Activo",
                                      };
                object[] Valores = { 
                                        tipoCosto.Id_Emp
                                        ,tipoCosto.Id_Tco
                                        ,tipoCosto.Tco_Descripcion
                                        ,tipoCosto.Tco_Activo
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoCosto_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
