namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for RepDeficit.
    /// </summary>
    public partial class Rep_InvTipoMovimiento : Telerik.Reporting.Report
    {
        public Rep_InvTipoMovimiento()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            this.DataSource = null;
            //
        }

        private void RepDeficit_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                 
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Estatus"].Value = this.ReportParameters["Estatus"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tmovimiento"].Value = this.ReportParameters["Tmovimiento"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tdocumento"].Value = this.ReportParameters["Tdocumento"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ptp"].Value = this.ReportParameters["Id_Ptp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Productos"].Value = this.ReportParameters["Productos"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaIni"].Value = this.ReportParameters["FechaIni"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaFin"].Value = this.ReportParameters["FechaFin"].Value;

                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}