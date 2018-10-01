using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using CapaEntidad;
using CapaNegocios;
using CapaDatos;
using Telerik.Web.UI;
using System.Data;
namespace SIANWEB
{ 
    public partial class ConfiguracionRelacionGestor : System.Web.UI.Page
    {
        #region Variables
        private string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }
        private bool _PermisoGuardar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
        private bool _PermisoModificar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
        private bool _PermisoEliminar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
        private bool _PermisoImprimir
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
        private Sesion sesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
        }
        private List<RelacionGestor> list
        {
            get { return (List<RelacionGestor>)Session["ListaRelacionGestor" + Session.SessionID]; }
            set { Session["ListaRelacionGestor" + Session.SessionID] = value; }
        }
        private List<Comun> CentrosSeleccionados
        {
            get { return (List<Comun>)Session["CentrosSeleccionados" + Session.SessionID]; }
            set { Session["CentrosSeleccionados" + Session.SessionID] = value; }
        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //bool postback = (bool)Session["PostBackPagos" + Session.SessionID];
                if (sesion == null)
                    CerrarVentana();
                else
                {
                    if (!IsPostBack)
                    {
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }

                        Inicializar();

                        double ancho = 0;
                        foreach (GridColumn gc in rgClientes.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgClientes.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgClientes.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {

        }
        protected void rgClientes_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                try
                {
                    if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    {
                        int[] Id_Cds = CentrosSeleccionados.Select(Comun => Comun.Id).ToArray();
                        rgClientes.DataSource = list.Where(RelacionGestor => Id_Cds.Contains(Convert.ToInt32(RelacionGestor.Id_Cd))).ToList();
                    }
                }
                catch (Exception ex)
                {
                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgClientes_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                rgClientes.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgClientes_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Delete":
                        Delete(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void Delete(GridCommandEventArgs e)
        {
            //int rgGralId = 0;
            GridItem gi = e.Item;
            list.Remove(list.Where(RelacionGestor => RelacionGestor.GUID == gi.Cells[rgClientes.Columns.FindByUniqueName("GUID").OrderIndex].Text).ToArray()[0]);

            //DataRow[] Ar_dr;
            //DataRow dr;

            //rgGralId = Convert.ToInt32(((Label)gi.FindControl("lblGralId1")).Text);
            //Ar_dr = dtDet.Select("Pag_Numero='" + rgGralId + "'");
            //if (Ar_dr.Length > 0)
            //{
            //    Alerta("No se puede eliminar, ya existen detalles de esta ficha");
            //    return;
            //}
            //Ar_dr = dtGral.Select("rgGralId='" + rgGralId + "'");
            //if (Ar_dr.Length > 0)
            //{
            //    Ar_dr[0].Delete();
            //    dtGral.AcceptChanges();
            //}
            //for (int x = 0; x < dtGral.Rows.Count; x++)
            //{
            //    dr = dtGral.Rows[x];
            //    dr.BeginEdit();
            //    dr["rgGralId"] = x + 1;
            //    dr.AcceptChanges();
            //}
            //CalcularImporteFichas();
        }
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            RAM1.ResponseScripts.Add("popup(" + cmbCentro.SelectedValue + ");");
        }
        protected void imgAgregar_Click(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }
            RelacionGestor rg = new RelacionGestor();
            rg.Id_Emp = sesion.Id_Emp;
            rg.Id_Cd = cmbCentro.SelectedValue;
            rg.Cd_Nombre = cmbCentro.Text;
            rg.Id_Cte = txtCliente.Value;
            rg.Cte_Nombre = txtClienteNombre.Text;
            rg.Id_Ter = txtTerritorio.Value;
            rg.Ter_Nombre = cmbTerritorio.Text;
            rg.GUID = System.Guid.NewGuid().ToString();

            if (rg.Id_Ter == null)
            {
                if (list.Where(RelacionGestor => RelacionGestor.Id_Cd == rg.Id_Cd && RelacionGestor.Id_Cte == rg.Id_Cte &&  RelacionGestor.Id_Ter == null).ToList().Count > 0)
                {
                    Alerta("La relación ya está incluida");
                }
                else
                {
                    foreach (RelacionGestor rg1 in list.Where(RelacionGestor => RelacionGestor.Id_Cd == rg.Id_Cd && RelacionGestor.Id_Cte == rg.Id_Cte).ToList())
                    {
                        list.Remove(rg1);
                    }
                }
            }

            if (list.Where(RelacionGestor => RelacionGestor.Id_Cd == rg.Id_Cd && RelacionGestor.Id_Cte == rg.Id_Cte && (RelacionGestor.Id_Ter == rg.Id_Ter || RelacionGestor.Id_Ter == null)).ToList().Count > 0)
            {
                Alerta("La relación ya está incluida");
            }
            else
            {
                list.Add(rg);
                Limpiar();
                rgClientes.Rebind();
            }
            txtCliente.Focus();
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {

                switch (e.Argument.ToString())
                {
                    case "cliente":
                        txtCliente.Text = Session["Id_Buscar" + Session.SessionID].ToString();
                        txtCliente_TextChanged(null, null);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void cmbCentro_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Limpiar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            if (this.ConsultarDatosCliente(txtCliente.Text))
            {
                CargarComboTerritorios();
                txtTerritorio.Focus();
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
                {
                    funcion = "CloseWindow()";
                }
                else
                {
                    funcion = "CloseAndRebind()";
                }
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
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
                if (list == null)
                {
                    list = new List<RelacionGestor>();
                }

                cmbCentro.DataSource = CentrosSeleccionados;
                cmbCentro.DataBind();
                rgClientes.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ConsultarDatosCliente(string idCliente)
        {
            try
            {
                txtTerritorio.Text = "";
                txtClienteNombre.Text = "";

                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = Convert.ToInt32(cmbCentro.SelectedValue);
                cliente.Id_Cte = Convert.ToInt32(idCliente);
                try
                {
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtCliente.ClientID);
                    cmbTerritorio.DataSource = new List<Territorios>();
                    cmbTerritorio.DataBind();
                    cmbTerritorio.Text = "";
                    txtCliente.Text = "";

                    return false;
                }
                txtClienteNombre.Text = cliente.Cte_NomComercial;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarComboTerritorios()
        {
            try
            {
               
                Sesion sesion2 = new Sesion();
                sesion2.Id_Emp = sesion.Id_Emp;
                sesion2.Id_Cd_Ver = Convert.ToInt32(cmbCentro.SelectedValue);
                sesion2.Emp_Cnx = sesion.Emp_Cnx;
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                int cliente = !string.IsNullOrEmpty(txtCliente.Value.ToString()) ? Convert.ToInt32(txtCliente.Value.ToString()) : -1;

                List<Territorios> listaTerritorios = new List<Territorios>();
                new CN_CatCliente().ConsultaTerritoriosDelCliente(cliente, sesion2, ref listaTerritorios);

                Territorios[] tr = (listaTerritorios.Where(Territorios => Territorios.Id_Ter == -1).ToArray());
                if (tr.Length > 0)
                {
                    tr[0].Descripcion = "-- Todos --";
                }
                cmbTerritorio.DataTextField = "Descripcion";
                cmbTerritorio.DataValueField = "Id_Ter";
                cmbTerritorio.DataSource = listaTerritorios;
                cmbTerritorio.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Limpiar()
        {
            try
            {
                txtCliente.Text = "";
                txtClienteNombre.Text = "";
                txtTerritorio.Text = "";
                cmbTerritorio.DataSource = new List<Territorios>();
                cmbTerritorio.DataBind();
                cmbTerritorio.Text = "";
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
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 340, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta2(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 600, 150);");
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