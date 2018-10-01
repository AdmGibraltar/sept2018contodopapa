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
    public partial class Rep_RentabilidadClientes : Telerik.Reporting.Report
    {
        public Rep_RentabilidadClientes()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.tabla.Columns.Add("Id_Emp", typeof(string));
            this.tabla.Columns.Add("Id_Cd", typeof(string));
            this.tabla.Columns.Add("Id_Cte", typeof(string));
            this.tabla.Columns.Add("Id_Ter", typeof(string));
            this.tabla.Columns.Add("periodo", typeof(string));
            this.tabla.Columns.Add("ventas", typeof(string));

            this.DataSource = null;

        }


        private void Rep_RentabilidadClientes_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                ////Transfer the ReportParameter value to the parameter of the select command
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["@Id_Emp"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["@Id_Cd"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Frc"].Value = this.ReportParameters["@Folio"].Value;

                DataRowCollection rowCol = this.tabla.Rows;
                if (rowCol.Count == 0)
                {
                    DataRow row = this.tabla.NewRow();
                    row["Id_Emp"] = this.ReportParameters["@Id_Emp"].Value;
                    row["Id_Cd"] = this.ReportParameters["@Id_Cd"].Value;
                    row["Id_Cte"] = this.ReportParameters["@Id_Cte"].Value;
                    row["Id_Ter"] = this.ReportParameters["@Id_Ter"].Value;
                    row["periodo"] = this.ReportParameters["@periodo"].Value;
                    row["ventas"] = this.ReportParameters["@ventas"].Value;
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


        private void subReport_CF_NeedDataSource(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.ReportItemBase reporteBase = (Telerik.Reporting.Processing.ReportItemBase)sender;
            this.rep_RentabilidadClientes_SubCF1.Id_Emp = ((DataRow)reporteBase.DataObject.RawData).ItemArray[0].ToString();
            this.rep_RentabilidadClientes_SubCF1.Id_Cd = ((DataRow)reporteBase.DataObject.RawData).ItemArray[1].ToString();
            this.rep_RentabilidadClientes_SubCF1.Id_Cte = ((DataRow)reporteBase.DataObject.RawData).ItemArray[2].ToString();
            this.rep_RentabilidadClientes_SubCF1.Id_Ter = ((DataRow)reporteBase.DataObject.RawData).ItemArray[3].ToString();
            this.rep_RentabilidadClientes_SubCF1.periodo = ((DataRow)reporteBase.DataObject.RawData).ItemArray[4].ToString();
            this.rep_RentabilidadClientes_SubCF1.Conexion = this.ReportParameters["@Conexion"].Value.ToString();

            //Obtener lista de productos con amortización del cliente
            Amortizacion amortizacion = new Amortizacion();
            amortizacion.Id_Emp = Convert.ToInt32(this.ReportParameters["@Id_Emp"].Value.ToString());
            amortizacion.Id_Cd = Convert.ToInt32(this.ReportParameters["@Id_Cd"].Value);
            amortizacion.Id_Cte = Convert.ToInt32(this.ReportParameters["@Id_Cte"].Value);
            List<Amortizacion> listAmortizacion = new List<Amortizacion>();
            new CN_Amortizacion().ConsultaAmortizacionCliente(
                amortizacion
                , this.ReportParameters["@Conexion"].Value.ToString()
                , ref listAmortizacion);
            this.rep_RentabilidadClientes_SubCF1.ListaAmortizacion = listAmortizacion;
        }

        private void subReport_Prod_NeedDataSource(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.ReportItemBase reporteBase = (Telerik.Reporting.Processing.ReportItemBase)sender;
            this.rep_RentabilidadClientes_SubProd1.Id_Emp = ((DataRow)reporteBase.DataObject.RawData).ItemArray[0].ToString();
            this.rep_RentabilidadClientes_SubProd1.Id_Cd = ((DataRow)reporteBase.DataObject.RawData).ItemArray[1].ToString();
            this.rep_RentabilidadClientes_SubProd1.Id_Cte = ((DataRow)reporteBase.DataObject.RawData).ItemArray[2].ToString();
            this.rep_RentabilidadClientes_SubProd1.Id_Ter = ((DataRow)reporteBase.DataObject.RawData).ItemArray[3].ToString();
            this.rep_RentabilidadClientes_SubProd1.periodo = ((DataRow)reporteBase.DataObject.RawData).ItemArray[4].ToString();
            this.rep_RentabilidadClientes_SubProd1.ventas = ((DataRow)reporteBase.DataObject.RawData).ItemArray[5].ToString();
            this.rep_RentabilidadClientes_SubProd1.Conexion = this.ReportParameters["@Conexion"].Value.ToString();

            //Obtener lista de tipos de moneda
            TipoMoneda tipoMoneda = new TipoMoneda();
            List<TipoMoneda> lista = new List<TipoMoneda>();
            new CN_CatTipoMoneda().ConsultaTipoMoneda(
                tipoMoneda
                , Convert.ToInt32(this.ReportParameters["@Id_Emp"].Value.ToString())
                , this.ReportParameters["@Conexion"].Value.ToString(), ref lista);
            this.rep_RentabilidadClientes_SubProd1.ListaTipoMoneda = lista;

        }

        
    }
}