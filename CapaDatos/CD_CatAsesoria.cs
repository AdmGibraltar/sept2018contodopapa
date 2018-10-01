using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatAsesoria
    {
        public void ConsultaAsesoria(Asesoria asesoria, string Conexion, int id_Emp, ref List<Asesoria> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAsesoria_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    asesoria = new Asesoria();
                    asesoria.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    asesoria.Id_Ase = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ase")));
                    asesoria.Ase_Descripcion = dr.GetValue(dr.GetOrdinal("Ase_Descripcion")).ToString();
                    asesoria.Ase_Revision = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ase_Revision")));
                    asesoria.Ase_Costo = Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Ase_Costo")));
                    asesoria.Ase_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ase_Activo")));
                    asesoria.Ase_ActivoStr = dr.GetValue(dr.GetOrdinal("Ase_ActivoStr")).ToString();
                    List.Add(asesoria);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarAsesoria(Asesoria asesoria, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                        "@Id_Emp"
                                        ,"@Id_Ase"
                                        ,"@Ase_Descripcion"
                                        ,"@Ase_Revision"
                                        ,"@Ase_Costo"
                                        ,"@Ase_Activo"
                                      };
                object[] Valores = { 
                                        asesoria.Id_Emp
                                        ,asesoria.Id_Ase
                                        ,asesoria.Ase_Descripcion
                                        ,asesoria.Ase_Revision
                                        ,asesoria.Ase_Costo
                                        ,asesoria.Ase_Activo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAsesoria_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarAsesoria(Asesoria asesoria, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp"
                                        ,"@Id_Ase"
                                        ,"@Ase_Descripcion"
                                        ,"@Ase_Revision"
                                        ,"@Ase_Costo"
                                        ,"@Ase_Activo"
                                      };
                object[] Valores = { 
                                        asesoria.Id_Emp
                                        ,asesoria.Id_Ase
                                        ,asesoria.Ase_Descripcion
                                        ,asesoria.Ase_Revision
                                        ,asesoria.Ase_Costo
                                        ,asesoria.Ase_Activo
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAsesoria_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
