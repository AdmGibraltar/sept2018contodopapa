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
    public partial class Rep_SerBaseInstalada : Telerik.Reporting.Report
    {
        public Rep_SerBaseInstalada()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            this.DataSource = null;
            //
        }

        private void Rep_InvControlRemisiones1A_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Grupo"].Value = this.ReportParameters["Grupo"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Territorio"].Value = this.ReportParameters["Territorio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Cliente"].Value = this.ReportParameters["Cliente"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Equipos"].Value = this.ReportParameters["Equipos"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Rik"].Value = this.ReportParameters["Rik"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Año"].Value = this.ReportParameters["Año"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Detalle"].Value = this.ReportParameters["Detalle"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Nuevo"].Value = this.ReportParameters["Agrupar"].Value;

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
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