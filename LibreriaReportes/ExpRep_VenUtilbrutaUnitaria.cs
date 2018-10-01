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
    /// Summary description for Rep_VenUtilbrutaUnitaria.
    /// </summary>
    public partial class ExpRep_VenUtilbrutaUnitaria : Telerik.Reporting.Report
    {
        public ExpRep_VenUtilbrutaUnitaria()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void Rep_VenUtilbrutaUnitaria_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

                if (this.ReportParameters["UBMin"].Value == null)
                {
                    this.textBox49.Visible = false;
                    this.textBox54.Visible = false;
                }

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ter"].Value = this.ReportParameters["Id_Ter"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cte"].Value = this.ReportParameters["Id_Cte"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@AñoIni"].Value = this.ReportParameters["AñoIni"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@AñoFin"].Value = this.ReportParameters["AñoFin"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@MesIni"].Value = this.ReportParameters["MesIni"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@MesFin"].Value = this.ReportParameters["MesFin"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@rbCalculadoCon"].Value = this.ReportParameters["rbCalculadoCon"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@UBMin"].Value = this.ReportParameters["UBP"].Value;

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