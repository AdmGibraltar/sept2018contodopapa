namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for SubRep_DetalleRemisionMeta.
    /// </summary>
    public partial class SubRep_DetalleRemisionMeta : Telerik.Reporting.Report
    {
        public SubRep_DetalleRemisionMeta()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        private void SubRep_DetalleRemisionMeta_NeedDataSource(object sender, EventArgs e)
        {
            this.sqlDataSource1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
            this.Report.DataSource = sqlDataSource1;
        }
    }
}