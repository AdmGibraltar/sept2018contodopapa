using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

namespace SIANWEB
{
    public partial class ProPrecioEspecial_Admin : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.CargarCentros();
                        CargarEstatus();
                        CargarVencido();
                        PoblarTablaDT();

                        dpFechaIni.DbSelectedDate = Sesion.CalendarioIni;
                        dpFechaFin.DbSelectedDate = Sesion.CalendarioFin;

                        double ancho = 0;
                        foreach (GridColumn gc in rg1.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rg1.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rg1.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rg1.Rebind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                //this.Nuevo();
                this.rg1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rg1.DataSource = GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //DataRow[] dr = dt.Select(GenerarQry());
                //foreach (DataRow d in dr)
                //{
                //    dt.ImportRow(d);
                //}
                this.rg1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rg1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                DateTime fechaPeriodoInicio = session2.CalendarioIni;
                DateTime fechaPeriodoFinal = session2.CalendarioFin;
                GridItem gi = e.Item;
                //List<string> statusPosibles;

        


                switch (e.CommandName.ToLower())
                {
                    case "renovar":
                        renovar(gi);
                        break;
                    case "traslape":
                        traslape(gi);
                        break;
                    case "editar":
                        editar(gi);
                        break;
                    case "enviar":
                         if(gi.Cells[4].Text.ToString().ToLower() == "c") {
                            EnviaEmail(Convert.ToInt32(gi.Cells[2].Text));
                         }  else {
                             Alerta("La solicitud se encuentra en estatus no válido para su envio");
                         }
                        break;
                    case "eliminar":
                        eliminar(gi);
                        break;

                    case "autorizar":
                        autorizar(gi);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void eliminar(GridItem gi)
        {
            Sesion session2 = new Sesion();
            session2 = (Sesion)Session["Sesion" + Session.SessionID];
            if (gi.Cells[4].Text.ToString().ToLower() == "s" || gi.Cells[4].Text.ToString().ToLower() == "c")
            {
                CN_PrecioEspecial cn_precioespecial = new CN_PrecioEspecial();
                PrecioEspecial p = new PrecioEspecial();
                p.Id_Ape = Convert.ToInt32(gi.Cells[2].Text);
                p.Id_Emp = session2.Id_Emp;
                p.Id_Cd = session2.Id_Cd_Ver;
                int verificador = 0;
                cn_precioespecial.Eliminar(p, session2.Emp_Cnx, ref verificador);

                if (verificador == 1)
                {
                    Alerta("La solicitud se cancelo exitosamente");
                }
                else if (verificador == 2)
                {
                    Alerta("La solicitud se encuentra en estatus no valido para la cancelación");
                }
                else
                {
                    Alerta("Ocurrio un error al intentar cancelar la solicitud");
                }

                rg1.Rebind();

            }
            else
            {
                Alerta("La solicitud se encuentra en estatus no valido para la cancelación");
            }
        }
        protected void rg1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    WebControl Button = default(WebControl);
                    string clickHandler = "";

                    Button = (WebControl)item["Renovar"].Controls[0];
                    clickHandler = Button.Attributes["onclick"];
                    Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Ape").ToString());

                    Button = (WebControl)item["Sustituir"].Controls[0];
                    clickHandler = Button.Attributes["onclick"];
                    Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Ape").ToString());

                    Button = (WebControl)item["Enviar"].Controls[0];
                    clickHandler = Button.Attributes["onclick"];
                    Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Ape").ToString());
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_ItemDataBound");
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        rg1.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rg1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                rg1.DataSource = GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion

        #region Funciones
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

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
                        this.rtb1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");


                if (ConsultarAutorizacionPrecio() == "True")
                {

                    this.rg1.Columns[11].Visible = true ;
                }

                else
                {
                    this.rg1.Columns[11].Visible = false;
                
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                throw ex;
            }
        }
        private void CargarEstatus()
        {
            cmbEstatus.Items.Add(new RadComboBoxItem("-- Todos --", ""));
            cmbEstatus.Items.Add(new RadComboBoxItem("Pendiente de solicitar", "C"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Solicitada", "S"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Autorizado", "A"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Parcialmente autorizada", "P"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Rechazada", "R"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Cancelada", "B"));
        }
        private DataTable GetList()
        {
            try
            {
                List<AutPrecioEspecial> List = new List<AutPrecioEspecial>();
                CN_PrecioEspecial clsCN_CapProAutPrecioEspecial = new CN_PrecioEspecial();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                AutPrecioEspecial AutPrecioEspecial = new AutPrecioEspecial();
                AutPrecioEspecial.Id_Emp = session2.Id_Emp;
                AutPrecioEspecial.Id_Cd = session2.Id_Cd_Ver;
                if (txtFolioIni.Text != "") AutPrecioEspecial.Folio1 = Convert.ToInt32(txtFolioIni.Text);
                if (txtFolioFin.Text != "") AutPrecioEspecial.Folio2 = Convert.ToInt32(txtFolioFin.Text);
                if (dpFechaIni.SelectedDate != null) AutPrecioEspecial.Fecha1 = dpFechaIni.SelectedDate;
                if (dpFechaFin.SelectedDate != null) AutPrecioEspecial.Fecha2 = Convert.ToDateTime(dpFechaFin.SelectedDate.Value.ToString("dd/MM/yyyy")).AddDays(1).AddSeconds(-1);
                AutPrecioEspecial.Estatus = cmbEstatus.SelectedValue;
                if (txtClienteIni.Text != "") AutPrecioEspecial.Id_CteFiltro1 = Convert.ToInt32(txtClienteIni.Text);
                if (txtClienteFin.Text != "") AutPrecioEspecial.Id_CteFiltro2 = Convert.ToInt32(txtClienteFin.Text);
               // if (string.IsNullOrEmpty(txtSolicitud.Text)) AutPrecioEspecial.Solicitud = Convert.ToInt32(txtSolicitud.Text);
                
                clsCN_CapProAutPrecioEspecial.ConsultaProAutPrecioEspecial_Lista(AutPrecioEspecial, session2.Emp_Cnx, ref List);

                dt.Clear();
                foreach (AutPrecioEspecial precEsp in List)
                {
                    int Vencido = 0;
                    clsCN_CapProAutPrecioEspecial.ConsultaProAutPrecioEspecialVencido(ref Vencido, session2.Id_Emp, session2.Id_Cd_Ver, precEsp.Id_Ape, session2.Emp_Cnx);

                    switch (Vencido)
                    {
                        case 1:
                            precEsp.VencidoStr = "Parcial"; //solicitud parcialmente expirada
                            break;
                        case 2:
                            precEsp.VencidoStr = "Si"; //todas las partidas están expiradas (toda la solicitud)
                            break;
                        case 3:
                            precEsp.VencidoStr = "No"; //ninguna partida de la solicitud está expirada
                            break;
                    }

                    DataRow row = dt.NewRow();
                    row["Ape_Estatus"] = precEsp.Ape_Estatus;
                    row["Ape_EstatusStr"] = precEsp.Ape_EstatusStr;
                    row["Ape_Fecha"] = precEsp.Ape_Fecha;
                    row["Cte_NomComercial"] = precEsp.Cte_NomComercial;
                    row["Id_Ape"] = precEsp.Id_Ape;
                    row["Id_Cte"] = precEsp.Id_Cte;
                    row["Vencido"] = Vencido;
                    row["VencidoStr"] = precEsp.VencidoStr;

                    if (cmbVencido.SelectedValue != "-1")
                    {
                        if (row["Vencido"].ToString() == cmbVencido.SelectedValue)
                            dt.Rows.Add(row);
                    }
                    else
                        dt.Rows.Add(row);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PoblarTablaDT()
        {
            try
            { //poblar la tabla virtual con columnas vacias:
                dt = new DataTable();
                dt.Columns.Add("Ape_Estatus", System.Type.GetType("System.String"));
                dt.Columns.Add("Ape_EstatusStr", System.Type.GetType("System.String"));
                dt.Columns.Add("Ape_Fecha", System.Type.GetType("System.DateTime"));
                dt.Columns.Add("Cte_NomComercial", System.Type.GetType("System.String"));
                dt.Columns.Add("Estatus", System.Type.GetType("System.String"));
                dt.Columns.Add("Fecha1", System.Type.GetType("System.DateTime"));
                dt.Columns.Add("Fecha2", System.Type.GetType("System.DateTime"));
                dt.Columns.Add("Folio1", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Folio2", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Ape", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Cd", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Cte", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Emp", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Vencido", System.Type.GetType("System.Int32"));
                dt.Columns.Add("VencidoStr", System.Type.GetType("System.String"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarVencido()
        {
            cmbVencido.Items.Add(new RadComboBoxItem("-- Todos --", "-1"));
            cmbVencido.Items.Add(new RadComboBoxItem("Si", "2"));
            cmbVencido.Items.Add(new RadComboBoxItem("No", "3"));
            cmbVencido.Items.Add(new RadComboBoxItem("Parcial", "1"));
        }
        private void EnviaEmail(int Id_Ape)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                int verificador = -1;
                PrecioEspecial pe = new PrecioEspecial();
                pe.Id_Emp = session.Id_Emp;
                pe.Id_Cd = session.Id_Cd_Ver;
                pe.Id_Ape = Id_Ape;

                CN_PrecioEspecial cn_precioespecial = new CN_PrecioEspecial();
                cn_precioespecial.ConsultaEnvio(ref pe, session.Emp_Cnx, ref verificador);

                if (CambiarEstatus(Id_Ape, "S") != 1)
                {
                    Alerta("Ocurrió un error al intentar realizar la solicitud");
                    return;
                }

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Se ha colocado una solicitud de autorización de precios especiales con el número de solicitud " + Id_Ape);
                if (pe.Ape_Sustituye != null)
                    cuerpo_correo.Append(" que sustituye a la solicitud #" + pe.Ape_Sustituye);

                cuerpo_correo.Append(", de la sucursal " + session.Id_Cd_Ver);
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("<a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "ProPrecioEspecial_Autorizacion.aspx?Id1=" + pe.Ape_Unique + "&Id2=" + session.Id_Emp + "&Id3=" + session.Id_Cd_Ver + "&Id4=1"  + "'>");
                cuerpo_correo.Append("Solicitud de autorización de precios especiales</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                m.To.Add(new MailAddress(ConsultarEmail(pe.Ape_Solicitar)));
                m.CC.Add(new MailAddress("eugenio.escamilla@key.com.mx"));
                m.CC.Add(new MailAddress("juan.campos@key.com.mx"));
                m.Subject = "Solicitud de autorización de precios especiales #" + Id_Ape + " del centro " + session.Id_Cd_Ver;
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);
                }
                catch (Exception)
                {
                    CambiarEstatus(Id_Ape, "C");
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }
                Alerta("Solicitud enviada correctamente");
                rg1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string ConsultarEmail(int id_u)
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CatUsuario cn_catusuario = new CN_CatUsuario();
            Usuario u = new Usuario();
            u.Id_Emp = session.Id_Emp;
            u.Id_Cd = session.Id_Cd_Ver;
            u.Id_U = id_u;
            string correo = "";
            cn_catusuario.ConsultaCorreoUsuario(u, session.Emp_Cnx, ref correo);
            return correo;
        }



        private void autorizar(GridItem gi)
        {
            try
               
            {
                 Sesion session = new Sesion();
                 
                     session = (Sesion)Session["Sesion" + Session.SessionID];
                     int verificador = -1;
                     //string[] url = Request.Url.ToString().Split(new char[] { '/' });
                     //string direccion ;
                     PrecioEspecial pe = new PrecioEspecial();
                     pe.Id_Emp = session.Id_Emp;
                     pe.Id_Cd = session.Id_Cd_Ver;
                     pe.Id_Ape = Convert.ToInt32(gi.Cells[2].Text);
                     int IdT = 2;

                     CN_PrecioEspecial cn_precioespecial = new CN_PrecioEspecial();
                     cn_precioespecial.ConsultaEnvio(ref pe, session.Emp_Cnx, ref verificador);

                     RAM1.ResponseScripts.Add("return AbrirVentana_PrecioEspAutorizacion('" + pe.Ape_Unique + "','" + session.Id_Emp + "','" + session.Id_Cd_Ver + "','" + IdT + "')");


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        
        
        }

        private string ConsultarAutorizacionPrecio()
        {
         try
           {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CatUsuario cn_catusuario = new CN_CatUsuario();
            Usuario u = new Usuario();
            u.Id_Emp = session.Id_Emp;
            u.Id_Cd = session.Id_Cd_Ver;
            u.Id_U = session.Id_U;
            string  Autorizacion = "";
            cn_catusuario.ConsultaAutorizacionPrecio(u, session.Emp_Cnx, ref Autorizacion);
            return  Autorizacion;
               
             }
            catch (Exception ex)
            {
                throw ex;
            }


        }




        private int CambiarEstatus(int Id_Ape, string estatus)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_PrecioEspecial cn_precioespecial = new CN_PrecioEspecial();
                PrecioEspecial ape = new PrecioEspecial();
                ape.Id_Emp = session.Id_Emp;
                ape.Id_Cd = session.Id_Cd_Ver;
                ape.Id_Ape = Id_Ape;
                ape.Ape_Estatus = estatus;
                int verificador = -1;
                cn_precioespecial.EnviarPrecioEspecial(ape, session.Emp_Cnx, ref verificador);
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void editar(GridItem gi)
        {
            try
            {
                if (_PermisoModificar)
                    RAM1.ResponseScripts.Add("return AbrirVentana_PrecioEspecial('" + gi.Cells[2].Text + "', 3)");
                else
                    Alerta("No tiene permiso para editar");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       

        private void traslape(GridItem gi)
        {
            try
            {
                if (gi.Cells[8].Text.ToString().ToLower() != "si")
                {
                    if (gi.Cells[4].Text.ToString().ToLower() == "a" || gi.Cells[4].Text.ToString().ToLower() == "p")
                        RAM1.ResponseScripts.Add("return AbrirVentana_PrecioEspecial('" + gi.Cells[2].Text + "',2)");
                    else
                        Alerta("La solicitud se encuentra en estatus no válido para realizar la sustitución por traslape");
                }
                else
                    Alerta("La solicitud no tiene precios especiales vigentes");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void renovar(GridItem gi)
        {
            try
            {
                if (gi.Cells[8].Text.ToString().ToLower() != "no")
                {
                    if (gi.Cells[4].Text.ToString().ToLower() == "a" || gi.Cells[4].Text.ToString().ToLower() == "p")
                        RAM1.ResponseScripts.Add("return AbrirVentana_PrecioEspecial('" + gi.Cells[2].Text + "',1)");
                    else
                        Alerta("La solicitud se encuentra en estatus no válido para realizar la renovación");
                }
                else
                    Alerta("La solicitud no tiene precios especiales vencidos");
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