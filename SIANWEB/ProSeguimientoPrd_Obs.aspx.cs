using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;
using System.Data;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using System.IO;
using System.Runtime.Remoting;

namespace SIANWEB
{
    public partial class CatSeguimientoProductos : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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

        private DataTable dtSeg
        {
            get
            {
                return (DataTable)Session["dtSegPrd" + Session.SessionID];
            }
            set
            {
                Session["dtSegPrd" + Session.SessionID] = value;
            }
        }
         
        DataTable dt_detalles { get { return (DataTable)Session["dt_detalles" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt_detalles" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        private List<SeguimientoProductos> ListaSeguimientoProducto
        {
            get { return (List<SeguimientoProductos>)Session["ListaSeguimientoProducto"]; }
            set { Session["ListaSeguimientoProducto"] = value; }
        }
        #endregion Variables

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (sesion == null)
                {
                    CerrarVentana();
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        if (!sesion.Cu_Modif_Pass_Voluntario)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }

                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void cargaObservaciones()
        {
            try
            {
                SeguimientoProductos segPrd = new SeguimientoProductos();
                List<SeguimientoProductos> listaSegPrd = new List<SeguimientoProductos>();

                segPrd.Id_Emp = this.sesion.Id_Emp;
                segPrd.Id_Cd = this.sesion.Id_Cd_Ver;
                segPrd.Id_Prd = Convert.ToInt32(this.HF_ID.Value);

                CN_ProSeguimientoPrd CNProSegPrd = new CN_ProSeguimientoPrd();

                CNProSegPrd.LlenaGridSeguimiento(segPrd, ref listaSegPrd, this.sesion.Emp_Cnx);

                for (int i = 0; i < listaSegPrd.Count; i++)
                {
                    this.dtSeg.Rows.Add(listaSegPrd[i].Id_SegPrd, listaSegPrd[i].Seg_fecha, listaSegPrd[i].Seg_Comentarios);
                }
                this.rgSeguimiento.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rgSeguimiento_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
             try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgSeguimiento.DataSource = this.dtSeg;
                }
            }
            catch
            {
                this.Alerta("Error al cargar el grid de observaciones de seguimiento");
            }
        }

        protected void rgSeguimiento_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgSeguimiento.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgSeguimiento_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem item = e.Item as GridEditableItem;
                    if (item.FindControl("rdFecha") != null)
                    {
                        (item.FindControl("rdFecha") as RadDatePicker).DateInput.Focus();
                    }
                }
                //if (e.Item.IsDataBound)
                //{
                //    GridDataItem item = (GridDataItem)e.Item;
                //    item["EditCommandColumn"].Controls[0].Visible = false;
                //    rgSeguimiento.Columns.FindByUniqueName("EditCommandColumn").Visible = false;
                //}                
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgSeguimiento_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (this.txtProducto.Text == "")
                {
                    this.Alerta("No se pueden agregar registros cuando no se tienen productos relacionados");
                    e.Canceled = true;
                }
                else
                {
                    switch (e.CommandName)
                    {
                        case "InitInsert":
                            break;
                        case "PerformInsert":
                            if (_PermisoGuardar)
                            {
                                PerformInsertSeg(e);
                            }
                            else
                            {
                                this.Alerta("No tiene permisos para agregar nuevos elementos");
                            }
                            break;
                        case "Update":
                            if (_PermisoModificar)
                            {
                                UpdateSeg(e);
                            }
                            else
                            {
                                this.Alerta("No tiene permisos para modificar los registros");
                            }
                            break;
                        case "Delete":
                            if (_PermisoEliminar)
                            {
                                DeleteSeg(e);
                            }
                            else
                            {
                                this.Alerta("No tiene permiso para eliminar los registros");
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "Confirmar")
                {

                }
                else if (btn.CommandName == "new")
                {

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
        #endregion Eventos

        #region Funciones      
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                }
                else
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                }
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                _PermisoGuardar = Permiso.PGrabar;
                _PermisoModificar = Permiso.PModificar;
                _PermisoEliminar = Permiso.PEliminar;
                _PermisoImprimir = Permiso.PImprimir;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DeleteSeg(GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;

                int verificador = 0;

                SeguimientoProductos SegPrd = new SeguimientoProductos();

                SegPrd.Id_SegPrd = Convert.ToInt32(((Label)item.FindControl("lblSegId1")).Text);
                SegPrd.Id_Emp = this.sesion.Id_Emp;
                SegPrd.Id_Cd = this.sesion.Id_Cd_Ver;
                SegPrd.Id_Prd = Convert.ToInt32(this.HF_ID.Value);

                CN_ProSeguimientoPrd CNProSeguimientoPrd = new CN_ProSeguimientoPrd();
                CNProSeguimientoPrd.EliminaObservaciones(SegPrd, this.sesion.Emp_Cnx, ref verificador);

                if (verificador == 1)
                {
                    this.Alerta("Registro eliminado satisfactoriamente");

                    this.Inicializar();
                }
                else
                {
                    this.Alerta("No fue posible eliminar el registro");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateSeg(GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;

                RadDatePicker rdf = ((RadDatePicker)gi.FindControl("rdFecha"));

                //if (!validarFecha(rdf, e))
                //{
                //    return;
                //}

                if (((RadTextBox)gi.FindControl("txtObservaciones")).Text == "" ||
                    !((RadDatePicker)gi.FindControl("rdFecha")).SelectedDate.HasValue)
                {
                    e.Canceled = true;
                    this.Alerta("Todos los campos son requeridos.");
                    return;
                }
                
                SeguimientoProductos SegPrd = new SeguimientoProductos();
                SegPrd.Id_SegPrd = Convert.ToInt32(((Label)gi.FindControl("lblSegId2")).Text);
                SegPrd.Id_Emp = this.sesion.Id_Emp;
                SegPrd.Id_Cd = this.sesion.Id_Cd_Ver;
                SegPrd.Id_Prd = Convert.ToInt32(this.HF_ID.Value);
                SegPrd.Seg_fecha = Convert.ToDateTime(((RadDatePicker)gi.FindControl("rdFecha")).SelectedDate);
                SegPrd.Seg_Comentarios = ((RadTextBox)gi.FindControl("txtObservaciones")).Text;

                CN_ProSeguimientoPrd CNProSeguimientoPrd = new CN_ProSeguimientoPrd();
                int verificador = -1;

                CNProSeguimientoPrd.ModificaObservaciones(SegPrd, this.sesion.Emp_Cnx, ref verificador);

                if (verificador > 0)
                {
                    this.Inicializar();
                }
                else
                {
                    this.Alerta("Ocurrio un error al intentar guardar la observación");
                }
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

        private void GetListSeg()
        {
            try
            {
                dtSeg = new DataTable();
                dtSeg.Columns.Add("Id_SegPrd", System.Type.GetType("System.Int32"));
                dtSeg.Columns.Add("Seg_Fecha", System.Type.GetType("System.DateTime"));
                dtSeg.Columns.Add("Seg_Comentarios", System.Type.GetType("System.String"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PerformInsertSeg(GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;
                RadDatePicker rdf = ((RadDatePicker)gi.FindControl("rdFecha"));
               
                if (((RadTextBox)gi.FindControl("txtObservaciones")).Text == "" ||
                    !((RadDatePicker)gi.FindControl("rdFecha")).SelectedDate.HasValue)
                {
                    e.Canceled = true;
                    this.Alerta("Todos los campos son requeridos");
                    return;
                }

                SeguimientoProductos SegPrd = new SeguimientoProductos();
                SegPrd.Id_Emp = this.sesion.Id_Emp;
                SegPrd.Id_Cd = this.sesion.Id_Cd_Ver;
                SegPrd.Id_Prd = Convert.ToInt32(this.HF_ID.Value);
                SegPrd.Seg_fecha = Convert.ToDateTime(((RadDatePicker)gi.FindControl("rdFecha")).SelectedDate);
                SegPrd.Seg_Comentarios = ((RadTextBox)gi.FindControl("txtObservaciones")).Text;

                CN_ProSeguimientoPrd CNProSeguimientoPrd = new CN_ProSeguimientoPrd();
                int verificador = -1;

                CNProSeguimientoPrd.GuardaObservaciones(SegPrd, this.sesion.Emp_Cnx, ref verificador);

                if (verificador > 0)
                {
                    this.Inicializar();
                }
                else
                {
                    this.Alerta("Ocurrio un error al intentar guardar la observación");
                }
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
                this.GetListSeg();

                this.HF_ID.Value = Request.QueryString["Id"];
                this.cargaObservaciones();

                this.rgSeguimiento.Rebind();

                List<Producto> listaProducto = new List<Producto>();
                Producto producto = new Producto();

                producto.Id_Emp = this.sesion.Id_Emp;
                producto.Id_Prd = Convert.ToInt32(this.HF_ID.Value);
                producto.Id_Cd = this.sesion.Id_Cd_Ver;

                int validador = 0;

                new CN_ProSeguimientoPrd().BuscaProSeguimientoPrd(ref producto, this.sesion.Emp_Cnx, ref listaProducto, ref validador);

                if (validador > 0)
                {
                    this.txtProducto.Text = producto.Id_Prd.ToString();
                    this.txtPrdDescripcion.Text = producto.Prd_Descripcion.ToString();
                }
                else
                {
                    this.Alerta("No existen datos relacionados al producto especificado");
                }
                _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
                _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
                _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
                _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void crearDT()
        {
            dt_detalles = new DataTable();
            dt_detalles.Columns.Add("Id_SegPrd");
            dt_detalles.Columns.Add("Seg_Fecha");
            dt_detalles.Columns.Add("Seg_Comentarios");
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

        #endregion ErrorManager
    }
}