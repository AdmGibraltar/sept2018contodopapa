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
    /// Summary description for Rep_CobRelacionCobranzaFto2.
    /// </summary>
    public partial class Rep_CobRelacionCobranzaFtoR2 : Telerik.Reporting.Report
    {
        public Rep_CobRelacionCobranzaFtoR2()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void Rep_CobRelacionCobranzaFto2_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                //if (this.ReportParameters["Orden"].Value.ToString() == "Cliente y territorio")
                //{
                //    new Telerik.Reporting.Sorting("=Fields.[Pag_Id_Cd] Asc, =Fields.[Id_Cte] Asc",
                //        Telerik.Reporting.SortDirection.Asc);
                //}
                //else
                //{
                //    new Telerik.Reporting.Sorting("= Fields.[Id_Pag]", Telerik.Reporting.SortDirection.Asc);
                //}

                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Rel"].Value = this.ReportParameters["Id_Rel"].Value;

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