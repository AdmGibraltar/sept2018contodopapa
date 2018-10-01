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
    public partial class Rep_VenEstadisticaVentas : Telerik.Reporting.Report
    {
        public Rep_VenEstadisticaVentas()
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
                textBox2.Value = mes;
                int strEmp = 0;
                int Emp = 0;
                Int32.TryParse(this.ReportParameters["strEmp"].Value.ToString(), out strEmp);
                Int32.TryParse(this.ReportParameters["Id_Emp"].Value.ToString(), out Emp);
                if (Emp != 0)
                    if (strEmp != Emp)
                    {
                        textBox1.Visible = false;
                        textBox2.Visible = false;
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ocultarcolumnas(string mes)
        {
            //ENCABEZADOS
            txtCabEne.Visible = mes == "0" || mes == "1";
            txtCabFeb.Visible = mes == "0" || mes == "2";
            txtCabMar.Visible = mes == "0" || mes == "3";
            txtCabAbr.Visible = mes == "0" || mes == "4";
            txtCabMay.Visible = mes == "0" || mes == "5";
            txtCabJun.Visible = mes == "0" || mes == "6";
            txtCabJul.Visible = mes == "0" || mes == "7";
            txtCabAgo.Visible = mes == "0" || mes == "8";
            txtCabSep.Visible = mes == "0" || mes == "9";
            txtCabOct.Visible = mes == "0" || mes == "10";
            txtCabNov.Visible = mes == "0" || mes == "11";
            txtCabDic.Visible = mes == "0" || mes == "12";
            txtCabTotal.Visible = mes == "0";

            //DETALLES
            txtDetEne.Visible = mes == "0" || mes == "1";
            txtDetFeb.Visible = mes == "0" || mes == "2";
            txtDetMar.Visible = mes == "0" || mes == "3";
            txtDetAbr.Visible = mes == "0" || mes == "4";
            txtDetMay.Visible = mes == "0" || mes == "5";
            txtDetJun.Visible = mes == "0" || mes == "6";
            textBox44.Visible = mes == "0" || mes == "7";
            textBox5.Visible = mes == "0" || mes == "8";
            textBox6.Visible = mes == "0" || mes == "9";
            textBox77.Visible = mes == "0" || mes == "10";
            textBox78.Visible = mes == "0" || mes == "11";
            textBox79.Visible = mes == "0" || mes == "12";
            txtDetTotal.Visible = mes == "0";

            //Total Producto
            textBox61.Visible = mes == "0" || mes == "1";
            textBox60.Visible = mes == "0" || mes == "2";
            textBox59.Visible = mes == "0" || mes == "3";
            textBox57.Visible = mes == "0" || mes == "4";
            textBox55.Visible = mes == "0" || mes == "5";
            textBox49.Visible = mes == "0" || mes == "6";
            textBox48.Visible = mes == "0" || mes == "7";
            textBox37.Visible = mes == "0" || mes == "8";
            textBox35.Visible = mes == "0" || mes == "9";
            textBox23.Visible = mes == "0" || mes == "10";
            textBox20.Visible = mes == "0" || mes == "11";
            textBox17.Visible = mes == "0" || mes == "12";
            txtTotProdTotal.Visible = mes == "0";

            //Total cliente
            textBox62.Visible = mes == "0" || mes == "1";
            textBox85.Visible = mes == "0" || mes == "2";
            textBox94.Visible = mes == "0" || mes == "3";
            textBox86.Visible = mes == "0" || mes == "4";
            textBox87.Visible = mes == "0" || mes == "5";
            textBox88.Visible = mes == "0" || mes == "6";
            textBox89.Visible = mes == "0" || mes == "7";
            textBox90.Visible = mes == "0" || mes == "8";
            textBox91.Visible = mes == "0" || mes == "9";
            textBox92.Visible = mes == "0" || mes == "10";
            textBox93.Visible = mes == "0" || mes == "11";
            textBox63.Visible = mes == "0" || mes == "12";
            txtTotCliTotal.Visible = mes == "0";

            //Total territorio
            textBox95.Visible = mes == "0" || mes == "1";
            textBox97.Visible = mes == "0" || mes == "2";
            textBox106.Visible = mes == "0" || mes == "3";
            textBox98.Visible = mes == "0" || mes == "4";
            textBox99.Visible = mes == "0" || mes == "5";
            textBox100.Visible = mes == "0" || mes == "6";
            textBox101.Visible = mes == "0" || mes == "7";
            textBox102.Visible = mes == "0" || mes == "8";
            textBox103.Visible = mes == "0" || mes == "9";
            textBox104.Visible = mes == "0" || mes == "10";
            textBox105.Visible = mes == "0" || mes == "11";
            textBox96.Visible = mes == "0" || mes == "12";
            txtTotTerTotal.Visible = mes == "0";

            //Total general
            textBox107.Visible = mes == "0" || mes == "1";
            textBox109.Visible = mes == "0" || mes == "2";
            textBox118.Visible = mes == "0" || mes == "3";
            textBox110.Visible = mes == "0" || mes == "4";
            textBox111.Visible = mes == "0" || mes == "5";
            textBox112.Visible = mes == "0" || mes == "6";
            textBox113.Visible = mes == "0" || mes == "7";
            textBox114.Visible = mes == "0" || mes == "8";
            textBox115.Visible = mes == "0" || mes == "9";
            textBox116.Visible = mes == "0" || mes == "10";
            textBox117.Visible = mes == "0" || mes == "11";
            textBox108.Visible = mes == "0" || mes == "12";
            txtTotTotal.Visible = mes == "0";

            if (mes != "0")
            {
                double width_extra = 1.5;
                double left_extra = 6.14;
                textBox56.Width = textBox56.Width + Unit.Cm(width_extra);
                txtDetNombre.Width = txtDetNombre.Width + Unit.Cm(width_extra);

                //ENCABEZADOS
                txtCabEne.Left = mes == "1" ? Unit.Cm(left_extra) : txtCabEne.Left;
                txtCabFeb.Left = mes == "2" ? Unit.Cm(left_extra) : Unit.Cm(0);
                txtCabMar.Left = mes == "3" ? Unit.Cm(left_extra) : Unit.Cm(0);
                txtCabAbr.Left = mes == "4" ? Unit.Cm(left_extra) : Unit.Cm(0);
                txtCabMay.Left = mes == "5" ? Unit.Cm(left_extra) : Unit.Cm(0);
                txtCabJun.Left = mes == "6" ? Unit.Cm(left_extra) : Unit.Cm(0);
                txtCabJul.Left = mes == "7" ? Unit.Cm(left_extra) : Unit.Cm(0);
                txtCabAgo.Left = mes == "8" ? Unit.Cm(left_extra) : Unit.Cm(0);
                txtCabSep.Left = mes == "9" ? Unit.Cm(left_extra) : Unit.Cm(0);
                txtCabOct.Left = mes == "10" ? Unit.Cm(left_extra) : Unit.Cm(0);
                txtCabNov.Left = mes == "11" ? Unit.Cm(left_extra) : Unit.Cm(0);
                txtCabDic.Left = mes == "12" ? Unit.Cm(left_extra) : Unit.Cm(0);


                //DETALLES
                txtDetEne.Left = mes == "1" ? Unit.Cm(left_extra) : Unit.Cm(0);
                txtDetFeb.Left = mes == "2" ? Unit.Cm(left_extra) : Unit.Cm(0);
                txtDetMar.Left = mes == "3" ? Unit.Cm(left_extra) : Unit.Cm(0);
                txtDetAbr.Left = mes == "4" ? Unit.Cm(left_extra) : Unit.Cm(0);
                txtDetMay.Left = mes == "5" ? Unit.Cm(left_extra) : Unit.Cm(0);
                txtDetJun.Left = mes == "6" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox44.Left = mes == "7" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox5.Left = mes == "8" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox6.Left = mes == "9" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox77.Left = mes == "10" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox78.Left = mes == "11" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox79.Left = mes == "12" ? Unit.Cm(left_extra) : Unit.Cm(0);


                //Total Producto
                textBox61.Left = mes == "1" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox60.Left = mes == "2" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox59.Left = mes == "3" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox57.Left = mes == "4" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox55.Left = mes == "5" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox49.Left = mes == "6" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox48.Left = mes == "7" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox37.Left = mes == "8" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox35.Left = mes == "9" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox23.Left = mes == "10" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox20.Left = mes == "11" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox17.Left = mes == "12" ? Unit.Cm(left_extra) : Unit.Cm(0);


                //Total cliente
                textBox62.Left = mes == "1" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox85.Left = mes == "2" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox94.Left = mes == "3" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox86.Left = mes == "4" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox87.Left = mes == "5" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox88.Left = mes == "6" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox89.Left = mes == "7" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox90.Left = mes == "8" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox91.Left = mes == "9" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox92.Left = mes == "10" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox93.Left = mes == "11" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox63.Left = mes == "12" ? Unit.Cm(left_extra) : Unit.Cm(0);


                //Total territorio
                textBox95.Left = mes == "1" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox97.Left = mes == "2" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox106.Left = mes == "3" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox98.Left = mes == "4" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox99.Left = mes == "5" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox100.Left = mes == "6" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox101.Left = mes == "7" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox102.Left = mes == "8" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox103.Left = mes == "9" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox104.Left = mes == "10" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox105.Left = mes == "11" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox96.Left = mes == "12" ? Unit.Cm(left_extra) : Unit.Cm(0);


                //Total general
                textBox107.Left = mes == "1" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox109.Left = mes == "2" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox118.Left = mes == "3" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox110.Left = mes == "4" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox111.Left = mes == "5" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox112.Left = mes == "6" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox113.Left = mes == "7" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox114.Left = mes == "8" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox115.Left = mes == "9" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox116.Left = mes == "10" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox117.Left = mes == "11" ? Unit.Cm(left_extra) : Unit.Cm(0);
                textBox108.Left = mes == "12" ? Unit.Cm(left_extra) : Unit.Cm(0);

            }
        }

        private void ocultarcampos(int valor)
        {
            switch (valor)
            {//tipo de reporte
                case 1:
                    territorio();
                    cliente();
                    producto();
                    break;
                case 16:
                    territorio();
                    cliente();
                    producto();
                    break;
                case 17:
                    territorio();
                    cliente();
                    producto();
                    break;
                case 18:
                    cliente();
                    producto();
                    break;
                case 19:
                    cliente();
                    producto();
                    break;
                case 20:
                    producto();
                    break;
                case 21:
                    producto();
                    break;
                case 2:
                    territorio();
                    cliente();
                    producto();
                    Numerico();
                    break;
                case 3:
                    cliente();
                    producto();
                    break;
                case 4:
                    cliente();
                    producto();
                    Numerico();
                    break;
                case 5:
                    cliente();
                    producto();
                    break;
                case 6:
                    cliente();
                    producto();
                    Numerico();
                    break;
                case 7:
                    producto();
                    break;
                case 8:
                    producto();
                    Numerico();
                    break;
                case 9:
                    territorio();
                    cliente();
                    producto();
                    break;
                case 10:
                    territorio();
                    cliente();
                    producto();
                    Numerico();
                    break;
                case 11:
                    territorio();
                    producto();
                    break;
                case 12:
                    territorio();
                    producto();
                    Numerico();
                    break;
                case 13:
                    territorio();
                    cliente();
                    producto();
                    break;
                case 14:
                    territorio();
                    cliente();
                    producto();
                    Numerico();
                    break;
            }
        }

        private void Numerico()
        {
            //DETALLES
            txtDetEne.Format = "{0:N0}";
            txtDetFeb.Format = "{0:N0}";
            txtDetMar.Format = "{0:N0}";
            txtDetAbr.Format = "{0:N0}";
            txtDetMay.Format = "{0:N0}";
            txtDetJun.Format = "{0:N0}";
            textBox44.Format = "{0:N0}";
            textBox5.Format = "{0:N0}";
            textBox6.Format = "{0:N0}";
            textBox77.Format = "{0:N0}";
            textBox78.Format = "{0:N0}";
            textBox79.Format = "{0:N0}";
            txtDetTotal.Format = "{0:N0}";

            //Total Producto
            textBox61.Format = "{0:N0}";
            textBox60.Format = "{0:N0}";
            textBox59.Format = "{0:N0}";
            textBox57.Format = "{0:N0}";
            textBox55.Format = "{0:N0}";
            textBox49.Format = "{0:N0}";
            textBox48.Format = "{0:N0}";
            textBox37.Format = "{0:N0}";
            textBox35.Format = "{0:N0}";
            textBox23.Format = "{0:N0}";
            textBox20.Format = "{0:N0}";
            textBox17.Format = "{0:N0}";
            txtTotProdTotal.Format = "{0:N0}";

            //Total cliente
            textBox62.Format = "{0:N0}";
            textBox85.Format = "{0:N0}";
            textBox94.Format = "{0:N0}";
            textBox86.Format = "{0:N0}";
            textBox87.Format = "{0:N0}";
            textBox88.Format = "{0:N0}";
            textBox89.Format = "{0:N0}";
            textBox90.Format = "{0:N0}";
            textBox91.Format = "{0:N0}";
            textBox92.Format = "{0:N0}";
            textBox93.Format = "{0:N0}";
            textBox63.Format = "{0:N0}";
            txtTotCliTotal.Format = "{0:N0}";

            //Total territorio
            textBox95.Format = "{0:N0}";
            textBox97.Format = "{0:N0}";
            textBox106.Format = "{0:N0}";
            textBox98.Format = "{0:N0}";
            textBox99.Format = "{0:N0}";
            textBox100.Format = "{0:N0}";
            textBox101.Format = "{0:N0}";
            textBox102.Format = "{0:N0}";
            textBox103.Format = "{0:N0}";
            textBox104.Format = "{0:N0}";
            textBox105.Format = "{0:N0}";
            textBox96.Format = "{0:N0}";
            txtTotTerTotal.Format = "{0:N0}";

            //Total territorio
            textBox107.Format = "{0:N0}";
            textBox109.Format = "{0:N0}";
            textBox118.Format = "{0:N0}";
            textBox110.Format = "{0:N0}";
            textBox111.Format = "{0:N0}";
            textBox112.Format = "{0:N0}";
            textBox113.Format = "{0:N0}";
            textBox114.Format = "{0:N0}";
            textBox115.Format = "{0:N0}";
            textBox116.Format = "{0:N0}";
            textBox117.Format = "{0:N0}";
            textBox108.Format = "{0:N0}";
            txtTotTotal.Format = "{0:N0}";

        }
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