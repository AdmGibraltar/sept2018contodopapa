namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_InvControlRemisiones1A.
    /// </summary>
    public partial class Rep_InvControlRemisiones3 : Telerik.Reporting.Report
    {
        public Rep_InvControlRemisiones3()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void Rep_InvControlRemisiones3_NeedDataSource(object sender, EventArgs e)
        {
            this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
            //Transfer the ReportParameter value to the parameter of the select command
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Tm"].Value = this.ReportParameters["Id_Tm"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@FechaInicial"].Value = this.ReportParameters["FechaInicial"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@FechaFinal"].Value = this.ReportParameters["FechaFinal"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@RIK"].Value = this.ReportParameters["RIK"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Territorio"].Value = this.ReportParameters["Territorio"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Cliente"].Value = this.ReportParameters["Cliente"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Producto"].Value = this.ReportParameters["Producto"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@FechaActual"].Value = this.ReportParameters["FechaActual"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@DiasVencidos"].Value = (int)this.ReportParameters["Estatus"].Value == 1 ? 30 : 0;

            //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
            report.DataSource = this.sqlDataAdapter1;
        }
    }
}