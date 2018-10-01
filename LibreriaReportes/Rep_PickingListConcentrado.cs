using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LibreriaReportes
{
    public partial class Rep_PickingListConcentrado : Telerik.Reporting.Report
    {
        public Rep_PickingListConcentrado()
        {
            InitializeComponent();
        }

        public Rep_PickingListConcentrado(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void Rep_PickingListConcentrado_NeedDataSource(object sender, EventArgs e)
        {
            this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
            //Transfer the ReportParameter value to the parameter of the select command
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd_Ver"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Filtro_FecIni"].Value = this.ReportParameters["FechaInicial"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Filtro_FecFin"].Value = this.ReportParameters["FechaFinal"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Filtro_PedIni"].Value = this.ReportParameters["PedidoInicial"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Filtro_PedFin"].Value = this.ReportParameters["PedidoFinal"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Filtro_CteIni"].Value = this.ReportParameters["ClienteInicial"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Filtro_CteFin"].Value = this.ReportParameters["ClienteFinal"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Filtro_SectorIni"].Value = this.ReportParameters["SectorInicial"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Filtro_SectorFin"].Value = this.ReportParameters["SectorFinal"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Filtro_RutaIni"].Value = this.ReportParameters["RutaInicial"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Filtro_RutaFin"].Value = this.ReportParameters["RutaFinal"].Value;
            this.sqlDataAdapter1.SelectCommand.Parameters["@Filtro_Ids"].Value = this.ReportParameters["Ids"].Value;

            //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
            report.DataSource = this.sqlDataAdapter1;
        }
    }
}
