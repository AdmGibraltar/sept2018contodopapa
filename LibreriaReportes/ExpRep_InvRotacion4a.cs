namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Globalization;

    /// <summary>
    /// Summary description for Rep_InvRotacion4a.
    /// </summary>
    public partial class ExpRep_InvRotacion4a : Telerik.Reporting.Report
    {
        public ExpRep_InvRotacion4a()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void Rep_InvRotacion4a_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Fecha"].Value = this.ReportParameters["Fecha"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_PrdStr"].Value = this.ReportParameters["Id_Prd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_CteStr"].Value = this.ReportParameters["Cliente"].Value;

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;


                DateTime fecha_Actual = DateTime.Now;
                textBox59.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-3).ToString("MMM"));
                textBox60.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-2).ToString("MMM"));
                textBox61.Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha_Actual.AddMonths(-1).ToString("MMM"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}