using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CapaEntidad;
using Telerik.Web.UI;

using CapaNegocios;
using System.Data;
using System.IO;
using System.Text;
using System.Collections;

namespace SIANWEB
{
    public partial class wfrmInformes : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["session" + Session.SessionID] = value;

            }
        }
        #endregion
        #region Eventos
        //protected override void OnLoad(EventArgs e)
        //{

        //    if (!ValidarSesion())
        //    {
        //        Response.Redirect("login.aspx", false);
        //    }
        //    else if (Page.IsPostBack)
        //    {
        //        //NO HACE NADA
        //    }
        //    else if (Session["refreshPage" + Session.SessionID] == null || (bool)Session["refreshPage" + Session.SessionID])
        //    {
        //        string Url = "";
        //        string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
        //        if (pag.Length > 1)
        //            Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
        //        else
        //            Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
        //        Session["refreshPage" + Session.SessionID] = false;
        //        Response.Redirect(Url);
        //    }
        //    else
        //    {
        //        Session["refreshPage" + Session.SessionID] = true;
        //        Page_Load(null, null);
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ValidarSesion())
                {
                    ValidarPermisos();
                    if (!Page.IsPostBack)
                        RadAjaxManager1.ResponseScripts.Add("refreshGrid();");
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat("Page_Load_error", ex.Message));
            }
        }
        protected void ibtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (!_PermisoImprimir)
                {
                    Alerta("No tiene permiso para imprimir");
                    return;
                }

                if (!radControl.Checked && !radDII.Checked && !radControlAplicacion.Checked && !radDIINumero.Checked && !radControlEntrada.Checked && !radCampania.Checked)
                {
                    Alerta(string.Concat("Seleccione una opción de control"));
                    return;
                }
                string periodo = !string.IsNullOrEmpty(ddPeriodo.SelectedValue) ? Convert.ToString(ddPeriodo.SelectedValue) : "-1*-1";

                if (periodo != "-1*-1")
                {
                    //switch (HiddenField1.Value)
                    //{
                    //    case "1": 
                    this.ExportarControlPromocion();
                    //        break;
                    //    case "2":
                    //        this.ExportarControlPromocion_LimpiezaAplicacion();
                    //        break;
                    //    case "3":
                    //        this.ExportarDII();
                    //        break;
                    //    case "4":
                    //        this.ExportarDIINumero();
                    //        break;

                    //}
                    //if (this.radControl.Checked)

                    //if (this.radDII.Checked)

                    //if (radControlAplicacion.Checked)

                    //if (this.radDIINumero.Checked)


                }
                else
                    Alerta(string.Concat("Seleccione un periodo"));
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void ddlZonas_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                this.CargarRepresentantes();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void ddlRepresentantes_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                //if (Convert.ToInt32(this.ddlRepresentantes.SelectedValue) == -1)
                //    this.CargarUENSegmentosTerritoriosSucursal();
                //else
                //    this.CargarUENS();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        #region ddls escondidos
        //protected void ddlUENs_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    try
        //    {
        //        this.CargarSegmentos();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.DisplayMensajeAlerta(ex.Message);
        //    }
        //}

        //protected void ddlSegmentos_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    try
        //    {
        //        this.CargarTerritorios();
        //        this.CargarAreasdeSegmento();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.DisplayMensajeAlerta(ex.Message);
        //    }
        //}

        //protected void ddlArea_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    try
        //    {
        //        this.CargarSolucionesdeArea();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.DisplayMensajeAlerta(ex.Message);
        //    }
        //}

        //protected void ddlSolucion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    try
        //    {
        //        this.CargarAplicacionesSolucion();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.DisplayMensajeAlerta(ex.Message);
        //    }
        //}
        #endregion
        #region Rads
        protected void radControl_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radControl.Checked)
                {
                    ibtnExcelSolucion.Visible = true;
                    ibtnExcelAplicacion.Visible = false;
                    ibtnExcelImporte.Visible = false;
                    ibtnExcelNumero.Visible = false;
                    pnlRepresentante.Visible = true;
                    ibtnExcelControlEntrada.Visible = false;
                    ibtnExcelCampania.Visible = false;
                    chkProyectoNuevo.Visible = false;
                    lNuevo.Visible = false;
                    IbtnExcelCierreMes.Visible = false;
                    chkProyectoNuevo.Checked = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void radCierreMes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radCierreMes.Checked)
                {
                    ibtnExcelSolucion.Visible = false;
                    ibtnExcelAplicacion.Visible = false;
                    ibtnExcelImporte.Visible = false;
                    ibtnExcelNumero.Visible = false;
                    pnlRepresentante.Visible = true;
                    ibtnExcelControlEntrada.Visible = false;
                    ibtnExcelCampania.Visible = false;
                    chkProyectoNuevo.Visible = false;
                    lNuevo.Visible = false;
                    IbtnExcelCierreMes.Visible = true;
                    chkProyectoNuevo.Checked = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void radControlAplicacion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radControlAplicacion.Checked)
                {
                    ibtnExcelSolucion.Visible = false;
                    ibtnExcelAplicacion.Visible = true;
                    ibtnExcelImporte.Visible = false;
                    ibtnExcelNumero.Visible = false;
                    ibtnExcelCampania.Visible = false;
                    ibtnExcelControlEntrada.Visible = false;
                    pnlRepresentante.Visible = true;
                    chkProyectoNuevo.Visible = true;
                    lNuevo.Visible = true;
                    IbtnExcelCierreMes.Visible = false;
                    chkProyectoNuevo.Checked = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void radDII_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radDII.Checked)
                {
                    ibtnExcelSolucion.Visible = false;
                    ibtnExcelAplicacion.Visible = false;
                    ibtnExcelImporte.Visible = false;
                    ibtnExcelNumero.Visible = true;
                    pnlRepresentante.Visible = false;
                    ibtnExcelCampania.Visible = false;
                    ibtnExcelControlEntrada.Visible = false;
                    chkProyectoNuevo.Visible = false;
                    lNuevo.Visible = false;
                    IbtnExcelCierreMes.Visible = false;
                    chkProyectoNuevo.Checked = false;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void radControlEntrada_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radControlEntrada.Checked)
                {
                    ibtnExcelSolucion.Visible = false;
                    ibtnExcelAplicacion.Visible = false;
                    ibtnExcelImporte.Visible = false;
                    ibtnExcelNumero.Visible = false;
                    ibtnExcelControlEntrada.Visible = true;
                    pnlRepresentante.Visible = false;
                    ibtnExcelCampania.Visible = false;
                    chkProyectoNuevo.Visible = false;
                    lNuevo.Visible = false;
                    IbtnExcelCierreMes.Visible = false;
                    chkProyectoNuevo.Checked = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void radDIINumero_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radDIINumero.Checked)
                {
                    ibtnExcelSolucion.Visible = false;
                    ibtnExcelAplicacion.Visible = false;
                    ibtnExcelImporte.Visible = true;
                    ibtnExcelNumero.Visible = false;
                    pnlRepresentante.Visible = false;
                    ibtnExcelCampania.Visible = false;
                    ibtnExcelControlEntrada.Visible = false;
                    chkProyectoNuevo.Visible = false;
                    lNuevo.Visible = false;
                    IbtnExcelCierreMes.Visible = false;
                    chkProyectoNuevo.Checked = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void radCampania_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radCampania.Checked)
                {
                    ibtnExcelSolucion.Visible = false;
                    ibtnExcelAplicacion.Visible = false;
                    ibtnExcelImporte.Visible = false;
                    ibtnExcelNumero.Visible = false;
                    pnlRepresentante.Visible = false;
                    ibtnExcelControlEntrada.Visible = false;
                    ibtnExcelCampania.Visible = true;
                    chkProyectoNuevo.Visible = false;
                    lNuevo.Visible = false;
                    IbtnExcelCierreMes.Visible = false;
                    chkProyectoNuevo.Checked = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion
        #region Funciones
        private bool ValidarSesion()
        {
            try
            {
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Inicializar()
        {
            try
            {
                this.radCierreMes.Style.Add("display", "none");
                this.radControl.Style.Add("display", "none");
                if (session.Id_TU == 2)
                {
                    this.radDII.Style.Add("display", "none");
                    this.radDIINumero.Style.Add("display", "none");
                    this.radCampania.Style.Add("display", "none");
                    this.radControl.Style.Add("display", "none");
                }
                this.CargarZonas();
                this.CargarRepresentantes();
                this.CargarPeriodos();
                //if (_PermisoImprimir)
                //    this.ibtnExcel.Visible = true;
                //else
                //    this.ibtnExcel.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Exportacion a Documento
        public void ExportarControlPromocion()
        {

            string ruta = null;
            string str = "";
            string tipo = "";
            //if (radControl.Checked)
            //{
            tipo = "Sol";
            //}
            //else if (radControlAplicacion.Checked)
            //{
            //    tipo = "Apl";
            //}
            //else if (radDII.Checked)
            //{
            //tipo = "num";
            //}
            //else
            //{
            //tipo = "imp";
            //}
            int intCancela = 0;
            int vintConsulta = 0;
            double vMonto1 = 0;
            double vMonto2 = 0;
            double totTeorico = 0;
            double totVenta = 0;
            string vRepID = null;
            int vZonaID = 0;
            if (this.session.Id_Cd_Ver != 0)
                vZonaID = this.session.Id_Cd_Ver;
            else
                vZonaID = Convert.ToInt32(this.ddlZonas.SelectedValue);

            if (this.ddlRepresentantesComercial.SelectedValue == string.Empty || Convert.ToInt32(this.ddlRepresentantesComercial.SelectedValue) == -1)
                vRepID = null;
            else
                vRepID = this.ddlRepresentantesComercial.SelectedValue;

            if (!string.IsNullOrEmpty(this.txtDe.Text.Trim()) & !string.IsNullOrEmpty(this.txtA.Text.Trim()))
            {
                vintConsulta = 1;
                vMonto1 = Convert.ToDouble(this.txtDe.Text.Trim());
                vMonto2 = Convert.ToDouble(this.txtA.Text.Trim());
            }
            else
            {
                vintConsulta = 0;
                vMonto1 = 0;
                vMonto2 = 0;
            }
            int periodo = !string.IsNullOrEmpty(ddPeriodo.SelectedValue) ? Convert.ToInt32(ddPeriodo.SelectedValue) : -1;

            String NombreArchivo = "Reporte" + this.session.Id_U.ToString() + tipo + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".xls";


            ruta = Server.MapPath("Reportes") + "\\Reporte" + this.session.Id_U.ToString() + tipo + ".txt";
            if ((string.IsNullOrEmpty(this.txtA.Text.Trim()) && !string.IsNullOrEmpty(this.txtDe.Text.Trim())) | (!string.IsNullOrEmpty(this.txtA.Text.Trim()) && string.IsNullOrEmpty(this.txtDe.Text.Trim())))
                this.DisplayMensajeAlerta("El rango introducido es incorrecto, falta un valor");
            else
            {
                if (vMonto1 > vMonto2)
                    this.DisplayMensajeAlerta("El rango inicial no debe ser mayor al rango final");
                else
                {
                    if (File.Exists(ruta))
                        File.Delete(ruta);
                    if (File.Exists(Server.MapPath("Reportes") + "\\" + NombreArchivo))
                        File.Delete(Server.MapPath("Reportes") + "\\" + NombreArchivo);

                    StreamWriter sw = null;
                    DataSet dsReporte = new DataSet();

                    new CN_CrmInformes().GenerarControlPromocion(this.session.Id_Emp,
                        Convert.ToInt32(ddlZonas.SelectedValue) == -1 ? session.Id_Cd_Ver : Convert.ToInt32(ddlZonas.SelectedValue),
                        session.Id_U,
                        ddlRepresentantesComercial.SelectedValue, periodo, vintConsulta, this.txtDe.Text.Trim(), this.txtA.Text.Trim(), ref dsReporte, this.session.Emp_Cnx);

                    if ((dsReporte != null))
                    {
                        if (!(dsReporte.Tables[0].Rows.Count == 0))
                        {
                            try
                            {
                                sw = new StreamWriter(ruta, false, Encoding.UTF8);
                                //Obteniendo totales por etapa
                                double vA = 0;
                                double vP = 0;
                                double vN = 0;
                                double vC = 0;
                                double vCa = 0;
                                for (int m = 0; m <= dsReporte.Tables[0].Rows.Count - 1; m++)
                                {
                                    DataRow dr = null;
                                    dr = dsReporte.Tables[0].Rows[m];
                                    switch (dr["Estatus"].ToString().ToLower())
                                    {
                                        case "cancelados":
                                            vCa += Convert.ToDouble(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "análisis":
                                            vA += Convert.ToDouble(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "promoción":
                                            vP += Convert.ToDouble(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "negociación":
                                            vN += Convert.ToDouble(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "cerrados":
                                            vC += Convert.ToDouble(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                    }
                                }
                                //foreach (DataRow dr in dsReporte.Tables[0].Rows)
                                //{
                                //    if (dr["VTeorico"] != System.DBNull.Value)
                                //    {
                                //        dr["VTeorico"] = Convert.ToDouble(dr["VTeorico"]).ToString("C");
                                //    }
                                //    dr["MontoProyecto"] = Convert.ToDouble(dr["MontoProyecto"]).ToString("C");

                                //}

                                ///''''''''''''''''''''''''''
                                //Agregando fila para totales
                                int entero = dsReporte.Tables[0].Rows.Count;
                                DataRow fila = null;
                                fila = dsReporte.Tables[0].NewRow();
                                dsReporte.Tables[0].Rows.InsertAt(fila, entero + 1);
                                ///''''''''''''''''''''''''''
                                for (int i = 0; i <= dsReporte.Tables[0].Rows.Count - 1; i++)
                                {
                                    if (i == 0)
                                    {
                                        sw.WriteLine("<html>");
                                        sw.WriteLine("<head>");
                                        sw.WriteLine("</head>");
                                        sw.WriteLine("<body>");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<strong>MODULO CRM - CONTROL DE LA PROMOCION NIVEL SOLUCIÓN</strong>");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        if (this.session.Id_TU == 2)
                                            sw.WriteLine("Representante: " + this.session.U_Nombre);
                                        else
                                        {
                                            sw.WriteLine("Representante: "
                                                + ((this.ddlRepresentantesComercial.SelectedValue == "-1" || this.ddlRepresentantesComercial.SelectedValue == string.Empty) ? "Todos" : this.ddlRepresentantesComercial.SelectedItem.Text));
                                        }
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("Fecha: " + DateTime.Now.Date.ToShortDateString());
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("Periodo: " + ddPeriodo.SelectedItem.Text.Replace("--", "").Trim());
                                        sw.WriteLine("<br />");
                                        if (session.Id_TU == 2 || session.Id_TU == 4 || session.Id_TU == 5)//representante// if (this.session.Id_TU == 5 | this.session.Id_TU == 6)
                                            sw.WriteLine("CDS: "
                                                + ((this.ddlZonas.SelectedValue == "-1" || this.ddlZonas.SelectedValue == string.Empty) ? "Todos" : this.ddlZonas.SelectedItem.Text));
                                        else
                                            sw.WriteLine("CDS:" + session.Cd_Nombre);
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<table border=1><font size=8pt>");
                                        sw.WriteLine("<tr>");
                                        for (int k = 0; k <= dsReporte.Tables[0].Columns.Count - 2; k++)
                                        {//titulos del reporte
                                            sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>");
                                            sw.WriteLine(dsReporte.Tables[0].Columns[k].Caption);
                                            sw.WriteLine("</td>");
                                        }
                                        sw.WriteLine("</tr>");
                                    }
                                    sw.WriteLine("<tr>");
                                    intCancela = 0;
                                    for (int j = 0; j <= dsReporte.Tables[0].Columns.Count - 2; j++)
                                    {

                                        if (dsReporte.Tables[0].Rows[i]["Estatus"].ToString().ToLower() == "cerrados")
                                        {
                                            sw.WriteLine("<td>");
                                            intCancela = 0;
                                        }
                                        else if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i]["Analisis"], System.DBNull.Value)))
                                        {

                                            if (Math.Abs(((TimeSpan)DateTime.Now.Subtract(Convert.ToDateTime(dsReporte.Tables[0].Rows[i]["Analisis"]))).Days) > 60)
                                            {
                                                sw.WriteLine("<td> <font color=\"#FF0000\">");
                                                intCancela = 1;
                                            }
                                            else if (Math.Abs(((TimeSpan)DateTime.Now.Subtract(Convert.ToDateTime(dsReporte.Tables[0].Rows[i]["Analisis"]))).Days) > 90)
                                            {
                                                sw.WriteLine("<td> <font color=\"#FF0000\">");
                                                intCancela = 1;
                                            }
                                            else
                                            {
                                                sw.WriteLine("<td>");
                                                intCancela = 0;
                                            }
                                        }
                                        else
                                        {
                                            sw.WriteLine("<td>");
                                            intCancela = 0;
                                        }

                                        sw.WriteLine(dsReporte.Tables[0].Rows[i][j]);
                                        if (j == 6)
                                        {
                                            if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i][j], System.DBNull.Value)))
                                                totTeorico += Convert.ToDouble(dsReporte.Tables[0].Rows[i][j].ToString().Replace("$", "").Replace(",", "").Trim());
                                            else
                                                if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                    sw.WriteLine("<strong>" + totTeorico.ToString("C") + "</strong>");
                                        }
                                        else if (j == 7)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vA.ToString("C") + "</strong>");
                                        }
                                        else if (j == 8)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vP.ToString("C") + "</strong>");
                                        }
                                        else if (j == 9)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vN.ToString("C") + "</strong>");
                                        }
                                        else if (j == 10)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vC.ToString("C") + "</strong>");
                                        }
                                        else if (j == 11)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vCa.ToString("C") + "</strong>");
                                        }
                                        else if (j == 12)
                                        {
                                            if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i][j], System.DBNull.Value)))
                                                totVenta += Convert.ToDouble(dsReporte.Tables[0].Rows[i][j].ToString().Replace("$", "").Replace(",", "").Trim());
                                            else
                                            {
                                                if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                    sw.WriteLine("<strong>" + totVenta.ToString("C") + "</strong>");
                                            }
                                        }
                                        if (intCancela == 1)
                                            sw.WriteLine("</font></td>");
                                        else
                                            sw.WriteLine("</td>");
                                    }
                                    sw.WriteLine("</tr>");
                                }
                                sw.WriteLine("</font></table>");
                                sw.WriteLine("</body>");
                                sw.WriteLine("</html>");
                                sw.Close();

                                if (File.Exists(ruta))
                                {
                                    string ruta2 = null;
                                    ruta2 = Server.MapPath("Reportes") + "\\" + NombreArchivo;
                                    File.Move(ruta, Server.MapPath("Reportes") + "\\" + NombreArchivo);
                                    Response.Redirect("Reportes\\" + NombreArchivo);
                                }
                            }
                            catch (Exception Ex)
                            {
                                str = Ex.Message;
                                sw.Close();
                            }
                        }
                        else
                            Alerta("No se encontraron proyectos de promoción");
                    }
                    else
                        Alerta("No se cargó la información");
                }
            }

        }

        //private void ExportarControlPromocion_Limpieza()
        //{
        //    string ruta = string.Empty;
        //    string str = string.Empty;
        //    int intCancela = 0, vintConsulta = 0;
        //    double vMonto1 = 0, vMonto2 = 0;
        //    double totTeorico = 0, totVenta = 0;
        //    string vRepID = string.Empty;
        //    int vZonaID = 0;

        //    StreamWriter sw = null;
        //    DataSet dsReporte = new DataSet();

        //    if (this.session.Id_Cd_Ver != 0)
        //        vZonaID = this.session.Id_Cd_Ver;
        //    else
        //        vZonaID = Convert.ToInt32(this.ddlZonas.SelectedValue);
        //    //if (this.session.Id_TU == 5 || this.session.Id_TU == 6 || this.session.Id_TU == 7)
        //    //{
        //        if (this.ddlRepresentantesComercial.SelectedValue == string.Empty || Convert.ToInt32(this.ddlRepresentantesComercial.SelectedValue) == -1)
        //            vRepID = "%";
        //        else
        //            vRepID = this.ddlRepresentantesComercial.SelectedValue;
        //    //}
        //    //else
        //    //{
        //    //    if (this.ddlRepresentantes.SelectedValue == string.Empty || Convert.ToInt32(this.ddlRepresentantes.SelectedValue) == -1)
        //    //        vRepID = "%";
        //    //    else
        //    //        vRepID = this.ddlRepresentantes.SelectedValue;
        //    //}

        //    //if (this.txtDe.Text.Trim() != string.Empty && this.txtA.Text.Trim() != string.Empty)
        //    //{
        //    //    vintConsulta = 1;
        //    //    vMonto1 = Convert.ToDouble(this.txtDe.Text.Trim());
        //    //    vMonto2 = Convert.ToDouble(this.txtA.Text.Trim());
        //    //}
        //    //else
        //    //{
        //    vintConsulta = 0;
        //    vMonto1 = 0;
        //    vMonto2 = 0;
        //    //}

        //    ruta = string.Concat(Server.MapPath("Reportes"), "\\Reporte", this.session.Id_U, ".txt");
        //    //if ((this.txtA.Text.Trim() == string.Empty && this.txtDe.Text.Trim() != string.Empty) || (this.txtA.Text.Trim() != string.Empty && this.txtDe.Text.Trim() == string.Empty))
        //    //{
        //    //    string popupScript = "<script>window.addEvent('domready', function() {Sexy = new SexyAlertBox(); Sexy.error('<h1>En caso de que requiera filtrar la búsqueda por Monto de proyecto, especifique correctamente el rango.</h1>');});</script>";
        //    //    RadAjaxManager1.ResponseScripts.Add(popupScript);
        //    //    //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "bloque1", popupScript);
        //    //}
        //    //else
        //    //{
        //    if (File.Exists(ruta))
        //        File.Delete(ruta);

        //    if (File.Exists(string.Concat(Server.MapPath("Reportes"), "\\Reporte", this.session.Id_U, ".xls")))
        //        File.Delete(string.Concat(Server.MapPath("Reportes"), "\\Reporte", this.session.Id_U, ".xls"));

        //    if (this.session.Id_TU == 2)
        //    {
        //        new CN_CrmInformes().GenerarControlPromocion_Limpieza(
        //            this.session.Id_Emp
        //            , this.session.Id_Cd_Ver
        //            , this.session.Id_U.ToString()
        //            , vintConsulta
        //            , vMonto1
        //            , vMonto2
        //            , ref dsReporte
        //            , this.session.Emp_Cnx);
        //    }
        //    else
        //    {
        //        vintConsulta = 2;
        //        new CN_CrmInformes().GenerarControlPromocion_Limpieza(
        //            this.session.Id_Emp
        //            , vZonaID
        //            , vRepID
        //            , vintConsulta
        //            , vMonto1
        //            , vMonto2
        //            , ref dsReporte
        //            , this.session.Emp_Cnx);
        //    }
        //    if ((dsReporte != null))
        //    {
        //        if (!(dsReporte.Tables[0].Rows.Count == 0))
        //        {
        //            try
        //            {
        //                sw = new System.IO.StreamWriter(ruta);
        //                //Obteniendo totales por etapa
        //                decimal vA = default(decimal);
        //                decimal vP = default(decimal);
        //                decimal vN = default(decimal);
        //                decimal vC = default(decimal);
        //                decimal vCa = default(decimal);
        //                for (int m = 0; m <= dsReporte.Tables[0].Rows.Count - 1; m++)
        //                {
        //                    DataRow dr = null;
        //                    dr = dsReporte.Tables[0].Rows[m];
        //                    switch (Convert.ToInt32(dr["Estatus"]))
        //                    {
        //                        case 0:
        //                            vCa += Convert.ToDecimal(dr["MontoProyecto"]);
        //                            break;
        //                        case 1:
        //                            vA += Convert.ToDecimal(dr["MontoProyecto"]);
        //                            break;
        //                        case 2:
        //                            vP += Convert.ToDecimal(dr["MontoProyecto"]);
        //                            break;
        //                        case 3:
        //                            vN += Convert.ToDecimal(dr["MontoProyecto"]);
        //                            break;
        //                        case 4:
        //                            vC += Convert.ToDecimal(dr["MontoProyecto"]);
        //                            break;
        //                    }
        //                }

        //                ///''''''''''''''''''''''''''
        //                //Agregando fila para totales
        //                int @int = dsReporte.Tables[0].Rows.Count;
        //                DataRow fila = null;
        //                fila = dsReporte.Tables[0].NewRow();
        //                dsReporte.Tables[0].Rows.InsertAt(fila, @int + 1);
        //                ///''''''''''''''''''''''''''
        //                for (int i = 0; i <= dsReporte.Tables[0].Rows.Count - 1; i++)
        //                {
        //                    if (i == 0)
        //                    {
        //                        sw.WriteLine("<html>");
        //                        sw.WriteLine("<head>");
        //                        sw.WriteLine("</head>");
        //                        sw.WriteLine("<body>");
        //                        sw.WriteLine("<br />");
        //                        sw.WriteLine("<br />");
        //                        sw.WriteLine("<strong>MODULO CRM - CONTROL DE LA PROMOCION</strong>");
        //                        sw.WriteLine("<br />");
        //                        sw.WriteLine("<br />");
        //                        if (this.session.Id_TU == 2)
        //                            sw.WriteLine(string.Concat("Representante: ", this.session.U_Nombre));
        //                        else
        //                        {
        //                            if (this.session.Id_TU == 5 || this.session.Id_TU == 6 || this.session.Id_TU == 7)
        //                                sw.WriteLine(string.Concat("Representante: ",
        //                                    ((this.ddlRepresentantesComercial.SelectedValue == "-1" || this.ddlRepresentantesComercial.SelectedValue == string.Empty) ? "Todos" : this.ddlRepresentantesComercial.SelectedItem.Text)));
        //                            else
        //                                sw.WriteLine(string.Concat("Representante: ",
        //                                    ((this.ddlRepresentantes.SelectedValue == "-1" || this.ddlRepresentantes.SelectedValue == string.Empty) ? "Todos" : this.ddlRepresentantes.SelectedItem.Text)));
        //                        }
        //                        sw.WriteLine("<br />");
        //                        sw.WriteLine(string.Concat("Fecha: ", DateTime.Now.Date.ToShortDateString()));
        //                        sw.WriteLine("<br />");

        //                        if (this.session.Id_TU == 5 || this.session.Id_TU == 6)
        //                            sw.WriteLine(string.Concat("CDS: ",
        //                                ((this.ddlZonas.SelectedValue == "-1" || this.ddlZonas.SelectedValue == string.Empty) ? "Todos" : this.ddlZonas.SelectedItem.Text)));
        //                        else
        //                            sw.WriteLine("CDS: Monterrey");

        //                        sw.WriteLine("<br />");
        //                        sw.WriteLine("<br />");
        //                        sw.WriteLine("<table border=1><font size=8pt>");
        //                        sw.WriteLine("<tr>");
        //                        for (int k = 0; k <= dsReporte.Tables[0].Columns.Count - 2; k++)
        //                        {
        //                            sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>");
        //                            sw.WriteLine(dsReporte.Tables[0].Columns[k].Caption);
        //                            sw.WriteLine("</td>");
        //                        }
        //                        sw.WriteLine("</tr>");
        //                    }
        //                    sw.WriteLine("<tr>");
        //                    intCancela = 0;
        //                    for (int j = 0; j <= dsReporte.Tables[0].Columns.Count - 2; j++)
        //                    {
        //                        if (!Convert.IsDBNull(dsReporte.Tables[0].Rows[i]["FechaModificacion"]))
        //                        {
        //                            if (Math.Abs(((TimeSpan)DateTime.Now.Subtract(Convert.ToDateTime(dsReporte.Tables[0].Rows[i]["FechaModificacion"]))).Days) >= 30)
        //                            {
        //                                sw.WriteLine("<td> <font color=\"#FF0000\">");
        //                                intCancela = 1;
        //                            }
        //                            else
        //                            {
        //                                sw.WriteLine("<td>");
        //                                intCancela = 0;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            sw.WriteLine("<td>");
        //                            intCancela = 0;
        //                        }
        //                        sw.WriteLine(dsReporte.Tables[0].Rows[i][j]);

        //                        if (j == 6)
        //                        {
        //                            if (!Convert.IsDBNull(dsReporte.Tables[0].Rows[i][j]))
        //                                totTeorico += Convert.ToDouble(dsReporte.Tables[0].Rows[i][j]);
        //                            else
        //                            {
        //                                if (i == dsReporte.Tables[0].Rows.Count - 1)
        //                                    sw.WriteLine(string.Concat("<strong>", totTeorico.ToString("C"), "</strong>"));
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (j == 7)
        //                            {
        //                                if (i == dsReporte.Tables[0].Rows.Count - 1)
        //                                    sw.WriteLine(string.Concat("<strong>", vA.ToString("C"), "</strong>"));
        //                            }
        //                            else
        //                            {
        //                                if (j == 8)
        //                                {
        //                                    if (i == dsReporte.Tables[0].Rows.Count - 1)
        //                                        sw.WriteLine(string.Concat("<strong>", vP.ToString("C"), "</strong>"));
        //                                }
        //                                else
        //                                {
        //                                    if (j == 9)
        //                                    {
        //                                        if (i == dsReporte.Tables[0].Rows.Count - 1)
        //                                            sw.WriteLine(string.Concat("<strong>", vN.ToString("C"), "</strong>"));
        //                                    }
        //                                    else
        //                                    {
        //                                        if (j == 10)
        //                                        {
        //                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
        //                                                sw.WriteLine(string.Concat("<strong>", vC.ToString("C"), "</strong>"));
        //                                        }
        //                                        else
        //                                        {
        //                                            if (j == 11)
        //                                            {
        //                                                if (i == dsReporte.Tables[0].Rows.Count - 1)
        //                                                    sw.WriteLine(string.Concat("<strong>", vCa.ToString("C"), "</strong>"));
        //                                            }
        //                                            else
        //                                            {
        //                                                if (j == 12)
        //                                                {
        //                                                    if (!Convert.IsDBNull(dsReporte.Tables[0].Rows[i][j]))
        //                                                        totVenta += Convert.ToDouble(dsReporte.Tables[0].Rows[i][j]);
        //                                                    else
        //                                                    {
        //                                                        if (i == dsReporte.Tables[0].Rows.Count - 1)
        //                                                            sw.WriteLine(string.Concat("<strong>", totVenta.ToString("C"), "</strong>"));
        //                                                    }
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }

        //                        if (intCancela == 1)
        //                            sw.WriteLine("</font></td>");
        //                        else
        //                            sw.WriteLine("</td>");
        //                    }
        //                    sw.WriteLine("</tr>");
        //                }
        //                sw.WriteLine("</font></table>");
        //                sw.WriteLine("</body>");
        //                sw.WriteLine("</html>");
        //                sw.Close();

        //                if (File.Exists(ruta))
        //                {
        //                    string ruta2 = string.Concat(Server.MapPath("Reportes"), "\\Reporte", this.session.Id_U.ToString(), ".xls");
        //                    File.Move(ruta, ruta2);
        //                    Response.Redirect(string.Concat("Reportes\\Reporte", this.session.Id_U, ".xls"));
        //                }
        //            }
        //            catch (Exception Ex)
        //            {
        //                str = Ex.Message;
        //                sw.Close();
        //            }
        //        }
        //        else
        //            str = "No se encontraron proyectos de promoción";
        //    }
        //    else
        //        str = "No se cargó la información";
        //    this.lblMensajes.Text = str;
        //}


        private void ExportarControlEntrada()
        {
            Session["dtRepresentantes"] = null;
            DataSet dsCE = new DataSet();
            string tipo = "";
            tipo = "ControlEntrada";
            System.IO.StreamWriter sw = null;
            string ruta = null;
            
            ruta = Server.MapPath("Reportes") + "\\Reporte" + this.session.Id_U.ToString() + tipo + ".txt";

            String NombreArchivo = "Reporte" + this.session.Id_U.ToString() + tipo + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".xls";

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\" + NombreArchivo))
                File.Delete(Server.MapPath("Reportes") + "\\" + NombreArchivo);
            sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            string RepID = null;
            string ZonaID = null;
            string Representante = null;
            if (this.ddlRepresentantesComercial.SelectedValue == string.Empty || Convert.ToInt32(this.ddlRepresentantesComercial.SelectedValue) == -1)
            {
                RepID = "%";
                Representante = "Todos";
            }
            else
            {
                RepID = this.ddlRepresentantesComercial.SelectedValue;
                Representante = this.ddlRepresentantesComercial.Text;
            }
            if (Convert.ToInt32(this.ddlZonas.SelectedValue) == -1)
                ZonaID = "%";
            else
                ZonaID = this.ddlZonas.SelectedValue;

            int periodo = !string.IsNullOrEmpty(ddPeriodo.SelectedValue) ? Convert.ToInt32(ddPeriodo.SelectedValue) : -1;

            new CN_CrmInformes().spCRM_ControlEntrada(this.session.Id_Emp, Convert.ToInt32(ddlZonas.SelectedValue) == -1 ? session.Id_Cd_Ver : Convert.ToInt32(ddlZonas.SelectedValue), periodo, RepID, ref dsCE, this.session.Emp_Cnx);

            if ((dsCE != null))
            {
                if (!(dsCE.Tables[0].Rows.Count == 0))
                {
                    DataRow dr = null;
                    DataTable dt = new DataTable();                    
                    for (int i = 0; i <= dsCE.Tables[0].Rows.Count - 1; i++)
                    {
                        dr = dsCE.Tables[0].Rows[i];
                        if ((Session["dtRepresentantes"] != null))
                            dt = (DataTable)Session["dtRepresentantes"];
                        else
                        {
                            this.CrearColumnasControlEntrada();
                        }

                        this.CrearFilasControlEntrada(dr["Zona"].ToString(), dr["UsuarioID"].ToString(), dr["Representante"].ToString(), dr["Fecha"].ToString());
                                    
                        
                    }
                        
                   
                    ///'''''''''ENCABEZADOS DEL REPORTE'''''''''''''''''
                    sw.WriteLine("<html>");
                    sw.WriteLine("<head>");
                    sw.WriteLine("</head>");
                    sw.WriteLine("<body>");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<strong>MODULO CRM - CONTROL DE ENTRADAS </strong>");
                    sw.WriteLine("<br />");
                    sw.WriteLine("Representante: " + Representante+ "");
                    sw.WriteLine("<br />");
                    sw.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + "");
                    sw.WriteLine("<br />");
                    
                    sw.WriteLine("<br />");
                    sw.WriteLine("<table border=1><font size=8pt>");
                    sw.WriteLine("<tr><td bgcolor=\"#DDDDDD\"><b>CDS</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>No. Rik</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Representante</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Fecha de Entrada</td></tr>");
                    ///'''''''''TERMINAN ENCABEZADOS DEL REPORTE'''''''''''''''''
                    if ((Session["dtRepresentantes"] != null))
                    {
                        dt = (DataTable)Session["dtRepresentantes"];
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {  
                            //Formando texto de la tabla
                            sw.WriteLine("<tr>");
                            sw.WriteLine("<td>" + dt.Rows[i]["CDS"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["UsuarioID"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Representante"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["FechaEntrada"] + "</td>");                            
                            sw.WriteLine("</tr>");
                        }
                        sw.WriteLine("<tr>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>"); 
                        sw.WriteLine("</tr>");
                        sw.WriteLine("</font></table>");
                        sw.WriteLine("</body>");
                        sw.WriteLine("</html>");
                        sw.Close();
                        if (File.Exists(ruta))
                        {
                            string ruta2 = null;
                            ruta2 = Server.MapPath("Reportes") + "\\" + NombreArchivo;
                            File.Move(ruta, Server.MapPath("Reportes") + "\\" + NombreArchivo);
                            Response.Redirect("Reportes\\" + NombreArchivo, false);
                        }
                    }
                   
                }
                else
                {
                    Alerta("No se encontraron Entradas");
                }
            }
        }



        private void ExportarCampania()
        {
            Session["dtRepresentantes"] = null;
            DataSet dsCE = new DataSet();
            string tipo = "";
            tipo = "Campana";
            System.IO.StreamWriter sw = null;
            string ruta = null;

            ruta = Server.MapPath("Reportes") + "\\Reporte" + this.session.Id_U.ToString() + tipo + ".txt";
            String NombreArchivo = "Reporte" + this.session.Id_U.ToString() + tipo + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".xls";


            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\" + NombreArchivo))
                File.Delete(Server.MapPath("Reportes") + "\\" + NombreArchivo);
            sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            string RepID = null;
            string ZonaID = null;
            string Representante = null;
            if (this.ddlRepresentantesComercial.SelectedValue == string.Empty || Convert.ToInt32(this.ddlRepresentantesComercial.SelectedValue) == -1)
            {
                RepID = "%";
                Representante = "Todos";
            }
            else
            {
                RepID = this.ddlRepresentantesComercial.SelectedValue;
                Representante = this.ddlRepresentantesComercial.Text;
            }
            if (Convert.ToInt32(this.ddlZonas.SelectedValue) == -1)
                ZonaID = "%";
            else
                ZonaID = this.ddlZonas.SelectedValue;

            int periodo = !string.IsNullOrEmpty(ddPeriodo.SelectedValue) ? Convert.ToInt32(ddPeriodo.SelectedValue) : -1;

            new CN_CrmInformes().spCRM_Campana(this.session.Id_Emp, Convert.ToInt32(ddlZonas.SelectedValue) == -1 ? session.Id_Cd_Ver : Convert.ToInt32(ddlZonas.SelectedValue), periodo, RepID, ref dsCE, this.session.Emp_Cnx);

            if ((dsCE != null))
            {
                if (!(dsCE.Tables[0].Rows.Count == 0))
                {
                    DataRow dr = null;
                    DataTable dt = new DataTable();
                    for (int i = 0; i <= dsCE.Tables[0].Rows.Count - 1; i++)
                    {
                        dr = dsCE.Tables[0].Rows[i];
                        if ((Session["dtRepresentantes"] != null))
                            dt = (DataTable)Session["dtRepresentantes"];
                        else
                        {
                            this.CrearColumnasCampania();
                        }

                        this.CrearFilasCampania(Int32.Parse(dr["Id_Cam"].ToString()), dr["Campana"].ToString(), dr["CDS"].ToString(), dr["Id_Rik"].ToString(), dr["Representante"].ToString(), dr["Uen"].ToString(), dr["Segmento"].ToString(), dr["Area"].ToString(), dr["Solucion"].ToString(), dr["Aplicacion"].ToString(), dr["Producto"].ToString(), Int32.Parse(dr["Cantidad"].ToString()), float.Parse(dr["Efectividad"].ToString()), decimal.Parse(dr["TotalPesos"].ToString()));


                    }


                    ///'''''''''ENCABEZADOS DEL REPORTE'''''''''''''''''
                    sw.WriteLine("<html>");
                    sw.WriteLine("<head>");
                    sw.WriteLine("</head>");
                    sw.WriteLine("<body>");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<strong>MODULO CRM - Campañas </strong>");
                    sw.WriteLine("<br />");
                    sw.WriteLine("Representante: " + Representante + "");
                    sw.WriteLine("<br />");
                    sw.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + "");
                    sw.WriteLine("<br />");

                    sw.WriteLine("<br />");
                    sw.WriteLine("<table border=1><font size=8pt>");

                    sw.WriteLine("<tr><td bgcolor=\"#DDDDDD\"><b>Num. Campaña</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Campaña</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>CDS</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>No. Rik</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Representante</td>");                    
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Uen</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Segmento</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Area</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Solución</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Aplicación</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Producto</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Efectividad</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Cantidad</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Pesos</td></tr>");
                
                    ///'''''''''TERMINAN ENCABEZADOS DEL REPORTE'''''''''''''''''
                    if ((Session["dtRepresentantes"] != null))
                    {
                        dt = (DataTable)Session["dtRepresentantes"];
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            //Formando texto de la tabla

                            sw.WriteLine("<tr>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Id_Cam"] + "</td>"); 
                            sw.WriteLine("<td>" + dt.Rows[i]["Campana"] + "</td>"); 
                            sw.WriteLine("<td>" + dt.Rows[i]["CDS"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Id_Rik"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Representante"] + "</td>");                                                      
                            sw.WriteLine("<td>" + dt.Rows[i]["Uen"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Segmento"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Area"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Solucion"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Aplicacion"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Producto"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Efectividad"] +"</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Cantidad"] + "</td>"); 
                            sw.WriteLine("<td>" + dt.Rows[i]["Pesos"] + "</td>"); 
                            sw.WriteLine("</tr>");
                        }
                        sw.WriteLine("<tr>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                         sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                       
                        sw.WriteLine("</tr>");
                        sw.WriteLine("</font></table>");
                        sw.WriteLine("</body>");
                        sw.WriteLine("</html>");
                        sw.Close();
                        if (File.Exists(ruta))
                        {
                            string ruta2 = null;
                            ruta2 = Server.MapPath("Reportes") + "\\" + NombreArchivo;
                            File.Move(ruta, Server.MapPath("Reportes") + "\\" + NombreArchivo);
                            Response.Redirect("Reportes\\" + NombreArchivo, false);
                        }
                    }

                }
                else
                {
                    Alerta("No se encontraron Campañas");
                }
            }
        }

        private void ExportarDII()
        {
            Session["dtRepresentantes"] = null;
            DataSet dsDII = new DataSet();
            string tipo = "";
            tipo = "num";
            System.IO.StreamWriter sw = null;
            string ruta = null;
            decimal totalA = 0;
            decimal totalP = 0;
            decimal totalN = 0;
            decimal totalMonto = 0;
            decimal totalC = 0;
            decimal totalX = 0;
            ruta = Server.MapPath("Reportes") + "\\Reporte" + this.session.Id_U.ToString() + tipo + ".txt";
            String NombreArchivo = "Reporte" + this.session.Id_U.ToString() + tipo + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".xls";

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\" + NombreArchivo))
                File.Delete(Server.MapPath("Reportes") + "\\" + NombreArchivo);
            sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            string RepID = null;
            string ZonaID = null;
            if (this.ddlRepresentantesComercial.SelectedValue == string.Empty || Convert.ToInt32(this.ddlRepresentantesComercial.SelectedValue) == -1)
                RepID = "%";
            else
                RepID = this.ddlRepresentantesComercial.SelectedValue;
            if (Convert.ToInt32(this.ddlZonas.SelectedValue) == -1)
                ZonaID = "%";
            else
                ZonaID = this.ddlZonas.SelectedValue;

            int periodo = !string.IsNullOrEmpty(ddPeriodo.SelectedValue) ? Convert.ToInt32(ddPeriodo.SelectedValue) : -1;

            new CN_CrmInformes().spCRM_ControlPromocion_DII(this.session.Id_Emp, Convert.ToInt32(ddlZonas.SelectedValue) == -1 ? session.Id_Cd_Ver : Convert.ToInt32(ddlZonas.SelectedValue), periodo, RepID, ref dsDII, this.session.Emp_Cnx);

            if ((dsDII != null))
            {
                if (!(dsDII.Tables[0].Rows.Count == 0))
                {
                    DataRow dr = null;
                    DataTable dt = new DataTable();
                    for (int i = 0; i <= dsDII.Tables[0].Rows.Count - 1; i++)
                    {
                        dr = dsDII.Tables[0].Rows[i];
                        if ((Session["dtRepresentantes"] != null))
                        {
                            dt = (DataTable)Session["dtRepresentantes"];
                        }
                        else
                        {
                            this.CrearColumnas();
                        }

                        this.CrearFilas(dr["ZonaID"].ToString(), dr["UsuarioID"].ToString(), dr["Representante"].ToString(), Convert.ToDouble(dr["A"]).ToString(), Convert.ToDouble(dr["P"]).ToString(), Convert.ToDouble(dr["N"]).ToString(), Convert.ToDouble(dr["total"]).ToString(), Convert.ToDouble(dr["C"]).ToString(), Convert.ToDouble(dr["X"]).ToString(), "0");
                    }
                    ///'''''''''ENCABEZADOS DEL REPORTE'''''''''''''''''
                    sw.WriteLine("<html>");
                    sw.WriteLine("<head>");
                    sw.WriteLine("</head>");
                    sw.WriteLine("<body>");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<strong>MODULO CRM - CONTROL DE LA PROMOCIÓN - Vista DII</strong>");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<table border=1><font size=8pt>");
                    sw.WriteLine("<tr><td bgcolor=\"#DDDDDD\"><b>CDS</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>No. Rik</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Representante</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Análisis</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Presentación</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Negociación</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Proyectos</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Cierre</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Cancelación</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Efectividad Cierre</td></tr>");
                    ///'''''''''TERMINAN ENCABEZADOS DEL REPORTE'''''''''''''''''
                    if ((Session["dtRepresentantes"] != null))
                    {
                        dt = (DataTable)Session["dtRepresentantes"];
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            
                            totalA += Convert.ToDecimal(dt.Rows[i]["A"]);
                            totalP += Convert.ToDecimal(dt.Rows[i]["P"]);
                            totalN += Convert.ToDecimal(dt.Rows[i]["N"]);
                            totalMonto += Convert.ToDecimal(dt.Rows[i]["monto"]);
                            totalC += Convert.ToDecimal(dt.Rows[i]["C"]);
                            totalX += Convert.ToDecimal(dt.Rows[i]["X"]);

                            if (Convert.ToDouble(dt.Rows[i]["C"]) > 0)
                            {
                                dt.Rows[i]["Efectividad"] = Convert.ToDecimal((Convert.ToDecimal(dt.Rows[i]["C"]) / (Convert.ToDecimal(dt.Rows[i]["C"]) + Convert.ToDecimal(dt.Rows[i]["X"])))) * 100;
                                dt.Rows[i]["Efectividad"] = dt.Rows[i]["Efectividad"] + "%";
                            }
                            //Formando texto de la tabla
                            sw.WriteLine("<tr>");
                            sw.WriteLine("<td>" + dt.Rows[i]["CDS"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["UsuarioID"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Representante"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["A"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["P"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["N"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Monto"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["C"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["X"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Efectividad"] + "</td>");
                            sw.WriteLine("</tr>");
                        }
                        sw.WriteLine("<tr>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalA + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalP + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalN + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalMonto + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalC + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalX + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("</tr>");
                        sw.WriteLine("</font></table>");
                        sw.WriteLine("</body>");
                        sw.WriteLine("</html>");
                        sw.Close();
                        if (File.Exists(ruta))
                        {
                            string ruta2 = null;
                            ruta2 = Server.MapPath("Reportes") + "\\" + NombreArchivo;
                            File.Move(ruta, Server.MapPath("Reportes") + "\\" + NombreArchivo);
                            Response.Redirect("Reportes\\" + NombreArchivo, false);
                        }
                    }
                    //Calculando la Efectividad
                }
                else
                {
                    Alerta("No se encontraron proyectos de promoción");
                }
            }
        }

        private void CrearColumnas()
        {
            if (Session["dtRepresentantes"] == null)
            {
                DataTable dtRepresentantes = new DataTable();
                DataColumn dcCDS = new DataColumn("CDS", Type.GetType("System.String"));
                DataColumn dcUsuarioID = new DataColumn("UsuarioID", Type.GetType("System.String"));
                DataColumn dcRepresentante = new DataColumn("Representante", Type.GetType("System.String"));
                DataColumn dcA = new DataColumn("A", Type.GetType("System.String"));
                DataColumn dcP = new DataColumn("P", Type.GetType("System.String"));
                DataColumn dcN = new DataColumn("N", Type.GetType("System.String"));
                DataColumn dcMonto = new DataColumn("Monto", Type.GetType("System.String"));
                DataColumn dcC = new DataColumn("C", Type.GetType("System.String"));
                DataColumn dcX = new DataColumn("X", Type.GetType("System.String"));
                DataColumn dcEfectividad = new DataColumn("Efectividad", Type.GetType("System.String"));

                dtRepresentantes.Columns.Add(dcCDS);
                dtRepresentantes.Columns.Add(dcUsuarioID);
                dtRepresentantes.Columns.Add(dcRepresentante);
                dtRepresentantes.Columns.Add(dcA);
                dtRepresentantes.Columns.Add(dcP);
                dtRepresentantes.Columns.Add(dcN);
                dtRepresentantes.Columns.Add(dcMonto);
                dtRepresentantes.Columns.Add(dcC);
                dtRepresentantes.Columns.Add(dcX);
                dtRepresentantes.Columns.Add(dcEfectividad);
                Session["dtRepresentantes"] = dtRepresentantes;
            }
        }

        private void CrearColumnasControlEntrada()
        {
            if (Session["dtRepresentantes"] == null)
            {
                DataTable dtRepresentantes = new DataTable();
                DataColumn dcCDS = new DataColumn("CDS", Type.GetType("System.String"));
                DataColumn dcUsuarioID = new DataColumn("UsuarioID", Type.GetType("System.String"));
                DataColumn dcRepresentante = new DataColumn("Representante", Type.GetType("System.String"));
                DataColumn dcFechaEntrada = new DataColumn("FechaEntrada", Type.GetType("System.String"));
                

                dtRepresentantes.Columns.Add(dcCDS);
                dtRepresentantes.Columns.Add(dcUsuarioID);
                dtRepresentantes.Columns.Add(dcRepresentante);
                dtRepresentantes.Columns.Add(dcFechaEntrada);                
                Session["dtRepresentantes"] = dtRepresentantes;
            }
        }



        private void CrearColumnasCampania()
        {
            if (Session["dtRepresentantes"] == null)
            {
                DataTable dtRepresentantes = new DataTable();
                DataColumn dcNumCampana = new DataColumn("Id_Cam", Type.GetType("System.String"));
                DataColumn dcCampana = new DataColumn("Campana", Type.GetType("System.String"));
                DataColumn dcCDS = new DataColumn("CDS", Type.GetType("System.String"));
                DataColumn dcUsuarioID = new DataColumn("Id_Rik", Type.GetType("System.String"));
                DataColumn dcRepresentante = new DataColumn("Representante", Type.GetType("System.String"));
                DataColumn dcUen = new DataColumn("Uen", Type.GetType("System.String"));
                DataColumn dcSegmento = new DataColumn("Segmento", Type.GetType("System.String"));
                DataColumn dcArea = new DataColumn("Area", Type.GetType("System.String"));
                DataColumn dcSolucion = new DataColumn("Solucion", Type.GetType("System.String"));
                DataColumn dcAplicacion = new DataColumn("Aplicacion", Type.GetType("System.String"));
                DataColumn dcProducto = new DataColumn("Producto", Type.GetType("System.String"));
                DataColumn dcCantidad = new DataColumn("Cantidad", Type.GetType("System.String"));
                DataColumn dcEfectividad = new DataColumn("EFectividad", Type.GetType("System.String"));
                DataColumn dcPesos = new DataColumn("Pesos", Type.GetType("System.String"));


                dtRepresentantes.Columns.Add(dcNumCampana);
                dtRepresentantes.Columns.Add(dcCampana);
                dtRepresentantes.Columns.Add(dcCDS);
                dtRepresentantes.Columns.Add(dcUsuarioID);
                dtRepresentantes.Columns.Add(dcRepresentante);
                dtRepresentantes.Columns.Add(dcUen);
                dtRepresentantes.Columns.Add(dcSegmento);
                dtRepresentantes.Columns.Add(dcArea);
                dtRepresentantes.Columns.Add(dcSolucion);
                dtRepresentantes.Columns.Add(dcAplicacion);
                dtRepresentantes.Columns.Add(dcProducto);
                dtRepresentantes.Columns.Add(dcCantidad);
                dtRepresentantes.Columns.Add(dcEfectividad);
                dtRepresentantes.Columns.Add(dcPesos);

                Session["dtRepresentantes"] = dtRepresentantes;
            }
        }

        public void CrearFilas(string CDS, string UsuarioID, string Representante, string A, string P, string N, string Monto, string C, string X, string Efectividad)
        {
            DataTable dtRepresentantes = new DataTable();
            dtRepresentantes = (DataTable)Session["dtRepresentantes"];
            DataRow drFila = null;
            drFila = dtRepresentantes.NewRow();
            drFila["CDS"] = CDS;
            drFila["UsuarioID"] = UsuarioID;
            drFila["Representante"] = Representante;
            drFila["A"] = A;
            drFila["P"] = P;
            drFila["N"] = N;
            drFila["Monto"] = Monto;
            drFila["C"] = C;
            drFila["X"] = X;
            drFila["Efectividad"] = Efectividad;
            dtRepresentantes.Rows.Add(drFila);
            dtRepresentantes.AcceptChanges();
            Session["dtRepresentantes"] = dtRepresentantes;
        }

        public void CrearFilasControlEntrada(string CDS, string UsuarioID, string Representante, string Fecha)
        {
            DataTable dtRepresentantes = new DataTable();
            dtRepresentantes = (DataTable)Session["dtRepresentantes"];
            DataRow drFila = null;
            drFila = dtRepresentantes.NewRow();
            drFila["CDS"] = CDS;
            drFila["UsuarioID"] = UsuarioID;
            drFila["Representante"] = Representante;
            drFila["FechaEntrada"] = Fecha;
            
            dtRepresentantes.Rows.Add(drFila);
            dtRepresentantes.AcceptChanges();
            Session["dtRepresentantes"] = dtRepresentantes;
        }


        public void CrearFilasCampania(int id_cam,string Campana, string CDS, string UsuarioID, string Representante, string Uen, string Segmento, string Area, string Solucion, string Aplicacion, string Producto, int Cantidad, float Efectividad, decimal TotalPesos)
        {
            DataTable dtRepresentantes = new DataTable();
            dtRepresentantes = (DataTable)Session["dtRepresentantes"];
            DataRow drFila = null;
            drFila = dtRepresentantes.NewRow();
            drFila["Id_Cam"] = id_cam;
            drFila["Campana"] = Campana;
            drFila["CDS"] = CDS;
            drFila["Id_Rik"] = UsuarioID;
            drFila["Representante"] = Representante;            
            drFila["Uen"] = Uen;
            drFila["Segmento"] = Segmento;
            drFila["Area"] = Area;
            drFila["Solucion"] = Solucion;
            drFila["Aplicacion"] = Aplicacion;
            drFila["Producto"] = Producto;
            drFila["Cantidad"] = Cantidad;
            drFila["Efectividad"] = Efectividad;
            drFila["Pesos"] = TotalPesos;
                      

            dtRepresentantes.Rows.Add(drFila);
            dtRepresentantes.AcceptChanges();
            Session["dtRepresentantes"] = dtRepresentantes;
        }

        public void ExportarControlPromocion_LimpiezaAplicacion()
        {
            string ruta = null;
            string str = "";
            string tipo = "";
            tipo = (this.chkProyectoNuevo.Checked)? "AplNuevo": "Apl";
             
            int intCancela = 0;
            int vintConsulta = 0;
            double vMonto1 = 0;
            double vMonto2 = 0;
            decimal totTeorico = default(decimal);
            decimal totVenta = default(decimal);
            string vRepID = null;
            int vZonaID = 0;
            if (this.session.Id_Cd_Ver != 0)
                vZonaID = this.session.Id_Cd_Ver;
            else
                vZonaID = Convert.ToInt32(this.ddlZonas.SelectedValue);

            if (this.ddlRepresentantesComercial.SelectedValue == string.Empty || Convert.ToInt32(this.ddlRepresentantesComercial.SelectedValue) == -1)
                vRepID = null;
            else
                vRepID = this.ddlRepresentantesComercial.SelectedValue;

            int periodo = !string.IsNullOrEmpty(ddPeriodo.SelectedValue) ? Convert.ToInt32(ddPeriodo.SelectedValue) : -1;

            if (!string.IsNullOrEmpty(this.txtDe.Text.Trim()) && !string.IsNullOrEmpty(this.txtA.Text.Trim()))
            {
                vintConsulta = 1;
                vMonto1 = Convert.ToDouble(this.txtDe.Text.Trim());
                vMonto2 = Convert.ToDouble(this.txtA.Text.Trim());
            }
            else
            {
                vintConsulta = 0;
                vMonto1 = 0;
                vMonto2 = 0;
            }

            ruta = Server.MapPath("Reportes") + "\\Reporte" + this.session.Id_U.ToString() + tipo + ".txt";
            if ((string.IsNullOrEmpty(this.txtA.Text.Trim()) && !string.IsNullOrEmpty(this.txtDe.Text.Trim())) || (!string.IsNullOrEmpty(this.txtA.Text.Trim()) && string.IsNullOrEmpty(this.txtDe.Text.Trim())))
                this.DisplayMensajeAlerta("El rango introducido es incorrecto, falta un valor");
            else
            {
                if (vMonto1 > vMonto2)
                    this.DisplayMensajeAlerta("El rango inicial no debe ser mayor al rango final");
                else
                {

                    String NombreArchivo = "Reporte" + this.session.Id_U.ToString() + tipo + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".xls";
                    if (File.Exists(ruta))
                        File.Delete(ruta);
                    if (File.Exists(Server.MapPath("Reportes") + "\\" + NombreArchivo))
                        File.Delete(Server.MapPath("Reportes") + "\\" + NombreArchivo);
                    System.IO.StreamWriter sw = null;
                    DataSet dsReporte = new DataSet();

                    new CN_CrmInformes().GenerarControlPromocion_LimpiezaAplicacion(
                        this.session.Id_Emp
                        , Convert.ToInt32(ddlZonas.SelectedValue) == -1 ? session.Id_Cd_Ver : Convert.ToInt32(ddlZonas.SelectedValue)
                        , session.Id_U
                        , ddlRepresentantesComercial.SelectedValue
                        , periodo
                        , vintConsulta
                        , this.txtDe.Text.Trim()
                        , this.txtA.Text.Trim(),
                        this.chkProyectoNuevo.Checked
                        , ref dsReporte
                        , this.session.Emp_Cnx
                    );

                    if ((dsReporte != null))
                    {
                        if (!(dsReporte.Tables[0].Rows.Count == 0))
                        {
                            try
                            {
                                sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
                                //Obteniendo totales por etapa
                                decimal vA = default(decimal);
                                decimal vP = default(decimal);
                                decimal vN = default(decimal);
                                decimal vC = default(decimal);
                                decimal vCa = default(decimal);
                                for (int m = 0; m <= dsReporte.Tables[0].Rows.Count - 1; m++)
                                {
                                    DataRow dr = null;
                                    dr = dsReporte.Tables[0].Rows[m];
                                    switch (dr["Estatus"].ToString().ToLower())
                                    {
                                        case "cancelados":
                                            vCa += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "análisis":
                                            vA += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "promoción":
                                            vP += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "negociación":
                                            vN += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "cerrados":
                                            vC += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                    }
                                }
                                //Agregando fila para totales
                                int entero = dsReporte.Tables[0].Rows.Count;
                                DataRow fila = null;
                                fila = dsReporte.Tables[0].NewRow();
                                dsReporte.Tables[0].Rows.InsertAt(fila, entero + 1);
                                ///''''''''''''''''''''''''''
                                for (int i = 0; i <= dsReporte.Tables[0].Rows.Count - 1; i++)
                                {
                                    if (i == 0)
                                    {
                                        sw.WriteLine("<html>");
                                        sw.WriteLine("<head>");
                                        sw.WriteLine("</head>");
                                        sw.WriteLine("<body>");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<strong>MODULO CRM - CONTROL DE LA PROMOCION NIVEL APLICACIÓN</strong>");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        if (this.session.Id_TU == 2)
                                            sw.WriteLine("Representante: " + this.session.U_Nombre);
                                        else
                                        {
                                            sw.WriteLine("Representante: "
                                                + ((this.ddlRepresentantesComercial.SelectedValue == "-1" || this.ddlRepresentantesComercial.SelectedValue == string.Empty) ? "Todos" : this.ddlRepresentantesComercial.SelectedItem.Text));
                                        }
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("Fecha: " + DateTime.Now.Date.ToShortDateString());
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("Periodo: " + ddPeriodo.SelectedItem.Text.Replace("--", "").Trim());
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("CDS: "
                                            + ((this.ddlZonas.SelectedValue == "-1" || this.ddlZonas.SelectedValue == string.Empty) ? "Todos" : this.ddlZonas.SelectedItem.Text));

                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<table border=1><font size=8pt>");
                                        sw.WriteLine("<tr>");
                                        for (int k = 0; k <= dsReporte.Tables[0].Columns.Count - 2; k++)
                                        {
                                            sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>");
                                            sw.WriteLine(dsReporte.Tables[0].Columns[k].Caption);
                                            sw.WriteLine("</td>");
                                        }
                                        sw.WriteLine("</tr>");
                                    }
                                    sw.WriteLine("<tr>");
                                    intCancela = 0;
                                    for (int j = 0; j <= dsReporte.Tables[0].Columns.Count - 2; j++)
                                    {                                                                           
                                           if (dsReporte.Tables[0].Rows[i]["Estatus"].ToString().ToLower() == "cerrados")
                                            {
                                                sw.WriteLine("<td>");
                                                intCancela = 0;
                                            }
                                           else if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i]["Analisis"], System.DBNull.Value)))
                                           {

                                               if (Math.Abs(((TimeSpan)DateTime.Now.Subtract(Convert.ToDateTime(dsReporte.Tables[0].Rows[i]["Analisis"]))).Days) > 60)
                                               {
                                                   sw.WriteLine("<td> <font color=\"#FF0000\">");
                                                   intCancela = 1;
                                               }
                                               else if (Math.Abs(((TimeSpan)DateTime.Now.Subtract(Convert.ToDateTime(dsReporte.Tables[0].Rows[i]["Analisis"]))).Days) > 90)
                                               {
                                                   sw.WriteLine("<td> <font color=\"#FF0000\">");
                                                   intCancela = 1;
                                               }
                                               else
                                               {
                                                   sw.WriteLine("<td>");
                                                   intCancela = 0;
                                               }
                                           }
                                           else {
                                               sw.WriteLine("<td>");
                                               intCancela = 0;
                                           }

                                            

                                    
                                        sw.WriteLine(dsReporte.Tables[0].Rows[i][j]);

                                        if (j == 6)
                                        {
                                            if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i][j], System.DBNull.Value)))
                                                totTeorico += Convert.ToDecimal(dsReporte.Tables[0].Rows[i][j].ToString().Replace("$", "").Replace(",", "").Trim());
                                            else
                                            {
                                                if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                    sw.WriteLine("<strong>" + totTeorico.ToString("C") + "</strong>");
                                            }
                                        }
                                        else if (j == 7)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vA.ToString("C") + "</strong>");
                                        }
                                        else if (j == 8)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vP.ToString("C") + "</strong>");
                                        }
                                        else if (j == 9)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vN.ToString("C") + "</strong>");
                                        }
                                        else if (j == 10)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vC.ToString("C") + "</strong>");
                                        }
                                        else if (j == 11)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vCa.ToString("C") + "</strong>");
                                        }
                                        else if (j == 12)
                                        {
                                            if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i][j], System.DBNull.Value)))
                                                totVenta += Convert.ToDecimal(dsReporte.Tables[0].Rows[i][j].ToString().Replace("$", "").Replace(",", "").Trim());
                                            else
                                            {
                                                if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                    sw.WriteLine("<strong>" + totVenta.ToString("C") + "</strong>");
                                            }
                                        }
                                        if (intCancela == 1)
                                            sw.WriteLine("</font></td>");
                                        else
                                            sw.WriteLine("</td>");
                                    }
                                    sw.WriteLine("</tr>");
                                }
                                sw.WriteLine("</font></table>");
                                sw.WriteLine("</body>");
                                sw.WriteLine("</html>");
                                sw.Close();

                                if (File.Exists(ruta))
                                {
                                    string ruta2 = null;
                                    ruta2 = Server.MapPath("Reportes") + "\\" + NombreArchivo;
                                    File.Move(ruta, Server.MapPath("Reportes") + "\\" + NombreArchivo);
                                    Response.Redirect("Reportes\\" + NombreArchivo);
                                }
                            }
                            catch (Exception Ex)
                            {
                                str = Ex.Message;
                                sw.Close();
                            }
                        }
                        else
                            Alerta("No se encontraron proyectos de promoción");
                    }
                    else
                        Alerta("No se cargó la información");
                }
            }
        }



        public void ExportarCierreMes()
        {
            string ruta = null;
            string str = "";
            string tipo = "";
            tipo = "Cierre";
            int intCancela = 0;
            int vintConsulta = 0;
            double vMonto1 = 0;
            double vMonto2 = 0;
            decimal totTeorico = default(decimal);
            decimal totVenta = default(decimal);
            string vRepID = null;
            int vZonaID = 0;
            if (this.session.Id_Cd_Ver != 0)
                vZonaID = this.session.Id_Cd_Ver;
            else
                vZonaID = Convert.ToInt32(this.ddlZonas.SelectedValue);

            if (this.ddlRepresentantesComercial.SelectedValue == string.Empty || Convert.ToInt32(this.ddlRepresentantesComercial.SelectedValue) == -1)
                vRepID = null;
            else
                vRepID = this.ddlRepresentantesComercial.SelectedValue;

            int periodo = !string.IsNullOrEmpty(ddPeriodo.SelectedValue) ? Convert.ToInt32(ddPeriodo.SelectedValue) : -1;

            if (!string.IsNullOrEmpty(this.txtDe.Text.Trim()) && !string.IsNullOrEmpty(this.txtA.Text.Trim()))
            {
                vintConsulta = 1;
                vMonto1 = Convert.ToDouble(this.txtDe.Text.Trim());
                vMonto2 = Convert.ToDouble(this.txtA.Text.Trim());
            }
            else
            {
                vintConsulta = 0;
                vMonto1 = 0;
                vMonto2 = 0;
            }

            ruta = Server.MapPath("Reportes") + "\\Reporte" + this.session.Id_U.ToString() + tipo + ".txt";
            if ((string.IsNullOrEmpty(this.txtA.Text.Trim()) && !string.IsNullOrEmpty(this.txtDe.Text.Trim())) || (!string.IsNullOrEmpty(this.txtA.Text.Trim()) && string.IsNullOrEmpty(this.txtDe.Text.Trim())))
                this.DisplayMensajeAlerta("El rango introducido es incorrecto, falta un valor");
            else
            {
                if (vMonto1 > vMonto2)
                    this.DisplayMensajeAlerta("El rango inicial no debe ser mayor al rango final");
                else
                {
                    String NombreArchivo = "Reporte" + this.session.Id_U.ToString() + tipo + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".xls";

                    if (File.Exists(ruta))
                        File.Delete(ruta);
                    if (File.Exists(Server.MapPath("Reportes") + "\\" + NombreArchivo))
                        File.Delete(Server.MapPath("Reportes") + "\\" + NombreArchivo);
                    System.IO.StreamWriter sw = null;
                    DataSet dsReporte = new DataSet();

                    new CN_CrmInformes().GenerarCierreMes(
                        this.session.Id_Emp
                        , Convert.ToInt32(ddlZonas.SelectedValue) == -1 ? session.Id_Cd_Ver : Convert.ToInt32(ddlZonas.SelectedValue)
                        , session.Id_U
                        , ddlRepresentantesComercial.SelectedValue
                        , periodo
                        , vintConsulta
                        , this.txtDe.Text.Trim()
                        , this.txtA.Text.Trim(),
                        this.chkProyectoNuevo.Checked
                        , ref dsReporte
                        , this.session.Emp_Cnx
                    );

                    if ((dsReporte != null))
                    {
                        if (!(dsReporte.Tables[0].Rows.Count == 0))
                        {
                            try
                            {
                                sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
                                //Obteniendo totales por etapa
                                decimal vA = default(decimal);
                                decimal vP = default(decimal);
                                decimal vN = default(decimal);
                                decimal vC = default(decimal);
                                decimal vCa = default(decimal);
                                for (int m = 0; m <= dsReporte.Tables[0].Rows.Count - 1; m++)
                                {
                                    DataRow dr = null;
                                    dr = dsReporte.Tables[0].Rows[m];
                                    switch (dr["Estatus"].ToString().ToLower())
                                    {
                                        case "cancelados":
                                            vCa += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "análisis":
                                            vA += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "promoción":
                                            vP += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "negociación":
                                            vN += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "cerrados":
                                            vC += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                    }
                                }
                                //Agregando fila para totales
                                int entero = dsReporte.Tables[0].Rows.Count;
                                DataRow fila = null;
                                fila = dsReporte.Tables[0].NewRow();
                                dsReporte.Tables[0].Rows.InsertAt(fila, entero + 1);
                                ///''''''''''''''''''''''''''
                                for (int i = 0; i <= dsReporte.Tables[0].Rows.Count - 1; i++)
                                {
                                    if (i == 0)
                                    {
                                        sw.WriteLine("<html>");
                                        sw.WriteLine("<head>");
                                        sw.WriteLine("</head>");
                                        sw.WriteLine("<body>");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<strong>MODULO CRM - CIERRE MES</strong>");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        if (this.session.Id_TU == 2)
                                            sw.WriteLine("Representante: " + this.session.U_Nombre);
                                        else
                                        {
                                            sw.WriteLine("Representante: "
                                                + ((this.ddlRepresentantesComercial.SelectedValue == "-1" || this.ddlRepresentantesComercial.SelectedValue == string.Empty) ? "Todos" : this.ddlRepresentantesComercial.SelectedItem.Text));
                                        }
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("Fecha: " + DateTime.Now.Date.ToShortDateString());
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("Periodo: " + ddPeriodo.SelectedItem.Text.Replace("--", "").Trim());
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("CDS: "
                                            + ((this.ddlZonas.SelectedValue == "-1" || this.ddlZonas.SelectedValue == string.Empty) ? "Todos" : this.ddlZonas.SelectedItem.Text));

                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<table border=1><font size=8pt>");
                                        sw.WriteLine("<tr>");
                                        for (int k = 0; k <= dsReporte.Tables[0].Columns.Count - 1; k++)
                                        {
                                            switch (k)
                                            {
                                                case 0:
                                                case 1:
                                                case 2:
                                                    sw.WriteLine("<td bgcolor=\"#FF0000\"><b>");
                                                    break;
                                                case 3:
                                                case 4:
                                                    sw.WriteLine("<td bgcolor=\"#B0E0E6\"><b>");                                                 
                                                    break;
                                                case 5:
                                                case 6:
                                                case 7:
                                                case 8:
                                                case 9:
                                                case 10:
                                                case 11:
                                                case 12:
                                                    sw.WriteLine("<td bgcolor=\"#191970\"><b>");
                                                    break;
                                                default:
                                                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>");
                                                    break;
                                            }
                                         
                                            sw.WriteLine(dsReporte.Tables[0].Columns[k].Caption);
                                            sw.WriteLine("</td>");
                                        }
                                        sw.WriteLine("</tr>");
                                    }
                                    sw.WriteLine("<tr>");
                                    intCancela = 0;
                                    for (int j = 0; j <= dsReporte.Tables[0].Columns.Count - 1; j++)
                                    {
                                        if (dsReporte.Tables[0].Rows[i]["Estatus"].ToString().ToLower() == "cerrados")
                                        {
                                            sw.WriteLine("<td>");
                                            intCancela = 0;
                                        }
                                        else if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i]["Analisis"], System.DBNull.Value)))
                                        {

                                            if (Math.Abs(((TimeSpan)DateTime.Now.Subtract(Convert.ToDateTime(dsReporte.Tables[0].Rows[i]["Analisis"]))).Days) > 60)
                                            {
                                                sw.WriteLine("<td> <font color=\"#FF0000\">");
                                                intCancela = 1;
                                            }
                                            else if (Math.Abs(((TimeSpan)DateTime.Now.Subtract(Convert.ToDateTime(dsReporte.Tables[0].Rows[i]["Analisis"]))).Days) > 90)
                                            {
                                                sw.WriteLine("<td> <font color=\"#FF0000\">");
                                                intCancela = 1;
                                            }
                                            else
                                            {
                                                sw.WriteLine("<td>");
                                                intCancela = 0;
                                            }
                                        }
                                        else
                                        {
                                            sw.WriteLine("<td>");
                                            intCancela = 0;
                                        }


                                        sw.WriteLine(dsReporte.Tables[0].Rows[i][j]);
                                        
                                        if (j == 8)
                                        {
                                            if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i][j], System.DBNull.Value)))
                                                totTeorico += Convert.ToDecimal(dsReporte.Tables[0].Rows[i][j].ToString().Replace("$", "").Replace(",", "").Trim());
                                            else
                                            {
                                                if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                    sw.WriteLine("<strong>" + totTeorico.ToString("C") + "</strong>");
                                            }
                                        }
                                        
                                        else if (j == 9)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vA.ToString("C") + "</strong>");
                                        }
                                        else if (j == 10)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vP.ToString("C") + "</strong>");
                                        }
                                            
                                        else if (j == 11)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vN.ToString("C") + "</strong>");
                                        }
                                        else if (j == 12)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vC.ToString("C") + "</strong>");
                                        }
                                        else if (j == 13)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vCa.ToString("C") + "</strong>");
                                        }
                                            
                                       /* else if (j == 15)
                                        {
                                            if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i][j], System.DBNull.Value)))
                                                totVenta += Convert.ToDecimal(dsReporte.Tables[0].Rows[i][j].ToString().Replace("$", "").Replace(",", "").Trim());
                                            else
                                            {
                                                if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                    sw.WriteLine("<strong>" + totVenta.ToString("C") + "</strong>");
                                            }
                                        }*/
                                        if (intCancela == 1)
                                            sw.WriteLine("</font></td>");
                                        else
                                            sw.WriteLine("</td>");
                                    }
                                    sw.WriteLine("</tr>");
                                }
                                sw.WriteLine("</font></table>");
                                sw.WriteLine("</body>");
                                sw.WriteLine("</html>");
                                sw.Close();

                                if (File.Exists(ruta))
                                {
                                    string ruta2 = null;
                                    ruta2 = Server.MapPath("Reportes") + "\\" + NombreArchivo;
                                    File.Move(ruta, Server.MapPath("Reportes") + "\\" + NombreArchivo);
                                    Response.Redirect("Reportes\\" + NombreArchivo);
                                }
                            }
                            catch (Exception Ex)
                            {
                                str = Ex.Message;
                                sw.Close();
                            }
                        }
                        else
                            Alerta("No se encontraron proyectos de promoción");
                    }
                    else
                        Alerta("No se cargó la información");
                }
            }
        }

        private void ExportarDIINumero()
        {
            Session["dtRepresentantes"] = null;
            DataSet dsDII = new DataSet();
            System.IO.StreamWriter sw = null;
            string ruta = null;
            string tipo = "";
            tipo = "imp";
         
            decimal totalA = default(decimal);
            decimal totalP = default(decimal);
            decimal totalN = default(decimal);
            decimal totalMonto = default(decimal);
            decimal totalC = default(decimal);
            decimal totalX = default(decimal);
            ruta = Server.MapPath("Reportes") + "\\Reporte" + this.session.Id_U.ToString() + tipo + ".txt";
            String NombreArchivo = "Reporte" + this.session.Id_U.ToString() + tipo + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".xls";

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\" + NombreArchivo))
                File.Delete(Server.MapPath("Reportes") + "\\" + NombreArchivo);
            sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            string RepID = null;
            string ZonaID = null;
            if (this.ddlRepresentantesComercial.SelectedValue == string.Empty || Convert.ToInt32(this.ddlRepresentantesComercial.SelectedValue) == -1)
                RepID = "%";
            else
                RepID = this.ddlRepresentantesComercial.SelectedValue;
            if (Convert.ToInt32(this.ddlZonas.SelectedValue) == -1)
                ZonaID = "%";
            else
                ZonaID = this.ddlZonas.SelectedValue;

            int periodo = !string.IsNullOrEmpty(ddPeriodo.SelectedValue) ? Convert.ToInt32(ddPeriodo.SelectedValue) : -1;

            new CN_CrmInformes().spCRM_ControlPromocion_DIINumero(
                       this.session.Id_Emp
                       , Convert.ToInt32(ddlZonas.SelectedValue) == -1 ? session.Id_Cd_Ver : Convert.ToInt32(ddlZonas.SelectedValue)
                       , periodo
                       , RepID
                       , ref dsDII
                       , this.session.Emp_Cnx);
            if (dsDII != null)
            {
                if (!(dsDII.Tables[0].Rows.Count == 0))
                {
                    DataRow dr = null;
                    DataTable dt = new DataTable();                    
                    for (int i = 0; i <= dsDII.Tables[0].Rows.Count - 1; i++)
                    {
                        dr = dsDII.Tables[0].Rows[i];
                        if ((Session["dtRepresentantes"] != null))
                        {
                            dt = (DataTable)Session["dtRepresentantes"];
                        }
                        else
                        {
                            this.CrearColumnas();
                        }

                        this.CrearFilas(dr["ZonaID"].ToString(), dr["UsuarioID"].ToString(), dr["Representante"].ToString(), Convert.ToDouble(dr["A"]).ToString(), Convert.ToDouble(dr["P"]).ToString(), Convert.ToDouble(dr["N"]).ToString(), Convert.ToDouble(dr["Monto"]).ToString(), Convert.ToDouble(dr["C"]).ToString(), Convert.ToDouble(dr["X"]).ToString(), "0");
                                                
                    }
                    ///'''''''''ENCABEZADOS DEL REPORTE'''''''''''''''''
                    sw.WriteLine("<html>");
                    sw.WriteLine("<head>");
                    sw.WriteLine("</head>");
                    sw.WriteLine("<body>");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<strong>MODULO CRM - CONTROL DE LA PROMOCION - Vista DII</strong>");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<table border=1><font size=8pt>");
                    sw.WriteLine("<tr><td bgcolor=\"#DDDDDD\"><b>CDS</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>No. Rik</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Representante</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Análisis</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Presentación</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Negociación</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Monto proyecto</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Cierre</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Cancelación</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Efectividad cierre</td></tr>");
                    ///'''''''''TERMINAN ENCABEZADOS DEL REPORTE'''''''''''''''''
                    if ((Session["dtRepresentantes"] != null))
                    {
                        dt = (DataTable)Session["dtRepresentantes"];
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            
                            totalA += Convert.ToDecimal(dt.Rows[i]["A"]);
                            totalP += Convert.ToDecimal(dt.Rows[i]["P"]);
                            totalN += Convert.ToDecimal(dt.Rows[i]["N"]);
                            totalMonto += Convert.ToDecimal(dt.Rows[i]["Monto"]);
                            totalC += Convert.ToDecimal(dt.Rows[i]["C"]);
                            totalX += Convert.ToDecimal(dt.Rows[i]["X"]);

                            if (Convert.ToDouble(dt.Rows[i]["C"]) > 0)
                            {
                                dt.Rows[i]["Efectividad"] = Convert.ToDecimal((Convert.ToDecimal(dt.Rows[i]["C"]) / (Convert.ToDecimal(dt.Rows[i]["C"]) + Convert.ToDecimal(dt.Rows[i]["X"])))) * 100;
                                dt.Rows[i]["Efectividad"] = dt.Rows[i]["Efectividad"] + "%";
                            }
                            //Formando texto de la tabla
                            sw.WriteLine("<tr>");
                            sw.WriteLine("<td>" + dt.Rows[i]["CDS"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["UsuarioID"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Representante"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["A"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["P"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["N"] + "</td>");
                            sw.WriteLine("<td>" + string.Concat("$", dt.Rows[i]["Monto"]) + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["C"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["X"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Efectividad"] + "</td>");
                            sw.WriteLine("</tr>");
                        }
                        sw.WriteLine("<tr>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalA.ToString("C") + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalP.ToString("C") + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalN.ToString("C") + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalMonto.ToString("C") + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalC.ToString("C") + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalX.ToString("C") + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("</tr>");

                        sw.WriteLine("</font></table>");
                        sw.WriteLine("</body>");
                        sw.WriteLine("</html>");
                        sw.Close();
                        if (File.Exists(ruta))
                        {
                            string ruta2 = null;
                            ruta2 = Server.MapPath("Reportes") + "\\" + NombreArchivo;
                            File.Move(ruta, Server.MapPath("Reportes") + "\\" + NombreArchivo);
                            Response.Redirect("Reportes\\" + NombreArchivo);
                        }
                    }
                    //Calculando la Efectividad
                }
                else
                {
                    Alerta("No se encontraron proyectos de promoción");
                }
            }
        }
        #endregion
        #region Combos
        private void CargarZonas()
        { //Cargar zonas (Centros de distribución) 
            new CN__Comun().LlenaCombo(1, this.session.Id_Emp, this.session.Id_U, this.session.Emp_Cnx, "spCatCentroDistribucion_Combo", ref ddlZonas);
            ddlZonas.SelectedIndex = ddlZonas.FindItemIndexByValue(this.session.Id_Cd_Ver.ToString());
            //ddlZonas.Items[0].Remove();
            if (session.Id_TU == 2 || session.Id_TU == 4 || session.Id_TU == 5)//representante
                ddlZonas.Enabled = false;
        }

        private void CargarRepresentantes()
        {//Cargar zonas (Centros de distribución)
            try
            {
                new CN__Comun().LlenaCombo(1, this.session.Id_Emp, Convert.ToInt32(ddlZonas.SelectedValue), session.Id_U, this.session.Emp_Cnx, "spCatRik_Combo", ref ddlRepresentantesComercial);
                if (!string.IsNullOrEmpty(this.session.Id_Rik.ToString()))
                    if (this.session.Id_Rik > 0)
                        ddlRepresentantesComercial.SelectedIndex = ddlRepresentantesComercial.FindItemIndexByValue(this.session.Id_Rik.ToString());
                if (session.Id_TU == 2 || session.Id_TU == 4 || session.Id_TU == 5)//representante
                    ddlRepresentantesComercial.Enabled = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CargarPeriodos()
        {//Cargar periodos
            try
            {
                new CN__Comun().LlenaCombo(this.session.Id_Emp, this.session.Id_Cd_Ver, this.session.Emp_Cnx, "spCatCalendario_Consulta_MesAnhio", ref ddPeriodo);
                ddPeriodo.SelectedIndex = ddPeriodo.Items.Count - 1;
                //RadComboBoxItem rcbi = new RadComboBoxItem();
                //rcbi.Text = "-- Todos --";
                //rcbi.Value = "-1";
                //ddPeriodo.Items.Insert(0, rcbi);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region
        //private void CargarUENS()
        //{
        //    if (Convert.ToInt32(this.ddlRepresentantes.SelectedValue) != -1)
        //    {
        //        DataTable dtUensRik = new DataTable();
        //        new CN_CrmInformes().CargarUENS(
        //            this.session.Id_Emp
        //            , this.session.Id_Cd_Ver
        //            , Convert.ToInt32(this.ddlRepresentantes.SelectedValue)
        //            , ref dtUensRik
        //            , this.session.Emp_Cnx);
        //        DataRowCollection dtCloeccion = dtUensRik.Rows;
        //        if (dtCloeccion.Count > 0)
        //        {
        //            DataRow row = dtUensRik.NewRow();
        //            //row["RIK"] = ddlRepresentantes.SelectedValue;
        //            row["UEN"] = -1;
        //            row["Descripcion"] = "-- Seleccionar --";
        //            dtUensRik.Rows.InsertAt(row, 0);
        //            ddlUENs.DataSource = dtUensRik;
        //            ddlUENs.DataValueField = "UEN";
        //            ddlUENs.DataTextField = "Descripcion";
        //            ddlUENs.DataBind();
        //            this.CargarSegmentos();
        //        }
        //        else
        //            ddlUENs.Items.Clear();
        //        ddlSegmentos.Items.Clear();
        //        ddlTerritorios.Items.Clear();
        //        ddlArea.Items.Clear();
        //        ddlSolucion.Items.Clear();
        //        ddlAplicacion.Items.Clear();
        //    }
        //}

        //private void CargarSegmentos()
        //{
        //    if (Convert.ToInt32(this.ddlRepresentantes.SelectedValue) != -1 && Convert.ToInt32(this.ddlUENs.SelectedValue) != -1)
        //    {
        //        DataTable dtSegRik = new DataTable();
        //        new CN_CrmInformes().CargarSegmentos(
        //            this.session.Id_Emp
        //            , this.session.Id_Cd_Ver
        //            , Convert.ToInt32(this.ddlRepresentantes.SelectedValue)
        //            , Convert.ToInt32(this.ddlUENs.SelectedValue)
        //            , ref dtSegRik
        //            , this.session.Emp_Cnx);

        //        DataRowCollection dtCloeccion = dtSegRik.Rows;
        //        if (dtCloeccion.Count > 0)
        //        {
        //            DataRow row = dtSegRik.NewRow();
        //            row["Id"] = -1;
        //            row["Descripcion"] = "-- Todos --";

        //            dtSegRik.Rows.InsertAt(row, 0);

        //            ddlSegmentos.DataSource = dtSegRik;
        //            ddlSegmentos.DataBind();

        //            this.CargarAreasdeSegmento();
        //            this.CargarTerritorios();
        //        }
        //        else
        //        {
        //            ddlSegmentos.Items.Clear();
        //            ddlTerritorios.Items.Clear();
        //        }
        //    }
        //}

        //private void CargarAreasdeSegmento()
        //{
        //    if (Convert.ToInt32(this.ddlRepresentantes.SelectedValue) != -1 && Convert.ToInt32(this.ddlUENs.SelectedValue) != -1)
        //    {
        //        DataTable dtAreasSegmento = new DataTable();
        //        new CN_CrmInformes().CargarAreasSegmento(
        //            true
        //            , this.session.Id_Emp
        //            , Convert.ToInt32(this.ddlSegmentos.SelectedValue)//this.session.Id_Cd_Ver
        //            , ref dtAreasSegmento
        //            , this.session.Emp_Cnx);

        //        DataRowCollection dtCloeccion = dtAreasSegmento.Rows;
        //        if (dtCloeccion.Count > 0)
        //        {
        //            DataRow row = dtAreasSegmento.NewRow();
        //            row["Id"] = -1;
        //            row["Descripcion"] = "-- Todas --";
        //            dtAreasSegmento.Rows.InsertAt(row, 0);
        //            ddlArea.DataSource = dtAreasSegmento;
        //            ddlArea.DataBind();
        //        }
        //        else
        //        {
        //            ddlArea.Items.Clear();
        //            ddlSolucion.Items.Clear();
        //        }
        //    }
        //}

        //private void CargarTerritorios()
        //{
        //    if (Convert.ToInt32(this.ddlRepresentantes.SelectedValue) != -1 && Convert.ToInt32(this.ddlSegmentos.SelectedValue) != -1)
        //    {
        //        DataTable dtTerritoriosRik = new DataTable();
        //        new CN_CrmInformes().CargarTerritoriosRik(
        //            this.session.Id_Emp
        //            , this.session.Id_Cd_Ver
        //            , Convert.ToInt32(this.ddlRepresentantes.SelectedValue)
        //            , Convert.ToInt32(this.ddlSegmentos.SelectedValue)
        //            , ref dtTerritoriosRik
        //            , this.session.Emp_Cnx);

        //        DataRowCollection dtCloeccion = dtTerritoriosRik.Rows;
        //        if (dtCloeccion.Count > 0)
        //        {
        //            DataRow row = dtTerritoriosRik.NewRow();
        //            row["Id"] = -1;
        //            row["Descripcion"] = "-- Todos --";
        //            dtTerritoriosRik.Rows.InsertAt(row, 0);
        //            ddlTerritorios.DataSource = dtTerritoriosRik;
        //            ddlTerritorios.DataBind();
        //        }
        //        else
        //            ddlTerritorios.Items.Clear();
        //    }
        //}

        //private void CargarUENSegmentosTerritoriosSucursal()
        //{
        //    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
        //    CN_Comun.LlenaCombo(1, session.Id_Emp, session.Emp_Cnx, "spCatUen_Combo", ref ddlUENs);
        //    DataSet ds = new DataSet();
        //    new CN_CrmInformes().ConsultarUENSegmentosTerritoriosSucursal(session.Id_Emp, session.Id_Cd, ref ds, session.Emp_Cnx);
        //    ddlTerritorios.DataSource = ds.Tables[0];
        //    ddlTerritorios.DataBind();
        //    ddlSegmentos.DataSource = ds.Tables[1];
        //    ddlSegmentos.DataBind();
        //    ddlUENs.DataSource = ds.Tables[2];
        //    ddlUENs.DataBind();
        //}

        //private void CargarSolucionesdeArea()
        //{
        //    if (Convert.ToInt32(this.ddlArea.SelectedValue) != -1)
        //    {
        //        DataTable dtSolucionesArea = new DataTable();
        //        new CN_CrmInformes().CargarSolucionesArea(
        //            this.session.Id_Emp
        //            , Convert.ToInt32(this.ddlArea.SelectedValue)
        //            , ref dtSolucionesArea
        //            , this.session.Emp_Cnx);

        //        DataRowCollection dtCloeccion = dtSolucionesArea.Rows;
        //        if (dtCloeccion.Count > 0)
        //        {
        //            DataRow row = dtSolucionesArea.NewRow();
        //            row["Id"] = -1;
        //            row["Descripcion"] = "-- Todas --";
        //            dtSolucionesArea.Rows.InsertAt(row, 0);
        //            ddlSolucion.DataSource = dtSolucionesArea;
        //            ddlSolucion.DataBind();
        //        }
        //        else
        //        {
        //            ddlSolucion.Items.Clear();
        //            ddlAplicacion.Items.Clear();
        //        }
        //    }
        //}

        //private void CargarAplicacionesSolucion()
        //{
        //    if (Convert.ToInt32(this.ddlArea.SelectedValue) != -1)
        //    {
        //        DataTable dtAplicacionesSolucion = new DataTable();
        //        new CN_CrmInformes().CargarAplicacionesSoluciones(
        //            this.session.Id_Emp
        //            , Convert.ToInt32(this.ddlSolucion.SelectedValue)
        //            , ref dtAplicacionesSolucion
        //            , this.session.Emp_Cnx);

        //        DataRowCollection dtCloeccion = dtAplicacionesSolucion.Rows;
        //        if (dtCloeccion.Count > 0)
        //        {
        //            DataRow row = dtAplicacionesSolucion.NewRow();
        //            row["Id"] = -1;
        //            row["Descripcion"] = "-- Todas --";

        //            dtAplicacionesSolucion.Rows.InsertAt(row, 0);

        //            ddlAplicacion.DataSource = dtAplicacionesSolucion;
        //            ddlAplicacion.DataBind();
        //        }
        //        else
        //            ddlAplicacion.Items.Clear();
        //    }
        //}
        #endregion
        private void ValidarPermisos()
        {
            try
            {
                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, session.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = session.Id_U;
                Permiso.Id_Cd = session.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, session.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta(string.Concat("Error al cargar la página."));
            else
                Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        #endregion
        #region ErrorManager
        private void Alerta(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje.Replace("'", "\"") + "', 330, 150);");
                //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "bloque1", "radalert('" + mensaje.Replace("'", "\"") + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void ErrorManager()
        {
            try
            {
                //this.lblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorManager(string Message)
        {
            try
            {
                //this.lblMensaje.Text = Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();
            }
            catch (Exception)
            {
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                this.radCierreMes.Style.Add("display", "none");//
                this.radControl.Style.Add("display", "none");
                if (session.Id_TU == 2)
                {
                    this.radDII.Style.Add("display", "none");//
                    this.radDIINumero.Style.Add("display", "none");//                   
                    this.radCampania.Style.Add("display", "none");
                }
                this.CargarZonas();
                this.CargarRepresentantes();
                this.CargarPeriodos();
                ibtnExcelSolucion.Visible = true;
            }
            catch (Exception)
            {

            }
        }

        protected void ibtnExcelAplicacion_Click(object sender, ImageClickEventArgs e)
        {
            if (!_PermisoImprimir)
            {
                Alerta("No tiene permiso para imprimir");
                return;
            }
            if (!radControl.Checked && !radDII.Checked && !radControlAplicacion.Checked && !radDIINumero.Checked && !radControlEntrada.Checked && !radCampania.Checked && !radCierreMes.Checked)
            {
                Alerta(string.Concat("Seleccione una opción de control"));
                return;
            }
            string periodo = !string.IsNullOrEmpty(ddPeriodo.SelectedValue) ? Convert.ToString(ddPeriodo.SelectedValue) : "-1*-1";

            if (periodo != "-1*-1")
            {
                ExportarControlPromocion_LimpiezaAplicacion();
            }
        }

        protected void ibtnExcelNumero_Click(object sender, ImageClickEventArgs e)
        {
            if (!_PermisoImprimir)
            {
                Alerta("No tiene permiso para imprimir");
                return;
            }

            if (!radControl.Checked && !radDII.Checked && !radControlAplicacion.Checked && !radDIINumero.Checked && !radControlEntrada.Checked && !radCampania.Checked && !radCierreMes.Checked)
            {
                Alerta(string.Concat("Seleccione una opción de control"));
                return;
            }
            string periodo = !string.IsNullOrEmpty(ddPeriodo.SelectedValue) ? Convert.ToString(ddPeriodo.SelectedValue) : "-1*-1";

            if (periodo != "-1*-1")
            {
                this.ExportarDII();
            }
        }



        protected void IbtnExcelCierreMes_Click(object sender, ImageClickEventArgs e)
        {
            if (!_PermisoImprimir)
            {
                Alerta("No tiene permiso para imprimir");
                return;
            }

            if (!radControl.Checked && !radDII.Checked && !radControlAplicacion.Checked && !radDIINumero.Checked && !radControlEntrada.Checked && !radCampania.Checked && !radCierreMes.Checked)
            {
                Alerta(string.Concat("Seleccione una opción de control"));
                return;
            }
            string periodo = !string.IsNullOrEmpty(ddPeriodo.SelectedValue) ? Convert.ToString(ddPeriodo.SelectedValue) : "-1*-1";

            if (periodo != "-1*-1")
            {
                this.ExportarCierreMes();
            }
        }


        protected void ibtnExcelControlEntrada_Click(object sender, ImageClickEventArgs e)
        {
            if (!_PermisoImprimir)
            {
                Alerta("No tiene permiso para imprimir");
                return;
            }

            if (!radControl.Checked && !radDII.Checked && !radControlAplicacion.Checked && !radDIINumero.Checked && !radControlEntrada.Checked && !radCampania.Checked && !radCierreMes.Checked)
            {
                Alerta(string.Concat("Seleccione una opción de control"));
                return;
            }
            string periodo = !string.IsNullOrEmpty(ddPeriodo.SelectedValue) ? Convert.ToString(ddPeriodo.SelectedValue) : "-1*-1";

            if (periodo != "-1*-1")
            {
                this.ExportarControlEntrada();
            }
        }

        protected void ibtnExcelImporte_Click(object sender, ImageClickEventArgs e)
        {
            if (!_PermisoImprimir)
            {
                Alerta("No tiene permiso para imprimir");
                return;
            }

            if (!radControl.Checked && !radDII.Checked && !radControlAplicacion.Checked && !radDIINumero.Checked && !radControlEntrada.Checked && !radCampania.Checked && !radCierreMes.Checked)
            {
                Alerta(string.Concat("Seleccione una opción de control"));
                return;
            }
            string periodo = !string.IsNullOrEmpty(ddPeriodo.SelectedValue) ? Convert.ToString(ddPeriodo.SelectedValue) : "-1*-1";

            if (periodo != "-1*-1")
                ExportarDIINumero();
        }


        protected void ibtnExcelCampania_Click(object sender, ImageClickEventArgs e)
        {
            if (!_PermisoImprimir)
            {
                Alerta("No tiene permiso para imprimir");
                return;
            }

            if (!radControl.Checked && !radDII.Checked && !radControlAplicacion.Checked && !radDIINumero.Checked && !radControlEntrada.Checked && !radCampania.Checked && !radCierreMes.Checked)
            {
                Alerta(string.Concat("Seleccione una opción de control"));
                return;
            }
            string periodo = !string.IsNullOrEmpty(ddPeriodo.SelectedValue) ? Convert.ToString(ddPeriodo.SelectedValue) : "-1*-1";

            if (periodo != "-1*-1")
            {
                this.ExportarCampania();
            }
        }
    }
}