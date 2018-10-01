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
    public partial class ExpRep_VenUtilidadBruta1 : Telerik.Reporting.Report
    {
        public ExpRep_VenUtilidadBruta1()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void Rep_InvValorizacionInventarios_NeedDataSource(object sender, EventArgs e)
        {
            this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

            //Transfer the ReportParameter value to the parameter of the select command
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = Convert.ToInt32(this.ReportParameters["Id_Emp"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = Convert.ToInt32(this.ReportParameters["Id_Cd"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_TerStr"].Value = this.ReportParameters["Id_TerStrDesgloce"].Value.ToString() == string.Empty ? (object)null : this.ReportParameters["Id_TerStrDesgloce"].Value.ToString();
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_CteStr"].Value = this.ReportParameters["Id_CteStrDesgloce"].Value.ToString() == string.Empty ? (object)null : this.ReportParameters["Id_CteStrDesgloce"].Value.ToString();
            this.sqlDataAdapter1.SelectCommand.Parameters["@Cte_Nombre"].Value = this.ReportParameters["NombreCliente"].Value.ToString() == string.Empty ? (object)null : this.ReportParameters["NombreCliente"].Value.ToString();
            this.sqlDataAdapter1.SelectCommand.Parameters["@mesInicial"].Value = Convert.ToInt32(this.ReportParameters["MesInicial"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@anioInicial"].Value = Convert.ToInt32(this.ReportParameters["AnioInicial"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@mesFinal"].Value = Convert.ToInt32(this.ReportParameters["MesFinal"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@anioFinal"].Value = Convert.ToInt32(this.ReportParameters["AnioFinal"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@rbCalculadoCon"].Value = Convert.ToInt32(this.ReportParameters["CalculadoCon"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@utilidadBruta"].Value = Convert.ToSingle(this.ReportParameters["UtilidadBruta"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@Salida"].Value = 2;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_U"].Value = Convert.ToSingle(this.ReportParameters["Id_U"].Value);
            //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
            report.DataSource = this.sqlDataAdapter1;
        }

        private void Rep_VenUtilidadBruta1_Error(object sender, ErrorEventArgs eventArgs)
        {
            //string a = "";
        }

        private void Rep_VenUtilidadBruta1_ItemDataBound(object sender, EventArgs e)
        {
            //string a = "";
        }

        private void Rep_VenUtilidadBruta1_ItemDataBinding(object sender, EventArgs e)
        {
            //string a = "";
        }
    }
}