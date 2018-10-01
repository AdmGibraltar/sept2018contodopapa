namespace LibreriaReportes
{
    partial class ReporteComisiones
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.InstanceReportSource instanceReportSource1 = new Telerik.Reporting.InstanceReportSource();
            Telerik.Reporting.InstanceReportSource instanceReportSource2 = new Telerik.Reporting.InstanceReportSource();
            Telerik.Reporting.InstanceReportSource instanceReportSource3 = new Telerik.Reporting.InstanceReportSource();
            Telerik.Reporting.InstanceReportSource instanceReportSource4 = new Telerik.Reporting.InstanceReportSource();
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter5 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter6 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter7 = new Telerik.Reporting.ReportParameter();
            this.repComisionesEdoRes1 = new LibreriaReportes.RepComisionesEdoRes();
            this.repComisionesPPP1 = new LibreriaReportes.RepComisionesPPP();
            this.repComisionesCob1 = new LibreriaReportes.RepComisionesCob();
            this.repComisionesEdoCon1 = new LibreriaReportes.RepComisionesEdoCon();
            this.groupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.subReport2 = new Telerik.Reporting.SubReport();
            this.groupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.subReport1 = new Telerik.Reporting.SubReport();
            this.subReport3 = new Telerik.Reporting.SubReport();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.sqlConnection2 = new System.Data.SqlClient.SqlConnection();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.subReport4 = new Telerik.Reporting.SubReport();
            ((System.ComponentModel.ISupportInitialize)(this.repComisionesEdoRes1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repComisionesPPP1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repComisionesCob1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repComisionesEdoCon1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // repComisionesEdoRes1
            // 
            this.repComisionesEdoRes1.Name = "RepComisionesEdoRes";
            // 
            // repComisionesPPP1
            // 
            this.repComisionesPPP1.Name = "RepComisionesPPP";
            // 
            // repComisionesCob1
            // 
            this.repComisionesCob1.Name = "RepComisionesPPP";
            // 
            // repComisionesEdoCon1
            // 
            this.repComisionesEdoCon1.Name = "RepComisionesEdoCon";
            // 
            // groupFooterSection
            // 
            this.groupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.80000048875808716D);
            this.groupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.subReport2});
            this.groupFooterSection.Name = "groupFooterSection";
            this.groupFooterSection.Style.BorderColor.Bottom = System.Drawing.Color.Silver;
            this.groupFooterSection.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.groupFooterSection.Style.Color = System.Drawing.Color.Black;
            this.groupFooterSection.Style.Visible = true;
            // 
            // subReport2
            // 
            this.subReport2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.399999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D));
            this.subReport2.Name = "subReport2";
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Conexion", "=Parameters.Conexion.Value"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Id_Cd", "=Parameters.Id_Cd.Value"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Anio", "=Parameters.Anio.Value"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Mes", "=Parameters.Mes.Value"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Id_Rik", "=Parameters.Id_Rik.Value"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Id_Cte", "=Fields.Id_Cte"));
            instanceReportSource1.ReportDocument = this.repComisionesEdoRes1;
            this.subReport2.ReportSource = instanceReportSource1;
            this.subReport2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.026458740234375D), Telerik.Reporting.Drawing.Unit.Cm(0.49999919533729553D));
            this.subReport2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            // 
            // groupHeaderSection
            // 
            this.groupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.50000029802322388D);
            this.groupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox14,
            this.textBox15});
            this.groupHeaderSection.Name = "groupHeaderSection";
            this.groupHeaderSection.Style.BackgroundColor = System.Drawing.Color.Empty;
            // 
            // textBox14
            // 
            this.textBox14.Action = null;
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5000004768371582D), Telerik.Reporting.Drawing.Unit.Cm(0.49999997019767761D));
            this.textBox14.Style.Color = System.Drawing.Color.Black;
            this.textBox14.Style.Font.Bold = true;
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "Cliente:";
            // 
            // textBox15
            // 
            this.textBox15.Action = null;
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(17.999998092651367D), Telerik.Reporting.Drawing.Unit.Cm(0.49999997019767761D));
            this.textBox15.Style.Color = System.Drawing.Color.Black;
            this.textBox15.Style.Font.Bold = true;
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Value = "=Fields.Cte_NomComercial";
            // 
            // subReport1
            // 
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.599999725818634D));
            this.subReport1.Name = "subReport1";
            instanceReportSource2.Parameters.Add(new Telerik.Reporting.Parameter("Conexion", "=Parameters.Conexion.Value"));
            instanceReportSource2.Parameters.Add(new Telerik.Reporting.Parameter("Id_Cd", "=Parameters.Id_Cd.Value"));
            instanceReportSource2.Parameters.Add(new Telerik.Reporting.Parameter("Anio", "=Parameters.Anio.Value"));
            instanceReportSource2.Parameters.Add(new Telerik.Reporting.Parameter("Mes", "=Parameters.Mes.Value"));
            instanceReportSource2.Parameters.Add(new Telerik.Reporting.Parameter("Id_Rik", "=Parameters.Id_Rik.Value"));
            instanceReportSource2.Parameters.Add(new Telerik.Reporting.Parameter("Id_Cte", "=Fields.Id_Cte"));
            instanceReportSource2.ReportDocument = this.repComisionesPPP1;
            this.subReport1.ReportSource = instanceReportSource2;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.426458358764648D), Telerik.Reporting.Drawing.Unit.Cm(0.49999919533729553D));
            this.subReport1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            // 
            // subReport3
            // 
            this.subReport3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.subReport3.Name = "subReport3";
            instanceReportSource3.Parameters.Add(new Telerik.Reporting.Parameter("Conexion", "=Parameters.Conexion.Value"));
            instanceReportSource3.Parameters.Add(new Telerik.Reporting.Parameter("Id_Cd", "=Parameters.Id_Cd.Value"));
            instanceReportSource3.Parameters.Add(new Telerik.Reporting.Parameter("Anio", "=Parameters.Anio.Value"));
            instanceReportSource3.Parameters.Add(new Telerik.Reporting.Parameter("Mes", "=Parameters.Mes.Value"));
            instanceReportSource3.Parameters.Add(new Telerik.Reporting.Parameter("Id_Rik", "=Parameters.Id_Rik.Value"));
            instanceReportSource3.Parameters.Add(new Telerik.Reporting.Parameter("Id_Cte", "=Fields.Id_Cte"));
            instanceReportSource3.ReportDocument = this.repComisionesCob1;
            this.subReport3.ReportSource = instanceReportSource3;
            this.subReport3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(20.5D), Telerik.Reporting.Drawing.Unit.Cm(0.49999919533729553D));
            this.subReport3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(3D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4,
            this.textBox2,
            this.textBox3,
            this.textBox1,
            this.textBox43,
            this.textBox22});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.PrintOnLastPage = true;
            this.pageHeader.Style.BorderColor.Bottom = System.Drawing.Color.Gray;
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5D), Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10D), Telerik.Reporting.Drawing.Unit.Cm(0.699999988079071D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Value = "Key Química S.A. de C.V.";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5D), Telerik.Reporting.Drawing.Unit.Cm(0.800000011920929D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.9999990463256836D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Value = "SIANWeb";
            // 
            // textBox3
            // 
            this.textBox3.Angle = 0D;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.3999998569488525D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(20.5D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Value = "Reporte comisiones";
            // 
            // textBox1
            // 
            this.textBox1.Angle = 0D;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.9999997615814209D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(20.5D), Telerik.Reporting.Drawing.Unit.Cm(0.40000012516975403D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Value = "Filtros";
            // 
            // textBox43
            // 
            this.textBox43.CanGrow = false;
            this.textBox43.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15D), Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D));
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.5099983215332031D), Telerik.Reporting.Drawing.Unit.Cm(0.40000003576278687D));
            this.textBox43.Style.Font.Name = "Verdana";
            this.textBox43.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox43.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox43.Value = "= \"Página \" + PageNumber + \" de \" + PageCount";
            // 
            // textBox22
            // 
            this.textBox22.CanGrow = false;
            this.textBox22.Format = "{0:g}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15D), Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.5D), Telerik.Reporting.Drawing.Unit.Cm(0.50999999046325684D));
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox22.Value = "=\"Fecha imp.:  \" + Now()";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(1.2999998331069946D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.subReport3,
            this.subReport1});
            this.detail.Name = "detail";
            this.detail.Style.Visible = true;
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "Data Source=189.206.126.68;Initial Catalog=SIANCentral;User ID=sa";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlConnection2
            // 
            this.sqlConnection2.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlDataAdapter1
            // 
            this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
            this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "spCapGenerarPoliza_Imprimir", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("cuenta", "cuenta"),
                        new System.Data.Common.DataColumnMapping("Referencia", "Referencia"),
                        new System.Data.Common.DataColumnMapping("subcuenta", "subcuenta"),
                        new System.Data.Common.DataColumnMapping("subsubcuenta", "subsubcuenta"),
                        new System.Data.Common.DataColumnMapping("Almacen", "Almacen"),
                        new System.Data.Common.DataColumnMapping("TipoMov", "TipoMov"),
                        new System.Data.Common.DataColumnMapping("Cargo", "Cargo"),
                        new System.Data.Common.DataColumnMapping("Abono", "Abono")})});
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "spRepComisiones";
            this.sqlSelectCommand1.CommandTimeout = 600;
            this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand1.Connection = this.sqlConnection1;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Id_Cd", System.Data.SqlDbType.Int),
            new System.Data.SqlClient.SqlParameter("@Anio", System.Data.SqlDbType.Int),
            new System.Data.SqlClient.SqlParameter("@Mes", System.Data.SqlDbType.Int),
            new System.Data.SqlClient.SqlParameter("@Id_Rik", System.Data.SqlDbType.Int),
            new System.Data.SqlClient.SqlParameter("@Reporte", System.Data.SqlDbType.NVarChar)});
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.56770813465118408D);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.subReport4});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // subReport4
            // 
            this.subReport4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.subReport4.Name = "subReport4";
            instanceReportSource4.Parameters.Add(new Telerik.Reporting.Parameter("Conexion", "=Parameters.Conexion.Value"));
            instanceReportSource4.Parameters.Add(new Telerik.Reporting.Parameter("Id_Cd", "=Parameters.Id_Cd.Value"));
            instanceReportSource4.Parameters.Add(new Telerik.Reporting.Parameter("Anio", "=Parameters.Anio.Value"));
            instanceReportSource4.Parameters.Add(new Telerik.Reporting.Parameter("Mes", "=Parameters.Mes.Value"));
            instanceReportSource4.Parameters.Add(new Telerik.Reporting.Parameter("Id_Rik", "=Parameters.Id_Rik.Value"));
            instanceReportSource4.ReportDocument = this.repComisionesEdoCon1;
            this.subReport4.ReportSource = instanceReportSource4;
            this.subReport4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.026458740234375D), Telerik.Reporting.Drawing.Unit.Cm(0.49999919533729553D));
            this.subReport4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            // 
            // ReporteComisiones
            // 
            this.DataSource = this.sqlDataAdapter1;
            group1.GroupFooter = this.groupFooterSection;
            group1.GroupHeader = this.groupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.Id_Cte"));
            group1.Name = "group";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection,
            this.groupFooterSection,
            this.pageHeader,
            this.detail,
            this.reportFooterSection1});
            this.Name = "ReporteComisiones";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "Conexion";
            reportParameter2.Name = "Id_Cd";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter3.Name = "Anio";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter4.Name = "Mes";
            reportParameter4.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter5.Name = "Id_Rik";
            reportParameter5.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter6.Name = "MesStr";
            reportParameter7.Name = "Representante";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.ReportParameters.Add(reportParameter5);
            this.ReportParameters.Add(reportParameter6);
            this.ReportParameters.Add(reportParameter7);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Style.Visible = true;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(20.526460647583008D);
            this.NeedDataSource += new System.EventHandler(this.RepComisiones_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this.repComisionesEdoRes1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repComisionesPPP1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repComisionesCob1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repComisionesEdoCon1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlConnection sqlConnection2;
        private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.Group group1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection;
        private Telerik.Reporting.GroupFooterSection groupFooterSection;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.SubReport subReport1;
        private RepComisionesPPP repComisionesPPP1;
        private Telerik.Reporting.SubReport subReport2;
        private RepComisionesEdoRes repComisionesEdoRes1;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.SubReport subReport3;
        private RepComisionesCob repComisionesCob1;
        private Telerik.Reporting.SubReport subReport4;
        private RepComisionesEdoCon repComisionesEdoCon1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox43;
        private Telerik.Reporting.TextBox textBox22;
    }
}