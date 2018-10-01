﻿namespace LibreriaReportes
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
    public partial class ExpRep_InvValorizacionInventarios : Telerik.Reporting.Report
    {
        public ExpRep_InvValorizacionInventarios()
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
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_PrdStr"].Value = this.ReportParameters["Id_PrdStrDesgloce"].Value.ToString() == string.Empty ? (object)null : this.ReportParameters["Id_PrdStrDesgloce"].Value.ToString();
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ptp"].Value = this.ReportParameters["tipoProducto"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Spo"].Value = (object)this.ReportParameters["tipoSisProp"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@TipoPrecioStr"].Value = this.ReportParameters["tipoPrecio"].Value.ToString();
            this.sqlDataAdapter1.SelectCommand.Parameters["@Orden"].Value = this.ReportParameters["Orden"].Value.ToString();

            //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
            report.DataSource = this.sqlDataAdapter1;
        }
    }
}