using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Xml;

using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace SIANWEB
{
    public partial class Ventana_Calendario : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string id_TG = Request.QueryString["Id_TG"];

                if (Session["Fechas_" + id_TG] != null)
                {

                    Dictionary<int,DateTime> fechasLista= (Dictionary<int,DateTime>) Session["Fechas_" + id_TG];

                   if(fechasLista.ContainsKey(1)) calEnero.SelectedDate = Convert.ToDateTime(fechasLista[1]);
                   if(fechasLista.ContainsKey(2)) calFebrero.SelectedDate = Convert.ToDateTime(fechasLista[2]);
                   if(fechasLista.ContainsKey(3)) calMarzo.SelectedDate = Convert.ToDateTime(fechasLista[3]);
                   if(fechasLista.ContainsKey(4)) calAbril.SelectedDate = Convert.ToDateTime(fechasLista[4]);
                   if(fechasLista.ContainsKey(5)) calMayo.SelectedDate = Convert.ToDateTime(fechasLista[5]);
                   if(fechasLista.ContainsKey(6)) calJunio.SelectedDate = Convert.ToDateTime(fechasLista[6]);
                   if(fechasLista.ContainsKey(7)) calJulio.SelectedDate = Convert.ToDateTime(fechasLista[7]);
                   if(fechasLista.ContainsKey(8)) calAgosto.SelectedDate = Convert.ToDateTime(fechasLista[8]);
                   if(fechasLista.ContainsKey(9)) calSeptiembre.SelectedDate = Convert.ToDateTime(fechasLista[9]);
                   if(fechasLista.ContainsKey(10)) calOctubre.SelectedDate = Convert.ToDateTime(fechasLista[10]);
                   if(fechasLista.ContainsKey(11)) calNoviembre.SelectedDate = Convert.ToDateTime(fechasLista[11]);
                   if(fechasLista.ContainsKey(12)) calDiciembre.SelectedDate = Convert.ToDateTime(fechasLista[12]);
                }


                calEnero.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
                calEnero.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);
                if (new DateTime(DateTime.Now.Year, 1, 31) < DateTime.Now)
                {
                    calEnero.Enabled = false;
                    this.RequiredFieldValidator15.Enabled = false;
                }

                calFebrero.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
                calFebrero.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);
                if (new DateTime(DateTime.Now.Year, 2, 28) < DateTime.Now)
                {
                    calFebrero.Enabled = false;
                    this.RequiredFieldValidator1.Enabled = false;
                }
                calFebrero.FocusedDate = new DateTime(DateTime.Now.Year, 2, 1);

                calMarzo.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
                calMarzo.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);
                if (new DateTime(DateTime.Now.Year, 3, 31) < DateTime.Now)
                {
                    calMarzo.Enabled = false;
                    this.RequiredFieldValidator2.Enabled = false;
                }
                calMarzo.FocusedDate = new DateTime(DateTime.Now.Year, 3, 1);


                calAbril.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
                calAbril.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);
                if (new DateTime(DateTime.Now.Year, 4, 30) < DateTime.Now)
                {
                    calAbril.Enabled = false;
                    this.RequiredFieldValidator3.Enabled = false;
                }
                calAbril.FocusedDate = new DateTime(DateTime.Now.Year, 4, 1);

                calMayo.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
                calMayo.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);
                if (new DateTime(DateTime.Now.Year, 5, 31) < DateTime.Now) 
                { 
                    calMayo.Enabled = false;
                    this.RequiredFieldValidator4.Enabled = false;
                }
                calMayo.FocusedDate = new DateTime(DateTime.Now.Year, 5, 1);

                calJunio.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
                calJunio.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);
                if (new DateTime(DateTime.Now.Year, 6, 30) < DateTime.Now)
                {
                    calJunio.Enabled = false;
                    this.RequiredFieldValidator5.Enabled = false;
                }
                calJunio.FocusedDate = new DateTime(DateTime.Now.Year, 6, 1);

                calJulio.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
                calJulio.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);
                if (new DateTime(DateTime.Now.Year, 7, 31) < DateTime.Now)
                {
                    calJulio.Enabled = false;
                    this.RequiredFieldValidator6.Enabled = false;
                }
                calJulio.FocusedDate = new DateTime(DateTime.Now.Year, 7, 1);

                calAgosto.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
                calAgosto.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);
                if (new DateTime(DateTime.Now.Year, 8, 31) < DateTime.Now)
                {
                    calAgosto.Enabled = false;
                    this.RequiredFieldValidator7.Enabled = false;
                }
                calAgosto.FocusedDate = new DateTime(DateTime.Now.Year, 8, 1);

                calSeptiembre.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
                calSeptiembre.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);
                if (new DateTime(DateTime.Now.Year, 9, 30) < DateTime.Now)
                {
                    calSeptiembre.Enabled = false;
                    this.RequiredFieldValidator8.Enabled = false;
                }
                calSeptiembre.FocusedDate = new DateTime(DateTime.Now.Year, 9, 1);

                calOctubre.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
                calOctubre.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);
                if (new DateTime(DateTime.Now.Year, 10, 31) < DateTime.Now)
                {
                    calOctubre.Enabled = false;
                    this.RequiredFieldValidator9.Enabled = false;
                }
                calOctubre.FocusedDate = new DateTime(DateTime.Now.Year, 10, 1);

                calNoviembre.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
                calNoviembre.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);
                if (new DateTime(DateTime.Now.Year, 11, 30) < DateTime.Now)
                {
                    calNoviembre.Enabled = false;
                    this.RequiredFieldValidator10.Enabled = false;
                }
                calNoviembre.FocusedDate = new DateTime(DateTime.Now.Year, 11, 1);

                calDiciembre.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
                calDiciembre.MaxDate = new DateTime(DateTime.Now.Year, 12, 31);
                if (new DateTime(DateTime.Now.Year, 12, 31) < DateTime.Now)
                {
                    calDiciembre.Enabled = false;
                    this.RequiredFieldValidator11.Enabled = false;
                }
                calDiciembre.FocusedDate = new DateTime(DateTime.Now.Year, 12, 1);
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Dictionary<int, DateTime> fechas= new Dictionary<int,DateTime>();

            string id_TG=Request.QueryString["Id_TG"];

            DateTime Enero = new DateTime(1999, 1, 1);
            DateTime Febrero = new DateTime(1999, 2, 1);
            DateTime Marzo = new DateTime(1999, 3, 1);
            DateTime Abril = new DateTime(1999, 4, 1);
            DateTime Mayo = new DateTime(1999, 5, 1);
            DateTime Junio = new DateTime(1999, 6, 1);
            DateTime Julio = new DateTime(1999, 7, 1);
            DateTime Agosto = new DateTime(1999, 8, 1);
            DateTime Septiembre = new DateTime(1999, 9, 1);
            DateTime Octubre = new DateTime(1999, 10, 1);
            DateTime Noviembre = new DateTime(1999, 11, 1);
            DateTime Diciembre = new DateTime(1999, 12, 1);


            if (calEnero.SelectedDate != null) { fechas.Add(1, calEnero.SelectedDate.Value); Enero = calEnero.SelectedDate.Value; }
            if (calFebrero.SelectedDate != null) { fechas.Add(2, calFebrero.SelectedDate.Value); Febrero = calFebrero.SelectedDate.Value; }
            if (calMarzo.SelectedDate != null) { fechas.Add(3, calMarzo.SelectedDate.Value); Marzo = calMarzo.SelectedDate.Value; }
            if (calAbril.SelectedDate != null) { fechas.Add(4, calAbril.SelectedDate.Value); Abril = calAbril.SelectedDate.Value; }
            if (calMayo.SelectedDate != null) { fechas.Add(5, calMayo.SelectedDate.Value); Mayo = calMayo.SelectedDate.Value; }
            if (calJunio.SelectedDate != null) { fechas.Add(6, calJunio.SelectedDate.Value); Junio = calJunio.SelectedDate.Value; }

            if (calJulio.SelectedDate != null)      { fechas.Add(7, calJulio.SelectedDate.Value); Julio = calJulio.SelectedDate.Value; }
            if (calAgosto.SelectedDate != null)     { fechas.Add(8, calAgosto.SelectedDate.Value); Agosto = calAgosto.SelectedDate.Value; }
            if (calSeptiembre.SelectedDate != null) { fechas.Add(9, calSeptiembre.SelectedDate.Value); Septiembre = calSeptiembre.SelectedDate.Value; }
            if (calOctubre.SelectedDate != null)    { fechas.Add(10, calOctubre.SelectedDate.Value); Octubre = calOctubre.SelectedDate.Value; }
            if (calNoviembre.SelectedDate != null)  { fechas.Add(11, calNoviembre.SelectedDate.Value); Noviembre = calNoviembre.SelectedDate.Value; }
            if (calDiciembre.SelectedDate != null)  { fechas.Add(12, calDiciembre.SelectedDate.Value); Diciembre = calDiciembre.SelectedDate.Value; }


            if (!(Enero < Febrero &&
                Febrero < Marzo &&
                Marzo < Abril &&
                Abril < Mayo &&
                Mayo < Junio &&
                Junio < Julio &&
                Julio < Agosto &&
                Agosto < Septiembre &&
                Septiembre < Octubre &&
                Octubre < Noviembre &&
                Noviembre < Diciembre)
                )
            {


                string funcion2 = "ValidacionFechas()";
                string script2 = "<script>" + funcion2 + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion2, script2, false);

                return;
            }
            Session["Fechas_" + id_TG] = fechas;
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);

            string funcion = "CloseWindow()";
            string script = "<script>" + funcion + "</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
        }


     
    }

}