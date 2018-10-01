namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for spRep_CobRotacionCartera.
    /// </summary>
    public partial class Rep_CobRotacionCartera : Telerik.Reporting.Report
    {
        public Rep_CobRotacionCartera()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void spRep_CobRotacionCartera_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

                string FPer = null;
                if (this.ReportParameters["FecPeriodo"].Value != null)
                {
                    string[] arrayFechaPeriodo = this.ReportParameters["FecPeriodo"].Value.ToString().Split(new char[] { '/' });
                    if (arrayFechaPeriodo.Length == 3)
                    {
                        FPer = string.Concat(arrayFechaPeriodo[2], ".", arrayFechaPeriodo[1], ".", arrayFechaPeriodo[0]);
                    }
                }

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd_Ver"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ter"].Value = this.ReportParameters["Id_Ter"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FecPeriodo"].Value = FPer;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Agrupado"].Value = 1;

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