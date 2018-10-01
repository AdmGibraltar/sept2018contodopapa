using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
   public class CD_RepVentas
    {
       public void Consulta(VenEstadisticaVentas ventas, string Conexion, ref List<VenEstadisticaVentas> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                         "@Id_Emp" ,
                                         "@Ano",
                                         "@Mes"
                                         
                                      };
                object[] Valores = { 
                                        ventas.Id_Emp,
                                        ventas.Anio,
                                        ventas.Mes
                                        
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepVentas", ref dr, Parametros, Valores);

                VenEstadisticaVentas venta= null;
                while (dr.Read())
                {
                    venta = new VenEstadisticaVentas();
                    venta.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    venta.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    venta.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    venta.Ene = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ene")));
                    venta.Feb = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Feb")));
                    venta.Mar = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Mar")));
                    venta.Abr = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Abr")));
                    venta.May = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("May")));
                    venta.Jun = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Jun")));
                    venta.Jul = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Jul")));
                    venta.Ago = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ago")));
                    venta.Sep = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Sep")));
                    venta.Oct = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Oct")));
                    venta.Nov = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Nov")));
                    venta.Dic = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Dic")));
                    venta.Total = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Total")));
                    List.Add(venta);
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
