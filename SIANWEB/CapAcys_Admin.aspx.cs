using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Collections;
using CapaDatos;
using System.Globalization;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace SIANWEB
{
    public partial class CapAcys_admin : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private Sesion sesion { get { return (Sesion)Session["Sesion" + Session.SessionID]; } set { Session["Sesion" + Session.SessionID] = value; } }


        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ValidarSesion();
                if (!Page.IsPostBack)
                {
                    ValidarPermisos();
                    if (sesion.Cu_Modif_Pass_Voluntario == false)
                    {
                        RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                        return;
                    }
                    CargarCentros();
                    Inicializar();
                    Random randObj = new Random(DateTime.Now.Millisecond);
                    HF_ClvPag.Value = randObj.Next().ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ValidarSesion();

                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();

                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Inicializar();


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void CargarVencido()
        {
            cmbVencido.Items.Clear();
            cmbVencido.Items.Add(new RadComboBoxItem("-- Todos --", "-1"));
            cmbVencido.Items.Add(new RadComboBoxItem("SI", "SI"));
            cmbVencido.Items.Add(new RadComboBoxItem("No", "NO"));

        }
        protected void rg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgAcuerdo.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgAcuerdo.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (txtFolio1.Value > txtFolio2.Value)
            {
                Alerta("La clave del acys inicial no debe ser mayor a la clave del acys final");
                return;
            }
            if (txtFecha1.SelectedDate > txtFecha2.SelectedDate)
            {
                Alerta("La fecha inicial no debe ser mayor a la fecha final");
                return;
            }
            try
            {
                this.rgAcuerdo.Rebind();
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
                        Inicializar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rg_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Session["Id_Buscar" + Session.SessionID] = null;
                Session["IdVersion_Buscar" + Session.SessionID] = null;
                Session["FechaVersion_Buscar" + Session.SessionID] = null;
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                DateTime fechaPeriodoInicio = session2.CalendarioIni;
                DateTime fechaPeriodoFinal = session2.CalendarioFin;
                GridItem gi = e.Item;
                List<string> statusPosibles;
                switch (e.CommandName)
                {
                    case "Imprimir":
                        statusPosibles = new List<string>() { "C", "I", "S", "A", "B", "R" };
                        if (!statusPosibles.Contains(gi.Cells[4].Text.ToUpper()))
                        {
                            Alerta("El acuerdo se encuentra en estatus no válido para realizar la impresión");
                            e.Canceled = true;
                            return;
                        }
                        Imprimir(gi.Cells[2].Text.ToUpper());
                        break;
                    case "Cancelar":
                        if (gi.Cells[4].Text == "B")
                        {
                            Alerta("El acuerdo se encuentra en estatus no válido para realizar la cancelación");
                            e.Canceled = true;
                            return;
                        }
                        int verificador = -1;
                        CN_CapAcys cn_capacys = new CN_CapAcys();
                        Acys acys = new Acys();
                        acys.Id_Emp = sesion.Id_Emp;
                        acys.Id_Cd = sesion.Id_Cd_Ver;
                        acys.Id_Acs = Convert.ToInt32(gi.Cells[2].Text);
                        cn_capacys.Cancelar(acys, sesion.Emp_Cnx, ref  verificador);


                        if (verificador == 1)
                            rgAcuerdo.Rebind();
                        else
                            Alerta("Ocurrió un error al intentar cancelar el acuerdo");
                        break;
                    case "Editar":
                        //RBM: se valida que el usuario sea de tipo RIK
                        //para editar la Acys sin importar el estaus
                        if (session2.Id_TU == 2) // || session2.Id_TU == 12)
                        {
                            RAM1.ResponseScripts.Add("return AbrirVentana_Acys('" + gi.Cells[2].Text + "',0, '" + gi.Cells[4].Text + "')");
                            break;
                        }
                        else
                        {
                            statusPosibles = new List<string>() { "C", "I" };
                            if (!statusPosibles.Contains(gi.Cells[4].Text.ToUpper()))
                            {
                                //Alerta("El acuerdo se encuentra en estatus no válido para realizar la modificación");
                                RAM1.ResponseScripts.Add("OpenAlert('El acuerdo se encuentra en estatus no válido para realizar la modificación','" + gi.Cells[2].Text + "','" + false + "','" + false + "','" + false + "','" + false + "')");
                                e.Canceled = true;
                                return;
                            }
                            RAM1.ResponseScripts.Add("return AbrirVentana_Acys('" + gi.Cells[2].Text + "',0)");
                            break;
                        }
                    case "Enviar":
                        statusPosibles = new List<string>() { "I", "S", "C" };
                        if (statusPosibles.Contains(gi.Cells[4].Text.ToUpper()))
                        {
                            EnviaEmail(Convert.ToInt32(gi.Cells[2].Text), Convert.ToInt32(gi.Cells[3].Text));
                        }
                        else
                        {
                            Alerta("La solicitud se encuentra en estatus no válido para su envio");
                        }
                        break;
                    case "Renovar":
                        renovar(gi);
                        break;
                    case "Autorizar":
                        autorizar(gi);
                        break;
                    case "Actualizar":
                        statusPosibles = new List<string>() { "A" };
                        if (statusPosibles.Contains(gi.Cells[4].Text.ToUpper()))
                        {
                            actualizar(gi);
                        }
                        else
                        {
                            Alerta("La solicitud se encuentra en estatus no válido para su envio");
                        }
                        break;
                    case "Ver_Versiones":
                        versiones(gi);
                        break;


                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "print")
                {
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
        protected void rg_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                WebControl Button = default(WebControl);
                string clickHandler = "";

                Button = (WebControl)item["Cancelar"].Controls[0];
                clickHandler = Button.Attributes["onclick"];
                string original = "[[MSG]]";
                string cambio = "¿Está seguro de dar de baja el acuerdo <b>#" + item.GetDataKeyValue("Id_Acs").ToString() + "</b> con el cliente <b>#" + item.GetDataKeyValue("Id_Cte").ToString() + "</b> para el territorio <b>#" + item.GetDataKeyValue("Id_Ter").ToString() + "</b>?";
                Button.Attributes["onclick"] = clickHandler.Replace(original, cambio);
            }
        }
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Clientes cte = new Clientes();
                cte.Id_Cte = Convert.ToInt32((sender as RadNumericTextBox).Value.HasValue ? (sender as RadNumericTextBox).Value : -1);
                cte.Id_Emp = sesion.Id_Emp;
                cte.Id_Cd = sesion.Id_Cd_Ver;
                CN_CatCliente cnCliente = new CN_CatCliente();
                cnCliente.ConsultaClientes(ref cte, sesion.Emp_Cnx);
                if (cte.Cte_NomComercial != null)
                {
                    txtClienteNombre.Text = cte.Cte_NomComercial;
                    txtFolio1.Focus();
                }
                else
                {
                    txtClienteNombre.Text = "";
                    Alerta("El cliente no existe o esta inactivo");
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones
        private void ValidarSesion()
        {
            try
            {
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void EnviaEmail(int Id_Acys, int Id_AcsVersion)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                int verificador = -1;
                Acys acys = new Acys();
                acys.Id_Emp = session.Id_Emp;
                acys.Id_Cd = session.Id_Cd_Ver;
                acys.Id_Acs = Id_Acys;

                CN_CapAcys clsCapAcys = new CN_CapAcys();
                clsCapAcys.ConsultaUltimaVersion(ref acys, sesion.Emp_Cnx);


                /* CN_CapAcys cn_acys = new CN_CapAcys();
                 cn_acys.ConsultaEnvio(ref acys, session.Emp_Cnx, ref verificador);*/

                if (CambiarEstatus(acys.Id_Acs, acys.Id_AcsVersion, "S") != 1)
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
                cuerpo_correo.Append("Se ha colocado una solicitud de autorización de acuerdo comercial con el número  " + Id_Acys);
                /*if (acys.Acs_Sustituye != null)
                    cuerpo_correo.Append(" que sustituye a la solicitud #" + acys.Acs_Sustituye);*/

                cuerpo_correo.Append(", de la sucursal " + session.Id_Cd_Ver);
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("<a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "CapAcys.aspx?Id=" + Id_Acys + "&Accion=2&email=1&PermisoGuardar=true&PermisoModificar=true&PermisoEliminar=true&PermisoImprimir=true'" + ">");
                cuerpo_correo.Append("Solicitud de autorización de acuerdos comerciales</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                m.To.Add(new MailAddress(configuracion.Mail_Acys));
                m.Subject = "Solicitud de autorización de Acuerdo Comercial #" + Id_Acys + " del centro " + session.Id_Cd_Ver;
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
                    CambiarEstatus(Id_Acys, Id_AcsVersion, "C");
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }
                Alerta("Solicitud enviada correctamente");
                rgAcuerdo.Rebind();
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


        private int CambiarEstatus(int Id_Acs, int Id_AcsVersion, string estatus)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapAcys cn_acys = new CN_CapAcys();
                Acys acys = new Acys();
                acys.Id_Emp = session.Id_Emp;
                acys.Id_Cd = session.Id_Cd_Ver;
                acys.Id_Acs = Id_Acs;
                acys.Id_AcsVersion = Id_AcsVersion;
                acys.Acs_Estatus = estatus;
                int verificador = -1;
                cn_acys.actualizarEstatus(acys, session.Emp_Cnx, ref verificador);
                return verificador;
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
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
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
            try
            {
                cmbEstatus.Items.Clear();
                cmbEstatus.Items.Add(new RadComboBoxItem("-- Todos --", ""));
                cmbEstatus.Items.Add(new RadComboBoxItem("Capturado", "C"));
                cmbEstatus.Items.Add(new RadComboBoxItem("Cancelado", "B"));
                cmbEstatus.Items.Add(new RadComboBoxItem("Solicitado", "S"));
                cmbEstatus.Items.Add(new RadComboBoxItem("Autorizado", "A"));
                cmbEstatus.Items.Add(new RadComboBoxItem("Rechazado", "R"));
                cmbEstatus.Sort = RadComboBoxSort.Ascending;
                cmbEstatus.SortItems();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Acys> GetList()
        {
            try
            {
                List<Acys> List = new List<Acys>();
                CN_CapAcys clsCapAcys = new CN_CapAcys();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Acys acys = new Acys();
                acys.Id_Emp = session2.Id_Emp;
                acys.Id_Cd = session2.Id_Cd_Ver;
                acys.Filtro_FecIni = txtFecha1.SelectedDate;
                acys.Filtro_FecFin = txtFecha2.SelectedDate;
                acys.Filtro_Estatus = cmbEstatus.SelectedValue == "-1" ? "" : cmbEstatus.SelectedValue;
                acys.Filtro_FolIni = txtFolio1.Text;
                acys.Filtro_FolFin = txtFolio2.Text;
                acys.Filtro_usuario = session2.Propia ? session2.Id_U.ToString() : "";
                acys.Id_Ter = cmbTerritorio.Text != "" && cmbTerritorio.SelectedValue != "-1" ? Convert.ToInt32(cmbTerritorio.SelectedValue) : -1;
                acys.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                acys.Id_Rik = cmbRepresentante.Text != "" && cmbRepresentante.SelectedValue != "-1" ? Convert.ToInt32(cmbRepresentante.SelectedValue) : -1;
                acys.Acs_Vencido = cmbVencido.Text != "" && cmbVencido.SelectedValue != "-1" ? cmbVencido.SelectedValue : "-1";
                acys.Id_Modalidad = cmbTipoModalidad.Text != "" && cmbTipoModalidad.SelectedValue != "0" ? Convert.ToInt32(cmbTipoModalidad.SelectedValue) : 0;
                clsCapAcys.ConsultarAcys_Lista(acys, session2.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Inicializar()
        {
            try
            {
                CargarEstatus();
                CargarTerritorios();
                CargarRik();
                CargarVencido();
                //Edsg 12102015
                CapaNegocios.CN_CatCliente cncliente = new CN_CatCliente();
                List<TipoVenta> modOpList = new List<TipoVenta>();
                cncliente.ConsultaModalidadOP(sesion.Emp_Cnx, ref modOpList);

                this.cmbTipoModalidad.DataSource = modOpList;
                this.cmbTipoModalidad.DataTextField = "nombre";
                this.cmbTipoModalidad.DataValueField = "id";
                this.cmbTipoModalidad.DataBind();
                if (!Page.IsPostBack)
                {
                    if (sesion.CalendarioIni >= txtFecha1.MinDate && sesion.CalendarioIni <= txtFecha1.MaxDate)
                        txtFecha1.DbSelectedDate = sesion.CalendarioIni;
                    if (sesion.CalendarioFin >= txtFecha2.MinDate && sesion.CalendarioFin <= txtFecha2.MaxDate)
                        txtFecha2.DbSelectedDate = sesion.CalendarioFin;
                }
                double ancho = 0;
                foreach (GridColumn gc in rgAcuerdo.Columns)
                {
                    if (gc.Display)
                    {
                        ancho = ancho + gc.HeaderStyle.Width.Value;
                    }
                }
                rgAcuerdo.Width = Unit.Pixel(Convert.ToInt32(ancho));
                rgAcuerdo.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                rgAcuerdo.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Imprimir(string Id_Acs)
        {
            try
            {
                CultureInfo cultura = CultureInfo.CurrentCulture;
                Funciones funcion = new Funciones();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();


                CN_CapAcys clsCapAcys = new CN_CapAcys();
                Acys acys = new Acys();
                acys.Id_Emp = sesion.Id_Emp;
                acys.Id_Cd = sesion.Id_Cd_Ver;
                acys.Id_Acs = Convert.ToInt32(Id_Acs);
                clsCapAcys.ConsultaUltimaVersion(ref acys, sesion.Emp_Cnx);



                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(acys.Id_Emp);
                ALValorParametrosInternos.Add(acys.Id_Cd);
                ALValorParametrosInternos.Add(acys.Id_Acs);

                ALValorParametrosInternos.Add(funcion.GetLocalDateTime(sesion.Minutos).ToString("dd"));
                ALValorParametrosInternos.Add(funcion.GetLocalDateTime(sesion.Minutos).ToString("MM"));
                ALValorParametrosInternos.Add(funcion.GetLocalDateTime(sesion.Minutos).ToString("yyyy"));
                ALValorParametrosInternos.Add(acys.Id_AcsVersion);

                Type instance = null;
                instance = typeof(LibreriaReportes.AcuerdoImpresion);
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;

                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
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
                if (gi.Cells[13].Text.ToString().ToLower() != "no")
                {
                    if (gi.Cells[4].Text.ToString().ToLower() == "i" || gi.Cells[4].Text.ToString().ToLower() == "a")
                        RAM1.ResponseScripts.Add("return AbrirVentana_Acys('" + gi.Cells[2].Text + "',1)");
                    else
                        Alerta("El Acuerdo comercial se encuentra en estatus no válido para realizar la renovación");
                }
                else
                    Alerta("El Acuerdo comercial aún se encuentra Vigente");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void actualizar(GridItem gi)
        {
            try
            {
                if (gi.Cells[13].Text.ToString().ToLower() != "si")
                {
                    if (gi.Cells[4].Text.ToString().ToLower() == "i" || gi.Cells[4].Text.ToString().ToLower() == "a")
                        RAM1.ResponseScripts.Add("return AbrirVentana_Acys('" + gi.Cells[2].Text + "',4)");
                    else
                        Alerta("El Acuerdo comercial se encuentra en estatus no válido para realizar la actualización");
                }
                else
                    Alerta("El Acuerdo comercial no se encuentra Vigente para actualizar");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void versiones(GridItem gi)
        {
            try
            {

                RAM1.ResponseScripts.Add("return abrirVersiones('" + gi.Cells[2].Text + "')");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void autorizar(GridItem gi)
        {
            try
            {
                if (gi.Cells[13].Text.ToString().ToLower() != "si")
                {
                    if (gi.Cells[4].Text.ToString().ToLower() == "s" || gi.Cells[4].Text.ToString().ToLower() == "a" || gi.Cells[4].Text.ToString().ToLower() == "r")
                        RAM1.ResponseScripts.Add("return AbrirVentana_Acys('" + gi.Cells[2].Text + "',2)");
                    else
                        Alerta("El Acuerdo comercial se encuentra en estatus no válido para realizar la autorización");
                }
                else
                    Alerta("El Acuerdo comercial no se encuentra Vigente");
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
                        this.rtb1.Items[6].Visible = false;
                    //Guardar
                    this.rtb1.Items[5].Visible = false;
                    //Regresar
                    this.rtb1.Items[4].Visible = false;
                    //Eliminar
                    this.rtb1.Items[3].Visible = false;
                    //Imprimir
                    this.rtb1.Items[2].Visible = false;
                    //Correo
                    this.rtb1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTerritorios()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatTerritorio_Combo", ref cmbTerritorio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarRik()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Id_TU == 2 ? Sesion.Id_U : (int?)null, Sesion.Emp_Cnx, "spCatRik_Combo", ref cmbRepresentante);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region ErrorManager
        //private void RadConfirm(string mensaje)
        //{
        //    try
        //    {

        //        RAM1.ResponseScripts.Add("radconfirm('" + mensaje + "', confirmCallBackFn);");
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager(ex, "Alerta");
        //    }

        //}
        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "</br></br>', 330, 150);");
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