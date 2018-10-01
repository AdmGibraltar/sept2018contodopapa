namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Data;

    /// <summary>
    /// Summary description for Rep_SerImproductivos.
    /// </summary>
    public partial class ExpRep_SerImproductivos : Telerik.Reporting.Report
    {
        public ExpRep_SerImproductivos()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //
            this.DataSource = null;
            //
        }

        private void Rep_SerImproductivos_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cte"].Value = this.ReportParameters["Id_Cte"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Spo"].Value = this.ReportParameters["Id_Spo"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Rik"].Value = this.ReportParameters["Id_Rik"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Detalle"].Value = this.ReportParameters["Por"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TercerMes"].Value = this.ReportParameters["TercerMes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Agrupado"].Value = this.ReportParameters["Agrup"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@DetalladoxProducto"].Value = 1;

                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;
                
                subReport1.ItemDataBound += new EventHandler(subReport1_ItemDataBound);
                if (ReportParameters["Estadistica"].Value.ToString().ToLower() == "true")
                {
                    subReport1.NeedDataSource += new EventHandler(subReport1_NeedDataSource);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void subReport1_NeedDataSource(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.ReportItemBase reporteBase = (Telerik.Reporting.Processing.ReportItemBase)sender;
            this.expSubImproductividad1.Id_Emp = this.ReportParameters["Id_Emp"].Value.ToString();
            this.expSubImproductividad1.Id_Cd = this.ReportParameters["Id_Cd"].Value.ToString();
            this.expSubImproductividad1.Id_Spo = ((DataRow)reporteBase.DataObject.RawData).ItemArray[8].ToString();
            this.expSubImproductividad1.Id_Cte = ((DataRow)reporteBase.DataObject.RawData).ItemArray[4].ToString();
            this.expSubImproductividad1.Id_Ter = ((DataRow)reporteBase.DataObject.RawData).ItemArray[0].ToString();

            this.expSubImproductividad1.Conexion = this.ReportParameters["Conexion"].Value.ToString();
        }

        private void subReport1_ItemDataBound(object sender, EventArgs e)
        {
            bool vis = true;
            Telerik.Reporting.Processing.SubReport subReport = (Telerik.Reporting.Processing.SubReport)sender;
            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)subReport.InnerReport;
            if (ReportParameters["Estadistica"].Value.ToString().ToLower() == "false")
            {
                vis = false;
            }
            else
            {
                vis = report.ChildElements.Find("detail", true).Length > 0;
            }

            subReport.Visible = vis;
            Telerik.Reporting.DetailSection detail = new DetailSection();
            detail.Visible = subReport.Visible;
        }
    }
}