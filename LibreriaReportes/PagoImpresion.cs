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
    /// Summary description for PagoImpresion.
    /// </summary>
    public partial class PagoImpresion : Telerik.Reporting.Report
    {
        public PagoImpresion()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void PagoImpresion_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Pag"].Value = this.ReportParameters["Id_Pag"].Value;


                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

                // ---------------------------------------------------------------------------------------------
                // Si se asigno correctamente el origen de datos, se actualiza el estatus del pago
                // ---------------------------------------------------------------------------------------------
                //actualiza estatus de orden de compra a Impreso (I)

                CN_CapPago cn_cappedido = new CN_CapPago();
                Pago pag = new Pago();
                pag.Id_Emp = (int)ReportParameters["Id_Emp"].Value;
                pag.Id_Cd = (int)ReportParameters["Id_Cd"].Value;
                pag.Id_Pag = (int)ReportParameters["Id_Pag"].Value;

                int verificador = 0;
                cn_cappedido.Imprimir(pag, ReportParameters["Conexion"].Value.ToString(), ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}