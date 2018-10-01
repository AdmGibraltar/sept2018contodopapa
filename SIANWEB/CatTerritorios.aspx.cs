using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Globalization;
using System.Configuration;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
namespace SIANWEB
{
    public partial class CatTerritorios : System.Web.UI.Page
    {
        public string claveTerr = string.Empty;
        public int tipoRepClave;
        public int idConsecutivo;

        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public string Valor
        {
            get
            {
                return MaximoId(null);
            }
            set { }
        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();

                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        CargarCentros();
                        Inicializar();
                    }
                    CreaCBTipoRepresentante(ref tblTipoRepresentante);
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Nuevo();
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    ErrorManager();
                    RadToolBarButton btn = e.Item as RadToolBarButton;
                    if (btn.CommandName == "save")
                    {
                        Guardar();
                    }
                    else if (btn.CommandName == "new")
                    {
                        Nuevo();
                    }
                    else if (btn.CommandName == "undo")
                    {
                        //Regresar()
                    }
                }
                else
                {
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadMultiPage1.PageViews[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
        protected void rg1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rg1.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_NeedDataSource");
            }
        }

        protected void rg1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.CommandName.ToString() == "Modificar")
                {
                    habilitarCampos(false);
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        CN__Comun.RemoverValidadores(Validators);

                        string tipoRep = string.Empty;
                        tipoRep = rg1.Items[item]["Id_TipoRepresentante"].Text;

                        hfTipoRepresentante.Value = tipoRep;

                        switch (tipoRep)
                        {
                            case "1":
                                //chkRSC.Checked = true;
                                RSC(true);
                                CargarRik();
                                //claveTerritorio();
                                break;

                            case "2":
                                //chkAsesor.Checked = true;
                                Asesor(true);
                                CargarRik();
                                //claveTerritorio();
                                break;

                            case "3":
                                //chkRIK.Checked = true;
                                RIK(true);
                                CargarRik();
                                //claveTerritorio();
                                break;

                            case "4":
                                //chkOTRO.Checked = true;
                                OTRO(true);
                                CargarRik();
                                break;

                            default:
                                //chkRIK.Checked = true;
                                RIK(true);
                                CargarRik();
                                //claveTerritorio();
                                break;
                        }

                        txtClave.Enabled = false;

                        HF_ID.Value = rg1.Items[item]["Id_Ter"].Text;
                        txtClave.Text = rg1.Items[item]["Id_Ter"].Text;

                        txtDescripcion.Text = rg1.Items[item]["Descripcion"].Text;
                        //
                        txtDescripcionSolicitud.Text = rg1.Items[item]["Descripcion"].Text;
                        //
                        txtUen.Text = rg1.Items[item]["Id_Uen"].Text;
                        if (cmbUen.FindItemIndexByValue(rg1.Items[item]["Id_Uen"].Text) > 0)
                        {
                            cmbUen.SelectedIndex = cmbUen.FindItemIndexByValue(rg1.Items[item]["Id_Uen"].Text);
                            cmbUen.Text = cmbUen.FindItemByValue(rg1.Items[item]["Id_Uen"].Text).Text;
                        }
                        else
                        {
                            cmbUen.SelectedIndex = 0;
                            cmbUen.Text = cmbUen.Items[0].Text;
                            txtUen.Text = "";
                        }
                        //CargarRik();
                        CargarSeg();


                        txtRik.Text = rg1.Items[item]["Id_Rik"].Text == "-1" ? string.Empty : rg1.Items[item]["Id_Rik"].Text;
                        if (cmbRik.FindItemIndexByValue(rg1.Items[item]["Id_Rik"].Text) > 0)
                        {
                            cmbRik.SelectedIndex = cmbRik.FindItemIndexByValue(rg1.Items[item]["Id_Rik"].Text);
                            cmbRik.Text = cmbRik.FindItemByValue(rg1.Items[item]["Id_Rik"].Text).Text;
                        }
                        else
                        {
                            cmbRik.SelectedIndex = 0;
                            cmbRik.Text = cmbRik.Items[0].Text;
                            txtRik.Text = "";
                        }

                        ///

                        txtRikSolicitud.Text = rg1.Items[item]["Id_Rik"].Text == "-1" ? string.Empty : rg1.Items[item]["Id_Rik"].Text;
                        if (cmbRikSolicitud.FindItemIndexByValue(rg1.Items[item]["Id_Rik"].Text) > 0)
                        {
                            cmbRikSolicitud.SelectedIndex = cmbRikSolicitud.FindItemIndexByValue(rg1.Items[item]["Id_Rik"].Text);
                            cmbRikSolicitud.Text = cmbRikSolicitud.FindItemByValue(rg1.Items[item]["Id_Rik"].Text).Text;
                        }
                        else
                        {
                            cmbRikSolicitud.SelectedIndex = 0;
                            cmbRikSolicitud.Text = cmbRik.Items[0].Text;
                            txtRikSolicitud.Text = "";
                        }

                        //

                        txtidLocal.Text = rg1.Items[item]["Id_Local"].Text;



                        txtSegmento.Text = rg1.Items[item]["Id_Seg"].Text == "-1" ? string.Empty : rg1.Items[item]["Id_Seg"].Text;
                        if (cmbSegmento.FindItemIndexByValue(rg1.Items[item]["Id_Seg"].Text) > 0)
                        {
                            cmbSegmento.SelectedIndex = cmbSegmento.FindItemIndexByValue(rg1.Items[item]["Id_Seg"].Text);
                            cmbSegmento.Text = cmbSegmento.FindItemByValue(rg1.Items[item]["Id_Seg"].Text).Text;
                        }
                        else
                        {
                            cmbSegmento.SelectedIndex = 0;
                            cmbSegmento.Text = cmbSegmento.Items[0].Text;
                            txtSegmento.Text = "";
                        }

                        txtTipoCliente.Text = rg1.Items[item]["Id_TipoCliente"].Text == "-1" ? string.Empty : rg1.Items[item]["Id_TipoCliente"].Text;
                        if (cmbTipoCliente.FindItemIndexByValue(rg1.Items[item]["Id_TipoCliente"].Text) > 0)
                        {
                            cmbTipoCliente.SelectedIndex = cmbTipoCliente.FindItemIndexByValue(rg1.Items[item]["Id_TipoCliente"].Text);
                            cmbTipoCliente.Text = cmbTipoCliente.FindItemByValue(rg1.Items[item]["Id_TipoCliente"].Text).Text;
                        }
                        else
                        {
                            cmbTipoCliente.SelectedIndex = 0;
                            cmbTipoCliente.Text = cmbSegmento.Items[0].Text;
                            txtTipoCliente.Text = "";
                        }



                        chkActivo.Checked = Convert.ToBoolean(rg1.Items[item]["Estatus"].Text);
                        GetListDet();
                        rgDet.Rebind();

                        //Verifica si hay autorizacion pendiente de cambio de territorio
                        if (!string.IsNullOrEmpty(txtClave.Text))
                        {

                            CapaDatos.CD_CatTerritorios cd_Territorios = new CapaDatos.CD_CatTerritorios();

                            ModelAutorizacionTerritorios DatosSolicitud = new ModelAutorizacionTerritorios();
                            string CNX = ConfigurationManager.AppSettings["strConnection"];
                            cd_Territorios.ConsultaAutorizacionPendienteTerritorio(int.Parse(txtClave.Text), ref DatosSolicitud, CNX);

                            if (DatosSolicitud.IdAutorizacion != 0)
                            {
                                LblIdSolicitud.Text = DatosSolicitud.IdAutorizacion.ToString();
                                txtRikSolicitud.Text = DatosSolicitud.IdRepresentante.ToString();
                                txtDescripcionSolicitud.Text = DatosSolicitud.Territorio;
                                cmbRikSolicitud.SelectedIndex = cmbRikSolicitud.FindItemIndexByValue(txtRikSolicitud.Text);
                                cmbRikSolicitud.Text = cmbRikSolicitud.FindItemByValue(txtRikSolicitud.Text).Text;
                                LblIdSolicitud.Visible = true;
                                Label6.Visible = true;
                            }
                            else
                            {
                                LblIdSolicitud.Text = DatosSolicitud.ToString();
                                LblIdSolicitud.Visible = false;
                                Label6.Visible = false;
                            }


                        }
                        //
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_ItemCommand");
            }
        }

        protected void rg1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rg1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_PageIndexChanged");
            }
        }
        protected void rgDet_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgDet.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                string anyo = "";
                string anyo_old = "";
                string mes = "";
                string mesStr = "";
                string mes_old = "";
                double presupuesto = 0;
                double presupuesto_old = 0;
                GridItem gi = null;
                DataRow[] Ar_dr;

                switch (e.CommandName)
                {
                    case "PerformInsert":
                        gi = e.Item;


                        if (((RadNumericTextBox)gi.FindControl("RadNumericTextBox1")).Text == "" ||
                            ((RadComboBox)gi.FindControl("RadComboBox1")).SelectedIndex == 0 ||
                            ((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Text == "")
                        {
                            Alerta("El año, mes y presupuesto son obligatorios");
                            e.Canceled = true;
                            break;
                        }


                        anyo = ((RadNumericTextBox)gi.FindControl("RadNumericTextBox1")).Text;
                        mes = ((RadComboBox)gi.FindControl("RadComboBox1")).SelectedValue;
                        mesStr = ((RadComboBox)gi.FindControl("RadComboBox1")).Text;
                        presupuesto = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Text);


                        Ar_dr = dt.Select("Anyo='" + anyo + "' and Mes='" + mes + "'");
                        if (Ar_dr.Length == 0)
                        {
                            dt.Rows.Add(new object[] { anyo, mes, mesStr, presupuesto });
                        }
                        else
                        {
                            Alerta("El presupuesto para ese periodo ya existe.");
                            e.Canceled = true;
                        }
                        break;
                    case "Update":
                        gi = e.Item;

                        if (((RadNumericTextBox)gi.FindControl("RadNumericTextBox1")).Text == "" ||
                            ((RadComboBox)gi.FindControl("RadComboBox1")).SelectedIndex == 0 ||
                            ((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Text == "")
                        {
                            Alerta("El año, mes y presupuesto son obligatorios");
                            e.Canceled = true;
                        }

                        anyo = ((RadNumericTextBox)gi.FindControl("RadNumericTextBox1")).Text;
                        mes = ((RadComboBox)gi.FindControl("RadComboBox1")).SelectedValue;
                        mesStr = ((RadComboBox)gi.FindControl("RadComboBox1")).Text;
                        presupuesto = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Text);
                        anyo_old = ((Label)gi.FindControl("lblold1")).Text;
                        mes_old = ((Label)gi.FindControl("lblold2")).Text;
                        presupuesto_old = Convert.ToDouble(((Label)gi.FindControl("lblold3")).Text);

                        Ar_dr = dt.Select("Anyo='" + anyo + "' and Mes='" + mes + "'");
                        if (Ar_dr.Length != 0)
                        {
                            Alerta("El presupuesto para ese periodo ya existe.");
                            e.Canceled = true;
                            return;
                        }

                        Ar_dr = dt.Select("Anyo='" + anyo_old + "' and Mes='" + mes_old + "' and Presupuesto='" + presupuesto_old + "'");
                        if (Ar_dr.Length > 0)
                        {
                            Ar_dr[0].BeginEdit();
                            Ar_dr[0]["Anyo"] = anyo;
                            Ar_dr[0]["Mes"] = mes;
                            Ar_dr[0]["MesStr"] = mesStr;
                            Ar_dr[0]["Presupuesto"] = presupuesto;
                            Ar_dr[0].AcceptChanges();
                        }
                        break;
                    case "Delete":
                        gi = e.Item;
                        anyo_old = ((Label)gi.FindControl("label1")).Text;
                        mes_old = ((Label)gi.FindControl("Label4")).Text;
                        presupuesto_old = Convert.ToDouble(((Label)gi.FindControl("label3")).Text);
                        Ar_dr = dt.Select("Anyo='" + anyo_old + "' and Mes='" + mes_old + "' and Presupuesto='" + presupuesto_old + "'");
                        if (Ar_dr.Length > 0)
                        {
                            Ar_dr[0].Delete();
                            dt.AcceptChanges();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                //this.rgDet.Rebind();
                rgDet.DataSource = dt;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_PageIndexChanged");
            }
        }
        protected void RadComboBox_DataBinding(object sender, EventArgs e)
        {
            RadComboBox comboBox = ((RadComboBox)sender);
            comboBox.Items.Add(new RadComboBoxItem("-- Seleccionar --", "-1"));
            CultureInfo cultura = CultureInfo.CurrentCulture;

            for (int x = 1; x < 13; x++)
            {
                comboBox.Items.Add(new RadComboBoxItem(cultura.TextInfo.ToTitleCase(cultura.DateTimeFormat.GetMonthName(x)), x.ToString()));
            }
        }
        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked && HF_ID.Value != "")
            {
                if (!Deshabilitar())
                {
                    Alerta("El registro está siendo utilizado por otro componente");
                    ((CheckBox)sender).Checked = true;
                }
            }
        }
        protected void cmbUen_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CargarRik();
                CargarSeg();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //----------------------------------------------------------------------------------------
        //David López 15/04/2014
        //----------------------------------------------------------------------------------------
        /*
        protected void chkAsesor_CheckedChanged(object sender, EventArgs e)
        {
            Asesor(chkAsesor.Checked);
            CargarRik();
            claveTerritorio();
        }
        protected void chkRSC_CheckedChanged(object sender, EventArgs e)
        {
            RSC(chkRSC.Checked);
            CargarRik();
            claveTerritorio();
        }
        
        protected void chkRIK_CheckedChanged(object sender, EventArgs e)
        {
            RIK(chkRIK.Checked);
            CargarRik();
            claveTerritorio();
        }

        protected void chkOTRO_CheckedChanged(object sender, EventArgs e)
        {
            OTRO(chkOTRO.Checked);
            CargarRik();
            claveTerritorio();
        }
        */


        protected void txtUen_TextChanged(object sender, EventArgs e)
        {
            claveTerritorio();
        }
        protected void txtSegmento_TextChanged(object sender, EventArgs e)
        {
            claveTerritorio();
        }
        protected void txtFuncion_TextChanged(object sender, EventArgs e)
        {
            claveTerritorio();
        }
        protected void txtTipoCliente_TextChanged(object sender, EventArgs e)
        {
            claveTerritorio();
        }
        protected void txtidLocal_TextChanged(object sender, EventArgs e)
        {
            //claveTerritorio();
        }

        //----------------------------------------------------------------------------------------

        #endregion
        #region Funciones

        private void Inicializar()
        {
            txtClave.Text = Valor;

            //CreaCBTipoRepresentante(ref tblTipoRepresentante);

            rg1.Rebind();
            CargarUen();
            CargarRik();
            CargarSeg();
            GetListDet();
            CargarTipoCliente();
            mostrarcampos(false);

            rgDet.Rebind();
        }

        //SAUL GUERRA 20150508 BEGIN
        private void CreaCBTipoRepresentante(ref Table table)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            using (CapaNegocios.CN_TipoRepresentante CN_TR = new CapaNegocios.CN_TipoRepresentante(Sesion.Emp_Cnx))
            {
                List<CapaEntidad.TipoRepresentante> TR = CN_TR.ConsultaTipoRepresentantes(0);

                foreach (CapaEntidad.TipoRepresentante e in TR)
                {
                    TableCell cell1 = new TableCell();
                    cell1.Width = 80;

                    RadioButton RB = new RadioButton();
                    RB.ID = e.TipoRepresentante_Descripcion.Replace(" ", "_");
                    RB.GroupName = "Tipos";
                    RB.Text = e.TipoRepresentante_Descripcion;

                    List<string> elementos = new List<string>();
                    foreach (string i in e.Territorializacion.Territorializacion.Split(new Char[] { ',' }))
                    {
                        elementos.Add("\"" + i.Trim().Replace(" ", "_") + "\"");
                    }

                    RB.Checked = (e.Id_TipoRepresentante == Convert.ToInt16("0" + hfTipoRepresentante.Value));
                    RB.CssClass = "RB" + "|" + Convert.ToString(e.Id_TipoRepresentante) + "|" + e.Territorializacion.Id_Territorializacion;
                    RB.Attributes.Add("Id_TipoRep", Convert.ToString(e.Id_TipoRepresentante));

                    EventHandler evh = new EventHandler(RB_CheckedChanged);
                    RB.CheckedChanged += evh;
                    RB.AutoPostBack = true;

                    TableCell cell2 = new TableCell();
                    cell2.Controls.Add(RB);

                    TableRow row = new TableRow();
                    row.Cells.Add(cell1);
                    row.Cells.Add(cell2);
                    table.Rows.Add(row);
                }
            }
        }

        protected void RB_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton RB = (sender as RadioButton);

            hfTipoRepresentante.Value = (RB.CssClass as string).Replace("RB", "").Trim().Split(new Char[] { '|' })[1];
            int TipoRep = Convert.ToInt16("0" + hfTipoRepresentante.Value);
            int Id_Territorializacion = Convert.ToInt16("0" + (RB.CssClass as string).Replace("RB", "").Trim().Split(new Char[] { '|' })[2]);

            CargarRik();
            limpiarcampos();
            mostrarcampos(true);

            if (Id_Territorializacion == 1)
            {
                lblUen.Visible = false;
                txtUen.Visible = false;
                cmbUen.Visible = false;
                lblSegmento.Visible = false;
                txtSegmento.Visible = false;
                cmbSegmento.Visible = false;
            }
        }
        //SAUL GUERRA 20150508 END

        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarUen() //Central
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUen_Combo", ref cmbUen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarRik() //Local
        {
            try
            {
                /*
                int tipoRep=0;
                if (chkOTRO.Checked) { tipoRep = 4; }
                if (chkRIK.Checked) { tipoRep = 3; }
                if (chkAsesor.Checked) { tipoRep = 2; }
                if (chkRSC.Checked) { tipoRep = 1; }
                */

                int tipoRep = Convert.ToInt16("0" + hfTipoRepresentante.Value);

                if (tipoRep != 0)
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Id_TU == 2 ? Sesion.Id_U : (int?)null, cmbUen.SelectedValue == "" ? -1 : Convert.ToInt32(cmbUen.SelectedValue), tipoRep, Sesion.Emp_Cnx, "spCatRik_Combo", ref cmbRik);
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Id_TU == 2 ? Sesion.Id_U : (int?)null, cmbUen.SelectedValue == "" ? -1 : Convert.ToInt32(cmbUen.SelectedValue), tipoRep, Sesion.Emp_Cnx, "spCatRik_Combo", ref cmbRikSolicitud);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarSeg() //Central
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, cmbUen.SelectedValue == "" ? -1 : Convert.ToInt32(cmbUen.SelectedValue), Sesion.Emp_Cnx, "spCatSegmentos_Combo", ref cmbSegmento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTipoCliente() //Central
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatTipoCliente_Combo", ref cmbTipoCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (Permiso.PGrabar == false)
                        this.rtb1.Items[6].Visible = false;
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                        this.rtb1.Items[5].Visible = false;
                    //Regresar
                    this.rtb1.Items[4].Visible = false;
                    //Eliminar
                    this.rtb1.Items[3].Visible = false;
                    //Imprimir
                    this.rtb1.Items[2].Visible = false;
                    //Correo
                    this.rtb1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");
                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Territorios> GetList()
        {
            try
            {
                List<Territorios> List = new List<Territorios>();
                CN_CatTerritorios clsCatTerritorios = new CN_CatTerritorios();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Territorios territorio = new Territorios();
                territorio.Id_Emp = session2.Id_Emp;
                territorio.Id_Cd = session2.Id_Cd_Ver;
                clsCatTerritorios.ConsultaTerritorios(territorio, session2.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetListDet()
        {
            dt = new DataTable();
            DataColumn dc = new DataColumn();
            dt.Columns.Add("Anyo", System.Type.GetType("System.Int32"));
            dt.Columns.Add("Mes", System.Type.GetType("System.Int32"));
            dt.Columns.Add("MesStr", System.Type.GetType("System.String"));
            dt.Columns.Add("Presupuesto", System.Type.GetType("System.Double"));
            if (HF_ID.Value != "")
            {
                CN_CatTerritorios clsCatTerritorios = new CN_CatTerritorios();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                TerritorioDet territorio = new TerritorioDet();
                territorio.Id_Emp = session2.Id_Emp;
                territorio.Id_Cd = session2.Id_Cd_Ver;
                territorio.Id_Ter = Convert.ToInt32(HF_ID.Value);
                DataTable dt2 = dt;
                clsCatTerritorios.ConsultaTerritoriosDet(territorio, session2.Emp_Cnx, ref dt2);
                dt = dt2;
            }
        }
        private void Nuevo()
        {
            habilitarCampos(true);
            txtClave.Text = Valor;
            txtClave.Enabled = true;
            txtDescripcion.Text = string.Empty;
            //
            txtDescripcionSolicitud.Text = string.Empty;
            //
            txtRik.Text = string.Empty;
            txtSegmento.Text = string.Empty;
            txtUen.Text = string.Empty;
            txtTipoCliente.Text = string.Empty;

            if (cmbRik.Items.Count > 0)
            {
                cmbRik.SelectedIndex = 0;
                cmbRik.Text = cmbRik.Items[0].Text;
            }

            if (cmbSegmento.Items.Count > 0)
            {
                cmbSegmento.SelectedIndex = 0;
                cmbSegmento.Text = cmbSegmento.Items[0].Text;
            }

            if (cmbUen.Items.Count > 0)
            {
                cmbUen.SelectedIndex = 0;
                cmbUen.Text = cmbUen.Items[0].Text;
            }

            if (cmbTipoCliente.Items.Count > 0)
            {
                cmbTipoCliente.SelectedIndex = 0;
                cmbTipoCliente.Text = cmbTipoCliente.Items[0].Text;
            }

            //chkAsesor.Checked = false;
            //chkRSC.Checked = false;
            //chkRIK.Checked = false;
            //chkOTRO.Checked = false;

            hfTipoRepresentante.Value = "0";

            limpiarcampos();
            mostrarcampos(false);



            chkActivo.Checked = true;
            HF_ID.Value = string.Empty;
            dt.Rows.Clear();
            rgDet.Rebind();
        }
        private void Guardar()
        {
            try
            {

                int valorValido;

                if (!int.TryParse(Valor, out valorValido))
                {
                    return;
                }

                //if (valorValido > 99)
                //{
                //Alerta("solo se permiten guardar territorios en un rango de 1 a 99");
                //return;
                //}

                CN__Comun.RemoverValidadores(Validators);

                //int TipoRep = 0;
                int TipoRep = Convert.ToInt16("0" + hfTipoRepresentante.Value);

                //-------------------------------
                //    RSC / ASESOR
                //-------------------------------

                //if (TipoRep == 1 || TipoRep == 2) //if (chkRSC.Checked || chkAsesor.Checked)
                //{
                //    if (chkRSC.Checked){TipoRep = 1;}
                //    if (chkAsesor.Checked){TipoRep = 2;}

                //}

                //-------------------------------
                //    RIK / OTRO
                //-------------------------------

                if (TipoRep == 3 || TipoRep == 4)
                {
                    //if (chkRIK.Checked) { TipoRep = 3; }
                    //if (chkOTRO.Checked) { TipoRep = 4; }

                    if (txtUen.Text != string.Empty)
                    { ReqFieldUEN.IsValid = true; }
                    else { ReqFieldUEN.IsValid = false; }

                    if (txtSegmento.Text != string.Empty)
                    { ReqFieldSegmento.IsValid = true; }
                    else { ReqFieldSegmento.IsValid = false; }
                }


                if (txtTipoCliente.Text != string.Empty)
                { ReqFieldTipoCliente.IsValid = true; }
                else { ReqFieldTipoCliente.IsValid = false; }

                if (txtidLocal.Text != string.Empty)
                { ReqFieldIdLocal.IsValid = true; }
                else { ReqFieldIdLocal.IsValid = false; }
                /*
                if (txtRik.Text != string.Empty)
                { ReqFieldRepresentante.IsValid = true; }
                else { ReqFieldRepresentante.IsValid = false; }

                if (txtDescripcion.Text != string.Empty)
                { ReqFieldTerritorio.IsValid = true; }
                else { ReqFieldTerritorio.IsValid = false; }
                */


                if (!ReqFieldUEN.IsValid || !ReqFieldSegmento.IsValid || !ReqFieldTipoCliente.IsValid || !ReqFieldIdLocal.IsValid /*|| !ReqFieldRepresentante.IsValid || !ReqFieldTerritorio.IsValid*/)
                {
                    return;
                }

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Territorios territorio = new Territorios();
                territorio.Id_Emp = session.Id_Emp;
                territorio.Id_Cd = session.Id_Cd_Ver;
                territorio.Descripcion = txtDescripcion.Text;
                if (cmbRik.SelectedValue==""){
                    territorio.Id_Rik = 0;
                } else {
                    territorio.Id_Rik = Convert.ToInt32(cmbRik.SelectedValue);
                }

                territorio.Id_TipoCliente = Convert.ToInt32(cmbTipoCliente.SelectedValue);

                switch (TipoRep)
                {
                    case 1:
                        territorio.Id_Seg = Convert.ToInt32("-1");
                        territorio.Id_Uen = Convert.ToInt32("-1");
                        break;

                    case 2:
                        territorio.Id_Seg = Convert.ToInt32("-1");
                        territorio.Id_Uen = Convert.ToInt32("-1");
                        break;

                    case 3:
                        territorio.Id_Uen = Convert.ToInt32(txtUen.Text);
                        territorio.Id_Seg = Convert.ToInt32(txtSegmento.Text);
                        break;

                    case 4:
                        territorio.Id_Uen = Convert.ToInt32(txtUen.Text);
                        territorio.Id_Seg = Convert.ToInt32(txtSegmento.Text);
                        break;

                    default:
                        break;
                }

                if ((txtRik.Text != txtRikSolicitud.Text) || (txtDescripcion.Text != txtDescripcionSolicitud.Text) || (chkActivo.Checked != chkActivoSolicitud.Checked))
                {
                    //Guardar Solicitud - Si difieren los campos
                    CapaDatos.CD_CatTerritorios cd_Territorios = new CapaDatos.CD_CatTerritorios();
                    CapaEntidad.ModelAutorizacionTerritorios NuevaSolicitud = new CapaEntidad.ModelAutorizacionTerritorios();
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    NuevaSolicitud.IdUSolicita = Sesion.Id_U;
                    NuevaSolicitud.ClaveTerritorio = long.Parse(txtClave.Text);
                    NuevaSolicitud.IdRepresentante = int.Parse(txtRikSolicitud.Text);
                    NuevaSolicitud.Territorio = txtDescripcionSolicitud.Text;
                    NuevaSolicitud.Activo = chkActivoSolicitud.Checked;
                    NuevaSolicitud.IdAutorizacion = 0;//string.IsNullOrEmpty(LblIdSolicitud.Text) ? 0 : long.Parse(LblIdSolicitud.Text);
                    int Respuesta = 0;


                    string CNX = ConfigurationManager.AppSettings["strConnection"];
                    cd_Territorios.GuardarAutorizacionTerritorios(NuevaSolicitud, ref Respuesta, CNX);

                    ModelAutorizacionTerritorios DatosSolicitud = new ModelAutorizacionTerritorios();

                    cd_Territorios.ConsultaAutorizacionPendienteTerritorio(int.Parse(txtClave.Text), ref DatosSolicitud, CNX);
                    EnviaEmail(DatosSolicitud.IdAutorizacion.ToString());

                    if (DatosSolicitud.IdAutorizacion == 0)
                    {
                        ErrorManager("No se guardo la solicitud, vuelva a intentar");
                    }
                }


                territorio.Id_TipoRepresentante = TipoRep;
                territorio.Id_Local = txtidLocal.Text;

                territorio.Estatus = chkActivo.Checked;
                CN_CatTerritorios clsCatSegmentos = new CN_CatTerritorios();
                int verificador = -1;
                if (HF_ID.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    territorio.Id_Ter = Convert.ToInt32(txtClave.Text);
                    territorio.Cve_Terr = Convert.ToInt32(Valor);
                    clsCatSegmentos.InsertarTerritorios(territorio, session.Emp_Cnx, ref verificador);

                    if (verificador == 1)
                    {
                        clsCatSegmentos.InsertarTerritoriosDet(territorio, dt, session.Emp_Cnx, ref verificador);
                        Nuevo();
                        Alerta("Los datos se guardaron correctamente");
                    }
                    else
                        Alerta("La clave ya existe");
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    territorio.Id_Ter = Convert.ToInt32(HF_ID.Value);
                    clsCatSegmentos.ModificarTerritorios(territorio, session.Emp_Cnx, ref verificador);

                    if (verificador == 1)
                    {
                        clsCatSegmentos.ModificarTerritoriosDet(territorio, dt, session.Emp_Cnx, ref verificador);
                        Alerta("Los datos se modificaron correctamente");
                    }
                    else
                        Alerta("Ocurrió un error al intentar modificar los datos");
                }
                rg1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool Deshabilitar()
        {
            try
            {
                bool verificador = false;
                if (HF_ID.Value != "")
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = Sesion.Id_Emp;
                    ct.Id_Cd = Sesion.Id_Cd_Ver;
                    ct.Id = Convert.ToInt32(HF_ID.Value);
                    ct.Tabla = "CatTerritorio";
                    ct.Columna = "Id_Ter";
                    CN_Comun.Deshabilitar(ct, Sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string MaximoId(string prefix)
        {
            try
            {
                Sesion Sesion = new Sesion();

                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                    return "";

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                //int TipoRep = chkRSC.Checked ? 1 : chkAsesor.Checked ? 2 : chkRIK.Checked? 3: 4;
                int TipoRep = Convert.ToInt16("0" + hfTipoRepresentante.Value);
                int TipoCliente = txtTipoCliente.Text.Length > 0 ? Convert.ToInt32(txtTipoCliente.Text) : 0;
                int idUen = txtUen.Text.Length > 0 ? Convert.ToInt32(txtUen.Text) : 0;
                int idSeg = txtSegmento.Text.Length > 0 ? Convert.ToInt32(txtSegmento.Text) : 0;

                /*
                int TipoRep = 0;
                int TipoCliente=0;
                int idUen=0;
                int idSeg=0;

                if (txtTipoCliente.Text.Length>0){TipoCliente = Convert.ToInt32(txtTipoCliente.Text);}
                if (txtUen.Text.Length>0){idUen = Convert.ToInt32(txtUen.Text);}
                if (txtSegmento.Text.Length>0){idSeg = Convert.ToInt32(txtSegmento.Text);}
                

                if (chkRSC.Checked) { TipoRep = 1; }
                if (chkAsesor.Checked){ TipoRep = 2; }
                if (chkRIK.Checked) { TipoRep = 3; }
                if (chkOTRO.Checked) { TipoRep = 4; }
                */

                return CN_Comun.MaximoTerritorio(
                    Sesion.Id_Emp,
                    Sesion.Id_Cd_Ver, TipoRep, TipoCliente, idUen, idSeg,
                    Sesion.Emp_Cnx,
                    "spCatTerritorio_Maximo",
                    prefix
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "0";
        }

        //----------------------------------------------------------------------------------------
        //David López 15/04/2014
        //----------------------------------------------------------------------------------------
        private void Asesor(bool estatus)
        {
            limpiarcampos();
            mostrarcampos(true);

            if (estatus)
            {
                //chkRIK.Checked = false;
                //chkRSC.Checked = false;
                //chkOTRO.Checked = false;

                lblUen.Visible = false;
                txtUen.Visible = false;
                cmbUen.Visible = false;

                lblSegmento.Visible = false;
                txtSegmento.Visible = false;
                cmbSegmento.Visible = false;
            }
            else
            {
                mostrarcampos(false);
            }
        }
        private void RSC(bool estatus)
        {
            limpiarcampos();
            mostrarcampos(true);

            if (estatus)
            {
                //chkRIK.Checked = false;
                //chkAsesor.Checked = false;
                //chkOTRO.Checked = false;

                lblUen.Visible = false;
                txtUen.Visible = false;
                cmbUen.Visible = false;

                lblSegmento.Visible = false;
                txtSegmento.Visible = false;
                cmbSegmento.Visible = false;

            }
            else
            {
                mostrarcampos(false);
            }
        }
        private void RIK(bool estatus)
        {
            limpiarcampos();
            mostrarcampos(true);

            if (estatus)
            {
                //chkAsesor.Checked = false;
                //chkRSC.Checked = false;
                //chkOTRO.Checked = false;
            }
            else
            {
                mostrarcampos(false);
            }
        }
        private void OTRO(bool estatus)
        {
            limpiarcampos();
            mostrarcampos(true);

            if (estatus)
            {
                //chkAsesor.Checked = false;
                //chkRSC.Checked = false;
                //chkRIK.Checked = false;
            }
            else
            {
                mostrarcampos(false);
            }
        }

        private void limpiarcampos()
        {
            CN__Comun.RemoverValidadores(Validators);

            cmbSegmento.SelectedIndex = 0;
            cmbSegmento.Text = cmbSegmento.Items[0].Text;

            cmbUen.SelectedIndex = 0;
            cmbUen.Text = cmbUen.Items[0].Text;

            cmbTipoCliente.SelectedIndex = 0;
            cmbTipoCliente.Text = cmbTipoCliente.Items[0].Text;

            if (cmbRik.SelectedIndex != -1)
            {
                cmbRik.SelectedIndex = 0;
                cmbRik.Text = cmbRik.Items[0].Text;
            }

            if (cmbRikSolicitud.SelectedIndex != -1)
            {
                cmbRikSolicitud.SelectedIndex = 0;
                cmbRikSolicitud.Text = cmbRikSolicitud.Items[0].Text;
            }

            txtSegmento.Text = string.Empty;
            txtUen.Text = string.Empty;
            txtTipoCliente.Text = string.Empty;
            txtRik.Text = string.Empty;
            txtidLocal.Text = string.Empty;

            txtRikSolicitud.Text = string.Empty;

        }
        private void mostrarcampos(bool estatus)
        {
            lblUen.Visible = estatus;
            txtUen.Visible = estatus;
            cmbUen.Visible = estatus;

            lblSegmento.Visible = estatus;
            txtSegmento.Visible = estatus;
            cmbSegmento.Visible = estatus;

            lblTipoCliente.Visible = estatus;
            txtTipoCliente.Visible = estatus;
            cmbTipoCliente.Visible = estatus;

            lblCveTerritorio.Visible = estatus;
            txtClave.Visible = estatus;

            lblidLocal.Visible = estatus;
            txtidLocal.Visible = estatus;

            lblRik.Visible = estatus;
            txtRik.Visible = estatus;
            cmbRik.Visible = estatus;
            Label6.Visible = estatus;

            //
            LabelSolicitudCambio.Visible = estatus;
            LabelTerritorioAutorizado.Visible = estatus;
            //
            lblRikSolicitud.Visible = estatus;
            txtRikSolicitud.Visible = estatus;
            cmbRikSolicitud.Visible = estatus;
            //

            lblTerritorio.Visible = estatus;
            txtDescripcion.Visible = estatus;
            chkActivo.Visible = estatus;

            //
            lblTerritorioSolicitud.Visible = estatus;
            txtDescripcionSolicitud.Visible = estatus;
            chkActivoSolicitud.Visible = estatus;
            //
        }



        private void habilitarCampos(bool estatus)
        {
            txtUen.Enabled = estatus;
            cmbUen.Enabled = estatus;

            txtSegmento.Enabled = estatus;
            cmbSegmento.Enabled = estatus;

            txtTipoCliente.Enabled = estatus;
            cmbTipoCliente.Enabled = estatus;

            //chkRIK.Enabled = estatus;
            //chkRSC.Enabled = estatus;
            //chkAsesor.Enabled = estatus;
            //chkOTRO.Enabled = estatus;
        }

        private void claveTerritorio()
        {

            if (HF_ID.Value == "")
            {
                string claveTerritorio = string.Empty;
                txtClave.Text = claveTerritorio;

                //string TipoRep = string.Empty;
                string TipoRep = hfTipoRepresentante.Value;

                //if (chkRSC.Checked) { TipoRep ="1"; }
                //if (chkAsesor.Checked) { TipoRep ="2"; }
                //if (chkRIK.Checked) { TipoRep ="3"; }
                //if (chkOTRO.Checked) { TipoRep = "4"; }

                claveTerritorio += TipoRep;

                if (txtUen.Text != string.Empty)
                { claveTerritorio += txtUen.Text.PadLeft(2, '0'); }

                if (txtSegmento.Text != string.Empty)
                {


                    List<Segmentos> List = new List<Segmentos>();
                    CN_CatSegmentos clsCatSegmentos = new CN_CatSegmentos();
                    Sesion session2 = new Sesion();
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    clsCatSegmentos.ConsultaSegmentos(session2.Id_Emp, Convert.ToInt32(txtSegmento.Text), session2.Emp_Cnx, ref List);
                    claveTerritorio += Convert.ToString(List[0].Seg_IdXUen).Trim().PadLeft(2, '0');
                    //claveTerritorio += txtSegmento.Text.PadLeft(2, '0');


                }

                if (txtTipoCliente.Text != string.Empty)
                { claveTerritorio += txtTipoCliente.Text.PadLeft(2, '0'); }

                claveTerritorio += MaximoId(claveTerritorio);
                txtClave.Text = claveTerritorio;
            }
        }

        //----------------------------------------------------------------------------------------

        private void EnviaEmail(string IdSolicitud)
        {
            try
            {
                string Link = ConfigurationManager.AppSettings["URLAutrizacionesTerritorio"];
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);
                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append(" <table>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td><img src=\"cid:companylogo\"></td>");
                cuerpo_correo.Append("   <td valign='middle' style='text-decoration: underline'><b><font face= 'Tahoma' size = '4'>Nueva solicitud de Autorizacion Cambio Territorio#" + IdSolicitud + " </font></b></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td colspan='2'><b><font face= 'Tahoma' size = '2'>Solcitud de cambios Territorio</font></b></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td colspan='2'><br><br></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td align='left' colspan='2'><b><font face= 'Tahoma' size = '2' color='#777777'>" + "<a href=' " + Link + "'>Ir al SIAN</a>");
                cuerpo_correo.Append("</font></b></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td colspan='2'><br><br></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append(" </table>");
                cuerpo_correo.Append("</div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);

                string To = configuracion.CorreAprobadoresCambiosTerriotrio;
                m.To.Add(new MailAddress(To));
                m.Subject = "Solicitud autorizacion cambio territorio #" + IdSolicitud;
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }
                m.AlternateViews.Add(vistaHtml);
                sm.Send(m);
            }
            catch (Exception)
            {
                Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
            }
        }

        #endregion
        #region ErrorManager
        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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
                this.lblMensaje.Text = "";
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
                this.lblMensaje.Text = Message;
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
                this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
    }
}
