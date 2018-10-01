namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_FacFacturasCanceladas.
    /// </summary>
    public partial class ExpRep_FacFacturasCanceladas : Telerik.Reporting.Report
    {
        public ExpRep_FacFacturasCanceladas()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //            
            //
        }

        private void Rep_FacFacturasCanceladas_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd_Ver"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaInicial"].Value = this.ReportParameters["FechaInicial"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaFinal"].Value = this.ReportParameters["FechaFinal"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Operadas"].Value = this.ReportParameters["Operadas"].Value;

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