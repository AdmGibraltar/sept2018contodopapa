using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatBanco
    {
        public void ConsultaBanco(Banco banco, string Conexion, ref List<Banco> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { banco.Empresa, banco.Centro };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatBanco_Consulta", ref dr, Parametros, Valores);

                 
                while (dr.Read())
                {
                    banco = new Banco();
                    banco.Empresa = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    banco.Centro = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    banco.Id = (int)dr.GetValue(dr.GetOrdinal("Id_Ban"));
                    banco.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Ban_Nombre"));
                    banco.Estado = dr.GetValue(dr.GetOrdinal("Ban_Estado")).ToString();
                    banco.Ciudad = (string)dr.GetValue(dr.GetOrdinal("Ban_Ciudad"));
                    banco.Cuenta = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ban_Cuenta")));
                    banco.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ban_Activo")));
                    if (Convert.ToBoolean(banco.Estatus))
                    {
                        banco.EstatusStr = "Activo";
                    }
                    else
                    {
                        banco.EstatusStr = "Inactivo";
                    }
                    List.Add(banco);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarBanco(Banco banco, string Conexion, ref int verificador)
        {
            try
            {
                
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ban", 
	                                    "@Ban_Nombre", 
	                                    "@Ban_Ciudad", 
	                                    "@Ban_Estado", 
	                                    "@Ban_Cuenta", 
	                                    "@Ban_Activo"
                                      };
                object[] Valores = { 
                                        banco.Empresa,
                                        banco.Centro,
                                        banco.Id,
                                        banco.Descripcion,
                                        banco.Ciudad,
                                        banco.Estado,
                                        banco.Cuenta,
                                        banco.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatBanco_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarBanco(Banco banco, string Conexion, ref int verificador)
        {
            try
            {
                
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ban",
                                        "@Id_Ban_Ant",
	                                    "@Ban_Nombre", 
	                                    "@Ban_Ciudad", 
	                                    "@Ban_Estado", 
	                                    "@Ban_Cuenta", 
	                                    "@Ban_Activo"
                                      };
                object[] Valores = { 
                                        banco.Empresa,
                                        banco.Centro,
                                        banco.Id,
                                        banco.Id_Ant,
                                        banco.Descripcion,
                                        banco.Ciudad, 
                                        banco.Estado,
                                        banco.Cuenta,
                                        banco.Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatBanco_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
