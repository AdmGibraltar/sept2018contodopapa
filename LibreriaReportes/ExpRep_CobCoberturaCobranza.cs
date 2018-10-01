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
    /// Summary description for Rep_CobCoberturaCobranza.
    /// </summary>
    public partial class ExpRep_CobCoberturaCobranza : Telerik.Reporting.Report
    {
        public ExpRep_CobCoberturaCobranza()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void ExpRep_CobCoberturaCobranza_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

                string FIni = null;
                if (this.ReportParameters["FecIni"].Value != null)
                {
                    string[] arrayFechaPeriodo = this.ReportParameters["FecIni"].Value.ToString().Split(new char[] { '/' });
                    if (arrayFechaPeriodo.Length == 3)
                    {
                        FIni = string.Concat(arrayFechaPeriodo[2], ".", arrayFechaPeriodo[1], ".", arrayFechaPeriodo[0]);
                    }
                }

                string FFin = null;
                if (this.ReportParameters["FecFin"].Value != null)
                {
                    string[] arrayFechaPeriodo = this.ReportParameters["FecFin"].Value.ToString().Split(new char[] { '/' });
                    if (arrayFechaPeriodo.Length == 3)
                    {
                        FFin = string.Concat(arrayFechaPeriodo[2], ".", arrayFechaPeriodo[1], ".", arrayFechaPeriodo[0]);
                    }
                }
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd_Ver"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FecIni"].Value = FIni;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FecFin"].Value = FFin;

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