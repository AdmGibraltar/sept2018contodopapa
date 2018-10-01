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
    /// Summary description for SubCumpVentaC.
    /// </summary>
    public partial class ExpSubCumpVentaC2 : Telerik.Reporting.Report
    {
        public ExpSubCumpVentaC2()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //
            //
            this.DataSource = null;
        }


          #region Propiedades

        public string Id_Emp
        {
            get { return this.ReportParameters["Id_Emp"].Value.ToString(); }
            set { this.ReportParameters["Id_Emp"].Value = value; }
        }

        public string Id_Cd
        {
            get { return this.ReportParameters["Id_Cd"].Value.ToString(); }
            set { this.ReportParameters["Id_Cd"].Value = value; }
        }

        public string Semana
        {
            get { return this.ReportParameters["Semana"].Value.ToString(); }
            set { this.ReportParameters["Semana"].Value = value; }
        }

        public string Id_Cte
        {
            get { return this.ReportParameters["Id_Cte"].Value.ToString(); }
            set { this.ReportParameters["Id_Cte"].Value = value; }
        }

        public string Id_Ter
        {
            get { return this.ReportParameters["Id_Ter"].Value.ToString(); }
            set { this.ReportParameters["Id_Ter"].Value = value; }
        }

        public string Id_Rik
        {
            get { return this.ReportParameters["Id_Rik"].Value.ToString(); }
            set { this.ReportParameters["Id_Rik"].Value = value; }
        }

        public string Id_Prd
        {
            get { return this.ReportParameters["Id_Prd"].Value.ToString(); }
            set { this.ReportParameters["Id_Prd"].Value = value; }
        }

        public string Conexion
        {
            get { return this.ReportParameters["Emp_Cnx"].Value.ToString(); }
            set { this.ReportParameters["Emp_Cnx"].Value = value; }
        }
        #endregion


        private void SubCumpVentaC2_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.Conexion;
                //Transfer the ReportParameter value to the parameter of the select command                    

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.Id_Emp;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.Id_Cd;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Semana"].Value = this.Semana;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cte"].Value = this.Id_Cte;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ter"].Value = this.Id_Ter;
               // this.sqlDataAdapter1.SelectCommand.Parameters["@RIK"].Value = this.Id_Rik;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Prd"].Value = this.Id_Prd;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tipo"].Value = "2";//VENTA ESPORADICA

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