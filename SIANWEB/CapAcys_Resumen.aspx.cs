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
using Telerik.Charting;

namespace SIANWEB
{
    public partial class CapAcys_Resumen : System.Web.UI.Page
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
                        //LblSitio.Text = Sesion.Cd_Nombre.ToString();
                        DateTime fechatemp = DateTime.Today;
                        DateTime fecha1, fecha2;

                        fecha1 = new DateTime(fechatemp.Year, fechatemp.Month, 1);

                        if (fechatemp.Month + 1 < 13)
                        { fecha2 = new DateTime(fechatemp.Year, fechatemp.Month + 1, 1).AddDays(-1); }
                        else
                        { fecha2 = new DateTime(fechatemp.Year + 1, 1, 1).AddDays(-1); }

                        RadDatePicker1.SelectedDate = fecha1;
                        RadDatePicker2.SelectedDate = fecha2;

                        Get_Resumen();

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
                    if (btn.CommandName == "Consultar")
                    {
                        Get_Resumen();
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
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
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


                cn_comun.LlenaCombo(1, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatTerritorio_ComboTodos", ref this.cmbTer);
                cn_comun.LlenaCombo(1, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatCliente_ComboTodos", ref this.cmbCte);
                cn_comun.LlenaCombo(1, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatRik_ComboTodos", ref this.cmbRep);

                //this.TxtAnio.Text = DateTime.Now.Year.ToString();
                //this.TxtMes.Text = DateTime.Now.Month.ToString();

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

               
                Type instance = null;

                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private void Get_Resumen()
        {
            try
            {
                List<Acys> List = new List<Acys>();
                CN_CapAcys clsCapAcys_Resumen = new CN_CapAcys();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Acys acys = new Acys();

                acys.Id_Emp = session2.Id_Emp;
                acys.Id_Cd = session2.Id_Cd_Ver;
                acys.Filtro_FecIni = RadDatePicker1.SelectedDate;
                acys.Filtro_FecFin = RadDatePicker2.SelectedDate;
               
                if (cmbTer.SelectedValue == "" || cmbTer.SelectedValue == "-1")
                { acys.Id_Ter = 0; }
                else
                {
                    acys.Id_Ter = Convert.ToInt32(cmbTer.SelectedValue);
                }

                acys.Id_Cte = 0; 


                if (cmbRep.SelectedValue == "" || cmbRep.SelectedValue == "-1")
                { acys.Id_Rik = 0; }
                else
                {
                    acys.Id_Rik = Convert.ToInt32(cmbRep.SelectedValue);
                }
                
                DataTable dt = new DataTable();
              
                dt.TableName = "MasterTable";


                clsCapAcys_Resumen.ConsultarAcys_Rpt_Resumen(acys, session2.Emp_Cnx, ref dt);

                int Cant_Capturado = 0; int Cant_Cancelados = 0; int Cant_Autorizado = 0; int Cant_Solicitado = 0; int Cant_Rechazado = 0;
                int Cant_NI = 0;

                foreach (DataRow row in dt.Rows) // Loop over the rows.
                {

                    switch (row["Acs_Estatus"].ToString())
                    {
                        case "A"://Autorizado
                            Cant_Autorizado = Cant_Autorizado + 1;
                            break;
                        case "B"://Cancelado
                            Cant_Cancelados = Cant_Cancelados + 1;
                            break;
                        case "C"://Capturado
                            Cant_Capturado = Cant_Capturado + 1;
                            break;
                        case "S"://Solicitado
                            Cant_Solicitado = Cant_Solicitado + 1;
                            break;
                        case "R"://Rechazado
                            Cant_Rechazado = Cant_Rechazado + 1;
                            break;

                        default:
                            Cant_NI = Cant_NI + 1;
                            break;
                    }
                }
                lblAutorizados_Valor.Text = Convert.ToString(Cant_Autorizado);
                lblCapturados_Valor.Text = Convert.ToString(Cant_Capturado);
                lblCancelados_Valor.Text = Convert.ToString(Cant_Cancelados);
                lblSolicitado_Valor.Text = Convert.ToString(Cant_Solicitado);
                lblRechazado_Valor.Text = Convert.ToString(Cant_Rechazado);

                



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }    
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            Get_Resumen();

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