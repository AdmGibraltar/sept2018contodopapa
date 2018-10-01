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
using System.Configuration;
using System.Collections;
using CapaDatos;

namespace SIANWEB
{
    public partial class Pro_ActualizacionComodato : System.Web.UI.Page
    {
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
                    Response.Redirect("login.aspx", false);
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

                        txtFecha1.DbSelectedDate = Sesion.CalendarioIni;
                        txtFecha2.DbSelectedDate = Sesion.CalendarioFin;

                        double ancho = 0;
                        foreach (GridColumn gc in rgBase.Columns)
                        {
                            if (gc.Display && gc.Visible)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgBase.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgBase.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        #region "Métodos para obtrener desde objetos los valores para los controles durante la inserción/actualización de un Grid editable"

        protected string ObtenerCliente(object oc)
        {
            if (oc is ContratoComodato) { return string.Concat(((ContratoComodato)oc).Id_Cte.ToString(), " - ", ((ContratoComodato)oc).Cte_NomComercial); } else { return string.Empty; }
        }

        protected string ObtenerTerritorio(object oc)
        {
            if (oc is ContratoComodato) { return string.Concat(((ContratoComodato)oc).Id_Ter.ToString(), " - ", ((ContratoComodato)oc).Ter_Nombre); } else { return string.Empty; }
        }

        protected string ObtenerRik(object oc)
        {
            if (oc is ContratoComodato) { return string.Concat(((ContratoComodato)oc).Id_Rik.ToString(), " - ", ((ContratoComodato)oc).Rik_Nombre); } else { return string.Empty; }
        }

        protected string ObtenerProducto(object oc)
        {
            if (oc is ContratoComodato) { return string.Concat(((ContratoComodato)oc).ContratoComodatoDetalle.Id_Prd.ToString(), " - ", ((ContratoComodato)oc).ContratoComodatoDetalle.Prd_Descripcion); } else { return string.Empty; }
        }

        protected string ObtenerFecha(object oc)
        {
            if (oc is ContratoComodato) { return ((ContratoComodato)oc).Cco_Fecha.ToString("dd/MM/YYYY"); } else { return string.Empty; }
        }

        protected string ObtenerCantidad(object oc)
        {
            if (oc is ContratoComodato) { return ((ContratoComodato)oc).ContratoComodatoDetalle.Cco_Cantidad.ToString("N"); } else { return string.Empty; }
        }

        #endregion

        #region Eventos
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "ImprimirContrato":
                        if (Session["ComodatoVentana" + Session.SessionID] != null)
                        {
                            int folio = Convert.ToInt32(Session["ComodatoVentana" + Session.SessionID]);
                            Session["ComodatoVentana" + Session.SessionID] = null;
                            ImprimirContrato(folio);

                        }
                        break;
                    case "cliente":
                        txtCliente.Text = Session["Id_Buscar" + Session.SessionID].ToString();
                        txtCliente_TextChanged(null, null);
                        break;
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "RAM1_AjaxRequest"));
            }
        }

        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ConsultarDatosCliente(txtCliente.Text);
                CargarTerritorios(ref cmbTerritorio);
                txtTerritorio.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void ConsultarDatosCliente(string idCliente)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (idCliente != string.Empty && idCliente != "-1")
                { //Consultar clientes
                    Clientes cliente = new Clientes();
                    cliente.Id_Emp = sesion.Id_Emp;
                    cliente.Id_Cd = sesion.Id_Cd_Ver;
                    cliente.Id_Rik = sesion.Id_Rik;
                    cliente.Id_Cte = Convert.ToInt32(idCliente);
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                    txtClienteNombre.Text = cliente.Cte_NomComercial;
                }
                else
                    txtClienteNombre.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
                //this.CargarCliente(ref cmbCliente);
                this.CargarTerritorios(ref cmbTerritorio);
                txtCliente.Text = string.Empty;
                txtTerritorio.Text = string.Empty;
                txtClienteNombre.Text = string.Empty;
                //cmbCliente.SelectedIndex = cmbCliente.FindItemIndexByValue("-1");
                cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue("-1");
                txtFecha1.SelectedDate = null;
                txtFecha2.SelectedDate = null;
                rgBase.DataSource = new List<ContratoComodato>();
                rgBase.DataBind();
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
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {  //Llenar Grid
                    rgBase.DataSource = this.GetList();
                    rgBase.HierarchySettings.ExpandTooltip = "Expandir";
                    rgBase.HierarchySettings.CollapseTooltip = "Contraer";
                }
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

        protected void rgBase_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            try
            {
                GridDataItem parentItem = e.DetailTableView.ParentItem as GridDataItem;
                if (parentItem.Edit)
                    return;
                if (e.DetailTableView.DataMember == "listaContratoComDet")
                {
                    e.DetailTableView.DataSource = this.GetListDetalle(
                        Convert.ToInt32(parentItem.OwnerTableView.DataKeyValues[parentItem.ItemIndex]["Id_Emp"])
                        , Convert.ToInt32(parentItem.OwnerTableView.DataKeyValues[parentItem.ItemIndex]["Id_Cd"])
                        , Convert.ToInt32(parentItem.OwnerTableView.DataKeyValues[parentItem.ItemIndex]["Id_Rem"]));
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgBaseDetalle_NeedDataSource"));
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            string mensajeError = string.Empty;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "save":
                        mensajeError = "CapContratoComodato_update_error";
                        this.Guardar();
                        break;

                    case "print":
                        mensajeError = "CapContratoComodato_imprimir_error";
                        this.ImprimirContrato(1);
                        break;
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, mensajeError);
                this.DisplayMensajeAlerta(mensaje);
            }
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                rgBase.Rebind();
                this.RadToolBar1.FindItemByValue("save").Enabled = true;
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "btnBuscar_error"));
            }
        }
        #endregion

        #region Funciones
        private void Guardar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string mensajeError = string.Empty;
            try
            {
                ErrorManager();
                int verificador = 0;
                List<ContratoComodato> listaContratoCom = new List<ContratoComodato>();
                Funciones funcion = new Funciones();
                //string statusPosibles = "C,I,E,N";

                if (((GridItemCollection)rgBase.SelectedItems).Count == 0)
                    this.DisplayMensajeAlerta("rgContratoComodato_NoSelectItems");
                else
                {
                    foreach (GridDataItem item in rgBase.SelectedItems) // loop through each selected row
                    {
                        if (item.OwnerTableView.Name == "Master")
                        {
                            ContratoComodato cm = new ContratoComodato();
                            cm.Id_Emp = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Emp"]);
                            cm.Id_Cd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Cd"]);
                            cm.Id_Cte = (int)txtCliente.Value;
                            cm.Id_Ter = (int)txtTerritorio.Value;
                            cm.Id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
                            cm.Cantidad = (item.FindControl("txtCantidad") as RadNumericTextBox).Value.HasValue ? (int)(item.FindControl("txtCantidad") as RadNumericTextBox).Value : 0;
                            cm.Cco_FechaIni = (DateTime?)(item.FindControl("rdpFechaIni") as RadDatePicker).DbSelectedDate;
                            cm.Cco_FechaFin = (DateTime?)(item.FindControl("rdpFechaFin") as RadDatePicker).DbSelectedDate;
                            cm.Cco_Fecha = funcion.GetLocalDateTime(sesion.Minutos);
                            cm.Id_U = sesion.Id_U;
                            //cm.Id_Cco = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Cco"]);

                            //if (!statusPosibles.Contains(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Rem_Estatus"].ToString().ToUpper()))
                            //{
                            //    throw new Exception("rgContratoComodato_imprimir_estatusNoValido");
                            //}

                            //Si ya esta en la lista el contrato, NO lo agrega
                            //bool existe_cm = false;
                            //foreach (ContratoComodato contCom in listaContratoCom)
                            //{
                            //    if (cm.Id_Emp == contCom.Id_Emp && cm.Id_Cd == contCom.Id_Cd && cm.Id_Cco == contCom.Id_Cco)
                            //    {
                            //        existe_cm = true;
                            //        break;
                            //    }
                            //}
                            //if (!existe_cm)
                            //{
                            listaContratoCom.Add(cm);
                            //}
                        }
                    }
                    if (listaContratoCom.Count > 0)
                    {
                        new CN_ContratoComodato().ModificarContratoComodato_FechaContrato(ref listaContratoCom, ref verificador, sesion.Emp_Cnx);
                        //this.RadToolBar1.FindItemByValue("save").Enabled = false;
                        //this.DisplayMensajeAlerta("rgContratoComodato_update_ok");
                        rgBase.Rebind();
                        ImprimirContrato(verificador);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ImprimirContrato(int folio)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_ContratoComodato cn_Contrato = new CN_ContratoComodato();
                int verificador = 0;
                cn_Contrato.Consultar(sesion.Id_Emp, sesion.Id_Cd_Ver, folio, ref verificador, sesion.Emp_Cnx);

                if (verificador > 0)
                {
                    ArrayList ALValorParametrosInternos = new ArrayList();
                   
                    ALValorParametrosInternos.Add(sesion.Id_Emp);
                    ALValorParametrosInternos.Add(sesion.Emp_Nombre);                    
                    ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                    ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                    ALValorParametrosInternos.Add(folio);

                    Type instance = null;
                    instance = typeof(LibreriaReportes.RemisionContratoComodatoElectronico);
                    Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value + "nx"] = null;
                    Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value + "nx"] = ALValorParametrosInternos;
                    Session["assembly" + Session.SessionID + HF_ClvPag.Value + "nx"] = instance.AssemblyQualifiedName;

                    RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value  + "nx" + "');");
                }
                else
                {
                    Alerta("El Contrato no existe");
                }
                //RadAjaxManager1.ResponseScripts.Add("refreshGrid();");
                //}
                //else
                //{
                //    this.DisplayMensajeAlerta("rgContratoComodato_NoSelectItems");
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ContratoComodato> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<ContratoComodato> listaContratoCom = new List<ContratoComodato>();
                ContratoComodato contratoComodato = new ContratoComodato();

                int? objectInt = null;

                contratoComodato.Id_Emp = sesion.Id_Emp;
                contratoComodato.Id_Cd = sesion.Id_Cd_Ver;
                contratoComodato.Id_U = sesion.Propia ? sesion.Id_U : objectInt;
                contratoComodato.Id_Ter = Convert.ToInt32(cmbTerritorio.SelectedValue);
                contratoComodato.Id_Cte = Convert.ToInt32(txtCliente.Text);

                new CN_ContratoComodato().ConsultarContratoComodato_BaseInstalada(
                    contratoComodato
                    , ref listaContratoCom
                    , Convert.ToDateTime(this.txtFecha1.SelectedDate)
                    , Convert.ToDateTime(this.txtFecha2.SelectedDate)
                    , sesion.Emp_Cnx);

                return listaContratoCom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<RemisionDet> GetListDetalle(int Id_Emp, int Id_Cd, int Id_Rem)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Remision remision = new Remision();

                remision.Id_Emp = Id_Emp;
                remision.Id_Cd = Id_Cd;
                remision.Id_Rem = Id_Rem;
                new CN_CapRemision().ConsultaRemisionDetalleContratoComodato(ref remision, sesion.Emp_Cnx);
                return remision.ListRemisionDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            this.CargarCentros();
            // this.CargarCliente(ref cmbCliente);
            this.CargarTerritorios(ref cmbTerritorio);

            rgBase.DataSource = new List<ContratoComodato>();
            rgBase.DataBind();
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
                    if (Permiso.PGrabar == false)
                        this.RadToolBar1.Items[1].Visible = false;
                    //this.RadToolBar1.FindItemByValue("save").Visible = false;                  

                    if (Permiso.PImprimir == false)
                        this.RadToolBar1.Items[2].Visible = false;
                    //this.RadToolBar1.FindItemByValue("print").Visible = false;                   
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

        //private void CargarCliente(ref RadComboBox combo)
        //{
        //    try
        //    {
        //        Sesion Sesion = new Sesion();
        //        Sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //        CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
        //        CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatCliente_Combo", ref combo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void CargarTerritorios(ref RadComboBox combo)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, txtCliente.Value.HasValue ? Convert.ToInt32(txtCliente.Value.Value) : -1, Sesion.Id_Rik != -1 ? Sesion.Id_Rik : (int?)null, Sesion.Emp_Cnx, "spCatTerritorioCte_Combo", ref combo);
                txtTerritorio.Text = "";
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
                if (mensaje.Contains("rgContratoComodato_NoSelectItems"))
                    Alerta("No ha seleccionado ningún contrato");
                else
                    if (mensaje.Contains("rgContratoComodato_imprimir_estatusNoValido"))
                        Alerta("La remisión de uno de los contratos seleccionados tiene un estatus no válido");
                    else
                        if (mensaje.Contains("rgBase_NeedDataSource"))
                            Alerta("Error al obtener la información del grid de contratos de comodato");
                        else
                            if (mensaje.Contains("rgBaseDetalle_NeedDataSource"))
                                Alerta("Error al obtener la información de detalle del contrato de comodato");
                            else
                                if (mensaje.Contains("rgContratoComodato_update_ok"))
                                    Alerta("La fecha de los contratos de comodato seleccionados ha sido actualizada correctamente");
                                else
                                    if (mensaje.Contains("CapContratoComodato_update_error"))
                                        Alerta("Error al momento de actualizar la fecha del contrato de comodato");
                                    else
                                        if (mensaje.Contains("CapContratoComodato_imprimir_error"))
                                            Alerta("Error al momento de imprimir el contrato de comodato");
                                        else
                                            if (mensaje.Contains("btnBuscar_error"))
                                                Alerta("Error al momento de actualizar el grid de base instalada de contrato de comodato");
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

        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("popup();");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
    }
}