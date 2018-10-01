using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatUnidad
    {
        public void ConsultaUnidad(int Id_Emp, string Conexion, ref List<Unidad> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp" };
                object[] Valores = {  Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUnidad_Consulta", ref dr, Parametros, Valores);

                Unidad unidad;
                while (dr.Read())
                {
                    unidad = new Unidad();
                    unidad.Id = dr.GetValue(dr.GetOrdinal("Id_Uni")).ToString();
                    unidad.Descripcion = dr.GetValue(dr.GetOrdinal("Uni_Descripcion")).ToString();
                    unidad.Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Uni_Tipo")));
                    unidad.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Uni_Activo")));
                    if (Convert.ToBoolean(unidad.Estatus))
                    {
                        unidad.EstatusStr = "Activo";
                    }
                    else
                    {
                        unidad.EstatusStr = "Inactivo";
                    }
                    List.Add(unidad);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarUnidad(Unidad unidad, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Uni",
                                        "@Id_Emp",
	                                    "@Uni_Descripcion", 
	                                    "@Uni_Tipo", 
	                                    "@Uni_Activo"
                                      };
                object[] Valores = { 
                                        unidad.Id,
                                        unidad.Id_Emp,
                                        unidad.Descripcion,
                                        unidad.Tipo,
                                        unidad.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUnidad_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarUnidad(Unidad unidad, string Conexion, ref int verificador)
        {
            try
            {
                 
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Uni",
                                       
                                        "@Id_Emp",
	                                    "@Uni_Descripcion", 
	                                    "@Uni_Tipo", 
	                                    "@Uni_Activo"
                                      };
                object[] Valores = { 
                                        unidad.Id,
                            
                                        unidad.Id_Emp,
                                        unidad.Descripcion,
                                        unidad.Tipo,
                                        unidad.Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUnidad_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
