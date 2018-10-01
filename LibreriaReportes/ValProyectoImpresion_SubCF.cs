namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for ValProyectoImpresion_SubCF.
    /// </summary>
    public partial class ValProyectoImpresion_SubCF : Telerik.Reporting.Report
    {
        public ValProyectoImpresion_SubCF()
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
            get { return this.ReportParameters["@Id_Emp"].Value.ToString(); }
            set { this.ReportParameters["@Id_Emp"].Value = value; }
        }

        public string Id_Cd
        {
            get { return this.ReportParameters["@Id_Cd"].Value.ToString(); }
            set { this.ReportParameters["@Id_Cd"].Value = value; }
        }

        public string Id_Vap
        {
            get { return this.ReportParameters["@Id_Vap"].Value.ToString(); }
            set { this.ReportParameters["@Id_Vap"].Value = value; }
        }

        public string Conexion
        {
            get { return this.ReportParameters["@Conexion"].Value.ToString(); }
            set { this.ReportParameters["@Conexion"].Value = value; }
        }

        #endregion

        private void ValProyectoImpresion_SubCF_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlConnection1.ConnectionString = this.Conexion;

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.Id_Emp;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.Id_Cd;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Vap"].Value = this.Id_Vap;

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
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