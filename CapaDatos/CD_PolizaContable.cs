using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;



namespace CapaDatos
{
    public class CD_PolizaContable
    {
        #region Variables

        string[] Parametros = {
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Cte"
                                        ,"@Id_Ter"
                                      };
        private string PermisoImprimir;



        #endregion



        public void PolizaAmortizacionSistemasPropietarios(
                int Id_Cd,
                int Orden,
                int NivelRik,
                int NivelTer,
                int NivelCte,
                int Mes,
                int Anio,
                String Conexion,
                ref String NombreArchivo
            )
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@Orden", "@NivelRik", "@NivelTer", "@NivelCte", "@Mes", "@Anio" };
                object[] Valores = { Id_Cd, Orden, NivelRik, NivelTer, NivelCte, Mes, Anio };

                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_PolizaAmortizacionSistemasPropietarios", ref dr, Parametros, Valores);
                Decimal Total;
                Decimal TotalAmortizacion;
                Decimal TotalAmortizacionAnticipada;
                Decimal TotalAmortizacionTotal;
                if (Orden == 1) //Poliza Amortización
                {
                    string HTML = "<table border=\"1\"><tr><td colspan=\"2\" align=\"center\">POLIZA AMORTIZACION DE SISTEMAS PROPIETARIOS PERIODO " + Convert.ToString(Anio) + " - " + Convert.ToString(Mes) + "</td></tr>";
                    HTML = HTML + "<tr><td align=\"center\">Concepto</td><td align=\"center\">Importe</td></tr>";
                    Total = 0;
                    while (dr.Read())
                    {
                        Total = Total + Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Importe")));
                        HTML = HTML + "<tr><td align=\"left\">" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Concepto"))) + "</td><td align=\"right\">" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Importe")))) + "</td></tr>";
                    }
                    HTML = HTML + "<tr><td align=\"left\">Total:</td><td align=\"right\">" + String.Format("{0:0,0.00}", Total) + "</td></tr>";
                    HTML = HTML + "</table>";
                    dr.Close();

                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                    NombreArchivo = "C:/files/key/sianwebcambio/_Fuentes/_Fuentes/sianweb/PolizaAmortizacion.xls";
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(NombreArchivo);
                    sw.WriteLine(HTML);
                    sw.Close();
                    NombreArchivo = "http://localhost:55447/PolizaAmortizacion.xls";
                }


                if (Orden == 2) //Poliza Amortización RIK
                {
                    string HTML = "<table border=\"1\"><tr><td colspan=\"5\" align=\"center\">POLIZA AMORTIZACION DE SISTEMAS PROPIETARIOS PERIODO " + Convert.ToString(Anio) + " - " + Convert.ToString(Mes) + "<BR> POR RIK</td></tr>";
                    HTML = HTML + "<tr><td align=\"center\">#Rik</td><td align=\"center\">Nombre RIK</td><td align=\"center\">Importe Amortización</td><td align=\"center\">Importe Amortización Anticipada</td><td align=\"center\">Total Amortización</td></tr>";

                    TotalAmortizacion = 0;
                    TotalAmortizacionAnticipada = 0;
                    TotalAmortizacionTotal = 0; 


                    while (dr.Read())
                    {

                        TotalAmortizacion = TotalAmortizacion + Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Amortizacion")));
                        TotalAmortizacionAnticipada = TotalAmortizacionAnticipada + 0;
                        TotalAmortizacionTotal = TotalAmortizacionTotal + 0; 

                        HTML = HTML + "<tr>";
                        HTML = HTML + "<td align=\"right\">" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Rik"))) + "</td>";
                        HTML = HTML + "<td align=\"right\">" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Rik_Nombre"))) + "</td>";
                        HTML = HTML + "<td align=\"right\">" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Amortizacion")))) + "</td>";
                        HTML = HTML + "<td align=\"right\">" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Anticipada")))) + "</td>";
                        HTML = HTML + "<td align=\"right\">" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("TotalAmotizacion")))) + "</td>";
                        HTML = HTML + "</tr>";
                        //Total = Total + Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Importe")));

                    }
                    //HTML = HTML + "<tr><td align=\"left\">Total:</td><td align=\"right\">" + String.Format("{0:0,0.00}", Total) + "</td></tr>";
                    HTML = HTML + "</table>";
                    dr.Close();

                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                    NombreArchivo = "C:/files/key/sianwebcambio/_Fuentes/_Fuentes/sianweb/PolizaAmortizacion.xls";
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(NombreArchivo);
                    sw.WriteLine(HTML);
                    sw.Close();
                    NombreArchivo = "http://localhost:55447/PolizaAmortizacion.xls";
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
