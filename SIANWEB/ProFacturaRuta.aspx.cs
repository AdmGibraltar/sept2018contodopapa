using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Collections;

namespace SIANWEB
{
    public partial class ProFacturaRuta : System.Web.UI.Page
    {
        #region Variables
        DataTable dt_detalles
        {
            get
            {
                return (DataTable)Session["dt_detalles" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }
                    , StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["dt_detalles" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }
                    , StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
        bool iva_cd
        {
            get
            {
                return (bool)Session["iva_cd" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }
                    , StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["iva_cd" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }
                    , StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
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
        //Propiedad de lista de la factura
        private List<Factura> ListaFacturaCompara
        {
            get { return (List<Factura>)Session["ListaFacturaCompara"]; }
            set { Session["ListaFacturaCompara"] = value; }
        }
        #endregion Variables

        #region Eventos
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        this.rgDetalles.Rebind();
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 100);
                        RadPageViewDetalles.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDGenerales.Width;
                        RadSplitter1.Height = altura;
                        RadPageViewDGenerales.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RadPageViewDGenerales.Width;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    RAM1.ResponseScripts.Add("RefreshParentPage()");
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

                        //obtener valores desde la URL
                        int Id_Emb = Convert.ToInt32(Page.Request.QueryString["Id_Emb"]);
                        int Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                        int Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                        string embModificable = Page.Request.QueryString["embModificable"].ToString();

                        Inicializar(embModificable);

                        double ancho = 0;
                        foreach (GridColumn gc in rgDetalles.Columns)
                        {
                            if (gc.Display && gc.Visible)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgDetalles.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgDetalles.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rcmbFac_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                RadComboBox rcmbFac = (sender as RadComboBox);
                Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)rcmbFac.Parent;
                RadNumericTextBox txtFact = tabla.FindControl("txtFact") as RadNumericTextBox;
                RadTextBox txtCte = tabla.FindControl("txtCte") as RadTextBox;
                RadNumericTextBox txtImporte = tabla.FindControl("txtImporte") as RadNumericTextBox;

                Factura factura = new Factura();
                factura.Id_Cd = this.sesion.Id_Cd_Ver;
                factura.Id_Emp = this.sesion.Id_Emp;
                if (txtFact.Text != string.Empty)
                    factura.Id_Fac = Convert.ToInt32(txtFact.Text);
                else
                    factura.Id_Fac = 0;

                CN_CapFactura CNCapFactura = new CN_CapFactura();
                try
                {
                    CNCapFactura.BuscaFacturaRuta(ref factura, sesion.Emp_Cnx);
                }
                catch (Exception ex)
                {
                    txtFact.Text = "";
                    txtCte.Text = "";
                    txtImporte.Text = "";
                    AlertaFocus(ex.Message, txtFact.ClientID);
                    return;
                }
                txtCte.Text = factura.Cte_NomComercial;
                txtImporte.Text = factura.Fac_Importe.ToString();

                txtCte.ReadOnly = true;
                txtImporte.ReadOnly = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rcmbFac_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    GridEditableItem editItem = (GridEditableItem)e.Item;
                    Control insertbtn = (Control)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        this.rgDetalles.Columns.FindByUniqueName("EditCommandColumn").Visible = true;
                        this.cargarEmbarque();
                    }
                }
                else
                    if (e.Item.IsDataBound)
                    {
                        GridDataItem item = (GridDataItem)e.Item;
                        item["EditCommandColumn"].Controls[0].Visible = false;
                        this.rgDetalles.Columns.FindByUniqueName("EditCommandColumn").Visible = false;
                    }
            }
            catch (Exception ex)
            {
                this.Alerta(string.Concat(ex.Message, "rcmbFac_ItemDataBound"));
            }
        }
        protected void rgDetalles_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                this.rgDetalles.Columns.FindByUniqueName("EditCommandColumn").Visible = true;
                RadComboBox rcmbFac = editItem.FindControl("rcbSerie") as RadComboBox;
                RadNumericTextBox txtFact = editItem.FindControl("txtFact") as RadNumericTextBox;
                new CN__Comun().LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spConsecutivos_Combo", ref rcmbFac);

