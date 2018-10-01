namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using CapaNegocios;
    using CapaEntidad;

    /// <summary>
    /// Summary description for Rep_InvOrdenCompra.
    /// </summary>
    public partial class Rep_InvOrdenCompra : Telerik.Reporting.Report
    {
        public Rep_InvOrdenCompra()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //
            
            //
            this.DataSource = null;
        }

        private void Rep_InvOrdenCompra_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                string OrdenEstatus = null;
                if (this.ReportParameters["estatus"].Value.ToString() == "Vencidos")                
                    OrdenEstatus = "VEN";           
                if (this.ReportParameters["estatus"].Value.ToString() == "Sin vencer")               
                    OrdenEstatus = "VIG";         
                string IdOrd = null;
                if (this.ReportParameters["Id_Ord"].Value.ToString() != "")               
                    IdOrd = this.ReportParameters["Id_Ord"].Value.ToString();  
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Prd"].Value = this.ReportParameters["Id_Prd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ord"].Value = IdOrd;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Estatus"].Value = OrdenEstatus;

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