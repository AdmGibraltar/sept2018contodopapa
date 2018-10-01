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
using Telerik.Web.UI.Calendar;
using System.Net.Mail;
using System.Net;

namespace SIANWEB
{
    public partial class CapGastosViaje : System.Web.UI.Page
    {
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

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
                    Inicializar();
                    ValidarPermisos();
                    CargarCentros();
                    CargarAcreedores();
                    CargarConceptos();
                    //CargarTipos();
                    Session["Lista"] = new List<GastoViaje>();
                }
            }
        }

        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
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
                rgPagoElectronico.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }

        protected void CmbTipo_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //if (CmbTipo.SelectedValue == "3")
            //{
            //    PnlCaptura2.Visible = true;
            //    PnlCaptura1.Visible = false;
            //}
            //else
            //{
            //    PnlCaptura1.Visible = true;
            //    PnlCaptura2.Visible = false;
            //}

            CargarConceptos();
        }

        //protected void CmbConcepto_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    try
        //    {
        //        Sesion Sesion = new Sesion();
        //        Sesion = (Sesion)Session["Sesion" + Session.SessionID];

        //        PagoElectronicoConcepto concepto = new PagoElectronicoConcepto() { Id_Cd = Sesion.Id_Cd_Ver, Id_Emp = Sesion.Id_Emp, Id_PagElecConcepto = Int32.Parse(CmbConcepto.SelectedValue) };

        //        //TxtCuenta.Text = "X";
        //        //TxtCc.Text = "X";
        //        //TxtNumero.Text = "X";

        //        CN_CatPagoElectronicoConcepto clsConcepto = new CN_CatPagoElectronicoConcepto();
        //        clsConcepto.ConsultaConcepto(concepto, Sesion.Emp_Cnx);

        //        //TxtCuenta.Text = concepto.PagElecCuenta_Descripcion;
        //        //TxtCc.Text = concepto.PagElecCuenta_CC;
        //        //TxtNumero.Text = concepto.PagElecCuenta_Numero;
        //        //TxtCuentaPago.Text = concepto.PagElecCuenta_CuentaPago;
        //        //TxtSubCuenta.Text = concepto.PagElecCuenta_SubCuenta;
        //        //TxtSubSubCuenta.Text = concepto.PagElecCuenta_SubSubCuenta;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager(ex, "CmbConcepto_SelectedIndexChanged");
        //    }
        //}

        protected void BtnObtenerImporte_Click(object sender, EventArgs e)
        {

//            TxtImporte.Text = ObtenerImporte(@"C:\Users\inftmp\Downloads\F0000005537.xml").ToString();
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

                int Id_PagElec = Convert.ToInt32(rgPagoElectronico.Items[item]["Id_PagElec"].Text);

                switch (e.CommandName.ToString())
                {
                    case "XML":
                        //descargarXML(Id_PagElec);
                        break;
                    case "PDF":
                        //descargarPDF(Id_PagElec);
                        break;

                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            ////List<GastoViajeComprobante> list = (List<GastoViajeComprobante>)Session["Lista"];
            //List<GastoViaje> list = (List<GastoViaje>)Session["Lista"];

            ////GastoViajeComprobante gasto = new GastoViajeComprobante();
            //GastoViaje gasto = new GastoViaje();

            //gasto.Comprobante = CmbTipoComprobante.SelectedItem.Text;
            //gasto.Tipo = CmbConcepto.SelectedItem.Text;
            //gasto.Importe = Decimal.Parse(TxtImporte.Text);
            //gasto.Observaciones = TxtObservaciones.Text;

            //list.Add(gasto);

            //Session["Lista"] = list;

            //rgPagoElectronico.Rebind();
        }

        protected void TxtFechaSalida_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (TxtFechaRegreso.SelectedDate != null)
            {
                TimeSpan ts = TxtFechaRegreso.SelectedDate.Value - TxtFechaSalida.SelectedDate.Value;
                //TxtCantidadDias.Text = TxtFechaRegreso.SelectedDate.Value.CompareTo(TxtFechaSalida.SelectedDate.Value).ToString();
                TxtCantidadDias.Text = ts.TotalDays.ToString();
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
            rgPagoElectronico.Rebind();
        }

        private void Inicializar()
        {
            rgPagoElectronico.Rebind();
        }

        protected void CargarAcreedores()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            //CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatAcreedor_Combo", ref CmbAcreedor);
        }

        protected void CargarTipos()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            //CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatPagoElectronicoTipo_Combo", ref CmbTipo);
        }

        protected void CargarConceptos()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            //int Id_PagElecTipo = Int32.Parse(CmbTipo.SelectedValue);

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            //CN_Comun.LlenaCombo(1, 3, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatPagoElectronicoConcepto_Combo", ref CmbConcepto);
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
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                //TxtSolicitante.Text = Sesion.U_Nombre;

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
                        this.rtb1.Items[6].Visible = false;
                    }
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
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void CalcularCuota(int dias)
        {
            int hospedate = 1000;
            int desayuno = 75;
            int comida = 125;
            int cena = 125;

            if (dias > 0)
            {
                TxtHospedajeImporte.Text = string.Format("{0:N2}", (dias - 1) * hospedate);
                TxtDesayunosImporte.Text = string.Format("{0:N2}", dias * desayuno);
                TxtCenasImporte.Text = string.Format("{0:N2}", dias * cena);
                TxtComidasImporte.Text = string.Format("{0:N2}", dias * comida);

                TxtHospedajeCutoa.Text = string.Format("{0:N2}", hospedate);
                TxtDesayunosCutoa.Text = string.Format("{0:N2}", desayuno);
                TxtCenasCutoa.Text = string.Format("{0:N2}", cena);
                TxtComidasCutoa.Text = string.Format("{0:N2}", comida);

                TxtHospedajeDias.Text = string.Format("{0}", (dias - 1));
                TxtDesayunosDias.Text = dias.ToString();
                TxtCenasDias.Text = string.Format("{0}", (dias - 1));
                TxtComidasDias.Text = dias.ToString();

                TxtTotal.Text = string.Format("{0:N2}", ((dias - 1) * hospedate) + (dias * desayuno) + (dias * cena) + (dias * comida));
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

        private void Guardar()
        {
            //Sesion Sesion = new Sesion();
            //Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            //PagoElectronico pagoElectronico = new PagoElectronico();

            //pagoElectronico.Id_Emp = Sesion.Id_Emp;
            //pagoElectronico.Id_Cd = Sesion.Id_Cd_Ver;
            //pagoElectronico.Id_PagElec = Int32.Parse(MaximoId());
            //pagoElectronico.Id_PagElecTipo = Int32.Parse(CmbTipo.SelectedValue);
            //pagoElectronico.Id_PagElecConcepto = Int32.Parse(CmbConcepto.SelectedValue);
            //pagoElectronico.Id_Acr = Int32.Parse(CmbAcreedor.SelectedValue);

            //pagoElectronico.PagElec_Solicitante = TxtSolicitante.Text.Trim();
            //pagoElectronico.PagElec_FechaRequiere = TxtFechaRequiere.SelectedDate;
            //pagoElectronico.PagElec_Cuenta = TxtCuenta.Text.Trim();
            //pagoElectronico.PagElec_Cc = TxtCc.Text.Trim();
            //pagoElectronico.PagElec_Numero = TxtNumero.Text.Trim();
            //pagoElectronico.PagElec_SubCuenta = TxtSubCuenta.Text.Trim();
            //pagoElectronico.PagElec_SubSubCuenta = TxtSubSubCuenta.Text.Trim();
            //pagoElectronico.PagElec_CuentaPago = TxtCuentaPago.Text.Trim();
            //pagoElectronico.PagElec_Importe = decimal.Parse(TxtImporte.Text.Trim());
            //pagoElectronico.PagElec_Observaciones = TxtObservaciones.Text.Trim();
            //pagoElectronico.PagElec_Xml = new System.Data.SqlTypes.SqlXml((Stream)(Session["xml"]));
            //pagoElectronico.PagElec_Pdf = RadUpload2.UploadedFiles.Count > 0 ? ReadFully(RadUpload2.UploadedFiles[0].InputStream) : null;
            //pagoElectronico.PagElec_IdU = Sesion.Id_U;
            //pagoElectronico.PagElec_FechaRegistro = DateTime.Now;

            //int verificador = -1;

            //CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
            //clsPagoElectronico.InsertarPagoElectronico(pagoElectronico, Sesion.Emp_Cnx, ref verificador);

            //if (verificador == 1)
            //{
            //    Nuevo();
            //    Alerta("El gasto se registro correctamente.");
            //    rgPagoElectronico.Rebind();
            //}

            EnviarCorreo(new PagoElectronico());
            Alerta("La solicitud de viaje ha sido registrada.");
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
            //CmbTipo.SelectedValue = "0";
            //CmbConcepto.SelectedValue = "0";
            //CmbAcreedor.SelectedValue = "0";

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            TxtSolicitante.Text = Sesion.U_Nombre;
            TxtFechaSalida.SelectedDate = null;
            TxtFechaRegreso.SelectedDate = null;
            TxtCantidadDias.Text = "";
            //TxtCuenta.Text = string.Empty;
            //TxtCc.Text = string.Empty;
            //TxtNumero.Text = string.Empty;
            //TxtImporte.Text = string.Empty;
            //TxtObservaciones.Text = string.Empty;

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

        private decimal ObtenerImporte(string xmlPath)
        {
            string str = null;

            //if (RadUpload1.UploadedFiles.Count > 0)
            //{
            //    XmlDataDocument xmldoc = new XmlDataDocument();
            //    XmlNodeList xmlnode;

            //    Stream fs = RadUpload1.UploadedFiles[0].InputStream;
            //    xmldoc.Load(fs);
            //    Session["xml"] = fs;
            //    xmlnode = xmldoc.GetElementsByTagName("cfdi:Concepto");
            //    str = xmlnode[0].Attributes["importe"].Value;
            //}

            return str != null ? decimal.Parse(str) : 0;
        }

        private List<GastoViaje> GetList()
        {
            try
            {
                CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
                List<PagoElectronico> list = new List<PagoElectronico>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                PagoElectronico pagoElectronico = new PagoElectronico();
                pagoElectronico.Id_Emp = session.Id_Emp;
                pagoElectronico.Id_Cd = session.Id_Cd_Ver;

                //clsPagoElectronico.ConsultaPagoElectronico(pagoElectronico, session.Emp_Cnx, ref list);

                //return list;
                return (List<GastoViaje>)Session["Lista"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void descargarXML(int id_PagElec)
        {
            PagoElectronico pagoElectronico = new PagoElectronico();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            pagoElectronico.Id_Emp = Sesion.Id_Emp;
            pagoElectronico.Id_Cd = Sesion.Id_Cd_Ver;
            pagoElectronico.Id_PagElec = id_PagElec;

            CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
            clsPagoElectronico.ConsultaPagoElectronico(pagoElectronico, Sesion.Emp_Cnx);

            string ruta = null;
            System.IO.StreamWriter sw = null;
            ruta = Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".txt";

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".xml"))
                File.Delete(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".xml");
            sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            sw.WriteLine(pagoElectronico.PagElec_Xml.ToString());
            sw.Close();
            File.Move(ruta, Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".xml");
            RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\archivoXml" + Sesion.Id_U.ToString() + "PagElec", id_PagElec.ToString(), ".xml')"));
        }

        private void descargarPDF(int id_PagElec)
        {
            PagoElectronico pagoElectronico = new PagoElectronico();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            pagoElectronico.Id_Emp = Sesion.Id_Emp;
            pagoElectronico.Id_Cd = Sesion.Id_Cd_Ver;
            pagoElectronico.Id_PagElec = id_PagElec;

            CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
            clsPagoElectronico.ConsultaPagoElectronico(pagoElectronico, Sesion.Emp_Cnx);

            byte[] archivoPdf = pagoElectronico.PagElec_Pdf;

            if (archivoPdf != null)
            {
                if (archivoPdf.Length > 0)
                {
                    string tempPDFname = string.Concat("GASTO_"
                             , Sesion.Id_Emp.ToString()
                             , "_", Sesion.Id_Cd.ToString()
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

        private void EnviarCorreo(PagoElectronico pagoElectronico)
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
            configuracion.Id_Cd = session.Id_Cd_Ver;
            configuracion.Id_Emp = session.Id_Emp;
            CN_Configuracion cn_configuracion = new CN_Configuracion();
            cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);

            StringBuilder cuerpo_correo = new StringBuilder();
            cuerpo_correo.Append("<table>");
            cuerpo_correo.Append("<tr>");
            cuerpo_correo.Append("<td>" + TxtSolicitante.Text + " solicita un anticipo para viaje, por el monto de $" + TxtTotal.Text + "</td>");
            cuerpo_correo.Append("</tr>");
            //cuerpo_correo.Append("<tr>");
            //cuerpo_correo.Append("<td>Concepto : " + pagoElectronico.PagElecConcepto_Descripcion + "</td>");
            //cuerpo_correo.Append("</tr>");
            cuerpo_correo.Append("<tr>");
            cuerpo_correo.Append("<td><br></td>");
            cuerpo_correo.Append("</tr>");
            cuerpo_correo.Append("<tr>");
            cuerpo_correo.Append("<td>Accesar a : <a href=\"http://10.1.0.120/SIANCENTRAL/ProAutorizacion_PagoElectronico.aspx\">Autorización de Gastos de Viaje</a></td>");
            cuerpo_correo.Append("</tr>");
            cuerpo_correo.Append("<tr>");
            cuerpo_correo.Append("<td></td>");
            cuerpo_correo.Append("</tr>");
            cuerpo_correo.Append("</table>");

            SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
            sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
            //sm.EnableSsl = true;
            MailMessage m = new MailMessage();
            m.From = new MailAddress(configuracion.Mail_Remitente);
            //JFCV envia mail al encargado de la comprobación y al gerente de la sucursal 
            m.To.Add(new MailAddress(configuracion.Mail_GastosAvisoGerente));
            m.Subject = "Autorización de Gasto de Viaje";
            m.IsBodyHtml = true;
            m.Body = cuerpo_correo.ToString();
            sm.Send(m);

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
    }
}