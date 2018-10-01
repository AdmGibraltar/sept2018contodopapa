using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using System.Data;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class wfrmDetalleClientesContactos : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ValidarSesion())
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();

                        if (session.Cu_Modif_Pass_Voluntario == false)
                        {
                            //RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }//CargarCentros();
                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RAM1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "1":
                        rg1.Rebind();
                        break;
                    case "2":
                        rg2.Rebind();
                        break;
                    case "3":
                        rg3.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //GENERALES
        protected void rg1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridItem gi = e.Item;
                switch (e.CommandName.ToString())
                {
                    case "Delete":
                        if (!_PermisoEliminar)
                        {
                            Alerta("No tiene permisos para eliminar");
                            return;
                        }
                        Int32 item = default(Int32);
                        item = e.Item.ItemIndex;

                        if (item >= 0)
                        {
                            if (EliminarDatosContacto(Convert.ToInt32(gi.Cells[12].Text)) > 0)                          
                                rg1.Rebind();     //CargarContactos(Convert.ToInt32(lblCte.Text), Convert.ToInt32(lblSeg.Text));                            
                        }
                        break;
                    case "Editar":
                        if (!_PermisoModificar)
                        {
                            Alerta("No tiene permisos para modificar");
                            return;
                        }
                        string Id_Con = gi.Cells[12].Text;
                        string Id_Pos = gi.Cells[8].Text;
                        RAM1.ResponseScripts.Add("AbrirVentana_Nuevos('E','" +
                            gi.Cells[2].Text + "','" +
                            lblCte.Text + "','" +
                            lblSeg.Text + "','" +
                            Id_Con + "','" +
                            Id_Pos + "','Estructura Gerencial','1')");
                        rg1.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg1_SortCommand(object source, GridSortCommandEventArgs e)
        {
            try
            {
                rg1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)                
                    this.rg1.DataSource = GetGenerales();               
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //COMPRAS
        protected void rg2_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridItem gi = e.Item;
                switch (e.CommandName.ToString())
                {
                    case "Delete":
                        if (!_PermisoEliminar)
                        {
                            Alerta("No tiene permisos para eliminar");
                            return;
                        }
                        Int32 item = default(Int32);
                        item = e.Item.ItemIndex;

                        if (item >= 0)
                        {
                            if (EliminarDatosContacto(Convert.ToInt32(gi.Cells[12].Text)) > 0)
                            {
                                rg2.Rebind();
                                //CargarContactos(Convert.ToInt32(lblCte.Text), Convert.ToInt32(lblSeg.Text));
                            }
                        }
                        break;
                    case "Editar":
                        if (!_PermisoModificar)
                        {
                            Alerta("No tiene permisos para modificar");
                            return;
                        }
                        string Id_Con = gi.Cells[12].Text;
                        string Id_Pos = gi.Cells[8].Text;
                        RAM1.ResponseScripts.Add("AbrirVentana_Nuevos('E','" +
                            gi.Cells[2].Text + "','" +
                            lblCte.Text + "','" +
                            lblSeg.Text + "','" +
                            Id_Con + "','" +
                            Id_Pos + "','Estructura de Compras','2')");
                        rg2.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg2_SortCommand(object source, GridSortCommandEventArgs e)
        {
            try
            {
                rg2.Rebind();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                    this.rg2.DataSource = GetCompras();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //INTERNOS
        protected void rg3_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridItem gi = e.Item;
                switch (e.CommandName.ToString())
                {
                    case "Delete":
                        if (!_PermisoEliminar)
                        {
                            Alerta("No tiene permisos para eliminar");
                            return;
                        }
                        Int32 item = default(Int32);
                        item = e.Item.ItemIndex;

                        if (item >= 0)
                        {
                            if (EliminarDatosContacto(Convert.ToInt32(gi.Cells[12].Text)) > 0)
                            {
                                rg3.Rebind();
                                //CargarContactos(Convert.ToInt32(lblCte.Text), Convert.ToInt32(lblSeg.Text));
                            }
                        }
                        break;
                    case "Editar":
                        if (!_PermisoModificar)
                        {
                            Alerta("No tiene permisos para modificar");
                            return;
                        }
                        string Id_Con = gi.Cells[12].Text;
                        string Id_Pos = gi.Cells[8].Text;
                        RAM1.ResponseScripts.Add("AbrirVentana_Nuevos('E','" +
                            gi.Cells[2].Text + "','" +
                            lblCte.Text + "','" +
                            lblSeg.Text + "','" +
                            Id_Con + "','" +
                            Id_Pos + "','Estructura de Usuarios Internos','3')");
                        rg2.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg3_SortCommand(object source, GridSortCommandEventArgs e)
        {
            try
            {
                rg3.Rebind();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg3_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                    this.rg3.DataSource = GetInternos();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void ibtnNuevoContacto_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }
                RAM1.ResponseScripts.Add("AbrirVentana_Nuevos('N','','" +
                   lblCte.Text + "','" +
                   lblSeg.Text + "','','','Estructura de Usuarios Internos')");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                ////txtClave.Text = Valor;
                Clientes cte = new Clientes();
                CN_CatCliente cn_catcliente = new CN_CatCliente();
                cte.Id_Emp = session.Id_Emp;
                cte.Id_Cd = session.Id_Cd_Ver;
                cte.Id_Cte = Convert.ToInt32(Request.QueryString["ID"]);
                cte.Id_Terr = Convert.ToInt32(Request.QueryString["Ter"]);
                cn_catcliente.ConsultaClienteTerritorio(ref cte, session.Emp_Cnx);

                txtCliente.Text = cte.Cte_NomComercial;

                txtUEN.Text = cte.Uen_Descripcion;
                txtSegmento.Text = cte.Seg_Descripcion;

                txtTerritorio.Text = cte.Ter_Nombre;

                lblCte.Text = cte.Id_Cte.ToString();
                lblSeg.Text = cte.Id_Seg.ToString();
                lblTer.Text = cte.Id_Terr.ToString();

                imgContactos.PostBackUrl = "wfrmDetalleCliente.aspx?ID=" + cte.Id_Cte.ToString() + "&Seg=" + cte.Id_Seg.ToString() + "&Ter=" + cte.Id_Terr.ToString();
                rg1.Rebind();
                rg2.Rebind();
                rg3.Rebind();
                //CargarContactos((int)cte.Id_Cte, (int)cte.Id_Seg);


                if (session.Id_TU != 2)
                {
                    txtCliente.Enabled = false;
                    txtSegmento.Enabled = false;
                    txtTerritorio.Enabled = false;
                    txtUEN.Enabled = false;
                    ibtnNuevoContacto.Visible = false;

                    rg1.Enabled = false;
                    rg2.Enabled = false;
                    rg3.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private object GetGenerales()
        {
            DataSet dsContactosClientes = new DataSet();
            CN_CatCliente cn_catcliente = new CN_CatCliente();
            Clientes cte = new Clientes();
            cte.Id_Cte = Convert.ToInt32(lblCte.Text);
            cte.Id_Seg = Convert.ToInt32(lblSeg.Text);
            cte.Id_Emp = session.Id_Emp;
            cte.Id_Cd = session.Id_Cd_Ver;

            cn_catcliente.ConsultaContactos(cte, ref dsContactosClientes, session.Emp_Cnx);

            return CargarContactosEstructura(dsContactosClientes.Tables[0], dsContactosClientes.Tables[1]);
        }
        private object GetCompras()
        {
            DataSet dsContactosClientes = new DataSet();
            CN_CatCliente cn_catcliente = new CN_CatCliente();
            Clientes cte = new Clientes();
            cte.Id_Cte = Convert.ToInt32(lblCte.Text);
            cte.Id_Seg = Convert.ToInt32(lblSeg.Text);
            cte.Id_Emp = session.Id_Emp;
            cte.Id_Cd = session.Id_Cd_Ver;

            cn_catcliente.ConsultaContactos(cte, ref dsContactosClientes, session.Emp_Cnx);

            return CargarContactosEstructura(dsContactosClientes.Tables[2], dsContactosClientes.Tables[3]);
        }
        private object GetInternos()
        {
            DataSet dsContactosClientes = new DataSet();
            CN_CatCliente cn_catcliente = new CN_CatCliente();
            Clientes cte = new Clientes();
            cte.Id_Cte = Convert.ToInt32(lblCte.Text);
            cte.Id_Seg = Convert.ToInt32(lblSeg.Text);
            cte.Id_Emp = session.Id_Emp;
            cte.Id_Cd = session.Id_Cd_Ver;

            cn_catcliente.ConsultaContactos(cte, ref dsContactosClientes, session.Emp_Cnx);

            return CargarContactosEstructura(dsContactosClientes.Tables[4], dsContactosClientes.Tables[5]);
        }
        private int EliminarDatosContacto(int Id_Con)
        {
            try
            {
                CN_CatCliente cn_catcliente = new CN_CatCliente();

                Contacto cont = new Contacto();
                cont.Id_Emp = session.Id_Emp;
                cont.Id_Cd = session.Id_Cd_Ver;
                cont.Id_Con = Id_Con;

                int verificador = 0;
                cn_catcliente.EliminarContacto(cont, ref verificador, session.Emp_Cnx);
                return verificador;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private bool ValidarSesion()
        {
            try
            {
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    //


                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);

                    return false;
                }
                return true;
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


                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);

                pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;


                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, session.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = session.Id_U;
                Permiso.Id_Cd = session.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, session.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;


                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private object CargarContactosEstructura(DataTable dtPosiciones, DataTable dtContactos)
        {
            try
            {
                int PosicionID = 0;
                if (dtPosiciones.Rows.Count != 0)
                {
                    for (int i = 0; i <= dtContactos.Rows.Count - 1; i++)
                    {
                        PosicionID = (int)dtContactos.Rows[i]["PosicionID"];
                        for (int j = 0; j <= dtPosiciones.Rows.Count - 1; j++)
                        {
                            if (PosicionID == (int)dtPosiciones.Rows[j]["PosicionID"])
                            {
                                dtPosiciones.Rows[j]["Nombres"] = dtContactos.Rows[i]["Nombres"];
                                dtPosiciones.Rows[j]["Apellidos"] = dtContactos.Rows[i]["Apellidos"];
                                dtPosiciones.Rows[j]["Telefono"] = dtContactos.Rows[i]["Telefono"];
                                dtPosiciones.Rows[j]["Correo"] = dtContactos.Rows[i]["Correo"];
                                dtPosiciones.Rows[j]["Celular"] = dtContactos.Rows[i]["Celular"];
                                dtPosiciones.Rows[j]["ContactoID"] = dtContactos.Rows[i]["ContactoID"];
                                break;
                            }
                            else
                            {
                                if ((int)dtContactos.Rows[i]["PosicionID"] == 0)
                                {
                                    DataRow dr = dtPosiciones.NewRow();
                                    dr["Nombres"] = dtContactos.Rows[i]["Nombres"];
                                    dr["Apellidos"] = dtContactos.Rows[i]["Apellidos"];
                                    dr["Telefono"] = dtContactos.Rows[i]["Telefono"];
                                    dr["Correo"] = dtContactos.Rows[i]["Correo"];
                                    dr["Celular"] = dtContactos.Rows[i]["Celular"];
                                    dr["ContactoID"] = dtContactos.Rows[i]["ContactoID"];
                                    dr["PosicionID"] = 0;
                                    dr["Posicion"] = dtContactos.Rows[i]["Posicion"];
                                    dtPosiciones.Rows.Add(dr);
                                    break;
                                }
                            }
                        }
                    }
                    return dtPosiciones;
                }
                return dtPosiciones;
            }
            catch (Exception)
            {
                throw;
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
                Alerta(Message);
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
                Alerta("Error: [" + NombreFuncion + "] " + eme.Message.ToString());
            }
            catch (Exception)
            {
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
    }
}