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
    /// Summary description for SubSubTablaProductos.
    /// </summary>
    public partial class SubTablaProductos2VP : Telerik.Reporting.Report
    {
        public SubTablaProductos2VP()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //
            //
          
        }


        public SubTablaProductos2VP(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }



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

        public string Id_Vap
        {
            get { return this.ReportParameters["Id_Vap"].Value.ToString(); }
            set { this.ReportParameters["Id_Vap"].Value = value; }
        }

        public string Conexion
        {
            get { return this.ReportParameters[3].Value.ToString(); }
            set { this.ReportParameters[3].Value = value; }
        }

        private void SubTablaProductos2VP_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.Conexion;
                //Transfer the ReportParameter value to the parameter of the select command                    

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.Id_Emp;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.Id_Cd;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Vap"].Value = this.Id_Vap;

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