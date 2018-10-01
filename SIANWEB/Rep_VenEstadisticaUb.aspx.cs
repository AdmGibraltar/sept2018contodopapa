using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using Telerik.Reporting.Processing;

namespace SIANWEB
{
    public partial class Rep_VenEstadisticaUb : System.Web.UI.Page
    {
        #region Variables
        

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

        #endregion Variables

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

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
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
                        this.CargarCombos();
                        this.Inicializar();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion.Id_Cd_Ver = Convert.ToInt32(CmbCentro.SelectedItem.Value);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
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
                        this.Imprimir(true);
                    }
                    else
                    {
                        this.Imprimir(false);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rbTerritorio_CheckedChanged(object sender, EventArgs e)
        {
            this.chkCliente.Checked = false;
            this.chkProducto.Checked = false;
            this.estatusInicialdelaForma();
        }

        protected void rbCliente_CheckedChanged(object sender, EventArgs e)
        {
            this.chkCliente.Checked = false;
            this.chkProducto.Checked = false;

            this.txtCliente.Text = "";
            this.txtProducto.Text = "";
            this.txtTerritorio.Text = "";

            this.cmbAño.SelectedValue = DateTime.Now.Year.ToString();

            this.RowTerritorio.Visible = false;
            this.RowCliente.Visible = true;
            this.RowProducto.Visible = false;
            this.chkCliente.Visible = false;
            this.chkProducto.Visible = true;

            if (cmbTer.Items != null && cmbTer.Items.Any())
            {
                cmbTer.Text = cmbTer.Items[0].Text;
            }
        }

        protected void rbProducto_CheckedChanged(object sender, EventArgs e)
        {
            this.chkCliente.Checked = false;
            this.chkProducto.Checked = false;

            this.txtCliente.Text = "";
            this.txtProducto.Text = "";
            this.txtTerritorio.Text = "";

            this.cmbAño.SelectedValue = DateTime.Now.Year.ToString();

            this.RowTerritorio.Visible = false;
            this.RowCliente.Visible = false;
            this.RowProducto.Visible = true;
            this.chkCliente.Visible = false;
            this.chkProducto.Visible = false;

            if (cmbTer.Items != null && cmbTer.Items.Any())
            {
                cmbTer.Text = cmbTer.Items[0].Text;
            }
        }
        #endregion Eventos

        #region Metodos
        private void estatusInicialdelaForma()
        {
            this.txtCliente.Text = "";
            this.txtProducto.Text = "";
            this.txtTerritorio.Text = "";

            this.cmbAño.SelectedValue = DateTime.Now.Year.ToString();

            this.RowTerritorio.Visible = true;
            this.RowCliente.Visible = false;
            this.RowProducto.Visible = false;
            this.chkCliente.Visible = true;
            this.chkProducto.Visible = true;

            if (cmbTer.Items != null && cmbTer.Items.Any())
            {
                cmbTer.Text = cmbTer.Items[0].Text;                
            }
        }

        #region Codigo boton Imprimir
        private void Inicializar()
        {
            this.cmbAño.Items.Add(new RadComboBoxItem("-- Seleccionar --", "-1"));
            for (int x = DateTime.Now.AddYears(-10).Year; x <= DateTime.Now.AddYears(1).Year; x++)
                cmbAño.Items.Add(new RadComboBoxItem(x.ToString(), x.ToString()));

            this.estatusInicialdelaForma();
        }

        private void Imprimir(bool a_pantalla)
        {
            try
            {
                string paramTer = string.Empty;
                string paramPrd = string.Empty;
                string paramCte = string.Empty;
                int error = 0;

                if (!string.IsNullOrEmpty(this.txtTerritorio.Text))
                {
                    boton(this.txtTerritorio.Text, ref error);
                    paramTer = this.txtTerritorio.Text;
                }
                if (!string.IsNullOrEmpty(this.txtProducto.Text))
                {
                    boton(this.txtProducto.Text, ref error);
                    paramPrd = this.txtProducto.Text;
                }
                if (!string.IsNullOrEmpty(this.txtCliente.Text))
                {
                    boton(this.txtCliente.Text, ref error);
                    paramCte = this.txtCliente.Text;
                }
                if (this.cmbAño.SelectedValue == "-1")
                {
                    error = 1;
                    this.Alerta("Seleccione un año valido");
                }

                if (error == 0)
                {
                    ArrayList ALValorParametrosInternos = new ArrayList();
                    #region Determina el "por" del reporte
                    string OpcRep = "";

                    if ((this.rbTerritorio.Checked) || (this.rbRepresentante.Checked))
                    {
                        OpcRep = "Territorio";
                    }
                    if (this.rbCliente.Checked)
                    {
                        OpcRep = "Cliente";
                    }
                    if (this.rbProducto.Checked)
                    {
                        OpcRep = "Producto";
                    }
                    #endregion Determina el "por" del reporte

                    #region Determina el nivel del reporte
                    string NivRep = "Ninguno";

                    if (this.chkCliente.Checked && !this.chkProducto.Checked)
                    {
                        NivRep = "Cliente";
                    }

                    if (this.chkCliente.Checked == false && this.chkProducto.Checked == true)
                    {
                        NivRep = "Producto";
                    }

                    if (this.chkCliente.Checked == true && this.chkProducto.Checked == true)
                    {
                        NivRep = "Cliente/Producto";
                    }
                    #endregion Determina el nivel del reporte

                    /*   PARAMETROS QUE LLENAN EL ENCABEZADO   */
                    ALValorParametrosInternos.Add(OpcRep); // TIPO DE REPORTE CATEGORIZADO "POR"
                    ALValorParametrosInternos.Add(this.txtTerritorio.Text == "" ? "Todos" : this.txtTerritorio.Text); // OPCION DE SELECCION DE TERRITORIOS
                    ALValorParametrosInternos.Add(this.txtProducto.Text == "" ? "Todos" : this.txtProducto.Text); // OPCION DE SELECCION DE PRODUCTOS
                    ALValorParametrosInternos.Add(this.cmbAño.SelectedValue); // PARAMETRO DE AÑOS
                    ALValorParametrosInternos.Add(NivRep); // NIVEL DE SELECCION DEL REPORTE

                    /*   PARAMETROS PARA LLENAR EL TITULO DEL ENCABEZADO   */
                    ALValorParametrosInternos.Add(this.sesion.U_Nombre); // NOMBRE DEL USUARIO
                    ALValorParametrosInternos.Add(this.sesion.Emp_Nombre); // NOMBRE DE LA EMPRESA
                    ALValorParametrosInternos.Add(this.sesion.Cd_Nombre); // UBICACION DE LA EMPRESA

                    /*   PARAMETROS OBLIGATORIOS PARA EL REPORTE   */
                    ALValorParametrosInternos.Add(sesion.Emp_Cnx); // CADENA DE CONEXION A LA BASE DE DATOS
                    ALValorParametrosInternos.Add(this.sesion.Id_Emp); // ID DE LA EMPRESA
                    ALValorParametrosInternos.Add(this.sesion.Id_Cd_Ver); //ID DEL CENTRO DE DISTRIBUCION

                    /*   PARAMETROS QUE PUEDEN SER NULOS   */
                    ALValorParametrosInternos.Add(paramTer == "" ? null : paramTer); //CADENA DE ID'S DE TERRITORIOS
                    ALValorParametrosInternos.Add(paramPrd == "" ? null : paramPrd); //CADENA DE ID'S DE PRODUCTOS
                    ALValorParametrosInternos.Add(paramCte == "" ? null : paramCte); // CADENA DE ID'S DE CLIENTES

                    ALValorParametrosInternos.Add(this.txtCliente.Text == "" ? "Todos" : this.txtCliente.Text);// OPCION DE SELECCION DE CLIENTES

                    Type instance = null;


                    if (this.rbRepresentante.Checked)
                    {

                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_VenEstadisticaUb1Rik);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_VenEstadisticaUb1Rik);
                        }
                    } 
                    if ((this.rbTerritorio.Checked))
                    {
                        if (this.chkCliente.Checked && !this.chkProducto.Checked)
                        {
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_VenEstadisticaUb1a);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_VenEstadisticaUb1a);
                            }
                        }
                        else
                        {
                            if (this.chkProducto.Checked)
                            {
                                if (!this.chkCliente.Checked)
                                {
                                    if (a_pantalla)
                                    {
                                        instance = typeof(LibreriaReportes.Rep_VenEstadisticaUb1b);
                                    }
                                    else
                                    {
                                        instance = typeof(LibreriaReportes.ExpRep_VenEstadisticaUb1b);
                                    }
                                }
                                else
                                {
                                    if (a_pantalla)
                                    {
                                        instance = typeof(LibreriaReportes.Rep_VenEstadisticaUb1ab);
                                    }
                                    else
                                    {
                                        instance = typeof(LibreriaReportes.ExpRep_VenEstadisticaUb1ab);
                                    }
                                }
                            }
                            else
                            {
                                if (a_pantalla)
                                {
                                    instance = typeof(LibreriaReportes.Rep_VenEstadisticaUb1);
                                }
                                else
                                {
                                    instance = typeof(LibreriaReportes.ExpRep_VenEstadisticaUb1);
                                }
                            }
                        }
                    }

                    if (this.rbCliente.Checked)
                    {
                        if (this.chkProducto.Checked)
                        {
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_VenEstadisticaUb2b);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_VenEstadisticaUb2b);
                            }
                        }
                        else
                        {
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_VenEstadisticaUb2);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_VenEstadisticaUb2);
                            }
                        }
                    }

                    if (this.rbProducto.Checked)
                    {
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_VenEstadisticaUb3);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_VenEstadisticaUb3);
                        }
                    }

                    if (_PermisoImprimir)
                    {
                        if (a_pantalla)
                        {
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
            }
            catch (Exception ex)
            {
                throw ex;
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
        #endregion Codigo boton Imprimir

        /// <summary>
        /// Funcion que separa una cadena dada para determinar los parametros de busqueda por
        /// ID de la tabla
        /// </summary>
        /// <param name="cadena">Cadena de caracteres que recibe para generar la cadena de parametros</param>
        /// <returns>Regresa la cadena de parametros en tipo string</returns>
        private void boton(string cadena, ref int error)
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
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarCombos()
        {
            try
            {
                CargarTerritorios(-1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarTerritorios(int pIdCliente)
        {
            try
            {
                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                string vIdTer = string.Empty;
                string vTerNombre = string.Empty;

                List<Territorios> listaTerritorios = new List<Territorios>();
                new CN_CatCliente().ConsultaTodosTerritoriosDelCliente(pIdCliente, gSession, ref listaTerritorios);
                cmbTer.DataTextField = "Descripcion";
                cmbTer.DataValueField = "Id_Ter";
                cmbTer.DataSource = listaTerritorios;
                cmbTer.DataBind();

                if (cmbTer.Items != null && cmbTer.Items.Any())
                {
                    cmbTer.Text = cmbTer.Items[0].Text;
                    if (pIdCliente > 0)
                    {
                        cmbTer.SelectedIndex = 1;
                        txtTerritorio.Text = cmbTer.Items[1].Value.ToString();
                        cmbTer.Text = cmbTer.Items[1].Text;

                        vIdTer = cmbTer.SelectedValue;
                        vTerNombre = cmbTer.Text;
                    }
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
                    _PermisoImprimir = Permiso.PImprimir;
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Metodos

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
        #endregion ErrorManager
    }
}