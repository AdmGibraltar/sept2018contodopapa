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
    public partial class Ventana_Indicadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    CerrarVentana();
                }
                else
                {
                    if (!IsPostBack)
                    {
                        RAM1_AjaxRequest(RAM1, new AjaxRequestEventArgs("refreshGrid"));
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

        private List<Producto> GetList()
        {
            try
            {
                List<Producto> List = new List<Producto>();

                Session["BuscarPrecio" + Session.SessionID] = null;
                CN_CatCliente clsCatCliente = new CN_CatCliente();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Clientes cte = new Clientes();
                cte.Id_Emp = session2.Id_Emp;
                cte.Id_Cd = session2.Id_Cd_Ver;
                cte.Id_Cte = Convert.ToInt32(Request.QueryString["cte"]);
                clsCatCliente.ConsultaIndicadores(cte, session2.Emp_Cnx, ref List, txtId_Cte.Value, txtCte.Text == "" ? null : txtCte.Text);
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
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                        Session["Id_Buscar" + Session.SessionID] = RadGrid1.Items[item]["Id_Prd"].Text;
                        Session["Descripcion_Buscar" + Session.SessionID] = RadGrid1.Items[item]["Prd_Descripcion"].Text;
                        CerrarVentana();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void CerrarVentana()
        {
            try
            {
                string funcion = "CloseAndRebind('precio')";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void CmbEn_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
        {
          var dataItem = RadGrid1.SelectedItems[0] as GridDataItem;
          if (dataItem != null)
          {
              txtId_Cte.Text = dataItem["Id_Prd"].Text;
              txtCte.Text = dataItem["Prd_Descripcion"].Text;
              txtInicial.DbValue = dataItem["Prd_InvInicial"].Text;
              txtFinal.DbValue = dataItem["Prd_InvFinal"].Text;
              txtAsignado.DbValue = dataItem["Prd_Asignado"].Text;
              txtOrdenado.DbValue = dataItem["Prd_Ordenado"].Text;
              txtTransito.DbValue = dataItem["Prd_Transito"].Text;
          }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "refreshGrid")
            {
                RadGrid1.Rebind();
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            RadGrid1.Rebind();
        }
    }
}