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
    public partial class RepCobEstDetalleDocumentoSW : Telerik.Reporting.Report
    {
        public RepCobEstDetalleDocumentoSW()
        {
            try
            {
                InitializeComponent();

                //

                //
                this.DataSource = null;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
    
        }

        private void RepCobEstDetalleDocumento_SW_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;

                //this.textBox3.Value = "Resumen de cartera " + rptq.Parameters["NombreCdi"].Value.ToString();
                //this.TxtFiltros.Value = "Fecha de corte: " + Convert.ToDateTime(rptq.Parameters["FechaCorte"].Value.ToString()).ToString("dd/MM/yyy").ToString();

                //if (rptq.Parameters["Plazo"].Value.ToString() == "1")
                //{
                //    this.TxtFiltros.Value = this.TxtFiltros.Value + "  Plazo: Normal  ";
                //}
                //else if (rptq.Parameters["Plazo"].Value.ToString() == "2")
                //{
                //    this.TxtFiltros.Value = this.TxtFiltros.Value + "  Plazo: Especial  ";

                //}
                //else
                //{
                //    this.TxtFiltros.Value = this.TxtFiltros.Value + "  Plazo: Todos  ";
                //}

                //if (rptq.Parameters["Vencimiento"].Value.ToString() == "1")
                //{
                //    this.TxtFiltros.Value = this.TxtFiltros.Value + "Estatus: Sin vencer  ";

                //}
                //else if (rptq.Parameters["Vencimiento"].Value.ToString() == "2")
                //{
                //    this.TxtFiltros.Value = this.TxtFiltros.Value + "Estatus: Vencido  ";
                //}
                //else
                //{
                //    this.TxtFiltros.Value = this.TxtFiltros.Value + "Estatus: Todos  ";
                //}


                //if (rptq.Parameters["TipoCte"].Value.ToString() == "1")
                //{
                //    this.TxtFiltros.Value = this.TxtFiltros.Value + "Tipo cte.: Local  ";

                //}
                //else if (rptq.Parameters["TipoCte"].Value.ToString() == "2")
                //{
                //    this.TxtFiltros.Value = this.TxtFiltros.Value + "Tipo cte.: Nacional  ";
                //}
                //else
                //{
                //    this.TxtFiltros.Value = this.TxtFiltros.Value + "Tipo cte.: Todos  ";
                //}


                //if (rptq.Parameters["Legal"].Value.ToString() == "1")
                //{
                //    this.TxtFiltros.Value = this.TxtFiltros.Value + "Inc. legal: No  ";
                //}
                //else
                //{
                //    this.TxtFiltros.Value = this.TxtFiltros.Value + "Inc. legal: Si ";
                //}

                this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = rptq.Parameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Serie"].Value = rptq.Parameters["Id_DocSerie"].Value;

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