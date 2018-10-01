namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_excesosInventarios2.
    /// </summary>
    public partial class ExpRep_CargosPdtesxRik: Telerik.Reporting.Report
    { 
        static double saldo_anterior = 0;
        static int Id_cliente = 0;
        static int Id_territorio = 0;

        public ExpRep_CargosPdtesxRik()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //            
            //
            this.DataSource = null;
        }

        private void ExpRep_CargosPdtesxRik_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                saldo_anterior = 0;
                validacion();
                this.sqlConnection2.ConnectionString = this.ReportParameters["Emp_Cnx"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command             
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cte"].Value = this.ReportParameters["strCliente"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Territorio"].Value = this.ReportParameters["strTerritorio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@DiasIni"].Value = this.ReportParameters["DiasIni"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@DiasFin"].Value = this.ReportParameters["DiasFin"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@agrupador"].Value = this.ReportParameters["agrupador"].Value;  
                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void validacion()
        {
            int agrupador = Convert.ToInt32(this.ReportParameters["agrupador"].Value);
            if (agrupador == 1)
            {
                textBox4.Visible = false;
                textBox5.Visible = false;
                textBox8.Visible = false;
                textBox22.Value = "";
                textBox32.Value = "";
                this.groupHeaderSection1.Visible = false;
                this.groupFooterSection1.Visible = false;
            }          
        }

        public static double calcularSaldo(double cargo, int cliente, int territorio)
        {
            if (Id_cliente != cliente || Id_territorio != territorio)
            {
                saldo_anterior = 0;
                Id_cliente = cliente;
                Id_territorio = territorio;
            }

            saldo_anterior += cargo;
            return saldo_anterior;
        }
    }
}