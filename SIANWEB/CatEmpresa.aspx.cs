using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using CapaDatos;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;

namespace SIANWEB
{
    public partial class CatEmpresa : System.Web.UI.Page
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

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        //ConsultarEmpresa(Sesion);

                        CargarEmpresas();
                        CargarCentros(Sesion.Emp_Cnx);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbEmpresa_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (cmbEmpresa.SelectedIndex == 0)
                {
                    Nuevo();
                    return;
                }
                CN_Empresa clsCatEmpresa = new CN_Empresa();
                Empresa empresa = new Empresa();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                empresa.Id_Emp = Convert.ToInt32(cmbEmpresa.SelectedValue);
                clsCatEmpresa.ConsultaEmpresas(ref empresa, session2.Emp_Cnx);

                this.HFId_Empresa.Value = empresa.Id_Emp.ToString();
                txtNombre.Text = empresa.Emp_Nombre;
                txtFleteLocales.Value = empresa.Emp_GastoLocal;
                txtGAdmitivos.Value = empresa.Emp_GastoAdmin;
                txtCostosFijosPapel.Value = empresa.Emp_ContribucionPapel;
                txtCostosFijosNoPapel.Value = empresa.Emp_ContribucionNoPapel;
                txtUcs.Value = empresa.Emp_Ucs;
                txtIsr.Value = empresa.Emp_Isr;
                txtInversionActivosFijos.Value = empresa.Emp_Inversion;
                txtCetes.Value = empresa.Emp_Cetes;
                txtAdicionalCetes.Value = empresa.Emp_AdicionalCetes;
                empresa.Emp_Activo = chkActivo.Checked;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    ErrorManager();
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
                ErrorManager(ex, "RadToolBar1_ButtonClick");
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN__Comun comun = new CN__Comun();
            comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
        }
        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked && HFId_Empresa.Value != "")
            {
                if (!Deshabilitar())
                {
                    Alerta("El registro está siendo utilizado por otro componente");
                    ((CheckBox)sender).Checked = true;
                }
            }
        }
        #endregion
        #region Funciones
        private void CargarEmpresas()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(-1, Sesion.Emp_Cnx, "spCatEmpresaCombo", ref cmbEmpresa);
                if (HFId_Empresa.Value != "")
                    cmbEmpresa.Text = cmbEmpresa.FindItemByValue(HFId_Empresa.Value).Text;
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

                CD_PermisosU CN_PermisosU = new CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (Permiso.PGrabar == false)
                    {
                        this.RadToolBar1.Items[6].Visible = false;
                    }
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    {
                        this.RadToolBar1.Items[5].Visible = false;
                    }
                    //Regresar
                    this.RadToolBar1.Items[4].Visible = false;
                    //Eliminar
                    this.RadToolBar1.Items[3].Visible = false;
                    //Imprimir
                    this.RadToolBar1.Items[2].Visible = false;
                    //Correo
                    this.RadToolBar1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");
                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Nuevo()
        {
            try
            {
                cmbEmpresa.SelectedIndex = -1;
                this.HFId_Empresa.Value = string.Empty;
                txtNombre.Text = string.Empty;
                chkActivo.Checked = true;
                txtAdicionalCetes.Text = "";
                txtCetes.Text = "";
                txtCostosFijosNoPapel.Text = "";
                txtCostosFijosPapel.Text = "";
                txtFleteLocales.Text = "";
                txtGAdmitivos.Text = "";
                txtInversionActivosFijos.Text = "";
                txtIsr.Text = "";
                txtUcs.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Guardar()
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Empresa clsCatEmpresa = new CN_Empresa();
            Empresa empresa = new Empresa();
            empresa.Emp_Nombre = txtNombre.Text;
            empresa.Emp_Activo = chkActivo.Checked;
            empresa.Emp_GastoLocal = txtFleteLocales.Value;
            empresa.Emp_GastoAdmin = txtGAdmitivos.Value;
            empresa.Emp_ContribucionPapel = txtCostosFijosPapel.Value;
            empresa.Emp_ContribucionNoPapel = txtCostosFijosNoPapel.Value;
            empresa.Emp_Ucs = txtUcs.Value;
            empresa.Emp_Isr = txtIsr.Value;
            empresa.Emp_Inversion = txtInversionActivosFijos.Value;
            empresa.Emp_Cetes = txtCetes.Value;
            empresa.Emp_AdicionalCetes = txtAdicionalCetes.Value;
            Int32 verificador = 0;
            //Nueva empresa
            if (this.HFId_Empresa.Value == string.Empty)
            {
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }
                clsCatEmpresa.InsertarEmpresa(ref empresa, sesion.Emp_Cnx, ref verificador);
                if (verificador == 1)
                {
                    this.txtNombre.Focus();
                    Alerta("La empresa introducida ya se encuentra registrada");
                }
                else
                {
                    // Alerta("Usuario admin_" + verificador.ToString() + " fue creado");
                    Nuevo();
                    Alerta("Los datos se guardaron correctamente");
                    CargarEmpresas();
                }

            }
            else
            {
                if (!_PermisoModificar)
                {
                    Alerta("No tiene permisos para modificar");
                    return;
                }
                empresa.Id_Emp = Convert.ToInt32(this.HFId_Empresa.Value);
                clsCatEmpresa.ModificarUsuario(empresa, sesion.Emp_Cnx, ref verificador);
                if (verificador == 1)
                {
                    this.txtNombre.Focus();
                    Alerta("La empresa introducida ya se encuentra registrada");
                }
                else
                {
                    Alerta("Los datos se modificaron correctamente");
                    CargarEmpresas();
                }
            }
        }
        private void DatosParaCorreo(ref Usuario Usuario, Int32 Verificador, string conexion)
        {
            try
            {
                Int32 Id = default(Int32);
                ConfiguracionGlobal Configuracion = new ConfiguracionGlobal();
                CentroDistribucion Cdis = new CentroDistribucion();
                CapaNegocios.CN_Login CN_login = new CapaNegocios.CN_Login();
                Id = 0;
                CN_login.RecuperarContraseña(ref Usuario, ref Cdis, ref Configuracion, out Id, conexion);
                EnviaEmail(Usuario, Cdis, Configuracion, Verificador.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EnviaEmail(Usuario Usuario, CentroDistribucion Cdis, ConfiguracionGlobal Configuracion, string Password)
        {
            try
            {
                //*****Saco el email ********

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                SmtpClient smtp = new SmtpClient();
                correo.From = new MailAddress(Configuracion.Mail_Remitente);
                correo.To.Add(Usuario.U_Correo);
                correo.Subject = ("Cuenta de acceso a Nuevo Sian");
                //correo.Body = ("<IMG SRC='cid:companylogo' ALIGN='left'><br><br><br><br><br><br><b><font face= 'Tahoma' size = '3'>Bievenido al CRM de APEX, A.C. / Welcome to APEX's CRM</font><br><br><font face= 'Tahoma' size = '2'>" & txtComentario.Text.Trim & "<br><br>Anexo se encuentra su usuario y contraseña/ Enclosed is your user and password<br><br><br>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Usuario/User: &nbsp<font face= 'Tahoma' size = '2' color='#777777'>" & txtusername.Text.Trim & "</font><br>Contraseña/Password: &nbsp<font face= 'Tahoma' size = '2' color='#777777'>" & txtcontraseña.Text.Trim & "</font><br><br><br>APEX, A.C.<br>TEL/PH (81) 8369 66660,64 & 65<br>FAX (81) 8369 6732<br><br><a href='http://www.apex.org.mx'>www.apex.org.mx</a></font></b>")
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString("" + "<div align='center'>" + " <table>" + "  <tr>" + "   <td><IMG SRC=\"cid:companylogo\" ALIGN='left'></td>" + "   <td valign='middle' style='text-decoration: underline'><b><font face= 'Tahoma' size = '4'>Cuenta de acceso a CRM</font></b></td>" + "  </tr>" + "  <tr>" + "   <td colspan='2'><br><br><br></td>" + "  </tr>" + "  <tr>" + "   <td colspan='2'><b><font face= 'Tahoma' size = '2'>Anexo se encuentra el usuario y contraseña de su cuenta de acceso</font></b></td>" + "  </tr>" + "  <tr>" + "   <td colspan='2'><br><br></td>" + "  </tr>" + "  <tr>" + "   <td align='right'><b><font face= 'Tahoma' size = '2'>Usuario:</font></b></td>" + "   <td align='left'><b><font face= 'Tahoma' size = '2' color='#777777'>" + Usuario.Cu_User + "</font></b></td>" + "  </tr>" + "  <tr>" + "   <td align='right'><b><font face= 'Tahoma' size = '2'>Contraseña:</font></b></td><td align='left'><b><font face= 'Tahoma' size = '2' color='#777777'>" + Usuario.Cu_pass + "</font></b></td>" + "  </tr>" + "  <tr>" + "   <td colspan='2'><br><br></td>" + "  </tr>" + "  <tr>" + "   <td align ='center' colspan='2'><b><font face= 'Tahoma' size = '2'></b></td>" + "  </tr>" + "  <tr>" + "   <td align ='Left' colspan='2'><b><font face= 'Tahoma' size = '2'>" + Cdis.Cd_Descripcion + "</font></b></td>" + "  </tr>" + "  <tr>" + "  <tr>" + "   <td align ='Left' colspan='2'><b><font face= 'Tahoma' size = '2'>" + Cdis.Cd_Tel + "</font></b></td>" + "  </tr>" + "  <tr>" + "   <td align ='Left' colspan='2'><b><font face= 'Tahoma' size = '2'></font></b>" + "   </td>" + "  </tr>" + "  <tr>" + "   <td align ='Left' colspan='2'><b><font face= 'Tahoma' size = '2'>" + Cdis.Cd_CalleNo + "</font></b></td>" + "  </tr>" + " </table>" + "</div>", null, "text/html");

                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    htmlView.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }
                //add the LinkedResource to the appropriate view
                correo.AlternateViews.Add(htmlView);
                correo.IsBodyHtml = true;

                smtp.Host = Configuracion.Mail_Servidor;
                smtp.Port = Convert.ToInt32(Configuracion.Mail_Puerto);
                //Estoy hay que ponerlo cuando se ponga en un host para que si lo mande
                smtp.Credentials = new System.Net.NetworkCredential(Configuracion.Mail_Usuario, Configuracion.Mail_Contraseña);
                smtp.EnableSsl = false;
                smtp.Send(correo);
            }
            catch (Exception)
            {
                Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                //Throw ex
            }
        }
        private void CargarCentros(string Conexion)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();


                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Conexion, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;

                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Conexion, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool Deshabilitar()
        {
            try
            {
                bool verificador = false;
                if (HFId_Empresa.Value != "")
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = -1;
                    ct.Id_Cd = -1;
                    ct.Id = Convert.ToInt32(this.HFId_Empresa.Value);
                    ct.Tabla = "CatEmpresa";
                    ct.Columna = "Id_Emp";
                    CN_Comun.Deshabilitar(ct, Sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
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