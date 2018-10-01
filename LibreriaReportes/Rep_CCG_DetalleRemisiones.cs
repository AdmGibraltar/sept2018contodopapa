namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_CCG_DetalleRemisiones.
    /// </summary>
    public partial class Rep_CCG_DetalleRemisiones : Telerik.Reporting.Report
    {
        public Rep_CCG_DetalleRemisiones()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        private void Rep_CCG_DetalleRemisiones_NeedDataSource(object sender, EventArgs e)
        {
            this.sqlDataSource1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
            this.Report.DataSource = sqlDataSource1;
        }
    }
}