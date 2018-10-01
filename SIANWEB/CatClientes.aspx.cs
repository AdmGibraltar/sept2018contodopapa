using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.Web.UI.HtmlControls;
using CapaModelo;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.IO;
using System.Xml.Serialization;
using System.Collections;

namespace SIANWEB
{
    public partial class CatClientes : System.Web.UI.Page
    {
        #region Variables
        bool lleno2 = false;
        bool lleno = false;
        public string strEmp = System.Configuration.ConfigurationManager.AppSettings["VGEmpresa"];
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool banco
        {
            get
            {
                bool? _banco = (bool?)Session["banco" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
                return _banco ?? false;
            }
            set
            {
                Session["banco" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
        //static DataTable dt;
        public bool Tipoterr = false;
        private DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private DataSets.CatCliente.CatClienteDS DataSet { get { return (DataSets.CatCliente.CatClienteDS)Session["cat_ctedet_ds" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["cat_ctedet_ds" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private DataTable dtDireccionEntrega { get { return (DataTable)Session["dtDireccionEntrega" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dtDireccionEntrega" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        DataTable dtClienteProducto { get { return (DataTable)Session["dtClienteProducto" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dtClienteProducto" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        DataTable dtPrecio { get { return (DataTable)Session["dtPrecio" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dtPrecio" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public DataTable TablaPermisosUEN
        {
            get
            {
                return (Session["TablaPermisosUEN" + Session.SessionID] as DataTable);
            }
            set
            {
                Session["TablaPermisosUEN" + Session.SessionID] = value;
            }
        }
        bool terr = false;
        bool terrServ = false;
        bool Seg = false;
        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
        }
        private string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }

        private string Emp_CnxCen
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
        }

        //Edsg 19062017
        private Boolean ModificaTerritorios
        {
            get { return (Boolean)Session["ModificaTerritorios" + Session.SessionID]; }
            set { Session["ModificaTerritorios" + Session.SessionID] = value; }
        }


        //10-06-2018 RBM 
        //Tablas de territorios para autorizar cambios
        //Inicio
        public DataTable objdtListaTerritoriosAnt { get; set; }
        protected DataTable objdtTablaTerritoriosAnt { get { if (ViewState["objdtTablaTerritoriosAnt"] != null) { return (DataTable)ViewState["objdtTablaTerritoriosAnt"]; } else { return objdtListaTerritoriosAnt; } } set { ViewState["objdtTablaTerritoriosAnt"] = value; } }


        public DataTable objdtListaTerritorios { get; set; }
        protected DataTable objdtTablaTerritorios { get { if (ViewState["objdtTablaTerritorios"] != null) { return (DataTable)ViewState["objdtTablaTerritorios"]; } else { return objdtListaTerritorios; } } set { ViewState["objdtTablaTerritorios"] = value; } }
        //fin

        #endregion
        #region Eventos
        protected void Page_Init(object sender, EventArgs e)
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];
            if (session == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                Response.Redirect("login.aspx", false);
            }
            else
            {
                CargarClientes();
            }
        }
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
                    Response.Redirect("login.aspx", true);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {

                        //Edsg 19062017 
                        this.ModificaTerritorios = false;

                        ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }

                        CargarCentros();
                        Inicializar();
                        CargarCuentaNacional();
                        int VGEmpresa = 0;
                        Int32.TryParse(strEmp, out VGEmpresa);
                        if (VGEmpresa == Sesion.Id_Emp)
                            trReferencia.Visible = true;
                        else
                            trReferencia.Visible = false;


                    }
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }

        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Inicializar();
                Nuevo();
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }

        protected void cmbRemisionElect_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CargarCuentaNacional(Convert.ToInt32(cmbRemisionElect.SelectedValue));

            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }

        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                    if (Page.IsValid)
                        PreGuardar();
                    //Guardar();
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
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void txtProducto_TextChanged(object sender, EventArgs e)
        {
            if (txtProductoID.Text == "")
            {
                cmbProducto_SelectedIndexChanged(null, null);
                return;
            }


            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CN_CatProducto cn_producto = new CN_CatProducto();
            Producto prd = new Producto();
            try
            {
                cn_producto.ConsultaProducto(ref prd, Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd_Ver, (int)txtProductoID.Value.Value);
            }
            catch (Exception ex)
            {
                AlertaFocus(ex.Message, txtProductoID.ClientID);
                txtProductoID.Text = "";
                cmbProducto_SelectedIndexChanged(null, null);
                return;
            }


            if (prd.Prd_Descripcion != null)
            {
                cmbProducto.Text = prd.Prd_Descripcion;
                cmbProducto.SelectedValue = prd.Id_Prd.ToString();
                //lblProducto.Text = prd.Id_Prd.ToString() + " - " + prd.Prd_Descripcion;
                cmbProducto_SelectedIndexChanged(null, null);
            }
            else
            {
                txtProductoID.Text = "";
                cmbProducto.Text = "";
                //lblProducto.Text = "";
                Inicializar();
            }


        }
        protected void cmbProducto_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ObtenerProductoInfo();
                if (txtClave.Text != "")
                {
                    GetListDetClienteProducto();
                    //rgDetPrecio.Rebind();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void cmbTipoCliente_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (txtIdTipoCliente.Text != "")
                {
                    Sesion session2 = new Sesion();
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    CN_CatCliente clsCliente = new CN_CatCliente();
                    List<Comun> List = new List<Comun>();
                    Clientes cte = new Clientes();

                    cte.Id_Emp = session2.Id_Emp;
                    cte.Id_Cd = session2.Id_Cd_Ver;
                    cte.Id_TCte = Int32.Parse(txtIdTipoCliente.Text);

                    clsCliente.ConsultaClienteTipo(cte, session2.Emp_Cnx, ref List);

                    if (List.Count > 0)
                    {
                        bool conCuentaCorporativa = List[0].ValorBool;

                        if (conCuentaCorporativa)
                        {
                            numCorporativo.Enabled = true;
                            cmbCorporativa.Enabled = true;
                        }
                        else
                        {
                            numCorporativo.Enabled = false;
                            cmbCorporativa.Enabled = false;

                            numCorporativo.Text = "10000";
                            cmbCorporativa.SelectedIndex = cmbCorporativa.FindItemIndexByValue("10000");

                            var comCCorporativa = cmbCorporativa.FindItemByValue("10000");
                            if (comCCorporativa != null)
                                cmbCorporativa.Text = comCCorporativa.Text;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RadComboBox11_DataBound(object sender, EventArgs e)
        {
            RadComboBox comboBox = ((RadComboBox)sender);
            string id = ((Label)comboBox.Parent.Parent.FindControl("Label118")).Text;
            if (id != "")
                comboBox.SelectedIndex = comboBox.FindItemIndexByValue(id);
        }

        protected void cmb22_DataBinding(object sender, EventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            try
            {
                if (!lleno2)
                {
                    lleno2 = true;
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo(3, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatTipoPrecio_Combo", ref rcb);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void RadComboBox11_DataBinding(object sender, EventArgs e)
        {
            CargarTPrecios((RadComboBox)sender);
        }

        private void CargarTPrecios(RadComboBox radComboBox)
        {
            try
            {
                if (!lleno)
                {
                    lleno = true;
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatTipoPrecio_Combo", ref radComboBox);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void chkClonarDireccionFiscal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkClonarDireccionFiscal.Checked)
            {
                txtEcalle.Text = txtFcalle.Text;
                txtEnumero.Text = txtFnumero.Text;
                txtEcp.Text = txtFcp.Text;
                txtEcolonia.Text = txtFcolonia.Text;
                txtEmunicipio.Text = txtFmunicipio.Text;
                txtEestado.Text = txtFestado.Text;
                txtEtelefono.Text = txtFtelefono.Text;
            }
            else
            {
                txtEcalle.Text = "";
                txtEnumero.Text = "";
                txtEcp.Text = "";
                txtEcolonia.Text = "";
                txtEmunicipio.Text = "";
                txtEestado.Text = "";
                txtEtelefono.Text = "";

            }
        }

        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (!((System.Web.UI.WebControls.CheckBox)sender).Checked && HF_ID.Value != "")
            {
                if (!Deshabilitar())
                {
                    Alerta("El registro está siendo utilizado por otro componente");
                    ((System.Web.UI.WebControls.CheckBox)sender).Checked = true;
                }
            }
        }

        private void GetListDetClienteProducto()
        {
            try
            {
                dtClienteProducto = new DataTable();
                DataColumn dc = new DataColumn();
                dtClienteProducto.Columns.Add("Id_ClpDet", System.Type.GetType("System.Int32"));
                dtClienteProducto.Columns.Add("TPrecio", System.Type.GetType("System.Int32"));
                dtClienteProducto.Columns.Add("TPrecioStr", System.Type.GetType("System.String"));
                dtClienteProducto.Columns.Add("Precio", System.Type.GetType("System.String"));
                dtClienteProducto.Columns.Add("Pesos", System.Type.GetType("System.Double"));

                txtClaveX.Text = "";
                txtDescripcionX.Text = "";
                //chkActivo.Checked = true;

                if (txtClave.Text != "" && txtProductoID.Text != "")
                {
                    if (Convert.ToInt32(txtClave.Text) > 0 && Convert.ToInt32(txtProductoID.Text) > 0)
                    {
                        CN_CatClienteProd clsCatCliente = new CN_CatClienteProd();
                        Sesion session2 = new Sesion();
                        session2 = (Sesion)Session["Sesion" + Session.SessionID];
                        ClienteProd ClienteProddet = new ClienteProd();
                        ClienteProddet.Id_Emp = session2.Id_Emp;
                        ClienteProddet.Id_Cd = session2.Id_Cd_Ver;
                        ClienteProddet.Id_Cte = Convert.ToInt32(txtClave.Text);
                        ClienteProddet.Id_Prd = Convert.ToInt32(txtProductoID.Text);
                        DataTable dt2 = dtClienteProducto;
                        clsCatCliente.ConsultaClienteProdDet(ClienteProddet, session2.Emp_Cnx, ref dt2);
                        dtClienteProducto = dt2;
                        txtClaveX.Text = ClienteProddet.Id_Clp;// == 0 ? (int?)null : ClienteProddet.Id_Clp;
                        txtDescripcionX.Text = ClienteProddet.Clp_descripcion == null ? "" : ClienteProddet.Clp_descripcion;
                        //chkActivo.Checked = string.IsNullOrEmpty(ClienteProddet.Id_Clp) ? true : ClienteProddet.Estatus;
                        if (!string.IsNullOrEmpty(txtClaveX.Text))
                            HF_IdCP.Value = txtClaveX.Text;

                        txtUnidades.Text = ClienteProddet.Unidades;
                        txtPresentacion.Text = ClienteProddet.Clp_Presentacion;
                        //txtCantFact.Value = ClienteProddet.CantFact;
                        //dpUltimaVta.DbSelectedDate = ClienteProddet.Clp_FecUltVta;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ObtenerProductoInfo()
        {
            try
            {
                int Id_Prd = txtProductoID.Text == "" ? -1 : Convert.ToInt32(cmbProducto.SelectedValue);//Id_Prd;
                if (Id_Prd != -1)
                {
                    Sesion session2 = new Sesion();
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    Producto prd = new Producto();
                    CN_CatProducto cn_catproducto = new CN_CatProducto();
                    cn_catproducto.ConsultaProducto(ref prd, session2.Emp_Cnx, session2.Id_Emp, session2.Id_Cd_Ver, Id_Prd);

                    //txtInventarioFin.Value = prd.Prd_InvFinal;
                    //txtAsignado.Value = prd.Prd_Asignado;
                }
                else
                {
                    //txtInventarioFin.Text = "";
                    //txtAsignado.Text = "";
                    cmbProducto.Text = "";
                    txtClave.Text = "";
                    txtDescripcion.Text = "";
                    txtPresentacion.Text = "";
                    txtUnidades.Text = "";
                    //txtCantFact.Text = "";
                    dt = new DataTable();
                    DataColumn dc = new DataColumn();
                    dt.Columns.Add("Id_ClpDet", System.Type.GetType("System.Int32"));
                    dt.Columns.Add("TPrecio", System.Type.GetType("System.Int32"));
                    dt.Columns.Add("TPrecioStr", System.Type.GetType("System.String"));
                    dt.Columns.Add("Precio", System.Type.GetType("System.String"));
                    dt.Columns.Add("Pesos", System.Type.GetType("System.Double"));
                    //rgDet.Rebind();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PreGuardar()
        {
            try
            {

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];


                CentroDistribucion cd = new CentroDistribucion();
                cd.Id_Emp = session.Id_Emp;
                CN_CatCentroDistribucion clsCatCd = new CN_CatCentroDistribucion();

                List<CentroDistribucion> list = new List<CentroDistribucion>();

                clsCatCd.ConsultaCentrosPropios(ref list, cd.Id_Emp, Emp_CnxCen);

                bool permiteGuardar = true;
                bool cambioTipoCliente = false;
                bool cambioCondicionesP = false;

                if (HF_ID.Value != "" && list.Count(a => a.Id_Cd == session.Id_Cd_Ver) > 0)
                {

                    Clientes cte = new Clientes();
                    cte.Id_Emp = session.Id_Emp;
                    cte.Id_Cd = session.Id_Cd_Ver;
                    cte.Id_Cte = Convert.ToInt32(cmbCliente.SelectedValue);
                    cte.Ignora_Inactivo = true;
                    CN_CatCliente clsCatClientes = new CN_CatCliente();
                    clsCatClientes.ConsultaClientes(ref cte, session.Emp_Cnx);

                    int CondicionesP = 0;
                    CondicionesP = txtDias1.Value.HasValue ? Convert.ToInt32(txtDias1.Value) : 0;

                    if (cte.Cte_CondPago < CondicionesP)
                    {
                        permiteGuardar = false;
                        cambioCondicionesP = true;
                    }

                    if (cte.Id_TCte != Int32.Parse(cmbTipoCliente.SelectedValue))
                    {
                        permiteGuardar = false;
                        cambioTipoCliente = true;

                        Session["Sesion" + Session.SessionID + "cambioTipoCliente"] = "true";
                    }
                    else
                    {
                        Session["Sesion" + Session.SessionID + "cambioTipoCliente"] = "false";
                    }


                }
                else
                {
                    cambioCondicionesP = true;
                }


                CN_ConfiguracionCobranza confCobranza = new CN_ConfiguracionCobranza();

                List<Acciones> list_accionesTemp = new List<Acciones>();
                List<Alertas> list_alertasTemp = new List<Alertas>();
                List<PeriodoGracia> list_graciaTemp = new List<PeriodoGracia>();
                Reglas reglas = new Reglas();

                confCobranza.Consultar(ref list_graciaTemp, ref list_accionesTemp, ref list_alertasTemp, session.Id_Emp, (new SqlConnectionStringBuilder(session.Emp_Cnx)).InitialCatalog, ref reglas, Emp_CnxCob);

                string id_tu = "";

                if (txtDias1.Value >= (double?)reglas.Val1 && txtDias1.Value <= (double?)reglas.Val2)
                {
                    id_tu = "," + reglas.Id_Tu1.ToString() + "," + reglas.Id_Tu2.ToString() + "," + reglas.Id_Tu3.ToString() + ",";
                }

                if (txtDias1.Value >= (double?)reglas.Val3 && txtDias1.Value <= (double?)reglas.Val4)
                {
                    id_tu = "," + reglas.Id_Tu3.ToString() + "," + reglas.Id_Tu2.ToString() + ",";
                }

                if (txtDias1.Value >= (double?)reglas.Val5 && txtDias1.Value <= (double?)reglas.Val6)
                {
                    id_tu = "," + reglas.Id_Tu3.ToString() + ",";
                }

                txtAutorizo.Text = "";


                if (permiteGuardar && HF_ID.Value != "")
                {
                    id_tu = "";
                }

                if (id_tu != "" && list.Count(a => a.Id_Cd == session.Id_Cd_Ver) > 0)
                {
                    Session["Sesion" + Session.SessionID + "Id_Tu"] = id_tu;

                    CN_CatTipoCliente clsTipoCliente = new CN_CatTipoCliente();
                    TipoCliente tipoCliente = new TipoCliente();
                    tipoCliente.Id_TCte = Int32.Parse(cmbTipoCliente.SelectedValue);
                    int verificador = 1;
                    clsTipoCliente.ConsultaAutorizadores(tipoCliente, session.Emp_Cnx, ref verificador);

                    if (tipoCliente.TCte_Autorizadores == ",")
                    {
                        Session["Sesion" + Session.SessionID + "cambioTipoCliente"] = "false";
                    }

                    Session["Sesion" + Session.SessionID + "TCte_Autorizadores"] = tipoCliente.TCte_Autorizadores;

                    if (cambioCondicionesP)
                    {
                        AbrirVentana_Autorizacion();
                    }
                    else
                    {
                        if (tipoCliente.TCte_Autorizadores == ",")
                        {
                            Guardar((int?)null, (int?)null);
                        }
                        else
                        {
                            AbrirVentana_AutorizacionTipoCliente();
                        }
                    }
                }
                else
                {
                    Guardar((int?)null, (int?)null);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void AbrirVentana_Autorizacion()
        {
            try
            {
                RAM1.ResponseScripts.Add("AbrirVentana_Autorizacion();");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void AbrirVentana_AutorizacionTipoCliente()
        {
            try
            {
                RAM1.ResponseScripts.Add("AbrirVentana_AutorizacionTipoCliente();");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void cmbCliente_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (cmbCliente.SelectedValue == "" && cmbCliente.Text != "")
                {
                    Alerta("El cliente no existe");
                    Nuevo();
                    cmbCliente.Focus();
                    return;
                }
                if (cmbCliente.SelectedValue != "" && Convert.ToInt32(cmbCliente.SelectedValue) > -1)
                {
                    CN__Comun.RemoverValidadores(Validators);
                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];
                    //RBM
                    //Se limpian las tablas de los cambios de territorios
                    //Inicio
                    objdtTablaTerritorios.Rows.Clear();
                    objdtTablaTerritoriosAnt.Rows.Clear();
                    //Fin
                    Clientes cte = new Clientes();
                    cte.Id_Emp = session.Id_Emp;
                    cte.Id_Cd = session.Id_Cd_Ver;
                    cte.Id_Cte = Convert.ToInt32(cmbCliente.SelectedValue);
                    cte.Ignora_Inactivo = true;
                    CN_CatCliente clsCatClientes = new CN_CatCliente();
                    clsCatClientes.ConsultaClientes(ref cte, session.Emp_Cnx);

                    txtClave.Enabled = false;
                    txtClave.Text = cte.Id_Cte.ToString();
                    HF_ID.Value = cte.Id_Cte.ToString();
                    txtDescripcion.Text = cte.Cte_NomComercial;
                    txtNombreCorto.Text = cte.Cte_NomCorto;
                    txtFcalle.Text = cte.Cte_FacCalle;
                    txtFnumero.Text = cte.Cte_FacNumero;
                    txtReferencia.Text = cte.Cte_Referencia;
                    try
                    {
                        txtFcp.Text = cte.Cte_FacCp;
                    }
                    catch
                    {
                        //EL TELEFONO TIENE FORMATO INCORRECTO
                    }
                    txtFcolonia.Text = cte.Cte_FacColonia;
                    txtFmunicipio.Text = cte.Cte_FacMunicipio;
                    try
                    {
                        txtFtelefono.Text = cte.Cte_FacTel;
                        // txtFtelefono.Text = cte.Cte_FacTel.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "").Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    }
                    catch
                    {
                        //EL TELEFONO TIENE FORMATO INCORRECTO
                    }

                    txtFrfc.Text = cte.Cte_FacRfc;
                    txtFestado.Text = cte.Cte_FacEstado;
                    txtDcalle.Text = cte.Cte_Calle;
                    txtDnumero.Text = cte.Cte_Numero.ToString();
                    try
                    {
                        if (!string.IsNullOrEmpty(cte.Cte_Cp.Trim()))
                        {
                            txtDcp.Text = cte.Cte_Cp;
                        }
                    }
                    catch
                    {
                        //EL CP TIENE FORMATO INCORRECTO
                    }
                    txtDcolonia.Text = cte.Cte_Colonia;
                    txtDmunicipio.Text = cte.Cte_Municipio;
                    txtDestado.Text = cte.Cte_Estado;
                    try
                    {
                        txtDtelefono.Text = cte.Cte_Telefono;
                        // txtDtelefono.Text = cte.Cte_Telefono.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "").Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    }
                    catch
                    {
                        //EL TELEFONO TIENE FORMATO INCORRECTO
                    }
                    try
                    {
                        txtDrfc.Text = cte.Cte_DRfc;
                    }
                    catch
                    {
                        //EL RFC TIENE FORMATO INCORRECTO
                    }
                    try
                    {
                        txtDfax.Text = cte.Cte_Fax;
                        // txtDfax.Text = cte.Cte_Fax.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "").ToString();
                    }
                    catch
                    {
                        //EL FAX TIENE FORMATO INCORRECTO
                    }
                    txtDcontacto.Text = cte.Cte_Contacto;
                    txtmail.Text = cte.Cte_Email;
                    chkCredito.Checked = cte.Cte_Credito;
                    chkFacturar.Checked = cte.Cte_Facturacion;
                    ChkSucursal.Checked = cte.Cte_EsSucursal;
                    this.Mod.Visible = true;
                    TxtFechaMod.Text = cte.Cte_Modfecha.ToShortDateString();
                    TxtU_Nombre.Text = cte.U_Nombre;

                    if (cte.Cte_EsSucursal)
                    {
                        deshabilitarcontroles(RadPageViewDGrales.Controls, false);
                        deshabilitarcontroles(RadPageViewDetalles.Controls, false);
                        deshabilitarcontroles(RadPageViewParametros.Controls, false);

                    }

                    else
                    {
                        deshabilitarcontroles(RadPageViewDGrales.Controls, true);
                        deshabilitarcontroles(RadPageViewDetalles.Controls, true);
                        deshabilitarcontroles(RadPageViewParametros.Controls, true);

                    }
                    ChkSucursal.Enabled = false;
                    cmbMoneda.SelectedIndex = cmbMoneda.FindItemIndexByValue(cte.Id_Mon.ToString());
                    cmbMoneda.Text = cmbMoneda.FindItemByValue(cte.Id_Mon.ToString()).Text;
                    txtCobranza.Text = cte.Cte_LimCobr.ToString();
                    if (cte.Cte_RHoraam1 == string.Empty)
                        txtRHoraam1.SelectedDate = null;
                    else
                        txtRHoraam1.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cte.Cte_RHoraam1);

                    if (cte.Cte_RHoraam2 == string.Empty)
                        txtRHoraam2.SelectedDate = null;
                    else
                        txtRHoraam2.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cte.Cte_RHoraam2);

                    if (cte.Cte_RHorapm1 == string.Empty)
                        txtRHorapm1.SelectedDate = null;
                    else
                        txtRHorapm1.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cte.Cte_RHorapm1);

                    if (cte.Cte_RHorapm2 == string.Empty)
                        txtRHorapm2.SelectedDate = null;
                    else
                        txtRHorapm2.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cte.Cte_RHorapm2);
                    chkRLunes.Checked = cte.Cte_RLunes;
                    chkRMartes.Checked = cte.Cte_RMartes;
                    chkRMiercoles.Checked = cte.Cte_RMiercoles;
                    chkRJueves.Checked = cte.Cte_RJueves;
                    chkRViernes.Checked = cte.Cte_RViernes;
                    chkRSabado.Checked = cte.Cte_RSabado;
                    chkRDomingo.Checked = cte.Cte_RDomingo;
                    txtDias1.Text = cte.Cte_CondPago.ToString();
                    chkPLunes.Checked = cte.Cte_CPLunes;
                    chkPMartes.Checked = cte.Cte_CPMartes;
                    chkPMiercoles.Checked = cte.Cte_CPMiercoles;
                    chkPJueves.Checked = cte.Cte_CPJueves;
                    chkPViernes.Checked = cte.Cte_CPViernes;
                    chkPSabado.Checked = cte.Cte_CPSabado;
                    chkPDomingo.Checked = cte.Cte_CPDomingo;

                    chkComisiones.Checked = cte.Cte_Comisiones;
                    chkDesglose.Checked = cte.Cte_DesgIva;
                    chkRetencion.Checked = cte.Cte_RetIva;
                    ChkPorcientoIVA.Checked = cte.BPorcientoIVA;

                    if (cte.Id_Corp <= 0)
                        numCorporativo.Text = "";
                    else
                        numCorporativo.Text = cte.Id_Corp.ToString();
                    cmbCorporativa.SelectedIndex = cmbCorporativa.FindItemByValue(cte.Id_Corp.ToString()) == null ? 0 : cmbCorporativa.FindItemIndexByValue(cte.Id_Corp.ToString());
                    cmbCorporativa.Text = cmbCorporativa.FindItemByValue(cte.Id_Corp.ToString()) == null ? cmbCorporativa.Items[0].Text : cmbCorporativa.FindItemByValue(cte.Id_Corp.ToString()).Text;

                    cmbSerie.SelectedIndex = cmbSerie.FindItemIndexByValue(cte.Id_Cfe.ToString());
                    cmbSerie.Text = cmbSerie.FindItemByValue(cte.Id_Cfe.ToString()) == null ? "" : cmbSerie.FindItemByValue(cte.Id_Cfe.ToString()).Text;

                    cmbAsignacion.SelectedIndex = cmbAsignacion.FindItemIndexByValue(cte.Cte_AsignacionPed.ToString());
                    cmbAsignacion.Text = cmbAsignacion.FindItemByValue(cte.Cte_AsignacionPed.ToString()) == null ? "" : cmbAsignacion.FindItemByValue(cte.Cte_AsignacionPed.ToString()).Text;

                    cmbAdenda.SelectedIndex = cmbAdenda.FindItemByValue(cte.Id_Ade.ToString()) == null ? 0 : cmbAdenda.FindItemIndexByValue(cte.Id_Ade.ToString());
                    cmbAdenda.Text = cmbAdenda.FindItemByValue(cte.Id_Ade.ToString()) == null ? cmbAdenda.Items[0].Text : cmbAdenda.FindItemByValue(cte.Id_Ade.ToString()).Text;

                    rdActivo.Checked = cte.Estatus;

                    cmbNCargo.SelectedIndex = cmbNCargo.FindItemByValue(cte.Cte_SerieNCa.ToString()) == null ? 0 : cmbNCargo.FindItemIndexByValue(cte.Cte_SerieNCa.ToString());
                    cmbNCargo.Text = cmbNCargo.FindItemByValue(cte.Cte_SerieNCa.ToString()) == null ? cmbNCargo.Items[0].Text : cmbNCargo.FindItemByValue(cte.Cte_SerieNCa.ToString()).Text;

                    cmbNCredito.SelectedIndex = cmbNCredito.FindItemByValue(cte.Cte_SerieNCre.ToString()) == null ? 0 : cmbNCredito.FindItemIndexByValue(cte.Cte_SerieNCre.ToString());
                    cmbNCredito.Text = cmbNCredito.FindItemByValue(cte.Cte_SerieNCre.ToString()) == null ? cmbNCredito.Items[0].Text : cmbNCredito.FindItemByValue(cte.Cte_SerieNCre.ToString()).Text;

                    chkCredSuspendido.Checked = cte.Cte_CreditoSuspendido;
                    if (cte.Cte_PHoraam1 == string.Empty)
                        txtPHoraam1.SelectedDate = null;
                    else
                        txtPHoraam1.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cte.Cte_PHoraam1);

                    if (cte.Cte_PHoraam2 == string.Empty)
                        txtPHoraam2.SelectedDate = null;
                    else
                        txtPHoraam2.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cte.Cte_PHoraam2);

                    if (cte.Cte_PHorapm1 == string.Empty)
                        txtPHorapm1.SelectedDate = null;
                    else
                        txtPHorapm1.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cte.Cte_PHorapm1);

                    if (cte.Cte_PHorapm2 == string.Empty)
                        txtPHorapm2.SelectedDate = null;
                    else
                        txtPHorapm2.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cte.Cte_PHorapm2);






                    txtSemana.Text = cte.Cte_SemRec.ToString();
                    txtSemanaRevision.Text = cte.Cte_SemRev.ToString();
                    txtSemanaRevision2.Text = cte.Cte_SemRev2.ToString();
                    txtSemanaPago.Text = cte.Cte_SemCob.ToString();
                    chkRecLunes.Checked = cte.Cte_RecLunes;
                    chkRecMartes.Checked = cte.Cte_RecMartes;
                    chkRecMiercoles.Checked = cte.Cte_RecMiercoles;
                    chkRecJueves.Checked = cte.Cte_RecJueves;
                    chkRecViernes.Checked = cte.Cte_RecViernes;
                    chkRecSabado.Checked = cte.Cte_RecSabado;
                    chkRecDomingo.Checked = cte.Cte_RecDomingo;
                    chkEfectivo.Checked = cte.Cte_Efectivo;
                    chkFactoraje.Checked = cte.Cte_Factoraje;
                    chkCheque.Checked = cte.Cte_Cheque;
                    chkTransferencia.Checked = cte.Cte_Transferencia;
                    chkOrdenCompra.Checked = cte.Cte_ReqOrdenCompra;
                    txtDocumentos.Text = cte.Cte_Documentos.Replace("'", "");
                    txtTelCobranza1.Text = cte.Cte_TelCobranza1;
                    txtTelCobranza2.Text = cte.Cte_TelCobranza2;


                    txtReferenciaTecleada.Text = cte.Cte_ReferenciaTecleada;
                    txtNumeroCuenta.Text = cte.Cte_NumeroCuenta;
                    txtClientePortal.Text = cte.Cte_Portal;

                    cmbBancos.SelectedIndex = cmbBancos.FindItemByValue(cte.Id_Ban.ToString()) == null ? 0 : cmbBancos.FindItemIndexByValue(cte.Id_Ban.ToString());
                    cmbBancos.Text = cmbBancos.FindItemByValue(cte.Id_Ban.ToString()) == null ? cmbBancos.Items[0].Text : cmbBancos.FindItemByValue(cte.Id_Ban.ToString()).Text;

                    if (cte.Id_TCte <= 0)
                    {
                        txtIdTipoCliente.Text = "";
                    }
                    else
                    {
                        txtIdTipoCliente.Text = cte.Id_TCte.ToString();
                        cmbTipoCliente_SelectedIndexChanged(null, null);
                    }
                    cmbTipoCliente.SelectedIndex = cmbTipoCliente.FindItemByValue(cte.Id_TCte.ToString()) == null ? 0 : cmbTipoCliente.FindItemIndexByValue(cte.Id_TCte.ToString());
                    cmbTipoCliente.Text = cmbTipoCliente.FindItemByValue(cte.Id_TCte.ToString()) == null ? cmbTipoCliente.Items[0].Text : cmbTipoCliente.FindItemByValue(cte.Id_TCte.ToString()).Text;

                    cmbRemisionElect.SelectedIndex = cmbRemisionElect.FindItemIndexByValue(cte.Cte_RemisionElectronica.ToString());
                    cmbRemisionElect.Text = cmbRemisionElect.FindItemByValue(cte.Cte_RemisionElectronica.ToString()).Text;

                    chkNotaCredFac.Checked = cte.BPorcNotaCredito;
                    txtPorcFacturar.Text = cte.PorcientoNotaCredito.ToString();
                    txtPorcRetencion.Text = cte.PorcientoRetencion.ToString();

                    txtPorcientoIVA.Text = cte.PorcientoIVA.ToString();
                    if (ChkPorcientoIVA.Checked == true) txtPorcientoIVA.Enabled = true; else txtPorcientoIVA.Enabled = false;

                    txtUDigitos.Text = cte.Cte_UDigitos;
                    txtAutorizo.Text = cte.UPlazo;

                    clsCatClientes.ConsultarClienteFormaPago(ref cte, session.Emp_Cnx);
                    CargarFormaPago();
                    foreach (FormaPago fp in cte.FormasPago)
                    {
                        ListFPago.FindItemByValue(fp.Id_Fpa.ToString()).Checked = true;
                    }
                    GetListDet();
                    GetListDireccionesEntrega();
                    rgDireccionesEntrega.Rebind();
                    rgDetalles.Rebind();

                    CN_CatUsuario cn_catusuario = new CN_CatUsuario();
                    Usuario usu = new Usuario();
                    usu.Id_Emp = session.Id_Emp;
                    usu.Id_Cd = session.Id_Cd;
                    usu.Id_U = session.Id_U;
                    cn_catusuario.ConsultaUsuarios(ref usu, session.Emp_Cnx);

                    int dias = cte.Cte_DiasVencidos;
                    chkCredSuspendido.Enabled = usu.U_SusCredito && usu.U_DiasVencimiento >= dias;


                    txtCorreo1.Text = cte.Cte_CorreoEdoCuenta1;
                    txtCorreo2.Text = cte.Cte_CorreoEdoCuenta2;
                    txtCorreo3.Text = cte.Cte_CorreoEdoCuenta3;

                    //JMM:Validamos el check de cobranza
                    ValidarCheckCobranza();


                    try
                    {
                        cmbUsoCFDI.SelectedIndex = cmbUsoCFDI.FindItemIndexByValue(cte.Cte_UsoCFDI.ToString());
                        cmbUsoCFDI.Text = cmbUsoCFDI.FindItemByValue(cte.Cte_UsoCFDI.ToString()).Text;
                        cmbMetodoPago.SelectedIndex = cmbMetodoPago.FindItemIndexByValue(cte.Cte_MetodoPago.ToString());
                        cmbMetodoPago.Text = cmbMetodoPago.FindItemByValue(cte.Cte_MetodoPago.ToString()).Text;
                    }
                    catch
                    {
                        cmbUsoCFDI.SelectedIndex = cmbUsoCFDI.FindItemIndexByValue("G01");
                        cmbUsoCFDI.Text = cmbUsoCFDI.FindItemByValue("G01").Text;
                        cmbMetodoPago.SelectedIndex = cmbMetodoPago.FindItemIndexByValue("PPD");
                        cmbMetodoPago.Text = cmbMetodoPago.FindItemByValue("PPD").Text;

                    }


                    try
                    {

                        cmbPUsoCFDI.SelectedIndex = cmbUsoCFDI.FindItemIndexByValue(cte.Cte_PagoUsoCFDI.ToString());
                        cmbPUsoCFDI.Text = cmbUsoCFDI.FindItemByValue(cte.Cte_PagoUsoCFDI.ToString()).Text;
                        cmbPFormaPago.SelectedIndex = cmbPFormaPago.FindItemIndexByValue(cte.Cte_PagoMetodoPago.ToString());
                        cmbPFormaPago.Text = cmbPFormaPago.FindItemByValue(cte.Cte_PagoMetodoPago.ToString()).Text;

                    }
                    catch
                    {
                        cmbPUsoCFDI.SelectedIndex = 22;
                        cmbPUsoCFDI.Text = cmbPUsoCFDI.Items[22].Text;
                    }


                    //para pagos. deshabilita el combo d uso de cfdi
                    cmbPUsoCFDI.Enabled = false;




                }
                else
                    Nuevo();
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }

        protected void rgDetPrecio_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgDetPrecio.DataSource = dtClienteProducto;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void rgDireccionesEntrega_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    double ancho = 0;
                    foreach (GridColumn gc in rgDireccionesEntrega.Columns)
                    {
                        if (gc.Display)
                        {
                            ancho = ancho + gc.HeaderStyle.Width.Value;
                        }
                    }
                    int extra = 0;
                    if (dtDireccionEntrega.Rows.Count > 11)
                    {
                        extra = 20;
                    }
                    rgDireccionesEntrega.Width = Unit.Pixel(Convert.ToInt32(ancho) + extra);
                    rgDireccionesEntrega.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    rgDireccionesEntrega.DataSource = dtDireccionEntrega;
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }

        protected void rgDetalles_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
            }
        }
        protected void rgDetalles_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    double ancho = 0;
                    foreach (GridColumn gc in rgDetalles.Columns)
                    {
                        if (gc.Display)
                        {
                            ancho = ancho + gc.HeaderStyle.Width.Value;
                        }
                    }
                    int extra = 0;
                    if (dt.Rows.Count > 11)
                    {
                        extra = 20;
                    }
                    rgDetalles.Width = Unit.Pixel(Convert.ToInt32(ancho) + extra);
                    rgDetalles.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    rgDetalles.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }

        protected void rgDetPrecio_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                int Id_ClpDet = 0;
                int Tprecio = 0;
                string TPrecioStr = "";
                string Precio = "";
                double Pesos = 0;

                DataRow[] dr;

                GridItem gi = null;
                DataRow[] Ar_dr;

                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgDetPrecio.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        gi = e.Item;


                        if (((RadComboBox)gi.FindControl("RadComboBox11")).Text == "" ||
                            ((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Text == "")
                        {
                            e.Canceled = true;
                            break;
                        }

                        Tprecio = Convert.ToInt32(((RadComboBox)gi.FindControl("RadComboBox11")).SelectedValue);
                        TPrecioStr = ((RadComboBox)gi.FindControl("RadComboBox11")).Text;
                        Precio = ((RadComboBox)gi.FindControl("RadComboBox22")).FindItemByValue(((RadComboBox)gi.FindControl("RadComboBox11")).SelectedValue).Text;
                        Pesos = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Text);

                        Id_ClpDet = dtClienteProducto.Rows.Count;

                        dr = dtClienteProducto.Select("TPrecio='" + Tprecio + "'");
                        if (dr.Length > 0)
                        {
                            Alerta("Ya se ha establecido un tipo de precio " + TPrecioStr);
                            e.Canceled = true;
                            return;
                        }

                        dtClienteProducto.Rows.Add(new object[] {  
                                Id_ClpDet, 
                                Tprecio, 
                                TPrecioStr,
                                Precio,
                                Pesos});
                        break;
                    case "Update":
                        gi = e.Item;

                        if (((RadComboBox)gi.FindControl("RadComboBox11")).Text == "" ||
                           ((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Text == "")
                        {
                            e.Canceled = true;
                            break;
                        }

                        Tprecio = Convert.ToInt32(((RadComboBox)gi.FindControl("RadComboBox11")).SelectedValue);
                        TPrecioStr = ((RadComboBox)gi.FindControl("RadComboBox11")).Text;
                        Precio = ((RadComboBox)gi.FindControl("RadComboBox22")).FindItemByValue(((RadComboBox)gi.FindControl("RadComboBox11")).SelectedValue).Text;
                        Pesos = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Text);
                        Id_ClpDet = Convert.ToInt32(((Label)gi.FindControl("lblold0")).Text);

                        dr = dtClienteProducto.Select("TPrecio='" + Tprecio + "'");
                        //if (dr.Length > 0)
                        //{
                        //    Alerta("Ya se ha establecido un tipo de precio " + TPrecioStr);
                        //    e.Canceled = true;
                        //    return;
                        //}

                        Ar_dr = dtClienteProducto.Select("Id_ClpDet='" + Id_ClpDet + "'");
                        if (Ar_dr.Length > 0)
                        {
                            Ar_dr[0].BeginEdit();
                            Ar_dr[0]["Tprecio"] = Tprecio;
                            Ar_dr[0]["TPrecioStr"] = TPrecioStr;
                            Ar_dr[0]["Precio"] = Precio;
                            Ar_dr[0]["Pesos"] = Pesos;
                            Ar_dr[0].AcceptChanges();
                        }
                        break;
                    case "Delete":
                        gi = e.Item;
                        Id_ClpDet = Convert.ToInt32(((Label)gi.FindControl("Label0")).Text);
                        Ar_dr = dtClienteProducto.Select("Id_ClpDet='" + Id_ClpDet + "'");
                        if (Ar_dr.Length > 0)
                        {
                            Ar_dr[0].Delete();
                            dtClienteProducto.AcceptChanges();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgDireccionesEntrega_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Edit":
                        Edit(e);
                        break;
                    case "Update":
                        UpdateGridDireccionesEntrega(e);
                        break;
                    case "Delete":
                        DeleteGridDireccionesEntrega(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }

        protected void rgDetalles_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Edit":
                        Edit(e);
                        break;
                    case "InitInsert":
                        //Edit(e);
                        break;
                    case "PerformInsert":
                        PerformInsert(e);
                        //Edsg 19062017
                        ModificaTerritorios = true;
                        break;
                    case "Update":
                        Update(e);

                        break;
                    case "Delete":
                        Delete(e);
                        //Edsg 19062017
                        ModificaTerritorios = true;
                        break;
                    case "Comentarios":
                        break;
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }

        protected void ActualizarRegistroDeDetalle(string id_CteDet, string tradicional, string garantia)
        {
            SIANWEB.DataSets.CatCliente.CatClienteDS.CatClienteDetRow registroDetalle = DetalleDeClienteTerritorio(id_CteDet);
            registroDetalle["Tradicional"] = tradicional;
            registroDetalle["Garantia"] = garantia;
            DataSet.CatClienteDet.AcceptChanges();
        }

        protected void CrearRegistroDeDetalle(string id_CteDet, string tradicional, string garantia)
        {
            DataSet.CatClienteDet.AddCatClienteDetRow(id_CteDet, tradicional, garantia);
            DataSet.CatClienteDet.AcceptChanges();
        }

        protected void AsignarSeleccionDeGarantiasAlEditar(RadComboBox rcbTiposGarantia, string id_cteDet)
        {
            List<string> garantiasElegidas = new List<string>();
            foreach (RadComboBoxItem i in rcbTiposGarantia.Items)
            {
                HiddenField hf = i.FindControl("chkItemValue") as HiddenField;
                CheckBox chk = i.FindControl("chkItem") as CheckBox;
                var garantiasAsignadas = GarantiasDeClienteTerritorio(cmbCliente.SelectedValue, id_cteDet);
                var garantiaEncontrada = (from gr in garantiasAsignadas
                                          where gr["Id_TG"].ToString().CompareTo(hf.Value) == 0
                                          select gr).ToList();
                var descripcionesGarantias = (from dr in garantiaEncontrada
                                              join item in rcbTiposGarantia.Items
                                              on dr["Id_TG"].ToString() equals (item.FindControl("chkItemValue") as HiddenField).Value
                                              select (item.FindControl("chkItem") as CheckBox).Text).ToList();
                if (garantiaEncontrada.Count > 0)
                {
                    garantiasElegidas.Add(descripcionesGarantias[0]);
                    chk.Checked = true;
                    chk.Attributes.Add("data-descripcion_elegida", chk.Text);
                }
            }
            rcbTiposGarantia.Text = string.Join(",", garantiasElegidas);
        }

        protected List<DataSets.CatCliente.CatClienteDS.CatClienteDetGarantiaRow> GarantiasDeClienteTerritorio(string id_cte, string id_cteDet)
        {
            var ret = (from r in DataSet.CatClienteDetGarantia
                       where r["Id_CteDet"].ToString().CompareTo(id_cteDet) == 0
                       && r["Id_Emp"].ToString().CompareTo(_Sesion.Id_Emp.ToString()) == 0
                       && r["Id_Cd"].ToString().CompareTo(_Sesion.Id_Cd.ToString()) == 0
                       && r["Id_Cte"].ToString().CompareTo(id_cte) == 0
                       select r).ToList();
            return ret;
        }

        protected DataSets.CatCliente.CatClienteDS.CatClienteDetRow DetalleDeClienteTerritorio(string id_cteDet)
        {
            var ret = (from ct in DataSet.CatClienteDet
                       where ct["Id_CteDet"].ToString().CompareTo(id_cteDet) == 0
                       select ct).ToList();
            if (ret.Count > 0)
            {
                return ret[0];
            }
            return null;
        }

        protected void ActualizarGarantiasAsociadas(GridCommandEventArgs e, DataRow data)
        {
            RadComboBox rcbGarantias = e.Item.FindControl("rcbGarantiasEdit") as RadComboBox;

            if (rcbGarantias != null)
            {
                //Eliminar los registros en la tabla CatClienteDetGarantia en memoria que no se encuentren presentes en la lista de garantías.
                //Agregar un registro a la tabla CatClienteDetGarantia en memoria por cada tipo de garantía nuevo identificado en la lista de garantías.

                //DataRow data = e.Item.DataItem as DataRow;

                var tiposGarantiasSeleccionadas = (from elemento in rcbGarantias.Items
                                                   where (elemento.FindControl("chkItem") as CheckBox).Checked
                                                   select (elemento.FindControl("chkItemValue") as HiddenField).Value).ToList();

                var tiposDeGarantiaEnMemoriaAEliminar = (from r in DataSet.CatClienteDetGarantia
                                                         where !tiposGarantiasSeleccionadas.Contains(r["Id_TG"])
                                                         && r["Id_CteDet"].ToString().CompareTo(data["Id_CteDet"].ToString()) == 0
                                                         //&& r["Id_Emp"].ToString().CompareTo(data["Id_Emp"].ToString()) == 0 
                                                         //&& r["Id_Cd"].ToString().CompareTo(data["Id_Cd"].ToString()) == 0
                                                         //&& r["Id_Cte"].ToString().CompareTo(data["Id_Cte"].ToString()) == 0
                                                         select r).ToList();
                var tiposDeGarantiaEnMemoriaAInsertar = (from tg in tiposGarantiasSeleccionadas
                                                         where !DataSet.ExisteGarantiaEnTerritorio(data["Id_CteDet"].ToString(), tg)
                                                         select tg).ToList();
                foreach (var r in tiposDeGarantiaEnMemoriaAEliminar)
                {
                    r.Delete();
                }

                foreach (var tg in tiposDeGarantiaEnMemoriaAInsertar)
                {
                    DataSet.CatClienteDetGarantia.Rows.Add(_Sesion.Id_Emp, _Sesion.Id_Cd, cmbCliente.SelectedValue, data["Id_CteDet"], tg, 0);
                }

                DataSet.CatClienteDetGarantia.AcceptChanges();
            }
        }

        //------------------------------------------------------------------------------------------------
        /*aqui        
                protected void TipoTerritorio_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
                {
                    RadComboBox RadCombo = (sender as RadComboBox);
                    RadCombo.SelectedIndex = RadCombo.FindItemIndexByValue((RadCombo.Parent.FindControl("LabelTT") as Label).Text);
                }
                */
        protected void Territorio_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            RadComboBox RadCombo = (sender as RadComboBox);
            RadCombo.SelectedIndex = RadCombo.FindItemIndexByValue((RadCombo.Parent.FindControl("lblold1") as Label).Text);

            //aqui
            //(RadCombo.Parent.Parent.FindControl("RadComboBox1") as RadComboBox).Enabled = (RadCombo.Parent.Parent.FindControl("RadTipoTerritorio") as RadComboBox).Enabled;

        }
        protected void Segmento_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            RadComboBox RadCombo = (sender as RadComboBox);
            RadCombo.SelectedIndex = RadCombo.FindItemIndexByValue((RadCombo.Parent.FindControl("lblold2") as Label).Text);
        }
        protected void cmbTerritorio_DataBinding(object sender, EventArgs e)
        {
            try
            {

                if (terr)
                    return;
                terr = true;
                RadComboBox comboBox = ((RadComboBox)sender);// new RadComboBox() ;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatTerritorio_Combo", ref comboBox);


                /* aqui if (terr)
                     return;
                 terr = true;

                 RadComboBox comboBox = ((RadComboBox)sender);// new RadComboBox() ;
                 Sesion Sesion = new Sesion();
                 Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                 CapaNegocios.CN__Comun CN_ComunC = new CapaNegocios.CN__Comun();

                 if ((comboBox.Parent.Parent.FindControl("RadTipoTerritorio") as RadComboBox).SelectedValue.ToString()== string.Empty)
                 {
                     (comboBox.Parent.Parent.FindControl("RadTipoTerritorio") as RadComboBox).SelectedValue = "-1";
                 }
                 */
                //aqui
                ////CN_ComunC.LlenaComboTerr(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Id_Rik, Convert.ToInt32((comboBox.Parent.Parent.FindControl("RadTipoTerritorio") as RadComboBox).SelectedValue), Sesion.Emp_Cnx, "spCatTerritorio_Combo", ref comboBox);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // SAUL GUERRA 20150506 BEGIN
        protected void cbTerServ_DataBinding(object sender, EventArgs e)
        {
            try
            {
                if (terrServ)
                    return;
                terrServ = true;

                RadComboBox comboBox = ((RadComboBox)sender);
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Emp_Cnx, "spCatTerritorioServ_Combo", ref comboBox);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void cbTerServ_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            RadComboBox RadCombo = (sender as RadComboBox);
            RadCombo.SelectedIndex = RadCombo.FindItemIndexByValue((RadCombo.Parent.FindControl("hdTerServ") as Label).Text);
        }

        protected void cbTerServ_TextChanged(object sender, EventArgs e)
        {
            GridEditableItem gi = null;
            using (RadComboBox rdcbx = (sender as RadComboBox))
            {
                gi = rdcbx.NamingContainer as GridEditableItem;

                ((Label)gi.FindControl("lblId_TerServEdit")).Text = rdcbx.SelectedValue;

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_CatRepresentantes cn_CatRep = new CN_CatRepresentantes();
                Territorios terServ = new Territorios();
                terServ.Id_Emp = sesion.Id_Emp;
                terServ.Id_Cd = sesion.Id_Cd_Ver;
                terServ.Id_Ter = Convert.ToInt32("0" + rdcbx.SelectedValue);
                Representantes RIK = null;
                cn_CatRep.ConsultarRepresentantePorTerritorio(terServ, sesion.Emp_Cnx, ref RIK);
                ((Label)gi.FindControl("lblId_RIKServEdit")).Text = Convert.ToString(RIK.Id_Rik);
                ((Label)gi.FindControl("lblRIKServEdit")).Text = RIK.Nombre;
            }
        }
        // SAUL GUERRA 20150506 END

        /*
        protected void cmbTipoTerritorio_DataBinding(object sender, EventArgs e)
        {
            try
            {
                if (Tipoterr)
                    return;
                Tipoterr = true;

                RadComboBox comboBox = ((RadComboBox)sender);// new RadComboBox() ;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1,Sesion.Id_Emp,Sesion.Emp_Cnx, "spCatTipoRepresentante_Combo", ref comboBox);

                //comboBox.Items.Remove(0);
                //comboBox.SelectedIndex = 0;
                //comboBox.SelectedValue = comboBox.Items[0].Value;
                //comboBox.Text = comboBox.Items[0].Text;
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
         * */

        protected void cmbSegmento_DataBinding(object sender, EventArgs e)
        {
            try
            {
                RadComboBox cmb = sender as RadComboBox;
                CargarSegmentoDet(cmb);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------------------------------------------------------------------------------
        /*aqui
        protected void cmbTipoTerritorioDet_TextChanged(object sender, EventArgs e)
        {
            try
            {

                CargarTerritorioDet(((sender as RadComboBox).Parent.Parent.FindControl("RadComboBox1") as RadComboBox));
                //RadComboBox cmb = (sender as RadComboBox);
                //(cmb.Parent.Parent.FindControl("RadComboBox1") as RadComboBox).SelectedValue = "-1";
            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }
          
        }
        */
        protected void cmbTerritorioDet_TextChanged(object sender, EventArgs e)
        {
            CargarSegmentoDet(((sender as RadComboBox).Parent.Parent.FindControl("RadComboBox2") as RadComboBox));
        }


        private void CargarTerritorioDet(RadComboBox comboBox)
        {
            try
            {
                comboBox.SelectedIndex = 0;
                comboBox.Text = comboBox.Items[0].Text;
                comboBox.Text = "";


                //comboBox.Parent..Parent.FindControl("lblold1") as Label).Text="";

                (comboBox.Parent.Parent.FindControl("txtId_Ter") as RadNumericTextBox).Value = null;


                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                //aqui
                CN_Comun.LlenaComboTerr
                (Sesion.Id_Emp,
                Sesion.Id_Cd_Ver, Sesion.Id_Rik,
                    /*Convert.ToInt32((comboBox.Parent.Parent.FindControl("RadTipoTerritorio") as RadComboBox).SelectedValue)*/3,
                Sesion.Emp_Cnx, "spCatTerritorio_Combo", ref comboBox);

            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }


        }

        protected void cmbSegmento_TextChanged(object sender, EventArgs e)
        {
            int valorRes = 0;
            bool IsNumeric = false;

            RadComboBox cmb = (sender as RadComboBox);


            IsNumeric = int.TryParse((cmb.Parent.Parent.FindControl("RadComboBox1") as RadComboBox).SelectedValue.ToString(), out valorRes);

            if (IsNumeric && valorRes > 0) { }
            else
            { return; }



            if ((Convert.ToInt32(cmb.SelectedValue)) > 0)
            {

                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatTerritorios cn_catter = new CN_CatTerritorios();
                Territorios terr = new Territorios();
                terr.Id_Emp = sesion.Id_Emp;
                terr.Id_Cd = sesion.Id_Cd_Ver;
                terr.Id_Ter = Convert.ToInt32((cmb.Parent.Parent.FindControl("RadComboBox1") as RadComboBox).SelectedValue);
                cn_catter.ConsultaTerritorios(ref terr, sesion.Emp_Cnx);
                //CargarSegmentoDet(((sender as RadComboBox).Parent.Parent.FindControl("RadComboBox2") as RadComboBox));
                (cmb.Parent.Parent.FindControl("lblUENDetEdit") as Label).Text = terr.Uen_Descripcion;
                (cmb.Parent.Parent.FindControl("lblIDUENEdit") as Label).Text = terr.Id_Uen.ToString();
                (cmb.Parent.Parent.FindControl("txtId_Uen") as RadNumericTextBox).Text = terr.Id_Uen.ToString();
                (cmb.Parent.Parent.FindControl("lblRikDetEdit") as Label).Text = terr.Rik_Nombre;
                (cmb.Parent.Parent.FindControl("LabelRikEdit") as Label).Text = terr.Id_Rik.ToString();

                CN_CatSegmentos cn_segmento = new CN_CatSegmentos();
                List<Segmentos> seg = new List<Segmentos>();

                cn_segmento.ConsultaSegmentos(sesion.Id_Emp, Convert.ToInt32(cmb.SelectedValue), sesion.Emp_Cnx, ref seg);
                (cmb.Parent.Parent.FindControl("RadTextBox3") as Label).Text = seg[0].Unidades;
                (cmb.Parent.Parent.FindControl("RadNumericTextBox5") as RadNumericTextBox).Value = seg[0].Valor;//RadTextBox4

                GetTablaPermisosUEN();

                List<int> uen_no_dimension = new List<int>();
                List<int> uen_modifica_potencial = new List<int>();

                foreach (DataRow dr in TablaPermisosUEN.Rows)
                {
                    if (!dr.IsNull("Id_UenDimension"))
                    {
                        int Id_UenPotencial = Convert.ToInt32(dr["Id_UenDimension"].ToString());
                        uen_no_dimension.Add(Id_UenPotencial);
                    }
                    if (!dr.IsNull("Id_UenPotencial"))
                    {
                        int Id_UenPotencial = Convert.ToInt32(dr["Id_UenPotencial"].ToString());
                        uen_modifica_potencial.Add(Id_UenPotencial);
                    }
                }

                //int[] uen_no_dimension = new int[] { 2, 3 };
                //int[] uen_modifica_potencial = new int[] { 2, 3 };

                (cmb.Parent.Parent.FindControl("RadTextBox4") as RadNumericTextBox).Enabled = uen_no_dimension.Contains(terr.Id_Uen);
                (cmb.Parent.Parent.FindControl("RadNumericTextBox6") as RadNumericTextBox).Enabled = uen_modifica_potencial.Contains(terr.Id_Uen);
            }
        }


        //------------------------------------------------------------------------------------------------




        protected void cmbBancos_DataBinding(object sender, EventArgs e)
        {
            try
            {
                if (banco)
                {
                    banco = false;
                    return;
                }
                banco = true;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                RadComboBox ComboBox = sender as RadComboBox;
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatBanco_Combo", ref ComboBox);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            DataRow[] Ar_dr;
            Ar_dr = dt.Select("Id_CteDet='" + ((CheckBox)sender).ToolTip.Replace("Cons. ", "") + "'");
            if (Ar_dr.Length > 0)
            {
                Ar_dr[0].BeginEdit();
                Ar_dr[0]["Cte_Activo"] = ((CheckBox)sender).Checked;
                Ar_dr[0].AcceptChanges();
            }
        }
        protected void rdActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked && HF_ID.Value != "")
                if (!Deshabilitar())
                {
                    Alerta("El registro está siendo utilizado por otro componente");
                    ((CheckBox)sender).Checked = true;
                }
        }
        protected void chkActivoDet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                CheckBox send = (sender as CheckBox);
                string activo = "";
                //RMB
                //Inicio
                AutClienteTerritorio ClienteTerritorio = new AutClienteTerritorio();
                ClienteTerritorio.Activo = send.Checked;
                

                DataRow[] Ar_drt;
                string Id_Ter1 = ((Label)send.Parent.FindControl("Label100")).Text;
                Ar_drt = dt.Select("Id_Ter='" + Id_Ter1 + "'");
                string Fec_Solicitud  = Ar_drt[0]["Fec_Solicitud"].ToString();
                string Fec_Autorizado = Ar_drt[0]["Fec_Autorizado"].ToString();
                string Fec_Rechazado  = Ar_drt[0]["Fec_Rechazado"].ToString();

                if (Fec_Solicitud != "")
                {
                    if (Fec_Autorizado == "" && Fec_Rechazado == "")
                    {
                        Alerta("Este territorio esta pendiente de una autorización de cambio, no se puede realizar otro cambio hasta que sea atendida la anterior.");
                        rgDetalles.Rebind();
                        return;
                    }
                }
                //Fin
                if (send.Checked)
                {
                    //Edsg28062017
                    CentroDistribucion cd = new CentroDistribucion();
                    cd.Id_Emp = session.Id_Emp;
                    CN_CatCentroDistribucion clsCatCd = new CN_CatCentroDistribucion();
                    var puedeVariasUEN = clsCatCd.ValidarVariasUEN(session.Id_Emp, session.Id_Cd, (int)txtClave.Value, session.Emp_Cnx);


                    string uenStr = (send.Parent.FindControl("txtId_UenIT") as RadNumericTextBox).Text;
                    int coincidencias = dt.Select("Id_Uen='" + uenStr + "' and Cte_Activo=true and Id_Seg <> 12").Length;
                    string Id_Seg = ((Label)send.Parent.FindControl("Label121")).Text;

                    //Edsg28062017
                    if ((coincidencias > 0 && Id_Seg != "12") && !puedeVariasUEN)
                    {
                        Alerta("No puede haber dos o mas territorios con el mismo UEN activos");
                        (sender as CheckBox).Checked = false;
                        return;


                    }
                    else
                    {
                        activo = "true";
                    }
                }
                else
                {
                    activo = "false";
                }

                DataRow[] Ar_dr;
                string Id_Ter = ((Label)send.Parent.FindControl("Label100")).Text;
                Ar_dr = dt.Select("Id_Ter='" + Id_Ter + "'");
                Ar_dr[0].BeginEdit();
                Ar_dr[0]["Fec_Solicitud"] = DateTime.Now.ToShortDateString();
                Ar_dr[0]["Fec_Autorizado"] = DBNull.Value;
                Ar_dr[0]["Fec_Rechazado"] = DBNull.Value;
                Ar_dr[0]["Cte_Activo"] = activo;
                Ar_dr[0].AcceptChanges();
                //this.ModificaTerritorios = true;

                rgDetalles.Rebind();
                bool bactivo;
                if (activo == "true")
                    bactivo = true;
                else
                    bactivo = false;

                CargarTerritoriosAutorizacion(Ar_dr[0].ItemArray, "Activo", bactivo);
                CargarTerritoriosAutorizacionAnt(Ar_dr[0].ItemArray, "Activo", bactivo);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        //RBM
        //Se inicializan las tablas y se cargan para los cambios de territorios
        //Inicio
        private void InicializarTablaTerritoriosAnt()
        {
            try
            {
                objdtListaTerritoriosAnt = new DataTable();
                objdtListaTerritoriosAnt.Columns.Add("Id_Emp");
                objdtListaTerritoriosAnt.Columns.Add("Id_Cd");
                objdtListaTerritoriosAnt.Columns.Add("Id_Cte");
                objdtListaTerritoriosAnt.Columns.Add("Nom_Cliente");
                objdtListaTerritoriosAnt.Columns.Add("Id_Ter");
                objdtListaTerritoriosAnt.Columns.Add("Nom_Territorio");
                objdtListaTerritoriosAnt.Columns.Add("Dimension");
                objdtListaTerritoriosAnt.Columns.Add("Pesos");
                objdtListaTerritoriosAnt.Columns.Add("Potencial");
                objdtListaTerritoriosAnt.Columns.Add("manodeobra");
                objdtListaTerritoriosAnt.Columns.Add("gastosTerritorio");
                objdtListaTerritoriosAnt.Columns.Add("fletespagadosporcliente");
                objdtListaTerritoriosAnt.Columns.Add("porcentaje");
                objdtListaTerritoriosAnt.Columns.Add("activo");
                objdtListaTerritoriosAnt.Columns.Add("nuevo");
                objdtListaTerritoriosAnt.Columns.Add("Id_Solicitud");
                objdtTablaTerritoriosAnt = objdtListaTerritoriosAnt;

            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void InicializarTablaTerritorios()
        {
            try
            {
                objdtListaTerritorios = new DataTable();
                objdtListaTerritorios.Columns.Add("Id_Emp");
                objdtListaTerritorios.Columns.Add("Id_Cd");
                objdtListaTerritorios.Columns.Add("Id_Cte");
                objdtListaTerritorios.Columns.Add("Nom_Cliente");
                objdtListaTerritorios.Columns.Add("Id_Ter");
                objdtListaTerritorios.Columns.Add("Nom_Territorio");
                objdtListaTerritorios.Columns.Add("Dimension");
                objdtListaTerritorios.Columns.Add("Pesos");
                objdtListaTerritorios.Columns.Add("Potencial");
                objdtListaTerritorios.Columns.Add("manodeobra");
                objdtListaTerritorios.Columns.Add("gastosTerritorio");
                objdtListaTerritorios.Columns.Add("fletespagadosporcliente");
                objdtListaTerritorios.Columns.Add("porcentaje");
                objdtListaTerritorios.Columns.Add("activo");
                objdtListaTerritorios.Columns.Add("nuevo");
                objdtListaTerritorios.Columns.Add("comentarios");
                objdtListaTerritorios.Columns.Add("Id_Solicitud");
                objdtTablaTerritorios = objdtListaTerritorios;

            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }

        }

        private void CargarTerritoriosAutorizacionAnt(object[] Territorio, string tipo, bool estatus)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                AutClienteTerritorio AutTerritorio = new AutClienteTerritorio();
                bool activo;
                bool nuevo;
                string Cte_Activo = Territorio[11].ToString();
                if (tipo != "Edit")
                {
                    if (Cte_Activo == "false"  && tipo == "Activo")
                        activo = true;
                    else
                        activo = false;
                }
                else
                    activo = bool.Parse(Territorio[11].ToString());

                if (tipo == "nuevo")
                    nuevo = true;
                else
                    nuevo = false;
              
                

                ArrayList ArrayTer = new ArrayList();
                ArrayTer.Add(session.Id_Emp);
                ArrayTer.Add(session.Id_Cd_Ver);
                ArrayTer.Add(Territorio[27]);
                ArrayTer.Add(txtDescripcion.Text);
                ArrayTer.Add(Territorio[3]);
                ArrayTer.Add(Territorio[4]);
                ArrayTer.Add(Territorio[8]);
                ArrayTer.Add(Territorio[9]);
                ArrayTer.Add(Territorio[10]);
                ArrayTer.Add(Territorio[19]);
                ArrayTer.Add(Territorio[20]);
                ArrayTer.Add(Territorio[21]);
                ArrayTer.Add(Territorio[22]);
                ArrayTer.Add(activo);
                ArrayTer.Add(nuevo);
                ArrayTer.Add(0);

                objdtTablaTerritoriosAnt.Rows.Add(ArrayTer.ToArray());
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }

        }
        private void CargarTerritoriosAutorizacion(object[] Territorio, string tipo, bool estatus)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                AutClienteTerritorio AutTerritorio = new AutClienteTerritorio();
                bool nuevo;

                if (tipo == "nuevo")
                    nuevo = true;
                else
                    nuevo = false;

                ArrayList ArrayTer = new ArrayList();
                ArrayTer.Add(session.Id_Emp);
                ArrayTer.Add(session.Id_Cd_Ver);
                ArrayTer.Add(Territorio[27]);
                ArrayTer.Add(txtDescripcion.Text);
                ArrayTer.Add(Territorio[3]);
                ArrayTer.Add(Territorio[4]);
                ArrayTer.Add(Territorio[8]);
                ArrayTer.Add(Territorio[9]);
                ArrayTer.Add(Territorio[10]);
                ArrayTer.Add(Territorio[19]);
                ArrayTer.Add(Territorio[20]);
                ArrayTer.Add(Territorio[21]);
                ArrayTer.Add(Territorio[22]);
                ArrayTer.Add(estatus);
                ArrayTer.Add(nuevo);
                ArrayTer.Add(null);
                ArrayTer.Add(0);

                objdtTablaTerritorios.Rows.Add(ArrayTer.ToArray());
                EnviarComentarios(int.Parse(Territorio[3].ToString()));
             }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }

        }

        //Fin
        protected void chkRetencion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRetencion.Checked == true)
                txtPorcRetencion.Enabled = true;
            else
                txtPorcRetencion.Enabled = false;
        }
        protected void chkPorcientoIVA_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkPorcientoIVA.Checked == true)
                txtPorcientoIVA.Enabled = true;
            else
                txtPorcientoIVA.Enabled = false;
        }

        protected void Click_BtnAgregarProducto(object sender, EventArgs e)
        {

            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                ClienteProd clienteprod = new ClienteProd();
                clienteprod.Id_Emp = session.Id_Emp;
                clienteprod.Id_Cd = session.Id_Cd_Ver;
                clienteprod.Id_Cte = Convert.ToInt32(txtClave.Text);//!string.IsNullOrEmpty(txtClienteID.Text) ? Convert.ToInt32(txtClienteID.Text) : 0;
                clienteprod.Id_Prd = !string.IsNullOrEmpty(txtProductoID.Text) ? Convert.ToInt32(txtProductoID.Text) : 0;
                clienteprod.Clp_descripcion = txtDescripcionX.Text;
                clienteprod.Estatus = true;//chkActivo.Checked;
                clienteprod.Id_Clp = !string.IsNullOrEmpty(txtClaveX.Text) ? txtClaveX.Text : "";
                clienteprod.Clp_Presentacion = txtPresentacion.Text;
                clienteprod.Unidades = txtUnidades.Text;
                clienteprod.CantFact = 0;//!string.IsNullOrEmpty(txtCantFact.Text) ? Convert.ToInt32(txtCantFact.Text) : 0;

                CN_CatClienteProd clsCatClienteProd = new CN_CatClienteProd();
                int verificador = -1;
                if (clienteprod.Id_Cte == 0)
                {
                    Alerta("Agregue un cliente");
                    return;
                }
                if (clienteprod.Id_Prd == 0)
                {
                    Alerta("Agregue un producto");
                    return;
                }
                if (HF_IdCP.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }

                    clsCatClienteProd.InsertarClienteProd(clienteprod, session.Emp_Cnx, ref verificador);
                    //                    Alerta("Los datos se guardaron correctamente");
                    if (verificador == 1)
                    {
                        clsCatClienteProd.InsertarClienteProdDet(clienteprod, dtClienteProducto, session.Emp_Cnx, ref verificador);
                        //Nuevo();
                        Alerta("Los datos se guardaron correctamente");

                    }
                    else
                        Alerta("La clave ya existe");
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }

                    clsCatClienteProd.ModificarClienteProd(clienteprod, session.Emp_Cnx, ref verificador);
                    //                    Alerta("Los datos se modificaron correctamente");
                    if (verificador == 1)
                    {
                        clsCatClienteProd.ModificarClienteProdDet(clienteprod, dtClienteProducto, session.Emp_Cnx, ref verificador);
                        //Nuevo();
                        Alerta("Los datos se modificaron correctamente");
                    }
                    else
                        Alerta("Ocurrió un error al intentar modificar los datos");
                }

                txtClaveX.Enabled = true;
                txtClaveX.Text = "";
                //txtAsignado.Text = string.Empty;
                //txtCantFact.Text = string.Empty;
                //txtClienteID.Text = string.Empty;
                txtDescripcionX.Text = string.Empty;
                //txtInventarioFin.Text = string.Empty;
                txtPresentacion.Text = string.Empty;
                txtProductoID.Text = string.Empty;
                txtUnidades.Text = string.Empty;
                //cmbCliente.ClearSelection();
                //cmbCliente.Text = "";
                cmbProducto.ClearSelection();
                cmbProducto.Text = "";
                //dpUltimaVta.SelectedDate = null;
                //chkActivo.Checked = true;
                HF_IdCP.Value = "";
                GetListDetClienteProducto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Click_BtnAgregarDirEntrega(object sender, EventArgs e)
        {


            if ((txtEHoraam1.SelectedDate == null) || (txtEHoraam2.SelectedDate == null) || (txtEHorapm1.SelectedDate == null) || (txtEHorapm2.SelectedDate == null))
            {
                Alerta("Los Horarios de Recepción son requeridos");

                return;
            }

            dtDireccionEntrega.Rows.Add(new object[] {  
                                1, 
                                dtDireccionEntrega.Rows.Count,
                                txtEcalle.Text, 
                                txtEnumero.Text,
                                txtEcp.Text,
                                txtEcolonia.Text, 
                                txtEmunicipio.Text,
                                txtEestado.Text,
                                txtESector.Text, 
                                txtEtelefono.Text,
                                txtEfax.Text,
                                txtEHoraam1.SelectedDate.Value.ToString("hh:mm tt"),
                                txtEHoraam2.SelectedDate.Value.ToString("hh:mm tt"),
                                txtEHorapm1.SelectedDate.Value.ToString("hh:mm tt"),
                                txtEHorapm2.SelectedDate.Value.ToString("hh:mm tt")
            });

            rgDireccionesEntrega.Rebind();

            txtEHoraam1.SelectedDate = null;
            txtEHoraam2.SelectedDate = null;
            txtEHorapm1.SelectedDate = null;
            txtEHorapm2.SelectedDate = null;
            txtEcalle.Text = string.Empty;
            txtEnumero.Text = string.Empty;
            txtEcp.Text = string.Empty;
            chkClonarDireccionFiscal.Checked = false;
            txtEcolonia.Text = string.Empty;
            txtEmunicipio.Text = string.Empty;
            txtEestado.Text = string.Empty;
            txtESector.Text = string.Empty;
            txtEtelefono.Text = string.Empty;
            txtEfax.Text = string.Empty;
        }
        protected void rcbGarantiasEdit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            //CheckBox chk = e.Item.FindControl("chkItem") as CheckBox;
            //HiddenField hf = e.Item.FindControl("chkItemValue") as HiddenField;
            //RadComboBox rcb = sender as RadComboBox;
            //chk.InputAttributes.Add("onclick", "rcbGarantiasEdit_itemClick(this, '" + rcb.ClientID + "', '" + hf.ClientID + "', '" + chk.Text + "')");
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                CargarCuentasCorporativas();
                CargarBanco();
                CargarTmoneda();
                CargarCFDIUso();
                CargarCFDIUsoParaPagos();
                CargarFormaPagoParaPagos();

                CargarMetodoPago();
                CargarConsecutivos();
                CargarTipoCliente();
                CargarAsignar();
                CargarAdenda();
                CargarNcre();
                CargarNca();
                CargarFormaPago();
                CargarProductos();
                Nuevo();
                GetListDet();
                GetListDireccionesEntrega();
                rgDetalles.Rebind();
                txtClave.Text = MaximoId();
                txtClave.Focus();

                //RBM
                //Tabla de Territorios para autorizar
                //Inicio
                InicializarTablaTerritoriosAnt();
                InicializarTablaTerritorios();
                //Fin


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarSegmentoDet(RadComboBox comboBox)
        {
            if ((comboBox.Parent.Parent.FindControl("RadComboBox1") as RadComboBox).SelectedValue == "")
            {
                return;
            }

            if (Seg || Convert.ToInt32((comboBox.Parent.Parent.FindControl("RadComboBox1") as RadComboBox).SelectedValue) == -1)
            {
                (comboBox.Parent.FindControl("lblId_Seg") as Label).Text = "";
                (comboBox.Parent.Parent.FindControl("lblUENDetEdit") as Label).Text = "";
                (comboBox.Parent.Parent.FindControl("lblIDUENEdit") as Label).Text = "";
                (comboBox.Parent.Parent.FindControl("txtId_Uen") as RadNumericTextBox).Text = "";
                (comboBox.Parent.Parent.FindControl("lblRikDetEdit") as Label).Text = "";
                (comboBox.Parent.Parent.FindControl("LabelRikEdit") as Label).Text = "";
                (comboBox.Parent.Parent.FindControl("RadTextBox3") as Label).Text = "";
                (comboBox.Parent.Parent.FindControl("RadNumericTextBox5") as RadNumericTextBox).Value = 0;

                return;
            }

            Seg = true;
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32((comboBox.Parent.Parent.FindControl("RadComboBox1") as RadComboBox).SelectedValue), Sesion.Emp_Cnx, "spCatSegmentosTer_Combo", ref comboBox);
            if (comboBox.Items.Count > 1)
            {
                comboBox.SelectedIndex = 1;
                comboBox.Text = comboBox.Items[1].Text;
                cmbSegmento_TextChanged(comboBox, null);

                //(comboBox.Parent.FindControl("LabelSeg_Descripcion") as Label).Text = comboBox.Items[1].Text;
                (comboBox.Parent.FindControl("lblId_Seg") as Label).Text = comboBox.Items[1].Value;

                if ((comboBox.Parent.FindControl("RadTextBox4") as RadNumericTextBox).Enabled)
                {
                    (comboBox.Parent.FindControl("RadTextBox4") as RadNumericTextBox).Focus();
                }
                else
                {
                    (comboBox.Parent.Parent.FindControl("RadNumericTextBox6") as RadNumericTextBox).Focus();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty((comboBox.Parent.Parent.FindControl("txtId_Ter") as RadNumericTextBox).Text))
                {
                    int ter = Convert.ToInt32((comboBox.Parent.Parent.FindControl("txtId_Ter") as RadNumericTextBox).Text);
                    if (ter > 0)
                    {
                        //(comboBox.Parent.FindControl("LabelSeg_Descripcion") as Label).Text = "";
                        (comboBox.Parent.FindControl("lblId_Seg") as Label).Text = "";
                        (comboBox.Parent.Parent.FindControl("lblUENDetEdit") as Label).Text = "";
                        (comboBox.Parent.Parent.FindControl("lblIDUENEdit") as Label).Text = "";
                        (comboBox.Parent.Parent.FindControl("txtId_Uen") as RadNumericTextBox).Text = "";
                        (comboBox.Parent.Parent.FindControl("lblRikDetEdit") as Label).Text = "";
                        (comboBox.Parent.Parent.FindControl("LabelRikEdit") as Label).Text = "";
                        (comboBox.Parent.Parent.FindControl("RadTextBox3") as Label).Text = "";
                        (comboBox.Parent.Parent.FindControl("RadNumericTextBox5") as RadNumericTextBox).Value = 0;
                        AlertaFocus("El territorio no tiene un segmento configurado", ((RadNumericTextBox)comboBox.Parent.Parent.FindControl("txtId_Ter")).ClientID);
                    }
                }
            }

            Sesion session2 = new Sesion();
            session2 = (Sesion)Session["Sesion" + Session.SessionID];
            int Tipo_CDC = 0;
            new CN_CatCliente().ConsultaTipoCDC(session2.Id_Cd_Ver, ref Tipo_CDC, session2.Emp_Cnx);
            if (Tipo_CDC == 1)
                comboBox.Enabled = true;
            else
                comboBox.Enabled = false;
        }
        private void CargarFormaPago()
        {//ListFPago
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaListBox(Sesion.Id_Emp, -1, Sesion.Emp_Cnx, "spCatFormaPago_Combo", ref ListFPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarMetodoPago()
        {
            cmbMetodoPago.Items.Clear();
            DataTable Dt = new DataTable();
            Dt.Columns.Add("Descripcion");
            Dt.Columns.Add("Id");
            Dt.Rows.Add(new object[] { "-- Seleccionar --", "-1" });
            Dt.Rows.Add(new object[] { "Pago en una sola exhibición", "PUE" });
            Dt.Rows.Add(new object[] { "Pago en parcialidades o diferido", "PPD" });



            cmbMetodoPago.DataSource = Dt;
            cmbMetodoPago.DataValueField = "Id";
            cmbMetodoPago.DataTextField = "Descripcion";
            cmbMetodoPago.DataBind();
        }
        private void CargarCFDIUso()
        {
            cmbUsoCFDI.Items.Clear();
            DataTable Dt = new DataTable();
            Dt.Columns.Add("Descripcion");
            Dt.Columns.Add("Id");
            Dt.Rows.Add(new object[] { "-- Seleccionar --", "-1" });
            Dt.Rows.Add(new object[] { "Adquisición de mercancias", "G01" });
            Dt.Rows.Add(new object[] { "Devoluciones, descuentos o bonificaciones", "G02" });
            Dt.Rows.Add(new object[] { "Gastos en general", "G03" });
            Dt.Rows.Add(new object[] { "Construcciones", "I01" });
            Dt.Rows.Add(new object[] { "Mobilario y equipo de oficina por inversiones", "I02" });
            Dt.Rows.Add(new object[] { "Equipo de transporte", "I03" });
            Dt.Rows.Add(new object[] { "Equipo de computo y accesorios", "I04" });
            Dt.Rows.Add(new object[] { "Dados, troqueles, moldes, matrices y herramental", "I05" });
            Dt.Rows.Add(new object[] { "Comunicaciones telefónicas", "I06" });
            Dt.Rows.Add(new object[] { "Comunicaciones satelitales", "I07" });
            Dt.Rows.Add(new object[] { "Otra maquinaria y equipo", "I08" });
            Dt.Rows.Add(new object[] { "Honorarios médicos, dentales y gastos hospitalarios.", "D01" });
            Dt.Rows.Add(new object[] { "Gastos médicos por incapacidad o discapacidad", "D02" });
            Dt.Rows.Add(new object[] { "Gastos funerales.", "D03" });
            Dt.Rows.Add(new object[] { "Donativos.", "D04" });
            Dt.Rows.Add(new object[] { "Intereses reales efectivamente pagados por créditos hipotecarios (casa habitación).", "D05" });
            Dt.Rows.Add(new object[] { "Aportaciones voluntarias al SAR.", "D06" });
            Dt.Rows.Add(new object[] { "Primas por seguros de gastos médicos.", "D07" });
            Dt.Rows.Add(new object[] { "Gastos de transportación escolar obligatoria.", "D08" });
            Dt.Rows.Add(new object[] { "Depósitos en cuentas para el ahorro, primas que tengan como base planes de pensiones.", "D09" });
            Dt.Rows.Add(new object[] { "Pagos por servicios educativos (colegiaturas)", "D10" });
            Dt.Rows.Add(new object[] { "Por definir", "P01" });


            cmbUsoCFDI.DataSource = Dt;
            cmbUsoCFDI.DataValueField = "Id";
            cmbUsoCFDI.DataTextField = "Descripcion";
            cmbUsoCFDI.DataBind();
        }
        private void CargarCFDIUsoParaPagos()
        {
            cmbPUsoCFDI.Items.Clear();
            DataTable Dt = new DataTable();
            Dt.Columns.Add("Descripcion");
            Dt.Columns.Add("Id");
            Dt.Rows.Add(new object[] { "-- Seleccionar --", "-1" });
            Dt.Rows.Add(new object[] { "Adquisición de mercancias", "G01" });
            Dt.Rows.Add(new object[] { "Devoluciones, descuentos o bonificaciones", "G02" });
            Dt.Rows.Add(new object[] { "Gastos en general", "G03" });
            Dt.Rows.Add(new object[] { "Construcciones", "I01" });
            Dt.Rows.Add(new object[] { "Mobilario y equipo de oficina por inversiones", "I02" });
            Dt.Rows.Add(new object[] { "Equipo de transporte", "I03" });
            Dt.Rows.Add(new object[] { "Equipo de computo y accesorios", "I04" });
            Dt.Rows.Add(new object[] { "Dados, troqueles, moldes, matrices y herramental", "I05" });
            Dt.Rows.Add(new object[] { "Comunicaciones telefónicas", "I06" });
            Dt.Rows.Add(new object[] { "Comunicaciones satelitales", "I07" });
            Dt.Rows.Add(new object[] { "Otra maquinaria y equipo", "I08" });
            Dt.Rows.Add(new object[] { "Honorarios médicos, dentales y gastos hospitalarios.", "D01" });
            Dt.Rows.Add(new object[] { "Gastos médicos por incapacidad o discapacidad", "D02" });
            Dt.Rows.Add(new object[] { "Gastos funerales.", "D03" });
            Dt.Rows.Add(new object[] { "Donativos.", "D04" });
            Dt.Rows.Add(new object[] { "Intereses reales efectivamente pagados por créditos hipotecarios (casa habitación).", "D05" });
            Dt.Rows.Add(new object[] { "Aportaciones voluntarias al SAR.", "D06" });
            Dt.Rows.Add(new object[] { "Primas por seguros de gastos médicos.", "D07" });
            Dt.Rows.Add(new object[] { "Gastos de transportación escolar obligatoria.", "D08" });
            Dt.Rows.Add(new object[] { "Depósitos en cuentas para el ahorro, primas que tengan como base planes de pensiones.", "D09" });
            Dt.Rows.Add(new object[] { "Pagos por servicios educativos (colegiaturas)", "D10" });
            Dt.Rows.Add(new object[] { "Por definir", "P01" });


            cmbPUsoCFDI.DataSource = Dt;
            cmbPUsoCFDI.DataValueField = "Id";
            cmbPUsoCFDI.DataTextField = "Descripcion";
            cmbPUsoCFDI.DataBind();
            cmbPUsoCFDI.Enabled = false;
        }

        private void CargarCuentasCorporativas()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Emp_CnxCen, "spCatCuentaCorp_Combo", ref cmbCorporativa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rgDetalles_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if ((e.Item is GridDataInsertItem) && (e.Item.IsInEditMode))
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                GridDataInsertItem insertItem = (GridDataInsertItem)e.Item;
                RadComboBox rcbGarantiasEdit = (RadComboBox)insertItem.FindControl("rcbGarantiasEdit");

                //CapaNegocios.CN_CatCliente cncliente = new CN_CatCliente();
                //List<ModalidadOperacion> modOpList = new List<ModalidadOperacion>();
                //cncliente.ConsultaModalidadOP(Sesion.Emp_Cnx, ref modOpList);

                CapaNegocios.CN_TipoGarantia cnTg = new CN_TipoGarantia(Sesion);
                rcbGarantiasEdit.DataSource = cnTg.ObtenerTodas();
                rcbGarantiasEdit.DataTextField = "TG_Nombre";
                rcbGarantiasEdit.DataValueField = "Id_TG";

                rcbGarantiasEdit.DataBind();

                rcbGarantiasEdit.SelectedIndex = -1;

                rcbGarantiasEdit.Attributes.Add("selectorgarantias", rcbGarantiasEdit.ClientID);

                foreach (var item in rcbGarantiasEdit.Items.AsEnumerable())
                {
                    CheckBox chk = item.FindControl("chkItem") as CheckBox;
                    HiddenField hf = item.FindControl("chkItemValue") as HiddenField;
                    RadComboBox rcb = rcbGarantiasEdit;
                    //chk.InputAttributes.Add("onclick", "rcbGarantiasEdit_itemClick(this, '" + rcb.ClientID + "', '" + hf.ClientID + "', '" + chk.Text + "')");
                }

            }

            if ((e.Item is GridDataItem) && (e.Item.IsInEditMode))
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                GridDataItem insertItem = (GridDataItem)e.Item;
                //RadComboBox comboBox = (RadComboBox)insertItem.FindControl("cboModalidadOP"); 

                //CapaNegocios.CN_CatCliente cncliente = new CN_CatCliente();
                //List<ModalidadOperacion> modOpList = new List<ModalidadOperacion>();
                //cncliente.ConsultaModalidadOP(Sesion.Emp_Cnx, ref modOpList);

                //comboBox.DataSource = modOpList;
                //comboBox.DataTextField = "nombre";
                //comboBox.DataValueField = "id";

                //comboBox.DataBind();

                RadComboBox rcbGarantiasEdit = (RadComboBox)insertItem.FindControl("rcbGarantiasEdit");

                //CapaNegocios.CN_CatCliente cncliente = new CN_CatCliente();
                //List<ModalidadOperacion> modOpList = new List<ModalidadOperacion>();
                //cncliente.ConsultaModalidadOP(Sesion.Emp_Cnx, ref modOpList);

                CapaNegocios.CN_TipoGarantia cnTg = new CN_TipoGarantia(Sesion);
                rcbGarantiasEdit.DataSource = cnTg.ObtenerTodas();
                rcbGarantiasEdit.DataTextField = "TG_Nombre";
                rcbGarantiasEdit.DataValueField = "Id_TG";

                rcbGarantiasEdit.DataBind();

                rcbGarantiasEdit.SelectedIndex = -1;

                rcbGarantiasEdit.Attributes.Add("selectorgarantias", rcbGarantiasEdit.ClientID);

                foreach (var item in rcbGarantiasEdit.Items.AsEnumerable())
                {
                    CheckBox chk = item.FindControl("chkItem") as CheckBox;
                    HiddenField hf = item.FindControl("chkItemValue") as HiddenField;
                    RadComboBox rcb = rcbGarantiasEdit;
                    //chk.InputAttributes.Add("onclick", "rcbGarantiasEdit_itemClick(this, '" + rcb.ClientID + "', '" + hf.ClientID + "', '" + chk.Text + "')");
                }

                DataRowView data = e.Item.DataItem as DataRowView;
                if (data != null)
                {
                    AsignarSeleccionDeGarantiasAlEditar(rcbGarantiasEdit, data["Id_CteDet"].ToString());

                    HtmlInputCheckBox chkTradicional = e.Item.FindControl("chkTradicionalEdicion") as HtmlInputCheckBox;
                    HtmlInputCheckBox chkGarantia = e.Item.FindControl("chkGarantiaEdicion") as HtmlInputCheckBox;
                    SIANWEB.DataSets.CatCliente.CatClienteDS.CatClienteDetRow registroDetalle = DetalleDeClienteTerritorio(data["Id_CteDet"].ToString());

                    if (registroDetalle != null)
                    {
                        if (chkTradicional != null)
                        {
                            if (registroDetalle["Tradicional"] != null)
                            {
                                if (registroDetalle["Tradicional"].ToString().CompareTo("1") == 0 || registroDetalle["Tradicional"].ToString().CompareTo("True") == 0)
                                {
                                    chkTradicional.Checked = true;
                                }
                            }
                        }

                        if (chkGarantia != null)
                        {
                            if (registroDetalle["Garantia"] != null)
                            {
                                if (registroDetalle["Garantia"].ToString().CompareTo("1") == 0 || registroDetalle["Garantia"].ToString().CompareTo("True") == 0)
                                {
                                    chkGarantia.Checked = true;
                                }
                            }
                        }
                    }

                    if (!chkTradicional.Checked && !chkGarantia.Checked)
                    {
                        chkTradicional.Checked = true;
                    }
                }
            }

            if (e.Item is GridDataItem && !e.Item.IsInEditMode)
            {
                Label etiquetaTiposGarantias = e.Item.FindControl("lblTipoGarantias") as Label;
                DataRowView data = e.Item.DataItem as DataRowView;

                //HtmlInputCheckBox chkTradicional = e.Item.FindControl("chkTradicional") as HtmlInputCheckBox;
                bool chkTradicional = false;
                HtmlControl iTradicional = e.Item.FindControl("iTradicional") as HtmlControl;
                //HtmlInputCheckBox chkGarantia = e.Item.FindControl("chkGarantia") as HtmlInputCheckBox;
                bool chkGarantia = false;
                HtmlControl iGarantia = e.Item.FindControl("iGarantia") as HtmlControl;

                if (data["Cte_Tradicional"] != null)
                {
                    if (typeof(bool) == data["Cte_Tradicional"].GetType())
                    {
                        chkTradicional = (bool)data["Cte_Tradicional"];
                    }
                    else if (typeof(string) == data["Cte_Tradicional"].GetType())
                    {
                        int tradicional = int.Parse(data["Cte_Tradicional"].ToString());
                        chkTradicional = tradicional > 0;
                    }
                }

                if (data["Cte_Garantia"] != null)
                {

                    if (typeof(bool) == data["Cte_Garantia"].GetType())
                    {
                        chkGarantia = (bool)data["Cte_Garantia"];
                    }
                    else if (typeof(string) == data["Cte_Garantia"].GetType())
                    {
                        int garantia = int.Parse(data["Cte_Garantia"].ToString());
                        chkGarantia = garantia > 0;
                    }

                    if (chkGarantia)
                    {
                        CN_CatClienteDetGarantia cnGarantias = new CN_CatClienteDetGarantia(_Sesion);
                        CN_TipoGarantia cnTg = new CN_TipoGarantia(_Sesion);
                        var tipoGarantias = cnTg.ObtenerTodas();
                        var listaGarantias = DataSet.ObtenerGarantiasDeTerritorio(data["Id_CteDet"].ToString()); //cnGarantias.ObtenerTodos((int?)data["Id_Cte"], int.Parse(data["Id_CteDet"].ToString()));
                        var nombres = (from g in listaGarantias
                                       join tg in tipoGarantias
                                       on g equals tg.Id_TG.ToString()
                                       select tg.TG_Nombre).ToList();
                        etiquetaTiposGarantias.Text = string.Join(",", nombres);
                        iGarantia.Attributes.Add("class", "fa fa-check");
                    }
                    else
                    {
                        etiquetaTiposGarantias.Text = "";
                    }
                }

                if (!chkTradicional && !chkGarantia)
                {
                    chkTradicional = true;
                }

                if (chkTradicional)
                {
                    iTradicional.Attributes.Add("class", "fa fa-check");
                }
            }

        }

        protected void cboModalidadOP_DataBinding(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void CargarClientes()//Local
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_Cd_Ver, (int?)null, Sesion.Id_Rik == -1 ? (int?)null : (int?)Sesion.Id_Rik, Sesion.Emp_Cnx, "spCatCliente_Combo", ref cmbCliente);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarBanco() //Central
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                RadComboBox ComboBox = new RadComboBox();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatBanco_Combo", ref cmbBancos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarTipoCliente() //Central
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                RadComboBox ComboBox = new RadComboBox();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatTCliente_Combo", ref cmbTipoCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarTmoneda() //Central
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatTmoneda_Combo", ref cmbMoneda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarConsecutivos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, 1, Sesion.Emp_Cnx, "spCatConsFactEle_Combo", ref cmbSerie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAdenda()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatAdenda_Combo", ref cmbAdenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarNca()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, 2, Sesion.Emp_Cnx, "spCatConsFactEle_Combo", ref cmbNCargo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarNcre()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, 3, Sesion.Emp_Cnx, "spCatConsFactEle_Combo", ref cmbNCredito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAsignar()
        {
            cmbAsignacion.Items.Clear();
            DataTable Dt = new DataTable();
            Dt.Columns.Add("Descripcion");
            Dt.Columns.Add("Id");
            Dt.Rows.Add(new object[] { "-- Seleccionar --", "-1" });
            Dt.Rows.Add(new object[] { "Dependiendo de existencia", "0" });
            Dt.Rows.Add(new object[] { "Sólo partidas completas", "1" });

            cmbAsignacion.DataSource = Dt;
            cmbAsignacion.DataValueField = "Id";
            cmbAsignacion.DataTextField = "Descripcion";
            cmbAsignacion.DataBind();
        }


        private void CargarCuentaNacional(int IdCuenta = 0)
        {
            /*WS_CuentaNacional.Service1 ws = new WS_CuentaNacional.Service1();
            ws.Url = ConfigurationManager.AppSettings["WS_CuentaNacional"].ToString();
            String respuesta = ws.RegresaCN(IdCuenta == 0? "": IdCuenta.ToString());
            XmlDocument Xml = new XmlDocument();
            Xml.LoadXml(respuesta);*/
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if (IdCuenta == 0)
            {
                cmbRemisionElect.Items.Clear();
            }
            //DataTable Dt = new DataTable();
            //Dt.Columns.Add("Descripcion");
            //Dt.Columns.Add("Id");
            //Dt.Rows.Add(new object[] { "-- Seleccionar --", "0" });
            if (IdCuenta == 0)
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Emp_Cnx, "Sp_CatCuentaContableNacional_Combo", ref cmbRemisionElect);
            }


            int CuentaContableNacional = 0;
            /*  XmlNodeList ListaCuentaNacionales;
              ListaCuentaNacionales = Xml.GetElementsByTagName("CuentaNacional");
              foreach (XmlElement nodo in ListaCuentaNacionales)
              {
                  int i = 0;
                  string nCliente = nodo.Attributes["CliNum"].Value.ToString();
                  string nClienteNombre = nodo.Attributes["CliNom"].Value.ToString();                
                  Dt.Rows.Add(new object[] { nClienteNombre, nCliente });
                  if (IdCuenta > 0)
                  {
                      CuentaContableNacional = Int32.Parse(nodo.Attributes["CtaCont"].Value);
                  }
              }
              */

            //Dt.Rows.Add(new object[] { "FARMACIAS DEL AHORRO", "4" });
            //Dt.Rows.Add(new object[] { "DELPHI", "6" });
            //Dt.Rows.Add(new object[] { "HOME DEPOT", "7" });
            //Dt.Rows.Add(new object[] { "PRAXAIR", "8" });
            //Dt.Rows.Add(new object[] { "GRISI", "9" });
            //Dt.Rows.Add(new object[] { "VIPS", "11" });


            if (IdCuenta > 0)
            {
                switch (IdCuenta)
                {
                    case 4:
                        CuentaContableNacional = 6500;
                        break;
                    case 6:
                        CuentaContableNacional = 6501;
                        break;
                    case 7:
                        CuentaContableNacional = 6502;
                        break;
                    case 8:
                        CuentaContableNacional = 6503;
                        break;
                    case 9:
                        CuentaContableNacional = 6504;
                        break;
                    case 11:
                        CuentaContableNacional = 6505;
                        break;
                }


            }

            HiddenCteNumCuentaContNal.Value = CuentaContableNacional == 0 ? "" : CuentaContableNacional.ToString();

            //if (IdCuenta == 0)
            //{
            //    cmbRemisionElect.DataSource = Dt;
            //    cmbRemisionElect.DataValueField = "Id";
            //    cmbRemisionElect.DataTextField = "Descripcion";
            //    cmbRemisionElect.DataBind();
            //}

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
                    CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarProductos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion != null)
                {
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatProducto_Combo", ref cmbProducto);
                }
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
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CatCliente", "Id_Cte", Sesion.Emp_Cnx, "spCatLocal_Maximo");
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
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
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
                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetListDireccionesEntrega()
        {
            try
            {
                dtDireccionEntrega = new DataTable();
                dtDireccionEntrega.Columns.Add("Id_Cte", System.Type.GetType("System.Int32"));
                dtDireccionEntrega.Columns.Add("Id_CteDirEntrega", System.Type.GetType("System.Int32"));
                dtDireccionEntrega.Columns.Add("Cte_Calle", System.Type.GetType("System.String"));
                dtDireccionEntrega.Columns.Add("Cte_Numero", System.Type.GetType("System.String"));
                dtDireccionEntrega.Columns.Add("Cte_Cp", System.Type.GetType("System.String"));
                dtDireccionEntrega.Columns.Add("Cte_Colonia", System.Type.GetType("System.String"));
                dtDireccionEntrega.Columns.Add("Cte_Municipio", System.Type.GetType("System.String"));
                dtDireccionEntrega.Columns.Add("Cte_Estado", System.Type.GetType("System.String"));
                dtDireccionEntrega.Columns.Add("Cte_Sector", System.Type.GetType("System.String"));
                dtDireccionEntrega.Columns.Add("Cte_Telefono", System.Type.GetType("System.String"));
                dtDireccionEntrega.Columns.Add("Cte_Fax", System.Type.GetType("System.String"));
                dtDireccionEntrega.Columns.Add("Cte_HoraAm1", System.Type.GetType("System.String"));
                dtDireccionEntrega.Columns.Add("Cte_HoraAm2", System.Type.GetType("System.String"));
                dtDireccionEntrega.Columns.Add("Cte_HoraPm1", System.Type.GetType("System.String"));
                dtDireccionEntrega.Columns.Add("Cte_HoraPm2", System.Type.GetType("System.String"));
                //dtDireccionEntrega.Columns.Add("Editar", System.Type.GetType("System.Boolean"));
                if (HF_ID.Value != "")
                {
                    CN_CatCliente clsCatCliente = new CN_CatCliente();
                    Sesion session2 = new Sesion();
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    ClienteDirEntrega clienteDirEntrega = new ClienteDirEntrega();
                    clienteDirEntrega.Id_Emp = session2.Id_Emp;
                    clienteDirEntrega.Id_Cd = session2.Id_Cd_Ver;
                    clienteDirEntrega.Id_Cte = Convert.ToInt32(HF_ID.Value);
                    DataTable dt2 = dtDireccionEntrega;
                    clsCatCliente.ConsultaClienteDirEntrega(clienteDirEntrega, session2.Emp_Cnx, ref dt2);
                    dtDireccionEntrega = dt2;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetListDetPrecio()
        {
            try
            {
                dtPrecio = new DataTable();
                DataColumn dc = new DataColumn();
                dt.Columns.Add("Id_ClpDet", System.Type.GetType("System.Int32"));
                dt.Columns.Add("TPrecio", System.Type.GetType("System.Int32"));
                dt.Columns.Add("TPrecioStr", System.Type.GetType("System.String"));
                dt.Columns.Add("Precio", System.Type.GetType("System.String"));
                dt.Columns.Add("Pesos", System.Type.GetType("System.Double"));

                txtClave.Text = "";
                txtDescripcion.Text = "";
                //chkActivo.Checked = true;

                if (txtClave.Text != "" && txtProductoID.Text != "")
                {
                    if (Convert.ToInt32(txtClave.Text) > 0 && Convert.ToInt32(txtProductoID.Text) > 0)
                    {
                        CN_CatClienteProd clsCatCliente = new CN_CatClienteProd();
                        Sesion session2 = new Sesion();
                        session2 = (Sesion)Session["Sesion" + Session.SessionID];
                        ClienteProd ClienteProddet = new ClienteProd();
                        ClienteProddet.Id_Emp = session2.Id_Emp;
                        ClienteProddet.Id_Cd = session2.Id_Cd_Ver;
                        ClienteProddet.Id_Cte = Convert.ToInt32(txtClave.Text);
                        ClienteProddet.Id_Prd = Convert.ToInt32(txtProductoID.Text);
                        DataTable dt2 = dtPrecio;
                        clsCatCliente.ConsultaClienteProdDet(ClienteProddet, session2.Emp_Cnx, ref dt2);
                        dtPrecio = dt2;
                        txtClaveX.Text = ClienteProddet.Id_Clp;// == 0 ? (int?)null : ClienteProddet.Id_Clp;
                        txtDescripcionX.Text = ClienteProddet.Clp_descripcion == null ? "" : ClienteProddet.Clp_descripcion;
                        //chkActivo.Checked = true;//string.IsNullOrEmpty(ClienteProddet.Id_Clp) ? true : ClienteProddet.Estatus;
                        if (!string.IsNullOrEmpty(txtClave.Text))
                            HF_ID.Value = txtClave.Text;

                        txtUnidades.Text = ClienteProddet.Unidades;
                        txtPresentacion.Text = ClienteProddet.Clp_Presentacion;
                        //txtCantFact.Value = 0;//ClienteProddet.CantFact;
                        //dpUltimaVta.DbSelectedDate = null;//ClienteProddet.Clp_FecUltVta;
                    }
                }
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
                DataSet = new DataSets.CatCliente.CatClienteDS();
                dt = new DataTable();
                dt.Columns.Add("Id_CteDet", System.Type.GetType("System.Int32"));

                dt.Columns.Add("Ter_Tipo", System.Type.GetType("System.Int32"));
                dt.Columns.Add("DescTer_Tipo", System.Type.GetType("System.String"));

                dt.Columns.Add("Id_Ter", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Ter_Nombre", System.Type.GetType("System.String"));

                dt.Columns.Add("Id_Seg", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Seg_Descripcion", System.Type.GetType("System.String"));
                dt.Columns.Add("Cte_UnidadDim", System.Type.GetType("System.String"));
                dt.Columns.Add("Cte_Dimension", System.Type.GetType("System.String"));
                dt.Columns.Add("Cte_Pesos", System.Type.GetType("System.Double"));
                dt.Columns.Add("Cte_Potencial", System.Type.GetType("System.Double"));
                dt.Columns.Add("Cte_Activo", System.Type.GetType("System.Boolean"));
                dt.Columns.Add("Id_Rik", System.Type.GetType("System.String"));
                dt.Columns.Add("rik", System.Type.GetType("System.String"));

                //SAUL GUERRA 20150506 BEGIN
                dt.Columns.Add("Id_TerServ", System.Type.GetType("System.Int32"));
                dt.Columns.Add("TerServ", System.Type.GetType("System.String"));
                dt.Columns.Add("Id_RIKServ", System.Type.GetType("System.Int32"));
                dt.Columns.Add("RIKServ", System.Type.GetType("System.String"));
                //SAUL GUERRA 20150506 END

                dt.Columns.Add("uen", System.Type.GetType("System.String"));

                dt.Columns.Add("Cte_ManoObra", System.Type.GetType("System.Double"));
                dt.Columns.Add("Cte_GastoTerritorio", System.Type.GetType("System.Double"));
                dt.Columns.Add("Cte_FletePaga", System.Type.GetType("System.Double"));
                dt.Columns.Add("Cte_PorcComision", System.Type.GetType("System.Double"));
                dt.Columns.Add("Id_Uen", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Editar", System.Type.GetType("System.Boolean"));
                // EDSG 02092015
                //dt.Columns.Add("ModalidadOP", System.Type.GetType("System.Int32"));
                //dt.Columns.Add("Meta", System.Type.GetType("System.Double"));
                //dt.Columns.Add("ModalidadOP_Desc", System.Type.GetType("System.String"));

                dt.Columns.Add("Cte_Tradicional", System.Type.GetType("System.Boolean"));
                dt.Columns.Add("Cte_Garantia", System.Type.GetType("System.Boolean"));
                dt.Columns.Add("Id_Cte", System.Type.GetType("System.Int32"));
                //RBM
                //Se agregan columnas para manejo de fechas de las solicitudes de cambios en cliente-territorio
                //Inicio
                dt.Columns.Add("Fec_Solicitud", System.Type.GetType("System.DateTime"));
                dt.Columns.Add("Fec_Autorizado", System.Type.GetType("System.DateTime"));
                dt.Columns.Add("Fec_Rechazado", System.Type.GetType("System.DateTime"));
                //Fin
                if (HF_ID.Value != "")
                {
                    CN_CatCliente clsCatCliente = new CN_CatCliente();
                    Sesion session2 = new Sesion();
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    ClienteDet clientedet = new ClienteDet();
                    clientedet.Id_Emp = session2.Id_Emp;
                    clientedet.Id_Cd = session2.Id_Cd_Ver;
                    clientedet.Id_Cte = Convert.ToInt32(HF_ID.Value);
                    DataTable dt2 = dt;
                    clsCatCliente.ConsultaClienteDet(clientedet, session2.Emp_Cnx, ref dt2);
                    dt = dt2;
                    CargarDataSet(dt, _Sesion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //throw new Exception("test");
            }
        }

        protected void CargarDataSet(DataTable detalles, Sesion sesion)
        {
            CN_CatClienteDetGarantia cnGarantias = new CN_CatClienteDetGarantia(sesion);

            foreach (var dr in detalles.AsEnumerable())
            {
                var r = DataSet.CatClienteDet.AddCatClienteDetRow(dr["Id_CteDet"].ToString(), dr["Cte_Tradicional"].ToString(), dr["Cte_Garantia"].ToString());

                //var garantias = cnGarantias.ObtenerTodos(Convert.ToInt32(HF_ID.Value), (int)dr["Id_CteDet"]);
                //foreach (var g in garantias)
                //{
                //    DataSet.CatClienteDetGarantia.AddCatClienteDetGarantiaRow(g.Id_Emp.ToString(), g.Id_Cd.ToString(), g.Id_Cte.ToString(), r, g.Id_TG.ToString(), g.Id_CteDetGtia.ToString());
                //}

            }
        }

        private void habilitarcomboSegmentos()
        {



        }
        private void GetTablaPermisosUEN()
        {
            try
            {
                TablaPermisosUEN = new DataTable();
                TablaPermisosUEN.Columns.Add("Id_UenPermiso", System.Type.GetType("System.Int32"));
                TablaPermisosUEN.Columns.Add("Id_UenPotencial", System.Type.GetType("System.Int32"));
                TablaPermisosUEN.Columns.Add("Id_UenDimension", System.Type.GetType("System.Int32"));

                CN_CatCliente clsCatCliente = new CN_CatCliente();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                DataTable dt2 = TablaPermisosUEN;
                clsCatCliente.ConsultaPermisosUEN(ref dt2, session2.Emp_Cnx);
                TablaPermisosUEN = dt2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Nuevo()
        {
            cmbCliente.SelectedIndex = -1;
            cmbCliente.Text = "";
            HF_ID.Value = string.Empty;
            txtClave.Text = Valor;
            txtClave.Enabled = true;
            numCorporativo.Text = string.Empty;
            txtNombreCorto.Text = string.Empty;
            txtCobranza.Text = string.Empty;
            txtDcontacto.Text = string.Empty;
            txtDcalle.Text = string.Empty;
            txtDciudad.Text = string.Empty;
            txtDcolonia.Text = string.Empty;
            txtDcp.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtDestado.Text = string.Empty;
            txtDias1.Text = string.Empty;
            txtDmunicipio.Text = string.Empty;
            txtDnumero.Text = string.Empty;
            txtDrfc.Text = string.Empty;
            txtDtelefono.Text = string.Empty;
            txtDfax.Text = string.Empty;
            txtFcalle.Text = string.Empty;
            txtFciudad.Text = string.Empty;
            txtFcolonia.Text = string.Empty;
            txtFcp.Text = string.Empty;
            txtFestado.Text = string.Empty;
            txtFmunicipio.Text = string.Empty;
            txtFnumero.Text = string.Empty;
            txtFrfc.Text = string.Empty;
            txtFtelefono.Text = string.Empty;
            txtmail.Text = string.Empty;
            txtDocumentos.Text = string.Empty;

            txtReferencia.Text = string.Empty;
            txtUDigitos.Text = string.Empty;
            cmbRemisionElect.SelectedIndex = 0;
            cmbRemisionElect.Text = cmbAdenda.Items[0].Text;

            cmbAdenda.SelectedIndex = 0;
            cmbAdenda.Text = cmbAdenda.Items[0].Text;
            cmbAsignacion.SelectedIndex = 0;
            cmbAsignacion.Text = cmbAsignacion.Items[0].Text;
            cmbCorporativa.SelectedIndex = 0;
            cmbCorporativa.Text = cmbCorporativa.Items[0].Text;

            cmbCliente.SelectedIndex = 0;
            if (cmbCliente.FindItemByValue("-1") != null)
                cmbCliente.Text = cmbCliente.FindItemByValue("-1").Text;
            cmbMoneda.SelectedIndex = 0;
            cmbMoneda.Text = cmbMoneda.Items[0].Text;
            cmbSerie.SelectedIndex = 0;
            cmbSerie.Text = cmbSerie.Items[0].Text;
            cmbNCredito.SelectedIndex = 0;
            cmbNCredito.Text = cmbNCredito.Items[0].Text;
            cmbNCargo.SelectedIndex = 0;
            cmbNCargo.Text = cmbNCargo.Items[0].Text;

            cmbTipoCliente.SelectedIndex = 0;
            cmbTipoCliente.Text = cmbTipoCliente.Items[0].Text;
            cmbBancos.SelectedIndex = 0;
            cmbBancos.Text = cmbBancos.Items[0].Text;

            rdActivo.Checked = true;
            chkComisiones.Checked = false;
            chkCredito.Checked = false;
            chkDesglose.Checked = false;
            chkFacturar.Checked = false;
            chkPDomingo.Checked = false;
            chkPJueves.Checked = false;
            chkPLunes.Checked = false;
            chkPMartes.Checked = false;
            chkPMiercoles.Checked = false;
            chkPSabado.Checked = false;
            chkPViernes.Checked = false;
            chkRDomingo.Checked = false;
            chkRetencion.Checked = false;
            ChkPorcientoIVA.Checked = false;
            chkRJueves.Checked = false;
            chkRLunes.Checked = false;
            chkRMartes.Checked = false;
            chkRMiercoles.Checked = false;
            chkRSabado.Checked = false;
            chkRViernes.Checked = false;
            chkEfectivo.Checked = false;
            chkFactoraje.Checked = false;
            chkTransferencia.Checked = false;
            chkCheque.Checked = false;
            chkOrdenCompra.Checked = false;
            chkCredSuspendido.Checked = false;

            txtRHoraam1.SelectedDate = null;
            txtRHoraam2.SelectedDate = null;
            txtRHorapm1.SelectedDate = null;
            txtRHorapm2.SelectedDate = null;

            txtPHoraam1.SelectedDate = null;
            txtPHoraam2.SelectedDate = null;
            txtPHorapm1.SelectedDate = null;
            txtPHorapm2.SelectedDate = null;

            txtEHoraam1.SelectedDate = null;
            txtEHoraam2.SelectedDate = null;
            txtEHorapm1.SelectedDate = null;
            txtEHorapm2.SelectedDate = null;

            chkNotaCredFac.Checked = false;
            txtPorcFacturar.Text = string.Empty;

            txtPorcRetencion.Text = string.Empty;
            txtPorcientoIVA.Text = string.Empty;

            txtCorreo1.Text = "";
            txtCorreo2.Text = "";
            txtCorreo3.Text = "";

            GetListDet();
            GetListDireccionesEntrega();
            rgDetalles.Rebind();
            rgDireccionesEntrega.Rebind();

            this.RadTabStripPrincipal.SelectedIndex = 0;
            RadMultiPagePrincipal.SelectedIndex = 0;

            txtEcalle.Text = string.Empty;
            txtEnumero.Text = string.Empty;
            txtEcp.Text = string.Empty;
            chkClonarDireccionFiscal.Checked = false;
            txtEcolonia.Text = string.Empty;
            txtEmunicipio.Text = string.Empty;
            txtEestado.Text = string.Empty;
            txtESector.Text = string.Empty;
            txtEtelefono.Text = string.Empty;
            txtEfax.Text = string.Empty;

            txtClientePortal.Text = string.Empty;
            txtReferenciaTecleada.Text = string.Empty;
            txtNumeroCuenta.Text = string.Empty;
            this.Mod.Visible = false;

            foreach (RadListBoxItem rbli in ListFPago.Items)
            {
                rbli.Checked = false;

            }



            //para pagos
            cmbPUsoCFDI.SelectedIndex = 22;
            cmbPUsoCFDI.Text = cmbPUsoCFDI.Items[22].Text;


            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            Session["Sesion" + Session.SessionID + "cambioTipoCliente"] = null;

            CN_CatUsuario cn_catusuario = new CN_CatUsuario();
            Usuario usu = new Usuario();
            usu.Id_Emp = session.Id_Emp;
            usu.Id_Cd = session.Id_Cd;
            usu.Id_U = session.Id_U;
            cn_catusuario.ConsultaUsuarios(ref usu, session.Emp_Cnx);

            int dias = 0;
            chkCredSuspendido.Enabled = usu.U_SusCredito && usu.U_DiasVencimiento >= dias;
        }
        private void Guardar(int? id_U, int? id_Cd)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];


                if (!chkRLunes.Checked && !chkRMartes.Checked && !chkRMiercoles.Checked && !chkRJueves.Checked && !chkRViernes.Checked && !chkRSabado.Checked && !chkRDomingo.Checked)
                {
                    Alerta("No se ha seleccionado ningún día de revisión");
                    RadTabStripPrincipal.Tabs[2].Selected = true;
                    RadPageViewParametros.Selected = true;
                    return;
                }
                if (!chkPLunes.Checked && !chkPMartes.Checked && !chkPMiercoles.Checked && !chkPJueves.Checked && !chkPViernes.Checked && !chkPSabado.Checked && !chkPDomingo.Checked)
                {
                    Alerta("No se ha seleccionado ningún día de pago");
                    RadTabStripPrincipal.Tabs[2].Selected = true;
                    RadPageViewParametros.Selected = true;
                    return;
                }
                if (!chkRecLunes.Checked && !chkRecMartes.Checked && !chkRecMiercoles.Checked && !chkRecJueves.Checked && !chkRecViernes.Checked && !chkRecSabado.Checked && !chkRecDomingo.Checked)
                {

                    Alerta("No se ha seleccionado ningún día de recepción");
                    RadTabStripPrincipal.Tabs[2].Selected = true;
                    RadPageViewParametros.Selected = true;

                    return;
                }
                if (ChkPorcientoIVA.Checked == true)
                {
                    if (txtPorcientoIVA.Value == 0 || txtPorcientoIVA.Value == null)
                    {
                        Alerta("El IVA del Cliente no puede ser CERO");
                        return;
                    }
                }
                if (!txtDias1.Value.HasValue)
                {
                    Alerta("No se ha capturado la condición de pago");
                    RadTabStripPrincipal.Tabs[2].Selected = true;
                    RadPageViewParametros.Selected = true;
                    return;
                }

                Clientes clientes = new Clientes();
                clientes.Id_Emp = session.Id_Emp;
                clientes.Id_Cd = session.Id_Cd_Ver;
                clientes.Id_Mon = Convert.ToInt32(cmbMoneda.SelectedValue);
                clientes.Id_Cfe = Convert.ToInt32(cmbSerie.SelectedValue);
                clientes.Cte_NomComercial = txtDescripcion.Text;
                clientes.Id_Corp = Convert.ToInt32(cmbCorporativa.SelectedValue);
                clientes.Cte_NomCorto = txtNombreCorto.Text;
                clientes.Cte_FacCalle = txtFcalle.Text;
                clientes.Cte_FacNumero = txtFnumero.Text;
                clientes.Cte_FacCp = txtFcp.Text;
                clientes.Cte_FacColonia = txtFcolonia.Text;
                clientes.Cte_FacMunicipio = txtFmunicipio.Text;
                clientes.Cte_FacTel = txtFtelefono.Text;
                clientes.Cte_FacRfc = txtFrfc.Text;
                clientes.Cte_FacEstado = txtFestado.Text;
                clientes.Cte_Calle = txtDcalle.Text;
                clientes.Cte_Numero = txtDnumero.Text;
                clientes.Cte_Cp = txtDcp.Text;
                clientes.Cte_Colonia = txtDcolonia.Text;
                clientes.Cte_Municipio = txtDmunicipio.Text;
                clientes.Cte_DRfc = txtDrfc.Text;
                clientes.Cte_Estado = txtDestado.Text;
                clientes.Cte_Telefono = txtDtelefono.Text;
                clientes.Cte_Fax = txtDfax.Text;
                clientes.Cte_Contacto = txtDcontacto.Text;
                clientes.Cte_Email = txtmail.Text;
                clientes.Cte_Referencia = txtReferencia.Text;
                clientes.Cte_Credito = chkCredito.Checked;
                clientes.Cte_Facturacion = chkFacturar.Checked;
                clientes.Cte_LimCobr = txtCobranza.Text == string.Empty ? 0 : Convert.ToDouble(txtCobranza.Text);
                clientes.Cte_RHoraam1 = txtRHoraam1.SelectedDate == null ? string.Empty : txtRHoraam1.SelectedDate.Value.ToString("HH:mm");
                clientes.Cte_RHoraam2 = txtRHoraam2.SelectedDate == null ? string.Empty : txtRHoraam2.SelectedDate.Value.ToString("HH:mm");
                clientes.Cte_RHorapm1 = txtRHorapm1.SelectedDate == null ? string.Empty : txtRHorapm1.SelectedDate.Value.ToString("HH:mm");
                clientes.Cte_RHorapm2 = txtRHorapm2.SelectedDate == null ? string.Empty : txtRHorapm2.SelectedDate.Value.ToString("HH:mm");

                clientes.Cte_RLunes = chkRLunes.Checked;
                clientes.Cte_RMartes = chkRMartes.Checked;
                clientes.Cte_RMiercoles = chkRMiercoles.Checked;
                clientes.Cte_RJueves = chkRJueves.Checked;
                clientes.Cte_RViernes = chkRViernes.Checked;
                clientes.Cte_RSabado = chkRSabado.Checked;
                clientes.Cte_RDomingo = chkRDomingo.Checked;
                clientes.Cte_CondPago = txtDias1.Text == string.Empty ? 0 : Convert.ToInt32(txtDias1.Text);
                clientes.Cte_CPLunes = chkPLunes.Checked;
                clientes.Cte_CPMartes = chkPMartes.Checked;
                clientes.Cte_CPMiercoles = chkPMiercoles.Checked;
                clientes.Cte_CPJueves = chkPJueves.Checked;
                clientes.Cte_CPViernes = chkPViernes.Checked;
                clientes.Cte_CPSabado = chkPSabado.Checked;
                clientes.Cte_CPDomingo = chkPDomingo.Checked;
                clientes.Cte_Comisiones = chkComisiones.Checked;
                clientes.Cte_DesgIva = chkDesglose.Checked;
                clientes.Cte_RetIva = chkRetencion.Checked;
                clientes.BPorcientoIVA = ChkPorcientoIVA.Checked;
                clientes.Cte_SerieNCre = Convert.ToInt32(cmbNCredito.SelectedValue);
                clientes.Cte_SerieNCa = Convert.ToInt32(cmbNCargo.SelectedValue);
                clientes.Cte_AsignacionPed = Convert.ToInt32(cmbAsignacion.SelectedValue);
                clientes.Id_Ade = Convert.ToInt32(cmbAdenda.SelectedValue);
                clientes.Estatus = rdActivo.Checked;
                clientes.Cte_NumCuentaContNacional = string.IsNullOrEmpty(HiddenCteNumCuentaContNal.Value) ? 0 : Int32.Parse(HiddenCteNumCuentaContNal.Value);

                clientes.Cte_CreditoSuspendido = chkCredSuspendido.Checked;

                clientes.Cte_UsoCFDI = cmbUsoCFDI.SelectedValue;
                clientes.Cte_MetodoPago = cmbMetodoPago.SelectedValue;


                //Para Pagos
                clientes.Cte_PagoUsoCFDI = cmbPUsoCFDI.SelectedValue;
                clientes.Cte_PagoMetodoPago = cmbPFormaPago.SelectedValue;


                if (txtRHoraam1.SelectedDate > txtRHoraam2.SelectedDate)
                {
                    Alerta("La 1era. hora de revisión a.m. no debe ser mayor que la 2da. hora a.m");
                    return;
                }
                if (txtRHorapm1.SelectedDate > txtRHorapm2.SelectedDate)
                {
                    Alerta("La 1era. hora de revisión p.m. no debe ser mayor que la 2da. hora p.m.");
                    return;
                }
                if (txtPHoraam1.SelectedDate > txtPHoraam2.SelectedDate)
                {
                    Alerta("La 1era. hora de pago a.m. no debe ser mayor que la 2da. hora de pago a.m");
                    return;
                }
                if (txtPHorapm1.SelectedDate > txtPHorapm2.SelectedDate)
                {
                    Alerta("La 1era. hora de pago p.m. no debe ser mayor que la 2da. hora de pago p.m.");
                    return;
                }

                clientes.Cte_PHoraam1 = txtPHoraam1.SelectedDate == null ? string.Empty : txtPHoraam1.SelectedDate.Value.ToString("HH:mm");
                clientes.Cte_PHoraam2 = txtPHoraam2.SelectedDate == null ? string.Empty : txtPHoraam2.SelectedDate.Value.ToString("HH:mm");
                clientes.Cte_PHorapm1 = txtPHorapm1.SelectedDate == null ? string.Empty : txtPHorapm1.SelectedDate.Value.ToString("HH:mm");
                clientes.Cte_PHorapm2 = txtPHorapm2.SelectedDate == null ? string.Empty : txtPHorapm2.SelectedDate.Value.ToString("HH:mm");

                clientes.Cte_SemRec = txtSemana.Text == "" ? 0 : Convert.ToInt32(txtSemana.Text);
                clientes.Cte_SemRev = txtSemanaRevision.Text == "" ? 0 : Convert.ToInt32(txtSemanaRevision.Text);
                clientes.Cte_SemRev2 = txtSemanaRevision2.Text == "" ? 0 : Convert.ToInt32(txtSemanaRevision2.Text);
                clientes.Cte_SemCob = txtSemanaPago.Text == "" ? 0 : Convert.ToInt32(txtSemanaPago.Text);
                clientes.Cte_RecLunes = chkRecLunes.Checked;
                clientes.Cte_RecMartes = chkRecMartes.Checked;
                clientes.Cte_RecMiercoles = chkRecMiercoles.Checked;
                clientes.Cte_RecJueves = chkRecJueves.Checked;
                clientes.Cte_RecViernes = chkRecViernes.Checked;
                clientes.Cte_RecSabado = chkRecSabado.Checked;
                clientes.Cte_RecDomingo = chkRecDomingo.Checked;
                clientes.Cte_Efectivo = chkEfectivo.Checked;
                clientes.Cte_Factoraje = chkFactoraje.Checked;
                clientes.Cte_Cheque = chkCheque.Checked;
                clientes.Cte_Transferencia = chkTransferencia.Checked;
                clientes.Cte_ReqOrdenCompra = chkOrdenCompra.Checked;
                clientes.Cte_Documentos = txtDocumentos.Text.Replace("'", "");
                clientes.Cte_TelCobranza1 = txtTelCobranza1.Text;
                clientes.Cte_TelCobranza2 = txtTelCobranza2.Text;
                clientes.Cte_RemisionElectronica = Convert.ToInt32(cmbRemisionElect.SelectedValue);
                clientes.BPorcNotaCredito = chkNotaCredFac.Checked;
                clientes.PorcientoNotaCredito = txtPorcFacturar.Value.HasValue ? txtPorcFacturar.Value.Value : 0;
                clientes.PorcientoRetencion = txtPorcRetencion.Value.HasValue ? txtPorcRetencion.Value.Value : 0;
                clientes.PorcientoIVA = txtPorcientoIVA.Value.HasValue ? Convert.ToInt32(txtPorcientoIVA.Value.Value) : 0;
                clientes.Cte_UDigitos = txtUDigitos.Text;
                clientes.Id_U = session.Id_U;
                clientes.Id_UCd = session.Id_Cd;
                clientes.Db = (new SqlConnectionStringBuilder(session.Emp_Cnx)).InitialCatalog;
                clientes.Db_Cobranza = (new SqlConnectionStringBuilder(Emp_CnxCob)).InitialCatalog;

                clientes.Cte_AutorizaPlazo_IdCd = id_Cd;
                clientes.Cte_AutorizaPlazo_IdU = id_U;

                clientes.Cte_CorreoEdoCuenta1 = txtCorreo1.Text;
                clientes.Cte_CorreoEdoCuenta2 = txtCorreo2.Text;
                clientes.Cte_CorreoEdoCuenta3 = txtCorreo3.Text;

                clientes.Id_UMod = session.Id_U;


                clientes.Id_Ban = Convert.ToInt32(cmbBancos.SelectedValue);
                clientes.Id_TCte = Convert.ToInt32(cmbTipoCliente.SelectedValue);
                clientes.Cte_Portal = txtClientePortal.Text;
                clientes.Cte_ReferenciaTecleada = txtReferenciaTecleada.Text;
                clientes.Cte_NumeroCuenta = txtNumeroCuenta.Text;

                List<FormaPago> listFP = new List<FormaPago>();
                FormaPago ItemFP;
                foreach (RadListBoxItem dr in ListFPago.Items)
                {
                    if (dr.Checked)
                    {
                        ItemFP = new FormaPago();
                        ItemFP.Id_Fpa = Convert.ToInt32(dr.Value);
                        listFP.Add(ItemFP);
                    }
                }
                clientes.FormasPago = listFP;



                //Edsg28062017
                CentroDistribucion cd = new CentroDistribucion();
                cd.Id_Emp = session.Id_Emp;
                CN_CatCentroDistribucion clsCatCd = new CN_CatCentroDistribucion();
                var puedeVariasUEN = clsCatCd.ValidarVariasUEN(session.Id_Emp, session.Id_Cd, (int)txtClave.Value, session.Emp_Cnx);


                bool continuar = false;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Id_Uen"] != DBNull.Value)
                    {

                        //Edsg28062017
                        if (dt.Select("Id_Uen='" + dr["Id_Uen"] + "' and Cte_Activo=true and Id_Seg <> 12 ").Length > 1 && !puedeVariasUEN)
                        {
                            Alerta("No puede haber dos o mas territorios con el mismo UEN activos");
                            return;
                        }
                        if (Convert.ToBoolean(dr["Cte_Activo"]))
                        {
                            continuar = true;
                        }
                    }
                    else
                    {
                        continuar = true;
                    }
                }

                if (!continuar)
                {
                    Alerta("No tiene territorios activos");
                    return;
                }

                if (dtDireccionEntrega.Rows.Count == 0)
                {
                    Alerta("Debe capturar al menos una dirección de entrega");
                    RadTabStripPrincipal.Tabs[2].Selected = true;
                    return;
                }





                CN_CatCliente clsCatClientes = new CN_CatCliente();
                int verificador = -1;
                if (HF_ID.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    clientes.Id_Cte = Convert.ToInt32(txtClave.Text);
                    clsCatClientes.InsertarClientes(clientes, session.Emp_Cnx, ref verificador);
                    if (verificador == 1)
                    {
                        clsCatClientes.InsertarCteFormaPago(clientes, session.Emp_Cnx, ref verificador);
                        //clsCatClientes.InsertarClientesDet(clientes, dt, session.Emp_Cnx);
                        clsCatClientes.UpdateClientesDet(clientes, dt, session.Emp_Cnx);


                        clsCatClientes.InsertarClientesDirEntrega(clientes, dtDireccionEntrega, session.Emp_Cnx);
                        //clsCatClientes.InsertarClientesDet(clientes, dt, session.Emp_Cnx, DataSet.CatClienteDet, DataSet.CatClienteDetGarantia, session.Emp_Cnx_EF);
                        clsCatClientes.UpdateClientesDet(clientes, dt, session.Emp_Cnx, DataSet.CatClienteDet, DataSet.CatClienteDetGarantia, session.Emp_Cnx_EF);
                        Nuevo();
                        Alerta("Los datos se guardaron correctamente");
                    }
                    else
                        Alerta("La clave ya existe");
                    CargarClientes();
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    clientes.Id_Cte = Convert.ToInt32(HF_ID.Value);
                    clsCatClientes.ModificarClientes(clientes, session.Emp_Cnx, ref verificador);
                    if (verificador == 1)
                    {
                        //clsCatClientes.InsertarClientesDet(clientes, dt, session.Emp_Cnx, DataSet.CatClienteDet, DataSet.CatClienteDetGarantia, session.Emp_Cnx_EF);
                        clsCatClientes.UpdateClientesDet(clientes, dt, session.Emp_Cnx, DataSet.CatClienteDet, DataSet.CatClienteDetGarantia, session.Emp_Cnx_EF);
                        clsCatClientes.InsertarClientesDirEntrega(clientes, dtDireccionEntrega, session.Emp_Cnx);

                        //Edsg 19062017
                        if (!ModificaTerritorios)
                            Alerta("La solicitud de los cambios en los territorios se enviarón correctamente");
                        else
                        {
                            Session["ClienteBaseInstalada" + Session.SessionID] = clientes.Id_Cte;
                            RAM1.ResponseScripts.Add("alertaBaseInstalada();");
                        }






                    }
                    else
                        Alerta("Ocurrió un error al intentar modificar los datos");
                }
                #endregion

               //RBM
               //Se crean los xml y se consume el wsTerritorios
               //Inicio
                #region Convertir a XML

                CN_CatCliente CN = new CN_CatCliente();
                AutClienteTerritorio ClienteTerritorio = new AutClienteTerritorio();
                XmlSerializer serializar = new XmlSerializer(typeof(AutClienteTerritorio));
                
                //Se cargan los valores de la tabla Cliente-Territorio a Autorizar
                System.IO.StringWriter writer = new System.IO.StringWriter();
                objdtTablaTerritorios.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string xmlTerritorios = writer.ToString();

                //Se cargan los valores de la tabla Cliente-Territorio Original
                System.IO.StringWriter writerant = new System.IO.StringWriter();
                objdtTablaTerritoriosAnt.WriteXml(writerant, XmlWriteMode.WriteSchema, false);
                string xmlTerritoriosAnt = writerant.ToString();
                #endregion

                #region Llamar a webService
                DataTable dtrespuesta = new DataTable();

                wsTerritorio.Service1 ws = new wsTerritorio.Service1();
                dtrespuesta = ws.GuardaAutClienteTerritorio(xmlTerritorios, xmlTerritoriosAnt);

                #endregion

                EnviaEmailAutorizacionTerritorios(clientes, dtrespuesta);

                //Fin



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected Sesion _sesion = null;

        protected Sesion _Sesion
        {
            get
            {
                if (_sesion == null)
                {
                    _sesion = new Sesion();
                    _sesion = (Sesion)Session["Sesion" + Session.SessionID];
                }
                return _sesion;
            }
        }
        private void Update(GridCommandEventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int Id_CteDet = 0;
            int Id_Ter = 0;
            string Ter_Nombre = "";
            int Id_Seg = 0;
            int uen = 0;
            int Ter_Tipo = 0;
            string DescTer_Tipo = string.Empty;
            string Seg_Descripcion = "";
            string Cte_UnidadDim = "";
            string Cte_Dimension = "";
            string Rik = "";
            string Uen = "";

            // SAUL GUERRA 20150506 BEGIN
            int Id_TerrServ = -1;
            string TerrServ = "";
            int Id_RIKServ = -1;
            string RIKServ = "";
            // SAUL GUERRA 20150506 END

            double Cte_Pesos = 0;
            double Cte_CarMP = 0;
            double Cte_GastoVarT = 0;
            double Cte_FletePaga = 0;
            double Cte_Potencial = 0;
            double Cte_PorcComision = 0;
            bool Cte_Activo = false;
            GridItem gi = null;

            DataRow[] Ar_dr;
            gi = e.Item;
            if (((RadComboBox)gi.FindControl("RadComboBox1")).Text == "" ||
              ((RadComboBox)gi.FindControl("RadComboBox2")).Text == "" ||
              ((Label)gi.FindControl("RadTextBox3")).Text == "" ||
              ((RadNumericTextBox)gi.FindControl("RadTextBox4")).Text == "" ||
              ((RadNumericTextBox)gi.FindControl("RadNumericTextBox5")).Text == "")
            {
                e.Canceled = true;
                Alerta("Todos los campos son requeridos");
                return;
            }

            try
            {
                Id_Ter = Convert.ToInt32(((RadComboBox)gi.FindControl("RadComboBox1")).SelectedValue);
                Ter_Nombre = ((RadComboBox)gi.FindControl("RadComboBox1")).Text;
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw new Exception("Por favor, capture el Territorio");
            }
            try
            {
                Id_Seg = Convert.ToInt32(((RadComboBox)gi.FindControl("RadComboBox2")).SelectedValue);
                Seg_Descripcion = ((RadComboBox)gi.FindControl("RadComboBox2")).Text;
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw new Exception("Por favor, capture el Segmento");
            }
            try
            {
                Cte_UnidadDim = ((Label)gi.FindControl("RadTextBox3")).Text;
                Cte_Dimension = ((RadNumericTextBox)gi.FindControl("RadTextBox4")).Text;
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw new Exception("Por favor, capture la Unidad de Dimension");
            }
            try
            {
                Cte_Pesos = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox5")).Text);
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw new Exception("Por favor, capture la cantidad");
            }

            try
            {
                Cte_Potencial = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox6")).Text);
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw new Exception("Por favor, capture el potencial");
            }

            Id_CteDet = Convert.ToInt32(((Label)gi.FindControl("Label0")).Text);
            Cte_Activo = ((CheckBox)gi.FindControl("chk8")).Checked;
            Cte_CarMP = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox7")).Text);
            Cte_GastoVarT = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox8")).Text);
            Cte_FletePaga = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox9")).Text);
            Cte_PorcComision = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtComision")).Text);
            uen = !string.IsNullOrEmpty(((Label)gi.FindControl("lblIDUENEdit")).Text) ? Convert.ToInt32(((Label)gi.FindControl("lblIDUENEdit")).Text) : 0;

            // SAUL GUERRA 20150506 BEGIN
            Id_TerrServ = Convert.ToInt32(((RadComboBox)gi.FindControl("cbTerServ")).SelectedValue);
            TerrServ = ((RadComboBox)gi.FindControl("cbTerServ")).Text;
            Id_RIKServ = Convert.ToInt32(((Label)gi.FindControl("lblId_RIKServEdit")).Text);
            RIKServ = ((Label)gi.FindControl("lblRIKServEdit")).Text;
            // SAUL GUERRA 20150506 END

            if (!(Id_Ter > 0))
            {
                Alerta("debe seleccionar Tipo de territorio y Territorio");
                e.Canceled = true;
                return;
            }

            /**
             * ID: AGSO
             * Fecha: 2015/09/09, 1251
             * Requerimiento: Garantias, KQ-R1001-00Y
             **/
            //RadComboBox rcbModalidadOP = (RadComboBox)gi.FindControl("cboModalidadOP");
            //if (rcbModalidadOP.Items.Count < 1)
            //{
            //    e.Canceled = true;
            //    throw new Exception("Se debe elegir un valor para la Modalidad de Operación");
            //}
            /*Requerimiento: Garantias, KQ-R1001-00Y*/

            /**
             * ID: AGSO
             * Fecha: 2015/09/09, 1235
             * Requerimiento: Garantias, KQ-R1001-00X
             **/
            //if (ModalidadOP_Desc == "GARANTIA")
            //{
            //    try
            //    {
            //        ValidarSeleccionGarantiaEnModalidadOperacion(gi);
            //    }
            //    catch (Exception ex)
            //    {
            //        e.Canceled = true;
            //        throw ex;
            //    }

            //}
            /*Requerimiento: Garantias, KQ-R1001-00X*/
            if (Cte_Potencial == 0)
            {
                AlertaFocus("El potencial no debe ser cero", ((RadNumericTextBox)gi.FindControl("RadNumericTextBox6")).ClientID);
                e.Canceled = true;
                return;
            }

            CN_CatTerritorios cn_catter = new CN_CatTerritorios();
            Territorios terr = new Territorios();
            terr.Id_Emp = sesion.Id_Emp;
            terr.Id_Cd = sesion.Id_Cd_Ver;
            terr.Id_Ter = Id_Ter;
            cn_catter.ConsultaTerritorios(ref terr, sesion.Emp_Cnx);
            Rik = terr.Rik_Nombre;
            Uen = terr.Uen_Descripcion;
            uen = terr.Id_Uen;

            Ar_dr = dt.Select("Id_CteDet='" + Id_CteDet + "'");
            if (Ar_dr.Length > 0)
            {
                DataRow dr;
                if (dt.Rows.Count > 0)
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        int UEN = !string.IsNullOrEmpty(dr["Id_Uen"].ToString()) ? Convert.ToInt32(dr["Id_Uen"].ToString()) : 0;
                        int id_ctedet = !string.IsNullOrEmpty(dr["Id_CteDet"].ToString()) ? Convert.ToInt32(dr["Id_CteDet"].ToString()) : 0;
                        bool activo = Convert.ToBoolean(dr["Cte_Activo"]);
                        /* if (Id_CteDet != id_ctedet)
                             if (UEN == uen && Cte_Activo)
                             {
                                 e.Canceled = true;
                                 Alerta("La UEN ya está registrada con otro territorio");
                                 return;
                             }*/
                    }
                //Ar_dr[0].BeginEdit();

                Ar_dr[0]["Ter_Tipo"] = Ter_Tipo;
                Ar_dr[0]["DescTer_Tipo"] = DescTer_Tipo;
                Ar_dr[0]["Id_Ter"] = Id_Ter;
                Ar_dr[0]["Ter_Nombre"] = Ter_Nombre;
                Ar_dr[0]["Id_Seg"] = Id_Seg;
                Ar_dr[0]["Seg_Descripcion"] = Seg_Descripcion;
                Ar_dr[0]["Cte_UnidadDim"] = Cte_UnidadDim;
                Ar_dr[0]["Cte_Dimension"] = Convert.ToDouble(Cte_Dimension).ToString("#,##0.00");
                Ar_dr[0]["Cte_Pesos"] = Cte_Pesos;
                Ar_dr[0]["Cte_Potencial"] = Cte_Potencial;
                Ar_dr[0]["Cte_Activo"] = Cte_Activo;
                Ar_dr[0]["rik"] = Rik;
                Ar_dr[0]["uen"] = Uen;

                //SAUL GUERRA 20150506 BEGIN
                Ar_dr[0]["Id_TerServ"] = Id_TerrServ;
                Ar_dr[0]["TerServ"] = TerrServ;
                Ar_dr[0]["Id_RIKServ"] = Id_RIKServ;
                Ar_dr[0]["RIKServ"] = RIKServ;
                //SAUL GUERRA 20150506 END

                Ar_dr[0]["Cte_ManoObra"] = Cte_CarMP;
                Ar_dr[0]["Cte_GastoTerritorio"] = Cte_GastoVarT;
                Ar_dr[0]["Cte_FletePaga"] = Cte_FletePaga;
                Ar_dr[0]["Cte_PorcComision"] = Cte_PorcComision;
                Ar_dr[0]["Id_Uen"] = uen;
                //Ar_dr[0]["ModalidadOP"] = ModalidadOP;
                //Ar_dr[0]["Meta"] = Meta;
                //Ar_dr[0]["ModalidadOP_Desc"] = ModalidadOP_Desc;

                HtmlInputCheckBox chkTradicional = e.Item.FindControl("chkTradicionalEdicion") as HtmlInputCheckBox;
                HtmlInputCheckBox chkGarantia = e.Item.FindControl("chkGarantiaEdicion") as HtmlInputCheckBox;
                ActualizarRegistroDeDetalle(Ar_dr[0]["Id_CteDet"].ToString(), chkTradicional.Checked ? "1" : "0", chkGarantia.Checked ? "1" : "0");
                if (!chkGarantia.Checked)
                {
                    RadComboBox rcbGarantias = e.Item.FindControl("rcbGarantiasEdit") as RadComboBox;
                    foreach (var item in rcbGarantias.Items.AsEnumerable())
                    {
                        CheckBox chk = item.FindControl("chkItem") as CheckBox;
                        chk.Checked = false;
                    }
                }

                if (!chkTradicional.Checked && !chkGarantia.Checked)
                {
                    chkTradicional.Checked = true;
                }

                ActualizarGarantiasAsociadas(e, Ar_dr[0]);
                Ar_dr[0]["Cte_Tradicional"] = chkTradicional.Checked;
                Ar_dr[0]["Cte_Garantia"] = chkGarantia.Checked;
                //Ar_dr[0].AcceptChanges();

                DataRow[] Ar_drTer;
                Ar_drTer = dt.Select("Id_Ter='" + Id_Ter + "'");

                CargarTerritoriosAutorizacion(Ar_drTer[0].ItemArray, "Edit", true);


            }
        }

        private void UpdateGridDireccionesEntrega(GridCommandEventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int id_CteDirEntrega = 0;
            string cte_Calle = "";
            string cte_Numero = "";
            string cte_Cp = "";
            string cte_Colonia = "";
            string cte_Municipio = "";
            string cte_Estado = "";
            string cte_Sector = "";
            string cte_Telefono = "";
            string cte_Fax = "";
            string cte_HoraAm1 = "";
            string cte_HoraAm2 = "";
            string cte_HoraPm1 = "";
            string cte_HoraPm2 = "";

            GridItem gi = null;

            DataRow[] Ar_dr;
            gi = e.Item;

            id_CteDirEntrega = Convert.ToInt32(((Label)gi.FindControl("LblRGEIdEdit")).Text);
            cte_Calle = ((RadTextBox)gi.FindControl("TxtRGCalle")).Text;
            cte_Numero = ((RadTextBox)gi.FindControl("TxtRGNumero")).Text;
            cte_Cp = ((RadNumericTextBox)gi.FindControl("TxtRGCp")).Text;
            cte_Colonia = ((RadTextBox)gi.FindControl("TxtRGColonia")).Text;
            cte_Municipio = ((RadTextBox)gi.FindControl("TxtRGMunicipio")).Text;
            cte_Estado = ((RadTextBox)gi.FindControl("TxtRGEstado")).Text;
            cte_Sector = ((RadTextBox)gi.FindControl("TxtRGSector")).Text;
            cte_Telefono = ((RadTextBox)gi.FindControl("TxtRGTelefono")).Text;
            cte_Fax = ((RadTextBox)gi.FindControl("TxtRGFax")).Text;
            cte_HoraAm1 = ((RadTextBox)gi.FindControl("TxtRGAm1")).Text;
            cte_HoraAm2 = ((RadTextBox)gi.FindControl("TxtRGAm2")).Text;
            cte_HoraPm1 = ((RadTextBox)gi.FindControl("TxtRGPm1")).Text;
            cte_HoraPm2 = ((RadTextBox)gi.FindControl("TxtRGPm2")).Text;

            Ar_dr = dtDireccionEntrega.Select("Id_CteDirEntrega='" + id_CteDirEntrega + "'");
            if (Ar_dr.Length > 0)
            {
                DataRow dr;
                if (dt.Rows.Count > 0)
                    Ar_dr[0].BeginEdit();
                Ar_dr[0]["Id_CteDirEntrega"] = id_CteDirEntrega;
                Ar_dr[0]["Cte_Calle"] = cte_Calle;
                Ar_dr[0]["Cte_Numero"] = cte_Numero;
                Ar_dr[0]["Cte_Cp"] = cte_Cp;
                Ar_dr[0]["Cte_Colonia"] = cte_Colonia;
                Ar_dr[0]["Cte_Municipio"] = cte_Municipio;
                Ar_dr[0]["Cte_Estado"] = cte_Estado;
                Ar_dr[0]["Cte_Sector"] = cte_Sector;
                Ar_dr[0]["Cte_Telefono"] = cte_Telefono;
                Ar_dr[0]["Cte_Fax"] = cte_Fax;
                Ar_dr[0]["Cte_HoraAm1"] = cte_HoraAm1;
                Ar_dr[0]["Cte_HoraAm2"] = cte_HoraAm2;
                Ar_dr[0]["Cte_HoraPm1"] = cte_HoraPm1;
                Ar_dr[0]["Cte_HoraPm2"] = cte_HoraPm2;
                Ar_dr[0].AcceptChanges();
            }
        }

        private void PerformInsert(GridCommandEventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];

            int Ter_Tipo = 0;
            string DescTer_Tipo = string.Empty;
            int Id_CteDet = 0;
            int Id_Ter = 0;
            int Id_Seg = 0;
            int id_uen = 0;
            string Ter_Nombre = "";
            string Seg_Descripcion = "";
            string Cte_UnidadDim = "";
            string Cte_Dimension = "";
            int Id_Rik = 0;
            string Rik = "";
            string Uen = "";
            double Cte_Pesos = 0;
            double Cte_CarMP = 0;
            double Cte_GastoVarT = 0;
            double Cte_FletePaga = 0;
            double Cte_PorcComision = 0;
            double Cte_Potencial = 0;
            bool Cte_Activo = false;
            GridItem gi = null;
            gi = e.Item;
            if (((RadComboBox)gi.FindControl("RadComboBox1")).Text == "" ||
                ((RadComboBox)gi.FindControl("RadComboBox2")).Text == "" ||
                ((Label)gi.FindControl("RadTextBox3")).Text == "" ||
                ((RadNumericTextBox)gi.FindControl("RadTextBox4")).Text == "" ||
                ((RadNumericTextBox)gi.FindControl("RadNumericTextBox5")).Text == "")
            {
                e.Canceled = true;
                Alerta("Todos los campos son requeridos");
                return;
            }

            Ter_Tipo = 3; /*Convert.ToInt32(((RadComboBox)gi.FindControl("RadTipoTerritorio")).SelectedValue);aqui*/
            DescTer_Tipo = "RIK";/*((RadComboBox)gi.FindControl("RadTipoTerritorio")).Text aqui*/;

            Id_Ter = Convert.ToInt32(((RadComboBox)gi.FindControl("RadComboBox1")).SelectedValue);
            Ter_Nombre = ((RadComboBox)gi.FindControl("RadComboBox1")).Text;
            Id_Seg = Convert.ToInt32(((RadComboBox)gi.FindControl("RadComboBox2")).SelectedValue);
            Seg_Descripcion = ((RadComboBox)gi.FindControl("RadComboBox2")).Text;
            Cte_UnidadDim = ((Label)gi.FindControl("RadTextBox3")).Text;
            Cte_Dimension = ((RadNumericTextBox)gi.FindControl("RadTextBox4")).Text;
            Cte_Pesos = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox5")).Text);
            Cte_Potencial = !string.IsNullOrEmpty(((RadNumericTextBox)gi.FindControl("RadNumericTextBox6")).Text) ? Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox6")).Text) : 0;
            Cte_Activo = ((CheckBox)gi.FindControl("chk8")).Checked;
            //Cte_Activo = false;//((CheckBox)gi.FindControl("chk8")).Checked;

            Cte_CarMP = !string.IsNullOrEmpty(((RadNumericTextBox)gi.FindControl("RadNumericTextBox7")).Text) ? Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox7")).Text) : 0;
            Cte_GastoVarT = !string.IsNullOrEmpty(((RadNumericTextBox)gi.FindControl("RadNumericTextBox8")).Text) ? Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox8")).Text) : 0;
            Cte_FletePaga = !string.IsNullOrEmpty(((RadNumericTextBox)gi.FindControl("RadNumericTextBox9")).Text) ? Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox9")).Text) : 0;
            Cte_PorcComision = !string.IsNullOrEmpty(((RadNumericTextBox)gi.FindControl("txtComision")).Text) ? Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtComision")).Text) : 0;


            //SAUL GUERRA  BEGIN
            int Id_TerrServ = Convert.ToInt32(((RadComboBox)gi.FindControl("cbTerServ")).SelectedValue);
            string TerrServ = ((RadComboBox)gi.FindControl("cbTerServ")).Text;
            int Id_RIKServ = Convert.ToInt32(((Label)gi.FindControl("lblId_RIKServEdit")).Text == "" ? "0" : ((Label)gi.FindControl("lblId_RIKServEdit")).Text);
            string RIKServ = ((Label)gi.FindControl("lblRIKServEdit")).Text;
            //SAUL GUERRA  END

            /**
             * ID: AGSO
             * Fecha: 2015/09/09, 1251
             * Requerimiento: Garantias, KQ-R1001-00Y
             **/
            //RadComboBox rcbModalidadOP = (RadComboBox)gi.FindControl("cboModalidadOP");
            //if (rcbModalidadOP.Items.Count < 1)
            //{
            //    throw new Exception("Se debe elegir un valor para la Modalidad de Operación");
            //}
            /*Requerimiento: Garantias, KQ-R1001-00Y*/

            //EDSG 20150902
            //ModalidadOP = Convert.ToInt32(((RadComboBox)gi.FindControl("cboModalidadOP")).SelectedValue);
            //if (((RadNumericTextBox)gi.FindControl("txtMeta")).Text != "") Meta = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtMeta")).Text);
            //ModalidadOP_Desc = ((RadComboBox)gi.FindControl("cboModalidadOP")).SelectedItem.Text;
            //ModalidadOP_Desc

            /**
             * ID: AGSO
             * Fecha: 2015/09/09, 1235
             * Requerimiento: Garantias, KQ-R1001-00X
             **/
            //if (ModalidadOP_Desc == "GARANTIA")
            //{
            //    try
            //    {
            //        ValidarSeleccionGarantiaEnModalidadOperacion(gi);
            //    }
            //    catch (Exception ex)
            //    {
            //        e.Canceled = true;
            //        throw ex;
            //    }

            //}

            /*Requerimiento: Garantias, KQ-R1001-00X*/
            if (Cte_Potencial == 0)
            {
                AlertaFocus("El potencial no debe ser cero", ((RadNumericTextBox)gi.FindControl("RadNumericTextBox6")).ClientID);
                e.Canceled = true;
                return;
            }

            CN_CatTerritorios cn_catterritorios = new CN_CatTerritorios();
            Territorios catterr = new Territorios();
            catterr.Id_Emp = sesion.Id_Emp;
            catterr.Id_Cd = sesion.Id_Cd_Ver;
            catterr.Id_Ter = Id_Ter;
            cn_catterritorios.ConsultaTerritorios(ref catterr, sesion.Emp_Cnx);
            Rik = catterr.Rik_Nombre;
            Id_Rik = catterr.Id_Rik;
            Uen = catterr.Uen_Descripcion;
            id_uen = catterr.Id_Uen;
            DataRow[] Ar_dr;
            DataRow dr;
            Ar_dr = dt.Select();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    int UEN = !string.IsNullOrEmpty(dr["Id_Uen"].ToString()) ? Convert.ToInt32(dr["Id_Uen"].ToString()) : 0;
                    bool activo = Convert.ToBoolean(dr["Cte_Activo"]);
                    if (UEN == id_uen && activo == Cte_Activo)
                    {
                        Cte_Activo = false;
                    }
                }
            }
            Id_CteDet = dt.Rows.Count + 1;
            HtmlInputCheckBox chkTraidicional = e.Item.FindControl("chkTradicionalEdicion") as HtmlInputCheckBox;
            HtmlInputCheckBox chkGarantia = e.Item.FindControl("chkGarantiaEdicion") as HtmlInputCheckBox;
            var row = dt.Rows.Add(new object[] {    
                                Id_CteDet,
                                Ter_Tipo,
                                DescTer_Tipo,
                                Id_Ter, 
                                Ter_Nombre,
                                Id_Seg,
                                Seg_Descripcion,
                                Cte_UnidadDim, 
                                Convert.ToDouble(Cte_Dimension).ToString("#,##0.00"),
                                Cte_Pesos,
                                Cte_Potencial,
                                Cte_Activo,
                                Id_Rik,
                                Rik,

                                //SAUL GUERRA  BEGIN
                                Id_TerrServ,
                                TerrServ,
                                Id_RIKServ,
                                RIKServ,
                                //SAUL GUERRA  END

                                Uen,
                                Cte_CarMP,
                                Cte_GastoVarT,
                                Cte_FletePaga,
                                Cte_PorcComision,
                                id_uen,
                                1,
                                chkTraidicional.Checked,
                                chkGarantia.Checked,
                                Id_Cte,
                                DateTime.Now
            });
            CrearRegistroDeDetalle(Id_CteDet.ToString(), chkTraidicional.Checked ? "1" : "0", chkGarantia.Checked ? "1" : "0");
            ActualizarGarantiasAsociadas(e, row);
            DataRow[] Ar_drSol;
            Ar_drSol = dt.Select("Id_Ter='" + Id_Ter + "'");
            
            CargarTerritoriosAutorizacion(Ar_drSol[0].ItemArray, "nuevo", true);
        }

        /// <summary>
        /// Determina el identificador del cliente en el contexto.
        /// </summary>
        protected int Id_Cte
        {
            get
            {
                int idCte = 0;
                int.TryParse(cmbCliente.SelectedValue, out idCte);
                return idCte;
            }
        }

        /**
         * Este método evalua las validaciones asociadas a la elección del valor "GARANTIA" en el campo "MODALIDAD DE OPERACION".
         * Específicamente:
         *  a) Que todos los campos del ACYS se encuentren capturados
         **/
        private void ValidarSeleccionGarantiaEnModalidadOperacion(GridItem gi)
        {
            RadNumericTextBox RadNumericTextBox6 = ((RadNumericTextBox)gi.FindControl("RadNumericTextBox6"));
            RadNumericTextBox RadNumericTextBox7 = ((RadNumericTextBox)gi.FindControl("RadNumericTextBox7"));
            RadNumericTextBox RadNumericTextBox8 = ((RadNumericTextBox)gi.FindControl("RadNumericTextBox8"));
            RadNumericTextBox RadNumericTextBox9 = ((RadNumericTextBox)gi.FindControl("RadNumericTextBox9"));
            RadNumericTextBox txtComision = ((RadNumericTextBox)gi.FindControl("txtComision"));

            //RadNumericTextBox txtMeta = ((RadNumericTextBox)gi.FindControl("txtMeta"));
            RadComboBox cbTerServ = ((RadComboBox)gi.FindControl("cbTerServ"));

            if (string.IsNullOrEmpty(RadNumericTextBox6.Text) || string.IsNullOrEmpty(RadNumericTextBox7.Text) || string.IsNullOrEmpty(RadNumericTextBox8.Text) || string.IsNullOrEmpty(RadNumericTextBox9.Text) || string.IsNullOrEmpty(txtComision.Text) || cbTerServ.Items.Count < 1 /*|| string.IsNullOrEmpty(txtMeta.Text)*/)
            {
                throw new Exception("El sistema NO PERMITE Mov. sin ACYS");
            }
        }
        private void Edit(GridCommandEventArgs e)
        {
            //RMB
            //Inicio
            DataRow[] Ar_dr;
            GridItem gi = null;
            gi = e.Item;

            Int32 Id_Ter = Convert.ToInt32(((Label)gi.FindControl("Label100")).Text);
            Ar_dr = dt.Select("Id_Ter='" + Id_Ter + "'");

            string Fec_Solicitud = Ar_dr[0]["Fec_Solicitud"].ToString();
            string Fec_Autorizado = Ar_dr[0]["Fec_Autorizado"].ToString();
            string Fec_Rechazado = Ar_dr[0]["Fec_Rechazado"].ToString();

            if (Fec_Solicitud != "" && Fec_Autorizado == "")
            {
                Alerta("Este territorio esta pendiente de una autorización de cambio, no se puede realizar otro cambio hasta que sea atendida la anterior.");
                rgDetalles.Rebind();
                return;
            }
            if (Fec_Solicitud != "" || Fec_Rechazado == "")
            {
                Alerta("Este territorio esta pendiente de una autorización de cambio, no se puede realizar otro cambio hasta que sea atendida la anterior.");
                rgDetalles.Rebind();
                return;
            }
            
            CargarTerritoriosAutorizacionAnt(Ar_dr[0].ItemArray, "Edit",false);
           //Fin

            //GridItem gi = e.Item;
            //(gi.FindControl("RadComboBox1") as RadComboBox).Enabled = Convert.ToBoolean((gi.FindControl("lblEditable") as Label).Text);
            //GridItem gi = e.Item;
            //(gi.FindControl("RadComboBox1") as RadComboBox).Enabled = Convert.ToBoolean((gi.FindControl("lblEditable") as Label).Text);
        }

        protected void DeleteGridDireccionesEntrega(GridCommandEventArgs e)
        {
            int Id_CteDirEntrega = 0;
            GridItem gi = null;
            DataRow[] Ar_dr;
            DataRow dr;
            gi = e.Item;

            Id_CteDirEntrega = Convert.ToInt32(((Label)gi.FindControl("LblRGEId")).Text);
            Ar_dr = dtDireccionEntrega.Select("Id_CteDirEntrega='" + Id_CteDirEntrega + "'");
            if (Ar_dr.Length > 0)
            {
                Ar_dr[0].Delete();
                dtDireccionEntrega.AcceptChanges();
            }
            for (int x = 0; x < dtDireccionEntrega.Rows.Count; x++)
            {
                dr = dtDireccionEntrega.Rows[x];
                dr.BeginEdit();
                dr["Id_CteDirEntrega"] = x + 1;
                dr.AcceptChanges();
            }
        }

        private void Delete(GridCommandEventArgs e)
        {
            int Id_CteDet = 0;
            GridItem gi = null;
            DataRow[] Ar_dr;
            DataRow dr;
            gi = e.Item;

            if (((Label)gi.FindControl("lbleditar")).Text == "False")
            {
                Alerta("Imposible eliminar, existen documentos que utilizan esta relacion");
                return;
            }

            Id_CteDet = Convert.ToInt32(((Label)gi.FindControl("Label0")).Text);
            Ar_dr = dt.Select("Id_CteDet='" + Id_CteDet + "'");
            if (Ar_dr.Length > 0)
            {
                Ar_dr[0].Delete();
                dt.AcceptChanges();
            }
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                dr = dt.Rows[x];
                dr.BeginEdit();
                dr["Id_CteDet"] = x + 1;
                dr.AcceptChanges();
            }
        }
        private bool Deshabilitar()
        {
            try
            {
                bool verificador = false;
                if (HF_ID.Value != "")
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = Sesion.Id_Emp;
                    ct.Id_Cd = Sesion.Id_Cd_Ver;
                    ct.Id = Convert.ToInt32(HF_ID.Value);
                    ct.Tabla = "CatCliente";
                    ct.Columna = "Id_Cte";
                    CN_Comun.Deshabilitar(ct, Sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void deshabilitarcontroles(System.Web.UI.ControlCollection controles_contenidos, bool valor)
        {
            for (int x = 0; x < controles_contenidos.Count; x++)
            {
                string Type = controles_contenidos[x].GetType().FullName;

                if (Type.Contains("RadMultiPage") || Type.Contains("RadPageView") || Type.Contains("Panel"))
                {
                    deshabilitarcontroles(controles_contenidos[x].Controls, valor);
                }

                switch (Type.Replace("Telerik.Web.UI.", ""))
                {
                    case "RadNumericTextBox":
                        (controles_contenidos[x] as RadNumericTextBox).Enabled = valor;
                        break;
                    case "RadTextBox":
                        (controles_contenidos[x] as RadTextBox).Enabled = valor;
                        break;
                    case "RadComboBox":
                        (controles_contenidos[x] as RadComboBox).Enabled = valor;
                        break;
                    case "RadDatePicker":
                        (controles_contenidos[x] as RadDatePicker).Enabled = valor;
                        break;
                    case "RadDateTimePicker":
                        (controles_contenidos[x] as RadDateTimePicker).Enabled = valor;
                        break;
                    case "RadTimePicker":
                        (controles_contenidos[x] as RadTimePicker).Enabled = valor;
                        break;
                    case "RadListBox":
                        (controles_contenidos[x] as RadListBox).Enabled = valor;
                        break;

                }
                if (Type.Contains("System.Web.UI.WebControls.CheckBox"))
                {
                    (controles_contenidos[x] as System.Web.UI.WebControls.CheckBox).Enabled = valor;
                }


            }
        }
        private void ValidarCheckCobranza()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Id_cte;
                int Verificador = 0;
                Id_cte = int.Parse(txtClave.Text);
                CN_CatUsuario cn_catu = new CN_CatUsuario();

                cn_catu.ConsultaModificarCredito(Id_cte, sesion, ref Verificador);

                if (Verificador == 1)
                {
                    chkCredSuspendido.Enabled = true;
                }
                else
                {
                    chkCredSuspendido.Enabled = false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #region Cliente-Territorio
        //RBM
        //Cambios para Cliente-Territorio en el catalogo de clientes
        //Se guarda comentarios en la solicitud 
        private void EnviarComentarios(int Id_Ter)
        {
            try
            {
                //return RAM1.ResponseScripts.Add(@"return EnviarComentariosTerritorios('", Id_Ter, "')").ToString();
                RAM1.ResponseScripts.Add(string.Concat(@"EnviarComentariosTerritorios('", Id_Ter, "')"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Se envia correo a la persona encargada del proceso de actualizacion de cambios en cliente-territorio
        private void EnviaEmailAutorizacionTerritorios(Clientes clientes, DataTable objdtTablaTerritorios)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);

                for (int x = 0; x < objdtTablaTerritorios.Rows.Count; x++)
                {
                    StringBuilder cuerpo_correo = new StringBuilder();
                    cuerpo_correo.Append("<div align='center'>");
                    cuerpo_correo.Append("<table><tr><td>");
                    cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                    cuerpo_correo.Append("<td></td>");
                    cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                    cuerpo_correo.Append("</tr><tr>");
                    cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                    cuerpo_correo.Append("Solicitud # " + objdtTablaTerritorios.Rows[x]["Id_Solicitud"]);
                    cuerpo_correo.Append("</td></tr><tr>");
                    cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                    cuerpo_correo.Append("Se ha solicitado cambio en el Territorio del cliente # " + Id_Cte);
                    cuerpo_correo.Append("</td></tr><tr><td><b><font face='Tahoma' size='2'>");
                    cuerpo_correo.Append("Centro de distribución:  " + session.Id_Cd_Ver + " - " + session.Cd_Nombre);
                    cuerpo_correo.Append("</td></tr><tr><td><b><font face='Tahoma' size='2'>");
                    cuerpo_correo.Append("Solicitó : " + session.Id_U + " - " + session.U_Nombre);
                    cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                    string[] url = Request.Url.ToString().Split(new char[] { '/' });

                    cuerpo_correo.Append("<center><br><br>");
                    cuerpo_correo.Append("<a href='" + "http://207.248.253.97/siancentralcliente-ter/" + "CapAutClienteTerr.aspx?Id_Solicitud=" + objdtTablaTerritorios.Rows[x]["Id_Solicitud"] + "&Id_Cte=" + clientes.Id_Cte + "&Id_Ter=" + objdtTablaTerritorios.Rows[x]["Id_Ter"] + "&Id_Emp=" + objdtTablaTerritorios.Rows[x]["Id_Emp"] + "&Id_Cd=" + objdtTablaTerritorios.Rows[x]["Id_Cd"] + "&Accion=2&email=1&PermisoGuardar=false&PermisoModificar=false&PermisoEliminar=false&PermisoImprimir=false&facModificable=0'" + ">");
                    cuerpo_correo.Append("Solicitud de cambios en Cliente-Territorio del catalogo de clientes</a></font></center>");
                    cuerpo_correo.Append("</td></tr></table></div>");

                    SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                    sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                    //sm.EnableSsl = true;
                    MailMessage m = new MailMessage();
                    m.From = new MailAddress(configuracion.Mail_Remitente);
                    m.To.Add(new MailAddress(configuracion.Mail_Autorizaterritorios));
                    m.Subject = "Solicitud de autorización para cambios en los territorios del cliente #" + Id_Cte + " del centro " + session.Id_Cd_Ver;
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
                        //CambiarEstatus(session, Id_Fac, "S");
                    }
                    catch (Exception)
                    {
                        //CambiarEstatus(ordCompra, "C");
                        Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                        return;
                    }
                    Alerta("Solicitud enviada correctamente");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //RBM
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

        private void CargarFormaPagoParaPagos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatPagoFormaPago_Combo", ref cmbPFormaPago);
                if (cmbPFormaPago.Items.Count > 0)
                {
                    cmbPFormaPago.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            string cmd = e.Argument.ToString();
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            switch (cmd)
            {
                case "Comentarios":
                    string Comentarios = Session["Comentarios" + Session.SessionID].ToString();
                    int Id_Ter = int.Parse(Session["Territorio" + Session.SessionID].ToString()); 
                        DataRow[] Ar_dr;
                        Ar_dr = objdtTablaTerritorios.Select("Id_Ter='" + Id_Ter + "'");
                        
                        Ar_dr[0].BeginEdit();
                        Ar_dr[0]["comentarios"] = Comentarios;

                        Ar_dr[0].AcceptChanges();
                    break;
            }


            //if (cmd.Split(new string[] { "@" }, StringSplitOptions.None)[3] == "1")
            //{
            //    if (Session["Sesion" + Session.SessionID + "cambioTipoCliente"] != null)
            //    {
            //        bool cambioTipoCliente = Session["Sesion" + Session.SessionID + "cambioTipoCliente"].ToString().ToUpper() == "TRUE";
            //        if (cambioTipoCliente)
            //        {
            //            AbrirVentana_AutorizacionTipoCliente();
            //        }
            //        else
            //        {
            //            Guardar(Convert.ToInt32(cmd.Split(new string[] { "@" }, StringSplitOptions.None)[0]), Convert.ToInt32(cmd.Split(new string[] { "@" }, StringSplitOptions.None)[1]));
            //            txtAutorizo.Text = cmd.Split(new string[] { "@" }, StringSplitOptions.None)[2];
            //        }
            //    }
            //    else
            //    {
            //        AbrirVentana_AutorizacionTipoCliente();
            //    }
            //}
            //else
            //{
            //    Guardar(Convert.ToInt32(cmd.Split(new string[] { "@" }, StringSplitOptions.None)[0]), Convert.ToInt32(cmd.Split(new string[] { "@" }, StringSplitOptions.None)[1]));
            //    txtAutorizo.Text = cmd.Split(new string[] { "@" }, StringSplitOptions.None)[2];
            //}

        }
    }
}
