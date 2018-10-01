using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;
using System.Configuration;
using System.Data;
using System.Xml;

namespace SIANWEB
{
    public partial class CapPagosElectronicos_Listado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CapaEntidad.Sesion Sesion = new CapaEntidad.Sesion();
            Sesion = (CapaEntidad.Sesion)Session["Sesion" + Session.SessionID];
            if (Sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
            }
            else
            {
                if (!IsPostBack)
                {
                    Inicializar();
                }
            }

        }

        private void Inicializar()
        {
            rgPagoElectronico.Rebind();
        }

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
                Alerta(Message);
                //this.lblMensaje.Text = Message;
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
                Alerta("Error: [" + NombreFuncion + "] " + eme.Message.ToString());
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                Alerta("Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString());
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion

        #region Eventos
        protected void rgPagoElectronico_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Int32 item = default(Int32);
                item = e.Item.ItemIndex;

                int id_RowFactura = Convert.ToInt32(rgPagoElectronico.Items[item]["id_RowFactura"].Text);

                switch (e.CommandName.ToString())
                {
                    case "XML":
                        descargarXML(id_RowFactura);
                        break;
                    case "PDF":
                        descargarPDF(id_RowFactura);
                        break;
                    case "Delete":
                        //Cancelar(id_RowFactura);
                        break;

                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void descargarXML(int id_RowFactura)
        {
            CapaEntidad.PagoElectronico PagElec = null;
            try
            {
                PagElec = (Session["PagElec"] as CapaEntidad.PagoElectronico);
            }
            catch 
            {
                PagElec = null;
            }

            if (PagElec != null && PagElec.PagElecArchivo.Count > 0)
            {
                CapaEntidad.Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (CapaEntidad.Sesion)Session["Sesion" + Session.SessionID];
                CapaEntidad.PagoElectronicoArchivo PagElecArchivo = null;

                foreach (CapaEntidad.PagoElectronicoArchivo item in PagElec.PagElecArchivo) {
                    if (item.id_RowFactura == id_RowFactura) {
                        PagElecArchivo = item;
                        break;
                    }
                }
                //JFCV 15 Oct 2015 en el servidor no se leia el xml , cambiar la ruta 
                //string ruta = null;
                //ruta = Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + PagElec.Id_PagElec.ToString() + ".txt";

                //if (File.Exists(ruta))
                //    File.Delete(ruta);
                //if (File.Exists(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + PagElec.Id_PagElec.ToString() + ".xml"))
                //    File.Delete(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + PagElec.Id_PagElec.ToString() + ".xml");


                //using (FileStream fileStream = File.Create(ruta))
                //{
                //    MemoryStream MS = new MemoryStream(PagElecArchivo.PagElec_XMLStream);
                //    MS.CopyTo(fileStream);
                //    fileStream.Close();
                //}
                //File.Move(ruta, Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + PagElec.Id_PagElec.ToString() + ".xml");
                //RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\archivoXml" + Sesion.Id_U.ToString() + "PagElec", PagElec.Id_PagElec.ToString(), ".xml')"));

                string ruta = null;
              
                ruta = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + PagElec.Id_PagElec.ToString() + ".txt"));

                string strarchivo = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + PagElec.Id_PagElec.ToString()));
                //JFCV 08nov2016 agregue un if para que si no tiene nada en el XML ( sin comprobante que no entre aqui ) 
                if (PagElecArchivo.PagElec_XMLStream != null)
                {
                    if (File.Exists(ruta))
                        File.Delete(ruta);
                    if (File.Exists(strarchivo + ".xml"))
                        File.Delete(strarchivo + ".xml");


                    using (FileStream fileStream = File.Create(ruta))
                    {
                        MemoryStream MS = new MemoryStream(PagElecArchivo.PagElec_XMLStream);
                        MS.CopyTo(fileStream);
                        fileStream.Close();
                    }
                    File.Move(ruta, strarchivo + ".xml");
                    //string jola = @"C:\Users\egdev001\Documents\Visual Studio 2010\Projects\SIANWeb - Gastos\SIANWEB\Reportes\archivoXml37PagElec15.xml";
                    //RAM1.ResponseScripts.Add(@"abrirArchivo('" + jola + "')");
                    RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('xmlSAT\\archivoXml" + Sesion.Id_U.ToString() + "PagElec", PagElec.Id_PagElec.ToString(), ".xml')"));
                }
              
            }
            else
            {
                Alerta("El registro no tiene un archivo XML.");
            }
        }


        private void descargarPDF(int id_RowFactura)
        {
            CapaEntidad.PagoElectronico PagElec = null;
            try
            {
                PagElec = (Session["PagElec"] as CapaEntidad.PagoElectronico);
            }
            catch
            {
                PagElec = null;
            }

            if (PagElec != null && PagElec.PagElecArchivo.Count > 0)
            {
                CapaEntidad.Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (CapaEntidad.Sesion)Session["Sesion" + Session.SessionID];
                CapaEntidad.PagoElectronicoArchivo PagElecArchivo = null;

                foreach (CapaEntidad.PagoElectronicoArchivo item in PagElec.PagElecArchivo)
                {
                    if (item.id_RowFactura == id_RowFactura)
                    {
                        PagElecArchivo = item;
                        break;
                    }
                }


                string tempPDFname = string.Concat(
                    "GASTO_", 
                    Sesion.Id_Emp.ToString(), 
                    "_", Sesion.Id_Cd.ToString(), 
                    "_", PagElec.Id_PagElec.ToString(), 
                    ".pdf"
                );
                string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                string WebURLtempPDFGastos = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDFGastos"].ToString(), tempPDFname);
                this.ByteToTempPDF(URLtempPDF, PagElecArchivo.PagElec_PDFStream);

                string URLtempPDFCN = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                string WebURLtempPDFCN = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDFGastos"].ToString(), tempPDFname);


                // ------------------------------------------------------------------------------------------------
                // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                // ------------------------------------------------------------------------------------------------
                RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDFGastos, "')"));
            }
            else
            {
                Alerta("El registro no tiene un archivo XML.");
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
                FileAccess.Write);
            fs.Write(filebytes, 0, filebytes.Length);
            fs.Close();
        }

        protected void rgPagoElectronico_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    
                    rgPagoElectronico.DataSource = GetList(Convert.ToInt32("0" + Request["id"]));
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected string ObtenerNombreAcredor(byte[] xmlObject)
        {
            string Result = null;
            if (xmlObject != null && xmlObject.Length > 0)
            {
                try
                {
                    MemoryStream xmlStream = new MemoryStream(xmlObject);
                    string nombre = null;

                    XmlDocument xmldoc = new XmlDocument();
                    XmlNodeList xmlnode;

                    xmldoc.Load(xmlStream);
                    Session["xml"] = xmlStream;
                    xmlnode = xmldoc.GetElementsByTagName("cfdi:Emisor");
                    try
                    {
                        nombre = xmlnode[0].Attributes["nombre"].Value;
                    }
                    catch
                    {
                        nombre = "";
                    }
                    Result = nombre;
                }
                catch
                {
                    Result = null;
                }
            }
            else
            {
                Result = "Sin Comprobante";
                //JFCV 08 nov 2016 Result = null;
            }


            return Result != null ? Result : "";
        }

        private List<CapaEntidad.PagoElectronicoArchivo> GetList(int Id_PagElec)
        {
            try
            {
                CapaNegocios.CN_CapPagoElectronico clsPagoElectronico = new CapaNegocios.CN_CapPagoElectronico();

                CapaEntidad.Sesion session = new CapaEntidad.Sesion();
                session = (CapaEntidad.Sesion)Session["Sesion" + Session.SessionID];

                CapaEntidad.PagoElectronico pagoElectronico = new CapaEntidad.PagoElectronico();
                pagoElectronico.Id_Emp = session.Id_Emp;
                pagoElectronico.Id_Cd = session.Id_Cd_Ver;
                pagoElectronico.Id_PagElec = Id_PagElec;

                clsPagoElectronico.ConsultaPagoElectronico(pagoElectronico, session.Emp_Cnx);

                Session["PagElec"] = pagoElectronico;

                return pagoElectronico.PagElecArchivo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CerrarVentana(string param)
        {
            try
            {
                string funcion = "CloseAndRebind('" + param + "')";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //JFCV 08 Nov 2016  paginación paginacion para que pagine correctamente
        protected void rgPagoElectronico_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {

                rgPagoElectronico.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #endregion
    }
}