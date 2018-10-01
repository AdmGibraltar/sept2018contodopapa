namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_CobCargosCreditos1.
    /// </summary>
    public partial class ExpRep_CobCargosCreditos1 : Telerik.Reporting.Report
    {
        public ExpRep_CobCargosCreditos1()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //            
            //
            this.DataSource = null;
        }

        private void Rep_CobCargosCreditos12_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaIni"].Value = this.ReportParameters["FechaInicial"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaFin"].Value = this.ReportParameters["FechaFinal"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Cliente"].Value = this.ReportParameters["Cliente"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@CSaldoInicial"].Value = 1;

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