namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_SerNoConformidades.
    /// </summary>
    public partial class Rep_RemisionVencidaB : Telerik.Reporting.Report
    {
        public Rep_RemisionVencidaB()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //

            // 
            this.DataSource = null;
        }

        private void Rep_SerNoConformidades_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                //this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                this.sqlConnection1.ConnectionString = this.ReportParameters["Emp_Cnx"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command             
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ter"].Value = this.ReportParameters["Territorio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cte"].Value = this.ReportParameters["Clientes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaIni"].Value = this.ReportParameters["FechaIni"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaFin"].Value = this.ReportParameters["FechaFin"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Vencido"].Value = this.ReportParameters["Vencido"].Value.ToString() == "-1" ? null : this.ReportParameters["Vencido"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoRemision"].Value = this.ReportParameters["TipoRemision"].Value;

                switch (ReportParameters["Vencido"].Value.ToString())
                {
                    case "-1": textBox70.Value = "Todos"; break;
                    case "1": textBox70.Value = "Si"; break;
                    case "0": textBox70.Value = "No"; break;
                }

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

                //if (ReportParameters["SFecha"].Value != null)
                //{
                //    textBox28.Value = ReportParameters["SFecha"].Value.ToString().Split(new string[] { "-" }, StringSplitOptions.None)[0].Trim();
                //    textBox20.Value = ReportParameters["SFecha"].Value.ToString().Split(new string[] { "-" }, StringSplitOptions.None)[1].Trim();
                //}
                //else
                //{
                //    textBox53.Visible = false;
                //    textBox19.Visible = false;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}