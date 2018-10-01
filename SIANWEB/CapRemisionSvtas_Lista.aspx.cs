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
using System.Configuration;

namespace SIANWEB 
{
    public partial class CapRemisionSvtas_Lista : System.Web.UI.Page
    {
        #region Variables

        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public List<RemisionSvtaAlmacen> listRemisionSvtaAlmacen;

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
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];     
                    Response.Redirect("login.aspx" , false);
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
                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        
        #region Eventos

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        rgRemisionSvtaAlmacen.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }

        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CN__Comun comun = new CN__Comun();
                Sesion sesion2 = (Sesion)Session["Sesion" + Session.SessionID];
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion2);
                if (sesion2.CalendarioIni >= txtFecha1.MinDate && sesion2.CalendarioIni <= txtFecha1.MaxDate)
                {
                    txtFecha1.DbSelectedDate = sesion2.CalendarioIni;
                }
                if (sesion2.CalendarioFin >= txtFecha2.MinDate && sesion2.CalendarioFin <= txtFecha2.MaxDate)
                {
                    txtFecha2.DbSelectedDate = sesion2.CalendarioFin;
                }
                Session["Sesion" + Session.SessionID] = sesion2;

                //this.CargarCliente(ref cmbCliente);
                //txtCliente.Text = string.Empty;
                //cmbCliente.SelectedIndex = cmbCliente.FindItemIndexByValue("-1");

                rgRemisionSvtaAlmacen.Rebind();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
            }
        }

        protected void rgRemisionSvtaAlmacen_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //Llenar Grid
                    rgRemisionSvtaAlmacen.DataSource = this.GetList();
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgRemisionSvtaAlmacen_NeedDataSource"));
            }
        }

        protected void rgRemisionSvtaAlmacen_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgRemisionSvtaAlmacen.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }

        protected void rgRemisionSvtaAlmacen_ItemCommand(object source, GridCommandEventArgs e)
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
                    int Id_Emp = Convert.ToInt32(rgRemisionSvtaAlmacen.Items[item]["Id_Emp"].Text);
                    int Id_Cd = Convert.ToInt32(rgRemisionSvtaAlmacen.Items[item]["Id_Cd"].Text);
                    int Id_Rva = Convert.ToInt32(rgRemisionSvtaAlmacen.Items[item]["Id_Rva"].Text);
                    string estatus = rgRemisionSvtaAlmacen.Items[item]["Rva_Estatus"].Text;
                    string[] datePart = rgRemisionSvtaAlmacen.Items[item]["Rva_Fecha"].Text.Split(new char[] { '/' });
                    DateTime fechaRemisionSvtaAlmacen = new DateTime(Convert.ToInt32(datePart[2]), Convert.ToInt32(datePart[1]), Convert.ToInt32(datePart[0]));

                    switch (e.CommandName.ToString())
                    {
                        case "Eliminar":
                            mensajeError = "CapRemisionSvtaAlmacen_delete_error";
                            if (estatus.ToUpper() == "B")
                            {
                                this.DisplayMensajeAlerta("CapRemisionSvtaAlmacen_EsBaja");
                            }
                            else
                            {
                                if (_PermisoEliminar)
                                {
                                    this.CancelarRemisionSvtaAlmacen(Id_Emp, Id_Cd, Id_Rva);
                                    this.DisplayMensajeAlerta("CapRemisionSvtaAlmacen_delete_ok");
                                }
                                else
                                {
                                    this.DisplayMensajeAlerta("PermisoEliminarDenegado");
                                }
                            }
                            break;
                        case "Modificar":
                            if (estatus.ToUpper() != "B")
                            {
                                if (_PermisoModificar)
                                {
                                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_RemisionSvtaAlmacen_Edicion('", Id_Emp, "','", Id_Cd, "','", Id_Rva, "','" + estatus.ToUpper() + "')"));
                                }
                                else
                                {
                                    this.DisplayMensajeAlerta("PermisoModificarDenegado");
                                }
                            }
                            else
                            {
                                this.DisplayMensajeAlerta("CapRemisionSvtaAlmacen_Modificar_Denegado");
                            }
                            break;
                        case "Imprimir":
                            mensajeError = "CapRemisionSvtaAlmacen_print_error";
                            if (estatus.ToUpper() == "I" || estatus.ToUpper() == "C")
                            {
                                if (_PermisoImprimir)
                                {
                                    this.ImpresionRemisionSvtaAlmacen(Id_Emp, Id_Cd, Id_Rva);
                                }
                                else
                                {
                                    this.DisplayMensajeAlerta("PermisoImprimirDenegado");
                                }
                            }
                            else
                            {
                                this.DisplayMensajeAlerta("CapRemisionSvtaAlmacen_EstatusIncorrecto");
                            }
                            break;
                    }
                }
                //para los botones de exportar
                if (e.CommandName.ToString().ToUpper().Contains("EXPORTTO"))
                {
                    rgRemisionSvtaAlmacen.MasterTableView.Columns.FindByUniqueName("Editar").Visible = false;
                    rgRemisionSvtaAlmacen.MasterTableView.Columns.FindByUniqueName("Eliminar").Visible = false;
                    rgRemisionSvtaAlmacen.MasterTableView.Columns.FindByUniqueName("Imprimir").Visible = false;

                    if (e.CommandName.ToString().ToUpper().Contains("PDF"))
                    {
                        rgRemisionSvtaAlmacen.MasterTableView.Columns.FindByUniqueName("Cte_NomComercial").HeaderStyle.Width = Unit.Pixel(200);
                        rgRemisionSvtaAlmacen.MasterTableView.Columns.FindByUniqueName("Ncr_Fecha").HeaderStyle.Width = Unit.Pixel(70);
                    }
                }
                if (e.CommandName.ToString().ToUpper().Contains("SORT"))
                {
                    ErrorManager();
                    this.rgRemisionSvtaAlmacen.Rebind();
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, mensajeError));
            }
        }

        protected void rgRemisionSvtaAlmacen_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                //GridItem cmdItem = rgRemisionRev.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                //cmdItem.FindControl("AddNewRecordButton").Parent.Visible = false;
            }
            catch (Exception ex)
            {
                //RadGrid1.Controls.Add(new LiteralControl("No se pudo agregar el Usuario. " + ex.Message));
                DisplayMensajeAlerta(ex.Message);
            }
        }
        
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                rgRemisionSvtaAlmacen.Rebind();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "btnBuscar_error"));
            }
        }

        #endregion

        #region Funciones

        private List<RemisionSvtaAlmacen> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                listRemisionSvtaAlmacen = new List<RemisionSvtaAlmacen>();
                RemisionSvtaAlmacen RemisionSvtaAlmacen = new RemisionSvtaAlmacen();
                RemisionSvtaAlmacen.Id_Emp = sesion.Id_Emp;
                RemisionSvtaAlmacen.Id_Cd = sesion.Id_Cd_Ver;

                int? objectInt = null;
                DateTime? objectDateTime = null;

                new CN_CapRemisionSvtaAlmacen().ConsultaRemisionSvtaAlmacen_Buscar(RemisionSvtaAlmacen, ref listRemisionSvtaAlmacen, sesion.Emp_Cnx
                    , sesion.Propia ? sesion.Id_U : objectInt
                    , this.txtFecha1.SelectedDate == null ? objectDateTime : Convert.ToDateTime(this.txtFecha1.SelectedDate)
                    , this.txtFecha2.SelectedDate == null ? objectDateTime : Convert.ToDateTime(this.txtFecha2.SelectedDate)
                    , this.cmbEstatus.SelectedValue == "-1" ? string.Empty : this.cmbEstatus.SelectedValue
                    , this.txtFolio1.Text == string.Empty ? objectInt : Convert.ToInt32(this.txtFolio1.Text)
                    , this.txtFolio2.Text == string.Empty ? objectInt : Convert.ToInt32(this.txtFolio2.Text)
                    , objectInt 
                    );

                return listRemisionSvtaAlmacen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ImpresionRemisionSvtaAlmacen(int Id_Emp, int Id_Cd, int Id_Rva) 
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                RemisionSvtaAlmacen RemisionSvtaAlmacen = new RemisionSvtaAlmacen();
                RemisionSvtaAlmacen.Id_Emp = Id_Emp;
                RemisionSvtaAlmacen.Id_Cd = Id_Cd;
                RemisionSvtaAlmacen.Id_Rva = Id_Rva;
                new CN_CapRemisionSvtaAlmacen().ConsultarRemisionSvtaAlmacen(ref RemisionSvtaAlmacen, sesion.Emp_Cnx);

                ALValorParametrosInternos.Add(Id_Emp);
                ALValorParametrosInternos.Add(Id_Cd);
                ALValorParametrosInternos.Add(Id_Rva);
                ALValorParametrosInternos.Add(RemisionSvtaAlmacen.Cd_Nombre);
                ALValorParametrosInternos.Add(sesion.U_Nombre);
                ALValorParametrosInternos.Add(DateTime.Now.ToShortDateString());
                string horaActual = DateTime.Now.Hour.ToString();
                string minActual = DateTime.Now.Minute.ToString();
                string segActual = DateTime.Now.Second.ToString();
                horaActual = string.Concat(horaActual, ":", minActual.Length == 1 ? string.Concat("0", minActual) : minActual);
                horaActual = string.Concat(horaActual, ":", segActual.Length == 1 ? string.Concat("0", segActual) : segActual);
                ALValorParametrosInternos.Add(horaActual);
                ALValorParametrosInternos.Add(RemisionSvtaAlmacen.Rva_Entrego);
                ALValorParametrosInternos.Add(RemisionSvtaAlmacen.Rva_Recibio);
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                Type instance = null;
                instance = typeof(LibreriaReportes.RemisionSvtaImpresion);
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;

                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CancelarRemisionSvtaAlmacen(int Id_Emp, int Id_Cd, int Id_Rva)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            RemisionSvtaAlmacen RemisionSvtaAlmacen = new RemisionSvtaAlmacen();
            int verificador = 0;

            RemisionSvtaAlmacen.Id_Emp = Id_Emp;
            RemisionSvtaAlmacen.Id_Cd = Id_Cd;
            RemisionSvtaAlmacen.Id_Rva = Id_Rva;
            new CN_CapRemisionSvtaAlmacen().EliminarRemisionSvtaAlmacen(RemisionSvtaAlmacen, sesion.Emp_Cnx, ref verificador);
            this.rgRemisionSvtaAlmacen.Rebind();
        }

        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            this.CargarCentros();
            //this.CargarCliente(ref cmbCliente);

            this.cmbEstatus.Sort = RadComboBoxSort.Ascending;
            this.cmbEstatus.SortItems();

            if (sesion.CalendarioIni >= txtFecha1.MinDate && sesion.CalendarioIni <= txtFecha1.MaxDate)
            {
                txtFecha1.DbSelectedDate = sesion.CalendarioIni;
            }
            if (sesion.CalendarioFin >= txtFecha2.MinDate && sesion.CalendarioFin <= txtFecha2.MaxDate)
            {
                txtFecha2.DbSelectedDate = sesion.CalendarioFin;
            }
            
            //Cargar grid
            rgRemisionSvtaAlmacen.Rebind();
        }

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
                    {
                        this.RadToolBar1.FindItemByValue("new").Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }

                //CN_Ctrl ctrl = new CN_Ctrl();
                //ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                //ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
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
            if (mensaje.Contains("CapRemisionSvtaAlmacen_MOdificar_Movimiento5"))
                Alerta("Movimiento generado en forma automática. Imposible modificar este documento");
            else
            if (mensaje.Contains("rgRemisionSvtaAlmacen_NeedDataSource"))
                Alerta("Error al momento de llenar el Grid");
            else
            if (mensaje.Contains("PermisoModificarDenegado"))
                Alerta("No tiene permisos para modificar");
            else
            if (mensaje.Contains("PermisoEliminarDenegado"))
                Alerta("No tiene permisos para dar de baja");
            else
            if (mensaje.Contains("PermisoImprimirDenegado"))
                Alerta("No tiene permisos para imprimir");
            else
            if (mensaje.Contains("CapRemisionSvtaAlmacen_Modificar_Denegado"))
                Alerta("Imposible modificar el documento");
            else
            if (mensaje.Contains("CapRemisionSvtaAlmacen_delete_ok"))
                Alerta("El registro de Remision a revisión o cobro se ha dado de baja (estatus \"B\") correctamente");
            else
            if (mensaje.Contains("CapRemisionSvtaALmacen_EstatusIncorrecto"))
                Alerta("No se puede realizar la operación. El estatus es incorrecto");
            else
            if (mensaje.Contains("CapRemisionSvtaAlmacen_EsBaja"))
                Alerta("El registro de Remision a revisión o cobro ya está dado de baja");
            else
            if (mensaje.Contains("CapRemisionSvtaALmacen_delete_error"))
                Alerta("Error al momento de dar de baja");
            else
            if (mensaje.Contains("RAM1_AjaxRequest"))
                Alerta("Error al momento de actualizar el grid");
            else
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