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
    public partial class CrmCatSoluciones : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

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
        #endregion
        #region Eventos
        public int area = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ValidarSesion())
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();                        
                        if (session.Cu_Modif_Pass_Voluntario == false)                       
                            return;                        

                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {//funciones de botones de menu
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                    if (Page.IsValid)
                        Guardar();
                }
                else if (btn.CommandName == "new")
                {
                    Nuevo();
                }
                else if (btn.CommandName == "undo")
                {
                    Regresar();
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {//datos del grid
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)               
                    rgAreas.DataSource = GetList();               
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }
        protected void rgAreas_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                WebControl Button = default(WebControl);
                string clickHandler = string.Empty;

                Button = (WebControl)item["Delete"].Controls[0];
                clickHandler = Button.Attributes["onclick"];
                //Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Clave").ToString());
            }
        }
        protected void rgAreas_ItemCommand(object source, GridCommandEventArgs e)
        {//botones del grid
            try
            {
                ErrorManager();
                Int32 item = default(Int32);
                item = e.Item.ItemIndex;

                if (e.CommandName == "Modificar")
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                  

                    txtPosicion.Text = rgAreas.Items[item]["Solucion"].Text;
                    txtPotencial.Text = rgAreas.Items[item]["Porcentaje"].Text;

                    ibtnCrear_Click(null, null);

                    HF_Modificar.Value = rgAreas.Items[item]["Clave"].Text;
                }
                else
                if (e.CommandName.ToString() == "Delete")
                {
                   
                    if (item >= 0)
                    {
                        CN__Comun.RemoverValidadores(Validators);
                        CrmCatSolucion solucion = new CrmCatSolucion();
                        solucion.Area = Convert.ToInt32(ddlAreas.SelectedValue);
                        solucion.Clave = Convert.ToInt32(rgAreas.Items[item]["Clave"].Text);
                        solucion.Descripcion = rgAreas.Items[item]["Solucion"].Text;
                        solucion.Porcentaje = Convert.ToDouble(rgAreas.Items[item]["Porcentaje"].Text);
                        Eliminar(solucion);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("conflicted with the REFERENCE constraint"))
                {
                    Alerta("El registro está siendo utilizado por otro componente");
                }
                else
                {
                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }
            }
        }
        protected void rgAreas_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {//paginador
            try
            {
                ErrorManager();
                rgAreas.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ddlUENs_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int valor = !string.IsNullOrEmpty(ddlUENs.SelectedValue) ? Convert.ToInt32(ddlUENs.SelectedValue) : 0;
            int segmento = CargarSegmentos(valor);
            if (segmento > 0)
            {
                ddlSegmentos.SelectedValue = segmento.ToString();
                ddlSegmentos.Text = ddlSegmentos.FindItemByValue(segmento.ToString()).Text;
                area = CargarAreas(segmento);
                ddlAreas.SelectedValue = area.ToString();
            }
            rgAreas.Rebind();
        }
        protected void ddlSegmentos_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int segmento = !string.IsNullOrEmpty(ddlSegmentos.SelectedValue) ? Convert.ToInt32(ddlSegmentos.SelectedValue) : 0;
            area = CargarAreas(segmento);
            if (area > 0)
            {
                ddlAreas.SelectedValue = area.ToString();
                ddlAreas.Text = ddlAreas.FindItemByValue(area.ToString()).Text;
            }
            rgAreas.Rebind();
        }
        protected void ddlAreas_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rgAreas.Rebind();
        }
        protected void ibtnCrear_Click(object sender, ImageClickEventArgs e)
        {
            btnAgregar.Visible = true;
            btnDeshacer.Visible = true;
            ibtnCrear.Visible = false;
            Label1.Visible = false;
            Nuevo();
        }
        protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Guardar();
                ibtnCrear.Visible = true;
                Label1.Visible = true;
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void btnDeshacer_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Regresar();
                ibtnCrear.Visible = true;
                Label1.Visible = true;
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                int uen = CargarUEN();
                ddlUENs.SelectedValue = uen.ToString();
                int segmento = CargarSegmentos(uen);
                if (segmento > 0)
                {
                    ddlSegmentos.SelectedValue = segmento.ToString();
                    int area = CargarAreas(segmento);
                    ddlAreas.SelectedValue = area.ToString();
                }
                rgAreas.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidarSesion()
        {
            try
            {
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private int CargarUEN()
        {
            int valorUEN = 0;
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<CrmCatSolucion> list = new List<CrmCatSolucion>();
                CN_CrmCatSoluciones clscrmCat = new CN_CrmCatSoluciones();
                clscrmCat.ComboUen(sesion, ref list);
                ddlUENs.Items.Clear();
                if (list.Count > 0)
                {
                    ddlUENs.DataSource = list;
                    ddlUENs.DataValueField = "Id";
                    ddlUENs.DataTextField = "Descripcion";
                    ddlUENs.DataBind();

                    valorUEN = list[0].Id;
                    ddlUENs.SelectedValue = valorUEN.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return valorUEN;
        }
        private int CargarSegmentos(int UEN)
        {
            int valorSegmento = 0;
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<CrmCatSolucion> list = new List<CrmCatSolucion>();
                CN_CrmCatSoluciones clscrmCat = new CN_CrmCatSoluciones();
                clscrmCat.ComboSegmento(sesion, UEN, ref list);
                ddlSegmentos.Items.Clear();
                if (list.Count > 0)
                {
                    ddlSegmentos.DataSource = list;
                    ddlSegmentos.DataValueField = "Id";
                    ddlSegmentos.DataTextField = "Descripcion";
                    ddlSegmentos.DataBind();
                    valorSegmento = list[0].Id;
                    ddlSegmentos.SelectedValue = valorSegmento.ToString();
                    ddlSegmentos.Text = ddlSegmentos.FindItemByValue(valorSegmento.ToString()).Text;
                }
                else
                {
                    ddlSegmentos.Items.Clear();
                    ddlSegmentos.Text = "";
                    ddlAreas.Items.Clear();
                    ddlAreas.Text = "";
                    pnlAgrega.Visible = false;
                    this.lblMensajes.Text = "No se ha registrado ningún segmento en la UEN seleccionada";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return valorSegmento;
        }
        private int CargarAreas(int segmento)
        {
            area = 0;
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<CrmCatSolucion> list = new List<CrmCatSolucion>();
                CN_CrmCatSoluciones clscrmCat = new CN_CrmCatSoluciones();
                clscrmCat.ComboArea(sesion, segmento, ref list);
                ddlAreas.Items.Clear();
                if (list.Count > 0)
                {
                    ddlAreas.DataSource = list;
                    ddlAreas.DataValueField = "Id";
                    ddlAreas.DataTextField = "Descripcion";
                    ddlAreas.DataBind();
                    area = list[0].Id;
                    //for (int i = 0; i < list.Count; i++)
                    //    ddlAreas.Items.Add(new RadComboBoxItem(list[i].Id.ToString() + " " + list[i].Descripcion, list[i].Id.ToString()));
                    ddlAreas.SelectedValue = area.ToString();
                    ddlAreas.Text = ddlAreas.FindItemByValue(area.ToString()).Text;
                    pnlAgrega.Visible = true;
                    this.lblMensajes.Text = "";
                }
                else
                {
                    ddlAreas.Items.Clear();
                    ddlAreas.Text = "";
                    pnlAgrega.Visible = false;
                    this.lblMensajes.Text = "No se ha registrado ningún área en el segmento y UEN seleccionada";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return area;
        }
        protected void Guardar()
        {
            try
            {
              

                CrmCatSolucion soluciones = new CrmCatSolucion();
                
                soluciones.Area = !string.IsNullOrEmpty(ddlAreas.SelectedValue) ? Convert.ToInt32(ddlAreas.SelectedValue) : 0;
                soluciones.Descripcion = txtPosicion.Text; //Request.Form["txtPosicion"].ToString();//txtPosicion.Text;
                soluciones.Porcentaje = txtPotencial.Value.Value;// == null) ? Convert.ToDouble(txtPotencial.Value.Value) : 0;

                if (soluciones.Area == 0)
                {
                    Alerta("Seleccione una área valida");
                    return;
                }
                if (string.IsNullOrEmpty(soluciones.Descripcion))
                {
                    Alerta("Ingrese una descripción de la solución");
                    return;
                }
                if (soluciones.Porcentaje == 0)
                {
                    Alerta("Ingrese un valor de porcentaje");
                    return;
                }

                soluciones.Id = Convert.ToInt32(MaximoId());
                int valido = 0;
                CN_CrmCatSoluciones cls = new CN_CrmCatSoluciones();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];

                if (HF_Modificar.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    cls.InsertCatSolucion(session2, soluciones, ref valido);
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    soluciones.Clave = Convert.ToInt32(HF_Modificar.Value);
                    cls.ModificarSolucion(soluciones, session.Emp_Cnx,session.Id_Emp, ref valido);
                }
                if (valido == 1)
                {
                    FinGuardar();
                    //Alerta("Registro agregado satisfactoriamente");
                }
                else
                {
                    Alerta("Ocurrio un error al intentar guardar");
                }

                rgAreas.Rebind();

                txtPosicion.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void FinGuardar()
        {
            try
            {
                rgAreas.Rebind();
                //'Me.pnlAgregar.Visible = false;
                this.txtPosicion.Text = "";
                this.txtPotencial.Text = "";
                this.lblAreas.Visible = false;
                this.txtPosicion.Visible = false;
                this.btnAgregar.Visible = false;
                this.btnDeshacer.Visible = false;
                this.lblPPotencial.Visible = false;
                this.txtPotencial.Visible = false;
                this.ddlAreas.Enabled = true;
                ddlSegmentos.Enabled = true;
                ddlUENs.Enabled = true;
                //this.lblPorcentaje.Visible = false;
                this.txtPosicion.Focus();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void Regresar()
        {
            ddlUENs.Enabled = true;
            ddlSegmentos.Enabled = true;
            ddlAreas.Enabled = true;
            lblAreas.Visible = false;
            txtPosicion.Text = string.Empty;
            txtPotencial.Text = string.Empty;
            txtPosicion.Visible = false;
            lblPPotencial.Visible = false;
            txtPotencial.Visible = false;
            btnAgregar.Visible = false;
            btnDeshacer.Visible = false;
            HF_Modificar.Value = "";
        }
        protected void Nuevo()
        {
            ddlUENs.Enabled = false;
            ddlSegmentos.Enabled = false;
            ddlAreas.Enabled = false;
            lblAreas.Visible = true;
            txtPosicion.Visible = true;
            lblPPotencial.Visible = true;
            txtPotencial.Visible = true;
            //txtPosicion.Text = string.Empty;
            //txtPotencial.Text = string.Empty;
            txtPosicion.Focus();
            HF_Modificar.Value = "";
        }
        private void ValidarPermisos()
        {
            try
            {
                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, session.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = session.Id_U;
                Permiso.Id_Cd = session.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, session.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Eliminar(CrmCatSolucion solucion)
        {
            try
            {
                if (!_PermisoEliminar)
                {
                    Alerta("No tiene permisos para eliminar");
                    return;
                }
                else
                {
                    HF_Modificar.Value = "";
                    int valido = 0;
                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];
                    CN_CrmCatSoluciones cls = new CN_CrmCatSoluciones();
                    cls.EliminarCatSolucion(session, solucion, ref valido);
                    if (valido == 1)
                        Alerta("La Solución #" + solucion.Clave + " fue eliminada satisfactoriamente");
                    else if (valido == -2)
                        Alerta("No se puede eliminar la solución, ya fue asignada a un proyecto");
                    else
                        Alerta("No se pudo eliminar la solución #" + solucion.Clave);
                    rgAreas.Rebind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<CrmCatSolucion> GetList()
        {
            try
            {
                if (area == 0)
                    area = !string.IsNullOrEmpty(ddlAreas.SelectedValue) ? Convert.ToInt32(ddlAreas.SelectedValue) : 0;
                List<CrmCatSolucion> List = new List<CrmCatSolucion>();
                CN_CrmCatSoluciones cls = new CN_CrmCatSoluciones();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                cls.ConsultaCatSolucion(session2, area, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string MaximoId()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, 0, "CatSolucion", "Id_Sol", Sesion.Emp_Cnx, "spCatLocal_Maximo");
            }//1,null,'CatSolucion','Id_Sol'
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