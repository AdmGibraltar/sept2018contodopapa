using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_CatAdenda
    {
        public void ConsultaAdenda(Adenda tipoCosto, string Conexion, ref List<Adenda> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { tipoCosto.Id_Emp, tipoCosto.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAdenda_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    tipoCosto = new Adenda();
                    tipoCosto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    tipoCosto.Id_Ade = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ade")));
                    tipoCosto.Tco_Descripcion = dr.GetValue(dr.GetOrdinal("Ade_Descripcion")).ToString();
                    tipoCosto.Tco_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ade_Activo")));
                   
                    if (tipoCosto.Tco_Activo)
                    {
                        tipoCosto.Tco_ActivoStr = "Activo";
                    }
                    else
                    {
                        tipoCosto.Tco_ActivoStr = "Inactivo";
                    }

                    List.Add(tipoCosto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarAdenda(Adenda adenda, string Conexion, DataTable dt, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Id_Ade", 
	                                    "@Ade_Descripcion", 
	                                    "@Ade_Activo",
                                      };
                object[] Valores = { 
                                        adenda.Id_Emp,
                                        adenda.Id_Cd
                                        ,adenda.Id_Ade
                                        ,adenda.Tco_Descripcion
                                        ,adenda.Tco_Activo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAdenda_Insertar", ref verificador, Parametros, Valores);

                if (verificador > 0)
                {
                    Parametros = new string[] { 
                    		"@Contador",
                            "@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_Ade", 
		                    "@Ade_Tipo",  
		                    "@Ade_Campo",  
		                    "@Ade_Longitud",  
		                    "@Ade_Nodo",
                            "@Ade_Requerido"
                    };

                    
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        verificador = 0;
                        Valores = new object[] {  
                            x,
                            adenda.Id_Emp,
                            adenda.Id_Cd,
                            adenda.Id_Ade,
		                    dt.Rows[x]["Tipo"].ToString(),
                            dt.Rows[x]["Campo"].ToString(),
                            dt.Rows[x]["Longitud"].ToString(),
                            dt.Rows[x]["Nodo"].ToString(),
                            dt.Rows[x]["Requerido"].ToString()
                        };

                        sqlcmd = CapaDatos.GenerarSqlCommand("spCatAdendaDet_Insertar", ref verificador, Parametros, Valores);
                    }
                }

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarAdenda(Adenda adenda, string Conexion, DataTable dt, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Id_Ade", 
	                                    "@Ade_Descripcion", 
	                                    "@Ade_Activo"
                                      };
                object[] Valores = { 
                                        adenda.Id_Emp,
                                        adenda.Id_Cd
                                        ,adenda.Id_Ade
                                        ,adenda.Tco_Descripcion
                                        ,adenda.Tco_Activo
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAdenda_Modificar", ref verificador, Parametros, Valores);

                if (verificador > 0)
                {
                    Parametros = new string[] { 
                    		"@Contador",
                            "@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_Ade",  
		                    "@Ade_Tipo",  
		                    "@Ade_Campo",  
		                    "@Ade_Longitud",  
		                    "@Ade_Nodo",
                            "@Ade_Requerido"
                    };

                    
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        verificador = 0;
                        Valores = new object[] {  
                            x,
                            adenda.Id_Emp,
                            adenda.Id_Cd,
                            adenda.Id_Ade,
		                    dt.Rows[x]["Tipo"].ToString(),
                            dt.Rows[x]["Campo"].ToString(),
                            dt.Rows[x]["Longitud"].ToString(),
                            dt.Rows[x]["Nodo"].ToString(),
                            dt.Rows[x]["Requerido"].ToString()
                        };

                        sqlcmd = CapaDatos.GenerarSqlCommand("spCatAdendaDet_Insertar", ref verificador, Parametros, Valores);
                    }
                }

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultaAdenda(Adenda adenda, DataTable dt, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ade" };
                object[] Valores = { adenda.Id_Emp, adenda.Id_Cd, adenda.Id_Ade };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAdendaDet_Consultar", ref dr, Parametros, Valores);

                dt.Rows.Clear();
                while (dr.Read())
                {
                    dt.Rows.Add(new object[] { 
                        dr.GetValue(dr.GetOrdinal("Id_AdeDet")), 
                        dr.GetValue(dr.GetOrdinal("Ade_Tipo")), 
                        dr.GetValue(dr.GetOrdinal("Ade_Nodo")), 
                        dr.GetValue(dr.GetOrdinal("Ade_Campo")), 
                        dr.GetValue(dr.GetOrdinal("Ade_Longitud")),
                        dr.GetValue(dr.GetOrdinal("Ade_Requerido"))
                    });
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
