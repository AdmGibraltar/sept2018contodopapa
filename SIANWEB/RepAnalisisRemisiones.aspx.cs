
using System;
using System.Collections;
using System.IO;
using System.Web.UI;
using CapaDatos;
using CapaEntidad;
using CapaNegocios;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class RepAnalisisRemisiones : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion
        #region Eventos
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
                        //ValidarPermisos();
                        CargarCentros();
                        if (!Sesion.Cu_Modif_Pass_Voluntario)
                        {
                            RadAjaxManager1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
                        }
                        

                        dpFechaini.DbSelectedDate = Sesion.CalendarioIni;
                        dpFechafin.DbSelectedDate = Sesion.CalendarioFin;

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Page_Load");
            }
        }
      
        
       
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "print":
                        Abrir_Reporte(true);
                        break;
                    case "excel":
                        Abrir_Reporte(false);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Sesion.Id_Cd_Ver = Convert.ToInt32(CmbCentro.SelectedItem.Value);
        }
        protected void rblRemisiones_SelectedIndexChanged(object sender, EventArgs e)
        {


            //if (rblRemisiones.SelectedValue == "1")
            //{
            //    rblDetalle.Enabled = true;
            //}
            //else
            //{
            //    rblDetalle.Enabled = false;
            //}
        }
         
     
       

        protected void RblTipoRep_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {//TODO que ponga la fecha o el mes 
                if (this.RblTipoRep.SelectedValue == "1")
                {
                    
                }
                else
                {
                     
                }

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        #endregion
        #region Metodos
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
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("print")).Visible = _PermisoImprimir;
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Abrir_Reporte(bool a_pantalla)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();
                ArrayList ALValorParametrosConsignacion = new ArrayList();
                ArrayList ALValorParametrosGeneral = new ArrayList();
                CN__Comun cn_comun = new CN__Comun();
               

                


                //Consulta centro de distribución
                string Emp_Nombre = "";
                string Cd_Nombre = "";
                string U_Nombre = "";
                new CN_CapPedido().ConsultarEncabezado_RepFacPedidosPendientes(sesion, ref Emp_Nombre, ref Cd_Nombre, ref U_Nombre);
                //parametros cabecera       
                ALValorParametrosInternos.Add("Listado"); //remisiones
                ALValorParametrosInternos.Add("63"); //tipo remision

                // calcular la fecha actual y final en base a este dato ALValorParametrosInternos.Add(this.RblTipoRep.SelectedValue == "1" ? null : this.cmbanio.SelectedValue);
                DateTime fechainicial = new DateTime();
                DateTime fechafinal = new DateTime();

                fechainicial = (DateTime)dpFechaini.SelectedDate.Value;
                fechafinal = (DateTime)dpFechafin.SelectedDate.Value;
             
                fechafinal = DateTime.Now;
                
                if (this.RblTipoRep.SelectedValue == "1")  //1 actual 2  cierre
                {
       
                    fechainicial = fechainicial.AddYears(-2);
                    int saño = fechainicial.Year;
                    string fecha = "";
                    fecha = saño.ToString() + "-01-01 00:00:00.000";
                    fechainicial = Convert.ToDateTime(fecha);
                }
                else
                {
                    fechafinal = fechainicial.AddDays(-1);

                    fechainicial = fechainicial.AddYears(-2);
                    int saño = fechainicial.Year;
                    string fecha = "";
                    fecha = saño.ToString() + "-01-01 00:00:00.000";
                    fechainicial = Convert.ToDateTime(fecha);

                }

                //Parametros del Reporte General
                ALValorParametrosGeneral.Add(sesion.Emp_Cnx);
                ALValorParametrosGeneral.Add(sesion.Id_Emp);
                ALValorParametrosGeneral.Add(sesion.U_Nombre);//usuario
                ALValorParametrosGeneral.Add(Emp_Nombre);//nombre empresa
                ALValorParametrosGeneral.Add("Todos");//nombre sucursal
                ALValorParametrosGeneral.Add(DateTime.Now.ToString());//fecha inicial
                ALValorParametrosGeneral.Add(fechainicial);//fecha inicial
                ALValorParametrosGeneral.Add(fechafinal);//fecha final

                //Reporte de consignación parametros 
                ALValorParametrosConsignacion.Add(sesion.Emp_Cnx);
                ALValorParametrosConsignacion.Add(sesion.Id_Emp);//nombre empresa
                ALValorParametrosConsignacion.Add(sesion.Id_Cd.ToString() );//nombre sucursal
                //9mayo2018 agregar el tipo de si es cierre o fecha actual 
                
                if (this.RblTipoRep.SelectedValue == "1")  //1 actual 2 cierre
                {
                    ALValorParametrosConsignacion.Add(1);
                }
                else
                {
                    ALValorParametrosConsignacion.Add(2);
                }

                
                ALValorParametrosConsignacion.Add("Producto en consignación");
                ALValorParametrosConsignacion.Add(fechafinal);//fecha final
                ALValorParametrosConsignacion.Add(sesion.U_Nombre);//usuario
                ALValorParametrosConsignacion.Add(Emp_Nombre);//nombre empresa
                ALValorParametrosConsignacion.Add("Todos");//nombre cdi
                ALValorParametrosConsignacion.Add(DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString());//fecha
                ALValorParametrosConsignacion.Add("");   

                ALValorParametrosInternos.Add(fechainicial);//fecha inicial
                ALValorParametrosInternos.Add(fechafinal);//fecha final

                ALValorParametrosInternos.Add("Todos");//RIK
                ALValorParametrosInternos.Add("Todos" );//Territorio
                ALValorParametrosInternos.Add("Todos"); //Cliente
                ALValorParametrosInternos.Add("Todos");//Producto
                ALValorParametrosInternos.Add("Vencidas");//Estatus
                ALValorParametrosInternos.Add( "A detalle");  // : "Sin detalle");//Detalle chkDetalle.Checked

                ALValorParametrosInternos.Add(Emp_Nombre);//nombre empresa
                ALValorParametrosInternos.Add(sesion.Id_Cd.ToString() + " - " + sesion.Cd_Nombre);//nombre sucursal
                ALValorParametrosInternos.Add(sesion.U_Nombre);//usuario
                ALValorParametrosInternos.Add(DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString());//fecha

                //parametros para el cuerpo del reporte
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                // JFCV envío la sucursal si fue elegido un CDI o CDC   ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(sesion.Id_Cd.ToString());
                
                ALValorParametrosInternos.Add("63");
                //agregar parametros de filtros 


                ALValorParametrosInternos.Add(fechainicial);//fecha inicial
               // ALValorParametrosInternos.Add(dpFechafin.SelectedDate);//fecha final
                ALValorParametrosInternos.Add(fechafinal);
                ALValorParametrosInternos.Add((object)null);//RIK
                ALValorParametrosInternos.Add((object)null); //territorio
                ALValorParametrosInternos.Add((object)null); //Cliente
                ALValorParametrosInternos.Add((object)null); //Producto

              

                ALValorParametrosInternos.Add("0");//Estatus vencida , no vencida 

                ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                Funciones funcion = new Funciones();
                ALValorParametrosInternos.Add(funcion.GetLocalDateTime(sesion.Minutos));//FECHA ACTUAL
              
                Type instance = null;
                Type instance2 = null;

                instance = typeof(LibreriaReportes.ExpRep_RemisionVencidaBCENTRAL);
                

                instance2 = typeof(LibreriaReportes.ExpRep_InvRotacion4Consig);

                ImprimirXLS(ALValorParametrosInternos, instance, ALValorParametrosConsignacion, instance2);
       
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void ImprimirXLS(ArrayList ALValorParametrosInternos, Type instance, ArrayList ALValorParametrosConsignacion, Type instance2)
        {
            try
            {

                //Reporte de Facturas vigentes
                Telerik.Reporting.Report report1 = (Telerik.Reporting.Report)Activator.CreateInstance(instance);
                for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
                {
                    report1.ReportParameters[i].AllowNull = true;
                    report1.ReportParameters[i].Value = ALValorParametrosInternos[i];
                }
                report1.ReportParameters[23].Value = 1;
                report1.ReportParameters[16].Value = 63;
                report1.ReportParameters[1].Value = "Pendiente por facturar";

                //Reporte de Facturas vencidas
                Telerik.Reporting.Report reportPXFVEN = (Telerik.Reporting.Report)Activator.CreateInstance(instance);
                for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
                {
                    if (i == 23)
                    {
                        reportPXFVEN.ReportParameters[i].AllowNull = true;
                        reportPXFVEN.ReportParameters[i].Value = 0;
                    }
                    else
                    {
                        reportPXFVEN.ReportParameters[i].AllowNull = true;
                        reportPXFVEN.ReportParameters[i].Value = ALValorParametrosInternos[i];
                    }
                }
                reportPXFVEN.ReportParameters[23].Value = 0;
                reportPXFVEN.ReportParameters[16].Value = 63;
                reportPXFVEN.ReportParameters[1].Value = "Pendiente por facturar";

                //Reporte de prueba Vigente
                Telerik.Reporting.Report PruebaVIG = (Telerik.Reporting.Report)Activator.CreateInstance(instance);
                for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
                {
                    
                        PruebaVIG.ReportParameters[i].AllowNull = true;
                        PruebaVIG.ReportParameters[i].Value = ALValorParametrosInternos[i];
                }

                PruebaVIG.ReportParameters[16].Value=64;
                PruebaVIG.ReportParameters[1].Value = "Prueba";
                PruebaVIG.ReportParameters[23].Value = 0;  //vigente

                //Reporte de prueba Vencidas
                Telerik.Reporting.Report PruebaVEN = (Telerik.Reporting.Report)Activator.CreateInstance(instance);
                for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
                {
                    PruebaVEN.ReportParameters[i].AllowNull = true;
                    PruebaVEN.ReportParameters[i].Value = ALValorParametrosInternos[i];
                }
                PruebaVEN.ReportParameters[16].Value = 64;
                PruebaVEN.ReportParameters[1].Value = "Prueba";
                PruebaVEN.ReportParameters[23].Value = 1;  //Vencida



                //Reporte de No conformes Vigente
                Telerik.Reporting.Report NCVIG = (Telerik.Reporting.Report)Activator.CreateInstance(instance);
                for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
                {

                    NCVIG.ReportParameters[i].AllowNull = true;
                    NCVIG.ReportParameters[i].Value = ALValorParametrosInternos[i];
                }

                NCVIG.ReportParameters[16].Value = 65;
                NCVIG.ReportParameters[1].Value = "Producto no conforme";
                NCVIG.ReportParameters[23].Value = 0;  //vigente

                //Reporte de No Conforme Vencidas
                Telerik.Reporting.Report NCVEN = (Telerik.Reporting.Report)Activator.CreateInstance(instance);
                for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
                {
                    NCVEN.ReportParameters[i].AllowNull = true;
                    NCVEN.ReportParameters[i].Value = ALValorParametrosInternos[i];
                }
                NCVEN.ReportParameters[16].Value = 65;
                NCVEN.ReportParameters[1].Value = "Producto no conforme";
                NCVEN.ReportParameters[23].Value = 1;  //Vencida


                //Reporte de prueba Vigente
                Telerik.Reporting.Report RepConsignacion = (Telerik.Reporting.Report)Activator.CreateInstance(instance2);
                for (int i = 0; i <= ALValorParametrosConsignacion.Count - 1; i++)
                {

                    RepConsignacion.ReportParameters[i].AllowNull = true;
                    RepConsignacion.ReportParameters[i].Value = ALValorParametrosConsignacion[i];
                }
  

                var reportBook = new ReportBook();
                reportBook.Reports.Add(report1);
                reportBook.Reports.Add(reportPXFVEN);
                reportBook.Reports.Add(PruebaVIG);
                reportBook.Reports.Add(PruebaVEN);
                reportBook.Reports.Add(NCVIG);
                reportBook.Reports.Add(NCVEN);
                reportBook.Reports.Add(RepConsignacion);
                reportBook.Reports[0].DocumentName = "PXF_VENCIDA";
                reportBook.Reports[1].DocumentName = "PXF_VIGENTE";
                reportBook.Reports[3].DocumentName = "PRUEBA VENCIDA";
                reportBook.Reports[2].DocumentName = "PRUEBA VIGENTE";
                reportBook.Reports[5].DocumentName = "NC VENCIDA";
                reportBook.Reports[4].DocumentName = "NC VIGENTE";
                reportBook.Reports[6].DocumentName = "Consignacion";  

                ReportProcessor reportProcessor = new ReportProcessor();
                RenderingResult result = reportProcessor.RenderReport("XLS", reportBook, null);
                string ruta = Server.MapPath("Reportes") + "\\" + ALValorParametrosConsignacion[2] +  instance.Name + ".xls";
                if (File.Exists(ruta))
                    File.Delete(ruta);
                FileStream fs = new FileStream(ruta, FileMode.Create);
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                fs.Flush();
                fs.Close();
       
                RadAjaxManager1.ResponseScripts.Add("startDownload('" + ALValorParametrosConsignacion[2] + instance.Name + ".xls');");

            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        #endregion

        #region ErrorManager
        private void Alerta(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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


