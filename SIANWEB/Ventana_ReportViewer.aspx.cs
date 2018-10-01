using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using System.Collections;
using Telerik.Reporting;

namespace SIANWEB
{
    public partial class Ventana_ReportViewer : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Dictionary<int, ReportInstances> rptInstances = new Dictionary<int, ReportInstances>();
                
                if (Sesion == null)
                {
                 
                }
                else
                {
                    if (Page.IsPostBack == false)
                    {                       
                        string reportName;
                        ArrayList ALValorParametrosInternos;
                        Session["Head" + Session.SessionID] = "Reporte";
                        string cve_pagina = "";

                        if (Page.Request.QueryString["cve"] != null)
                        {
                            cve_pagina = Page.Request.QueryString["cve"].ToString();
                            reportName = Session["assembly" + Session.SessionID + cve_pagina].ToString();
                            rptInstances = (Dictionary<int, ReportInstances>)Session["assemblies" + Session.SessionID];
                            ALValorParametrosInternos = (ArrayList)Session["InternParameter_Values" + Session.SessionID + cve_pagina];
                        }
                        else
                        {
                            reportName = Session["assembly" + Session.SessionID].ToString();
                            rptInstances = (Dictionary<int, ReportInstances>)Session["assemblies" + Session.SessionID];
                            ALValorParametrosInternos = (ArrayList)Session["InternParameter_Values" + Session.SessionID];
                        }
                                                
                        if (!string.IsNullOrEmpty(reportName))
                        {
                            Func<string, Report> getReport = (s) =>
                            {
                                Type reportType = Type.GetType(s);
                                return (Report)Activator.CreateInstance(reportType);
                            };

                            Report report = getReport(reportName);
                                                        
                            if (rptInstances != null && rptInstances.Any())
                            {
                                ReportBook rb = new ReportBook();

                                foreach (KeyValuePair<int, ReportInstances> item in rptInstances)
                                {
                                    Report rpt = getReport(item.Value.ReportInstance);
                                    if (item.Value.Parameters != null)
                                    {
                                        for (int i = 0; i <= item.Value.Parameters.Count - 1; i++)
                                        {
                                            rpt.ReportParameters[i].AllowNull = true;
                                            rpt.ReportParameters[i].Value = item.Value.Parameters[i];

                                        }
                                    }
                                    rb.Reports.Add(rpt);
                                }                              

                                this.ReportViewer1.ReportSource = rb;
                                Session["assemblies" + Session.SessionID] = new Dictionary<int, ReportInstances>();
                            }
                            else
                            {
                                for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
                                {
                                    report.ReportParameters[i].AllowNull = true;
                                    report.ReportParameters[i].Value = ALValorParametrosInternos[i];
                                }

                                this.ReportViewer1.ReportSource = report;
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void Salir()
        {
            try
            {
                string funcion = null;
                funcion = "CloseWindow()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}