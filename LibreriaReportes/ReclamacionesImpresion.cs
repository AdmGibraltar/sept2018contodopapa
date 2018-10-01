namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using CapaNegocios;
    using CapaEntidad;

    /// <summary>
    /// Summary description for ReclamacionesOmpresion.
    /// </summary>
    public partial class ReclamacionesImpresion : Telerik.Reporting.Report
    {
        public ReclamacionesImpresion()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void ReclamacionesImpresion_NeedDataSource(object sender, EventArgs e)
        {
            this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
            this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Rec"].Value = this.ReportParameters["Id_Rec"].Value;

            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
            report.DataSource = this.sqlDataAdapter1;
        }
    }
}