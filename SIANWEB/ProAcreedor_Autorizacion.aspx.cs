using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using CapaDatos;
using System.Collections;
using System.Net.Mail;
using System.Text;
using System.Net;


namespace SIANWEB
{
    public partial class ProAcreedor_Autorizacion : System.Web.UI.Page
    {
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

        protected void rgAcreedor_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgAcreedor.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        
        protected void rgAcreedor_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() == "Autorizar")
                {
                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];

                    Acreedor acreedor = new Acreedor();

                    int item = e.Item.ItemIndex;

                    if (((RadTextBox)(rgAcreedor.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text != string.Empty)
                    {
                        acreedor.Acr_NumeroGenerado = ((RadTextBox)(rgAcreedor.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text.Trim().ToUpper();
                        acreedor.Id_Acr = Int32.Parse(rgAcreedor.Items[item]["Id_Acr"].Text);
                        acreedor.Id_Emp = Int32.Parse(rgAcreedor.Items[item]["Id_Emp"].Text);
                        acreedor.Id_Cd = Int32.Parse(rgAcreedor.Items[item]["Id_Cd"].Text);
                        acreedor.Acr_Tipo = Int32.Parse(rgAcreedor.Items[item]["Acr_Tipo"].Text);
                        acreedor.Acr_Nombre = rgAcreedor.Items[item]["Acr_Nombre"].Text;

                        int verificador = -1;

                        CN_CatAcreedor clsAcreedor = new CN_CatAcreedor();

                        clsAcreedor.AutorizarAcreedor(acreedor, session.Emp_Cnx, ref verificador);

                        EnviarCorreo(acreedor.Acr_Tipo.ToString(), acreedor.Acr_Nombre, 1, acreedor.Acr_NumeroGenerado);
                                                
                        if (verificador == 1)
                        {
                            rgAcreedor.Rebind();
                        }
                    }
                    else
                    {
                        Alerta("Capture el Número del Acreedor");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //JFCV 05 oct 2015 Inicio
        //protected void rgAcreedor_PageIndexChanged(object source, GridPageChangedEventArgs e)
        //{
        //    try
        //    {
        //        ErrorManager();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager(ex, "rg1_PageIndexChanged");
        //    }
        //}


        protected void rgAcreedor_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {

                rgAcreedor.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //JFCV 05 oct 2015 Fin


        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
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
                    //_PermisoGuardar = Permiso.PGrabar;
                    //_PermisoModificar = Permiso.PModificar;
                    //_PermisoEliminar = Permiso.PEliminar;
                    //_PermisoImprimir = Permiso.PImprimir;

                    //if (Permiso.PGrabar == false)
                    //{
                    //    this.rtb1.Items[6].Visible = false;
                    //}
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

        private void Inicializar()
        {
            rgAcreedor.Rebind();
        }

        private List<Acreedor> GetList()
        {
            try
            {
                CN_CatAcreedor clsAcreedor = new CN_CatAcreedor();
                List<Acreedor> list = new List<Acreedor>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                Acreedor acreedor = new Acreedor();
                acreedor.Id_Emp = session.Id_Emp;
                acreedor.Id_Cd = session.Id_Cd_Ver;

                clsAcreedor.ConsultaAcreedor(acreedor, session.Emp_Cnx, ref list);

                return list.Where(x => x.Acr_Autorizado == false).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
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

        //JFCV 10 julio 2016 agregar que envíe correo cuando se autorice un acreedor o proveedor

        private void EnviarCorreo(string tipoProveedor, string nombreProveedor, int autorizada, string acr_NumeroGenerado)
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
            cuerpo_correo.Append("<td> Se le informa que el {cmbTipo}: {TxtNombre}</td>");
            cuerpo_correo.Append("</tr>");
            cuerpo_correo.Append("<tr>");
            cuerpo_correo.Append("<td><br></td>");
            cuerpo_correo.Append("</tr>");
            cuerpo_correo.Append("<tr>");
            if (autorizada == 1)
            {
                cuerpo_correo.Append("<td>Ha sido Autorizado. Y se le asigno el número: {txtNumeroGenerado}</td>");
            }
            else
            {
                //actualmente no existe el proceso de rechazar un proveedor o acreedor
                cuerpo_correo.Append("<td>Ha sido Rechazado.</td>");
            }
            cuerpo_correo.Append("</tr>");
            cuerpo_correo.Append("<tr>");
            cuerpo_correo.Append("<td>&nbsp;</td>");
            cuerpo_correo.Append("</tr>");
            cuerpo_correo.Append("</table>");

            string strUrl = (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/").Replace("//", "/").Replace("http:/", "http://");
            string txtCuerpoMail = cuerpo_correo.ToString();
            txtCuerpoMail = txtCuerpoMail.Replace("{cmbTipo}", (tipoProveedor == "1" ? "Proveedor" : "Acreedor"));
            txtCuerpoMail = txtCuerpoMail.Replace("{TxtNombre}", nombreProveedor);
            txtCuerpoMail = txtCuerpoMail.Replace("{txtNumeroGenerado}", acr_NumeroGenerado); 
            //txtCuerpoMail = txtCuerpoMail.Replace("{href}", strUrl + "ProAcreedor_Autorizacion.aspx");


            SmtpClient smtp = new SmtpClient();
            smtp.Host = configuracion.Mail_Servidor;
            smtp.Port = Convert.ToInt32(configuracion.Mail_Puerto);
            smtp.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);

            MailAddress from = new MailAddress(configuracion.Mail_Remitente);
            MailMessage mail = new MailMessage();

            mail.From = from;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Subject = "Autorización de " + (tipoProveedor == "1" ? "Proveedor" : "Acreedor");
            mail.To.Add( new MailAddress(configuracion.Mail_GastosAvisoUsuario));

            mail.Body = txtCuerpoMail;
            smtp.Send(mail);

        }



    }
}