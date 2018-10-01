using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using CapaDatos;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using Telerik.Web.UI.Calendar;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.Services;


namespace SIANWEB
{
    public partial class CapPagosElectoronicosModifica : System.Web.UI.Page
    {
        private string CryptoPassIn = "k3y9u1m1c4";
        private string CryptoPassOut = "k3y9u1m1c4";
        private byte[] PagElec_Soporte4 = null;
        public string NombreArchivo;
        public string Nombreextension;
        private decimal totalpartidas = 0;
        private int id_PagElec = 0;

        private string BuzonKey
        {
            get
            {
                if (Session["TimeBuzonServer"] == null || Session["TimeBuzonServer"].ToString() == "")
                {
                    wsBuzonWeb.wsBuzonWeb wsBuzon = new wsBuzonWeb.wsBuzonWeb();
                    DateTime FechaServer = Convert.ToDateTime(CapaDatos.clsCrypto.BlowFish.Decrypt(wsBuzon.GetKey(), CryptoPassIn)).AddSeconds(-20);
                    Session["TimeBuzonServer"] = (FechaServer - DateTime.Now).TotalSeconds;
                }

                DateTime TimeServer = DateTime.Now.AddSeconds(Convert.ToDouble(Session["TimeBuzonServer"]));

                string key = String.Format(
                    "{0}|&|{1}|&|{2}",
                    "SIANWEB",
                    TimeServer.ToString("yyyy-MM-dd HH:mm:ss"),
                    TimeServer.AddSeconds(80).ToString("yyyy-MM-dd HH:mm:ss")
                );

                return CapaDatos.clsCrypto.BlowFish.Encrypt(key, CryptoPassOut);
            }
        }

        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        protected string Emp_RFC
        {
            get
            {
                return (string)(Session["Emp_RFC"] != null ? Session["Emp_RFC"] : GetEmp_RFC());
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            id_PagElec = Int32.Parse(Request.QueryString["Id"] == null ? "-1" : Request.QueryString["Id"]);

            if (Sesion == null)
            {
                //string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                //Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                RAM1.ResponseScripts.Add("CloseWindow();");
            }
            else
            {
                CargarProveedores();
                CargarCtaGastos();

                if (!IsPostBack)
                {

                    ValidarPermisos();
                    CargarTipos();
                    CargarAcreedores();
                    //     inicializartabla();
                    Inicializar();

                }

            }
            //JFCV 24oct2016 que pueda teclearse el numero de proveedor punto 4
            txtProveedor.TextChanged += new EventHandler(RadInput_TextChanged);


        }

        private string GetEmp_RFC()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            Session["Emp_RFC"] = (new CN_CapPagoElectronico().ConsultaEmpRFC(Sesion.Id_Emp, Sesion.Emp_Cnx));

            return (string)Session["Emp_RFC"];
        }


        protected void CmbTipo_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CmbAcreedor.Enabled = true;
            if (CmbTipo.SelectedValue == "1")
            {
                lblSubTipoGasto.Visible = true;
                CmbSubTipoGasto.Visible = true;
              //jfcv 16dic2016
                cmbCtaGastos.Enabled = false;

            }
            else
            {
                lblSubTipoGasto.Visible = false;
                CmbSubTipoGasto.Visible = false;

                //jfcv 16dic2016
                cmbCtaGastos.Enabled = true;
                if (CmbTipo.SelectedValue == "2")
                {
                    ChkConComprobante.Checked = true;
                }

            }

            habilitarcontroles();
        }

        //JFCV 16 Dic 2016 
        protected void CmbSubTipo_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            List<PagoElectronicoCuenta> CtaGastos = new List<PagoElectronicoCuenta>();

            cmbCtaGastos.Enabled = true;
            cmbCtaGastos.Items.Clear();
            //JFCV subtipos 19dic2016
            int subtipo = Convert.ToInt32(e.Value);
            subtipo = Convert.ToInt32(CmbSubTipoGasto.SelectedValue);
            if (subtipo < 1)
            {
                cmbCtaGastos.Enabled = false;
            }
            else
            {
                cmbCtaGastos.Enabled = true;
            }
            cmbCtaGastos.SelectedValue = "-1";
            cmbCtaGastos.Text = "";
            TxtCc.Text = "";
            TxtCuenta.Text = "";
            TxtCuentaPago.Text = "";
            //if (CmbTipo.SelectedValue != "")


            (new CN_CatPagoElectronicoCuenta()).ConsultaCuenta(
                new PagoElectronicoCuenta() { Id_Emp = Sesion.Id_Emp, Id_Cd = Sesion.Id_Cd_Ver, Id_Subtipo = subtipo },
                Sesion.Emp_Cnx,
                ref CtaGastos
            );


