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
    /// Summary description for AcuerdoImpresion.
    /// </summary>
    public partial class AcuerdoImpresion : Telerik.Reporting.Report
    {
        public AcuerdoImpresion()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void AcuerdoImpresion_NeedDataSource(object sender, EventArgs e)
        {

            this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
            //Transfer the ReportParameter value to the parameter of the select command
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Acs"].Value = this.ReportParameters["Id_Acs"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_AcsVersion"].Value = this.ReportParameters["Id_AcsVersion"].Value;

            this.sqlDataAdapter2.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
            this.sqlDataAdapter2.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
            this.sqlDataAdapter2.SelectCommand.Parameters["@Id_Acs"].Value = this.ReportParameters["Id_Acs"].Value;
            this.sqlDataAdapter2.SelectCommand.Parameters["@Id_AcsVersion"].Value = this.ReportParameters["Id_AcsVersion"].Value;

            this.sqlDataAdapter3.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
            this.sqlDataAdapter3.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
            this.sqlDataAdapter3.SelectCommand.Parameters["@Id_Acs"].Value = this.ReportParameters["Id_Acs"].Value;
            this.sqlDataAdapter3.SelectCommand.Parameters["@Id_AcsVersion"].Value = this.ReportParameters["Id_AcsVersion"].Value;

            this.sqlDataAdapter4.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
            this.sqlDataAdapter4.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
            this.sqlDataAdapter4.SelectCommand.Parameters["@Id_Acs"].Value = this.ReportParameters["Id_Acs"].Value;
            this.sqlDataAdapter4.SelectCommand.Parameters["@Id_AcsVersion"].Value = this.ReportParameters["Id_AcsVersion"].Value;

            this.sqlDataAdapter5.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
            this.sqlDataAdapter5.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
            this.sqlDataAdapter5.SelectCommand.Parameters["@Id_Acs"].Value = this.ReportParameters["Id_Acs"].Value;
            this.sqlDataAdapter5.SelectCommand.Parameters["@Id_AcsVersion"].Value = this.ReportParameters["Id_AcsVersion"].Value;

            //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
            report.DataSource = this.sqlDataAdapter1;

            // ---------------------------------------------------------------------------------------------
            // Si se asigno correctamente el origen de datos, se actualiza el estatus de la orden de compra
            // ---------------------------------------------------------------------------------------------
            //actualiza estatus de orden de compra a Impreso (I)

            CN_CapAcys cn_capacys = new CN_CapAcys();
            Acys acys = new Acys();
            acys.Id_Emp = (int)ReportParameters["Id_Emp"].Value;
            acys.Id_Cd = (int)ReportParameters["Id_Cd"].Value;
            acys.Id_Acs = (int)ReportParameters["Id_Acs"].Value;
            acys.Acs_Estatus = "I";
            int verificador = 0;
            cn_capacys.Imprimir(acys, ReportParameters["Conexion"].Value.ToString(), ref verificador);

             
        }
    }
}