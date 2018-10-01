namespace LibreriaReportes
{
    partial class SubTablaProductos
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
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox13 = new Telerik.Reporting.TextBox();   //detail
            this.textBox14 = new Telerik.Reporting.TextBox();   //detail
            this.textBox15 = new Telerik.Reporting.TextBox();   //detail
            this.textBox16 = new Telerik.Reporting.TextBox();   //detail
            this.textBox17 = new Telerik.Reporting.TextBox();   //detail
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.textBox20 = new Telerik.Reporting.TextBox();   
            this.textBox37 = new Telerik.Reporting.TextBox();   
            this.textBox38 = new Telerik.Reporting.TextBox();   
            this.textBox39 = new Telerik.Reporting.TextBox();   
            this.textBox40 = new Telerik.Reporting.TextBox();   
            this.textBox41 = new Telerik.Reporting.TextBox();   
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            this.reportHeaderSection1.Style.Font.Name = "Verdana";
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox20,
            this.textBox37,
            this.textBox38,
            this.textBox39,
            this.textBox40,
            this.textBox41});
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.8000001907348633D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000009536743164D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox20.Style.Font.Name = "Verdana";
            this.textBox20.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9D, Telerik.Reporting.Drawing.UnitType.Point);            
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "EQUIPO";
            //
            double w11 = 1.8;
            double w21 = 4.5;
            double w31 = 2;
            double w41 = 2;
            double w51 = 2;

            double p11 = 0;
            double p21 = p11 + w11;
            double p31 = p21 + w21;
            double p41 = p31 + w31;
            double p51 = p41 + w41;
            //
            //// textBox37
            ////            
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(p11, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(.5D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(w11, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000009536743164D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox37.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox37.Style.Font.Bold = true;
            this.textBox37.Style.Font.Name = "Verdana";
            this.textBox37.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox37.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox37.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox37.Value = "CLAVE";

            //
            //// textBox38
            //// 
            this.textBox38.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(p21, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(.5D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(w21, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000009536743164D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox38.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox38.Style.Font.Bold = true;
            this.textBox38.Style.Font.Name = "Verdana";
            this.textBox38.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox38.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox38.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox38.Value = "DESCRIPCION";
            // 
            // textBox39
            // 
            this.textBox39.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(p31, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(.5D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(w31, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000009536743164D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox39.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox39.Style.Font.Bold = true;
            this.textBox39.Style.Font.Name = "Verdana";
            this.textBox39.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox39.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox39.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox39.Value = "CANTIDAD";
            // 
            // textBox40
            // 
            this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(p41, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(.50D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(w41, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000009536743164D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox40.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox40.Style.Font.Bold = true;
            this.textBox40.Style.Font.Name = "Verdana";
            this.textBox40.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox40.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox40.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox40.Value = "COSTO";
            // 
            // textBox41
            // 
            this.textBox41.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(p51, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(.50D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(w51, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000009536743164D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox41.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox41.Style.Font.Bold = true;
            this.textBox41.Style.Font.Name = "Verdana";
            this.textBox41.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox41.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox41.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox41.Value = "TOTAL";
            

            // 
            // detail
            // 
            this.detail.Height = new Telerik.Reporting.Drawing.Unit(0.4D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox13,
            this.textBox14,
            this.textBox15,
            this.textBox16,
            this.textBox17});
            this.detail.Name = "detail";
            this.detail.Style.Font.Name = "Verdana";
            // 
            // textBox13
            //             

            double w1 = 1.8;
            double w2 = 4.5;
            double w3 = 2;
            double w4 = 2;
            double w5 = 2;

            double p1 = 0;
            double p2 = p1 + w1;
            double p3 = p2 + w2;
            double p4 = p3 + w3;
            double p5 = p4 + w4;
            
                        
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(p1, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(w1, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm));                        
            this.textBox13.Style.Font.Name = "Verdana";
            this.textBox13.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(7.5D, Telerik.Reporting.Drawing.UnitType.Point);            
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;           
            this.textBox13.Value = "= Fields.Id_Prd";
            // 
            // textBox14
            //  
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(p2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(w2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox14.Style.Font.Name = "Verdana";
            this.textBox14.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox14.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(6D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox14.Value = "= Fields.[Prd_Descripcion]";
            // 
            // textBox15
            //  
            this.textBox15.Format = "{0:N2}";
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(p3, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(w3, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm));                        
            this.textBox15.Style.Font.Name = "Verdana";
            this.textBox15.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox15.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(7.5D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;           
            this.textBox15.Value = "= Fields.[Vap_Cantidad]";
            // 
            // textBox16
            //  
            this.textBox16.Format = "{0:N2}";
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(p4, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(w4, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox16.Style.Font.Name = "Verdana";
            this.textBox16.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox16.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(7.5D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox16.Value = "= Fields.[Vap_Costo]";
           //
            // textBox17
            //  
            this.textBox17.Format = "{0:N2}";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(p5, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(w5, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox17.Style.Font.Name = "Verdana";
            this.textBox17.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(7.5D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.Value = "= Fields.[Total1]";
            // sqlDataAdapter1
            // 
            this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
            this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "spCapValuacionCtasMarg_ImpresionLista2", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Ficha", "Ficha"),
                        new System.Data.Common.DataColumnMapping("Pag_Importe", "Pag_Importe")})});
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "dbo.spCapValuacionCtasMarg_ImpresionLista2";
            this.sqlSelectCommand1.CommandTimeout = 600;
            this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand1.Connection = this.sqlConnection1;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, 0),
            new System.Data.SqlClient.SqlParameter("@Id_Emp", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, "1"),
            new System.Data.SqlClient.SqlParameter("@Id_Cd", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, "110"),
            new System.Data.SqlClient.SqlParameter("@Id_Vap", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, "12101")});
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            //             
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4,
            this.textBox5});
            this.reportFooterSection1.Name = "reportFooterSection1";
            this.reportFooterSection1.Style.Font.Name = "Verdana";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(p1, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(w1+w2+w3+w4, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox4.Style.BorderStyle.Default= Telerik.Reporting.Drawing.BorderType.Solid;
            //this.textBox4.Style.BorderWidth.Default = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox4.Style.Font.Name = "Verdana";
            this.textBox4.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(7.5D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox4.Style.Font.Strikeout = false;
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.TextWrap = false;
            this.textBox4.Value = "INVERSION TOTAL EN SISTEMAS PROPIETARIOS";
            // 
            // textBox5
            // 
            this.textBox5.Format = "{0:N2}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(w1 + w2 + w3 + w4, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(w5, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox5.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            //this.textBox5.Style.BorderWidth.Default = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox5.Style.Font.Name = "Verdana";
            this.textBox5.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(7.5D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox5.Style.Font.Strikeout = false;
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox5.TextWrap = false;
            this.textBox5.Value = "=Sum(Fields.Total1)";
            // 
            
            // SubTablaProductos
            // 
            this.DataSource = this.sqlDataAdapter1;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.reportHeaderSection1,
            this.detail,
            this.reportFooterSection1,
            });
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "Id_Emp";
            reportParameter2.Name = "Id_Cd";
            reportParameter3.Name = "Id_Vap";
            reportParameter4.Name = "Emp_Cnx";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Name = "Verdana";
            this.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(7.5D, Telerik.Reporting.Drawing.UnitType.Point);
            this.Width = new Telerik.Reporting.Drawing.Unit(12.3D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.NeedDataSource += new System.EventHandler(this.SubTablaProductos_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detail;
        private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox38;
        private Telerik.Reporting.TextBox textBox39;
        private Telerik.Reporting.TextBox textBox40;
        private Telerik.Reporting.TextBox textBox41;
        private Telerik.Reporting.ReportHeaderSection reportHeaderSection1;
    }
}