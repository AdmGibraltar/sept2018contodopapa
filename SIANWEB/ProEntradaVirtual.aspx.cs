using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Net.Mime;
using CapaDatos;
using System.IO;

namespace SIANWEB
{
    public partial class ProEntradaVirtual : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        bool moneda = false;
        bool producto = false;
        public DataTable dt2
        {
            get
            {
                return (DataTable)Session["dt2EntradaVirtual" + Session.SessionID];
            }
            set
            {
                Session["dt2EntradaVirtual" + Session.SessionID] = value;
            }
        }

        private List<EntradaVirtualDetalleMov> List_DetalleMov
        {
            get { return (List<EntradaVirtualDetalleMov>)Session["DetallemovEntradaVirtual" + Session.SessionID]; }
            set { Session["DetallemovEntradaVirtual" + Session.SessionID] = value; }
        }


        private List<EntradaVirtualDet> List_Saldo
        {
            get { return (List<EntradaVirtualDet>)Session["DetalleSaldoEntradaVirtual" + Session.SessionID]; }
            set { Session["DetalleSaldoEntradaVirtual" + Session.SessionID] = value; }
        }

        #endregion
        #region Eventos



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];



                if (sesion == null)
                {
                    CerrarVentana();
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        hiddenId.Value = Page.Request.QueryString["Id_Folio"];
                        HF_Tipo.Value = Page.Request.QueryString["Accion"];
                        hiddenGui.Value = Page.Request.QueryString["Id1"];

                        dpFecha1.SelectedDate = DateTime.Now;
                        CargarSolicitante();
                        CargarEstatus();
                        CargarEmails();
                        CargarProveedor();
                        txtNotaResp.Enabled = false;
                        if (HF_Tipo.Value == "5")
                        {

                            BtnAutorizar.Visible = true;
                            BtnRechazar.Visible = true;
                            txtNotaResp.Enabled = true;
                        }

                        Session["dt2EntradaVirtual" + Session.SessionID] = PoblarTablaPro();
                        txtSolicitud.Enabled = false;

                        if (hiddenId.Value != "-1")
                        {
                            this.LLenarVentanaEntradaVirtual(Convert.ToInt32(hiddenId.Value), Convert.ToInt32(HF_Tipo.Value));
                        }
                        else
                        {
                            Nuevo();

                            cmbSolicitante.SelectedIndex = cmbSolicitante.FindItemIndexByValue(sesion.Id_U.ToString());
                            cmbSolicitante.Text = cmbSolicitante.FindItemByValue(sesion.Id_U.ToString()).Text;
                        }

                        if (dpFecha1.SelectedDate > sesion.CalendarioFin)
                        {
                            dpFecha1.DbSelectedDate = sesion.CalendarioFin;
                        }
                        List_DetalleMov = GetListDetalleMov();
                        List_Saldo = GetListSaldo();
                        rgDetalleMov.Rebind();
                        rgDevParcial.Rebind();

                    }
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
                if (btn.CommandName == "save" && Page.IsValid)
                {
                    switch (Convert.ToInt32(HF_Tipo.Value))
                    {
                        case 1: //renovar
                            Guardar(false);
                            break;

                        case 2: //traslape
                            Guardar(false);
                            break;

                        case 3: //edición
                            Guardar(false);
                            break;

                        case 4: //nuevo
                            Guardar(false);
                            break;
                    }
                }
                else if (btn.CommandName == "new")
                {
                    Nuevo();
                }
                else if (btn.CommandName == "mail")
                {
                    Enviar();
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
        protected void txtSolicitud_OnTextChanged(object sender, EventArgs e)
        {
            ErrorManager();
        }
        protected void cmbTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ErrorManager();
            this.CambiarNomColumna();
        }



        protected void cmbProducto_DataBinding(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if (producto)
                {
                    producto = false;
                    return;
                }
                producto = true;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                RadComboBox ComboBox = sender as RadComboBox;
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spVentanaEntradaVirtual_Combo", ref ComboBox);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }




        //PRODUCTOS
        protected void rgProductos_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                switch (e.CommandName)
                {
                    case "InitInsert":
                        break;
                    case "PerformInsert":
                        this.PerformInsert_rgProductos(e);
                        break;
                    case "Edit":
                        //nada aun
                        break;
                    case "Update":
                        this.Update_rgProductos(e);
                        break;
                    case "Delete":
                        this.Delete_rgProductos(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgProductos_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    if (dt2 != null)
                    {
                        rgProductos.DataSource = dt2;
                    }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }



        protected void rgDetalleMov_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgDetalleMov.DataSource = List_DetalleMov;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void rgDevParcial_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgDevParcial.DataSource = List_Saldo;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void CargarProveedor()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spProveedores_Combo", ref cmbProveedor);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void txtProducto_TextChanged(object sender, EventArgs e)//protected void cmbProducto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                RadNumericTextBox combo = (RadNumericTextBox)sender;
                Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;


                RadNumericTextBox Env_Cantidad = (RadNumericTextBox)tabla.FindControl("txtCantidad");
                RadNumericTextBox Env_Costo = (RadNumericTextBox)tabla.FindControl("txtCosto");
                RadNumericTextBox Env_PreVta = (RadNumericTextBox)tabla.FindControl("txtPreVta");

                Env_PreVta.Value = Env_PreVta.Value.ToString() == "" ? 0 : Env_PreVta.Value;
                Env_Costo.Value = Env_Costo.Value.ToString() == "" ? 0 : Env_Costo.Value;


                double Env_importe = Convert.ToDouble(Env_PreVta.Text) * Convert.ToDouble(Env_Cantidad.Value.ToString());
                double Env_ImporteCosto = (Convert.ToDouble(Env_Costo.Text) * Convert.ToDouble(Env_Cantidad.Text));

                double uBruta = 0;


                double Env_UBruta = Math.Round(((Env_importe - Env_ImporteCosto) / Env_importe) * 100, 2);

                ((RadNumericTextBox)tabla.FindControl("txtEnvImporte")).Value = Env_importe;
                ((RadNumericTextBox)tabla.FindControl("txtUBruta")).Value = Env_UBruta;



            }
            catch (Exception ex)
            {
                this.Alerta(string.Concat(ex.Message, "cmbProducto_IndexChanging_error"));
            }
        }




        protected void rgProductos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();

                if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    //Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditFormItem editItem = (GridEditFormItem)e.Item;
                    RadComboBox CmbPrd = (RadComboBox)editItem["Prd_Descripcion"].FindControl("CmbPrd");
                    RadNumericTextBox txtClave = (RadNumericTextBox)editItem["Prd_Descripcion"].FindControl("txtClave");


                    if (txtClave.Text != "")
                    {
                        CmbPrd.Enabled = false;
                        txtClave.Enabled = false;
                        CmbPrd.SelectedValue = txtClave.Text;


                    }
                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        RadNumericTextBox txtId_Prd = (RadNumericTextBox)editItem.FindControl("txtClave");
                        txtId_Prd.Enabled = false;
                    }


                }
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    GridEditableItem editItem = (GridEditableItem)e.Item;
                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        RadNumericTextBox txtId_Prd = (RadNumericTextBox)editItem.FindControl("txtClave");
                        txtId_Prd.Enabled = false;

                        RadTextBox txtProductoNombre = (RadTextBox)editItem.FindControl("txtProductoNombre");
                        txtProductoNombre.Enabled = false;


                        if ((Convert.ToInt32(HF_Tipo.Value) == 3 && cmbEstatus.SelectedValue.ToLower() == "s"))
                        {


                            RadNumericTextBox txtPreVta = (RadNumericTextBox)editItem.FindControl("txtPreVta");
                            txtPreVta.Enabled = false;


                            RadNumericTextBox txtCantidad = (RadNumericTextBox)editItem.FindControl("txtCantidad");
                            txtCantidad.Enabled = false;


                        }


                    }
                }


                //TODO: AGREGAR PARA PONER EL FOCUS
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditableItem form = (GridEditableItem)e.Item;
                    RadNumericTextBox dataField = (RadNumericTextBox)form["Id_Prd"].FindControl("txtClave");

                    dataField.Focus();


                }
                //-----------------------------------------
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }




        protected void cmbProductoDet_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Producto prd = new Producto();
                CN_CatProducto cnProducto = new CN_CatProducto();
                RadNumericTextBox txtProducto = (RadNumericTextBox)sender;

                try
                {
                    cnProducto.ConsultaProducto(ref prd, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32((sender as RadNumericTextBox).Value.HasValue ? (sender as RadNumericTextBox).Value : -1));
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtProducto.ClientID);
                    return;
                }
                ((sender as RadNumericTextBox).Parent.Parent.FindControl("txtProductoNombre") as RadTextBox).Text = prd.Prd_Descripcion;
                ((sender as RadNumericTextBox).Parent.Parent.FindControl("txtCosto") as RadNumericTextBox).Text = prd.Prd_AAA.ToString();



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
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //Consultar clientes
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Cte = Convert.ToInt32((sender as RadNumericTextBox).Value.HasValue ? (sender as RadNumericTextBox).Value.Value : -1);
                cliente.Id_Rik = sesion.Id_Rik;
                try
                {
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                    ((sender as RadNumericTextBox).Parent.Parent.FindControl("txtClienteNombre") as RadTextBox).Text = cliente.Cte_NomComercial;
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, (sender as RadNumericTextBox).ClientID);
                    (sender as RadNumericTextBox).Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void Ini_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];


                RadDatePicker dpIni = sender as RadDatePicker;
                RadDatePicker dpFin = (dpIni.Parent.FindControl("rdp_FecFin") as RadDatePicker);
                RadNumericTextBox txtPreEsp = (dpFin.Parent.FindControl("txtPreEsp") as RadNumericTextBox);

                if (dpIni.SelectedDate.HasValue && dpFin.SelectedDate.HasValue)
                {
                    if (dpIni.SelectedDate > dpFin.SelectedDate)
                    {
                        AlertaFocus("La fecha de fin debe ser mayor a la fecha de inicio", dpFin.DateInput.ClientID);
                        dpFin.DbSelectedDate = null;
                        return;
                    }
                    else
                    {
                        txtPreEsp.Focus();
                    }
                }
                else if (dpIni.SelectedDate.HasValue)
                {
                    if (dpIni.SelectedDate < sesion.CalendarioIni)
                    {
                        AlertaFocus("La fecha de inicio debe ser mayor o igual a la fecha de inicio del periodo actual", dpIni.DateInput.ClientID);
                        dpIni.DbSelectedDate = null;
                        return;
                    }
                    else
                    {
                        dpFin.DateInput.Focus();
                    }
                }
                else
                {
                    dpIni.DateInput.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void fin_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                RadDatePicker dpFin = sender as RadDatePicker;
                RadDatePicker dpIni = (dpFin.Parent.FindControl("rdp_FecIni") as RadDatePicker);

                if (dpIni.SelectedDate.HasValue && dpFin.SelectedDate.HasValue)
                {
                    if (dpIni.SelectedDate > dpFin.SelectedDate)
                    {
                        AlertaFocus("La fecha de fin debe ser mayor a la fecha de inicio", dpFin.DateInput.ClientID);
                        dpFin.DbSelectedDate = null;
                        return;
                    }
                    else
                    {
                        ((Telerik.Web.UI.GridDataItem)(dpFin.Parent.Parent))["EditCommandColumn"].Controls[0].Focus();
                    }
                }
                else if (dpIni.SelectedDate.HasValue)
                {
                    dpFin.DateInput.Focus();
                }
                else
                {
                    dpIni.DateInput.Focus();
                }
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
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value));

                        RadPane2.Height = altura;
                        RadSplitter2.Height = altura;
                        rgProductos.Height = (Unit)(Convert.ToDouble(HiddenHeight.Value));
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
        private void CerrarVentana()
        {
            try
            {
                string funcion;
                if (this.HiddenRebind.Value == "0")
                    funcion = "CloseWindow()";
                else
                    funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private void CerrarVentana(string str)
        {
            try
            {
                RAM1.ResponseScripts.Add("alertClose('" + str + "')");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CambiarNomColumna()
        {

            rgProductos.Columns[2].HeaderText = "";
            rgProductos.Rebind();

        }
        private string Guardar(bool enviar)
        {

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if (dt2.Rows.Count < 1)
            {
                Alerta("La solicitud debe llevar como mínimo un producto");
                return "";
            }

            //JFCV 14 Abr 2016 Agregue validación para obligar a que elija un proveedor porque marcaba error 
            if (Int32.Parse(this.cmbProveedor.SelectedValue) < 1)
            {
                Alerta("La solicitud debe tener un proveedor");
                return "";
            }

            string EditType = "";
            if (HF_Tipo.Value == "3")
            {
                if (!_PermisoModificar)
                {
                    this.Alerta("No tiene permisos para modificar");
                    return "";
                }
                EditType = "update";
            }
            else
            {
                if (!_PermisoGuardar)
                {
                    this.Alerta("No tiene permisos para grabar");
                    return "";
                }
                EditType = "insert";
            }

            try
            {

                string filename;
                string path;



                int id = Convert.ToInt32(txtSolicitud.Text);
                EntradaVirtual PEspecial = this.LlenarEntradaVirtual(id);
                PEspecial.Env_Solicitar = 1;


                List<EntradaVirtualDet> list2 = new List<EntradaVirtualDet>();
                int keycounter1 = 0;
                int keycounter2 = 0;


                foreach (DataRow row in dt2.Rows)
                {
                    EntradaVirtualDet vpep = new EntradaVirtualDet();
                    vpep.Id_Emp = sesion.Id_Emp;
                    vpep.Id_Cd = Convert.ToInt32(row["Id_Cd"]);
                    vpep.Id_Env = id;
                    vpep.Id_Prd = Convert.ToInt32(row["Id_Prd"]);
                    vpep.Env_Cantidad = Convert.ToInt32(row["Env_Cantidad"]);
                    vpep.Env_Costo = Convert.ToDecimal(row["Env_Costo"]);
                    vpep.Env_PreVta = Convert.ToDecimal(row["Env_PreVta"]);

                    list2.Add(vpep);
                    keycounter2++;
                }


                PEspecial.ListVentanaEntradaVirtualDet = list2;


                CN_EntradaVirtual clsEntradaVirtual = new CN_EntradaVirtual();
                string verificadorstr = "";

                if (EditType == "insert")
                {
                    clsEntradaVirtual.InsertarVentanaEntradaVirtual(PEspecial, sesion.Emp_Cnx, ref verificadorstr);

                    if (verificadorstr != "")
                    {
                        //Enviar();
                        if (!enviar)
                        {
                            CerrarVentana("La solicitud <b>#" + txtSolicitud.Text + "</b> se han guardado correctamente");
                            return "";
                        }
                        else
                            return verificadorstr;
                    }
                    else
                    {
                        this.Alerta("Hubo un problema al insertar los datos");
                        return "";
                    }
                }
                if (EditType == "update")
                {
                    clsEntradaVirtual.ModificarVentanaEntradaVirtual(PEspecial, sesion.Emp_Cnx, ref verificadorstr);
                    if (verificadorstr != "")
                    {
                        if (!enviar)
                        {
                            CerrarVentana("Los datos se han guardado correctamente");
                            return "";
                        }
                        else
                            return verificadorstr;
                    }
                    else
                    {
                        Alerta("Ocurrió un error al intentar modificar los datos");
                        return "";
                    }
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Enviar()
        {
            string GUID = Guardar(true);
            if (GUID.Length > 0)
            {
                if (EnviaEmail(GUID))
                {
                    //CerrarVentana();
                }
            }
        }
        private void Nuevo()
        {
            try
            {
                txtSolicitud.Text = MaximoId("ProEntradaVirtual", "Id_Env");




                txtNota.Text = "";

                Session["dt" + Session.SessionID] = null;
                Session["dt2EntradaVirtual" + Session.SessionID] = null;

                Session["dt2EntradaVirtual" + Session.SessionID] = PoblarTablaPro();


                rgProductos.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                rgProductos.Columns.FindByUniqueName("EditCommandColumn").Display = true; //11
                rgProductos.Columns.FindByUniqueName("DeleteColumn").Display = true; //12


                rgProductos.DataSource = Session["dt2EntradaVirtual" + Session.SessionID];

                rgProductos.Rebind();


                cmbSolicitud.Enabled = true;
                cmbSolicitud.SelectedIndex = 0;
                cmbSolicitud.Text = cmbSolicitud.Items[0].Text;
                txtNota.Enabled = true;
                //cmbProveedor.Enabled = true;
                // txtNumConvenio.Enabled = true;

                rtb1.Items[2].Visible = true;
                rtb1.Items[1].Visible = true;

                HF_Tipo.Value = "4";
                cmbEstatus.SelectedIndex = cmbEstatus.FindItemIndexByValue("C");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private EntradaVirtual LlenarEntradaVirtual(int id)
        {
            CapaDatos.Funciones funciones = new CapaDatos.Funciones();
            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            EntradaVirtual ape = new EntradaVirtual();
            ape.Id_USolicita = session.Id_U;
            ape.Id_Emp = session.Id_Emp;
            ape.Id_Cd = session.Id_Cd_Ver;
            ape.Id_Env = id;
            ape.Env_Fecha = DateTime.Now;
            ape.Id_UAutorizo = Int32.Parse(this.cmbSolicitud.SelectedValue);

            if (HF_Tipo.Value == "1" || HF_Tipo.Value == "2")
            {
                ape.Env_Estatus = "C";
            }
            else
            {
                ape.Env_Estatus = cmbEstatus.SelectedValue;
            }


            ape.Env_Credito = Convert.ToInt32(txtCredito.Text);
            ape.Env_ImporteFacturar = Convert.ToDecimal(txtImporteFactura.Text);
            ape.Id_Cte = Int32.Parse(txtNumCliente.Text);
            ape.Env_Rentabilidad = Convert.ToDecimal(txtImporteUb.Text);
            ape.Env_ComentariosSolicitante = txtNota.Text;
            ape.Env_CteNomComercial = txtCliente.Text;
            ape.Id_Pvd = Int32.Parse(this.cmbProveedor.SelectedValue);


            return ape;
        }

        private DataTable PoblarTablaPro()
        {
            try
            { //poblar la tabla virtual con columnas vacias:
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("Id_Emp", System.Type.GetType("System.Int32"));
                dt2.Columns.Add("Id_Cd", System.Type.GetType("System.Int32"));
                dt2.Columns.Add("Id_Env", System.Type.GetType("System.Int32"));
                dt2.Columns.Add("Id_Prd", System.Type.GetType("System.Int32"));
                dt2.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
                dt2.Columns.Add("Env_Cantidad", System.Type.GetType("System.Int32"));
                dt2.Columns.Add("Env_Costo", System.Type.GetType("System.Double"));
                dt2.Columns.Add("Env_PreVta", System.Type.GetType("System.Double"));
                dt2.Columns.Add("Env_Importe", System.Type.GetType("System.Double"));
                dt2.Columns.Add("Env_uBruta", System.Type.GetType("System.Double"));

                return dt2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private DataTable PoblarTablaDetalleMov()
        {
            try
            { //poblar la tabla virtual con columnas vacias:
                DataTable dtDetalleMov = new DataTable();
                dtDetalleMov.Columns.Add("Id_Emp", System.Type.GetType("System.Int32"));
                dtDetalleMov.Columns.Add("Id_Cd", System.Type.GetType("System.Int32"));
                dtDetalleMov.Columns.Add("Id_Env", System.Type.GetType("System.Int32"));
                dtDetalleMov.Columns.Add("Id_Prd", System.Type.GetType("System.Int32"));
                dtDetalleMov.Columns.Add("Id_Es", System.Type.GetType("System.String"));
                dtDetalleMov.Columns.Add("Id_Tm", System.Type.GetType("System.Int32"));
                dtDetalleMov.Columns.Add("Fecha", System.Type.GetType("System.Double"));
                dtDetalleMov.Columns.Add("Cant", System.Type.GetType("System.Double"));
                dtDetalleMov.Columns.Add("Tipo", System.Type.GetType("System.Double"));


                return dtDetalleMov;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }







        private List<EntradaVirtualDetalleMov> GetListDetalleMov()
        {
            try
            {
                List<EntradaVirtualDetalleMov> List = new List<EntradaVirtualDetalleMov>();
                CN_EntradaVirtual clsCapEntradaVirtual = new CN_EntradaVirtual();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                EntradaVirtual Ev = new EntradaVirtual();
                Ev.Id_Emp = sesion.Id_Emp;
                Ev.Id_Cd = sesion.Id_Cd_Ver;
                Ev.Id_Env = int.Parse(hiddenId.Value.ToString());//(int)txtSolicitud.Value.Value;

                clsCapEntradaVirtual.ConsultaEntradaVirtualDetallemov(Ev, sesion.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private List<EntradaVirtualDet> GetListSaldo()
        {
            try
            {
                List<EntradaVirtualDet> List = new List<EntradaVirtualDet>();
                CN_EntradaVirtual clsCapEntradaVirtual = new CN_EntradaVirtual();
                EntradaVirtual Ev = new EntradaVirtual();
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Ev.Id_Emp = Sesion.Id_Emp;
                Ev.Id_Cd = Sesion.Id_Cd_Ver;
                Ev.Id_Env = int.Parse(hiddenId.Value);  //(int)txtSolicitud.Value.Value;

                clsCapEntradaVirtual.ConsultaEntradaVirtualSaldo(Ev, Sesion.Emp_Cnx, ref List);
                if (List.Count == 0)
                {
                    BtnDevolucion.Enabled = false;

                }
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
                _PermisoGuardar = Convert.ToBoolean(Page.Request.QueryString["PermisoGuardar"]);
                _PermisoModificar = Convert.ToBoolean(Page.Request.QueryString["PermisoModificar"]);
                _PermisoEliminar = Convert.ToBoolean(Page.Request.QueryString["PermisoEliminar"]);
                _PermisoImprimir = Convert.ToBoolean(Page.Request.QueryString["PermisoImprimir"]);

                if (_PermisoGuardar == false)
                    this.rtb1.Items[3].Visible = false;

                if (_PermisoGuardar == false & _PermisoModificar == false)
                    this.rtb1.Items[2].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        private void CargarEstatus()
        {
            cmbEstatus.Items.Add(new RadComboBoxItem("Pendiente por solicitar", "C"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Solicitado", "S"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Autorizado", "A")); //estos mismos valores se usan en pantalla de facturación de santiago
            cmbEstatus.Items.Add(new RadComboBoxItem("Rechazado", "R"));
            cmbEstatus.Items.Add(new RadComboBoxItem("Parcialmente autorizada", "P"));
            //cmbEstatus.Items.Add(new RadComboBoxItem("Vencido", "V")); //???
        }
        private void CargarEmails()
        {
            try
            {
                //cargar emails por pipes:
                string pipelist = "";

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN_EntradaVirtual CNEntradaVirtual = new CN_EntradaVirtual();
                CNEntradaVirtual.ConsultarEmailsPt1(sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, ref pipelist);

                string[] array1 = pipelist.Split(new char[] { ',' });


                DataTable dt_emails = new DataTable();
                dt_emails.Columns.Add("Id", System.Type.GetType("System.String"));
                dt_emails.Columns.Add("Email", System.Type.GetType("System.String"));
                dt_emails.Columns.Add("Nombre", System.Type.GetType("System.String"));
                dt_emails.Rows.Add("-1", "", "-- Seleccionar --");

                foreach (string email in array1)
                {
                    string nombre = "";
                    int? id = null;
                    CNEntradaVirtual.ConsultarEmailsPt2(sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, email, ref nombre, ref id);
                    dt_emails.Rows.Add(id, email, nombre);
                }

                cmbSolicitud.DataSource = dt_emails;
                cmbSolicitud.DataBind();

                //foreach (DataRow row in dt_emails.Rows)
                //{
                //    cmbSolicitud.Items.Add(new RadComboBoxItem(row["nombre"].ToString(), row["email"].ToString()));
                //}
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string MaximoId(string nomTabla, string nomColumna)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, nomTabla, nomColumna, sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                RAM1.ResponseScripts.Add("UpExcel()");



            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ImageButton1_Click");
            }
        }


        protected void BtnAutorizar_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                BtnAutorizar.Enabled = false;
                BtnRechazar.Enabled = false;

                CN_CatCentroDistribucion cd = new CN_CatCentroDistribucion();
                CentroDistribucion centroDistribucion = new CentroDistribucion();
                double icd = 0;
                cd.ConsultarIva(sesion.Id_Emp, sesion.Id_Cd_Ver, ref icd, sesion.Emp_Cnx);

                CN_EntradaVirtual Ev = new CN_EntradaVirtual();
                EntradaVirtual Evirtual = new EntradaVirtual();



                string mensaje = string.Empty;

                int verificador = 0;


                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }

                CN_CapEntradaSalida cn_capEntradaSalida = new CN_CapEntradaSalida();

                EntradaSalida entsal = new EntradaSalida();
                entsal.Id_Emp = sesion.Id_Emp;
                entsal.Id_Cd = sesion.Id_Cd_Ver;
                entsal.Id_U = Int32.Parse(cmbSolicitud.SelectedValue);
                entsal.Id_Es = Int32.Parse(MaximoId("CapEntsal", "Id_Es"));
                entsal.Es_Naturaleza = 0;
                entsal.Es_Fecha = DateTime.Now;
                entsal.Id_Tm = 21;
                entsal.Id_Pvd = Convert.ToInt32(txtProveedorId.Value.HasValue ? txtProveedorId.Value.Value : -1);
                if (dpFecha1.SelectedDate.HasValue)
                {
                    entsal.Es_FechaReferencia = Convert.ToDateTime(dpFecha1.SelectedDate);
                }

                // De acuerdo al tipo de mov se toma de un combo u otro el valor

                entsal.Es_Referencia = txtSolicitud.Text;
                entsal.Es_Notas = "Movimiento por proceso de autorización de entrada virtual, número de solicitud:" + txtSolicitud.Text;

                entsal.Es_Estatus = "I";
                entsal.ManAut = true;
                CN_CatCliente cn_catCliente = new CN_CatCliente();

                List<Territorios> lTerritios = new List<Territorios>();

                cn_catCliente.ConsultaTerritoriosDelCliente(entsal.Id_Cte, sesion, ref lTerritios);


                entsal.Id_Ter = (lTerritios.FindLast(a => a.Id_Cd == 0).Id_Ter != null) ? lTerritios.FindLast(a => a.Id_Cd == 0).Id_Ter : -1;
                entsal.Es_CteCuentaNacional = -1;
                entsal.Es_CteCuentaContNacional = 0;
                entsal.Id_Cte = -1;
                List<EntradaSalidaDetalle> listaDetalle = new List<EntradaSalidaDetalle>();

                double TotalEs = 0;

                foreach (DataRow row in dt2.Rows)
                {
                    EntradaSalidaDetalle vpep = new EntradaSalidaDetalle();
                    vpep.Id_Emp = sesion.Id_Emp;
                    vpep.Id_Cd = Convert.ToInt32(row["Id_Cd"]);
                    vpep.Id_Es = entsal.Id_Es;
                    vpep.Id_Prd = Convert.ToInt32(row["Id_Prd"]);
                    vpep.Es_Cantidad = Convert.ToInt32(row["Env_Cantidad"]);
                    vpep.Es_Costo = double.Parse(row["Env_Costo"].ToString());
                    TotalEs = TotalEs + vpep.Es_Costo;
                    vpep.EsDet_Naturaleza = 0;
                    listaDetalle.Add(vpep);
                }
                entsal.Es_SubTotal = TotalEs;
                entsal.Es_Iva = TotalEs * (icd / 100);
                entsal.Es_Total = TotalEs + entsal.Es_Iva;

                int strEmp = sesion.Id_Emp;
                int Id_Env = Int32.Parse(txtSolicitud.Text);

                string verificadorStr = "";
                if (Request.QueryString["id"] == "-1" || Request.QueryString["id"] == null)
                {
                    Ev.GuardarEntradaSalida(entsal, listaDetalle, ref verificadorStr, strEmp, sesion.Emp_Cnx, Id_Env);
                }


                verificador = CambiarEstatus("A");

                if (verificador == 1)
                {
                    CerrarVentana("La solicitud de Autorización ha sido atendida");
                    EnviaEmailAtencion();

                }
                else
                {
                    Alerta("Ocurrió un error al intentar atender la solicitud #" + hiddenId.Value);
                }

            }
            catch (Exception ex)
            {
                BtnAutorizar.Enabled = true;
                BtnRechazar.Enabled = true;
                ErrorManager(ex, "BtnAutorizar_Click");
            }
        }



        protected void BtnRechazar_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                BtnAutorizar.Enabled = false;
                BtnRechazar.Enabled = false;

                string mensaje = string.Empty;

                int verificador = 0;


                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }


                verificador = CambiarEstatus("R");

                if (verificador == 1)
                {
                    Alerta("La solicitud de Autorización ha sido atendida");
                    EnviaEmailAtencion();

                }
                else
                {
                    Alerta("Ocurrió un error al intentar atender la solicitud #" + hiddenId.Value);
                }

            }
            catch (Exception ex)
            {
                BtnAutorizar.Enabled = false;
                BtnRechazar.Enabled = false;
                ErrorManager(ex, "BtnAutorizar_Click");
            }
        }




        protected void BtnDevolucion_Click(object sender, EventArgs e)
        {
            try
            {
                BtnDevolucion.Enabled = false;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];


                CN_CatCentroDistribucion cd = new CN_CatCentroDistribucion();
                CentroDistribucion centroDistribucion = new CentroDistribucion();
                double icd = 0;
                cd.ConsultarIva(sesion.Id_Emp, sesion.Id_Cd_Ver, ref icd, sesion.Emp_Cnx);

                CN_EntradaVirtual Ev = new CN_EntradaVirtual();
                EntradaVirtual Evirtual = new EntradaVirtual();

                int verificador = 0;
                Evirtual.Id_Emp = sesion.Id_Emp;
                Evirtual.Id_Cd = sesion.Id_Cd_Ver;
                Evirtual.Id_Env = Int32.Parse(txtSolicitud.Text);


                Ev.ConsultaProAutEntradaVirtual(ref Evirtual, sesion.Emp_Cnx, ref verificador);

                string mensaje = string.Empty;



                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }

                CN_CapEntradaSalida cn_capEntradaSalida = new CN_CapEntradaSalida();

                EntradaSalida entsal = new EntradaSalida();
                entsal.Id_Emp = sesion.Id_Emp;
                entsal.Id_Cd = sesion.Id_Cd_Ver;
                entsal.Id_U = Int32.Parse(cmbSolicitud.SelectedValue);
                entsal.Id_Es = Int32.Parse(MaximoId("CapEntsal", "Id_Es"));
                entsal.Es_Naturaleza = 1;
                entsal.Es_Fecha = DateTime.Now;
                entsal.Id_Tm = 81;
                entsal.Id_Pvd = Convert.ToInt32(txtProveedorId.Value.HasValue ? txtProveedorId.Value.Value : -1);


                // De acuerdo al tipo de mov se toma de un combo u otro el valor

                entsal.Es_Referencia = Evirtual.Id_Es.ToString();
                entsal.Es_Notas = "Movimiento por proceso de autorización de entrada virtual, número de solicitud:" + txtSolicitud.Text;

                entsal.Es_Estatus = "I";
                entsal.ManAut = true;
                CN_CatCliente cn_catCliente = new CN_CatCliente();

                List<Territorios> lTerritios = new List<Territorios>();

                cn_catCliente.ConsultaTerritoriosDelCliente(entsal.Id_Cte, sesion, ref lTerritios);


                entsal.Id_Ter = (lTerritios.FindLast(a => a.Id_Cd == 0).Id_Ter != null) ? lTerritios.FindLast(a => a.Id_Cd == 0).Id_Ter : -1;
                entsal.Es_CteCuentaNacional = -1;
                entsal.Es_CteCuentaContNacional = 0;
                entsal.Id_Cte = -1;
                List<EntradaSalidaDetalle> listaDetalle = new List<EntradaSalidaDetalle>();

                double TotalEs = 0;

                bool CantMayor = false;
                string Cantidad;

                foreach (GridDataItem item in rgDevParcial.MasterTableView.Items)
                {
                    EntradaSalidaDetalle vpep = new EntradaSalidaDetalle();
                    vpep.Id_Emp = sesion.Id_Emp;
                    vpep.Id_Cd = sesion.Id_Cd_Ver;
                    vpep.Id_Es = entsal.Id_Es;
                    vpep.Id_Prd = Convert.ToInt32(item["Id_Prd"].Text);
                    Cantidad = ((RadNumericTextBox)item.FindControl("NumCantDevuelta")).Text;
                    vpep.Es_Cantidad = string.IsNullOrEmpty(Cantidad) ? 0 : Convert.ToInt32(Cantidad);
                    vpep.Es_Costo = double.Parse(item["Env_Costo"].Text);
                    //JFCV 14 Abr 2016 agregar validación para que si el costo es cero , que no realice la devolución.
                    vpep.Prd_Descripcion = Convert.ToString(item["Prd_Descripcion"].Text);
                    //FIN

                    if (vpep.Es_Cantidad > int.Parse(item["Env_Cantidad"].Text))
                    {
                        CantMayor = true;
                        Alerta("La cantidad a Devolver no puede ser mayor al saldo de la Entrada");
                        return;

                    }
                    //JFCV 14 Abr 2016 agregar validación para que si el costo es cero , que no realice la devolución.
                    if (vpep.Es_Costo <= 0)
                    {

                        Alerta("El costo del producto: " + vpep.Id_Prd + " - " + vpep.Prd_Descripcion + ", es cero. Actualicelo para poder realizar la devolución.");
                        return;

                    }
                    //FIN


                    vpep.EsDet_Naturaleza = 1;
                    if (vpep.Es_Cantidad != 0)
                    {
                        TotalEs = TotalEs + (vpep.Es_Costo * vpep.Es_Cantidad);
                        listaDetalle.Add(vpep);
                    }
                }

                if (!CantMayor)
                {
                    entsal.Es_SubTotal = TotalEs;
                    entsal.Es_Iva = TotalEs * (icd / 100);
                    entsal.Es_Total = TotalEs + entsal.Es_Iva;

                    int strEmp = sesion.Id_Emp;
                    int Id_Env = Int32.Parse(txtSolicitud.Text);

                    string verificadorStr = "";
                    if (Request.QueryString["id"] == "-1" || Request.QueryString["id"] == null)
                    {
                        Ev.GuardarEntradaSalida(entsal, listaDetalle, ref verificadorStr, strEmp, sesion.Emp_Cnx, Id_Env);
                    }

                    List_DetalleMov = GetListDetalleMov();
                    List_Saldo = GetListSaldo();
                    rgDetalleMov.Rebind();
                    rgDevParcial.Rebind();
                    Alerta("El devolución ha sido Creada");
                    BtnDevolucion.Enabled = true;
                }


                //  EnviaEmailAtencion();


            }
            catch (Exception ex)
            {
                BtnDevolucion.Enabled = false;
                ErrorManager(ex, "BtnAutorizar_Click");
            }
        }







        private void EnviaEmailAtencion()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_Configuracion cn_configuracion = new CN_Configuracion();
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Emp = sesion.Id_Emp;
                configuracion.Id_Cd = sesion.Id_Cd_Ver;

                cn_configuracion.Consulta(ref configuracion, sesion.Emp_Cnx);



                CN_EntradaVirtual Ev = new CN_EntradaVirtual();
                EntradaVirtual Evirtual = new EntradaVirtual();

                int verificador = 0;
                Evirtual.Id_Emp = sesion.Id_Emp;
                Evirtual.Id_Cd = sesion.Id_Cd_Ver;
                Evirtual.Id_Env = Int32.Parse(txtSolicitud.Text);


                Ev.ConsultaProAutEntradaVirtual(ref Evirtual, sesion.Emp_Cnx, ref verificador);

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face= 'Tahoma' size = '2'>");
                if (Evirtual.Env_Estatus.Trim() == "R")
                {
                    cuerpo_correo.Append("La solicitud #" + txtSolicitud.Text + " ha sido Rechazada.");
                }
                else
                {
                    cuerpo_correo.Append("La solicitud #" + txtSolicitud.Text + " ha sido Autorizada con el folio de Entrada #" + Evirtual.Id_Es);
                }
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");

                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<center><br><a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "ProEntradaVirtual_Admin.aspx'>Solicitud de autorización de Entradas Virtuales</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                m.To.Add(new MailAddress(ConsultarEmail(Convert.ToInt32(cmbSolicitante.SelectedValue))));
                m.Subject = "Confirmación de autorización de entradas virtuales";
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
                sm.Send(m);
            }
            catch (Exception)
            {
                Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                //Throw ex

            }
        }


        private void LLenarVentanaEntradaVirtual(int Id_Folio, int tipoAccion)
        {
            try
            {

                dt2.Rows.Clear();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                EntradaVirtual v = null;

                new CN_EntradaVirtual().ConsultaVentanaEntradaVirtual(ref v, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Folio);

                string Estatus = v.Env_Estatus.Trim();
                if (Estatus != "C")
                {
                    this.rtb1.Items[1].Visible = false;
                    this.rtb1.Items[2].Visible = false;
                }

                if (Estatus == "A" || Estatus == "R")
                {

                    BtnAutorizar.Enabled = false;
                    BtnRechazar.Enabled = false;
                }
                txtNotaResp.Enabled = false;

                if (Estatus == "A")
                {
                    BtnDevolucion.Visible = true;
                    RadTabStrip1.Tabs[1].Enabled = true;
                    RadTabStrip1.Tabs[2].Enabled = true;
                }


                dpFecha1.SelectedDate = v.Env_Fecha;
                cmbEstatus.SelectedValue = v.Env_Estatus.Trim();

                txtCredito.Text = v.Env_Credito.ToString();
                txtImporteUb.Text = v.Env_Rentabilidad.ToString();
                txtImporteFactura.DbValue = v.Env_ImporteFacturar.ToString();
                txtNumCliente.Text = v.Id_Cte.ToString();
                txtCliente.Text = v.Env_CteNomComercial;
                txtProveedorId.Text = v.Id_Pvd.ToString();


                try
                {
                    cmbSolicitud.SelectedIndex = cmbSolicitud.FindItemIndexByValue(v.Id_UAutorizo.ToString());
                    cmbSolicitud.Text = cmbSolicitud.FindItemByValue(v.Id_UAutorizo.ToString()).Text;
                    cmbSolicitante.SelectedIndex = cmbSolicitante.FindItemIndexByValue(v.Id_USolicita.ToString());
                    cmbSolicitante.Text = cmbSolicitante.FindItemByValue(v.Id_USolicita.ToString()).Text;
                    cmbProveedor.SelectedIndex = cmbProveedor.FindItemIndexByValue(v.Id_Pvd.ToString());
                    cmbProveedor.Text = cmbProveedor.FindItemByValue(v.Id_Pvd.ToString()).Text;
                }
                catch
                {
                }

                txtNota.Text = v.Env_ComentariosSolicitante;
                txtNotaResp.Text = v.Env_ComentariosAutoriza;



                List<EntradaVirtualDet> lista2 = GetListPro(Id_Folio);
                foreach (EntradaVirtualDet objPro in lista2)
                {
                    DataRow row = dt2.NewRow();
                    row["Id_Emp"] = objPro.Id_Emp;
                    row["Id_Cd"] = objPro.Id_Cd;
                    row["Id_Env"] = objPro.Id_Env;
                    row["Id_Prd"] = objPro.Id_Prd;
                    row["Prd_Descripcion"] = objPro.Prd_Descripcion;
                    row["Env_Cantidad"] = objPro.Env_Cantidad;
                    row["Env_PreVta"] = objPro.Env_PreVta;
                    row["Env_Costo"] = objPro.Env_Costo;
                    row["Env_Importe"] = objPro.Env_PreVta * objPro.Env_Cantidad;
                    decimal UBruta = 0;
                    UBruta = (objPro.Env_PreVta * objPro.Env_Cantidad) - (objPro.Env_Costo * objPro.Env_Cantidad);
                    row["Env_uBruta"] = Math.Round((UBruta / (objPro.Env_PreVta * objPro.Env_Cantidad)) * 100, 2);
                    dt2.Rows.Add(row);
                }


                bool validadorAdmin = false;
                foreach (RadComboBoxItem item in cmbSolicitud.Items)
                {
                    int valor = 0;
                    Int32.TryParse(item.Value, out valor);
                    if (sesion.Id_U == valor)
                        validadorAdmin = true;
                }

                if (tipoAccion == 1 || tipoAccion == 2 || validadorAdmin)
                {

                    if (validadorAdmin)
                    {
                        txtSolicitud.Text = Id_Folio.ToString();
                    }
                    else
                    {
                        txtSolicitud.Text = MaximoId("ProEntradaVirtual", "Id_Env");
                    }
                    //cmbProveedor.Enabled = false;
                    //txtNumConvenio.Enabled = false;
                    /* rgCliente.Columns[3].Display = false;
                     rgCliente.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                     rgProductos.Columns.FindByUniqueName("DeleteColumn").Display = false;
                     rgProductos.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;       */
                }
                else if (tipoAccion == 3)
                {
                    if (cmbEstatus.SelectedValue.ToLower() == "s" || cmbEstatus.SelectedValue.ToLower() == "a" || cmbEstatus.SelectedValue.ToLower() == "p")
                    {


                        //cmbProveedor.Enabled = false;
                        //txtNumConvenio.Enabled = false;


                        rgProductos.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                        rgProductos.Columns.FindByUniqueName("DeleteColumn").Display = false;

                        if (cmbEstatus.SelectedValue.ToLower() == "a" || cmbEstatus.SelectedValue.ToLower() == "p")
                        {
                            rgProductos.Columns.FindByUniqueName("EditCommandColumn").Display = false;
                            txtNota.Enabled = false;
                            cmbSolicitud.Enabled = false;
                            rtb1.Items[2].Visible = false;
                            rtb1.Items[1].Visible = false;

                        }
                    }
                    txtSolicitud.Text = Id_Folio.ToString();
                }
                else if (tipoAccion == 4)
                {
                    Nuevo();
                    txtSolicitud.Text = Id_Folio.ToString();
                }
                //JFCV 12 abr 2016 que muestre el id de la solicitud cuando estoy autorizando.

                else if (tipoAccion == 5)
                {
                    txtSolicitud.Text = Id_Folio.ToString();
                }

                //JFCV 15 Abr 2016 cuando viene de el mail de autorización si no coincide el GUI no  se activa botón autorizar
                if (tipoAccion == 5)
                {
                    //if (v.Env_Unique != hiddenGui.Value && hiddenGui.Value != "")
                    //{
                    //    BtnAutorizar.Enabled = false;
                    //    BtnRechazar.Enabled = false;
                    //}
                    if (v.Env_Unique.Trim() != hiddenGui.Value.Trim())
                    {
                        BtnAutorizar.Enabled = false;
                        BtnRechazar.Enabled = false;
                        txtNotaResp.Text = txtNotaResp.Text + " .";
                    }
                    else
                    {
                        txtNotaResp.Text = txtNotaResp.Text + " ,";
                    }
                }

                txtNotaResp.Enabled = false;

                rgProductos.DataSource = dt2;
                rgProductos.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<EntradaVirtualDet> GetListPro(int Id_Folio)
        {
            try
            {
                Funciones funciones = new CapaDatos.Funciones();
                List<EntradaVirtualDet> List = new List<EntradaVirtualDet>();
                CN_EntradaVirtual clsCN_EntradaVirtual = new CN_EntradaVirtual();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                EntradaVirtualDet EntradaVirtualDet = new EntradaVirtualDet();
                EntradaVirtual ape = new EntradaVirtual();
                ape.Id_Emp = sesion.Id_Emp;
                ape.Id_Cd = sesion.Id_Cd_Ver;
                ape.Id_Env = Id_Folio;
                ape.Accion = HF_Tipo.Value;
                if (HF_Tipo.Value == "1" || HF_Tipo.Value == "2")
                {
                    ape.Env_Fecha = funciones.GetLocalDateTime(sesion.Minutos);
                }
                clsCN_EntradaVirtual.ConsultaVentanaEntradaVirtualPro(ape, sesion.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool EnviaEmail(string GUID) //edit sender and receiver info, etc.
        {
            try
            {
                bool ok = false;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (CambiarEstatus("S") != 1)
                {
                    Alerta("Ocurrió un error al intentar realizar la solicitud");
                    return ok;
                }

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = sesion.Id_Cd_Ver;
                configuracion.Id_Emp = sesion.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, sesion.Emp_Cnx);

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Se ha colocado una solicitud de autorización de entrada virtual con el número de solicitud " + txtSolicitud.Text);

                cuerpo_correo.Append(", de la sucursal " + sesion.Id_Cd_Ver);
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");

                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("<a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "ProEntradaVirtual_Autorizacion.aspx?Id1=" + GUID + "&Id2=" + sesion.Id_Emp + "&Id3=" + sesion.Id_Cd_Ver + "&Id4=1" + "'>");
                cuerpo_correo.Append("Solicitud de autorización de Entrada Virtual</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                m.To.Add(new MailAddress(configuracion.Mail_EVirtual));
                m.CC.Add(new MailAddress("rafael.garcia@gibraltar.com.mx"));

                //m.Subject = "Solicitud de autorización de precios especiales";
                m.Subject = "Solicitud de autorización de entrada virtual #" + txtSolicitud.Text + " del centro " + sesion.Id_Cd_Ver;
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
                    CambiarEstatus("C");
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                }
                CerrarVentana("Solicitud <b>#" + txtSolicitud.Text + "</b> enviada correctamente");
                return ok;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ConsultarEmail(int id_u)
        {
            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CatUsuario cn_catusuario = new CN_CatUsuario();
            Usuario u = new Usuario();
            u.Id_Emp = Sesion.Id_Emp;
            u.Id_Cd = Sesion.Id_Cd_Ver;
            u.Id_U = id_u;
            string correo = "";
            cn_catusuario.ConsultaCorreoUsuario(u, Sesion.Emp_Cnx, ref correo);
            return correo;
        }
        private int CambiarEstatus(string estatus)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_EntradaVirtual cn_EntradaVirtual = new CN_EntradaVirtual();
                EntradaVirtual ape = new EntradaVirtual();
                ape.Id_Emp = sesion.Id_Emp;
                ape.Id_Cd = sesion.Id_Cd_Ver;
                ape.Id_Env = Convert.ToInt32(txtSolicitud.Text);
                ape.Env_Estatus = estatus;
                int verificador = -1;
                cn_EntradaVirtual.EnviarEntradaVirtual(ape, sesion.Emp_Cnx, ref verificador);
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarCliente()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            bool cancelar = false;
            Clientes cte = new Clientes();
            cte.Id_Emp = sesion.Id_Emp;
            cte.Id_Cd = sesion.Id_Cd_Ver;
            cte.Id_Cte = Convert.ToInt32(txtNumCliente.Value.HasValue ? txtNumCliente.Value.Value : -1);
            cte.Id_Rik = sesion.Id_Rik;
            CN_CatCliente catcliente = new CN_CatCliente();
            try
            {
                catcliente.ConsultaClientes(ref cte, sesion.Emp_Cnx);

            }
            catch (Exception ex)
            {
                AlertaFocus(ex.Message, txtNumCliente.ClientID);
                cancelar = true;
            }

            if (cancelar)
            {
                txtCliente.Text = "";
                txtNumCliente.Text = "";

            }

            txtCliente.Text = cte.Cte_NomComercial;
            txtCredito.Text = cte.Cte_CondPago.ToString();

        }


        protected void txtNumCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CargarCliente();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        //PRODUCTOS
        private void PerformInsert_rgProductos(GridCommandEventArgs e)
        {
            try
            {
                int Id_Emp;
                int Id_Cd;
                int Id_Env;
                int? Id_EnvPro;
                int Id_Prd;
                string Prd_Descripcion = "";
                int Env_Cantidad;
                int Id_Mon;
                double Env_PreVta;
                double Env_Costo;
                DateTime? Env_FecInicio;
                DateTime? Env_FecFin;
                double Env_PreEsp;
                string Mon_Descripcion = "";

                DataRow[] Ar_dr;
                GridEditableItem gi = (GridEditableItem)e.Item;

                if (
                   ((RadNumericTextBox)gi.FindControl("txtClave")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtCantidad")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtPreVta")).Text == ""

                    )
                {
                    e.Canceled = true;
                    this.Alerta("Todos los campos son requeridos");
                    return;
                }


                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Id_Emp = sesion.Id_Emp;
                Id_Cd = sesion.Id_Cd_Ver;
                Id_Env = Convert.ToInt32(txtSolicitud.Text);
                Id_Prd = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtClave")).Text);
                Env_Cantidad = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text);
                Env_PreVta = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPreVta")).Text);
                Env_Costo = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtCosto")).Text);
                double Env_importe = Env_PreVta * Convert.ToDouble(Env_Cantidad);
                double Env_ImporteCosto = (Env_Costo * Convert.ToDouble(Env_Cantidad));


                double uBruta = 0;
                double Env_UBruta = Math.Round(((Env_importe - Env_ImporteCosto) / Env_importe) * 100, 2);

                (gi.FindControl("txtEnvImporte") as RadNumericTextBox).Text = Env_importe.ToString();
                (gi.FindControl("txtUBruta") as RadNumericTextBox).Text = Env_UBruta.ToString();

                CN_EntradaVirtual cn_EntradaVirtual = new CN_EntradaVirtual();
                cn_EntradaVirtual.ConsultaVentanaEntradaVirtual_ComboProducto(Id_Emp, Id_Cd, Id_Prd, sesion.Emp_Cnx, ref Prd_Descripcion);



                dt2.Rows.Add(new object[] {  
                    Id_Emp,
                    Id_Cd,
                    Id_Env,                    
                    Id_Prd,                   
                    Prd_Descripcion,
                    Env_Cantidad,
                    Env_Costo,
                    Env_PreVta,
                    Env_importe,
                    Env_UBruta
                   
                    });
                rgProductos.Rebind();
                this.CalcularTotales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void NumCantDevuelta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ErrorManager();
                RadNumericTextBox combo = (RadNumericTextBox)sender;
                int CantidadDevolver = combo.Value.HasValue ? Int32.Parse(combo.Text) : 0;

                GridDataItem dataItem = combo.Parent.Parent as GridDataItem;

                CN_CatProducto cn_producto = new CN_CatProducto();
                List<int> actuales = new List<int>();
                cn_producto.ConsultaProducto_Disponible(sesion.Id_Emp, sesion.Id_Cd_Ver, dataItem["Id_Prd"].Text, ref actuales, sesion.Emp_Cnx);


                if (actuales[2] < CantidadDevolver)
                {
                    this.AlertaFocus(string.Concat("Producto "
                            , dataItem["Id_Prd"].Text
                            , ", inventario disponible insuficiente.<br/>Inventario final: "
                            , actuales[0].ToString()
                            , "<br/>Asignado: "
                            , actuales[1].ToString()
                            , "<br/>Disponible: "
                            , (actuales[2]).ToString()), combo.ClientID);//cantidad_A)
                    combo.Text = "";
                    return;
                }

                //JFCV 14 Abr 2016 agregar validación para que si el costo es cero , que no realice la devolución.
                if (Convert.ToDecimal(dataItem["Env_Costo"].Text) <= 0)
                {
                    combo.Value = 0;
                    Alerta("El costo del producto: " + dataItem["Id_Prd"].Text + " - " + dataItem["Prd_Descripcion"].Text + ", es cero. Actualicelo para poder realizar la devolución.");
                    return;
                }
                //FIN

                if (Int32.Parse(dataItem["Env_Cantidad"].Text) < CantidadDevolver)
                {
                    combo.Value = 0;
                    Alerta("No se puede devolver una cantidad Mayor al saldo del producto ");
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }









        private void CargarSolicitante()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(sesion.Id_Cd_Ver, 1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.cmbSolicitante);
                this.cmbSolicitante.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        private void CalcularTotales()
        {
            try
            {
                double importeTotal = 0;
                double importeUb = 0;

                foreach (DataRow row in dt2.Rows)
                {
                    importeTotal += double.Parse(row["Env_Importe"].ToString());
                    importeUb += double.Parse(row["Env_uBruta"].ToString());
                }
                txtImporteFactura.Text = importeTotal.ToString();
                txtImporteUb.Text = importeUb.ToString();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Update_rgProductos(GridCommandEventArgs e)
        {
            try
            {
                int Id_Emp;
                int Id_Cd;
                int Id_Prd;
                string Prd_Descripcion = "";
                int Env_Cantidad;
                double Env_PreVta;
                double Env_Costo;


                GridItem gi = e.Item;
                DataRow[] Ar_dr;

                if (((RadNumericTextBox)gi.FindControl("txtClave")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtCantidad")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtPreVta")).Text == "")
                {
                    this.Alerta("Todos los campos son requeridos");
                    e.Canceled = true;
                    return;
                }

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Id_Emp = sesion.Id_Emp;
                Id_Cd = sesion.Id_Cd_Ver;
                Id_Prd = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtClave")).Text);
                Env_Cantidad = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text);
                Env_PreVta = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPreVta")).Text);
                Env_Costo = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtCosto")).Text);
                double Env_importe = Env_PreVta * Convert.ToDouble(Env_Cantidad);
                double Env_ImporteCosto = (Env_Costo * Convert.ToDouble(Env_Cantidad));
                double uBruta = 0;
                double Env_UBruta = Math.Round(((Env_importe - Env_ImporteCosto) / Env_importe) * 100, 2);
                (gi.FindControl("txtEnvImporte") as RadNumericTextBox).Text = Env_importe.ToString();
                (gi.FindControl("txtUBruta") as RadNumericTextBox).Text = Env_UBruta.ToString();
                CN_EntradaVirtual CN_EntradaVirtual = new CN_EntradaVirtual();
                CN_EntradaVirtual.ConsultaVentanaEntradaVirtual_ComboProducto(Id_Emp, Id_Cd, Id_Prd, sesion.Emp_Cnx, ref Prd_Descripcion);


                Ar_dr = dt2.Select("Id_Prd='" + Id_Prd + "'");

                if (Ar_dr.Length > 0)
                {

                    CN_EntradaVirtual cn_EntradaVirtual = new CN_EntradaVirtual();
                    List<Clientes> List_cte = new List<Clientes>();

                    Ar_dr = dt2.Select("Id_Prd='" + Id_Prd + "'");

                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Env_Cantidad"] = Env_Cantidad;
                    Ar_dr[0]["Env_Costo"] = Env_Costo;
                    Ar_dr[0]["Env_PreVta"] = Env_PreVta;
                    Ar_dr[0]["Env_importe"] = Env_importe;
                    Ar_dr[0]["Env_uBruta"] = Env_UBruta;

                    Ar_dr[0].AcceptChanges();
                }

                this.CalcularTotales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtImporteFactura_TextChanged(object sender, EventArgs e)
        {

        }

        private void Delete_rgProductos(GridCommandEventArgs e)
        {
            try
            {

                int Id_Prd = 0;
                DataRow[] Ar_dr;
                GridItem gi = e.Item;

                Id_Prd = Convert.ToInt32(((Label)gi.FindControl("lblClave")).Text);

                Ar_dr = dt2.Select("Id_Prd='" + Id_Prd + "'");

                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].Delete();
                    dt2.AcceptChanges();
                }
                this.CalcularTotales();
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
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "<br/><br/>', 330, 150);");
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