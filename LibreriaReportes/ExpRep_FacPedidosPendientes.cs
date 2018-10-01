namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_FacPedidosPendientes.
    /// </summary>
    public partial class ExpRep_FacPedidosPendientes : Telerik.Reporting.Report
    {
        public ExpRep_FacPedidosPendientes()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //            
            //
            this.DataSource = null;
        }

        private void Rep_FacPedidosPendientes_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd_Ver"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Territorios"].Value = this.ReportParameters["Territorios"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Clientes"].Value = this.ReportParameters["Clientes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Productos"].Value = this.ReportParameters["Productos"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tipo"].Value = 2;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaIni"].Value = this.ReportParameters["FechaIni"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaFin"].Value = this.ReportParameters["FechaFin"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Pedido"].Value = this.ReportParameters["Pedidos"].Value;

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

                // ---------------------------------------------------------------------------------------------
                // Si se asigno correctamente el origen de datos, se actualiza el estatus de la orden de compra
                // ---------------------------------------------------------------------------------------------
                ////actualiza estatus de entrada salida a Impreso (I)
                //int verificador = 0;
                //EntradaSalida entSal = new EntradaSalida();

                //entSal.Id_Emp = int.Parse(this.ReportParameters["Id_Emp"].Value.ToString());
                //entSal.Id_Cd = int.Parse(this.ReportParameters["Id_Cd"].Value.ToString());
                ////this.sqlDastaAdapter1.SelectCommand.Parameters["@Id_Es"].Value = this.ReportParameters["@Id_Es"].Value; <==borrame
                //entSal.Id_Es = int.Parse(this.ReportParameters["Id_Es"].Value.ToString());
                //entSal.Es_Naturaleza = int.Parse(this.ReportParameters["EsDet_Naturaleza"].Value.ToString());
                //entSal.Es_Estatus = "i";
                //new CN_CapEntradaSalida().ModificarEntradaSalida_Estatus(entSal, this.ReportParameters["Conexion"].Value.ToString(), ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}