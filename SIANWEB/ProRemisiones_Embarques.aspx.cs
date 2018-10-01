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

namespace SIANWEB
{
    public partial class ProRemisiones_Embarques : System.Web.UI.Page
    {
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
                    
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
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
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                                Sesion sesion = new Sesion();                sesion = (Sesion)Session["Sesion" + Session.SessionID];                if (sesion == null)                {                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);                                        Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);                }                CN__Comun comun = new CN__Comun();                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
              
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {           
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)                
                    rgRemisiones.DataSource = GetList();                
            }
            catch (Exception ex)
            {
               ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }
        protected void rgRemisiones_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {          
                if (e.CommandName.ToString() == "Autorizar")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        CN__Comun.RemoverValidadores(Validators);
                        EmbarquesRemision embarque = new EmbarquesRemision();
                        embarque.Id_Rem = Convert.ToInt32(rgRemisiones.Items[item]["Id_Rem"].Text);
                        embarque.Num_Cliente = Convert.ToInt32(rgRemisiones.Items[item]["Num_Cliente"].Text);            
                        embarque.Pedido = Convert.ToInt32(rgRemisiones.Items[item]["Pedido"].Text);
                        Autorizar(embarque);
                    }
                }               
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgRemisiones_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgRemisiones.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (dpFecha1.SelectedDate > dpFecha2.SelectedDate)
            {
                Alerta("La fecha inicial no debe ser mayor a la fecha final");
                return;
            }
            if (txtCliente.Value > txtCliente2.Value)
            {
                Alerta("El número de cliente inicial no debe ser mayor al número de cliente final");
                return;
            }
            try
            {
                this.rgRemisiones.Rebind();   
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgRemisiones_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                WebControl Button = default(WebControl);
                string clickHandler = string.Empty;

                Button = (WebControl)item["Autorizar"].Controls[0];
                clickHandler = Button.Attributes["onclick"];
                Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Rem").ToString());
            }
        }

        #endregion    
        #region Funciones
        private void Inicializar()
        {
            try
            {
                rgRemisiones.Rebind();               
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
        private void Nuevo()
        {
            try
            {
                txtCliente.Text = string.Empty;
                txtNombre.Text = string.Empty;               
                dpFecha1.DateInput.Text = string.Empty;                
                dpFecha2.DateInput.Text = string.Empty;
                dpFecha1.Clear();
                dpFecha2.Clear();
                HF_ID.Value = string.Empty;
                HF_PED.Value = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Autorizar(EmbarquesRemision embarques)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                CD_EmbarquesRemision clsEmbarquesRem = new CD_EmbarquesRemision();
                int verificador = -1;
                               
                clsEmbarquesRem.ModificarProEmbarquesRemision(session.Id_Emp, session.Id_Cd_Ver, session.Id_U, embarques, session.Emp_Cnx, ref verificador);
                if (verificador == 1)
                    Alerta("La remisión # " + embarques.Id_Rem.ToString() + " fue autorizada correctamente");
                else
                    Alerta("No se pudo autorizar la remisión");

                rgRemisiones.Rebind();
               // Nuevo();               
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
                }
                else
                    Response.Redirect("Inicio.aspx");       
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EmbarquesRemision> GetList()
        {
            try
            {
                List<EmbarquesRemision> List = new List<EmbarquesRemision>();
                CN_EmbarquesRemision clsEmbarquesRem = new CN_EmbarquesRemision();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                EmbarquesRemision embarquefiltro = new EmbarquesRemision();

                embarquefiltro.Filtro_Nombre = txtNombre.Text;
                embarquefiltro.Filtro_Id_Cte = txtCliente.Text;
                embarquefiltro.Filtro_Id_Cte2 = txtCliente2.Text;
                embarquefiltro.Filtro_FecIni = dpFecha1.SelectedDate.HasValue ? dpFecha1.SelectedDate.Value.ToString("dd/MM/yyyy") : "";
                embarquefiltro.Filtro_FecFin = dpFecha2.SelectedDate.HasValue ? dpFecha2.SelectedDate.Value.ToString("dd/MM/yyyy") : "";
                
                clsEmbarquesRem.ConsultaEmbarquesRemision(session2.Id_Emp, session2.Id_Cd_Ver, session2.Emp_Cnx, embarquefiltro, ref List);
                return List;
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