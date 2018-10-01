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
    public partial class ExpSubImproductividad : Telerik.Reporting.Report
    {
        public ExpSubImproductividad()
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

        public string Id_Spo
        {
            get { return this.ReportParameters["Id_Spo"].Value.ToString(); }
            set { this.ReportParameters["Id_Spo"].Value = value; }
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
        public string Conexion
        {
            get { return this.ReportParameters["Emp_Cnx"].Value.ToString(); }
            set { this.ReportParameters["Emp_Cnx"].Value = value; }
        }
        #endregion

        private void SubRelacionCobranza_NeedDataSource(object sender, EventArgs e)
        {
            try
            {

                this.sqlConnection1.ConnectionString = Conexion;
                //Transfer the ReportParameter value to the parameter of the select command                    

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = Id_Emp;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = Id_Cd;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Spo"].Value = Id_Spo;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cte"].Value = Id_Cte;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ter"].Value = Id_Ter;

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;

                DataTable tabla = new DataTable();
                this.sqlDataAdapter1.Fill(tabla);

                report.DataSource = tabla;

            }
            catch (Exception ex)
            {

            }
        }
    }
}