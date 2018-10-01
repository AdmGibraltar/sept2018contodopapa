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
using CapaEntidad;
using CapaNegocios;
using System.Text;

namespace SIANWEB
{
    public partial class CapGastosViajeComprobantes_Listado : System.Web.UI.Page
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
                        //Cancelar(id_RowFactura);
                        break;

                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        #region Decsragar xmls y pdfs
        
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

            //string rutadestino = ConfigurationManager.AppSettings["URLtempPDF"].ToString();
            
            //ruta = Server.MapPath(string.Concat(rutadestino, Sesion.Id_U.ToString() + "GV" + id_GVComprobante.ToString() + ".txt"));

            //if (File.Exists(ruta))
            //    File.Delete(ruta);
            //if (File.Exists(rutadestino + "\\archivoXml" + Sesion.Id_U.ToString() + "GV" + id_GVComprobante.ToString() + ".xml"))
            //    File.Delete(rutadestino + "\\archivoXml" + Sesion.Id_U.ToString() + "GV" + id_GVComprobante.ToString() + ".xml");
            //sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            //sw.WriteLine(gastoViajeComprobante.GVComprobante_Xml.ToString());
            //sw.Close();
            //File.Move(ruta, rutadestino + "\\archivoXml" + Sesion.Id_U.ToString() + "GV" + id_GVComprobante.ToString() + ".xml");
            //RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('" + rutadestino + "\\archivoXml" + Sesion.Id_U.ToString() + "GV", id_GVComprobante.ToString(), ".xml')"));


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
            //string jola = @"C:\Users\egdev001\Documents\Visual Studio 2010\Projects\SIANWeb - Gastos\SIANWEB\Reportes\archivoXml37PagElec15.xml";
            //RAM1.ResponseScripts.Add(@"abrirArchivo('" + jola + "')");
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
                FileAccess.Write);
            fs.Write(filebytes, 0, filebytes.Length);
            fs.Close();
        }

        #endregion
        
        protected void rgPagoElectronico_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    
                    //rgPagoElectronico.DataSource = GetList(Convert.ToInt32("0" + Request["id"]));
                    rgPagoElectronico.DataSource = GetList();

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
                Result = "No es Comp. Fiscal";
                //JFCV 08 nov 2016 Result = null;
            }


            return Result != null ? Result : "";
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
                Session["Id_GV"] = gastoViajeComprobante.Id_GV;

                return list;
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