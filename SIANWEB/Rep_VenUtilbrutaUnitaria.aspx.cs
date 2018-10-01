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
using System.Globalization;
using Telerik.Reporting.Processing;

namespace SIANWEB
{
    public partial class Rep_VenUtilbrutaUnitaria : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoImprimir { get { return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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

        protected void rbPrecio_CheckedChanged(object sender, EventArgs e)
        {
            this.txtUb.Enabled = false;
            this.txtUb.Text = "";
        }

        protected void rbUtilidad_CheckedChanged(object sender, EventArgs e)
        {
            this.txtUb.Enabled = true;
            this.txtUb.Text = "45.00";
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
        public void txtNumCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];

                Clientes cliente = new Clientes();
                cliente.Id_Emp = gSession.Id_Emp;
                cliente.Id_Cd = gSession.Id_Cd_Ver;
                cliente.Id_Cte = Convert.ToInt32((sender as RadNumericTextBox).Value.HasValue ? (sender as RadNumericTextBox).Value.Value : -1);
                new CN_CatCliente().ConsultaClientes(ref cliente, gSession.Emp_Cnx);
                txtNombreCliente.Text = cliente.Cte_NomComercial;
                CargarTerritorios(cliente.Id_Cte.Value);

            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
                txtNumeroCliente.Text = string.Empty;
                txtNombreCliente.Text = string.Empty;
                txtTerritorio.Text = string.Empty;
                CargarTerritorios(-1);
            }
        }                        
        #endregion Eventos

