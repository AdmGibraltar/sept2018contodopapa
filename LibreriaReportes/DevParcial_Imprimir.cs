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
   
    public partial class DevParcial_Imprimir : Telerik.Reporting.Report
    {
        public DevParcial_Imprimir()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void DevParcial_Imprimir_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection2.ConnectionString = this.ReportParameters["Emp_Cnx"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Dev"].Value = this.ReportParameters["Id_Nca2"].Value;                

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

                // ---------------------------------------------------------------------------------------------
                // Si se asigno correctamente el origen de datos, se actualiza el estatus de la devolucion parcial
                // ---------------------------------------------------------------------------------------------
                //actualiza estatus de devolucion parcial a Impreso (I)
                int verificador = 0;                
                DevParcial_Detalle devParcial = new DevParcial_Detalle();
                devParcial.Territorio = int.Parse(this.ReportParameters["Id_Emp"].Value.ToString());
                devParcial.TipoDev = int.Parse(this.ReportParameters["Id_Cd"].Value.ToString());
                devParcial.TipoMovimiento = int.Parse(this.ReportParameters["Id_Nca2"].Value.ToString());
                new CN_DevParcialDetalle().ActualizarDevParcialImpresion(devParcial, this.ReportParameters["Emp_Cnx"].Value.ToString(), ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}