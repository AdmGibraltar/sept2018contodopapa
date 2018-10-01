using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Reporting.Processing;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using System.Collections;
using System.IO;
using System.Configuration;

namespace SIANWEB
{
    public partial class FiltrosReporte_CumplimientoCostoGarantia : System.Web.UI.Page
    {
        public FiltrosReporte_CumplimientoCostoGarantia()
        {
            _anyos = new List<int>();
            for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 7; i--)
            {
                _anyos.Add(i);
            }
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

        protected void BindDataControls()
        {
            for(int i=0; i<_anyos.Count; i++)
            {
                rcbFechaDetalleRemisionesAno.Items.Add(new RadComboBoxItem(_anyos[i].ToString(), _anyos[i].ToString()));
            }

            for (int i = 0; i < _months.Length; i++)
            {
                rcbFechaDetalleRemisionesMes.Items.Add(new RadComboBoxItem(_months[i], i.ToString()));
            }

            rcbFechaDetalleRemisionesMes.SelectedValue = (DateTime.Now.Month-1).ToString();

            PoblarRepresentates();
            PoblarTiposDeGarantia();
        }

        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        private void PoblarRepresentates()
        {
            CapaDatos.CD_CatRepresentantes representantesCD = new CD_CatRepresentantes();
            CapaEntidad.Representantes reps=new Representantes();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string StrCnx = ConfigurationManager.AppSettings.Get("strConnection");
            reps.Id_Emp=Sesion.Id_Emp;
            reps.Id_Cd=Sesion.Id_Cd;
            CN_CatUsuario cnCatUsuario = new CN_CatUsuario();
            List<Usuario> usuarios=new List<Usuario>();
            cnCatUsuario.ConsultaUsuarios(new Usuario() { Id_Cd=Sesion.Id_Cd, Id_Emp=Sesion.Id_Emp }, StrCnx, ref usuarios);

            rcbAsesor.Items.Add(new RadComboBoxItem("--Todos--", "0"));
            if (usuarios != null)
            {
                for (int i = 0; i < usuarios.Count; i++)
                {
                    rcbAsesor.Items.Add(new RadComboBoxItem(usuarios[i].U_Nombre, usuarios[i].Id_U.ToString()));
                }
            }
        }
        private void PoblarTiposDeGarantia()
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_TipoGarantia cnTg = new CN_TipoGarantia(sesion);
            chklstGarantias.DataSource = cnTg.ObtenerTodas();
            chklstGarantias.DataBind();
        }

