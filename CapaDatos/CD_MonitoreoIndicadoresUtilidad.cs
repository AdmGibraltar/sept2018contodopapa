using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;



namespace CapaDatos
{
    public class CD_MonitoreoIndicadoresUtilidad
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



        public void MonitoreoGestionRentabildiadRIK_Buscar(MonitoreoGestionRentabilidadRIK monitoreogestionrentabilidadRIK, string Conexion, ref List<MonitoreoGestionRentabilidadRIK> List
            , int Id_Emp
            , int Id_Cd
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , int Id_Rik
            , ref string Grafica
            )
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@mesInicial", "@anioInicial", "@mesFinal", "@anioFinal", "@Id_U", "@Id_Rik" };


                object[] Valores = { Id_Emp, Id_Cd, MesInicial, AnioInicial, MesFinal, AnioFinal, Id_U ,Id_Rik};


                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepVenUtilidadBrutaMonitoreoTiempoRIK", ref dr, Parametros, Valores);


                string Categorias = "";
                // string SerieVentas = "";
                string SerieUtilidadBrutaImporte = "";
                string SerieMetaUtilidadBrutaImporte = "";
                string SerieUtilidadBrutaProyectadaImporte = "";
                decimal PuntoMedio = 0;
                string Rik_Nombre = "";

