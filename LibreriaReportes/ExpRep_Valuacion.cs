namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Collections;

    /// <summary>
    /// Summary description for Rep_Valuacion.
    /// </summary>
    public partial class ExpRep_Valuacion : Telerik.Reporting.Report
    {
        public ExpRep_Valuacion()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //

            this.DataSource = null;
        }

        private void Rep_Valuacion_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
               

                this.sqlDataAdapter1.SelectCommand.Parameters["@ID_EMP"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@ID_CD"].Value = this.ReportParameters["Id_Cd"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@ID_CTE"].Value = this.ReportParameters["Id_Cte"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@ID_VAP"].Value = this.ReportParameters["Id_Vap"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@VigenciaACYS"].Value = this.ReportParameters["VigenciaAcys"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@PROPORCION_PARTICIPACION_UTILIDAD_RIK"].Value = this.ReportParameters["PROPORCION_PARTICIPACION_UTILIDAD_RIK"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@MANO_OBRA_PROYECTOS"].Value = this.ReportParameters["MANO_OBRA_PROYECTOS"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@AMORT_EQUIPOS_ARRENDADOS"].Value = this.ReportParameters["AMORT_EQUIPOS_ARRENDADOS"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@NUMERO_ENTREGA"].Value = this.ReportParameters["NUMERO_ENTREGA"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@COSTO_ENTREGA"].Value = this.ReportParameters["COSTO_ENTREGA"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@PORCENTAJE_COMISION_FACTORAJE"].Value = this.ReportParameters["PORCENTAJE_COMISION_FACTORAJE"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@PORCENTAJE_COMISION_CRUCE"].Value = this.ReportParameters["PORCENTAJE_COMISION_CRUCE"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@IVA_CD"].Value = this.ReportParameters["IVA_CD"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@PLAZO_PAGO"].Value = this.ReportParameters["PLAZO_PAGO"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@INVENTARIO_KEY"].Value = this.ReportParameters["INVENTARIO_KEY"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@INVENTARIO_KEY_CONSIGNACION"].Value = this.ReportParameters["INVENTARIO_KEY_CONSIGNACION"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@INVENTARIO_PROVEEDOR_PAPEL"].Value = this.ReportParameters["INVENTARIO_PROVEEDOR_PAPEL"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@INVENTARIO_PROVEEDOR_PAPEL_CONSIGNACION"].Value = this.ReportParameters["INVENTARIO_PROVEEDOR_PAPEL_CONSIGNACION"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@CREDITO_PROVEEDOR_KEY"].Value = this.ReportParameters["CREDITO_PROVEEDOR_KEY"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@CREDITO_PROVEEDOR_PAPEL"].Value = this.ReportParameters["CREDITO_PROVEEDOR_PAPEL"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@GASTO_FLETE_ENTREGAS_LOCALES"].Value = this.ReportParameters["GASTO_FLETE_ENTREGAS_LOCALES"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@CONTRIBUCION_COSTOS_FIJOS_NO_PAPEL"].Value = this.ReportParameters["CONTRIBUCION_COSTOS_FIJOS_NO_PAPEL"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@CONTRIBUCION_COSTOS_FIJOS_PAPEL"].Value = this.ReportParameters["CONTRIBUCION_COSTOS_FIJOS_PAPEL"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@GADMITIVOS"].Value = this.ReportParameters["GADMITIVOS"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@UCS"].Value = this.ReportParameters["UCS"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@ISRPTU"].Value = this.ReportParameters["ISRPTU"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@INVERSION_ACTIVOS_FIJOS_DIAS"].Value = this.ReportParameters["INVERSION_ACTIVOS_FIJOS_DIAS"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@CETES"].Value = this.ReportParameters["CETES"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@ADICIONAL_CETES"].Value = this.ReportParameters["ADICIONAL_CETES"].Value;
                 

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