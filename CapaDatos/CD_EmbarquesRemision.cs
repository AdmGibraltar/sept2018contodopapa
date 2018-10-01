using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_EmbarquesRemision
    {
        public void ConsultaProEmbarquesRemision(int Id_Emp, int Id_Cd, string Conexion, EmbarquesRemision embarquefiltro, ref List<EmbarquesRemision> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Filtro_Nombre",
                                          "@Filtro_CteIni", 
                                          "@Filtro_CteFin",                                                                               
                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       embarquefiltro.Filtro_Nombre  == "" ? (object)null : embarquefiltro.Filtro_Nombre,
                                       embarquefiltro.Filtro_Id_Cte  == "" ? (object)null : embarquefiltro.Filtro_Id_Cte,
                                       embarquefiltro.Filtro_Id_Cte2  == "" ? (object)null : embarquefiltro.Filtro_Id_Cte2,
                                       embarquefiltro.Filtro_FecIni  == "" ? (object)null : embarquefiltro.Filtro_FecIni,
                                       embarquefiltro.Filtro_FecFin  == "" ? (object)null : embarquefiltro.Filtro_FecFin 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProRemisionesEmbarques_Consulta", ref dr, Parametros, Valores);

                EmbarquesRemision embarquesRemision;
                while (dr.Read())
                {
                    embarquesRemision = new EmbarquesRemision();
                    embarquesRemision.Id_Rem = (int)dr.GetValue(dr.GetOrdinal("Id_Rem"));
                    embarquesRemision.Tipo = (string)dr.GetValue(dr.GetOrdinal("Rem_Tipo"));
                    embarquesRemision.Estatus = (string)dr.GetValue(dr.GetOrdinal("Rem_Estatus"));
                    embarquesRemision.Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Rem_Fecha"));
                    embarquesRemision.Numero = (int)dr.GetValue(dr.GetOrdinal("Id_Rem"));
                    embarquesRemision.Pedido = (int)dr.GetValue(dr.GetOrdinal("Id_Ped"));
                    embarquesRemision.Cliente = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    embarquesRemision.Num_Cliente = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    embarquesRemision.Fecha2 = !string.IsNullOrEmpty(dr.GetValue(dr.GetOrdinal("Rem_FechaEntrega")).ToString()) ? (DateTime)dr.GetValue(dr.GetOrdinal("Rem_FechaEntrega")) : DateTime.Now;
                    List.Add(embarquesRemision);                  
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarProEmbarquesRemision(int Id_Emp, int Id_Cd, int Id_U, EmbarquesRemision embarquesRemision, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                         "@Id_Emp",        
                                         "@Id_Cd",   
                                         "@Id_U",
                                         "@Id_Rem",
                                         "@Id_Ped"
                                      };
                object[] Valores = {                                       
                                       Id_Emp,
                                       Id_Cd,       
                                       Id_U,
                                       embarquesRemision.Id_Rem,
                                       embarquesRemision.Pedido                                                                        
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProRemisionesEmbarques_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