        #region Metodos
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
                    _PermisoImprimir = Permiso.PImprimir;
                ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("print")).Visible = _PermisoImprimir;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Imprimir(bool a_pantalla)
        {
            try
            {
                ArrayList ALValorParametrosInternos = new ArrayList();

                string paramTer = this.boton(this.txtTerritorio.Text);
                string paramCte = this.boton(this.txtNumeroCliente.Text);

                #region Determina como esta calculado el reporte
                string CalCon = "Precio de Lista";

                if (this.rbUtilidad.Checked)
                {
                    CalCon = "Utilidad bruta unitaria";
                }
                #endregion Determina como esta calculado el reporte

                /*   PARAMETROS QUE LLENAN EL ENCABEZADO   */
                ALValorParametrosInternos.Add(this.txtTerritorio.Text == "" ? "Todos" : this.txtTerritorio.Text); // OPCION DE SELECCION DE TERRITORIOS
                ALValorParametrosInternos.Add(this.txtNumeroCliente.Text == "" ? "Todos" : this.txtNumeroCliente.Text); // OPCION DE SELECCION DE CLIENTES
                ALValorParametrosInternos.Add(this.cmbAñoIni.SelectedValue); // PARAMETRO DE AÑO DE INICIO
                ALValorParametrosInternos.Add(this.cmbAñoFin.SelectedValue); // PARAMETRO DE AÑO DE FIN
                ALValorParametrosInternos.Add(this.cmbMesIni.SelectedValue); // PARAMETRO DE MES DE INICIO
                ALValorParametrosInternos.Add(this.cmbMesFin.SelectedValue); // PARAMETRO DE MES DE FIN
                ALValorParametrosInternos.Add(CalCon); // PARAMETRO DE CALCULADO CON
                ALValorParametrosInternos.Add(this.txtUb.Text == "" ? null : this.txtUb.Text); // PARAMETRO DE %

                /*   PARAMETROS PARA LLENAR EL TITULO DEL ENCABEZADO   */
                ALValorParametrosInternos.Add(this.sesion.Emp_Nombre); // NOMBRE DE LA EMPRESA
                ALValorParametrosInternos.Add(this.sesion.Cd_Nombre); // UBICACION DE LA EMPRESA
                ALValorParametrosInternos.Add(this.sesion.U_Nombre); // NOMBRE DEL USUARIO

                /*   PARAMETROS OBLIGATORIOS PARA EL REPORTE   */
                ALValorParametrosInternos.Add(sesion.Emp_Cnx); // CADENA DE CONEXION A LA BASE DE DATOS
                ALValorParametrosInternos.Add(this.sesion.Id_Emp); // ID DE LA EMPRESA
                ALValorParametrosInternos.Add(this.sesion.Id_Cd_Ver); //ID DEL CENTRO DE DISTRIBUCION

                ///*   PARAMETROS QUE PUEDEN SER NULOS   */
                ALValorParametrosInternos.Add(paramTer == "" ? null : paramTer); //CADENA DE ID'S DE TERRITORIOS
                ALValorParametrosInternos.Add(paramCte == "" ? null : paramCte); // CADENA DE ID'S DE CLIENTES
                ALValorParametrosInternos.Add(rbPrecio.Checked ? 1 : 2); // CADENA DE ID'S DE CLIENTES
                ALValorParametrosInternos.Add(this.txtUb.Text == "" ? "0.00" : this.txtUb.Text); // DETERMINA SI SE CALCUA EL % O NO

                if (this.rbUtilidad.Checked && Convert.ToDouble(this.txtUb.Text) == 0.00)
                {
                    this.Alerta("Ingrese un valor aceptable para el cálculo de la utilidad bruta unitaria");
                    this.txtUb.Focus();
                }
                else
                {
                    /*  MANDA LLAMAR EL REPORTE Y LO MUESTRA EN PANTALLA  */
                    Type instance = null;
                    if (a_pantalla)
                    {
                        instance = typeof(LibreriaReportes.Rep_VenUtilbrutaUnitaria);
                    }
                    else
                    {
                        instance = typeof(LibreriaReportes.ExpRep_VenUtilbrutaUnitaria);
                    }

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
        #region Metodo para separar una cadena dada y generar otra de ids
        /// <summary>
        /// Funcion que separa una cadena dada para determinar los parametros de busqueda por
        /// ID de la tabla
        /// </summary>
        /// <param name="cadena">Cadena de caracteres que recibe para generar la cadena de parametros</param>
        /// <returns>Regresa la cadena de parametros en tipo string</returns>
        private string boton(string cadena)
        {
            StringBuilder condicion = new StringBuilder("");

            if (cadena == "")
            {
                return "";
            }
            else
            {
                string[] split = cadena.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string[] split2;

                foreach (string a in split)
                {
                    if (a.Contains("-"))
                    {
                        split2 = a.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                        if (Convert.ToInt32(split2[0]) > Convert.ToInt32(split2[1]))
                        {
                            Alerta("El rango " + a.ToString() + " no es valido");
                            return "error";
                        }
                        for (int i = Convert.ToInt32(split2[0]); i < Convert.ToInt32(split2[1]) + 1; i++)
                        {
                            condicion.Append(i.ToString() + ",");
                        }
                    }
                    else
                    {
                        condicion.Append(a + ",");
                    }
                }
                condicion.Remove(condicion.Length - 1, 1);
            }
            return condicion.ToString();
        }
        #endregion Metodo para separar una cadena dada y generar otra de ids
        private void Inicializar()
        {
            try
            {
                /*-------LLENA LOS COMBOBOX DE AÑOS DE LOS PERIODOS DE INICIO Y FIN-------*/
                for (int x = DateTime.Now.AddYears(-10).Year;
                      x <= DateTime.Now.AddYears(1).Year; x++)
                {
                    cmbAñoIni.Items.Add(new RadComboBoxItem(x.ToString(), x.ToString()));
                    cmbAñoFin.Items.Add(new RadComboBoxItem(x.ToString(), x.ToString()));
                }

                this.cmbAñoIni.SelectedValue = "2011";
                this.cmbAñoFin.SelectedValue = "2011";
                /*------LLENA LOS COMBOBOX DE MESES DE LOS PERIODOS DE INICIO Y FIN------*/
                CultureInfo cultura = CultureInfo.CurrentCulture;
                for (int x = 1; x < 13; x++)
                {
                    cmbMesIni.Items.Add(new RadComboBoxItem(cultura.TextInfo
                        .ToTitleCase(cultura.DateTimeFormat.GetMonthName(x)), x.ToString()));
                    cmbMesFin.Items.Add(new RadComboBoxItem(cultura.TextInfo
                        .ToTitleCase(cultura.DateTimeFormat.GetMonthName(x)), x.ToString()));
                }
                this.cmbMesIni.SelectedValue = "1";
                this.cmbMesFin.SelectedValue = "12";
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