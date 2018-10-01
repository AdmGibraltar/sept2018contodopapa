namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_InvValorizacionInventarios.
    /// </summary>
    public partial class ExpRep_InvCumplimientoEntregas2 : Telerik.Reporting.Report
    {
        public ExpRep_InvCumplimientoEntregas2()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //            
            //
            this.DataSource = null;
        }

        private void Rep_InvCumplimientoEntregas_NeedDataSource(object sender, EventArgs e)
        {
            this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

            ////Transfer the ReportParameter value to the parameter of the select command
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = Convert.ToInt32(this.ReportParameters["Id_Emp"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = Convert.ToInt32(this.ReportParameters["Id_Cd"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@fechaInicial"].Value = this.ReportParameters["fecha1"].Value.ToString();
            this.sqlDataAdapter1.SelectCommand.Parameters["@fechaFinal"].Value = this.ReportParameters["fecha2"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_OrdInicial"].Value = this.ReportParameters["folio1"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_OrdFinal"].Value = this.ReportParameters["folio2"].Value.ToString();
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_PrdStr"].Value = this.ReportParameters["Id_PrdStrDesgloce"].Value.ToString() == string.Empty ? (object)null : this.ReportParameters["Id_PrdStrDesgloce"].Value.ToString();
            if (this.ReportParameters["proveedor"].Value.ToString() == "Sian")
            {
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Pvd"].Value = 100;
                this.sqlDataAdapter1.SelectCommand.Parameters["@ProvComprasConv"].Value = null;
                this.sqlDataAdapter1.SelectCommand.Parameters["@ProvComprasLocal"].Value = null;
            }
            if (this.ReportParameters["proveedor"].Value.ToString() == "Compras locales")
            {
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Pvd"].Value = null;
                this.sqlDataAdapter1.SelectCommand.Parameters["@ProvComprasConv"].Value = null;
                this.sqlDataAdapter1.SelectCommand.Parameters["@ProvComprasLocal"].Value = 1;
            }
            if (this.ReportParameters["proveedor"].Value.ToString() == "Proveedores de convenio")
            {
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Pvd"].Value = null;
                this.sqlDataAdapter1.SelectCommand.Parameters["@ProvComprasConv"].Value = 1;
                this.sqlDataAdapter1.SelectCommand.Parameters["@ProvComprasLocal"].Value = null;
            }

            //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
            report.DataSource = this.sqlDataAdapter1;
        }
    }
}