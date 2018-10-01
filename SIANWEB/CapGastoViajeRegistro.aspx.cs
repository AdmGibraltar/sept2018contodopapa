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

namespace SIANWEB
{
    public partial class CapGastoViajeRegistro : System.Web.UI.Page
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

                }
            }
        }

        protected void CmbConcepto_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                //PagoElectronicoConcepto concepto = new PagoElectronicoConcepto() { Id_Cd = Sesion.Id_Cd_Ver, Id_Emp = Sesion.Id_Emp, Id_PagElecConcepto = Int32.Parse(CmbConcepto.SelectedValue) };

                //CN_CatPagoElectronicoConcepto clsConcepto = new CN_CatPagoElectronicoConcepto();
                //clsConcepto.ConsultaConcepto(concepto, Sesion.Emp_Cnx);

                //TxtCuenta.Text = concepto.PagElecCuenta_Descripcion;
                //TxtCc.Text = concepto.PagElecCuenta_CC;
                //TxtNumero.Text = concepto.PagElecCuenta_Numero;
                //TxtCuentaPago.Text = concepto.PagElecCuenta_CuentaPago;
                //TxtSubCuenta.Text = concepto.PagElecCuenta_SubCuenta;
                //TxtSubSubCuenta.Text = concepto.PagElecCuenta_SubSubCuenta;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbConcepto_SelectedIndexChanged");
            }
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
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            rgPagoElectronico.Rebind();
        }

        private void Inicializar()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            GastoViaje gastoViaje = new GastoViaje();

            gastoViaje.Id_Emp = Sesion.Id_Emp;
            gastoViaje.Id_Cd = Sesion.Id_Cd_Ver;
            gastoViaje.Id_GV = Int32.Parse(Request.QueryString["Id"] == null ? "-1" : Request.QueryString["Id"]);

            CN_CapGastoViaje clsGastoViaje = new CN_CapGastoViaje();
            clsGastoViaje.ConsultaGastoViaje(gastoViaje, Sesion.Emp_Cnx);

            TxtSolicitanteViajero.Text = gastoViaje.GV_Solicitante;
            TxtMotivo.Text = gastoViaje.GV_Motivo;
            TxtFechaSalida.SelectedDate = gastoViaje.GV_FechaSalida;
            TxtFechaRegreso.SelectedDate = gastoViaje.GV_FechaRegreso;
            TxtImporteSolicitado.Text = gastoViaje.GV_Importe.ToString();

            TimeSpan ts = TxtFechaRegreso.SelectedDate.Value - TxtFechaSalida.SelectedDate.Value;
            TxtCantidadDias.Text = ts.TotalDays.ToString();

            rgPagoElectronico.Rebind();
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

            int verificador = 0;

            CN_CapGastoViaje clsGastoViaje = new CN_CapGastoViaje();
            clsGastoViaje.RegistrarGastoViaje(gastoViaje, Sesion.Emp_Cnx, ref verificador);

            if (verificador == 1)
            {
                AlertaCerrar("La cuenta de gastos de viaje ha sido registada.");
            }
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
                TxtSaldoFavor.Text = diferencia < 0 ? string.Format("{0:N2}", diferencia * -1) : string.Format("{0:N2}", diferencia);
                LblSaldoFavor.Text = diferencia < 0 ? "A mi Cargo" : "A Mi Favor";

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

            string ruta = null;
            System.IO.StreamWriter sw = null;
            ruta = Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "GVComprobante" + id_GVComprobante.ToString() + ".txt";

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "GVComprobante" + id_GVComprobante.ToString() + ".xml"))
                File.Delete(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "GVComprobante" + id_GVComprobante.ToString() + ".xml");
            sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            sw.WriteLine(gastoViajeComprobante.GVComprobante_Xml.ToString());
            sw.Close();
            File.Move(ruta, Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "GVComprobante" + id_GVComprobante.ToString() + ".xml");
            RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\archivoXml" + Sesion.Id_U.ToString() + "GVComprobante", id_GVComprobante.ToString(), ".xml')"));
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
    }
}