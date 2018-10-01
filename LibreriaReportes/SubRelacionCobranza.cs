namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Data;

    /// <summary>
    /// Summary description for SubRelacionCobranza.
    /// </summary>
    public partial class SubRelacionCobranza : Telerik.Reporting.Report
    {
        public SubRelacionCobranza()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //
            //
            this.DataSource = null;
        }



        public int SetParameters(object Id_Emp, object Id_Cd, object Id_Rel, string Cnx)
        {
            try
            {
                this.ReportParameters["Id_Emp_SubParam"].Value = Id_Emp;
                this.ReportParameters["Id_Cd"].Value = Id_Cd;
                this.ReportParameters["Id_Pag"].Value = Id_Rel;
                this.ReportParameters["Emp_Cnx"].Value = Cnx;
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void SubRelacionCobranza_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = ReportParameters["Emp_Cnx"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command                    

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp_SubParam"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Pag"].Value = this.ReportParameters["Id_Pag"].Value;

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;

                DataTable tabla = new DataTable();
                this.sqlDataAdapter1.Fill(tabla);

                report.DataSource = tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}