namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using CapaNegocios;
    using CapaEntidad;
    using System.Data;

    /// <summary>
    /// Summary description for Rep_CobRelacionCobranzaFto1.
    /// </summary>
    public partial class ExpRep_CobRelacionCobranzaFto1 : Telerik.Reporting.Report
    {
        public ExpRep_CobRelacionCobranzaFto1()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void ExRep_CobRelacionCobranzaFto1_NeedDataSource(object sender, EventArgs e)
        {
            try
            {               
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
               // this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp_Param"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Rel"].Value = this.ReportParameters["Id_Rel"].Value;

               /* this.sqlDataAdapter2.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter2.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter2.SelectCommand.Parameters["@Id_Pag"].Value = this.ReportParameters["Id_Rel"].Value;*/

                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void subReport1_NeedDataSource(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.ReportItemBase reporteBase = (Telerik.Reporting.Processing.ReportItemBase)sender;
            subRelacionCobranza1.SetParameters(this.ReportParameters["Id_Emp_Param"].Value.ToString(), this.ReportParameters["Id_Cd"].Value.ToString(), (this.ReportParameters["Id_Rel"].Value == null) ? "0" : this.ReportParameters["Id_Rel"].Value.ToString(), this.ReportParameters["Conexion"].Value.ToString());
        }

        private void subReport1_ItemDataBound(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.SubReport subReport = (Telerik.Reporting.Processing.SubReport)sender;
            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)subReport.InnerReport;
            subReport.Visible = report.ChildElements.Find("detail", true).Length > 0;
            Telerik.Reporting.DetailSection detail = new DetailSection();
            detail.Visible = subReport.Visible;
        }
    }
}