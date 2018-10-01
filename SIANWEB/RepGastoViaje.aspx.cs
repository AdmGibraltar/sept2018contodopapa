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
    public partial class RepGastoViaje : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (Sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
            }
            else
            {
                if (!IsPostBack)
                {
                    ValidarPermisos();
                    //CargarCDI();
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
                    case "Comprobantes":
                        //RAM1.ResponseScripts.Add("return AbrirVentana_ComprobacionGastos(" + gastoViaje.Id_GV.ToString() + ")");
                        RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_LstComprobantesGV('", gastoViaje.Id_GV, "' ,'", gastoViaje.Id_Emp, "' ,'", gastoViaje.Id_Cd, "')"));
                        break;

                    case "Consultar":
                        int Id_PagElec = Convert.ToInt32(rgPago.Items[item]["Id_PagElec"].Text);
                        RAM1.ResponseScripts.Add("return AbrirVentana_GastosConsultar(" + Id_PagElec.ToString() + ")");
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPagoGastoViaje_ItemCommand(object source, GridCommandEventArgs e)
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

                    case "Comprobantes":
                        RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_LstComprobantesGV('", gastoViaje.Id_GV, "' ,'", gastoViaje.Id_Emp, "' ,'", gastoViaje.Id_Cd, "')"));

                        break;


                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }



        //JFCV 19 feb 2016  paginación paginacion para que pagine correctamente
        protected void rgPago_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {

                rgPago.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            rgPago.Rebind();
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            rgPago.Rebind();
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
                //JFCV 29nov2016 INICIO agregar los filtros para que se realice la busqueda 
                if (CmbId_Cd.SelectedIndex == -1)
                {
                    gastoViaje.Id_Cd = session.Id_Cd_Ver;
                }
                else
                {
                    gastoViaje.Id_Cd = Convert.ToInt32(CmbId_Cd.SelectedValue);
                }

                if (txtidGastoViaje.Text != "")
                {
                    gastoViaje.Id_GV = Convert.ToInt32(txtidGastoViaje.Text);
                }

                if (txtidPagoElectronico.Text != "")
                {
                    gastoViaje.Id_PagElec = Convert.ToInt32(txtidPagoElectronico.Text);
                }





                gastoViaje.Id_PagElecTipo = Convert.ToInt32(CmbTipo.SelectedValue);


                //JFCV 29nov2016 FIN agregar los filtros para que se realice la busqueda 

                clsGastoViaje.ConsultaGastoViaje(gastoViaje, session.Emp_Cnx, ref list);

                int idGVEst = Int32.Parse(CmbEstatus.SelectedValue);

                if (idGVEst != 0)
                {
                    //Si el gasto de viaje tiene estatus 3 es que esta ya comprobado
                    if (idGVEst == 6)
                        idGVEst = 3;
                    else
                    {
                        if (idGVEst == 3)
                        { idGVEst = 6; }
                    }
                    return list.Where(x => x.Id_GVEst == idGVEst).ToList();
                }
                else
                {
                    return list;
                }

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
                    // Ejecuta funciós JS para abrir una nueva ventana de Explorador y visualizar el archivo PDF
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

        private void CargarCDI()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Emp_Cnx, "SpCatCdi_Combo", ref CmbId_Cd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}