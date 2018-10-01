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
using System.Text;
using System.IO;

namespace SIANWEB
{
    public partial class CapFisico : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private DataTable dt
        {
            get
            {
                return (DataTable)Session["dtFisico" + Session.SessionID];
            }
            set
            {
                Session["dtFisico" + Session.SessionID] = value;
            }
        }
        private object _cliente;
        public object cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }
        private object _territorio;
        public object territorio
        {
            get { return _territorio; }
            set { _territorio = value; }
        }
        bool cte = false;
        #endregion
        #region Eventos
        //COMBO PRODUCTO
        protected void Page_Init(object sender, EventArgs e)
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
                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.CargarCentros();
                        this.GetListDet();
                        this.rg1.Rebind();
                        HF_Contador.Value = "0";

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                this.Nuevo();
                this.rg1.Rebind();

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
                if (Page.IsValid)
                {
                    ErrorManager();
                    RadToolBarButton btn = e.Item as RadToolBarButton;
                    switch (btn.CommandName)
                    {
                        case "save":
                            this.Guardar("");
                            HF_Contador.Value = "0";
                            break;
                        case "new":
                            break;
                        case "undo":
                            break;
                        case "DwExcel":                            
                            List<Producto> List = new List<Producto>();
                            CN_CapFisico clsCN_CapFisicoConsignado = new CN_CapFisico();
                            Sesion session2 = (Sesion)Session["Sesion" + Session.SessionID];
                            Producto fisico = new Producto();
                            fisico.Id_Emp = session2.Id_Emp;
                            fisico.Id_Cd = session2.Id_Cd_Ver;
                            clsCN_CapFisicoConsignado.ConsultaFisico(fisico, session2.Emp_Cnx, ref List);
                            string ruta = Server.MapPath("Reportes") + "\\Fisico.txt";
                            StreamWriter sw = new StreamWriter(ruta, false, Encoding.UTF8);
                            sw.WriteLine("<html>");
                            sw.WriteLine("<head>");
                            sw.WriteLine("</head>");
                            sw.WriteLine("<body>");
                            sw.WriteLine("<table border=1><font size=8pt>");

                            sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Núm. Producto</td>");
                            sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Descripción</td>");
                            sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Presentación</td>");
                            sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Unidades</td>");
                            sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Inventario final</td>");
                            sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Físico</td>");
                            sw.WriteLine("</tr>");

                            foreach (Producto p in List)
                            {
                                sw.WriteLine("<tr>");
                                sw.WriteLine("<td>" + p.Id_Prd + "</td>");
                                sw.WriteLine("<td>" + p.Prd_Descripcion + "</td>");
                                sw.WriteLine("<td>" + p.Prd_Presentacion + "</td>");
                                sw.WriteLine("<td>" + p.Prd_UniNs + "</td>");
                                sw.WriteLine("<td>" + p.Prd_InvFinal + "</td>");
                                sw.WriteLine("<td>" + p.Prd_Fisico + "</td>");
                                sw.WriteLine("</tr>");
                            }                            
                            sw.WriteLine("</font></table>");
                            sw.WriteLine("</body>");
                            sw.WriteLine("</html>");
                            sw.Close();
                            if (File.Exists(ruta))
                            {
                                string ruta2 = null;
                                ruta2 = Server.MapPath("Reportes") + "\\Fisico.xls";
                                if (File.Exists(ruta2))
                                {
                                    File.Delete(ruta2);
                                }
                                File.Move(ruta, Server.MapPath("Reportes") + "\\Fisico.xls");
                                Response.Redirect("Reportes\\Fisico.xls", false);
                            }
                            break;
                        case "UpExcel":
                            RAM1.ResponseScripts.Add("UpExcel()");
                            break;
                    }                   
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rg1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_NeedDataSource");
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
                cte.Id_Terr = ((sender as RadNumericTextBox).Parent.FindControl("txtIdTer") as RadNumericTextBox).Value.HasValue ? (int)((sender as RadNumericTextBox).Parent.FindControl("txtIdTer") as RadNumericTextBox).Value.Value : 0;

                if (cte.Id_Cte == -1)
                {
                    return;
                }
                DataRow[] Ar_Dr2 = dt.Select("Id_Emp='" + cte.Id_Emp + "' and id_cd='" + cte.Id_Cd + "' and Id_Cte='" + cte.Id_Cte + "' and Id_Ter='" + cte.Id_Terr + "'");
                if (Ar_Dr2.Length > 0)
                {
                    AlertaFocus("El cliente-territorio ya fue capturado", (sender as RadNumericTextBox).ClientID);
                    (sender as RadNumericTextBox).Text = "";
                    ((sender as RadNumericTextBox).Parent.FindControl("txtCliente") as RadTextBox).Text = "";
                    return;
                }

                CN_CatCliente cnCliente = new CN_CatCliente();
                try
                {
                    cnCliente.ConsultaClientes(ref cte, sesion.Emp_Cnx);
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, (sender as RadNumericTextBox).ClientID);
                    (sender as RadNumericTextBox).Text = "";
                    ((sender as RadNumericTextBox).Parent.FindControl("txtCliente") as RadTextBox).Text = "";
                    return;
                }
                ((sender as RadNumericTextBox).Parent.FindControl("txtCliente") as RadTextBox).Text = cte.Cte_NomComercial;
                ((sender as RadNumericTextBox).Parent.FindControl("txtFis_Consignados") as RadNumericTextBox).Focus();
                RadComboBox combo = ((sender as RadNumericTextBox).Parent.FindControl("Cmb_Id_Ter") as RadComboBox);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtTerritorio_TextChanged(object sender, EventArgs e)
        {
            RadNumericTextBox txtcliente = ((sender as RadNumericTextBox).Parent.FindControl("txtIdCte") as RadNumericTextBox);
            txtCliente_TextChanged(txtcliente, null);
            txtcliente.Focus();
        }
        protected void rg1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (!txtProducto.Value.HasValue)
                        {
                            this.Alerta("No se ha seleccionado un producto");
                            e.Canceled = true;
                        }
                        else
                        {
                            int HFC = Convert.ToInt32(HF_Contador.Value.ToString());
                            HFC++;
                            HF_Contador.Value = HFC.ToString();
                        }
                        break;
                    case "PerformInsert":
                        this.PerformInsert(e);
                        break;

                    case "Update":
                        this.Update(e);
                        break;

                    case "Delete":
                        this.Delete(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_ItemCommand");
            }
        }
        protected void rg1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.ErrorManager();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbProductosLista_DataBound(object sender, EventArgs e)
        {
            try
            {
                ((Literal)cmbProductosLista.Footer.FindControl("cmbProductosCount")).Text = Convert.ToString(cmbProductosLista.Items.Count);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtProducto_TextChanged(object sender, EventArgs e)
        {
            if (txtProducto.Text == "")
            {
                return;
            }
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CatProducto cn_producto = new CN_CatProducto();
            Producto prd = new Producto();
            cn_producto.ConsultaProducto(ref prd, Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd_Ver, (int)txtProducto.Value.Value);

            if (prd.Prd_Descripcion != null)
            {
                cmbProductosLista.Text = prd.Prd_Descripcion;
                txtPresentacion.Text = prd.Prd_Presentacion;
                txtPresentacion0.Text = prd.Prd_UniNs;
                txtInventario.Text = prd.Prd_InvFinal.ToString();
                txtFisico.Text = prd.Prd_Fisico.ToString();
                lblProducto2.Text = txtProducto.Text + " " + prd.Prd_Descripcion;
                dt.Rows.Clear();
                List<FisicoConsignado> lista = GetList(prd.Id_Prd);
                foreach (FisicoConsignado objFisico in lista)
                {
                    DataRow row = dt.NewRow();
                    row["Id_Emp"] = objFisico.Id_Emp;
                    row["Id_Cd"] = objFisico.Id_Cd;
                    row["Id_Fis"] = objFisico.Id_Fis;
                    row["Id_FisCons"] = objFisico.Id_FisCons;
                    row["Id_Cte"] = objFisico.Id_Cte;
                    row["Id_CteStr"] = objFisico.Cte_Nombre;
                    row["Id_Ter"] = objFisico.Id_Ter;
                    row["Id_TerStr"] = objFisico.Ter_Nombre;
                    row["Fis_Consignados"] = objFisico.Fis_Consignados;
                    dt.Rows.Add(row);
                }

                rg1.DataSource = dt;
                rg1.DataBind();
                txtFisico.Focus();
            }
            else
            {
                Nuevo();
            }
        }
        protected void cmbProductosLista_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (e.Value != string.Empty && e.Value != "-1")
                {
                    if (Convert.ToInt32(HF_Contador.Value.ToString()) > 0)
                    {
                        this.Alerta("Aún no se han guardado los cambios al producto anteriormente seleccionado. Los cambios serán deshechados");
                        HF_Contador.Value = "0";
                    }

                    Producto p = null;
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    new CN_CatProducto().ConsultaProducto(ref p, Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(e.Value));
                    cmbProductosLista.Text = p.Prd_Descripcion;
                    txtProducto.Text = p.Id_Prd.ToString();
                    txtPresentacion.Text = p.Prd_Presentacion;
                    txtPresentacion0.Text = p.Prd_UniNs;
                    txtInventario.Text = p.Prd_InvFinal.ToString();
                    txtFisico.Text = p.Prd_Fisico.ToString();
                    lblProducto2.Text = e.Text;

                    dt.Rows.Clear();
                    List<FisicoConsignado> lista = GetList(Convert.ToInt32(e.Value));
                    foreach (FisicoConsignado objFisico in lista)
                    {
                        DataRow row = dt.NewRow();
                        row["Id_Emp"] = objFisico.Id_Emp;
                        row["Id_Cd"] = objFisico.Id_Cd;
                        row["Id_Fis"] = objFisico.Id_Fis;
                        row["Id_FisCons"] = objFisico.Id_FisCons;
                        row["Id_Cte"] = objFisico.Id_Cte;
                        row["Id_CteStr"] = objFisico.Cte_Nombre;
                        row["Id_Ter"] = objFisico.Id_Ter;
                        row["Id_TerStr"] = objFisico.Ter_Nombre;
                        row["Fis_Consignados"] = objFisico.Fis_Consignados;
                        dt.Rows.Add(row);
                    }
                    rg1.DataSource = dt;
                    rg1.DataBind();
                }
                else
                {
                    Nuevo();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbProductosLista_ItemDataBound(object sender, RadComboBoxItemEventArgs e) //necesaria?
        {
            try
            {
                if (e.Item.Value == "-1")
                {
                    Label lblAuxiliar = new Label();
                    lblAuxiliar.Width = new Unit(80, UnitType.Pixel);
                    e.Item.FindControl("liActivo").Controls.Clear();
                    e.Item.FindControl("liDescripcion").Controls.AddAt(0, lblAuxiliar);
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }       
        protected void cmbCte_DataBinding(object sender, EventArgs e)
        {
            try
            {
                if (cte)
                {
                    cte = false;
                    return;
                }
                cte = true;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                RadComboBox ComboBox = sender as RadComboBox;
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatCliente_Combo", ref ComboBox);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbTerr_DataBinding(object sender, EventArgs e)
        {
            try
            {
                if (cte)
                {
                    cte = false;
                    return;
                }
                cte = true;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                RadComboBox ComboBox = sender as RadComboBox;
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatTerritorio_Combo", ref ComboBox);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtIdCte_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                RadNumericTextBox txt = (RadNumericTextBox)sender;
                RadComboBox Cmb_Id_Cte = (RadComboBox)txt.Parent.FindControl("Cmb_Id_Cte");
                RadComboBox Cmb_Id_Ter = (RadComboBox)txt.Parent.FindControl("Cmb_Id_Ter");
                RadNumericTextBox txtIdTer = (RadNumericTextBox)txt.Parent.FindControl("txtIdTer");

                if (txt.Text.Contains("-") | txt.Text == "0")
                {
                    txt.Text = "";
                    return;
                }
                Cmb_Id_Cte.SelectedValue = txt.Text;
                //cargar territorio del cliente a txtIdTer y Cmb_Id_Ter, despues de escribir en txtIdCte:
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txt.Text), Sesion.Emp_Cnx, "spCatTerritorioCte_Combo", ref Cmb_Id_Ter);

                if (Cmb_Id_Ter.SelectedValue != "-1")
                {
                    txtIdTer.Text = Cmb_Id_Ter.SelectedValue;
                }

                txtIdTer.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtIdTer_OnTextChanged(object sender, EventArgs e)
        {
            try
            {  //actualizar combo de territorio:
                RadNumericTextBox txt = (RadNumericTextBox)sender;
                RadComboBox combo = (RadComboBox)txt.Parent.FindControl("Cmb_Id_Ter");

                if (txt.Text.Contains("-") | txt.Text == "0")
                {
                    txt.Text = "";
                    return;
                }
                combo.SelectedValue = txt.Text;

                RadNumericTextBox txtFis_Consignados = (RadNumericTextBox)txt.Parent.FindControl("txtFis_Consignados");
                txtFis_Consignados.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void Cmb_Id_Ter_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                //actualizar textbox de territorio:
                RadComboBox box = (RadComboBox)sender;
                RadNumericTextBox txt = (RadNumericTextBox)box.Parent.FindControl("txtIdTer");

                if (box.SelectedValue == "-1")
                {
                    txt.Text = "";
                }
                else
                {
                    txt.Text = box.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void Cmb_Id_Cte_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {  //actualizar textbox de cliente:
                RadComboBox box = (RadComboBox)sender;
                RadNumericTextBox txt = (RadNumericTextBox)box.Parent.FindControl("txtIdCte");

                if (box.SelectedValue == "-1")
                {
                    txt.Text = "";
                }
                else
                {
                    txt.Text = box.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void PerformInsert(GridCommandEventArgs e)
        {
            int Id_Emp = 0;
            int Id_Cd = 0;
            int Id_Fis = 0;
            int Id_FisCons = 0;
            int Id_Cte = 0;
            int Id_Ter = 0;
            int Fis_Consignados = 0;
            string Id_CteStr = "";
            string Id_TerStr = "";

            DataRow[] Ar_dr;
            GridItem gi = e.Item;

            if (((RadNumericTextBox)gi.FindControl("txtIdCte")).Text == "" ||
                ((RadNumericTextBox)gi.FindControl("txtIdTer")).Text == "" ||
                ((RadNumericTextBox)gi.FindControl("txtFis_Consignados")).Text == "")
            {
                e.Canceled = true;
                this.Alerta("Todos los campos son requeridos");
                return;
            }

            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            Id_Emp = session.Id_Emp;
            Id_Cd = session.Id_Cd_Ver;
            Id_Fis = Convert.ToInt32(MaximoId("CapFisico", "Id_Fis"));
            Id_FisCons = dt.Rows.Count + Convert.ToInt32(MaximoId("CapFisicoConsignado", "Id_FisCons"));
            Id_Cte = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtIdCte")).Text);
            Id_CteStr = ((RadTextBox)gi.FindControl("txtCliente")).Text;
            Id_Ter = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtIdTer")).Text);
            Id_TerStr = ((RadComboBox)gi.FindControl("Cmb_Id_Ter")).SelectedItem.Text;
            Fis_Consignados = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtFis_Consignados")).Text);

            Ar_dr = dt.Select("Id_Emp='" + Id_Emp + "' and Id_Cd='" + Id_Cd + "' and Id_FisCons='" + Id_FisCons + "'");
            DataRow[] Ar_Dr2 = dt.Select("Id_Emp='" + Id_Emp + "' and id_cd='" + Id_Cd + "' and Id_Cte='" + Id_Cte + "' and Id_Ter='" + Id_Ter + "'");
            if (Ar_dr.Length > 0)
            {
                this.Alerta("Producto ya capturado");
                e.Canceled = true;
            }
            else if (Ar_Dr2.Length > 0)
            {
                this.Alerta("El cliente-territorio ya fue capturado");
                e.Canceled = true;
            }
            else
            {
                dt.Rows.Add(new object[] {  
                    Id_Emp,
                    Id_Cd,
                    Id_Fis,
                    Id_FisCons,
                    Id_Cte,
                    Id_CteStr,
                    Id_Ter,
                    Id_TerStr,
                    Fis_Consignados
                });
            }
        }
        private void Update(GridCommandEventArgs e)
        {
            int Id_FisCons = 0;
            int Id_Cte = 0;
            int Id_Ter = 0;
            int Fis_Consignados = 0;
            string Id_CteStr = "";
            string Id_TerStr = "";
            int Id_Emp = 0;
            int Id_Cd = 0;

            GridItem gi = e.Item;
            DataRow[] Ar_dr;

            if (((RadNumericTextBox)gi.FindControl("txtIdCte")).Text == "" ||
                ((RadNumericTextBox)gi.FindControl("txtIdTer")).Text == "" ||
                ((RadNumericTextBox)gi.FindControl("txtFis_Consignados")).Text == "")
            {
                e.Canceled = true;
                this.Alerta("Todos los campos son requeridos");
                return;
            }
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            Id_Emp = session.Id_Emp;
            Id_Cd = session.Id_Cd_Ver;
            Id_FisCons = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtId_FisCons")).Text);
            Id_Cte = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtIdCte")).Text);
            Id_CteStr = ((RadTextBox)gi.FindControl("txtCliente")).Text;
            Id_Ter = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtIdTer")).Text);
            Id_TerStr = ((RadComboBox)gi.FindControl("Cmb_Id_Ter")).SelectedItem.Text;
            Fis_Consignados = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtFis_Consignados")).Text);

            Ar_dr = dt.Select("Id_FisCons='" + Id_FisCons + "'");
            DataRow[] Ar_Dr2 = dt.Select("Id_Emp='" + Id_Emp + "' and id_cd='" + Id_Cd + "' and Id_Cte='" + Id_Cte + "' and Id_Ter='" + Id_Ter + "'");
            if (Ar_Dr2.Length > 1)
            {
                this.Alerta("El cliente-territorio ya fue capturado");
                e.Canceled = true;
            }
            else if (Ar_dr.Length > 0)
            {
                Ar_dr[0].BeginEdit();
                Ar_dr[0]["Id_Cte"] = Id_Cte;
                Ar_dr[0]["Id_CteStr"] = Id_CteStr;
                Ar_dr[0]["Id_Ter"] = Id_Ter;
                Ar_dr[0]["Id_TerStr"] = Id_TerStr;
                Ar_dr[0]["Fis_Consignados"] = Fis_Consignados;
                Ar_dr[0].AcceptChanges();
            }
        }
        private void Delete(GridCommandEventArgs e)
        {
            int Id_FisCons = 0;
            DataRow[] Ar_dr;
            GridItem gi = e.Item;

            Id_FisCons = Convert.ToInt32(((Label)gi.FindControl("lblId_FisCons")).Text);

            Ar_dr = dt.Select("Id_FisCons='" + Id_FisCons + "'");

            if (Ar_dr.Length > 0)
            {
                Ar_dr[0].Delete();
                dt.AcceptChanges();
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "ok":
                        this.Inicializar();
                        break;
                    case "guardar":
                        this.Guardar("sin_mensaje");
                        HF_Contador.Value = "0";
                        txtProducto.Focus();
                        break;
                    case "cliente":
                        ((RadNumericTextBox)cliente).Text = Session["Id_Buscar" + Session.SessionID].ToString();
                        txtCliente_TextChanged(cliente, null);
                        break;
                    case "fisico":
                        Alerta("El inventario físico fue actualizado correctamente");
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string terr = ((RadNumericTextBox)territorio).Text == "" ? "-1" : ((RadNumericTextBox)territorio).Text;
                RAM1.ResponseScripts.Add("popup(" + terr + ");");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtProducto_Load(object sender, EventArgs e)
        {
            cliente = sender;
        }
        protected void txtTerritorio_Load(object sender, EventArgs e)
        {
            territorio = sender;
        }
        protected void rg1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //TODO: AGREGAR PARA PONER EL FOCUS
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem form = (GridEditableItem)e.Item;
                RadNumericTextBox dataField = (RadNumericTextBox)form["Id_Ter"].FindControl("txtIdTer");
                if (!dataField.Enabled)
                {
                    dataField = (RadNumericTextBox)form["Id_Cte"].FindControl("txtIdCte");
                }
                dataField.Focus();
            }
        }
        #endregion
        #region Funciones

        private void CargarProductos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion != null)
                {
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatProducto_Combo", ref cmbProductosLista);
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
                //poblar la tabla virtual con columnas vacias:
                dt = new DataTable();
                dt.Columns.Add("Id_Emp", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Cd", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Fis", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_FisCons", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Cte", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_CteStr", System.Type.GetType("System.String"));
                dt.Columns.Add("Id_Ter", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_TerStr", System.Type.GetType("System.String"));
                dt.Columns.Add("Fis_Consignados", System.Type.GetType("System.Int32"));
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
                    if (Permiso.PGrabar == false & Permiso.PModificar == false)
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

        private void Nuevo()
        {
            try
            {


                txtProducto.Text = string.Empty;
                txtPresentacion.Text = string.Empty;
                txtFisico.Text = string.Empty;
                HF_ID.Value = string.Empty;
                txtInventario.Text = string.Empty;

                Label lblAuxiliar2 = new Label();
                lblAuxiliar2.Width = new Unit(85, UnitType.Pixel);
                Label lblAuxiliar = new Label();
                lblAuxiliar.Text = "-- Seleccionar --";
                lblProducto2.Text = "";
                cmbProductosLista.ClearSelection();
                cmbProductosLista.Text = "";
                this.GetListDet();
                this.rg1.Rebind();
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
                CN_CapFisico CN_Fisico = new CN_CapFisico();
                Fisico fisico = new Fisico();
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                int verificador = -1;
                fisico.Id_Emp = session.Id_Emp;
                fisico.Id_Cd = session.Id_Cd_Ver;
                CN_Fisico.EliminarFisico(fisico, session.Emp_Cnx, ref verificador);
                Nuevo();
                this.Alerta("El inventario físico se inicializó exitosamente");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void Guardar(string mensaje)
        {
            try
            {
                if (!_PermisoGuardar)
                {
                    this.Alerta("No tiene permisos para grabar");
                    return;
                }
                if (txtFisico.Text == "" | txtInventario.Text == "" | txtPresentacion.Text == "")
                {
                    this.Alerta("Un campo requerido se encuentra vacío");
                    return;
                }
                if (cmbProductosLista.SelectedValue == "-1")
                {
                    this.Alerta("Se debe seleccionar un producto");
                    this.Nuevo();
                    return;
                }
                Fisico PFisico = this.LlenarFisico();
                List<FisicoConsignado> list = new List<FisicoConsignado>();
                foreach (DataRow row in dt.Rows)
                {
                    FisicoConsignado fc = new FisicoConsignado();
                    fc.Id_Emp = Convert.ToInt32(row["Id_Emp"]);
                    fc.Id_Cd = Convert.ToInt32(row["Id_Cd"]);
                    fc.Id_FisCons = Convert.ToInt32(row["Id_FisCons"]);
                    fc.Id_Cte = Convert.ToInt32(row["Id_Cte"]);
                    fc.Id_Ter = Convert.ToInt32(row["Id_Ter"]);
                    fc.Fis_Consignados = Convert.ToInt32(row["Fis_Consignados"]);
                    list.Add(fc);
                }

                PFisico.ListFisicoConsignado = list;

                CN_CapFisico clsFisico = new CN_CapFisico();
                int verificador = -1;

                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                clsFisico.InsertarFisico(PFisico, session.Emp_Cnx, ref verificador);

                if (verificador > 0)
                {
                    if (mensaje != "sin_mensaje")
                    {
                        this.AlertaFocus("Los datos se han guardado correctamente", txtProducto.ClientID);
                    }
                    Nuevo();
                }
                else
                    this.Alerta("Hubo un problema al insertar los datos");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTerritorio(RadComboBox ComboBox, int? Id_Cte)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                CN_CatTerritorios cnTerritorio = new CN_CatTerritorios();
                Territorios terr = new Territorios();
                terr.Id_Emp = Sesion.Id_Emp;
                terr.Id_Cd = Sesion.Id_Cd_Ver;
                List<Territorios> list = new List<Territorios>();

                cnTerritorio.ConsultaTerritorios(terr, Sesion.Emp_Cnx, ref list);

                ComboBox.DataSource = list;
                RadComboBoxItem rbi;
                foreach (Territorios territorio in list)
                {
                    rbi = new RadComboBoxItem();
                    rbi.Value = territorio.Id_Ter.ToString();
                    rbi.Text = territorio.Descripcion;
                    ComboBox.Items.Add(rbi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private Fisico LlenarFisico()
        {
            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            Fisico Fisico = new Fisico();
            Fisico.Id_Emp = session.Id_Emp;
            Fisico.Id_Cd = session.Id_Cd_Ver;
            Fisico.Id_Fis = Convert.ToInt32(MaximoId("CapFisico", "Id_Fis"));
            Fisico.Id_Prd = Convert.ToInt32(txtProducto.Text);
            Fisico.Fis_Fecha = DateTime.Now;
            Fisico.Fis_Fisico = Convert.ToInt32(txtFisico.Text);
            return Fisico;
        }

        private List<FisicoConsignado> GetList(int Id_Prd)
        {
            try
            {
                List<FisicoConsignado> List = new List<FisicoConsignado>();
                CN_CapFisico clsCN_CapFisicoConsignado = new CN_CapFisico();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                FisicoConsignado FisicoConsignado = new FisicoConsignado();
                FisicoConsignado.Id_Emp = session2.Id_Emp;
                FisicoConsignado.Id_Cd = session2.Id_Cd_Ver;
                clsCN_CapFisicoConsignado.ConsultaFisicoConsignado(FisicoConsignado, Id_Prd, session2.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string MaximoId(string nomTabla, string nomCol)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, nomTabla, nomCol, Sesion.Emp_Cnx, "spCatLocal_Maximo");
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
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 350, 150);");
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
