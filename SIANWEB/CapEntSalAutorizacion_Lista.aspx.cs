using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class CapEntSalAutorizacion_Lista : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion

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
                        dpFecha1.DbSelectedDate = Sesion.CalendarioIni;
                        dpFecha2.DbSelectedDate = Sesion.CalendarioFin;
                        ValidarPermisos();
                        //LlenarCombos();
                        CargarCentros();

                        if (!Sesion.Cu_Modif_Pass_Voluntario)
                            RadAjaxManager1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");

                        //rgEntSal.Rebind();
                        double ancho = 0;
                        foreach (GridColumn gc in rgEntSal.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgEntSal.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgEntSal.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();

                        RadAjaxManager1.ResponseScripts.Add("IniciarPaginasAuxiliares();");

                        if (Page.Request.QueryString["u"].ToString() != "0")
                        {
                            string ESol_Unique = Page.Request.QueryString["u"].ToString();
                            int Id_ESol = 0;
                            CN_EntSalSolicitud cn_es = new CN_EntSalSolicitud();

                            cn_es.CapEntSalSolicitud_ConsultaFolio(ESol_Unique, ref Id_ESol, Sesion.Emp_Cnx);

                            this.TxtIdESol_Ini.Text = Id_ESol.ToString();
                            this.TxtIdESol_Fin.Text = Id_ESol.ToString();
                            this.dpFecha1.SelectedDate = (DateTime?)null;
                            this.dpFecha2.SelectedDate = (DateTime?)null;
                        }

                        rgEntSal.Rebind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Page_Load");
            }
        }

        #region Eventos
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();

                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                if (sesion.CalendarioIni >= dpFecha1.MinDate && sesion.CalendarioIni <= dpFecha1.MaxDate)
                {
                    dpFecha1.DbSelectedDate = sesion.CalendarioIni;
                }
                if (sesion.CalendarioFin >= dpFecha2.MinDate && sesion.CalendarioFin <= dpFecha2.MaxDate)
                {
                    dpFecha2.DbSelectedDate = sesion.CalendarioFin;
                }

                Session["Sesion" + Session.SessionID] = sesion;

                rgEntSal.Rebind();
                //limpiarCamposBusqueda();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                rgEntSal.Rebind();
            }
            catch (Exception ex)
            {

                throw ex;
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
                    case "new":
                        //nuevo();
                        RadAjaxManager1.ResponseScripts.Add("return AbrirVentana('-1','-1','" + _PermisoGuardar.ToString().ToLower() + "','" + _PermisoModificar.ToString().ToLower() + "','"
                                + _PermisoEliminar.ToString().ToLower() + "','" + _PermisoImprimir.ToString().ToLower() + "','" + 1 + "')");
                        break;
                    case "save":
                        //Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgEntSal_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgEntSal.DataSource = GetList();
                }

            }
            catch (Exception)
            {
                Alerta("Error al cargar los datos");
            }
        }
        protected void rgEntSal_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridItem item = e.Item;
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string id = null;
                DateTime fecha;

                switch (e.CommandName)
                {
                    case "Baja":
                        Baja(e);
                        break;
                    case "Enviar":
                        Enviar(e);
                        break;

                    case "Editar":
                        id = rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["ESol_Unique"].Text;
                        fecha = DateTime.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["ESol_Fecha"].Text);
                        if (rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["ESol_EstatusStr"].Text != "Pendiente")
                        {
                            RadAjaxManager1.ResponseScripts.Add("OpenAlert('El documento se encuentra en estatus no válido para realizar la modificación','" + id + "');");
                            break;
                        }

                        if (!(fecha >= Sesion.CalendarioIni && fecha <= Sesion.CalendarioFin))//validar fecha dentro del periodo
                        {
                            //solo se muestra la info del movimientoEntradaSalida pero no se permite modificar
                            RadAjaxManager1.ResponseScripts.Add("OpenAlert('La fecha del documento esta fuera del periodo y no puede ser modificado','" + id + "');");
                            break;
                        }
                        else
                        {
                            RadAjaxManager1.ResponseScripts.Add("return AbrirVentana('" + id + "','" + 0
                                + "','" + _PermisoGuardar.ToString().ToLower() + "','" + _PermisoModificar.ToString().ToLower() + "','"
                                + _PermisoEliminar.ToString().ToLower() + "','" + _PermisoImprimir.ToString().ToLower() + "','" + 2 + "')");
                            break;
                        }
                    case "Autorizar":

                        id = rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["ESol_Unique"].Text;
                        fecha = DateTime.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["ESol_Fecha"].Text);
                        if (int.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Id_UEnviar"].Text) != Sesion.Id_U &&
                             int.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Id_UCC"].Text) != Sesion.Id_U)
                        {
                            Alerta("No tiene permisos para autorizar el movimiento");
                            return;
                        }


                        if (rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["ESol_EstatusStr"].Text != "Pendiente")
                        {
                            Alerta("Solo se pueden atender solicitudes con estatus <b> Pendiente</b>");
                            return;
                        }
                        else
                        {

                            RadAjaxManager1.ResponseScripts.Add("return AbrirVentana('" + id + "','" + 0
                              + "','" + _PermisoGuardar.ToString().ToLower() + "','" + _PermisoModificar.ToString().ToLower() + "','"
                              + _PermisoEliminar.ToString().ToLower() + "','" + _PermisoImprimir.ToString().ToLower() + "','" + 3 + "')");
                            break;
                        }

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rgEntSal_ItemCommand");
            }
        }
        protected void rgEntSal_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                rgEntSal.Rebind();

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                        rgEntSal.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        #endregion

        #region Funciones
        private List<EntSalSolicitud> GetList()
        {
            try
            {
                List<EntSalSolicitud> List = new List<EntSalSolicitud>();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_EntSalSolicitud cn_es = new CN_EntSalSolicitud();
                EntSalSolicitud es = new EntSalSolicitud();
                es.Id_Cd = sesion.Id_Cd_Ver;
                es.Id_ESolIni = this.TxtIdESol_Ini.Text == "" ? (int?)null : int.Parse(TxtIdESol_Ini.Text);
                es.Id_ESolFin = this.TxtIdESol_Fin.Text == "" ? (int?)null : int.Parse(TxtIdESol_Fin.Text);
                es.ESol_FechaIni = this.dpFecha1.SelectedDate.ToString() == "" ? (DateTime?)null : dpFecha1.SelectedDate;
                es.ESol_FechaFin = this.dpFecha2.SelectedDate.ToString() == "" ? (DateTime?)null : dpFecha2.SelectedDate;
                es.ESol_Estatus = this.cmbEstatus.SelectedValue;

                cn_es.CapEntSalSolicitud_ConsultaLista(es, ref List, sesion.Emp_Cnx);

                return List;
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
                //string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                //if (pag.Length > 1)
                //    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                //else
                //    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                pagina.Url = "CapEntSalAutorizacion_Lista.aspx?u=0";
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

                    if (Permiso.PGrabar)
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("new")).Visible = _PermisoGuardar;
                }
                else
                    Response.Redirect("Inicio.aspx");
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
        private void Baja(GridCommandEventArgs e)
        {
            try
            {

                if (rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["ESol_EstatusStr"].Text != "Pendiente")
                {
                    Alerta("Solo se pueden cancelar solicitudes con estatus Pendiente");
                    return;
                }

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                EntSalSolicitud es = new EntSalSolicitud();
                CN_EntSalSolicitud cn_es = new CN_EntSalSolicitud();
                int Verificador = 0;
                es.Id_Cd = int.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Id_Cd"].Text);
                es.Id_ESol = int.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Id_ESol"].Text);

                cn_es.CapEntSalSolicitud_Cancelar(es, ref Verificador, sesion.Emp_Cnx);

                if (Verificador == -1)
                {
                    rgEntSal.Rebind();
                    Alerta("Se ha cancelado la solicitud correctamente");
                }
                else
                {
                    Alerta("Error al cancelar la solicitud");
                }


            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }

        }
        private void Enviar(GridCommandEventArgs e)
        {
            try
            {

                if (rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["ESol_EstatusStr"].Text != "Pendiente")
                {
                    Alerta("Solo se pueden enviar solicitudes en estatus pendiente");
                    return;
                }

                int Id_ESol = int.Parse(rgEntSal.MasterTableView.Items[e.Item.ItemIndex]["Id_ESol"].Text);
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_EntSalSolicitud cn_es = new CN_EntSalSolicitud();
                EntSalSolicitud es = new EntSalSolicitud();

                cn_es.CapEntSolicitud_ConsultaDatosEnvio(sesion.Id_Cd_Ver, Id_ESol, ref es, sesion.Emp_Cnx);

                string[] url = Request.Url.ToString().Split(new char[] { '/' });
                string URL = Request.Url.ToString().Replace(url[url.Length - 1], "");


                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = sesion.Id_Cd_Ver;
                configuracion.Id_Emp = sesion.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, sesion.Emp_Cnx);

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<Table> <tr><td><b></b><td></tr> <tr><td><b>");
                cuerpo_correo.Append("Se ha colocado la solicitud de movimiento de almacén con el folio #" + Id_ESol + "");
                cuerpo_correo.Append("</b><td></tr><tr><td>&nbsp;<td></tr><tr><td>");
                //cuerpo_correo.Append("<a href=" + URL + "CapEntSalAutorizacion_Lista.aspx?u=" + es.ESol_Unique + ">Ver solicitud de autorización</a>");
                cuerpo_correo.Append("<a href=" + URL + "CapEntSalAutorizacion.aspx?Id=" + es.ESol_Unique + "&Es_Naturaleza=0&PermisoGuardar=true&PermisoModificar=true&PermisoEliminar=true&PermisoImprimir=true&TipoOp=3 >Ver solicitud de autorización</a>");
                  cuerpo_correo.Append("</td></tr></Table>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                m.To.Add(new MailAddress(es.ESol_CorreoDest));
                //  m.To.Add(new MailAddress("jmartinez@axsistec.com"));
                if (es.ESol_CorreoCC.Length > 1)
                {
                    m.CC.Add(new MailAddress(es.ESol_CorreoCC));
                }
                //m.Subject = "Solicitud de autorización de precios especiales";
                m.Subject = "Solicitud de movimiento de almacén #" + Id_ESol + " " + sesion.U_Nombre;
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
                catch (Exception ex)
                {


                }



            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }

        }
        private void Inicializar()
        {//funcion usada para el refresh del grid , despues de cambiar la pantalla "anidada"
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion.CalendarioIni >= dpFecha1.MinDate && Sesion.CalendarioIni <= dpFecha1.MaxDate)
                {
                    dpFecha1.DbSelectedDate = Sesion.CalendarioIni;
                }
                if (Sesion.CalendarioFin >= dpFecha2.MinDate && Sesion.CalendarioFin <= dpFecha2.MaxDate)
                {
                    dpFecha2.DbSelectedDate = Sesion.CalendarioFin;
                }


                rgEntSal.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ErrorManager
        private void RadConfirm(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("radconfirm('" + mensaje + "', confirmCallBackFn);");
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