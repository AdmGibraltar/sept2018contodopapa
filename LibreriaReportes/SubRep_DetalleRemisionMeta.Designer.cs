namespace LibreriaReportes
{
    partial class SubRep_DetalleRemisionMeta
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter5 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter6 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter7 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter8 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter9 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter10 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.metaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.pROMEDIOCaptionTextBox = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.metaDataTextBox = new Telerik.Reporting.TextBox();
            this.pROMEDIODataTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.CalculatedFields.AddRange(new Telerik.Reporting.CalculatedField[] {
            new Telerik.Reporting.CalculatedField("Margen", typeof(decimal), "= IIf(Fields.PROMEDIO=0, 0, (1-(Parameters.ventaAAA.Value/Fields.PROMEDIO)))")});
            this.sqlDataSource1.ConnectionString = "LibreriaReportes.Properties.Settings.sianwebmtyg";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.Parameters.AddRange(new Telerik.Reporting.SqlDataSourceParameter[] {
            new Telerik.Reporting.SqlDataSourceParameter("@Id_Emp", System.Data.DbType.Int32, "=Parameters.Id_Emp.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("@Id_Cd", System.Data.DbType.Int32, "=Parameters.Id_Cd.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("@Id_Ter", System.Data.DbType.Int32, "=Parameters.Id_Ter.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("@anyo", System.Data.DbType.Int32, "=Parameters.anyo.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("@mes", System.Data.DbType.Int32, "=Parameters.mes.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("@Id_Cte", System.Data.DbType.Int32, "=Parameters.Id_Cte.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("@Id_TG", System.Data.DbType.Int32, "=Parameters.idTg.Value")});
            this.sqlDataSource1.SelectCommand = "dbo.proc_facturaPromedio";
            this.sqlDataSource1.SelectCommandType = Telerik.Reporting.SqlDataSourceCommandType.StoredProcedure;
            // 
            // metaCaptionTextBox
            // 
            this.metaCaptionTextBox.CanGrow = true;
            this.metaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.199920654296875D), Telerik.Reporting.Drawing.Unit.Inch(1.6000000238418579D));
            this.metaCaptionTextBox.Name = "metaCaptionTextBox";
            this.metaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8000004291534424D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.metaCaptionTextBox.StyleName = "Caption";
            this.metaCaptionTextBox.Value = "% U.B. Meta";
            // 
            // pROMEDIOCaptionTextBox
            // 
            this.pROMEDIOCaptionTextBox.CanGrow = true;
            this.pROMEDIOCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.199920654296875D), Telerik.Reporting.Drawing.Unit.Inch(0.30835309624671936D));
            this.pROMEDIOCaptionTextBox.Name = "pROMEDIOCaptionTextBox";
            this.pROMEDIOCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8000004291534424D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.pROMEDIOCaptionTextBox.StyleName = "Caption";
            this.pROMEDIOCaptionTextBox.Value = "Venta Prom. Trim. Anterior";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(2.8000001907348633D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.metaDataTextBox,
            this.pROMEDIODataTextBox,
            this.pROMEDIOCaptionTextBox,
            this.metaCaptionTextBox,
            this.textBox1,
            this.textBox2,
            this.textBox3});
            this.detail.Name = "detail";
            // 
            // metaDataTextBox
            // 
            this.metaDataTextBox.CanGrow = true;
            this.metaDataTextBox.Format = "{0:P2}";
            this.metaDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3D), Telerik.Reporting.Drawing.Unit.Inch(1.5999997854232788D));
            this.metaDataTextBox.Name = "metaDataTextBox";
            this.metaDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0791664123535156D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.metaDataTextBox.StyleName = "Data";
            this.metaDataTextBox.Value = "= Parameters.margenMeta.Value/100";
            // 
            // pROMEDIODataTextBox
            // 
            this.pROMEDIODataTextBox.CanGrow = true;
            this.pROMEDIODataTextBox.Format = "{0:C2}";
            this.pROMEDIODataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3D), Telerik.Reporting.Drawing.Unit.Inch(0.30835327506065369D));
            this.pROMEDIODataTextBox.Name = "pROMEDIODataTextBox";
            this.pROMEDIODataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0791664123535156D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.pROMEDIODataTextBox.StyleName = "Data";
            this.pROMEDIODataTextBox.Value = "=Fields.PROMEDIO";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.199920654296875D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8000004291534424D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "% U.B.";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Format = "{0:P2}";
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0020039081573486D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0791664123535156D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox2.StyleName = "Data";
            this.textBox2.Value = "= Fields.Margen";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0020039081573486D), Telerik.Reporting.Drawing.Unit.Inch(2.1000001430511475D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0771627426147461D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox3.Value = "= IIf(Fields.Margen< Parameters.margenMeta.Value/100, \"POR DEBAJO DE MARGEN META\"" +
    ", \"ACEPTABLE\")";
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(2D);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4,
            this.textBox5,
            this.textBox6,
            this.textBox7,
            this.textBox8,
            this.textBox9,
            this.textBox10,
            this.textBox11});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            this.reportHeaderSection1.Style.Visible = false;
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1000001430511475D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.textBox4.Value = "= Parameters.anyo.Value";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.5D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.textBox5.Value = "= Parameters.Conexion.Value";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1000001430511475D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox6.Value = "= Parameters.Id_Cd.Value";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1000001430511475D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox7.Value = "= Parameters.Id_Cte.Value";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1.4000000953674316D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox8.Value = "= Parameters.Id_Emp.Value";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1.7000001668930054D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9000002145767212D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox9.Value = "= Parameters.Id_Ter.Value";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7000002861022949D), Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7000001668930054D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox10.Value = "= Parameters.mes.Value";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7000002861022949D), Telerik.Reporting.Drawing.Unit.Inch(1.5D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7000001668930054D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox11.Value = "= Parameters.ventaAAA.Value";
            // 
            // SubRep_DetalleRemisionMeta
            // 
            this.DataSource = null;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail,
            this.reportHeaderSection1});
            this.Name = "SubRep_DetalleRemisionMeta";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.AllowNull = true;
            reportParameter1.Name = "Id_Emp";
            reportParameter1.Text = "IdEmp";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter2.AllowNull = true;
            reportParameter2.Name = "Id_Cd";
            reportParameter2.Text = "IdCd";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter3.AllowNull = true;
            reportParameter3.Name = "Id_Ter";
            reportParameter3.Text = "IdTer";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter4.Name = "anyo";
            reportParameter4.Text = "anyo";
            reportParameter4.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter5.Name = "mes";
            reportParameter5.Text = "mes";
            reportParameter5.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter6.AllowNull = true;
            reportParameter6.Name = "Id_Cte";
            reportParameter6.Text = "IdCte";
            reportParameter6.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter7.Name = "ventaAAA";
            reportParameter7.Type = Telerik.Reporting.ReportParameterType.Float;
            reportParameter8.Name = "Conexion";
            reportParameter9.Name = "margenMeta";
            reportParameter9.Type = Telerik.Reporting.ReportParameterType.Float;
            reportParameter10.AllowNull = true;
            reportParameter10.Name = "idTg";
            reportParameter10.Type = Telerik.Reporting.ReportParameterType.Integer;
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.ReportParameters.Add(reportParameter5);
            this.ReportParameters.Add(reportParameter6);
            this.ReportParameters.Add(reportParameter7);
            this.ReportParameters.Add(reportParameter8);
            this.ReportParameters.Add(reportParameter9);
            this.ReportParameters.Add(reportParameter10);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule1.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            styleRule1.Style.Font.Name = "Calibri";
            styleRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule2.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(167)))), ((int)(((byte)(227)))));
            styleRule2.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            styleRule2.Style.Font.Name = "Calibri";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule3.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            styleRule3.Style.Font.Name = "Calibri";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule4.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            styleRule4.Style.Font.Name = "Calibri";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.4583334922790527D);
            this.NeedDataSource += new System.EventHandler(this.SubRep_DetalleRemisionMeta_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.TextBox metaCaptionTextBox;
        private Telerik.Reporting.TextBox pROMEDIOCaptionTextBox;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox metaDataTextBox;
        private Telerik.Reporting.TextBox pROMEDIODataTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.ReportHeaderSection reportHeaderSection1;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;

    }
}