namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using CapaEntidad;
    using CapaNegocios;

    /// <summary>
    /// Summary description for PedidoImpresion.
    /// </summary>
    public partial class PedidoImpresion : Telerik.Reporting.Report
    {
        public PedidoImpresion()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //

            this.DataSource = null;
        }

        private void PedidoImpresion_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ped"].Value = this.ReportParameters["Id_Ped"].Value;

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

                // ---------------------------------------------------------------------------------------------
                // Si se asigno correctamente el origen de datos, se actualiza el estatus de la orden de compra
                // ---------------------------------------------------------------------------------------------
                //actualiza estatus de orden de compra a Impreso (I)

                CN_CapPedido cn_cappedido = new CN_CapPedido();
                Pedido ped = new Pedido();
                ped.Id_Emp = (int)ReportParameters["Id_Emp"].Value;
                ped.Id_Cd = (int)ReportParameters["Id_Cd"].Value;
                ped.Id_Ped = (int)ReportParameters["Id_Ped"].Value;
                ped.Estatus = "I";
                int verificador = 0;
                cn_cappedido.Imprimir(ped, ReportParameters["Conexion"].Value.ToString(), ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}