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
    /// Summary description for FacturaRevCobImpresion.
    /// </summary>
    public partial class FacturaSvtaImpresion : Telerik.Reporting.Report
    {
        public FacturaSvtaImpresion()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //

            //
            this.DataSource = null;
        }

        private void FacturaSvtaImpresion_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Fva"].Value = this.ReportParameters["Folio"].Value;

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

                // ---------------------------------------------------------------------------------------------
                // Si se asigno correctamente el origen de datos, se actualiza el estatus de la orden de compra
                // ---------------------------------------------------------------------------------------------
                //actualiza estatus de orden de compra a Impreso (I)
                int verificador = 0;
                FacturaSvtaAlmacen facturaSvtaAlmacen = new FacturaSvtaAlmacen();
                facturaSvtaAlmacen.Id_Emp = Convert.ToInt32(this.ReportParameters["Id_Emp"].Value);
                facturaSvtaAlmacen.Id_Cd = Convert.ToInt32(this.ReportParameters["Id_Cd"].Value);
                facturaSvtaAlmacen.Id_Fva = Convert.ToInt32(this.ReportParameters["Folio"].Value);
                facturaSvtaAlmacen.Fva_Estatus = "I";
                new CN_CapFacturaSvtaAlmacen().ModificarEstatusFacturaSvtaAlmacen(facturaSvtaAlmacen, this.ReportParameters["Conexion"].Value.ToString(), ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}