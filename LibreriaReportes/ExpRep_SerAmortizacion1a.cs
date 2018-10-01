namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_excesosInventarios2a.
    /// </summary>
    public partial class ExpRep_SerAmortizacion1a : Telerik.Reporting.Report
    {
        public ExpRep_SerAmortizacion1a()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //
            
            //
            this.DataSource = null;
        }

        private void Rep_SerAmortizacionSisP_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                int valor = Convert.ToInt32(this.ReportParameters["Reporte"].Value);
                //ocultarcampos(valor);

                this.sqlConnection2.ConnectionString = this.ReportParameters["Emp_Cnx"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command             
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Rik"].Value = this.ReportParameters["Representante"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ter"].Value = this.ReportParameters["Territorio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cte"].Value = this.ReportParameters["Cliente"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Prd"].Value = this.ReportParameters["Equipo"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Año"].Value = this.ReportParameters["Año"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Mes"].Value = this.ReportParameters["Mes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tipo"].Value = 1;

 
                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void ocultarcampos(int valor)
        //{
        //    switch (valor)
        //    {//tipo de reporte
        //        case 1:
        //            break;
        //        case 2:
        //            textBox13.Visible = false;
        //            textBox22.Visible = false;                                        
        //            textBox2.Visible = false;
        //            textBox24.Visible = false;
        //            textBox45.Visible = false;
        //            textBox46.Visible = false;
        //            break;
        //        case 3:
        //            textBox22.Visible = false;
        //            textBox44.Visible = false;
        //            textBox52.Visible = false;
        //            break;
        //        case 4:
        //            textBox13.Visible = false;
        //            textBox14.Visible = false;                    
        //            textBox22.Visible = false;
        //            textBox2.Visible = false;
        //            textBox24.Visible = false;
        //            textBox39.Visible = false;
        //            textBox50.Visible = false;                   
        //            textBox45.Visible = false;
        //            textBox46.Visible = false;
        //            break;
        //    }
        //}
    }
}