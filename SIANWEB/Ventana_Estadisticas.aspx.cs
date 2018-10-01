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
    public partial class Ventana_Estadisticas : System.Web.UI.Page
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
                        txtId_Cte.DbValue = Convert.ToInt32(Request.QueryString["cte"]);
                        txtCte.Text = Request.QueryString["cteNom"].ToString();
                        RadGrid1.Rebind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        private List<Comun> GetList()
        {
            try
            {
                List<Comun> List = new List<Comun>();

                Session["BuscarPrecio" + Session.SessionID] = null;
                CN_CatCliente clsCatCliente = new CN_CatCliente();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Clientes cte = new Clientes();
                cte.Id_Emp = session2.Id_Emp;
                cte.Id_Cd = session2.Id_Cd_Ver;
                cte.Id_Cte = Convert.ToInt32(Request.QueryString["cte"]);

                clsCatCliente.ConsultaEstadistica(cte, session2.Emp_Cnx, ref List, RadNumericTextBox1.Value, RadTextBox1.Text == "" ? null : RadTextBox1.Text);

                RadGrid1.Columns[3].Display = CmbEn.SelectedValue == "1";
                RadGrid1.Columns[4].Display = CmbEn.SelectedValue == "1";
                RadGrid1.Columns[5].Display = CmbEn.SelectedValue == "1";

                RadGrid1.Columns[6].Display = !(CmbEn.SelectedValue == "1");
                RadGrid1.Columns[7].Display = !(CmbEn.SelectedValue == "1");
                RadGrid1.Columns[8].Display = !(CmbEn.SelectedValue == "1");

                CN_CatCalendario cn_calendario = new CN_CatCalendario();
                Calendario calendario = new Calendario();
                cn_calendario.ConsultaCalendarioActual(ref calendario, session2);

                DateTime mes_actual = Convert.ToDateTime("01/" + calendario.Cal_Mes.ToString() + "/" + calendario.Cal_Año.ToString());

                RadGrid1.Columns[3].HeaderText = mes_actual.AddMonths(-3).ToString("MM/yyyy");
                RadGrid1.Columns[6].HeaderText = mes_actual.AddMonths(-3).ToString("MM/yyyy");

                RadGrid1.Columns[4].HeaderText = mes_actual.AddMonths(-2).ToString("MM/yyyy");
                RadGrid1.Columns[7].HeaderText = mes_actual.AddMonths(-2).ToString("MM/yyyy");

                RadGrid1.Columns[5].HeaderText = mes_actual.AddMonths(-1).ToString("MM/yyyy");
                RadGrid1.Columns[8].HeaderText = mes_actual.AddMonths(-1).ToString("MM/yyyy");

                if (List.Count > 0)
                {
                    //RadGrid1.Columns[3].HeaderText = List[0].ValorStr.PadLeft(7, '0');
                    // List[0].ValorStr2.PadLeft(7, '0');
                    //List[0].ValorStr3.PadLeft(7, '0');
                    //RadGrid1.Columns[6].HeaderText = List[0].ValorStr.PadLeft(7, '0');
                    // List[0].ValorStr2.PadLeft(7, '0');
                    //List[0].ValorStr3.PadLeft(7, '0');
                }
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
                        Session["Id_Buscar" + Session.SessionID] = RadGrid1.Items[item]["Id"].Text;
                        Session["Descripcion_Buscar" + Session.SessionID] = RadGrid1.Items[item]["Descripcion"].Text;
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

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            RadGrid1.Rebind();
        }
    }
}