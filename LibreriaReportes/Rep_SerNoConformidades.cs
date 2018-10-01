namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_SerNoConformidades.
    /// </summary>
    public partial class Rep_SerNoConformidades: Telerik.Reporting.Report
    {
        public Rep_SerNoConformidades()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //
            
            //
            this.DataSource = null;
        }

        private void Rep_SerNoConformidades_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                //this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                this.sqlConnection2.ConnectionString = this.ReportParameters["Emp_Cnx"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command             
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Rec"].Value = this.ReportParameters["Reclamacion"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Cliente"].Value = this.ReportParameters["Clientes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Territorio"].Value = this.ReportParameters["Territorio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tipo"].Value = this.ReportParameters["TipoRecl"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Rec_Estatus"].Value = this.ReportParameters["EstatusRecl"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaIni"].Value = this.ReportParameters["Fechaini"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaFin"].Value = this.ReportParameters["Fechafin"].Value;
              
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