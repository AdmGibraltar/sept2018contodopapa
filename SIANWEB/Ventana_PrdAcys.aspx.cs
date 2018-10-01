using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Collections;
using System.Data;

namespace SIANWEB
{
    public partial class Ventana_PrdAcys : System.Web.UI.Page
    {
        private DataTable Seleccionados
        {
            get { return (DataTable)Session["SeleccionadosAcys" + Session.SessionID]; }
            set { Session["SeleccionadosAcys" + Session.SessionID] = value; }
        }
        private List<Comun> list
        {
            get { return (List<Comun>)Session["ListEquivalentesAcys" + Session.SessionID]; }
            set { Session["ListEquivalentesAcys" + Session.SessionID] = value; }
        }
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
                        if (Seleccionados == null)
                        {
                            Seleccionados = new DataTable();

                            Seleccionados.Columns.Add("Id_Original");
                            Seleccionados.Columns.Add("Id_Similar");
                            Seleccionados.Columns.Add("Prd_Descripcion");
                            Seleccionados.Columns.Add("Seleccionado");
                        }

                        DataRow[] dr = Seleccionados.Select("Id_Original='" + Request.QueryString["Id_Prd"].ToString() + "'");
                        list = new List<Comun>();
                        if (dr.Length == 0)
                        {
                            CN_CatProducto clsCatProducto = new CN_CatProducto();
                            Sesion session2 = new Sesion();
                            session2 = (Sesion)Session["Sesion" + Session.SessionID];

                            Producto prd = new Producto();
                            prd.Id_Emp = session2.Id_Emp;
                            prd.Id_Cd = session2.Id_Cd_Ver;
                            prd.Id_Prd = Convert.ToInt32(Request.QueryString["Id_Prd"]);
                            List<Comun> list2 = new List<Comun>();
                            clsCatProducto.ConsultaListaProductoAgrupador(prd, Convert.ToInt32(Request.QueryString["Id_Acs"]), session2.Emp_Cnx, ref list2);
                            list = list2;
                        }
                        else
                        {
                            Comun cm;
                          
                            foreach (DataRow dr1 in dr)
                            {
                                cm = new Comun();
                                cm.IdStr = dr1[1].ToString();
                                cm.Descripcion = dr1[2].ToString();
                                cm.ValorBool = Convert.ToBoolean(dr1[3]);
                                list.Add(cm);
                            }
                        }

                        RadGrid1.Rebind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Page_Load");
            }
        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    RadGrid1.DataSource = GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }

        //protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        //{
        //    try
        //    {
        //        foreach (GridDataItem i in RadGrid1.Items)
        //        {
        //            if ((i.FindControl("ChkSeleccionar") as CheckBox).Checked)
        //            {

        //                Seleccionados.Add(i["Id"].Text);
        //            }
        //            else
        //            {
        //                Seleccionados.Remove(i["Id"].Text);
        //            }
        //        }

        //        RadGrid1.Rebind();
        //    }
        //    catch (Exception ex)
        //    {

        //        ErrorManager(ex, "RadGrid1_PageIndexChanged");
        //    }
        //}

        private List<Comun> GetList()
        {
            try
            {
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CerrarVentana(string param)
        {
            try
            {
                string funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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


        //protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridDataItem)
        //    {
        //        GridDataItem item = (GridDataItem)e.Item;

        //        CheckBox chk = item.FindControl("ChkSeleccionar") as CheckBox;

        //        chk.Checked = Seleccionados.Contains( item["Id"].Text);

        //    }
        //}

        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {


            DataRow[] dr = Seleccionados.Select("Id_Original='" + Request.QueryString["Id_Prd"].ToString() + "'");
            foreach (DataRow dr2 in dr)
            {
                Seleccionados.Rows.Remove(dr2);
            }

            foreach (GridDataItem i in RadGrid1.Items)
            {
                Seleccionados.Rows.Add(new object[] { Convert.ToInt32(Request.QueryString["Id_Prd"]), (i.FindControl("lblId") as Label).Text, (i.FindControl("lblDescripcion") as Label).Text, (i.FindControl("ChkSeleccionar") as CheckBox).Checked });
            }

            CerrarVentana("");
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

                try
                {
                    cnProducto.ConsultaProducto(ref prd, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, id_prd);
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, (sender as RadNumericTextBox).ClientID);
                    (cmbProd.Parent.FindControl("txtDescripcion") as RadTextBox).Text = "";
                    (sender as RadNumericTextBox).Text = "";
                    return;
                }
                (cmbProd.Parent.FindControl("txtDescripcion") as RadTextBox).Text = prd.Prd_Descripcion;

            }
            catch (Exception)
            {

                throw;
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
                        //Update(e);
                        break;

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void PerformInsert(GridCommandEventArgs e)
        {
            try
            {
                int Id = 0;
                string Descripcion = "";
                bool seleccionado = false;

                GridItem gi = e.Item;

                if (((RadNumericTextBox)gi.FindControl("txtId")).Text == "")
                {
                    e.Canceled = true;
                    this.Alerta("No se ha capturado un producto");
                    return;
                }

                Id = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtId")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("txtId")).Value.Value : -1);
                Descripcion = ((RadTextBox)gi.FindControl("txtDescripcion")).Text;
                seleccionado = ((CheckBox)gi.FindControl("ChkSeleccionarEdit")).Checked;

                foreach (Comun c in list)
                {
                    if (c.IdStr == Id.ToString())
                    {
                        e.Canceled = true;
                        this.Alerta("El producto ya esta incluido");
                        return;
                    }
                }

                Comun com = new Comun();
                com.IdStr = Id.ToString();
                com.Descripcion = Descripcion;
                com.ValorBool = seleccionado;
                list.Add(com);

            }
            catch (Exception ex)
            {
                throw ex;
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
        //private void Update(GridCommandEventArgs e)
        //{
        //    try
        //    {
        //        int Id = 0;
        //        string Descripcion = "";
        //        int Disponible = 0;
        //        int Cantidad = 0;
        //        double precio = 0;

        //        GridItem gi = e.Item;

        //        if (((RadNumericTextBox)gi.FindControl("txtId")).Text == "")
        //        {
        //            e.Canceled = true;
        //            this.Alerta("No se ha capturado un producto");
        //            return;
        //        }

        //        Id = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtId")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("txtId")).Value.Value : -1);
        //        Descripcion = ((RadTextBox)gi.FindControl("txtDescripcion")).Text;
        //        Disponible = Convert.ToInt32(((Label)gi.FindControl("lblDisponibleEdit")).Text);
        //        Cantidad = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text);
        //        precio = ((RadNumericTextBox)gi.FindControl("txtPrecio")).Text != "" ? Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPrecio")).Text) : 0;

        //        DataRow[] Ar_dr = list.Select("Id='" + Id + "'");
        //        if (Ar_dr.Length > 0)
        //        {
        //            Ar_dr[0].BeginEdit();
        //            Ar_dr[0]["Id"] = Id;
        //            Ar_dr[0]["Descripcion"] = Descripcion;
        //            Ar_dr[0]["Disponible"] = Disponible;
        //            Ar_dr[0]["Cantidad"] = Cantidad;
        //            Ar_dr[0]["precio"] = precio;
        //            Ar_dr[0].AcceptChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}