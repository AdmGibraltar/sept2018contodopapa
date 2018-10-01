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
    /// Summary description for Rep_Valuacion_CtasMarg
    /// </summary>
    public partial class Rep_Valuacion_CtasMarg : Telerik.Reporting.Report
    {
        public Rep_Valuacion_CtasMarg()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void Rep_Valuacion_CtasMarg_NeedDataSource(object sender, EventArgs e)
        {
            try
            {               
               // this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Vap"].Value = this.ReportParameters["Id_Vap"].Value;

                this.sqlDataAdapter2.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter2.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter2.SelectCommand.Parameters["@Id_Vap"].Value = this.ReportParameters["Id_Vap"].Value;

                this.sqlDataAdapter3.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter3.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter3.SelectCommand.Parameters["@Id_Vap"].Value = this.ReportParameters["Id_Vap"].Value;

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
            this.subTablaProducto1.Id_Emp = this.ReportParameters["Id_Emp"].Value.ToString();
            this.subTablaProducto1.Id_Cd = this.ReportParameters["Id_Cd"].Value.ToString();
            this.subTablaProducto1.Id_Vap = this.ReportParameters["Id_Vap"].Value.ToString();

            this.subTablaProducto1.Conexion = this.ReportParameters["Conexion"].Value.ToString();
        }

        private void subReport1_ItemDataBound(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.SubReport subReport = (Telerik.Reporting.Processing.SubReport)sender;
            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)subReport.InnerReport;
            subReport.Visible = report.ChildElements.Find("detail", true).Length > 0;
            Telerik.Reporting.DetailSection detail = new DetailSection();
            detail.Visible = subReport.Visible;
        }


        private void subReport2_NeedDataSource(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.ReportItemBase reporteBase = (Telerik.Reporting.Processing.ReportItemBase)sender;
            this.subTablaProducto2.Id_Emp = this.ReportParameters["Id_Emp"].Value.ToString();
            this.subTablaProducto2.Id_Cd = this.ReportParameters["Id_Cd"].Value.ToString();
            this.subTablaProducto2.Id_Vap = this.ReportParameters["Id_Vap"].Value.ToString();

            this.subTablaProducto2.Conexion = this.ReportParameters["Conexion"].Value.ToString();
        }

        private void subReport2_ItemDataBound(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.SubReport subReport = (Telerik.Reporting.Processing.SubReport)sender;
            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)subReport.InnerReport;
            subReport.Visible = report.ChildElements.Find("detail", true).Length > 0;
            Telerik.Reporting.DetailSection detail = new DetailSection();
            detail.Visible = subReport.Visible;
        }
    }
}