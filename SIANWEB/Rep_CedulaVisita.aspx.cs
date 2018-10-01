using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.IO;
using System.Collections;
using Telerik.Reporting.Processing;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Web;
using System.Data;

namespace SIANWEB
{
    public partial class Rep_CedulaVisita : PaginaBase
    {
        #region Variables
        public List<Acys> ListaAcysAgregados
        {
            get 
            {
                return (List<Acys>)ViewState["ListaAcysAgregados"]; 
            }
            set 
            {
                ViewState["ListaAcysAgregados"] = value; 
            }
        }
                        
        public bool _PermisoImprimir
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
            set
            {
                Session["Sesion" + Session.SessionID] = value;
            }
        }

        Func<int, RepVentas, double> obtieneMontoPorMes = (month, rVentas) =>
        {                                    
            PropertyInfo vProps = rVentas.GetType().GetProperty(string.Format("Mes{0}", month));
            return (double)vProps.GetValue(rVentas, null);
        };

        static string[] months = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

        static Func<int, string> obtieneMes = (month) => months[month-1];
        
        #endregion Variables
        #region Eventos

        public void rgAgregar_ItemCommand(object source, GridCommandEventArgs e)
        {
            RadGrid grid = source as RadGrid;
            GridDataItem gi = null;

            if (e.Item.ItemIndex > -1)
            {
                gi = grid.Items[e.Item.ItemIndex];

                switch (e.CommandName.ToUpper())
                {
                    case "CANCELAR":
                        eliminarRegistro(gi);
                        break;
                }
            }
        }

        public void rgAgregar_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            RadGrid grd = source as RadGrid;

            try
            {
                grd.DataSource = ListaAcysAgregados;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
                
        public void cmbAcys_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            if (e.Item.DataItem is Acys)
            {
                Acys a = e.Item.DataItem as Acys;
                e.Item.Text = string.Format("{0} - {1}", a.Acs_RscTerritorio, a.Acs_Territorio);
                e.Item.Value = ((Acys)e.Item.DataItem).Id_Acs.ToString();
            }
        }

        public void cmbAcys_DataBound(object sender, EventArgs e)
        {
            RadComboBox cmb = sender as RadComboBox;
            ((Literal)cmb.Footer.FindControl("RadComboItemsCountAcys")).Text = Convert.ToString(cmb.Items.Count);
        }
                
