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
using Telerik.Reporting.Processing;

namespace SIANWEB
{
    public partial class Rep_CumpVentaInstalada : System.Web.UI.Page
    {
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "Mostrar":
                        Mostrar(true);
                        break;
                    case "excel":
                        Mostrar(false);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RBVentaInstalada_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RBVentaInstalada.Checked)
                    this.cmbAnalisis.Enabled = false;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RBVentaNueva_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RBVentaNueva.Checked)
                    this.cmbAnalisis.Enabled = false;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RBAnalisis_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RBAnalisis.Checked)
                    this.cmbAnalisis.Enabled = true;
                else
                    this.cmbAnalisis.Enabled = false;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Semanas();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbNivel_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbNivel.SelectedValue == "2")
            {
                chkDetalle.Visible = true;
                chkDetalle.Enabled = true;
            }
            else
            {
                chkDetalle.Checked = false;
                chkDetalle.Enabled = false;
                chkDetalle.Visible = false;
            }
        }
        #endregion
        #region Funciones
        private void Semanas()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Funciones funcion = new Funciones();
                Semana semana = new Semana();
                semana.Sem_FechaAct = funcion.GetLocalDateTime(Sesion.Minutos);
                semana.Id_Emp = Sesion.Id_Emp;
                semana.Id_Cd = Sesion.Id_Cd_Ver;
                CN_CatSemana cn_semana = new CN_CatSemana();
                int verificador = 0;
                cn_semana.ConsultaSemana(ref semana, Sesion.Emp_Cnx, ref verificador);

                string[] semanas_arr = semana.Periodo.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                RadComboBoxItem rci = new RadComboBoxItem();
                rci.Text = "Acumulado";
                rci.Value = semana.Periodo;
                ComboSemana.Items.Add(rci);

                foreach (string s in semanas_arr)
                {
                    rci = new RadComboBoxItem();
                    rci.Text = s;
                    rci.Value = s;
                    ComboSemana.Items.Add(rci);
                }

                if (/*verificador > 0 && */ !string.IsNullOrEmpty(semana.Periodo))
                {
                    //txtSemana.Text = semana.Periodo;
                }
                else
                {
                    //txtSemana.Text = "";
                    ComboSemana.Items.Clear();
                    Alerta("Aun no se han configurado las semanas del periodo actual");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void Inicializar()
        {
            try
            {
                CargarCentros();
                Semanas();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CmbCentro.Items.Clear();

                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
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

                //Imprimir
                if (!Permiso.PImprimir)
                    this.RadToolBar1.Items[0].Visible = false;

                _PermisoImprimir = Permiso.PImprimir;
                if (Permiso.PAccesar == true)
                {
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void Mostrar(bool a_pantalla)
        {
            try
            {
                #region captura de Variables y sesion
                int error = 0;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CumpVentaInstalada ventaInstalada = new CumpVentaInstalada();
                ventaInstalada.Id_cd = sesion.Id_Cd_Ver;
                if (RBVentaInstalada.Checked)
                {
                    ventaInstalada.Formato = 1;
                    ventaInstalada.Sformato = RBVentaInstalada.Text;
                }
                if (RBVentaNueva.Checked)
                {
                    ventaInstalada.Formato = 2;
                    ventaInstalada.Sformato = RBVentaNueva.Text;
                }
                if (RBAnalisis.Checked)
                {
                    ventaInstalada.Sformato = RBAnalisis.Text;
                    ventaInstalada.CmbFormato = Convert.ToInt32(cmbAnalisis.SelectedValue);
                    ventaInstalada.SCmbFormato = cmbAnalisis.SelectedItem.Text;
                    if (ventaInstalada.CmbFormato == 1)
                        ventaInstalada.Formato = 3;
                    if (ventaInstalada.CmbFormato == 2)
                        ventaInstalada.Formato = 4;
                    if (ventaInstalada.CmbFormato == 3)
                        ventaInstalada.Formato = 5;
                }
                if (!string.IsNullOrEmpty(/*txtSemana.Text*/ComboSemana.SelectedItem.Text))
                {
                    boton(/*txtSemana.Text*/ComboSemana.SelectedItem.Value, ref error);
                    ventaInstalada.Semana = ComboSemana.SelectedItem.Value;//txtSemana.Text;
                    ventaInstalada.Ssemana = ComboSemana.SelectedItem.Text;//txtSemana.Text;
                }
                else
                    ventaInstalada.Ssemana = "Acumulado";

                if (!string.IsNullOrEmpty(txtRIK.Text))
                {
                    boton(txtRIK.Text, ref error);
                    ventaInstalada.Rik = txtRIK.Text;
                    ventaInstalada.Srik = txtRIK.Text;
                }
                else
                    ventaInstalada.Srik = "Todos";
                if (!string.IsNullOrEmpty(txtTerritorio.Text))
                {
                    boton(txtTerritorio.Text, ref error);
                    ventaInstalada.Territorio = txtTerritorio.Text;
                    ventaInstalada.Sterritorio = txtTerritorio.Text;
                }
                else
                    ventaInstalada.Sterritorio = "Todos";
                if (!string.IsNullOrEmpty(txtProducto.Text))
                {
                    boton(txtProducto.Text, ref error);
                    ventaInstalada.Producto = txtProducto.Text;
                    ventaInstalada.Sproducto = txtProducto.Text;
                }
                else
                    ventaInstalada.Sproducto = "Todos";

                ventaInstalada.Nivel = Convert.ToInt32(cmbNivel.SelectedValue);
                ventaInstalada.Snivel = cmbNivel.SelectedItem.Text;

                if (chkDetalle.Enabled)
                    ventaInstalada.Detalle = chkDetalle.Checked;
                else
                    ventaInstalada.Detalle = false;

                if (ventaInstalada.Detalle)
                    ventaInstalada.SDetalle = "Activado";
                else
                    ventaInstalada.SDetalle = "Desactivado";
                ArrayList ALValorParametrosInternos = new ArrayList();

                CN_CumpVentaInstalada serVenta = new CN_CumpVentaInstalada();
                string nombreEmpresa = sesion.Emp_Nombre;
                string nombreSucursal = sesion.Cd_Nombre;

                DateTime Fechalocal = DateTime.Now;
                #endregion
                //datos de filtros
                ALValorParametrosInternos.Add(ventaInstalada.Formato);
                ALValorParametrosInternos.Add(ventaInstalada.Sformato);
                ALValorParametrosInternos.Add(ventaInstalada.CmbFormato);
                ALValorParametrosInternos.Add(ventaInstalada.SCmbFormato);
                ALValorParametrosInternos.Add(ventaInstalada.Semana);
                ALValorParametrosInternos.Add(ventaInstalada.Ssemana);
                ALValorParametrosInternos.Add(ventaInstalada.Rik);
                ALValorParametrosInternos.Add(ventaInstalada.Srik);
                ALValorParametrosInternos.Add(ventaInstalada.Territorio);
                ALValorParametrosInternos.Add(ventaInstalada.Sterritorio);
                ALValorParametrosInternos.Add(ventaInstalada.Producto);
                ALValorParametrosInternos.Add(ventaInstalada.Sproducto);
                ALValorParametrosInternos.Add(ventaInstalada.Nivel);
                ALValorParametrosInternos.Add(ventaInstalada.Snivel);
                ALValorParametrosInternos.Add(ventaInstalada.SDetalle);

                Type instance = null;
                #region Parametros Formato 1
                if (ventaInstalada.Formato == 1)
                {//crear store para totales..y enviarlos..      
                    if (ventaInstalada.Nivel == 1)
                    {//a- General     
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada1a);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada1a);
                        }
                    }
                    if (ventaInstalada.Nivel == 2)
                    {//b- RIK
                        if (ventaInstalada.Detalle)
                        {//detallado       
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada1bx);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada1bx);
                            }
                        }
                        else
                        {//sin detalle                        
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada1by);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada1by);
                            }
                        }
                    }
                    if (ventaInstalada.Nivel == 3)
                    {//c-Producto                  
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada1c);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada1c);
                        }
                    }
                }
                #endregion
                #region Parametros Formato 2
                if (ventaInstalada.Formato == 2)
                {//3--Análisis de venta instalada  
                    if (ventaInstalada.Nivel == 1)
                    {//a- General  
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada2_a);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada2_a);
                        }
                    }
                    if (ventaInstalada.Nivel == 2)
                    {//b- Rik
                        if (ventaInstalada.Detalle)
                        {//x--detallado               
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada2bx);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada2bx);
                            }
                        }
                        else
                        {//y--sin detallado
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada2by);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada2by);
                            }
                        }
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado1);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado2);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado3);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado4);
                    }
                    if (ventaInstalada.Nivel == 3)
                    {//c- Producto
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada2c);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada2c);
                        }
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado1);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado2);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado3);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado4);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado5);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado6);
                    }
                }
                #endregion
                #region Parametros Formato 3
                if (ventaInstalada.Formato == 3)
                {//3--Análisis de venta instalada           
                    if (ventaInstalada.CmbFormato == 1)
                    {//1--Venta pdte. por autorizar(3)
                        if (ventaInstalada.Nivel == 1)
                        {//a- General          
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada3a);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada3a);
                            }
                        }
                        if (ventaInstalada.Nivel == 2)
                        {//b- Rik
                            if (ventaInstalada.Detalle)
                            {//x--detallado            
                                if (a_pantalla)
                                {
                                    instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada3bx);
                                }
                                else
                                {
                                    instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada3bx);
                                }
                            }
                            else
                            {//y--sin detallado
                                if (a_pantalla)
                                {
                                    instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada3by);
                                }
                                else
                                {
                                    instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada3by);
                                }
                            }
                            ALValorParametrosInternos.Add(ventaInstalada.Encabezado1);
                            ALValorParametrosInternos.Add(ventaInstalada.Encabezado2);
                            ALValorParametrosInternos.Add(ventaInstalada.Encabezado3);
                            ALValorParametrosInternos.Add(ventaInstalada.Encabezado4);
                        }
                        if (ventaInstalada.Nivel == 3)
                        {//c- Producto
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada3c);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada3c);
                            }
                            ALValorParametrosInternos.Add(ventaInstalada.Encabezado1);
                            ALValorParametrosInternos.Add(ventaInstalada.Encabezado2);
                            ALValorParametrosInternos.Add(ventaInstalada.Encabezado3);
                            ALValorParametrosInternos.Add(ventaInstalada.Encabezado4);
                            ALValorParametrosInternos.Add(ventaInstalada.Encabezado5);
                            ALValorParametrosInternos.Add(ventaInstalada.Encabezado6);
                        }
                    }
                }
                #endregion
                #region Parametros Formato 4
                if (ventaInstalada.Formato == 4)
                {//2- Venta Autorizada
                    if (ventaInstalada.Nivel == 1)
                    {//a- General                      
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada4_a);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada4_a);
                        }
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado1);
                    }
                    if (ventaInstalada.Nivel == 2)
                    {//b- Rik
                        if (ventaInstalada.Detalle)
                        {//x--detallado             
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada4bx);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada4bx);
                            }
                        }
                        else
                        {//y--sin detallado
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada4by);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada4by);
                            }
                        }
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado1);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado2);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado3);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado4);
                    }
                    if (ventaInstalada.Nivel == 3)
                    {//c- Producto                     
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada4c);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada4c);
                        }
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado1);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado2);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado3);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado4);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado5);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado6);
                    }
                }
                #endregion
                #region Parametros Formato 5
                if (ventaInstalada.Formato == 5)
                {//3- Integración de venta instalada (5)
                    if (ventaInstalada.Nivel == 1)
                    {//a- General                      
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada5_a);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada5_a);
                        }
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado1);
                    }
                    if (ventaInstalada.Nivel == 2)
                    {//b- Rik
                        if (ventaInstalada.Detalle)
                        {//x--detallado             
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada5bx);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada5bx);
                            }
                        }
                        else
                        {//y--sin detallado
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada5by);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada5by);
                            }
                        }
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado1);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado2);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado3);
                        ALValorParametrosInternos.Add(ventaInstalada.Encabezado4);
                    }
                    if (ventaInstalada.Nivel == 3)
                    {//c- Producto               
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_CumpVentaInstalada5c);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_CumpVentaInstalada5c);
                        }
                    }
                }
                #endregion
                //parametros para el cuerpo del reporte
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(ventaInstalada.Id_cd);
                ALValorParametrosInternos.Add(sesion.U_Nombre);
                ALValorParametrosInternos.Add(Fechalocal);
                ALValorParametrosInternos.Add(nombreEmpresa);
                ALValorParametrosInternos.Add(nombreSucursal);
                //conexion
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                if (_PermisoImprimir)
                {
                    if (error == 0)
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
                else
                    Alerta("No tiene permiso para imprimir");
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
                throw ex;
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