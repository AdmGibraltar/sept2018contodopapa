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
    /// Summary description for OrdenCompraImpresion.
    /// </summary>
    public partial class OrdenCompraImpresion : Telerik.Reporting.Report
    {
        public OrdenCompraImpresion()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //

            this.DataSource = null;
        }

        private void OrdenCompraImpresion_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ord"].Value = this.ReportParameters["Id_Ord"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Responsable"].Value = this.ReportParameters["@IdUserCombo"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Concepto"].Value = this.ReportParameters["@TipoCompromiso"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@FechaIni"].Value = this.ReportParameters["@FechaA"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@FechaFin"].Value = this.ReportParameters["@FechaB"].Value;

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

                // ---------------------------------------------------------------------------------------------
                // Si se asigno correctamente el origen de datos, se actualiza el estatus de la orden de compra
                // ---------------------------------------------------------------------------------------------
                //actualiza estatus de orden de compra a Impreso (I)
                int verificador = 0;
                OrdenCompra ordCompra = new OrdenCompra();
                ordCompra.Id_Emp = Convert.ToInt32(this.ReportParameters["Id_Emp"].Value);
                ordCompra.Id_Cd = Convert.ToInt32(this.ReportParameters["Id_Cd"].Value);
                ordCompra.Id_Ord = Convert.ToInt32(this.ReportParameters["Id_Ord"].Value);
                ordCompra.Ord_Estatus = "I";
                new CN_CapOrdenCompra().ModificarOrdenCompra_Estatus(ordCompra, this.ReportParameters["Conexion"].Value.ToString(), ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}