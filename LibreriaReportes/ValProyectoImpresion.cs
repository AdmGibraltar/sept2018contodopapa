namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Data;
    using CapaEntidad;
    using System.Collections.Generic;
    using CapaNegocios;

    /// <summary>
    /// Summary description for ValProyectoImpresion.
    /// </summary>
    public partial class ValProyectoImpresion : Telerik.Reporting.Report
    {
        public ValProyectoImpresion()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();           
            this.tabla.Columns.Add("Id_Emp", typeof(string));
            this.tabla.Columns.Add("Id_Cd", typeof(string));
            this.tabla.Columns.Add("Id_Vap", typeof(string));

            this.DataSource = null;
        }

        private void ValProyectoImpresion_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                ////Transfer the ReportParameter value to the parameter of the select command
                DataRowCollection rowCol = this.tabla.Rows;
                if (rowCol.Count == 0)
                {
                    DataRow row = this.tabla.NewRow();
                    row["Id_Emp"] = this.ReportParameters["@Id_Emp"].Value;
                    row["Id_Cd"] = this.ReportParameters["@Id_Cd"].Value;
                    row["Id_Vap"] = this.ReportParameters["@Id_Vap"].Value;
                    this.tabla.Rows.Add(row);
                }
                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValProyectoImpresion_ItemDataBound(object sender, EventArgs e)
        {
            
        }

        private void subReport_CF_NeedDataSource(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.ReportItemBase reporteBase = (Telerik.Reporting.Processing.ReportItemBase)sender;
            this.valProyectoImpresion_SubCF1.Id_Emp = ((DataRow)reporteBase.DataObject.RawData).ItemArray[0].ToString();
            this.valProyectoImpresion_SubCF1.Id_Cd = ((DataRow)reporteBase.DataObject.RawData).ItemArray[1].ToString();
            this.valProyectoImpresion_SubCF1.Id_Vap = ((DataRow)reporteBase.DataObject.RawData).ItemArray[2].ToString();
            this.valProyectoImpresion_SubCF1.Conexion = this.ReportParameters["@Conexion"].Value.ToString();
        }

        private void subReport_Prod_NeedDataSource(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.ReportItemBase reporteBase = (Telerik.Reporting.Processing.ReportItemBase)sender;
            this.valProyectoImpresion_SubProd1.Id_Emp = ((DataRow)reporteBase.DataObject.RawData).ItemArray[0].ToString();
            this.valProyectoImpresion_SubProd1.Id_Cd = ((DataRow)reporteBase.DataObject.RawData).ItemArray[1].ToString();
            this.valProyectoImpresion_SubProd1.Id_Vap = ((DataRow)reporteBase.DataObject.RawData).ItemArray[2].ToString();
            this.valProyectoImpresion_SubProd1.Conexion = this.ReportParameters["@Conexion"].Value.ToString();

            TipoMoneda tipoMoneda = new TipoMoneda();
            List<TipoMoneda> lista = new List<TipoMoneda>();
            new CN_CatTipoMoneda().ConsultaTipoMoneda(
                tipoMoneda
                , Convert.ToInt32(this.ReportParameters["@Id_Emp"].Value.ToString())
                , this.ReportParameters["@Conexion"].Value.ToString(), ref lista);
            this.valProyectoImpresion_SubProd1.ListaTipoMoneda = lista;
        }
    }
}