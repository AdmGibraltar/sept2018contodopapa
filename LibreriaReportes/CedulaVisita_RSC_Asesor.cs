namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for CedulaVisita_RSC_Asesor.
    /// </summary>
    public partial class CedulaVisita_RSC_Asesor : Telerik.Reporting.Report
    {
        public CedulaVisita_RSC_Asesor()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.DataSource = null;
        }

        private void CedulaVisita_RSC_Asesor_NeedDataSource(object sender, EventArgs e)
        {
            this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Acs"].Value = this.ReportParameters["Id_Acs"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_AcsVersion"].Value = this.ReportParameters["Id_AcsVersion"].Value;
                        
            this.DataSource = sqlDataAdapter1;
        }
    }
}