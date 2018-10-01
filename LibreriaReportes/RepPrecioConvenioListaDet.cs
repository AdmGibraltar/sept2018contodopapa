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
    /// Summary description for RepInvKardex.
    /// </summary>
    public partial class RepPrecioConvenioListaDet : Telerik.Reporting.Report
    {
        public RepPrecioConvenioListaDet()
        {
            try
            {
                InitializeComponent();

                //

                //
                this.DataSource = null;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
    
        }

        private void RepPrecioConvenioListaDet_NeedDataSource(object sender, EventArgs e)
        {
            try
            {


                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;

                this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoFiltro"].Value = this.ReportParameters["TipoFiltro"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Vencido"].Value = this.ReportParameters["Vencido"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cat"].Value = this.ReportParameters["Id_Cat"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Filtro"].Value = this.ReportParameters["Filtro"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
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