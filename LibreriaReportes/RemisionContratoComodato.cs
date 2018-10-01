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
    /// Summary description for RemisionContratoComodato.
    /// </summary>
    public partial class RemisionContratoComodato : Telerik.Reporting.Report
    {
        public RemisionContratoComodato()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            this.DataSource = null; 
        }

        private void RemisionContratoComodato_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Rem"].Value = this.ReportParameters["Id_Rem"].Value;

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;


                // ---------------------------------------------------------------------------------------------
                // Si se asigno correctamente el origen de datos, se actualiza el estatus de la orden de compra
                // ---------------------------------------------------------------------------------------------
                //actualiza estatus de entrada salida a Impreso (I)
                int verificador = 0;
                Remision remision = new Remision();

                remision.Id_Emp = int.Parse(this.ReportParameters["Id_Emp"].Value.ToString());
                remision.Id_Cd = int.Parse(this.ReportParameters["Id_Cd"].Value.ToString());
                remision.Id_Rem = int.Parse(this.ReportParameters["Id_Rem"].Value.ToString());
                remision.Rem_Estatus = "I";
                new CN_CapRemision().ModificarRemision_Estatus(remision, this.ReportParameters["Conexion"].Value.ToString(), ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}