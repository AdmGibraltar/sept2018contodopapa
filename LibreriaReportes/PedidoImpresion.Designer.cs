namespace LibreriaReportes
{
    partial class PedidoImpresion
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
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox38 = new Telerik.Reporting.TextBox();
            this.textBox39 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.textBox40 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.textBox36 = new Telerik.Reporting.TextBox();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.sqlConnection2 = new System.Data.SqlClient.SqlConnection();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(6.7000002861022949D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox9,
            this.textBox10,
            this.textBox11,
            this.textBox12,
            this.textBox13,
            this.textBox14,
            this.textBox15,
            this.textBox16,
            this.textBox17,
            this.textBox18,
            this.textBox19,
            this.textBox20,
            this.textBox21,
            this.textBox22,
            this.textBox23,
            this.textBox24,
            this.textBox37,
            this.textBox38,
            this.textBox39});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox9
            // 
            this.textBox9.Format = "{0:d}";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D), Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.5D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox9.Value = "=Fields.[Ped_Fecha]";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.50009989738464355D));
            this.textBox10.Multiline = false;
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(0.59999990463256836D));
            this.textBox10.Value = "=Fields.[U_Nombre]";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.69999885559082D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0000007152557373D), Telerik.Reporting.Drawing.Unit.Cm(0.59999990463256836D));
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox11.Value = "=Fields.[Id_Ped]";
            // 
            // textBox12
            // 
            this.textBox12.Format = "{0:g}";
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.300602912902832D), Telerik.Reporting.Drawing.Unit.Cm(1.4000000953674316D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.5000014305114746D), Telerik.Reporting.Drawing.Unit.Cm(0.59999990463256836D));
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.Value = "=Fields.[Ped_Fecha]";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.30000004172325134D), Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.5000004768371582D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox13.Value = "=Fields.[Cte_Nombre]";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.600101470947266D), Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox14.Value = "=Fields.[Cte_Nombre]";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.30000004172325134D), Telerik.Reporting.Drawing.Unit.Cm(2.900200366973877D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.5000004768371582D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox15.Value = "=Fields.[Cte_FacCalle] + \" #\" +  Fields.[Cte_FacNumero] + \" \" + Fields.[Cte_FacCo" +
    "lonia] + \" \" + Fields.[Cte_FacEstado]";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.30000004172325134D), Telerik.Reporting.Drawing.Unit.Cm(3.5004007816314697D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.5000014305114746D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox16.Value = "= \"C.P. \" + Fields.[Cte_FacCp]";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.600102424621582D), Telerik.Reporting.Drawing.Unit.Cm(3.508742094039917D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.5000014305114746D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox17.Value = "= \"C.P. \" + Fields.[Cte_Cp]";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.600102424621582D), Telerik.Reporting.Drawing.Unit.Cm(2.900200366973877D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.5000004768371582D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox18.Value = "= Fields.[Cte_Calle] + \" #\" +  Fields.[Cte_Numero] + \" \" + Fields.[Cte_Colonia] +" +
    " \" \" + Fields.[Cte_Estado]";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.30000004172325134D), Telerik.Reporting.Drawing.Unit.Cm(5.1999998092651367D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.5000004768371582D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox19.Value = "=Fields.[Ped_Solicito]";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.899698257446289D), Telerik.Reporting.Drawing.Unit.Cm(4.7999005317687988D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0000007152557373D), Telerik.Reporting.Drawing.Unit.Cm(0.60000050067901611D));
            this.textBox20.Value = "=Fields.[Id_Cd]";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.899899482727051D), Telerik.Reporting.Drawing.Unit.Cm(4.8000001907348633D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0000007152557373D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox21.Value = "=Fields.[Id_Ter]";
            // 
            // textBox22
            // 
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.899900436401367D), Telerik.Reporting.Drawing.Unit.Cm(4.7999005317687988D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0000007152557373D), Telerik.Reporting.Drawing.Unit.Cm(0.599999725818634D));
            this.textBox22.Value = "=Fields.[Id_Rik]";
            // 
            // textBox23
            // 
            this.textBox23.Format = "{0:d}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.3000011444091797D), Telerik.Reporting.Drawing.Unit.Cm(5.8001995086669922D));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.599999725818634D));
            this.textBox23.Value = "=Fields.[Ped_FechaEntrega]";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.899901390075684D), Telerik.Reporting.Drawing.Unit.Cm(5.8000001907348633D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9999990463256836D), Telerik.Reporting.Drawing.Unit.Cm(0.599999725818634D));
            this.textBox24.Value = "=Fields.[Ped_CondEntrega]";
            // 
            // textBox37
            // 
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.30000025033950806D), Telerik.Reporting.Drawing.Unit.Cm(4.2999997138977051D));
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.5000009536743164D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox37.Value = "= Fields.[Ped_PedidoDel]";
            // 
            // textBox38
            // 
            this.textBox38.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.2999997138977051D), Telerik.Reporting.Drawing.Unit.Cm(4.2999997138977051D));
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.5000009536743164D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox38.Value = "= Fields.[Ped_OrdenEntrega]";
            // 
            // textBox39
            // 
            this.textBox39.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.3000001907348633D), Telerik.Reporting.Drawing.Unit.Cm(5.1999993324279785D));
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox39.Value = "= Fields.[Ped_Requisicion]";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.40030017495155334D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2,
            this.textBox4,
            this.textBox5,
            this.textBox6,
            this.textBox7,
            this.textBox3});
            this.detail.Name = "detail";
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Cm(0.40019923448562622D));
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox1.TextWrap = false;
            this.textBox1.Value = "=Fields.[Id_Ter]";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.89999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.5D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox2.TextWrap = false;
            this.textBox2.Value = "=Fields.[Id_Prd]";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.799999237060547D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.0999990701675415D), Telerik.Reporting.Drawing.Unit.Cm(0.40019923448562622D));
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox4.TextWrap = false;
            this.textBox4.Value = "=Fields.[Prd_Cantidad]";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.300000190734863D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0000007152557373D), Telerik.Reporting.Drawing.Unit.Cm(0.40019923448562622D));
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox5.TextWrap = false;
            this.textBox5.Value = "=Fields.[Prd_Presentacion] + \' \' + Fields.Prd_Unidad";
            // 
            // textBox6
            // 
            this.textBox6.Format = "{0:N2}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.500000953674316D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9999990463256836D), Telerik.Reporting.Drawing.Unit.Cm(0.40019923448562622D));
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.TextWrap = false;
            this.textBox6.Value = "=Fields.[Prd_Precio]";
            // 
            // textBox7
            // 
            this.textBox7.Format = "{0:N2}";
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.69999885559082D), Telerik.Reporting.Drawing.Unit.Cm(0.00010052680590888485D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.40019887685775757D));
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox7.TextWrap = false;
            this.textBox7.Value = "=Fields.[Prd_Importe]";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.5D), Telerik.Reporting.Drawing.Unit.Cm(0.00010093052696902305D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.1999998092651367D), Telerik.Reporting.Drawing.Unit.Cm(0.40019923448562622D));
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox3.TextWrap = false;
            this.textBox3.Value = "=Fields.[Prd_Descripcion]";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(1.2000001668930054D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox40});
            this.pageFooter.Name = "pageFooter";
            // 
            // textBox40
            // 
            this.textBox40.CanGrow = false;
            this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.0999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.19999989867210388D));
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8000001907348633D), Telerik.Reporting.Drawing.Unit.Cm(0.49999803304672241D));
            this.textBox40.Style.Font.Name = "Verdana";
            this.textBox40.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox40.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox40.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox40.TextWrap = false;
            this.textBox40.Value = "= \"Página \" + PageNumber + \" de \" + PageCount";
            // 
            // textBox8
            // 
            this.textBox8.Format = "{0:N2}";
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.69999885559082D), Telerik.Reporting.Drawing.Unit.Cm(2.6994895935058594D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.499998539686203D));
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox8.Value = "=Fields.[Ped_Total]";
            // 
            // textBox25
            // 
            this.textBox25.Format = "{0:N2}";
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.69999885559082D), Telerik.Reporting.Drawing.Unit.Cm(0.19950008392333984D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.49999776482582092D));
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox25.Value = "=sum(Fields.[Prd_Importe])";
            // 
            // textBox26
            // 
            this.textBox26.Format = "{0:N2}";
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.69999885559082D), Telerik.Reporting.Drawing.Unit.Cm(2.199491024017334D));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.499998539686203D));
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox26.Value = "=Fields.[Ped_Iva]";
            // 
            // textBox27
            // 
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.5D), Telerik.Reporting.Drawing.Unit.Cm(0.69949781894683838D));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.79999899864196777D), Telerik.Reporting.Drawing.Unit.Cm(0.49999803304672241D));
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox27.Value = "= Fields.[Ped_DescPorcen1]";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.499998092651367D), Telerik.Reporting.Drawing.Unit.Cm(1.1994955539703369D));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.80000060796737671D), Telerik.Reporting.Drawing.Unit.Cm(0.49999803304672241D));
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox28.Value = "= Fields.[Ped_DescPorcen2]";
            // 
            // textBox29
            // 
            this.textBox29.CanGrow = false;
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.59999942779541D), Telerik.Reporting.Drawing.Unit.Cm(0.69949781894683838D));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8000001907348633D), Telerik.Reporting.Drawing.Unit.Cm(0.49999803304672241D));
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox29.TextWrap = false;
            this.textBox29.Value = "= Fields.[Ped_Desc1]";
            // 
            // textBox30
            // 
            this.textBox30.CanGrow = false;
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.59999942779541D), Telerik.Reporting.Drawing.Unit.Cm(1.1994955539703369D));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8000001907348633D), Telerik.Reporting.Drawing.Unit.Cm(0.49999803304672241D));
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox30.TextWrap = false;
            this.textBox30.Value = "= Fields.[Ped_Desc2]";
            // 
            // textBox31
            // 
            this.textBox31.CanGrow = false;
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.30000025033950806D), Telerik.Reporting.Drawing.Unit.Cm(0.49969977140426636D));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8D), Telerik.Reporting.Drawing.Unit.Cm(2.8000006675720215D));
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox31.Value = "= Fields.[Ped_Comentarios]";
            // 
            // textBox32
            // 
            this.textBox32.Format = "{0:N2}";
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.69999885559082D), Telerik.Reporting.Drawing.Unit.Cm(0.69949781894683838D));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.49999776482582092D));
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox32.Value = "=sum(Fields.[Prd_Importe]) * Fields.[Ped_DescPorcen1] /100";
            // 
            // textBox33
            // 
            this.textBox33.Format = "{0:N2}";
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.69999885559082D), Telerik.Reporting.Drawing.Unit.Cm(1.1994955539703369D));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.49999776482582092D));
            this.textBox33.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox33.Value = "=(sum(Fields.[Prd_Importe]) * (1-(Fields.[Ped_DescPorcen1] /100))) * (Fields.[Ped" +
    "_DescPorcen2] / 100)";
            // 
            // textBox34
            // 
            this.textBox34.Format = "{0:N2}";
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.69999885559082D), Telerik.Reporting.Drawing.Unit.Cm(1.6994932889938355D));
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.49999776482582092D));
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox34.Value = "= Fields.[Ped_Subtotal]";
            // 
            // textBox35
            // 
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.300199508666992D), Telerik.Reporting.Drawing.Unit.Cm(0.69949781894683838D));
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.39949372410774231D), Telerik.Reporting.Drawing.Unit.Cm(0.49999803304672241D));
            this.textBox35.Value = "%";
            // 
            // textBox36
            // 
            this.textBox36.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.299999237060547D), Telerik.Reporting.Drawing.Unit.Cm(1.1994955539703369D));
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.39949372410774231D), Telerik.Reporting.Drawing.Unit.Cm(0.49999803304672241D));
            this.textBox36.Value = "%";
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "Data Source=egbksvr;Initial Catalog=SIANWEBPruebas;Persist Security Info=True;Use" +
    "r ID=sa;Password=sistemas;Asynchronous Processing=True;Connect Timeout=600";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlConnection2
            // 
            this.sqlConnection2.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "dbo.spCapPedido_Impresion";
            this.sqlSelectCommand1.CommandTimeout = 600;
            this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand1.Connection = this.sqlConnection2;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, 0),
            new System.Data.SqlClient.SqlParameter("@Id_Emp", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, "1"),
            new System.Data.SqlClient.SqlParameter("@Id_Cd", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, "110"),
            new System.Data.SqlClient.SqlParameter("@Id_Ped", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, "121013")});
            // 
            // sqlDataAdapter1
            // 
            this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
            this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "spCapPedido_Impresion", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Id_PedDet", "Id_PedDet"),
                        new System.Data.Common.DataColumnMapping("Id_Ter", "Id_Ter"),
                        new System.Data.Common.DataColumnMapping("Ter_Nombre", "Ter_Nombre"),
                        new System.Data.Common.DataColumnMapping("Id_Prd", "Id_Prd"),
                        new System.Data.Common.DataColumnMapping("Prd_Descripcion", "Prd_Descripcion"),
                        new System.Data.Common.DataColumnMapping("Prd_Presentacion", "Prd_Presentacion"),
                        new System.Data.Common.DataColumnMapping("Prd_Unidad", "Prd_Unidad"),
                        new System.Data.Common.DataColumnMapping("Prd_Precio", "Prd_Precio"),
                        new System.Data.Common.DataColumnMapping("Prd_Cantidad", "Prd_Cantidad"),
                        new System.Data.Common.DataColumnMapping("Prd_Importe", "Prd_Importe"),
                        new System.Data.Common.DataColumnMapping("Ped_Fecha", "Ped_Fecha"),
                        new System.Data.Common.DataColumnMapping("Ped_FechaEntrega", "Ped_FechaEntrega"),
                        new System.Data.Common.DataColumnMapping("U_Nombre", "U_Nombre"),
                        new System.Data.Common.DataColumnMapping("Id_Ped", "Id_Ped"),
                        new System.Data.Common.DataColumnMapping("Cte_Nombre", "Cte_Nombre"),
                        new System.Data.Common.DataColumnMapping("Ped_Solicito", "Ped_Solicito"),
                        new System.Data.Common.DataColumnMapping("Id_Cd", "Id_Cd"),
                        new System.Data.Common.DataColumnMapping("Id_Ter_Ped", "Id_Ter_Ped"),
                        new System.Data.Common.DataColumnMapping("Id_Rik", "Id_Rik"),
                        new System.Data.Common.DataColumnMapping("Ped_CondEntrega", "Ped_CondEntrega"),
                        new System.Data.Common.DataColumnMapping("Cte_FacCalle", "Cte_FacCalle"),
                        new System.Data.Common.DataColumnMapping("Cte_FacNumero", "Cte_FacNumero"),
                        new System.Data.Common.DataColumnMapping("Cte_FacCp", "Cte_FacCp"),
                        new System.Data.Common.DataColumnMapping("Cte_FacColonia", "Cte_FacColonia"),
                        new System.Data.Common.DataColumnMapping("Cte_FacMunicipio", "Cte_FacMunicipio"),
                        new System.Data.Common.DataColumnMapping("Cte_FacEstado", "Cte_FacEstado"),
                        new System.Data.Common.DataColumnMapping("Cte_Calle", "Cte_Calle"),
                        new System.Data.Common.DataColumnMapping("Cte_Numero", "Cte_Numero"),
                        new System.Data.Common.DataColumnMapping("Cte_Cp", "Cte_Cp"),
                        new System.Data.Common.DataColumnMapping("Cte_Colonia", "Cte_Colonia"),
                        new System.Data.Common.DataColumnMapping("Cte_Municipio", "Cte_Municipio"),
                        new System.Data.Common.DataColumnMapping("Cte_Estado", "Cte_Estado"),
                        new System.Data.Common.DataColumnMapping("Ped_Subtotal", "Ped_Subtotal"),
                        new System.Data.Common.DataColumnMapping("Ped_Iva", "Ped_Iva"),
                        new System.Data.Common.DataColumnMapping("Ped_Total", "Ped_Total"),
                        new System.Data.Common.DataColumnMapping("Ped_Desc1", "Ped_Desc1"),
                        new System.Data.Common.DataColumnMapping("Ped_DescPorcen1", "Ped_DescPorcen1"),
                        new System.Data.Common.DataColumnMapping("Ped_Desc2", "Ped_Desc2"),
                        new System.Data.Common.DataColumnMapping("Ped_DescPorcen2", "Ped_DescPorcen2"),
                        new System.Data.Common.DataColumnMapping("Ped_Comentarios", "Ped_Comentarios"),
                        new System.Data.Common.DataColumnMapping("Ped_PedidoDel", "Ped_PedidoDel"),
                        new System.Data.Common.DataColumnMapping("Ped_OrdenEntrega", "Ped_OrdenEntrega"),
                        new System.Data.Common.DataColumnMapping("Ped_Requisicion", "Ped_Requisicion")})});
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(3.2997000217437744D);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox31,
            this.textBox25,
            this.textBox26,
            this.textBox27,
            this.textBox28,
            this.textBox29,
            this.textBox30,
            this.textBox8,
            this.textBox32,
            this.textBox33,
            this.textBox34,
            this.textBox35,
            this.textBox36});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // PedidoImpresion
            // 
            this.DataSource = this.sqlDataAdapter1;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.pageFooter,
            this.reportFooterSection1});
            this.Name = "PedidoImpresion";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "Conexion";
            reportParameter2.Name = "Id_Emp";
            reportParameter3.Name = "Id_Cd";
            reportParameter4.Name = "Id_Ped";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(20D);
            this.NeedDataSource += new System.EventHandler(this.PedidoImpresion_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlConnection sqlConnection2;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox35;
        private Telerik.Reporting.TextBox textBox36;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox38;
        private Telerik.Reporting.TextBox textBox39;
        private Telerik.Reporting.TextBox textBox40;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
    }
}