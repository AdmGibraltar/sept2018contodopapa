namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_RelacionFacturacionTer.
    /// </summary>
    public partial class Rep_RelacionFacturacionTer : Telerik.Reporting.Report
    {
        public Rep_RelacionFacturacionTer()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //
            
            //
            this.DataSource = null;
        }

        private void Rep_RelacionFacturacionTer_NeedDataSource(object sender, EventArgs e)
        {
            try
            {                               
                this.sqlConnection2.ConnectionString = this.ReportParameters["Emp_Cnx"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command             
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cte1"].Value = this.ReportParameters["Cliente1"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cte2"].Value = this.ReportParameters["Cliente2"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ter1"].Value = this.ReportParameters["Territorio1"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ter2"].Value = this.ReportParameters["Territorio2"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaInicio"].Value = this.ReportParameters["FechaInicial"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaFin"].Value = this.ReportParameters["FechaFinal"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Fac1"].Value = this.ReportParameters["Facturas1"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Fac2"].Value = this.ReportParameters["Facturas2"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Estatus"].Value = this.ReportParameters["Estatus"].Value;

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