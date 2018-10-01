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
    /// Summary description for PagoListado.
    /// </summary>
    public partial class RepPedidoVI : Telerik.Reporting.Report
    {
        public RepPedidoVI()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void RepPedidoVI_NeedDataSource(object sender, EventArgs e)
        {

            try
            {

                this.textBox5.Value =Convert.ToInt32 (this.ReportParameters["CteIni"].Value) == 0 ? "" : this.ReportParameters["CteIni"].Value.ToString();
                this.textBox7.Value = Convert.ToInt32(this.ReportParameters["CteFin"].Value) == 0 ? "" : this.ReportParameters["CteFin"].Value.ToString();
                this.textBox10.Value = Convert.ToInt32(this.ReportParameters["TerIni"].Value) == 0 ? "" : this.ReportParameters["TerIni"].Value.ToString();
                this.textBox8.Value = Convert.ToInt32(this.ReportParameters["TerFin"].Value) == 0 ? "" : this.ReportParameters["TerFin"].Value.ToString();
                this.textBox20.Value = Convert.ToInt32(this.ReportParameters["SemIni"].Value) == 0 ? "" : this.ReportParameters["SemIni"].Value.ToString();
                this.textBox27.Value = Convert.ToInt32(this.ReportParameters["SemFin"].Value) == 0 ? "" : this.ReportParameters["SemFin"].Value.ToString();
                this.textBox30.Value = Convert.ToInt32(this.ReportParameters["AnioIni"].Value) == 0 ? "" : this.ReportParameters["AnioIni"].Value.ToString();
                this.textBox28.Value = Convert.ToInt32(this.ReportParameters["AnioFin"].Value) == 0 ? "" : this.ReportParameters["AnioFin"].Value.ToString();
                
                
                
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Estatus"].Value = this.ReportParameters["Estatus"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Vigencia"].Value = this.ReportParameters["Vigencia"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@CteIni"].Value = this.ReportParameters["CteIni"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@CteFin"].Value = this.ReportParameters["CteFin"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@SemIni"].Value = this.ReportParameters["SemIni"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@SemFin"].Value = this.ReportParameters["SemFin"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@AnioIni"].Value = this.ReportParameters["AnioIni"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@AnioFin"].Value = this.ReportParameters["AnioFin"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TerIni"].Value = this.ReportParameters["TerIni"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TerFin"].Value = this.ReportParameters["TerFin"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_U"].Value = this.ReportParameters["Id_U"].Value;



                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

                // ---------------------------------------------------------------------------------------------
                // Si se asigno correctamente el origen de datos, se actualiza el estatus de la orden de compra
                // ---------------------------------------------------------------------------------------------
                //actualiza estatus de orden de compra a Impreso (I)

                //CN_CapPago cn_cappedido = new CN_CapPago();
                //Pago pag = new Pago();
                //pag.Id_Emp = (int)ReportParameters["@Id_Emp"].Value;
                //pag.Id_Cd = (int)ReportParameters["@Id_Cd"].Value;
                //pag.Id_Pag = (int)ReportParameters["@Id_Ped"].Value;

                //int verificador = 0;
                //cn_cappedido.Imprimir(pag, ReportParameters["@Conexion"].Value.ToString(), ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}