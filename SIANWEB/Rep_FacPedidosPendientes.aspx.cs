using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using CapaEntidad;
using System.Collections;
using CapaNegocios;
using Telerik.Web.UI;
using Telerik.Reporting.Processing;
using System.IO;
using System.Data;

namespace SIANWEB
{
    public partial class Rep_FacPedidos : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        RadioButtonList1.SelectedIndex = 0;
                        ValidarPermisos();
                        CargarCentros();
                        CargarCombos();
                        if (!Sesion.Cu_Modif_Pass_Voluntario)
                            RadAjaxManager1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
                        dpFecha2.DbSelectedDate = Sesion.CalendarioIni;
                        dpFecha3.DbSelectedDate = Sesion.CalendarioFin;
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Page_Load");
            }
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {

        }
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            }
            catch (Exception)
            {
                throw;
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
                    case "print":
                        Abrir_Reporte(true);
                        break;
                    case "excel":
                        Abrir_Reporte(false);
                        break;
                    case "Listado":
                        GenerarExcel();
                    break;
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
        #endregion
        #region Metodos
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
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("print")).Visible = _PermisoImprimir;
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Abrir_Reporte(bool a_pantalla)
        {
            try
            {
                if (validarCadena(txtTerritorio.Text) || validarCadena(txtNumeroCliente.Text) || validarCadena(txtProducto.Text))
                    return;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                //Consulta centro de distribución
                string Emp_Nombre = "";
                string Cd_Nombre = "";
                string U_Nombre = "";
                new CN_CapPedido().ConsultarEncabezado_RepFacPedidosPendientes(sesion, ref Emp_Nombre, ref Cd_Nombre, ref U_Nombre);
                ALValorParametrosInternos.Add(txtTerritorio.Text == "" ? "Todos" : txtTerritorio.Text);//Encabezado Territorios
                ALValorParametrosInternos.Add(txtNumeroCliente.Text == "" ? "Todos" : txtNumeroCliente.Text);//Encabezado Clientes
                ALValorParametrosInternos.Add(txtProducto.Text == "" ? "Todos" : txtProducto.Text);//Encabezado Productos
                ALValorParametrosInternos.Add(RadioButtonList1.SelectedItem.Text);//opcion
                ALValorParametrosInternos.Add(Emp_Nombre);//nombre empresa
                ALValorParametrosInternos.Add(Cd_Nombre);//nombre sucursal
                ALValorParametrosInternos.Add(sesion.U_Nombre);//usuario
                ALValorParametrosInternos.Add(DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString());//fecha

                //parametros para el cuerpo del reporte
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(txtTerritorio.Text == "" ? (object)null : txtTerritorio.Text);//territorio
                ALValorParametrosInternos.Add(txtNumeroCliente.Text == "" ? (object)null : txtNumeroCliente.Text);//clientes
                ALValorParametrosInternos.Add(txtProducto.Text == "" ? (object)null : txtProducto.Text);//productos
                ALValorParametrosInternos.Add(dpFecha2.SelectedDate);
                ALValorParametrosInternos.Add(dpFecha3.SelectedDate);
                ALValorParametrosInternos.Add(cmbPedido.SelectedValue == "T" ? null : cmbPedido.SelectedValue);

                //conexion
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                Type instance = null;

                if (RadioButtonList1.SelectedValue == "1")
                {
                    if (a_pantalla)
                        instance = typeof(LibreriaReportes.Rep_FacPedidosPendientes);
                    else
                        instance = typeof(LibreriaReportes.ExpRep_FacPedidosPendientes);
                }
                if (RadioButtonList1.SelectedValue == "2")
                {
                    if (a_pantalla)
                        instance = typeof(LibreriaReportes.Rep_FacPedidosPendientes_Det);
                    else
                        instance = typeof(LibreriaReportes.ExpRep_FacPedidosPendientes_Det);
                }

                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                if (a_pantalla)
                {
                    Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = null;
                    Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                    Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                    RadAjaxManager1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
                }
                else
                {
                    ImprimirXLS(ALValorParametrosInternos, instance);
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

                RadAjaxManager1.ResponseScripts.Add("startDownload('" + instance.Name + ".xls');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool validarCadena(string cadena)
        {
            string[] split = cadena.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string a in split)
            {
                if (a.StartsWith("-"))
                {
                    Alerta("El rango " + a + " no es válido");
                    return true;
                }
            }
            return false;
        }
        private void GenerarExcel()
        {
            try
            {

                StringBuilder tabla = new StringBuilder();
                tabla.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'></head><body><table style='width:700px'>");
                EscribeEncabezado(ref tabla);
                EscribeDetalle(ref tabla);
                tabla.Append("</table></body></html>");
                ExportarExcel("Pedidos_Pendientes", tabla.ToString());

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void EscribeEncabezado(ref StringBuilder Tabla)
        {
            try
            {


                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
          
                Tabla.Append("<tr>");
                Tabla.Append("<td  colspan='15' style='width:400px; text-align:center; font-weight:bold'>&nbsp; " + sesion.Emp_Nombre + "  </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td  colspan='15' style='width:400px; text-align:center; font-weight:bold'>&nbsp; " + sesion.Cd_Nombre  + "  </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td  colspan='15' style='width:400px; text-align:center; font-weight:bold'>Listado de pedidos pendientes </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td  colspan='15' style='width:400px; text-align:left; font-weight:bold'>Usuario: "+ sesion.U_Nombre + " </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td  colspan='15' style='width:400px; text-align:left; font-weight:bold'>Fecha: " + DateTime.Now + " </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                string Territorios = txtTerritorio.Text == "" ? "Todos" : txtTerritorio.Text;
                Tabla.Append("<td  colspan='15' style='width:400px; text-align:left; font-weight:bold'>Territorio: " + Territorios + " </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                string Clientes = txtNumeroCliente.Text == "" ? "Todos" : txtNumeroCliente.Text;
                Tabla.Append("<td  colspan='15' style='width:400px; text-align:left; font-weight:bold'>Cliente: " +Clientes + " </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                string Producto = txtProducto.Text == "" ? "Todos" : txtProducto.Text;
                Tabla.Append("<td  colspan='15' style='width:400px; text-align:left; font-weight:bold'>Producto: " + Producto + " </td>");
                Tabla.Append("</tr>");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void EscribeDetalle(ref StringBuilder Tabla)
        {
            try
            {
                String width;

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();
                CN_CapPedido cn_ped = new CN_CapPedido();
                DataTable dt = null;

                pedido.Id_Emp = sesion.Id_Emp;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                pedido.Territorios = txtTerritorio.Text== ""? null : txtTerritorio.Text;
                pedido.Clientes = txtNumeroCliente.Text == "" ? null : txtNumeroCliente.Text;
                pedido.Productos = txtProducto.Text == ""? null : txtProducto.Text;
                pedido.FechaIni = dpFecha2.SelectedDate.ToString() == "" ? (DateTime?) null: dpFecha2.SelectedDate;
                pedido.FechaFin = dpFecha3.SelectedDate.ToString() == "" ? (DateTime?)null : dpFecha3.SelectedDate;
                pedido.Pedidos = cmbPedido.SelectedValue == "T" ? null : cmbPedido.SelectedValue;

                cn_ped.PedidosPendientes_ConsultaReporte(pedido, ref dt, sesion.Emp_Cnx);
                 
                



                Tabla.Append("<tr>");

                //lectura y escritura de columnas
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    if (dt.Columns[i].ColumnName == "Territorio")
                    {
                        width = (i == 0) ? "210px" : "240px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Territorio");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Cliente")
                    {
                        width = (i == 0) ? "400px" : "430px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Cliente");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Id_Ped")
                    {
                        width = (i == 0) ? "40px" : "60px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Pedido");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Ped_Fecha")
                    {
                        width = (i == 0) ? "70px" : "80px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Fecha");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Id_Prd")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Código");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Prd_Descripcion")
                    {
                        width = (i == 0) ? "290px" : "320px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Descripción");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "PresentacionUnidadesEmp")
                    {
                        width = (i == 0) ? "70px" : "80px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Presentación");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Pedida")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Pedida");
                        Tabla.Append("</th>");
                    }

                    else if (dt.Columns[i].ColumnName == "Asignada")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Asignada");
                        Tabla.Append("</th>");
                    }

                    else if (dt.Columns[i].ColumnName == "Pendiente")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Pendiente");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "PendienteReal")
                    {
                        width = (i == 0) ? "90px" : "120px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Pendiente real");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Porc")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("%");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Ped_Precio")
                    {
                        width = (i == 0) ? "90px" : "120px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Precio U.");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "ImportePendiente")
                    {
                        width = (i == 0) ? "90px" : "120px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Importe pendiente");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "ImportePendienteReal")
                    {
                        width = (i == 0) ? "90px" : "120px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Importe pendiente real");
                        Tabla.Append("</th>");
                    }


                }
                Tabla.Append("</tr>");
                // lectura y escritura de filas
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Tabla.Append("<tr>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        if (dt.Columns[i].ColumnName == "Territorio")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Cliente")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Id_Ped")
                        {
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Ped_Fecha")
                        {

                            DateTime datetime = Convert.ToDateTime(dt.Rows[j][i].ToString());
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(datetime.ToShortDateString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Id_Prd")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Prd_Descripcion")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "PresentacionUnidadesEmp")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Pedida")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Asignada")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Pendiente")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "PendienteReal")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Porc")
                        {
                            double valor = Convert.ToDouble(dt.Rows[j][i].ToString());
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(valor.ToString("N2") );
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Ped_Precio")
                        {
                            double valor = Convert.ToDouble(dt.Rows[j][i].ToString());
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(valor.ToString("N2"));
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "ImportePendiente")
                        {
                            double valor = Convert.ToDouble(dt.Rows[j][i].ToString());
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(valor.ToString("N2"));
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "ImportePendienteReal")
                        {
                            double valor = Convert.ToDouble(dt.Rows[j][i].ToString());
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(valor.ToString("N2"));
                            Tabla.Append("</td>");
                        }





                    }
                }
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>");
                Tabla.Append("&nbsp; &nbsp;</td>");
                Tabla.Append("</tr>");



            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void ExportarExcel(String nombreArchivo, String tabla)
        {
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + nombreArchivo + ".xls");
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"; //Excel
                System.IO.StringWriter sw = new System.IO.StringWriter();
                sw.WriteLine("<html xmlns='http://www.w3.org/1999/xhtml'>");
                sw.WriteLine("<head>");
                sw.WriteLine("<meta http-equiv='content-type' content='text/html; charset=UTF-8' />");
                sw.WriteLine("<title>");
                sw.WriteLine("Page-");
                sw.WriteLine(Guid.NewGuid().ToString());
                sw.WriteLine("</title>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");
                sw.Write(tabla);
                sw.WriteLine("</body>");
                sw.WriteLine("</html>");
                HttpContext.Current.Response.Output.Write(sw.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
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
        #endregion
        #region ErrorManager
        private void Alerta(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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
