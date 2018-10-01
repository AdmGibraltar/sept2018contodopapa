namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_FacRegistroFacturacion.
    /// </summary>
    public partial class ExpRep_FacRegistroFacturacionBts : Telerik.Reporting.Report
    {
        public ExpRep_FacRegistroFacturacionBts()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void Rep_FacRegistroFacturacion_NeedDataSource(object sender, EventArgs e)
        {
            this.sqlConnection1.ConnectionString = this.ReportParameters["@Conexion"].Value.ToString();

            //Transfer the ReportParameter value to the parameter of the select command
            string[] arrayFechaInicial = this.ReportParameters["fechaInicial"].Value.ToString().Split(new char[] { '/' });
            string[] arrayFechaFinal = this.ReportParameters["fechaFinal"].Value.ToString().Split(new char[] { '/' });
            string fechaInicial = null;
            string fechaFinal = null;
            if (arrayFechaInicial.Length == 3)            
                fechaInicial = string.Concat(arrayFechaInicial[2], ".", arrayFechaInicial[1], ".", arrayFechaInicial[0]);
           
            if (arrayFechaFinal.Length == 3)            
                fechaFinal = string.Concat(arrayFechaFinal[2], ".", arrayFechaFinal[1], ".", arrayFechaFinal[0]);
            
            string estatus = null;
            if (!string.IsNullOrEmpty(this.ReportParameters["estatus"].Value.ToString()))            
                estatus = this.ReportParameters["estatus"].Value.ToString();
            
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Fecha_inicio"].Value = fechaInicial;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Fecha_fin"].Value = fechaFinal;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Estatus"].Value = estatus;

            //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
            report.DataSource = this.sqlDataAdapter1;
        }
    }
}