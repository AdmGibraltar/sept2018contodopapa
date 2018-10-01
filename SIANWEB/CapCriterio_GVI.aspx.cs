using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using System.IO;
using System.Runtime.Remoting;
using System.Text;
using Telerik.Reporting.Processing;

namespace SIANWEB
{
    public partial class CapCriterio_GVI : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion

        #region Metodos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
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
                        CargarCombos();
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                        rgCriterios.DataSource = GetList();

                        //Eliminar
                        this.rtb1.Items[1].Visible = false;
                        //Regresar
                        this.rtb1.Items[2].Visible = false;
                        //Guardar
                        this.rtb1.Items[3].Visible = true;
                        //Nuevo
                        this.rtb1.Items[4].Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void cmbCentrosDist_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }

        public void txtNumCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtNumeroCliente.Text != "")
                {
                    ErrorManager();
                    Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];

                    Clientes cliente = new Clientes();
                    cliente.Id_Emp = gSession.Id_Emp;
                    cliente.Id_Cd = gSession.Id_Cd_Ver;
                    cliente.Id_Cte = Convert.ToInt32((sender as RadNumericTextBox).Value.HasValue ? (sender as RadNumericTextBox).Value.Value : -1);
                    new CN_CatCliente().ConsultaClientes(ref cliente, gSession.Emp_Cnx);
                    txtNombreCliente.Text = cliente.Cte_NomComercial;

                    this.cmbRSC.SelectedValue = "27";
                    this.cmbRSC.Enabled = false;

                    CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();
                    string strVisita = "0";
                    CN_Citas.ObtieneTipoVisitaCte(gSession.Id_Emp, gSession.Id_Cd, Convert.ToInt32(cliente.Id_Cte), gSession.Emp_Cnx, ref strVisita);
                    this.cmbTipoVisita.SelectedValue = strVisita;
                    if (strVisita != "0")
                    {
                        this.cmbTipoVisita.Enabled = false;
                    }
                    else
                    {
                        this.cmbTipoVisita.Enabled = true;
                    }
                    if (strVisita == "1")
                    {
                        chklstPreRequisitos.Enabled = true;
                    }
                    else
                    {
                        chklstPreRequisitos.Enabled = false;
                    }

                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
                txtNumeroCliente.Text = string.Empty;
                txtNombreCliente.Text = string.Empty;
            }
        }

        #endregion

        #region Eventos

        protected void rgCriterios_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgCriterios.DataSource = GetList();
            }
            catch (Exception ex)
            {
                Alerta(string.Concat(ex.Message, "rgCriterios_NeedDataSource"));
            }
        }

        protected void rgCriterios_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgCriterios.Rebind();
            }
            catch (Exception ex)
            {
                Alerta(string.Concat(ex.Message, "rgCriterios_PageIndexChanged"));
            }
        }

        private List<CriterioCita> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<CriterioCita> listCriterioCita = new List<CriterioCita>();
                CN_CatCriterioCitas clsCatCriterioCitas = new CN_CatCriterioCitas();
                CriterioCita criterioCita = new CriterioCita();
                clsCatCriterioCitas.ConsultaCriteriosCita(criterioCita, sesion.Emp_Cnx, ref listCriterioCita);
                return listCriterioCita;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
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
                        this.txtNumeroCliente.Enabled = true;
                    }
                    if (btn.CommandName == "new")
                    {
                        Nuevo();
                    }
                    if (btn.CommandName == "undo")
                    {
                        //Regresar()
                        Nuevo();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }

        protected void rgCriterios_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.CommandName.ToString() == "Modificar")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        CN__Comun.RemoverValidadores(Validators);
                        foreach (ListItem iteem in chklstPreRequisitos.Items)
                        {
                            iteem.Selected = false;
                        }
                        chklstPreRequisitos.Enabled = false;
                        this.HF_ID.Value = rgCriterios.Items[item]["Id_CriterioCita"].Text;
                        this.txtNumeroCliente.Text = rgCriterios.Items[item]["Id_Cliente"].Text;
                        this.txtNombreCliente.Text = rgCriterios.Items[item]["Cliente"].Text;
                        this.txtNumeroCliente.Enabled = false;
                        this.cmbFrecuencia.SelectedValue = rgCriterios.Items[item]["Id_Frecuencia"].Text;
                        this.cmbTipoVisita.SelectedValue = rgCriterios.Items[item]["Id_TipoVisita"].Text;
                        this.cmbRSC.SelectedValue = rgCriterios.Items[item]["Id_RSC"].Text;

                        CN_CatRequisitoCitas clsRequi = new CN_CatRequisitoCitas();
                        List<RequisitoCita> ListadoReq = new List<RequisitoCita>();
                        Sesion session = new Sesion();
                        session = (Sesion)Session["Sesion" + Session.SessionID];
                        //  string ssps = "spRequisitosCitas_Consulta " + rgCriterios.Items[item]["Id_CriterioCita"].Text;
                        clsRequi.ListadoPrerequisitos_Todos(session.Emp_Cnx, rgCriterios.Items[item]["Id_CriterioCita"].Text, ref ListadoReq);

                        foreach (ListItem iteem in chklstPreRequisitos.Items)
                        {
                            foreach (RequisitoCita Reqqui in ListadoReq)
                            {
                                if (iteem.Text == Reqqui.PreRequisito)
                                {
                                iteem.Selected = true;
                                chklstPreRequisitos.Enabled = true;
                                break;
                                }
                            }
                        }
                        if (rgCriterios.Items[item]["FechaInicial"].Text != null)
                        {
                            this.txtFechaini.SelectedDate = Convert.ToDateTime(rgCriterios.Items[item]["FechaInicial"].Text);
                        }

                        this.chkTodoElDia.Checked = true;   //   Convert.ToBoolean(rgCriterios.Items[item]["Fam_Activo"].Text);
                        this.rtb1.Items[2].Visible = true;
                        this.rtb1.Items[3].Visible = true;
                        this.rtb1.Items[4].Visible = false;
                    }
                }

                if (e.CommandName.ToString() == "Calendarizar")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        CN__Comun.RemoverValidadores(Validators);
                        foreach (ListItem iteem in chklstPreRequisitos.Items)
                        {
                            iteem.Selected = false;
                        }
                        chklstPreRequisitos.Enabled = false;
                        this.HF_ID.Value = rgCriterios.Items[item]["Id_CriterioCita"].Text;
                        this.txtNumeroCliente.Text = rgCriterios.Items[item]["Id_Cliente"].Text;
                        this.txtNombreCliente.Text = rgCriterios.Items[item]["Cliente"].Text;
                        this.txtNumeroCliente.Enabled = false;
                        this.cmbFrecuencia.SelectedValue = rgCriterios.Items[item]["Id_Frecuencia"].Text;
                        this.cmbFrecuencia.Enabled = false;
                        this.cmbTipoVisita.SelectedValue = rgCriterios.Items[item]["Id_TipoVisita"].Text;
                        this.cmbTipoVisita.Enabled = false;
                        this.cmbRSC.SelectedValue = rgCriterios.Items[item]["Id_RSC"].Text;
                        this.cmbTipoVisita.Enabled = false;

                        CN_CatRequisitoCitas clsRequi = new CN_CatRequisitoCitas();
                        List<RequisitoCita> ListadoReq = new List<RequisitoCita>();
                        Sesion session = new Sesion();
                        session = (Sesion)Session["Sesion" + Session.SessionID];
                        //  string ssps = "spRequisitosCitas_Consulta " + rgCriterios.Items[item]["Id_CriterioCita"].Text;
                        clsRequi.ListadoPrerequisitos_Todos(session.Emp_Cnx, rgCriterios.Items[item]["Id_CriterioCita"].Text, ref ListadoReq);

                        foreach (ListItem iteem in chklstPreRequisitos.Items)
                        {
                            foreach (RequisitoCita Reqqui in ListadoReq)
                            {
                                if (iteem.Text == Reqqui.PreRequisito)
                                {
                                    iteem.Selected = true;
                                    chklstPreRequisitos.Enabled = true;
                                    break;
                                }
                            }
                        }
                        if (rgCriterios.Items[item]["FechaInicial"].Text != null)
                        {
                            this.txtFechaini.SelectedDate = Convert.ToDateTime(rgCriterios.Items[item]["FechaInicial"].Text);
                        }
                        this.chkTodoElDia.Checked = true;
                        this.rtb1.Items[2].Visible = true;
                        this.rtb1.Items[3].Visible = true;
                        this.rtb1.Items[4].Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                Alerta(string.Concat(ex.Message, "rgCriterios_ItemCommand"));
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
                //Permiso Permiso = new Permiso();
                //Permiso.Id_U = Sesion.Id_U;
                //Permiso.Id_Cd = Sesion.Id_Cd;
                //Permiso.Sm_cve = pagina.Clave;
                ////Esta clave depende de la pantalla

                //CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                //CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                //if (Permiso.PAccesar == true)
                //{
                //    /*  _PermisoGuardar = Permiso.PGrabar;
                //      _PermisoModificar = Permiso.PModificar;
                //      _PermisoEliminar = Permiso.PEliminar;
                //      _PermisoImprimir = Permiso.PImprimir;*/

                //    if (Permiso.PGrabar == false)
                //        this.rtb1.Items[1].Visible = false;
                //}
                //else
                //    Response.Redirect("Inicio.aspx");



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Funciones

        private void Nuevo()
        {
            try
            {
                this.cmbFrecuencia.Enabled =true;
                this.cmbTipoVisita.Enabled = true;
                this.cmbRSC.Enabled = true;
                this.cmbFrecuencia.SelectedValue = "0";
                this.cmbTipoVisita.SelectedValue = "0";
                this.cmbRSC.SelectedValue = "0";
                this.txtNumeroCliente.Enabled = true;
                this.txtNumeroCliente.Text = "";
                this.txtNombreCliente.Text = "";
                this.chkTodoElDia.Checked = true;
                HF_ID.Value = "";
                foreach (ListItem iteem in chklstPreRequisitos.Items)
                {
                    iteem.Selected = false;
                }
                chklstPreRequisitos.Enabled = false;
                this.txtFechaini.SelectedDate = null;

                this.rtb1.Items[2].Visible = false;
                this.rtb1.Items[3].Enabled = true;
                this.rtb1.Items[4].Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

        private void CargarCombos()
        {
            try
            {
                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun CN_Comun = new CN__Comun();
                CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();

                CN_Comun.LlenaCombo(gSession.Emp_Cnx, "spCatTipoVisita_Todos", ref this.cmbTipoVisita);
                this.cmbTipoVisita.Items.Add(new RadComboBoxItem("-- Seleccione --", "0"));
                this.cmbTipoVisita.SelectedValue = "0";

                CN_Comun.LlenaCombo(gSession.Emp_Cnx, "spCatFrecuenciaVisita_Todos", ref this.cmbFrecuencia);
                this.cmbFrecuencia.Items.Add(new RadComboBoxItem("-- Seleccione --", "0"));
                this.cmbFrecuencia.SelectedValue = "0";

                CN_Comun.LlenaCombo(gSession.Id_Emp, gSession.Id_Cd, gSession.Emp_Cnx, "spRSC_Todos", ref this.cmbRSC, false);
                this.cmbRSC.Items.Add(new RadComboBoxItem("-- Seleccione --", "0"));
                this.cmbRSC.SelectedValue = "0";

                CN_Citas.ListadoPrerequisitos_Todos(gSession.Emp_Cnx, "spCatPreRequisitos_Todos", ref this.chklstPreRequisitos);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LimpiaCampos()
        {
            try
            {
                this.txtNumeroCliente.Text = string.Empty;
                this.txtNombreCliente.Text = string.Empty;
                this.cmbFrecuencia.SelectedValue = "0";
                this.cmbTipoVisita.SelectedValue = "0";
                this.cmbRSC.SelectedValue = "0";
                foreach (ListItem iteem in chklstPreRequisitos.Items)
                {
                    iteem.Selected = false;
                }
                this.txtFechaini.SelectedDate = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Guardar()
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                CriterioCita Cita = new CriterioCita();
                Cita.Id_Emp = session.Id_Emp;
                Cita.Id_Cd = session.Id_Cd_Ver;
                Cita.Id_Cliente = Convert.ToInt32(this.txtNumeroCliente.Text);
                Cita.Id_Frecuencia = Convert.ToInt32(this.cmbFrecuencia.SelectedValue);
                Cita.Id_TipoVisita = Convert.ToInt32(this.cmbTipoVisita.SelectedValue);
                Cita.Id_RSC = Convert.ToInt32(this.cmbRSC.SelectedValue);

                //  this.chklstPreRequisitos
                foreach (ListItem itemA in this.chklstPreRequisitos.Items)
                {
                    if (itemA.Selected)
                    {
                        Cita.TienePreRequi = 1;
                        break;
                    }
                }

                CN_CatCriterioCitas clsCita = new CN_CatCriterioCitas();
                int verificador = -1;
                
                //se graba en base al cliente
                clsCita.InsertarCriteriosCita(Cita, session.Emp_Cnx, ref verificador);
                if (verificador != 0)
                    {
                        Cita.Id_CriterioCita = verificador;     //  Convert.ToInt32(HF_ID.Value);
                        List<RequisitoCita> ListadoReq = new List<RequisitoCita>();
                        RequisitoCita Requi = new RequisitoCita();
                        // obtiene los datos de los prerequisitos
                        foreach (ListItem item in this.chklstPreRequisitos.Items)
                        {
                            if (item.Selected)
                            {
                                Requi = new RequisitoCita();
                                Requi.Id_CriterioCita = verificador;
                                Requi.IdPreRequisito = Convert.ToInt32(item.Value);
                                ListadoReq.Add(Requi);
                            }
                        }
                        if (ListadoReq.Count != 0)
                        {
                            CN_CatRequisitoCitas clsRequi = new CN_CatRequisitoCitas();
                            clsRequi.InsertarRequisitosCita(ListadoReq,session.Emp_Cnx,ref verificador);
                        }
                        Alerta("Los datos se guardaron correctamente.");
                        if (this.txtFechaini.SelectedDate != null)
                        {// checar lo de la fecha...
                            
                            Cita.FechaInicial = Convert.ToDateTime(this.txtFechaini.SelectedDate.ToString());
                            Cita.Usuario = session.Id_U;
                            clsCita.AsignarFechaCriteriosCita(Cita, session.Emp_Cnx, ref verificador);
                            if (verificador == 1)
                            {
                                Alerta("Se genero la calendarización correctamente.");
                            }
                        }
                        Nuevo();
                    }
                this.rgCriterios.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
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