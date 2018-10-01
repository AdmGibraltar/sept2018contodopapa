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

    /// <summary>
    /// Summary description for ValProyectoImpresion_SubCF.
    /// </summary>
    public partial class ExpRep_RentabilidadClientes_SubCF : Telerik.Reporting.Report
    {
        public ExpRep_RentabilidadClientes_SubCF()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        #region Propiedades

        private List<Amortizacion> _listaAmortizacion;
        public List<Amortizacion> ListaAmortizacion
        {
            get { return _listaAmortizacion; }
            set { _listaAmortizacion = value; }
        }

        public string Id_Emp
        {
            get { return this.ReportParameters["@Id_Emp"].Value.ToString(); }
            set { this.ReportParameters["@Id_Emp"].Value = value; }
        }

        public string Id_Cd
        {
            get { return this.ReportParameters["@Id_Cd"].Value.ToString(); }
            set { this.ReportParameters["@Id_Cd"].Value = value; }
        }

        public string Id_Cte
        {
            get { return this.ReportParameters["@Id_Cte"].Value.ToString(); }
            set { this.ReportParameters["@Id_Cte"].Value = value; }
        }

        public string Id_Ter
        {
            get { return this.ReportParameters["@Id_Ter"].Value.ToString(); }
            set { this.ReportParameters["@Id_Ter"].Value = value; }
        }

        public string periodo
        {
            get { return this.ReportParameters["@periodo"].Value.ToString(); }
            set { this.ReportParameters["@periodo"].Value = value; }
        }

        //public string ventas
        //{
        //    get { return this.ReportParameters["@ventas"].Value.ToString(); }
        //    set { this.ReportParameters["@ventas"].Value = value; }
        //}


        public string Conexion
        {
            get { return this.ReportParameters["@Conexion"].Value.ToString(); }
            set { this.ReportParameters["@Conexion"].Value = value; }
        }

        #endregion

        private void ValProyectoImpresion_SubCF_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["@Conexion"].Value.ToString();

                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.Id_Emp;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd_Ver"].Value = this.Id_Cd;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cte"].Value = this.Id_Cte;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ter"].Value = this.Id_Ter == "-1" ? null : this.Id_Ter;
                this.sqlDataAdapter1.SelectCommand.Parameters["@periodo"].Value = this.periodo;                

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                //report.DataSource = this.sqlDataAdapter1;

                DataTable tabla = new DataTable();
                this.sqlDataAdapter1.Fill(tabla);
                this.CalcularAmortizacion(ref tabla);

                report.DataSource = tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void CalcularAmortizacion(ref DataTable tabla)
        {
            foreach (DataRow row in tabla.Rows)
            {
                int Id_Prd = Convert.ToInt32(row[4]);

                //Calcular AMORTIZACION del producto
                int anioActual = DateTime.Now.Year;
                int mesActual = DateTime.Now.Month;
                foreach (Amortizacion amor in this.ListaAmortizacion)
                {
                    if (Id_Prd == amor.Id_Prd)
                    {
                        //si el año y mes actual es mayor al año y mes de la amortizacion del producto
                        //la amortizacion se queda en 0
                        DateTime fechaFinAmortizacion = new DateTime(amor.Amo_AnioFin, amor.Amo_MesFin, 1);
                        DateTime fechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        int mesesAmortizacion = 0;
                        if (((TimeSpan)(fechaFinAmortizacion.Subtract(fechaActual))).Ticks > 0)
                        {
                            //calcula meses de amortizacion
                            //al final al mes actual se le resta 1 porque aun no se acaba el mes actual
                            mesesAmortizacion = (((anioActual - amor.Amo_AnioInicio) * 12) - amor.Amo_MesInicio) + (mesActual - 1);
                        }
                        float importeTotalAmortizacion = amor.Amo_Cant * amor.Amo_Costo;
                        double montoAmortizacion = importeTotalAmortizacion / mesesAmortizacion;

                        row[10] = montoAmortizacion;
                        break;
                    }
                }
            }
        }
    }
}