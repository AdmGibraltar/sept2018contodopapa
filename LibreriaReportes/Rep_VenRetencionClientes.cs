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
    public partial class Rep_VenRetencionClientes: Telerik.Reporting.Report
    {
        public Rep_VenRetencionClientes()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //
            
            //
            this.DataSource = null;
        }

        private void Rep_VenRetencionClientes_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                int valor = Convert.ToInt32(this.ReportParameters["Agrupar"].Value);
                if (valor == 2)
                {
                    this.groupHeaderSection1.Visible = false;
                    this.groupFooterSection1.Visible = false;
                    this.groupHeaderSection1.Group.Grouping.Clear();
                }
               
                this.sqlConnection2.ConnectionString = this.ReportParameters["Emp_Cnx"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command             
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Territorio"].Value = this.ReportParameters["Territorio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@ColIni1"].Value = this.ReportParameters["Colini1"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@ColFin1"].Value = this.ReportParameters["Colfin1"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@ColIni2"].Value = this.ReportParameters["Colini2"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@ColFin2"].Value = this.ReportParameters["Colfin2"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@ColIni3"].Value = this.ReportParameters["Colini3"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@ColFin3"].Value = this.ReportParameters["Colfin3"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@ColIni4"].Value = this.ReportParameters["Colini4"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@ColFin4"].Value = this.ReportParameters["Colfin4"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Agrupar"].Value = this.ReportParameters["Agrupar"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Todos"].Value = this.ReportParameters["Todos"].Value;

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