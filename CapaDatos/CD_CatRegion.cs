using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatRegion
    {
        public void ConsultaRegion(ref Region region, int id_region, string reg_descripcion, CapaEntidad.Sesion sesion, ref List<Region> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Reg",
                                          "@Reg_Descripcion"
                                      };
                string[] Valores = {
                                       sesion.Id_Emp.ToString(),                                       
                                       id_region.ToString(),
                                       reg_descripcion
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRegion_Consulta", ref dr, parametros, Valores);
                                
                while (dr.Read())
                {
                    region = new Region();
                    region.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    region.Id_Reg = dr.GetInt32(dr.GetOrdinal("Id_Reg"));
                    region.Reg_Descripcion = dr.GetString(dr.GetOrdinal("Reg_Descripcion"));
                    region.Reg_Activo = dr.GetBoolean(dr.GetOrdinal("Reg_Activo"));
                    list.Add(region);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarRegion(ref Region region_nueva, ref Region region_antigua, CapaEntidad.Sesion sesion, ref int verificador, bool actualizar)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = {
			        "@Id_EmpN",
	                "@Id_RegN",
	                "@Reg_DescripcionN",
	                "@Reg_ActivoN",
                    "@Id_EmpV",
	                "@Id_RegV",
	                "@Reg_DescripcionV",
	                "@Reg_ActivoV"
		        };

                object[] Valores = {
                    region_nueva.Id_Emp,
                    region_nueva.Id_Reg,
                    region_nueva.Reg_Descripcion,
                    region_nueva.Reg_Activo,
                    region_antigua.Id_Emp,
                    region_antigua.Id_Reg,
                    region_antigua.Reg_Descripcion,
                    region_antigua.Reg_Activo
		        };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(actualizar ? "spCatRegion_Modificar" : "spCatRegion_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaRegionConsecutivo(ref Region region, CapaEntidad.Sesion sesion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] parametros = { 
                                          "@Id_Emp"                                          
                                      };
                string[] Valores = {
                                       sesion.Id_Emp.ToString()
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRegion_Consulta_Consecutivo", ref dr, parametros, Valores);

                while (dr.Read())
                {
                    region = new Region();                    
                    region.Id_Reg = dr.GetInt32(dr.GetOrdinal("consecutivo"));
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
