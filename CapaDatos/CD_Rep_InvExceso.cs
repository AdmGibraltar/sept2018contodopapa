using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Rep_InvExceso
    {
        public void Consulta(RepExcesos exceso, string Conexion, ref List<RepExcesos> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                         "@Id_Emp",  
                                         "@Id_Cd",  
                                         "@Id_Cd_Filtro",  
                                         "@Id_Pvd",  
                                         "@Dias",  
                                         "@Id_Ptp",  
                                         "@Salida",  
                                         "@Id_U",  
                                         "@Rota",
                                         "@Dias_Ver"
                                      };
                object[] Valores = { 
                                        exceso.Id_Emp,
                                        exceso.Id_Cd,                                            
                                        exceso.Centro == -1? (int?)null: exceso.Centro,                                        
                                        exceso.Proveedor == -1? (int?)null: exceso.Proveedor,
                                        exceso.Dias,
                                        exceso.Tproducto == -1? (int?)null: exceso.Tproducto,
                                        exceso.Salida,
                                        exceso.Id_U,
                                        exceso.Indicador == -1? (int?)null: exceso.Indicador,
                                        exceso.DiasVer==-1?(int?)null: exceso.DiasVer,
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_InvExceso", ref dr, Parametros, Valores);

                RepExcesos repExcesos = new RepExcesos();
                while (dr.Read())
                {
                    repExcesos = new RepExcesos();
                    repExcesos.Id_Pvd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pvd")));
                    repExcesos.Pvd_Nombre = dr.GetValue(dr.GetOrdinal("Pvd_Nombre")).ToString();
                    repExcesos.Costo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Costo")));
                    repExcesos.Exceso = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Exceso")));
                    repExcesos.Disponible = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Disponible")));
                    repExcesos.Url = "return myJS(exceso.Proveedor, exceso.Centro,exceso.DiasVer,exceso.Tproducto,exceso.Indicador ,exceso.Dias, repExcesos.Id_Pvd);";
                    //repExcesos.Url = "Rep_InvExceso3.aspx?Proveedor=" + exceso.Proveedor + "&Centro=" + exceso.Centro + "&DiasVer=" + exceso.DiasVer + "&Tproducto=" + exceso.Tproducto + "&Indicador=" + exceso.Indicador + "&Dias=" + exceso.Dias + "&ProveedorVer=" + Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pvd")));
                    List.Add(repExcesos);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consulta3(RepExcesos exceso, string Conexion, ref List<RepExcesos> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                         "@Id_Emp",  
                                         "@Id_Cd",  
                                         "@Id_Cd_Filtro",  
                                         "@Id_Pvd",  
                                         "@Dias",  
                                         "@Id_Ptp",  
                                         "@Salida",  
                                         "@Id_U",  
                                         "@Rota",
                                         "@Dias_Ver",
                                         "@Id_Pvd_Ver"
                                      };
                object[] Valores = { 
                                        exceso.Id_Emp,
                                        exceso.Id_Cd,                                            
                                        exceso.Centro == -1? (int?)null: exceso.Centro,                                        
                                        exceso.Proveedor == -1? (int?)null: exceso.Proveedor,
                                        exceso.Dias,
                                        exceso.Tproducto == -1? (int?)null: exceso.Tproducto,
                                        exceso.Salida,
                                        exceso.Id_U,
                                        exceso.Indicador == -1? (int?)null: exceso.Indicador  ,
                                        exceso.DiasVer== -1? (int?)null: exceso.DiasVer  ,
                                        exceso.ProveedorVer
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_InvExceso", ref dr, Parametros, Valores);

                RepExcesos repExcesos = new RepExcesos();
                while (dr.Read())
                {
                    repExcesos = new RepExcesos();
                    repExcesos.Id_Pvd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pvd")));
                    repExcesos.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    repExcesos.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    repExcesos.Costo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Costo")));
                    repExcesos.Exceso = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Exceso")));
                    repExcesos.Disponible = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Disponible")));
                    List.Add(repExcesos);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaGrafica(RepExcesos exceso, ref DataTable valores, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                         "@Id_Emp",  
                                         "@Id_Cd",  
                                         "@Id_Cd_Filtro",  
                                         "@Id_Pvd",  
                                         "@Dias",  
                                         "@Id_Ptp",  
                                         "@Salida",  
                                         "@Id_U",  
                                         "@Rota"          
                                      };
                object[] Valores = { 
                                        exceso.Id_Emp,
                                        exceso.Id_Cd,                                            
                                        exceso.Centro == -1? (int?)null: exceso.Centro,                                        
                                        exceso.Proveedor == -1? (int?)null: exceso.Proveedor,
                                        exceso.Dias,
                                        exceso.Tproducto == -1? (int?)null: exceso.Tproducto,
                                        exceso.Salida,
                                        exceso.Id_U,
                                        exceso.Indicador == -1? (int?)null: exceso.Indicador     
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_InvExceso", ref dr, Parametros, Valores);

                RepExcesos repExcesos = new RepExcesos();
                Comun cm;
                while (dr.Read())
                {
                    valores.Rows.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Dias"))), Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Costo"))));
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

