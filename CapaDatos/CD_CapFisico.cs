using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CapFisico
    {
        public void EliminarFisico(Fisico Fisico, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { Fisico.Id_Emp, Fisico.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFisico_Eliminar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarFisico(Fisico fisico, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                //()

                string[] Parametros = { 
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Fis",
                                        "@Id_Prd",
                                        "@Fis_Fecha",
                                        "@Fis_Fisico",
                                        
                                      };
                object[] Valores = { 
                                        fisico.Id_Emp
                                        ,fisico.Id_Cd
                                        ,fisico.Id_Fis
                                        ,fisico.Id_Prd == -1 ? (object)null : fisico.Id_Prd
                                        ,fisico.Fis_Fecha
                                        ,fisico.Fis_Fisico
                                         
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFisico_Insertar", ref verificador, Parametros, Valores);
                //Fisico.Id_Fis = verificador; //identity del fisico

                string[] ParametrosDet = { 
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Fis",
                                        "@Id_FisCons",
                                        "@Id_Cte",
                                        "@Id_Ter",
                                        "@Fis_Consignados"
                                      };

                foreach (FisicoConsignado FisicoConsignado in fisico.ListFisicoConsignado)
                {
                    object[] ValoresDet = { 
                                        FisicoConsignado.Id_Emp,
                                        FisicoConsignado.Id_Cd,
                                        fisico.Id_Fis, //FisicoConsignado.Id_Fis,
                                        FisicoConsignado.Id_FisCons, //Id de orden de tabla (consecutivo de fisico consignado)
                                        FisicoConsignado.Id_Cte,
                                        FisicoConsignado.Id_Ter,
                                        FisicoConsignado.Fis_Consignados
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapFisicoConsignado_Insertar", ref verificador, ParametrosDet, ValoresDet);
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

        public void ConsultaFisicoConsignado(FisicoConsignado FisicoConsignado, int Id_Prd, string Conexion, ref List<FisicoConsignado> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Prd"                                        
                                      };
                object[] Valores = { 
                                        FisicoConsignado.Id_Emp,
                                        FisicoConsignado.Id_Cd,
                                        Id_Prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFisicoConsignado_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    FisicoConsignado = new FisicoConsignado();
                    FisicoConsignado.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    FisicoConsignado.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    FisicoConsignado.Id_Fis = (int)dr.GetValue(dr.GetOrdinal("Id_Fis"));
                    FisicoConsignado.Id_FisCons = (int)dr.GetValue(dr.GetOrdinal("Id_FisCons"));
                    FisicoConsignado.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    FisicoConsignado.Cte_Nombre = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    FisicoConsignado.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    FisicoConsignado.Ter_Nombre = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    FisicoConsignado.Fis_Consignados = (int)dr.GetValue(dr.GetOrdinal("Fis_Consignados"));
                    List.Add(FisicoConsignado);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFisico(Producto fisico, string Conexion, ref List<Producto> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd"                                         
                                      };
                object[] Valores = { 
                                        fisico.Id_Emp,
                                        fisico.Id_Cd 
                                        
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFisico_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    fisico = new Producto();
                    fisico.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    fisico.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    fisico.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    fisico.Prd_UniNs = dr.GetValue(dr.GetOrdinal("Prd_UniNs")).ToString();
                    fisico.Prd_InvFinal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvFinal")));
                    fisico.Prd_Fisico = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Fisico")));
                    List.Add(fisico);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Automatico(int Id_Emp, int Id_Cd, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd"                                         
                                      };
                object[] Valores = { 
                                         Id_Emp,
                                         Id_Cd 
                                        
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFisico_Automatico", ref dr, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
