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
    public partial class ExpSubRelacionCobranza : Telerik.Reporting.Report
    {
        public ExpSubRelacionCobranza()
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

        public string Id_Pag
        {
            get { return this.ReportParameters["Id_Pag"].Value.ToString(); }
            set { this.ReportParameters["Id_Pag"].Value = value; }
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
                this.sqlConnection1.ConnectionString = this.Conexion;
                //Transfer the ReportParameter value to the parameter of the select command                    

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.Id_Emp;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.Id_Cd;               
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Pag"].Value = this.Id_Pag;

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