        public void cmbDireccion_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            if (e.Item.DataItem is ClienteDirEntrega)
            {
                e.Item.Text = ((ClienteDirEntrega)e.Item.DataItem).Direccion;
                e.Item.Value = ((ClienteDirEntrega)e.Item.DataItem).Id_CteDirEntrega.ToString(); 
            }
        }

        public void cmbDireccion_DataBound(object sender, EventArgs e)
        {
            RadComboBox cmb = sender as RadComboBox;
            ((Literal)cmb.Footer.FindControl("RadComboItemsCount")).Text = Convert.ToString(cmb.Items.Count);
        }

        public void txtCliente_TextChanged(object sender, EventArgs e)
        {
            RadNumericTextBox txt = sender as RadNumericTextBox;
            CN_CatCliente cnCliente = new CN_CatCliente();
            gSession = (Sesion)Session["Sesion" + Session.SessionID];
            int idRik = 0;

            try
            {
                ErrorManager();
                Clientes cte = new Clientes()
                {
                    Id_Emp = gSession.Id_Emp,
                    Id_Cd = gSession.Id_Cd_Ver,
                    Id_Cte = Convert.ToInt32(txt.Value)
                };

                Int32.TryParse(txtAsesor.Text, out idRik);
                cnCliente.ConsultaClientes(ref cte, gSession.Emp_Cnx);
                txtClienteNombre.Text = cte.Cte_NomComercial;
                CargarTerritorio(cte.Id_Cte.Value, idRik);
                CargarDirecciones(cte.Id_Cte.Value);
            }
            catch (Exception ex)
            {
                txtNumCliente.Value = null;
                txtClienteNombre.Text = string.Empty;
                cmbAcys.Items.Clear();
                cmbAcys.Text = string.Empty;
                cmbDireccion.Items.Clear();
                cmbDireccion.Text = string.Empty;
                Alerta(ex.Message);
            }            
        }

        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }

        public void cmbAsesor_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bool enabled = true;
            int vValue = 0;
                        
            enabled = (Int32.TryParse(e.Value, out vValue) && vValue > 0);

            txtNumCliente.Text = string.Empty;
            txtClienteNombre.Text = string.Empty;

            cmbAcys.DataSource = new List<Acys>();
            cmbDireccion.DataSource = new List<ClienteDirEntrega>();

            cmbAcys.Text = string.Empty;
            cmbDireccion.Text = string.Empty;

            cmbAcys.DataBind();
            cmbDireccion.DataBind();

            imgBuscar.Enabled = enabled;
            txtNumCliente.Enabled = enabled;
            cmbAcys.Enabled = enabled;
            cmbDireccion.Enabled = enabled;
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {                    
                    case "cliente":
                        txtNumCliente.Value = Convert.ToInt32(Session["Id_Buscar" + Session.SessionID].ToString());
                        txtCliente_TextChanged(txtNumCliente, new EventArgs());
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }

        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                if (!_PermisoImprimir)
                {
                    this.Alerta("No tiene permisos para ver el reporte");
                    return;
                }
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                                
                if (Page.IsValid)
                {
                    if (btn.CommandName == "print")
                    {
                        this.Imprimir();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.GlobalRAM = RAM1;
            this.LabelMensaje = lblMensaje;                        
            base.OnInit(e);
        }
                                
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {               
                if (!Page.IsPostBack)
                {
                    this.ListaAcysAgregados = new List<Acys>();
                    this.ValidarPermisos();
                    if (gSession.Cu_Modif_Pass_Voluntario == false)
                    {
                        RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                        return;
                    }
                    this.CargarCentros();
                    this.CargarRik(2);
                    cmbDireccion.Enabled = false;
                    cmbAcys.Enabled = false;
                    txtClienteNombre.Enabled = false;
                    Random randObj = new Random(DateTime.Now.Millisecond);
                    HF_Cve.Value = randObj.Next().ToString();                    
                }
                
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {                
                RAM1.ResponseScripts.Add("popup(false);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
            }
            CN__Comun comun = new CN__Comun();
            comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            //nuevo();
        }

        public void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNumCliente.Value > 0)
            {
                Acys enAcys = obtieneAcys(txtNumCliente.Value.ToString(), cmbAcys.SelectedValue, cmbDireccion.SelectedValue, cmbAcys.Text);

                if (!ListaAcysAgregados.Any(x => x.Id_Cte == enAcys.Id_Cte &&
                                                 x.Id_Acs == enAcys.Id_Acs &&
                                                 x.IdCte_DirEntrega == enAcys.IdCte_DirEntrega))
                {
                    ListaAcysAgregados.Add(enAcys);
                    rgAgregar.Rebind();
                }
                else
                {
                    Alerta("El territorio ya fue agregado a la lista de impresión.");
                }
            }
        }

        #endregion Eventos        
        #region Metodos        

        private Acys obtieneAcys(string pIdCte, string pIdAcs, string pIdDir, string pTerritorio)
        {
            CN_CapAcys cnCapAcys = new CN_CapAcys();
            CN_CatCliente cnCatCte = new CN_CatCliente();
            int vIdCte = 0;
            int vIdAcs = 0;
            int vIdDir = 0;

            if (!Int32.TryParse(pIdCte, out vIdCte))
            {
                Alerta("Debe capturar un número de cliente para continuar.");
                return null;
            }

            if (!Int32.TryParse(pIdAcs, out vIdAcs))
            {
                Alerta("Debe seleccionar un territorio valido.");
                return null;
            }

            if (!Int32.TryParse(pIdDir, out vIdDir))
            {
                Alerta("Debe seleccionar una dirección de entrega.");
                return null;
            }

            if (vIdCte == 0 || vIdAcs == 0)
            {
                Alerta("El Acuerdo Comercial y de Servicio no es válido.");
                return null;
            }
            
            ClienteDirEntrega cde = new ClienteDirEntrega()
            {
                Id_Emp = gSession.Id_Emp,
                Id_Cd = gSession.Id_Cd_Ver,
                Id_Cte = vIdCte,
                Id_CteDirEntrega = vIdDir
            };

            cnCatCte.ConsultaClienteDirEntrega(cde, gSession.Emp_Cnx);

            Func<ClienteDirEntrega, string> formateaDireccion = (cDirEn) =>
                string.Format("{0} No. {1}, C.P. {2}, {3}, {4}, {5}", cDirEn.Cte_Calle.Trim(), cDirEn.Cte_Numero.Trim(), cDirEn.Cte_Cp.Trim(), cDirEn.Cte_Colonia.Trim(), cDirEn.Cte_Municipio.Trim(), cDirEn.Cte_Estado.Trim());

            Acys enAcys = new Acys()
            {
                Id_Emp = gSession.Id_Emp,
                Id_Cd = gSession.Id_Cd_Ver,
                Id_Acs = vIdAcs                
            };

            cnCapAcys.ConsultaUltimaVersion(ref enAcys, gSession.Emp_Cnx);
            enAcys.Id_Cte = vIdCte;
            cnCapAcys.CedVis_Consultar(ref enAcys, gSession.Emp_Cnx);
            enAcys.Id_AcsVersion = enAcys.Acs_Version;                        
            enAcys.IdCte_DirEntrega = vIdDir;
            enAcys.DireccionEntrega = formateaDireccion(cde);

            return enAcys;
        }

        private void eliminarRegistro(GridDataItem gi)
        {
            Acys enAcys = obtieneAcys(gi["Id_Cte"].Text, gi["Id_Acs"].Text, gi["IdCte_DirEntrega"].Text, string.Empty);

            if (enAcys != null)
            {
                ListaAcysAgregados.RemoveAll(x => x.Id_Cte == enAcys.Id_Cte &&
                                             x.Id_Acs == enAcys.Id_Acs &&
                                             x.IdCte_DirEntrega == enAcys.IdCte_DirEntrega);
                rgAgregar.Rebind();
            }            
        }

        private void ValidarPermisos()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                CN_Pagina capaNegocio = new CN_Pagina();
                capaNegocio.PaginaConsultar(ref pagina, sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;

                Permiso permiso = new Permiso();
                permiso.Id_U = sesion.Id_U;
                permiso.Id_Cd = sesion.Id_Cd;
                permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU cNPermisosU = new CapaDatos.CD_PermisosU();
                cNPermisosU.ValidaPermisosUsuario(ref permiso, sesion.Emp_Cnx);

                if (permiso.PAccesar == true)
                    _PermisoImprimir = permiso.PImprimir;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Imprimir()
        {
            try
            {
                gSession = (Sesion)Session["Sesion" + Session.SessionID];

                Dictionary<int, ReportInstances> rptInstances = new Dictionary<int, ReportInstances>();
                CN_CapAcys cnCapAcys = new CN_CapAcys();
                CN_CatCliente cnCatCte = new CN_CatCliente();
               
                Acys eAcys = null;
                
                
                ArrayList vALValorParametrosInternos = new ArrayList();
                Type instance = null;
                               
                int vCount = 0;                                                            
                
                foreach (Acys itemAcys in ListaAcysAgregados)
                {                        
                    eAcys = itemAcys;

                    eAcys.Id_Emp = itemAcys.Id_Emp;
                    eAcys.Id_Cd = itemAcys.Id_Cd;
                    eAcys.Id_Acs = itemAcys.Id_Acs;
                    eAcys.Id_AcsVersion = itemAcys.Id_AcsVersion;
                                                                                            
                    vALValorParametrosInternos = new ArrayList();
                                        
                    /*   PARAMETROS OBLIGATORIOS PARA EL REPORTE   */
                    vALValorParametrosInternos.Add(eAcys.Id_Emp); //Id_Emp
                    vALValorParametrosInternos.Add(eAcys.Id_Cd); //Id_Cd
                    vALValorParametrosInternos.Add(eAcys.Id_Acs); //Id_Acs
                    vALValorParametrosInternos.Add(eAcys.Id_AcsVersion); //Id_AcsVersion                    
                    vALValorParametrosInternos.Add(gSession.Emp_Cnx); //Conexion

                    /*  PARAMETROS PARA EL ENCABEZADO DEL REPORTE   */
                    vALValorParametrosInternos.Add(eAcys.Id_Cte); //Id_Cte
                    vALValorParametrosInternos.Add(eAcys.Acs_NomComercial); //Cte_Nombre
                    vALValorParametrosInternos.Add(eAcys.Acs_Contacto); //Contacto
                    vALValorParametrosInternos.Add(eAcys.Acs_Telefono); //Telefono
                    vALValorParametrosInternos.Add(eAcys.Acs_RscNombre); //Nom_RSC
                    vALValorParametrosInternos.Add(eAcys.DireccionEntrega); //Direccion
                    vALValorParametrosInternos.Add(eAcys.Acs_RikNombre); //Nom_RIK
                    vALValorParametrosInternos.Add(eAcys.Vis_Frecuencia); //VisitaMes
                    vALValorParametrosInternos.Add(string.Empty); //Sector
                    vALValorParametrosInternos.Add(eAcys.Id_Ter); //Territorio
                    vALValorParametrosInternos.Add(string.Format("{0:C}", eAcys.VentaProm)); //VentaPromUTrim
                    vALValorParametrosInternos.Add(string.Format("{0:C}", eAcys.VentaInst)); //VIMensAcys
                    vALValorParametrosInternos.Add(obtieneMes(gSession.CalendarioIni.Month)); //Mes
                              
                    instance = typeof(LibreriaReportes.CedulaVisita_RSC_Asesor);
                    vCount++;
                    rptInstances.Add(vCount, new ReportInstances() { ReportInstance = instance.AssemblyQualifiedName, Parameters = vALValorParametrosInternos});
                    vCount++;
                    rptInstances.Add(vCount, new ReportInstances() { ReportInstance = typeof(LibreriaReportes.CedulaVisita_RSC_Asesor_F01).AssemblyQualifiedName, Parameters = null });
                    vCount++;
                    rptInstances.Add(vCount, new ReportInstances() { ReportInstance = typeof(LibreriaReportes.CedulaVisita_RSC_Asesor_F02).AssemblyQualifiedName, Parameters = null });                                                                        
                        
                    eAcys = null;
                }
                                       
                Session["InternParameter_Values" + Session.SessionID] = vALValorParametrosInternos;
                Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;
                Session["assemblies" + Session.SessionID] = rptInstances;
                RAM1.ResponseScripts.Add("AbrirReporte(" + HF_Cve.Value + ");");                                                  
                    
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
                Sesion vSesion = new Sesion();
                vSesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun cNComun = new CapaNegocios.CN__Comun();


                if (vSesion.U_MultiOfi == false)
                {
                    cNComun.LlenaCombo(2, vSesion.Id_Emp, vSesion.Id_U, vSesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(vSesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    cNComun.LlenaCombo(1, vSesion.Id_Emp, vSesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, vSesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = vSesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarDirecciones(int pIdCliente)
        {
            try
            {                
                gSession = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun cNComun = new CapaNegocios.CN__Comun();
                Dictionary<string, object> iParameters = new Dictionary<string, object>();

                iParameters.Add("@Id1", gSession.Id_Emp);
                iParameters.Add("@Id2", gSession.Id_Cd_Ver);
                iParameters.Add("@Id3", pIdCliente);

                cmbDireccion.Items.Clear();
                if (cmbAcys.Items.Count > 0)
                {
                    cNComun.LlenaCombo<ClienteDirEntrega>(iParameters, gSession.Emp_Cnx, "spCatClienteDirEntrega_Combo", ref cmbDireccion, "Id_CteDirEntrega", "Direccion");
                }
                this.cmbDireccion.SelectedIndex = 0;
                cmbDireccion.Enabled = (cmbDireccion.Items.Count > 1);
                if (!cmbDireccion.Enabled)
                    cmbDireccion.Text = string.Empty;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarTerritorio(int pIdCliente, int pIdRik)
        {
            bool vFlag = false;

            try
            {
                gSession = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun cNComun = new CapaNegocios.CN__Comun();
                CapaNegocios.CN_CapAcys cnAcys = new CN_CapAcys();

                List<Acys> acysList = new List<Acys>();
                              
                Acys vAcys = new Acys()
                {
                    Id_Emp = gSession.Id_Emp,
                    Id_Cd = gSession.Id_Cd_Ver,
                    Id_Cte = pIdCliente,
                    Id_Rik = pIdRik
                };

                cmbAcys.Items.Clear();
                cnAcys.AcysXCliente_Consulta(vAcys, gSession.Emp_Cnx, ref acysList);

                cNComun.LlenaCombo<Acys>(ref cmbAcys, "Id_Acs", "Acs_RscTerritorio", acysList);

                this.cmbAcys.SelectedIndex = 0;
                cmbAcys.Enabled = acysList.Any();

                if (!acysList.Any())
                {
                    vFlag = true;
                    throw new Exception("El cliente no tiene un Acuerdo Comercial y de Servicio vigente.");                    
                }

                if (acysList.Count() == acysList.Where(a => a.Acs_RscIdTerr <= 0).Count())
                {
                    vFlag = true;
                    throw new Exception("El Cliente no tiene territorios para el Asesor seleccionado.");
                }
            }
            catch (Exception ex)
            {
                if (vFlag)
                {
                    cmbAcys.Text = string.Empty;
                    txtClienteNombre.Text = string.Empty;
                    txtNumCliente.Value = null;
                    txtNumCliente.Focus();
                    CargarDirecciones(pIdCliente);
                }
                throw ex;
            }
        }

        private void CargarRik(int idTipoRik)
        {
            try
            {
                gSession = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun cNComun = new CapaNegocios.CN__Comun();
                CapaNegocios.CN_CatTiposUsuario cnTipoUsuario = new CN_CatTiposUsuario();
                

                Dictionary<string, object> iParameters = new Dictionary<string, object>();
                List<TipoUsuario> vTipoUsList = new List<TipoUsuario>();
                
                iParameters.Add("@Id1", 1);
                iParameters.Add("@Id2", gSession.Id_Emp);
                iParameters.Add("@Id3", gSession.Id_Cd_Ver);
                iParameters.Add("@IdTipoRep", idTipoRik);

                cmbAsesor.Items.Clear();
                cNComun.LlenaCombo<Comun>(iParameters, gSession.Emp_Cnx, "spCatRik_Combo", ref cmbAsesor, "Id", "Descripcion");

                TipoUsuario vTipoUs = new TipoUsuario()
                {
                    Id_Emp = gSession.Id_Emp
                };

                cnTipoUsuario.ConsultaTiposDeUsuario(vTipoUs, gSession.Emp_Cnx, ref vTipoUsList);

                if (gSession.Id_Rik > 0 && vTipoUsList.FirstOrDefault(x => x.Id_TU == gSession.Id_TU).Tu_Propia)
                {                   
                    cmbAsesor.SelectedValue = gSession.Id_Rik.ToString();
                    txtAsesor.Text = cmbAsesor.SelectedValue;
                    cmbAsesor.Enabled = false;
                    txtAsesor.Enabled = false;
                }

                cmbAsesor_SelectedIndexChanged(cmbAsesor, new RadComboBoxSelectedIndexChangedEventArgs(cmbAsesor.Text, string.Empty, cmbAsesor.SelectedValue, string.Empty));    
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }                        
        #endregion Metodos
        #region ErrorManager                
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("Impresion_error"))
                    Alerta("Error al momento de imprimir");
                else
                    Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        #endregion
    }
}