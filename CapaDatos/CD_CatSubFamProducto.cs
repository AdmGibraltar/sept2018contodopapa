using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatSubFamProducto
    {
        public void ConsultaSubFamProducto(SubFamProducto subFamProducto, string Conexion, int id_Emp, ref List<SubFamProducto> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSubFamProducto_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    subFamProducto = new SubFamProducto();
                    subFamProducto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    subFamProducto.Id_Fam = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fam")));
                    subFamProducto.Id_Fam_Str = dr.GetValue(dr.GetOrdinal("Id_Fam_Str")).ToString();
                    subFamProducto.Id_Sub = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Sub")));
                    subFamProducto.Sub_Descripcion = dr.GetValue(dr.GetOrdinal("Sub_Descripcion")).ToString();
                    subFamProducto.Sub_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Sub_Activo")));
                    subFamProducto.Sub_ActivoStr = dr.GetValue(dr.GetOrdinal("Sub_ActivoStr")).ToString();

                    List.Add(subFamProducto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarSubFamProducto(SubFamProducto subFamProducto, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp", 
                                        "@Id_Fam", 
	                                    "@Id_Sub", 
	                                    "@Sub_Descripcion", 
	                                    "@Sub_Activo",
                                      };
                object[] Valores = { 
                                        subFamProducto.Id_Emp
                                        ,subFamProducto.Id_Fam
                                        ,subFamProducto.Id_Sub
                                        ,subFamProducto.Sub_Descripcion
                                        ,subFamProducto.Sub_Activo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSubFamProducto_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarSubFamProducto(SubFamProducto subFamProducto, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp", 
                                        "@Id_Fam", 
	                                    "@Id_Sub", 
	                                    "@Sub_Descripcion", 
	                                    "@Sub_Activo",
                                      };
                object[] Valores = { 
                                        subFamProducto.Id_Emp
                                        ,subFamProducto.Id_Fam
                                        ,subFamProducto.Id_Sub
                                        ,subFamProducto.Sub_Descripcion
                                        ,subFamProducto.Sub_Activo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSubFamProducto_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
