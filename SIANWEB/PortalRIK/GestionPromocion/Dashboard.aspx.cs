using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaDatos;
using Telerik.Web.UI;
using System.Data;
using CapaNegocios;
using System.Text;

namespace SIANWEB.PortalRIK
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SIANWEB.MasterPage.PortalRIK mp = Master as SIANWEB.MasterPage.PortalRIK;
            mp.CurrentPath = new List<string>() { "Gestion de la Promoción", "Dashboard" }.ToArray();

            if (!IsPostBack)
            {
                Session["activeMenu"] = 2;
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

        protected void ddlCDS_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CN__Comun comun = new CN__Comun();
                Sesion sesion2 = (Sesion)Session["Sesion" + Session.SessionID];
                comun.CambiarCdVer(ddlCDS.SelectedItem, ref sesion2);
                Session["Sesion" + Session.SessionID] = sesion2;
                CargarMetas();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ddl_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CN__Comun comun = new CN__Comun();
                Sesion sesion2 = (Sesion)Session["Sesion" + Session.SessionID];
                CargarMetas();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #region Funciones
        private void Inicializar()
        {
            try
            {
                switch (session.Id_TU)
                {
                    case 3:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        CargarCentros();
                        lblcd.Visible = true;
                        ddlCDS.Visible = true;
                        pnlGeneral.Visible = false;
                        pnlComercial.Visible = true;
                        break;
                    default:
                        lblcd.Visible = false;
                        ddlCDS.Visible = false;
                        pnlGeneral.Visible = true;
                        pnlComercial.Visible = true;
                        break;
                }
                if (session.Id_TU != 3)
                {
                    lnkMetas.Visible = false;
                }
                if (_PermisoImprimir)
                    ibtnImprimir.OnClientClick = String.Format("printpage()", ibtnImprimir.UniqueID, "");
                MesesHistorial();
                CargarMetas();
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
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref ddlCDS);
                this.ddlCDS.SelectedValue = Sesion.Id_Cd_Ver.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarMetas()
        {
            try
            {
                Meta m = new Meta();
                m.Id_Emp = session.Id_Emp;
                m.Id_Cd = session.Id_Cd_Ver;
                CN_CatMeta meta = new CN_CatMeta();
                meta.Consultar(session.Id_Rik, ref m, session.Emp_Cnx);

                NumProyectos.Value = m.Met_Proyectos;
                MontoProyectos.Value = m.Met_MontoProyecto;
                AvanceMes.Value = m.Met_Avances;
                CantidadCerrados.Value = m.Met_CantCerrado;
                MontoCerrados.Value = m.Met_MontCerrado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GeneraGraficaDistribucion()
        {
            try
            {
                if (session != null)
                {
                    if (this.ddlMeses.SelectedIndex == -1 || this.ddlMeses.SelectedIndex == 0)
                    {
                        //'Función que crea el XML con los datos de la cantidad de proyectos que se encuentran
                        //'en cada una de las etapas de la venta, mismos que serán interpretados por el componente
                        //'para proceder a dibujar la gráfica.
                        int intEtapa = 0;
                        int intEtapaAnterior = 0;
                        bool bolEtapa = false;
                        int intDdl = !string.IsNullOrEmpty(ddl.SelectedValue) ? Convert.ToInt32(ddl.SelectedValue) : 1;
                        StringBuilder xmlData = new StringBuilder();
                        DataSet dsGraficaDistribucion = new DataSet();
                        int rolId = session.Id_TU;
                        CN_CRMGraficas graficas = new CN_CRMGraficas();
                        switch (rolId)
                        {
                            case 3:
                            case 5:
                            case 6:
                                graficas.GraficaDistribucion(session.Id_Emp, session.Id_Cd_Ver, "1,2,3,4,5", (int?)null, (int?)null, (int?)null, intDdl, ref dsGraficaDistribucion, session.Emp_Cnx);
                                //ModuloCRM_CN.clsGraficasCN.LeerDatosGraficaDistribucion_Gerente("%", Session("EmpresaID"), Me.ddlCDS.SelectedValue, "%", _
                                //    "%", "%", "%", "%", "%", -1, "%")
                                break;
                            case 7:
                                graficas.GraficaDistribucion(session.Id_Emp, session.Id_Cd_Ver, "1,2,3,4,5", (int?)null, session.Id_U, (int?)null, intDdl, ref dsGraficaDistribucion, session.Emp_Cnx);
                                //        dsGraficaDistribucion = ModuloCRM_CN.clsGraficasCN.LeerDatosGraficaDistribucion_GteSegmento("%", _
                                //        Session("EmpresaID"), Me.ddlCDS.SelectedValue, "%", "%", "%", "%", "%", "%", _
                                //        -1, "%", Session("UsuarioID"))
                                break;
                            case 8:
                                graficas.GraficaDistribucion(session.Id_Emp, session.Id_Cd_Ver, "1,2,3,4,5", (int?)null, (int?)null, session.Id_U, intDdl, ref dsGraficaDistribucion, session.Emp_Cnx);
                                //        dsGraficaDistribucion = ModuloCRM_CN.clsGraficasCN.LeerDatosGraficaDistribucion_GteUEN("%", _
                                //        Session("EmpresaID"), Me.ddlCDS.SelectedValue, "%", "%", "%", "%", "%", "%", _
                                //        -1, "%", Session("UsuarioID"))
                                break;
                            default:
                                graficas.GraficaDistribucion(session.Id_Emp, session.Id_Cd_Ver, "1,2,3,4,5", session.Id_Rik, (int?)null, (int?)null, intDdl, ref dsGraficaDistribucion, session.Emp_Cnx);
                                //        dsGraficaDistribucion = 
                                //ModuloCRM_CN.clsGraficasCN.LeerDatosGraficaDistribucion(Session("UsuarioID"), Session("EmpresaID"), Session("ZonaID"), 2)
                                break;
                        }

                        if (dsGraficaDistribucion != null)
                        {
                            if (intDdl == 1)
                            {
                                xmlData.Append("<chart caption='DISTRIBUCIÓN' xAxisName='Etapa' yAxisName='$' showValues='1' formatNumberScale='0' showBorder='0' numberPrefix='$' showSum='1' decimals='4'>");
                                if (dsGraficaDistribucion.Tables[0].Rows.Count != 0)
                                {
                                    //Creando el codigo xml para mostrar los datos en la grafica
                                    intEtapaAnterior = 1;
                                    for (int i = 0; i <= dsGraficaDistribucion.Tables[0].Rows.Count - 1; i++)
                                    {
                                        DataRow dr = dsGraficaDistribucion.Tables[0].Rows[i];
                                        if (intEtapaAnterior == (int)dr["Estatus"])
                                        {
                                            xmlData.Append("<set label='" + dr["Etapa"] + "(" + dr["Cantidad"] + ")' value='" + dr["Monto"] + "'/>");
                                            intEtapaAnterior = (int)dr["Estatus"];
                                            intEtapaAnterior += 1;
                                        }
                                        else
                                        {
                                            intEtapa = (int)dr["Estatus"];
                                            bolEtapa = false;
                                            while (bolEtapa)
                                            {
                                                xmlData.Append(this.AgregaFilaGraficaDistribucion(intEtapaAnterior));
                                                intEtapaAnterior += 1;
                                                if (intEtapaAnterior == intEtapa)
                                                    bolEtapa = true;
                                            }
                                            xmlData.Append("<set label='" + dr["Etapa"] + "(" + dr["Cantidad"] + ")' value='" + dr["Monto"] + "'/>");
                                            intEtapaAnterior += 1;
                                        }
                                        //if (i != dsGraficaDistribucion.Tables[0].Rows.Count - 1)
                                        //{
                                        intEtapa = (int)dr["Estatus"];
                                        //}
                                    }
                                    if (intEtapa != 4)
                                    {
                                        switch (intEtapa)
                                        {
                                            case 1:
                                                xmlData.Append("<set label='Promoción(0)' value='0'/>");
                                                xmlData.Append("<set label='Negociación(0)' value='0'/>");
                                                xmlData.Append("<set label='Cierre(0)' value='0'/>");
                                                break;
                                            case 2:
                                                xmlData.Append("<set label='Negociación(0)' value='0'/>");
                                                xmlData.Append("<set label='Cierre(0)' value='0'/>");
                                                break;
                                            case 3:
                                                xmlData.Append("<set label='Cierre(0)' value='0'/>");
                                                break;
                                        }
                                    }
                                    xmlData.Append("</chart>");
                                }
                                else
                                {
                                    xmlData.Append("<set label='Análisis(0)' value='0'/>");
                                    xmlData.Append("<set label='Promoción(0)' value='0'/>");
                                    xmlData.Append("<set label='Negociación(0)' value='0'/>");
                                    xmlData.Append("<set label='Cierre(0)' value='0'/>");
                                    xmlData.Append("</chart>");
                                }
                            }
                            else
                            {//intDdl = 2
                                xmlData.Append("<chart caption='DISTRIBUCIÓN' xAxisName='Etapa' yAxisName='Proyectos' showValues='1' formatNumberScale='0' showBorder='0' showSum='1' decimals='2'>");
                                if (dsGraficaDistribucion.Tables[0].Rows.Count != 0)
                                {
                                    //Creando el codigo xml para mostrar los datos en la grafica
                                    intEtapaAnterior = 1;
                                    for (int i = 0; i <= dsGraficaDistribucion.Tables[0].Rows.Count - 1; i++)
                                    {
                                        DataRow dr = dsGraficaDistribucion.Tables[0].Rows[i];
                                        if (intEtapaAnterior == (int)dr["Estatus"])
                                        {
                                            xmlData.Append("<set label='" + dr["Etapa"] + "(" + Convert.ToDouble(dr["Cantidad"]).ToString("C") + ")' value='" + dr["Monto"] + "'/>");
                                            intEtapaAnterior = (int)dr["Estatus"];
                                            intEtapaAnterior += 1;
                                        }
                                        else
                                        {
                                            intEtapa = (int)dr["Estatus"];
                                            bolEtapa = false;
                                            while (bolEtapa)
                                            {
                                                xmlData.Append(this.AgregaFilaGraficaDistribucion(intEtapaAnterior));
                                                intEtapaAnterior += 1;
                                                if (intEtapaAnterior == intEtapa)
                                                    bolEtapa = true;
                                            }
                                            xmlData.Append("<set label='" + dr["Etapa"] + "(" + Convert.ToDouble(dr["Cantidad"]).ToString("C") + ")' value='" + dr["Monto"] + "'/>");
                                            intEtapaAnterior += 1;
                                        }
                                        intEtapa = (int)dr["Estatus"];
                                    }
                                    if (intEtapa != 4)
                                    {
                                        switch (intEtapa)
                                        {
                                            case 1:
                                                xmlData.Append("<set label='Promoción($0.00)' value='0'/>");
                                                xmlData.Append("<set label='Negociación($0.00)' value='0'/>");
                                                xmlData.Append("<set label='Cierre($0.00)' value='0'/>");
                                                break;
                                            case 2:
                                                xmlData.Append("<set label='Negociación($0.00)' value='0'/>");
                                                xmlData.Append("<set label='Cierre($0.00)' value='0'/>");
                                                break;
                                            case 3:
                                                xmlData.Append("<set label='Cierre($0.00)' value='0'/>");
                                                break;
                                        }
                                    }
                                    xmlData.Append("</chart>");
                                }
                                else
                                {
                                    xmlData.Append("<set label='Análisis($0.00)' value='0'/>");
                                    xmlData.Append("<set label='Promoción($0.00)' value='0'/>");
                                    xmlData.Append("<set label='Negociación($0.00)' value='0'/>");
                                    xmlData.Append("<set label='Cierre($0.00)' value='0'/>");
                                    xmlData.Append("</chart>");
                                }
                            }
                        }
                        //Return the chart HTML - Column 3D Chart with data from xmlData variable using dataXML method
                        return InfoSoftGlobal.FusionCharts.RenderChartHTML(Page.ResolveUrl("http://localhost:63048/FusionCharts/Column3D.swf"), "", xmlData.ToString(), "myNext", "500", "300", false);
                    }
                    else
                    {
                        DataSet dsDatos = new DataSet();
                        DataSet dsMes = new DataSet();
                        double vMontoA = 0;
                        double vMontoP = 0;
                        double vMontoN = 0;
                        double vMontoC = 0;
                        double vMontoCancela = 0;
                        int vCantA = 0;
                        int vCantP = 0;
                        int vCantN = 0;
                        int vCantC = 0;
                        int vCantCancela = 0;
                        DataRow dr = default(DataRow);
                        int Mes = 0;
                        int Año = 0;

                        switch (ddlMeses.SelectedItem.Text.Substring(0, this.ddlMeses.SelectedItem.Text.Length - 5).ToLower())
                        {
                            case "enero":
                                Mes = 1;
                                break;
                            case "febrero":
                                Mes = 2;
                                break;
                            case "marzo":
                                Mes = 3;
                                break;
                            case "abril":
                                Mes = 4;
                                break;
                            case "mayo":
                                Mes = 5;
                                break;
                            case "junio":
                                Mes = 6;
                                break;
                            case "julio":
                                Mes = 7;
                                break;
                            case "agosto":
                                Mes = 8;
                                break;
                            case "septiembre":
                                Mes = 9;
                                break;
                            case "octubre":
                                Mes = 10;
                                break;
                            case "noviembre":
                                Mes = 11;
                                break;
                            case "diciembre":
                                Mes = 12;
                                break;
                        }
                        Año = Convert.ToInt32(this.ddlMeses.SelectedItem.Text.Substring(this.ddlMeses.SelectedItem.Text.Length - 4, 4));
                        // dsMes = ModuloCRM_CN.clsFuncionesGeneralesCN.LeerPeriodoMes(Año, Mes)
                        int totalDiasMes = DateTime.DaysInMonth(Año, Mes);
                        //    dsDatos = ModuloCRM_CN.clsOportunidadesCN.LeerOportunidadesRepresentante(Session("UsuarioID"), Session("EmpresaID"), _
                        //    Session("ZonaID"), "%", "%", "%")
                        if (dsDatos != null)
                        {
                            if (dsDatos.Tables[4].Rows.Count != 0)
                            {
                                for (int i = 0; i <= dsDatos.Tables[4].Rows.Count - 1; i++)
                                {
                                    dr = dsDatos.Tables[4].Rows[i];
                                    //if ((int)dr["OportunidadID"] == 38) // COMENTARIZADO POR QUE NO SE UTILIZA
                                    //{
                                    //    int x = 1; 
                                    //}
                                    if (Convert.ToDateTime(dr["Analisis"]) <= Convert.ToDateTime(dsMes.Tables[0].Rows[0]["FechaFin"]))
                                    {
                                        switch (Convert.ToInt32(dr["Estatus"]))
                                        {
                                            case 1:
                                                vCantA += 1;
                                                vMontoA += Convert.ToDouble(dr["MontoProyecto"]);
                                                break;
                                            case 2:
                                                if (Convert.ToDateTime(dr["Presentacion"]) <= Convert.ToDateTime(dsMes.Tables[0].Rows[0]["FechaFin"]))
                                                {
                                                    vCantP += 1;
                                                    vMontoP += Convert.ToDouble(dr["MontoProyecto"]);
                                                }
                                                else
                                                {
                                                    vCantA += 1;
                                                    vMontoA += Convert.ToDouble(dr["MontoProyecto"]);
                                                }
                                                break;
                                            case 3:
                                                if (Convert.ToDateTime(dr["Negociacion"]) <= Convert.ToDateTime(dsMes.Tables[0].Rows[0]["FechaFin"]))
                                                {
                                                    vCantN += 1;
                                                    vMontoN += Convert.ToDouble(dr["MontoProyecto"]);
                                                }
                                                else
                                                {
                                                    if ((Convert.ToDateTime(dr["Presentacion"]) <= Convert.ToDateTime(dsMes.Tables[0].Rows[0]["FechaFin"])))
                                                    {
                                                        vCantP += 1;
                                                        vMontoP += Convert.ToDouble(dr["MontoProyecto"]);
                                                    }
                                                    else
                                                    {
                                                        vCantA += 1;
                                                        vMontoA += Convert.ToDouble(dr["MontoProyecto"]);
                                                    }
                                                }
                                                break;
                                            case 4:
                                                if (Convert.ToDateTime(dr["Cierre"]) <= Convert.ToDateTime(dsMes.Tables[0].Rows[0]["FechaFin"]))
                                                {
                                                    vCantC += 1;
                                                    vMontoC += Convert.ToDouble(dr["MontoProyecto"]);
                                                }
                                                else
                                                {
                                                    if (Convert.ToDateTime(dr["Negociacion"]) <= Convert.ToDateTime(dsMes.Tables[0].Rows[0]["FechaFin"]))
                                                    {
                                                        vCantN += 1;
                                                        vMontoN += Convert.ToDouble(dr["MontoProyecto"]);
                                                    }
                                                    else
                                                    {
                                                        if (Convert.ToDateTime(dr["Presentacion"]) <= Convert.ToDateTime(dsMes.Tables[0].Rows[0]["FechaFin"]))
                                                        {
                                                            vCantP += 1;
                                                            vMontoP += Convert.ToDouble(dr["MontoProyecto"]);
                                                        }
                                                        else
                                                        {
                                                            vCantA += 1;
                                                            vMontoA += Convert.ToDouble(dr["MontoProyecto"]);
                                                        }
                                                    }
                                                }
                                                break;
                                            case 0:
                                                vMontoCancela += Convert.ToDouble(dr["MontoProyecto"]);
                                                vCantCancela += 1;
                                                //'''''''''''''''
                                                DateTime fecha;
                                                fecha = Convert.ToDateTime(dsMes.Tables[0].Rows[0]["FechaFin"]);
                                                if (dr["Cierre"] != null)
                                                {
                                                    if (Convert.ToDateTime(dr["Cierre"]) <= Convert.ToDateTime(dr["FechaCancelacion"]))
                                                    {
                                                        vCantC += 1;
                                                        vMontoC += Convert.ToDouble(dr["MontoProyecto"]);
                                                    }
                                                }
                                                else if (dr["Negociacion"] != null)
                                                {
                                                    if (Convert.ToDateTime(dr["Negociacion"]) <= Convert.ToDateTime(dr["FechaCancelacion"]))
                                                    {
                                                        vCantC += 1;
                                                        vMontoC += Convert.ToDouble(dr["MontoProyecto"]);
                                                    }
                                                }
                                                else if (dr["Presentacion"] != null)
                                                {
                                                    if (Convert.ToDateTime(dr["Presentacion"]) <= Convert.ToDateTime(dr["FechaCancelacion"]))
                                                    {
                                                        vCantC += 1;
                                                        vMontoC += Convert.ToDouble(dr["MontoProyecto"]);
                                                    }
                                                }
                                                else if (dr["Analisis"] != null)
                                                {
                                                    if (Convert.ToDateTime(dr["Analisis"]) <= Convert.ToDateTime(dr["FechaCancelacion"]))
                                                    {
                                                        vCantC += 1;
                                                        vMontoC += Convert.ToDouble(dr["MontoProyecto"]);
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                        StringBuilder xmlData = new StringBuilder();
                        xmlData.Append("<chart caption='DISTRIBUCION' xAxisName='Etapa' yAxisName='$' showValues='1' formatNumberScale='0' showBorder='0' numberPrefix='$' showSum='1' decimals='4'>");
                        xmlData.Append("<set label='Análisis" + "(" + vCantA + ")' value='" + vMontoA + "'/>");
                        xmlData.Append("<set label='Promoción" + "(" + vCantP + ")' value='" + vMontoP + "'/>");
                        xmlData.Append("<set label='Negociación" + "(" + vCantN + ")' value='" + vMontoN + "'/>");
                        xmlData.Append("<set label='Cierre" + "(" + vCantC + ")' value='" + vMontoC + "'/>");
                        xmlData.Append("</chart>");
                        return InfoSoftGlobal.FusionCharts.RenderChartHTML(Page.ResolveUrl("~/FusionCharts/Column3D.swf"), "", xmlData.ToString(), "myNext", "500", "300", false);
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GeneraGraficaActividad()
        {
            try
            {
                if (session != null)
                {
                    //Función que crea el XML con los datos de los proyectos cancelados, proyectos activos
                    //y proyectos inactivos (que no tienen registrado seguimiento en el último mes) del representante, 
                    //mismos que serán interpretados por el componente para proceder a dibujar la gráfica.

                    DataSet dsGraficaActividad = new DataSet();
                    int rolid = session.Id_TU;

                    CN_CRMGraficas cn_actividad = new CN_CRMGraficas();
                    int intDdl = !string.IsNullOrEmpty(ddl.SelectedValue) ? Convert.ToInt32(ddl.SelectedValue) : 1;

                    switch (rolid)
                    {
                        case 3:
                        case 5:
                        case 6:
                            cn_actividad.GraficaActividad(ref dsGraficaActividad, ddlCDS.SelectedValue, session.Id_Emp, null, null, null, session.Emp_Cnx, intDdl);
                            //("%", Session("EmpresaID"), Me.ddlCDS.SelectedValue, "%", "%", "%", "%", "%", "%", -1, "%");
                            break;
                        case 7:
                            //("%", Session("EmpresaID"), Me.ddlCDS.SelectedValue, "%", "%", "%", "%", "%", "%", -1, "%", Session("UsuarioID"));
                            cn_actividad.GraficaActividad(ref dsGraficaActividad, ddlCDS.SelectedValue, session.Id_Emp, null, session.Id_U, null, session.Emp_Cnx, intDdl);
                            break;
                        case 8:
                            //("%", Session("EmpresaID"), Me.ddlCDS.SelectedValue, "%", "%", "%", "%", "%", "%",-1, "%", Session("UsuarioID"));
                            cn_actividad.GraficaActividad(ref dsGraficaActividad, ddlCDS.SelectedValue, session.Id_Emp, null, null, session.Id_U, session.Emp_Cnx, intDdl);
                            break;
                        default:
                            //(Session("UsuarioID"), Session("EmpresaID"), Session("ZonaID"), 2)
                            cn_actividad.GraficaActividad(ref dsGraficaActividad, session.Id_Cd_Ver.ToString(), session.Id_Emp, session.Id_Rik, null, null, session.Emp_Cnx, intDdl);
                            break;
                    }

                    StringBuilder xmlData = new StringBuilder();
                    if (dsGraficaActividad != null)
                    {
                        if (intDdl == 1)
                        {
                            xmlData.Append("<chart palette='1' caption='ACTIVIDAD' shownames='1' showvalues='0' numberPrefix='$' showSum='1' decimals='4' overlapColumns='0' formatNumberScale='0'>");
                            xmlData.Append("<categories>");
                            xmlData.Append("<category label='Proyectos de promoción' />");
                            xmlData.Append("</categories>");

                            if (dsGraficaActividad.Tables[0].Rows.Count != 0) //Proyectos Cancelados
                            {
                                DataRow dr;
                                dr = dsGraficaActividad.Tables[0].Rows[0];
                                xmlData.Append("<dataset seriesName='Proy. cancelados(" + dr["Cantidad"] + ")' showValues='1' color='ff0000'>");
                                xmlData.Append("<set value='-" + dr["Monto"] + "' color='ff0000'/>");
                                xmlData.Append("</dataset>");
                            }
                            else
                            {
                                xmlData.Append("<dataset seriesName='Proy. cancelados(0)' showValues='1' color='ff0000'>");
                                xmlData.Append("<set value='0' color='ff0000'/>");
                                xmlData.Append("</dataset>");
                            }
                            if (dsGraficaActividad.Tables[1].Rows.Count != 0) //Proyectos Inactivos
                            {
                                DataRow dr;
                                dr = dsGraficaActividad.Tables[1].Rows[0];
                                xmlData.Append("<dataset seriesName='Proy. inactivos(" + dr["Cantidad"].ToString() + ")' showValues='1' color='CC6600'>");
                                xmlData.Append("<set value='" + dr["Monto"].ToString() + "' color='CC6600'/>");
                                xmlData.Append("</dataset>");
                            }
                            else
                            {
                                xmlData.Append("<dataset seriesName='Proy. inactivos(0)' showValues='1' color='ffff33'>");
                                xmlData.Append("<set value='0' color='CC6600'/>");
                                xmlData.Append("</dataset>");
                            }
                            if (dsGraficaActividad.Tables[2].Rows.Count != 0) //Proyectos Activos
                            {
                                DataRow dr;
                                dr = dsGraficaActividad.Tables[2].Rows[0];
                                xmlData.Append("<dataset seriesName='Proy. activos(" + dr["Cantidad"] + ")' showValues='1' color='66CC33'>");
                                xmlData.Append("<set value='" + dr["Monto"].ToString() + "' color='66CC33'/>");
                                xmlData.Append("</dataset>");
                            }
                            else
                            {
                                xmlData.Append("<dataset seriesName='Proy. activos(0)' showValues='1' color='66CC33'>");
                                xmlData.Append("<set value='0' color='ff00cc'/>");
                                xmlData.Append("</dataset>");
                            }
                        }
                        else
                        {//intDdl = 2
                            xmlData.Append("<chart palette='1' caption='ACTIVIDAD' shownames='1' showvalues='0' showSum='1' decimals='2' overlapColumns='0' formatNumberScale='0'>");
                            xmlData.Append("<categories>");
                            xmlData.Append("<category label='Proyectos de promoción' />");
                            xmlData.Append("</categories>");

                            if (dsGraficaActividad.Tables[0].Rows.Count != 0) //Proyectos Cancelados
                            {
                                DataRow dr;
                                dr = dsGraficaActividad.Tables[0].Rows[0];
                                xmlData.Append("<dataset seriesName='Proy. cancelados(" + Convert.ToDouble(dr["Cantidad"]).ToString("C") + ")' showValues='1' color='ff0000'>");
                                xmlData.Append("<set value='-" + dr["Monto"] + "' color='ff0000'/>");
                                xmlData.Append("</dataset>");
                            }
                            else
                            {
                                xmlData.Append("<dataset seriesName='Proy. cancelados($0.00)' showValues='1' color='ff0000'>");
                                xmlData.Append("<set value='0' color='ff0000'/>");
                                xmlData.Append("</dataset>");
                            }
                            if (dsGraficaActividad.Tables[1].Rows.Count != 0) //Proyectos Inactivos
                            {
                                DataRow dr;
                                dr = dsGraficaActividad.Tables[1].Rows[0];
                                xmlData.Append("<dataset seriesName='Proy. inactivos(" + Convert.ToDouble(dr["Cantidad"]).ToString("C") + ")' showValues='1' color='CC6600'>");
                                xmlData.Append("<set value='" + dr["Monto"].ToString() + "' color='CC6600'/>");
                                xmlData.Append("</dataset>");
                            }
                            else
                            {
                                xmlData.Append("<dataset seriesName='Proy. inactivos($0.00)' showValues='1' color='ffff33'>");
                                xmlData.Append("<set value='0' color='CC6600'/>");
                                xmlData.Append("</dataset>");
                            }
                            if (dsGraficaActividad.Tables[2].Rows.Count != 0) //Proyectos Activos
                            {
                                DataRow dr;
                                dr = dsGraficaActividad.Tables[2].Rows[0];
                                xmlData.Append("<dataset seriesName='Proy. activos(" + Convert.ToDouble(dr["Cantidad"]).ToString("C") + ")' showValues='1' color='66CC33'>");
                                xmlData.Append("<set value='" + dr["Monto"].ToString() + "' color='66CC33'/>");
                                xmlData.Append("</dataset>");
                            }
                            else
                            {
                                xmlData.Append("<dataset seriesName='Proy. activos($0.00)' showValues='1' color='66CC33'>");
                                xmlData.Append("<set value='0' color='ff00cc'/>");
                                xmlData.Append("</dataset>");
                            }
                        }
                        xmlData.Append("</chart>");
                        return InfoSoftGlobal.FusionCharts.RenderChartHTML(Page.ResolveUrl("~/FusionCharts/StackedColumn3D.swf"), "", xmlData.ToString(), "myNext", "300", "300", false);
                    }
                    else
                        return "";
                }
                else
                    return "";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private string AgregaFilaGraficaDistribucion(int intEtapaAnterior)
        {
            try
            {
                switch (intEtapaAnterior)
                {
                    case 1:
                        return "<set label='Análisis(0)' value='0'/>";
                    case 2:
                        return "<set label='Promoción(0)' value='0'/>";
                    case 3:
                        return "<set label='Negociación(0)' value='0'/>";
                    case 4:
                        return "<set label='Cierre(0)' value='0'/>";
                    default:
                        return "";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void MesesHistorial()
        {
            try
            {
                //Función que tiene por objetivo mostrar los últimos 12 meses móviles en el combo ddlMeses, para que una vez seleccionado
                //un mes en particular, la gráfica muestre los datos referentes al mes seleccionado.
                Funciones funcion = new Funciones();
                DateTime FechaActual = funcion.GetLocalDateTime(session.Minutos).AddMonths(1);
                DateTime FechaHistorial;
                for (int i = 0; i <= 11; i++)
                {
                    FechaHistorial = FechaActual.AddMonths(-1);
                    switch (FechaHistorial.Month)
                    {
                        case 1:
                            this.ddlMeses.Items.Add(new RadComboBoxItem("Enero" + " " + FechaHistorial.Year));
                            break;
                        case 2:
                            this.ddlMeses.Items.Add(new RadComboBoxItem("Febrero" + " " + FechaHistorial.Year));
                            break;
                        case 3:
                            this.ddlMeses.Items.Add(new RadComboBoxItem("Marzo" + " " + FechaHistorial.Year));
                            break;
                        case 4:
                            this.ddlMeses.Items.Add(new RadComboBoxItem("Abril" + " " + FechaHistorial.Year));
                            break;
                        case 5:
                            this.ddlMeses.Items.Add(new RadComboBoxItem("Mayo" + " " + FechaHistorial.Year));
                            break;
                        case 6:
                            this.ddlMeses.Items.Add(new RadComboBoxItem("Junio" + " " + FechaHistorial.Year));
                            break;
                        case 7:
                            this.ddlMeses.Items.Add(new RadComboBoxItem("Julio" + " " + FechaHistorial.Year));
                            break;
                        case 8:
                            this.ddlMeses.Items.Add(new RadComboBoxItem("Agosto" + " " + FechaHistorial.Year));
                            break;
                        case 9:
                            this.ddlMeses.Items.Add(new RadComboBoxItem("Septiembre" + " " + FechaHistorial.Year));
                            break;
                        case 10:
                            this.ddlMeses.Items.Add(new RadComboBoxItem("Octubre" + " " + FechaHistorial.Year));
                            break;
                        case 11:
                            this.ddlMeses.Items.Add(new RadComboBoxItem("Noviembre" + " " + FechaHistorial.Year));
                            break;
                        case 12:
                            this.ddlMeses.Items.Add(new RadComboBoxItem("Diciembre" + " " + FechaHistorial.Year));
                            break;
                        default: break;
                    }
                    //Fecha de inicio de producción del sistema
                    FechaActual = FechaHistorial;
                    if (FechaActual.Month == 1 && FechaActual.Year == 2009)
                    {
                        break;
                    }
                }
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
                CapaNegocio.PaginaConsultar(ref pagina, session.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = session.Id_U;
                Permiso.Id_Cd = session.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, session.Emp_Cnx);
                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (_PermisoImprimir)
                        this.divImprimir.Visible = true;
                    else
                        this.divImprimir.Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");
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
                //RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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
                //this.lblMensaje.Text = "";
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
                //this.lblMensaje.Text = Message;
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
        #endregion

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
    }
}