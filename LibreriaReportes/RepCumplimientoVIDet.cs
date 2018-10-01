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
    /// Summary description for RepInvKardex.
    /// </summary>
    public partial class RepCumplimientoVIDet : Telerik.Reporting.Report
    {
        public RepCumplimientoVIDet()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void RepCumplimientoVIDet_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;
                this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = rptq.Parameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Anio"].Value = rptq.Parameters["Anio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Mes"].Value = rptq.Parameters["Mes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_UEN"].Value = rptq.Parameters["Id_UEN"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Seg"].Value = rptq.Parameters["Id_Seg"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Rik"].Value = rptq.Parameters["Id_Rik"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ter"].Value = rptq.Parameters["Id_Ter"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cte"].Value = rptq.Parameters["Id_Cte"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tipo"].Value = rptq.Parameters["Tipo"].Value;

                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}