        private void Inicializar()
        {
            try
            {
                BindDataControls();
                rbDetalleRemisiones.Checked = true;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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

#warning "Descomentar los bloques de autorización"
                //Imprimir
                //if (!Permiso.PImprimir)
                    //this.RadToolBar1.Items[0].Visible = false;

                _PermisoImprimir = Permiso.PImprimir;
                if (Permiso.PAccesar == true) //if(Sesion != null)
                {
                }
                else
                {
                    //Response.Redirect("Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "Mostrar":
                        if (Page.IsValid)
                        {
                            this.Mostrar(true);//true
                        }
                        break;
                    case "excel":
                        if (Page.IsValid)
                        {
                            this.Mostrar(false);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected bool NingunTipoDeGarantiaElegido()
        {
            var elementosElegidos = (from ListItem e in chklstGarantias.Items
                                    where e.Selected
                                    select e).ToList();
            return elementosElegidos.Count == 0;

        }

        private void Mostrar(bool a_pantalla)
        {
            if (NingunTipoDeGarantiaElegido())
            {
                Alerta("Por favor, elija al menos un tipo de garantía.");
                return;
            }

            try
            {
                #region captura de Variables y sesion
                int error = 0;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (_PermisoImprimir)
                {
                    if (error == 0)
                    {
                        ArrayList ALValorParametrosInternos = new ArrayList();
                        
                #endregion
                        //datos de filtros                             
                        CapaNegocios.CN_CatCentroDistribucion cdn=new CN_CatCentroDistribucion();
                        CentroDistribucion cd=new CentroDistribucion();
                        //cdn.ConsultarCentroDistribucion(ref cd, sesion.Id_Cd

                        Type instance = null;
                        if (rbDetalleRemisiones.Checked)
                        {
                            instance = typeof(LibreriaReportes.Rep_CCG_DetalleRemisiones);

                            ALValorParametrosInternos.Add(null); //remision
                            ALValorParametrosInternos.Add(string.IsNullOrEmpty(rtbTerritorio.Text) ? null : rtbTerritorio.Text); //territorio
                            ALValorParametrosInternos.Add(string.IsNullOrEmpty(rtbCliente.Text) ? null : rtbCliente.Text); //cliente
                            ALValorParametrosInternos.Add(int.Parse(rcbFechaDetalleRemisionesAno.SelectedItem.Value)); //año
                            ALValorParametrosInternos.Add(int.Parse(rcbFechaDetalleRemisionesMes.SelectedItem.Value)+1); //mes
                            ALValorParametrosInternos.Add(rcbAsesor.SelectedItem.Value == "0" ? (int?)null : int.Parse(rcbAsesor.SelectedItem.Value)); //rik
                            //cdnombre
                            ALValorParametrosInternos.Add(sesion.Cd_Nombre);
                            //usrnombre
                            ALValorParametrosInternos.Add(sesion.U_Nombre);
                            //ctenombre
                            ALValorParametrosInternos.Add(string.IsNullOrEmpty(rbtNombreCliente.Text) ? null : rbtNombreCliente.Text);
                            //riknombre
                            ALValorParametrosInternos.Add(rcbAsesor.SelectedItem.Value=="0" ? null : rcbAsesor.SelectedItem.Text);
                            //mesnombre
                            ALValorParametrosInternos.Add(rcbFechaDetalleRemisionesMes.SelectedItem.Text);
                            //idemp
                            ALValorParametrosInternos.Add(sesion.Id_Emp);
                            //idcd
                            ALValorParametrosInternos.Add(sesion.Id_Cd);

                            ALValorParametrosInternos.Add(sesion.Emp_Cnx); //conexion

                            List<string> idTgs = new List<string>();
                            var ids = (from ListItem v in chklstGarantias.Items
                                       where v.Selected
                                       select v.Value).ToArray();
                            ALValorParametrosInternos.Add(ids);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.Rep_CCG_MonitoreoDesviaciones);
                            ALValorParametrosInternos.Add(string.IsNullOrEmpty(rtbTerritorio.Text) ? null : rtbTerritorio.Text); //territorio
                            ALValorParametrosInternos.Add(string.IsNullOrEmpty(rtbCliente.Text) ? null : rtbCliente.Text); //cliente

                            ALValorParametrosInternos.Add(int.Parse(rcbFechaDetalleRemisionesAno.SelectedItem.Value));
                            ALValorParametrosInternos.Add(int.Parse(rcbFechaDetalleRemisionesMes.SelectedItem.Value) + 1);
                            ALValorParametrosInternos.Add(int.Parse(rcbFechaDetalleRemisionesAno.SelectedItem.Value));
                            ALValorParametrosInternos.Add(int.Parse(rcbFechaDetalleRemisionesMes.SelectedItem.Value) + 1);
                            ALValorParametrosInternos.Add(rcbAsesor.SelectedItem.Value == "0" ? (int?)null : int.Parse(rcbAsesor.SelectedItem.Value)); //rik
                            ALValorParametrosInternos.Add(sesion.Emp_Cnx); //conexion

                            ALValorParametrosInternos.Add(sesion.Id_Cd);
                            ALValorParametrosInternos.Add(sesion.Cd_Nombre);
                            ALValorParametrosInternos.Add(sesion.Id_Emp);
                            ALValorParametrosInternos.Add(sesion.Emp_Nombre);
                            ALValorParametrosInternos.Add(rbtNombreCliente.Text);
                            ALValorParametrosInternos.Add(rcbAsesor.Text);
                            List<string> idTgs = new List<string>();
                            var ids = (from ListItem v in chklstGarantias.Items
                                      where v.Selected
                                      select v.Value).ToArray();
                            ALValorParametrosInternos.Add(ids);
                            ALValorParametrosInternos.Add(rcbFechaDetalleRemisionesMes.SelectedItem.Text);
                        }

                        if (a_pantalla)
                        {
                            
                        }
                        else
                        {
                            //if (agrupador == 1)//cliente
                            //    instance = typeof(LibreriaReportes.ExpRep_CargosPdtesxTer1);
                            //else//territorio
                            //    instance = typeof(LibreriaReportes.Rep_CargosPdtesxTer2);
                        }

                        //parametros para el cuerpo del reporte
                        
                        //ALValorParametrosInternos.Add(Fechalocal);
                        //ALValorParametrosInternos.Add(nombreEmpresa);
                        //ALValorParametrosInternos.Add(nombreSucursal);
                        //conexion
                        //ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                        //NOTA: El estatus de impresión, lo pone cuando el reporte se carga    
                        if (a_pantalla)
                        {
                            Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = null;
                            Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                            Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                            RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
                        }
                        else
                        {
                            ImprimirXLS(ALValorParametrosInternos, instance);
                        }
                    }
                }
                else
                {
                    Alerta("No tiene permiso para imprimir");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void boton(string cadena, ref int error)
        {
            try
            {
                if (!string.IsNullOrEmpty(cadena))
                {
                    string[] split = cadena.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string[] split2;
                    foreach (string a in split)
                    {
                        if (a.Contains("-"))
                        {
                            split2 = a.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                            if (split2.Length != 2)
                            {
                                Alerta("El rango " + a.ToString() + " no es válido");
                                error = 1;
                            }
                            if (split2.Length == 2)
                                if (Convert.ToInt32(split2[0]) > Convert.ToInt32(split2[1]))
                                {
                                    Alerta("El rango " + a.ToString() + " no es válido");
                                    error = 1;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected List<int> _anyos = null;
        protected string[] _months ={
                                        "Enero",
                                        "Febrero",
                                        "Marzo",
                                        "Abril",
                                        "Mayo",
                                        "Junio",
                                        "Julio",
                                        "Agosto",
                                        "Septiembre",
                                        "Octubre",
                                        "Noviembre",
                                        "Diciembre",
                                   };

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