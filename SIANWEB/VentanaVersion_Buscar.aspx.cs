using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
 
namespace SIANWEB
{
    public partial class VentanaVersion_Buscar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    CerrarVentana("0");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        //if (Session["BuscarPrecio" + Session.SessionID] != null)
                        //{
                        
                        RadGrid1.Rebind();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    RadGrid1.DataSource = GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }

        private List<Acys> GetList()
        {
            try
            {
                    List<Acys> List = new List<Acys>();
                    CN_CapAcys clsCapAcys = new CN_CapAcys();
                    int Id_Acs = Convert.ToInt32(Request.QueryString["Id_Acs"]);
                    Sesion session2 = new Sesion();
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    Acys acs = new Acys();
                    acs.Id_Emp = session2.Id_Emp;
                    acs.Id_Cd = session2.Id_Cd_Ver;
                    acs.Id_Acs = Id_Acs;
                    clsCapAcys.ConsultarAcysVersiones_Lista(acs, session2.Emp_Cnx, ref List);
                
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_PageIndexChanged");
            }
        }
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
                Alerta(Message);
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
                Alerta("Error: [" + NombreFuncion + "] " + eme.Message.ToString());
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                Alerta("Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString());
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
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
                        Session["Id_Buscar" + Session.SessionID] = RadGrid1.Items[item]["Id_Acs"].Text;
                        Session["IdVersion_Buscar" + Session.SessionID] = RadGrid1.Items[item]["Acs_Version"].Text;
                        Session["FechaVersion_Buscar" + Session.SessionID] = RadGrid1.Items[item]["Acs_Fecha"].Text;
                        CerrarVentana(RadGrid1.Items[item]["Id_Acs"].Text);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void CerrarVentana(string Id_Acs)
        {
            try
            {
                string funcion = "CloseAndRebind('" + Id_Acs + "')";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnBuscar1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RadGrid1.Rebind();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}