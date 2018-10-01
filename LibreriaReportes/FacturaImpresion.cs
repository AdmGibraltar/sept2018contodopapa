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
    using System.Xml;
    using System.Data;
    using System.Web;

    /// <summary>
    /// Summary description for FacturaImpresion.
    /// </summary>
    public partial class FacturaImpresion : Telerik.Reporting.Report
    {
        public FacturaImpresion()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void FacturaImpresion_NeedDataSource(object sender, EventArgs e)
        {
            this.CrearDataSource();
            //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
            report.DataSource = this.source;
        }

        private void CrearDataSource()
        {
            try
            {
                //this.sqlConnection2.ConnectionString = this.ReportParameters["@Conexion"].Value.ToString();

                ////Transfer the ReportParameter value to the parameter of the select command
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["@Id_Emp"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["@Id_Cd"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ord"].Value = this.ReportParameters["@Id_Ord"].Value;

                // --------------------------------------------
                // Generar source a partir del XML de factura
                // --------------------------------------------
                if (this.source.Columns.Count == 0)
                {
                    this.source.Columns.Add("Id_Prd", typeof(string));
                    this.source.Columns.Add("Prd_Descripcion", typeof(string));
                    this.source.Columns.Add("Prd_Unidad", typeof(string));
                    this.source.Columns.Add("Prd_Cantidad", typeof(string));
                    this.source.Columns.Add("Prd_PrecioUnitario", typeof(string));
                    this.source.Columns.Add("Prd_Importe", typeof(string));
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(this.ReportParameters["@FacturaXML"].Value.ToString());
                //XmlNode nodoPage = doc.SelectSingleNode("//Page");
                //string height = nodoPage.Attributes["height"].Value;
                //string width = nodoPage.Attributes["width"].Value;
                this.source.Rows.Clear();
                XmlNodeList nodosProductos = doc.SelectNodes("//Concepto");
                foreach (XmlNode producto in nodosProductos)
                {
                    //XmlNode producto = xn.SelectSingleNode("Concepto");
                    if (producto.Attributes.Count > 0)
                    {
                        DataRow row = this.source.NewRow();
                        row["Id_Prd"] = producto.Attributes["noIdentificacion"].Value;
                        row["Prd_Descripcion"] = producto.Attributes["descripcion"].Value;
                        row["Prd_Unidad"] = "LT"; // producto.Attributes["cantidad"].Value;
                        row["Prd_Cantidad"] = producto.Attributes["cantidad"].Value;
                        row["Prd_PrecioUnitario"] = producto.Attributes["valorUnitario"].Value;
                        row["Prd_Importe"] = producto.Attributes["importe"].Value;
                        this.source.Rows.Add(row);
                    }
                }
                
                // ---------------------------------------------------------------------------------------------
                // Si se asigno correctamente el origen de datos, se actualiza el estatus de la factura
                // ---------------------------------------------------------------------------------------------
                //actualiza estatus de factura a Impreso (I)
                int verificador = 0;

                Factura factura = new Factura();
                factura.Id_Emp = Convert.ToInt32(this.ReportParameters["Id_Emp"].Value);
                factura.Id_Cd = Convert.ToInt32(this.ReportParameters["Id_Cd"].Value);
                factura.Id_Fac = Convert.ToInt32(this.ReportParameters["Id_Fac"].Value);
                factura.Fac_Estatus = "I";
                new CN_CapFactura().ModificarFactura_Estatus(factura, this.ReportParameters["Conexion"].Value.ToString(), ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}