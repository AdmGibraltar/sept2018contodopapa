using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using CapaEntidad;
using System.Text;
using CapaNegocios;
using System.IO;
using System.Configuration;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class CapGastoViajeEnviado : System.Web.UI.Page
    {
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        //SAUL GUERRA 20150716  BEGIN

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
                    ValidarPermisos();
                    CargarCentros();
                    Inicializar();
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
                //rgAsignacion.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }

        protected void rgPago_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgPago.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPago_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GastoViaje gastoViaje = new GastoViaje();

                int item = e.Item.ItemIndex;

                gastoViaje.Id_GV = Int32.Parse(rgPago.Items[item]["Id_GV"].Text);
                gastoViaje.Id_Emp = Int32.Parse(rgPago.Items[item]["Id_Emp"].Text);
                gastoViaje.Id_Cd = Int32.Parse(rgPago.Items[item]["Id_Cd"].Text);


                switch (e.CommandName.ToString())
                {
                    case "Registro":
                        RAM1.ResponseScripts.Add("return AbrirVentana_RegistroGastos(" + gastoViaje.Id_GV.ToString() + ")");
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPago_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_PageIndexChanged");
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            rgPago.Rebind();
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
                //throw ex;
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                    //if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    //{
                    //    this.rtb1.Items[5].Visible = false;
                    //}
                    //this.rtb1.Items[4].Visible = false;
                    ////Eliminar
                    //this.rtb1.Items[3].Visible = false;
                    ////Imprimir
                    //this.rtb1.Items[2].Visible = false;
                    ////Correo
                    //this.rtb1.Items[1].Visible = false;
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                //throw ex;
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
                       // Guardar();
                    }
                    else if (btn.CommandName == "new")
                    {
                        RAM1.ResponseScripts.Add("return AbrirVentana_RegistroGastos('-1', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "')");
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

        private void Inicializar()
        {
            rgPago.Rebind();
        }

        private List<GastoViaje> GetList()
        {
            try
            {
                CN_CapGastoViaje clsGastoViaje = new CN_CapGastoViaje();
                List<GastoViaje> list = new List<GastoViaje>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                GastoViaje gastoViaje = new GastoViaje();
                gastoViaje.Id_Emp = session.Id_Emp;
                gastoViaje.Id_Cd = session.Id_Cd_Ver;

                clsGastoViaje.ConsultaGastoViaje(gastoViaje, session.Emp_Cnx, ref list);

                return list.Where(x => x.Id_GVEst == 3).ToList();
                //return list;
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
            cuerpo_correo.Append("<td>Le informamos que su solicitud de gastos de viaje ha sido autorizada</td>");
            cuerpo_correo.Append("</tr>");
            //cuerpo_correo.Append("<tr>");
            //cuerpo_correo.Append("<td>Concepto : " + pagoElectronico.PagElecConcepto_Descripcion + "</td>");
            //cuerpo_correo.Append("</tr>");
            cuerpo_correo.Append("<tr>");
            cuerpo_correo.Append("<td><br></td>");
            cuerpo_correo.Append("</tr>");
            cuerpo_correo.Append("<tr>");
            cuerpo_correo.Append("<td>Accesar a : <a href=\"http://10.1.0.120/SIANCENTRAL/RepPagoElectronico.aspx\">Rastreo de Solicitud</a></td>");
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
            m.From = new MailAddress(configuracion.Mail_GastosAvisoGerente);
            m.Subject = "Autorización de Gasto de Viaje";
            m.IsBodyHtml = true;
            m.Body = cuerpo_correo.ToString();
            sm.Send(m);

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