using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaNegocios;
using CapaEntidad;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class ProPedidoVI_InvIns : System.Web.UI.Page
    {
        #region Variables
        public DataTable dt
        {
            get
            {
                return (DataTable)Session["dtPedidoVI" + Session.SessionID];
            }
            set
            {
            }
        }
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    txtFecha.DbSelectedDate = Request.QueryString["fecha"].ToString().Substring(0, 2) + "/" + Request.QueryString["fecha"].ToString().Substring(2, 2) + "/" + Request.QueryString["fecha"].ToString().Substring(4, 4);
                    txtOrden.Text = Request.QueryString["orden"].ToString();
                    RadGrid1.Rebind();
                    RadGrid2.Rebind();

                    double ancho = 0;
                    foreach (GridColumn gc in RadGrid1.Columns)
                    {
                        if (gc.Display)
                        {
                            ancho = ancho + gc.HeaderStyle.Width.Value;
                        }
                    }
                    RadGrid1.Width = Unit.Pixel(Convert.ToInt32(ancho) + 20);
                    RadGrid1.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho) );
                }
                catch (Exception ex)
                {
                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    RadGrid1.DataSource = GetListDet();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadGrid2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    RadGrid2.DataSource = GetListPrecio();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Session["Respuesta" + Session.SessionID] = false;
                string funcion;
                funcion = "CloseWindow()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Session["Respuesta" + Session.SessionID] = true;
                string funcion;
                funcion = "CloseAndContinue()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    LinkButton imgButton = (LinkButton)item.FindControl("hlEquivalencias");
                    string id_Prd = item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"].ToString();
                    string Id_Acs = Request.QueryString["Id_Acs"].ToString(); //txtFolio.Text == "" ? "-1" : txtFolio.Text;
                    string Id_Cte = Request.QueryString["cte"].ToString();
                    imgButton.OnClientClick = "return popup2('" + id_Prd + "', '" + Id_Acs + "','" + Id_Cte + "')";
                }
                catch (Exception)
                {

                }

            }
        }
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                ErrorManager();
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        RadGrid1.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                //this.DisplayMensajeAlerta(string.Concat(ex.Message, "RAM1_AjaxRequest"));
            }
        }
        #endregion
        #region Funciones
        private DataTable GetListDet()
        {
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("Id_Prd", System.Type.GetType("System.Int32"));
            dtTemp.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("Prd_Cantidad", System.Type.GetType("System.Int32"));
            dtTemp.Columns.Add("Prd_Asignado", System.Type.GetType("System.Int32"));
            dtTemp.Columns.Add("Prd_InvFinal", System.Type.GetType("System.Int32"));
            dtTemp.Columns.Add("Prd_Disponible", System.Type.GetType("System.Int32"));

            CN_CatProducto cn_catproducto = new CN_CatProducto();
            Producto pr = new Producto();
            List<int> actuales;

            foreach (DataRow dr in dt.Rows)
            {

                actuales = new List<int>();
                cn_catproducto.ConsultaProducto_Disponible(session.Id_Emp, session.Id_Cd_Ver, dr["Id_Prd"].ToString(), ref actuales, session.Emp_Cnx);
                if (Convert.ToInt32(dr["Prd_Cantidad"]) > Convert.ToInt32(actuales[2]))
                {
                    dtTemp.Rows.Add(new object[] { dr["Id_Prd"], dr["Prd_Descripcion"], dr["Prd_Cantidad"], actuales[1], actuales[0], actuales[2] });
                }
            }

            return dtTemp;
        }
        private DataTable GetListPrecio()
        {
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("Id_Prd", System.Type.GetType("System.Int32"));
            dtTemp.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("Precio_Convenido", System.Type.GetType("System.Int32"));
            dtTemp.Columns.Add("Precio_Captado", System.Type.GetType("System.Double"));

            CN_CatProducto cn_catproducto = new CN_CatProducto();
            Producto pr = new Producto();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Prd_PrecioAcys"] == DBNull.Value)
                {
                    dr["Prd_PrecioAcys"] = 0;
                }
                if (Convert.ToInt32(dr["Id_PrdOld"]) != -1 && Convert.ToDouble(dr["Prd_Precio"]) != Convert.ToDouble(dr["Prd_PrecioAcys"]))
                {
                    dtTemp.Rows.Add(new object[] { dr["Id_Prd"], dr["Prd_Descripcion"], dr["Prd_PrecioAcys"], dr["Prd_Precio"] });
                }
            }

            return dtTemp;
        }
        #endregion
        #region ErrorManager
        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "</br></br>', 330, 150);");
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