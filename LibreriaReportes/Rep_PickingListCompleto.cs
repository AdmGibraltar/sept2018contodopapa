using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LibreriaReportes
{
    public partial class Rep_PickingListCompleto : Telerik.Reporting.Report
    {
        public Rep_PickingListCompleto()
        {
            InitializeComponent();
        }

        public Rep_PickingListCompleto(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        
        private void Rep_PickingListCompleto_NeedDataSource(object sender, EventArgs e)
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
            //report.DataSource = this.sqlDataAdapter1;

            Telerik.Reporting.Processing.ReportItemBase reporteBase = (Telerik.Reporting.Processing.ReportItemBase)sender;
            subRep_PickingListConcentrado.ReportParameters[2] = this.ReportParameters["Conexion"];
            subRep_PickingListConcentrado.ReportParameters[0] = this.ReportParameters["Id_Emp"];
            subRep_PickingListConcentrado.ReportParameters[1] = this.ReportParameters["Id_Cd_Ver"];
            subRep_PickingListConcentrado.ReportParameters[3] = this.ReportParameters["FechaInicial"];
            subRep_PickingListConcentrado.ReportParameters[4] = this.ReportParameters["FechaFinal"];
            subRep_PickingListConcentrado.ReportParameters[5] = this.ReportParameters["PedidoInicial"];
            subRep_PickingListConcentrado.ReportParameters[6] = this.ReportParameters["PedidoFinal"];
            subRep_PickingListConcentrado.ReportParameters[7] = this.ReportParameters["ClienteInicial"];
            subRep_PickingListConcentrado.ReportParameters[8] = this.ReportParameters["ClienteFinal"];
            subRep_PickingListConcentrado.ReportParameters[9] = this.ReportParameters["SectorInicial"];
            subRep_PickingListConcentrado.ReportParameters[10] = this.ReportParameters["SectorFinal"];
            subRep_PickingListConcentrado.ReportParameters[11] = this.ReportParameters["RutaInicial"];
            subRep_PickingListConcentrado.ReportParameters[12] = this.ReportParameters["RutaFinal"];
            subRep_PickingListConcentrado.ReportParameters[13] = this.ReportParameters["Ids"];

            Telerik.Reporting.Processing.ReportItemBase reporteBase2 = (Telerik.Reporting.Processing.ReportItemBase)sender;
            subRep_PickingListCliente.ReportParameters[2] = this.ReportParameters["Conexion"];
            subRep_PickingListCliente.ReportParameters[0] = this.ReportParameters["Id_Emp"];
            subRep_PickingListCliente.ReportParameters[1] = this.ReportParameters["Id_Cd_Ver"];
            subRep_PickingListCliente.ReportParameters[3] = this.ReportParameters["FechaInicial"];
            subRep_PickingListCliente.ReportParameters[4] = this.ReportParameters["FechaFinal"];
            subRep_PickingListCliente.ReportParameters[5] = this.ReportParameters["PedidoInicial"];
            subRep_PickingListCliente.ReportParameters[6] = this.ReportParameters["PedidoFinal"];
            subRep_PickingListCliente.ReportParameters[7] = this.ReportParameters["ClienteInicial"];
            subRep_PickingListCliente.ReportParameters[8] = this.ReportParameters["ClienteFinal"];
            subRep_PickingListCliente.ReportParameters[9] = this.ReportParameters["SectorInicial"];
            subRep_PickingListCliente.ReportParameters[10] = this.ReportParameters["SectorFinal"];
            subRep_PickingListCliente.ReportParameters[11] = this.ReportParameters["RutaInicial"];
            subRep_PickingListCliente.ReportParameters[12] = this.ReportParameters["RutaFinal"];
            subRep_PickingListCliente.ReportParameters[13] = this.ReportParameters["Ids"];
        }

        private void subReport1_NeedDataSource(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.ReportItemBase reporteBase = (Telerik.Reporting.Processing.ReportItemBase)sender;
            subRep_PickingListConcentrado.ReportParameters[2] = this.ReportParameters["Conexion"];
            subRep_PickingListConcentrado.ReportParameters[0] = this.ReportParameters["Id_Emp"];
            subRep_PickingListConcentrado.ReportParameters[1] = this.ReportParameters["Id_Cd_Ver"];
            subRep_PickingListConcentrado.ReportParameters[3] = this.ReportParameters["FechaInicial"];
            subRep_PickingListConcentrado.ReportParameters[4] = this.ReportParameters["FechaFinal"];
            subRep_PickingListConcentrado.ReportParameters[5] = this.ReportParameters["PedidoInicial"];
            subRep_PickingListConcentrado.ReportParameters[6] = this.ReportParameters["PedidoFinal"];
            subRep_PickingListConcentrado.ReportParameters[7] = this.ReportParameters["ClienteInicial"];
            subRep_PickingListConcentrado.ReportParameters[8] = this.ReportParameters["ClienteFinal"];
            subRep_PickingListConcentrado.ReportParameters[9] = this.ReportParameters["SectorInicial"];
            subRep_PickingListConcentrado.ReportParameters[10] = this.ReportParameters["SectorFinal"];
            subRep_PickingListConcentrado.ReportParameters[11] = this.ReportParameters["RutaInicial"];
            subRep_PickingListConcentrado.ReportParameters[12] = this.ReportParameters["RutaFinal"];
            subRep_PickingListConcentrado.ReportParameters[13] = this.ReportParameters["Ids"];
        }

    }
}