            if (CtaGastos.Count > 0)
            {
                //JFCV 18 noviembre 2016 que se pueda buscar por la subcuenta  punto 5
                var datasource = from x in CtaGastos
                                 select new
                                 {
                                     x.Id_Emp,
                                     x.Id_Cd,
                                     x.Id_PagElecCuenta,
                                     x.PagElecCuenta_CC,
                                     x.PagElecCuenta_CuentaPago,
                                     x.PagElecCuenta_Descripcion,
                                     x.PagElecCuenta_Numero,
                                     x.PagElecCuenta_SubCuenta,
                                     x.PagElecCuenta_SubSubCuenta,
                                     DisplayField = String.Format("{0} ({1})", x.PagElecCuenta_Descripcion, x.PagElecCuenta_SubCuenta)
                                 };


                cmbCtaGastos.DataSource = datasource;
                cmbCtaGastos.DataValueField = "Id_PagElecCuenta";
                cmbCtaGastos.DataTextField = "DisplayField";
                //cmbCtaGastos.DataTextField = "PagElecCuenta_Descripcion" ;
                cmbCtaGastos.DataBind();
            }

        }


        protected void cmbCtaGastos_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];


                if (e.Value != null && e.Value != "")
                {
                    PagoElectronicoCuenta CtaGastos = new PagoElectronicoCuenta()
                    {
                        Id_Emp = Sesion.Id_Emp,
                        Id_Cd = Sesion.Id_Cd_Ver,
                        Id_PagElecCuenta = Convert.ToInt32(e.Value)
                    };

                    (new CN_CatPagoElectronicoCuenta()).ConsultaCuenta(
                        CtaGastos,
                        Sesion.Emp_Cnx
                    );

                    TxtCuenta.Text = CtaGastos.PagElecCuenta_Numero;
                    TxtCc.Text = CtaGastos.PagElecCuenta_CC;
                    TxtNumero.Text = CtaGastos.PagElecCuenta_Numero;
                    TxtCuentaPago.Text = CtaGastos.PagElecCuenta_CuentaPago;
                    TxtSubCuenta.Text = CtaGastos.PagElecCuenta_SubCuenta;
                    TxtSubSubCuenta.Text = CtaGastos.PagElecCuenta_SubSubCuenta;
                }
                else
                {
                    TxtCuenta.Text = "";
                    TxtCc.Text = "";
                    TxtNumero.Text = "";
                    TxtCuentaPago.Text = "";
                    TxtSubCuenta.Text = "";
                    TxtSubSubCuenta.Text = "";

                }




                //JFCV  11 feb activar el grid si es tiposol = 1 
                if (CmbTipo.SelectedValue == "1")
                {
                    rgPagoElectronico.Visible = true;
                }

                habilitarcontroles();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "cmbCtaGastos_SelectedIndexChanged");
            }
        }

        protected void cmbProveedor_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs args)
        {
            if (args.Value.Trim() != "")
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];

                Acreedor provee = new Acreedor() { Id_Emp = session.Id_Emp, Id_Cd = session.Id_Cd_Ver, Id_Acr = Convert.ToInt32(args.Value.Trim()) };
                new CN_CatAcreedor().ConsultaAcreedor(provee, session.Emp_Cnx);

                txtProveedor.Text = provee.Acr_NumeroGenerado == null ? "Sin Autorizacion" : provee.Acr_NumeroGenerado.ToString().Trim().ToUpper();

                RadListBoxDestination.Items.Clear();
            }
        }

         
        //jfcv 26oct2016 que solicite el numero de proveedor punto 4
        protected void RadInput_TextChanged(object sender, EventArgs e)
        {
            RadTextBox tb = (RadTextBox)sender;
            if (tb.Text.Trim() != "")
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];

                Acreedor provee = new Acreedor() { Id_Emp = session.Id_Emp, Id_Cd = session.Id_Cd_Ver, Acr_NumeroGenerado = tb.Text.Trim() };
                new CN_CatAcreedor().ConsultaAcreedorPorNumero(provee, session.Emp_Cnx);

                if (provee.Id_Acr != 0)
                {
                    this.cmbProveedor.SelectedValue = provee.Id_Acr.ToString();
                    this.cmbProveedor.Text = provee.Acr_Nombre.ToString();
                }
                else
                {
                    cmbProveedor.ClearSelection();
                }
            }
            RadListBoxDestination.Items.Clear();
                

        }


        //jfcv 16Nov2016 Inicio que permita editar un registro  punto 5
        protected void rgPagoElectronico_ItemDataBound(object sender, GridItemEventArgs e)
        {
             try
            {



                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditableItem editItem = (GridEditableItem)e.Item;

                    //obtener nombres de los controles de formulario de inserción/edición de registro de grid.
                    //RadNumericTextBox Ctrl_txtOrd_Cantidad = (RadNumericTextBox)editItem.FindControl("txtFac_Cantidad");
                    //string lblFac_Cantidad = ((Label)editItem.FindControl("lblVal_txtFac_Cantidad")).ClientID.ToString();
                    //string txtFac_Cantidad = Ctrl_txtOrd_Cantidad.ClientID.ToString();
                    //string lbl_cmbProducto = ((Label)editItem.FindControl("lbl_cmbProducto")).ClientID.ToString();
                    //string txtId_Prd = ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).ClientID.ToString();
                    //string lbltxtTerritorioPartida = ((Label)editItem.FindControl("lbltxtTerritorioPartida")).ClientID.ToString();
                    //string txtTerritorioPartida = ((RadNumericTextBox)editItem.FindControl("txtTerritorioPartida")).ClientID.ToString();
                    //string lblTxtClienteExterno = ((Label)editItem.FindControl("lblTxtClienteExterno")).ClientID.ToString();
                    //string txtClienteExterno = ((RadNumericTextBox)editItem.FindControl("txtClienteExterno")).ClientID.ToString();

                    ////Llenar combo de productos
                    //RadComboBox comboProductoItem = (RadComboBox)editItem.FindControl("cmbProducto");
                    ////comboProductoItem.DataSource = this.ListaProductos;
                    ////comboProductoItem.DataBind();
                    ////
                    //CargarProductos(comboProductoItem);
                    //comboProductoItem.SelectedIndex = 0;

                    //Llenar combo de clientes, solo si es movimiento 70
                    //si no, el combo se oculta
                    //RadComboBox comboClientesItem = (RadComboBox)editItem.FindControl("cmbClienteExterno");
                    //RadNumericTextBox txtClienteExternoItem = (RadNumericTextBox)editItem.FindControl("txtClienteExterno");
                    //RadTextBox txtClienteExternoStr = (RadTextBox)editItem.FindControl("txtNombreCliente");
                    ////if (cmbMov.SelectedValue == "70")
                    ////{
                    //    //((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).ReadOnly = false;
                    //    ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).Display = true;
                    //    ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExtN")).Display = true;

                    ////}
                    ////else
                    ////{
                    ////    ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExtN")).Display = false;
                    ////    ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).Display = false;


                    ////}


                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];



                    RadComboBox CtaGastosPartidaItem = (RadComboBox)editItem.FindControl("txtcmbCtaGastos");

                    List<PagoElectronicoCuenta> CtaGastos = new List<PagoElectronicoCuenta>();



                    (new CN_CatPagoElectronicoCuenta()).ConsultaCuenta(
                        new PagoElectronicoCuenta() { Id_Emp = Sesion.Id_Emp, Id_Cd = Sesion.Id_Cd_Ver },
                        Sesion.Emp_Cnx,
                        ref CtaGastos
                    );

                    CtaGastosPartidaItem.Items.Clear();
                    if (CtaGastos.Count > 0)
                    {


                        var datasource = from x in CtaGastos
                                         select new
                                         {
                                             x.Id_Emp,
                                             x.Id_Cd,
                                             x.Id_PagElecCuenta,
                                             x.PagElecCuenta_CC,
                                             x.PagElecCuenta_CuentaPago,
                                             x.PagElecCuenta_Descripcion,
                                             x.PagElecCuenta_Numero,
                                             x.PagElecCuenta_SubCuenta,
                                             x.PagElecCuenta_SubSubCuenta,
                                             DisplayField = String.Format("{0} ({1})", x.PagElecCuenta_Descripcion, x.PagElecCuenta_SubCuenta)
                                         };



                        CtaGastosPartidaItem.DataSource = datasource;
                        CtaGastosPartidaItem.DataValueField = "Id_PagElecCuenta";
                        CtaGastosPartidaItem.DataTextField = "DisplayField";
                        CtaGastosPartidaItem.DataBind();
                    }

                    CtaGastosPartidaItem.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                    int pagElec_Id_PagElecCuenta = Convert.ToInt32((editItem["Id_PagElec_Id_PagElecCuenta"].FindControl("lblId_PagElec_Id_PagElecCuenta") as Label).Text);
 
                  

                    PagoElectronicoCuenta CtaGastos2 = new PagoElectronicoCuenta()
                    {
                        Id_Emp = sesion.Id_Emp,
                        Id_Cd = sesion.Id_Cd_Ver,
                        Id_PagElecCuenta = Convert.ToInt32(pagElec_Id_PagElecCuenta)
                    };


                    (new CN_CatPagoElectronicoCuenta()).ConsultaCuenta(
                        CtaGastos2,
                        sesion.Emp_Cnx
                    );

                    
                    CtaGastosPartidaItem.SelectedValue = pagElec_Id_PagElecCuenta.ToString();
                    CtaGastosPartidaItem.Text = CtaGastos2.PagElecCuenta_Descripcion;

                }


 
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rgPagoElectronico_ItemDataBound");
                //DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaDet_ItemDataBound"));
            }
        }

        protected void txtcmbCtaGastos_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];


                //if (e.Value != null && e.Value != "")
                //{
                //    PagoElectronicoCuenta CtaGastos = new PagoElectronicoCuenta()
                //    {
                //        Id_Emp = Sesion.Id_Emp,
                //        Id_Cd = Sesion.Id_Cd_Ver,
                //        Id_PagElecCuenta = Convert.ToInt32(e.Value)
                //    };

                //    (new CN_CatPagoElectronicoCuenta()).ConsultaCuenta(
                //        CtaGastos,
                //        Sesion.Emp_Cnx
                //    );

                //    TxtCuenta.Text = CtaGastos.PagElecCuenta_Numero;
                //    TxtCc.Text = CtaGastos.PagElecCuenta_CC;
                //    TxtNumero.Text = CtaGastos.PagElecCuenta_Numero;
                //    TxtCuentaPago.Text = CtaGastos.PagElecCuenta_CuentaPago;
                //    TxtSubCuenta.Text = CtaGastos.PagElecCuenta_SubCuenta;
                //    TxtSubSubCuenta.Text = CtaGastos.PagElecCuenta_SubSubCuenta;
                //}
                //else
                //{
                //    TxtCuenta.Text = "";
                //    TxtCc.Text = "";
                //    TxtNumero.Text = "";
                //    TxtCuentaPago.Text = "";
                //    TxtSubCuenta.Text = "";
                //    TxtSubSubCuenta.Text = "";

                //}
 
                
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "txtcmbCtaGastos_SelectedIndexChanged");
            }
        }
 
        //jfcv 16Nov2016 FIN que permita editar un registro  punto 5


        protected void CmbAcreedor_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                CN_CatAcreedor clsAcreedor = new CN_CatAcreedor();

                Acreedor acreedor = new Acreedor();
                acreedor.Id_Emp = session.Id_Emp;
                acreedor.Id_Cd = session.Id_Cd_Ver;

                List<string> lstAcreedor = new List<string>();
                using (RadComboBox CB = (RadComboBox)sender)
                {
                    foreach (Telerik.Web.UI.RadComboBoxItem rcbItem in CB.Items)
                    {
                        if (((CheckBox)rcbItem.FindControl("CheckBox1")).Checked)
                        {
                            acreedor.Id_Acr = Int32.Parse(rcbItem.Value);
                            clsAcreedor.ConsultaAcreedor(acreedor, session.Emp_Cnx);

                            lstAcreedor.Add(acreedor.Acr_RFC);
                        }
                    }
                }

                if (!(CmbTipo.SelectedValue == "2" || CmbTipo.SelectedValue == "3"))
                {
                    if (lstAcreedor.Count > 1)
                    {
                        Alerta("Para este tipo. No se permiten multiples Acredores");
                        return;
                    }
                }

                DateTime fechaRequiere = DateTime.Now;
                TxtFechaRequiere.SelectedDate = fechaRequiere.AddDays(acreedor.Acr_CondPago);

                wsBuzonWeb.wsBuzonWeb wsBuzon = new wsBuzonWeb.wsBuzonWeb();
                wsBuzonWeb.InvoiceList[] ListaFacturaTmp = wsBuzon.GetListFactura(
                    BuzonKey,
                    lstAcreedor.ToArray(),
                    session.Id_Cd_Ver,
                    Emp_RFC
                );

                List<InvoiceList> ListaFactura = new List<InvoiceList>();
                foreach (wsBuzonWeb.InvoiceList Item in ListaFacturaTmp)
                {
                    ListaFactura.Add(new InvoiceList(Item));
                }

                cmbFactura.Items.Clear();
                cmbFactura.DataSource = ListaFactura;
                cmbFactura.DataValueField = "Value";
                cmbFactura.DataTextField = "Text";
                cmbFactura.DataBind();

                ViewState["dtFactura"] = ListaFactura;
                ViewState["RFCAcredor"] = lstAcreedor;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbAcreedor_SelectedIndexChanged");
            }
        }


        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                CN_CatAcreedor clsAcreedor = new CN_CatAcreedor();

                Acreedor acreedor = new Acreedor();
                acreedor.Id_Emp = session.Id_Emp;
                acreedor.Id_Cd = session.Id_Cd_Ver;

                List<string> lstAcreedor = new List<string>();
                foreach (Telerik.Web.UI.RadComboBoxItem rcbItem in CmbAcreedor.Items)
                {
                    if (((CheckBox)rcbItem.FindControl("CheckBox1")).Checked)
                    {
                        acreedor.Id_Acr = Int32.Parse(rcbItem.Value);
                        clsAcreedor.ConsultaAcreedor(acreedor, session.Emp_Cnx);

                        lstAcreedor.Add(acreedor.Acr_RFC);
                    }
                }

                if (!(CmbTipo.SelectedValue == "2" || CmbTipo.SelectedValue == "3"))
                {
                    if (lstAcreedor.Count > 1)
                    {
                        Alerta("Para este tipo. No se permiten multiples Acredores");
                        (sender as CheckBox).Checked = false;
                        return;
                    }
                }



                DateTime fechaRequiere = DateTime.Now;
                TxtFechaRequiere.SelectedDate = fechaRequiere.AddDays(acreedor.Acr_CondPago);

                wsBuzonWeb.wsBuzonWeb wsBuzon = new wsBuzonWeb.wsBuzonWeb();
                wsBuzonWeb.InvoiceList[] ListaFacturaTmp = wsBuzon.GetListFactura(
                    BuzonKey,
                    lstAcreedor.ToArray(),
                    session.Id_Cd_Ver,
                    Emp_RFC
                );

                List<InvoiceList> ListaFactura = new List<InvoiceList>();
                foreach (wsBuzonWeb.InvoiceList Item in ListaFacturaTmp)
                {
                    ListaFactura.Add(new InvoiceList(Item));
                }

                cmbFactura.Items.Clear();
                cmbFactura.DataSource = ListaFactura;
                cmbFactura.DataValueField = "Value";
                cmbFactura.DataTextField = "Text";
                cmbFactura.DataBind();

                ViewState["dtFactura"] = ListaFactura;
                ViewState["RFCAcredor"] = lstAcreedor;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CheckBox1_CheckedChanged");
            }



        }

        protected void cmbFactura_ItemDataBound(object sender, Telerik.Web.UI.RadListBoxItemEventArgs e)
        {
            var data = e.Item.DataItem as InvoiceList;
            if (data == null)
            {
                return;
            }


            if (data.IsGroupHeader)
            {
                e.Item.Text = String.Format(
                    "{0}",
                    data.nombre
                );
            }
            else
            {
                e.Item.Text = String.Format(
                    "{0}{1} Fecha:{2} Importe:{3}",
                    data.Serie,
                    data.Folio_Documento,
                    Convert.ToDateTime(data.Fecha_Documento).ToString("MM/dd/yyyy"),
                    Convert.ToDecimal(data.Importe_Total_Documento).ToString("C")
                );
            }

            e.Item.Value = String.Format(
               "{0}|{1}|{2}|{3}|{4}|{5}|{6}",
               data.ID,
               data.rfc,
               data.SucursalCorta,
               data.Tipo_Documento,
               data.Serie,
               data.Folio_Documento,
               data.Importe_Total_Documento
           );

            e.Item.Enabled = !data.IsGroupHeader;

            var divItem = e.Item.FindControl("divItem") as System.Web.UI.HtmlControls.HtmlGenericControl;
            var divHeader = e.Item.FindControl("divHeader") as System.Web.UI.HtmlControls.HtmlGenericControl;

            if (divHeader != null)
            {
                divHeader.Visible = data.IsGroupHeader;
            }
            if (divItem != null)
            {
                divItem.Visible = !data.IsGroupHeader;
            }
        }

        protected void CmbFactura_SelectedIndexChanged(object sender, Telerik.Web.UI.RadListBoxItemEventArgs e)
        {
            using (RadComboBox CB = (RadComboBox)sender)
            {
                decimal SumaImporte = 0;
                List<string[]> Result = new List<string[]>();
                foreach (string item in GetCheckBoxValues(CB))
                {
                    string[] selected = (item).Split('|');
                    string[] param = { selected[3], selected[4], selected[5] };
                    SumaImporte += Convert.ToDecimal(selected[5]);
                    Result.Add(param);
                }
                TxtImporte.Text = SumaImporte.ToString();
                ViewState["seletedFactura"] = Result;
            }
        }

        private List<string> GetCheckBoxValues(Telerik.Web.UI.RadComboBox comboCheckbox)
        {
            List<string> Result = new List<string>();
            int I = 0;
            foreach (Telerik.Web.UI.RadComboBoxItem rcbItem in comboCheckbox.Items)
            {
                if (Request["ctl00$CPH$" + comboCheckbox.ID + "$i" + (I).ToString() + "$CheckBox1"] != null && Request["ctl00$CPH$" + comboCheckbox.ID + "$i" + (I).ToString() + "$CheckBox1"].ToString().ToUpper() == "ON")
                {
                    Result.Add(rcbItem.Value);
                }
                I++;
            }
            return Result;
        }

        private List<string> GetCheckBoxText(Telerik.Web.UI.RadComboBox comboCheckbox)
        {
            List<string> Result = new List<string>();
            foreach (Telerik.Web.UI.RadComboBoxItem rcbItem in comboCheckbox.Items)
            {
                if (((CheckBox)rcbItem.FindControl("CheckBox1")).Checked)
                {
                    Result.Add(rcbItem.Text);
                }
            }
            return Result;
        }
        protected void ChkConComprobante_CheckedChanged(object sender, EventArgs e)
        {
            cmbFactura.Visible = ChkConComprobante.Checked;
            RadListBoxDestination.Visible = ChkConComprobante.Checked;
            PnlSoporte.Visible = !ChkConComprobante.Checked;

            RadAsyncUpload1.Visible = true;


            Label3.Text = "";
            Label7.Text = "";
            Label9.Text = "";
            habilitarcontroles();
        }


        protected void btnAcredor_OnClick(object sender, EventArgs arg)
        {
            CmbAcreedor_SelectedIndexChanged(CmbAcreedor, null);
        }

        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    this.lblMensaje.Text = string.Empty;
                    RadToolBarButton btn = e.Item as RadToolBarButton;
                    if (btn.CommandName == "save")
                    {
                        Guardar();
                    }
                    else if (btn.CommandName == "new")
                    {
                        Nuevo();
                    }
                    else if (btn.CommandName == "undo")
                    {
                        //Regresar()
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick_2");
               
            }
        }

        protected void TxtFechaSalida_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (TxtFechaRegreso.SelectedDate != null)
            {
                TimeSpan ts = TxtFechaRegreso.SelectedDate.Value - TxtFechaSalida.SelectedDate.Value;
                TxtCantidadDias.Text = ts.TotalDays.ToString();

                //jfcv 09 feb 2016
                TxtHospedajeDias.Text = string.Format("{0}", (Int32.Parse(TxtCantidadDias.Text) - 1));
                TxtDesayunosDias.Text = Int32.Parse(TxtCantidadDias.Text).ToString();
                TxtCenasDias.Text = string.Format("{0}", (Int32.Parse(TxtCantidadDias.Text) - 1));
                TxtComidasDias.Text = Int32.Parse(TxtCantidadDias.Text).ToString();
                CalcularCuota(Int32.Parse(TxtCantidadDias.Text));
            }
            else
            {
                TxtCantidadDias.Text = "0";
                CalcularCuota(0);
            }
        }

        protected void TxtFechaRegreso_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (TxtFechaSalida.SelectedDate != null)
            {
                TimeSpan ts = TxtFechaRegreso.SelectedDate.Value - TxtFechaSalida.SelectedDate.Value;
                TxtCantidadDias.Text = ts.TotalDays.ToString();

                //jfcv 09 feb 2016
                TxtHospedajeDias.Text = string.Format("{0}", (Int32.Parse(TxtCantidadDias.Text) - 1));
                TxtDesayunosDias.Text = Int32.Parse(TxtCantidadDias.Text).ToString();
                TxtCenasDias.Text = string.Format("{0}", (Int32.Parse(TxtCantidadDias.Text) - 1));
                TxtComidasDias.Text = Int32.Parse(TxtCantidadDias.Text).ToString();

                CalcularCuota(Int32.Parse(TxtCantidadDias.Text));
            }
            else
            {
                TxtCantidadDias.Text = "0";
                CalcularCuota(0);
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            //rgPagoElectronico.Rebind();
        }

        private void Inicializar()
        {
            cmbFactura.TransferMode = ListBoxTransferMode.Copy;
            cmbFactura.AllowTransferOnDoubleClick = true;
            cmbFactura.ButtonSettings.ShowTransferAll = false;
            cmbFactura.EnableDragAndDrop = true;
            DateTime fechaElaboracion = DateTime.Now;
            txtFechaElaboracion.SelectedDate = fechaElaboracion;
            Session["Table"] = null;
            Session["TableDel"] = null;


            //Modificacion , obtener los datos del pago electrónico a modificar
            //spCapPagoElectronico_Consulta
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            PagoElectronico gastoViaje = new PagoElectronico();

            gastoViaje.Id_Emp = Sesion.Id_Emp;
            gastoViaje.Id_Cd = Sesion.Id_Cd_Ver;
            gastoViaje.Id_PagElec = Int32.Parse(Request.QueryString["Id"] == null ? "-1" : Request.QueryString["Id"]);
            TxtPagoElectronico.Text = gastoViaje.Id_PagElec.ToString();


            CN_CapPagoElectronico clsCapPagoElectronico = new CN_CapPagoElectronico();
            clsCapPagoElectronico.ConsultaPagoElectronico(gastoViaje, Sesion.Emp_Cnx);


            this.CmbTipo.Text = (this.CmbTipo.FindItemByValue(gastoViaje.Id_PagElecTipo.ToString())).Text;
            this.CmbTipo.SelectedValue = gastoViaje.Id_PagElecTipo.ToString();


            this.CmbSubTipoGasto.Text = (this.CmbSubTipoGasto.FindItemByValue(gastoViaje.Id_PagElecSubTipo.ToString())).Text;
            this.CmbSubTipoGasto.SelectedValue = gastoViaje.Id_PagElecSubTipo.ToString();

            this.cmbCtaGastos.SelectedValue = gastoViaje.Id_PagElecCuenta.ToString();
            this.cmbCtaGastos.Text = gastoViaje.pagElecCuenta_Descripcion.ToString();

            this.cmbProveedor.SelectedValue = gastoViaje.Id_AcrCheque.ToString();


            this.cmbProveedor.Text = gastoViaje.AcrCheque_Nombre.ToString();

            Acreedor provee = new Acreedor() { Id_Emp = Sesion.Id_Emp, Id_Cd = Sesion.Id_Cd_Ver, Id_Acr = gastoViaje.Id_AcrCheque };
            new CN_CatAcreedor().ConsultaAcreedor(provee, Sesion.Emp_Cnx);

            txtProveedor.Text = provee.Acr_NumeroGenerado == null ? "Sin Autorizacion" : provee.Acr_NumeroGenerado.ToString().Trim().ToUpper();

            TxtSolicitante.Text = gastoViaje.PagElec_Solicitante;
            txtTotalAPagar.Text = gastoViaje.PagElec_Importe.ToString();

            TxtCuenta.Text = gastoViaje.PagElec_Cuenta;
            TxtCc.Text = gastoViaje.PagElec_Cc;
            TxtNumero.Text = gastoViaje.PagElec_Numero;
            TxtSubCuenta.Text = gastoViaje.PagElec_SubCuenta;
            TxtSubSubCuenta.Text = gastoViaje.PagElec_SubSubCuenta;
            TxtCuentaPago.Text = gastoViaje.PagElec_CuentaPago;
            TxtFechaRequiere.SelectedDate = Convert.ToDateTime(gastoViaje.PagElec_FechaRequiere);
            TxtObservaciones.Text = gastoViaje.PagElec_Observaciones;



            #region gastoviaje

            if (gastoViaje.Id_PagElecTipo == 3)
            {

                //if (PnlGastosViaje.Visible)
                //{

                TxtSolicitanteViajero.Text = gastoViaje.PagElec_Solicitante;

                TxtFechaSalida.SelectedDate = Convert.ToDateTime(gastoViaje.PagElec_FechaRequiere);

                TxtTotal.Text = gastoViaje.PagElec_Importe.ToString();
                TxtMotivo.Text = gastoViaje.PagElec_Observaciones;

                //mandar ejecutar la consulta de gasto de viaje 
                GastoViaje gastodeviaje = new GastoViaje();
                CN_CapGastoViaje clsCapGastoViaje = new CN_CapGastoViaje();
                gastodeviaje.Id_Emp = gastoViaje.Id_Emp;
                gastodeviaje.Id_Cd = gastoViaje.Id_Cd;
                gastodeviaje.Id_GV = Convert.ToInt32(gastoViaje.Id_GV);


                clsCapGastoViaje.ConsultaGastoViaje(gastodeviaje, Sesion.Emp_Cnx);

                TxtFechaSalida.SelectedDate = Convert.ToDateTime(gastodeviaje.GV_FechaSalida);
                txtFechaElaboracion.SelectedDate = Convert.ToDateTime(gastodeviaje.GV_FechaElaboracion == null ? DateTime.Now : gastodeviaje.GV_FechaElaboracion); ;
                TxtHospedajeDias.Text = gastodeviaje.GV_DiasHospedaje.ToString();
                TxtDesayunosDias.Text = gastodeviaje.GV_CantidadDesayunos.ToString();
                TxtComidasDias.Text = gastodeviaje.GV_CantidadComidas.ToString();
                TxtCenasDias.Text = gastodeviaje.GV_CantidadCenas.ToString();
                TxtOtrosGastosDias.Text = gastodeviaje.GV_CantidadOtros.ToString();
                TxtOtrosGastosCutoa.Text = gastodeviaje.GV_ImporteOtros.ToString();
                TxtOtrosGastosImporte.Text = (Convert.ToDecimal(TxtOtrosGastosCutoa.Text) * Convert.ToDecimal(TxtOtrosGastosDias.Text)).ToString();

                TxtFechaRegreso.SelectedDate = Convert.ToDateTime(gastodeviaje.GV_FechaRegreso);
                TxtCantidadDias.Text = gastoViaje.PagElec_Dias.ToString();

                //JFCV 12 ene 2016
                TxtDestino.Text = gastodeviaje.GV_PagElec_Destino;


                TimeSpan ts = TxtFechaRegreso.SelectedDate.Value - TxtFechaSalida.SelectedDate.Value;
                TxtCantidadDias.Text = ts.TotalDays.ToString();

                TxtTransporteCuota.Text = gastodeviaje.GV_TransporteCuota.ToString();


                CalcularCuota(Convert.ToInt32(TxtCantidadDias.Text));

                this.CmbTipoComprobante.Text = (this.CmbTipoComprobante.FindItemByValue(gastodeviaje.GV_TipoTransporte.ToString())).Text;
                this.CmbTipoComprobante.SelectedValue = gastodeviaje.GV_TipoTransporte.ToString();
                //si es gasto de viaje con estatus 6 el checkComprobante le pongo check == true   
                // porque cuando tiene comprobantes ya no tiene que comprobarse mas delante
                if (gastodeviaje.Id_GVEst == 6)
                {
                    ChkConComprobante.Checked = true;
                }
                else
                {
                    ChkConComprobante.Checked = false;
                }

            }
            else
            {
                TxtSolicitanteViajero.Text = gastoViaje.PagElec_Solicitante;
                //TxtTotal.Text = gastoViaje.PagElec_Importe.ToString();


            }

            #endregion Gastoviaje


            #region Cargar Archivos ( comprobantes )
            //llena el grid con los archivos 
            cargararchivos(gastoViaje.PagElecArchivo);

            dtValues = (DataTable)Session["Table"];
            if (dtValues.Rows.Count > 0 && CmbTipo.SelectedValue == "1")
            {
                ChkConComprobante.Checked = true;
            }

            #endregion Cargar Archivos ( comprobantes )

            #region Cargar Archivo de soporte

            //decimal totaleapagarinicializar = 0;

            ////JFCV 23 oct Si tengo un archivo de soporte lo agrego  al grid

            if (gastoViaje.PagElec_Soporte != null)
            {

                DataRow drValues = dtValues.NewRow();
                drValues["Id_GVComprobante"] = dtValues.Rows.Count + 1;
                drValues["GVComprobante_ConComprobanteDescripcion"] = "Sin Comprobante";
                if (gastoViaje.Id_PagElecCuenta != 0)
                    drValues["PagElec_Id_PagElecCuenta"] = gastoViaje.Id_PagElecCuenta;

                drValues["PagElec_Cuenta"] = Convert.ToString(gastoViaje.PagElec_Cuenta);
                drValues["PagElec_Cc"] = Convert.ToString(gastoViaje.PagElec_Cc);
                drValues["PagElec_Numero"] = Convert.ToString(gastoViaje.PagElec_Numero);
                drValues["PagElec_SubCuenta"] = Convert.ToString(gastoViaje.PagElec_SubCuenta);
                drValues["PagElec_SubSubCuenta"] = Convert.ToString(gastoViaje.PagElec_SubSubCuenta);
                drValues["PagElec_CuentaPago"] = Convert.ToString(gastoViaje.PagElec_CuentaPago);

                drValues["PagElec_Serie"] = "";
                drValues["PagElec_Folio"] = "";

                drValues["PagElec_CuentaPago"] = Convert.ToString(gastoViaje.PagElec_CuentaPago);

                Label7.Text = gastoViaje.PagElec_Soporte_Nombre;
                Label9.Text = gastoViaje.PagElec_Soporte_Nombre;
                Label3.Text = gastoViaje.PagElec_Soporte_Tipo;

                drValues["GVComprobante_Pdf"] = gastoViaje.PagElec_Soporte;



                drValues["GVComprobante_Observaciones"] = Convert.ToString(gastoViaje.PagElec_Observaciones);
                drValues["GVComprobante_Fecha"] = Convert.ToString(gastoViaje.PagElec_FechaRegistro);


                //if (txtTotalAPagar.Text != "")
                //    totaleapagarinicializar = Convert.ToDecimal(txtTotalAPagar.Text);

                //totalsoporte = totaleapagarinicializar - totalpartidas;
                //jfcv poner campo de soporte en encabezado pago electronico
                //drValues["GVComprobante_Importe"] = totalsoporte;
                drValues["GVComprobante_Importe"] = gastoViaje.PagElec_SoporteImporte;


                dtValues.Rows.Add(drValues);//adding new row into datatable
                dtValues.AcceptChanges();


                //TxtImporte.Text = Convert.ToString(totalsoporte);
                TxtImporte.Text = gastoViaje.PagElec_SoporteImporte == null ? "" : gastoViaje.PagElec_SoporteImporte.ToString().Trim().ToUpper();

                Session["Table"] = dtValues;

                //if (CmbTipo.SelectedValue == "1")
                //{
                //    Label7.Text = gastoViaje.PagElec_Soporte_Nombre;
                //    Label9.Text = gastoViaje.PagElec_Soporte_Nombre;
                //    Label3.Text = gastoViaje.PagElec_Soporte_Tipo;
                //    PagElec_Soporte4 = gastoViaje.PagElec_Soporte;
                //    btnQuitar.Visible = true;
                //    Label9.Visible = true;
                //    RadAsyncUpload1.Visible = false;

                //}
            }
            else
            {
                Label7.Text = "";
                Label3.Text = "";
                Label9.Text = "";
                RadAsyncUpload1.Visible = true;
                btnQuitar.Visible = false;
                Label9.Visible = false;

            }
            //}
            #endregion Cargar Archivo de soporte

            //jfcv 22 jun 2017
            ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
            configuracion.Id_Cd = Sesion.Id_Cd_Ver;
            configuracion.Id_Emp = Sesion.Id_Emp;
            CN_Configuracion cn_configuracion = new CN_Configuracion();
            cn_configuracion.Consulta(ref configuracion, Sesion.Emp_Cnx);


            //JFCV para saber si originalmente la cuenta era de acreedores o no 
            //string ccPagoAcreedor = ConfigurationManager.AppSettings["CCPagoAcreedor"].ToString();
            //string subCuentaPagoAcreedor = ConfigurationManager.AppSettings["SubCuentaPagoAcreedor"].ToString();
            //string subSubCuentaPagoAcreedor = ConfigurationManager.AppSettings["SubSubCuentaPagoAcreedor"].ToString();
            ////if (CmbTipo.SelectedValue == "1" && pagoElectronico.PagElec_Cc == "1031" && pagoElectronico.PagElec_SubCuenta == "20001" && pagoElectronico.PagElec_SubSubCuenta == "00")
            //if (CmbTipo.SelectedValue == "1" && TxtCc.Text == ccPagoAcreedor && TxtSubCuenta.Text == subCuentaPagoAcreedor && TxtSubSubCuenta.Text == subSubCuentaPagoAcreedor)
            //{
            //    Session["eraAnticipoAcreedores"] = 1;
            //}
            //else
            //{
            //    Session["eraAnticipoAcreedores"] = 0;
            //}

          
            Session["eraAnticipoAcreedores"] = 0;
            if (CmbTipo.SelectedValue == "1" && configuracion.Cuenta_GastosComprobacion == TxtCc.Text + TxtSubCuenta.Text + TxtSubSubCuenta.Text)
            {
                Session["eraAnticipoAcreedores"] = 1;
            }
            if (CmbTipo.SelectedValue == "1" && configuracion.Cuenta_GastosComprobacionCompras == TxtCc.Text + TxtSubCuenta.Text + TxtSubSubCuenta.Text)
            {
                Session["eraAnticipoAcreedores"] = 2;
            }


            Session["Table"] = dtValues;

            Session["TableDel"] = dtValues;




            #region guardagastoviaje

            //JFCV si es de gasto de viaje , tengo que cargar la información de las tablas de gasto de viaje 
            ////   if (Boolean.Parse(HF_AnticipoPorComprobar.Value))
            ////   {
            //// /////////////      GastoViaje gastoViaje = new GastoViaje();

            ////       if (PnlGastosViaje.Visible)
            ////       {
            ////           gastoViaje.Id_Emp = Sesion.Id_Emp;
            ////           gastoViaje.Id_Cd = Sesion.Id_Cd_Ver;
            ////    /////////////       gastoViaje.Id_GV = Int32.Parse(MaximoIdGV());
            ////         /////////////  gastoViaje.Id_GVEst = 1;
            ////     /////////////      gastoViaje.GV_Solicitante = TxtSolicitanteViajero.Text.Trim();
            ////     /////////////      gastoViaje.GV_Motivo = TxtMotivo.Text.Trim();
            ////   /////////////        gastoViaje.GV_FechaSalida = TxtFechaSalida.SelectedDate;
            ////    /////////////       gastoViaje.GV_FechaRegreso = TxtFechaRegreso.SelectedDate;
            ////   /////////////        gastoViaje.GV_Importe = decimal.Parse(TxtTotal.Text);
            ////   /////////////        gastoViaje.Id_PagElec = pagoElectronico.Id_PagElec;
            ////       }
            ////       else
            ////       {
            ////           gastoViaje.Id_Emp = Sesion.Id_Emp;
            ////           gastoViaje.Id_Cd = Sesion.Id_Cd_Ver;
            ////     /////////////      gastoViaje.Id_GV = Int32.Parse(MaximoIdGV());
            ////   /////////////        gastoViaje.Id_GVEst = 2;
            ////  /////////////         gastoViaje.GV_Solicitante = TxtSolicitante.Text.Trim();
            ////  /////////////         gastoViaje.GV_Motivo = TxtObservaciones.Text.Trim();
            /////////////////           gastoViaje.GV_FechaSalida = TxtFechaRequiere.SelectedDate;
            //// /////////////          gastoViaje.GV_FechaRegreso = TxtFechaRequiere.SelectedDate;
            ////           //SAUL GUERRA 20150623  BEGIN
            ////   /////////////        gastoViaje.GV_Importe = sumImporte; // decimal.Parse(param[2].ToString().Trim());
            /////////////////           gastoViaje.GV_Importe = decimal.Parse(TxtImporte.Text);
            ////           //SAUL GUERRA 20150623  END
            ////           gastoViaje.Id_PagElec = pagoElectronico.Id_PagElec;
            ////       }


            //// ////       verificador = -1;
            //// ////       ////////////CN_CapGastoViaje clsGastoViaje = new CN_CapGastoViaje();
            //// //// /////////////      clsGastoViaje.InsertarGastoViaje(gastoViaje, Sesion.Emp_Cnx, ref verificador);
            //// ////   }
            #endregion guardagastoviaje



            CmbAcreedor.Enabled = true;
            if (CmbTipo.SelectedValue == "1")
            {
                lblSubTipoGasto.Visible = true;
                CmbSubTipoGasto.Visible = true;

            }
            else
            {
                lblSubTipoGasto.Visible = false;
                CmbSubTipoGasto.Visible = false;

                if (CmbTipo.SelectedValue == "2")
                {
                    ChkConComprobante.Checked = true;
                }

            }

            HF_AnticipoPorComprobar.Value = "False";
            ChkConComprobante.Visible = false;
            LblConComprobante.Visible = false;

            if (CmbTipo.SelectedValue == "3")
            {
                ChkConComprobante.Visible = true;
                LblConComprobante.Visible = true;

                PnlGastosViaje.Visible = true;
                PnlSolicitudCheque.Visible = false;
                HF_AnticipoPorComprobar.Value = "True";
                ChkConComprobante.Visible = false;
                LblConComprobante.Visible = false;
            }
            else
            {
                PnlGastosViaje.Visible = false;
                PnlSolicitudCheque.Visible = true;
            }

            cmbFactura.Visible = ChkConComprobante.Checked;
            RadListBoxDestination.Visible = ChkConComprobante.Checked;
            txtTotalAPagar.Visible = true;
            lblTotalPagar.Visible = true;

            if (CmbTipo.SelectedValue == "2")
            {
                PnlSoporte.Visible = true;
                BtnAgregar.Visible = true;


            }
            else
            {
                ///JFCV 13 enero 2016 se quito que el botón de oculte porque en modificación si quieren que este activo BtnAgregar.Visible = false;
                BtnAgregar.Visible = true;
                PnlSoporte.Visible = !ChkConComprobante.Checked;
                txtTotalAPagar.Visible = false;
                lblTotalPagar.Visible = false;
            }

            //JFCV el grid siempre será visible 
            rgPagoElectronico.Visible = true;
            //JFCV no se podrá editar el combo de tipo comprobante 
            CmbTipo.Enabled = false;

            habilitarcontroles();

        }
        // fin de como es guaradr



        //JFCV agregar movimientos desde la BD 
        private void cargararchivos(List<PagoElectronicoArchivo> PagElecArchivo)
        {

            int partidarow = 0;
            bool facturaexiste = false;
            DataTable dtValues;
            inicializartabla();
            dtValues = (DataTable)Session["Table"];

            foreach (PagoElectronicoArchivo pagoElectronico in PagElecArchivo)
            {
                facturaexiste = false;

                if (dtValues.Rows.Count > 0)
                {

                    foreach (DataRow row in dtValues.Rows)
                    {
                        if (Convert.ToString(row["PagElec_Serie"].ToString()) == pagoElectronico.Serie)
                        {
                            if (Convert.ToString(row["PagElec_Folio"].ToString()) == pagoElectronico.Folio)
                            {
                                if (Convert.ToDecimal(row["GVComprobante_Importe"].ToString()) == Convert.ToDecimal(pagoElectronico.Importe))
                                {
                                    facturaexiste = true;
                                }
                            }
                        }
                    }
                }


                if (facturaexiste == false)
                {
                    if (pagoElectronico.PagElec_ConSoporte == "Con Comprobante")
                    {
                        DataRow drValues = dtValues.NewRow();
                        drValues["PagElec_Id_PagElecCuenta"] = Convert.ToInt32(pagoElectronico.Id_PagElecCuenta);
                        drValues["PagElec_Cuenta"] = Convert.ToString(pagoElectronico.PagElec_Cuenta);
                        drValues["PagElec_Cc"] = Convert.ToString(pagoElectronico.PagElec_Cc);
                        drValues["PagElec_Numero"] = Convert.ToString(pagoElectronico.PagElec_Numero);
                        drValues["PagElec_SubCuenta"] = Convert.ToString(pagoElectronico.PagElec_SubCuenta);
                        drValues["PagElec_SubSubCuenta"] = Convert.ToString(pagoElectronico.PagElec_SubSubCuenta);
                        drValues["PagElec_CuentaPago"] = Convert.ToString(pagoElectronico.PagElec_CuentaPago);
                        drValues["GVComprobante_Importe"] = Convert.ToDecimal(pagoElectronico.Importe);

                        drValues["GVComprobante_Observaciones"] = Convert.ToString(pagoElectronico.PagElec_Observaciones);
                        drValues["GVComprobante_ConComprobanteDescripcion"] = "Con Comprobante";
                        drValues["GVComprobante_Fecha"] = Convert.ToString(pagoElectronico.FechaFactura);


                        //byte[] xmlpdf2 = (drValues["GVComprobante_Pdf"] == System.DBNull.Value ? null : (byte[])(drValues["GVComprobante_Pdf"]));
                        //byte[] xmlFile2 = (drValues["GVComprobante_Xml"] == System.DBNull.Value ? null : (byte[])(drValues["GVComprobante_Xml"]));
                        drValues["GVComprobante_Pdf"] = pagoElectronico.PagElec_PDFStream;
                        drValues["GVComprobante_Xml"] = pagoElectronico.PagElec_XMLStream;


                        partidarow += 1;
                        drValues["Id_GVComprobante"] = partidarow;

                        drValues["PagElec_Serie"] = Convert.ToString(pagoElectronico.Serie);
                        drValues["PagElec_Folio"] = Convert.ToString(pagoElectronico.Folio);
                        drValues["PagElec_Rfc"] = Convert.ToString(pagoElectronico.PagElec_RFC);

                        //JFCV 2 feb 2016 agregar campos 
                        drValues["PagElec_UUID"] = Convert.ToString(pagoElectronico.PagElec_UUID);
                        drValues["PagElec_Subtotal"] = Convert.ToDecimal(pagoElectronico.PagElec_Subtotal);
                        drValues["PagElec_Iva"] = Convert.ToDecimal(pagoElectronico.PagElec_Iva);
                        drValues["PagElec_ImpRetenido"] = Convert.ToDecimal(pagoElectronico.PagElec_ImpRetenido);
                        drValues["PagElec_IvaRetenido"] = Convert.ToDecimal(pagoElectronico.PagElec_IvaRetenido);

                        dtValues.Rows.Add(drValues);//adding new row into datatable
                        dtValues.AcceptChanges();

                        totalpartidas += Convert.ToDecimal(pagoElectronico.Importe);
                    }

                }

            }   // fin foreach lista

        }

        private void cargararchivosDel(List<PagoElectronicoArchivo> PagElecArchivoDel)
        {

            int partidarow = 0;
            bool facturaexiste = false;
            DataTable dtValuesDel;
            dtValuesDel = (DataTable)Session["TableDel"];

            foreach (PagoElectronicoArchivo pagoElectronico in PagElecArchivoDel)
            {
                facturaexiste = false;

                if (dtValuesDel.Rows.Count > 0)
                {

                    foreach (DataRow row in dtValuesDel.Rows)
                    {
                        if (Convert.ToString(row["PagElec_Serie"].ToString()) == pagoElectronico.Serie)
                        {
                            if (Convert.ToString(row["PagElec_Folio"].ToString()) == pagoElectronico.Folio)
                            {
                                if (Convert.ToDecimal(row["GVComprobante_Importe"].ToString()) == Convert.ToDecimal(pagoElectronico.Importe))
                                {
                                    facturaexiste = true;
                                }
                            }
                        }
                    }
                }


                if (facturaexiste == false)
                {
                    DataRow drValues = dtValuesDel.NewRow();
                    drValues["PagElec_Id_PagElecCuenta"] = Convert.ToString(pagoElectronico.PagElec_Cuenta);
                    drValues["PagElec_Rfc"] = Convert.ToString(pagoElectronico.PagElec_RFC);
                    drValues["PagElec_Cuenta"] = Convert.ToString(pagoElectronico.PagElec_Cuenta);
                    drValues["PagElec_Cc"] = Convert.ToString(pagoElectronico.PagElec_Cc);
                    drValues["PagElec_Numero"] = Convert.ToString(pagoElectronico.PagElec_Numero);
                    drValues["PagElec_SubCuenta"] = Convert.ToString(pagoElectronico.PagElec_SubCuenta);
                    drValues["PagElec_SubSubCuenta"] = Convert.ToString(pagoElectronico.PagElec_SubSubCuenta);
                    drValues["PagElec_CuentaPago"] = Convert.ToString(pagoElectronico.PagElec_CuentaPago);
                    drValues["GVComprobante_Importe"] = Convert.ToDecimal(pagoElectronico.Importe);

                    drValues["GVComprobante_Observaciones"] = Convert.ToString(pagoElectronico.PagElec_Observaciones);
                    drValues["GVComprobante_ConComprobanteDescripcion"] = "Con Comprobante";
                    drValues["GVComprobante_Fecha"] = Convert.ToString(pagoElectronico.FechaFactura);


                    drValues["GVComprobante_Pdf"] = pagoElectronico.PagElec_PDFStream;
                    drValues["GVComprobante_Xml"] = pagoElectronico.PagElec_XMLStream;


                    partidarow += 1;
                    drValues["Id_GVComprobante"] = partidarow;

                    drValues["PagElec_Serie"] = Convert.ToString(pagoElectronico.Serie);
                    drValues["PagElec_Folio"] = Convert.ToString(pagoElectronico.Folio);
                    drValues["PagElec_Rfc"] = Convert.ToString(pagoElectronico.PagElec_RFC);

                    //jfcv 02 feb 2016 
                    drValues["PagElec_UUID"] = Convert.ToString(pagoElectronico.PagElec_UUID);
                    drValues["PagElec_Subtotal"] = Convert.ToDecimal(pagoElectronico.PagElec_Subtotal);
                    drValues["PagElec_Iva"] = Convert.ToDecimal(pagoElectronico.PagElec_Iva);
                    drValues["PagElec_ImpRetenido"] = Convert.ToDecimal(pagoElectronico.PagElec_ImpRetenido);
                    drValues["PagElec_IvaRetenido"] = Convert.ToDecimal(pagoElectronico.PagElec_IvaRetenido);

                    dtValuesDel.Rows.Add(drValues);//adding new row into datatable
                    dtValuesDel.AcceptChanges();

                    totalpartidas += Convert.ToDecimal(pagoElectronico.Importe);


                }

            }   // fin foreach lista

        }

        protected void CargarProveedores()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            List<Acreedor> lista = new List<Acreedor>();
            using (dbAccess oDB = new dbAccess(Sesion.Emp_Cnx))
            {
                DataSet DS = oDB.spExecDataSet(
                    "spCatAcreedor_LlenaCombo",
                    new System.Data.SqlClient.SqlParameter("@Id_Emp", Sesion.Id_Emp),
                    new System.Data.SqlClient.SqlParameter("@Id_Cd", Sesion.Id_Cd_Ver),
                    new System.Data.SqlClient.SqlParameter("@Acr_Autorizado", 1)
                );

                if (DS != null && DS.Tables.Count == 1 && DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {
                        Acreedor row = new Acreedor();
                        row.Id_Emp = (int)Item["Id_Emp"];
                        row.Id_Cd = (int)Item["Id_Cd"];
                        row.Id_Acr = (int)Item["Id_Acr"];
                        row.Acr_Tipo = (int)Item["Acr_Tipo"];
                        row.Acr_Nombre = Item["Acr_Nombre"].ToString();
                        row.Acr_Calle = Item["Acr_Calle"].ToString();
                        row.Acr_Numero = Item["Acr_Numero"].ToString();
                        row.Acr_NumInterior = Item["Acr_NumInterior"].ToString();
                        row.Acr_CP = Item["Acr_CP"].ToString();
                        row.Acr_Colonia = Item["Acr_Colonia"].ToString();
                        row.Acr_Municipio = Item["Acr_Municipio"].ToString();
                        row.Acr_Estado = Item["Acr_Estado"].ToString();
                        row.Acr_RFC = Item["Acr_RFC"].ToString();
                        row.Acr_Telefono = Item["Acr_Telefono"].ToString();
                        row.Acr_Correo = Item["Acr_Correo"].ToString();
                        row.Acr_Contacto = Item["Acr_Contacto"].ToString();
                        row.Acr_CondPago = (int)Item["Acr_CondPago"];
                        row.Acr_Clave = Item["Acr_Clave"].ToString();
                        row.Acr_Autorizado = Boolean.Parse(Item["Acr_Autorizado"].ToString());
                        row.Acr_NumeroGenerado = Item["Acr_NumeroGenerado"].ToString();
                        lista.Add(row);
                    }
                }
            }

            if (lista.Count > 0)
            {
                cmbProveedor.Items.Clear();
                cmbProveedor.DataSource = lista;
                cmbProveedor.DataValueField = "Id_Acr";
                cmbProveedor.DataTextField = "Acr_Nombre";

                cmbProveedor.DataBind();
            }
        }

        protected void CargarAcreedores()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatAcreedor_Combo", ref CmbAcreedor);
        }

        protected void CargarTipos()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatPagoElectronicoTipo_Combo", ref CmbTipo);
        }

        protected void CargarCtaGastos()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            List<PagoElectronicoCuenta> CtaGastos = new List<PagoElectronicoCuenta>();

            cmbCtaGastos.Items.Clear();
            int subtipo = 0;
            if (CmbTipo.SelectedValue != "1" || CmbSubTipoGasto.SelectedValue != "") //Solicitud de cheque
            {
                subtipo = Convert.ToInt32(CmbSubTipoGasto.SelectedValue);
            }
            else
            {
                subtipo = 0;
            }

            (new CN_CatPagoElectronicoCuenta()).ConsultaCuenta(
                new PagoElectronicoCuenta() { Id_Emp = Sesion.Id_Emp, Id_Cd = Sesion.Id_Cd_Ver, Id_Subtipo = subtipo },
                Sesion.Emp_Cnx,
                ref CtaGastos
            );

            
            if (CtaGastos.Count > 0)
            {
                //JFCV 18 noviembre 2016 que se pueda buscar por la subcuenta 
                var datasource = from x in CtaGastos
                                 select new
                                 {
                                     x.Id_Emp,
                                     x.Id_Cd,
                                     x.Id_PagElecCuenta,
                                     x.PagElecCuenta_CC,
                                     x.PagElecCuenta_CuentaPago,
                                     x.PagElecCuenta_Descripcion,
                                     x.PagElecCuenta_Numero,
                                     x.PagElecCuenta_SubCuenta,
                                     x.PagElecCuenta_SubSubCuenta,
                                     DisplayField = String.Format("{0} ({1})", x.PagElecCuenta_Descripcion, x.PagElecCuenta_SubCuenta)
                                 };


                cmbCtaGastos.DataSource = datasource;
                cmbCtaGastos.DataValueField = "Id_PagElecCuenta";
                cmbCtaGastos.DataTextField = "DisplayField";
                //cmbCtaGastos.DataTextField = "PagElecCuenta_Descripcion" ;
                cmbCtaGastos.DataBind();
            }
        }

        //JFCV obtener el nombre de la cuenta de Gastos  para la edición
        protected void CargarCtaGastos(int id_PagElecCuenta)
        {

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            PagoElectronicoCuenta CtaGastos = new PagoElectronicoCuenta()
            {
                Id_Emp = Sesion.Id_Emp,
                Id_Cd = Sesion.Id_Cd_Ver,
                Id_PagElecCuenta = id_PagElecCuenta
            };

            
            (new CN_CatPagoElectronicoCuenta()).ConsultaCuenta(
                CtaGastos,
                Sesion.Emp_Cnx
            );

            TxtCuenta.Text = CtaGastos.PagElecCuenta_Numero;
            TxtCc.Text = CtaGastos.PagElecCuenta_CC;
            TxtNumero.Text = CtaGastos.PagElecCuenta_Numero;
            TxtCuentaPago.Text = CtaGastos.PagElecCuenta_CuentaPago;
            TxtSubCuenta.Text = CtaGastos.PagElecCuenta_SubCuenta;
            TxtSubSubCuenta.Text = CtaGastos.PagElecCuenta_SubSubCuenta;
            this.cmbCtaGastos.Text = CtaGastos.PagElecCuenta_Descripcion;


        }
 
          
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                TxtSolicitante.Text = Sesion.U_Nombre;

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

                _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
                _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
                _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
                _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);

                //todo eliminar estos permisos 
                _PermisoGuardar = true;
                _PermisoModificar = true;


                //JFCV boton de nuevo no podrá verse 
                //if (_PermisoGuardar == false)
                //{
                this.rtb1.Items[6].Visible = false;
                //}
                if (_PermisoGuardar == false && _PermisoModificar == false)
                {
                    this.rtb1.Items[5].Visible = false;
                }
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
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #region Guardar
        private void Guardar()
        {
            #region GUardar
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if (cmbProveedor.SelectedValue.Trim() == "")
            {
                Alerta("Por favor seleccione un Proveedor.");
                return;
            }

            if (CmbTipo.SelectedValue == "1") //Solicitud de cheque 
            {

                dtValues = (DataTable)Session["Table"];
                if (dtValues.Rows.Count == 0)
                {
                    Alerta("Por favor incluya al menos un documento.");
                    return;
                }

            }

            if (CmbTipo.SelectedValue == "3") //Reposición de caja 
            {
                CalcularCuota(1);
            }




            CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(Sesion.Emp_Cnx);

            try
            {

                wsBuzonWeb.wsBuzonWeb wsBuzon = new wsBuzonWeb.wsBuzonWeb();

                decimal sumImporte = 0;

                PagoElectronico pagoElectronico = new PagoElectronico();

                pagoElectronico.Id_Emp = Sesion.Id_Emp;
                pagoElectronico.Id_Cd = Sesion.Id_Cd_Ver;
                pagoElectronico.Id_PagElec = id_PagElec;
                pagoElectronico.Id_PagElecTipo = Int32.Parse(CmbTipo.SelectedValue);
                pagoElectronico.Id_PagElecSubTipo = Int32.Parse(CmbSubTipoGasto.SelectedValue);

                if (CmbTipo.SelectedValue == "2") //Reposición de caja 
                {
                    dtValues = (DataTable)Session["Table"];
                    if (dtValues.Rows.Count > 0)
                    {

                        //Aqui en consulta no se ocupa todo 
                        pagoElectronico.Id_PagElecCuenta = Convert.ToInt32(dtValues.Rows[0]["PagElec_Id_PagElecCuenta"]);
                        pagoElectronico.PagElec_Cuenta = Convert.ToString(dtValues.Rows[0]["PagElec_Cuenta"]);
                        pagoElectronico.PagElec_Cc = Convert.ToString(dtValues.Rows[0]["PagElec_Cc"]);
                        pagoElectronico.PagElec_Numero = Convert.ToString(dtValues.Rows[0]["PagElec_Numero"]);
                        pagoElectronico.PagElec_SubCuenta = Convert.ToString(dtValues.Rows[0]["PagElec_SubCuenta"]);
                        pagoElectronico.PagElec_SubSubCuenta = Convert.ToString(dtValues.Rows[0]["PagElec_SubSubCuenta"]);
                        pagoElectronico.PagElec_CuentaPago = Convert.ToString(dtValues.Rows[0]["PagElec_CuentaPago"]);
                        //JFCV Agregar este valor al grid  pagoElectronico.pagElecCuenta_Descripcion = cmbCtaGastos.Text.Trim() == "" ? "Sin cuenta asignada" : cmbCtaGastos.Text.Trim();
                        pagoElectronico.pagElecCuenta_Descripcion = cmbCtaGastos.Text.Trim() == "" ? "Sin cuenta asignada" : cmbCtaGastos.Text.Trim();
                    }

                }
                else
                {
                    pagoElectronico.Id_PagElecCuenta = Convert.ToInt32(cmbCtaGastos.SelectedValue.Trim());

                    pagoElectronico.PagElec_Cuenta = TxtCuenta.Text.Trim();
                    pagoElectronico.PagElec_Cc = TxtCc.Text.Trim();
                    pagoElectronico.PagElec_Numero = TxtNumero.Text.Trim();
                    pagoElectronico.PagElec_SubCuenta = TxtSubCuenta.Text.Trim();
                    pagoElectronico.PagElec_SubSubCuenta = TxtSubSubCuenta.Text.Trim();
                    pagoElectronico.PagElec_CuentaPago = TxtCuentaPago.Text.Trim();
                    pagoElectronico.pagElecCuenta_Descripcion = cmbCtaGastos.Text.Trim() == "" ? "Sin cuenta asignada" : cmbCtaGastos.Text.Trim();
                    pagoElectronico.PagElec_Destino = TxtDestino.Text;
                }


                pagoElectronico.Id_AcrCheque = Convert.ToInt32(cmbProveedor.SelectedValue.Trim());

                //Cambie la condición porque tengo dos tipos diferentes en gasto de viaje 
                //y asi no me servía para identificarlo
                //if (PnlGastosViaje.Visible)

                if (CmbTipo.SelectedValue == "3") //Cuenta de Gastos 
                {
                    if (ChkConComprobante.Checked)
                    {
                        pagoElectronico.Id_Acr = Convert.ToInt32(GetCheckBoxValues(CmbAcreedor).Count > 1 ? GetCheckBoxValues(CmbAcreedor)[0] : cmbProveedor.SelectedValue.Trim());

                    }
                    else
                    {
                        pagoElectronico.Id_Acr = 0;
                    }
                    pagoElectronico.PagElec_Solicitante = TxtSolicitanteViajero.Text.Trim();
                    pagoElectronico.PagElec_FechaRequiere = TxtFechaSalida.SelectedDate;
                    pagoElectronico.PagElec_Observaciones = TxtMotivo.Text.Trim();
                }
                else
                {
                    pagoElectronico.Id_Acr = Convert.ToInt32(GetCheckBoxValues(CmbAcreedor).Count > 1 ? GetCheckBoxValues(CmbAcreedor)[0] : cmbProveedor.SelectedValue.Trim());
                    pagoElectronico.PagElec_Solicitante = TxtSolicitante.Text.Trim();
                    pagoElectronico.PagElec_FechaRequiere = TxtFechaRequiere.SelectedDate;

                    sumImporte = txtTotalAPagar.Text.Trim() == "" ? 0 : decimal.Parse(txtTotalAPagar.Text.Trim());

                    pagoElectronico.PagElec_Importe = sumImporte;
                    pagoElectronico.PagElec_Observaciones = TxtObservaciones.Text.Trim();
                }


                //Si es de tipo reposición de caja  tomar los valores de las facturas del grid
                //if (CmbTipo.SelectedValue == "2" || CmbTipo.SelectedValue == "1") //Reposición de caja  o Solicitud de Gasto
                //{

                pagoElectronico.PagElec_Importe = 0;
                DataTable dtFcaturas = (DataTable)Session["Table"];

                sumImporte = 0;
                int CR2 = 1;
                #region agregar registros a grabar
                if (!PnlGastosViaje.Visible)
                {
                    foreach (DataRow row in dtFcaturas.Rows)
                    {
                        if (Convert.ToString(row["GVComprobante_ConComprobanteDescripcion"].ToString()) == "Con Comprobante")
                        {

                            //JFCV 21 enero 2016 aqui no ocupo que lea la información del XML porque ya traigo todos los campos 

                            byte[] xmlpdf = (row["GVComprobante_Pdf"] == System.DBNull.Value ? null : (byte[])(row["GVComprobante_Pdf"]));
                            byte[] xmlFile = (row["GVComprobante_Xml"] == System.DBNull.Value ? null : (byte[])(row["GVComprobante_Xml"]));

                            Decimal n_subtotal = (row["PagElec_Subtotal"] == System.DBNull.Value ? 0 : (Convert.ToDecimal(row["PagElec_Subtotal"])));
                            Decimal n_iva = (row["PagElec_Iva"] == System.DBNull.Value ? 0 : (Convert.ToDecimal(row["PagElec_Iva"])));
                            Decimal n_impretenido = (row["PagElec_ImpRetenido"] == System.DBNull.Value ? 0 : (Convert.ToDecimal(row["PagElec_ImpRetenido"])));
                            Decimal n_ivaretenido = (row["PagElec_IvaRetenido"] == System.DBNull.Value ? 0 : (Convert.ToDecimal(row["PagElec_IvaRetenido"])));

                            pagoElectronico.PagElecArchivo.Add(
                                   new CapaEntidad.PagoElectronicoArchivo(
                                       (int)Sesion.Id_Emp,
                                       (int)Sesion.Id_Cd_Ver,
                                       (int)pagoElectronico.Id_PagElec,
                                       (int)CR2++,
                                       xmlpdf,
                                       xmlFile,
                                       Convert.ToDateTime(row["GVComprobante_Fecha"]), //DT.Rows[0]["fecha"],
                                       (string)row["PagElec_Serie"], //DT.Rows[0]["serie"],
                                       (string)row["PagElec_Folio"], //DT.Rows[0]["folio"],
                                       Convert.ToDecimal(row["GVComprobante_Importe"]), //DT.Rows[0]["importe"]),
                                       (string)row["PagElec_Cuenta"],
                                       (string)row["PagElec_Cc"],
                                       (string)row["PagElec_Numero"],
                                       (string)row["PagElec_SubCuenta"],
                                       (string)row["PagElec_SubSubCuenta"],
                                       (string)row["PagElec_CuentaPago"],
                                       (string)row["GVComprobante_Observaciones"],
                                       Convert.ToInt32(row["PagElec_Id_PagElecCuenta"]),
                                       (string)row["PagElec_Rfc"],
                                       (string)row["PagElec_UUID"],
                                        n_subtotal,
                                        n_iva,
                                        n_impretenido,
                                        n_ivaretenido,
                                         "") //Con soporte o sin soporte se va en blanco 
                               );


                            //JFCV calcular la suma de las partidas 

                        }
                        else  //else si el registro es de tipo sin comprobante , entonces lo que hago es que tomo la cuenta que capturo ahi y se la pongo al encabezado 
                        // en lugar de que tome la primer cuenta si tiene soporte entonces guarda en el encabezado esa cuenta 
                        {
                            if (CmbTipo.SelectedValue != "1") //Si diferente a solicitud de cheque no toma la cuenta de los comprobantes si no de el combo
                            {

                                if (row["PagElec_Id_PagElecCuenta"] != null)
                                    pagoElectronico.Id_PagElecCuenta = Convert.ToInt32(row["PagElec_Id_PagElecCuenta"]);
                                pagoElectronico.PagElec_Cuenta = (string)row["PagElec_Cuenta"];
                                pagoElectronico.PagElec_Cc = (string)row["PagElec_Cc"];
                                pagoElectronico.PagElec_Numero = (string)row["PagElec_Numero"];
                                pagoElectronico.PagElec_SubCuenta = (string)row["PagElec_SubCuenta"];
                                pagoElectronico.PagElec_SubSubCuenta = (string)row["PagElec_SubSubCuenta"];
                                pagoElectronico.PagElec_CuentaPago = (string)row["PagElec_CuentaPago"];
                            }

                        }
                        sumImporte += (row["GVComprobante_Importe"] == System.DBNull.Value ? 0 : Convert.ToDecimal(row["GVComprobante_Importe"].ToString()));
                    }
                    //JFCV 02 feb 2016 cargo en pdf el primer archivo que haya registrado
                    pagoElectronico.PagElec_Xml = pagoElectronico.PagElecArchivo.Count > 0 && pagoElectronico.PagElecArchivo[0].PagElec_XMLStream.Length > 0 ? new System.Data.SqlTypes.SqlXml((Stream)(new System.IO.MemoryStream(pagoElectronico.PagElecArchivo[0].PagElec_XMLStream))) : null;
                    pagoElectronico.PagElec_Pdf = pagoElectronico.PagElecArchivo.Count > 0 && pagoElectronico.PagElecArchivo[0].PagElec_PDFStream.Length > 0 ? pagoElectronico.PagElecArchivo[0].PagElec_PDFStream : null;

                    //JFCV para qu ecalcule el total correctamente 05 feb 2016 
                    pagoElectronico.PagElec_Importe = sumImporte;
                }
                else
                {
                    pagoElectronico.PagElec_Importe = decimal.Parse(TxtTotal.Text.Trim());
                    pagoElectronico.PagElec_Xml = null;
                    pagoElectronico.PagElec_Pdf = null;
                }

                #endregion archivos que va a grabar


                //guardar la session tabledel
                DataTable dtFcaturasDel = (DataTable)Session["TableDel"];

                PagoElectronico gastoViaje1 = new PagoElectronico();

                gastoViaje1.Id_Emp = Sesion.Id_Emp;
                gastoViaje1.Id_Cd = Sesion.Id_Cd_Ver;
                gastoViaje1.Id_PagElec = Int32.Parse(Request.QueryString["Id"] == null ? "-1" : Request.QueryString["Id"]);

                CN_CapPagoElectronico clsCapPagoElectronico1 = new CN_CapPagoElectronico();
                clsCapPagoElectronico1.ConsultaPagoElectronico(gastoViaje1, Sesion.Emp_Cnx);

                cargararchivosDel(gastoViaje1.PagElecArchivo);
                dtFcaturasDel = (DataTable)Session["TableDel"];

                //if (PnlSoporte.Visible)
                //{
                #region Tiene Soporte visible
                //if (CmbTipo.SelectedValue == "2" || CmbTipo.SelectedValue == "1") //Reposición de caja 
                //{
                dtValues = (DataTable)Session["Table"];
                if (dtValues.Rows.Count > 0)
                {
                    bool tienesoporte = false;
                    foreach (DataRow row in dtValues.Rows)
                    {
                        // si encuentra un archivo con soporte ( GVComprobante_Pdf 1= "" 9 entonces tienesoporte = true
                        if (Convert.ToString(row["GVComprobante_ConComprobanteDescripcion"].ToString()) == "Sin Comprobante")
                        {
                            if (Convert.ToString(row["GVComprobante_Pdf"].ToString()) != "")
                            {
                                //jfcv 27oct
                                tienesoporte = true;
                                PagElec_Soporte4 = row["GVComprobante_Pdf"] == System.DBNull.Value ? null : (byte[])(row["GVComprobante_Pdf"]);
                                pagoElectronico.PagElec_Soporte = PagElec_Soporte4;
                                pagoElectronico.PagElec_Soporte_Nombre = row["PagElec_Soporte_Nombre"] == System.DBNull.Value ? null : (string)(row["PagElec_Soporte_Nombre"]);
                                pagoElectronico.PagElec_Soporte_Tipo = row["PagElec_Soporte_Tipo"] == System.DBNull.Value ? null : (string)(row["PagElec_Soporte_Tipo"]);
                                //JFCV 4feb 4:41 sumImporte = sumImporte + Convert.ToDecimal(row["GVComprobante_Importe"].ToString());
                                pagoElectronico.PagElec_SoporteImporte = Convert.ToDecimal(row["GVComprobante_Importe"].ToString());
                            }
                        }
                    }

                    if (tienesoporte == false)
                    {
                        pagoElectronico.PagElec_Soporte = null;
                        pagoElectronico.PagElec_Soporte_Nombre = null;
                        pagoElectronico.PagElec_Soporte_Tipo = null;
                        pagoElectronico.PagElec_SoporteImporte = 0;
                    }
                    //JFCV 4feb 4:41
                    //else
                    //{
                    //    pagoElectronico.PagElec_Importe = sumImporte;
                    //}


                }

                #endregion Tiene Soporte visible
                //}
                //else
                //{
                //    pagoElectronico.PagElec_Soporte = null;
                //    pagoElectronico.PagElec_SoporteImporte = 0;
                //}

                //JFCV 11nov2015 validar si  pagoElectronico.PagElec_Importe = sumImporte; = 0 

                pagoElectronico.PagElec_IdU = Sesion.Id_U;
                pagoElectronico.PagElec_FechaRegistro = DateTime.Now;
                if (CmbTipo.SelectedValue == "3") //Gastos de viaje  
                {
                    pagoElectronico.PagElec_FechaSalida = txtFechaElaboracion.SelectedDate;
                }
                else
                {
                    pagoElectronico.PagElec_FechaSalida = TxtFechaSalida.SelectedDate;
                }

                pagoElectronico.Acr_Nombre = CmbAcreedor.SelectedItem.Text;

                int verificador = -1;

                CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();

                oDB.BeginTransaction();


                #region EliminarArchivo las facturas anteriores las desasigna
                ////FCO CEPEDA urge poner esto   //DataTable dtFcaturasDel = (DataTable)Session["TableDel"];

                // aqui agrego el que elimina el movimiento del grid ( poner transacción ) 
                clsPagoElectronico.ModificarPagoElectronico(pagoElectronico, Sesion.Emp_Cnx, ref verificador);

                if (verificador == 1)
                {

                    //JFCV elimino las facturas que haya tenido grabadas 
                    foreach (DataRow row in dtFcaturasDel.Rows)
                    {
                        GastoViajeComprobante gastoViajeComprobante = new GastoViajeComprobante();

                        if (Convert.ToString(row["GVComprobante_ConComprobanteDescripcion"].ToString()) == "Con Comprobante")
                        {
                            try
                            {
                                if (row != null)
                                {
                                    wsBuzonWeb.wsBuzonWeb wsBuzonDel = new wsBuzonWeb.wsBuzonWeb();
                                    wsBuzonDel.DelAsigFacGastos(
                                        BuzonKey,
                                        (string)row["PagElec_Rfc"],
                                        (string)row["PagElec_Serie"],
                                        (string)row["PagElec_Folio"],
                                        Sesion.Id_Emp,
                                        Sesion.Id_Cd,
                                        Sesion.Id_U, Emp_RFC
                                    );
                                }

                            }
                            catch
                            {
                            }

                        }
                    }

                }
                #endregion EliminarArchivo

                #region reserva  los archivos cargados
                if (verificador == 1)
                {
                    //if (CmbTipo.SelectedValue == "2" || CmbTipo.SelectedValue == "1") //Reposición de caja  agregue solicitu de gastos 
                    //{
                    if (CmbTipo.SelectedValue == "3" && ChkConComprobante.Checked == false)
                    {
                        //Si es tipo cuenta de gastos y no cargo archivos , no entra al proceso siguiente
                    }
                    else
                    {
                        ///aqui vuelvo a poner las facturas con estatus de ya ocupadas 
                        ///para que no las puedan tomar 
                        for (int CR = 0; CR <= (pagoElectronico.PagElecArchivo.Count - 1); CR++)
                        {
                            if (Convert.ToString(pagoElectronico.PagElecArchivo[CR].PagElec_PDFStream.ToString()) != "")
                            {
                                wsBuzon.PutAsigFacGastos(
                                BuzonKey,
                                Convert.ToString(pagoElectronico.PagElecArchivo[CR].PagElec_RFC.ToString()),
                                Convert.ToString(pagoElectronico.PagElecArchivo[CR].Serie.ToString()),
                                Convert.ToString(pagoElectronico.PagElecArchivo[CR].Folio.ToString()),
                                Sesion.Id_Emp,
                                Sesion.Id_Cd_Ver,
                                Sesion.Id_U,
                                Sesion.U_Nombre,
                                Sesion.U_Correo,
                                Emp_RFC
                            );
                            }
                        }
                    }
                    //}
                }
                #endregion

                #region guardagastoviaje

                //JFCV 28 dic 2016
                // Si es de tipo solicitud de gastos y la cuenta elegida es la de cuenta de acreedores 
                // inserta también el gasto de viaje para su autorización 
                //JFCV TODO 3 feb 2016
                int esanticipoAcreedores = 0;
                int eraAnticipoAcreedores = 0;

                eraAnticipoAcreedores = (int)Session["eraAnticipoAcreedores"];

                //if (CmbTipo.SelectedValue == "1" && TxtCc.Text == "1031" && TxtSubSubCuenta.Text == "20001" && TxtSubSubCuenta.Text == "00")
                //jfcv 22 jun 2017
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = Sesion.Id_Cd_Ver;
                configuracion.Id_Emp = Sesion.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, Sesion.Emp_Cnx);


                //string ccPagoAcreedor = ConfigurationManager.AppSettings["CCPagoAcreedor"].ToString();
                //string subCuentaPagoAcreedor = ConfigurationManager.AppSettings["SubCuentaPagoAcreedor"].ToString();
                //string subSubCuentaPagoAcreedor = ConfigurationManager.AppSettings["SubSubCuentaPagoAcreedor"].ToString();
                ////if (CmbTipo.SelectedValue == "1" && pagoElectronico.PagElec_Cc == "1031" && pagoElectronico.PagElec_SubCuenta == "20001" && pagoElectronico.PagElec_SubSubCuenta == "00")
                //if (CmbTipo.SelectedValue == "1" && pagoElectronico.PagElec_Cc == ccPagoAcreedor && pagoElectronico.PagElec_SubCuenta == subCuentaPagoAcreedor && pagoElectronico.PagElec_SubSubCuenta == subSubCuentaPagoAcreedor)
                //{
                //    esanticipoAcreedores = 1;
                //}

                if (CmbTipo.SelectedValue == "1" && configuracion.Cuenta_GastosComprobacion == pagoElectronico.PagElec_Cc + pagoElectronico.PagElec_SubCuenta + pagoElectronico.PagElec_SubSubCuenta)
                {
                    esanticipoAcreedores = 1;
                }
                if (CmbTipo.SelectedValue == "1" && configuracion.Cuenta_GastosComprobacionCompras == pagoElectronico.PagElec_Cc + pagoElectronico.PagElec_SubCuenta + pagoElectronico.PagElec_SubSubCuenta)
                {
                    esanticipoAcreedores = 2;
                }


                // si la solicitud antes era de tipo anticipo proveedor y ahora no
                // entonces cancelo el movimiento de comprobación.
                if (esanticipoAcreedores == 0  &&  CmbTipo.SelectedValue == "1"  && ( eraAnticipoAcreedores == 1 || eraAnticipoAcreedores == 2)   )
                {
                    GastoViaje gastoViajecancelar = new GastoViaje();
                    gastoViajecancelar.Id_Emp = Sesion.Id_Emp;
                    gastoViajecancelar.Id_Cd = Sesion.Id_Cd_Ver;
                    gastoViajecancelar.Id_GV = Int32.Parse(MaximoIdGV());
                    gastoViajecancelar.Id_GVEst = 5;

                    CN_CapGastoViaje clsGastoViajecancelar = new CN_CapGastoViaje();
                    clsGastoViajecancelar.ModificarEstatusGastoViaje(gastoViajecancelar, Sesion.Emp_Cnx, ref verificador);

                }

                // if (Boolean.Parse(HF_AnticipoPorComprobar.Value) || esanticipoAcreedores == 1)
                if (Boolean.Parse(HF_AnticipoPorComprobar.Value) || esanticipoAcreedores != 0)
                {
                    GastoViaje gastoViaje = new GastoViaje();
                    //if (esanticipoAcreedores == 1)
                    if (esanticipoAcreedores != 0)
                    {
                        gastoViaje.Id_Emp = Sesion.Id_Emp;
                        gastoViaje.Id_Cd = Sesion.Id_Cd_Ver;
                        gastoViaje.Id_GV = Int32.Parse(MaximoIdGV());
                        gastoViaje.Id_GVEst = 1; //estatus de Por Comprobar
                        gastoViaje.GV_Solicitante = TxtSolicitante.Text.Trim();
                        gastoViaje.GV_Motivo = TxtObservaciones.Text.Trim();
                        gastoViaje.GV_FechaSalida = TxtFechaRequiere.SelectedDate;
                        gastoViaje.GV_FechaRegreso = TxtFechaRequiere.SelectedDate;
                        //SAUL GUERRA 20150623  BEGIN
                        gastoViaje.GV_Importe = sumImporte; // decimal.Parse(param[2].ToString().Trim());
                        //gastoViaje.GV_Importe = decimal.Parse(TxtImporte.Text);
                        //SAUL GUERRA 20150623  END
                        gastoViaje.Id_PagElec = pagoElectronico.Id_PagElec;
                        //JFCV
                        gastoViaje.GV_FechaElaboracion = txtFechaElaboracion.SelectedDate;
                        gastoViaje.GV_TipoTransporte = Convert.ToInt32(CmbTipoComprobante.SelectedValue.Trim());
                        gastoViaje.GV_DiasHospedaje = 0;
                        gastoViaje.GV_CantidadDesayunos = 0;
                        gastoViaje.GV_CantidadComidas = 0;
                        gastoViaje.GV_CantidadOtros = 0;
                        gastoViaje.GV_ImporteOtros = 0;
                        gastoViaje.UsuarioMod = Sesion.Id_U;
                        gastoViaje.GV_TransporteCuota = 0;
                        ///JFCV 22JUN2017 gastoViaje.GV_TipoGasto = 2; // SI ES ANTICIPOACREEDORES ES TIPO 2 ANTICIPO ACREEDORES
                        if (esanticipoAcreedores == 1)
                        {
                            gastoViaje.GV_TipoGasto = 2; // SI ES ANTICIPOACREEDORES ES TIPO 2 ANTICIPO ACREEDORES
                        }
                        else
                        {
                            gastoViaje.GV_TipoGasto = 4; // SI ES ANTICIPOCOMPRAS  ES TIPO 4  ANTICIPO ACREEDORES
                        }


                    }
                    //if (PnlGastosViaje.Visible && esanticipoAcreedores != 1)
                    if (PnlGastosViaje.Visible && esanticipoAcreedores == 0)
                    {
                        gastoViaje.Id_Emp = Sesion.Id_Emp;
                        gastoViaje.Id_Cd = Sesion.Id_Cd_Ver;
                        gastoViaje.Id_GV = Int32.Parse(MaximoIdGV());
                        gastoViaje.Id_GVEst = 1;
                        gastoViaje.GV_Solicitante = TxtSolicitanteViajero.Text.Trim();
                        gastoViaje.GV_Motivo = TxtMotivo.Text.Trim();
                        gastoViaje.GV_FechaSalida = TxtFechaSalida.SelectedDate;
                        gastoViaje.GV_FechaRegreso = TxtFechaRegreso.SelectedDate;
                        gastoViaje.GV_Importe = decimal.Parse(TxtTotal.Text);
                        gastoViaje.Id_PagElec = pagoElectronico.Id_PagElec;
                        gastoViaje.GV_FechaElaboracion = txtFechaElaboracion.SelectedDate;
                        gastoViaje.GV_TipoTransporte = Convert.ToInt32(CmbTipoComprobante.SelectedValue.Trim());
                        //jfcv 03 feb 2016 cambie txtCantidadDias porque no estaba poniendo bien los dias hospedaje 
                        gastoViaje.GV_DiasHospedaje = Int32.Parse(TxtHospedajeDias.Text);
                        gastoViaje.GV_CantidadDesayunos = Int32.Parse(TxtDesayunosDias.Text);
                        gastoViaje.GV_CantidadComidas = Int32.Parse(TxtComidasDias.Text);
                        gastoViaje.GV_CantidadCenas = Int32.Parse(TxtCenasDias.Text);
                        if (TxtOtrosGastosDias.Text == "")
                            gastoViaje.GV_CantidadOtros = 0;
                        else
                            gastoViaje.GV_CantidadOtros = Int32.Parse(TxtOtrosGastosDias.Text);
                        if (TxtOtrosGastosImporte.Text == "")
                        {
                            gastoViaje.GV_ImporteOtros = 0;
                        }
                        else
                        {
                            if (TxtOtrosGastosCutoa.Text == "")
                            {
                                gastoViaje.GV_ImporteOtros = 0;
                            }
                            else
                            {
                                gastoViaje.GV_ImporteOtros = decimal.Parse(TxtOtrosGastosCutoa.Text);
                            }

                        }
                        gastoViaje.UsuarioMod = Sesion.Id_U;

                        if (TxtTransporteCuota.Text == "")
                        {
                            gastoViaje.GV_TransporteCuota = 0;
                        }
                        else
                        {
                            gastoViaje.GV_TransporteCuota = Convert.ToDecimal(TxtTransporteCuota.Text);
                        }
                        gastoViaje.GV_TipoGasto = 1; // AQUI ES CUANDO ES UN GASTO DE VIAJE 

                    }
                    //if (PnlGastosViaje.Visible == false && esanticipoAcreedores != 1)
                    if (PnlGastosViaje.Visible == false && esanticipoAcreedores == 0)
                    {
                        gastoViaje.Id_Emp = Sesion.Id_Emp;
                        gastoViaje.Id_Cd = Sesion.Id_Cd_Ver;
                        gastoViaje.Id_GV = Int32.Parse(MaximoIdGV());
                        //jfcv 09 mzo es gasto viaje comprobado
                        gastoViaje.Id_GVEst = 6;
                        gastoViaje.GV_Solicitante = TxtSolicitanteViajero.Text.Trim();
                        gastoViaje.GV_Motivo = TxtMotivo.Text.Trim();
                        gastoViaje.GV_FechaSalida = TxtFechaSalida.SelectedDate;
                        gastoViaje.GV_FechaRegreso = TxtFechaRegreso.SelectedDate;
                        gastoViaje.GV_Importe = decimal.Parse(TxtTotal.Text);
                        gastoViaje.Id_PagElec = pagoElectronico.Id_PagElec;
                        gastoViaje.GV_FechaElaboracion = txtFechaElaboracion.SelectedDate;
                        gastoViaje.GV_TipoTransporte = Convert.ToInt32(CmbTipoComprobante.SelectedValue.Trim());

                        gastoViaje.GV_DiasHospedaje = 0;
                        gastoViaje.GV_CantidadDesayunos = 0;
                        gastoViaje.GV_CantidadComidas = 0;
                        gastoViaje.GV_CantidadCenas = 0;
                        gastoViaje.GV_FechaElaboracion = txtFechaElaboracion.SelectedDate;
                        gastoViaje.GV_TipoTransporte = Convert.ToInt32(CmbTipoComprobante.SelectedValue.Trim());
                        gastoViaje.GV_DiasHospedaje = 0;
                        gastoViaje.GV_CantidadDesayunos = 0;
                        gastoViaje.GV_CantidadComidas = 0;
                        gastoViaje.GV_CantidadOtros = 0;
                        gastoViaje.GV_ImporteOtros = 0;
                        gastoViaje.UsuarioMod = Sesion.Id_U;
                        gastoViaje.GV_TransporteCuota = 0;

                        gastoViaje.GV_TipoGasto = 3; // SI ES CUENTA GASTO COMPROBADO ES decir que eligio con comprobantes
                    }

                    verificador = -1;
                    CN_CapGastoViaje clsGastoViaje = new CN_CapGastoViaje();

                    // falta que le cambie el estatus y tipo al gasto de sin comprobantes a con comprobantes 
                    // para ello deberia correr un update al gasto y depende si es con comprobantes o sin comprobantes 
                    // al cargar que cheque si es con o sin y lo marque
                    // 
                    //if (esanticipoAcreedores == 1 && eraAnticipoAcreedores == 0)
                    if (esanticipoAcreedores != 0  && eraAnticipoAcreedores == 0)
                    {
                        clsGastoViaje.InsertarGastoViaje(gastoViaje, Sesion.Emp_Cnx, ref verificador);
                    }

                    clsGastoViaje.ModificarGastoViaje(gastoViaje, Sesion.Emp_Cnx, ref verificador);


                }
                #endregion guardagastoviaje

            #endregion Guardar

                oDB.Commit();

                // JFCV 14 Enero preguntar si desea enviar a autorización
                string message = "¿Desea Enviar el gasto a Autorizar?";

                string importeTotal = Convert.ToString(pagoElectronico.PagElec_Importe);
                string pagElec_Solicitante = pagoElectronico.PagElec_Solicitante;
                string acr_NombreAcreedor = cmbProveedor.Text;

                string pagECta_Desc = pagoElectronico.pagElecCuenta_Descripcion;
                string acmbTipo = pagoElectronico.Id_PagElecTipo.ToString();
                string cmbSubTipoGasto = pagoElectronico.Id_PagElecSubTipo.ToString();

                RAM1.ResponseScripts.Add(string.Concat(@"ShowPopup('" + message + "','" + pagoElectronico.Id_Emp.ToString() + "','" + pagoElectronico.Id_Cd.ToString() + "','" + pagoElectronico.Id_PagElec.ToString() + "','" + pagoElectronico.PagElec_IdU + "','" + Sesion.Emp_Cnx + "','" + importeTotal + "','" + acr_NombreAcreedor + "','" + pagElec_Solicitante + "','" + pagECta_Desc + "','" + acmbTipo + "','" + cmbSubTipoGasto + "')"));
                Nuevo();

            }

            catch (Exception ex)
            {
                oDB.RollBack();

                ErrorManager(ex, "Guardar_click");
              
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                oDB.RollBack();


            }

        }
        #endregion Guardar


        [WebMethod]
        public static string ProcesoEnviar(int id_Emp, int id_Cd_Ver, int id_GV, int id_U, string emp_Cnx, string importeTotal, string acr_NombreAcreedor, string pagElec_Solicitante, string pagElecCuenta_Descripcion, string acmbTipo, string cmbSubTipoGasto)
        {
            string result = "La solicitud de Pago fue Modificada y enviada.";


            PagoElectronico pagoElectronico = new PagoElectronico();


            pagoElectronico.Id_Emp = Convert.ToInt32(id_Emp);
            pagoElectronico.Id_Cd = Convert.ToInt32(id_Cd_Ver);
            pagoElectronico.Id_PagElec = Convert.ToInt32(id_GV);
            pagoElectronico.PagElec_IdU = Convert.ToInt32(id_U);
            pagoElectronico.Id_PagElecEstatus = 5;   // le pongo estatus de Solicitado 

            int verificador = 0;

            //CN_CapGastoViaje clsGastoViaje = new CN_CapGastoViaje();
            //clsGastoViaje.EnviarGastoViaje(gastoViaje, emp_Cnx, ref verificador);
            CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
            clsPagoElectronico.CambiarEstatusPagoElectronico(pagoElectronico, emp_Cnx, ref verificador);

            if (verificador == 1)
            {
                //EnviarCorreo(id_Emp, id_Cd_Ver, id_GV, id_U, emp_Cnx, importeTotal, acr_Motivo, pagElec_Solicitante);
                EnviarCorreo(id_Emp, id_Cd_Ver, id_GV, id_U, emp_Cnx, importeTotal, acr_NombreAcreedor, pagElec_Solicitante, pagElecCuenta_Descripcion, acmbTipo, cmbSubTipoGasto);

            }


            return result;
        }

        //SAUL GUERRA 20150625  BEGIN
        private DataTable ObtenerDatos(byte[] xmlObject)
        {
            DataTable dtable = new DataTable();
            dtable.Columns.Add("rfc", typeof(string));
            dtable.Columns.Add("fecha", typeof(DateTime));
            dtable.Columns.Add("serie", typeof(string));
            dtable.Columns.Add("folio", typeof(string));
            dtable.Columns.Add("importe", typeof(string));
            //jfcv 24nov2015 Agregar 3 valores de impuestos
            dtable.Columns.Add("subtotal", typeof(string));
            dtable.Columns.Add("ivaretenido", typeof(string));
            dtable.Columns.Add("impuestoretenido", typeof(string));
            dtable.Columns.Add("iva", typeof(string));
            dtable.Columns.Add("uuid", typeof(string));


            if (xmlObject != null && xmlObject.Length > 0)
            {
                try
                {
                    MemoryStream xmlStream = new MemoryStream(xmlObject);
                    string rfc = null;
                    string serie = null;
                    string folio = null;
                    DateTime? fecha = null;
                    decimal? importe = null;
                    //jfcv 24nov2015 Agregar 3 valores de impuestos
                    decimal? subtotal = null;
                    decimal? ivaretenido = null;
                    decimal? impuestoretenido = null;
                    decimal? iva = null;
                    string uuid = null;


                    XmlDocument xmldoc = new XmlDocument();
                    XmlNodeList xmlnode;
                    try
                    {
                        xmldoc.Load(xmlStream);
                        Session["xml"] = xmlStream;
                    }
                    catch (Exception ex)
                    {
                        dtable = null;
                        //Alerta("Problemas al leer el XML de la factura" + ex.Message);
                        return dtable;
                    }

                    xmlnode = xmldoc.GetElementsByTagName("cfdi:Comprobante");
                    try
                    {
                        serie = xmlnode[0].Attributes["serie"].Value;
                    }
                    catch
                    {
                        try
                        {
                            serie = xmlnode[0].Attributes["Serie"].Value;
                        }
                        catch
                        {
                            serie = "";
                        }
                    }
                    //JFCV 04 ene 2016 algunas facturas pueden no traer serie ni folio
                    try
                    {
                        folio = xmlnode[0].Attributes["folio"].Value;
                    }
                    catch
                    {
                        try
                        {
                            folio = xmlnode[0].Attributes["Folio"].Value;
                        }
                        catch
                        {
                            folio = "";
                        }
                    }
                    //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                    try
                    {
                        fecha = Convert.ToDateTime(xmlnode[0].Attributes["fecha"].Value);
                        importe = decimal.Parse("0" + xmlnode[0].Attributes["total"].Value);

                        //jfcv 24nov2015 Agregar 3 valores de impuestos
                        subtotal = decimal.Parse("0" + xmlnode[0].Attributes["subTotal"].Value);
                        //jfcv fin
                    }
                    catch
                    {
                        fecha = Convert.ToDateTime(xmlnode[0].Attributes["Fecha"].Value);
                        importe = decimal.Parse("0" + xmlnode[0].Attributes["Total"].Value);

                        //jfcv 24nov2015 Agregar 3 valores de impuestos
                        subtotal = decimal.Parse("0" + xmlnode[0].Attributes["SubTotal"].Value);
                        //jfcv fin
                    }
                    xmlnode = xmldoc.GetElementsByTagName("cfdi:Emisor");
                    //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                    try
                    {
                        rfc = xmlnode[0].Attributes["rfc"].Value;
                    }
                    catch
                    {
                        rfc = xmlnode[0].Attributes["Rfc"].Value;
                    }


                    TxtImporte.Text = importe.ToString();


                    xmlnode = xmldoc.GetElementsByTagName("tfd:TimbreFiscalDigital");
                    uuid = xmlnode[0].Attributes["UUID"].Value;
                    if (folio == "")
                    {
                        folio = uuid.Substring(0, 10);
                    }
                    iva = 0;

                    try
                    {

                        xmlnode = xmldoc.GetElementsByTagName("cfdi:Traslados");
                        if (xmlnode[0].ChildNodes.Count > 0)
                        {
                            if (xmlnode[0].ChildNodes.Count == 1)
                            {
                                //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                                try
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IVA")
                                        iva = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                }
                                catch
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["Impuesto"].Value == "002")
                                        iva = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                }
                            }
                            else
                            {
                                //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                                try
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IVA")
                                        iva = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);

                                    if (xmlnode[0].ChildNodes[1].Attributes["impuesto"].Value == "IVA")
                                        iva = iva + decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["importe"].Value);
                                }
                                catch
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["Impuesto"].Value == "002")
                                        iva = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                    if (xmlnode[0].ChildNodes[1].Attributes["Impuesto"].Value == "002")
                                        iva = iva + decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["Importe"].Value);
                                }

                            }
                        }

                    }
                    catch
                    {

                    }


                    ivaretenido = 0;
                    impuestoretenido = 0;

                    try
                    {

                        xmlnode = xmldoc.GetElementsByTagName("cfdi:Retenciones");
                        if (xmlnode[0].ChildNodes.Count > 0)
                        {

                            if (xmlnode[0].ChildNodes.Count == 1)
                            {
                                //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                                try
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IVA")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                }
                                catch
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["Impuesto"].Value == "002")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                }
                            }
                            else
                            {
                                //JFCV 11 AGO2017 TUVE QUE PONER UN TRY CATCH PORQUE EN LA VERSION DE CFDI 3.3 CAMBIaron nombre por ejemplo fecha por Fecha
                                try
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IVA")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);

                                    if (xmlnode[0].ChildNodes[1].Attributes["impuesto"].Value == "IVA")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["importe"].Value);
                                }
                                catch
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["Impuesto"].Value == "002")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                    if (xmlnode[0].ChildNodes[1].Attributes["Impuesto"].Value == "002")
                                        ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["Importe"].Value);
                                    else
                                        impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["Importe"].Value);
                                }

                            }
                        }

                    }
                    catch
                    {

                    }


                    //jfcv fin 24nov2015 Agregar 3 valores de impuestos

                    //jfcv 24nov2015 Agregar 3 valores de impuestos
                    //dtable.Rows.Add(rfc, fecha, serie, folio, importe);

                    dtable.Rows.Add(rfc, fecha, serie, folio, importe, subtotal, ivaretenido, impuestoretenido, iva, uuid);

                }
                catch (Exception ex)
                {
                    dtable = null;
                }
            }
            else
            {
                dtable = null;
            }


            return dtable != null && dtable.Rows.Count > 0 ? dtable : null;
        }
	


        //JFCV ver si el xml es legal 
        /// <summary>
        /// Remove illegal XML characters from a string.
        /// </summary>
        public string SanitizeXmlString(string xml)
        {
            if (xml == null)
            {
                throw new ArgumentNullException("xml");
            }

            StringBuilder buffer = new StringBuilder(xml.Length);

            foreach (char c in xml)
            {

                if (IsLegalXmlChar(c))
                {
                    buffer.Append(c);
                }
            }

            return buffer.ToString();
        }

        /// <summary>
        /// Whether a given character is allowed by XML 1.0.
        /// </summary>
        public bool IsLegalXmlChar(int character)
        {

            if (character == 0x27)
                return false;

            return
            (
                 character == 0x9 /* == '\t' == 9   */          ||
                 character == 0xA /* == '\n' == 10  */          ||
                 character == 0xD /* == '\r' == 13  */          ||
                 character == 0x27 ||
                (character >= 0x20 && character <= 0xD7FF) ||
                (character >= 0xE000 && character <= 0xFFFD) ||
                (character >= 0x10000 && character <= 0x10FFFF)
            );
        }


        protected byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        protected void Nuevo()
        {
            CmbTipo.SelectedValue = "0";

            //Pendiente Eliminar
            //CmbConcepto.SelectedValue = "0";

            CmbAcreedor.SelectedValue = "0";
            rgPagoElectronico.DataSource = new List<GastoViajeComprobante>();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            TxtSolicitante.Text = Sesion.U_Nombre;
            TxtFechaRequiere.SelectedDate = null;
            TxtCuenta.Text = string.Empty;
            TxtCc.Text = string.Empty;
            TxtNumero.Text = string.Empty;
            //SAUL GUERRA 20150623  BEGIN
            TxtImporte.Text = string.Empty;
            //SAUL GUERRA 20150623  END
            TxtObservaciones.Text = string.Empty;
            TxtCuentaPago.Text = string.Empty;
            TxtSubCuenta.Text = string.Empty;
            TxtSubSubCuenta.Text = string.Empty;

            RadAsyncUpload1.Visible = true;
            btnQuitar.Visible = false;
            Label9.Visible = false;
            Label9.Text = "";
            Label3.Text = "";
            Label7.Text = "";


            //JFCV 5 oct 2015 inicilizar las facturas seleccionadas si le da botón nuevo
            RadListBoxDestination.Items.Clear();

        }

        private decimal ObtenerImporte(string xmlPath)
        {
            string str = null;

            return str != null ? decimal.Parse(str) : 0;
        }

        private void descargarXML(int id_PagElec, byte[] archivoXML)
        {
            PagoElectronico pagoElectronico = new PagoElectronico();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            pagoElectronico.Id_Emp = Sesion.Id_Emp;
            pagoElectronico.Id_Cd = Sesion.Id_Cd_Ver;
            pagoElectronico.Id_PagElec = id_PagElec;


            string ruta = null;

            ruta = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + pagoElectronico.Id_PagElec.ToString() + ".txt"));

            string strarchivo = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + pagoElectronico.Id_PagElec.ToString()));
            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(strarchivo + ".xml"))
                File.Delete(strarchivo + ".xml");

            if (archivoXML != null)
            {
                using (FileStream fileStream = File.Create(ruta))
                {
                    MemoryStream MS = new MemoryStream(archivoXML);
                    MS.CopyTo(fileStream);
                    fileStream.Close();
                }
                File.Move(ruta, strarchivo + ".xml");
                RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('xmlSAT\\archivoXml" + Sesion.Id_U.ToString() + "PagElec", pagoElectronico.Id_PagElec.ToString(), ".xml')"));
            }

        }

        private void descargarPDF(int id_PagElec, byte[] archivoPdf)
        {
            PagoElectronico pagoElectronico = new PagoElectronico();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            pagoElectronico.Id_Emp = Sesion.Id_Emp;
            pagoElectronico.Id_Cd = Sesion.Id_Cd_Ver;
            pagoElectronico.Id_PagElec = id_PagElec;


            if (archivoPdf != null)
            {
                if (archivoPdf.Length > 0)
                {
                    string tempPDFname = string.Concat("GASTO_"
                             , Sesion.Id_Emp.ToString()
                             , "_", Sesion.Id_Cd_Ver.ToString()
                             , "_", id_PagElec.ToString()
                             , ".pdf");
                    string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    string WebURLtempPDFGastos = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDFGastos"].ToString(), tempPDFname);
                    this.ByteToTempPDF(URLtempPDF, archivoPdf);
                    // ------------------------------------------------------------------------------------------------
                    // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    // ------------------------------------------------------------------------------------------------
                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDFGastos, "')"));
                }
            }
            else
            {
                Alerta("El registro no tiene un archivo PDF.");
            }
        }

        private void ByteToTempPDF(string tempPath, byte[] filebytes)
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
            FileStream fs = new FileStream(tempPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);
            fs.Write(filebytes, 0, filebytes.Length);
            fs.Close();
        }

        private string MaximoId()
        {
            try
            {
                Sesion Sesion = new Sesion();

                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                    return "";
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CapPagoElectronico", "Id_PagElec", Sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string MaximoIdGV()
        {
            try
            {
                Sesion Sesion = new Sesion();

                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                    return "";
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CapGastoViaje", "Id_GV", Sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        #region Enviar Correo y Alerta
        private void CalcularCuota(int dias)
        {
            int hospedate = 1000;
            int desayuno = 75;
            int comida = 125;
            int cena = 125;

            if (dias > 0)
            {
                TxtHospedajeImporte.Text = string.Format("{0:N2}", (Convert.ToInt32(TxtHospedajeDias.Text)) * hospedate);
                TxtDesayunosImporte.Text = string.Format("{0:N2}", Convert.ToInt32(TxtDesayunosDias.Text) * desayuno);
                TxtComidasImporte.Text = string.Format("{0:N2}", Convert.ToInt32(TxtComidasDias.Text) * comida);
                TxtCenasImporte.Text = string.Format("{0:N2}", (Convert.ToInt32(TxtCenasDias.Text)) * cena);

                TxtHospedajeCutoa.Text = string.Format("{0:N2}", hospedate);
                TxtDesayunosCutoa.Text = string.Format("{0:N2}", desayuno);
                TxtCenasCutoa.Text = string.Format("{0:N2}", cena);
                TxtComidasCutoa.Text = string.Format("{0:N2}", comida);



                //JFCV 20 Ene 2016 en el calculo se incluyo la cuota de transporte 
                int gv_CantidadOtros = 0;
                decimal gv_ImporteOtros = 0;
                decimal transporteCuota = 0;
                if (TxtOtrosGastosDias.Text != "")
                    gv_CantidadOtros = Int32.Parse(TxtOtrosGastosDias.Text);
                //JFCV otros gastos estaba calculando aml el total 
                //if (TxtOtrosGastosImporte.Text != "")
                //    gv_ImporteOtros = decimal.Parse(TxtOtrosGastosImporte.Text);
                if (TxtOtrosGastosCutoa.Text != "")
                    gv_ImporteOtros = decimal.Parse(TxtOtrosGastosCutoa.Text);
                if (TxtTransporteCuota.Text != "")
                    transporteCuota = decimal.Parse(TxtTransporteCuota.Text);


                TxtOtrosGastosImporte.Text = string.Format("{0:N2}", (gv_ImporteOtros * gv_CantidadOtros));
                TxtTotal.Text = string.Format("{0:N2}", (Convert.ToDecimal(TxtHospedajeImporte.Text) + Convert.ToDecimal(TxtDesayunosImporte.Text) + Convert.ToDecimal(TxtOtrosGastosImporte.Text) + Convert.ToDecimal(TxtComidasImporte.Text) + Convert.ToDecimal(TxtCenasImporte.Text) + transporteCuota));
                //TxtTotal.Text = string.Format("{0:N2}", ((dias - 1) * hospedate) + (dias * desayuno) + ((dias - 1) * cena) + (dias * comida));


            }
            else
            {
                TxtHospedajeImporte.Text = "0";
                TxtDesayunosImporte.Text = "0";
                TxtCenasImporte.Text = "0";
                TxtComidasImporte.Text = "0";

                TxtHospedajeCutoa.Text = "0";
                TxtDesayunosCutoa.Text = "0";
                TxtCenasCutoa.Text = "0";
                TxtComidasCutoa.Text = "0";

                TxtHospedajeDias.Text = "0";
                TxtDesayunosDias.Text = "0";
                TxtCenasDias.Text = "0";
                TxtComidasDias.Text = "0";
                TxtTotal.Text = "0";
            }
        }

        private static void EnviarCorreo(int id_Emp, int id_Cd_Ver, int id_GV, int id_U, string emp_Cnx, string importeComprobado, string acr_Nombre, string pagElec_Solicitante, string pagElecCuenta_Descripcion, string cmbTipo, string cmbSubTipoGasto)
        {
            try
            {
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = Convert.ToInt32(id_Cd_Ver);
                configuracion.Id_Emp = Convert.ToInt32(id_Emp);
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, emp_Cnx);

                StringBuilder cuerpo_correo = new StringBuilder();

                cuerpo_correo.Append("<table>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td>{PagElec_Solicitante} solicita el pago al acreedor {Acr_Nombre} por el monto de $" + importeComprobado + "</td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td>Cuenta : {pagElecCuenta_Descripcion}</td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td><br></td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td>Accesar a : <a href='{href}'>Autorización de Gastos</a></td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td>&nbsp;</td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("</table>");



                string strUrl = (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/").Replace("//", "/").Replace("http:/", "http://");
                string txtCuerpoMail = cuerpo_correo.ToString();
                txtCuerpoMail = txtCuerpoMail.Replace("{Acr_Nombre}", acr_Nombre);
                txtCuerpoMail = txtCuerpoMail.Replace("{PagElec_Solicitante}", pagElec_Solicitante);
                txtCuerpoMail = txtCuerpoMail.Replace("{pagElecCuenta_Descripcion}", pagElecCuenta_Descripcion);
                txtCuerpoMail = txtCuerpoMail.Replace("{href}", strUrl + "ProAutorizacion_PagoElectronico.aspx");



                SmtpClient smtp = new SmtpClient();
                smtp.Host = configuracion.Mail_Servidor;
                smtp.Port = Convert.ToInt32(configuracion.Mail_Puerto);
                smtp.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);

                MailAddress from = new MailAddress(configuracion.Mail_Remitente);
                MailMessage mail = new MailMessage();

                mail.From = from;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.Subject = "Mail de Autorización de Gastos";


                if (cmbTipo == "1")
                {
                    if (cmbSubTipoGasto.Trim() == "1")
                    {
                        mail.To.Add(new MailAddress(configuracion.Mail_GastosFletes));
                    }
                    else if (cmbSubTipoGasto.Trim() == "2")
                    {
                        mail.To.Add(new MailAddress(configuracion.Mail_GastosNoInventariables));
                    }
                    else if (cmbSubTipoGasto.Trim() == "3")
                    {
                        mail.To.Add(new MailAddress(configuracion.Mail_GastosComprasLocales));
                    }
                    else if (cmbSubTipoGasto.Trim() == "4")
                    {
                        mail.To.Add(new MailAddress(configuracion.Mail_GastosPagoServicios));
                    }
                    else if (cmbSubTipoGasto.Trim() == "5")
                    {
                        mail.To.Add(new MailAddress(configuracion.Mail_GastosOtrosPagos));
                    }
                    //JFCV 09 sep 2016 agregar dos subtipos de gasto Mail_GastosPagoServicios para que le lleguen a Mario Medina
                    else if (cmbSubTipoGasto.Trim() == "6")
                    {
                        mail.To.Add(new MailAddress(configuracion.Mail_GastosPagoServicios));
                    }
                    else if (cmbSubTipoGasto.Trim() == "7")
                    {
                        mail.To.Add(new MailAddress(configuracion.Mail_GastosPagoServicios));
                    }
                    else
                    {
                        mail.To.Add(new MailAddress(configuracion.Mail_GastosCuentaGastos));
                        //throw new Exception("Error falta el Sub-Tipo de Gasto");
                    }
                }
                else if (cmbTipo.Trim() == "2")
                {
                    mail.To.Add(new MailAddress(configuracion.Mail_GastosReposicionCaja));
                }
                else if (cmbTipo.Trim() == "3")
                {
                    mail.To.Add(new MailAddress(configuracion.Mail_GastosCuentaGastos));
                }
                else
                {
                    throw new Exception("Error falta el Tipo de Gasto");
                }

                //JFCV envia mail al   gerente de la sucursal 
                mail.To.Add(new MailAddress(configuracion.Mail_GastosAvisoGerente));
                //mail.To.Add(new MailAddress(configuracion.Mail_GastosFletes));

                mail.Body = txtCuerpoMail;
                smtp.Send(mail);

            }
            catch (Exception ex)
            {
                //Alerta("El correo no pudo ser enviado. Error: " + ex.Message);
            }
        }

    
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


        #endregion Enviar Correo y Alerta

        protected class cbFacturaTemplate : ITemplate
        {
            bool isGroup { get; set; }
            bool isMultiSelect { get; set; }

            public cbFacturaTemplate() : this(false, false) { }

            public cbFacturaTemplate(bool isGroup, bool isMultiSelect)
            {
                this.isGroup = isGroup;
                this.isMultiSelect = isMultiSelect;
            }

            public void InstantiateIn(Control container)
            {
                Table table = new Table();
                table.Width = Unit.Percentage(100);
                TableRow mainRow = new TableRow();
                TableCell cell1 = new TableCell();
                if (this.isGroup)
                {
                    cell1.DataBinding += new EventHandler(cellBodyCheck_DataBinding);
                }
                else
                {
                    cell1.DataBinding += new EventHandler(cellBody_DataBinding);
                }
                mainRow.Cells.Add(cell1);

                table.Rows.Add(mainRow);
                container.Controls.Add(table);
            }

            private void cellHead_DataBinding(object sender, EventArgs e)
            {

                TableCell target = (TableCell)sender;
                RadComboBoxItem item = (RadComboBoxItem)target.BindingContainer;

                string itemText = (string)(((wsBuzonWeb.InvoiceList)item.DataItem)).nombre;
                target.Controls.Add(new LiteralControl(itemText));
            }

            private void cellBodyCheck_DataBinding(object sender, EventArgs e)
            {
                TableCell target = (TableCell)sender;
                RadListBoxItem item = (RadListBoxItem)target.BindingContainer;

                if (item.DataItem != null)
                {
                    Panel ASPPanel = new Panel();
                    ASPPanel.ID = !((bool)(((wsBuzonWeb.InvoiceList)item.DataItem)).IsGroupHeader) ? "divItem" : "divHeader";
                    ASPPanel.Width = 385;

                    CheckBox ASPCheckBox = new CheckBox();
                    if (!((bool)(((wsBuzonWeb.InvoiceList)item.DataItem)).IsGroupHeader))
                    {
                        ASPCheckBox.ID = "CheckBox1";
                        ASPCheckBox.Text = String.Format(
                            "{0}{1} Fecha:{2} Importe:{3} Suc.:{4}",
                            (string)(((wsBuzonWeb.InvoiceList)item.DataItem)).Serie,
                            (string)(((wsBuzonWeb.InvoiceList)item.DataItem)).Folio_Documento,
                            Convert.ToDateTime((((wsBuzonWeb.InvoiceList)item.DataItem)).Fecha_Documento).ToString("MM/dd/yyyy"),
                            Convert.ToDecimal((((wsBuzonWeb.InvoiceList)item.DataItem)).Importe_Total_Documento).ToString("C"),
                            (string)(((wsBuzonWeb.InvoiceList)item.DataItem)).SucursalCorta
                        );

                        ASPPanel.Controls.Add(ASPCheckBox);
                    }
                    else
                    {
                        ASPPanel.Controls.Add(new LiteralControl((string)(((wsBuzonWeb.InvoiceList)item.DataItem)).nombre));
                    }



                    target.Controls.Add(ASPPanel);
                }
            }

            private void cellBody_DataBinding(object sender, EventArgs e)
            {

                HtmlTableCell target = (HtmlTableCell)sender;

                RadComboBoxItem item = (RadComboBoxItem)target.BindingContainer;

                Panel ASPPanel = new Panel();
                ASPPanel.ID = "divItem";
                ASPPanel.Width = 385;

                string itemText = String.Format(
                    "{0}{1} Fecha:{2} Importe:{3} Suc.:{4}",
                    (string)DataBinder.Eval(item, "Serie"),
                    (string)DataBinder.Eval(item, "Folio_Documento"),
                    Convert.ToDateTime((string)DataBinder.Eval(item, "Fecha_Documento")).ToString("MM/dd/yyyy"),
                    Convert.ToDecimal((string)DataBinder.Eval(item, "Importe_Total_Documento")).ToString("C"),
                    (string)DataBinder.Eval(item, "SucursalCorta")
                );

                ASPPanel.Controls.Add(new LiteralControl(itemText));

                target.Controls.Add(ASPPanel);
            }
        }

        [Serializable]
        private class InvoiceList : wsBuzonWeb.InvoiceList
        {
            public InvoiceList(wsBuzonWeb.InvoiceList From)
            {
                if (From != null)
                {
                    this.ID = From.ID;
                    this.SucursalCorta = From.SucursalCorta;
                    this.Tipo_Documento = From.Tipo_Documento;
                    this.Serie = From.Serie;
                    this.Folio_Documento = From.Folio_Documento;
                    this.Fecha_Documento = From.Fecha_Documento;
                    this.Hora_Documento = From.Hora_Documento;
                    this.Importe_Total_Documento = From.Importe_Total_Documento;
                    this.nombre = From.nombre;
                    this.rfc = From.rfc;
                    this.ArchivoPDF = From.ArchivoPDF;
                    this.ArchivoXML = From.ArchivoXML;
                    this.IsGroupHeader = From.IsGroupHeader;
                }
                else
                {
                    this.ID = null;
                    this.SucursalCorta = null;
                    this.Tipo_Documento = null;
                    this.Serie = null;
                    this.Folio_Documento = null;
                    this.Fecha_Documento = null;
                    this.Hora_Documento = null;
                    this.Importe_Total_Documento = null;
                    this.nombre = null;
                    this.rfc = null;
                    this.ArchivoPDF = null;
                    this.ArchivoXML = null;
                    this.IsGroupHeader = false;
                }
            }

            public override string ToString()
            {
                return String.Format(
                    "{0}{1} Fecha:{2} Importe:{3} Suc.:{4}",
                    this.Serie,
                    this.Folio_Documento,
                    Convert.ToDateTime(this.Fecha_Documento).ToString("MM/dd/yyyy"),
                    Convert.ToDecimal(this.Importe_Total_Documento).ToString("C"),
                    (string)this.SucursalCorta
                );
            }

            public string Value
            {
                get
                {
                    return String.Format(
                    "{0}|{1}|{2}|{3}|{4}|{5}|{6}",
                    this.ID,
                    this.rfc,
                    this.SucursalCorta,
                    this.Tipo_Documento,
                    this.Serie,
                    this.Folio_Documento,
                    this.Importe_Total_Documento
                );
                }
            }

            public string Text
            {
                get
                {
                    return String.Format(
                        "{0}{1} Fecha:{2} Importe:{3} Suc.:{4}",
                        this.Serie,
                        this.Folio_Documento,
                        Convert.ToDateTime(this.Fecha_Documento).ToString("MM/dd/yyyy"),
                        Convert.ToDecimal(this.Importe_Total_Documento).ToString("C"),
                        this.SucursalCorta
                    );
                }
            }
        }


        public static byte[] ConvertirFileToByteArray(string ruta)
        {

            FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            /*Create a byte array of file stream length*/
            byte[] b = new byte[fs.Length];
            /*Read block of bytes from stream into the byte array*/
            fs.Read(b, 0, System.Convert.ToInt32(fs.Length));
            /*Close the File Stream*/
            fs.Close();

            return b;
        }

        public void OnClientFileUploaded()
        {
            btnText_Click(null, EventArgs.Empty);
        }

        protected void btnText_Click(object sender, EventArgs e)
        {
            //if (RadAsyncUpload1.UploadedFiles.Count > 0)
            string patharchivo = "";
            try
            {

                Label9.Text = "";
                Label3.Text = "";
                Label7.Text = "";
                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + NombreArchivo;
                Label9.Text = "";

                NombreArchivo = null;
                Nombreextension = null;
                patharchivo = path;

                foreach (UploadedFile f in RadAsyncUpload1.UploadedFiles)
                {
                    NombreArchivo = f.GetName();
                    Nombreextension = f.GetExtension();

                    patharchivo = path + NombreArchivo;

                    Label7.Text = patharchivo;
                    Label9.Text = RadAsyncUpload1.UploadedFiles[0].FileName;
                    Label3.Text = RadAsyncUpload1.UploadedFiles[0].ContentType;
                    if (File.Exists(patharchivo))
                    {
                        File.Delete(patharchivo);
                    }
                    f.SaveAs(patharchivo, true);
                    PagElec_Soporte4 = ConvertirFileToByteArray(patharchivo);

                    RadAsyncUpload1.Visible = false;
                    btnQuitar.Visible = true;
                    Label9.Visible = true;

                    //al cargar el archivo automaticamente agregaba renglón pero ahora no , porque provocaba confusipon
                    ////object objeto = new object();
                    ////System.EventArgs evento = new EventArgs();
                    ////BtnAgregar_Click(objeto, evento);

                }


                try
                {
                    // File.Delete(patharchivo);
                }
                catch
                {
                }
            }
            catch (Exception ex)
            {

                //   this.DisplayMensajeAlerta(ex.Message);
            }


        }

        //JFCV que se oculte el seleccionar y quede solo un texto y el boton de quitar

        protected void btnQuitar_Click(object sender, EventArgs e)
        {

            try
            {


                Label9.Text = "";
                Label3.Text = "";
                Label7.Text = "";

                RadAsyncUpload1.Visible = true;
                btnQuitar.Visible = false;
                Label9.Visible = false;

                NombreArchivo = null;
                Nombreextension = null;


            }
            catch (Exception ex)
            {

                //   this.DisplayMensajeAlerta(ex.Message);
            }


        }

        #region Eventos del Grid
        //JFCV 21oct2015 inicio de grid que guarda en memoria 
        DataTable dtValues;

        protected void RadGrid1_InsertCommand(object sender, GridCommandEventArgs e)
        {
            GridEditFormInsertItem insertItem = (GridEditFormInsertItem)e.Item;
            TextBox txt1 = (TextBox)insertItem["Id_GVComprobante"].Controls[0];
            TextBox txt2 = (TextBox)insertItem["GVComprobante_ConComprobanteDescripcion"].Controls[0];


            TextBox txt3 = (TextBox)insertItem["GVComprobante_Importe"].Controls[0];
            TextBox txt4 = (TextBox)insertItem["GVComprobante_Fecha"].Controls[0];
            TextBox txt5 = (TextBox)insertItem["GVComprobante_Observaciones"].Controls[0];
            TextBox txt6 = (TextBox)insertItem["GVComprobante_Xml"].Controls[0];
            TextBox txt7 = (TextBox)insertItem["GVComprobante_Pdf"].Controls[0];


            dtValues = (DataTable)Session["Table"];
            DataRow drValues = dtValues.NewRow();
            drValues["Id_GVComprobante"] = txt1.Text;
            drValues["GVComprobante_ConComprobanteDescripcion"] = txt2.Text;
            drValues["GVComprobante_Importe"] = txt3.Text;
            drValues["GVComprobante_Fecha"] = txt4.Text;
            drValues["GVComprobante_Observaciones"] = txt5.Text;
            drValues["GVComprobante_Xml"] = txt6.Text;
            drValues["GVComprobante_Pdf"] = txt7.Text;

            dtValues.Rows.Add(drValues);//adding new row into datatable
            dtValues.AcceptChanges();
            Session["Table"] = dtValues;
            rgPagoElectronico.Rebind();

        }
        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //creating datatable
            dtValues = new DataTable();
            dtValues.Columns.Add("Id_GVComprobante");
            dtValues.Columns.Add("GVComprobante_ConComprobanteDescripcion");
            dtValues.Columns.Add("GVComprobante_Importe");
            dtValues.Columns.Add("GVComprobante_Fecha");
            dtValues.Columns.Add("GVComprobante_Observaciones");
            //dtValues.Columns.Add("GVComprobante_Xml");
            DataColumn column2 = new DataColumn("GVComprobante_Xml"); //Create the column.
            column2.DataType = System.Type.GetType("System.Byte[]"); //Type byte[] to store image bytes.
            column2.AllowDBNull = true;
            column2.Caption = "GVComprobante_Xml";

            dtValues.Columns.Add(column2); //Add the column to the table.

            DataColumn column = new DataColumn("GVComprobante_Pdf"); //Create the column.
            column.DataType = System.Type.GetType("System.Byte[]"); //Type byte[] to store image bytes.
            column.AllowDBNull = true;
            column.Caption = "GVComprobante_Pdf";

            dtValues.Columns.Add(column); //Add the column to the table.


            //dtValues.Columns.Add("GVComprobante_Pdf");
            dtValues.Columns.Add("PagElec_Cuenta");
            dtValues.Columns.Add("PagElec_Cc");
            dtValues.Columns.Add("PagElec_Numero");
            dtValues.Columns.Add("PagElec_SubCuenta");
            dtValues.Columns.Add("PagElec_SubSubCuenta");
            dtValues.Columns.Add("PagElec_CuentaPago");
            dtValues.Columns.Add("PagElec_Folio");
            dtValues.Columns.Add("PagElec_Serie");
            dtValues.Columns.Add("PagElec_Rfc");
            dtValues.Columns.Add("PagElec_Soporte_Tipo");
            dtValues.Columns.Add("PagElec_Soporte_Nombre");
            dtValues.Columns.Add("PagElec_Id_PagElecCuenta");
            //02 feb 2016 jfcv
            dtValues.Columns.Add("PagElec_UUID");
            dtValues.Columns.Add("PagElec_Subtotal");
            dtValues.Columns.Add("PagElec_Iva");
            dtValues.Columns.Add("PagElec_ImpRetenido");
            dtValues.Columns.Add("PagElec_IvaRetenido");




            if (Session["Table"] != null)
            {
                dtValues = (DataTable)Session["Table"];
            }
            rgPagoElectronico.DataSource = dtValues;//populate RadGrid with datatable
            Session["Table"] = dtValues;
            //23dic2015 para controlar los registros que el usuario elimino voy a crear una copia de los registros originales.

        }
        
        protected void RadGrid1_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GastoViajeComprobante comprobante = new GastoViajeComprobante();


               

                comprobante.Id_GVComprobante = Convert.ToInt32((insertedItem["Id_GVComprobante"].FindControl("lblId_GVComprobante") as Label).Text);

                RadTextBox txtBox = e.Item.FindControl("txtGVComprobante_Observaciones") as RadTextBox;
                comprobante.GVComprobante_Observaciones = txtBox.Text;

                RadComboBox cmbcuentadegastos = e.Item.FindControl("txtcmbCtaGastos") as RadComboBox;

                //RadComboBox cmbcuentadegastos = (insertedItem["Frc_Tipo"].FindControl("cmbTipo") as RadComboBox);
                comprobante.GVComprobante_GV_PagElec_Id_PagElecCuenta = cmbcuentadegastos.SelectedValue;

                PagoElectronicoCuenta CtaGastos = new PagoElectronicoCuenta()
                {
                    Id_Emp = sesion.Id_Emp,
                    Id_Cd = sesion.Id_Cd_Ver,
                    Id_PagElecCuenta = Convert.ToInt32(comprobante.GVComprobante_GV_PagElec_Id_PagElecCuenta )
                };


                (new CN_CatPagoElectronicoCuenta()).ConsultaCuenta(
                    CtaGastos,
                    sesion.Emp_Cnx
                );

                comprobante.GVComprobante_GV_Numero = CtaGastos.PagElecCuenta_Numero;
                comprobante.GVComprobante_GV_Cc = CtaGastos.PagElecCuenta_CC;
                comprobante.GVComprobante_GV_Cuenta = CtaGastos.PagElecCuenta_CC;
                comprobante.GVComprobante_GV_Numero = CtaGastos.PagElecCuenta_Numero;
                comprobante.GVComprobante_GV_CuentaPago = CtaGastos.PagElecCuenta_CuentaPago;
                comprobante.GVComprobante_GV_SubCuenta = CtaGastos.PagElecCuenta_SubCuenta;
                comprobante.GVComprobante_GV_SubSubCuenta = CtaGastos.PagElecCuenta_SubSubCuenta;
           

                dtValues = (DataTable)Session["Table"];
                foreach (DataRow row in dtValues.Rows)
                {


                    if (Convert.ToInt32(row["Id_GVComprobante"].ToString()) == comprobante.Id_GVComprobante)
                    {
                        row["GVComprobante_Observaciones"] = comprobante.GVComprobante_Observaciones;

                        row["PagElec_Numero"] = comprobante.GVComprobante_GV_Numero;
                        row["PagElec_Cc"] = comprobante.GVComprobante_GV_Cc;
                        row["PagElec_Cuenta"] = comprobante.GVComprobante_GV_Cuenta;
                        row["PagElec_CuentaPago"] = comprobante.GVComprobante_GV_CuentaPago;
                        row["PagElec_SubCuenta"] = comprobante.GVComprobante_GV_SubCuenta;
                        row["PagElec_SubSubCuenta"] = comprobante.GVComprobante_GV_SubSubCuenta;
                        row["PagElec_Id_PagElecCuenta"] = comprobante.GVComprobante_GV_PagElec_Id_PagElecCuenta;
                         

                    }
                }

                dtValues.AcceptChanges();

                rgPagoElectronico.DataSource = dtValues;
               Session["Table"] = dtValues;
               rgPagoElectronico.Rebind();

                //DataRow[] drr = dtValues.Select("Id_GVComprobante='" + id_GVComprobante + "'");
                //for (int i = 0; i < drr.Length; i++)
                //{
                //    if (txtTotalAPagar.Text != "")
                //        totalapagar = Convert.ToDecimal(txtTotalAPagar.Text);

                //    totalapagar = totalapagar - Convert.ToDecimal(drr[i]["GVComprobante_Importe"].ToString());
                //    drr[i].Delete();
                //    txtTotalAPagar.Text = totalapagar.ToString();
                //    TxtImporte.Text = "";
                //}
 
              


                


 

                //facturaRevisionCobroDet.Id_Emp = sesion.Id_Emp;
                //facturaRevisionCobroDet.Id_Cd = sesion.Id_Cd_Ver;
                //facturaRevisionCobroDet.Id_Frc = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                //facturaRevisionCobroDet.Id_FrcDet = 0;
                //facturaRevisionCobroDet.Id_Reg = null;

                
                //facturaRevisionCobroDet.Frc_Doc =
                //    Convert.ToInt32((insertedItem["Frc_Doc"].FindControl("txtFrc_Doc") as RadNumericTextBox).Text);
                //facturaRevisionCobroDet.Frc_Fecha =
                //    Convert.ToDateTime((insertedItem["Frc_Fecha"].FindControl("txtFrc_Fecha") as RadDatePicker).SelectedDate);
                //facturaRevisionCobroDet.Id_Cte =
                //    Convert.ToInt32((insertedItem["Id_Cte"].FindControl("lblId_CteEdit") as Label).Text);
                //facturaRevisionCobroDet.Cte_NomComercial = (insertedItem["Id_CteStr"].FindControl("lblId_CteStrEdit") as Label).Text;
                //facturaRevisionCobroDet.Frc_Importe =
                //    Convert.ToDouble((insertedItem["Frc_Importe"].FindControl("txtFrc_ImporteEdit") as RadNumericTextBox).Text);
                //facturaRevisionCobroDet.Frc_EnviarA =
                //    Convert.ToInt32((insertedItem["Frc_EnviarA"].FindControl("cmbFrc_EnviarA") as RadComboBox).SelectedValue);
                //facturaRevisionCobroDet.Frc_EnviarAStr = (insertedItem["Frc_EnviarA"].FindControl("cmbFrc_EnviarA") as RadComboBox).SelectedItem.Text;
                //string doc_old = (insertedItem["Frc_Doc"].FindControl("lblVal_txtFrc_Doc") as Label).Text;



                //facturaRevisionCobroDet.Frc_ReqRecibo = Convert.ToInt32((insertedItem["Frc_ReqRecibo"].FindControl("Frc_ReqRecibo") as RadTextBox).Text);

                //if (facturaRevisionCobroDet.Frc_ReqRecibo == 1)
                //    facturaRevisionCobroDet.Frc_MailEnviado = 1;
                //else
                //    facturaRevisionCobroDet.Frc_MailEnviado = 0;

                ////actualizar producto de nota de cargo a la lista
                //this.ListaComprobante_Modificar(gastoViajeComprobante, doc_old);



                

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RadGrid1_DeleteCommand(int id_GVComprobante)
        {
            decimal totalapagar = 0;

            dtValues = (DataTable)Session["Table"];
            // dtValues.Rows[0].Delete();
            DataRow[] drr = dtValues.Select("Id_GVComprobante='" + id_GVComprobante + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                if (txtTotalAPagar.Text != "")
                    totalapagar = Convert.ToDecimal(txtTotalAPagar.Text);

                totalapagar = totalapagar - Convert.ToDecimal(drr[i]["GVComprobante_Importe"].ToString());
                drr[i].Delete();
                txtTotalAPagar.Text = totalapagar.ToString();
                TxtImporte.Text = "";
            }

            dtValues.AcceptChanges();


            Session["Table"] = dtValues;
            rgPagoElectronico.Rebind();
        }
        //JFCV 04-NOV-2016 
        protected void RadGrid1_EditCommand(int id_GVComprobante)
        {
            //decimal totalapagar = 0;

            dtValues = (DataTable)Session["Table"];
            
            //Sesion Sesion = new Sesion();
            //Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            // dtValues.Rows[0].Delete();
            //DataRow[] drr = dtValues.Select("Id_GVComprobante='" + id_GVComprobante + "'");
            //for (int i = 0; i < drr.Length; i++)
            //{
                //if (txtTotalAPagar.Text != "")
                //    totalapagar = Convert.ToDecimal(txtTotalAPagar.Text);

                //totalapagar = totalapagar - Convert.ToDecimal(drr[i]["GVComprobante_Importe"].ToString());
                ////drr[i].Delete();
                //txtTotalAPagar.Text = totalapagar.ToString();
                //TxtImporte.Text = "";


                //this.cmbCtaGastos.SelectedValue = drr[i]["PagElec_Id_PagElecCuenta"].ToString(); //drr[i]["Id_PagElecCuenta"].ToString();
                ///////this.cmbCtaGastos.Text = drr[i]["pagElecCuenta_Descripcion"].ToString();

                //CargarCtaGastos(Convert.ToInt32(drr[i]["PagElec_Id_PagElecCuenta"].ToString()));

                
                //this.cmbProveedor.SelectedValue = gastoViaje.Id_AcrCheque.ToString();


                //this.cmbProveedor.Text = gastoViaje.AcrCheque_Nombre.ToString();

                //Acreedor provee = new Acreedor() { Id_Emp = Sesion.Id_Emp, Id_Cd = Sesion.Id_Cd_Ver, Id_Acr = gastoViaje.Id_AcrCheque };
                //new CN_CatAcreedor().ConsultaAcreedor(provee, Sesion.Emp_Cnx);

                //txtProveedor.Text = provee.Acr_NumeroGenerado == null ? "Sin Autorizacion" : provee.Acr_NumeroGenerado.ToString().Trim().ToUpper();

                //TxtSolicitante.Text = gastoViaje.PagElec_Solicitante;
                //txtTotalAPagar.Text = gastoViaje.PagElec_Importe.ToString();

                //TxtCuenta.Text = gastoViaje.PagElec_Cuenta;
                //TxtCc.Text = gastoViaje.PagElec_Cc;
                //TxtNumero.Text = gastoViaje.PagElec_Numero;
                //TxtSubCuenta.Text = gastoViaje.PagElec_SubCuenta;
                //TxtSubSubCuenta.Text = gastoViaje.PagElec_SubSubCuenta;
                //TxtCuentaPago.Text = gastoViaje.PagElec_CuentaPago;
                //TxtFechaRequiere.SelectedDate = Convert.ToDateTime(gastoViaje.PagElec_FechaRequiere);
                //TxtObservaciones.Text = gastoViaje.PagElec_Observaciones;


                //this.cmbProveedor.SelectedValue = drr[i]["Id_AcrCheque"].ToString();


                //this.cmbProveedor.Text = drr[i]["AcrCheque_Nombre"].ToString();

                //Acreedor provee = new Acreedor() { Id_Emp = Sesion.Id_Emp, Id_Cd = Sesion.Id_Cd_Ver, Id_Acr = Convert.ToInt32(drr[i]["Id_AcrCheque"]) };
                //new CN_CatAcreedor().ConsultaAcreedor(provee, Sesion.Emp_Cnx);

                //txtProveedor.Text = provee.Acr_NumeroGenerado == null ? "Sin Autorizacion" : provee.Acr_NumeroGenerado.ToString().Trim().ToUpper();

                //TxtSolicitante.Text = drr[i]["PagElec_Solicitante"].ToString();
                //TxtCuenta.Text = drr[i]["PagElec_Cuenta"].ToString();
                //TxtCc.Text = drr[i]["PagElec_Cc"].ToString();
                //TxtNumero.Text = drr[i]["PagElec_Numero"].ToString();
                //TxtSubCuenta.Text = drr[i]["PagElec_SubCuenta"].ToString();
                //TxtSubSubCuenta.Text = drr[i]["PagElec_SubSubCuenta"].ToString();
                //TxtCuentaPago.Text = drr[i]["PagElec_CuentaPago"].ToString();
                //TxtFechaRequiere.SelectedDate = Convert.ToDateTime(drr[i]["PagElec_FechaRequiere"].ToString());
                //TxtObservaciones.Text = drr[i]["GVComprobante_Observaciones"].ToString();

                //TxtImporte.Text = drr[i]["GVComprobante_Importe"].ToString();
                //TxtCuenta.Text = drr[i]["PagElec_Numero"].ToString();
                //TxtCc.Text = drr[i]["PagElec_Cc"].ToString();
                //TxtCuentaPago.Text = drr[i]["PagElec_CuentaPago"].ToString();
                //TxtNumero.Text = drr[i]["PagElec_Numero"].ToString();
                ////PagElec_Cuenta HeaderText="Número" UniqueName="PagElec_Cuenta"
                //TxtSubCuenta.Text = drr[i]["PagElec_SubCuenta"].ToString();
                //TxtSubSubCuenta.Text = drr[i]["PagElec_SubSubCuenta"].ToString();

                //si es de tipo sin comprobante 
                //poner aqui el archivo que se carga etc.
     //comprobante.Id_GVComprobanteTipo = 1;
     
                //comprobante.GVComprobante_GV_PagElec_Id_PagElecCuenta = cmbCtaGastos.SelectedValue.Trim();
                 
                //File.Delete(Label7.Text);
            
                //                    <telerik:GridBoundColumn DataField="GVComprobante_Xml" HeaderText="GVComprobante_Xml" UniqueName="GVComprobante_Xml" Visible="false"></telerik:GridBoundColumn>

//<telerik:GridBoundColumn DataField="GVComprobante_Ruta" HeaderText="GVComprobante_Ruta" UniqueName="GVComprobante_Ruta" Visible="false"></telerik:GridBoundColumn>

//DataField="Id_GVComprobante" HeaderText="Id" 
                // DataField="PagElec_Rfc" HeaderText="PagElec_Rfc" 

                //DataField="PagElec_Serie"  
                //DataField="PagElec_Folio" HeaderText="Folio"  

                //DataField="PagElec_UUID" HeaderText="PagElec_UUID" UniqueName="PagElec_UUID" Visible="false"></telerik:GridBoundColumn>
                //DataField="PagElec_Subtotal" HeaderText="PagElec_Subtotal" UniqueName="PagElec_Subtotal" Visible="false"></telerik:GridBoundColumn>
                //DataField="PagElec_Iva" HeaderText="PagElec_Iva" UniqueName="PagElec_Iva" Visible="false"></telerik:GridBoundColumn>

//DateTime? dfecharequiere ;
//dfecharequiere = Convert.ToDateTime(drr[i]["GVComprobante_Fecha"].ToString());
//TxtFechaRequiere.SelectedDate= Convert.ToDateTime(dfecharequiere) ;
//TxtFechaRequiere.SelectedDate = Convert.ToDateTime(.PagElec_FechaRequiere);


                //TODO jfcv cargar si es con comprobante el grid de facturas el de la derecha 
            //    if (Convert.ToString(drr[i]["GVComprobante_ConComprobanteDescripcion"].ToString()) == "Sin Comprobante")
            //    {
            //        ChkConComprobante.Checked = false;
                    

            //        this.CmbTipoComprobanteSin.SelectedIndex = this.CmbTipoComprobanteSin.FindItemIndexByValue("1");
            //        this.CmbTipoComprobanteSin.Text = "Sin Comprobante";
            //        this.CmbTipoComprobanteSin.SelectedValue = "2";

            //        //Use RadComboBoxItem.Selected
            //        RadComboBoxItem item = CmbTipoComprobanteSin.FindItemByText("Sin Comprobante");
            //        item.Selected = true;

            //        //Use RadComboBox.SelectedIndex
            //        int index = CmbTipoComprobanteSin.FindItemIndexByValue("2");
            //        CmbTipoComprobanteSin.SelectedIndex = index;

            //        ////You can also use the SelectedValue property.
            //        //RadComboBox1.SelectedValue = value;


            //        // this.cmbProveedor.SelectedValue = provee.Id_Acr.ToString();
            //        // this.cmbProveedor.Text = provee.Acr_Nombre.ToString();


            //        if (Convert.ToString(drr[i]["GVComprobante_Pdf"].ToString()) != "")
            //        {
            //            //tienesoporte = true;
            //            PagElec_Soporte4 = drr[i]["GVComprobante_Pdf"] == System.DBNull.Value ? null : (byte[])(drr[i]["GVComprobante_Pdf"]);
                                 
            //            Label7.Text  = drr[i]["PagElec_Soporte_Nombre"] == System.DBNull.Value ? null : (string)(drr[i]["PagElec_Soporte_Nombre"]);
            //            Label9.Text= Label7.Text;
            //            Label3.Text = drr[i]["PagElec_Soporte_Tipo"] == System.DBNull.Value ? null : (string)(drr[i]["PagElec_Soporte_Tipo"]);
            //            //pagoElectronico.PagElec_SoporteImporte = Convert.ToDecimal(drr[i]["GVComprobante_Importe"].ToString());
            //            RadAsyncUpload1.Visible = false;
            //            btnQuitar.Visible = true;
            //            Label9.Visible = true;

            //        }
                
            //    }
                
            //else
            //    {
            //        CmbTipoComprobanteSin.SelectedIndex = 1;
            //        this.CmbTipoComprobanteSin.SelectedValue = "1";
            //        ChkConComprobante.Checked = true;
            //        Label7.Text = "";
            //        Label3.Text = "";
            //        Label9.Text = "";
            //        RadAsyncUpload1.Visible = true;
            //        btnQuitar.Visible = false;
            //        Label9.Visible = false;

            //    }

 //                   if (gastoViaje.PagElec_Soporte != null)
                   
                //decimal totaleapagar = 0;
                //decimal totalpartida = 0;

                //if (txtTotalAPagar.Text != "")
                //    totaleapagar = Convert.ToDecimal(txtTotalAPagar.Text);
                //if (TxtImporte.Text != "")
                //    totalpartida = Convert.ToDecimal(TxtImporte.Text);
                //totaleapagar = totaleapagar + totalpartida;
                //txtTotalAPagar.Text = Convert.ToString(totaleapagar);
                //////RadGrid1_DeleteCommand(id_GVComprobante);
            //}

            //dtValues.AcceptChanges();


            //Session["Table"] = dtValues;
            //rgPagoElectronico.Rebind();
        }

       public void inicializartabla()
        {
            dtValues = new DataTable();
            dtValues.Columns.Add("Id_GVComprobante");
            dtValues.Columns.Add("GVComprobante_ConComprobanteDescripcion");
            dtValues.Columns.Add("GVComprobante_Importe");
            dtValues.Columns.Add("GVComprobante_Fecha");
            dtValues.Columns.Add("GVComprobante_Observaciones");
            //dtValues.Columns.Add("GVComprobante_Xml");
            DataColumn column2 = new DataColumn("GVComprobante_Xml"); //Create the column.
            column2.DataType = System.Type.GetType("System.Byte[]"); //Type byte[] to store image bytes.
            column2.AllowDBNull = true;
            column2.Caption = "GVComprobante_Xml";

            dtValues.Columns.Add(column2); //Add the column to the table.

            DataColumn column = new DataColumn("GVComprobante_Pdf"); //Create the column.
            column.DataType = System.Type.GetType("System.Byte[]"); //Type byte[] to store image bytes.
            column.AllowDBNull = true;
            column.Caption = "GVComprobante_Pdf";

            dtValues.Columns.Add(column); //Add the column to the table.


            //dtValues.Columns.Add("GVComprobante_Pdf");
            dtValues.Columns.Add("PagElec_Cuenta");
            dtValues.Columns.Add("PagElec_Cc");
            dtValues.Columns.Add("PagElec_Numero");
            dtValues.Columns.Add("PagElec_SubCuenta");
            dtValues.Columns.Add("PagElec_SubSubCuenta");
            dtValues.Columns.Add("PagElec_CuentaPago");
            dtValues.Columns.Add("PagElec_Folio");
            dtValues.Columns.Add("PagElec_Serie");
            dtValues.Columns.Add("PagElec_Rfc");
            dtValues.Columns.Add("PagElec_Soporte_Tipo");
            dtValues.Columns.Add("PagElec_Soporte_Nombre");
            dtValues.Columns.Add("PagElec_Id_PagElecCuenta");

            //02 feb 2016 jfcv
            dtValues.Columns.Add("PagElec_UUID");
            dtValues.Columns.Add("PagElec_Subtotal");
            dtValues.Columns.Add("PagElec_Iva");
            dtValues.Columns.Add("PagElec_ImpRetenido");
            dtValues.Columns.Add("PagElec_IvaRetenido");



            if (Session["Table"] != null)
            {
                dtValues = (DataTable)Session["Table"];
            }
            rgPagoElectronico.DataSource = dtValues;//populate RadGrid with datatable
            Session["Table"] = dtValues;

            //23 dic 2015 mantener los valores originales del grid 
            if (Session["TableDel"] != null)
            {
                dtValues = (DataTable)Session["TableDel"];
            }
            Session["TableDel"] = dtValues;


        }

        public static string antes(string s)
        {
            int l = s.IndexOf("<cfdi:Addenda>");
            if (l > 0)
            {
                return s.Substring(0, l);
            }
            // si no encuentra adenda regreso el valor de s completo
            return s;

        }

        public static string despues(string s)
        {
            int l = s.IndexOf("</cfdi:Addenda>");
            if (l > 0)
            {
                //le sumo 15 para que  tome de donde termina "</cfdi:Addenda>"
                return s.Substring(l + 15, s.Length - l - 15);
            }
            //si no tiene adenda regreso espacio vacio para que al juntar el antes y despues no se duplique 
            return "";

        }

        //JFCV 1 abr 2016 
        public static string corregirDescripcion(string s)
        {
            int seguir = 0;
            int inicial = -1;
            do
            {
                int l = s.IndexOf("descripcion=", inicial + 1);
                if (l > 0)
                {
                    inicial = l;
                    int lv = s.IndexOf("valorUnitario=", inicial + 1);
                    if (lv > 0)
                    {
                        string sdescripcion = "";
                        sdescripcion = s.Substring(l + 13, (lv - 2 - (l + 13)));
                        //string corregido = sdescripcion.Replace("\"", "");
                        //corregido = corregido.Replace("&", "");
                        string corregido = sdescripcion.Replace("&", "&amp;");
                        corregido = corregido.Replace("\"", "&quot;");

                        s = s.Substring(0, l + 13) + corregido + s.Substring(lv - 2, s.Length - lv + 2);
                    }
                }
                else
                {
                    seguir = 1;
                }


            } while (seguir == 0);


            // si no encuentra adenda regreso el valor de s completo
            return s;

        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkConComprobante.Checked == false)
                {
                    if (TxtImporte.Text == "")
                    {
                        Alerta("Por favor teclee el importe.");
                        return;
                    }
                }

                if ((cmbCtaGastos.SelectedValue != "") && (TxtFechaRequiere.SelectedDate != null))
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    wsBuzonWeb.wsBuzonWeb wsBuzon = new wsBuzonWeb.wsBuzonWeb();

                    List<string[]> param = new List<string[]>();
                    foreach (RadListBoxItem item in RadListBoxDestination.Items)
                    {
                        char[] delim = { '|' };
                        string[] Tmp = item.Value.Split(delim);
                        param.Add(Tmp);
                    }

                    List<GastoViajeComprobante> list;
                    if (Session["Lista"] != null)
                    {
                        list = (List<GastoViajeComprobante>)Session["Lista"];
                    }
                    else
                    {
                        list = new List<GastoViajeComprobante>();
                    }

                    GastoViajeComprobante comprobante = new GastoViajeComprobante();

                    //JFCV primero vamos aver si es con comprobante o sin comprobante 


                    comprobante.Id_Emp = Sesion.Id_Emp;
                    comprobante.Id_Cd = Sesion.Id_Cd_Ver;
                    comprobante.Id_GV = Int32.Parse(Request.QueryString["Id"] == null ? "-1" : Request.QueryString["Id"]);
                    comprobante.GVComprobante_Fecha = TxtFechaRequiere.SelectedDate;
                    comprobante.GVComprobante_ConComprobante = true;
                    comprobante.Id_GVComprobanteTipo = 1;
                    comprobante.GVComprobante_Observaciones = TxtObservaciones.Text;

                    comprobante.GVComprobante_GV_Cuenta = TxtCuenta.Text.Trim();
                    comprobante.GVComprobante_GV_Cc = TxtCc.Text.Trim();
                    comprobante.GVComprobante_GV_CuentaPago = TxtCuentaPago.Text.Trim();

                    comprobante.GVComprobante_GV_Numero = TxtNumero.Text.Trim();
                    comprobante.GVComprobante_GV_SubCuenta = TxtSubCuenta.Text.Trim();
                    comprobante.GVComprobante_GV_SubSubCuenta = TxtSubSubCuenta.Text.Trim();



                    //////if (cmbFactura.Visible)
                    //////{
                    int partidarow = 0;

                    dtValues = (DataTable)Session["Table"];
                    foreach (DataRow row in dtValues.Rows)
                    {
                        if (Convert.ToInt32(row["Id_GVComprobante"].ToString()) > partidarow)
                        {
                            partidarow = Convert.ToInt32(row["Id_GVComprobante"].ToString());
                        }
                    }

                    partidarow += 1;

                    foreach (string[] item in param)
                    {
                        wsBuzonWeb.InvoiceContainer[] factura = wsBuzon.GetFactura(BuzonKey, Sesion.Id_Cd_Ver, item[1], item[4], item[5], Emp_RFC);

                        DataTable DT = ObtenerDatos(factura[0].XMLFile.Length > 0 ? factura[0].XMLFile : null);
                        if (DT == null)
                        {
                            //si marca error , quitar caracteres raros y addenda para que se suba el archivo y no falle
                            var archivoxml = System.Text.Encoding.ASCII.GetString(factura[0].XMLFile);

                            string xmlantes = antes(archivoxml);
                            string xmldespues = despues(archivoxml);
                            archivoxml = xmlantes + xmldespues;

                            //JFCV 1 abr 2016 marcaba error porque tenia " en descripcion 
                            archivoxml = corregirDescripcion(archivoxml);

                            archivoxml = SanitizeXmlString(archivoxml);
                            archivoxml = archivoxml.Replace("'", Convert.ToString(Convert.ToChar(34)));

                            // Este es si eligen que se grabe la addenda en el xml por eso esta comentarizado si no hay que quitarlo
                            //byte[] facturaxml = Encoding.ASCII.GetBytes(archivoxml);
                            //DT = ObtenerDatos(facturaxml.Length > 0 ? facturaxml : null);

                            factura[0].XMLFile = Encoding.ASCII.GetBytes(archivoxml);

                            DT = ObtenerDatos(factura[0].XMLFile.Length > 0 ? factura[0].XMLFile : null);
                            if (DT == null)
                            {
                                Alerta("Error en el comprobante de la factura. " + item[4] + item[5]);
                                return;
                            }
                        }

                        // Valido si la factura ya existe en el grid y si es asi no la agrego
                        // para validar comparo folio serie e importe 
                        // insertar si o no 
                        bool facturaexiste = false;

                        foreach (DataRow row in dtValues.Rows)
                        {
                            if (Convert.ToString(row["PagElec_Serie"].ToString()) == (string)DT.Rows[0]["serie"])
                            {
                                if (Convert.ToString(row["PagElec_Folio"].ToString()) == (string)DT.Rows[0]["folio"])
                                {
                                    if (Convert.ToDecimal(row["GVComprobante_Importe"].ToString()) == Convert.ToDecimal(DT.Rows[0]["importe"]))
                                    {
                                        facturaexiste = true;
                                    }
                                }
                            }
                        }

                        if (facturaexiste == false)
                        {
                            comprobante.GVComprobante_Serie = (string)DT.Rows[0]["serie"];
                            comprobante.GVComprobante_Folio = (string)DT.Rows[0]["folio"];
                            comprobante.GVComprobante_Importe = Convert.ToDecimal(DT.Rows[0]["importe"]);

                            comprobante.GVComprobante_Pdf = (byte[])(factura[0].PDFFile.Length > 0 ? factura[0].PDFFile : null);
                            comprobante.GVComprobante_Xml = (byte[])(factura[0].XMLFile.Length > 0 ? factura[0].XMLFile : null);
                            comprobante.GVComprobante_Serie = (string)DT.Rows[0]["serie"];
                            comprobante.GVComprobante_Folio = (string)DT.Rows[0]["folio"];
                            comprobante.GVComprobante_Importe = Convert.ToDecimal(DT.Rows[0]["importe"]);
                            comprobante.GVComprobante_GV_Cuenta = TxtCuenta.Text.Trim();
                            comprobante.GVComprobante_GV_Cc = TxtCc.Text.Trim();
                            comprobante.GVComprobante_GV_CuentaPago = TxtCuentaPago.Text.Trim();

                            comprobante.GVComprobante_GV_Numero = TxtNumero.Text.Trim();
                            comprobante.GVComprobante_GV_SubCuenta = TxtSubCuenta.Text.Trim();
                            comprobante.GVComprobante_GV_SubSubCuenta = TxtSubSubCuenta.Text.Trim();

                            comprobante.GVComprobante_XmlStream = factura[0].XMLFile.Length > 0 ? (new System.IO.MemoryStream(factura[0].XMLFile)).ToArray() : null;

                            list.Add(comprobante);


                            DataRow drValues = dtValues.NewRow();
                            drValues["Id_GVComprobante"] = partidarow;
                            drValues["GVComprobante_ConComprobanteDescripcion"] = "Con Comprobante";
                            drValues["GVComprobante_Importe"] = comprobante.GVComprobante_Importe;
                            drValues["GVComprobante_Fecha"] = comprobante.GVComprobante_Fecha;
                            drValues["GVComprobante_Observaciones"] = comprobante.GVComprobante_Observaciones;
                            drValues["GVComprobante_Xml"] = comprobante.GVComprobante_Xml;
                            drValues["GVComprobante_Pdf"] = comprobante.GVComprobante_Pdf;

                            drValues["PagElec_Cuenta"] = comprobante.GVComprobante_GV_Cuenta;
                            drValues["PagElec_Cc"] = comprobante.GVComprobante_GV_Cc;
                            drValues["PagElec_CuentaPago"] = comprobante.GVComprobante_GV_CuentaPago;
                            drValues["PagElec_Numero"] = comprobante.GVComprobante_GV_Numero;
                            drValues["PagElec_SubCuenta"] = comprobante.GVComprobante_GV_SubCuenta;
                            drValues["PagElec_SubSubCuenta"] = comprobante.GVComprobante_GV_SubSubCuenta;

                            //esto lo guardo solo para luego poder validar si el comprobante ya esta en el grid
                            drValues["PagElec_Serie"] = comprobante.GVComprobante_Serie;
                            drValues["PagElec_Folio"] = comprobante.GVComprobante_Folio;

                            //JFCV RFC del emisor
                            drValues["PagElec_Rfc"] = factura[0].AcredorRFC;
                            drValues["PagElec_Soporte_Tipo"] = null;
                            drValues["PagElec_Soporte_Nombre"] = null;
                            drValues["PagElec_Id_PagElecCuenta"] = cmbCtaGastos.SelectedValue.Trim();

                            //02 feb 2016 jfcv
                            drValues["PagElec_UUID"] = (string)DT.Rows[0]["uuid"];
                            drValues["PagElec_Subtotal"] = (string)DT.Rows[0]["subtotal"];
                            drValues["PagElec_Iva"] = (string)DT.Rows[0]["iva"];
                            drValues["PagElec_ImpRetenido"] = (string)DT.Rows[0]["impuestoretenido"];
                            drValues["PagElec_IvaRetenido"] = (string)DT.Rows[0]["ivaretenido"];



                            dtValues.Rows.Add(drValues);//adding new row into datatable
                            dtValues.AcceptChanges();
                            decimal totaleapagar = 0;
                            decimal totalpartida = 0;

                            if (txtTotalAPagar.Text != "")
                                totaleapagar = Convert.ToDecimal(txtTotalAPagar.Text);
                            if (TxtImporte.Text != "")
                                totalpartida = Convert.ToDecimal(TxtImporte.Text);
                            totaleapagar = totaleapagar + totalpartida;
                            txtTotalAPagar.Text = Convert.ToString(totaleapagar);

                            partidarow += 1;
                        }

                    }
                    Session["Table"] = dtValues;


                    //JFCV 23 oct Si tengo un archivo de soporte lo agrego  al grid
                    if (Label7.Text != "")
                    {

                        bool facturaexiste = false;

                        foreach (DataRow row in dtValues.Rows)
                        {
                            if (Convert.ToString(row["GVComprobante_ConComprobanteDescripcion"].ToString()) == "Sin Comprobante")
                            {

                                facturaexiste = true;
                                Alerta("Ya tiene cargado un archivo de soporte, y solo puede haber uno por solicitud.");
                            }
                        }

                        if (facturaexiste == false)
                        {
                            comprobante.GVComprobante_Serie = "";
                            comprobante.GVComprobante_Folio = "";
                            comprobante.GVComprobante_Importe = Convert.ToDecimal(TxtImporte.Text);
                            comprobante.GVComprobante_GV_Cuenta = TxtCuenta.Text.Trim();
                            comprobante.GVComprobante_GV_Cc = TxtCc.Text.Trim();
                            comprobante.GVComprobante_GV_CuentaPago = TxtCuentaPago.Text.Trim();

                            comprobante.GVComprobante_GV_Numero = TxtNumero.Text.Trim();
                            comprobante.GVComprobante_GV_SubCuenta = TxtSubCuenta.Text.Trim();
                            comprobante.GVComprobante_GV_SubSubCuenta = TxtSubSubCuenta.Text.Trim();
                            comprobante.GVComprobante_XmlStream = null;
                            comprobante.GVComprobante_Xml = null;
                            comprobante.GVComprobante_Pdf = null;

                            PagElec_Soporte4 = ConvertirFileToByteArray(Label7.Text);
                            comprobante.GVComprobante_Pdf = PagElec_Soporte4;

                            //JFCV RFC del emisor
                            comprobante.GVComprobante_GV_PagElec_Rfc = null;
                            comprobante.GVComprobante_GV_PagElec_Soporte_Nombre = Label9.Text;
                            comprobante.GVComprobante_GV_PagElec_Soporte_Tipo = Label3.Text;
                            comprobante.GVComprobante_GV_PagElec_Id_PagElecCuenta = cmbCtaGastos.SelectedValue.Trim();


                            File.Delete(Label7.Text);

                            //JFCV 23 oct agrego el archivo de soporte al grid
                            DataRow drValues = dtValues.NewRow();
                            drValues["Id_GVComprobante"] = partidarow;
                            drValues["GVComprobante_ConComprobanteDescripcion"] = "Sin Comprobante";
                            drValues["GVComprobante_Importe"] = comprobante.GVComprobante_Importe;
                            drValues["GVComprobante_Fecha"] = comprobante.GVComprobante_Fecha;
                            drValues["GVComprobante_Observaciones"] = comprobante.GVComprobante_Observaciones;
                            drValues["GVComprobante_Xml"] = comprobante.GVComprobante_Xml;
                            drValues["GVComprobante_Pdf"] = comprobante.GVComprobante_Pdf;

                            drValues["PagElec_Cuenta"] = comprobante.GVComprobante_GV_Cuenta;
                            drValues["PagElec_Cc"] = comprobante.GVComprobante_GV_Cc;
                            drValues["PagElec_CuentaPago"] = comprobante.GVComprobante_GV_CuentaPago;
                            drValues["PagElec_Numero"] = comprobante.GVComprobante_GV_Numero;
                            drValues["PagElec_SubCuenta"] = comprobante.GVComprobante_GV_SubCuenta;
                            drValues["PagElec_SubSubCuenta"] = comprobante.GVComprobante_GV_SubSubCuenta;

                            //esto lo guardo solo para luego poder validar si el comprobante ya esta en el grid
                            drValues["PagElec_Serie"] = comprobante.GVComprobante_Serie;
                            drValues["PagElec_Folio"] = comprobante.GVComprobante_Folio;
                            drValues["PagElec_Id_PagElecCuenta"] = comprobante.GVComprobante_GV_PagElec_Id_PagElecCuenta;


                            dtValues.Rows.Add(drValues);//adding new row into datatable
                            dtValues.AcceptChanges();
                            partidarow += 1;
                            decimal totaleapagar = 0;
                            decimal totalpartida = 0;

                            if (txtTotalAPagar.Text != "")
                                totaleapagar = Convert.ToDecimal(txtTotalAPagar.Text);
                            if (TxtImporte.Text != "")
                                totalpartida = Convert.ToDecimal(TxtImporte.Text);
                            totaleapagar = totaleapagar + totalpartida;
                            txtTotalAPagar.Text = Convert.ToString(totaleapagar);

                            //txtTotalAPagar.Text = Convert.ToString(Convert.ToDecimal(txtTotalAPagar.Text) + Convert.ToDecimal(TxtImporte.Text));

                            Session["Table"] = dtValues;
                        }
                        Label7.Text = "";
                        Label3.Text = "";
                        Label9.Text = "";
                        RadAsyncUpload1.Visible = true;
                        btnQuitar.Visible = false;
                        Label9.Visible = false;
                    }


                    //JFCV comprobante.GVComprobante_Pdf = RadUpload1.UploadedFiles.Count > 0 ? ReadFully(RadUpload1.UploadedFiles[0].InputStream) : null;
                    //CN_CapGastoViajeComprobante clsGastoViajeComprobante = new CN_CapGastoViajeComprobante();
                    //clsGastoViajeComprobante.InsertarGastoViajeComprobante(comprobante, Sesion.Emp_Cnx, ref verificador);
                    //////}

                    //JFCV inicio que limpie el combo y la seleccion de facturas 

                    RadListBoxDestination.Items.Clear();
                    cmbFactura.Items.Clear();
                    CmbAcreedor.Items.Clear();
                    CmbAcreedor.Text = "";
                    TxtImporte.Text = "";
                    CargarAcreedores();

                    //JFCV fin que limpie el combo y la seleccion de facturas 
                    rgPagoElectronico.Rebind();


                }
                else
                {
                    Alerta("Capture los datos del comprobante.");
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "cmbCtaGastos_SelectedIndexChanged");
            }
        }

        protected void rgPagoElectronico_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                Int32 item = default(Int32);
                item = e.Item.ItemIndex;
          
                switch (e.CommandName.ToString())
                {
                    case "XML":
                           
                            
                            byte[] xmlFile = null;
                            //int id_GVComprobante = Convert.ToInt32(rgPagoElectronico.Items[item]["Id_GVComprobante"].Text);
                           Label txtBox = e.Item.FindControl("lblId_GVComprobante") as Label;
                           int id_GVComprobante = Convert.ToInt32(txtBox.Text);

                            DataTable dtFcaturas = (DataTable)Session["Table"];
                            foreach (DataRow row in dtFcaturas.Rows)
                            {
                                if (Convert.ToInt32(row["Id_GVComprobante"].ToString()) == id_GVComprobante)
                                {
                                      xmlFile = (row["GVComprobante_Xml"] == System.DBNull.Value ? null : (byte[])(row["GVComprobante_Xml"]));
                                }
                            }

                        descargarXML(id_GVComprobante, xmlFile);
                        break;
                    case "PDF":
                                 
                                item = e.Item.ItemIndex;
                                byte[] xmlPdf = null;
                                 //int id_GVComprobantepdf = Convert.ToInt32(rgPagoElectronico.Items[item]["Id_GVComprobante"].Text);
  
                                 Label lblComprobantepdf = e.Item.FindControl("lblId_GVComprobante") as Label;
                                 int id_GVComprobantepdf = Convert.ToInt32(lblComprobantepdf.Text);


                                DataTable dtFcaturaspdf = (DataTable)Session["Table"];
                                foreach (DataRow row in dtFcaturaspdf.Rows)
                                {
                                    if (Convert.ToInt32(row["Id_GVComprobante"].ToString()) == id_GVComprobantepdf)
                                    {
                                        xmlPdf = (row["GVComprobante_Pdf"] == System.DBNull.Value ? null : (byte[])(row["GVComprobante_Pdf"]));
                                         
                                    }
                                }
                        descargarPDF(id_GVComprobantepdf, xmlPdf);
                        break;
                    case "Delete":
                        //if (this.rgPagoElectronico.SelectedValue.Count > 0)
                          //id_GVComprobante = Convert.ToInt32(rgPagoElectronico.Items[item]["Id_GVComprobante"].Text);
                         Label lblComprobantedel = e.Item.FindControl("lblId_GVComprobante") as Label;
                                 int id_GVComprobantedel = Convert.ToInt32(lblComprobantedel.Text);

                        RadGrid1_DeleteCommand(id_GVComprobantedel);
                        //Borrar(id_GVComprobante);
                        break;
                    case "Edit":
                        //if (this.rgPagoElectronico.SelectedValue.Count > 0)
                        //id_GVComprobante = Convert.ToInt32(rgPagoElectronico.Items[item]["Id_GVComprobante"].Text);
                        //RadGrid1_EditCommand(id_GVComprobante);
                        //Borrar(id_GVComprobante);
                        break;
                    case "Modificar":
                        //if (this.rgPagoElectronico.SelectedValue.Count > 0)
                        ////id_GVComprobante = Convert.ToInt32(rgPagoElectronico.Items[item]["Id_GVComprobante"].Text);
                        ////RadGrid1_EditCommand(id_GVComprobante);
                        //Borrar(id_GVComprobante);
                        break;

                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        // JFCV FIN 
        #endregion  Eventos Grid

        protected void CmbTipoComprobanteSin_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs args)
        {
            cmbFactura.Visible = Convert.ToBoolean(args.Value);
            RadListBoxDestination.Visible = Convert.ToBoolean(args.Value);
            PnlSoporte.Visible = !Convert.ToBoolean(args.Value);
            RadAsyncUpload1.Visible = true;
            ChkConComprobante.Checked = Convert.ToBoolean(args.Value);
            TxtImporte.Enabled = !Convert.ToBoolean(args.Value);

            txtTotalAPagar.Visible = true;
            lblTotalPagar.Visible = true;
            BtnAgregar.Visible = true;

            if (CmbTipo.SelectedValue == "2")
            {
                PnlSoporte.Visible = true;
                BtnAgregar.Visible = true;
                rgPagoElectronico.Visible = true;
                txtTotalAPagar.Visible = true;
                lblTotalPagar.Visible = true;
                ChkConComprobante.Checked = Convert.ToBoolean(args.Value);
                CmbAcreedor.Enabled = ChkConComprobante.Checked;

            }
            else
            {
                //JFCV 
                CmbAcreedor.Enabled = true;
            }

            if (CmbTipo.SelectedValue == "1")
            {
                rgPagoElectronico.Visible = true;
            }

            Label3.Text = "";
            Label7.Text = "";
            Label9.Text = "";


            if (cmbFactura.Visible)
            {
                cmbProveedor_SelectedIndexChanged(
                    cmbProveedor,
                    new RadComboBoxSelectedIndexChangedEventArgs(
                        cmbProveedor.Text,
                        null,
                        cmbProveedor.SelectedValue,
                        null
                    )
                );
            }
            else
            {
                RadListBoxDestination.Items.Clear();
            }
        }

        private void habilitarcontroles()
        {
            //JFCV 15 oct si es tipo de cuenta de gastos debo grabar en gastos de viaje por eso activo HF_AnticipoPorComprobar a true y si no false
            // y también oculto el check box de comprobantes 
            HF_AnticipoPorComprobar.Value = "False";
            //JFCV 9mzo2016 Solo se mostrará este icono cuando sea de tipo cuenta de gastos
            ChkConComprobante.Visible = false;
            LblConComprobante.Visible = false;

            PnlGastosViaje.Visible = false;
            pnlEncabezadoGastosViaje.Visible = false;

            PnlSolicitudCheque.Visible = true;

            cmbFactura.Visible = ChkConComprobante.Checked;
            RadListBoxDestination.Visible = ChkConComprobante.Checked;
            rgPagoElectronico.Visible = true;

            //JFCV que en los tres tipos de mov pueda agregar archivos de soporte 
            PnlSoporte.Visible = !ChkConComprobante.Checked;
            BtnAgregar.Visible = true;
            txtTotalAPagar.Visible = true;
            lblTotalPagar.Visible = true;
            LblFechaRequiere.Visible = true;
            LblSolicitante.Visible = true;
            TxtSolicitante.Visible = true;
            TxtFechaRequiere.Visible = true;
            if (CmbTipo.SelectedValue == "3")
            {
                pnlEncabezadoGastosViaje.Visible = true;

                HF_AnticipoPorComprobar.Value = "True";
                ChkConComprobante.Visible = true;
                LblConComprobante.Visible = true;
                LblSolicitante.Visible = false;
                TxtSolicitante.Visible = false;
                TxtFechaRequiere.Visible = false;
                LblFechaRequiere.Visible = false;

                if (ChkConComprobante.Checked)
                {
                    PnlGastosViaje.Visible = false;
                    PnlSolicitudCheque.Visible = true;
                }
                else
                {
                    rgPagoElectronico.Visible = false;
                    PnlSolicitudCheque.Visible = false;
                    PnlGastosViaje.Visible = true;
                    txtTotalAPagar.Visible = false;
                    lblTotalPagar.Visible = false;
                }
            }
            CargarCtaGastos();
        }
    }
}