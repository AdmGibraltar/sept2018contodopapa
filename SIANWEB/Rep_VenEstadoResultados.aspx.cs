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
    public partial class Rep_VenEstadoResultados : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
                        this.Inicializar();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
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
        #endregion Eventos

        #region Metodos
        private void Inicializar()
        {
            //this.cmbAnio.Items.Add(new RadComboBoxItem("-- Seleccionar --", "-1"));
            for (int x = DateTime.Now.AddYears(-10).Year; x < DateTime.Now.AddYears(1).Year; x++)
            {
                this.cmbAnio.Items.Add(new RadComboBoxItem(x.ToString(), x.ToString()));
            }
            //this.estatusInicialdelaForma();
            this.cmbAnio.SelectedValue = DateTime.Now.Year.ToString();

            CultureInfo cultura = CultureInfo.CurrentCulture;
            for (int x = 1; x < 13; x++)
            {
                cmbMes.Items.Add(new RadComboBoxItem(cultura.TextInfo
                    .ToTitleCase(cultura.DateTimeFormat.GetMonthName(x)), x.ToString()));
            }

            this.cmbMes.SelectedValue = (DateTime.Now.Month - 1).ToString();
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
                    _PermisoImprimir = Permiso.PImprimir;
                }
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
                DateTime ff, ff2;
                ff = Convert.ToDateTime("01/" + this.cmbMes.SelectedValue.ToString() + "/" + this.cmbAnio.Text);

                int f = ff.Month;
                int f2 = f;
                int a = ff.Year;
                int a2 = a; ;

                switch (this.cmbPeriodo.SelectedValue)
                {
                    case "1":
                        f = ff.Month;
                        a = ff.Year;
                        ff2 = ff.AddMonths(-3);
                        f2 = ff2.Month;
                        a2 = ff2.Year;
                        break;
                    case "2":
                        f = ff.Month;
                        a = ff.Year;
                        ff2 = ff.AddMonths(-6);
                        f2 = ff2.Month;
                        a2 = ff2.Year;
                        break;
                    default:
                        break;
                }

                /*   PARAMETROS QUE LLENAN EL ENCABEZADO   */
                ALValorParametrosInternos.Add(this.cmbAnio.SelectedValue); // AÑO DEL REPORTE
                ALValorParametrosInternos.Add(this.cmbMes.Text); // MES DEL REPORTE
                ALValorParametrosInternos.Add(this.cmbPeriodo.Text); // PERIODO DEL REPORTE

                /*   PARAMETROS PARA LLENAR EL TITULO DEL ENCABEZADO   */
                ALValorParametrosInternos.Add(this.sesion.Emp_Nombre); // NOMBRE DE LA EMPRESA
                ALValorParametrosInternos.Add(this.sesion.Cd_Nombre); // UBICACION DE LA EMPRESA
                ALValorParametrosInternos.Add(this.sesion.U_Nombre); // NOMBRE DEL USUARIO

                /*   PARAMETROS OBLIGATORIOS PARA EL REPORTE   */
                ALValorParametrosInternos.Add(sesion.Emp_Cnx); // CADENA DE CONEXION A LA BASE DE DATOS
                ALValorParametrosInternos.Add(this.sesion.Id_Emp); // ID DE LA EMPRESA
                ALValorParametrosInternos.Add(this.sesion.Id_Cd_Ver); //ID DEL CENTRO DE DISTRIBUCION
                ALValorParametrosInternos.Add(f2); // ESTABLECE EL MES DE INICIO
                ALValorParametrosInternos.Add(a2); // ESTABLECE EL AÑO DE INICIO
                ALValorParametrosInternos.Add(f); // ESTABLECE EL MES DE FIN
                ALValorParametrosInternos.Add(a); // ESTBLECE EL AÑO DE FIN

                /*  MANDA LLAMAR EL REPORTE Y LO MUESTRA EN PANTALLA  */
                Type instance = null;
                if (a_pantalla)
                {
                    instance = typeof(LibreriaReportes.spRep_VenEstadoResultado);
                }
                else
                {
                    instance = typeof(LibreriaReportes.ExpspRep_VenEstadoResultado);
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