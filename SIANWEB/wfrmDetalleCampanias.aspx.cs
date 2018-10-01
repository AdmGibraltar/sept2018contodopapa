using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CapaEntidad;
using Telerik.Web.UI;

using CapaNegocios;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace SIANWEB
{
    public partial class wfrmDetalleCampanias : System.Web.UI.Page
    {

        #region Variables
                private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        //Propiedad de lista de aplicaiones de la campaña
        private List<AplicacionCampana> ListaAplicacionCampana
        {
            get { return (List<AplicacionCampana>)Session["ListaAplicacionCampana"]; }
            set { Session["ListaAplicacionCampana"] = value; }
        }


        //Propiedad de lista de metas de la campaña
        private List<CampanasMetas> ListaCampanasMetas
        {
            get { return (List<CampanasMetas>)Session["ListaCampanasMetas"]; }
            set { Session["ListaCampanasMetas"] = value; }
        }

        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["session" + Session.SessionID] = value;

            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (session.Cu_Modif_Pass_Voluntario == false)
                    {
                        //RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                        return;
                    }

                    //CargarCentros();
                    Inicializar();
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat("Page_Load_error", ex.Message));
            }
        }

        #region Eventos

        protected void ddlUENs_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                this.CargarSegmentos();
                this.chkAplicaciones.Items.Clear();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void ddlSegmentos_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                this.CargarSegmentoAplicaciones();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void ddlCDS_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                this.VerMetasCampania();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void rgCampanaAplicaciones_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgCampanaAplicaciones.DataSource = this.ListaAplicacionCampana;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void rgCampanaAplicaciones_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgCampanaAplicaciones.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }


        protected void rgCampanaAplicaciones_ItemCommand(object source, GridCommandEventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string mensajeError = string.Empty;
            try
            {
                ErrorManager();
                Int32 item = default(Int32);

                if (e.Item == null) return;

                item = e.Item.ItemIndex;
                if (item >= 0)
                {
                    int Id_Emp = Convert.ToInt32(rgCampanaAplicaciones.Items[item]["Id_Emp"].Text);
                    int Id_Cam = this.HD_CampanaActual.Value != string.Empty ? Convert.ToInt32(this.HD_CampanaActual.Value) : 0;
                    int Id_Apl = Convert.ToInt32(rgCampanaAplicaciones.Items[item]["Id_Apl"].Text);

                    switch (e.CommandName.ToString())
                    {
                        case "Eliminar":
                            mensajeError = "wfrmCampanaAplicaion_delete_error";
                            //int verificador = 0;
                            //new CN_wfrmCampanas().EliminarCampanaAplicacion(Id_Emp, Id_Cam, Id_Apl, this.session.Emp_Cnx, ref verificador);
                            for (int i = 0; i < this.ListaAplicacionCampana.Count; i++)
                            {
                                if (this.ListaAplicacionCampana[i].Id_Apl == Id_Apl)
                                {
                                    this.ListaAplicacionCampana.RemoveAt(i);
                                }
                            }
                            rgCampanaAplicaciones.Rebind();

                            //Si el panel es visible, actualiza los representantes...
                            if (pnlRepresentantes.Visible == true)
                            {
                                this.VerMetasCampania();
                            }
                            
                            DisplayMensajeAlerta("wfrmCampanaAplicacion_delete_ok");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, mensajeError));
            }
        }

        protected void rgCampanaRikMetas_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgCampanaRikMetas.DataSource = this.ListaCampanasMetas;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void rgCampanaRikMetas_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgCampanaRikMetas.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }

        protected void ibtnAgregar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                List<AplicacionCampana> lista = this.ListaAplicacionCampana;
                for (int i = 0; i < chkAplicaciones.Items.Count; i++)
                {
                    if (chkAplicaciones.Items[i].Selected == true)
                    {
                        AplicacionCampana aplicacionCampana = new AplicacionCampana();
                        aplicacionCampana.Id_Emp = this.session.Id_Emp;
                        aplicacionCampana.Id_Cam = 0; //se asigna cuando se guarda la campaña
                        aplicacionCampana.Id_Apl = Convert.ToInt32(chkAplicaciones.Items[i].Value);
                        aplicacionCampana.Apl_Descripcion = chkAplicaciones.Items[i].Text;
                        aplicacionCampana.Id_Uen = Convert.ToInt32(ddlUENs.SelectedValue);
                        aplicacionCampana.Uen_Descripcion = ddlUENs.SelectedItem.Text;
                        aplicacionCampana.Id_Seg = Convert.ToInt32(ddlSegmentos.SelectedValue);
                        aplicacionCampana.Seg_Descripcion = ddlSegmentos.SelectedItem.Text;

                        aplicacionCampana.CamApl_Estatus = 1;

                        //revisa si ya existe la aplicación en la lista
                        bool encontrado = false;
                        foreach (AplicacionCampana ac in lista)
                        {
                            if (ac.Id_Uen == aplicacionCampana.Id_Uen && ac.Id_Seg == aplicacionCampana.Id_Seg && ac.Id_Apl == aplicacionCampana.Id_Apl)
                            {
                                encontrado = true;
                                break;
                            }
                        }
                        if (!encontrado)
                        {
                            lista.Add(aplicacionCampana);
                        }
                    }
                }
                this.ListaAplicacionCampana = lista;
                rgCampanaAplicaciones.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void lnkGuardar_Click(object sender, EventArgs e)
        {
            try 
            {
                if (this.ListaAplicacionCampana.Count > 0)
                {
                    Campanas campana = new Campanas();
                    campana.Id_Emp = this.session.Id_Emp;
                    campana.Id_Cam = 0;
                    campana.Cam_Nombre = txtNombre.Text;
                    campana.ListaAplicacionCampana = this.ListaAplicacionCampana;

                    int verificador = 0;
                    //if (Request.QueryString["Id_Emp"] != null && Request.QueryString["Id_Cam"] != null)
                    if (this.HD_CampanaActual.Value != string.Empty)
                    {
                        campana.Id_Cam = Convert.ToInt32(this.HD_CampanaActual.Value);
                        new CN_wfrmCampanas().ModificarCampana(ref campana, this.session.Emp_Cnx, ref verificador);

                        //si el panel era visible, carga de nuevo las metas de la campaña
                        if (pnlRepresentantes.Visible == true)
                        {
                            this.VerMetasCampania();
                        }
                    }
                    else
                    {
                        new CN_wfrmCampanas().InsertarCampana(ref campana, this.session.Emp_Cnx, ref verificador);
                    }
                    //hace visivle botón para asignar metas de representantes para la campaña
                    ibtnAsignarReps.Visible = true;
                    HD_CampanaActual.Value = campana.Id_Cam.ToString();
                }
                else
                {
                    this.DisplayMensajeAlerta("No se ha asignado ninguna aplicación a la campaña");
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void lnkGuardarReps_Click(object sender, EventArgs e)
        {
            try 
            {
                int verificador = 0;
                List<CampanasMetas> listaMetasCampana = this.ListaCampanasMetas;
                if (listaMetasCampana.Count > 0)
                {
                    new CN_wfrmCampanas().GuardarCampanaMetas(Convert.ToInt32(this.HD_CampanaActual.Value), ref listaMetasCampana, this.session.Emp_Cnx, ref verificador);
                    this.DisplayMensajeAlerta("wfrmCampanaAplicacion_guardarReps_ok");
                }
                else
                {
                    this.DisplayMensajeAlerta("No hay ninguna meta asignada");
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void ibtnAsignarReps_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.VerMetasCampania();
                lnkGuardarReps.Visible = true;
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void ibtnAplicar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                foreach (CampanasMetas cm in this.ListaCampanasMetas)
                {
                    cm.MetCam_Cantidad = Convert.ToInt32(txtACantidad.Text);
                    cm.MetCam_Monto = Convert.ToDouble(txtAMonto.Text);
                }
                rgCampanaRikMetas.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(ex.Message);
            }
        }

        #endregion

        #region Funciones
        
        public object ClonarMetaCampania(object obj)
        {
            object objResult = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);

                ms.Position = 0;
                objResult = bf.Deserialize(ms);
            }
            return objResult;
        }


        private void Inicializar()
        {
            try
            {
                Session["ListaAplicacionCampana"] = new List<AplicacionCampana>();
                Session["ListaCampanasMetas"] = new List<CampanasMetas>();
                this.CargarUEN();
                
                //Cargar zonas (Centros de distribución)
                new CN__Comun().LlenaCombo(1, this.session.Id_Emp, this.session.Id_U, this.session.Emp_Cnx, "spCatCentroDistribucion_Combo", ref ddlCDS);
                ddlCDS.SelectedIndex = ddlCDS.FindItemIndexByValue(this.session.Id_Cd_Ver.ToString());

                if (Request.QueryString["Id_Emp"] != null && Request.QueryString["Id_Cam"] != null)
                {
                    this.CargarCampana();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void CargarCampana()
        {
            try
            {
                Campanas campana = new Campanas();
                List<AplicacionCampana> listaAplicacionesCampana = new List<AplicacionCampana>();
                List<CampanasMetas> listaCampanaMetas = new List<CampanasMetas>();

                //consulta campaña que se ca a editar
                campana.Id_Emp = this.session.Id_Emp;
                campana.Id_Cam = Convert.ToInt32(Request.QueryString["Id_Cam"]);
                new CN_wfrmCampanas().ConsultaCampana(ref campana, this.session.Emp_Cnx);

                //consulta aplicaciones de la campaña
                new CN_wfrmCampanas().ConsultaCampanaAplicaciones(this.session.Emp_Cnx
                    , this.session.Id_Emp
                    , this.session.Id_Cd_Ver
                    , Convert.ToInt32(Request.QueryString["Id_Cam"])
                    , ref listaAplicacionesCampana);

                //consulta lista de metas de campaña
                new CN_wfrmCampanas().ConsultaCampanasMetasLista(this.session.Emp_Cnx
                    , this.session.Id_Emp
                    , this.session.Id_Cd_Ver
                    , Convert.ToInt32(Request.QueryString["Id_Cam"])
                    , ref listaCampanaMetas);


                this.pnlRepresentantes.Visible = true;
                this.lnkGuardarReps.Visible = true;
                //Llenar datos de la pagina
                this.HD_CampanaActual.Value = Request.QueryString["Id_Cam"].ToString();
                this.txtNombre.Text = campana.Cam_Nombre;
                this.ListaAplicacionCampana = listaAplicacionesCampana;
                this.ListaCampanasMetas = listaCampanaMetas;
                this.rgCampanaAplicaciones.Rebind();
                this.rgCampanaRikMetas.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void VerMetasCampania()
        {
            pnlRepresentantes.Visible = true;

            if (ddlCDS.SelectedValue != "-1")
            {
                //obtener identificador de campaña actual
                int? id_CampanaActual = null;
                if (HD_CampanaActual.Value != string.Empty)
                {
                    id_CampanaActual = Convert.ToInt32(HD_CampanaActual.Value);
                }

                //inicializa metas de campaña ya que se consulta ahora para un nuevo centro de distribución
                this.ListaCampanasMetas = new List<CampanasMetas>();

                //Cargar representantes de cada una de las UENs de las aplicaciones de la campaña
                foreach (AplicacionCampana apliCampana in this.ListaAplicacionCampana)
                {
                    List<CampanasMetas> listaMetasCampania = new List<CampanasMetas>();
                    new CN_wfrmCampanas().ConsultaCampanasMetas(this.session.Emp_Cnx
                        , this.session.Id_Emp
                        , Convert.ToInt32(ddlCDS.SelectedValue)
                        , apliCampana.Id_Uen
                        , id_CampanaActual
                        , ref listaMetasCampania);

                    //agregar cada meta de campaña a lista proncipal
                    foreach (CampanasMetas cm in listaMetasCampania)
                    {
                        //revisa si ya existe la meta en la lista
                        bool encontrado = false;
                        foreach (CampanasMetas meta in this.ListaCampanasMetas)
                        {
                            if (meta.Id_Emp == cm.Id_Emp && meta.Id_Rik == cm.Id_Rik)
                            {
                                encontrado = true;
                                break;
                            }
                        }
                        if (!encontrado)
                        {
                            this.ListaCampanasMetas.Add((CampanasMetas)this.ClonarMetaCampania(cm));
                        }
                    }
                }
            }
            else
            {
                this.ListaCampanasMetas = new List<CampanasMetas>();
            }
            this.rgCampanaRikMetas.Rebind();
        }

        private void CargarSegmentoAplicaciones()
        {
            try
            {
                List<Aplicacion> listaAplicaciones = new List<Aplicacion>();
                if (ddlUENs.SelectedIndex <= 0)
                {
                    this.DisplayMensajeAlerta("No ha seleccionado la UEN");
                }
                else
                {
                    if (ddlSegmentos.SelectedIndex != 0)
                    {
                        new CN_CatAplicacion().AplicacionesSegmento_Consultar(
                            this.session.Id_Emp
                            , Convert.ToInt32(ddlSegmentos.SelectedValue)
                            , this.session.Emp_Cnx
                            , ref listaAplicaciones);
                    }
                    this.chkAplicaciones.DataSource = listaAplicaciones;
                    this.chkAplicaciones.DataTextField = "Apl_Descripcion";
                    this.chkAplicaciones.DataValueField = "Id_Apl";
                    this.chkAplicaciones.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarSegmentos()
        {
            try
            {
                if (ddlUENs.SelectedIndex != 0)
                {
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo(1, session.Id_Emp, Convert.ToInt32(ddlUENs.SelectedValue), session.Emp_Cnx, "spCatSegmentosUen_Combo", ref ddlSegmentos);
                }
                else
                {
                    ddlSegmentos.Items.Clear();
                    ddlSegmentos.Text = "--Seleccionar--";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarUEN()
        {
            try
            {

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, session.Id_Emp, session.Emp_Cnx, "spCatUen_Combo", ref ddlUENs);
                //cmbUEN.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta(string.Concat("Error al cargar la página"));
            else
            if (mensaje.Contains("wfrmCampanaAplicacion_delete_ok"))
                Alerta(string.Concat("Aplicación eliminada"));
            else
            if (mensaje.Contains("wfrmCampanaAplicacion_guardarReps_ok"))
                Alerta(string.Concat("Datos guardados correctamente"));
            else
                Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
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
            catch (Exception )
            {
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
    }
}