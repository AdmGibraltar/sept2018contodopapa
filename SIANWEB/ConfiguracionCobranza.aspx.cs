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
using System.Collections;
using System.Text;
using System.Data.SqlClient; 
namespace SIANWEB
{
    public partial class ConfiguracionCobranza : System.Web.UI.Page
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
        private List<Acciones> list_acciones
        {
            get { return (List<Acciones>)Session["Sesion" + Session.SessionID + "list_acciones"]; }
            set { Session["Sesion" + Session.SessionID + "list_acciones"] = value; }
        }
        private List<Comun> list_respuestas
        {
            get { return (List<Comun>)Session["Sesion" + Session.SessionID + "list_respuestas"]; }
            set { Session["Sesion" + Session.SessionID + "list_respuestas"] = value; }
        }
        private List<Alertas> list_alertas
        {
            get { return (List<Alertas>)Session["Sesion" + Session.SessionID + "list_alertas"]; }
            set { Session["Sesion" + Session.SessionID + "list_alertas"] = value; }
        }
        private List<PeriodoGracia> list_gracia
        {
            get { return (List<PeriodoGracia>)Session["Sesion" + Session.SessionID + "list_gracia"]; }
            set { Session["Sesion" + Session.SessionID + "list_gracia"] = value; }
        }
        private List<ConfigCredito> list_credito
        {
            get { return (List<ConfigCredito>)Session["Sesion" + Session.SessionID + "list_credito"]; }
            set { Session["Sesion" + Session.SessionID + "list_credito"] = value; }
        }

        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ValidarSesion())
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        CargarCentros();
                        Inicializar();
                        Session["FecUltMod" + Session.SessionID] = "Última fecha de modificación: " + System.IO.File.GetLastWriteTime(Request.PhysicalPath).ToString();

                    }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                    Guardar();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgRespuestas_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {

                    case "PerformInsert":
                        PerformInsert_Respuestas(e);
                        break;

                    case "Delete":
                        Delete_Respuestas(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgRespuestas_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add("Respuesta");
            //dt.Rows.Add(new object[] { "" });

            //double ancho = 0;
            //foreach (GridColumn gc in rgAcciones.Columns)
            //{
            //    if (gc.Display)
            //    {
            //        ancho = ancho + gc.HeaderStyle.Width.Value;
            //    }
            //}

            //int agregado_scroll = 0;
            //if (rgRespuestas.Items.Count > 6)
            //{
            //    agregado_scroll = 17;
            //}
            //rgAcciones.Width = Unit.Pixel(Convert.ToInt32(ancho) + agregado_scroll);
            //rgAcciones.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho) + agregado_scroll);
            rgRespuestas.DataSource = list_respuestas;
        }
        protected void rgRespuestas_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            rgRespuestas.Rebind();
        }

        protected void rgAcciones_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Delete":
                        Delete_Acciones(e);
                        break;
                    case "Modificar":
                        ModificarAcciones(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgAcciones_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            try
            {
                //double ancho = 0;
                //foreach (GridColumn gc in rgRespuestas.Columns)
                //{
                //    if (gc.Display)
                //    {
                //        ancho = ancho + gc.HeaderStyle.Width.Value;
                //    }
                //}
                //int agregado_scroll = 0;
                //if (rgRespuestas.Items.Count > 10)
                //{
                //    agregado_scroll = 17;
                //}
                //rgRespuestas.Width = Unit.Pixel(Convert.ToInt32(ancho) + agregado_scroll);
                //rgRespuestas.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho) + agregado_scroll);
                rgAcciones.DataSource = list_acciones;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rgAcciones_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            rgAcciones.Rebind();
        }
        protected void imgAgregar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Acciones acciones;
                if (rgRespuestas.Items.Count == 0 && cmbTPregunta.SelectedValue == "M")
                {
                    Alerta("No se han capturado respuestas");
                    return;
                }

                if (txtGUIDAccion.Text == "")
                {
                    acciones = new Acciones();
                    acciones.GUID = Guid.NewGuid().ToString();
                    CrearAccion(ref acciones);
                    list_acciones.Add(acciones);
                }
                else
                {
                    acciones = list_acciones.Where(Acciones => Acciones.GUID == txtGUIDAccion.Text).ToList()[0];
                    CrearAccion(ref acciones);
                }


                rgAcciones.Rebind();
                LimpiarAcciones();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAlertas_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Delete":
                        Delete_Alertas(e);
                        break;
                    case "Modificar":
                        ModificarAlertas(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgAlertas_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //double ancho = 0;
            //foreach (GridColumn gc in rgAlertas.Columns)
            //{
            //    if (gc.Display)
            //    {
            //        ancho = ancho + gc.HeaderStyle.Width.Value;
            //    }
            //}
            //int agregado_scroll = 0;
            //if (rgAlertas.Items.Count > 10)
            //{
            //    agregado_scroll = 17;
            //}
            //rgAlertas.Width = Unit.Pixel(Convert.ToInt32(ancho) + agregado_scroll);
            //rgAlertas.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho) + agregado_scroll);
            rgAlertas.DataSource = list_alertas;
        }
        protected void rgAlertas_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {

        }
        protected void imgAgregarAlertas_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Alertas alertas;
                if (txtGUIDAlerta.Text == "")
                {
                    alertas = new Alertas();
                    alertas.GUID = Guid.NewGuid().ToString();
                    CrearAlerta(ref alertas);
                    list_alertas.Add(alertas);
                }
                else
                {
                    alertas = list_alertas.Where(Alertas => Alertas.GUID == txtGUIDAlerta.Text).ToList()[0];
                    CrearAlerta(ref alertas);
                }


                rgAlertas.Rebind();
                LimpiarAlertas();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgGracia_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Delete":
                        Borrar_Gracia(e);
                        break;
                    case "Modificar":
                        Modificar_Gracia(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgGracia_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //double ancho = 0;
            //foreach (GridColumn gc in rgGracia.Columns)
            //{
            //    if (gc.Display)
            //    {
            //        ancho = ancho + gc.HeaderStyle.Width.Value;
            //    }
            //}
            //int agregado_scroll = 0;
            //if (rgGracia.Items.Count > 6)
            //{
            //    agregado_scroll = 17;
            //}
            //rgGracia.Width = Unit.Pixel(Convert.ToInt32(ancho) + agregado_scroll);
            //rgGracia.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho) + agregado_scroll);
            rgGracia.DataSource = list_gracia;
        }
        protected void imgAgregarGracia_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                PeriodoGracia gracia;
                if (txtGUIDGracia.Text == "")
                {
                    gracia = new PeriodoGracia();
                    gracia.GUID = TxtCondiciones.Text;
                    if (list_gracia.Where(PeriodoGracia => PeriodoGracia.GUID == gracia.GUID).ToList().Count == 0)
                    {
                        CrearGracia(ref gracia);
                        list_gracia.Add(gracia);
                    }
                    else
                    {
                        AlertaFocus("Ya existe una regla para condiciones de pago a " + gracia.GUID + " días", TxtCondiciones.ClientID);
                        return;
                    }
                }
                else
                {
                    if (list_gracia.Where(PeriodoGracia => PeriodoGracia.GUID == TxtCondiciones.Text).ToList().Count == 0)
                    {
                        gracia = list_gracia.Where(PeriodoGracia => PeriodoGracia.GUID == txtGUIDGracia.Text).ToList()[0];
                        gracia.GUID = TxtCondiciones.Text;
                        CrearGracia(ref gracia);
                    }
                    else
                    {
                        AlertaFocus("Ya existe una regla para condiciones de pago a " + TxtCondiciones.Text + " días", TxtCondiciones.ClientID);
                        return;
                    }
                }
                rgGracia.Rebind();
                LimpiarGracia();
                TxtCondiciones.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgCredito_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Delete":
                        Delete_Credito(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgCredito_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                RgCredito.DataSource = list_credito;
            }
            catch (Exception ex)
            {
                
                 ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
         
            
        }
        protected void rgCredito_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {

                RgCredito.Rebind();
            }
            catch (Exception ex)
            {
                
               ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void imgAgregarCuenta_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.CmbTipoU.SelectedValue  == "0")
                {
                    this.RfvTipoU.IsValid = false;
                    return;
                }

                if (this.TxtMaxDias.Text == string.Empty)
                {
                    this.RfvMaxDias.IsValid = false;
                    return;
                }


                ConfigCredito C = new ConfigCredito();
                C.Id_Tu = int.Parse(CmbTipoU.SelectedValue);
                C.Tu_Descripcion  =CmbTipoU.Text;
                C.MaxDias = int.Parse(this.TxtMaxDias.Text);

                for (int i = 0; i < this.list_credito.Count; i++)
                {
                    if (list_credito[i].Tu_Descripcion.ToLower().Contains(this.CmbTipoU.Text.ToLower()))
                    {
                        list_credito.RemoveAt(i);
                    }
                }

                 list_credito.Add(C);
                 RgCredito.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
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
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }
        protected void cmbTPregunta_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            trRespuestas.Visible = cmbTPregunta.SelectedValue == "M";
            //imgAgregar.Visible = cmbTPregunta.SelectedValue == "A";
            list_respuestas = new List<Comun>();
            rgRespuestas.Rebind();
        }
        #endregion
        #region Funciones
        private void CrearAccion(ref Acciones acciones)
        {
            try
            {
                acciones.Etapa = cmbATipo.SelectedValue;
                acciones.EtapaStr = cmbATipo.Text;
                acciones.Dias = txtADias.Value;
                acciones.Tipo_Respuesta = cmbTPregunta.SelectedValue;
                acciones.Tipo_RespuestaStr = cmbTPregunta.Text;
                acciones.Pregunta = txtPregunta.Text;
                acciones.Respuestas = new ArrayList();
                acciones.RespuestasStr = "";

                int columna_respuesta = rgRespuestas.Columns.FindByUniqueName("Respuesta").OrderIndex - 2;
                foreach (GridDataItem gdi in rgRespuestas.Items)
                {
                    acciones.Respuestas.Add((gdi.Cells[columna_respuesta].FindControl("lblRespuesta") as Label).Text);
                    acciones.RespuestasStr += (gdi.Cells[columna_respuesta].FindControl("lblRespuesta") as Label).Text + ", ";
                }
                if (acciones.RespuestasStr != null)
                {
                    if (acciones.RespuestasStr != "")
                    {
                        acciones.RespuestasStr = acciones.RespuestasStr.Substring(0, acciones.RespuestasStr.Length - 2);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void CrearAlerta(ref Alertas alertas)
        {
            try
            {
                alertas.GUID = Guid.NewGuid().ToString();
                alertas.Etapa = cmbAlTipo.SelectedValue;
                alertas.EtapaStr = cmbAlTipo.Text;
                alertas.Dias = txtAlDias.Value;
                alertas.EnviarA = Convert.ToInt32(cmbEnviar.SelectedValue);
                alertas.EnviarAStr = cmbEnviar.Text;
                alertas.SuspenderCredito = chkSuspender.Checked;
                alertas.SuspenderCreditoStr = chkSuspender.Checked ? "Si" : "No";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void CrearGracia(ref PeriodoGracia gracia)
        {
            try
            {
                gracia.Reg_Periodo = Convert.ToInt32(TxtPeriodoGracia.Value);
                gracia.Reg_Condicion = Convert.ToInt32(TxtCondiciones.Value);

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
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CargarComboTipoU();
                CargarComboTipoUCredito();
                CargarComboTipoU_Autoriza1();
                CargarComboTipoU_Autoriza2();
                CargarComboTipoU_Autoriza3();
                CargarEtapasAcciones();
                CargarEtapasAlertas();

                CN_ConfiguracionCobranza confCobranza = new CN_ConfiguracionCobranza();

                List<Acciones> list_accionesTemp = new List<Acciones>();
                List<Alertas> list_alertasTemp = new List<Alertas>();
                List<PeriodoGracia> list_graciaTemp = new List<PeriodoGracia>();
                Reglas reglas = new Reglas();

                confCobranza.Consultar(ref list_graciaTemp, ref list_accionesTemp, ref list_alertasTemp, sesion.Id_Emp, (new SqlConnectionStringBuilder(sesion.Emp_Cnx)).InitialCatalog, ref reglas, Emp_CnxCob);

                list_acciones = list_accionesTemp;
                list_respuestas = new List<Comun>();
                list_alertas = list_alertasTemp;
                list_gracia = list_graciaTemp;
                list_credito = ConfCreditoList();

                TxtPlazo.Value = (double?)reglas.Plazo;
                cmbAutoriza1.SelectedValue = reglas.Id_Tu1.ToString();
                cmbAutoriza2.SelectedValue = reglas.Id_Tu2.ToString();
                cmbAutoriza3.SelectedValue = reglas.Id_Tu3.ToString();

                txtDias1.DbValue = reglas.Val1;
                txtDias2.DbValue = reglas.Val2;
                txtDias3.DbValue = reglas.Val3;
                txtDias4.DbValue = reglas.Val4;
                txtDias5.DbValue = reglas.Val5;
                txtDias6.DbValue = reglas.Val6;

                CobProceso cobProceso = new CobProceso();
                cobProceso.Id_Emp = Sesion.Id_Emp;
                cobProceso.Id_Cd = Sesion.Id_Cd_Ver;
                confCobranza.ConsultarCobProceso(ref cobProceso, Emp_CnxCob);
                CheckSvtasAlm.Checked = cobProceso.SvtasAlm;
                CheckEmbAlm.Checked = cobProceso.EmbAlm;
                CheckEntAlm.Checked = cobProceso.EntAlm;
                CheckAlmCob.Checked = cobProceso.AlmCob;
                CheckRevCob.Checked = cobProceso.RevCob;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
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
        private bool ValidarSesion()
        {
            try
            {
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                    return false;
                }
                return true;
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
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                if (sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, sesion.Id_Emp, sesion.Id_U, sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarEtapasAcciones()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Comun.LlenaComboStr(1, Emp_CnxCob, "spCatTipo_Combo", ref this.cmbATipo);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarEtapasAlertas()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Comun.LlenaComboStr(2, Emp_CnxCob, "spCatTipo_Combo", ref this.cmbAlTipo);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarComboTipoU()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "SpSysTipoUsuario_Combo", ref this.cmbEnviar);


                //Comentado porque ya no hay usuarios que dependan del administrador               
                Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem();
                item.Value = "0";
                item.Text = "-- Seleccionar --";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarComboTipoUCredito()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "SpSysTipoUsuario_Combo", ref this.CmbTipoU );

                //Comentado porque ya no hay usuarios que dependan del administrador               
                Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem();
                item.Value = "0";
                item.Text = "-- Seleccionar --";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarComboTipoU_Autoriza1()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "SpSysTipoUsuario_Combo", ref this.cmbAutoriza1);

                //Comentado porque ya no hay usuarios que dependan del administrador               
                Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem();
                item.Value = "0";
                item.Text = "-- Seleccionar --";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarComboTipoU_Autoriza2()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "SpSysTipoUsuario_Combo", ref this.cmbAutoriza2);

                //Comentado porque ya no hay usuarios que dependan del administrador               
                Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem();
                item.Value = "0";
                item.Text = "-- Seleccionar --";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarComboTipoU_Autoriza3()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "SpSysTipoUsuario_Combo", ref this.cmbAutoriza3);

                //Comentado porque ya no hay usuarios que dependan del administrador               
                Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem();
                item.Value = "0";
                item.Text = "-- Seleccionar --";
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
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = sesion.Id_U;
                Permiso.Id_Cd = sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla
                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, sesion.Emp_Cnx);

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

        private void PerformInsert_Respuestas(GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;
                int columna_respuesta = rgRespuestas.Columns.FindByUniqueName("Respuesta").OrderIndex - 2;
                Comun comun = new Comun();
                comun.IdStr = Guid.NewGuid().ToString();
                comun.Descripcion = (gi.Cells[columna_respuesta].FindControl("txtRespuesta") as RadTextBox).Text;
                list_respuestas.Add(comun);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Delete_Respuestas(GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;
                int columna_respuesta = rgRespuestas.Columns.FindByUniqueName("IdStr").OrderIndex;
                list_respuestas.Remove(list_respuestas.Where(Comun => Comun.IdStr == gi.Cells[columna_respuesta].Text).ToList()[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Delete_Acciones(GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;
                int columna_respuesta = rgAcciones.Columns.FindByUniqueName("GUID").OrderIndex;
                list_acciones.Remove(list_acciones.Where(Acciones => Acciones.GUID == gi.Cells[columna_respuesta].Text).ToList()[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void Delete_Alertas(GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;
                int columna_respuesta = rgAlertas.Columns.FindByUniqueName("GUID").OrderIndex;
                list_alertas.Remove(list_alertas.Where(Alertas => Alertas.GUID == gi.Cells[columna_respuesta].Text).ToList()[0]);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Delete_Credito(GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;
                int columna_respuesta = this.RgCredito.Columns.FindByUniqueName("Id_Tu").OrderIndex;
                list_credito.Remove(list_credito.Where(Id_Tu => Id_Tu.Id_Tu == int.Parse(gi.Cells[columna_respuesta].Text)).ToList()[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Borrar_Gracia(GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;
                int columna_respuesta = rgGracia.Columns.FindByUniqueName("GUID").OrderIndex;
                list_gracia.Remove(list_gracia.Where(PeriodoGracia => PeriodoGracia.GUID == gi.Cells[columna_respuesta].Text).ToList()[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LimpiarAcciones()
        {
            cmbATipo.SelectedIndex = 0;
            txtADias.Value = null;
            cmbTPregunta.SelectedIndex = 0;
            cmbTPregunta_SelectedIndexChanged(cmbTPregunta, null);
            txtPregunta.Text = "";
            txtGUIDAccion.Text = "";
            list_respuestas = new List<Comun>();
            rgRespuestas.Rebind();
        }
        private void LimpiarAlertas()
        {
            cmbAlTipo.SelectedIndex = 0;
            txtAlDias.Value = null;
            cmbEnviar.SelectedIndex = 0;
            txtGUIDAlerta.Text = "";
            chkSuspender.Checked = false;
            rgAlertas.Rebind();
            RequiredFieldValidator7.IsValid = true;
        }
        private void LimpiarGracia()
        {

            txtGUIDGracia.Text = "";
            TxtCondiciones.DbValue = null;
            TxtPeriodoGracia.DbValue = null;
            rgGracia.Rebind();
        }
        private void Guardar()
        {
            try
            {

                if (!_PermisoModificar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }

                if (!validarEspeciales())
                {
                    return;
                }

                CN_ConfiguracionCobranza confCobranza = new CN_ConfiguracionCobranza();
                int verificador = 0;
                Reglas reglas = new Reglas();
                reglas.List_gracia = list_gracia;
                reglas.Plazo = TxtPlazo.Value;
                reglas.Id_Tu1 = Convert.ToInt32(cmbAutoriza1.SelectedValue);
                reglas.Id_Tu2 = Convert.ToInt32(cmbAutoriza2.SelectedValue);
                reglas.Id_Tu3 = Convert.ToInt32(cmbAutoriza3.SelectedValue);
                reglas.Val1 = txtDias1.Value;
                reglas.Val2 = txtDias2.Value;
                reglas.Val3 = txtDias3.Value;
                reglas.Val4 = txtDias4.Value;
                reglas.Val5 = txtDias5.Value;
                reglas.Val6 = txtDias6.Value;
                CobProceso cobProceso = new CobProceso();
                cobProceso.Id_Emp = sesion.Id_Emp;
                cobProceso.Id_Cd = sesion.Id_Cd_Ver;
                cobProceso.SvtasAlm = Convert.ToBoolean(CheckSvtasAlm.Checked);
                cobProceso.EmbAlm = Convert.ToBoolean(CheckEmbAlm.Checked);
                cobProceso.EntAlm = Convert.ToBoolean(CheckEntAlm.Checked);
                cobProceso.AlmCob = Convert.ToBoolean(CheckAlmCob.Checked);
                cobProceso.RevCob = Convert.ToBoolean(CheckRevCob.Checked);

                confCobranza.Guardar(list_acciones, list_alertas,list_credito, reglas, cobProceso, sesion.Id_Emp, Emp_CnxCob, ref verificador);

                if (verificador == 1)
                {
                    Alerta("Los cambios se guardaron correctamente");
                }
                else
                {
                    Alerta("Ocurrió un error al intentar guardar los cambios");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool validarEspeciales()
        {

            bool correcto = true;

            if (Convert.ToInt32(cmbAutoriza1.SelectedValue) == 0)
            {
                txtDias1.Value = null;
                txtDias2.Value = null;
            }
            if (Convert.ToInt32(cmbAutoriza2.SelectedValue) == 0)
            {
                txtDias3.Value = null;
                txtDias4.Value = null;
            }

            if (Convert.ToInt32(cmbAutoriza3.SelectedValue) == 0)
            {
                txtDias5.Value = null;
                txtDias6.Value = null;
            }

            if ((txtDias1.Value.HasValue && !txtDias2.Value.HasValue) || (!txtDias1.Value.HasValue && txtDias2.Value.HasValue))
            {
                Alerta("El primer rango de <b>Autorización de condiciones de pago especiales</b> no es valido");
                correcto = false;
            }
            else if ((txtDias3.Value.HasValue && !txtDias4.Value.HasValue) || (!txtDias3.Value.HasValue && txtDias4.Value.HasValue))
            {
                Alerta("El segundo rango de <b>Autorización de condiciones de pago especiales</b> no es valido");
                correcto = false;
            }
            else if ((txtDias5.Value.HasValue && !txtDias6.Value.HasValue) || (!txtDias5.Value.HasValue && txtDias6.Value.HasValue))
            {
                Alerta("El tercer rango de <b>Autorización de condiciones de pago especiales</b> no es valido");
                correcto = false;
            }

            if (!(txtDias1.Value.HasValue && txtDias2.Value.HasValue))
            {
                cmbAutoriza1.SelectedValue = "0";
                cmbAutoriza1.Text = cmbAutoriza1.FindItemByValue("0").Text;
            }
            if (!(txtDias3.Value.HasValue && txtDias4.Value.HasValue))
            {
                cmbAutoriza2.SelectedValue = "0";
                cmbAutoriza2.Text = cmbAutoriza2.FindItemByValue("0").Text;
            }
            if (!(txtDias5.Value.HasValue && txtDias6.Value.HasValue))
            {
                cmbAutoriza3.SelectedValue = "0";
                cmbAutoriza3.Text = cmbAutoriza3.FindItemByValue("0").Text;
            }

            if ((Convert.ToInt32(cmbAutoriza1.SelectedValue) != 0 && Convert.ToInt32(cmbAutoriza2.SelectedValue) != 0) && cmbAutoriza1.SelectedValue == cmbAutoriza2.SelectedValue)
            {
                Alerta("Favor de seleccionar diferentes tipo de usuario para cada rango de autorización de condiciones de pago especiales");
                correcto = false;
            }
            else if ((Convert.ToInt32(cmbAutoriza1.SelectedValue) != 0 && Convert.ToInt32(cmbAutoriza3.SelectedValue) != 0) && cmbAutoriza1.SelectedValue == cmbAutoriza3.SelectedValue)
            {
                Alerta("Favor de seleccionar diferentes tipo de usuario para cada rango de autorización de condiciones de pago especiales");
                correcto = false;
            }
            else if ((Convert.ToInt32(cmbAutoriza2.SelectedValue) != 0 && Convert.ToInt32(cmbAutoriza3.SelectedValue) != 0) && cmbAutoriza2.SelectedValue == cmbAutoriza3.SelectedValue)
            {
                Alerta("Favor de seleccionar diferentes tipo de usuario para cada rango de autorización de condiciones de pago especiales");
                correcto = false;
            }
            return correcto;
        }

        private void ModificarAcciones(GridCommandEventArgs e)
        {
            GridItem gi = e.Item;
            int columna_respuesta = rgAcciones.Columns.FindByUniqueName("GUID").OrderIndex;
            Acciones a = list_acciones.Where(Acciones => Acciones.GUID == gi.Cells[columna_respuesta].Text).ToList()[0];

            txtGUIDAccion.Text = a.GUID;
            cmbATipo.SelectedValue = a.Etapa;
            txtADias.Value = a.Dias;
            cmbTPregunta.SelectedValue = a.Tipo_Respuesta;
            txtPregunta.Text = a.Pregunta;
            cmbTPregunta_SelectedIndexChanged(cmbTPregunta, null);

            Comun comun;
            foreach (string respuesta in a.Respuestas)
            {
                comun = new Comun();
                comun.IdStr = Guid.NewGuid().ToString();
                comun.Descripcion = respuesta;
                list_respuestas.Add(comun);
            }
            rgRespuestas.Rebind();
        }
        private void ModificarAlertas(GridCommandEventArgs e)
        {
            GridItem gi = e.Item;
            int columna_respuesta = rgAlertas.Columns.FindByUniqueName("GUID").OrderIndex;
            Alertas a = list_alertas.Where(Alertas => Alertas.GUID == gi.Cells[columna_respuesta].Text).ToList()[0];

            txtGUIDAlerta.Text = a.GUID;
            cmbAlTipo.SelectedValue = a.Etapa;
            txtAlDias.Value = a.Dias;
            cmbEnviar.SelectedValue = a.EnviarA.ToString();
            chkSuspender.Checked = a.SuspenderCredito;

        }
        private void Modificar_Gracia(GridCommandEventArgs e)
        {
            GridItem gi = e.Item;
            int columna_respuesta = rgGracia.Columns.FindByUniqueName("GUID").OrderIndex;
            PeriodoGracia a = list_gracia.Where(PeriodoGracia => PeriodoGracia.GUID == gi.Cells[columna_respuesta].Text).ToList()[0];

            txtGUIDGracia.Text = a.GUID;
            TxtCondiciones.DbValue = a.Reg_Condicion;
            TxtPeriodoGracia.DbValue = a.Reg_Periodo;

        }
        private bool validar_rangos()
        {
            try
            {

                if (cmbAutoriza1.SelectedValue != "-1")
                {
                    if (txtDias1.Value == null && txtDias2.Value == null)
                    {
                        return true;
                    }
                    if (txtDias1.Value == null && txtDias2.Value != null)
                    {
                        AlertaFocus("Falta el dia inicial del primer rango", txtDias2.ClientID);
                        return true; ;
                    }
                    if (txtDias1.Value != null && txtDias2.Value == null)
                    {
                        AlertaFocus("Falta el dia final del primer rango", txtDias2.ClientID);
                        return true;
                    }


                }

                if (cmbAutoriza2.SelectedValue != "-1")
                {
                    if (txtDias3.Value == null && txtDias4.Value == null)
                    {
                        return true;
                    }
                    if (txtDias3.Value == null && txtDias4.Value != null)
                    {
                        AlertaFocus("Falta el dia inicial del segundo rango", txtDias3.ClientID);
                        return true;
                    }
                    if (txtDias3.Value != null && txtDias4.Value == null)
                    {
                        AlertaFocus("Falta el dia final del segundo rango", txtDias4.ClientID);
                        return true;
                    }

                    if (txtDias1.Value != null && txtDias2.Value != null)
                    {
                        if (txtDias3.Value >= txtDias1.Value && txtDias3.Value <= txtDias2.Value)
                        {
                            AlertaFocus("Día ya contenido en otro rango", txtDias3.ClientID);
                            return true;
                        }
                        if (txtDias4.Value >= txtDias1.Value && txtDias4.Value <= txtDias2.Value)
                        {
                            AlertaFocus("Día ya contenido en otro rango", txtDias4.ClientID);
                            return true;
                        }
                    }

                }

                if (cmbAutoriza3.SelectedValue != "-1")
                {
                    if (txtDias5.Value == null && txtDias6.Value == null)
                    {
                        return true;
                    }
                    if (txtDias5.Value == null && txtDias6.Value != null)
                    {
                        AlertaFocus("Falta el dia inicial del tercer rango", txtDias5.ClientID);
                        return true;
                    }
                    if (txtDias5.Value != null && txtDias6.Value == null)
                    {
                        AlertaFocus("Falta el dia final del tercer rango", txtDias6.ClientID);
                        return true;
                    }

                    if (txtDias1.Value != null && txtDias2.Value != null)
                    {
                        if (txtDias5.Value >= txtDias1.Value && txtDias5.Value <= txtDias2.Value)
                        {
                            AlertaFocus("Día ya contenido en otro rango", txtDias5.ClientID);
                            return true;
                        }
                        if (txtDias6.Value >= txtDias1.Value && txtDias6.Value <= txtDias2.Value)
                        {
                            AlertaFocus("Día ya contenido en otro rango", txtDias6.ClientID);
                            return true;
                        }
                    }
                    if (txtDias3.Value != null && txtDias4.Value != null)
                    {
                        if (txtDias5.Value >= txtDias3.Value && txtDias5.Value <= txtDias4.Value)
                        {
                            AlertaFocus("Día ya contenido en otro rango", txtDias5.ClientID);
                            return true;
                        }
                        if (txtDias6.Value >= txtDias3.Value && txtDias6.Value <= txtDias4.Value)
                        {
                            AlertaFocus("Día ya contenido en otro rango", txtDias6.ClientID);
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<ConfigCredito> ConfCreditoList()
        {
            try
            {
                string conexion;
                conexion = ConfigurationManager.AppSettings["strConnectionCobranza"];
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<ConfigCredito> List = new List<ConfigCredito>();
                CN_ConfiguracionCobranza cn_cc = new CN_ConfiguracionCobranza();
                cn_cc.ConsultarConfiguCredito(ref List, sesion.Id_Emp, sesion.Id_Cd, conexion);
                return List;

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

        protected void CheckEmbAlm_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (CheckEmbAlm.Checked == true)
                {
                    CheckEntAlm.Checked = true;
                    CheckAlmCob.Checked = true;
                }
                else
                {
                    CheckEntAlm.Checked = false;
                    CheckAlmCob.Checked = false;
                }
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void CheckEntAlm_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckEntAlm.Checked == true)
                {
                    CheckEmbAlm.Checked = true;
                    CheckAlmCob.Checked = true;
                }
                else
                {
                    CheckEmbAlm.Checked = false;
                    CheckAlmCob.Checked = false;
                }

            }
            catch (Exception ex)
            {
                
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void CheckAlmCob_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (CheckAlmCob.Checked == true)
                {
                    CheckEmbAlm.Checked = true;
                    CheckEntAlm.Checked = true;
                }
                else
                {
                    CheckEmbAlm.Checked = false;
                    CheckEntAlm.Checked = false;
                }


            }
            catch (Exception ex)
            {
                
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
    }
}