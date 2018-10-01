using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using System.IO;
using System.Runtime.Remoting;
using System.Text;

namespace SIANWEB
{
    public partial class CapRutaServicio : System.Web.UI.Page
    {
        #region Variables
                private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
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
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
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
                Sesion sesion = new Sesion(); 
                sesion = (Sesion)Session["Sesion" + Session.SessionID];    
                if (sesion == null)                
                {                    
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);              
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx" , false);              
                }             
                CN__Comun comun = new CN__Comun();    
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Inicializar();
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
                if (btn.CommandName == "save")
                {
                    if (Page.IsValid)
                    {
                        Guardar();
                    }
                }
                else if (btn.CommandName == "new")
                {
                    //Nuevo();
                }
                else if (btn.CommandName == "undo")
                {
                    //Regresar()
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgServicio.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }
        protected void rgServicio_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                
                if (e.CommandName.ToString() == "Modificar")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        CN__Comun.RemoverValidadores(Validators);
                        HF_ID.Value = rgServicio.Items[item]["Id_Cap"].Text;
                        txtCliente.Text = rgServicio.Items[item]["Id_Cliente"].Text;
                        txtAparatos.Text = rgServicio.Items[item]["Aparatos"].Text;
                        txtFechaRev.SelectedDate = Convert.ToDateTime(rgServicio.Items[item]["Fecha"].Text);
                        txtRuta.Text = rgServicio.Items[item]["Id_Ruta"].Text.Replace("&nbsp;", string.Empty);
                        //cmbCliente.SelectedIndex = cmbCliente.FindItemIndexByValue(rgServicio.Items[item]["Id_Cliente"].Text);
                        //cmbCliente.Text = cmbCliente.FindItemByValue(rgServicio.Items[item]["Id_Cliente"].Text).Text;
                        txtClienteDescripcion.Text = rgServicio.Items[item]["Cte_NomComercial"].Text;
                        cmbRuta.SelectedIndex = cmbRuta.FindItemIndexByValue(rgServicio.Items[item]["Id_Ruta"].Text);
                        cmbRuta.Text = cmbRuta.FindItemByValue(rgServicio.Items[item]["Id_Ruta"].Text).Text;                       
                    }
                }
                  if (e.CommandName.ToString() == "Eliminar") 
                  {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                            
                    if (_PermisoEliminar)
                    {
                        int Id_Cap = Convert.ToInt32(rgServicio.Items[item]["Id_Cap"].Text);
                        int Id_Cliente = Convert.ToInt32(rgServicio.Items[item]["Id_Cliente"].Text);
                        int Id_Ruta = Convert.ToInt32(rgServicio.Items[item]["Id_Ruta"].Text.Replace("&nbsp;", string.Empty));
                        this.EliminarRuta(Id_Cap, Id_Cliente, Id_Ruta);
                        this.rgServicio.Rebind();
                        Alerta("El registro ha sido eliminado exitosamente");
                        
                    }
                    else 
                    {
                        Alerta("No tiene permisos para eliminar");
                    }
                               
                    }   
                           
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgServicio_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgServicio.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {               
                rgServicio.Rebind();
                CargarRuta();
                //CargarClientes();
                Nuevo();
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
        //private void CargarClientes()
        //{
        //    try
        //    {
        //        Sesion Sesion = new Sesion();
        //        Sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //        CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
        //        CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatCliente_Combo", ref cmbCliente);                
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        private void CargarRuta()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatRutaSer_Combo", ref cmbRuta);                              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EliminarRuta(int Id_Cap, int Id_Cliente, int Id_Ruta)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            RutaServicio ruta = new RutaServicio();
            int verificador = 0;
            
            ruta.Id_Cap = Id_Cap;
            ruta.Id_Cliente = Id_Cliente;
            ruta.Id_Ruta = Id_Ruta;           
           
            CN_CapRutaServicio clsCapRutaServicio = new CN_CapRutaServicio();
            clsCapRutaServicio.EliminarCapRutaServicio(sesion.Id_Emp, sesion.Id_Cd_Ver, ruta, sesion.Emp_Cnx, ref verificador);
        }

        private void Nuevo()
        {
            try
            {
                txtCliente.Text = string.Empty;               
                txtAparatos.Text = string.Empty;
                txtRuta.Text = string.Empty;                
                HF_ID.Value = string.Empty;
                txtFechaRev.Clear();
                CargarRuta();
                //CargarClientes();
                //cmbCliente.SelectedIndex = 0;
                //cmbCliente.Text = cmbCliente.Items[0].Text;
                txtClienteDescripcion.Text = "";
                cmbRuta.SelectedIndex = 0;
                cmbRuta.Text = cmbRuta.Items[0].Text;                                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Guardar()
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                RutaServicio ruta = new RutaServicio();
                ruta.Id_Cliente = Convert.ToInt32(txtCliente.Text);
                ruta.Aparatos = Convert.ToInt32(txtAparatos.Text);
                ruta.Fecha = txtFechaRev.SelectedDate.Value;
                ruta.Id_Ruta = !string.IsNullOrEmpty(txtRuta.Text) ? Convert.ToInt32(txtRuta.Text) : 0;
                if (ruta.Fecha > DateTime.Now)
                {
                    Alerta("La fecha no debe ser mayor al día de hoy");
                    return;
                }
                else
                {
                    CN_CapRutaServicio clsCapRutaServicio = new CN_CapRutaServicio();
                    int verificador = -1;

                    if (string.IsNullOrEmpty(HF_ID.Value))
                    {
                        if (!_PermisoGuardar)
                        {
                            Alerta("No tiene permisos para grabar");
                            return;
                        }
                        ruta.Id_Cap = !string.IsNullOrEmpty(Valor) ? Convert.ToInt32(Valor) : 1;
                        clsCapRutaServicio.InsertarCapRutaServicio(session.Id_Emp, session.Id_Cd_Ver, session.Id_U, ruta, session.Emp_Cnx, ref verificador);
                        if (verificador == 1)
                        {
                            Alerta("Los datos se guardaron correctamente");
                            Nuevo();
                        }
                        else
                        {
                            Alerta("La clave ya existe1");
                        }
                    }
                    else
                    {
                        if (!_PermisoModificar)
                        {
                            Alerta("No tiene permisos para modificar");
                            return;
                        }
                        ruta.Id_Cap = Convert.ToInt32(HF_ID.Value);
                        clsCapRutaServicio.ModificarCapRutaServicio(session.Id_Emp, session.Id_Cd_Ver, session.Id_U, ruta, session.Emp_Cnx, ref verificador);
                        if (verificador == 1)
                        {
                            Alerta("Los datos se modificaron correctamente");
                            Nuevo();
                        }
                        else
                        {
                            Alerta("Los datos no se pudieron modificar");
                        }
                    }
                    rgServicio.Rebind();
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
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                }
                else
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                }
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
                    {
                        this.rtb1.Items[0].Enabled = false;
                    }

                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }

                //CN_Ctrl ctrl = new CN_Ctrl();
                //ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                //ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<RutaServicio> GetList()
        {
            try
            {
                List<RutaServicio> List = new List<RutaServicio>();
                CN_CapRutaServicio clsCapRutaServicio = new CN_CapRutaServicio();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                clsCapRutaServicio.ConsultaCapRutaServicio(session2.Id_Emp, session2.Id_Cd_Ver, session2.Emp_Cnx, ref List);
                return List;
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
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd, "capRutaServicio", "Id_Crs", Sesion.Emp_Cnx, "spCatLocal_Maximo");
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

        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (!txtCliente.Value.HasValue)
                {
                    txtClienteDescripcion.Text = "";
                    return;
                }
                Clientes cte = new Clientes();
                cte.Id_Emp = Sesion.Id_Emp;
                cte.Id_Cd = Sesion.Id_Cd_Ver;
                //cte.Id_Cte = Convert.ToInt32(cmbCliente.SelectedValue);
                cte.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                CN_CatCliente catcliente = new CN_CatCliente();
                try
                {
                    catcliente.ConsultaClientes(ref cte, Sesion.Emp_Cnx);
                    txtClienteDescripcion.Text = cte.Cte_NomComercial;
                    txtAparatos.Focus();
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtCliente.ClientID);
                    txtCliente.Text = "";
                    txtClienteDescripcion.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
    }
}