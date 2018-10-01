using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Rep_InvValorizacionInventario
    {
        public void ConsultaValorizacion(ValorizacionInventario valorizacion, string Conexion, ref List<Producto> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd", 
                                        "@Id_PrdStr",
                                        "@Id_Ptp",
                                        "@Id_Spo",
                                        "@TipoPrecioStr",
                                        "@Orden"
                                      };
                object[] Valores = { 
                                        valorizacion.Id_Emp,
                                        valorizacion.Id_Cd,    
                                        valorizacion.Id_PrdStr,
                                        string.IsNullOrEmpty(valorizacion.Id_Ptp) ? (int?)null : Convert.ToInt32(valorizacion.Id_Ptp),
                                        string.IsNullOrEmpty(valorizacion.Id_Spo) ? (int?)null : Convert.ToInt32(valorizacion.Id_Spo),                                       
                                        valorizacion.TipoPrecioStr,
                                        valorizacion.Orden
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepInvValorizacionInventario_Consulta", ref dr, Parametros, Valores);

                Producto producto = new Producto();
                while (dr.Read())
                {
                    producto = new Producto();
                    producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    producto.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    producto.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    producto.Prd_UniNs = dr.GetValue(dr.GetOrdinal("Prd_UniNs")).ToString();
                    producto.Prd_InvFinal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvFinal")));
                    producto.Prd_Fisico = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Fisico")));
                    List.Add(producto);
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
