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

namespace SIANWEB
{
    public partial class Ventana_PrecioEspecial : System.Web.UI.Page
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
                return (DataTable)Session["dt2" + Session.SessionID];
            }
            set
            {
                Session["dt2" + Session.SessionID] = value;
            }
        }
        #endregion
        #region Eventos

      

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (Sesion == null)
                    CerrarVentana();
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        hiddenId.Value = Page.Request.QueryString["Id_Folio"];
                        HF_Tipo.Value = Page.Request.QueryString["Accion"];
                       
                        dpFecha1.SelectedDate = DateTime.Now;
                        CargarTipoVenta();
                        CargarEstatus();
                        CargarEmails();
                        CargarProveedor();
                        Session["dt" + Session.SessionID] = PoblarTablaCte();
                        Session["dt2" + Session.SessionID] = PoblarTablaPro();
                       
                        txtSolicitud.Enabled = false;

                        if (hiddenId.Value != "-1")
                            this.LLenarVentanaPrecioEspecial(Convert.ToInt32(hiddenId.Value), Convert.ToInt32(HF_Tipo.Value));
                        else
                            Nuevo();
                        this.CambiarNomColumna();

                        if (dpFecha1.SelectedDate > Sesion.CalendarioFin)
                        {
                            dpFecha1.DbSelectedDate = Sesion.CalendarioFin;
                        }

                    
                        
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

        protected void cmbProveedor_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RequiredFieldValidator4.Enabled = Convert.ToInt32(cmbProveedor.SelectedValue) == 1 ? false : true;
            RequiredFieldValidator5.Enabled = Convert.ToInt32(cmbProveedor.SelectedValue) == 3 ? true : false;
           
            ErrorManager();
           
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
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                RadComboBox ComboBox = sender as RadComboBox;
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spVentanaPrecioEspecial_Combo", ref ComboBox);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbMoneda_DataBinding(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if (moneda)
                {
                    moneda = false;
                    return;
                }
                moneda = true;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                RadComboBox ComboBox = sender as RadComboBox;
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatTmoneda_Combo", ref ComboBox);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        //CLIENTES
        protected void rgCliente_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                switch (e.CommandName)
                {
                    case "InitInsert":
                        break;
                    case "PerformInsert":
                        this.PerformInsert_rgCliente(e);
                        break;
                    case "Update":
                        break;
                    case "Delete":
                        this.Delete_rgCliente(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgCliente_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                DataTable dt = (DataTable)Session["dt" + Session.SessionID];

                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgCliente.DataSource = dt;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgCliente_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    GridEditableItem editItem = (GridEditableItem)e.Item;
                    Control insertbtn = (Control)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                        rgCliente.Columns.FindByUniqueName("EditCommandColumn").Visible = true;
                }
                else
                {
                    if (e.Item.IsDataBound)
                    {
                        GridDataItem item = (GridDataItem)e.Item;
                        item["EditCommandColumn"].Controls[0].Visible = false;
                        rgCliente.Columns.FindByUniqueName("EditCommandColumn").Visible = false;
                    }
                }
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
                    rgProductos.DataSource = dt2;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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

                    CargarTmoneda(editItem);
                    if (txtClave.Text != "")
                    {
                        CmbPrd.Enabled = false;
                        txtClave.Enabled = false;
                        CmbPrd.SelectedValue = txtClave.Text;

                        RadComboBox cmbMoneda = (RadComboBox)editItem["Id_Mon"].FindControl("cmbMoneda");
                        DataRow row = dt2.Rows[e.Item.ItemIndex];
                        cmbMoneda.SelectedValue = row["Id_Mon"].ToString();
                     
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


                        if ((Convert.ToInt32(HF_Tipo.Value) == 3 && cmbEstatus.SelectedValue.ToLower() == "s")  )
                        {
                            RadNumericTextBox txtVolVta = (RadNumericTextBox)editItem.FindControl("txtVolVta");
                            txtVolVta.Enabled = false;

                            RadNumericTextBox txtPreVta = (RadNumericTextBox)editItem.FindControl("txtPreVta");
                            txtPreVta.Enabled = false;

                            RadDatePicker rdp_FecIni = (RadDatePicker)editItem.FindControl("rdp_FecIni");
                            rdp_FecIni.Enabled = false;

                            RadDatePicker rdp_FecFin = (RadDatePicker)editItem.FindControl("rdp_FecFin");
                            rdp_FecFin.Enabled = false;

                            RadNumericTextBox txtPreEsp = (RadNumericTextBox)editItem.FindControl("txtPreEsp");
                            txtPreEsp.Enabled = false;
                        }

                        
                    }
                }


                //TODO: AGREGAR PARA PONER EL FOCUS
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditableItem form = (GridEditableItem)e.Item;
                    RadNumericTextBox dataField = (RadNumericTextBox)form["Id_Prd"].FindControl("txtClave");

                    if (!dataField.Enabled)
                    {
                        dataField = (RadNumericTextBox)form["Ape_PreVta"].FindControl("txtPreVta");
                    }


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
                ((sender as RadNumericTextBox).Parent.Parent.FindControl("txtPreEsp") as RadNumericTextBox).Text = prd.Prd_AAA.ToString();
                ((sender as RadNumericTextBox).Parent.Parent.FindControl("rdp_FecIni") as RadDatePicker).DbSelectedDate = sesion.CalendarioIni;
                //((sender as RadNumericTextBox).Parent.Parent.FindControl("rdp_FecIni") as RadDatePicker).Enabled = false;
                ((sender as RadNumericTextBox).Parent.Parent.FindControl("txtVolVta") as RadNumericTextBox).Focus();
            
               
               



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
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

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
                    if (dpIni.SelectedDate < Sesion.CalendarioIni)
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
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 380);
                       
                        RadPane2.Height = altura;
                        RadSplitter2.Height = altura;
                        rgProductos.Height = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 390);
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
                funcion = "CloseWindow()";
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
            if (cmbTipo.SelectedValue == "1") //Repetitiva
            {
                rgProductos.Columns[2].HeaderText = "Vol. mens. (unidades)";
                rgProductos.Rebind(); // causes big problem if used: http://www.telerik.com/community/forums/aspnet-ajax/ajax/222106-invalid-postback-or-callback-argument.aspx
            }
            else if (cmbTipo.SelectedValue == "2")
            {
                rgProductos.Columns[2].HeaderText = "Vol. total (unidades)";
                rgProductos.Rebind(); // causes big problem if used (see above)
            }
            else
            {
                rgProductos.Columns[2].HeaderText = "";
                rgProductos.Rebind();
            }
        }
        private string Guardar(bool enviar)
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            DataTable dt = (DataTable)Session["dt" + Session.SessionID];

            if (dt.Rows.Count < 1 || dt2.Rows.Count < 1)
            {
                Alerta("La solicitud debe llevar como mínimo un cliente y un producto");
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
                if (cmbTipo.SelectedValue == "-1")
                {
                    this.Alerta("Se debe seleccionar un tipo de venta");
                    return "";
                }

                int id = Convert.ToInt32(txtSolicitud.Text);
                PrecioEspecial PEspecial = this.LlenarPrecioEspecial(id);
               /* if (PEspecial.Ape_Solicitar <= 0)
                {
                    Alerta("Seleccione a un usuario para solicitar la autorización");
                    return "";
                }*/

                List<VentanaPrecioEspecialCte> list1 = new List<VentanaPrecioEspecialCte>();
                List<VentanaPrecioEspecialPro> list2 = new List<VentanaPrecioEspecialPro>();
                int keycounter1 = 0;
                int keycounter2 = 0;
                foreach (DataRow row in dt.Rows)
                {
                    VentanaPrecioEspecialCte vpec = new VentanaPrecioEspecialCte();
                    vpec.Id_Emp = session.Id_Emp;
                    vpec.Id_Cd = session.Id_Cd_Ver;
                    vpec.Id_Ape = id;
                    vpec.Id_ApeCte = keycounter1 + Convert.ToInt32(MaximoId("PrecioEspecialCte", "Id_ApeCte"));
                    vpec.Id_Cte = Convert.ToInt32(row["Id_Cte"]);
                    list1.Add(vpec);
                    keycounter1++;
                }

                foreach (DataRow row in dt2.Rows)
                {
                    VentanaPrecioEspecialPro vpep = new VentanaPrecioEspecialPro();
                    vpep.Id_Emp = session.Id_Emp;
                    vpep.Id_Cd = Convert.ToInt32(row["Id_Cd"]);
                    vpep.Id_Ape = id;
                    vpep.Id_Prd = Convert.ToInt32(row["Id_Prd"]);
                    vpep.Id_Mon = Convert.ToInt32(row["Id_Mon"]);
                    vpep.Ape_VolVta = Convert.ToInt32(row["Ape_VolVta"]);
                    vpep.Ape_PreVta = Convert.ToDouble(row["Ape_PreVta"]);
                    vpep.Ape_FecInicio = Convert.ToDateTime(row["Ape_FecInicio"]);
                    vpep.Ape_FecFin = Convert.ToDateTime(row["Ape_FecFin"]);
                    vpep.Ape_PreEsp = Convert.ToDouble(row["Ape_PreEsp"]);
                    list2.Add(vpep);
                    keycounter2++;
                }

                PEspecial.ListVentanaPrecioEspecialCte = list1;
                PEspecial.ListVentanaPrecioEspecialPro = list2;

                CN_PrecioEspecial clsPrecioEspecial = new CN_PrecioEspecial();
                string verificadorstr = "";

                if (EditType == "insert")
                {
                    clsPrecioEspecial.InsertarVentanaPrecioEspecial(PEspecial, session.Emp_Cnx, ref verificadorstr);

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
                    clsPrecioEspecial.ModificarVentanaPrecioEspecial(PEspecial, session.Emp_Cnx, ref verificadorstr);
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
                txtSolicitud.Text = MaximoId("PrecioEspecial", "Id_Ape");
                txtSolSus.Text = "";
                txtSolSus.Visible = false;
                lblSolicitudS.Visible = false;
                cmbTipo.SelectedIndex = -1;
                txtNotaResp.Enabled = false;
                txtNota.Text = "";
                ImageButton1.Visible = false;
                Session["dt" + Session.SessionID] = null;
                Session["dt2" + Session.SessionID] = null;
                Session["dt" + Session.SessionID] = PoblarTablaCte();
                Session["dt2" + Session.SessionID] = PoblarTablaPro();
                rgCliente.DataSource = Session["dt" + Session.SessionID];
                rgCliente.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                rgCliente.Columns[3].Display = true;

                rgProductos.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                rgProductos.Columns.FindByUniqueName("EditCommandColumn").Display = true; //11
                rgProductos.Columns.FindByUniqueName("DeleteColumn").Display = true; //12
                rgProductos.Columns.FindByUniqueName("Ape_Autorizado").Display = false; //7
                rgProductos.Columns.FindByUniqueName("Ape_Rechazado").Display = false;
                //rgProductos.Columns.FindByUniqueName("Ape_PreEsp").Display = false; //8
                rgProductos.Columns.FindByUniqueName("Ape_FecAut").Display = false; //9
                rgProductos.Columns.FindByUniqueName("Ape_HrAut").Display = false; //10

                rgProductos.DataSource = Session["dt2" + Session.SessionID];
                rgCliente.Rebind();
                rgProductos.Rebind();

                cmbTipo.Enabled = true;
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
        private void CargarTmoneda(GridEditFormItem i)
        {
            try
            {
                RadComboBox cmbMoneda = (RadComboBox)i.FindControl("cmbMoneda");

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
        private PrecioEspecial LlenarPrecioEspecial(int id)
        {
            CapaDatos.Funciones funciones = new CapaDatos.Funciones();
            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            PrecioEspecial ape = new PrecioEspecial();
            ape.Id_U = session.Id_U;
            ape.Id_Emp = session.Id_Emp;
            ape.Id_Cd = session.Id_Cd_Ver;
            ape.Id_Ape = id;
            ape.Ape_Fecha = DateTime.Now;
            ape.Ape_Naturaleza = HF_Tipo.Value;
            if (HF_Tipo.Value == "1" || HF_Tipo.Value == "2")
                ape.Ape_Estatus = "C";
            else
                ape.Ape_Estatus = cmbEstatus.SelectedValue;

            ape.Ape_Tipo = Convert.ToInt32(cmbTipo.SelectedValue);
            ape.Ape_Convenio = txtNumConvenio.Text;
            ape.Ape_NumProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
            ape.Ape_NumUsuario = txtNumUsuario.Text;


            ape.Ape_Solicitar = !string.IsNullOrEmpty(cmbSolicitud.SelectedValue) ? Convert.ToInt32(cmbSolicitud.SelectedValue) : -1;
            ape.Ape_Nota = txtNota.Text;
            //if (HF_Tipo.Value == "1" || HF_Tipo.Value == "2")
            if (txtSolSus.Visible)
                ape.Ape_Sustituye = Convert.ToInt32(txtSolSus.Text);
            return ape;
        }
        private DataTable PoblarTablaCte()
        {
            try
            { //poblar la tabla virtual con columnas vacias:
                DataTable dt = new DataTable();
                dt.Columns.Add("Id_Emp", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Cd", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Ape", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_ApeCte", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Cte", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Cte_NomComercial", System.Type.GetType("System.String"));

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable PoblarTablaPro()
        {
            try
            { //poblar la tabla virtual con columnas vacias:
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("Id_Emp", System.Type.GetType("System.Int32"));
                dt2.Columns.Add("Id_Cd", System.Type.GetType("System.Int32"));
                dt2.Columns.Add("Id_Ape", System.Type.GetType("System.Int32"));
                dt2.Columns.Add("Id_ApePro", System.Type.GetType("System.Int32"));
                dt2.Columns.Add("Id_Prd", System.Type.GetType("System.Int32"));
                dt2.Columns.Add("Id_Mon", System.Type.GetType("System.Int32"));
                dt2.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
                dt2.Columns.Add("Ape_VolVta", System.Type.GetType("System.Int32"));
                dt2.Columns.Add("Ape_PreVta", System.Type.GetType("System.Double"));
                dt2.Columns.Add("Ape_FecInicio", System.Type.GetType("System.DateTime"));
                dt2.Columns.Add("Ape_FecFin", System.Type.GetType("System.DateTime"));
                dt2.Columns.Add("Ape_PreEsp", System.Type.GetType("System.Double"));

                dt2.Columns.Add("Mon_Descripcion", System.Type.GetType("System.String"));
                dt2.Columns.Add("Ape_FecAut", System.Type.GetType("System.DateTime"));
                dt2.Columns.Add("Ape_Estatus", System.Type.GetType("System.String"));

                return dt2;
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
        private void CargarTipoVenta()
        {
            cmbTipo.Items.Add(new RadComboBoxItem("-- Seleccionar --", "-1"));
            cmbTipo.Items.Add(new RadComboBoxItem("Repetitiva", "1"));
            cmbTipo.Items.Add(new RadComboBoxItem("Unica", "2"));
        }

        private void CargarProveedor()
        {
            cmbProveedor.Items.Add(new RadComboBoxItem("-- Seleccionar --", "-1"));
            cmbProveedor.Items.Add(new RadComboBoxItem("Almacén Central", "1"));
            cmbProveedor.Items.Add(new RadComboBoxItem("Kimberly Clark ", "2"));
            cmbProveedor.Items.Add(new RadComboBoxItem("Georgia Pacific ", "3"));
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

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN_PrecioEspecial CNPrecioEspecial = new CN_PrecioEspecial();
                CNPrecioEspecial.ConsultarEmailsPt1(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, ref pipelist);

                string[] array1 = pipelist.Split(new char[] { '|' });


                DataTable dt_emails = new DataTable();
                dt_emails.Columns.Add("Id", System.Type.GetType("System.String"));
                dt_emails.Columns.Add("Email", System.Type.GetType("System.String"));
                dt_emails.Columns.Add("Nombre", System.Type.GetType("System.String"));
                dt_emails.Rows.Add("-1", "", "-- Seleccionar --");

                foreach (string email in array1)
                {
                    string nombre = "";
                    int? id = null;
                    CNPrecioEspecial.ConsultarEmailsPt2(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, email, ref nombre, ref id);
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
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, nomTabla, nomColumna, Sesion.Emp_Cnx, "spCatLocal_Maximo");
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
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                PrecioEspecial pe = new PrecioEspecial();
                pe.Id_Emp = Sesion.Id_Emp;
                pe.Id_Cd = Sesion.Id_Cd_Ver;
                pe.Id_Ape = Convert.ToInt32(txtSolicitud.Text);
                pe.Ape_NumProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
                pe.Ape_Convenio = txtNumConvenio.Text;
                pe.Ape_NumUsuario = txtNumUsuario.Text;

                int Verificador = 0;

                if (Convert.ToInt32(cmbProveedor.SelectedValue) != 1 && string.IsNullOrEmpty(txtNumConvenio.Text) )
                {
                    Alerta("El convenio es requerido para este proveedor");
                }
                else if (Convert.ToInt32(cmbProveedor.SelectedValue) == 3 && string.IsNullOrEmpty(txtNumUsuario.Text))
                {
                    Alerta("El Numero de Usuario es requerido para este proveedor");
                }
                else {
                    new CN_PrecioEspecial().ActualizaProveedor(pe, Sesion.Emp_Cnx, ref Verificador);

                    if (Verificador == 1)
                    {
                        CerrarVentana("Los datos se han guardado correctamente");
                    }
                
                }


            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ImageButton1_Click");
            }
        }

        private void LLenarVentanaPrecioEspecial(int Id_Folio, int tipoAccion)
        {
            try
            {
                DataTable dt = (DataTable)Session["dt" + Session.SessionID];
                dt.Rows.Clear();
                dt2.Rows.Clear();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                PrecioEspecial v = null;

                new CN_PrecioEspecial().ConsultaVentanaPrecioEspecial(ref v, Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Id_Folio);

                dpFecha1.SelectedDate = v.Ape_Fecha;
                cmbEstatus.SelectedValue = v.Ape_Estatus;
                cmbTipo.SelectedValue = v.Ape_Tipo.ToString();
                txtNumConvenio.Text = v.Ape_Convenio;
                txtNumUsuario.Text = v.Ape_NumUsuario;
              
                
                try
                {
                    cmbSolicitud.SelectedIndex = cmbSolicitud.FindItemIndexByValue(v.Ape_Solicitar.ToString());
                    cmbSolicitud.Text = cmbSolicitud.FindItemByValue(v.Ape_Solicitar.ToString()).Text;

                    cmbProveedor.SelectedIndex = cmbProveedor.FindItemIndexByValue(v.Ape_NumProveedor.ToString());
                    cmbProveedor.Text = cmbProveedor.FindItemByValue(v.Ape_NumProveedor.ToString()).Text;
                }
                catch
                {
                }

                if (v.Ape_NumProveedor != null)
                {

                    RequiredFieldValidator4.Enabled = v.Ape_NumProveedor == 1 ? false : true;
                    RequiredFieldValidator5.Enabled = v.Ape_NumProveedor == 3 ? true : false;
                }

                txtNota.Text = v.Ape_Nota;
                txtNotaResp.Text = v.Ape_NotaResp;
                txtNotaResp.Enabled = false;

                if (v.Ape_Sustituye > 0)
                {
                    txtSolSus.Visible = true;
                    lblSolicitudS.Visible = true;
                    lblSolicitudS.Text = "Solicitud que sustituye";
                    txtSolSus.Value = v.Ape_Sustituye;
                }
                else if (v.Ape_Sustituida > 0)
                {
                    txtSolSus.Visible = true;
                    lblSolicitudS.Visible = true;
                    lblSolicitudS.Text = "Solicitud sustituida por";
                    txtSolSus.Value = v.Ape_Sustituida;
                }
                else
                {
                    txtSolSus.Visible = false;
                    lblSolicitudS.Visible = false;
                }

                List<VentanaPrecioEspecialCte> lista = GetListCte(Id_Folio);
                foreach (VentanaPrecioEspecialCte objCte in lista)
                {
                    DataRow row = dt.NewRow();
                    row["Id_Emp"] = objCte.Id_Emp;
                    row["Id_Cd"] = objCte.Id_Cd;
                    row["Id_Ape"] = objCte.Id_Ape;
                    row["Id_ApeCte"] = objCte.Id_ApeCte;
                    row["Id_Cte"] = objCte.Id_Cte;
                    row["Cte_NomComercial"] = objCte.Cte_NomComercial;
                    dt.Rows.Add(row);
                }


                Calendario calendario = new Calendario();
                CapaNegocios.CN_CatCalendario CN_CatCalendario = new CN_CatCalendario();

                CN_CatCalendario.ConsultaCalendarioActual(ref calendario, Sesion);
                string Mes = calendario.Cal_Mes < 10 ? "0" + ""  +  calendario.Cal_Mes : calendario.Cal_Mes.ToString();
                string str = "" + calendario.Cal_Año + Mes + "01";
                string Dia = (calendario.Cal_Mes == 2) ? "28" : "30";
                string strFin = "" + calendario.Cal_Año + Mes + Dia;
                string[] format = { "yyyyMMdd" };
                DateTime date;

                if (!DateTime.TryParseExact(str, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    date = Sesion.CalendarioIni;
                }

                if (date < Sesion.CalendarioIni)
                {
                    date = Sesion.CalendarioIni;
                }

                DateTime dateFin;

                if (!DateTime.TryParseExact(strFin, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateFin))
                {
                    dateFin = Sesion.CalendarioFin;
                } 

               
                List<VentanaPrecioEspecialPro> lista2 = GetListPro(Id_Folio);
                foreach (VentanaPrecioEspecialPro objPro in lista2)
                {
                    DataRow row = dt2.NewRow();
                    row["Id_Emp"] = objPro.Id_Emp;
                    row["Id_Cd"] = objPro.Id_Cd;
                    row["Id_Ape"] = objPro.Id_Ape;
                    row["Id_ApePro"] = objPro.Id_ApePro;
                    row["Id_Prd"] = objPro.Id_Prd;
                    row["Id_Mon"] = objPro.Id_Mon;
                    row["Prd_Descripcion"] = objPro.Prd_Descripcion;
                    row["Ape_VolVta"] = objPro.Ape_VolVta;
                    row["Ape_PreVta"] = objPro.Ape_PreVta;
                    row["Ape_FecInicio"] = (tipoAccion == 1 || tipoAccion == 2) ? date : objPro.Ape_FecInicio;
                    row["Ape_FecFin"] = (tipoAccion == 1 || tipoAccion == 2) ? dateFin : objPro.Ape_FecFin; 
                    row["Ape_PreEsp"] = objPro.Ape_PreEsp;
                    row["Mon_Descripcion"] = objPro.Mon_Descripcion;
                    row["Ape_FecAut"] = objPro.Ape_FecAut;
                    row["Ape_Estatus"] = objPro.Ape_Estatus;
                    dt2.Rows.Add(row);
                }

                bool validadorAdmin = false;
                foreach (RadComboBoxItem item in cmbSolicitud.Items)
                {
                    int valor = 0;
                    Int32.TryParse(item.Value, out valor);
                    if (Sesion.Id_U == valor)
                        validadorAdmin = true;
                }

                if (tipoAccion == 1 || tipoAccion == 2 || validadorAdmin)
                {
                    ImageButton1.Visible = true; 
                    if (validadorAdmin)
                    {
                        txtSolicitud.Text = Id_Folio.ToString();
                        
                    }
                    else
                    {
                        txtSolicitud.Text = MaximoId("PrecioEspecial", "Id_Ape");
                        txtSolSus.Visible = true;
                        lblSolicitudS.Visible = true;
                        lblSolicitudS.Text = "Solicitud que sustituye";
                        txtSolSus.Text = Id_Folio.ToString();
                        ImageButton1.Visible = false; 
                    }
                        cmbTipo.Enabled = false;
                        
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
                        cmbTipo.Enabled = false;
                        ImageButton1.Visible = true;
                        //cmbProveedor.Enabled = false;
                        //txtNumConvenio.Enabled = false;
                        rgCliente.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                        rgCliente.Columns[3].Display = false;

                        rgProductos.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                        rgProductos.Columns.FindByUniqueName("DeleteColumn").Display = false;

                        if (cmbEstatus.SelectedValue.ToLower() == "a" || cmbEstatus.SelectedValue.ToLower() == "p")
                        {
                            rgProductos.Columns.FindByUniqueName("EditCommandColumn").Display = false;
                            txtNota.Enabled = false;
                            cmbSolicitud.Enabled = false;
                            rtb1.Items[2].Visible = false;
                            rtb1.Items[1].Visible = false;
                            rgProductos.Columns.FindByUniqueName("Ape_Autorizado").Display = true;
                            rgProductos.Columns.FindByUniqueName("Ape_Rechazado").Display = true;
                            rgProductos.Columns.FindByUniqueName("Ape_PreEsp").Display = true;
                            rgProductos.Columns.FindByUniqueName("Ape_FecAut").Display = true;
                            rgProductos.Columns.FindByUniqueName("Ape_HrAut").Display = true;
                        }
                    }
                    txtSolicitud.Text = Id_Folio.ToString();
                }
                else if (tipoAccion == 4)
                {
                    Nuevo();
                    txtSolicitud.Text = Id_Folio.ToString();
                }
                txtNotaResp.Enabled = false;
                rgCliente.DataSource = dt;
                rgCliente.DataBind();
                rgProductos.DataSource = dt2;
                rgProductos.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<VentanaPrecioEspecialCte> GetListCte(int Id_Folio)
        {
            try
            {
                List<VentanaPrecioEspecialCte> List = new List<VentanaPrecioEspecialCte>();
                CN_PrecioEspecial clsCN_PrecioEspecial = new CN_PrecioEspecial();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                VentanaPrecioEspecialCte VentanaPrecioEspecialCte = new VentanaPrecioEspecialCte();

                PrecioEspecial ape = new PrecioEspecial();
                ape.Id_Emp = session2.Id_Emp;
                ape.Id_Cd = session2.Id_Cd_Ver;
                ape.Id_Ape = Id_Folio;

                clsCN_PrecioEspecial.ConsultaVentanaPrecioEspecialCte(ape, session2.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<VentanaPrecioEspecialPro> GetListPro(int Id_Folio)
        {
            try
            {
                Funciones funciones = new CapaDatos.Funciones();
                List<VentanaPrecioEspecialPro> List = new List<VentanaPrecioEspecialPro>();
                CN_PrecioEspecial clsCN_PrecioEspecial = new CN_PrecioEspecial();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                VentanaPrecioEspecialPro VentanaPrecioEspecialPro = new VentanaPrecioEspecialPro();
                PrecioEspecial ape = new PrecioEspecial();
                ape.Id_Emp = session2.Id_Emp;
                ape.Id_Cd = session2.Id_Cd_Ver;
                ape.Id_Ape = Id_Folio;
                ape.Accion = HF_Tipo.Value;
                if (HF_Tipo.Value == "1" || HF_Tipo.Value == "2")
                {
                    ape.Ape_Fecha = funciones.GetLocalDateTime(session2.Minutos);
                }
                clsCN_PrecioEspecial.ConsultaVentanaPrecioEspecialPro(ape, session2.Emp_Cnx, ref List);
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
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                if (CambiarEstatus("S") != 1)
                {
                    Alerta("Ocurrió un error al intentar realizar la solicitud");
                    return ok;
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
                cuerpo_correo.Append("Se ha colocado una solicitud de autorización de precios especiales con el número de solicitud " + txtSolicitud.Text);
                if (txtSolSus.Visible)
                {
                    cuerpo_correo.Append(" que sustituye a la solicitud #" + txtSolSus.Text);
                }
                cuerpo_correo.Append(", de la sucursal " + session.Id_Cd_Ver);
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");

                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("<a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "ProPrecioEspecial_Autorizacion.aspx?Id1=" + GUID + "&Id2=" + session.Id_Emp + "&Id3=" + session.Id_Cd_Ver  +"&Id4=1" + "'>");
                cuerpo_correo.Append("Solicitud de autorización de precios especiales</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                m.To.Add(new MailAddress((cmbSolicitud.SelectedItem.FindControl("lblMail") as Label).Text));
                m.CC.Add(new MailAddress("eugenio.escamilla@key.com.mx"));
                m.CC.Add(new MailAddress("juan.campos@key.com.mx"));
                //m.Subject = "Solicitud de autorización de precios especiales";
                m.Subject = "Solicitud de autorización de precios especiales #" + txtSolicitud.Text + " del centro " + session.Id_Cd_Ver;
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
        private int CambiarEstatus(string estatus)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                CN_PrecioEspecial cn_precioespecial = new CN_PrecioEspecial();
                PrecioEspecial ape = new PrecioEspecial();
                ape.Id_Emp = session.Id_Emp;
                ape.Id_Cd = session.Id_Cd_Ver;
                ape.Id_Ape = Convert.ToInt32(txtSolicitud.Text);
                ape.Ape_Estatus = estatus;
                int verificador = -1;
                cn_precioespecial.EnviarPrecioEspecial(ape, session.Emp_Cnx, ref verificador);
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PerformInsert_rgCliente(GridCommandEventArgs e)
        {
            DataTable dt = (DataTable)Session["dt" + Session.SessionID];

            int Id_Emp;
            int Id_Cd;
            int Id_Ape;
            int? Id_ApeCte;
            int Id_Cte;
            string Cte_NomComercial = "";

            DataRow[] Ar_dr;
            GridItem gi = e.Item;

            if (((RadNumericTextBox)gi.FindControl("txtNum")).Text == "")
            {
                e.Canceled = true;
                this.Alerta("El cliente especificado no existe");
                return;
            }

            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            Id_Emp = session.Id_Emp;
            Id_Cd = session.Id_Cd_Ver;
            Id_Ape = Convert.ToInt32(txtSolicitud.Text);
            Id_ApeCte = null;
            Id_Cte = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtNum")).Text);

            CN_PrecioEspecial CN_PrecioEspecial = new CN_PrecioEspecial();
            CN_PrecioEspecial.ConsultaVentanaPrecioEspecial_ComboCliente(Id_Emp, Id_Cd, Id_Cte, session.Emp_Cnx, ref Cte_NomComercial);

            if (Cte_NomComercial == "")
            {
                this.Alerta("El cliente especificado no existe: " + Id_Cte);
                return;
            }

            Ar_dr = dt.Select("Id_Emp='" + Id_Emp + "' and Id_Cd='" + Id_Cd + "' and Id_Cte='" + Id_Cte + "'"); //remove id_emp and id_cd?

            if (Ar_dr.Length > 0)
            {
                this.Alerta("El cliente ya ha sido capturado");
                e.Canceled = true;
            }
            else
            {
                dt.Rows.Add(new object[] {  
                    Id_Emp,
                    Id_Cd,
                    Id_Ape,
                    Id_ApeCte,
                    Id_Cte,
                    Cte_NomComercial
                });
            }
        }
        private void Delete_rgCliente(GridCommandEventArgs e)
        {
            DataTable dt = (DataTable)Session["dt" + Session.SessionID];

            int Id_Cte = 0;
            DataRow[] Ar_dr;
            GridItem gi = e.Item;

            Id_Cte = Convert.ToInt32(((Label)gi.FindControl("lblId_Cte")).Text);

            Ar_dr = dt.Select("Id_Cte='" + Id_Cte + "'");

            if (Ar_dr.Length > 0)
            {
                Ar_dr[0].Delete();
                dt.AcceptChanges();
            }
        }

        //PRODUCTOS
        private void PerformInsert_rgProductos(GridCommandEventArgs e)
        {
            try
            {
                int Id_Emp;
                int Id_Cd;
                int Id_Ape;
                int? Id_ApePro;
                int Id_Prd;
                string Prd_Descripcion = "";
                int Ape_VolVta;
                int Id_Mon;
                double Ape_PreVta;
                DateTime? Ape_FecInicio;
                DateTime? Ape_FecFin;
                double Ape_PreEsp;
                string Mon_Descripcion = "";

                DataRow[] Ar_dr;
                GridEditableItem gi = (GridEditableItem)e.Item;

                if (
                   ((RadNumericTextBox)gi.FindControl("txtClave")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtVolVta")).Text == "" ||
                    ((RadComboBox)gi.FindControl("cmbMoneda")).SelectedValue == "-1" ||
                    ((RadNumericTextBox)gi.FindControl("txtPreVta")).Text == "" ||
                    ((RadDatePicker)gi.FindControl("rdp_FecIni")).SelectedDate == null ||
                    ((RadDatePicker)gi.FindControl("rdp_FecFin")).SelectedDate == null
                    )
                {
                    e.Canceled = true;
                    this.Alerta("Todos los campos son requeridos");
                    return;
                }

                if (((RadNumericTextBox)gi.FindControl("txtPreEsp")).Text == "")
                    ((RadNumericTextBox)gi.FindControl("txtPreEsp")).Text = "0";

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                Id_Emp = session.Id_Emp;
                Id_Cd = session.Id_Cd_Ver;
                Id_Ape = Convert.ToInt32(txtSolicitud.Text);
                Id_ApePro = null;
                Id_Prd = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtClave")).Text);
                Ape_VolVta = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtVolVta")).Text);
                Id_Mon = Convert.ToInt32(((RadComboBox)gi.FindControl("cmbMoneda")).SelectedValue);
                Mon_Descripcion = ((RadComboBox)gi.FindControl("cmbMoneda")).SelectedItem.Text;

                Ape_PreVta = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPreVta")).Text);
                Ape_FecInicio = ((RadDatePicker)gi.FindControl("rdp_FecIni")).SelectedDate; //necesita validacion **************************
                DateTime? Ape_FecInicio2 = ((RadDatePicker)gi.FindControl("rdp_FecIni")).SelectedDate;
                Ape_FecFin = ((RadDatePicker)gi.FindControl("rdp_FecFin")).SelectedDate; //necesita validacion *****************************
                DateTime? Ape_FecFin2 = ((RadDatePicker)gi.FindControl("rdp_FecFin")).SelectedDate; //necesita validacion *****************************
                Ape_PreEsp = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPreEsp")).Text);

                CN_PrecioEspecial cn_precioespecial = new CN_PrecioEspecial();
                cn_precioespecial.ConsultaVentanaPrecioEspecial_ComboProducto(Id_Emp, Id_Cd, Id_Prd, session.Emp_Cnx, ref Prd_Descripcion);

                if (Ape_FecInicio > Ape_FecFin)
                {
                    Alerta("La fecha de fin debe ser mayor a la fecha de inicio");
                    e.Canceled = true;
                    return;
                }

                if (Convert.ToInt32(Ape_FecInicio.Value.ToString("MM")) < 3 && Convert.ToInt32(Ape_FecFin.Value.ToString("MM")) > 3)
                {
                    Alerta("La vigencia solo puede ser hasta el mes de Marzo, si el inicio es antes de dicho mes");
                    e.Canceled = true;
                    return;
                }

                List<Clientes> List_cte = new List<Clientes>();
                Clientes cte = default(Clientes);
                for (int x = 0; x < rgCliente.Items.Count; x++)
                {
                    cte = new Clientes();
                    cte.Id_Emp = Id_Emp;
                    cte.Id_Cd = Id_Cd;
                    cte.Id_Cte = Convert.ToInt32((rgCliente.Items[x]["Id_Cte"].FindControl("lblId_Cte") as Label).Text);
                    cte.GenInt = Id_Prd;

                    List_cte.Add(cte);
                }
                cn_precioespecial.ConsultaProductoCliente(ref List_cte, session.Emp_Cnx, ref Ape_FecInicio, ref Ape_FecFin);

                for (int x = 0; x < List_cte.Count; x++)
                {
                    if (List_cte[x].GenBool)
                    {
                        if (List_cte[x].GenInt.ToString() != txtSolicitud.Text)
                        {
                            Alerta("Ya existe el convenio <b>#" + List_cte[x].GenInt + "</b> con el producto <b>#" + Id_Prd + "</b> y cliente <b>#" + List_cte[x].Id_Cte + "</b> cuya vigencia es de <b>" + Ape_FecInicio.Value.ToString("dd MMM yyyy") + "</b> a <b>" + Ape_FecFin.Value.ToString("dd MMM yyyy") + "</b> no es posible traslapar la información");
                            e.Canceled = true;
                            return;
                        }
                    }
                }

                Ar_dr = dt2.Select("Id_Prd='" + Id_Prd + "' and (('" + Ape_FecInicio + "' >= Ape_FecInicio and  '" + Ape_FecInicio + "' <= Ape_FecFin) or ('" + Ape_FecFin + "' >= Ape_FecInicio and '" + Ape_FecFin + "' <= Ape_FecFin))");

                if (Ar_dr.Length > 0)
                {
                    this.Alerta("El producto ya ha sido capturado, no es posible traslapar la información");
                    e.Canceled = true;
                    return;
                }

                Ar_dr = dt2.Select("Id_Prd='" + Id_Prd + "' and ('" + Ape_FecInicio + "'  <= Ape_FecInicio and '" + Ape_FecFin + "' >= Ape_FecInicio)");
                if (Ar_dr.Length > 0)
                {
                    this.Alerta("El producto ya ha sido capturado, no es posible traslapar la información");
                    e.Canceled = true;
                    return;
                }

                dt2.Rows.Add(new object[] {  
                    Id_Emp,
                    Id_Cd,
                    Id_Ape,
                    Id_ApePro,
                    Id_Prd,
                    Id_Mon,
                    Prd_Descripcion,
                    Ape_VolVta,
                    Ape_PreVta,
                    Ape_FecInicio2,
                    Ape_FecFin2,
                    Ape_PreEsp,
                    Mon_Descripcion,
                    (object)null,
                    false
                    });
                rgProductos.Rebind();
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
                int Ape_VolVta;
                int Id_Mon;
                double Ape_PreVta;
                DateTime? Ape_FecInicio;
                DateTime? Ape_FecFin;
                double Ape_PreEsp;
                string Mon_Descripcion = "";
                GridItem gi = e.Item;
                DataRow[] Ar_dr;

                if (((RadNumericTextBox)gi.FindControl("txtClave")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtVolVta")).Text == "" ||
                    ((RadComboBox)gi.FindControl("cmbMoneda")).SelectedValue == "-1" ||
                    ((RadNumericTextBox)gi.FindControl("txtPreVta")).Text == "" ||
                    ((RadDatePicker)gi.FindControl("rdp_FecIni")).SelectedDate == null ||
                    ((RadDatePicker)gi.FindControl("rdp_FecFin")).SelectedDate == null ||
                    ((RadNumericTextBox)gi.FindControl("txtPreEsp")).Text == "")
                {
                    this.Alerta("Todos los campos son requeridos");
                    e.Canceled = true;
                    return;
                }

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                Id_Emp = session.Id_Emp;
                Id_Cd = session.Id_Cd_Ver;
                Id_Prd = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtClave")).Text);
                Ape_VolVta = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtVolVta")).Text);
                Id_Mon = Convert.ToInt32(((RadComboBox)gi.FindControl("cmbMoneda")).SelectedValue);
                Mon_Descripcion = ((RadComboBox)gi.FindControl("cmbMoneda")).SelectedItem.Text;
                Ape_PreVta = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPreVta")).Text);
                Ape_FecInicio = ((RadDatePicker)gi.FindControl("rdp_FecIni")).SelectedDate; //necesita validacion **************************
                DateTime? Ape_FecInicio2 = ((RadDatePicker)gi.FindControl("rdp_FecIni")).SelectedDate; //necesita validacion **************************
                Ape_FecFin = ((RadDatePicker)gi.FindControl("rdp_FecFin")).SelectedDate; //necesita validacion *****************************
                DateTime? Ape_FecFin2 = ((RadDatePicker)gi.FindControl("rdp_FecFin")).SelectedDate; //necesita validacion *****************************
                Ape_PreEsp = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPreEsp")).Text);
                CN_PrecioEspecial CN_PrecioEspecial = new CN_PrecioEspecial();
                CN_PrecioEspecial.ConsultaVentanaPrecioEspecial_ComboProducto(Id_Emp, Id_Cd, Id_Prd, session.Emp_Cnx, ref Prd_Descripcion);

                if (Ape_FecInicio >= Ape_FecFin)
                {
                    Alerta("La fecha de fin debe ser mayor a la fecha de inicio");
                    e.Canceled = true;
                    return;
                }
                Ar_dr = dt2.Select("Id_Prd='" + Id_Prd + "'");

                if (Ar_dr.Length > 0)
                {
                    if (HF_Tipo.Value == "2")
                    {
                        if (!(Ape_FecInicio >= Convert.ToDateTime(Ar_dr[0]["Ape_FecInicio"]) && Ape_FecInicio <= Convert.ToDateTime(Ar_dr[0]["Ape_FecFin"])))
                        {
                            Alerta("La fecha de inicio debe estar entre el rango de fechas original");
                            e.Canceled = true;
                            return;
                        }
                    }

                    if (Convert.ToInt32(Ape_FecInicio.Value.ToString("MM")) < 3 && Convert.ToInt32(Ape_FecFin.Value.ToString("MM")) > 3)
                    {
                        Alerta("La vigencia solo puede ser hasta el mes de Marzo, si el inicio es antes de dicho mes");
                        e.Canceled = true;
                        return;
                    }

                    CN_PrecioEspecial cn_precioespecial = new CN_PrecioEspecial();
                    List<Clientes> List_cte = new List<Clientes>();
                    Clientes cte = default(Clientes);
                    for (int x = 0; x < rgCliente.Items.Count; x++)
                    {
                        cte = new Clientes();
                        cte.Id_Emp = Id_Emp;
                        cte.Id_Cd = Id_Cd;
                        cte.Id_Cte = Convert.ToInt32((rgCliente.Items[x]["Id_Cte"].FindControl("lblId_Cte") as Label).Text);
                        cte.GenInt = Id_Prd;
                        cte.GenInt2 = txtSolSus.Value.HasValue ? (int?)txtSolSus.Value.Value : null;
                        List_cte.Add(cte);
                    }

                    cn_precioespecial.ConsultaProductoCliente(ref List_cte, session.Emp_Cnx, ref Ape_FecInicio, ref Ape_FecFin);

                    for (int x = 0; x < List_cte.Count; x++)
                    {
                        if (List_cte[x].GenBool && e.Item.RowIndex - 2 != x)
                        {
                            Alerta("Ya existe el convenio <b>#" + List_cte[x].GenInt + "</b> con el producto <b>#" + Id_Prd + "</b> y cliente <b>#" + List_cte[x].Id_Cte + "</b> cuya vigencia es de <b>" + Ape_FecInicio.Value.ToString("dd MMM yyyy") + "</b> a <b>" + Ape_FecFin.Value.ToString("dd MMM yyyy") + "</b> no es posible traslapar la información");
                            e.Canceled = true;
                            return;
                        }
                    }
                    Ar_dr = dt2.Select("Id_Prd='" + Id_Prd + "'");

                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Ape_VolVta"] = Ape_VolVta;
                    Ar_dr[0]["Id_Mon"] = Id_Mon;
                    Ar_dr[0]["Ape_PreVta"] = Ape_PreVta;
                    Ar_dr[0]["Ape_FecInicio"] = Ape_FecInicio2.Value;
                    Ar_dr[0]["Ape_FecFin"] = Ape_FecFin2.Value;
                    Ar_dr[0]["Ape_PreEsp"] = Ape_PreEsp;
                    Ar_dr[0]["Mon_Descripcion"] = Mon_Descripcion;
                    Ar_dr[0].AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ValidarFechaInicio(DateTime fechaValidar)
        {
            
            int thisMonth = DateTime.Now.Month;
            int thisYear = DateTime.Now.Year;

            if (thisYear >= Convert.ToDateTime(fechaValidar).Year)
            {
                if (Convert.ToDateTime(fechaValidar).Month < thisMonth)
                {
                    RAM1.ResponseScripts.Add("radalert('La fecha inicio de un producto no puede ser de un mes anterior', 330, 150);");
                    return;
                }
            }
            else
            {
                RAM1.ResponseScripts.Add("radalert('No se pueden usar fechas de años anteriores', 330, 150);");
                return;
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