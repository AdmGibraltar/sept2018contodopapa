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

namespace SIANWEB
{
    public partial class ProAsignPrdxPed : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //imprimir();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    CerrarVentana();
                }
                else
                {
                    if (!Page.IsPostBack)
                    {


                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        

                        Inicializar();

                        double ancho = 0;
                        foreach (GridColumn gc in rgPedido.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        double extra = 0;
                        if (rgPedido.Items.Count > 7)
                        {
                            extra = 17;
                        }

                        rgPedido.Width = Unit.Pixel(Convert.ToInt32(ancho + extra));
                        rgPedido.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    }
                }
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
                {
                    rgPedido.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            StringBuilder Expresion = new StringBuilder();
            if (txtProducto1.Text.Length > 0)
            {
                Expresion.Append("(Id_Prd>=" + txtProducto1.Text + ")");
            }
            if (txtProducto2.Text.Length > 0)
            {
                if (Expresion.Length > 0)
                {
                    Expresion.Append(" and ");
                }
                Expresion.Append("(Id_Prd<=" + txtProducto2.Text + ")");
            }
            if (txtProducto.Text.Length > 0)
            {
                if (Expresion.Length > 0)
                {
                    Expresion.Append(" and ");
                }
                Expresion.Append("([Prd_Desc] LIKE \'%" + txtProducto.Text + "%\')");
                GridColumn column = rgPedido.MasterTableView.GetColumnSafe("Prd_Desc");
                column.CurrentFilterFunction = GridKnownFunction.Contains;
            }

            if (Expresion.Length > 0)
            {
                rgPedido.MasterTableView.FilterExpression = Expresion.ToString();
            }
            else
            {
                rgPedido.MasterTableView.FilterExpression = "";
            }
            rgPedido.MasterTableView.Rebind();
        }
        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {

                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                    Guardar();
                }
                else if (btn.CommandName == "new")
                {
                    //Nuevo();
                }
                else if (btn.CommandName == "undo")
                {
                    CerrarVentana();
                }
            }

            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }

        #endregion
        #region funciones
        private void CerrarVentana()
        {
            try
            {
                string funcion;
                if (this.HiddenRebind.Value == "0")
                {
                    funcion = "CloseWindow()";
                }
                else
                {
                    funcion = "CloseAndRebind()";
                }
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void Inicializar()
        {
            HF_Ped.Value = Request.QueryString["Id"].ToString();
            txtPedido.Text = Request.QueryString["Id"].ToString();
            txtCliente.Text = Request.QueryString["Id_Cte"].ToString();
            txtClienteNombre.Text = Request.QueryString["Nom_Cte"].ToString();
            _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
            _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
            _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
            _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);

            rgPedido.Rebind();
            ValidarPermisos();
        }
        private void Guardar()
        {
            try
            {
                if (Convert.ToBoolean(HF_Guardar.Value))
                {
                    //if (!Convert.ToBoolean(HiddenRebind.Value))
                    //{
                    //    Alerta("No ha realizado ninguna asignación");
                    //}
                    CapaDatos.Funciones funciones = new CapaDatos.Funciones();
                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];
                    List<PedidoDet> list = new List<PedidoDet>();
                    Pedido pedido = new Pedido();
                    pedido.Id_Emp = session.Id_Emp;
                    pedido.Id_Cd = session.Id_Cd_Ver;
                    pedido.Id_Ped = Convert.ToInt32(HF_Ped.Value);
                    pedido.Ped_Fecha = funciones.GetLocalDateTime(session.Minutos);
                    pedido.Id_U = session.Id_U;

                    PedidoDet Ped_Det = default(PedidoDet);
                    for (int x = 0; x < rgPedido.Items.Count; x++)
                    {
                        Ped_Det = new PedidoDet();
                        Ped_Det.Id_Prd = Convert.ToInt32(rgPedido.Items[x]["Id_Prd"].Text);
                        Ped_Det.Prd_Asig =
                            Convert.ToInt32((rgPedido.Items[x]["Prd_Asig"].FindControl("txtAsig") as RadNumericTextBox).Text);
                           // - Convert.ToInt32(rgPedido.Items[x]["Prd_AsigOld"].Text);
                        Ped_Det.Id_PedDet = Convert.ToInt32(rgPedido.Items[x]["Id_PedDet"].Text);
                        list.Add(Ped_Det);
                    }

                    int verificador = 0;
                    CN_CapPedido cn_cappedido = new CN_CapPedido();

                    cn_cappedido.AsignarPrdXPed(pedido, list, session.Emp_Cnx, ref verificador);

                    if (verificador == 1)
                    {
                        AlertaCerrar("Se realizó la asignación correctamente");
                        rgPedido.Rebind();
                    }
                    else if (verificador == 2)
                    {
                        Alerta("No se cuenta con el inventario suficiente, no se realizo la asignación.");
                        rgPedido.Rebind();
                        return;
                    }
                    else if (verificador == 3)
                    {
                        Alerta("No se cuenta con el inventario suficiente, no se realizo la asignación.");
                        rgPedido.Rebind();
                        return;
                    }
                    else
                    {
                        Alerta("Ocurrió un error al intentar asignar");
                    }
                }
                else
                {
                    HF_Guardar.Value = "true";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<PedidoDet> GetList()
        {
            try
            {
                try
                {
                    List<PedidoDet> List = new List<PedidoDet>();
                    CN_CapPedido cn_CapPedido = new CN_CapPedido();
                    Sesion session2 = new Sesion();
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    Pedido pedido = new Pedido();
                    pedido.Id_Emp = session2.Id_Emp;
                    pedido.Id_Cd = session2.Id_Cd_Ver;
                    pedido.Id_Ped = Convert.ToInt32(HF_Ped.Value);

                    cn_CapPedido.ConsultaPedidoAsig(pedido, session2.Emp_Cnx, ref List);
                    return List;
                }
                catch (Exception ex)
                {
                    throw ex;
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

                if (_PermisoGuardar == false & _PermisoModificar == false)
                {
                    this.rtb1.Items[5].Visible = false;
                }

                //Nuevo
                this.rtb1.Items[6].Visible = false;
                //Guardar
                //Me.RadToolBar1.Items(5).Enabled = False
                //Regresar
                this.rtb1.Items[4].Visible = false;
                //Eliminar
                this.rtb1.Items[3].Visible = false;
                //Imprimir
                this.rtb1.Items[2].Visible = false;
                //Correo
                this.rtb1.Items[1].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region ErrorManager
        private void AlertaCerrar(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("CloseAlert('" + mensaje + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
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