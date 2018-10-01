using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Text;
using System.Configuration;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Data;
using System.Web.Services;
using System.Net.Mail;
using System.Net;

namespace SIANWEB
{
    public partial class CapGastosViajeComprobacion : System.Web.UI.Page
    {
        private string CryptoPassIn = "k3y9u1m1c4";
        private string CryptoPassOut = "k3y9u1m1c4";
        //jfcv agregue 3 variables par el archivo de soporte 
        private byte[] PagElec_Soporte4 = null;
        public string NombreArchivo;
        public string Nombreextension;
        public int tipoGasto;  //JFCV 26 ene 2016 tipogasto puede ser  1 gasto de viaje y 2 pago acreedores jfcv 23jun2017 puede ser también 4 anticipo compras y 3 que es gasto ya comprobado por lo tanto no entraria  aqui

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
        //SAUL GUERRA 20150716  BEGIN
        protected string Emp_RFC
        {
            get
            {
                return (string)(Session["Emp_RFC"] != null ? Session["Emp_RFC"] : GetEmp_RFC());
            }
        }
        //SAUL GUERRA 20150716  END

        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (Sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);

                //string str = Context.Items["href"].ToString();
                //string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);Context.Items.Add("href", pag[pag.Length-1]);Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
            }
            else
            {
                if (!IsPostBack)
                {


                    DateTime fechaComprobante = DateTime.Now;
                    TxtFechaComprobante.SelectedDate = fechaComprobante;
                    Inicializar();
                    ValidarPermisos();
                }
                CargarCtaGastos();
                CargarProveedores();
            }
        }

        private string GetEmp_RFC()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            Session["Emp_RFC"] = (new CN_CapPagoElectronico().ConsultaEmpRFC(Sesion.Id_Emp, Sesion.Emp_Cnx));

            return (string)Session["Emp_RFC"];
        }

        /// <summary>
        ///    JFCV carga las cuentas 19 Oct 2015

        protected void cmbCtaGastos_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

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
            catch (Exception ex)
            {
                ErrorManager(ex, "cmbCtaGastos_SelectedIndexChanged");
            }
        }


        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void CmbTipoComprobante_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs args)
        {
            pnlFacturas.Visible = Convert.ToBoolean(args.Value);
            pnlSoporte.Visible = !Convert.ToBoolean(args.Value);

            if (pnlFacturas.Visible)
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

        protected void cmbProveedor_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs args)
        {
            if (args.Value.Trim() != "")
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];

                Acreedor provee = new Acreedor() { Id_Emp = session.Id_Emp, Id_Cd = session.Id_Cd_Ver, Id_Acr = Convert.ToInt32(args.Value.Trim()) };
                new CN_CatAcreedor().ConsultaAcreedor(provee, session.Emp_Cnx);

                string[] lstAcreedor = { provee.Acr_RFC };

                try
                {
                    DateTime fechaRequiere = DateTime.Now;
                    TxtFechaRequiere.SelectedDate = fechaRequiere.AddDays(provee.Acr_CondPago);

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
        }
        protected void txtRFC_TextChanged(object sender, EventArgs e)
        {
            if (!(System.Text.RegularExpressions.Regex.IsMatch(
                ((TextBox)sender).Text.Trim(),
                @"^([a-zñA-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([a-zA-Z\d]{3})?$"
            )))
            {
                Alerta("RFC Invalido");
                return;
            }

            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            TextBox txtText = (TextBox)sender;
            List<string> lstAcredores = new List<string>();
            lstAcredores.Add(txtText.Text.Replace("-", "").Replace(" ", ""));

            //SAUL GUERRA 20150623  BEGIN
            wsBuzonWeb.wsBuzonWeb wsBuzon = new wsBuzonWeb.wsBuzonWeb();
            wsBuzonWeb.InvoiceList[] ListaFacturaTmp = wsBuzon.GetListFactura(
                BuzonKey,
                lstAcredores.ToArray(),
                session.Id_Cd,
                Emp_RFC
            );

            List<InvoiceList> ListaFactura = new List<InvoiceList>();
            foreach (wsBuzonWeb.InvoiceList Item in ListaFacturaTmp)
            {
                ListaFactura.Add(new InvoiceList(Item));
            }

            //cmbFactura.DataSource = dtable;
            cmbFactura.DataSource = ListaFactura;
            cmbFactura.DataValueField = "Value";
            cmbFactura.DataTextField = "Text";
            cmbFactura.DataBind();

            ViewState["dtFactura"] = ListaFactura;
            ViewState["RFCAcredor"] = txtText.Text.Replace("-", "").Replace(" ", "");
        }
        //SAUL GUERRA 20150623  END


        //protected void RadAutoCompleteBox1_TextChanged(object sender, object e)
        //{
        //    List<string> RFCList;

        //    try
        //    {
        //        wsBuzonWeb.wsBuzonWeb wsBuzon = new wsBuzonWeb.wsBuzonWeb();
        //        RFCList = wsBuzon.GetRFCList(acbRFCProvee.Text, Emp_RFC);
        //    }
        //    catch
        //    {
        //        RFCList = new List<string>();
        //    }

        //    acbRFCProvee.DataSource = RFCList;
        //    acbRFCProvee.DataBind();
        //}

        //SAUL GUERRA 20150624  BEGIN
        //protected void CmbFactura_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    using (RadComboBox CB = (RadComboBox)sender)
        //    {
        //        DataTable DT = null;
        //        try
        //        {
        //            DT = (DataTable)ViewState["dtFactura"];
        //        }
        //        catch
        //        {
        //            DT = null;
        //        }

        //        string[] selected = ((string)CB.SelectedValue).Split('|');
        //        string[] param = { selected[3], selected[4], selected[5] };
        //        ViewState["seletedFactura"] = param;
        //    }
        //}

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
                //TxtImporte.Text = SumaImporte.ToString();
                ViewState["seletedFactura"] = Result;
            }
        }

        protected void cmbFactura_ItemDataBound(object sender, Telerik.Web.UI.RadListBoxItemEventArgs e)
        {
            //cbFacturaTemplate cfFac = ((cbFacturaTemplate)(((RadComboBox)sender).ItemTemplate));
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

            //e.Item.DataBind();
            //cfFac.InstantiateIn(e.Item);
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

        private List<string> GetCheckBoxValues(Telerik.Web.UI.RadComboBox comboCheckbox)
        {
            List<string> Result = new List<string>();
            int I = 0;
            foreach (Telerik.Web.UI.RadComboBoxItem rcbItem in comboCheckbox.Items)
            {
                //If the box is checked return a value
                if (Request["ctl00$CPH$" + comboCheckbox.ID + "$i" + (I).ToString() + "$CheckBox1"] != null && Request["ctl00$CPH$" + comboCheckbox.ID + "$i" + (I).ToString() + "$CheckBox1"].ToString().ToUpper() == "ON")
                {
                    Result.Add(rcbItem.Value);
                }
                I++;
            }
            return Result;
        }
        //SAUL GUERRA 20150624  END

        protected void BtnObtenerImporte_Click(object sender, EventArgs e)
        {
            //SAUL GUERRA 20150625  BEGIN
            //TxtImporte.Text = ObtenerImporte(@"C:\Users\inftmp\Downloads\F0000005537.xml").ToString();
            //SAUL GUERRA 20150625  END
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
                        //Nuevo();
                    }
                    else if (btn.CommandName == "undo")
                    {
                        //Regresar()
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }

        protected void rgPagoElectronico_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgPagoElectronico.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPagoElectronico_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Int32 item = default(Int32);
                item = e.Item.ItemIndex;

                int id_GVComprobante = Convert.ToInt32(rgPagoElectronico.Items[item]["Id_GVComprobante"].Text);

                switch (e.CommandName.ToString())
                {
                    case "XML":
                        descargarXML(id_GVComprobante);
                        break;
                    case "PDF":
                        descargarPDF(id_GVComprobante);
                        break;
                    case "Delete":
                        Borrar(id_GVComprobante);
                        break;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
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

            return s;

        }


        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            if ((cmbCtaGastos.SelectedValue != "") && (TxtFechaComprobante.SelectedDate != null))
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                wsBuzonWeb.wsBuzonWeb wsBuzon = new wsBuzonWeb.wsBuzonWeb();
                //SAUL GUERRA 20150707  BEGIN JFCV 16 oct
                List<string[]> param = new List<string[]>();
                foreach (RadListBoxItem item in RadListBoxDestination.Items)
                {
                    char[] delim = { '|' };
                    string[] Tmp = item.Value.Split(delim);
                    param.Add(Tmp);
                }
                decimal sumImporte = 0;
                //SAUL GUERRA 20150707  END

                //SAUL GUERRA 20150623  BEGIN JFCV 
                if (param != null && param.Count > 0)
                {
                    foreach (string[] item in param)
                    {
                        sumImporte += decimal.Parse(item[6].ToString().Trim());
                    }
                }
                else
                {
                    sumImporte = decimal.Parse(TxtImporteComprobado.Text.Trim());
                }
                ////JFCV PagElec_Importe = sumImporte;
                //SAUL GUERRA 20150623  END

                //JFCV string[] param = (string[])ViewState["seletedFactura"];
                //JFCV wsBuzonWeb.InvoiceContainer[] factura = wsBuzon.GetFactura(BuzonKey, Sesion.Id_Cd_Ver, (string)ViewState["RFCAcredor"], param[0], param[1], Emp_RFC);

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

                //JFCV insertar por cada factura un registro en la BD 
                //JFCV primero vamos aver si es con comprobante o son comprobante 

                comprobante.Id_Emp = Sesion.Id_Emp;
                comprobante.Id_Cd = Sesion.Id_Cd_Ver;
                comprobante.Id_GV = Int32.Parse(Request.QueryString["Id"] == null ? "-1" : Request.QueryString["Id"]);
                comprobante.GVComprobante_Fecha = TxtFechaComprobante.SelectedDate;
                comprobante.GVComprobante_ConComprobante = Boolean.Parse(CmbTipoComprobante.SelectedValue);
                comprobante.Id_GVComprobanteTipo = Int32.Parse(cmbCtaGastos.SelectedValue);
                comprobante.GVComprobante_Observaciones = TxtObservaciones.Text;
                int verificador = 0;

                if (cmbFactura.Visible)
                {

                    foreach (string[] item in param)
                    {
                        //JFCV agregar try catch
                        try
                        {
                            wsBuzonWeb.InvoiceContainer[] factura = wsBuzon.GetFactura(BuzonKey, Sesion.Id_Cd_Ver, item[1], item[4], item[5], Emp_RFC);
                            DataTable DT = ObtenerDatos(factura[0].XMLFile.Length > 0 ? factura[0].XMLFile : null);

                            //(int)Sesion.Id_Emp,
                            //            (int)Sesion.Id_Cd_Ver,
                            //           (int)pagoElectronico.Id_PagElec,
                            if (DT == null)
                            {
                                //si marca error , quitar caracteres raros y addenda para que se suba el archivo y no falle
                                var archivoxml = System.Text.Encoding.ASCII.GetString(factura[0].XMLFile);

                                string xmlantes = antes(archivoxml);
                                string xmldespues = despues(archivoxml);
                                archivoxml = xmlantes + xmldespues;
                                archivoxml = corregirDescripcion(archivoxml);

                                archivoxml = SanitizeXmlString(archivoxml);
                                archivoxml = archivoxml.Replace("'", Convert.ToString(Convert.ToChar(34)));
                                //archivoxml = archivoxml.Replace("", "\"");
                                //archivoxml.Replace("\"", "").Replace("'", "").Replace("&", "");
                                //JFCV 01 abril 2016 factura[0].XMLFile.(" Descipcion=\"" + d.Prd_Descripcion.ToString().Replace("\"", "").Replace("'", "").Replace("&", "")

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

                            comprobante.GVComprobante_Pdf = (byte[])(factura[0].PDFFile.Length > 0 ? factura[0].PDFFile : null);
                            comprobante.GVComprobante_Xml = (byte[])(factura[0].XMLFile.Length > 0 ? factura[0].XMLFile : null);
                            ///JFCV TODO la fecha va a ser la que le teclee y no la del comprobante comprobante.GVComprobante_Fecha = (DateTime)DT.Rows[0]["fecha"];
                            comprobante.GVComprobante_Serie = (string)DT.Rows[0]["serie"];
                            comprobante.GVComprobante_Folio = (string)DT.Rows[0]["folio"];
                            comprobante.GVComprobante_Importe = Convert.ToDecimal(DT.Rows[0]["importe"]);
                            comprobante.GVComprobante_GV_Cuenta = TxtCuenta.Text.Trim();
                            comprobante.GVComprobante_GV_Cc = TxtCc.Text.Trim();
                            comprobante.GVComprobante_GV_CuentaPago = TxtCuentaPago.Text.Trim();

                            comprobante.GVComprobante_GV_Numero = TxtNumero.Text.Trim();
                            comprobante.GVComprobante_GV_SubCuenta = TxtSubCuenta.Text.Trim();
                            comprobante.GVComprobante_GV_SubSubCuenta = TxtSubSubCuenta.Text.Trim();

                            //pagoElectronico.pagElecCuenta_Descripcion = cmbCtaGastos.Text.Trim() == "" ? "Sin cuenta asignada" : cmbCtaGastos.Text.Trim();


                            //comprobante.GVComprobante_Xml = new System.Data.SqlTypes.SqlXml((Stream)(new System.IO.MemoryStream(factura[0].XMLFile)));
                            comprobante.GVComprobante_XmlStream = factura[0].XMLFile.Length > 0 ? (new System.IO.MemoryStream(factura[0].XMLFile)).ToArray() : null;

                            list.Add(comprobante);

                        }
                        catch (Exception ex)
                        {
                            ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                        }

                        try
                        {
                            CN_CapGastoViajeComprobante clsGastoViajeComprobante = new CN_CapGastoViajeComprobante();
                            clsGastoViajeComprobante.InsertarGastoViajeComprobante(comprobante, Sesion.Emp_Cnx, ref verificador);
                        }
                        catch (Exception ex)
                        {
                            ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                        }
                    }
                }
                else
                {
                    comprobante.GVComprobante_Xml = null;
                    comprobante.GVComprobante_Pdf = null;
                    comprobante.GVComprobante_Serie = "";
                    comprobante.GVComprobante_Folio = "";
                    comprobante.GVComprobante_Importe = Convert.ToDecimal(TxtImporteSoporte.Text);
                    comprobante.GVComprobante_GV_Cuenta = TxtCuenta.Text.Trim();
                    comprobante.GVComprobante_GV_Cc = TxtCc.Text.Trim();
                    comprobante.GVComprobante_GV_CuentaPago = TxtCuentaPago.Text.Trim();

                    comprobante.GVComprobante_GV_Numero = TxtNumero.Text.Trim();
                    comprobante.GVComprobante_GV_SubCuenta = TxtSubCuenta.Text.Trim();
                    comprobante.GVComprobante_GV_SubSubCuenta = TxtSubSubCuenta.Text.Trim();
                    comprobante.GVComprobante_XmlStream = null;


                    if (Label7.Text != null)
                    {
                        PagElec_Soporte4 = ConvertirFileToByteArray(Label7.Text);
                        comprobante.GVComprobante_Pdf = PagElec_Soporte4;
                        //comprobante.PagElec_Soporte_Nombre = Label9.Text;
                        //comprobante.PagElec_Soporte_Tipo = Label3.Text;
                        File.Delete(Label7.Text);
                    }


                    //JFCV comprobante.GVComprobante_Pdf = RadUpload1.UploadedFiles.Count > 0 ? ReadFully(RadUpload1.UploadedFiles[0].InputStream) : null;
                    CN_CapGastoViajeComprobante clsGastoViajeComprobante = new CN_CapGastoViajeComprobante();
                    clsGastoViajeComprobante.InsertarGastoViajeComprobante(comprobante, Sesion.Emp_Cnx, ref verificador);
                }


                ///JFCV todo aqui va si no es con comprobante 
                ///debo insertar un registro pero en el campo de soporte 
                ///   list.Add(comprobante);
                ///   TODO
                ///   que grabe aqui 
                ///    int verificador = 0;

                ////// CN_CapGastoViajeComprobante clsGastoViajeComprobante = new CN_CapGastoViajeComprobante();
                ////// clsGastoViajeComprobante.InsertarGastoViajeComprobante(comprobante, Sesion.Emp_Cnx, ref verificador);


                //////JFCV 
                //////comprobante.Id_Emp = Sesion.Id_Emp;
                //////comprobante.Id_Cd = Sesion.Id_Cd_Ver;
                //////comprobante.Id_GV = Int32.Parse(Request.QueryString["Id"] == null ? "-1" : Request.QueryString["Id"]);
                //////comprobante.GVComprobante_Fecha = TxtFechaComprobante.SelectedDate;
                //////comprobante.GVComprobante_ConComprobante = Boolean.Parse(CmbTipoComprobante.SelectedValue);
                //////comprobante.Id_GVComprobanteTipo = Int32.Parse(cmbCtaGastos.SelectedValue);

                //////comprobante.GVComprobante_Importe = decimal.Parse(param[2].ToString().Trim());

                //SAUL GUERRA 20150625  BEGIN  
                //comprobante.GVComprobante_Xml = Session["xml"] != null ? new System.Data.SqlTypes.SqlXml((Stream)(Session["xml"])) : (RadUpload1.UploadedFiles.Count > 0 ? new System.Data.SqlTypes.SqlXml(RadUpload1.UploadedFiles[0].InputStream) : null);
                //comprobante.GVComprobante_Pdf = RadUpload2.UploadedFiles.Count > 0 ? ReadFully(RadUpload2.UploadedFiles[0].InputStream) : null;
                /////////JFCV comente estas 3 lineas 
                //////////comprobante.GVComprobante_Xml = new System.Data.SqlTypes.SqlXml((Stream)(new System.IO.MemoryStream(factura[0].XMLFile)));
                //////////comprobante.GVComprobante_XmlStream = factura[0].XMLFile.Length > 0 ? (new System.IO.MemoryStream(factura[0].XMLFile)).ToArray() : null;
                //////////comprobante.GVComprobante_Pdf = factura[0].PDFFile.Length > 0 ? (new System.IO.MemoryStream(factura[0].PDFFile).ToArray()) : null;
                //SAUL GUERRA 20150625  END  


                //int verificador = 0;
                /////   list.Add(comprobante);

                //CN_CapGastoViajeComprobante clsGastoViajeComprobante = new CN_CapGastoViajeComprobante();
                //clsGastoViajeComprobante.InsertarGastoViajeComprobante(comprobante, Sesion.Emp_Cnx, ref verificador);

                rgPagoElectronico.Rebind();
                //SAUL GUERRA 20150628  BEGIN
                ////////wsBuzon.PutAsigFacGastos(
                ////////        BuzonKey, 
                ////////        (string)ViewState["RFCAcredor"], 
                ////////        param[0], 
                ////////        param[1], 
                ////////        Sesion.Id_Emp, 
                ////////        Sesion.Id_Cd, 
                ////////        Sesion.Id_U, 
                ////////        Sesion.U_Nombre, 
                ////////        Sesion.U_Correo, 
                ////////        Emp_RFC
                ////////);
                //txtRFC_TextChanged(tbRFC, null);
                //SAUL GUERRA 20150628  END

                //if (verificador == 1)
                //{
                if (param != null && param.Count > 0)
                {
                    foreach (string[] item in param)
                    {
                        wsBuzon.PutAsigFacGastos(
                            BuzonKey,
                            item[1],
                            item[4],
                            item[5],
                            Sesion.Id_Emp,
                            Sesion.Id_Cd_Ver,
                            Sesion.Id_U,
                            Sesion.U_Nombre,
                            Sesion.U_Correo,
                            Emp_RFC
                        );
                    }
                }
                //}

                //// comentarizado porque si no me saca de la pantalla AlertaCerrar("El Comprobante se registro correctamente.");
                Nuevo();
            }
            else
            {
                Alerta("Capture los datos del comprobante.");
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            rgPagoElectronico.Rebind();
        }

        private void Inicializar()
        {
            cmbFactura.TransferMode = ListBoxTransferMode.Copy;
            cmbFactura.AllowTransferOnDoubleClick = true;
            cmbFactura.ButtonSettings.ShowTransferAll = false;
            cmbFactura.EnableDragAndDrop = true;

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            GastoViaje gastoViaje = new GastoViaje();

            gastoViaje.Id_Emp = Sesion.Id_Emp;
            gastoViaje.Id_Cd = Sesion.Id_Cd_Ver;
            gastoViaje.Id_GV = Int32.Parse(Request.QueryString["Id"] == null ? "-1" : Request.QueryString["Id"]);
            tipoGasto = Int32.Parse(Request.QueryString["ref"] == null ? "-1" : Request.QueryString["ref"]);

            if (tipoGasto == 2)
            {
                LblCantidadDias.Visible = false;
                TxtCantidadDias.Visible = false;
                lblDestino.Visible = false;
                TxtDestino.Visible = false;
            }
            else
            {
                LblCantidadDias.Visible = true;
                TxtCantidadDias.Visible = true;
                lblDestino.Visible = true;
                TxtDestino.Visible = true;
            }
           
            CN_CapGastoViaje clsGastoViaje = new CN_CapGastoViaje();
            clsGastoViaje.ConsultaGastoViaje(gastoViaje, Sesion.Emp_Cnx);

            tipoGasto = gastoViaje.GV_TipoGasto;
            HF_tipoPago.Value = Convert.ToString(tipoGasto);
            TxtSolicitanteViajero.Text = gastoViaje.GV_Solicitante;
            TxtMotivo.Text = gastoViaje.GV_Motivo;
            TxtFechaSalida.SelectedDate = gastoViaje.GV_FechaSalida;
            TxtFechaRegreso.SelectedDate = gastoViaje.GV_FechaRegreso;
            TxtImporteSolicitado.Text = gastoViaje.GV_Importe.ToString();

            TimeSpan ts = TxtFechaRegreso.SelectedDate.Value - TxtFechaSalida.SelectedDate.Value;
            TxtCantidadDias.Text = ts.TotalDays.ToString();
            //JFCV 08 ene 2016
            TxtDestino.Text = gastoViaje.GV_PagElec_Destino;

            if (TxtFechaSalida.SelectedDate == TxtFechaRegreso.SelectedDate)
            {
                LblFechaRequiere.Visible = true;
                TxtFechaRequiere.Visible = true;
                TxtFechaRequiere.SelectedDate = TxtFechaSalida.SelectedDate;

                LblFechaSalida.Visible = false;
                LblFechaRegreso.Visible = false;
                LblCantidadDias.Visible = false;
                TxtFechaSalida.Visible = false;
                TxtFechaRegreso.Visible = false;
                TxtCantidadDias.Visible = false;
            }

            rgPagoElectronico.Rebind();

            //CargarConceptos(gastoViaje.Id_PagElecTipo);
        }

        //protected void CargarConceptos(int? id_PagElecTipo)
        //{
        //    Sesion Sesion = new Sesion();
        //    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

        //    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
        //    CN_Comun.LlenaCombo(1, 3, Sesion.Id_Emp, Sesion.Id_Cd_Ver, id_PagElecTipo, Sesion.Emp_Cnx, "spCatGastoViajeComprobanteTipo_Combo", ref CmbConcepto);
        //}

        protected void CargarCtaGastos()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            List<PagoElectronicoCuenta> CtaGastos = new List<PagoElectronicoCuenta>();

            (new CN_CatPagoElectronicoCuenta()).ConsultaCuenta(
                new PagoElectronicoCuenta() { Id_Emp = Sesion.Id_Emp, Id_Cd = Sesion.Id_Cd_Ver },
                Sesion.Emp_Cnx,
                ref CtaGastos
            );

            cmbCtaGastos.Items.Clear();
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

        protected void CargarProveedores()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            List<Acreedor> lista = new List<Acreedor>();
            using (CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(Sesion.Emp_Cnx))
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

        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

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

                //if (Permiso.PAccesar == true)
                if (true)
                {
                    Permiso.PGrabar = true;
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;


                    _PermisoModificar = true;


                    //if (Permiso.PGrabar == false)
                    //{
                    //la opción de nuevo no debe mostrarse 
                    this.rtb1.Items[6].Visible = false;
                    //}
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
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
                //else
                //{
                //    Response.Redirect("Inicio.aspx");
                //}
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void Guardar()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            GastoViaje gastoViaje = new GastoViaje();

            gastoViaje.Id_Emp = Sesion.Id_Emp;
            gastoViaje.Id_Cd = Sesion.Id_Cd_Ver;
            gastoViaje.Id_GV = Int32.Parse(Request.QueryString["Id"] == null ? "-1" : Request.QueryString["Id"]);
            gastoViaje.UsuarioMod = Sesion.Id_U;

            int verificador = 0;
            //JFCV 8 ene 2016 aqui grabo la información que haya seleccionado en la cuenta de gastos 
            // No envio a macola ya que eso lo hace hasta que autoriza ,  pregunto si desea enviarlo a autorización o no.

            gastoViaje.Id_GVEst = 1;  //le pongo estatus de Por Comprobar
            CN_CapGastoViaje clsGastoViaje = new CN_CapGastoViaje();
            clsGastoViaje.ModificarEstatusGastoViaje(gastoViaje, Sesion.Emp_Cnx, ref verificador);


            if (verificador == 1 && Convert.ToDecimal(TxtSaldoFavor.Text) < 0)
            {
                AlertaCerrar("La cuenta de gastos de viaje ha sido Actualizada, No podrá ser enviada hasta que el saldo por comprobar sea 0 o mayor a 0.");
            }


            if (Convert.ToDecimal(TxtSaldoFavor.Text) >= 0)
            {
                // si tiene saldo por comprobar == 0 o mayor de cero entonces si puede enviar a autorización
                string message = "¿Desea Enviar el gasto a Autorizar?";

                //   RAM1.ResponseScripts.Add(string.Concat(@"ShowPopup('" + message + "')"));
                string importeComprobado = TxtImporteSolicitado.Text;
                string pagElec_Solicitante = TxtSolicitanteViajero.Text;
                string acr_Motivo = TxtMotivo.Text;
                int tipocomprobacion = Int32.Parse(Request.QueryString["ref"] == null ? "-1" : Request.QueryString["ref"]);

                tipocomprobacion = Convert.ToInt32(HF_tipoPago.Value);
                 
                CmbTipoComprobante.SelectedValue = "true";
                pnlFacturas.Visible = true;
                pnlSoporte.Visible = false;
 
                rgPagoElectronico.DataSource = new List<GastoViajeComprobante>();

                TxtObservaciones.Text = string.Empty;
                RadListBoxDestination.Items.Clear();

                cmbFactura.Items.Clear();
                cmbProveedor.SelectedIndex = 0;
                cmbProveedor.Text = "";
                cmbProveedor.ClearSelection();
                TxtImporteSoporte.Text = "";
                Label3.Text = "";
                Label7.Text = "";
                Label9.Text = "";

                //finally _PermisoEliminar datos pantalla
                RAM1.ResponseScripts.Add(string.Concat(@"ShowPopup('" + message + "','" + gastoViaje.Id_Emp.ToString() + "','" + gastoViaje.Id_Cd.ToString() + "','" + gastoViaje.Id_GV.ToString() + "','" + gastoViaje.UsuarioMod + "','" + Sesion.Emp_Cnx + "','" + importeComprobado + "','" + acr_Motivo + "','" + pagElec_Solicitante + "','" + tipocomprobacion.ToString() + "')"));
            }
        }

        [WebMethod]
        public static string ProcesoEnviar(int id_Emp, int id_Cd_Ver, int id_GV, int id_U, string emp_Cnx, string importeComprobado, string acr_Motivo, string pagElec_Solicitante, int tipocomprobacion)
        {
            string result = "La cuenta de gastos de viaje ha sido enviada.";

            GastoViaje gastoViaje = new GastoViaje();

            gastoViaje.Id_Emp = Convert.ToInt32(id_Emp);
            gastoViaje.Id_Cd = Convert.ToInt32(id_Cd_Ver);
            gastoViaje.Id_GV = Convert.ToInt32(id_GV);
            gastoViaje.UsuarioMod = Convert.ToInt32(id_U);

            int verificador = 0;

            CN_CapGastoViaje clsGastoViaje = new CN_CapGastoViaje();
            clsGastoViaje.EnviarGastoViaje(gastoViaje, emp_Cnx, ref verificador);

            if (verificador == 1)
            {
                EnviarCorreo(id_Emp, id_Cd_Ver, id_GV, id_U, emp_Cnx, importeComprobado, acr_Motivo, pagElec_Solicitante, tipocomprobacion);
            }
            return result;
        }

        private static void EnviarCorreo(int id_Emp, int id_Cd_Ver, int id_GV, int id_U, string emp_Cnx, string importeComprobado, string acr_Motivo, string pagElec_Solicitante,int tipocomprobacion)
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
                //if ( tipocomprobacion != 2 ) 
                //    cuerpo_correo.Append("<td>{PagElec_Solicitante} solicita la comprobación del Gasto de Viaje con Motivo {acr_Motivo} por el monto de $" + importeComprobado + "</td>");
                //else
                //     cuerpo_correo.Append("<td>{PagElec_Solicitante} solicita la comprobación del Gasto de Acreedor con Motivo {acr_Motivo} por el monto de $" + importeComprobado + "</td>");

                if (tipocomprobacion == 2)
                {
                    cuerpo_correo.Append("<td>{PagElec_Solicitante} solicita la comprobación del Gasto de Acreedor con Motivo {acr_Motivo} por el monto de $" + importeComprobado + "</td>");
                }
                else if (tipocomprobacion == 4)
                {
                    cuerpo_correo.Append("<td>{PagElec_Solicitante} solicita la comprobación del Gasto de Compras con Motivo {acr_Motivo} por el monto de $" + importeComprobado + "</td>");
                }
                else
                {
                    cuerpo_correo.Append("<td>{PagElec_Solicitante} solicita la comprobación del Gasto de Viaje con Motivo {acr_Motivo} por el monto de $" + importeComprobado + "</td>");
                }

                cuerpo_correo.Append("</tr>");
                //cuerpo_correo.Append("<tr>");
                //cuerpo_correo.Append("<td>Cuenta : {pagElecCuenta_Descripcion}</td>");
                //cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td><br></td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td>Accesar a : <a href='{href}'>Autorización de Gastos de Comprobación</a></td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td>&nbsp;</td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("</table>");

                string strUrl = (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/").Replace("//", "/").Replace("http:/", "http://");
                string txtCuerpoMail = cuerpo_correo.ToString();
                txtCuerpoMail = txtCuerpoMail.Replace("{acr_Motivo}", acr_Motivo);
                txtCuerpoMail = txtCuerpoMail.Replace("{PagElec_Solicitante}", pagElec_Solicitante);
                txtCuerpoMail = txtCuerpoMail.Replace("{href}", strUrl + "ProAutorizacion_GastoViaje.aspx?ref=" + tipocomprobacion);


                SmtpClient smtp = new SmtpClient();
                smtp.Host = configuracion.Mail_Servidor;
                smtp.Port = Convert.ToInt32(configuracion.Mail_Puerto);
                smtp.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);

                MailAddress from = new MailAddress(configuracion.Mail_Remitente);
                MailMessage mail = new MailMessage();

                mail.From = from;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.Subject = "Mail de Autorización de Gastos de Comprobación";


                //JFCV envia mail al encargado de la comprobación y al gerente de la sucursal 
                //mail.To.Add(new MailAddress(configuracion.Mail_GastosComprobacion));

                if (tipocomprobacion == 4)
                {
                    mail.To.Add(new MailAddress(configuracion.Mail_GastosComprobacionCompras));
                    mail.Subject = "Mail de Autorización de Gastos de Comprobación de Compras";
                }
                else 
                {
                    mail.To.Add(new MailAddress(configuracion.Mail_GastosComprobacion));
                }
                

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

        protected void Borrar(int id_GVComprobante)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            GastoViajeComprobante gastoViajeComprobante = new GastoViajeComprobante();

            gastoViajeComprobante.Id_Emp = Sesion.Id_Emp;
            gastoViajeComprobante.Id_Cd = Sesion.Id_Cd_Ver;
            gastoViajeComprobante.Id_GV = Int32.Parse(Request.QueryString["Id"] == null ? "-1" : Request.QueryString["Id"]);
            gastoViajeComprobante.Id_GVComprobante = id_GVComprobante;

            CN_CapGastoViajeComprobante clsGastoViajeComprobante = new CN_CapGastoViajeComprobante();
            clsGastoViajeComprobante.ConsultaGastoViajeComprobante(gastoViajeComprobante, Sesion.Emp_Cnx);

            int verificador = 0;
            clsGastoViajeComprobante.EliminarGastoViajeComprobante(gastoViajeComprobante, Sesion.Emp_Cnx, ref verificador);

            if (verificador == 1)
            {
                try
                {
                    DataTable DT = ObtenerDatos(gastoViajeComprobante.GVComprobante_XmlStream);
                    if (DT != null)
                    {
                        wsBuzonWeb.wsBuzonWeb wsBuzon = new wsBuzonWeb.wsBuzonWeb();
                        wsBuzon.DelAsigFacGastos(
                            BuzonKey,
                            DT.Rows[0]["RFC"].ToString(),
                            DT.Rows[0]["Serie"].ToString(),
                            DT.Rows[0]["Folio"].ToString(),
                            Sesion.Id_Emp,
                            Sesion.Id_Cd,
                            Sesion.Id_U, Emp_RFC
                        );
                    }
                }
                catch
                {
                }

                Alerta("El comprobante se ha eliminado de la cuenta de gastos.");
            }
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
           
            CmbTipoComprobante.SelectedValue = "true";
            pnlFacturas.Visible = true;
            pnlSoporte.Visible = false;

            if (pnlFacturas.Visible)
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

            rgPagoElectronico.DataSource = new List<GastoViajeComprobante>();

            //SAUL GUERRA 20150625  BEGIN  (CODIGO VIEJO)
            //TxtImporte.Text = string.Empty;
            //SAUL GUERRA 20150625  END  (CODIGO VIEJO)
            TxtObservaciones.Text = string.Empty;
            //JFCV 16 oct limpiar las facturas una vez que agrego
            RadListBoxDestination.Items.Clear();

            cmbFactura.Items.Clear();
            cmbProveedor.SelectedIndex = 0;
            cmbProveedor.Text = "";
            cmbProveedor.ClearSelection();




            //cmbFactura
            // JFCV no se quita el proveedor  cmbProveedor.SelectedIndex = 0;
            TxtImporteSoporte.Text = "";
            Label3.Text = "";
            Label7.Text = "";
            Label9.Text = "";


        }

        private decimal ObtenerImporte(string xmlPath)
        {
            string str = null;

            //SAUL GUERRA 20150625  BEGIN  (CODIGO VIEJO)
            //if (RadUpload1.UploadedFiles.Count > 0)
            //{
            //    XmlDataDocument xmldoc = new XmlDataDocument();
            //    XmlNodeList xmlnode;

            //    Stream fs = RadUpload1.UploadedFiles[0].InputStream;
            //    xmldoc.Load(fs);
            //    Session["xml"] = fs;                
            //    xmlnode = xmldoc.GetElementsByTagName("cfdi:Comprobante");
            //    str = xmlnode[0].Attributes["total"].Value;
            //}
            //SAUL GUERRA 20150625  END  (CODIGO VIEJO)

            return str != null ? decimal.Parse(str) : 0;
        }

        //SAUL GUERRA 20150625  BEGIN JFCV agregue la columna de importe
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

        //SAUL GUERRA 20150625  END 

        private List<GastoViajeComprobante> GetList()
        {
            try
            {
                CN_CapGastoViajeComprobante clsGastoViajeComprobante = new CN_CapGastoViajeComprobante();
                List<GastoViajeComprobante> list = new List<GastoViajeComprobante>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                GastoViajeComprobante gastoViajeComprobante = new GastoViajeComprobante();
                gastoViajeComprobante.Id_Emp = session.Id_Emp;
                gastoViajeComprobante.Id_Cd = session.Id_Cd_Ver;
                gastoViajeComprobante.Id_GV = Int32.Parse(Request.QueryString["Id"] == null ? "-1" : Request.QueryString["Id"]);

                clsGastoViajeComprobante.ConsultaGastoViajeComprobante(gastoViajeComprobante, session.Emp_Cnx, ref list);

                //TxtImporteComprobado.Text = totalComprobado < 0 ? string.Format("{0:N2}", totalComprobado * -1) : string.Format("{0:N2}", totalComprobado);
                TxtImporteComprobado.Text = string.Format("{0:N2}", list.Sum(x => x.GVComprobante_Importe));
                decimal diferencia = decimal.Parse(TxtImporteComprobado.Text.Trim()) - decimal.Parse(TxtImporteSolicitado.Text.Trim());

                TxtImporteEnGrid.Text = string.Format("{0:N2}", list.Sum(x => x.GVComprobante_Importe));

                //JFCV 11 ene 2016 el saldo ahora se muestra solo en positivo o negativo , no cambio el textbox del label
                //TxtSaldoFavor.Text = diferencia < 0 ? string.Format("{0:N2}", diferencia * -1) : string.Format("{0:N2}", diferencia);
                //LblSaldoFavor.Text = diferencia < 0 ? "A mi Cargo" : "A Mi Favor";

                LblSaldoFavor.Text = "Saldo";
        
                ///ocmentar color ahorita siempre será el default  TxtSaldoFavor.Style.Add("color ", "Black");

                if (diferencia < 0)
                {
                    TxtSaldoFavor.Text = string.Format("{0:N2}", diferencia);
                    //TxtSaldoFavor.ForeColor = System.Drawing.Color.Blue;
                   ///ocmentar color ahorita siempre será el default  TxtSaldoFavor.Style.Add("color ", "Red");
                }
                else
                {
                    TxtSaldoFavor.Text = string.Format("{0:N2}", diferencia);
                }


                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void descargarXML(int id_GVComprobante)
        {
            GastoViajeComprobante gastoViajeComprobante = new GastoViajeComprobante();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            gastoViajeComprobante.Id_Emp = Sesion.Id_Emp;
            gastoViajeComprobante.Id_Cd = Sesion.Id_Cd_Ver;
            gastoViajeComprobante.Id_GV = Int32.Parse(Request.QueryString["Id"] == null ? "-1" : Request.QueryString["Id"]);
            gastoViajeComprobante.Id_GVComprobante = id_GVComprobante;

            CN_CapGastoViajeComprobante clsGastoViajeComprobante = new CN_CapGastoViajeComprobante();
            clsGastoViajeComprobante.ConsultaGastoViajeComprobante(gastoViajeComprobante, Sesion.Emp_Cnx);

            //string ruta = null;
            //System.IO.StreamWriter sw = null;
            //ruta = Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "GVComprobante" + id_GVComprobante.ToString() + ".txt";

            //if (File.Exists(ruta))
            //    File.Delete(ruta);
            //if (File.Exists(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "GVComprobante" + id_GVComprobante.ToString() + ".xml"))
            //    File.Delete(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "GVComprobante" + id_GVComprobante.ToString() + ".xml");
            //sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            //sw.WriteLine(gastoViajeComprobante.GVComprobante_Xml.ToString());
            //sw.Close();
            //File.Move(ruta, Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "GVComprobante" + id_GVComprobante.ToString() + ".xml");
            //RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\archivoXml" + Sesion.Id_U.ToString() + "GVComprobante", id_GVComprobante.ToString(), ".xml')"));


            string ruta = null;

            ruta = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_GVComprobante.ToString() + ".txt"));

            string strarchivo = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_GVComprobante.ToString()));
            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(strarchivo + ".xml"))
                File.Delete(strarchivo + ".xml");


            using (FileStream fileStream = File.Create(ruta))
            {
                MemoryStream MS = new MemoryStream(gastoViajeComprobante.GVComprobante_XmlStream);
                MS.CopyTo(fileStream);
                fileStream.Close();
            }
            File.Move(ruta, strarchivo + ".xml");
            RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('xmlSAT\\archivoXml" + Sesion.Id_U.ToString() + "PagElec", id_GVComprobante.ToString(), ".xml')"));



        }

        private void descargarPDF(int id_GVComprobante)
        {
            GastoViajeComprobante gastoViajeComprobante = new GastoViajeComprobante();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            gastoViajeComprobante.Id_Emp = Sesion.Id_Emp;
            gastoViajeComprobante.Id_Cd = Sesion.Id_Cd_Ver;
            gastoViajeComprobante.Id_GV = Int32.Parse(Request.QueryString["Id"] == null ? "-1" : Request.QueryString["Id"]);
            gastoViajeComprobante.Id_GVComprobante = id_GVComprobante;

            CN_CapGastoViajeComprobante clsGastoViajeComprobante = new CN_CapGastoViajeComprobante();
            clsGastoViajeComprobante.ConsultaGastoViajeComprobante(gastoViajeComprobante, Sesion.Emp_Cnx);

            byte[] archivoPdf = gastoViajeComprobante.GVComprobante_Pdf;

            if (archivoPdf != null)
            {
                if (archivoPdf.Length > 0)
                {
                    string tempPDFname = string.Concat("GVComprobante_"
                             , Sesion.Id_Emp.ToString()
                             , "_", Sesion.Id_Cd.ToString()
                             , "_", id_GVComprobante.ToString()
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
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CapGastoViajeComprobante", "Id_PagElec", Sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
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

        // JFCV INICIO cambie la forma de obtener el archivo de soporte porque el control fallaba y asi como esta aqui lo hice en 
        // la captura de solicitudes y ahi me funciono bien
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

                foreach (UploadedFile f in RadUpload1.UploadedFiles)
                {
                    NombreArchivo = f.GetName();
                    Nombreextension = f.GetExtension();

                    patharchivo = path + NombreArchivo;

                    Label7.Text = patharchivo;
                    Label9.Text = RadUpload1.UploadedFiles[0].FileName;
                    Label3.Text = RadUpload1.UploadedFiles[0].ContentType;
                    if (File.Exists(patharchivo))
                    {
                        File.Delete(patharchivo);
                    }
                    f.SaveAs(patharchivo, true);
                    PagElec_Soporte4 = ConvertirFileToByteArray(patharchivo);

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

        //JFCV FIN

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
    }
}