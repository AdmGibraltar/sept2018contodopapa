namespace LibreriaReportes
{
    partial class Rep_RCitasAgendadas
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
            Telerik.Reporting.ReportParameter reportParameter11 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter12 = new Telerik.Reporting.ReportParameter();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox36 = new Telerik.Reporting.TextBox();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox39 = new Telerik.Reporting.TextBox();
            this.textBox40 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.sqlConnection2 = new System.Data.SqlClient.SqlConnection();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlConnection3 = new System.Data.SqlClient.SqlConnection();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(4.6999998092651367D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox30,
            this.textBox31,
            this.textBox32,
            this.textBox33,
            this.textBox34,
            this.textBox43,
            this.textBox3,
            this.textBox5,
            this.textBox7,
            this.textBox11,
            this.textBox13,
            this.textBox25,
            this.textBox26,
            this.textBox29,
            this.textBox36,
            this.textBox37,
            this.textBox39,
            this.textBox40,
            this.textBox12,
            this.textBox10});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox30
            // 
            this.textBox30.CanGrow = false;
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.100000381469727D), Telerik.Reporting.Drawing.Unit.Cm(1.7500001192092896D));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.190000057220459D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox30.Style.Font.Name = "Verdana";
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox30.Value = "= Parameters.fecha";
            // 
            // textBox31
            // 
            this.textBox31.CanGrow = false;
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.100000381469727D), Telerik.Reporting.Drawing.Unit.Cm(1.1500000953674316D));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.190000057220459D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox31.Style.Font.Name = "Verdana";
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox31.Value = "= \"Usuario: \" + Parameters.Usuario";
            // 
            // textBox32
            // 
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.0625009536743164D), Telerik.Reporting.Drawing.Unit.Cm(1.5003999471664429D));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.4649982452392578D), Telerik.Reporting.Drawing.Unit.Cm(1.7996008396148682D));
            this.textBox32.Style.Font.Bold = true;
            this.textBox32.Style.Font.Name = "Verdana";
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox32.Value = "Reporte de Citas Agendadas";
            // 
            // textBox33
            // 
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.5350003242492676D), Telerik.Reporting.Drawing.Unit.Cm(0.90020018815994263D));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.8649988174438477D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox33.Style.Font.Bold = true;
            this.textBox33.Style.Font.Name = "Verdana";
            this.textBox33.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox33.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox33.Value = "= Parameters.nombreSucursal";
            // 
            // textBox34
            // 
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.5350003242492676D), Telerik.Reporting.Drawing.Unit.Cm(0.30000001192092896D));
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.8649988174438477D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox34.Style.Font.Bold = true;
            this.textBox34.Style.Font.Name = "Verdana";
            this.textBox34.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox34.Value = "= Parameters.nombreEmpresa";
            // 
            // textBox43
            // 
            this.textBox43.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.100000381469727D), Telerik.Reporting.Drawing.Unit.Cm(0.550000011920929D));
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.190000057220459D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox43.Style.Font.Name = "Verdana";
            this.textBox43.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox43.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox43.Value = "= \"Página \" + PageNumber + \" de \" + PageCount";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.878715806924447E-06D), Telerik.Reporting.Drawing.Unit.Cm(0.20000003278255463D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1999964714050293D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Verdana";
            this.textBox3.Style.Visible = false;
            this.textBox3.Value = "Territorio: ";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.6941048493827111E-07D), Telerik.Reporting.Drawing.Unit.Cm(1.5000002384185791D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1999964714050293D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Name = "Verdana";
            this.textBox5.Value = "Cliente: ";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.7870905796589796E-06D), Telerik.Reporting.Drawing.Unit.Cm(2.2000002861022949D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9997971057891846D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Name = "Verdana";
            this.textBox7.Value = "A Partir de:";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.0000002384185791D), Telerik.Reporting.Drawing.Unit.Cm(2.2000000476837158D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9172515869140625D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox11.Style.Font.Bold = false;
            this.textBox11.Style.Font.Name = "Verdana";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox11.Value = "= Parameters.FechaInicial";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.2172515392303467D), Telerik.Reporting.Drawing.Unit.Cm(1.5000001192092896D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.6999998092651367D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox13.Style.Font.Bold = false;
            this.textBox13.Style.Font.Name = "Verdana";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox13.Value = "= Parameters.Clientee";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(4.0999999046325684D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.7999998927116394D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox25.Style.Font.Bold = true;
            this.textBox25.Style.Font.Name = "Verdana";
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox25.Value = "#";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.70020055770874D), Telerik.Reporting.Drawing.Unit.Cm(4.0999999046325684D));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.7997989654541016D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox26.Style.Font.Bold = true;
            this.textBox26.Style.Font.Name = "Verdana";
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox26.Value = "Descripción";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Cm(4.0999999046325684D));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.90000057220459D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox29.Style.Font.Bold = true;
            this.textBox29.Style.Font.Name = "Verdana";
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox29.Value = "Cita";
            // 
            // textBox36
            // 
            this.textBox36.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.559027671813965D), Telerik.Reporting.Drawing.Unit.Cm(4.0999999046325684D));
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0550012588500977D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox36.Style.Font.Bold = true;
            this.textBox36.Style.Font.Name = "Verdana";
            this.textBox36.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox36.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox36.Value = "Inicia";
            // 
            // textBox37
            // 
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.699995040893555D), Telerik.Reporting.Drawing.Unit.Cm(4.0999999046325684D));
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7263940572738648D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox37.Style.Font.Bold = true;
            this.textBox37.Style.Font.Name = "Verdana";
            this.textBox37.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox37.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox37.Value = "Termina";
            // 
            // textBox39
            // 
            this.textBox39.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.5D), Telerik.Reporting.Drawing.Unit.Cm(4.0999999046325684D));
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3768277168273926D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox39.Style.Font.Bold = true;
            this.textBox39.Style.Font.Name = "Verdana";
            this.textBox39.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox39.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox39.Value = "Estatus";
            // 
            // textBox40
            // 
            this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.100000381469727D), Telerik.Reporting.Drawing.Unit.Cm(4.0999999046325684D));
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.4899003505706787D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox40.Style.Font.Bold = true;
            this.textBox40.Style.Font.Name = "Verdana";
            this.textBox40.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox40.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox40.Value = "Motivo";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.8997998833656311D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.6999998092651367D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox12.Style.Font.Bold = false;
            this.textBox12.Style.Font.Name = "Verdana";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox12.Value = "= Parameters.strRSC";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.021209945902228355D), Telerik.Reporting.Drawing.Unit.Cm(0.8997998833656311D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1999964714050293D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Font.Name = "Verdana";
            this.textBox10.Style.Visible = true;
            this.textBox10.Value = "RSC:";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.40009960532188416D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2,
            this.textBox16,
            this.textBox17,
            this.textBox19,
            this.textBox24,
            this.textBox4});
            this.detail.Name = "detail";
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.7999998927116394D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox1.Style.Font.Name = "Verdana";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox1.Value = "= Fields.[ID]";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.7002010345458984D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.7997984886169434D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox2.Style.Font.Name = "Verdana";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Value = "= Fields.[Description]";
            // 
            // textBox16
            // 
            this.textBox16.Format = "{0}";
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.100000381469727D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.4900000095367432D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox16.Style.Font.Name = "Verdana";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox16.Value = "= Fields.[MotivoEliminacion]";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.6000000238418579D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.1000008583068848D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox17.Style.Font.Name = "Verdana";
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox17.Value = "= Fields.[Subject]";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4000009298324585D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox19.Style.Font.Name = "Verdana";
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox19.Value = "= Fields.[Estatus]";
            // 
            // textBox24
            // 
            this.textBox24.Format = "{0:d}";
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.559029579162598D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0549995899200439D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox24.Style.Font.Name = "Verdana";
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox24.Value = "= Fields.[Start]";
            // 
            // textBox4
            // 
            this.textBox4.Format = "{0:d}";
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.699999809265137D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7263902425765991D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox4.Style.Font.Name = "Verdana";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.Value = "= Fields.[Start]";
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "Data Source=EGBKSVR;Initial Catalog=SIANWEBPruebas;Persist Security Info=True;Use" +
    "r ID=sa;Password=sistemas";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlConnection2
            // 
            this.sqlConnection2.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "dbo.spRep_CitasAgendadas";
            this.sqlSelectCommand1.CommandTimeout = 600;
            this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand1.Connection = this.sqlConnection2;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Id_Emp", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Id_Cd", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@RSC", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@Cliente", System.Data.SqlDbType.NVarChar, 2147483647),
            new System.Data.SqlClient.SqlParameter("@FechaInicial", System.Data.SqlDbType.DateTime, 8)});
            // 
            // sqlConnection3
            // 
            this.sqlConnection3.ConnectionString = "Data Source=(local);Initial Catalog=sianwebmtycl;Integrated Security=True";
            this.sqlConnection3.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlDataAdapter1
            // 
            this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
            this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "spRep_CitasAgendadas", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID", "ID"),
                        new System.Data.Common.DataColumnMapping("Subject", "Subject"),
                        new System.Data.Common.DataColumnMapping("Description", "Description"),
                        new System.Data.Common.DataColumnMapping("Start", "Start"),
                        new System.Data.Common.DataColumnMapping("End", "End"),
                        new System.Data.Common.DataColumnMapping("IdPreRequisito", "IdPreRequisito"),
                        new System.Data.Common.DataColumnMapping("Estatus", "Estatus"),
                        new System.Data.Common.DataColumnMapping("MotivoEliminacion", "MotivoEliminacion")})});
            // 
            // Rep_RCitasAgendadas
            // 
            this.DataSource = this.sqlDataAdapter1;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail});
            this.Name = "Rep_RCitasAgendadas";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "nombreEmpresa";
            reportParameter2.Name = "nombreSucursal";
            reportParameter3.Name = "usuario";
            reportParameter4.Name = "Fecha";
            reportParameter5.Name = "Clientee";
            reportParameter6.Name = "Id_Emp";
            reportParameter6.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter7.Name = "Id_Cd";
            reportParameter7.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter8.Name = "Cliente";
            reportParameter9.Name = "FechaInicial";
            reportParameter9.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter10.Name = "IdRSC";
            reportParameter11.Name = "strRSC";
            reportParameter12.Name = "Conexion";
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
            this.ReportParameters.Add(reportParameter11);
            this.ReportParameters.Add(reportParameter12);
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(20.590002059936523D);
            this.NeedDataSource += new System.EventHandler(this.Rep_RCitasAgendadas_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox43;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlConnection sqlConnection2;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox24;
        private System.Data.SqlClient.SqlConnection sqlConnection3;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox36;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox39;
        private Telerik.Reporting.TextBox textBox40;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox10;
    }
}