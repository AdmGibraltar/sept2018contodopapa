using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using Telerik.Reporting.Processing;

namespace SIANWEB
{
    public partial class RepCumplimientoVISemanal : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private Sesion sesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;
            }
        }
        #endregion Variables
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
                        this.ValidarPermisos();
                       
                        

                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.CargarCentros();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                        CargarCombos();
                      
                        this.TblEncabezado.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        #region Eventos
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

           
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        //this.rgFacturaRuta.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                string Evento = Request.Form["__EVENTTARGET"].ToString();

                if (!_PermisoImprimir)
                {
                    this.Alerta("No tiene permisos para ver el reporte");
                    return;
                }
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (Page.IsValid)
                {
                    if (btn.CommandName == "print")
                    {
                        Imprimir();
                    }

                }
            }
            catch (Exception ex)
            {
                Alerta("Imposible generar el reporte; aún no se han generado los respaldos del mes y año seleccionados");
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
            }
            CN__Comun comun = new CN__Comun();
            comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            //nuevo();
        }

        protected void RadComboBoxAño_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ActualizaSemanas();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void cmbMes_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ActualizaSemanas();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //txtNumCliente_TextChanged
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
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
                txtNumeroCliente.Text = string.Empty;
                txtNombreCliente.Text = string.Empty;
            }
        }

        #endregion Eventos

        #region Funciones
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
                _PermisoImprimir = true;
                if (Permiso.PAccesar == true)
                    _PermisoImprimir = Permiso.PImprimir;
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
                    //  this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
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
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();
                //  cn_comun.LlenaCombo(0, 1, sesion.Emp_Cnx, "spCatUen_Combo", ref cmbUEN);
                //  cn_comun.LlenaCombo(3,1, sesion.Emp_Cnx, "spCatSegmentos_Combo", ref cmbSegmento);
                cn_comun.LlenaCombo(1, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatRik_ComboTodos", ref this.cmbRIK);
                cn_comun.LlenaCombo(1, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatTerritorio_ComboTodos", ref this.cmbTer);
                //  cn_comun.LlenaCombo(1, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatCliente_ComboTodos", ref this.cmbCte);

                this.TxtAnio.Text = DateTime.Now.Year.ToString();
                /// this.TxtMes.Text = DateTime.Now.Month.ToString();
                this.cmbMes.SelectedValue = DateTime.Now.Month.ToString();
                ActualizaSemanas();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void Imprimir()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                if (this.TxtAnio.Text == string.Empty)
                {
                    Alerta("Ingrese el año.");
                    return;
                }

                    
                if (this.cmbMes.SelectedValue == "-1")
                {
                    Alerta("Seleccione el mes.");
                    return;
                }

                if (this.chklstSemanas.SelectedValue == "-1")
                {
                    Alerta("Seleccione al menos una semana.");
                    return;
                }

                string Semanas = "";
                string strSemanas = "";
                string strSem1 = "";
                string strSem2 = "";
                string strSem3 = "";
                string strSem4 = "";
                string strSem5 = "";
                int s = 1;
                foreach (ListItem chk in this.chklstSemanas.Items)
                {
                    if (chk.Selected)
                    {
                        Semanas = chk.Value.ToString() + "," + Semanas;
                        strSemanas = strSemanas + " | " + chk.Text.ToString();
                        if (s == 1) strSem1 = chk.Text.ToString();
                        if (s == 2) strSem2 = chk.Text.ToString();
                        if (s == 3) strSem3 = chk.Text.ToString();
                        if (s == 4) strSem4 = chk.Text.ToString();
                        if (s == 5) strSem5 = chk.Text.ToString();
                        s = s + 1;
                    }
                }

                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(this.TxtAnio.Text);
                ALValorParametrosInternos.Add(this.cmbMes.SelectedValue);
                ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(Semanas);
                ALValorParametrosInternos.Add(strSemanas);
                //  ALValorParametrosInternos.Add(null);        //  this.cmbUEN
                //  ALValorParametrosInternos.Add(null);        //  this.cmbSegmento
                ALValorParametrosInternos.Add(this.cmbRIK.SelectedValue == "-1" || this.cmbRIK.SelectedValue == "" ? null : this.cmbRIK.SelectedValue);
                ALValorParametrosInternos.Add(this.cmbTer.SelectedValue == "-1" || this.cmbTer.SelectedValue == "" ? null : this.cmbTer.SelectedValue);
                //  (this.cmbTer.SelectedValue == "-1" || this.cmbTer.SelectedValue == "" ? null : this.cmbTer.SelectedValue);
                ALValorParametrosInternos.Add(this.txtNumeroCliente.Text == "" ? null : this.txtNumeroCliente.Text); //  cmbCte.SelectedValue == "-1"
                ALValorParametrosInternos.Add(this.RblTipo.SelectedValue);
                ALValorParametrosInternos.Add(sesion.U_Nombre);
                ALValorParametrosInternos.Add(this.cmbRIK.Text);
                ALValorParametrosInternos.Add(this.cmbTer.Text);
                ALValorParametrosInternos.Add(this.txtNombreCliente.Text == "" ? null : this.txtNombreCliente.Text);      //  (this.cmbCte.Text);
                ALValorParametrosInternos.Add(this.cmbMes.SelectedItem.Text);
                ALValorParametrosInternos.Add(strSem1);
                ALValorParametrosInternos.Add(strSem2);
                ALValorParametrosInternos.Add(strSem3);
                ALValorParametrosInternos.Add(strSem4);
                ALValorParametrosInternos.Add(strSem5);

                Type instance = null;

                if (RblTipo.SelectedValue == "1")
                {
                    instance = typeof(LibreriaReportes.RepCumplimientoVISemanal);
                }
                else
                {
                    instance = typeof(LibreriaReportes.RepCumplimientoVISemDeta);
                }

                Session["InternParameter_Values" + Session.SessionID + HF_Cve.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_Cve.Value] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_Cve.Value + "');");
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private string NombreMes()
        {
            try
            {

                int Mes = int.Parse(TxtMes.Text);

                if (Mes == 1)
                {
                    return "Enero";
                }
                else if (Mes == 2)
                {
                    return "Febrero";
                }
                else if (Mes == 3)
                {
                    return "Marzo";
                }
                else if (Mes == 4)
                {
                    return "Abril";
                }
                else if (Mes == 5)
                {
                    return "Mayo";
                }
                else if (Mes == 6)
                {
                    return "Junio";
                }
                else if (Mes == 7)
                {
                    return "Julio";
                }
                else if (Mes == 8)
                {
                    return "Agosto";
                }
                else if (Mes == 9)
                {
                    return "Septiembre";
                }
                else if (Mes == 10)
                {
                    return "Octubre";
                }
                else if (Mes == 11)
                {
                    return "Noviembre";
                }
                else 
                {
                    return "Diciembre";
                }
             
              
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void ActualizaSemanas()
        {
            try
            {
                int año;    // pone el año actual
                //  int.TryParse(RadComboBoxAño.SelectedValue, out año);
                int.TryParse(this.TxtAnio.Text, out año);
                if (año == 0)
                {
                    año = DateTime.Now.Year;
                    RadComboBoxAño.SelectedValue = año.ToString();
                }
                List<Calendario> calendarios = new List<Calendario>();
                CN_CatCalendario cn_calendario = new CN_CatCalendario();
                Calendario calendario = new Calendario();
                Sesion session = new Sesion();
                int Id_Calendario;
                session = (Sesion)Session["Sesion" + Session.SessionID];
                cn_calendario.VerificaCalendario(ref calendario, año, cmbMes.SelectedIndex, session, ref calendarios);
                if (calendarios.Count >= 1)
                {
                    Id_Calendario = 0;
                    foreach (Calendario calen in calendarios)
                    {
                        Id_Calendario = calen.Id_Cal;
                    }
                
                    List<Semana> List = new List<Semana>();
                    CN_CatSemana cn_catSemana = new CN_CatSemana();
                    Semana semana = new Semana();

                    cn_catSemana.ConsultaSemanaRep(ref semana, año, cmbMes.SelectedIndex, session, ref List);
                // con List se llena el Checkboxlist con las semanas..
                    this.chklstSemanas.Items.Clear();

                    
                    //Listado.DataValueField = "Id";
                    //Listado.DataTextField = "Descripcion";
                    //Listado.DataBind();

                    this.chklstSemanas.DataSource = List;
                    this.chklstSemanas.DataValueField = "Id_Sem";
                    this.chklstSemanas.DataTextField = "Rango";
                    this.chklstSemanas.DataBind();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        
        #endregion Funciones

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

        private void Alerta2(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 600, 150);");
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

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("Impresion_error"))
                    Alerta("Error al momento de imprimir");
                else
                    Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        #endregion

     
    }
}