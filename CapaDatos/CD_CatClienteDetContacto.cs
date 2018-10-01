using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatClienteDetContacto
    {

        public void InsertarUpdate(CapaEntidad.ClienteDetContacto Contacto, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
                                        "@Id_Cte", 
                                        "@Id_CteDet", 
                                        "@Id_Ter", 
                                        "@Id_Seg", 
                                        "@Nombre", 
	                                    "@Puesto", 
	                                    "@Cumpleanios", 
	                                    "@Correo", 
	                                    "@Direccion1", 
	                                    "@Direccion2", 
	                                    "@TelNegocio",
                                        "@TelCasa",
                                        "@Id_Consecutivo"
                                      };
                object[] Valores = { 
                                        Contacto.Id_Emp,
                                        Contacto.Id_Cd,
                                        Contacto.Id_Cte,
                                        Contacto.Id_CteDet,
                                        Contacto.Id_Ter,
                                        Contacto.Id_Seg,
                                        Contacto.Nombre,
                                        Contacto.Puesto,
                                        Contacto.Cumpleanios,
                                        Contacto.Correo,
                                        Contacto.Direccion1,
                                        Contacto.Direccion2,
                                        Contacto.TelNegocio,
                                        Contacto.TelCasa,
                                        Contacto.Id_Consecutivo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteDetContacto_InsertarUpdate", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consulta(CapaEntidad.ClienteDetContacto Contacto, string Conexion, ref List<CapaEntidad.ClienteDetContacto> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Emp", "@Id_Cd","@Id_Cte","@Id_Ter" };
                object[] Valores = { Contacto.Id_Emp, Contacto.Id_Cd, Contacto.Id_Cte, Contacto.Id_Ter };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteDetContacto_Sel", ref dr, Parametros, Valores);
                
                while (dr.Read())
                {
                    Contacto = new CapaEntidad.ClienteDetContacto();
                    Contacto.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    Contacto.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    Contacto.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    Contacto.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    Contacto.Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Nombre")));
                    Contacto.TelNegocio = Convert.ToString(dr.GetValue(dr.GetOrdinal("TelNegocio")));
                    Contacto.TelCasa = Convert.ToString(dr.GetValue(dr.GetOrdinal("TelCasa")));
                    Contacto.Id_Consecutivo = (int)dr.GetValue(dr.GetOrdinal("Id_Consecutivo"));
                    List.Add(Contacto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaPorId(CapaEntidad.ClienteDetContacto Contacto, string Conexion, ref CapaEntidad.ClienteDetContacto List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Ter","@Id_Consecutivo"};
                object[] Valores = { Contacto.Id_Emp, 
                                       Contacto.Id_Cd, 
                                       Contacto.Id_Cte, 
                                       Contacto.Id_Ter, 
                                       Contacto.Id_Consecutivo
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteDetContacto_SelById", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Contacto = new CapaEntidad.ClienteDetContacto();
                    Contacto.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    Contacto.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    Contacto.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    Contacto.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    Contacto.Id_Seg = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    Contacto.Id_Consecutivo = (int)dr.GetValue(dr.GetOrdinal("Id_Consecutivo"));
                    Contacto.Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Nombre")));
                    Contacto.Puesto = Convert.ToString(dr.GetValue(dr.GetOrdinal("Puesto")));
                    Contacto.Cumpleanios = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cumpleanios")));
                    Contacto.Correo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Correo")));
                    Contacto.Direccion1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Direccion1")));
                    Contacto.Direccion2 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Direccion2")));
                    Contacto.TelNegocio = Convert.ToString(dr.GetValue(dr.GetOrdinal("TelNegocio")));
                    Contacto.TelCasa = Convert.ToString(dr.GetValue(dr.GetOrdinal("TelCasa")));                    
                    List = Contacto;
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
