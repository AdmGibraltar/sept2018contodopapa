using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Collections;

namespace SIANWEB
{
    public partial class CapReclamaciones : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        //private bool terr = false; //COMENTARIZADA POR NO SER UTILIZADA
        private Sesion sesion
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
        protected void cmbTerritorio_TextChanged(object sender, EventArgs e)
        {

        }
        protected void dpFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (this.dpFecha.SelectedDate < this.rdFecha.SelectedDate)
            {
                this.Alerta("La fecha de acción es menor que la fecha de descripción");
                this.dpFecha.SelectedDate = this.rdFecha.SelectedDate;
                this.dpFecha.Focus();
            }
        }
        protected void dpFecha1_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (this.dpFecha1.SelectedDate < this.rdFecha.SelectedDate)
            {
                this.Alerta("La fecha de conformidad es menor que la fecha de descripción");
                this.dpFecha1.SelectedDate = this.rdFecha.SelectedDate;
                this.dpFecha1.Focus();
            }
        }
        protected void cmbTipo_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                this.CargaNoConformidades();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NombreCliente();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("popup();");
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
                ErrorManager();
                switch (e.Argument.ToString())
                {
                    case "cliente":
                        txtNumCliente.Text = Session["Id_Buscar" + Session.SessionID].ToString();
                        txtCliente_TextChanged(null, null);
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 140);
                        RadPageViewDescripcion.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewAccion.Width;
                        RadSplitter1.Height = altura;
                        RadPageViewAccion.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RadPageViewAccion.Width;
                        RadPageViewConformidad.Height = altura;
                        RadSplitter3.Height = altura;
                        RadPane3.Height = altura;
                        RadPane3.Width = RadPageViewConformidad.Width;
                        RadPageViewComentarios.Height = altura;
                        RadSplitter4.Height = altura;
                        RadPane4.Height = altura;
                        RadPane4.Width = RadPageViewComentarios.Width;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones
        private void CargarTerritorios()
        {
            try
            {
                int cte = 0;
                if (!string.IsNullOrEmpty(this.txtNumCliente.Text))
                    cte = Convert.ToInt32(this.txtNumCliente.Text);
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, cte, sesion.Emp_Cnx, "spCatTerCte_Combo", ref cmbTerritorio);
                this.txtTerritorioId.Text = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargaNoConformidades()
        {
            try
            {
                this.txtCodigo.Text = string.Empty;
                this.cmbCodigo.Text = string.Empty;

                this.cmbCodigo.SelectedIndex = 0;
                this.cmbCodigo.Text = this.cmbTipo.Items[0].Text;

                this.cmbCodigo.Items.Clear();

                if (this.cmbTipo.SelectedIndex != -1)
                {
                    CapaNegocios.CN__Comun CN_Comun = new CN__Comun();
                    CN_Comun.LlenaCombo(1, sesion.Id_Emp, Convert.ToInt32(this.cmbTipo.SelectedValue),
                        sesion.Emp_Cnx, "spCatNoConformidades_Combo", ref this.cmbCodigo);
                }
                else
                {
                    this.cmbCodigo.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void CargaTipo()
        {
            this.cmbTipo.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            this.cmbTipo.Items.Insert(1, new RadComboBoxItem("Producto", "1"));
            this.cmbTipo.Items.Insert(2, new RadComboBoxItem("Servicio administrativo/operativo", "2"));
            this.cmbTipo.Items.Insert(3, new RadComboBoxItem("Servicio de asesoría", "3"));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (sesion == null)
                {
                    CerrarVentana();
                }
                else
                {
                    if (!Page.IsPostBack)
                    {

                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private bool loadValidaFecha()
        {
            if (this.rdFecha.SelectedDate < sesion.CalendarioIni
                || this.rdFecha.SelectedDate > sesion.CalendarioFin)
            {
                this.Alerta("Fecha se encuentra fuera del período, favor de teclear la fecha correcta al período que se encuentra configurado el sistema");
                return false;
            }
            else
            {
                return true;
            }
        }
        private void Inicializar()
        {
            try
            {
                this.rdFecha.SelectedDate = DateTime.Now;
                this.rdFecha.Enabled = true;
                this.dpFecha.SelectedDate = null;
                this.dpFecha1.SelectedDate = null;
                this.CargaTipo();
                this.cmbTipo.SelectedIndex = -1;
                this.GetListDet();

                if (Request.QueryString["id"] != "-1")
                {
                    this.txtReclamacion.Text = Request.QueryString["id"];
                    this.HF_ID.Value = Request.QueryString["id"];
                    this.cargarReclamaciones();
                }
                else
                {
                    this.txtReclamacion.Text = MaximoId();
                    this.GetListDet();
                }
                _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
                _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
                _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
                _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);


                if (!_PermisoGuardar && !_PermisoModificar)
                {
                    RadToolBar1.Items[1].Visible = false;


                    deshabilitarcontroles(divDescripcion.Controls);
                    deshabilitarcontroles(divAccion.Controls);
                    deshabilitarcontroles(divConformidad.Controls);
                    txtComentario.Enabled = false;
                }



            }
            catch (Exception)
            {
                throw;
            }
        }
        private void deshabilitarcontroles(System.Web.UI.ControlCollection controles_contenidos)
        {
            for (int x = 0; x < controles_contenidos.Count; x++)
            {
                string Type = controles_contenidos[x].GetType().FullName;

                if (Type.Contains("RadMultiPage") || Type.Contains("RadPageView") || Type.Contains("Panel"))
                {
                    deshabilitarcontroles(controles_contenidos[x].Controls);
                }

                switch (Type.Replace("Telerik.Web.UI.", ""))
                {
                    case "RadNumericTextBox":
                        (controles_contenidos[x] as RadNumericTextBox).Enabled = false;
                        break;
                    case "RadTextBox":
                        (controles_contenidos[x] as RadTextBox).Enabled = false;
                        break;
                    case "RadComboBox":
                        (controles_contenidos[x] as RadComboBox).Enabled = false;
                        break;
                    case "RadDatePicker":
                        (controles_contenidos[x] as RadDatePicker).Enabled = false;
                        break;

                }

                if (Type.Contains("CheckBox"))
                {
                    (controles_contenidos[x] as CheckBox).Enabled = false;
                }

                if (Type.Contains("ImageButton"))
                {
                    (controles_contenidos[x] as ImageButton).Enabled = false;
                }
            }


        }
        private void cargarReclamaciones()
        {
            try
            {
                Reclamaciones reclamaciones = new Reclamaciones();

                reclamaciones.Id_Emp = this.sesion.Id_Emp;
                reclamaciones.Id_Cd = this.sesion.Id_Cd_Ver;
                reclamaciones.Id_Rec = Convert.ToInt32(this.HF_ID.Value);

                CN_CapReclamaciones CNCapReclamaciones = new CN_CapReclamaciones();
                CNCapReclamaciones.ConsultaReclamaciones(ref reclamaciones, sesion.Emp_Cnx);

                //*** DESCRIPCION ***\\
                this.rdFecha.SelectedDate = reclamaciones.Rec_Fecha;
                this.txtNumCliente.Text = reclamaciones.Id_Cte.ToString();
                this.txtClienteNombre.Text = reclamaciones.Cte_NomComercial;
                this.CargarTerritorios();
                this.txtUsuario.Text = reclamaciones.Rec_Usuario.ToString();
                this.txtTelefono.Text = reclamaciones.Rec_Telefono.ToString();
                this.cmbTerritorio.SelectedIndex = this.cmbTerritorio.FindItemIndexByValue(reclamaciones.Id_Ter.ToString());
                this.cmbTerritorio.Text = this.cmbTerritorio.FindItemByValue(reclamaciones.Id_Ter.ToString()).Text;
                this.txtTerritorioId.Text = reclamaciones.Id_Ter.ToString();
                this.cmbTipo.SelectedIndex = this.cmbTipo.FindItemIndexByValue(reclamaciones.Id_tipo.ToString());

                this.CargaNoConformidades();
                this.txtCodigo.Text = reclamaciones.Id_NoConf.ToString();
                this.cmbCodigo.SelectedIndex = this.cmbCodigo.FindItemIndexByValue(reclamaciones.Id_NoConf.ToString());
                this.cmbCodigo.Text = this.cmbCodigo.FindItemByValue(reclamaciones.Id_NoConf.ToString()).Text;

                this.txtDescripcion.Text = reclamaciones.Rec_Descripcion.ToString();
                this.txtCausa.Text = reclamaciones.Rec_CausaRaiz.ToString();

                //*** ACCION ***\\
                this.dpFecha.SelectedDate = reclamaciones.Rec_FecAccion;
                this.txtAccion1.Text = reclamaciones.Rec_AcAccion1.ToString();
                this.txtAccion2.Text = reclamaciones.Rec_AcAccion2.ToString();
                this.txtResponsable.Text = reclamaciones.Rec_AcResponsable.ToString();

                //*** CONFORMIDAD ***\\
                this.dpFecha1.SelectedDate = reclamaciones.Rec_FecConformidad;
                this.txtNombre.Text = reclamaciones.Rec_ConNombre.ToString();
                this.txtDepartamento.Text = reclamaciones.Rec_ConDepartamento.ToString();

                //*** COMENTARIOS ***\\
                this.txtComentario.Text = reclamaciones.Rec_Comentarios.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetListDet()
        {
            try
            {
                dt = new DataTable();

                dt.Columns.Add("Id_PedDet", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Ter", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Ter_Nombre", System.Type.GetType("System.String"));
                dt.Columns.Add("Id_Prd", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Presentacion", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Unidad", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Precio", System.Type.GetType("System.Double"));
                dt.Columns.Add("Prd_Cantidad", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_Importe", System.Type.GetType("System.Double"));
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
                CerrarVentana();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CerrarVentana()
        {
            try
            {
                string funcion;
                funcion = "CloseAndRebind()";

                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string MaximoId()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CapReclamaciones", "Id_Rec", sesion.Emp_Cnx, "spCatLocal_Maximo");

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
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                if (btn.CommandName == "save")
                {
                    this.camposLlenos();

                    if (Page.IsValid)
                    {
                        this.GuardarReclamacion();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private bool camposLlenos()
        {
            if (this.txtNumCliente.Text == "" ||
                this.txtTelefono.Text == "" ||
                this.txtTerritorioId.Text == "" ||
                this.cmbTipo.SelectedValue == "-1" ||
                this.txtCodigo.Text == "" ||
                this.txtDescripcion.Text == "" ||
                this.txtCausa.Text == "")
            {
                this.Alerta("Todos los campos de la sección de descripción son obligatorios");
                return false;
            }
            return true;
        }

        private void GuardarReclamacion()
        {
            try
            {
                if (dpFecha.DbSelectedDate != null)
                {
                    bool accion = false;
                    if (txtAccion1.Text == "")
                    {
                        Label21.Text = "*Requerido";
                        accion = true;
                    }
                    else
                    {
                        Label21.Text = "";
                    }

                    if (txtAccion2.Text == "")
                    {
                        Label22.Text = "*Requerido";
                        accion = true;
                    }
                    else
                    {
                        Label22.Text = "";
                    }

                    if (txtResponsable.Text == "")
                    {
                        Label23.Text = "*Requerido";
                        accion = true;
                    }
                    else
                    {
                        Label23.Text = "";
                    }

                    if (accion)
                    {
                        RadPageViewAccion.Selected = true;
                        RadTabStrip1.Tabs[1].Selected = true;
                        return;
                    }
                }

                if (dpFecha1.DbSelectedDate != null)
                {
                    bool accion = false;
                    if (txtAccion1.Text == "")
                    {
                        Label21.Text = "*Requerido";
                        accion = true;
                    }
                    else
                    {
                        Label21.Text = "";
                    }

                    if (txtAccion2.Text == "")
                    {
                        Label22.Text = "*Requerido";
                        accion = true;
                    }
                    else
                    {
                        Label22.Text = "";
                    }

                    if (txtResponsable.Text == "")
                    {
                        Label23.Text = "*Requerido";
                        accion = true;
                    }
                    else
                    {
                        Label23.Text = "";
                    }

                    if (accion)
                    {
                        RadPageViewAccion.Selected = true;
                        RadTabStrip1.Tabs[1].Selected = true;
                        return;
                    }

                    bool conformidad = false;
                    if (txtNombre.Text == "")
                    {
                        Label24.Text = "*Requerido";
                        conformidad = true;
                    }
                    else
                    {
                        Label24.Text = "";
                    }

                    if (txtDepartamento.Text == "")
                    {
                        Label25.Text = "*Requerido";
                        conformidad = true;
                    }
                    else
                    {
                        Label25.Text = "";
                    }

                    if (conformidad)
                    {
                        RadPageViewConformidad.Selected = true;
                        RadTabStrip1.Tabs[2].Selected = true;
                        return;
                    }
                }

                //Valida que las fechas de accion o conformidad esten vacias
                //si hay datos en los respectivos textboxes de ests
                if (this.rdFecha.SelectedDate == null)
                {
                    this.Alerta("Es necesaria la fecha de descripción");
                    this.rdFecha.Focus();
                    return;
                }
                if (this.dpFecha.SelectedDate == null
                    && (this.txtAccion1.Text != string.Empty
                    || this.txtAccion2.Text != string.Empty
                    || this.txtResponsable.Text != string.Empty))
                {
                    this.Alerta("Es necesaria la fecha de acción");
                    this.dpFecha.Focus();
                    return;
                }

                if (this.dpFecha1.SelectedDate == null &&
                    (this.txtNombre.Text != string.Empty
                   || this.txtDepartamento.Text != string.Empty))
                {
                    this.Alerta("Es necesaria la fecha de conformidad");
                    this.dpFecha.Focus();
                    return;
                }

                Reclamaciones reclamaciones = new Reclamaciones();

                string varEstatus = "C"; //Estatus por defaul de las reclamaciones C -Creado-

                //***   DESCRIPCION   ***\\
                reclamaciones.Id_Emp = sesion.Id_Emp;
                reclamaciones.Id_Cd = sesion.Id_Cd_Ver;
                reclamaciones.Id_Rec = Convert.ToInt32(this.txtReclamacion.Text);
                reclamaciones.Rec_Fecha = this.rdFecha.SelectedDate.Value;
                reclamaciones.Id_Cte = Convert.ToInt32(this.txtNumCliente.Text);
                reclamaciones.Rec_Usuario = this.txtUsuario.Text.ToString();
                reclamaciones.Rec_Telefono = this.txtTelefono.Text.ToString();
                reclamaciones.Id_Ter = Convert.ToInt32(this.txtTerritorioId.Text);
                reclamaciones.Id_tipo = Convert.ToInt32(this.cmbTipo.SelectedValue);
                reclamaciones.Id_NoConf = Convert.ToInt32(this.txtCodigo.Text);
                reclamaciones.Rec_Descripcion = this.txtDescripcion.Text.ToString();
                reclamaciones.Rec_CausaRaiz = this.txtCausa.Text.ToString();

                if (this.dpFecha.SelectedDate != null)
                {
                    reclamaciones.Rec_FecAccion = this.dpFecha.SelectedDate.Value;
                }
                else
                {
                    reclamaciones.Rec_FecAccion = null;
                }
                //***   ACCION  ***\\
                reclamaciones.Rec_AcAccion1 = this.txtAccion1.Text.ToString();
                reclamaciones.Rec_AcAccion2 = this.txtAccion2.Text.ToString();
                reclamaciones.Rec_AcResponsable = this.txtResponsable.Text.ToString();


                // Verifica si los contrles de la pestaña de accion estan llenos, si por lo menos 
                // uno tiene datos, establecera el estatus en A -Accion-, sino, entonces lo
                // deja el estatus por default C -Creado-
                if (this.txtAccion1.Text != string.Empty
                    || this.txtAccion2.Text != string.Empty
                    || this.txtResponsable.Text != string.Empty
                    || this.dpFecha.SelectedDate != null)
                {
                    varEstatus = "A";
                }

                //***   CONFORMIDAD ***\\
                if (this.dpFecha1.SelectedDate != null)
                {
                    reclamaciones.Rec_FecConformidad = this.dpFecha1.SelectedDate.Value;
                }
                else
                {
                    reclamaciones.Rec_FecConformidad = null;
                }
                reclamaciones.Rec_ConNombre = this.txtNombre.Text.ToString();
                reclamaciones.Rec_ConDepartamento = this.txtDepartamento.Text.ToString();

                // Verifica si los datos correspondientes estan llenos, si es asi, entonces pone como
                // estatus F -Conformidad-, sino lo deja el estatus por default C -Creado-
                if (this.txtNombre.Text != string.Empty
                    || this.txtDepartamento.Text != string.Empty
                        || this.dpFecha1.SelectedDate != null)
                {
                    if (this.txtAccion1.Text == string.Empty
                        || this.txtAccion2.Text == string.Empty
                        || this.txtResponsable.Text == string.Empty
                        || this.dpFecha.SelectedDate == null)
                    {
                        this.Alerta("No puede generar una conformidad sin los datos de la acción tomada");
                        return;
                    }
                    else
                    {
                        varEstatus = "F";
                    }
                }

                //***   COMENTARIOS ***\\
                reclamaciones.Rec_Comentarios = this.txtComentario.Text.ToString();

                //***   ESTATUS ***\\
                reclamaciones.Rec_Estatus = varEstatus;

                string mensaje = string.Empty;
                int verificador = -1;
                CN_CapReclamaciones CNCapReclamaciones = new CN_CapReclamaciones();

                if (this.HF_ID.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }

                    CNCapReclamaciones.InsertaReclamaciones(reclamaciones, dt, sesion.Emp_Cnx, ref verificador);

                    mensaje = "Los datos se guardaron correctamente";
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permiso para modificar los datos");
                        return;
                    }

                    reclamaciones.Id_Rec = Convert.ToInt32(this.HF_ID.Value);

                    CNCapReclamaciones.ModificaReclamaciones(reclamaciones, dt, sesion.Emp_Cnx, ref verificador);

                    mensaje = "Los datos se modificaron correctamente";
                }

                if (verificador > 0)
                {
                    //cerrar ventana radWindow de reclamaciones
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')"));
                }
                else
                {
                    Alerta("Ocurrio un error al intentar guardar la reclamación");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void NombreCliente()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string idCliente = txtNumCliente.Value.HasValue ? txtNumCliente.Value.Value.ToString() : "-1";
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Cte = Convert.ToInt32(idCliente);
                try
                {
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtNumCliente.ClientID);
                    txtClienteNombre.Text = "";
                    return;
                }
                this.txtClienteNombre.Text = cliente.Cte_NomComercial;
                CargarTerritorios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region ErrorManager
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
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