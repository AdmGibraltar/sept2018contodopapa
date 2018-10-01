namespace LibreriaReportes
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
    /// Summary description for RemisionContratoComodato.
    /// </summary>
    public partial class RemisionContratoComodatoActualizacion : Telerik.Reporting.Report
    {
        public RemisionContratoComodatoActualizacion()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void RemisionContratoComodato_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cco"].Value = this.ReportParameters["Id_Cco"].Value.ToString();

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;


                //// ---------------------------------------------------------------------------------------------
                //// Si se asigno correctamente el origen de datos, se actualiza el estatus 
                //// ---------------------------------------------------------------------------------------------
                ////actualiza estatus de remisiones a Impreso (I)
                //int verificador = 0;
                //new CN_CapRemision().ModificarRemisiones_Estatus(
                //    int.Parse(this.ReportParameters["Id_Emp"].Value.ToString())
                //    , int.Parse(this.ReportParameters["Id_Cd"].Value.ToString())
                //    , this.ReportParameters["Id_RemStr"].Value.ToString()
                //    , "I"
                //    , this.ReportParameters["Conexion"].Value.ToString()
                //    , ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}