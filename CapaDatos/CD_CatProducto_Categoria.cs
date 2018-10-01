using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatProducto_Categoria
    {
        public void ConsultaCategoriaProducto(CategoriaProducto CategoriaProducto, string Conexion, ref List<CategoriaProducto> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { CategoriaProducto.Id_Emp };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProductoCategoria_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CategoriaProducto = new CategoriaProducto();
                    CategoriaProducto.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    CategoriaProducto.Id_Cpr = (int)dr.GetValue(dr.GetOrdinal("Id_Cpr"));
                    CategoriaProducto.Cpr_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Cpr_Descripcion"));
                    CategoriaProducto.Cpr_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cpr_Activo")));
                    if (Convert.ToBoolean(CategoriaProducto.Cpr_Activo))                   
                        CategoriaProducto.Cpr_ActivoStr = "Activo";                    
                    else                    
                        CategoriaProducto.Cpr_ActivoStr = "Inactivo";                   
                    List.Add(CategoriaProducto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCategoriaProducto(CategoriaProducto CategoriaProducto, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Cpr",
                                        "@Cpr_Descripcion",
                                        "@Cpr_Activo"
                                      };
                object[] Valores = { 
                                        CategoriaProducto.Id_Emp,
                                        CategoriaProducto.Id_Cpr,
                                        CategoriaProducto.Cpr_Descripcion,
                                        CategoriaProducto.Cpr_Activo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProductoCategoria_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCategoriaProducto(CategoriaProducto CategoriaProducto, string Conexion, ref int verificador)
        {
            try
            {                
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Cpr",
                                        "@Id_Cpr_Ant",
                                        "@Cpr_Descripcion",
                                        "@Cpr_Activo"
                                      };
                object[] Valores = { 
                                        CategoriaProducto.Id_Emp,
                                        CategoriaProducto.Id_Cpr,
                                        CategoriaProducto.Id_Cpr_Ant,
                                        CategoriaProducto.Cpr_Descripcion,
                                        CategoriaProducto.Cpr_Activo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProductoCategoria_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}