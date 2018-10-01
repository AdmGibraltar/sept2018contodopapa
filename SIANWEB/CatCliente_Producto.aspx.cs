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
using System.Globalization;
using System.Collections;
using CapaDatos;
using Telerik.Reporting.Processing;
using System.IO;

namespace SIANWEB
{
    public partial class CatCliente_Producto : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
        }
        bool lleno = false;
        bool lleno2 = false;
        #endregion
        #region Eventos
        protected void Page_Init(object sender, EventArgs e)
        {
            CargarClientes();
            CargarProductos();
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
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
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
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Inicializar();
                Nuevo();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
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
                    else if (btn.CommandName == "print")
                    {
                        this.Listado(true);
                    }
                    else if (btn.CommandName == "excel")
                    {
                        this.Listado(false);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
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
        //COMBO TIPO DE PRECIO GRID
        protected void cmb2_DataBinding(object sender, EventArgs e)
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
        protected void RadComboBox_DataBinding(object sender, EventArgs e)
        {
            CargarTPrecios((RadComboBox)sender);
        }
        protected void RadComboBox_DataBound(object sender, EventArgs e)
        {
            RadComboBox comboBox = ((RadComboBox)sender);
            string id = ((Label)comboBox.Parent.Parent.FindControl("lblold1")).Text;
            if (id != "")
                comboBox.SelectedIndex = comboBox.FindItemIndexByValue(id);
        }
        //COMBO PRODUCTO
        protected void cmbProducto_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ObtenerProductoInfo();
                if (txtClienteID.Text != "")
                {
                    GetListDet();
                    rgDet.Rebind();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        //COMBO CLIENTE
        protected void cmbCliente_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                if (txtProductoID.Text != "")
                {

                    GetListDet();
                    rgDet.Rebind();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        //GRID
        protected void rgDet_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgDet.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDet_ItemCommand(object source, GridCommandEventArgs e)
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
                        if (rgDet.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        gi = e.Item;


                        if (((RadComboBox)gi.FindControl("RadComboBox1")).Text == "" ||
                            ((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Text == "")
                        {
                            e.Canceled = true;
                            break;
                        }

                        Tprecio = Convert.ToInt32(((RadComboBox)gi.FindControl("RadComboBox1")).SelectedValue);
                        TPrecioStr = ((RadComboBox)gi.FindControl("RadComboBox1")).Text;
                        Precio = ((RadComboBox)gi.FindControl("RadComboBox2")).FindItemByValue(((RadComboBox)gi.FindControl("RadComboBox1")).SelectedValue).Text;
                        Pesos = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Text);

                        Id_ClpDet = dt.Rows.Count;

                        dr = dt.Select("TPrecio='" + Tprecio + "'");
                        if (dr.Length > 0)
                        {
                            Alerta("Ya se ha establecido un tipo de precio " + TPrecioStr);
                            e.Canceled = true;
                            return;
                        }

                        dt.Rows.Add(new object[] {  
                                Id_ClpDet, 
                                Tprecio, 
                                TPrecioStr,
                                Precio,
                                Pesos});
                        break;
                    case "Update":
                        gi = e.Item;

                        if (((RadComboBox)gi.FindControl("RadComboBox1")).Text == "" ||
                           ((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Text == "")
                        {
                            e.Canceled = true;
                            break;
                        }

                        Tprecio = Convert.ToInt32(((RadComboBox)gi.FindControl("RadComboBox1")).SelectedValue);
                        TPrecioStr = ((RadComboBox)gi.FindControl("RadComboBox1")).Text;
                        Precio = ((RadComboBox)gi.FindControl("RadComboBox2")).FindItemByValue(((RadComboBox)gi.FindControl("RadComboBox1")).SelectedValue).Text;
                        Pesos = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Text);
                        Id_ClpDet = Convert.ToInt32(((Label)gi.FindControl("lblold0")).Text);

                        dr = dt.Select("TPrecio='" + Tprecio + "'");
                        //if (dr.Length > 0)
                        //{
                        //    Alerta("Ya se ha establecido un tipo de precio " + TPrecioStr);
                        //    e.Canceled = true;
                        //    return;
                        //}

                        Ar_dr = dt.Select("Id_ClpDet='" + Id_ClpDet + "'");
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
                        Ar_dr = dt.Select("Id_ClpDet='" + Id_ClpDet + "'");
                        if (Ar_dr.Length > 0)
                        {
                            Ar_dr[0].Delete();
                            dt.AcceptChanges();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgDet.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtClave_TextChanged(object sender, EventArgs e)
        {
            if (txtClienteID.Text == "")
            {
                cmbCliente.ClearSelection();
                cmbCliente.Text = "";
                return;
            }

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CatCliente cn_cliente = new CN_CatCliente();
            Clientes cliente = new Clientes();
            cliente.Id_Emp = Sesion.Id_Emp;
            cliente.Id_Cd = Sesion.Id_Cd_Ver;
            cliente.Id_Cte = txtClienteID.Value.HasValue ? Convert.ToInt32(txtClienteID.Value.Value) : 0;
            try
            {
                cn_cliente.ConsultaClientes(ref cliente, Sesion.Emp_Cnx);
            }
            catch (Exception ex)
            {
                AlertaFocus(ex.Message, txtClienteID.ClientID);
                txtClienteID.Text = "";
                cmbCliente.Text = "";
                cmbCliente_SelectedIndexChanged(null, null);
                return;
            }
            if (cliente.Cte_NomComercial != null)
            {
                cmbCliente.SelectedValue = cliente.Id_Cte.ToString();
                cmbCliente.Text = cliente.Cte_NomComercial;
                lblCliente.Text = cliente.Id_Cte.ToString() + " - " + cliente.Cte_NomComercial;
            }
            else
            {
                txtClienteID.Text = "";
                cmbCliente.Text = "";
                lblCliente.Text = "";
            }
            cmbCliente_SelectedIndexChanged(null, null);
            txtProductoID.Focus();
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
                lblProducto.Text = prd.Id_Prd.ToString() + " - " + prd.Prd_Descripcion;
                cmbProducto_SelectedIndexChanged(null, null);
            }
            else
            {
                txtProductoID.Text = "";
                cmbProducto.Text = "";
                lblProducto.Text = "";
                Inicializar();
            }


        }
        #endregion
        #region Funciones
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

                    txtInventarioFin.Value = prd.Prd_InvFinal;
                    txtAsignado.Value = prd.Prd_Asignado;
                }
                else
                {
                    txtInventarioFin.Text = "";
                    txtAsignado.Text = "";
                    cmbProducto.Text = "";
                    txtClave.Text = "";
                    txtDescripcion.Text = "";
                    txtPresentacion.Text = "";
                    txtUnidades.Text = "";
                    txtCantFact.Text = "";
                    dt = new DataTable();
                    DataColumn dc = new DataColumn();
                    dt.Columns.Add("Id_ClpDet", System.Type.GetType("System.Int32"));
                    dt.Columns.Add("TPrecio", System.Type.GetType("System.Int32"));
                    dt.Columns.Add("TPrecioStr", System.Type.GetType("System.String"));
                    dt.Columns.Add("Precio", System.Type.GetType("System.String"));
                    dt.Columns.Add("Pesos", System.Type.GetType("System.Double"));
                    rgDet.Rebind();

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
        private void CargarClientes() //LOCAL
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion != null)
                {
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatCliente_Combo", ref cmbCliente);
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
                Permiso.Id_Cd = Sesion.Id_Cd_Ver;
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
                    //this.rtb1.Items[2].Visible = false;
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
        private void Inicializar()
        {
            try
            {
                txtClave.Text = "";
                GetListDet();
                rgDet.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Nuevo()
        {
            txtClave.Enabled = true;
            txtClave.Text = "";
            txtAsignado.Text = string.Empty;
            txtCantFact.Text = string.Empty;
            txtClienteID.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtInventarioFin.Text = string.Empty;
            txtPresentacion.Text = string.Empty;
            txtProductoID.Text = string.Empty;
            txtUnidades.Text = string.Empty;
            cmbCliente.ClearSelection();
            cmbCliente.Text = "";
            cmbProducto.ClearSelection();
            cmbProducto.Text = "";
            dpUltimaVta.SelectedDate = null;
            chkActivo.Checked = true;
            HF_ID.Value = "";
            GetListDet();
            rgDet.Rebind();
        }
        private void Guardar()
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                ClienteProd clienteprod = new ClienteProd();
                clienteprod.Id_Emp = session.Id_Emp;
                clienteprod.Id_Cd = session.Id_Cd_Ver;
                clienteprod.Id_Cte = !string.IsNullOrEmpty(txtClienteID.Text) ? Convert.ToInt32(txtClienteID.Text) : 0;
                clienteprod.Id_Prd = !string.IsNullOrEmpty(txtProductoID.Text) ? Convert.ToInt32(txtProductoID.Text) : 0;
                clienteprod.Clp_descripcion = txtDescripcion.Text;
                clienteprod.Estatus = chkActivo.Checked;
                clienteprod.Id_Clp = !string.IsNullOrEmpty(txtClave.Text) ? txtClave.Text : "";
                clienteprod.Clp_Presentacion = txtPresentacion.Text;
                clienteprod.Unidades = txtUnidades.Text;
                clienteprod.CantFact = !string.IsNullOrEmpty(txtCantFact.Text) ? Convert.ToInt32(txtCantFact.Text) : 0;

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
                if (HF_ID.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }

                    clsCatClienteProd.InsertarClienteProd(clienteprod, session.Emp_Cnx, ref verificador);

                    if (verificador == 1)
                    {
                        clsCatClienteProd.InsertarClienteProdDet(clienteprod, dt, session.Emp_Cnx, ref verificador);
                        Nuevo();
                        RadTabStrip1.Tabs[0].Selected = true;
                        RadMultiPage1.PageViews[0].Selected = true;
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

                    if (verificador == 1)
                    {
                        clsCatClienteProd.ModificarClienteProdDet(clienteprod, dt, session.Emp_Cnx, ref verificador);
                        Nuevo();
                        RadTabStrip1.Tabs[0].Selected = true;
                        RadMultiPage1.PageViews[0].Selected = true;
                        Alerta("Los datos se modificaron correctamente");
                    }
                    else
                        Alerta("Ocurrió un error al intentar modificar los datos");
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
                if (HF_ID.Value != "")
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = Sesion.Id_Emp;
                    ct.Id_Cd = Sesion.Id_Cd_Ver;
                    ct.Id = Convert.ToInt32(HF_ID.Value);
                    ct.Tabla = "CatClienteProducto";
                    ct.Columna = "Id_Clp";
                    CN_Comun.Deshabilitar(ct, Sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
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
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CatClienteProducto", "Id_Clp", Sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Listado(bool a_pantalla)
        {

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            ArrayList ALValorParametrosInternos = new ArrayList();

            Funciones funcion = new Funciones();
            DateTime date = funcion.GetLocalDateTime(sesion.Minutos);

            ALValorParametrosInternos.Add(sesion.Emp_Nombre);
            ALValorParametrosInternos.Add(sesion.Cd_Nombre);
            ALValorParametrosInternos.Add(date.ToShortDateString());
            ALValorParametrosInternos.Add(date.ToShortTimeString());
            ALValorParametrosInternos.Add(sesion.U_Nombre);
            ALValorParametrosInternos.Add(txtClienteID.Text);
            ALValorParametrosInternos.Add(sesion.Emp_Cnx);
            ALValorParametrosInternos.Add(sesion.Id_Emp);
            ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
            Type instance = null;
            if (a_pantalla)
            {
                instance = typeof(LibreriaReportes.Rep_ClienteProducto);
            }
            else
            {
                instance = typeof(LibreriaReportes.ExpRep_ClienteProducto);
            }

            if (a_pantalla)
            {
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
            }
            else
            {
                ImprimirXLS(ALValorParametrosInternos, instance);
            }
        }
        private void ImprimirXLS(ArrayList ALValorParametrosInternos, Type instance)
        {
            try
            {
                Telerik.Reporting.Report report1 = (Telerik.Reporting.Report)Activator.CreateInstance(instance);
                for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
                {
                    report1.ReportParameters[i].AllowNull = true;
                    report1.ReportParameters[i].Value = ALValorParametrosInternos[i];
                }
                ReportProcessor reportProcessor = new ReportProcessor();
                RenderingResult result = reportProcessor.RenderReport("XLS", report1, null);

                string ruta = Server.MapPath("Reportes") + "\\" + instance.Name + ".xls";
                if (File.Exists(ruta))
                    File.Delete(ruta);
                FileStream fs = new FileStream(ruta, FileMode.Create);
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);

                fs.Flush();
                fs.Close();

                RAM1.ResponseScripts.Add("startDownload('" + instance.Name + ".xls');");
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
                DataColumn dc = new DataColumn();
                dt.Columns.Add("Id_ClpDet", System.Type.GetType("System.Int32"));
                dt.Columns.Add("TPrecio", System.Type.GetType("System.Int32"));
                dt.Columns.Add("TPrecioStr", System.Type.GetType("System.String"));
                dt.Columns.Add("Precio", System.Type.GetType("System.String"));
                dt.Columns.Add("Pesos", System.Type.GetType("System.Double"));

                txtClave.Text = "";
                txtDescripcion.Text = "";
                chkActivo.Checked = true;

                if (txtClienteID.Text != "" && txtProductoID.Text != "")
                {
                    if (Convert.ToInt32(txtClienteID.Text) > 0 && Convert.ToInt32(txtProductoID.Text) > 0)
                    {
                        CN_CatClienteProd clsCatCliente = new CN_CatClienteProd();
                        Sesion session2 = new Sesion();
                        session2 = (Sesion)Session["Sesion" + Session.SessionID];
                        ClienteProd ClienteProddet = new ClienteProd();
                        ClienteProddet.Id_Emp = session2.Id_Emp;
                        ClienteProddet.Id_Cd = session2.Id_Cd_Ver;
                        ClienteProddet.Id_Cte = Convert.ToInt32(txtClienteID.Text);
                        ClienteProddet.Id_Prd = Convert.ToInt32(txtProductoID.Text);
                        DataTable dt2 = dt;
                        clsCatCliente.ConsultaClienteProdDet(ClienteProddet, session2.Emp_Cnx, ref dt2);
                        dt = dt2;
                        txtClave.Text = ClienteProddet.Id_Clp;// == 0 ? (int?)null : ClienteProddet.Id_Clp;
                        txtDescripcion.Text = ClienteProddet.Clp_descripcion == null ? "" : ClienteProddet.Clp_descripcion;
                        chkActivo.Checked = string.IsNullOrEmpty(ClienteProddet.Id_Clp) ? true : ClienteProddet.Estatus;
                        if (!string.IsNullOrEmpty(txtClave.Text))
                            HF_ID.Value = txtClave.Text;

                        txtUnidades.Text = ClienteProddet.Unidades;
                        txtPresentacion.Text = ClienteProddet.Clp_Presentacion;
                        txtCantFact.Value = ClienteProddet.CantFact;
                        dpUltimaVta.DbSelectedDate = ClienteProddet.Clp_FecUltVta;
                    }
                }
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