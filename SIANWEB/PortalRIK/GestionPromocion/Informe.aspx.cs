using CapaModelo;
using SIANWEB.Core.UI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CapaEntidad;
using Telerik.Web.UI;

using CapaNegocios;
using System.Data;
using System.IO;
using System.Text;
using System.Collections;

namespace SIANWEB.PortalRIK.GestionPromocion
{
    public partial class Informe : BaseServerPage
    {
        public int Id_TU1; // Tipo Usaurio 3.- Gerente         
        public int Id_Rik; // Representante Institucional Key RIK , para recibir parametro 
        public int Id_CD;
        public string CDI_Nombre;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["activeMenu"] = 9;
            }

            try
            {
                if (ValidarSesion())
                {
                    if (!Page.IsPostBack)
                    {
                        //ValidarPermisos();
                        if (session.Cu_Modif_Pass_Voluntario == false)
                            return;
                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void Inicializar()
        {
            Id_TU1 = session.Id_TU;
            Id_CD = session.Id_Cd;
            Id_Rik= session.Id_Rik;
            CDI_Nombre = session.Cd_Nombre;
            
            //rbTipo.Items.Add(new ListItem("Control de la promoción nivel solución", "1")); // radControl              
            if (session.Id_TU == 2)
            {
                rbTipo.Items.Add(new ListItem("Control de la promoción nivel aplicación", "2")); // radControlAplicacion
                rbTipo.Items.Add(new ListItem("Control de Entradas", "3")); // radControlEntrada            
            } else  {
                rbTipo.Items.Add(new ListItem("Control de la promoción nivel aplicación", "2")); // radControlAplicacion
                rbTipo.Items.Add(new ListItem("Control de Entradas", "3")); // radControlEntrada
                rbTipo.Items.Add(new ListItem("DII en número de proyectos", "4")); // radDII
                rbTipo.Items.Add(new ListItem("DII en importe de proyectos", "5")); //radDIINumero
                rbTipo.Items.Add(new ListItem("Campañas", "6")); //radCampania
                rbTipo.Items.Add(new ListItem("Cierre de Mes", "7")); // radCierreMes                
            }    
            /*
                <option value="1">Control de la promoción nivel solución</option>
                <option value="2">Control de la promoción nivel aplicación</option>
                <option value="3">Control de Entradas</option>
                <option value="4">DII en número de proyectos</option>
                <option value="5">DII en importe de proyectos</option>
                <option value="6">Campañas</option>
                <option value="7">Cierre de Mes</option>
            */

            try
            {
                switch (session.Id_TU)
                {
                    case 3:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        break;
                    default:
                        break;
                }
                if (session.Id_TU != 3)
                {
                }
                if (_PermisoImprimir)
                {
                    //ibtnImprimir.OnClientClick = String.Format("printpage()", ibtnImprimir.UniqueID, "");
                }

                //MesesHistorial();
                //CargarMetas();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        
        protected void radControl_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.radControl.Checked)
            //    {
            //        ibtnExcelSolucion.Visible = true;
            //        ibtnExcelAplicacion.Visible = false;
            //        ibtnExcelImporte.Visible = false;
            //        ibtnExcelNumero.Visible = false;
            //        pnlRepresentante.Visible = true;
            //        ibtnExcelControlEntrada.Visible = false;
            //        ibtnExcelCampania.Visible = false;
            //        chkProyectoNuevo.Visible = false;
            //        lNuevo.Visible = false;
            //        IbtnExcelCierreMes.Visible = false;
            //        chkProyectoNuevo.Checked = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        protected void radControlAplicacion_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.radControlAplicacion.Checked)
            //    {
            //        ibtnExcelSolucion.Visible = false;
            //        ibtnExcelAplicacion.Visible = true;
            //        ibtnExcelImporte.Visible = false;
            //        ibtnExcelNumero.Visible = false;
            //        ibtnExcelCampania.Visible = false;
            //        ibtnExcelControlEntrada.Visible = false;
            //        pnlRepresentante.Visible = true;
            //        chkProyectoNuevo.Visible = true;
            //        lNuevo.Visible = true;
            //        IbtnExcelCierreMes.Visible = false;
            //        chkProyectoNuevo.Checked = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        protected void radControlEntrada_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.radControlEntrada.Checked)
            //    {
            //        ibtnExcelSolucion.Visible = false;
            //        ibtnExcelAplicacion.Visible = false;
            //        ibtnExcelImporte.Visible = false;
            //        ibtnExcelNumero.Visible = false;
            //        ibtnExcelControlEntrada.Visible = true;
            //        pnlRepresentante.Visible = false;
            //        ibtnExcelCampania.Visible = false;
            //        chkProyectoNuevo.Visible = false;
            //        lNuevo.Visible = false;
            //        IbtnExcelCierreMes.Visible = false;
            //        chkProyectoNuevo.Checked = false;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }


        protected void radDII_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.radDII.Checked)
            //    {
            //        ibtnExcelSolucion.Visible = false;
            //        ibtnExcelAplicacion.Visible = false;
            //        ibtnExcelImporte.Visible = false;
            //        ibtnExcelNumero.Visible = true;
            //        pnlRepresentante.Visible = false;
            //        ibtnExcelCampania.Visible = false;
            //        ibtnExcelControlEntrada.Visible = false;
            //        chkProyectoNuevo.Visible = false;
            //        lNuevo.Visible = false;
            //        IbtnExcelCierreMes.Visible = false;
            //        chkProyectoNuevo.Checked = false;

            //    }

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }


        protected void radDIINumero_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.radDIINumero.Checked)
            //    {
            //        ibtnExcelSolucion.Visible = false;
            //        ibtnExcelAplicacion.Visible = false;
            //        ibtnExcelImporte.Visible = true;
            //        ibtnExcelNumero.Visible = false;
            //        pnlRepresentante.Visible = false;
            //        ibtnExcelCampania.Visible = false;
            //        ibtnExcelControlEntrada.Visible = false;
            //        chkProyectoNuevo.Visible = false;
            //        lNuevo.Visible = false;
            //        IbtnExcelCierreMes.Visible = false;
            //        chkProyectoNuevo.Checked = false;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        protected void radCampania_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.radCampania.Checked)
            //    {
            //        ibtnExcelSolucion.Visible = false;
            //        ibtnExcelAplicacion.Visible = false;
            //        ibtnExcelImporte.Visible = false;
            //        ibtnExcelNumero.Visible = false;
            //        pnlRepresentante.Visible = false;
            //        ibtnExcelControlEntrada.Visible = false;
            //        ibtnExcelCampania.Visible = true;
            //        chkProyectoNuevo.Visible = false;
            //        lNuevo.Visible = false;
            //        IbtnExcelCierreMes.Visible = false;
            //        chkProyectoNuevo.Checked = false;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        protected void ibtnExcelImporte_Click(object sender, ImageClickEventArgs e)
        {
            if (!_PermisoImprimir)
            {
                Alerta("No tiene permiso para imprimir");
                return;
            }

            //if (!radControl.Checked && !radDII.Checked && !radControlAplicacion.Checked && !radDIINumero.Checked && !radControlEntrada.Checked && !radCampania.Checked && !radCierreMes.Checked)
            //{
            //    Alerta(string.Concat("Seleccione una opción de control"));
            //    return;
            //}

            string ddPeriodo = "";

            string periodo = !string.IsNullOrEmpty(ddPeriodo) ? Convert.ToString(ddPeriodo) : "-1*-1";

            if (periodo != "-1*-1")
                ExportarDIINumero();
        }

        private void ExportarDIINumero()
        {
            Session["dtRepresentantes"] = null;
            DataSet dsDII = new DataSet();
            System.IO.StreamWriter sw = null;
            string ruta = null;
            string tipo = "";
            tipo = "imp";

            int ddlZona = 0; // RFH this.ddlZonas.SelectedValue
            string ddPeriodo = "";

            decimal totalA = default(decimal);
            decimal totalP = default(decimal);
            decimal totalN = default(decimal);
            decimal totalMonto = default(decimal);
            decimal totalC = default(decimal);
            decimal totalX = default(decimal);
            ruta = Server.MapPath("Reportes") + "\\Reporte" + this.session.Id_U.ToString() + tipo + ".txt";
            String NombreArchivo = "Reporte" + this.session.Id_U.ToString() + tipo + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".xls";

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\" + NombreArchivo))
                File.Delete(Server.MapPath("Reportes") + "\\" + NombreArchivo);
            sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            string RepID = null;
            string ZonaID = null;

            string ddlRepresentantesComercial = "";

            if (ddlRepresentantesComercial== string.Empty || Convert.ToInt32(ddlRepresentantesComercial) == -1)
                RepID = "%";
            else
                RepID = ddlRepresentantesComercial;
            if (Convert.ToInt32(ddlZona) == -1)
                ZonaID = "%";
            else
                ZonaID = ddlZona.ToString();


            int periodo = !string.IsNullOrEmpty(ddPeriodo) ? Convert.ToInt32(ddPeriodo) : -1;

            new CN_CrmInformes().spCRM_ControlPromocion_DIINumero(
                       this.session.Id_Emp
                       , Convert.ToInt32(ddlZona) == -1 ? session.Id_Cd_Ver : Convert.ToInt32(ddlZona)
                       , periodo
                       , RepID
                       , ref dsDII
                       , this.session.Emp_Cnx);
            if (dsDII != null)
            {
                if (!(dsDII.Tables[0].Rows.Count == 0))
                {
                    DataRow dr = null;
                    DataTable dt = new DataTable();
                    for (int i = 0; i <= dsDII.Tables[0].Rows.Count - 1; i++)
                    {
                        dr = dsDII.Tables[0].Rows[i];
                        if ((Session["dtRepresentantes"] != null))
                        {
                            dt = (DataTable)Session["dtRepresentantes"];
                        }
                        else
                        {
                            this.CrearColumnas();
                        }

                        this.CrearFilas(dr["ZonaID"].ToString(), dr["UsuarioID"].ToString(), dr["Representante"].ToString(), Convert.ToDouble(dr["A"]).ToString(), Convert.ToDouble(dr["P"]).ToString(), Convert.ToDouble(dr["N"]).ToString(), Convert.ToDouble(dr["Monto"]).ToString(), Convert.ToDouble(dr["C"]).ToString(), Convert.ToDouble(dr["X"]).ToString(), "0");

                    }
                    ///'''''''''ENCABEZADOS DEL REPORTE'''''''''''''''''
                    sw.WriteLine("<html>");
                    sw.WriteLine("<head>");
                    sw.WriteLine("</head>");
                    sw.WriteLine("<body>");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<strong>MODULO CRM - CONTROL DE LA PROMOCION - Vista DII</strong>");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<table border=1><font size=8pt>");
                    sw.WriteLine("<tr><td bgcolor=\"#DDDDDD\"><b>CDS</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>No. Rik</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Representante</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Análisis</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Presentación</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Negociación</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Monto proyecto</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Cierre</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Cancelación</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Efectividad cierre</td></tr>");
                    ///'''''''''TERMINAN ENCABEZADOS DEL REPORTE'''''''''''''''''
                    if ((Session["dtRepresentantes"] != null))
                    {
                        dt = (DataTable)Session["dtRepresentantes"];
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {

                            totalA += Convert.ToDecimal(dt.Rows[i]["A"]);
                            totalP += Convert.ToDecimal(dt.Rows[i]["P"]);
                            totalN += Convert.ToDecimal(dt.Rows[i]["N"]);
                            totalMonto += Convert.ToDecimal(dt.Rows[i]["Monto"]);
                            totalC += Convert.ToDecimal(dt.Rows[i]["C"]);
                            totalX += Convert.ToDecimal(dt.Rows[i]["X"]);

                            if (Convert.ToDouble(dt.Rows[i]["C"]) > 0)
                            {
                                dt.Rows[i]["Efectividad"] = Convert.ToDecimal((Convert.ToDecimal(dt.Rows[i]["C"]) / (Convert.ToDecimal(dt.Rows[i]["C"]) + Convert.ToDecimal(dt.Rows[i]["X"])))) * 100;
                                dt.Rows[i]["Efectividad"] = dt.Rows[i]["Efectividad"] + "%";
                            }
                            //Formando texto de la tabla
                            sw.WriteLine("<tr>");
                            sw.WriteLine("<td>" + dt.Rows[i]["CDS"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["UsuarioID"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Representante"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["A"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["P"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["N"] + "</td>");
                            sw.WriteLine("<td>" + string.Concat("$", dt.Rows[i]["Monto"]) + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["C"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["X"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Efectividad"] + "</td>");
                            sw.WriteLine("</tr>");
                        }
                        sw.WriteLine("<tr>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalA.ToString("C") + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalP.ToString("C") + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalN.ToString("C") + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalMonto.ToString("C") + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalC.ToString("C") + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalX.ToString("C") + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("</tr>");

                        sw.WriteLine("</font></table>");
                        sw.WriteLine("</body>");
                        sw.WriteLine("</html>");
                        sw.Close();
                        if (File.Exists(ruta))
                        {
                            string ruta2 = null;
                            ruta2 = Server.MapPath("Reportes") + "\\" + NombreArchivo;
                            File.Move(ruta, Server.MapPath("Reportes") + "\\" + NombreArchivo);
                            Response.Redirect("Reportes\\" + NombreArchivo);
                        }
                    }
                    //Calculando la Efectividad
                }
                else
                {
                    Alerta("No se encontraron proyectos de promoción");
                }
            }
        }

        public void CrearFilas(string CDS, string UsuarioID, string Representante, string A, string P, string N, string Monto, string C, string X, string Efectividad)
        {
            DataTable dtRepresentantes = new DataTable();
            dtRepresentantes = (DataTable)Session["dtRepresentantes"];
            DataRow drFila = null;
            drFila = dtRepresentantes.NewRow();
            drFila["CDS"] = CDS;
            drFila["UsuarioID"] = UsuarioID;
            drFila["Representante"] = Representante;
            drFila["A"] = A;
            drFila["P"] = P;
            drFila["N"] = N;
            drFila["Monto"] = Monto;
            drFila["C"] = C;
            drFila["X"] = X;
            drFila["Efectividad"] = Efectividad;
            dtRepresentantes.Rows.Add(drFila);
            dtRepresentantes.AcceptChanges();
            Session["dtRepresentantes"] = dtRepresentantes;
        }

        private void CrearColumnas()
        {
            if (Session["dtRepresentantes"] == null)
            {
                DataTable dtRepresentantes = new DataTable();
                DataColumn dcCDS = new DataColumn("CDS", Type.GetType("System.String"));
                DataColumn dcUsuarioID = new DataColumn("UsuarioID", Type.GetType("System.String"));
                DataColumn dcRepresentante = new DataColumn("Representante", Type.GetType("System.String"));
                DataColumn dcA = new DataColumn("A", Type.GetType("System.String"));
                DataColumn dcP = new DataColumn("P", Type.GetType("System.String"));
                DataColumn dcN = new DataColumn("N", Type.GetType("System.String"));
                DataColumn dcMonto = new DataColumn("Monto", Type.GetType("System.String"));
                DataColumn dcC = new DataColumn("C", Type.GetType("System.String"));
                DataColumn dcX = new DataColumn("X", Type.GetType("System.String"));
                DataColumn dcEfectividad = new DataColumn("Efectividad", Type.GetType("System.String"));

                dtRepresentantes.Columns.Add(dcCDS);
                dtRepresentantes.Columns.Add(dcUsuarioID);
                dtRepresentantes.Columns.Add(dcRepresentante);
                dtRepresentantes.Columns.Add(dcA);
                dtRepresentantes.Columns.Add(dcP);
                dtRepresentantes.Columns.Add(dcN);
                dtRepresentantes.Columns.Add(dcMonto);
                dtRepresentantes.Columns.Add(dcC);
                dtRepresentantes.Columns.Add(dcX);
                dtRepresentantes.Columns.Add(dcEfectividad);
                Session["dtRepresentantes"] = dtRepresentantes;
            }
        }


        protected void ibtnExcelCampania_Click(object sender, ImageClickEventArgs e)
        {
            if (!_PermisoImprimir)
            {
                Alerta("No tiene permiso para imprimir");
                return;
            }

            //if (!radControl.Checked && !radDII.Checked && !radControlAplicacion.Checked && !radDIINumero.Checked && !radControlEntrada.Checked && !radCampania.Checked && !radCierreMes.Checked)
            //{
            //    Alerta(string.Concat("Seleccione una opción de control"));
            //    return;
            //}

            string ddPeriodo = "";

            string periodo = !string.IsNullOrEmpty(ddPeriodo) ? Convert.ToString(ddPeriodo) : "-1*-1";

            if (periodo != "-1*-1")
            {
                this.ExportarCampania();
            }
        }

        private void ExportarCampania()
        {
            Session["dtRepresentantes"] = null;
            DataSet dsCE = new DataSet();
            string tipo = "";
            tipo = "Campana";
            System.IO.StreamWriter sw = null;
            string ruta = null;

            string ddlRepresentantesComercial ="";

            int ddlZonas = 0;

            string ddPeriodo = "";

            ruta = Server.MapPath("Reportes") + "\\Reporte" + this.session.Id_U.ToString() + tipo + ".txt";
            String NombreArchivo = "Reporte" + this.session.Id_U.ToString() + tipo + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".xls";


            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\" + NombreArchivo))
                File.Delete(Server.MapPath("Reportes") + "\\" + NombreArchivo);
            sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            string RepID = null;
            string ZonaID = null;
            string Representante = null;
            if (ddlRepresentantesComercial== string.Empty || Convert.ToInt32(ddlRepresentantesComercial) == -1)
            {
                RepID = "%";
                Representante = "Todos";
            }
            else
            {
                RepID = ddlRepresentantesComercial;
                Representante = ddlRepresentantesComercial;
            }
            if (Convert.ToInt32(ddlZonas) == -1)
                ZonaID = "%";
            else
                ZonaID = ddlZonas.ToString();

            int periodo = !string.IsNullOrEmpty(ddPeriodo) ? Convert.ToInt32(ddPeriodo) : -1;

            new CN_CrmInformes().spCRM_Campana(this.session.Id_Emp, Convert.ToInt32(ddlZonas) == -1 ? session.Id_Cd_Ver : Convert.ToInt32(ddlZonas), periodo, RepID, ref dsCE, this.session.Emp_Cnx);

            if ((dsCE != null))
            {
                if (!(dsCE.Tables[0].Rows.Count == 0))
                {
                    DataRow dr = null;
                    DataTable dt = new DataTable();
                    for (int i = 0; i <= dsCE.Tables[0].Rows.Count - 1; i++)
                    {
                        dr = dsCE.Tables[0].Rows[i];
                        if ((Session["dtRepresentantes"] != null))
                            dt = (DataTable)Session["dtRepresentantes"];
                        else
                        {
                            this.CrearColumnasCampania();
                        }
                        this.CrearFilasCampania(Int32.Parse(dr["Id_Cam"].ToString()), dr["Campana"].ToString(), dr["CDS"].ToString(), dr["Id_Rik"].ToString(), dr["Representante"].ToString(), dr["Uen"].ToString(), dr["Segmento"].ToString(), dr["Area"].ToString(), dr["Solucion"].ToString(), dr["Aplicacion"].ToString(), dr["Producto"].ToString(), Int32.Parse(dr["Cantidad"].ToString()), float.Parse(dr["Efectividad"].ToString()), decimal.Parse(dr["TotalPesos"].ToString()));
                    }

                    ///'''''''''ENCABEZADOS DEL REPORTE'''''''''''''''''
                    sw.WriteLine("<html>");
                    sw.WriteLine("<head>");
                    sw.WriteLine("</head>");
                    sw.WriteLine("<body>");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<strong>MODULO CRM - Campañas </strong>");
                    sw.WriteLine("<br />");
                    sw.WriteLine("Representante: " + Representante + "");
                    sw.WriteLine("<br />");
                    sw.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + "");
                    sw.WriteLine("<br />");

                    sw.WriteLine("<br />");
                    sw.WriteLine("<table border=1><font size=8pt>");

                    sw.WriteLine("<tr><td bgcolor=\"#DDDDDD\"><b>Num. Campaña</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Campaña</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>CDS</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>No. Rik</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Representante</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Uen</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Segmento</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Area</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Solución</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Aplicación</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Producto</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Efectividad</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Cantidad</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Pesos</td></tr>");

                    ///'''''''''TERMINAN ENCABEZADOS DEL REPORTE'''''''''''''''''
                    if ((Session["dtRepresentantes"] != null))
                    {
                        dt = (DataTable)Session["dtRepresentantes"];
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            //Formando texto de la tabla

                            sw.WriteLine("<tr>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Id_Cam"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Campana"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["CDS"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Id_Rik"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Representante"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Uen"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Segmento"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Area"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Solucion"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Aplicacion"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Producto"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Efectividad"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Cantidad"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Pesos"] + "</td>");
                            sw.WriteLine("</tr>");
                        }
                        sw.WriteLine("<tr>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");

                        sw.WriteLine("</tr>");
                        sw.WriteLine("</font></table>");
                        sw.WriteLine("</body>");
                        sw.WriteLine("</html>");
                        sw.Close();
                        if (File.Exists(ruta))
                        {
                            string ruta2 = null;
                            ruta2 = Server.MapPath("Reportes") + "\\" + NombreArchivo;
                            File.Move(ruta, Server.MapPath("Reportes") + "\\" + NombreArchivo);
                            Response.Redirect("Reportes\\" + NombreArchivo, false);
                        }
                    }

                }
                else
                {
                    Alerta("No se encontraron Campañas");
                }
            }
        }

        public void CrearFilasCampania(int id_cam, string Campana, string CDS, string UsuarioID, string Representante, string Uen, string Segmento, string Area, string Solucion, string Aplicacion, string Producto, int Cantidad, float Efectividad, decimal TotalPesos)
        {
            DataTable dtRepresentantes = new DataTable();
            dtRepresentantes = (DataTable)Session["dtRepresentantes"];
            DataRow drFila = null;
            drFila = dtRepresentantes.NewRow();
            drFila["Id_Cam"] = id_cam;
            drFila["Campana"] = Campana;
            drFila["CDS"] = CDS;
            drFila["Id_Rik"] = UsuarioID;
            drFila["Representante"] = Representante;
            drFila["Uen"] = Uen;
            drFila["Segmento"] = Segmento;
            drFila["Area"] = Area;
            drFila["Solucion"] = Solucion;
            drFila["Aplicacion"] = Aplicacion;
            drFila["Producto"] = Producto;
            drFila["Cantidad"] = Cantidad;
            drFila["Efectividad"] = Efectividad;
            drFila["Pesos"] = TotalPesos;


            dtRepresentantes.Rows.Add(drFila);
            dtRepresentantes.AcceptChanges();
            Session["dtRepresentantes"] = dtRepresentantes;
        }



        protected void radCierreMes_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.radCierreMes.Checked)
            //    {
            //        ibtnExcelSolucion.Visible = false;
            //        ibtnExcelAplicacion.Visible = false;
            //        ibtnExcelImporte.Visible = false;
            //        ibtnExcelNumero.Visible = false;
            //        pnlRepresentante.Visible = true;
            //        ibtnExcelControlEntrada.Visible = false;
            //        ibtnExcelCampania.Visible = false;
            //        chkProyectoNuevo.Visible = false;
            //        lNuevo.Visible = false;
            //        IbtnExcelCierreMes.Visible = true;
            //        chkProyectoNuevo.Checked = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void CrearColumnasCampania()
        {
            if (Session["dtRepresentantes"] == null)
            {
                DataTable dtRepresentantes = new DataTable();
                DataColumn dcNumCampana = new DataColumn("Id_Cam", Type.GetType("System.String"));
                DataColumn dcCampana = new DataColumn("Campana", Type.GetType("System.String"));
                DataColumn dcCDS = new DataColumn("CDS", Type.GetType("System.String"));
                DataColumn dcUsuarioID = new DataColumn("Id_Rik", Type.GetType("System.String"));
                DataColumn dcRepresentante = new DataColumn("Representante", Type.GetType("System.String"));
                DataColumn dcUen = new DataColumn("Uen", Type.GetType("System.String"));
                DataColumn dcSegmento = new DataColumn("Segmento", Type.GetType("System.String"));
                DataColumn dcArea = new DataColumn("Area", Type.GetType("System.String"));
                DataColumn dcSolucion = new DataColumn("Solucion", Type.GetType("System.String"));
                DataColumn dcAplicacion = new DataColumn("Aplicacion", Type.GetType("System.String"));
                DataColumn dcProducto = new DataColumn("Producto", Type.GetType("System.String"));
                DataColumn dcCantidad = new DataColumn("Cantidad", Type.GetType("System.String"));
                DataColumn dcEfectividad = new DataColumn("EFectividad", Type.GetType("System.String"));
                DataColumn dcPesos = new DataColumn("Pesos", Type.GetType("System.String"));


                dtRepresentantes.Columns.Add(dcNumCampana);
                dtRepresentantes.Columns.Add(dcCampana);
                dtRepresentantes.Columns.Add(dcCDS);
                dtRepresentantes.Columns.Add(dcUsuarioID);
                dtRepresentantes.Columns.Add(dcRepresentante);
                dtRepresentantes.Columns.Add(dcUen);
                dtRepresentantes.Columns.Add(dcSegmento);
                dtRepresentantes.Columns.Add(dcArea);
                dtRepresentantes.Columns.Add(dcSolucion);
                dtRepresentantes.Columns.Add(dcAplicacion);
                dtRepresentantes.Columns.Add(dcProducto);
                dtRepresentantes.Columns.Add(dcCantidad);
                dtRepresentantes.Columns.Add(dcEfectividad);
                dtRepresentantes.Columns.Add(dcPesos);

                Session["dtRepresentantes"] = dtRepresentantes;
            }
        }


        protected void ddlZonas_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                this.CargarRepresentantes();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta(string.Concat("Error al cargar la página."));
            else
                Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }

        private bool ValidarSesion()
        {
            try
            {
                if (session == null)
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

        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception)
            {
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }

        private void CargarZonas()
        { //Cargar zonas (Centros de distribución) 

            //new CN__Comun().LlenaCombo(1, this.session.Id_Emp, this.session.Id_U, this.session.Emp_Cnx, "spCatCentroDistribucion_Combo", ref ddlZonas);
            //ddlZonas.SelectedIndex = ddlZonas.FindItemIndexByValue(this.session.Id_Cd_Ver.ToString());
            ////ddlZonas.Items[0].Remove();
            //if (session.Id_TU == 2 || session.Id_TU == 4 || session.Id_TU == 5)//representante
            //    ddlZonas.Enabled = false;

        }
        
        private void CargarRepresentantes()
        {//Cargar zonas (Centros de distribución)
            //try
            //{
            //    new CN__Comun().LlenaCombo(1, this.session.Id_Emp, Convert.ToInt32(ddlZonas.SelectedValue), session.Id_U, this.session.Emp_Cnx, "spCatRik_Combo", ref ddlRepresentantesComercial);
            //    if (!string.IsNullOrEmpty(this.session.Id_Rik.ToString()))
            //        if (this.session.Id_Rik > 0)
            //            ddlRepresentantesComercial.SelectedIndex = ddlRepresentantesComercial.FindItemIndexByValue(this.session.Id_Rik.ToString());
            //    if (session.Id_TU == 2 || session.Id_TU == 4 || session.Id_TU == 5)//representante
            //        ddlRepresentantesComercial.Enabled = false;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        protected void ibtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (!_PermisoImprimir)
                {
                    Alerta("No tiene permiso para imprimir");
                    return;
                }

                string ddPeriodo = "";

                //if (!radControl.Checked && !radDII.Checked && !radControlAplicacion.Checked && !radDIINumero.Checked && !radControlEntrada.Checked && !radCampania.Checked)
                //{
                //    Alerta(string.Concat("Seleccione una opción de control"));
                //    return;
                //}
                string periodo = !string.IsNullOrEmpty(ddPeriodo) ? Convert.ToString(ddPeriodo) : "-1*-1";

                if (periodo != "-1*-1")
                {
                    //switch (HiddenField1.Value)
                    //{
                    //    case "1": 
                    this.ExportarControlPromocion();
                    //        break;
                    //    case "2":
                    //        this.ExportarControlPromocion_LimpiezaAplicacion();
                    //        break;
                    //    case "3":
                    //        this.ExportarDII();
                    //        break;
                    //    case "4":
                    //        this.ExportarDIINumero();
                    //        break;

                    //}
                    //if (this.radControl.Checked)

                    //if (this.radDII.Checked)

                    //if (radControlAplicacion.Checked)

                    //if (this.radDIINumero.Checked)


                }
                else
                    Alerta(string.Concat("Seleccione un periodo"));
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(ex.Message);
            }
        }

        public void ExportarControlPromocion()
        {

            string ruta = null;
            string str = "";
            string tipo = "";
            //if (radControl.Checked)
            //{
            tipo = "Sol";
            //}
            //else if (radControlAplicacion.Checked)
            //{
            //    tipo = "Apl";
            //}
            //else if (radDII.Checked)
            //{
            //tipo = "num";
            //}
            //else
            //{
            //tipo = "imp";
            //}
            int intCancela = 0;
            int vintConsulta = 0;
            double vMonto1 = 0;
            double vMonto2 = 0;
            double totTeorico = 0;
            double totVenta = 0;
            string vRepID = null;
            int vZonaID = 0;

            string ddlZonas = "";
            string ddlRepresentantesComercial = "";

            string ddPeriodo = "";
            string txtDe = "";
            string txtA = "";
            
            if (this.session.Id_Cd_Ver != 0)
                vZonaID = this.session.Id_Cd_Ver;
            else
                vZonaID = Convert.ToInt32(ddlZonas);

            if (ddlRepresentantesComercial== string.Empty || Convert.ToInt32(ddlRepresentantesComercial) == -1)
                vRepID = null;
            else
                vRepID = ddlRepresentantesComercial;

            if (!string.IsNullOrEmpty(txtDe) & !string.IsNullOrEmpty(txtA))
            {
                vintConsulta = 1;
                vMonto1 = Convert.ToDouble(txtDe);
                vMonto2 = Convert.ToDouble(txtA);
            }
            else
            {
                vintConsulta = 0;
                vMonto1 = 0;
                vMonto2 = 0;
            }
            int periodo = !string.IsNullOrEmpty(ddPeriodo) ? Convert.ToInt32(ddPeriodo) : -1;

            String NombreArchivo = "Reporte" + this.session.Id_U.ToString() + tipo + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".xls";


            ruta = Server.MapPath("Reportes") + "\\Reporte" + this.session.Id_U.ToString() + tipo + ".txt";
            if ((string.IsNullOrEmpty(txtA) && !string.IsNullOrEmpty(txtDe)) | (!string.IsNullOrEmpty(txtA) && string.IsNullOrEmpty(txtDe)))
                this.DisplayMensajeAlerta("El rango introducido es incorrecto, falta un valor");
            else
            {
                if (vMonto1 > vMonto2)
                    this.DisplayMensajeAlerta("El rango inicial no debe ser mayor al rango final");
                else
                {
                    if (File.Exists(ruta))
                        File.Delete(ruta);
                    if (File.Exists(Server.MapPath("Reportes") + "\\" + NombreArchivo))
                        File.Delete(Server.MapPath("Reportes") + "\\" + NombreArchivo);

                    StreamWriter sw = null;
                    DataSet dsReporte = new DataSet();

                    new CN_CrmInformes().GenerarControlPromocion(this.session.Id_Emp,
                        Convert.ToInt32(ddlZonas) == -1 ? session.Id_Cd_Ver : Convert.ToInt32(ddlZonas),
                        session.Id_U,
                        ddlRepresentantesComercial, periodo, vintConsulta, txtDe, txtA, ref dsReporte, this.session.Emp_Cnx);

                    if ((dsReporte != null))
                    {
                        if (!(dsReporte.Tables[0].Rows.Count == 0))
                        {
                            try
                            {
                                sw = new StreamWriter(ruta, false, Encoding.UTF8);
                                //Obteniendo totales por etapa
                                double vA = 0;
                                double vP = 0;
                                double vN = 0;
                                double vC = 0;
                                double vCa = 0;
                                for (int m = 0; m <= dsReporte.Tables[0].Rows.Count - 1; m++)
                                {
                                    DataRow dr = null;
                                    dr = dsReporte.Tables[0].Rows[m];
                                    switch (dr["Estatus"].ToString().ToLower())
                                    {
                                        case "cancelados":
                                            vCa += Convert.ToDouble(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "análisis":
                                            vA += Convert.ToDouble(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "promoción":
                                            vP += Convert.ToDouble(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "negociación":
                                            vN += Convert.ToDouble(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "cerrados":
                                            vC += Convert.ToDouble(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                    }
                                }
                                //foreach (DataRow dr in dsReporte.Tables[0].Rows)
                                //{
                                //    if (dr["VTeorico"] != System.DBNull.Value)
                                //    {
                                //        dr["VTeorico"] = Convert.ToDouble(dr["VTeorico"]).ToString("C");
                                //    }
                                //    dr["MontoProyecto"] = Convert.ToDouble(dr["MontoProyecto"]).ToString("C");

                                //}

                                ///''''''''''''''''''''''''''
                                //Agregando fila para totales
                                int entero = dsReporte.Tables[0].Rows.Count;
                                DataRow fila = null;
                                fila = dsReporte.Tables[0].NewRow();
                                dsReporte.Tables[0].Rows.InsertAt(fila, entero + 1);
                                ///''''''''''''''''''''''''''
                                for (int i = 0; i <= dsReporte.Tables[0].Rows.Count - 1; i++)
                                {
                                    if (i == 0)
                                    {
                                        sw.WriteLine("<html>");
                                        sw.WriteLine("<head>");
                                        sw.WriteLine("</head>");
                                        sw.WriteLine("<body>");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<strong>MODULO CRM - CONTROL DE LA PROMOCION NIVEL SOLUCIÓN</strong>");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        if (this.session.Id_TU == 2)
                                            sw.WriteLine("Representante: " + this.session.U_Nombre);
                                        else
                                        {
                                            sw.WriteLine("Representante: "
                                                + ((ddlRepresentantesComercial== "-1" || ddlRepresentantesComercial== string.Empty) ? "Todos" : ddlRepresentantesComercial));
                                        }
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("Fecha: " + DateTime.Now.Date.ToShortDateString());
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("Periodo: " + ddPeriodo.Replace("--", "").Trim());
                                        sw.WriteLine("<br />");
                                        if (session.Id_TU == 2 || session.Id_TU == 4 || session.Id_TU == 5)//representante// if (this.session.Id_TU == 5 | this.session.Id_TU == 6)
                                            sw.WriteLine("CDS: "
                                                + ((ddlZonas == "-1" || ddlZonas == string.Empty) ? "Todos" : ddlZonas));
                                        else
                                            sw.WriteLine("CDS:" + session.Cd_Nombre);
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<table border=1><font size=8pt>");
                                        sw.WriteLine("<tr>");
                                        for (int k = 0; k <= dsReporte.Tables[0].Columns.Count - 2; k++)
                                        {//titulos del reporte
                                            sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>");
                                            sw.WriteLine(dsReporte.Tables[0].Columns[k].Caption);
                                            sw.WriteLine("</td>");
                                        }
                                        sw.WriteLine("</tr>");
                                    }
                                    sw.WriteLine("<tr>");
                                    intCancela = 0;
                                    for (int j = 0; j <= dsReporte.Tables[0].Columns.Count - 2; j++)
                                    {

                                        if (dsReporte.Tables[0].Rows[i]["Estatus"].ToString().ToLower() == "cerrados")
                                        {
                                            sw.WriteLine("<td>");
                                            intCancela = 0;
                                        }
                                        else if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i]["Analisis"], System.DBNull.Value)))
                                        {

                                            if (Math.Abs(((TimeSpan)DateTime.Now.Subtract(Convert.ToDateTime(dsReporte.Tables[0].Rows[i]["Analisis"]))).Days) > 60)
                                            {
                                                sw.WriteLine("<td> <font color=\"#FF0000\">");
                                                intCancela = 1;
                                            }
                                            else if (Math.Abs(((TimeSpan)DateTime.Now.Subtract(Convert.ToDateTime(dsReporte.Tables[0].Rows[i]["Analisis"]))).Days) > 90)
                                            {
                                                sw.WriteLine("<td> <font color=\"#FF0000\">");
                                                intCancela = 1;
                                            }
                                            else
                                            {
                                                sw.WriteLine("<td>");
                                                intCancela = 0;
                                            }
                                        }
                                        else
                                        {
                                            sw.WriteLine("<td>");
                                            intCancela = 0;
                                        }

                                        sw.WriteLine(dsReporte.Tables[0].Rows[i][j]);
                                        if (j == 6)
                                        {
                                            if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i][j], System.DBNull.Value)))
                                                totTeorico += Convert.ToDouble(dsReporte.Tables[0].Rows[i][j].ToString().Replace("$", "").Replace(",", "").Trim());
                                            else
                                                if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                    sw.WriteLine("<strong>" + totTeorico.ToString("C") + "</strong>");
                                        }
                                        else if (j == 7)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vA.ToString("C") + "</strong>");
                                        }
                                        else if (j == 8)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vP.ToString("C") + "</strong>");
                                        }
                                        else if (j == 9)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vN.ToString("C") + "</strong>");
                                        }
                                        else if (j == 10)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vC.ToString("C") + "</strong>");
                                        }
                                        else if (j == 11)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vCa.ToString("C") + "</strong>");
                                        }
                                        else if (j == 12)
                                        {
                                            if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i][j], System.DBNull.Value)))
                                                totVenta += Convert.ToDouble(dsReporte.Tables[0].Rows[i][j].ToString().Replace("$", "").Replace(",", "").Trim());
                                            else
                                            {
                                                if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                    sw.WriteLine("<strong>" + totVenta.ToString("C") + "</strong>");
                                            }
                                        }
                                        if (intCancela == 1)
                                            sw.WriteLine("</font></td>");
                                        else
                                            sw.WriteLine("</td>");
                                    }
                                    sw.WriteLine("</tr>");
                                }
                                sw.WriteLine("</font></table>");
                                sw.WriteLine("</body>");
                                sw.WriteLine("</html>");
                                sw.Close();

                                if (File.Exists(ruta))
                                {
                                    string ruta2 = null;
                                    ruta2 = Server.MapPath("Reportes") + "\\" + NombreArchivo;
                                    File.Move(ruta, Server.MapPath("Reportes") + "\\" + NombreArchivo);
                                    Response.Redirect("Reportes\\" + NombreArchivo);
                                }
                            }
                            catch (Exception Ex)
                            {
                                str = Ex.Message;
                                sw.Close();
                            }
                        }
                        else
                            Alerta("No se encontraron proyectos de promoción");
                    }
                    else
                        Alerta("No se cargó la información");
                }
            }

        }

        protected void ibtnExcelAplicacion_Click(object sender, ImageClickEventArgs e)
        {
            if (!_PermisoImprimir)
            {
                Alerta("No tiene permiso para imprimir");
                return;
            }
            //if (!radControl.Checked && !radDII.Checked && !radControlAplicacion.Checked && !radDIINumero.Checked && !radControlEntrada.Checked && !radCampania.Checked && !radCierreMes.Checked)
            //{
            //    Alerta(string.Concat("Seleccione una opción de control"));
            //    return;
            //}
            string ddPeriodo = "";

            string periodo = !string.IsNullOrEmpty(ddPeriodo) ? Convert.ToString(ddPeriodo) : "-1*-1";

            if (periodo != "-1*-1")
            {
                ExportarControlPromocion_LimpiezaAplicacion();
            }
        }

        public void ExportarControlPromocion_LimpiezaAplicacion()
        {
            string ruta = null;
            string str = "";
            string tipo = "";
            //tipo = (this.chkProyectoNuevo.Checked) ? "AplNuevo" : "Apl";

            int intCancela = 0;
            int vintConsulta = 0;
            double vMonto1 = 0;
            double vMonto2 = 0;
            decimal totTeorico = default(decimal);
            decimal totVenta = default(decimal);
            string vRepID = null;
            int vZonaID = 0;

            string ddlRepresentantesComercial = "";

            string ddlZonas = "";

            string ddPeriodo = "";

            string txtDe = "";
            string txtA = "";

            if (this.session.Id_Cd_Ver != 0)
                vZonaID = this.session.Id_Cd_Ver;
            else
                vZonaID = Convert.ToInt32(ddlZonas);

            if (ddlRepresentantesComercial== string.Empty || Convert.ToInt32(ddlRepresentantesComercial) == -1)
                vRepID = null;
            else
                vRepID = ddlRepresentantesComercial;

            int periodo = !string.IsNullOrEmpty(ddPeriodo) ? Convert.ToInt32(ddPeriodo) : -1;

            if (!string.IsNullOrEmpty(txtDe) && !string.IsNullOrEmpty(txtA))
            {
                vintConsulta = 1;
                vMonto1 = Convert.ToDouble(txtDe);
                vMonto2 = Convert.ToDouble(txtA);
            }
            else
            {
                vintConsulta = 0;
                vMonto1 = 0;
                vMonto2 = 0;
            }

            ruta = Server.MapPath("Reportes") + "\\Reporte" + this.session.Id_U.ToString() + tipo + ".txt";
            if ((string.IsNullOrEmpty(txtA) && !string.IsNullOrEmpty(txtDe)) || (!string.IsNullOrEmpty(txtA) && string.IsNullOrEmpty(txtDe)))
                this.DisplayMensajeAlerta("El rango introducido es incorrecto, falta un valor");
            else
            {
                if (vMonto1 > vMonto2)
                    this.DisplayMensajeAlerta("El rango inicial no debe ser mayor al rango final");
                else
                {

                    String NombreArchivo = "Reporte" + this.session.Id_U.ToString() + tipo + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".xls";
                    if (File.Exists(ruta))
                        File.Delete(ruta);
                    if (File.Exists(Server.MapPath("Reportes") + "\\" + NombreArchivo))
                        File.Delete(Server.MapPath("Reportes") + "\\" + NombreArchivo);
                    System.IO.StreamWriter sw = null;
                    DataSet dsReporte = new DataSet();

                    new CN_CrmInformes().GenerarControlPromocion_LimpiezaAplicacion(
                        this.session.Id_Emp
                        , Convert.ToInt32(ddlZonas) == -1 ? session.Id_Cd_Ver : Convert.ToInt32(ddlZonas)
                        , session.Id_U
                        , ddlRepresentantesComercial
                        , periodo
                        , vintConsulta
                        , txtDe
                        , txtA,
                        false //this.chkProyectoNuevo.Checked
                        , ref dsReporte
                        , this.session.Emp_Cnx
                    );

                    if ((dsReporte != null))
                    {
                        if (!(dsReporte.Tables[0].Rows.Count == 0))
                        {
                            try
                            {
                                sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
                                //Obteniendo totales por etapa
                                decimal vA = default(decimal);
                                decimal vP = default(decimal);
                                decimal vN = default(decimal);
                                decimal vC = default(decimal);
                                decimal vCa = default(decimal);
                                for (int m = 0; m <= dsReporte.Tables[0].Rows.Count - 1; m++)
                                {
                                    DataRow dr = null;
                                    dr = dsReporte.Tables[0].Rows[m];
                                    switch (dr["Estatus"].ToString().ToLower())
                                    {
                                        case "cancelados":
                                            vCa += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "análisis":
                                            vA += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "promoción":
                                            vP += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "negociación":
                                            vN += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "cerrados":
                                            vC += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                    }
                                }
                                //Agregando fila para totales
                                int entero = dsReporte.Tables[0].Rows.Count;
                                DataRow fila = null;
                                fila = dsReporte.Tables[0].NewRow();
                                dsReporte.Tables[0].Rows.InsertAt(fila, entero + 1);
                                ///''''''''''''''''''''''''''
                                for (int i = 0; i <= dsReporte.Tables[0].Rows.Count - 1; i++)
                                {
                                    if (i == 0)
                                    {
                                        sw.WriteLine("<html>");
                                        sw.WriteLine("<head>");
                                        sw.WriteLine("</head>");
                                        sw.WriteLine("<body>");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<strong>MODULO CRM - CONTROL DE LA PROMOCION NIVEL APLICACIÓN</strong>");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        if (this.session.Id_TU == 2)
                                            sw.WriteLine("Representante: " + this.session.U_Nombre);
                                        else
                                        {
                                            sw.WriteLine("Representante: "
                                                + ((ddlRepresentantesComercial== "-1" || ddlRepresentantesComercial== string.Empty) ? "Todos" : ddlRepresentantesComercial));
                                        }
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("Fecha: " + DateTime.Now.Date.ToShortDateString());
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("Periodo: " + ddPeriodo.Replace("--", "").Trim());
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("CDS: "
                                            + ((ddlZonas == "-1" || ddlZonas == string.Empty) ? "Todos" : ddlZonas));

                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<table border=1><font size=8pt>");
                                        sw.WriteLine("<tr>");
                                        for (int k = 0; k <= dsReporte.Tables[0].Columns.Count - 2; k++)
                                        {
                                            sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>");
                                            sw.WriteLine(dsReporte.Tables[0].Columns[k].Caption);
                                            sw.WriteLine("</td>");
                                        }
                                        sw.WriteLine("</tr>");
                                    }
                                    sw.WriteLine("<tr>");
                                    intCancela = 0;
                                    for (int j = 0; j <= dsReporte.Tables[0].Columns.Count - 2; j++)
                                    {
                                        if (dsReporte.Tables[0].Rows[i]["Estatus"].ToString().ToLower() == "cerrados")
                                        {
                                            sw.WriteLine("<td>");
                                            intCancela = 0;
                                        }
                                        else if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i]["Analisis"], System.DBNull.Value)))
                                        {

                                            if (Math.Abs(((TimeSpan)DateTime.Now.Subtract(Convert.ToDateTime(dsReporte.Tables[0].Rows[i]["Analisis"]))).Days) > 60)
                                            {
                                                sw.WriteLine("<td> <font color=\"#FF0000\">");
                                                intCancela = 1;
                                            }
                                            else if (Math.Abs(((TimeSpan)DateTime.Now.Subtract(Convert.ToDateTime(dsReporte.Tables[0].Rows[i]["Analisis"]))).Days) > 90)
                                            {
                                                sw.WriteLine("<td> <font color=\"#FF0000\">");
                                                intCancela = 1;
                                            }
                                            else
                                            {
                                                sw.WriteLine("<td>");
                                                intCancela = 0;
                                            }
                                        }
                                        else
                                        {
                                            sw.WriteLine("<td>");
                                            intCancela = 0;
                                        }
                                        
                                        sw.WriteLine(dsReporte.Tables[0].Rows[i][j]);

                                        if (j == 6)
                                        {
                                            if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i][j], System.DBNull.Value)))
                                                totTeorico += Convert.ToDecimal(dsReporte.Tables[0].Rows[i][j].ToString().Replace("$", "").Replace(",", "").Trim());
                                            else
                                            {
                                                if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                    sw.WriteLine("<strong>" + totTeorico.ToString("C") + "</strong>");
                                            }
                                        }
                                        else if (j == 7)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vA.ToString("C") + "</strong>");
                                        }
                                        else if (j == 8)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vP.ToString("C") + "</strong>");
                                        }
                                        else if (j == 9)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vN.ToString("C") + "</strong>");
                                        }
                                        else if (j == 10)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vC.ToString("C") + "</strong>");
                                        }
                                        else if (j == 11)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vCa.ToString("C") + "</strong>");
                                        }
                                        else if (j == 12)
                                        {
                                            if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i][j], System.DBNull.Value)))
                                                totVenta += Convert.ToDecimal(dsReporte.Tables[0].Rows[i][j].ToString().Replace("$", "").Replace(",", "").Trim());
                                            else
                                            {
                                                if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                    sw.WriteLine("<strong>" + totVenta.ToString("C") + "</strong>");
                                            }
                                        }
                                        if (intCancela == 1)
                                            sw.WriteLine("</font></td>");
                                        else
                                            sw.WriteLine("</td>");
                                    }
                                    sw.WriteLine("</tr>");
                                }
                                sw.WriteLine("</font></table>");
                                sw.WriteLine("</body>");
                                sw.WriteLine("</html>");
                                sw.Close();

                                if (File.Exists(ruta))
                                {
                                    string ruta2 = null;
                                    ruta2 = Server.MapPath("Reportes") + "\\" + NombreArchivo;
                                    File.Move(ruta, Server.MapPath("Reportes") + "\\" + NombreArchivo);
                                    Response.Redirect("Reportes\\" + NombreArchivo);
                                }
                            }
                            catch (Exception Ex)
                            {
                                str = Ex.Message;
                                sw.Close();
                            }
                        }
                        else
                            Alerta("No se encontraron proyectos de promoción");
                    }
                    else
                        Alerta("No se cargó la información");
                }
            }
        }

        protected void ibtnExcelControlEntrada_Click(object sender, ImageClickEventArgs e)
        {
            if (!_PermisoImprimir)
            {
                Alerta("No tiene permiso para imprimir");
                return;
            }

            string ddPeriodo = "";

            //if (!radControl.Checked && !radDII.Checked && !radControlAplicacion.Checked && !radDIINumero.Checked && !radControlEntrada.Checked && !radCampania.Checked && !radCierreMes.Checked)
            //{
            //    Alerta(string.Concat("Seleccione una opción de control"));
            //    return;
            //}
            string periodo = !string.IsNullOrEmpty(ddPeriodo) ? Convert.ToString(ddPeriodo) : "-1*-1";

            if (periodo != "-1*-1")
            {
                this.ExportarControlEntrada();
            }
        }

        private void ExportarControlEntrada()
        {

            string ddlZonas = "";

            Session["dtRepresentantes"] = null;
            DataSet dsCE = new DataSet();
            string tipo = "";
            tipo = "ControlEntrada";
            System.IO.StreamWriter sw = null;
            string ruta = null;

            string ddlRepresentantesComercial = "";

            string ddPeriodo = "";

            ruta = Server.MapPath("Reportes") + "\\Reporte" + this.session.Id_U.ToString() + tipo + ".txt";

            String NombreArchivo = "Reporte" + this.session.Id_U.ToString() + tipo + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".xls";

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\" + NombreArchivo))
                File.Delete(Server.MapPath("Reportes") + "\\" + NombreArchivo);
            sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            string RepID = null;
            string ZonaID = null;
            string Representante = null;
            if (ddlRepresentantesComercial== string.Empty || Convert.ToInt32(ddlRepresentantesComercial) == -1)
            {
                RepID = "%";
                Representante = "Todos";
            }
            else
            {
                RepID = ddlRepresentantesComercial;
                Representante = ddlRepresentantesComercial;
            }
            if (Convert.ToInt32(ddlZonas) == -1)
                ZonaID = "%";
            else
                ZonaID = ddlZonas;

            int periodo = !string.IsNullOrEmpty(ddPeriodo) ? Convert.ToInt32(ddPeriodo) : -1;

            new CN_CrmInformes().spCRM_ControlEntrada(this.session.Id_Emp, Convert.ToInt32(ddlZonas) == -1 ? session.Id_Cd_Ver : Convert.ToInt32(ddlZonas), periodo, RepID, ref dsCE, this.session.Emp_Cnx);

            if ((dsCE != null))
            {
                if (!(dsCE.Tables[0].Rows.Count == 0))
                {
                    DataRow dr = null;
                    DataTable dt = new DataTable();
                    for (int i = 0; i <= dsCE.Tables[0].Rows.Count - 1; i++)
                    {
                        dr = dsCE.Tables[0].Rows[i];
                        if ((Session["dtRepresentantes"] != null))
                            dt = (DataTable)Session["dtRepresentantes"];
                        else
                        {
                            this.CrearColumnasControlEntrada();
                        }

                        this.CrearFilasControlEntrada(dr["Zona"].ToString(), dr["UsuarioID"].ToString(), dr["Representante"].ToString(), dr["Fecha"].ToString());


                    }


                    ///'''''''''ENCABEZADOS DEL REPORTE'''''''''''''''''
                    sw.WriteLine("<html>");
                    sw.WriteLine("<head>");
                    sw.WriteLine("</head>");
                    sw.WriteLine("<body>");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<strong>MODULO CRM - CONTROL DE ENTRADAS </strong>");
                    sw.WriteLine("<br />");
                    sw.WriteLine("Representante: " + Representante + "");
                    sw.WriteLine("<br />");
                    sw.WriteLine("Fecha: " + DateTime.Now.ToShortDateString() + "");
                    sw.WriteLine("<br />");

                    sw.WriteLine("<br />");
                    sw.WriteLine("<table border=1><font size=8pt>");
                    sw.WriteLine("<tr><td bgcolor=\"#DDDDDD\"><b>CDS</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>No. Rik</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Representante</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Fecha de Entrada</td></tr>");
                    ///'''''''''TERMINAN ENCABEZADOS DEL REPORTE'''''''''''''''''
                    if ((Session["dtRepresentantes"] != null))
                    {
                        dt = (DataTable)Session["dtRepresentantes"];
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            //Formando texto de la tabla
                            sw.WriteLine("<tr>");
                            sw.WriteLine("<td>" + dt.Rows[i]["CDS"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["UsuarioID"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Representante"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["FechaEntrada"] + "</td>");
                            sw.WriteLine("</tr>");
                        }
                        sw.WriteLine("<tr>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("</tr>");
                        sw.WriteLine("</font></table>");
                        sw.WriteLine("</body>");
                        sw.WriteLine("</html>");
                        sw.Close();
                        if (File.Exists(ruta))
                        {
                            string ruta2 = null;
                            ruta2 = Server.MapPath("Reportes") + "\\" + NombreArchivo;
                            File.Move(ruta, Server.MapPath("Reportes") + "\\" + NombreArchivo);
                            Response.Redirect("Reportes\\" + NombreArchivo, false);
                        }
                    }

                }
                else
                {
                    Alerta("No se encontraron Entradas");
                }
            }
        }


        private void CrearColumnasControlEntrada()
        {
            if (Session["dtRepresentantes"] == null)
            {
                DataTable dtRepresentantes = new DataTable();
                DataColumn dcCDS = new DataColumn("CDS", Type.GetType("System.String"));
                DataColumn dcUsuarioID = new DataColumn("UsuarioID", Type.GetType("System.String"));
                DataColumn dcRepresentante = new DataColumn("Representante", Type.GetType("System.String"));
                DataColumn dcFechaEntrada = new DataColumn("FechaEntrada", Type.GetType("System.String"));


                dtRepresentantes.Columns.Add(dcCDS);
                dtRepresentantes.Columns.Add(dcUsuarioID);
                dtRepresentantes.Columns.Add(dcRepresentante);
                dtRepresentantes.Columns.Add(dcFechaEntrada);
                Session["dtRepresentantes"] = dtRepresentantes;
            }
        }


        public void CrearFilasControlEntrada(string CDS, string UsuarioID, string Representante, string Fecha)
        {
            DataTable dtRepresentantes = new DataTable();
            dtRepresentantes = (DataTable)Session["dtRepresentantes"];
            DataRow drFila = null;
            drFila = dtRepresentantes.NewRow();
            drFila["CDS"] = CDS;
            drFila["UsuarioID"] = UsuarioID;
            drFila["Representante"] = Representante;
            drFila["FechaEntrada"] = Fecha;

            dtRepresentantes.Rows.Add(drFila);
            dtRepresentantes.AcceptChanges();
            Session["dtRepresentantes"] = dtRepresentantes;
        }

        protected void ibtnExcelNumero_Click(object sender, ImageClickEventArgs e)
        {
            if (!_PermisoImprimir)
            {
                Alerta("No tiene permiso para imprimir");
                return;
            }

            string ddPeriodo = "";

            //if (!radControl.Checked && !radDII.Checked && !radControlAplicacion.Checked && !radDIINumero.Checked && !radControlEntrada.Checked && !radCampania.Checked && !radCierreMes.Checked)
            //{
            //    Alerta(string.Concat("Seleccione una opción de control"));
            //    return;
            //}
            string periodo = !string.IsNullOrEmpty(ddPeriodo) ? Convert.ToString(ddPeriodo) : "-1*-1";

            if (periodo != "-1*-1")
            {
                this.ExportarDII();
            }
        }

        protected void IbtnExcelCierreMes_Click(object sender, ImageClickEventArgs e)
        {
            if (!_PermisoImprimir)
            {
                Alerta("No tiene permiso para imprimir");
                return;
            }

            string ddPeriodo = ""; 

            //if (!radControl.Checked && !radDII.Checked && !radControlAplicacion.Checked && !radDIINumero.Checked && !radControlEntrada.Checked && !radCampania.Checked && !radCierreMes.Checked)
            //{
            //    Alerta(string.Concat("Seleccione una opción de control"));
            //    return;
            //}
            string periodo = !string.IsNullOrEmpty(ddPeriodo) ? Convert.ToString(ddPeriodo) : "-1*-1";

            if (periodo != "-1*-1")
            {
                this.ExportarCierreMes();
            }
        }

        public void ExportarCierreMes()
        {
            string ruta = null;
            string str = "";
            string tipo = "";
            tipo = "Cierre";
            int intCancela = 0;
            int vintConsulta = 0;
            double vMonto1 = 0;
            double vMonto2 = 0;
            decimal totTeorico = default(decimal);
            decimal totVenta = default(decimal);
            string vRepID = null;

            string ddlZonas = "";
            string ddlRepresentantesComercial = "";
            string ddPeriodo = "";
            string txtDe = "";
            string txtA = "";

            int vZonaID = 0;
            if (this.session.Id_Cd_Ver != 0)
                vZonaID = this.session.Id_Cd_Ver;
            else
                vZonaID = Convert.ToInt32(ddlZonas);

            if (ddlRepresentantesComercial== string.Empty || Convert.ToInt32(ddlRepresentantesComercial) == -1)
                vRepID = null;
            else
                vRepID = ddlRepresentantesComercial;

            int periodo = !string.IsNullOrEmpty(ddPeriodo) ? Convert.ToInt32(ddPeriodo) : -1;

            if (!string.IsNullOrEmpty(txtDe) && !string.IsNullOrEmpty(txtA))
            {
                vintConsulta = 1;
                vMonto1 = Convert.ToDouble(txtDe);
                vMonto2 = Convert.ToDouble(txtA);
            }
            else
            {
                vintConsulta = 0;
                vMonto1 = 0;
                vMonto2 = 0;
            }

            ruta = Server.MapPath("Reportes") + "\\Reporte" + this.session.Id_U.ToString() + tipo + ".txt";
            if ((string.IsNullOrEmpty(txtA) && !string.IsNullOrEmpty(txtDe)) || (!string.IsNullOrEmpty(txtA) && string.IsNullOrEmpty(txtDe)))
                this.DisplayMensajeAlerta("El rango introducido es incorrecto, falta un valor");
            else
            {
                if (vMonto1 > vMonto2)
                    this.DisplayMensajeAlerta("El rango inicial no debe ser mayor al rango final");
                else
                {
                    String NombreArchivo = "Reporte" + this.session.Id_U.ToString() + tipo + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".xls";

                    if (File.Exists(ruta))
                        File.Delete(ruta);
                    if (File.Exists(Server.MapPath("Reportes") + "\\" + NombreArchivo))
                        File.Delete(Server.MapPath("Reportes") + "\\" + NombreArchivo);
                    System.IO.StreamWriter sw = null;
                    DataSet dsReporte = new DataSet();

                    new CN_CrmInformes().GenerarCierreMes(
                        this.session.Id_Emp
                        , Convert.ToInt32(ddlZonas) == -1 ? session.Id_Cd_Ver : Convert.ToInt32(ddlZonas)
                        , session.Id_U
                        , ddlRepresentantesComercial
                        , periodo
                        , vintConsulta
                        , txtDe
                        , txtA,
                        false // this.chkProyectoNuevo.Checked
                        , ref dsReporte
                        , this.session.Emp_Cnx
                    );

                    if ((dsReporte != null))
                    {
                        if (!(dsReporte.Tables[0].Rows.Count == 0))
                        {
                            try
                            {
                                sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
                                //Obteniendo totales por etapa
                                decimal vA = default(decimal);
                                decimal vP = default(decimal);
                                decimal vN = default(decimal);
                                decimal vC = default(decimal);
                                decimal vCa = default(decimal);
                                for (int m = 0; m <= dsReporte.Tables[0].Rows.Count - 1; m++)
                                {
                                    DataRow dr = null;
                                    dr = dsReporte.Tables[0].Rows[m];
                                    switch (dr["Estatus"].ToString().ToLower())
                                    {
                                        case "cancelados":
                                            vCa += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "análisis":
                                            vA += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "promoción":
                                            vP += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "negociación":
                                            vN += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                        case "cerrados":
                                            vC += Convert.ToDecimal(dr["MontoProyecto"].ToString().Replace("$", "").Replace(",", "").Trim());
                                            break;
                                    }
                                }
                                //Agregando fila para totales
                                int entero = dsReporte.Tables[0].Rows.Count;
                                DataRow fila = null;
                                fila = dsReporte.Tables[0].NewRow();
                                dsReporte.Tables[0].Rows.InsertAt(fila, entero + 1);
                                ///''''''''''''''''''''''''''
                                for (int i = 0; i <= dsReporte.Tables[0].Rows.Count - 1; i++)
                                {
                                    if (i == 0)
                                    {
                                        sw.WriteLine("<html>");
                                        sw.WriteLine("<head>");
                                        sw.WriteLine("</head>");
                                        sw.WriteLine("<body>");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<strong>MODULO CRM - CIERRE MES</strong>");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        if (this.session.Id_TU == 2)
                                            sw.WriteLine("Representante: " + this.session.U_Nombre);
                                        else
                                        {
                                            sw.WriteLine("Representante: "
                                                + ((ddlRepresentantesComercial== "-1" || ddlRepresentantesComercial== string.Empty) ? "Todos" : ddlRepresentantesComercial));
                                        }
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("Fecha: " + DateTime.Now.Date.ToShortDateString());
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("Periodo: " + ddPeriodo.Replace("--", "").Trim());
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("CDS: "
                                            + ((ddlZonas == "-1" || ddlZonas == string.Empty) ? "Todos" : ddlZonas));

                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<br />");
                                        sw.WriteLine("<table border=1><font size=8pt>");
                                        sw.WriteLine("<tr>");
                                        for (int k = 0; k <= dsReporte.Tables[0].Columns.Count - 1; k++)
                                        {
                                            switch (k)
                                            {
                                                case 0:
                                                case 1:
                                                case 2:
                                                    sw.WriteLine("<td bgcolor=\"#FF0000\"><b>");
                                                    break;
                                                case 3:
                                                case 4:
                                                    sw.WriteLine("<td bgcolor=\"#B0E0E6\"><b>");
                                                    break;
                                                case 5:
                                                case 6:
                                                case 7:
                                                case 8:
                                                case 9:
                                                case 10:
                                                case 11:
                                                case 12:
                                                    sw.WriteLine("<td bgcolor=\"#191970\"><b>");
                                                    break;
                                                default:
                                                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>");
                                                    break;
                                            }

                                            sw.WriteLine(dsReporte.Tables[0].Columns[k].Caption);
                                            sw.WriteLine("</td>");
                                        }
                                        sw.WriteLine("</tr>");
                                    }
                                    sw.WriteLine("<tr>");
                                    intCancela = 0;
                                    for (int j = 0; j <= dsReporte.Tables[0].Columns.Count - 1; j++)
                                    {
                                        if (dsReporte.Tables[0].Rows[i]["Estatus"].ToString().ToLower() == "cerrados")
                                        {
                                            sw.WriteLine("<td>");
                                            intCancela = 0;
                                        }
                                        else if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i]["Analisis"], System.DBNull.Value)))
                                        {

                                            if (Math.Abs(((TimeSpan)DateTime.Now.Subtract(Convert.ToDateTime(dsReporte.Tables[0].Rows[i]["Analisis"]))).Days) > 60)
                                            {
                                                sw.WriteLine("<td> <font color=\"#FF0000\">");
                                                intCancela = 1;
                                            }
                                            else if (Math.Abs(((TimeSpan)DateTime.Now.Subtract(Convert.ToDateTime(dsReporte.Tables[0].Rows[i]["Analisis"]))).Days) > 90)
                                            {
                                                sw.WriteLine("<td> <font color=\"#FF0000\">");
                                                intCancela = 1;
                                            }
                                            else
                                            {
                                                sw.WriteLine("<td>");
                                                intCancela = 0;
                                            }
                                        }
                                        else
                                        {
                                            sw.WriteLine("<td>");
                                            intCancela = 0;
                                        }


                                        sw.WriteLine(dsReporte.Tables[0].Rows[i][j]);

                                        if (j == 8)
                                        {
                                            if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i][j], System.DBNull.Value)))
                                                totTeorico += Convert.ToDecimal(dsReporte.Tables[0].Rows[i][j].ToString().Replace("$", "").Replace(",", "").Trim());
                                            else
                                            {
                                                if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                    sw.WriteLine("<strong>" + totTeorico.ToString("C") + "</strong>");
                                            }
                                        }

                                        else if (j == 9)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vA.ToString("C") + "</strong>");
                                        }
                                        else if (j == 10)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vP.ToString("C") + "</strong>");
                                        }

                                        else if (j == 11)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vN.ToString("C") + "</strong>");
                                        }
                                        else if (j == 12)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vC.ToString("C") + "</strong>");
                                        }
                                        else if (j == 13)
                                        {
                                            if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                sw.WriteLine("<strong>" + vCa.ToString("C") + "</strong>");
                                        }

                                        /* else if (j == 15)
                                         {
                                             if ((!object.ReferenceEquals(dsReporte.Tables[0].Rows[i][j], System.DBNull.Value)))
                                                 totVenta += Convert.ToDecimal(dsReporte.Tables[0].Rows[i][j].ToString().Replace("$", "").Replace(",", "").Trim());
                                             else
                                             {
                                                 if (i == dsReporte.Tables[0].Rows.Count - 1)
                                                     sw.WriteLine("<strong>" + totVenta.ToString("C") + "</strong>");
                                             }
                                         }*/
                                        if (intCancela == 1)
                                            sw.WriteLine("</font></td>");
                                        else
                                            sw.WriteLine("</td>");
                                    }
                                    sw.WriteLine("</tr>");
                                }
                                sw.WriteLine("</font></table>");
                                sw.WriteLine("</body>");
                                sw.WriteLine("</html>");
                                sw.Close();

                                if (File.Exists(ruta))
                                {
                                    string ruta2 = null;
                                    ruta2 = Server.MapPath("Reportes") + "\\" + NombreArchivo;
                                    File.Move(ruta, Server.MapPath("Reportes") + "\\" + NombreArchivo);
                                    Response.Redirect("Reportes\\" + NombreArchivo);
                                }
                            }
                            catch (Exception Ex)
                            {
                                str = Ex.Message;
                                sw.Close();
                            }
                        }
                        else
                            Alerta("No se encontraron proyectos de promoción");
                    }
                    else
                        Alerta("No se cargó la información");
                }
            }
        }


        private void ExportarDII()
        {
            Session["dtRepresentantes"] = null;
            DataSet dsDII = new DataSet();
            string tipo = "";
            tipo = "num";
            System.IO.StreamWriter sw = null;
            string ruta = null;
            decimal totalA = 0;
            decimal totalP = 0;
            decimal totalN = 0;
            decimal totalMonto = 0;
            decimal totalC = 0;
            decimal totalX = 0;

            string ddlZonas = "";
            string ddlRepresentantesComercial = "";
            string ddPeriodo = "";

            ruta = Server.MapPath("Reportes") + "\\Reporte" + this.session.Id_U.ToString() + tipo + ".txt";
            String NombreArchivo = "Reporte" + this.session.Id_U.ToString() + tipo + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".xls";

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\" + NombreArchivo))
                File.Delete(Server.MapPath("Reportes") + "\\" + NombreArchivo);
            sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            string RepID = null;
            string ZonaID = null;
            if (ddlRepresentantesComercial== string.Empty || Convert.ToInt32(ddlRepresentantesComercial) == -1)
                RepID = "%";
            else
                RepID = ddlRepresentantesComercial;
            if (Convert.ToInt32(ddlZonas) == -1)
                ZonaID = "%";
            else
                ZonaID = ddlZonas;

            int periodo = !string.IsNullOrEmpty(ddPeriodo) ? Convert.ToInt32(ddPeriodo) : -1;

            new CN_CrmInformes().spCRM_ControlPromocion_DII(this.session.Id_Emp, Convert.ToInt32(ddlZonas) == -1 ? session.Id_Cd_Ver : Convert.ToInt32(ddlZonas), periodo, RepID, ref dsDII, this.session.Emp_Cnx);

            if ((dsDII != null))
            {
                if (!(dsDII.Tables[0].Rows.Count == 0))
                {
                    DataRow dr = null;
                    DataTable dt = new DataTable();
                    for (int i = 0; i <= dsDII.Tables[0].Rows.Count - 1; i++)
                    {
                        dr = dsDII.Tables[0].Rows[i];
                        if ((Session["dtRepresentantes"] != null))
                        {
                            dt = (DataTable)Session["dtRepresentantes"];
                        }
                        else
                        {
                            this.CrearColumnas();
                        }

                        this.CrearFilas(dr["ZonaID"].ToString(), dr["UsuarioID"].ToString(), dr["Representante"].ToString(), Convert.ToDouble(dr["A"]).ToString(), Convert.ToDouble(dr["P"]).ToString(), Convert.ToDouble(dr["N"]).ToString(), Convert.ToDouble(dr["total"]).ToString(), Convert.ToDouble(dr["C"]).ToString(), Convert.ToDouble(dr["X"]).ToString(), "0");
                    }
                    ///'''''''''ENCABEZADOS DEL REPORTE'''''''''''''''''
                    sw.WriteLine("<html>");
                    sw.WriteLine("<head>");
                    sw.WriteLine("</head>");
                    sw.WriteLine("<body>");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<strong>MODULO CRM - CONTROL DE LA PROMOCIÓN - Vista DII</strong>");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<br />");
                    sw.WriteLine("<table border=1><font size=8pt>");
                    sw.WriteLine("<tr><td bgcolor=\"#DDDDDD\"><b>CDS</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>No. Rik</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Representante</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Análisis</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Presentación</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Negociación</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Proyectos</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Cierre</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Cancelación</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Efectividad Cierre</td></tr>");
                    ///'''''''''TERMINAN ENCABEZADOS DEL REPORTE'''''''''''''''''
                    if ((Session["dtRepresentantes"] != null))
                    {
                        dt = (DataTable)Session["dtRepresentantes"];
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {

                            totalA += Convert.ToDecimal(dt.Rows[i]["A"]);
                            totalP += Convert.ToDecimal(dt.Rows[i]["P"]);
                            totalN += Convert.ToDecimal(dt.Rows[i]["N"]);
                            totalMonto += Convert.ToDecimal(dt.Rows[i]["monto"]);
                            totalC += Convert.ToDecimal(dt.Rows[i]["C"]);
                            totalX += Convert.ToDecimal(dt.Rows[i]["X"]);

                            if (Convert.ToDouble(dt.Rows[i]["C"]) > 0)
                            {
                                dt.Rows[i]["Efectividad"] = Convert.ToDecimal((Convert.ToDecimal(dt.Rows[i]["C"]) / (Convert.ToDecimal(dt.Rows[i]["C"]) + Convert.ToDecimal(dt.Rows[i]["X"])))) * 100;
                                dt.Rows[i]["Efectividad"] = dt.Rows[i]["Efectividad"] + "%";
                            }
                            //Formando texto de la tabla
                            sw.WriteLine("<tr>");
                            sw.WriteLine("<td>" + dt.Rows[i]["CDS"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["UsuarioID"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Representante"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["A"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["P"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["N"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Monto"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["C"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["X"] + "</td>");
                            sw.WriteLine("<td>" + dt.Rows[i]["Efectividad"] + "</td>");
                            sw.WriteLine("</tr>");
                        }
                        sw.WriteLine("<tr>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalA + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalP + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalN + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalMonto + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalC + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\">" + totalX + "</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"></td>");
                        sw.WriteLine("</tr>");
                        sw.WriteLine("</font></table>");
                        sw.WriteLine("</body>");
                        sw.WriteLine("</html>");
                        sw.Close();
                        if (File.Exists(ruta))
                        {
                            string ruta2 = null;
                            ruta2 = Server.MapPath("Reportes") + "\\" + NombreArchivo;
                            File.Move(ruta, Server.MapPath("Reportes") + "\\" + NombreArchivo);
                            Response.Redirect("Reportes\\" + NombreArchivo, false);
                        }
                    }
                    //Calculando la Efectividad
                }
                else
                {
                    Alerta("No se encontraron proyectos de promoción");
                }
            }
        }


        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { try { return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } catch (Exception ex) { return false; } } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public Sesion session
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
        #endregion

        private void Alerta(string mensaje)
        {
            try
            {
                //RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje.Replace("'", "\"") + "', 330, 150);");
                //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "bloque1", "radalert('" + mensaje.Replace("'", "\"") + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                //this.radCierreMes.Style.Add("display", "none");//
                //this.radControl.Style.Add("display", "none");
                if (session.Id_TU == 2)
                {
                    //this.radDII.Style.Add("display", "none");//
                    //this.radDIINumero.Style.Add("display", "none");//                   
                    //this.radCampania.Style.Add("display", "none");
                }
                this.CargarZonas();
                this.CargarRepresentantes();
                this.CargarPeriodos();
                //ibtnExcelSolucion.Visible = true;
            }
            catch (Exception)
            {

            }
        }

        private void CargarPeriodos()
        {//Cargar periodos
            string ddPeriodo = "";
            try
            {
                //new CN__Comun().LlenaCombo(this.session.Id_Emp, this.session.Id_Cd_Ver, this.session.Emp_Cnx, "spCatCalendario_Consulta_MesAnhio", ref ddPeriodo);
                //ddPeriodo.SelectedIndex = ddPeriodo.Items.Count - 1;
                //RadComboBoxItem rcbi = new RadComboBoxItem();
                //rcbi.Text = "-- Todos --";
                //rcbi.Value = "-1";
                //ddPeriodo.Items.Insert(0, rcbi);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}