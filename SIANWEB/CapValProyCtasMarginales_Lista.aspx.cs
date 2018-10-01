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
using System.Net;
using System.Collections;

namespace SIANWEB
{
    public partial class CapValProyCtasMarginales_Lista : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public List<NotaCredito> listNotaCredito;
        #endregion

        #region Propiedades
        public int PermisoGuardar { get { return _PermisoGuardar == true ? 1 : 0; } }
        public int PermisoModificar { get { return _PermisoModificar == true ? 1 : 0; } }
        public int PermisoEliminar { get { return _PermisoEliminar == true ? 1 : 0; } }
        public int PermisoImprimir { get { return _PermisoImprimir == true ? 1 : 0; } }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Response.Redirect("login.aspx?id=2", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.Inicializar();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        #region Eventos
        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

                txtCliente1.Text = string.Empty;
                txtCliente2.Text = string.Empty;
                rgBase.Rebind();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
            }
        }
        protected void rgBase_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)//Llenar Grid
                    rgBase.DataSource = this.GetList();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgBase_NeedDataSource"));
            }
        }
        protected void rgBase_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgBase.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }
        protected void rgBase_ItemCommand(object source, GridCommandEventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string mensajeError = string.Empty;
            try
            {
                ErrorManager();
                Int32 item = default(Int32);

                if (e.Item == null) return;

                item = e.Item.ItemIndex;
                if (item >= 0)
                {
                    int Id_Emp = Convert.ToInt32(rgBase.Items[item]["Id_Emp"].Text);
                    int Id_Cd = Convert.ToInt32(rgBase.Items[item]["Id_Cd"].Text);
                    int Id_Vap = Convert.ToInt32(rgBase.Items[item]["Id_Vap"].Text);
                    int Id_Cte = Convert.ToInt32(rgBase.Items[item]["Id_Cte"].Text);
                    string Cte_NomComercial = rgBase.Items[item]["Cte_NomComercial"].Text;
                    string estatus = rgBase.Items[item]["Vap_Estatus"].Text;
                    string[] datePart = rgBase.Items[item]["Vap_Fecha"].Text.Split(new char[] { '/' });
                    DateTime fechaVapProyecto = new DateTime(Convert.ToInt32(datePart[2]), Convert.ToInt32(datePart[1]), Convert.ToInt32(datePart[0]));

                    switch (e.CommandName.ToString())
                    {
                        case "Eliminar":
                            mensajeError = "CapNotaCredito_delete_error";
                            if (_PermisoEliminar)
                                this.Alerta("Registro eliminado correctamente");
                            else
                                this.Alerta("No tiene permisos para eliminar");
                            break;
                        case "Modificar":
                            if (_PermisoModificar)
                            {
                                string valuacionModificable = "1";
                                if (estatus.ToLower() == "análisis" && (fechaVapProyecto >= sesion.CalendarioIni && fechaVapProyecto <= sesion.CalendarioFin))
                                    valuacionModificable = "1";
                                else
                                    valuacionModificable = "0";
                                RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_ValProyecto_Edicion('", Id_Emp, "','", Id_Cd, "','", Id_Vap, "','" + valuacionModificable + "')"));
                            }
                            else
                                this.Alerta("No tiene permisos para modificar");
                            break;
                        case "Imprimir":
                            mensajeError = "CapNotaCredito_print_error";
                            if (_PermisoImprimir)
                                ImpresionValuacionProyecto(Id_Emp, Id_Cd, Id_Vap);
                            else
                                this.Alerta("No tiene permiso para imprimir");
                            break;
                    }
                }
                if (e.CommandName.ToString().ToUpper().Contains("SORT"))
                {
                    ErrorManager();
                    this.rgBase.Rebind();
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, mensajeError));
            }
        }
        protected void rgBase_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                //GridItem cmdItem = rgBase.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                //cmdItem.FindControl("AddNewRecordButton").Parent.Visible = false;
            }
            catch (Exception ex)
            {//RadGrid1.Controls.Add(new LiteralControl("No se pudo agregar el Usuario. " + ex.Message));
                DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                rgBase.Rebind();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "btnBuscar_error"));
            }
        }
        protected void RAM1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            rgBase.Rebind(); 
        }
        #endregion

        #region Funciones
        private void ImpresionValuacionProyecto(int Id_Emp, int Id_Cd, int Id_Vap)
        {
            try
            {               
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                ArrayList al = (Session["ValProyectos" + Session.SessionID] as ArrayList);

                

                /*   PARAMETROS OBLIGATORIOS PARA EL REPORTE   */
                ALValorParametrosInternos.Add(sesion.Emp_Cnx); // CADENA DE CONEXION A LA BASE DE DATOS
                ALValorParametrosInternos.Add(Id_Emp);
                ALValorParametrosInternos.Add(Id_Cd);
                ALValorParametrosInternos.Add(Id_Vap);              

                Type instance = null;
                instance = typeof(LibreriaReportes.Rep_Valuacion_CtasMarg);               
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;

                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                if (_PermisoImprimir)
                    RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
                else
                    Alerta("No tiene permiso para imprimir");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<ValuacionProyecto> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<ValuacionProyecto> listValuacionProyecto = new List<ValuacionProyecto>();
                ValuacionProyecto valuacionProyecto = new ValuacionProyecto();
                valuacionProyecto.Id_Emp = sesion.Id_Emp;
                valuacionProyecto.Id_Cd = sesion.Id_Cd_Ver;
                //valuacionProyecto.Vap_Estatus = cmbEstatus.SelectedValue;
                valuacionProyecto.Vap_Estatus = "0";
                int? objectInt = null;
                DateTime? objectDateTime = null;

                new CN_CapValuacionProyectoCtasMarg().ConsultaValuacionProyecto_Buscar(valuacionProyecto, ref listValuacionProyecto, sesion.Emp_Cnx
                    , sesion.Propia ? sesion.Id_U : objectInt
                    , txtNombreCliente.Text
                    , this.txtCliente1.Text == string.Empty ? objectInt : Convert.ToInt32(this.txtCliente1.Text)
                    , this.txtCliente2.Text == string.Empty ? objectInt : Convert.ToInt32(this.txtCliente2.Text)
                    , this.txtFecha1.SelectedDate == null ? objectDateTime : Convert.ToDateTime(this.txtFecha1.SelectedDate)
                    , this.txtFecha2.SelectedDate == null ? objectDateTime : Convert.ToDateTime(this.txtFecha2.SelectedDate)
                    );

                return listValuacionProyecto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Inicializar()
        {
            this.CargarCentros();
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            txtFecha1.DbSelectedDate = sesion.CalendarioIni;
            txtFecha2.DbSelectedDate = sesion.CalendarioFin;
            //Cargar grid de ordenes de compra
            rgBase.Rebind();
        }
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

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
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (Permiso.PGrabar == false)
                        this.RadToolBar1.FindItemByValue("new").Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");
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
        private void CargarCliente(ref RadComboBox combo)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatCliente_Combo", ref combo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }  
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("TempPDFNoData"))
                    Alerta("El archivo PDF no contiene datos.");
                else
                    if (!mensaje.Contains("RAM1_AjaxRequest"))
                        Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
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