                Control insertbtn = (Control)editItem.FindControl("PerformInsertButton");
                if (insertbtn != null)
                {
                    txtFact.Enabled = true;
                    (e.Item.FindControl("txtFact") as RadNumericTextBox).Enabled = true;
                }
            }
            else
                if (e.Item.IsDataBound)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    item["EditCommandColumn"].Controls[0].Visible = false;
                }
            //TODO: AGREGAR PARA PONER EL FOCUS
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem form = (GridEditableItem)e.Item;
                RadNumericTextBox dataField = (RadNumericTextBox)form["Factura"].FindControl("txtFact");
                dataField.Focus();
            }
        }
        protected void rgDetalles_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;

                RadNumericTextBox txtFact = editedItem.FindControl("txtFact") as RadNumericTextBox;
                RadComboBox cmbFac = editedItem.FindControl("rcbSerie") as RadComboBox;
                if (txtFact.Text == string.Empty || cmbFac.Text == string.Empty)
                {
                    this.Alerta("Seleccione primero una factura");
                    this.rgDetalles.Columns.FindByUniqueName("EditCommandColumn").Visible = true;
                    return;
                }
                else
                {
                    Factura factura = new Factura();
                    factura.Id_Cd = this.sesion.Id_Cd_Ver;
                    factura.Id_Emp = this.sesion.Id_Emp;
                    factura.Id_Fac = Convert.ToInt32(txtFact.Text);
                    CN_CapFactura CNCapFactura = new CN_CapFactura();
                    CNCapFactura.BuscaFacturaRuta(ref factura, sesion.Emp_Cnx);

                    DataRow[] ar = dt_detalles.Select("Id_Fac='" + factura.Id_Fac + "' and Id_FacSerie='" + factura.Id_FacSerie.Replace(factura.Id_Fac.ToString(), "") + "'");

                    if (ar.Length > 0)
                    {
                        Alerta("Esta factura ya ha sido asignada en esta ruta");
                    }
                    else
                    {
                        dt_detalles.Rows.Add(new object[] { factura.Id_Fac, factura.Id_FacSerie.Replace(factura.Id_Fac.ToString(), ""), factura.Cte_NomComercial, factura.Fac_Importe });
                    }

                    this.rgDetalles.Columns.FindByUniqueName("EditCommandColumn").Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                e.Canceled = true;
            }
        }
        protected void rgDetalles_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;

                int id_Fac = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Fac"]);
                string serie = item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_FacSerie"].ToString();
                //this.ListaFactura_EliminarFactura(id_Fac);


                DataRow[] ar = dt_detalles.Select("Id_Fac='" + id_Fac + "' and Id_FacSerie='" + serie.Replace(id_Fac.ToString(), "") + "'");
                if (ar.Length > 0)
                {
                    ar[0].BeginEdit();
                    ar[0].Delete();
                    dt_detalles.AcceptChanges();
                }
                else
                {
                    Alerta("No se encontro la factura");
                }
            }
            catch
            {
                this.Alerta("Error al quitar la factura del listado <br/>");
            }
        }
        /// <summary>
        /// Determina si las facturas han sido asignadas o no a la lita de la ruta
        /// </summary>
        /// <param name="facturaIns">Entidad de las facturas</param>
        /// <returns>true si puede continar, false en caso contrario</returns>
        protected bool ListaFactura_AgregarFactura(Factura facturaIns)
        {
            List<Factura> lista = this.ListaFacturaCompara;

            //buscar producto de factura en la lista para ver si ya existe
            for (int i = 0; i < lista.Count; i++)
            {
                Factura factura = lista[i];
                if (factura.Id_Fac == facturaIns.Id_Fac)//si el producto es el mismo
                {
                    this.Alerta("Esta factura ya ha sido asignada en esta ruta");
                    return false;
                }
            }
            lista.Add(facturaIns);
            this.ListaFacturaCompara = lista;
            return true;
        }
        /// <summary>
        /// Metodo para eliminar registros del listado de facturas por ruta
        /// </summary>
        /// <param name="id_fac">Id de la factura a remover</param>
        protected void ListaFactura_EliminarFactura(int id_fac)
        {
            try
            {
                List<Factura> lista = this.ListaFacturaCompara;
                int verificador = 0;
                Factura factura2 = new Factura();
                factura2.Id_Emp = this.sesion.Id_Emp;
                factura2.Id_Cd = this.sesion.Id_Cd_Ver;
                factura2.Id_Fac = id_fac;
                if (this.HF_ID.Value != string.Empty)
                    factura2.Id_Emb = Convert.ToInt32(this.txtEmbarque.Text);
                else
                    verificador = Convert.ToInt32(this.txtEmbarque.Text);

                CN_Embarques CNEmbarques = new CN_Embarques();
                //buscar producto de factura en la lista
                for (int i = 0; i < lista.Count; i++)
                {
                    Factura factura = lista[i];
                    if (factura.Id_Fac == id_fac)
                    {
                        CNEmbarques.RegresaEstatusFactura(factura2, this.sesion.Emp_Cnx, ref verificador);

                        if (verificador > 0)
                            lista.RemoveAt(i);
                        else
                            this.Alerta("No es posible eliminar este registro del listado");
                    }
                }
                this.ListaFacturaCompara = lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                if (btn.CommandName == "save")
                    if (Page.IsValid)
                        this.Guardar();
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
                    rgDetalles.DataSource = dt_detalles;
                }

            }
            catch (Exception)
            {
                this.Alerta("Error al cargar el grid de facturas en embarque");
            }
        }
        protected void rgDetalles_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                if (!validarCamposDetalle())
                {
                    e.Canceled = true;
                    return;
                }
                if (!validarFecha(this.dpFecha))
                {
                    e.Canceled = true;
                    return;
                }
            }
            if (e.CommandName == "Edit")
            {
                this.Alerta("No puede editar esta inforación");
                return;

            }
        }
        protected void txtFac_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RadNumericTextBox txtfac = sender as RadNumericTextBox;
                RadComboBox txtserie = (txtfac.Parent.FindControl("rcbSerie") as RadComboBox);
                RadTextBox rgCteNombre = (txtfac.Parent.FindControl("txtCte") as RadTextBox);
                RadNumericTextBox rgtxtImporte = (txtfac.Parent.FindControl("txtImporte") as RadNumericTextBox);

                DataRow[] ar = dt_detalles.Select("Id_Fac='" + txtfac.Text + "' and Id_FacSerie='" + txtserie.Text + "'");

                if (ar.Length > 0)
                {
                    AlertaFocus("Esta factura ya ha sido asignada en esta ruta", txtfac.ClientID);
                    txtfac.Text = "";
                    rgCteNombre.Text = "";
                    rgtxtImporte.Text = "";
                    return;
                }
                Factura factura = new Factura();
                factura.Id_Cd = this.sesion.Id_Cd_Ver;
                factura.Id_Emp = this.sesion.Id_Emp;
                factura.Id_Fac = Convert.ToInt32(txtfac.Text);
                CN_CapFactura CNCapFactura = new CN_CapFactura();
                try
                {
                    CNCapFactura.BuscaFacturaRuta(ref factura, sesion.Emp_Cnx);
                }
                catch (Exception ex)
                {
                    txtfac.Text = "";
                    rgCteNombre.Text = "";
                    rgtxtImporte.Text = "";
                    AlertaFocus(ex.Message, txtfac.ClientID);
                    return;
                }
                rgCteNombre.Text = factura.Cte_NomComercial;
                rgtxtImporte.Text = (factura.Fac_Importe).ToString();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        #endregion Eventos

        #region Funciones
        private void Inicializar(string embModificable)
        {
            this.dpFecha.SelectedDate = DateTime.Now;
            this.txtDia.SelectedDate = DateTime.Now;
            //nueva variable para controlar tabla dinamica de facturas por ruta de embarque
            Session["ListaFacturaCompara"] = new List<Factura>();
            this.crearDT();
            if (Request.QueryString["id"] != "0")
            {
                this.txtEmbarque.Text = Request.QueryString["id"];
                this.HF_ID.Value = Request.QueryString["id"];

                if (Convert.ToInt32(this.HF_ID.Value) != 0)
                {
                    this.cargarEmbarque();
                }
            }
            else
                this.txtEmbarque.Text = MaximoId();
            //PARA OCULTAR LA COLUMNA CON LA OPCION DE "EDITAR"
            this.rgDetalles.Columns.FindByUniqueName("EditCommandColumn").Visible = false;
            rgDetalles.Rebind();
            if (embModificable != "0")
            {
                this.RadToolBar1.Items[1].Visible = true;
            }
            else
            {
                this.RadToolBar1.Items[1].Visible = false;
            }
        }
        private bool validarFecha(RadDatePicker comparaFecha)
        {
            try
            {
                if (!((comparaFecha.SelectedDate >= sesion.CalendarioIni) && (comparaFecha.SelectedDate <= sesion.CalendarioFin)))
                {
                    Alerta("Fecha se encuentra fuera del periodo");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void cargarEmbarque()
        {
            try
            {
                Embarques embarques = new Embarques();
                List<Embarques> listaEmbarques = new List<Embarques>();

                embarques.Id_Emp = sesion.Id_Emp;
                embarques.Id_Cd = sesion.Id_Cd_Ver;
                embarques.Id_Emb = Convert.ToInt32(this.HF_ID.Value);

                CN_ProFacturaRuta_Embarque CNProFacturaRuraEmbarque = new CN_ProFacturaRuta_Embarque();
                CNProFacturaRuraEmbarque.ConsultaProFacturaRutaEmbarque(ref embarques, sesion.Emp_Cnx);

                this.dpFecha.SelectedDate = embarques.Emb_Fec;
                this.txtChofer.Text = embarques.Emb_Chofer;
                if (embarques.Emb_Dia > DateTime.MinValue && embarques.Emb_Dia != Convert.ToDateTime("01/01/1900"))
                    this.txtDia.SelectedDate = embarques.Emb_Dia;
                this.txtCamioneta.Text = embarques.Emb_Camioneta;

                this.llenaGrid(Convert.ToInt32(this.HF_ID.Value));
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
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "Embarques", "Id_Emb", sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void crearDT()
        {
            dt_detalles = new DataTable();
            dt_detalles.Columns.Add("Id_Fac");
            dt_detalles.Columns.Add("Id_FacSerie");
            dt_detalles.Columns.Add("Cte_NomComercial");
            dt_detalles.Columns.Add("Fac_Importe");
        }
        private bool validarCamposDetalle()
        {
            if (this.txtChofer.Text == string.Empty)
            {
                this.Alerta("Especifique el nombre del chofer");
                return false;
            }
            if (this.txtCamioneta.Text == string.Empty)
            {
                this.Alerta("Especifique la camioneta");
                return false;
            }
            return true;
        }
        protected int ObtenerIdFac(object oc)
        {
            if (oc is Factura)
            {
                return ((Factura)oc).Id_Fac;
            }
            else
            {
                return 0;
            }
        }
        protected string ObtenerCte(object oc)
        {
            if (oc is Factura)
                return (((Factura)oc).Cte_NomComercial).ToString();
            else
                return string.Empty;
        }
        protected float ObtenerImporte(object oc)
        {
            if (oc is Factura)
                return float.Parse(((Factura)oc).Fac_Importe.ToString());
            else
                return 0;
        }
        private void Guardar()
        {
            try
            {
                if (!validarCamposDetalle())
                {
                    this.RadTabStrip1.Focus();
                    this.RadMultiPage1.Focus();
                    if (this.txtCamioneta.Text == string.Empty)
                        this.txtCamioneta.Focus();
                    if (this.txtChofer.Text == string.Empty)
                        this.txtChofer.Focus();
                    return;
                }
                if (!validarFecha(this.dpFecha))
                {
                    this.dpFecha.Focus();
                    this.RadTabStrip1.Focus();
                    this.RadMultiPage1.Focus();
                    return;
                }

                if (dt_detalles == null || dt_detalles.Rows.Count == 0)
                {
                    Alerta("Aún no se han capturado facturas");
                    return;
                }
                if (this.dpFecha.SelectedDate > this.txtDia.SelectedDate)
                {
                    this.Alerta("El día no puede ser menor a la fecha");
                    return;
                }
                Embarques embarques = new Embarques();

                embarques.Id_Emp = sesion.Id_Emp;
                embarques.Id_Cd = sesion.Id_Cd_Ver;
                embarques.Emb_Fec = Convert.ToDateTime(this.dpFecha.SelectedDate);
                embarques.Emb_Chofer = this.txtChofer.Text;
                embarques.Emb_Dia = Convert.ToDateTime(this.txtDia.SelectedDate);
                embarques.Emb_Camioneta = this.txtCamioneta.Text;
                embarques.Emb_Estatus = "C";
                embarques.Id_U = sesion.Id_U;

                if (this.HF_ID.Value != string.Empty)
                    embarques.Id_Emb = Convert.ToInt32(this.txtEmbarque.Text);
                else
                    embarques.Id_Emb = -1;

                List<EmbarquesDet> listaEmbarquesDet = new List<EmbarquesDet>();
                EmbarquesDet embarquesDet = new EmbarquesDet();

                foreach (DataRow dr in dt_detalles.Rows)
                {
                    //Genera la lista para la tabla detalles del embarque (EmbarquesDet)
                    embarquesDet = new EmbarquesDet();

                    embarquesDet.Id_Emp = sesion.Id_Emp;
                    embarquesDet.Id_Cd = sesion.Id_Cd_Ver;
                    embarquesDet.Id_Emb = Convert.ToInt32(this.txtEmbarque.Text);
                    embarquesDet.Id_Fac = Convert.ToInt32(dr["Id_Fac"].ToString());

                    listaEmbarquesDet.Add(embarquesDet);
                }

                List<Factura> listaFactura = new List<Factura>();
                Factura factura = new Factura();

                foreach (DataRow dr in dt_detalles.Rows)
                {
                    factura = new Factura();
                    //Genera la lista actualizar el estatus en la table factura
                    factura.Id_Emp = sesion.Id_Emp;
                    factura.Id_Cd = sesion.Id_Cd_Ver;
                    factura.Id_Fac = Convert.ToInt32(dr["Id_Fac"].ToString());
                    factura.Fac_Estatus = "E";

                    listaFactura.Add(factura);
                }

                string mensaje = string.Empty;
                int verificador = -1;

                CN_Embarques CNEmbarques = new CN_Embarques();
                CNEmbarques.GuardaEmbarques(embarques, listaEmbarquesDet, sesion, listaFactura, ref verificador);

                if (verificador > 0)
                {
                    mensaje = "Los datos se guardaron correctamente";
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')"));
                }
                else
                    this.Alerta("No fue posible guardar la información");
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
                funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void llenaGrid(int Id_Emb)
        {
            try
            {
                Factura factura = new Factura();
                factura.Id_Emp = this.sesion.Id_Emp;
                factura.Id_Cd = this.sesion.Id_Cd_Ver;
                factura.Id_Emb = Id_Emb;

                List<Factura> listaFactura = new List<Factura>();

                CN_Embarques CNEmbarques = new CN_Embarques();
                CNEmbarques.LlenaGridProFacturaRuta(ref factura, ref listaFactura, sesion.Emp_Cnx);


                foreach (Factura f in listaFactura)
                {
                    dt_detalles.Rows.Add(new object[] { f.Id_Fac, f.Id_FacSerie.Replace(f.Id_Fac.ToString(), ""), f.Cte_NomComercial, f.Fac_Importe });
                }

                //rgDetalles.Rebind();
                //this.rgDetalles.DataSource = listaFactura;
                //this.ListaFacturaCompara = listaFactura;
                //for (int i = 0; i < listaFactura.Count; i++)
                //    dt_detalles.Rows.Add(new object[] { factura.Id_Fac, factura.Id_FacSerie.Replace(factura.Id_Fac.ToString(), ""), factura.Cte_NomComercial, factura.Fac_Importe });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Funciones

        #region ErrorManager
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
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