using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_PreciosEspeciales
    {
        public void Consulta(PrecioEspecial precioEspecial, string Conexion, ref List<PrecioEspecial> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                         "@Id_Emp",
                                         "@ANO",
                                         "@MES"

                                         
                                      };
                object[] Valores = { 
                                        precioEspecial.Id_Emp,
                                        precioEspecial.Ano,
                                        precioEspecial.Mes                                        
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpRepPreciosEspeciales", ref dr, Parametros, Valores);

                PrecioEspecial precio = null;
                while (dr.Read())
                {
                    precio = new PrecioEspecial();
                    precio.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    precio.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    /*
                    precio.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    precio.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    precio. = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                   precio. = float.Parse(dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString());
                    InvRotacion.Id_Uni = dr.GetValue(dr.GetOrdinal("Id_Uni")).ToString();
                    InvRotacion.Prd_InvFinal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvFinal")));
                    InvRotacion.Prd_PrecioAAA = float.Parse(dr.GetValue(dr.GetOrdinal("Prd_PrecioAAA")).ToString());
                    InvRotacion.ImporteInventario = float.Parse(dr.GetValue(dr.GetOrdinal("ImporteInventario")).ToString());
                    InvRotacion.Antepenultimo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Antepenultimo")));
                    InvRotacion.Penultimo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Penultimo")));
                    InvRotacion.Ultimo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ultimo")));
                    InvRotacion.Promedio = float.Parse(dr.GetValue(dr.GetOrdinal("Promedio")).ToString());
                    InvRotacion.CostoPromedio = float.Parse(dr.GetValue(dr.GetOrdinal("CostoPromedio")).ToString());
                    InvRotacion.Rotacion = float.Parse(dr.GetValue(dr.GetOrdinal("Rotacion")).ToString());*/

                    List.Add(precio);
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
