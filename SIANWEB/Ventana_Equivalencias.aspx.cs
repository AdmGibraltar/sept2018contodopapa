using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Data;

namespace SIANWEB
{
    public partial class Ventana_Equivalencias : System.Web.UI.Page
    {
        #region Variables
        private DataTable list
        {
            get { return (DataTable)Session["DtEquivalentesAcys" + Session.SessionID]; }
            set { Session["DtEquivalentesAcys" + Session.SessionID] = value; }
        }
        public DataTable dt
        {
            get
            {
                return (DataTable)Session["dt" + Session.SessionID];
            }
            set
            {
                Session["dt" + Session.SessionID] = value;
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
                {
                    CerrarVentana("");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        //if (list == null)
                        //{
                        list = new DataTable();

                        list.Columns.Add("Id");
                        list.Columns.Add("Descripcion");
                        list.Columns.Add("Disponible");
                        list.Columns.Add("Cantidad");
                        list.Columns.Add("Precio");
                        CN_CapAcys acys = new CN_CapAcys();
                        Acys a = new Acys();
                        a.Id_Emp = Sesion.Id_Emp;
                        a.Id_Cd = Sesion.Id_Cd_Ver;
                        a.Id_Acs = Convert.ToInt32(Request.QueryString["Id_Acs"]);
                        DataTable list2 = list;
                        acys.ConsultarReemplazos(a, Convert.ToInt32(Request.QueryString["Id_Prd"]), ref list2, Sesion.Emp_Cnx);
                        list = list2;
                        //}
                        RadGrid1.Rebind();

                        CN_CatProducto cn_catproducto = new CN_CatProducto();
                        Producto pr = new Producto();
                        int id_Prd;
                        for (int x = 0; x < RadGrid1.Items.Count; x++)
                        {
                            id_Prd = Convert.ToInt32((RadGrid1.Items[x]["Id"].FindControl("lblId") as Label).Text);
                            cn_catproducto.ConsultaProducto(ref pr, Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd_Ver, id_Prd);
                            (RadGrid1.Items[x]["Precio"].FindControl("txtPrecio") as RadNumericTextBox).DbValue = pr.Prd_Precio;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        protected void txtId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RadNumericTextBox cmbProd = sender as RadNumericTextBox;
                Producto prd = new Producto();
                CN_CatProducto cnProducto = new CN_CatProducto();
                int id_prd = Convert.ToInt32(cmbProd.Value.HasValue ? cmbProd.Value.Value : -1);

                DataRow[] Ar_Dr = list.Select("Id='" + id_prd + "'");
                if (Ar_Dr.Length > 0)
                {

                    this.Alerta("El producto ya esta incluido");

                    (sender as RadNumericTextBox).Text = "";

                    return;
                }


                try
                {
                    cnProducto.ConsultaProducto(ref prd, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, id_prd);
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, (sender as RadNumericTextBox).ClientID);
                    (cmbProd.Parent.FindControl("txtDescripcion") as RadTextBox).Text = "";
                    (cmbProd.Parent.FindControl("lblDisponibleEdit") as Label).Text = "";
                    (cmbProd.Parent.FindControl("txtPrecio") as RadNumericTextBox).Text = "";
                    (sender as RadNumericTextBox).Text = "";

                    return;
                }
                (cmbProd.Parent.FindControl("txtDescripcion") as RadTextBox).Text = prd.Prd_Descripcion;

                List<int> listInt = new List<int>();
                cnProducto.ConsultaProducto_Disponible(sesion.Id_Emp, sesion.Id_Cd_Ver, id_prd.ToString(), ref listInt, sesion.Emp_Cnx);

                (cmbProd.Parent.FindControl("lblDisponibleEdit") as Label).Text = listInt[2].ToString();

                CN_CatProducto cn_catproducto = new CN_CatProducto();
                Producto pr = new Producto();
                cn_catproducto.ConsultaProducto(ref pr, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, id_prd);
                (cmbProd.Parent.FindControl("txtPrecio") as RadNumericTextBox).Text = pr.Prd_Precio.ToString();

            }
            catch (Exception)
            {

                throw;
            }

        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    RadGrid1.DataSource = list;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }
        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                RadGrid1.Rebind();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, "RadGrid1_PageIndexChanged");
            }
        }
        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.CommandName.ToString() == "Select")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        if (Request.QueryString["Precio"] != null)
                        {
                            Session["Id_Buscar" + Session.SessionID] = RadGrid1.Items[item]["Id"].Text;
                            Session["Descripcion_Buscar" + Session.SessionID] = RadGrid1.Items[item]["Descripcion"].Text;
                            CerrarVentana("precio");
                        }
                        else
                        {
                            Session["Id_Buscar" + Session.SessionID] = RadGrid1.Items[item]["Id"].Text;
                            Session["Descripcion_Buscar" + Session.SessionID] = RadGrid1.Items[item]["Descripcion"].Text;
                            CerrarVentana("cliente");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (RadGrid1.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        PerformInsert(e);
                        break;
                    case "Update":
                        Update(e);
                        break;

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


                int Id_Prd_Original = Convert.ToInt32(Request.QueryString["Id_Prd"]);
                int Id_Prd = 0;
                int Cantidad = 0;
                double precio = 0;
                string Acs_Doc = "";
                string Acs_Dia = "";
                string Acs_DiaStr = "";


                DateTime rdFechaF = default(DateTime);
                DataRow[] Ar_dr = dt.Select("Id_Prd='" + Id_Prd_Original + "'");
                if (Ar_dr.Length > 0)
                {
                    Acs_Doc = Ar_dr[0]["Acs_Doc"].ToString();
                    Acs_Dia = Ar_dr[0]["Acs_Dia"].ToString();
                    Acs_DiaStr = Ar_dr[0]["Acs_DiaStr"].ToString();

                    try
                    {
                        rdFechaF = Convert.ToDateTime(Ar_dr[0]["Acs_FechaF"]);
                    }
                    catch (Exception)
                    {
                    }

                    //Ar_dr[0].Delete();
                    //dt.AcceptChanges();
                }

                int contador = 0;
                string cantidad = "";
                for (int x = 0; x < RadGrid1.Items.Count; x++)
                {
                    cantidad = (RadGrid1.Items[x].FindControl("txtCantidad") as RadNumericTextBox).Text;
                    if (cantidad != "0" && cantidad != "")
                    {
                        Cantidad = Convert.ToInt32((RadGrid1.Items[x].FindControl("txtCantidad") as RadNumericTextBox).Text);
                        Id_Prd = Convert.ToInt32((RadGrid1.Items[x].FindControl("lblId") as Label).Text);
                        precio = (RadGrid1.Items[x].FindControl("txtPrecio") as RadNumericTextBox).Text == "" ? 0 : Convert.ToDouble((RadGrid1.Items[x].FindControl("txtPrecio") as RadNumericTextBox).Text);

                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        CN_CatProducto cn_catproducto = new CN_CatProducto();
                        Producto pr = new Producto();
                        cn_catproducto.ConsultaProducto(ref pr, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Prd);
                        string Id_Cte = Request.QueryString["Id_Cte"].ToString();
                        cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(Id_Cte), sesion.Emp_Cnx);


                        dt.Rows.Add(new object[] { 
                                -1,
                                Id_Prd, 
                                pr.Prd_Descripcion, 
                                pr.Prd_Presentacion,
                                pr.Prd_UniNs,
                                pr.ventaMes[0],
                                pr.ventaMes[1],
                                pr.ventaMes[2],
                                Cantidad,
                                precio,
                                precio,
                                Cantidad * precio,
                                Acs_Doc,
                                rdFechaF,
                                0,
                                Acs_Dia,
                                Acs_DiaStr,
                                0,
                                0,
                                0
                                 });

                        CN_CapAcys cn_acys = new CN_CapAcys();
                        cn_acys.ModificarEquivalencia(Id_Prd, Id_Prd_Original, Request.QueryString["Id_Acs"].ToString(), 1, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx);
                        contador++;
                    }

                    if (Ar_dr.Length > 0 && contador > 0)
                    {
                        Ar_dr[0].Delete();
                        dt.AcceptChanges();
                    }
                }
                CerrarVentana("");
                list = null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem && !e.Item.IsInEditMode)
            {

                GridDataItem item = (GridDataItem)e.Item;
                ImageButton editButton = (ImageButton)item.FindControl("EditButton");
                editButton.Visible = false;
            }

        }
        #endregion
        #region Funciones
        private void CerrarVentana(string param)
        {
            try
            {
                string funcion = "CloseAndRebind('" + param + "')";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PerformInsert(GridCommandEventArgs e)
        {
            try
            {
                int Id = 0;
                string Descripcion = "";
                int Disponible = 0;
                int Cantidad = 0;
                double precio = 0;

                GridItem gi = e.Item;

                if (((RadNumericTextBox)gi.FindControl("txtId")).Text == "")
                {
                    e.Canceled = true;
                    this.Alerta("No se ha capturado un producto");
                    return;
                }

                Id = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtId")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("txtId")).Value.Value : -1);
                Descripcion = ((RadTextBox)gi.FindControl("txtDescripcion")).Text;
                Disponible = Convert.ToInt32(((Label)gi.FindControl("lblDisponibleEdit")).Text);
                Cantidad = ((RadNumericTextBox)gi.FindControl("txtCantidad")).Text != "" ? Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text) : 0;
                precio = ((RadNumericTextBox)gi.FindControl("txtPrecio")).Text != "" ? Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPrecio")).Text) : 0;

                DataRow[] Ar_Dr = list.Select("Id='" + Id + "'");
                if (Ar_Dr.Length > 0)
                {
                    e.Canceled = true;
                    this.Alerta("El producto ya esta incluido");
                    return;
                }

                Ar_Dr = dt.Select("Id_Prd='" + Id + "'");
                if (Ar_Dr.Length > 0)
                {
                    e.Canceled = true;
                    this.Alerta("El producto ya esta incluido");
                    return;
                }

                list.Rows.Add(new object[] { 
                Id, 
                Descripcion, 
                Disponible, 
                Cantidad,
                precio
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Update(GridCommandEventArgs e)
        {
            try
            {
                int Id = 0;
                string Descripcion = "";
                int Disponible = 0;
                int Cantidad = 0;
                double precio = 0;

                GridItem gi = e.Item;

                if (((RadNumericTextBox)gi.FindControl("txtId")).Text == "")
                {
                    e.Canceled = true;
                    this.Alerta("No se ha capturado un producto");
                    return;
                }

                Id = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtId")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("txtId")).Value.Value : -1);
                Descripcion = ((RadTextBox)gi.FindControl("txtDescripcion")).Text;
                Disponible = Convert.ToInt32(((Label)gi.FindControl("lblDisponibleEdit")).Text);
                Cantidad = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text);
                precio = ((RadNumericTextBox)gi.FindControl("txtPrecio")).Text != "" ? Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPrecio")).Text) : 0;

                DataRow[] Ar_dr = list.Select("Id='" + Id + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Id"] = Id;
                    Ar_dr[0]["Descripcion"] = Descripcion;
                    Ar_dr[0]["Disponible"] = Disponible;
                    Ar_dr[0]["Cantidad"] = Cantidad;
                    Ar_dr[0]["precio"] = precio;
                    Ar_dr[0].AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Comun> GetList()
        {
            try
            {
                List<Comun> List = new List<Comun>();
                if (Request.QueryString["Precio"] != null)
                {
                    Session["BuscarPrecio" + Session.SessionID] = null;
                    CN_CatCliente clsCatProveedores = new CN_CatCliente();
                    Sesion session2 = new Sesion();
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    Clientes prv = new Clientes();
                    prv.Id_Emp = session2.Id_Emp;
                    prv.Id_Cd = session2.Id_Cd_Ver;
                    prv.Id_Cte = Convert.ToInt32(Request.QueryString["cte"]);
                    clsCatProveedores.ConsultaPrecios(prv, session2.Emp_Cnx, ref List, null, null);
                }
                else
                {
                    CN_CatCliente clsCatProveedores = new CN_CatCliente();
                    Sesion session2 = new Sesion();
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    Clientes prv = new Clientes();
                    prv.Id_Emp = session2.Id_Emp;
                    prv.Id_Cd = session2.Id_Cd_Ver;
                    clsCatProveedores.ConsultaClientes(prv, session2.Emp_Cnx, ref List, null, null);
                }
                return List;
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
                Alerta(Message);
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
                Alerta("Error: [" + NombreFuncion + "] " + eme.Message.ToString());
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                Alerta("Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString());
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
    }
}