                while (dr.Read())
                {

                    MonitoreoGestionRentabilidadRIK DmonitoreogestionrentabilidadRIK = new MonitoreoGestionRentabilidadRIK();
                    DmonitoreogestionrentabilidadRIK.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    DmonitoreogestionrentabilidadRIK.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CD")));
                    DmonitoreogestionrentabilidadRIK.Periodo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Periodo")));
                    DmonitoreogestionrentabilidadRIK.Rik_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Rik_Nombre")));
                    DmonitoreogestionrentabilidadRIK.Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Anio")));
                    DmonitoreogestionrentabilidadRIK.Mes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Mes")));
                    DmonitoreogestionrentabilidadRIK.VentaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaImporte")));
                    DmonitoreogestionrentabilidadRIK.UtilidadBrutaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte")));
                    DmonitoreogestionrentabilidadRIK.UtilidadBrutaPorc = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaPorc")));
                    DmonitoreogestionrentabilidadRIK.MetaUtilidadBrutaPorc = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaPorc")));
                    DmonitoreogestionrentabilidadRIK.MetaUtilidadBrutaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte")));
                    DmonitoreogestionrentabilidadRIK.UtilidadBrutaProyectadaPorc = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaPorc")));
                    DmonitoreogestionrentabilidadRIK.UtilidadBrutaProyectadaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaImporte")));

                    Categorias = Categorias + "<category label='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Periodo"))) + "' />";
                    //   SerieVentas = SerieVentas + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("VentaImporte"))) + "' />";
                    SerieUtilidadBrutaImporte = SerieUtilidadBrutaImporte + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte"))) + "' link='RepMonitoreoIndicadores.aspx?Id_Rik=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Rik"))) + "%26TxtAnioInicial=" + Convert.ToString(AnioInicial) + "%26TxtAnioFinal=" + Convert.ToString(AnioFinal) + "%26txtMesInicial=" + Convert.ToString(MesInicial) + "%26txtMesFinal=" + Convert.ToString(MesFinal) + "'/>";
                    SerieMetaUtilidadBrutaImporte = SerieMetaUtilidadBrutaImporte + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte"))) + "' link='RepMonitoreoIndicadores.aspx?Id_Rik=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Rik"))) + "%26TxtAnioInicial=" + Convert.ToString(AnioInicial) + "%26TxtAnioFinal=" + Convert.ToString(AnioFinal) + "%26txtMesInicial=" + Convert.ToString(MesInicial) + "%26txtMesFinal=" + Convert.ToString(MesFinal) + "' />";
                    SerieUtilidadBrutaProyectadaImporte = SerieUtilidadBrutaProyectadaImporte + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaImporte"))) + "'  link='RepMonitoreoIndicadores.aspx?Id_Rik=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Rik"))) + "%26TxtAnioInicial=" + Convert.ToString(AnioInicial) + "%26TxtAnioFinal=" + Convert.ToString(AnioFinal) + "%26txtMesInicial=" + Convert.ToString(MesInicial) + "%26txtMesFinal=" + Convert.ToString(MesFinal) + "'/>";


                    Rik_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Rik_Nombre")));

                    if (PuntoMedio < Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte"))))
                    {
                        PuntoMedio = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte")));
                    }


                    List.Add(DmonitoreogestionrentabilidadRIK);


                }


                Grafica = "<div id='myNextDiv' ><!-- START Code Block for Chart myNext --><embed src=\"FusionCharts/msLine.swf\" FlashVars=\"&chartWidth=800&chartHeight=800&debugMode=0&registerWithJS=0&DOMId=myNext&dataXML=<chart caption='RIK " + Rik_Nombre +  "' xAxisName='Periodos' yAxisName='Utilidad Bruta' showValues='0' numberPrefix='$' labelDisplay='ROTATE'><categories>" + Categorias + "</categories><dataset seriesName='Utilidad Bruta'>" + SerieUtilidadBrutaImporte + "</dataset><dataset seriesName='Meta Utilidad Bruta'>" + SerieMetaUtilidadBrutaImporte + "</dataset><dataset seriesName='Proyectada Utilidad Bruta'>" + SerieUtilidadBrutaProyectadaImporte + "</dataset><trendlines><line startValue='" + Convert.ToString(PuntoMedio) + "' color='91C728' displayValue='Maxima' showOnTop='1'/></trendlines></chart>&scaleMode=noScale&lang=EN\" quality=\"high\" width=\"600\" height=\"600\" name=\"myNext\" id=\"myNext\" allowScriptAccess=\"always\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"   /> <!-- END Code Block for Chart myNext --> </div>";
                dr.Close();



                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Reporte_Monitore_Representante(string Conexion, int Id_Emp
            , int Id_Cd
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , ref String NombreArchivo
            )
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@mesInicial", "@anioInicial", "@mesFinal", "@anioFinal", "@Id_U" };


                object[] Valores = { Id_Emp, Id_Cd, MesInicial, AnioInicial, MesFinal, AnioFinal, Id_U };


                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepVenUtilidadBrutaMonitoreoTiempo", ref dr, Parametros, Valores);


                NombreArchivo = "F:/APLICACIONES_IIS/sianwebmty/RIK.xls";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(NombreArchivo);
                NombreArchivo = "http://189.206.126.67/sianwebmty/RIK.xls";


                String Linea;

                Linea = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>";
                sw.WriteLine(Linea);

                Linea = "<table>";
                sw.WriteLine(Linea);
                Linea = "<tr>";
                sw.WriteLine(Linea);
                Linea = "<td align=\"center\">";
                sw.WriteLine(Linea);
                Linea = "REPORTE MONITOREO GESTION DE RENTABILIDAD POR REPRESENTANTE";
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);
                Linea = "</tr>";
                sw.WriteLine(Linea);


                Linea = "<tr>";
                sw.WriteLine(Linea);
                Linea = "<td>";
                sw.WriteLine(Linea);

                Linea = "<table border=\"1\">";
                sw.WriteLine(Linea);


                Linea = "<tr>";
                sw.WriteLine(Linea);

                Linea = "<td>#Representante</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Representante</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Venta $</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Utilidad Bruta $</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Meta Utilidad Bruta $</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Utilidad Bruta Proyectada $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Diferencial $</td>";
                sw.WriteLine(Linea);                
                
                Linea = "</tr>";
                sw.WriteLine(Linea); sw.WriteLine(Linea);

                double VentaImporte;
                double UtilidadBrutaImporte;
                double MetaUtilidadBrutaImporte;
                double UtilidadBrutaProyectadaImporte;
                double Diferencial;


                VentaImporte=0;
                UtilidadBrutaImporte = 0;
                MetaUtilidadBrutaImporte = 0;
                UtilidadBrutaProyectadaImporte = 0;
                Diferencial = 0;


                while (dr.Read())
                {

                    Linea = "<tr>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Rik")))  + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Rik_Nombre")))  + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaImporte")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaImporte")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte"))) - Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte")))) + "</td>";
                    sw.WriteLine(Linea);



                    Linea = "</tr>";
                    sw.WriteLine(Linea); sw.WriteLine(Linea);


                    VentaImporte = VentaImporte + Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VentaImporte")));
                    UtilidadBrutaImporte = UtilidadBrutaImporte + Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte")));
                    MetaUtilidadBrutaImporte = MetaUtilidadBrutaImporte + Convert.ToDouble(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte")));
                    UtilidadBrutaProyectadaImporte = UtilidadBrutaProyectadaImporte + Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaImporte"))) ;
                    Diferencial = Diferencial + (Convert.ToDouble(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte"))) - Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte"))));


                    //Dmonitoreogestionrentabilidad.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    //Dmonitoreogestionrentabilidad.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CD")));
                    //Dmonitoreogestionrentabilidad.Rik_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Rik_Nombre")));
                    //Dmonitoreogestionrentabilidad.VentaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaImporte")));
                    //Dmonitoreogestionrentabilidad.UtilidadBrutaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte")));
                    //Dmonitoreogestionrentabilidad.UtilidadBrutaPorc = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaPorc")));
                    //Dmonitoreogestionrentabilidad.MetaUtilidadBrutaPorc = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaPorc")));
                    // Dmonitoreogestionrentabilidad.MetaUtilidadBrutaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte")));
                    //Dmonitoreogestionrentabilidad.UtilidadBrutaProyectadaPorc = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaPorc")));
                    //Dmonitoreogestionrentabilidad.UtilidadBrutaProyectadaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaImporte")));



                }

                Linea = "<tr>";
                sw.WriteLine(Linea);

                Linea = "<td>&nbsp;</td>";
                sw.WriteLine(Linea);

                Linea = "<td>TOTAL</td>";
                sw.WriteLine(Linea);

                Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(VentaImporte)) + "</td>";
                sw.WriteLine(Linea);

                Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(UtilidadBrutaImporte)) + "</td>";
                sw.WriteLine(Linea);

                Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(MetaUtilidadBrutaImporte)) + "</td>";
                sw.WriteLine(Linea);

                Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(UtilidadBrutaProyectadaImporte)) + "</td>";
                sw.WriteLine(Linea);


                Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(Diferencial)) + "</td>";
                sw.WriteLine(Linea);
                Linea = "</tr>";
                sw.WriteLine(Linea); sw.WriteLine(Linea);




                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                Linea = "</table>";
                sw.WriteLine(Linea);

                Linea = "</td>";
                sw.WriteLine(Linea);


                Linea = "</tr>";
                sw.WriteLine(Linea);

                Linea = "</table>";
                sw.WriteLine(Linea);
                sw.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }




        }



        public void Reporte_Monitore_Representante_Territorio(string Conexion, int Id_Emp
            , int Id_Cd
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , ref String NombreArchivo
            )
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@mesInicial", "@anioInicial", "@mesFinal", "@anioFinal", "@Id_U" };


                object[] Valores = { Id_Emp, Id_Cd, MesInicial, AnioInicial, MesFinal, AnioFinal, Id_U };


                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepVenUtilidadBrutaMonitoreoTiempoTerr", ref dr, Parametros, Valores);


                NombreArchivo = "F:/APLICACIONES_IIS/sianwebmty/Terr.xls";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(NombreArchivo);
                NombreArchivo = "http://189.206.126.67/sianwebmty/Terr.xls";
                String Linea;

                Linea = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>";
                sw.WriteLine(Linea);

                Linea = "<table>";
                sw.WriteLine(Linea);
                Linea = "<tr>";
                sw.WriteLine(Linea);
                Linea = "<td align=\"center\">";
                sw.WriteLine(Linea);
                Linea = "REPORTE MONITOREO GESTION DE RENTABILIDAD POR TERRITORIO";
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);
                Linea = "</tr>";
                sw.WriteLine(Linea);


                Linea = "<tr>";
                sw.WriteLine(Linea);
                Linea = "<td>";
                sw.WriteLine(Linea);

                Linea = "<table border=\"1\">";
                sw.WriteLine(Linea);


                Linea = "<tr>";
                sw.WriteLine(Linea);

                Linea = "<td>#Representante</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Representante</td>";
                sw.WriteLine(Linea);

                Linea = "<td>#Territorio</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Venta $</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Utilidad Bruta $</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Meta Utilidad Bruta $</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Utilidad Bruta Proyectada $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Diferencial $</td>";
                sw.WriteLine(Linea);

                Linea = "</tr>";
                sw.WriteLine(Linea); sw.WriteLine(Linea);

                double VentaImporte;
                double UtilidadBrutaImporte;
                double MetaUtilidadBrutaImporte;
                double UtilidadBrutaProyectadaImporte;
                double Diferencial;


                VentaImporte = 0;
                UtilidadBrutaImporte = 0;
                MetaUtilidadBrutaImporte = 0;
                UtilidadBrutaProyectadaImporte = 0;
                Diferencial = 0;


                while (dr.Read())
                {

                    Linea = "<tr>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Rik"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Rik_Nombre"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Ter"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaImporte")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaImporte")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte"))) - Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte")))) + "</td>";
                    sw.WriteLine(Linea);



                    Linea = "</tr>";
                    sw.WriteLine(Linea); sw.WriteLine(Linea);


                    VentaImporte = VentaImporte + Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VentaImporte")));
                    UtilidadBrutaImporte = UtilidadBrutaImporte + Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte")));
                    MetaUtilidadBrutaImporte = MetaUtilidadBrutaImporte + Convert.ToDouble(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte")));
                    UtilidadBrutaProyectadaImporte = UtilidadBrutaProyectadaImporte + Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaImporte")));
                    Diferencial = Diferencial + (Convert.ToDouble(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte"))) - Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte"))));


                    //Dmonitoreogestionrentabilidad.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    //Dmonitoreogestionrentabilidad.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CD")));
                    //Dmonitoreogestionrentabilidad.Rik_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Rik_Nombre")));
                    //Dmonitoreogestionrentabilidad.VentaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaImporte")));
                    //Dmonitoreogestionrentabilidad.UtilidadBrutaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte")));
                    //Dmonitoreogestionrentabilidad.UtilidadBrutaPorc = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaPorc")));
                    //Dmonitoreogestionrentabilidad.MetaUtilidadBrutaPorc = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaPorc")));
                    // Dmonitoreogestionrentabilidad.MetaUtilidadBrutaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte")));
                    //Dmonitoreogestionrentabilidad.UtilidadBrutaProyectadaPorc = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaPorc")));
                    //Dmonitoreogestionrentabilidad.UtilidadBrutaProyectadaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaImporte")));



                }

                Linea = "<tr>";
                sw.WriteLine(Linea);

                Linea = "<td>&nbsp;</td>";
                sw.WriteLine(Linea);

                Linea = "<td>&nbsp;</td>";
                sw.WriteLine(Linea);

                Linea = "<td>TOTAL</td>";
                sw.WriteLine(Linea);

                Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(VentaImporte)) + "</td>";
                sw.WriteLine(Linea);

                Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(UtilidadBrutaImporte)) + "</td>";
                sw.WriteLine(Linea);

                Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(MetaUtilidadBrutaImporte)) + "</td>";
                sw.WriteLine(Linea);

                Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(UtilidadBrutaProyectadaImporte)) + "</td>";
                sw.WriteLine(Linea);


                Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(Diferencial)) + "</td>";
                sw.WriteLine(Linea);
                Linea = "</tr>";
                sw.WriteLine(Linea); sw.WriteLine(Linea);




                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                Linea = "</table>";
                sw.WriteLine(Linea);

                Linea = "</td>";
                sw.WriteLine(Linea);


                Linea = "</tr>";
                sw.WriteLine(Linea);

                Linea = "</table>";
                sw.WriteLine(Linea);
                sw.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }




        }



        public void Reporte_Monitore_Representante_Cliente(string Conexion, int Id_Emp
            , int Id_Cd
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , ref String NombreArchivo
            )
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@mesInicial", "@anioInicial", "@mesFinal", "@anioFinal", "@Id_U" };


                object[] Valores = { Id_Emp, Id_Cd, MesInicial, AnioInicial, MesFinal, AnioFinal, Id_U };


                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepVenUtilidadBrutaMonitoreoTiempoCliente", ref dr, Parametros, Valores);


                NombreArchivo = "F:/APLICACIONES_IIS/sianwebmty/TerrCliente.xls";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(NombreArchivo);
                NombreArchivo = "http://189.206.126.67/sianwebmty/TerrCliente.xls";


                String Linea;

                Linea = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>";
                sw.WriteLine(Linea);

                Linea = "<table>";
                sw.WriteLine(Linea);
                Linea = "<tr>";
                sw.WriteLine(Linea);
                Linea = "<td align=\"center\">";
                sw.WriteLine(Linea);
                Linea = "REPORTE MONITOREO GESTION DE RENTABILIDAD POR CLIENTE";
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);
                Linea = "</tr>";
                sw.WriteLine(Linea);


                Linea = "<tr>";
                sw.WriteLine(Linea);
                Linea = "<td>";
                sw.WriteLine(Linea);

                Linea = "<table border=\"1\">";
                sw.WriteLine(Linea);


                Linea = "<tr>";
                sw.WriteLine(Linea);

                Linea = "<td>#Territorio</td>";
                sw.WriteLine(Linea);

                Linea = "<td>#Cliente</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Cliente</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Venta $</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Utilidad Bruta $</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Meta Utilidad Bruta $</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Utilidad Bruta Proyectada $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Diferencial $</td>";
                sw.WriteLine(Linea);

                Linea = "</tr>";
                sw.WriteLine(Linea); sw.WriteLine(Linea);

                double VentaImporte;
                double UtilidadBrutaImporte;
                double MetaUtilidadBrutaImporte;
                double UtilidadBrutaProyectadaImporte;
                double Diferencial;


                VentaImporte = 0;
                UtilidadBrutaImporte = 0;
                MetaUtilidadBrutaImporte = 0;
                UtilidadBrutaProyectadaImporte = 0;
                Diferencial = 0;


                while (dr.Read())
                {

                    Linea = "<tr>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Ter"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Cte"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Cliente"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaImporte")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaImporte")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte"))) - Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte")))) + "</td>";
                    sw.WriteLine(Linea);



                    Linea = "</tr>";
                    sw.WriteLine(Linea); sw.WriteLine(Linea);


                    VentaImporte = VentaImporte + Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VentaImporte")));
                    UtilidadBrutaImporte = UtilidadBrutaImporte + Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte")));
                    MetaUtilidadBrutaImporte = MetaUtilidadBrutaImporte + Convert.ToDouble(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte")));
                    UtilidadBrutaProyectadaImporte = UtilidadBrutaProyectadaImporte + Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaImporte")));
                    Diferencial = Diferencial + (Convert.ToDouble(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte"))) - Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte"))));


                    //Dmonitoreogestionrentabilidad.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    //Dmonitoreogestionrentabilidad.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CD")));
                    //Dmonitoreogestionrentabilidad.Rik_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Rik_Nombre")));
                    //Dmonitoreogestionrentabilidad.VentaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaImporte")));
                    //Dmonitoreogestionrentabilidad.UtilidadBrutaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte")));
                    //Dmonitoreogestionrentabilidad.UtilidadBrutaPorc = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaPorc")));
                    //Dmonitoreogestionrentabilidad.MetaUtilidadBrutaPorc = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaPorc")));
                    // Dmonitoreogestionrentabilidad.MetaUtilidadBrutaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte")));
                    //Dmonitoreogestionrentabilidad.UtilidadBrutaProyectadaPorc = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaPorc")));
                    //Dmonitoreogestionrentabilidad.UtilidadBrutaProyectadaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaImporte")));



                }

                Linea = "<tr>";
                sw.WriteLine(Linea);

                Linea = "<td>&nbsp;</td>";
                sw.WriteLine(Linea);

                Linea = "<td>&nbsp;</td>";
                sw.WriteLine(Linea);

                Linea = "<td>TOTAL</td>";
                sw.WriteLine(Linea);

                Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(VentaImporte)) + "</td>";
                sw.WriteLine(Linea);

                Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(UtilidadBrutaImporte)) + "</td>";
                sw.WriteLine(Linea);

                Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(MetaUtilidadBrutaImporte)) + "</td>";
                sw.WriteLine(Linea);

                Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(UtilidadBrutaProyectadaImporte)) + "</td>";
                sw.WriteLine(Linea);


                Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(Diferencial)) + "</td>";
                sw.WriteLine(Linea);
                Linea = "</tr>";
                sw.WriteLine(Linea); sw.WriteLine(Linea);




                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                Linea = "</table>";
                sw.WriteLine(Linea);

                Linea = "</td>";
                sw.WriteLine(Linea);


                Linea = "</tr>";
                sw.WriteLine(Linea);

                Linea = "</table>";
                sw.WriteLine(Linea);
                sw.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }




        }






        public void Reporte_Monitore_Acciones_Producto(string Conexion, int Id_Emp
            , int Id_Cd
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , ref String NombreArchivo
            )
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@mesInicial", "@anioInicial", "@mesFinal", "@anioFinal", "@Id_U" };


                object[] Valores = { Id_Emp, Id_Cd, MesInicial, AnioInicial, MesFinal, AnioFinal, Id_U };


                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepVenUtilidadBrutaMonitoreoAcciones", ref dr, Parametros, Valores);


                NombreArchivo = "F:/APLICACIONES_IIS/sianwebmty/AccionesProducto.xls";
                ///NombreArchivo = "C:/files/key/rentabilidad/AccionesProducto.xls";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(NombreArchivo);
                NombreArchivo = "http://189.206.126.67/sianwebmty/AccionesProducto.xls";


                String Linea;

                Linea = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>";
                sw.WriteLine(Linea);

                Linea = "<table>";
                sw.WriteLine(Linea);
                Linea = "<tr>";
                sw.WriteLine(Linea);
                Linea = "<td align=\"center\">";
                sw.WriteLine(Linea);
                Linea = "REPORTE MONITOREO GESTION DE RENTABILIDAD POR PRODUCTO";
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);
                Linea = "</tr>";
                sw.WriteLine(Linea);


                Linea = "<tr>";
                sw.WriteLine(Linea);
                Linea = "<td>";
                sw.WriteLine(Linea);

                Linea = "<table border=\"1\">";
                sw.WriteLine(Linea);


                Linea = "<tr>";
                sw.WriteLine(Linea);

                Linea = "<td>#Representante</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Representante</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Acción</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Año Acción</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Mes Acción</td>";
                sw.WriteLine(Linea);

                Linea = "<td>#Territorio</td>";
                sw.WriteLine(Linea);

                Linea = "<td>#Cliente</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Cliente</td>";
                sw.WriteLine(Linea);

                Linea = "<td>#Producto</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Producto</td>";
                sw.WriteLine(Linea);


                Linea = "<td>#Producto Cambio</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Producto Cambio</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Cantidad</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Precio Venta $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Precio Distribuidor $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Venta $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Costo $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Utilidad Bruta $</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Utilidad Bruta %</td>";
                sw.WriteLine(Linea);
                
                Linea = "<td>Cantidad Anterio</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Precio Venta Anterior $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Precio Distribuidor Anterior $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Venta Anterior $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Costo Anterior $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Utilidad Bruta Anterior $</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Utilidad Bruta Anterior %</td>";
                sw.WriteLine(Linea);
                



                Linea = "</tr>";
                sw.WriteLine(Linea); sw.WriteLine(Linea);



                while (dr.Read())
                {

                    Linea = "<tr>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Rik"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Rik_Nombre"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Accion"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("AnioAccion"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("MesAccion"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Ter"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Cte"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial"))) + "</td>";
                    sw.WriteLine(Linea);


                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Prd"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_PrdP"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_DescripcionP"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cantidadP")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PrecioVentaP")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PrecioDistribuidorP")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ventaP")))) + "</td>";
                    sw.WriteLine(Linea);


                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CostoP")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaP")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorcUBRealP")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cantidad")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PrecioVenta")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PrecioDistribuidor")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("venta")))) + "</td>";
                    sw.WriteLine(Linea);


                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Costo")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBruta")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorcUBReal")))) + "</td>";
                    sw.WriteLine(Linea);


                    Linea = "</tr>";
                    sw.WriteLine(Linea); sw.WriteLine(Linea);

                }


                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                Linea = "</table>";
                sw.WriteLine(Linea);

                Linea = "</td>";
                sw.WriteLine(Linea);


                Linea = "</tr>";
                sw.WriteLine(Linea);

                Linea = "</table>";
                sw.WriteLine(Linea);
                sw.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }




        }





        public void Reporte_Monitore_Acciones_Producto_Cumplimiento(string Conexion, int Id_Emp
            , int Id_Cd
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , ref String NombreArchivo
            )
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@mesInicial", "@anioInicial", "@mesFinal", "@anioFinal", "@Id_U" };


                object[] Valores = { Id_Emp, Id_Cd, MesInicial, AnioInicial, MesFinal, AnioFinal, Id_U };


                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepVenUtilidadBrutaMonitoreoAccionesCumplimiento", ref dr, Parametros, Valores);


                NombreArchivo = "F:/APLICACIONES_IIS/sianwebmty/AccionesProductoCumplimiento.xls";
               // NombreArchivo = "C:/AccionesProductoCumplimiento.xls";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(NombreArchivo);
                NombreArchivo = "http://189.206.126.67/sianwebmty/AccionesProductoCumplimiento.xls";


                String Linea;

                Linea = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>";
                sw.WriteLine(Linea);

                Linea = "<table>";
                sw.WriteLine(Linea);
                Linea = "<tr>";
                sw.WriteLine(Linea);
                Linea = "<td align=\"center\">";
                sw.WriteLine(Linea);
                Linea = "REPORTE MONITOREO GESTION DE RENTABILIDAD<br>CUMPLIMIENTO POR PRODUCTO";
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);
                Linea = "</tr>";
                sw.WriteLine(Linea);


                Linea = "<tr>";
                sw.WriteLine(Linea);
                Linea = "<td>";
                sw.WriteLine(Linea);

                Linea = "<table border=\"1\">";
                sw.WriteLine(Linea);


                Linea = "<tr><td colspan=\"10\" align=\"center\" bgcolor=\"#FFFF99\">Información General</td><td colspan=\"9\" align=\"center\" bgcolor=\"#CCFFFF\">Información Proyectada</td><td colspan=\"8\" align=\"center\" bgcolor=\"#FFCCFF\">Información Real</td></tr>";
                sw.WriteLine(Linea);

                Linea = "<td>#Representante</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Representante</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Acción</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Año Acción</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Mes Acción</td>";
                sw.WriteLine(Linea);

                Linea = "<td>#Territorio</td>";
                sw.WriteLine(Linea);

                Linea = "<td>#Cliente</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Cliente</td>";
                sw.WriteLine(Linea);

                Linea = "<td>#Producto</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Producto</td>";
                sw.WriteLine(Linea);


                Linea = "<td>#Producto Cambio</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Producto Cambio</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Cantidad</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Precio Venta $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Precio Distribuidor $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Venta $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Costo $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Utilidad Bruta $</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Utilidad Bruta %</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Cantidad</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Precio Venta $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Precio Distribuidor $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Venta $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Costo $</td>";
                sw.WriteLine(Linea);


                Linea = "<td>Utilidad Bruta $</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Utilidad Bruta %</td>";
                sw.WriteLine(Linea);

                Linea = "<td>Cumplimiento %</td>";
                sw.WriteLine(Linea);


                Linea = "</tr>";
                sw.WriteLine(Linea); sw.WriteLine(Linea);


                decimal UtilidadBrutaProyectada;
                decimal UtilidadBrutaReal;

                decimal TotalUtilidadBrutaProyectada;
                decimal TotalUtilidadBrutaReal;


                string IndiceRik;

                UtilidadBrutaProyectada = 0;
                UtilidadBrutaReal = 0;

                TotalUtilidadBrutaProyectada = 0;
                TotalUtilidadBrutaReal = 0;

                IndiceRik = "";
                while (dr.Read())
                {
                    if (IndiceRik == "")
                    {
                        IndiceRik = Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    }

                    if (IndiceRik != Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Rik"))))
                    {

                        Linea = "<tr bgcolor=\"#FFFF00\"><td colspan=\"17\"><font color=\"#FF0000\"><b>Total</b></font></td><td><font color=\"#FF0000\"><b>" + String.Format("{0:0,0.00}", UtilidadBrutaProyectada) + "</b></font></td><td colspan=\"6\">&nbsp;</td><td><font color=\"#FF0000\"><b>" + String.Format("{0:0,0.00}", UtilidadBrutaReal) + "</b></font></td><td>&nbsp;</td><td><font color=\"#FF0000\"><b>" + String.Format("{0:0,0.00}", ((UtilidadBrutaReal / UtilidadBrutaProyectada) * 100)) + "</b></font></td></tr>";
                        sw.WriteLine(Linea);

                        UtilidadBrutaProyectada = 0;
                        UtilidadBrutaReal = 0;
                        IndiceRik = Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    }

                    Linea = "<tr>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Rik"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Rik_Nombre"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Accion"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("AnioAccion"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("MesAccion"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Ter"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Cte"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial"))) + "</td>";
                    sw.WriteLine(Linea);


                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Prd"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_PrdP"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_DescripcionP"))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cantidadP")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PrecioVentaP")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PrecioDistribuidorP")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ventaP")))) + "</td>";
                    sw.WriteLine(Linea);


                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CostoP")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaP")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorcUBRealP")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cantidad")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PrecioVenta")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PrecioDistribuidor")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("venta")))) + "</td>";
                    sw.WriteLine(Linea);


                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Costo")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBruta")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorcUBReal")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>" + String.Format("{0:0,0.00}", Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cumplimiento")))) + "</td>";
                    sw.WriteLine(Linea);

                    Linea = "</tr>";
                    sw.WriteLine(Linea); sw.WriteLine(Linea);

                    UtilidadBrutaProyectada = UtilidadBrutaProyectada + Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaP")));
                    UtilidadBrutaReal = UtilidadBrutaReal + Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBruta")));


                    TotalUtilidadBrutaProyectada = TotalUtilidadBrutaProyectada + Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaP")));
                    TotalUtilidadBrutaReal = TotalUtilidadBrutaReal + Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBruta")));

                }

                Linea = "<tr bgcolor=\"#FFFF00\"><td colspan=\"17\"><font color=\"#FF0000\"><b>Total</b></font></td><td><font color=\"#FF0000\"><b>" + String.Format("{0:0,0.00}", UtilidadBrutaProyectada) + "</b></font></td><td colspan=\"6\">&nbsp;</td><td><font color=\"#FF0000\"><b>" + String.Format("{0:0,0.00}", UtilidadBrutaReal) + "</b></font></td><td>&nbsp;</td><td><font color=\"#FF0000\"><b>" + String.Format("{0:0,0.00}", ((UtilidadBrutaReal / UtilidadBrutaProyectada) * 100)) + "</b></font></td></tr>";
                sw.WriteLine(Linea);

                Linea = "<tr><td colspan=\"27\">&nbsp;</td></tr>";
                sw.WriteLine(Linea);

                Linea = "<tr bgcolor=\"#FFFF00\"><td colspan=\"17\"><font color=\"#FF0000\"><b>Gran Total</b></font></td><td><font color=\"#FF0000\"><b>" + String.Format("{0:0,0.00}", TotalUtilidadBrutaProyectada) + "</b></font></td><td colspan=\"6\">&nbsp;</td><td><font color=\"#FF0000\"><b>" + String.Format("{0:0,0.00}", TotalUtilidadBrutaReal) + "</b></font></td><td>&nbsp;</td><td><font color=\"#FF0000\"><b>" + String.Format("{0:0,0.00}", ((TotalUtilidadBrutaReal / TotalUtilidadBrutaProyectada) * 100)) + "</b></font></td></tr>";
                sw.WriteLine(Linea);


                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                Linea = "</table>";
                sw.WriteLine(Linea);

                Linea = "</td>";
                sw.WriteLine(Linea);


                Linea = "</tr>";
                sw.WriteLine(Linea);

                Linea = "</table>";
                sw.WriteLine(Linea);
                sw.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }




        }






        public void MonitoreoGestionRentabildiad_Buscar(MonitoreoGestionRentabilidad monitoreogestionrentabilidad, string Conexion, ref List<MonitoreoGestionRentabilidad> List
            , int Id_Emp
            , int Id_Cd
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , ref string Grafica
            )
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@mesInicial", "@anioInicial", "@mesFinal", "@anioFinal", "@Id_U" };


                object[] Valores = { Id_Emp, Id_Cd,  MesInicial, AnioInicial, MesFinal, AnioFinal, Id_U };


                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepVenUtilidadBrutaMonitoreoTiempo", ref dr, Parametros, Valores);


                string Categorias = "";
               // string SerieVentas = "";
                string SerieUtilidadBrutaImporte = "";
                string SerieMetaUtilidadBrutaImporte = "";
                string SerieUtilidadBrutaProyectadaImporte = "";


                while (dr.Read())
                {

                    MonitoreoGestionRentabilidad Dmonitoreogestionrentabilidad = new MonitoreoGestionRentabilidad();
                    Dmonitoreogestionrentabilidad.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    Dmonitoreogestionrentabilidad.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CD")));
                    Dmonitoreogestionrentabilidad.Rik_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Rik_Nombre")));
                    Dmonitoreogestionrentabilidad.VentaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaImporte")));
                    Dmonitoreogestionrentabilidad.UtilidadBrutaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte")));
                    Dmonitoreogestionrentabilidad.UtilidadBrutaPorc = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaPorc")));
                    Dmonitoreogestionrentabilidad.MetaUtilidadBrutaPorc = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaPorc")));
                    Dmonitoreogestionrentabilidad.MetaUtilidadBrutaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte")));
                    Dmonitoreogestionrentabilidad.UtilidadBrutaProyectadaPorc = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaPorc")));
                    Dmonitoreogestionrentabilidad.UtilidadBrutaProyectadaImporte = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaImporte")));

                    Categorias = Categorias + "<category label='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Rik_Nombre"))) + "' />";
                 //   SerieVentas = SerieVentas + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("VentaImporte"))) + "' />";
                    SerieUtilidadBrutaImporte = SerieUtilidadBrutaImporte + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("UtilidadBrutaImporte"))) + "' link='CapMonitoreoGestionRentabilidadRIK.aspx?Id_Rik=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Rik"))) + "%26TxtAnioInicial=" + Convert.ToString(AnioInicial) + "%26TxtAnioFinal=" + Convert.ToString(AnioFinal) + "%26txtMesInicial=" + Convert.ToString(MesInicial) + "%26txtMesFinal=" + Convert.ToString(MesFinal) + "'/>";
                    SerieMetaUtilidadBrutaImporte = SerieMetaUtilidadBrutaImporte + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("MetaUtilidadBrutaImporte"))) + "' link='CapMonitoreoGestionRentabilidadRIK.aspx?Id_Rik=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Rik"))) + "%26TxtAnioInicial=" + Convert.ToString(AnioInicial) + "%26TxtAnioFinal=" + Convert.ToString(AnioFinal) + "%26txtMesInicial=" + Convert.ToString(MesInicial) + "%26txtMesFinal=" + Convert.ToString(MesFinal) + "' />";
                    SerieUtilidadBrutaProyectadaImporte = SerieUtilidadBrutaProyectadaImporte + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("UtilidadBrutaProyectadaImporte"))) + "'  link='CapMonitoreoGestionRentabilidadRIK.aspx?Id_Rik=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Rik"))) + "%26TxtAnioInicial=" + Convert.ToString(AnioInicial) + "%26TxtAnioFinal=" + Convert.ToString(AnioFinal) + "%26txtMesInicial=" + Convert.ToString(MesInicial) + "%26txtMesFinal=" + Convert.ToString(MesFinal) + "'/>";

                    List.Add(Dmonitoreogestionrentabilidad);


                }

                //Grafica = "<div id='myNextDiv' ><!-- START Code Block for Chart myNext --><embed src=\"FusionCharts/msLine.swf\" FlashVars=\"&chartWidth=800&chartHeight=800&debugMode=0&registerWithJS=0&DOMId=myNext&dataXML=<chart caption='Utilidad Bruta vs Proyectada vs Metas' xAxisName='" + xAxisName + "' yAxisName='Utilidad Bruta' showValues='0' numberPrefix='$' labelDisplay='ROTATE'><categories>" + Categorias + "</categories><dataset seriesName='Utilidad Bruta'>" + Linea1 + "</dataset><dataset seriesName='Meta Utilidad Bruta'>" + Linea2 + "</dataset><dataset seriesName='Proyectada Utilidad Bruta'>" + Linea3 + "</dataset><trendlines><line startValue='" + Convert.ToString(PuntoMedio) + "' color='91C728' displayValue='Objetivo' showOnTop='1'/></trendlines></chart>&scaleMode=noScale&lang=EN\" quality=\"high\" width=\"600\" height=\"600\" name=\"myNext\" id=\"myNext\" allowScriptAccess=\"always\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"   /> <!-- END Code Block for Chart myNext --> </div>";
                //Grafica = "<div id='myNextDiv' ><!-- START Code Block for Chart myNext --><embed src=\"FusionCharts/MSColumn3D.swf\" FlashVars=\"&chartWidth=800&chartHeight=800&debugMode=0&registerWithJS=0&DOMId=myNext&dataXML=<chart caption='Monitoreo de Gestión de Rentabilidad' XAxisName='RIKS-Territrorios' palette='2' animation='1' formatNumberScale='0' numberPrefix='$' showValues='0' numDivLines='4' legendPosition='BOTTOM'><categories>" + Categorias + "</categories><dataset seriesName='Venta'>" + SerieVentas + "</dataset><dataset seriesName='Utilidad Bruta'>" + SerieUtilidadBrutaImporte + "</dataset><dataset seriesName='Utilidad Bruta Meta'>" + SerieMetaUtilidadBrutaImporte + "</dataset><dataset seriesName='Utilidad Bruta Proyectada'>" + SerieUtilidadBrutaProyectadaImporte + "</dataset><styles><definition><style type='font' name='CaptionFont' color='666666' size='15' /><style type='font' name='SubCaptionFont' bold='0' /></definition><application><apply toObject='caption' styles='CaptionFont' /><apply toObject='SubCaption' styles='SubCaptionFont' /></application></styles></chart>&scaleMode=noScale&lang=EN\" quality=\"high\" width=\"600\" height=\"600\" name=\"myNext\" id=\"myNext\" allowScriptAccess=\"always\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"   /> <!-- END Code Block for Chart myNext --> </div>";
                Grafica = "<div id='myNextDiv' ><!-- START Code Block for Chart myNext --><embed src=\"FusionCharts/MSColumn3D.swf\" FlashVars=\"&chartWidth=800&chartHeight=800&debugMode=0&registerWithJS=0&DOMId=myNext&dataXML=<chart caption='Monitoreo de Gestión de Rentabilidad' XAxisName='RIKS' palette='2' animation='1' formatNumberScale='0' numberPrefix='$' showValues='0' numDivLines='4' legendPosition='BOTTOM' labelDisplay='ROTATE'><categories>" + Categorias + "</categories><dataset seriesName='Utilidad Bruta'>" + SerieUtilidadBrutaImporte + "</dataset><dataset seriesName='Utilidad Bruta Meta'>" + SerieMetaUtilidadBrutaImporte + "</dataset><dataset seriesName='Utilidad Bruta Proyectada'>" + SerieUtilidadBrutaProyectadaImporte + "</dataset><styles><definition><style type='font' name='CaptionFont' color='666666' size='15' /><style type='font' name='SubCaptionFont' bold='0' /></definition><application><apply toObject='caption' styles='CaptionFont' /><apply toObject='SubCaption' styles='SubCaptionFont' /></application></styles></chart>&scaleMode=noScale&lang=EN\" quality=\"high\" width=\"600\" height=\"600\" name=\"myNext\" id=\"myNext\" allowScriptAccess=\"always\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"   /> <!-- END Code Block for Chart myNext --> </div>";

                dr.Close();



                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public void MonitoreoIndicadoresUtilidad_Buscar(MonitoreoIndicadoresUtilidad monitoreoIndicadoresUtilidad, string Conexion, ref List<MonitoreoIndicadoresUtilidad> List
            , int Id_Emp
            , int Id_Cd
            , int Id_Ter
            , int Id_Rik
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , ref string Grafica
            )
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ter", "@mesInicial", "@anioInicial", "@mesFinal", "@anioFinal", "@Salida", "@Id_U","@Id_Rik" };


                object[] Valores = { Id_Emp, Id_Cd, Id_Ter, MesInicial, AnioInicial, MesFinal, AnioFinal, 2, Id_U, Id_Rik };


                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spMonitoreoIndicadoresUtilidad", ref dr, Parametros, Valores);


                string LineaUtilidadBruta = "";


                string Categorias = "";
                string Linea1 = "";
                string Linea2 = "";
                string Linea3 = "";
                Decimal PuntoMedio = 0;
                string xAxisName = "";


