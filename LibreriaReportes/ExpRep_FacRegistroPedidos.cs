namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_FacRegistroPedidos.
    /// </summary>
    public partial class ExpRep_FacRegistroPedidos : Telerik.Reporting.Report
    {
        public ExpRep_FacRegistroPedidos()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //            
            //
        }

        private void Rep_FacRegistroPedidos_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd_Ver"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Fecha_inicial"].Value = this.ReportParameters["Fecha_inicial"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Fecha_final"].Value = this.ReportParameters["Fecha_final"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Pedidos_a"].Value = this.ReportParameters["Pedidos_a"].Value;
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