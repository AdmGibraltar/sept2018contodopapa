﻿namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Data;

    /// <summary>
    /// Summary description for Rep_excesosInventarios2.
    /// </summary>
    public partial class ExpRep_CumpVentaInstalada5c: Telerik.Reporting.Report
    {
        public ExpRep_CumpVentaInstalada5c()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //
            
            //
            this.DataSource = null;
        }

        private void Rep_CumpVentaInstalada5c_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection2.ConnectionString = this.ReportParameters["Emp_Cnx"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command             
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Semana"].Value = this.ReportParameters["Semana"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@RIK"].Value = this.ReportParameters["Rik"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ter"].Value = this.ReportParameters["Territorio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Prd"].Value = this.ReportParameters["Producto"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Tipo"].Value = "4";//this.ReportParameters["Nivel"].Value;

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void subReport1_NeedDataSource(object sender, EventArgs e)
        //{
        //    Telerik.Reporting.Processing.ReportItemBase reporteBase = (Telerik.Reporting.Processing.ReportItemBase)sender;
        //    this.subCumpVentaC51.Id_Emp = this.ReportParameters["Id_Emp"].Value.ToString();
        //    this.subCumpVentaC51.Id_Cd = this.ReportParameters["Id_Cd"].Value.ToString();
        //    this.subCumpVentaC51.Semana = this.ReportParameters["Semana"].Value.ToString();
        //    this.subCumpVentaC51.Id_Cte = ((DataRow)reporteBase.DataObject.RawData).ItemArray[4].ToString();
        //    this.subCumpVentaC51.Id_Ter = ((DataRow)reporteBase.DataObject.RawData).ItemArray[0].ToString();
        //    //this.subCumpVentaC51.Id_Prd = null;// ((DataRow)reporteBase.DataObject.RawData).ItemArray[6].ToString(); //this.ReportParameters["Sproducto"].Value.ToString();

        //    this.subCumpVentaC51.Conexion = this.ReportParameters["Emp_Cnx"].Value.ToString();
        //}

        //private void subReport1_ItemDataBound(object sender, EventArgs e)
        //{
        //    Telerik.Reporting.Processing.SubReport subReport = (Telerik.Reporting.Processing.SubReport)sender;
        //    Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)subReport.InnerReport;
        //    subReport.Visible = report.ChildElements.Find("detail", true).Length > 0;
        //    Telerik.Reporting.DetailSection detail = new DetailSection();
        //    detail.Visible = subReport.Visible;      
        //}
    }
}