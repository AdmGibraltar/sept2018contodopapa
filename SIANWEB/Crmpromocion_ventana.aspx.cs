using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Data;

namespace SIANWEB
{
    public partial class Crmpromocion_ventana : System.Web.UI.Page
    {
        public int valorGrid = 0;
        public int Cliente = 0;
        public int id_Ter = 0;
        public int id_Uen = 0;
        public int id_Rik = 0;
        public int id_Seg = 0;//solo saltillo
        public Sesion session
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
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (session == null)
                {
                    CerrarVentana();
                }
                else
                {
                    if (ValidarSesion())
                        if (!Page.IsPostBack)
                        {
                            if (session.Cu_Modif_Pass_Voluntario == false)
                                return;
                            Inicializar();
                            txtBuscaCliente.Focus();
                        }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void dgClientes_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    dgClientes.DataSource = GetListClientes();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "dgClientes_NeedDataSource");
            }
        }
        protected void dgClientes_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.CommandName.ToString() == "Select")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        CN__Comun.RemoverValidadores(Validators);
                        HF_ID.Value = dgClientes.Items[item]["Id_Cte"].Text;
                        if (!string.IsNullOrEmpty(HF_ID.Value)) 
                        {
                            txtNoCliente.Text = HF_ID.Value;
                            txtBuscaCliente.Text = dgClientes.Items[item]["NombreCte"].Text;
                       }   
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ibtnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            dgClientes.Rebind();
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                int cte = !string.IsNullOrEmpty(txtNoCliente.Value.ToString()) ? Convert.ToInt32(txtNoCliente.Value) : 0;
                valorGrid = validador(cte);
                if (cte == 0 || valorGrid == 0)
                    Alerta("Seleccione un cliente válido");
                else
                {//retornar valor..
                    Session["NumCliente" + Session.SessionID] = cte.ToString();
                    Session["NombreCliente" + Session.SessionID] = txtBuscaCliente.Text;
                    string funcion = "returnToParent(" + cte.ToString() + ")";
                    string script = "<script>" + funcion + "</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
                }
            }
            catch (Exception)
            {
                throw;
            }           
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CerrarVentana();  
            }
            catch (Exception)
            {
                
                throw;
            }           
        }
        protected void dgClientes_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {//paginador
            try
            {
                ErrorManager();
                dgClientes.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones
        private bool ValidarSesion()
        {
            try
            {
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
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

                parametros();
                //if (Request.QueryString["cte"] != null)
                //{
                //    Int32.TryParse(Request.QueryString["cte"].ToString(), out Cliente);
                //    txtNoCliente.Text = Cliente.ToString();
                //}
                dgClientes.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void parametros()
        {
            id_Ter = 0;
            id_Uen = 0;
            id_Rik = 0;
            id_Seg = 0;//solo saltillo
            if (Request.QueryString["ter"] != null)
                Int32.TryParse(Request.QueryString["ter"].ToString(), out id_Ter);

            if (Request.QueryString["uen"] != null)
                Int32.TryParse(Request.QueryString["uen"].ToString(), out id_Uen);

            if (Request.QueryString["rik"] != null)
                Int32.TryParse(Request.QueryString["rik"].ToString(), out id_Rik);

            if (Request.QueryString["seg"] != null)//solo saltillo
                Int32.TryParse(Request.QueryString["seg"].ToString(), out id_Seg);//solo saltillo
        }

        private int validador(int valorCliente)
        {
            try
            {
                int validador = 0; 
                for (int i = 0; i < dgClientes.Items.Count; i++)
                {
                    CrmPromociones promo = new CrmPromociones();                    
                    promo.Id = Convert.ToInt32(dgClientes.Items[i]["Id_Cte"].Text);
                    if (valorCliente == promo.Id)
                        validador = 1;
                }
                return validador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<CrmPromociones> GetListClientes()
        {
            try
            {
                parametros();
                int idCliente = !string.IsNullOrEmpty(txtNoCliente.Text) ? Convert.ToInt32(txtNoCliente.Text) : 0;
                string nombreCliente = txtBuscaCliente.Text;               
                List<CrmPromociones> List = new List<CrmPromociones>();
                CN_CrmPromocion cls = new CN_CrmPromocion();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                cls.ConsultaCatClientes(session2, id_Ter, id_Uen, id_Rik /*session2.Id_Rik*/, id_Seg, idCliente, nombreCliente, ref List);
                valorGrid = List.Count;
                HiddenField1.Value = List.Count.ToString();
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CerrarVentana()
        {
            try
            {
                string funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
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
                RadAjaxManager1.ResponseScripts.Add("alert('" + mensaje + "', 330, 150);");
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