﻿namespace LibreriaReportes
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
    /// Summary description for EntSalImprimir.
    /// </summary>
    public partial class EntSalImprimir : Telerik.Reporting.Report
    {
        public EntSalImprimir()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void EntSalImprimir_NeedDataSource(object sender, EventArgs e)
        {
            try
            {                
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Es"].Value = this.ReportParameters["Id_Es"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@EsDet_Naturaleza"].Value = this.ReportParameters["EsDet_Naturaleza"].Value;
                
                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

                // ---------------------------------------------------------------------------------------------
                // Si se asigno correctamente el origen de datos, se actualiza el estatus de la orden de compra
                // ---------------------------------------------------------------------------------------------
                //actualiza estatus de entrada salida a Impreso (I)
                int verificador = 0;
                EntradaSalida entSal = new EntradaSalida();

                entSal.Id_Emp = int.Parse(this.ReportParameters["Id_Emp"].Value.ToString());
                entSal.Id_Cd = int.Parse(this.ReportParameters["Id_Cd"].Value.ToString());
                //this.sqlDastaAdapter1.SelectCommand.Parameters["@Id_Es"].Value = this.ReportParameters["@Id_Es"].Value; <==borrame
                entSal.Id_Es = int.Parse(this.ReportParameters["Id_Es"].Value.ToString());
                entSal.Es_Naturaleza = int.Parse(this.ReportParameters["EsDet_Naturaleza"].Value.ToString());
                entSal.Es_Estatus = "i";
                new CN_CapEntradaSalida().ModificarEntradaSalida_Estatus(entSal,this.ReportParameters["Conexion"].Value.ToString(), ref verificador);                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}