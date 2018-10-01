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
using System.Text;
using System.Drawing;
using System.Data.SqlClient;
namespace SIANWEB
{
    public partial class MonitorCobranza : System.Web.UI.Page
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
        string[] cabezera = new string[] { "Id_Emp", "Id_Cd", "TipoN", "Etapa", "Días", "Documento", "Cliente", "Importe", "Saldo", "Acciones" };
        bool[] Visible = new bool[] { false, false, false, true, true, true, true, true, true, true };
        private bool GenerarGrafica
        {
            get { return (bool)Session["GenerarGrafica" + Session.SessionID]; }
            set { Session["GenerarGrafica" + Session.SessionID] = value; }
        }
        string NoDocumentos = "<table><tr><td width=\"300\" align=\"center\">No se encontraron documentos</td></tr></table>";
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
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }

                        Inicializar();
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
        //protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    try
        //    {
        //        Sesion sesion = new Sesion();
        //        sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //        if (sesion == null)
        //        {
        //            string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
        //            Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
        //        }
        //        CN__Comun comun = new CN__Comun();
        //        comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        //    }
        //}
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                //string cmd = e.Argument.ToString();
                CargarPaneles();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            CargarPaneles();
        }
        protected void cmbFiltroTCentro_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CargarCentrosFiltro();
        }
        #endregion
        #region Funciones
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
        //private void CargarCentros()
        //{
        //    try
        //    {
        //        CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
        //        if (sesion.U_MultiOfi == false)
        //        {
        //            CN_Comun.LlenaCombo(2, sesion.Id_Emp, sesion.Id_U, sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
        //            this.CmbCentro.Visible = false;
        //            this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(sesion.Id_Cd_Ver.ToString()).Text;
        //        }
        //        else
        //        {
        //            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
        //            this.CmbCentro.SelectedValue = sesion.Id_Cd_Ver.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        private void CargarCentrosFiltro()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                string cnx = (new SqlConnectionStringBuilder(sesion.Emp_Cnx)).InitialCatalog;
                cmbFiltroCentro.Items.Clear();
                CN_Comun.LlenaCombo(cnx, sesion.Id_Emp, sesion.Id_Cd, Convert.ToInt32(cmbFiltroTCentro.SelectedValue), sesion.Id_U, Emp_CnxCob, "spCatCentroDistribucion_Combo", ref cmbFiltroCentro);
                if (cmbFiltroCentro.Items.Count > 0)
                {
                    cmbFiltroCentro.Items.Insert(0, new RadComboBoxItem("-- Todos --", "-1"));
                }
                else
                {
                    cmbFiltroCentro.EmptyMessage = "No se encontraron centros";
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
        private void Inicializar()
        {
            try
            {
                //CargarCentros();
                CargarTCentro();
                CargarCentrosFiltro();
                if (cmbFiltroCentro.SelectedValue != "")
                {
                    CargarPaneles();
                }
                tblFiltro.Visible = cmbFiltroCentro.Items.Count > 2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTCentro()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Emp_CnxCob, "spCatTcentro_Combo", ref cmbFiltroTCentro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarPaneles()
        { 
            try
            {
                CN_GestorCobranza cn_gestor = new CN_GestorCobranza();
                DataSet ds = new DataSet();
                Cobranza cob = new Cobranza();
                cob.Id_Emp = sesion.Id_Emp;
                cob.Id_Cd_Ver = Convert.ToInt32(cmbFiltroCentro.SelectedValue);
                cob.Id_TCd = Convert.ToInt32(cmbFiltroTCentro.SelectedValue);
                cob.Id_Cd = sesion.Id_Cd;
                cob.Id_U = sesion.Id_U;
                cob.DbName = (new SqlConnectionStringBuilder(sesion.Emp_Cnx)).InitialCatalog;
                cn_gestor.ConsultarDocumentos(cob, ref ds, Emp_CnxCob);
                //TODO
                AccionesAtiempo(ds);
                AccionesAtrasadas(ds);
                ClientesSinIdentificar(ds);

                if (sesion.Id_TU == 14)
                {
                    if (cmbFiltroVer.SelectedValue == "%")
                    {
                        LiteralAvance.Text = GeneraGraficaAvance().Replace("<param name=\"quality\" value=\"high\" />", "<param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\">");
                    }
                    else
                    {
                        LiteralAvance.Text = GeneraGraficaAvance_Saldos().Replace("<param name=\"quality\" value=\"high\" />", "<param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\">");
                    }
                    divGraficas1.Visible = false;
                    divGraficas2.Visible = true;
                }
                else
                {
                    if (cmbFiltroVer.SelectedValue == "%")
                    {
                        LiteralEntrega.Text = GeneraGraficaEntrega().Replace("<param name=\"quality\" value=\"high\" />", "<param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\">");
                        LiteralCobranza.Text = GeneraGraficaCobranza().Replace("<param name=\"quality\" value=\"high\" />", "<param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\">");
                        LiteralRevision.Text = GeneraGraficaRevision().Replace("<param name=\"quality\" value=\"high\" />", "<param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\">");
                        LiteralVencidas.Text = GeneraGraficaVencidas().Replace("<param name=\"quality\" value=\"high\" />", "<param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\">");
                    }
                    else
                    {
                        LiteralEntrega.Text = GeneraGraficaEntrega_Saldos().Replace("<param name=\"quality\" value=\"high\" />", "<param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\">");
                        LiteralCobranza.Text = GeneraGraficaCobranza_Saldos().Replace("<param name=\"quality\" value=\"high\" />", "<param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\">");
                        LiteralRevision.Text = GeneraGraficaRevision_Saldos().Replace("<param name=\"quality\" value=\"high\" />", "<param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\">");
                        LiteralVencidas.Text = GeneraGraficaVencidas_Saldos().Replace("<param name=\"quality\" value=\"high\" />", "<param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\">");
                    }

                    LiteralDiasVencidos.Text = GeneraGraficaDiasVencidos().Replace("<param name=\"quality\" value=\"high\" />", "<param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\">");
                    LiteralCartera.Text = GeneraGraficaCartera().Replace("<param name=\"quality\" value=\"high\" />", "<param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\">");
                    LiteralCosto.Text = GeneraGraficaCosto().Replace("<param name=\"quality\" value=\"high\" />", "<param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\">");

                    divGraficas1.Visible = true;
                    divGraficas2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void ClientesSinIdentificar(DataSet ds)
        {
            try
            {
                object o = ds.Tables[2].Compute("Sum(Saldo)", string.Empty);
                PnlDetalle.Items[2].Text = TituloColapsable("Clientes sin identificar", ds.Tables[2].Rows.Count, o == DBNull.Value ? 0 : Convert.ToDouble(o));
                divClientes.Controls.Clear();
                divClientes.Controls.Add(GeneraTablaClientes(ds.Tables[2].Rows));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void AccionesAtrasadas(DataSet ds)
        {
            try
            {
                object o = ds.Tables[1].Compute("Sum(Saldo)", string.Empty);
                PnlDetalle.Items[1].Text = TituloColapsable("Acciones urgentes atrasadas", ds.Tables[1].Rows.Count, o == DBNull.Value ? 0 : Convert.ToDouble(o));
                divAtrasado.Controls.Clear();
                divAtrasado.Controls.Add(GeneraTabla(ds.Tables[1].Rows));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void AccionesAtiempo(DataSet ds)
        {
            try
            {
                object o = ds.Tables[0].Compute("Sum(Saldo)", string.Empty);
                PnlDetalle.Items[0].Text = TituloColapsable("Acciones urgentes en tiempo", ds.Tables[0].Rows.Count, o == DBNull.Value ? 0 : Convert.ToDouble(o));
                divaTiempo.Controls.Clear();
                divaTiempo.Controls.Add(GeneraTabla(ds.Tables[0].Rows));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private Table GeneraTablaClientes(DataRowCollection dataRowCollection)
        {
            Table tb = new Table();

            tb.HorizontalAlign = HorizontalAlign.Left;
            tb.GridLines = GridLines.Both;

            double saldo = 0;
            string Tipo = "";

            tb.Rows.Add(crearFilaHdr(new string[] { "Id_Emp", "Id_Cd", "TipoN", "Tipo", "Caso", "Cliente", "Saldo" }, new int[] { 0, 0, 0, 80, 270, 400, 100 }));
            foreach (DataRow dr in dataRowCollection)
            {
                saldo += Convert.ToDouble(dr["Saldo"]);

                Tipo = dr["Revision"].ToString();

                if (dr["Recepcion"].ToString() != "" && Tipo != "")
                {
                    Tipo += ", ";
                }
                Tipo += dr["Recepcion"].ToString();

                if (dr["Pago"].ToString() != "" && Tipo != "")
                {
                    Tipo += ", ";
                }
                Tipo += dr["Pago"].ToString();

                if (dr["Condiciones"].ToString() != "" && Tipo != "")
                {
                    Tipo += ", ";
                }
                Tipo += dr["Condiciones"].ToString();

                tb.Rows.Add(crearFila(true, false, false, new string[] { dr["Id_Emp"].ToString(), dr["Id_Cd"].ToString(), "", dr["Tipo"].ToString(), Tipo, dr["Id_Cte"].ToString().PadLeft(6) + " " + dr["Cte_Nombre"].ToString(), Convert.ToDouble(dr["Saldo"]).ToString("C") }));
            }
            tb.Rows.Add(crearFilaPie(true, new string[] { "", "", "", "Total", "", "", saldo.ToString("C") }));

            return tb;
        }
        private Table GeneraTabla(DataRowCollection dataRowCollection)
        {
            Table tb = new Table();

            tb.HorizontalAlign = HorizontalAlign.Left;
            tb.GridLines = GridLines.Both;
            tb.Rows.Add(crearFilaHdr(cabezera, new int[] { 0, 0, 0, 100, 50, 100, 350, 100, 100, 50 }));

            string grupo = "";
            string Cliente = "";
            string grupoStr = "";
            string ClienteStr = "";
            int contador = 0;
            int contador1 = 0;

            double importe = 0;
            double saldo = 0;

            double importeTotal = 0;
            double saldoTotal = 0;

             

            foreach (DataRow dr in dataRowCollection)
            {
               
                if (dr["Id_Cte"].ToString() != Cliente && dr["Tipo"].ToString()  == "VEN")
                {
                    if (contador1 > 0)
                    {
                       Factura factura = null;
                       double TotalVenta = 0;
                       double TotalSaldo = 0;
                       factura = saldoCliente(dataRowCollection, Int32.Parse(Cliente.ToString()));
                       TotalVenta = double.Parse(factura.Fac_Importe.ToString());
                       TotalSaldo = double.Parse(factura.Fac_Saldo.ToString());


                       tb.Rows.Add(crearFilaPieCliente(false, new string[] { dr["Id_Emp"].ToString(), dr["Id_Cd"].ToString(), Cliente, ClienteStr, "", "", "Total", TotalVenta.ToString("C"), TotalSaldo.ToString("C") }));
                  
                    }
                    Cliente = dr["Id_Cte"].ToString();
                    ClienteStr = dr["Cte_Nombre"].ToString();

                    
                    contador1++;
                }
                

                
                if (dr["Tipo"].ToString() != grupo)
                {

                    if (contador > 0)
                    {
                        tb.Rows.Add(crearFilaPie(false, new string[] { "", "", "", grupoStr, "", "", "Total", importe.ToString("C"), saldo.ToString("C"), "" }));

                        importeTotal += importe;
                        saldoTotal += saldo;

                        importe = 0;
                        saldo = 0;
                    }
                    grupo = dr["Tipo"].ToString();
                    grupoStr = dr["TipoStr"].ToString();

                    contador++;
                }

                importe += Convert.ToDouble(dr["Fac_Importe"]);
                saldo += Convert.ToDouble(dr["Saldo"]);
                tb.Rows.Add(crearFila(false, Convert.ToBoolean(dr["AccionCapturada"]), Convert.ToBoolean(dr["AccionPendiente"]), new string[] { dr["Id_Emp"].ToString(), dr["Id_Cd"].ToString(), dr["Tipo"].ToString(), dr["TipoStr"].ToString(), dr["Dias"].ToString(), dr["Fac_Serie"].ToString(), dr["Id_Cte"].ToString().PadLeft(6) + " " + dr["Cte_Nombre"].ToString(), Convert.ToDouble(dr["Fac_Importe"]).ToString("C"), Convert.ToDouble(dr["Saldo"]).ToString("C") }, Int32.Parse(dr["Id_Cte"].ToString())));
            }
            tb.Rows.Add(crearFilaPie(false, new string[] { "", "", "", grupoStr, "", "", "Total", importe.ToString("C"), saldo.ToString("C"), "" }));
            importeTotal += importe;
            saldoTotal += saldo;

            tb.Rows.Add(crearFilaPie(false, new string[] { "", "", "", "Total", "", "", "", importeTotal.ToString("C"), saldoTotal.ToString("C"), "" }));

            return tb;

        }
        private Factura saldoCliente(DataRowCollection dataRowCollection, int Id_Cte) {
            Double Importe = 0;
            Double Saldo = 0;
            Factura factura = new Factura();
           
            foreach (DataRow dr in dataRowCollection) {
                if(Id_Cte == Int32.Parse(dr["Id_Cte"].ToString()) &&  dr["Tipo"].ToString()  == "VEN") {
                    Importe += Convert.ToDouble(dr["Fac_Importe"]);
                    Saldo += Convert.ToDouble(dr["Saldo"]);
                }
            }


            factura.Fac_Importe = Importe;
            factura.Fac_Saldo = Saldo;

            return factura;
        }

        private TableHeaderRow crearFilaHdr(string[] param, int[] param_width)
        {
            TableHeaderRow tr = new TableHeaderRow();
            for (int i = 0; i < param.Length; i++)
            {
                tr.Cells.Add(crearCeldaHdr(param[i], Visible[i], param_width[i]));
            }
            return tr;
        }
        private TableHeaderRow crearFilaPie(bool identificar, string[] param)
        {
            TableHeaderRow tr = new TableHeaderRow();
            for (int i = 0; i < param.Length; i++)
            {
                tr.Cells.Add(crearCeldaPie(identificar, i, param[i]));
            }
            return tr;
        }
        
        private TableHeaderRow crearFilaPieCliente(bool identificar, string[] param)
        {
            TableHeaderRow tr = new TableHeaderRow();            
            StringBuilder dir = new StringBuilder();
            tr.Height = Unit.Pixel(10);

            dir.Append("Id_Emp='" + Server.UrlEncode(param[0]) + "'&");
            dir.Append("Id_Cd='" + Server.UrlEncode(param[1]) + "'&");
            dir.Append("Id_Cte='" + Server.UrlEncode(param[2]) + "'&");
            dir.Append("Etapa='" + Server.UrlEncode("Vencimiento") + "'&");
            dir.Append("Dias='" + Server.UrlEncode("-1") + "'&");
            dir.Append("Cliente='" + Server.UrlEncode(param[3]) + "'&");
            dir.Append("Documento='" + Server.UrlEncode("-1") + "'&");
            dir.Append("Importe='" + Server.UrlEncode(param[7]) + "'&");
            dir.Append("Saldo='" + Server.UrlEncode(param[8]) + "'&");
            dir.Append("TipoN='" + Server.UrlEncode("VEN") + "'&");

            for (int i = 0; i < param.Length; i++)
            {
                tr.Cells.Add(crearCeldaPieCliente(identificar, i, param[i]));
                
                //dir.Append(QuitaAcento(cabezera[i]) + "='" + Server.UrlEncode(param[i]) + "'&");
            }
          



            if (!identificar)
            {
                bool icono = true;

                tr.Cells.Add(crearCeldaPieCliente(dir.ToString().Substring(0, dir.Length - 1), icono));
            }


            return tr;
        }

        private TableRow crearFila(bool identificar, bool AccionCapturada, bool AccionPendiente, string[] param, int Id_Cte = 0)
        {
            TableRow tr = new TableRow();
            StringBuilder dir = new StringBuilder();
            tr.Height = Unit.Pixel(10);
            //tr.Attributes["onmouseover"] = "highlight(this, true);";
            //tr.Attributes["onmouseout"] = "highlight(this, false);";

            for (int i = 0; i < param.Length; i++)
            {
                tr.Cells.Add(crearCelda(identificar, i, param[i]));
                dir.Append(QuitaAcento(cabezera[i]) + "='" + Server.UrlEncode(param[i]) + "'&");
            }
            dir.Append("Id_Cte='" + Server.UrlEncode(Id_Cte.ToString()) + "'&");
            if (!identificar)
            {
                bool icono = false;
                if (!AccionCapturada && AccionPendiente)
                {
                    icono = true;
                }

                tr.Cells.Add(crearCelda(dir.ToString().Substring(0, dir.Length - 1), icono));
            }

            return tr;
        }
        private TableHeaderCell crearCeldaHdr(string param, bool visible, int param_width)
        {
            TableHeaderCell tc = new TableHeaderCell();
            tc.Visible = visible;
            tc.Text = param;
            tc.BackColor = Color.WhiteSmoke;
            tc.Width = Unit.Pixel(param_width);
            return tc;
        }
        private TableHeaderCell crearCeldaPie(bool identificar, int indice, string param)
        {
            TableHeaderCell tc = new TableHeaderCell();
            tc.Visible = Visible[indice];
             if (indice == 3 && identificar)
            {
                tc.HorizontalAlign = HorizontalAlign.Right;
            }
            else if (indice > 3)
            {
                tc.HorizontalAlign = HorizontalAlign.Right;
            }
            else
            {
                tc.HorizontalAlign = HorizontalAlign.Left;
            }

            tc.Text = param;
            tc.BackColor = Color.WhiteSmoke;
            return tc;
        }


        private TableHeaderCell crearCeldaPieCliente(string dir, bool icono)
        {
            TableHeaderCell tc = new TableHeaderCell();
            ImageButton ib = new ImageButton();
            ib.ImageUrl = icono ? @"~\Img\Abierto.png" : @"~\Img\Cerrado.png";
            ib.OnClientClick = "return Button_Click(\"" + dir + "\", this);";
            ib.Style.Add("border", "0");
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Controls.Add(ib);
            tc.BackColor = Color.OrangeRed;
            return tc;
        }

        private TableHeaderCell crearCeldaPieCliente(bool identificar, int indice, string param)
        {
            TableHeaderCell tc = new TableHeaderCell();
            tc.Visible = Visible[indice];
            if (indice == 1)
            {
                tc.ColumnSpan = 3;

            }
            else if (indice == 3 && identificar)
            {
                tc.HorizontalAlign = HorizontalAlign.Right;
               
            }
            else if (indice > 3)
            {
                tc.HorizontalAlign = HorizontalAlign.Right;
            }
            else
            {
                tc.HorizontalAlign = HorizontalAlign.Left;
            }

            tc.Text = param;
            tc.BackColor = Color.OrangeRed;
            return tc;
        }

        
        private TableCell crearCelda(string dir, bool icono)
        {
            TableCell tc = new TableCell();
            ImageButton ib = new ImageButton();
            ib.ImageUrl = icono ? @"~\Img\Abierto.png" : @"~\Img\Cerrado.png";
            ib.OnClientClick = "return Button_Click(\"" + dir + "\", this);";
            ib.Style.Add("border", "0");
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Controls.Add(ib);
            return tc;
        }
        private TableCell crearCelda(bool identificar, int indice, string param)
        {
            TableCell tc = new TableCell();
            tc.Visible = Visible[indice];

            switch (indice - 3)
            {
                case 0:
                    if (identificar)
                    {
                        //tc.Width = Unit.Pixel(80);
                        tc.HorizontalAlign = HorizontalAlign.Center;
                    }
                    else
                    {
                        //tc.Width = Unit.Pixel(100);
                    }
                    tc.HorizontalAlign = HorizontalAlign.Left;
                    break;
                case 1:
                    if (identificar)
                    {
                        //tc.Width = Unit.Pixel(370);
                        tc.HorizontalAlign = HorizontalAlign.Left;
                    }
                    else
                    {
                        //tc.Width = Unit.Pixel(50);
                        tc.HorizontalAlign = HorizontalAlign.Right;
                    }

                    break;
                case 2:
                    if (identificar)
                    {
                        //tc.Width = Unit.Pixel(300);
                    }
                    else
                    {
                        //tc.Width = Unit.Pixel(100);
                    }
                    tc.HorizontalAlign = HorizontalAlign.Left;
                    break;
                case 3:
                    if (identificar)
                    {
                        //tc.Width = Unit.Pixel(100);
                        tc.HorizontalAlign = HorizontalAlign.Right;
                    }
                    else
                    {
                        //tc.Width = Unit.Pixel(350);
                        tc.HorizontalAlign = HorizontalAlign.Left;
                    }

                    break;
                case 4:
                    //tc.Width = Unit.Pixel(100);
                    tc.HorizontalAlign = HorizontalAlign.Right;
                    break;
                case 5:
                    //tc.Width = Unit.Pixel(100);
                    tc.HorizontalAlign = HorizontalAlign.Right;
                    break;
            }


            tc.Text = param;
            return tc;
        }
        private string TituloColapsable(string titulo, int pendientes, double Saldo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            sb.Append("<tr>");
            sb.Append("<td width=\"200px\">");
            sb.Append(titulo);
            sb.Append("</td>");
            sb.Append("<td width=\"100px\">");
            sb.Append("</td>");
            sb.Append("<td>");
            sb.Append("#Pendientes");
            sb.Append("</td>");
            sb.Append("<td align=\"right\" width=\"50\">");
            sb.Append("<b>");
            sb.Append(pendientes.ToString());
            sb.Append("</b>");
            sb.Append("</td>");
            sb.Append("<td width=\"100px\">");
            sb.Append("</td>");
            sb.Append("<td>");
            sb.Append("Saldo");
            sb.Append("</td>");
            sb.Append("<td align=\"right\" width=\"100\">");
            sb.Append("<b>");
            sb.Append(Saldo.ToString("C"));
            sb.Append("</b>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            return sb.ToString();
        }
        private string QuitaAcento(string palabra)
        {
            palabra = palabra.Replace("á", "a");
            palabra = palabra.Replace("é", "e");
            palabra = palabra.Replace("í", "i");
            palabra = palabra.Replace("ó", "o");
            palabra = palabra.Replace("ú", "u");
            return palabra;
        }

        private Usuario GeneraFiltros()
        {
            Usuario usu = new Usuario();
            usu.Id_Emp = sesion.Id_Emp;
            usu.Id_Cd = sesion.Id_Cd;
            usu.Id_U = sesion.Id_U;
            usu.DbName = (new SqlConnectionStringBuilder(sesion.Emp_Cnx)).InitialCatalog;
            usu.Id_Cd_Ver = Convert.ToInt32(cmbFiltroCentro.SelectedValue);
            usu.Id_TCd = Convert.ToInt32(cmbFiltroTCentro.SelectedValue);
            return usu;
        }

        private string GeneraGraficaEntrega()
        {
            try
            {
                List<object> list = new List<object>();
                CN_GestorCobranza cn_gestor = new CN_GestorCobranza();

                cn_gestor.GraficaEntrega(ref list, GeneraFiltros(), Emp_CnxCob);
                Label1.Text = "Entrega mercancía<BR>E=" + ((double)list[4]).ToString("##0.00") + "%";

                return InfoSoftGlobal.FusionCharts.RenderChartHTML("Charts/AngularGauge.swf", "", GraficaAngular(list[3]), "myNext", "300", "170", false);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private string GeneraGraficaEntrega_Saldos()
        {
            try
            {
                List<double> list = new List<double>();
                CN_GestorCobranza cn_gestor = new CN_GestorCobranza();

                cn_gestor.GraficaEntrega_Saldos(ref list, GeneraFiltros(), Emp_CnxCob);

                if (list[0] == 0)
                {
                    Label1.Text = "Entrega mercancía";
                    return NoDocumentos;
                }
                Label1.Text = "Entrega mercancía<BR>E=" + ((double)list[4]).ToString("##0.00") + "%";
                return InfoSoftGlobal.FusionCharts.RenderChartHTML("Charts/AngularGauge.swf", "", GraficaAngular_Saldos(list[0], list[1]), "myNext", "350", "170", false);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private string GeneraGraficaCobranza()
        {
            try
            {
                List<object> list = new List<object>();
                CN_GestorCobranza cn_gestor = new CN_GestorCobranza();
                cn_gestor.GraficaCobranza(ref list, GeneraFiltros(), Emp_CnxCob);
                Label2.Text = "Entrega cobranza<BR>E=" + ((double)list[4]).ToString("##0.00") + "%";
                return InfoSoftGlobal.FusionCharts.RenderChartHTML("Charts/AngularGauge.swf", "", GraficaAngular(list[3]), "myNext", "300", "170", false);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private string GeneraGraficaCobranza_Saldos()
        {
            try
            {
                List<double> list = new List<double>();
                CN_GestorCobranza cn_gestor = new CN_GestorCobranza();

                cn_gestor.GraficaCobranza_Saldos(ref list, GeneraFiltros(), Emp_CnxCob);
                if (list[0] == 0)
                {
                    Label2.Text = "Entrega cobranza";
                    return NoDocumentos;
                }
                Label2.Text = "Entrega cobranza<BR>E=" + ((double)list[4]).ToString("##0.00") + "%";
                return InfoSoftGlobal.FusionCharts.RenderChartHTML("Charts/AngularGauge.swf", "", GraficaAngular_Saldos(list[0], list[1]), "myNext", "350", "170", false);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private string GeneraGraficaRevision()
        {
            try
            {
                List<object> list = new List<object>();
                CN_GestorCobranza cn_gestor = new CN_GestorCobranza();

                cn_gestor.GraficaRevision(ref list, GeneraFiltros(), Emp_CnxCob);
                Label3.Text = "Revisión<BR>E=" + ((double)list[4]).ToString("##0.00") + "%";
                return InfoSoftGlobal.FusionCharts.RenderChartHTML("Charts/AngularGauge.swf", "", GraficaAngular(list[3]), "myNext", "300", "170", false);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private string GeneraGraficaRevision_Saldos()
        {
            try
            {
                List<double> list = new List<double>();
                CN_GestorCobranza cn_gestor = new CN_GestorCobranza();

                cn_gestor.GraficaRevision_Saldos(ref list, GeneraFiltros(), Emp_CnxCob);
                if (list[0] == 0)
                {
                    Label3.Text = "Revisión";
                    return NoDocumentos;
                }
                Label3.Text = "Revisión<BR>E=" + ((double)list[4]).ToString("##0.00") + "%";
                return InfoSoftGlobal.FusionCharts.RenderChartHTML("Charts/AngularGauge.swf", "", GraficaAngular_Saldos(list[0], list[1]), "myNext", "350", "170", false);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private string GeneraGraficaVencidas()
        {
            try
            {
                List<object> list = new List<object>();
                CN_GestorCobranza cn_gestor = new CN_GestorCobranza();

                cn_gestor.GraficaVencidas(ref list, GeneraFiltros(), Emp_CnxCob);
                lblGraficaVencidos.Text = "Documentos por recuperar<BR>E=" + ((double)list[4]).ToString("##0.00") + "%";
                return InfoSoftGlobal.FusionCharts.RenderChartHTML("Charts/AngularGauge.swf", "", GraficaAngular(list[3]), "myNext", "300", "170", false);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private string GeneraGraficaVencidas_Saldos()
        {
            try
            {
                List<double> list = new List<double>();
                CN_GestorCobranza cn_gestor = new CN_GestorCobranza();

                cn_gestor.GraficaVencidas_Saldos(ref list, GeneraFiltros(), Emp_CnxCob);
                if (list[0] == 0)
                {
                    lblGraficaVencidos.Text = "Saldo por recuperar";
                    return NoDocumentos;
                }
                lblGraficaVencidos.Text = "Saldo por recuperar<BR>E=" + ((double)list[4]).ToString("##0.00") + "%";
                return InfoSoftGlobal.FusionCharts.RenderChartHTML("Charts/AngularGauge.swf", "", GraficaAngular_Saldos(list[0], list[1]), "myNext", "350", "170", false);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private string GeneraGraficaAvance()
        {
            try
            {
                List<object> list = new List<object>();
                CN_GestorCobranza cn_gestor = new CN_GestorCobranza();

                cn_gestor.GraficaVencidas(ref list, GeneraFiltros(), Emp_CnxCob);
                return InfoSoftGlobal.FusionCharts.RenderChartHTML("Charts/HLinearGauge.swf", "", GraficaLineal(list[3]), "myNext", "500", "105", false);
            }
            catch (Exception)
            {
                return "";
            }
        }
        private string GeneraGraficaAvance_Saldos()
        {
            try
            {
                List<double> list = new List<double>();
                CN_GestorCobranza cn_gestor = new CN_GestorCobranza();

                cn_gestor.GraficaVencidas_Saldos(ref list, GeneraFiltros(), Emp_CnxCob);

                if (list[0] == 0)
                {
                    return NoDocumentos;
                }
                return InfoSoftGlobal.FusionCharts.RenderChartHTML("Charts/HLinearGauge.swf", "", GraficaLineal_Saldos(list[0], list[1]), "myNext", "500", "105", false);
            }
            catch (Exception)
            {
                return "";
            }
        }
        private string GeneraGraficaCosto()
        {
            try
            {
                List<Comun> list = new List<Comun>();
                CN_GestorCobranza cn_gestor = new CN_GestorCobranza();

                cn_gestor.GraficaCosto(ref list, GeneraFiltros(), Emp_CnxCob);
                return InfoSoftGlobal.FusionCharts.RenderChartHTML("FusionCharts/Line.swf", "", GraficaBarras(list), "myNext", "400", "200", false);
            }
            catch (Exception)
            {
                return "";
            }
        }

        private string GeneraGraficaCartera()
        {
            try
            {
                List<Comun> list = new List<Comun>();
                CN_GestorCobranza cn_gestor = new CN_GestorCobranza();

                cn_gestor.GraficaRotacion(ref list, GeneraFiltros(), Emp_CnxCob);
                return InfoSoftGlobal.FusionCharts.RenderChartHTML("FusionCharts/Line.swf", "", GraficaBarras(list), "myNext", "400", "200", false);
            }
            catch (Exception)
            {
                return "";
            }
        }

        private string GeneraGraficaDiasVencidos()
        {
            try
            {
                List<Comun> list = new List<Comun>();
                CN_GestorCobranza cn_gestor = new CN_GestorCobranza();

                cn_gestor.GraficaDiasVencidos(ref list, GeneraFiltros(), Emp_CnxCob);
                return InfoSoftGlobal.FusionCharts.RenderChartHTML("FusionCharts/Line.swf", "", GraficaBarras(list), "myNext", "400", "200", false);
            }
            catch (Exception)
            {
                return "";
            }
        }

        private string GraficaBarras(List<Comun> list)
        {
            try
            {
                StringBuilder xmlData = new StringBuilder();

                string yaxsisname = "";

                xmlData.Append("<chart formatNumberScale='0' caption=' ' yAxisMaxValue='" + Math.Round((list.Max(Comun => Comun.ValorDoble) * 1.25), 0).ToString()/*.15*/ + "' yaxisname='" + yaxsisname + "' bgColor='FFFFFF'  bgAlpha='100' baseFontColor='000000' canvasBgAlpha='0' canvasBorderColor='696969' divLineColor='696969' divLineAlpha='100' numVDivlines='10' vDivLineisDashed='1' showAlternateVGridColor='1' lineColor='399E38' anchorRadius='4' anchorBgColor='BBDA00' anchorBorderColor='696969' anchorBorderThickness='1' showValues='0'  toolTipBgColor='FFFFFF' toolTipBorderColor='406181' alternateHGridAlpha='5' labelDisplay='ROTATE' canvaspadding='6' showBorder='0'>");
                foreach (Comun c in list)
                {
                    xmlData.Append("<set label='" + c.ValorDateTime.ToString("dd/MMM") + "' value='" + c.ValorDoble.ToString("#0.00") + "'  distance='6'/>");
                }

                xmlData.Append("<styles>");
                xmlData.Append("<definition>");
                xmlData.Append("<style name='LineShadow' type='shadow' color='333333' distance='6'/>");
                xmlData.Append("</definition>");
                xmlData.Append("<application>");
                xmlData.Append("<apply toObject='DATAPLOT' styles='LineShadow' />");
                xmlData.Append("</application>");
                xmlData.Append("</styles>");
                xmlData.Append("</chart>");

                return xmlData.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GraficaAngular(object valor)
        {
            try
            {
                string valorSTR = ((double)valor).ToString("##0.00") + "%25";
                StringBuilder xmlData = new StringBuilder();
                xmlData.Append("<chart manageResize='1' origW='420' origH='250' showValue='0' manageValueOverlapping='1' autoAlignTickValues='1' bgColor='FFFFFF' fillAngle='45' upperLimit='100' lowerLimit='0' majorTMNumber='10' majorTMHeight='8' showGaugeBorder='0' gaugeOuterRadius='140' gaugeOriginX='205' gaugeOriginY='206' gaugeInnerRadius='2' formatNumberScale='1' numberSuffix='%25' pivotRadius='17' showPivotBorder='0' pivotBorderColor='000000' pivotBorderThickness='5' pivotFillMix='FFFFFF,000000' tickValueDistance='10' showBorder='0' tickValueStep='10' >");
                xmlData.Append("<colorRange>");
                //xmlData.Append("<color minValue='0' maxValue='50' code='B40001'/>");
                //xmlData.Append("<color minValue='25' maxValue='50' code='e2e754'/>");
                //xmlData.Append("<color minValue='50' maxValue='100' code='399E38'/>");
                xmlData.Append("<color minValue='0' maxValue='" + valor + "' code='399E38'/>");
                xmlData.Append("<color minValue='" + valor + "' maxValue='100' code='B40001'/>");
                xmlData.Append("</colorRange>");
                xmlData.Append("<dials>");
                xmlData.Append("<dial value='" + valor.ToString() + "' borderAlpha='0' bgColor='000000' baseWidth='15' topWidth='1' radius='130' />");
                xmlData.Append("</dials>");
                xmlData.Append("<trendpoints>");
                xmlData.Append("<point startValue='" + valor + "' displayValue='" + valorSTR + "' useMarker='0' markerRadius='0' dashed='1' dashLen='2' dashGap='2'  />");
                xmlData.Append("</trendpoints>");
                xmlData.Append("<annotations>");
                xmlData.Append("<!-- Draw the background arcs -->");
                xmlData.Append("<annotationGroup x='205' y='207.5'>");
                xmlData.Append("<annotation type='circle' x='0' y='2.5' radius='150' startAngle='0' endAngle='180' fillPattern='linear' fillAsGradient='1' fillColor='dddddd,666666' fillAlpha='100,100' fillRatio='50,50' fillAngle='0' showBorder='1' borderColor='444444' borderThickness='2'/>");
                xmlData.Append("<annotation type='circle' x='0' y='0' radius='145' startAngle='0' endAngle='180' fillPattern='linear' fillAsGradient='1' fillColor='666666,ffffff' fillAlpha='100,100' fillRatio='50,50' fillAngle='0' />");
                xmlData.Append("</annotationGroup>");
                xmlData.Append("</annotations>");
                xmlData.Append("<styles>");
                xmlData.Append("<definition>");
                xmlData.Append("<style type='font' name='myValueFont' bgColor='F1f1f1' borderColor='999999' />");
                xmlData.Append("</definition>");
                xmlData.Append("<application>");
                xmlData.Append("<apply toObject='Value' styles='myValueFont' />");
                xmlData.Append("</application>");
                xmlData.Append("</styles>");
                xmlData.Append("</chart>");
                return xmlData.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GraficaAngular_Saldos(double valor1, double valor2)
        {
            try
            {
                double rango1 = valor1 / 2.0;
                double rango2 = rango1 / 2.0;
                string valorSTR = "$" + ((double)valor2).ToString("#,###,##0.00");
                StringBuilder xmlData = new StringBuilder();
                xmlData.Append("<chart adjustTM='0' manageResize='1' origW='440' origH='250' showValue='0' manageValueOverlapping='1' autoAlignTickValues='1' bgColor='FFFFFF' fillAngle='45' upperLimit='" + valor1 + "' lowerLimit='0' majorTMNumber='5' minorTMNumber='5' majorTMHeight='8' showGaugeBorder='0' gaugeOuterRadius='140' gaugeOriginX='205' gaugeOriginY='206' gaugeInnerRadius='2' formatNumberScale='0' numberprefix='$' pivotRadius='17' showPivotBorder='0' pivotBorderColor='000000' pivotBorderThickness='5' pivotFillMix='FFFFFF,000000' tickValueDistance='10' forceDecimals='1' showBorder='0' tickValueStep='10'>");
                xmlData.Append("<colorRange>");
                //xmlData.Append("<color minValue='0' maxValue='" + rango1 + "' code='B40001'/>");
                //xmlData.Append("<color minValue='" + rango2 + "' maxValue='" + rango1 + "' code='e2e754'/>");
                //xmlData.Append("<color minValue='" + rango1 + "' maxValue='" + valor1 + "' code='399E38'/>");
                xmlData.Append("<color minValue='0' maxValue='" + valor2 + "' code='399E38'/>");
                xmlData.Append("<color minValue='" + valor2 + "' maxValue='" + valor1 + "' code='B40001'/>");
                xmlData.Append("</colorRange>");
                xmlData.Append("<dials>");
                xmlData.Append("<dial value='" + valor2.ToString() + "' borderAlpha='0' bgColor='000000' baseWidth='15' topWidth='1' radius='130'/>");
                xmlData.Append("</dials>");
                xmlData.Append("<trendpoints>");
                xmlData.Append("<point startValue='" + valor2 + "' displayValue='" + valorSTR + "' useMarker='0' markerRadius='0' dashed='1' dashLen='2' dashGap='2'  />");
                xmlData.Append("</trendpoints>");
                xmlData.Append("<annotations>");
                xmlData.Append("<!-- Draw the background arcs -->");
                xmlData.Append("<annotationGroup x='205' y='207.5'>");
                xmlData.Append("<annotation type='circle' x='0' y='2.5' radius='150' startAngle='0' endAngle='180' fillPattern='linear' fillAsGradient='1' fillColor='dddddd,666666' fillAlpha='100,100' fillRatio='50,50' fillAngle='0' showBorder='1' borderColor='444444' borderThickness='2'/>");
                xmlData.Append("<annotation type='circle' x='0' y='0' radius='145' startAngle='0' endAngle='180' fillPattern='linear' fillAsGradient='1' fillColor='666666,ffffff' fillAlpha='100,100' fillRatio='50,50' fillAngle='0' />");
                xmlData.Append("</annotationGroup>");
                xmlData.Append("</annotations>");
                xmlData.Append("<styles>");
                xmlData.Append("<definition>");
                xmlData.Append("<style type='font' name='myValueFont' bgColor='F1f1f1' borderColor='999999' />");
                xmlData.Append("</definition>");
                xmlData.Append("<application>");
                xmlData.Append("<apply toObject='Value' styles='myValueFont' />");
                xmlData.Append("</application>");
                xmlData.Append("</styles>");
                xmlData.Append("</chart>");
                return xmlData.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GraficaLineal(object valor)
        {
            StringBuilder xmlData = new StringBuilder();
            xmlData.Append("<chart manageResize='1' bgColor='FFFFFF' borderColor='DCCEA1' chartTopMargin='20' chartLeftMargin='100' chartRightMargin='100' numberSuffix='%25' chartBottomMargin='0' upperLimit='100' lowerLimit='0' ticksBelowGauge='1' tickMarkDistance='3' valuePadding='-2' pointerRadius='5' majorTMColor='000000' majorTMNumber='3' minorTMNumber='4' minorTMHeight='4' majorTMHeight='8' showShadow='0' pointerBgColor='FFFFFF' pointerBorderColor='000000' gaugeBorderThickness='3' baseFontColor='000000' gaugeFillMix='{color},{FFFFFF}' gaugeFillRatio='50,50' forceDecimals='1' showBorder='0'>");
            xmlData.Append("<colorRange>");
            xmlData.Append("<color minValue='0' maxValue='50' code='B40001' borderColor='B40001' label=''/>");
            xmlData.Append("<color minValue='50' maxValue='100' code='5C8F0E' label='' />");
            xmlData.Append("</colorRange>");
            xmlData.Append("<pointers>");
            xmlData.Append("<pointer value='" + valor + "' radius='10'/>");
            xmlData.Append("</pointers>");
            xmlData.Append("<styles>");
            xmlData.Append("<definition>");
            xmlData.Append("<style name='limitFont' type='Font' bold='1'/>");
            xmlData.Append("<style name='labelFont' type='Font' bold='1' size='10' color='FFFFFF'/>");
            xmlData.Append("</definition>");
            xmlData.Append("<application>");
            xmlData.Append("<apply toObject='GAUGELABELS' styles='labelFont' />");
            xmlData.Append("<apply toObject='LIMITVALUES' styles='limitFont' />");
            xmlData.Append("</application>");
            xmlData.Append("</styles>");
            xmlData.Append("</chart>");
            return xmlData.ToString();
        }
        private string GraficaLineal_Saldos(double valor1, double valor2)
        {
            StringBuilder xmlData = new StringBuilder();
            xmlData.Append("<chart manageResize='1' bgColor='FFFFFF' borderColor='DCCEA1' chartTopMargin='20' chartLeftMargin='100' chartRightMargin='100' numberPrefix='$' chartBottomMargin='0' upperLimit='" + valor1 + "' lowerLimit='0' ticksBelowGauge='1' tickMarkDistance='3' valuePadding='-2' pointerRadius='5' majorTMColor='000000' majorTMNumber='3' minorTMNumber='4' minorTMHeight='4' majorTMHeight='8' showShadow='0' pointerBgColor='FFFFFF' pointerBorderColor='000000' gaugeBorderThickness='3' baseFontColor='000000' gaugeFillMix='{color},{FFFFFF}' gaugeFillRatio='50,50' forceDecimals='1' showBorder='0'>");
            xmlData.Append("<colorRange>");
            xmlData.Append("<color minValue='0' maxValue='" + (valor1 / 2).ToString() + "' code='B40001' borderColor='B40001' label=''/>");
            xmlData.Append("<color minValue='" + (valor1 / 2).ToString() + "' maxValue='" + valor1.ToString() + "' code='5C8F0E' label='' />");
            xmlData.Append("</colorRange>");
            xmlData.Append("<pointers>");
            xmlData.Append("<pointer value='" + valor2 + "' radius='10'/>");
            xmlData.Append("</pointers>");
            xmlData.Append("<styles>");
            xmlData.Append("<definition>");
            xmlData.Append("<style name='limitFont' type='Font' bold='1'/>");
            xmlData.Append("<style name='labelFont' type='Font' bold='1' size='10' color='FFFFFF'/>");
            xmlData.Append("</definition>");
            xmlData.Append("<application>");
            xmlData.Append("<apply toObject='GAUGELABELS' styles='labelFont' />");
            xmlData.Append("<apply toObject='LIMITVALUES' styles='limitFont' />");
            xmlData.Append("</application>");
            xmlData.Append("</styles>");
            xmlData.Append("</chart>");
            return xmlData.ToString();
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