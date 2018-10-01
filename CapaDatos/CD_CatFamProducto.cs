using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatFamProducto
    {
        public void ConsultaFamProducto(FamProducto famProducto, string Conexion, int id_Emp, ref List<FamProducto> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatFamProducto_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    famProducto = new FamProducto();
                    famProducto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    famProducto.Id_Fam = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fam")));
                    famProducto.Fam_Descripcion = dr.GetValue(dr.GetOrdinal("Fam_Descripcion")).ToString();
                    famProducto.Fam_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Fam_Activo")));
                    famProducto.Fam_ActivoStr = dr.GetValue(dr.GetOrdinal("Fam_ActivoStr")).ToString();
                    List.Add(famProducto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarFamProducto(FamProducto famProducto, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp", 
	                                    "@Id_Fam", 
	                                    "@Fam_Descripcion", 
	                                    "@Fam_Activo",
                                      };
                object[] Valores = { 
                                        famProducto.Id_Emp
                                        ,famProducto.Id_Fam
                                        ,famProducto.Fam_Descripcion
                                        ,famProducto.Fam_Activo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatFamProducto_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFamProducto(FamProducto famProducto, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp", 
	                                    "@Id_Fam", 
	                                    "@Fam_Descripcion", 
	                                    "@Fam_Activo",
                                      };
                object[] Valores = { 
                                        famProducto.Id_Emp
                                        ,famProducto.Id_Fam
                                        ,famProducto.Fam_Descripcion
                                        ,famProducto.Fam_Activo
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatFamProducto_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
