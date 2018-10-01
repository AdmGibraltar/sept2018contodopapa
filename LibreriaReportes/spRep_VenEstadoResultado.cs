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
    /// Summary description for spRep_VenEstadoResultado.
    /// </summary>
    public partial class spRep_VenEstadoResultado : Telerik.Reporting.Report
    {
        public spRep_VenEstadoResultado()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void spRep_VenEstadoResultado_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@mesInicial"].Value = this.ReportParameters["MesInicio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@anioInicial"].Value = this.ReportParameters["AñoInicio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@mesFinal"].Value = this.ReportParameters["MesFin"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@anioFinal"].Value = this.ReportParameters["AñoFin"].Value;

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