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
    public partial class RepCumplimientoVISemanalGnral : Telerik.Reporting.Report
    {
        public RepCumplimientoVISemanalGnral()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void RepCumplimientoVISemanalGnral_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Anio"].Value = this.ReportParameters["Anio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Mes"].Value = this.ReportParameters["Mes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Semanas"].Value = this.ReportParameters["Semanas"].Value;
//                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Seg"].Value = this.ReportParameters["Id_Seg"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Rik"].Value = this.ReportParameters["Id_Rik"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ter"].Value = this.ReportParameters["Id_Ter"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cte"].Value = this.ReportParameters["Id_Cte"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tipo"].Value = this.ReportParameters["Tipo"].Value;

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