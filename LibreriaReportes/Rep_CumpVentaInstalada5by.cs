namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_excesosInventarios2.
    /// </summary>
    public partial class Rep_CumpVentaInstalada5by: Telerik.Reporting.Report
    {
        public Rep_CumpVentaInstalada5by()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //
            
            //
            this.DataSource = null;
        }

        private void Rep_CumpVentaInstalada3by_NeedDataSource(object sender, EventArgs e)
        {
            try
            {              
                this.sqlConnection2.ConnectionString = this.ReportParameters["Emp_Cnx"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command             
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@SemanasStr"].Value = this.ReportParameters["Semana"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@RIK"].Value = this.ReportParameters["Rik"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Territorio"].Value = this.ReportParameters["Territorio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Producto"].Value = this.ReportParameters["Producto"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tipo"].Value = this.ReportParameters["Nivel"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@SubTipo"].Value = "Y"; 

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