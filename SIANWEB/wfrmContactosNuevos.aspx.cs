using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaNegocios;
using CapaEntidad;

namespace SIANWEB
{
    public partial class wfrmContactosNuevos : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;

            }
        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ValidarSesion())
                {
                    if (!Page.IsPostBack)
                        Inicializar();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtNombres.Text != "" && this.txtApellidos.Text != "" && this.txtTelefono.Text != "" && this.txtCorreo.Text != "")
                {
                    DateTime? vFecha;
                    if (this.txtFechaNacimiento.SelectedDate.HasValue)
                        vFecha = this.txtFechaNacimiento.SelectedDate;
                    else
                        vFecha = null;
                    Contacto contacto = new Contacto();

                    contacto.Id_Emp = session.Id_Emp;
                    contacto.Id_Cd = session.Id_Cd_Ver;
                    contacto.Id_Cte = Convert.ToInt32(HF_IdCte.Value);
                    contacto.Id_Pos = Convert.ToInt32(HF_IdPos.Value);
                    contacto.Con_Nombre = this.txtNombres.Text.Trim();
                    contacto.Con_Apellido = this.txtApellidos.Text.Trim();
                    contacto.Con_Correo = this.txtCorreo.Text.Trim();
                    contacto.Con_Telefono1 = this.txtTelefono.Text.Trim();
                    contacto.Con_Celular = this.txtCelular.Text.Trim();
                    contacto.Con_Titulo = this.txtTitulo.Text.Trim();
                    contacto.Con_Telefono2 = this.txtOtroTel.Text.Trim();
                    contacto.Con_JefeInmediato = this.txtJefeInmediato.Text.Trim();
                    contacto.Con_Departamento = this.txtDepartamento.Text.Trim();
                    contacto.Con_Calle = this.txtCalle.Text.Trim();
                    contacto.Con_Colonia = this.txtColonia.Text.Trim();
                    contacto.Con_Municipio = this.txtMunicipio.Text.Trim();
                    contacto.Con_Estado = this.txtEstado.Text.Trim();
                    contacto.Con_CodigoPostal = this.txtCodigoPostal.Text.Trim();
                    contacto.Con_FechaNac = vFecha;
                    contacto.Con_Asistente = this.txtAsistente.Text.Trim();
                    contacto.Con_TelefonoAsistente = this.txtTelefonoAsistente.Text.Trim();
                    contacto.Con_Comentarios = this.txtComentariosGenerales.Text.Trim();
                    contacto.Con_Extension = this.txtExt.Text.Trim();
                    contacto.Id_Est = this.ddlEstructura.SelectedValue;
                    contacto.Con_OtraPosicion = this.txtPosicion.Text.Trim();
                    contacto.Id_Seg = Convert.ToInt32(HF_IdSeg.Value);
                    int verificador = 0;
                    string mensaje = string.Empty;
                    CN_CrmContacto cn_catcontacto = new CN_CrmContacto();
                    if (HF_IdCon.Value == "0")
                    {
                        cn_catcontacto.Insertar(contacto, ref verificador, session.Emp_Cnx);
                        mensaje = "No se pudo insertar el registro";
                    }
                    else
                    {
                        contacto.Id_Con = Convert.ToInt32(HF_IdCon.Value);
                        cn_catcontacto.Modificar(contacto, ref verificador, session.Emp_Cnx);
                        mensaje = "No se pudo modificar el registro";
                    }
                    if (verificador == 1)
                    {
                        LimpiarDatos();
                        RAM1.ResponseScripts.Add("CloseAndRebind('" + ddlEstructura.SelectedValue + "')");
                    }
                    else
                        RAM1.ResponseScripts.Add("alert('" + mensaje + "', 330, 150);");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnDeshacer_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                RAM1.ResponseScripts.Add("CloseWindow()");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                cargarEstructura();
                this.HF_IdCte.Value = Request.QueryString["Cte"].ToString();
                this.HF_IdSeg.Value = Request.QueryString["Seg"].ToString();

                if (Request.QueryString["Mov"].ToString() == "E")
                {
                    this.lblPosicion.Text = Request.QueryString["PosNombre"].ToString();

                    this.HF_IdCon.Value = Request.QueryString["Con"].ToString();
                    this.HF_IdPos.Value = Request.QueryString["Pos"].ToString();
                    this.lblEstructura.Text = Request.QueryString["Tipo"].ToString();
                    this.ddlEstructura.SelectedValue = Request.QueryString["Grid"].ToString();
                    this.ddlEstructura.Visible = false;
                    this.lblEstructura.Visible = true;
                    this.lblPosicion.Visible = true;
                    this.txtPosicion.Visible = false;
                    MuestraDatosContacto(Convert.ToInt32(Request.QueryString["Cte"]), Convert.ToInt32(Request.QueryString["Con"].ToString()));
                }
                else
                {
                    this.ddlEstructura.Visible = true;
                    this.lblEstructura.Visible = false;
                    this.lblPosicion.Visible = false;
                    this.txtPosicion.Visible = true;
                    HF_IdCon.Value = "0";
                    HF_IdPos.Value = "0";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cargarEstructura()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(Sesion.Emp_Cnx, "CrmEstructuraPosiciones_Combo", ref ddlEstructura);

        }
        private bool ValidarSesion()
        {
            try
            {
                if (session == null)
                {
                    RAM1.ResponseScripts.Add("CloseWindow()");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void MuestraDatosContacto(int Id_Cte, int Id_Con)
        {
            try
            {
                DataSet dsContacto = new DataSet();
                CN_CrmContacto cn_catcliente = new CN_CrmContacto();
                Contacto contacto = new Contacto();
                contacto.Id_Cte = Id_Cte;
                contacto.Id_Con = Id_Con;
                contacto.Id_Emp = session.Id_Emp;
                contacto.Id_Cd = session.Id_Cd_Ver;

                cn_catcliente.Consulta(contacto, ref dsContacto, session.Emp_Cnx);
                if (dsContacto != null)
                {
                    if (dsContacto.Tables[0].Rows.Count != 0)
                    {
                        DataRow drContacto = dsContacto.Tables[0].Rows[0];
                        this.lblEstructura.Text = ValidarNulo(drContacto["Estructura"]);
                        lblEstructura.Visible = true;
                        this.lblPosicion.Text = ValidarNulo(drContacto["Posicion"]);
                        this.txtNombres.Text = ValidarNulo(drContacto["Nombres"]);
                        this.txtApellidos.Text = ValidarNulo(drContacto["Apellidos"]);
                        this.txtTelefono.Text = ValidarNulo(drContacto["Telefono"]);
                        this.txtCelular.Text = ValidarNulo(drContacto["Celular"]);
                        this.txtCorreo.Text = ValidarNulo(drContacto["Correo"]);
                        this.txtTitulo.Text = ValidarNulo(drContacto["Titulo"]);
                        this.txtOtroTel.Text = ValidarNulo(drContacto["Telefono2"]);
                        this.txtJefeInmediato.Text = ValidarNulo(drContacto["JefeInmediato"]);
                        this.txtDepartamento.Text = ValidarNulo(drContacto["Departamento"]);
                        this.txtCalle.Text = ValidarNulo(drContacto["Calle"]);
                        this.txtColonia.Text = ValidarNulo(drContacto["Colonia"]);
                        this.txtMunicipio.Text = ValidarNulo(drContacto["Municipio"]);
                        this.txtEstado.Text = ValidarNulo(drContacto["Estado"]);
                        this.txtCodigoPostal.Text = ValidarNulo(drContacto["CodigoPostal"]);
                        this.txtExt.Text = ValidarNulo(drContacto["Extension"]);
                        this.txtFechaNacimiento.DbSelectedDate = ValidarNulo(drContacto["FechaNacimiento"]);
                        this.txtAsistente.Text = ValidarNulo(drContacto["Asistente"]);
                        this.txtTelefonoAsistente.Text = ValidarNulo(drContacto["TelefonoAsistente"]);
                        this.txtComentariosGenerales.Text = ValidarNulo(drContacto["ComentariosGenerales"]);
                    }
                    else
                    {
                        if (dsContacto.Tables[1].Rows.Count == 0)
                        {
                            DataRow drContacto = dsContacto.Tables[1].Rows[0];
                            this.txtCalle.Text = ValidarNulo(drContacto["Calle"]);
                            this.txtMunicipio.Text = ValidarNulo(drContacto["Municipio"]);
                            this.txtCodigoPostal.Text = ValidarNulo(drContacto["CP"]);
                            this.txtColonia.Text = ValidarNulo(drContacto["Colonia"]);
                            this.txtEstado.Text = ValidarNulo(drContacto["Ciudad"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LimpiarDatos()
        {
            try
            {
                this.lblEstructura.Text = "";
                this.lblPosicion.Text = "";
                this.txtNombres.Text = "";
                this.txtApellidos.Text = "";
                this.txtTelefono.Text = "";
                this.txtCelular.Text = "";
                this.txtCorreo.Text = "";
                this.txtTitulo.Text = "";
                this.txtOtroTel.Text = "";
                this.txtJefeInmediato.Text = "";
                this.txtDepartamento.Text = "";
                this.txtCalle.Text = "";
                this.txtColonia.Text = "";
                this.txtMunicipio.Text = "";
                this.txtEstado.Text = "";
                this.txtCodigoPostal.Text = "";
                this.txtFechaNacimiento.SelectedDate = null;
                this.txtAsistente.Text = "";
                this.txtTelefonoAsistente.Text = "";
                this.txtComentariosGenerales.Text = "";
                this.txtExt.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string ValidarNulo(object Valor)
        {
            if (Valor != System.DBNull.Value)
                return Valor.ToString();
            else
                return "";
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
                //this.lblMensaje.Text = "";
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
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception)
            {
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
    }
}