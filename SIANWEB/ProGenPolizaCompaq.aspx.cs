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

namespace SIANWEB
{
    public partial class ProGenPolizaCompaq : System.Web.UI.Page
    {
        #region eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
                }
                else
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        Inicializar();
                        cmbAnhio.Focus();
                    }
                    else
                        cmbMes.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }    
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {

        }
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            ArrayList verificador = default(ArrayList);
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];
            ProGenPoliza_Compaq filtroPoliza = new ProGenPoliza_Compaq();
            filtroPoliza.FiltroTipo = Convert.ToInt32(cmbTipo.SelectedValue);
            if (!string.IsNullOrEmpty(cmbAnhio.SelectedValue) && cmbAnhio.SelectedValue != "-1")
                filtroPoliza.FiltroAnhio = Convert.ToInt32(cmbAnhio.SelectedValue);
            else
            {
                Alerta("Seleccione un año válido");
                return;
            }
            if (!string.IsNullOrEmpty(cmbMes.SelectedValue) && cmbMes.SelectedValue != "-1")
                filtroPoliza.FiltroMes = Convert.ToInt32(cmbMes.SelectedValue);
            else
            {
                Alerta("Seleccione un mes válido");
                return;
            }
            filtroPoliza.FiltroCuenta1 = txtCuenta1.Text;
            filtroPoliza.FiltroCuenta2 = txtCuenta2.Text;
            filtroPoliza.FiltroCuenta3 = txtCuenta3.Text;
            filtroPoliza.FiltroCmbCentro = Convert.ToInt32(CmbCentro.SelectedValue);
            CN_ProGenPoliza_Compaq clsProGenPoliza = new CN_ProGenPoliza_Compaq();
            clsProGenPoliza.FiltrarProGenPoliza(session, filtroPoliza, ref verificador);
            if (verificador.Count > 0)
            {
                //Alerta("Se generó correctamente el archivo en la ubicación: C:\\polizavta.txt");
                Descargar(verificador);
                return;
            }
            else              
                Alerta("No se pudo crear el archivo, no se encontraron registros");
            return;           
        }
        protected void CmbCentro_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Funciones
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

                //if (Permiso.PAccesar != true)
                //    Response.Redirect("Inicio.aspx");               
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
                CargarAnhios(); 
                CargarMeses();
                CargarCentros();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAnhios()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatCalendarioAnhio_Combo", ref cmbAnhio);
                cmbAnhio.ClearSelection();
                cmbAnhio.SelectedIndex = -1;
                cmbAnhio.SelectedValue = "-1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarMeses()
        {
            try
            {           
                cmbMes.Items.Add(new RadComboBoxItem("-- Seleccionar --", "-1"));
                cmbMes.Items.Add(new RadComboBoxItem("Enero", "1"));
                cmbMes.Items.Add(new RadComboBoxItem("Febrero", "2"));
                cmbMes.Items.Add(new RadComboBoxItem("Marzo", "3"));
                cmbMes.Items.Add(new RadComboBoxItem("Abril", "4"));
                cmbMes.Items.Add(new RadComboBoxItem("Mayo", "5"));
                cmbMes.Items.Add(new RadComboBoxItem("Junio", "6"));
                cmbMes.Items.Add(new RadComboBoxItem("Julio", "7"));
                cmbMes.Items.Add(new RadComboBoxItem("Agosto", "8"));
                cmbMes.Items.Add(new RadComboBoxItem("Septiembre", "9"));
                cmbMes.Items.Add(new RadComboBoxItem("Octubre", "10"));
                cmbMes.Items.Add(new RadComboBoxItem("Noviembre", "11"));
                cmbMes.Items.Add(new RadComboBoxItem("Diciembre", "12"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Descargar(ArrayList arrText)
        {        
            string fic = Server.MapPath(@"TextFiles/polizavta.txt");
            
            StreamWriter sw = new StreamWriter(fic, false, Encoding.UTF8);
            if (arrText.Count > 0)
                for (int i = 0; i < arrText.Count; i++)
                {
                    sw.WriteLine(arrText[i].ToString());                 
                }
            sw.Close();

            //String FileName = "BulkAdd_Instructions.txt";            
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=polizavta.txt;");
            response.TransmitFile(fic);
            response.Flush();
            response.End();  
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