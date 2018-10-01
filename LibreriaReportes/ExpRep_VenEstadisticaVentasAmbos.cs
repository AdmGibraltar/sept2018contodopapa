namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_VenEstadisticaVentas.
    /// </summary>
    public partial class ExpRep_VenEstadisticaVentasAmbos: Telerik.Reporting.Report
    {
        public ExpRep_VenEstadisticaVentasAmbos()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //
            
            //
            this.DataSource = null;
        }

        private void Rep_VenEstadisticaVentas_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                int valor = Convert.ToInt32(this.ReportParameters["Reporte"].Value);
                ocultarcampos(valor);
                ocultarcolumnas(this.ReportParameters["Mes"].Value.ToString());
                this.sqlConnection2.ConnectionString = this.ReportParameters["Emp_Cnx"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command             
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Anio"].Value = this.ReportParameters["Anio"].Value;               
                this.sqlDataAdapter1.SelectCommand.Parameters["@Territorio"].Value = this.ReportParameters["Territorio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Cliente"].Value = this.ReportParameters["Cliente"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Producto"].Value = this.ReportParameters["Producto"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tipo"].Value = this.ReportParameters["Mostrar"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@NivelCliente"].Value = this.ReportParameters["Nivel"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@NivelProducto"].Value = this.ReportParameters["Nivel2"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Reporte"].Value = this.ReportParameters["Reporte"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_U"].Value = this.ReportParameters["Id_U"].Value;
            
                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

                string mes = "Todos";
                switch (this.ReportParameters["Mes"].Value.ToString())
                {
                    case "1": mes = "Enero"; break;
                    case "2": mes = "Febrero"; break;
                    case "3": mes = "Marzo"; break;
                    case "4": mes = "Abril"; break;
                    case "5": mes = "Mayo"; break;
                    case "6": mes = "Junio"; break;
                    case "7": mes = "Julio"; break;
                    case "8": mes = "Agosto"; break;
                    case "9": mes = "Septiembre"; break;
                    case "10": mes = "Octubre"; break;
                    case "11": mes = "Noviembre"; break;
                    case "12": mes = "Diciembre"; break;
                }
                textBox204.Value = mes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ocultarcolumnas(string mes)
        {
            //ENCABEZADOS
            textBox19.Visible = mes == "0" || mes == "1";
            textBox177.Visible = mes == "0" || mes == "1";
            textBox178.Visible = mes == "0" || mes == "1";
            textBox21.Visible = mes == "0" || mes == "2";
            textBox180.Visible = mes == "0" || mes == "2";
            textBox179.Visible = mes == "0" || mes == "2";
            textBox12.Visible = mes == "0" || mes == "3";
            textBox184.Visible = mes == "0" || mes == "3";
            textBox183.Visible = mes == "0" || mes == "3";
            textBox18.Visible = mes == "0" || mes == "4";
            textBox181.Visible = mes == "0" || mes == "4";
            textBox182.Visible = mes == "0" || mes == "4";
            textBox14.Visible = mes == "0" || mes == "5";
            textBox192.Visible = mes == "0" || mes == "5";
            textBox191.Visible = mes == "0" || mes == "5";
            textBox13.Visible = mes == "0" || mes == "6";
            textBox189.Visible = mes == "0" || mes == "6";
            textBox190.Visible = mes == "0" || mes == "6";
            textBox22.Visible = mes == "0" || mes == "7";
            textBox185.Visible = mes == "0" || mes == "7";
            textBox186.Visible = mes == "0" || mes == "7";
            textBox1.Visible = mes == "0" || mes == "8";
            textBox188.Visible = mes == "0" || mes == "8";
            textBox187.Visible = mes == "0" || mes == "8";
            textBox4.Visible = mes == "0" || mes == "9";
            textBox193.Visible = mes == "0" || mes == "9";
            textBox194.Visible = mes == "0" || mes == "9";
            textBox16.Visible = mes == "0" || mes == "10";
            textBox196.Visible = mes == "0" || mes == "10";
            textBox195.Visible = mes == "0" || mes == "10";
            textBox41.Visible = mes == "0" || mes == "11";
            textBox200.Visible = mes == "0" || mes == "11";
            textBox199.Visible = mes == "0" || mes == "11";
            textBox76.Visible = mes == "0" || mes == "12";
            textBox197.Visible = mes == "0" || mes == "12";
            textBox198.Visible = mes == "0" || mes == "12";
            textBox7.Visible = mes == "0";
            textBox202.Visible = mes == "0";
            textBox201.Visible = mes == "0";
            //DETALLES
            textBox2.Visible = mes == "0" || mes == "1";
            textBox176.Visible = mes == "0" || mes == "1";
            textBox24.Visible = mes == "0" || mes == "2";
            textBox175.Visible = mes == "0" || mes == "2";
            textBox25.Visible = mes == "0" || mes == "3";
            textBox174.Visible = mes == "0" || mes == "3";
            textBox36.Visible = mes == "0" || mes == "4";
            textBox173.Visible = mes == "0" || mes == "4";
            textBox39.Visible = mes == "0" || mes == "5";
            textBox172.Visible = mes == "0" || mes == "5";
            textBox42.Visible = mes == "0" || mes == "6";
            textBox171.Visible = mes == "0" || mes == "6";
            textBox44.Visible = mes == "0" || mes == "7";
            textBox170.Visible = mes == "0" || mes == "7";
            textBox5.Visible = mes == "0" || mes == "8";
            textBox169.Visible = mes == "0" || mes == "8";
            textBox6.Visible = mes == "0" || mes == "9";
            textBox168.Visible = mes == "0" || mes == "9";
            textBox77.Visible = mes == "0" || mes == "10";
            textBox167.Visible = mes == "0" || mes == "10";
            textBox78.Visible = mes == "0" || mes == "11";
            textBox166.Visible = mes == "0" || mes == "11";
            textBox79.Visible = mes == "0" || mes == "12";
            textBox165.Visible = mes == "0" || mes == "12";
            textBox8.Visible = mes == "0";
            textBox164.Visible = mes == "0";

            //Total Producto
            textBox61.Visible = mes == "0" || mes == "1";
            textBox152.Visible = mes == "0" || mes == "1";
            textBox60.Visible = mes == "0" || mes == "2";
            textBox154.Visible = mes == "0" || mes == "2";
            textBox59.Visible = mes == "0" || mes == "3";
            textBox163.Visible = mes == "0" || mes == "3";
            textBox57.Visible = mes == "0" || mes == "4";
            textBox155.Visible = mes == "0" || mes == "4";
            textBox55.Visible = mes == "0" || mes == "5";
            textBox156.Visible = mes == "0" || mes == "5";
            textBox49.Visible = mes == "0" || mes == "6";
            textBox157.Visible = mes == "0" || mes == "6";
            textBox48.Visible = mes == "0" || mes == "7";
            textBox158.Visible = mes == "0" || mes == "7";
            textBox37.Visible = mes == "0" || mes == "8";
            textBox159.Visible = mes == "0" || mes == "8";
            textBox35.Visible = mes == "0" || mes == "9";
            textBox160.Visible = mes == "0" || mes == "9";
            textBox23.Visible = mes == "0" || mes == "10";
            textBox161.Visible = mes == "0" || mes == "10";
            textBox20.Visible = mes == "0" || mes == "11";
            textBox162.Visible = mes == "0" || mes == "11";
            textBox17.Visible = mes == "0" || mes == "12";
            textBox153.Visible = mes == "0" || mes == "12";
            textBox45.Visible = mes == "0";
            textBox151.Visible = mes == "0";

            //Total cliente
            textBox62.Visible = mes == "0" || mes == "1";
            textBox150.Visible = mes == "0" || mes == "1";
            textBox85.Visible = mes == "0" || mes == "2";
            textBox148.Visible = mes == "0" || mes == "2";
            textBox94.Visible = mes == "0" || mes == "3";
            textBox139.Visible = mes == "0" || mes == "3";
            textBox86.Visible = mes == "0" || mes == "4";
            textBox147.Visible = mes == "0" || mes == "4";
            textBox87.Visible = mes == "0" || mes == "5";
            textBox146.Visible = mes == "0" || mes == "5";
            textBox88.Visible = mes == "0" || mes == "6";
            textBox145.Visible = mes == "0" || mes == "6";
            textBox89.Visible = mes == "0" || mes == "7";
            textBox144.Visible = mes == "0" || mes == "7";
            textBox90.Visible = mes == "0" || mes == "8";
            textBox143.Visible = mes == "0" || mes == "8";
            textBox91.Visible = mes == "0" || mes == "9";
            textBox142.Visible = mes == "0" || mes == "9";
            textBox92.Visible = mes == "0" || mes == "10";
            textBox141.Visible = mes == "0" || mes == "10";
            textBox93.Visible = mes == "0" || mes == "11";
            textBox140.Visible = mes == "0" || mes == "11";
            textBox63.Visible = mes == "0" || mes == "12";
            textBox149.Visible = mes == "0" || mes == "12";
            textBox46.Visible = mes == "0";
            textBox138.Visible = mes == "0";

            //Total territorio
            textBox95.Visible = mes == "0" || mes == "1";
            textBox137.Visible = mes == "0" || mes == "1";
            textBox97.Visible = mes == "0" || mes == "2";
            textBox136.Visible = mes == "0" || mes == "2";
            textBox106.Visible = mes == "0" || mes == "3";
            textBox127.Visible = mes == "0" || mes == "3";
            textBox98.Visible = mes == "0" || mes == "4";
            textBox135.Visible = mes == "0" || mes == "4";
            textBox99.Visible = mes == "0" || mes == "5";
            textBox134.Visible = mes == "0" || mes == "5";
            textBox100.Visible = mes == "0" || mes == "6";
            textBox133.Visible = mes == "0" || mes == "6";
            textBox101.Visible = mes == "0" || mes == "7";
            textBox132.Visible = mes == "0" || mes == "7";
            textBox102.Visible = mes == "0" || mes == "8";
            textBox131.Visible = mes == "0" || mes == "8";
            textBox103.Visible = mes == "0" || mes == "9";
            textBox130.Visible = mes == "0" || mes == "9";
            textBox104.Visible = mes == "0" || mes == "10";
            textBox129.Visible = mes == "0" || mes == "10";
            textBox105.Visible = mes == "0" || mes == "11";
            textBox128.Visible = mes == "0" || mes == "11";
            textBox96.Visible = mes == "0" || mes == "12";
            textBox125.Visible = mes == "0" || mes == "12";
            textBox47.Visible = mes == "0";
            textBox126.Visible = mes == "0";
            //Total general
            textBox107.Visible = mes == "0" || mes == "1";
            textBox124.Visible = mes == "0" || mes == "1";
            textBox109.Visible = mes == "0" || mes == "2";
            textBox122.Visible = mes == "0" || mes == "2";
            textBox118.Visible = mes == "0" || mes == "3";
            textBox52.Visible = mes == "0" || mes == "3";
            textBox110.Visible = mes == "0" || mes == "4";
            textBox121.Visible = mes == "0" || mes == "4";
            textBox111.Visible = mes == "0" || mes == "5";
            textBox120.Visible = mes == "0" || mes == "5";
            textBox112.Visible = mes == "0" || mes == "6";
            textBox119.Visible = mes == "0" || mes == "6";
            textBox113.Visible = mes == "0" || mes == "7";
            textBox84.Visible = mes == "0" || mes == "7";
            textBox114.Visible = mes == "0" || mes == "8";
            textBox83.Visible = mes == "0" || mes == "8";
            textBox115.Visible = mes == "0" || mes == "9";
            textBox82.Visible = mes == "0" || mes == "9";
            textBox116.Visible = mes == "0" || mes == "10";
            textBox64.Visible = mes == "0" || mes == "10";
            textBox117.Visible = mes == "0" || mes == "11";
            textBox54.Visible = mes == "0" || mes == "11";
            textBox108.Visible = mes == "0" || mes == "12";
            textBox123.Visible = mes == "0" || mes == "12";
            textBox50.Visible = mes == "0";
            textBox51.Visible = mes == "0";


            if (mes != "0")
            {
                //double width_extra = 1.5;
                double Uleft_extra = 10.11;
                double Pleft_extra = 12.05;
                //textBox56.Width = textBox56.Width + Unit.Cm(width_extra);
                //textBox3.Width = textBox3.Width + Unit.Cm(width_extra);

                //ENCABEZADOS
                textBox19.Left = mes == "1" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox177.Left = mes == "1" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox178.Left = mes == "1" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);

                textBox21.Left = mes == "2" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox180.Left = mes == "2" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox179.Left = mes == "2" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);

                textBox12.Left = mes == "3" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox184.Left = mes == "3" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox183.Left = mes == "3" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);

                textBox18.Left = mes == "4" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox181.Left = mes == "4" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox182.Left = mes == "4" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);

                textBox14.Left = mes == "5" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox192.Left = mes == "5" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox191.Left = mes == "5" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);

                textBox13.Left = mes == "6" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox189.Left = mes == "6" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox190.Left = mes == "6" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);

                textBox22.Left = mes == "7" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox185.Left = mes == "7" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox186.Left = mes == "7" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);

                textBox1.Left = mes == "8" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox188.Left = mes == "8" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox187.Left = mes == "8" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);

                textBox4.Left = mes == "9" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox193.Left = mes == "9" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox194.Left = mes == "9" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);

                textBox16.Left = mes == "10" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox196.Left = mes == "10" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox195.Left = mes == "10" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);

                textBox41.Left = mes == "11" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox200.Left = mes == "11" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox199.Left = mes == "11" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);

                textBox76.Left = mes == "12" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox197.Left = mes == "12" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox198.Left = mes == "12" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);

                ////DETALLES
                textBox2.Left = mes == "1" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox176.Left = mes == "1" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox24.Left = mes == "2" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox175.Left = mes == "2" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox25.Left = mes == "3" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox174.Left = mes == "3" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox36.Left = mes == "4" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox173.Left = mes == "4" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox39.Left = mes == "5" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox172.Left = mes == "5" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox42.Left = mes == "6" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox171.Left = mes == "6" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox44.Left = mes == "7" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox170.Left = mes == "7" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox5.Left = mes == "8" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox169.Left = mes == "8" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox6.Left = mes == "9" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox168.Left = mes == "9" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox77.Left = mes == "10" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox167.Left = mes == "10" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox78.Left = mes == "11" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox166.Left = mes == "11" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox79.Left = mes == "12" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox165.Left = mes == "12" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);


                ////Total Producto
                textBox61.Left = mes == "1" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox152.Left = mes == "1" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox60.Left = mes == "2" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox154.Left = mes == "2" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox59.Left = mes == "3" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox163.Left = mes == "3" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox57.Left = mes == "4" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox155.Left = mes == "4" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox55.Left = mes == "5" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox156.Left = mes == "5" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox49.Left = mes == "6" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox157.Left = mes == "6" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox48.Left = mes == "7" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox158.Left = mes == "7" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox37.Left = mes == "8" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox159.Left = mes == "8" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox35.Left = mes == "9" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox160.Left = mes == "9" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox23.Left = mes == "10" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox161.Left = mes == "10" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox20.Left = mes == "11" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox162.Left = mes == "11" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox17.Left = mes == "12" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox153.Left = mes == "12" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);


                ////Total cliente
                textBox62.Left = mes == "1" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox150.Left = mes == "1" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox85.Left = mes == "2" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox148.Left = mes == "2" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox94.Left = mes == "3" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox139.Left = mes == "3" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox86.Left = mes == "4" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox147.Left = mes == "4" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox87.Left = mes == "5" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox146.Left = mes == "5" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox88.Left = mes == "6" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox145.Left = mes == "6" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox89.Left = mes == "7" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox144.Left = mes == "7" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox90.Left = mes == "8" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox143.Left = mes == "8" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox91.Left = mes == "9" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox142.Left = mes == "9" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox92.Left = mes == "10" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox141.Left = mes == "10" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox93.Left = mes == "11" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox140.Left = mes == "11" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox63.Left = mes == "12" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox149.Left = mes == "12" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);


                ////Total territorio
                textBox95.Left = mes == "1" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox137.Left = mes == "1" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox97.Left = mes == "2" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox136.Left = mes == "2" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox106.Left = mes == "3" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox127.Left = mes == "3" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox98.Left = mes == "4" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox135.Left = mes == "4" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox99.Left = mes == "5" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox134.Left = mes == "5" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox100.Left = mes == "6" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox133.Left = mes == "6" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox101.Left = mes == "7" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox132.Left = mes == "7" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox102.Left = mes == "8" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox131.Left = mes == "8" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox103.Left = mes == "9" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox130.Left = mes == "9" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox104.Left = mes == "10" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox129.Left = mes == "10" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox105.Left = mes == "11" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox128.Left = mes == "11" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox96.Left = mes == "12" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox125.Left = mes == "12" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);


                ////Total general
                textBox107.Left = mes == "1" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox124.Left = mes == "1" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox109.Left = mes == "2" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox122.Left = mes == "2" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox118.Left = mes == "3" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox52.Left = mes == "3" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox110.Left = mes == "4" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox121.Left = mes == "4" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox111.Left = mes == "5" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox120.Left = mes == "5" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox112.Left = mes == "6" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox119.Left = mes == "6" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox113.Left = mes == "7" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox84.Left = mes == "7" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox114.Left = mes == "8" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox83.Left = mes == "8" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox115.Left = mes == "9" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox82.Left = mes == "9" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox116.Left = mes == "10" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox64.Left = mes == "10" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox117.Left = mes == "11" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox54.Left = mes == "11" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);
                textBox108.Left = mes == "12" ? Unit.Cm(Uleft_extra) : Unit.Cm(0);
                textBox123.Left = mes == "12" ? Unit.Cm(Pleft_extra) : Unit.Cm(0);

            }
        }
        private void ocultarcampos(int valor)
        {
            switch (valor)
            {//tipo de reporte
                case 15:
                    territorio();
                    cliente();
                    producto();
                    break;
                 
                case 16:
                    cliente();
                    producto();
                    break;
                
                case 17:
                    cliente();
                    producto();
                    break;
               
                case 18:
                    producto();
                    break;
                case 19:
                    territorio();
                    cliente();
                    producto();
                    break;
                case 20:
                    territorio();
                    producto();
                    break;
                case 21:
                    territorio();
                    cliente();
                    producto();
                    break;
                
            }
        }
        private void Numerico()
        { }
     
        private void territorio()
        {//ocultar campos de filtro
            this.groupHeaderSection1.Visible = false;
            this.groupFooterSection1.Visible = false;
            this.groupHeaderSection1.Group.Grouping.Clear();
        }
        private void cliente()
        {//ocultar campos de filtro
            this.groupHeaderSection4.Visible = false;
            this.groupFooterSection4.Visible = false;
            this.groupHeaderSection4.Group.Grouping.Clear();
        }
        private void producto()
        {//ocultar campos de filtro
            this.groupHeaderSection5.Visible = false;
            this.groupFooterSection5.Visible = false;
            this.groupHeaderSection5.Group.Grouping.Clear();
        }
    }
}