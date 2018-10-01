using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;


namespace CapaDatos
{
    public class CD_InvRotacion
    {
        public void Consulta(InvRotacion rotacion, string Conexion, ref List<InvRotacion> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                         "@Id_Emp",
                                         "@ANIO",
                                         "@MES"

                                         
                                      };
                object[] Valores = { 
                                        rotacion.Id_Emp,
                                        rotacion.Ano,
                                        rotacion.Mes                                        
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepInvRotacion", ref dr, Parametros, Valores);

                InvRotacion InvRotacion = null;
                while (dr.Read())
                {
                    InvRotacion = new InvRotacion();
                    InvRotacion.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    InvRotacion.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    InvRotacion.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    InvRotacion.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    InvRotacion.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    InvRotacion.Prd_Presentacion = float.Parse(dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString());
                    InvRotacion.Id_Uni = dr.GetValue(dr.GetOrdinal("Id_Uni")).ToString();
                    InvRotacion.Prd_InvFinal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvFinal")));
                    InvRotacion.Prd_PrecioAAA = float.Parse(dr.GetValue(dr.GetOrdinal("Prd_PrecioAAA")).ToString());
                    InvRotacion.ImporteInventario = float.Parse(dr.GetValue(dr.GetOrdinal("ImporteInventario")).ToString());
                    InvRotacion.Antepenultimo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Antepenultimo")));
                    InvRotacion.Penultimo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Penultimo")));
                    InvRotacion.Ultimo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ultimo")));
                    InvRotacion.Promedio = float.Parse(dr.GetValue(dr.GetOrdinal("Promedio")).ToString());
                    InvRotacion.CostoPromedio = float.Parse(dr.GetValue(dr.GetOrdinal("CostoPromedio")).ToString());
                    InvRotacion.Rotacion = float.Parse(dr.GetValue(dr.GetOrdinal("Rotacion")).ToString());

                    InvRotacion.CostoPromedio = float.Parse(dr.GetValue(dr.GetOrdinal("CostoPromedio")).ToString());
                    InvRotacion.Rotacion = float.Parse(dr.GetValue(dr.GetOrdinal("Rotacion")).ToString());
                    InvRotacion.Vigente = float.Parse(dr.GetValue(dr.GetOrdinal("Vigente")).ToString());
                    InvRotacion.Vencido = float.Parse(dr.GetValue(dr.GetOrdinal("Vencido")).ToString()); 

                    List.Add(InvRotacion);
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