while (dr.Read())
{

    MonitoreoIndicadoresUtilidad DmonitoreoIndicadoresUtilidad = new MonitoreoIndicadoresUtilidad();
    DmonitoreoIndicadoresUtilidad.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
    DmonitoreoIndicadoresUtilidad.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_CD")));
    DmonitoreoIndicadoresUtilidad.Rik_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion")));
    DmonitoreoIndicadoresUtilidad.UtilidadBruta = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBruta")));
    DmonitoreoIndicadoresUtilidad.MetaUtilidadBruta = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("MetaUtilidadBruta")));
    DmonitoreoIndicadoresUtilidad.UtilidadBrutaGestion = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaGestion")));

    LineaUtilidadBruta = LineaUtilidadBruta + "<set label='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion"))) + "' value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("UtilidadBruta"))) + "'  distance='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Numero"))) + "'/>";

    Categorias = Categorias + "<category label='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion")))  + "' />";
    if (Id_Rik == 0 || Id_Rik == -1)
    { /*http://localhost:64336/*/
        Linea1 = Linea1 + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("UtilidadBruta"))) + "' link='RepMonitoreoIndicadores.aspx?Id_Rik=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Numero"))) + "%26TxtAnioInicial=" + Convert.ToString(AnioInicial) + "%26TxtAnioFinal=" + Convert.ToString(AnioFinal) + "%26txtMesInicial=" + Convert.ToString(MesInicial) + "%26txtMesFinal=" + Convert.ToString(MesFinal) + "' />";
        Linea2 = Linea2 + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("MetaUtilidadBruta"))) + "' link='RepMonitoreoIndicadores.aspx?Id_Rik=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Numero"))) + "%26TxtAnioInicial=" + Convert.ToString(AnioInicial) + "%26TxtAnioFinal=" + Convert.ToString(AnioFinal) + "%26txtMesInicial=" + Convert.ToString(MesInicial) + "%26txtMesFinal=" + Convert.ToString(MesFinal) + "' />";
        Linea3 = Linea3 + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("UtilidadBrutaGestion"))) + "' link='RepMonitoreoIndicadores.aspx?Id_Rik=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Numero"))) + "%26TxtAnioInicial=" + Convert.ToString(AnioInicial) + "%26TxtAnioFinal=" + Convert.ToString(AnioFinal) + "%26txtMesInicial=" + Convert.ToString(MesInicial) + "%26txtMesFinal=" + Convert.ToString(MesFinal) + "' />";
        xAxisName = "RIKS";
    }
    else
    {
        if (Id_Ter == 0 || Id_Ter == -1)
        {
            Linea1 = Linea1 + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("UtilidadBruta"))) + "' link='RepMonitoreoIndicadores.aspx?Id_Rik=" + Convert.ToString(Id_Rik) + "%26Id_Ter=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Numero"))) + "%26TxtAnioInicial=" + Convert.ToString(AnioInicial) + "%26TxtAnioFinal=" + Convert.ToString(AnioFinal) + "%26txtMesInicial=" + Convert.ToString(MesInicial) + "%26txtMesFinal=" + Convert.ToString(MesFinal) + "' />";
            Linea2 = Linea2 + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("MetaUtilidadBruta"))) + "' link='RepMonitoreoIndicadores.aspx?Id_Rik=" + Convert.ToString(Id_Rik) + "%26Id_Ter=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Numero"))) + "%26TxtAnioInicial=" + Convert.ToString(AnioInicial) + "%26TxtAnioFinal=" + Convert.ToString(AnioFinal) + "%26txtMesInicial=" + Convert.ToString(MesInicial) + "%26txtMesFinal=" + Convert.ToString(MesFinal) + "' />";
            Linea3 = Linea3 + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("UtilidadBrutaGestion"))) + "' link='RepMonitoreoIndicadores.aspx?Id_Rik=" + Convert.ToString(Id_Rik) + "%26Id_Ter=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Numero"))) + "%26TxtAnioInicial=" + Convert.ToString(AnioInicial) + "%26TxtAnioFinal=" + Convert.ToString(AnioFinal) + "%26txtMesInicial=" + Convert.ToString(MesInicial) + "%26txtMesFinal=" + Convert.ToString(MesFinal) + "' />";
            xAxisName = "Territorios";
        }
        else
        {
            Linea1 = Linea1 + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("UtilidadBruta"))) + "' link='CapGestionRentabilidadSimulador.aspx?Id_Emp=1%26Id_Cd=" + Convert.ToString(Id_Cd) + "%26Id_Ter=" + Convert.ToString(Id_Ter) + "%26Id_Cte=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Numero"))) + "%26TxtNombreCliente=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion"))) + "%26txtMesInicial=" + Convert.ToString(MesInicial) + "%26txtMesFinal=" + Convert.ToString(MesFinal) + "%26TxtAnioInicial=" + Convert.ToString(AnioInicial) + "%26TxtAnioFinal=" + Convert.ToString(AnioFinal) + "%26txtDondeViene=" + Convert.ToString(Id_Rik) + "'/>";
            Linea2 = Linea2 + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("MetaUtilidadBruta"))) + "' link='CapGestionRentabilidadSimulador.aspx?Id_Emp=1%26Id_Cd=" + Convert.ToString(Id_Cd) + "%26Id_Ter=" + Convert.ToString(Id_Ter) + "%26Id_Cte=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Numero"))) + "%26TxtNombreCliente=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion"))) + "%26txtMesInicial=" + Convert.ToString(MesInicial) + "%26txtMesFinal=" + Convert.ToString(MesFinal) + "%26TxtAnioInicial=" + Convert.ToString(AnioInicial) + "%26TxtAnioFinal=" + Convert.ToString(AnioFinal) + "%26txtDondeViene=" + Convert.ToString(Id_Rik) + "' />";
            Linea3 = Linea3 + "<set value='" + Convert.ToString(dr.GetValue(dr.GetOrdinal("UtilidadBrutaGestion"))) + "' link='CapGestionRentabilidadSimulador.aspx?Id_Emp=1%26Id_Cd=" + Convert.ToString(Id_Cd) + "%26Id_Ter=" + Convert.ToString(Id_Ter) + "%26Id_Cte=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Numero"))) + "%26TxtNombreCliente=" + Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion"))) + "%26txtMesInicial=" + Convert.ToString(MesInicial) + "%26txtMesFinal=" + Convert.ToString(MesFinal) + "%26TxtAnioInicial=" + Convert.ToString(AnioInicial) + "%26TxtAnioFinal=" + Convert.ToString(AnioFinal) + "%26txtDondeViene=" + Convert.ToString(Id_Rik) + "' />";
            xAxisName = "Clientes";
        }
    }


    List.Add(DmonitoreoIndicadoresUtilidad);

    if (PuntoMedio < Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBruta"))))
    {
        PuntoMedio = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBruta")));
    }



}

Grafica = "<div id='myNextDiv' ><!-- START Code Block for Chart myNext --><embed src=\"FusionCharts/msLine.swf\" FlashVars=\"&chartWidth=800&chartHeight=800&debugMode=0&registerWithJS=0&DOMId=myNext&dataXML=<chart caption='Utilidad Bruta vs Proyectada vs Metas' xAxisName='" + xAxisName + "' yAxisName='Utilidad Bruta' showValues='0' numberPrefix='$' labelDisplay='ROTATE'><categories>" + Categorias + "</categories><dataset seriesName='Utilidad Bruta'>" + Linea1 + "</dataset><dataset seriesName='Meta Utilidad Bruta'>" + Linea2 + "</dataset><dataset seriesName='Proyectada Utilidad Bruta'>" + Linea3 + "</dataset><trendlines><line startValue='" + Convert.ToString(PuntoMedio) + "' color='91C728' displayValue='Maxima' showOnTop='1'/></trendlines></chart>&scaleMode=noScale&lang=EN\" quality=\"high\" width=\"600\" height=\"600\" name=\"myNext\" id=\"myNext\" allowScriptAccess=\"always\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"   /> <!-- END Code Block for Chart myNext --> </div>";



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
