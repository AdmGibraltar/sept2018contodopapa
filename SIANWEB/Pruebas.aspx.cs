using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;

namespace SIANWEB
{
    public partial class Pruebas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string GeneraGraficaDistribucion()
        {
            string caption = "Exceso de inventario";
            string subcaption = "Click en la columna para ver el detalle";
            string yAxsisName = "Costo de exceso de inventario";
            string xAxsisName = "";

            int dias = 30;

            double valor1 = 0;
            double valor2 = 91457;
            double valor3 = 567638;
            double valor4 = 254054;
            double valor5 = 148640;
            double valor6 = 1164030;
            
            StringBuilder xmlData = new StringBuilder();
            xmlData.Append("<chart subCaption='" + subcaption + "' Caption='" + caption + "' xAxisName='" + xAxsisName + "' yAxisName='" + yAxsisName + "' showValues='1' formatNumberScale='0' showBorder='0' numberPrefix='$' showSum='1' decimals='4'>");
            xmlData.Append("<set label='Días: " + (dias * 1).ToString() + "' value='" + valor1.ToString() + "' link='n-detalle.aspx?dias=" + (dias * 1).ToString() + "'/>");
            xmlData.Append("<set label='Días: " + (dias * 2).ToString() + "' value='" + valor2.ToString() + "' link='n-detalle.aspx?dias=" + (dias * 2).ToString() + "'/>");
            xmlData.Append("<set label='Días: " + (dias * 3).ToString() + "' value='" + valor3.ToString() + "' link='n-detalle.aspx?dias=" + (dias * 3).ToString() + "'/>");
            xmlData.Append("<set label='Días: " + (dias * 4).ToString() + "' value='" + valor4.ToString() + "'/>");
            xmlData.Append("<set label='Días: " + (dias * 5).ToString() + "' value='" + valor5.ToString() + "'/>");
            xmlData.Append("<set label='Días: " + (dias * 6).ToString() + "' value='" + valor6.ToString() + "'/>");
            xmlData.Append("<set label='Total' value='" + (valor1 + valor2 + valor3 + valor4 + valor5 + valor6).ToString() + "'/>");
            xmlData.Append("<styles>");
            xmlData.Append("<definition>");
            xmlData.Append("<style name='myCaptionFont' type='font' font='Arial' size='14' bold='1' />");
            xmlData.Append("</definition>");
            xmlData.Append("<application>");
            xmlData.Append("<apply toObject='Caption' styles='myCaptionFont' />");
            xmlData.Append("</application>");
            xmlData.Append("</styles>");
            xmlData.Append("</chart>");
            return InfoSoftGlobal.FusionCharts.RenderChartHTML("FusionCharts/Column3D.swf", "", xmlData.ToString(), "myNext", "100%", "300", false);
        }
    }
}

