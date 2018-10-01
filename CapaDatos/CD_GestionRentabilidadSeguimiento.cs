using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_GestionRentabilidadSeguimiento
    {

        string[] Parametros = {
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Cte"
                                        ,"@Id_Ter"
                                      };
        private string PermisoImprimir;


        public void ConsultaGestionRentabilidadSeguimiento_Buscar(GestionRentabilidadSeguimiento gestionRentabilidadseguimiento, string Conexion, ref List<GestionRentabilidadSeguimiento> List
            ,int Id_Emp
            ,int Id_Cd 
            ,int Id_Ter
            ,int Id_Cte
             )
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ter", "@Id_Cte" };


                object[] Valores = { Id_Emp, Id_Cd, Id_Ter, Id_Cte };

                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGestionRentabilidad_Seguimiento", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    GestionRentabilidadSeguimiento DgestionRentabilidadSeguimiento = new GestionRentabilidadSeguimiento();
                    DgestionRentabilidadSeguimiento.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    DgestionRentabilidadSeguimiento.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    DgestionRentabilidadSeguimiento.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    DgestionRentabilidadSeguimiento.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    DgestionRentabilidadSeguimiento.Cte_NomComercial = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    DgestionRentabilidadSeguimiento.venta = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Venta")));
                    DgestionRentabilidadSeguimiento.Costo = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Costo")));
                    DgestionRentabilidadSeguimiento.UtilidadBruta = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBruta")));
                    DgestionRentabilidadSeguimiento.ventaP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ventaP")));
                    DgestionRentabilidadSeguimiento.CostoP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("costoP")));
                    DgestionRentabilidadSeguimiento.UtilidadBrutaP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaP")));
                    List.Add(DgestionRentabilidadSeguimiento);


                }
               
                dr.Close();



                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
