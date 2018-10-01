namespace LibreriaReportes
{
    partial class Rep_PickingListConcentrado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
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
            Telerik.Reporting.ReportParameter reportParameter13 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter14 = new Telerik.Reporting.ReportParameter();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.sqlConnection2 = new System.Data.SqlClient.SqlConnection();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            this.TxtTituloPrincipal = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaCodigo = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaDescripcion = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaPresentacion = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaUM = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaPiezasFacturadas = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaPiezasAsignadas = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaPiezasEncontradas = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaPiezasNoConformes = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaPiezasNoEncontradas = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaCodigo = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaDescripcion = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaPresentacion = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaUM = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaPiezasFacturadas = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaPiezasAsignadas = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaPiezasEncontradas = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaPiezasNoConformes = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaPiezasNoEncontradas = new Telerik.Reporting.TextBox();
            this.TxtPieEtiquetaTotal = new Telerik.Reporting.TextBox();
            this.TxtPieTotalPiezasAsignadas = new Telerik.Reporting.TextBox();
            this.TxtPieTotalPiezasEncontradas = new Telerik.Reporting.TextBox();
            this.TxtPieTotalPiezasNoConformes = new Telerik.Reporting.TextBox();
            this.TxtPieTotalPiezasNoEncontradas = new Telerik.Reporting.TextBox();
            this.ShpPieLinea1 = new Telerik.Reporting.Shape();
            this.ShpPieLinea2 = new Telerik.Reporting.Shape();
            this.TxtPieEtiquetaFirma1 = new Telerik.Reporting.TextBox();
            this.TxtPieEtiquetaFirma2 = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.reportHeaderSection = new Telerik.Reporting.ReportHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlConnection2
            // 
            this.sqlConnection2.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "dbo.spRep_PickingListConcentrado";
            this.sqlSelectCommand1.CommandTimeout = 600;
            this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand1.Connection = this.sqlConnection1;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, 0),
            new System.Data.SqlClient.SqlParameter("@Id_Emp", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, ""),
            new System.Data.SqlClient.SqlParameter("@Id_Cd", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, ""),
            new System.Data.SqlClient.SqlParameter("@Filtro_FecIni", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, "2011-01-01"),
            new System.Data.SqlClient.SqlParameter("@Filtro_FecFin", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, "2011-07-07"),
            new System.Data.SqlClient.SqlParameter("@Filtro_PedIni", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Filtro_PedFin", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Filtro_CteIni", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Filtro_CteFin", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Filtro_SectorIni", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Filtro_SectorFin", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Filtro_RutaIni", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Filtro_RutaFin", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Filtro_Ids", System.Data.SqlDbType.NVarChar, 1000)});
            // 
            // sqlDataAdapter1
            // 
            this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
            // 
            // TxtTituloPrincipal
            // 
            this.TxtTituloPrincipal.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010047038085758686D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TxtTituloPrincipal.Name = "TxtTituloPrincipal";
            this.TxtTituloPrincipal.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.199905395507812D), Telerik.Reporting.Drawing.Unit.Cm(0.599999725818634D));
            this.TxtTituloPrincipal.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtTituloPrincipal.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtTituloPrincipal.Style.Font.Bold = true;
            this.TxtTituloPrincipal.Style.Font.Name = "Verdana";
            this.TxtTituloPrincipal.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.TxtTituloPrincipal.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtTituloPrincipal.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.TxtTituloPrincipal.Value = "Picking List Concentrado";
            // 
            // TxtEncabezadoColumnaCodigo
            // 
            this.TxtEncabezadoColumnaCodigo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(0.39999979734420776D));
            this.TxtEncabezadoColumnaCodigo.Name = "TxtEncabezadoColumnaCodigo";
            this.TxtEncabezadoColumnaCodigo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.799899697303772D), Telerik.Reporting.Drawing.Unit.Cm(0.799899697303772D));
            this.TxtEncabezadoColumnaCodigo.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtEncabezadoColumnaCodigo.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtEncabezadoColumnaCodigo.Style.Font.Bold = true;
            this.TxtEncabezadoColumnaCodigo.Style.Font.Name = "Verdana";
            this.TxtEncabezadoColumnaCodigo.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtEncabezadoColumnaCodigo.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtEncabezadoColumnaCodigo.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.TxtEncabezadoColumnaCodigo.Value = "Código";
            // 
            // TxtEncabezadoColumnaDescripcion
            // 
            this.TxtEncabezadoColumnaDescripcion.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.8001998662948608D), Telerik.Reporting.Drawing.Unit.Cm(0.39999979734420776D));
            this.TxtEncabezadoColumnaDescripcion.Name = "TxtEncabezadoColumnaDescripcion";
            this.TxtEncabezadoColumnaDescripcion.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.099599838256836D), Telerik.Reporting.Drawing.Unit.Cm(0.799899697303772D));
            this.TxtEncabezadoColumnaDescripcion.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtEncabezadoColumnaDescripcion.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtEncabezadoColumnaDescripcion.Style.Font.Bold = true;
            this.TxtEncabezadoColumnaDescripcion.Style.Font.Name = "Verdana";
            this.TxtEncabezadoColumnaDescripcion.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtEncabezadoColumnaDescripcion.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtEncabezadoColumnaDescripcion.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.TxtEncabezadoColumnaDescripcion.Value = "Descripción";
            // 
            // TxtEncabezadoColumnaPresentacion
            // 
            this.TxtEncabezadoColumnaPresentacion.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.899999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0.39999979734420776D));
            this.TxtEncabezadoColumnaPresentacion.Name = "TxtEncabezadoColumnaPresentacion";
            this.TxtEncabezadoColumnaPresentacion.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0998001098632812D), Telerik.Reporting.Drawing.Unit.Cm(0.799899697303772D));
            this.TxtEncabezadoColumnaPresentacion.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtEncabezadoColumnaPresentacion.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtEncabezadoColumnaPresentacion.Style.Font.Bold = true;
            this.TxtEncabezadoColumnaPresentacion.Style.Font.Name = "Verdana";
            this.TxtEncabezadoColumnaPresentacion.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtEncabezadoColumnaPresentacion.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtEncabezadoColumnaPresentacion.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.TxtEncabezadoColumnaPresentacion.Value = "Presentación";
            // 
            // TxtEncabezadoColumnaUM
            // 
            this.TxtEncabezadoColumnaUM.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.39999979734420776D));
            this.TxtEncabezadoColumnaUM.Name = "TxtEncabezadoColumnaUM";
            this.TxtEncabezadoColumnaUM.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9998005628585815D), Telerik.Reporting.Drawing.Unit.Cm(0.799899697303772D));
            this.TxtEncabezadoColumnaUM.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtEncabezadoColumnaUM.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtEncabezadoColumnaUM.Style.Font.Bold = true;
            this.TxtEncabezadoColumnaUM.Style.Font.Name = "Verdana";
            this.TxtEncabezadoColumnaUM.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtEncabezadoColumnaUM.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtEncabezadoColumnaUM.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.TxtEncabezadoColumnaUM.Value = "UM";
            // 
            // TxtEncabezadoColumnaPiezasFacturadas
            // 
            this.TxtEncabezadoColumnaPiezasFacturadas.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16D), Telerik.Reporting.Drawing.Unit.Cm(0.39999979734420776D));
            this.TxtEncabezadoColumnaPiezasFacturadas.Name = "TxtEncabezadoColumnaPiezasFacturadas";
            this.TxtEncabezadoColumnaPiezasFacturadas.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1999990940093994D), Telerik.Reporting.Drawing.Unit.Cm(0.79989951848983765D));
            this.TxtEncabezadoColumnaPiezasFacturadas.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtEncabezadoColumnaPiezasFacturadas.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtEncabezadoColumnaPiezasFacturadas.Style.Font.Bold = true;
            this.TxtEncabezadoColumnaPiezasFacturadas.Style.Font.Name = "Verdana";
            this.TxtEncabezadoColumnaPiezasFacturadas.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtEncabezadoColumnaPiezasFacturadas.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtEncabezadoColumnaPiezasFacturadas.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.TxtEncabezadoColumnaPiezasFacturadas.Value = "Pzas Facturadas";
            // 
            // TxtEncabezadoColumnaPiezasAsignadas
            // 
            this.TxtEncabezadoColumnaPiezasAsignadas.Name = "TxtEncabezadoColumnaPiezasAsignadas";
            // 
            // TxtEncabezadoColumnaPiezasEncontradas
            // 
            this.TxtEncabezadoColumnaPiezasEncontradas.Name = "TxtEncabezadoColumnaPiezasEncontradas";
            // 
            // TxtEncabezadoColumnaPiezasNoConformes
            // 
            this.TxtEncabezadoColumnaPiezasNoConformes.Name = "TxtEncabezadoColumnaPiezasNoConformes";
            // 
            // TxtEncabezadoColumnaPiezasNoEncontradas
            // 
            this.TxtEncabezadoColumnaPiezasNoEncontradas.Name = "TxtEncabezadoColumnaPiezasNoEncontradas";
            // 
            // TxtDetalleColumnaCodigo
            // 
            this.TxtDetalleColumnaCodigo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtDetalleColumnaCodigo.Name = "TxtDetalleColumnaCodigo";
            this.TxtDetalleColumnaCodigo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.799899697303772D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtDetalleColumnaCodigo.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtDetalleColumnaCodigo.Style.Font.Name = "Verdana";
            this.TxtDetalleColumnaCodigo.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtDetalleColumnaCodigo.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtDetalleColumnaCodigo.Value = "=Fields.[Id_Prd]";
            // 
            // TxtDetalleColumnaDescripcion
            // 
            this.TxtDetalleColumnaDescripcion.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.80020010471344D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtDetalleColumnaDescripcion.Name = "TxtDetalleColumnaDescripcion";
            this.TxtDetalleColumnaDescripcion.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.099599838256836D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtDetalleColumnaDescripcion.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtDetalleColumnaDescripcion.Style.Font.Name = "Verdana";
            this.TxtDetalleColumnaDescripcion.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtDetalleColumnaDescripcion.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtDetalleColumnaDescripcion.TextWrap = false;
            this.TxtDetalleColumnaDescripcion.Value = "=Fields.[Prd_Descripcion]";
            // 
            // TxtDetalleColumnaPresentacion
            // 
            this.TxtDetalleColumnaPresentacion.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.899999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtDetalleColumnaPresentacion.Name = "TxtDetalleColumnaPresentacion";
            this.TxtDetalleColumnaPresentacion.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0998001098632812D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtDetalleColumnaPresentacion.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtDetalleColumnaPresentacion.Style.Font.Name = "Verdana";
            this.TxtDetalleColumnaPresentacion.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtDetalleColumnaPresentacion.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtDetalleColumnaPresentacion.Value = "=Fields.[Prd_Presentacion]";
            // 
            // TxtDetalleColumnaUM
            // 
            this.TxtDetalleColumnaUM.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtDetalleColumnaUM.Name = "TxtDetalleColumnaUM";
            this.TxtDetalleColumnaUM.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9998005628585815D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtDetalleColumnaUM.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtDetalleColumnaUM.Style.Font.Name = "Verdana";
            this.TxtDetalleColumnaUM.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtDetalleColumnaUM.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtDetalleColumnaUM.Value = "=Fields.[Prd_UniNe]";
            // 
            // TxtDetalleColumnaPiezasFacturadas
            // 
            this.TxtDetalleColumnaPiezasFacturadas.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtDetalleColumnaPiezasFacturadas.Name = "TxtDetalleColumnaPiezasFacturadas";
            this.TxtDetalleColumnaPiezasFacturadas.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1999990940093994D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtDetalleColumnaPiezasFacturadas.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtDetalleColumnaPiezasFacturadas.Style.Font.Name = "Verdana";
            this.TxtDetalleColumnaPiezasFacturadas.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtDetalleColumnaPiezasFacturadas.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtDetalleColumnaPiezasFacturadas.Value = "=Fields.[Ped_Asignar]";
            // 
            // TxtDetalleColumnaPiezasAsignadas
            // 
            this.TxtDetalleColumnaPiezasAsignadas.Name = "TxtDetalleColumnaPiezasAsignadas";
            // 
            // TxtDetalleColumnaPiezasEncontradas
            // 
            this.TxtDetalleColumnaPiezasEncontradas.Name = "TxtDetalleColumnaPiezasEncontradas";
            // 
            // TxtDetalleColumnaPiezasNoConformes
            // 
            this.TxtDetalleColumnaPiezasNoConformes.Name = "TxtDetalleColumnaPiezasNoConformes";
            // 
            // TxtDetalleColumnaPiezasNoEncontradas
            // 
            this.TxtDetalleColumnaPiezasNoEncontradas.Name = "TxtDetalleColumnaPiezasNoEncontradas";
            // 
            // TxtPieEtiquetaTotal
            // 
            this.TxtPieEtiquetaTotal.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.40000057220459D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtPieEtiquetaTotal.Name = "TxtPieEtiquetaTotal";
            this.TxtPieEtiquetaTotal.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5997998714447021D), Telerik.Reporting.Drawing.Unit.Cm(0.43552213907241821D));
            this.TxtPieEtiquetaTotal.Style.Font.Bold = true;
            this.TxtPieEtiquetaTotal.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtPieEtiquetaTotal.Value = "Total General: ";
            // 
            // TxtPieTotalPiezasAsignadas
            // 
            this.TxtPieTotalPiezasAsignadas.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.0000057220459D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtPieTotalPiezasAsignadas.Name = "TxtPieTotalPiezasAsignadas";
            this.TxtPieTotalPiezasAsignadas.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1999990940093994D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtPieTotalPiezasAsignadas.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtPieTotalPiezasAsignadas.Style.Font.Bold = true;
            this.TxtPieTotalPiezasAsignadas.Style.Font.Name = "Verdana";
            this.TxtPieTotalPiezasAsignadas.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtPieTotalPiezasAsignadas.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtPieTotalPiezasAsignadas.Value = "=Sum(Fields.[Ped_Asignar])";
            // 
            // TxtPieTotalPiezasEncontradas
            // 
            this.TxtPieTotalPiezasEncontradas.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.800200462341309D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtPieTotalPiezasEncontradas.Name = "TxtPieTotalPiezasEncontradas";
            this.TxtPieTotalPiezasEncontradas.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1999990940093994D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtPieTotalPiezasEncontradas.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtPieTotalPiezasEncontradas.Style.Font.Bold = true;
            this.TxtPieTotalPiezasEncontradas.Style.Font.Name = "Verdana";
            this.TxtPieTotalPiezasEncontradas.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtPieTotalPiezasEncontradas.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtPieTotalPiezasEncontradas.Value = "=Sum(Fields.[Ped_Encontrado])";
            // 
            // TxtPieTotalPiezasNoConformes
            // 
            this.TxtPieTotalPiezasNoConformes.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.000400543212891D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtPieTotalPiezasNoConformes.Name = "TxtPieTotalPiezasNoConformes";
            this.TxtPieTotalPiezasNoConformes.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1999990940093994D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtPieTotalPiezasNoConformes.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtPieTotalPiezasNoConformes.Style.Font.Bold = true;
            this.TxtPieTotalPiezasNoConformes.Style.Font.Name = "Verdana";
            this.TxtPieTotalPiezasNoConformes.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtPieTotalPiezasNoConformes.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtPieTotalPiezasNoConformes.Value = "=Sum(Fields.[Prd_NoConf])";
            // 
            // TxtPieTotalPiezasNoEncontradas
            // 
            this.TxtPieTotalPiezasNoEncontradas.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.200599670410156D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtPieTotalPiezasNoEncontradas.Name = "TxtPieTotalPiezasNoEncontradas";
            this.TxtPieTotalPiezasNoEncontradas.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1999990940093994D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtPieTotalPiezasNoEncontradas.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtPieTotalPiezasNoEncontradas.Style.Font.Bold = true;
            this.TxtPieTotalPiezasNoEncontradas.Style.Font.Name = "Verdana";
            this.TxtPieTotalPiezasNoEncontradas.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtPieTotalPiezasNoEncontradas.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtPieTotalPiezasNoEncontradas.Value = "=Sum(Fields.[Prd_NoEnc])";
            // 
            // ShpPieLinea1
            // 
            this.ShpPieLinea1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.2000001668930054D), Telerik.Reporting.Drawing.Unit.Cm(1.0998997688293457D));
            this.ShpPieLinea1.Name = "ShpPieLinea1";
            this.ShpPieLinea1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.ShpPieLinea1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.9999990463256836D), Telerik.Reporting.Drawing.Unit.Cm(0.46427720785140991D));
            // 
            // ShpPieLinea2
            // 
            this.ShpPieLinea2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.200000762939453D), Telerik.Reporting.Drawing.Unit.Cm(1.0998997688293457D));
            this.ShpPieLinea2.Name = "ShpPieLinea2";
            this.ShpPieLinea2.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.ShpPieLinea2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.0000004768371582D), Telerik.Reporting.Drawing.Unit.Cm(0.46427720785140991D));
            // 
            // TxtPieEtiquetaFirma1
            // 
            this.TxtPieEtiquetaFirma1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.2000001668930054D), Telerik.Reporting.Drawing.Unit.Cm(1.5643771886825562D));
            this.TxtPieEtiquetaFirma1.Name = "TxtPieEtiquetaFirma1";
            this.TxtPieEtiquetaFirma1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.9999995231628418D), Telerik.Reporting.Drawing.Unit.Cm(0.9999995231628418D));
            this.TxtPieEtiquetaFirma1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtPieEtiquetaFirma1.Value = "Firma Almacenista y/u Operador de Reparto";
            // 
            // TxtPieEtiquetaFirma2
            // 
            this.TxtPieEtiquetaFirma2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.200000762939453D), Telerik.Reporting.Drawing.Unit.Cm(1.5643771886825562D));
            this.TxtPieEtiquetaFirma2.Name = "TxtPieEtiquetaFirma2";
            this.TxtPieEtiquetaFirma2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6D), Telerik.Reporting.Drawing.Unit.Cm(1.0000003576278687D));
            this.TxtPieEtiquetaFirma2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtPieEtiquetaFirma2.Value = "Firma Coordinador de Almacen y Reparto";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(1.4000002145767212D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TxtTituloPrincipal});
            this.pageHeader.Name = "pageHeader";
            // 
            // reportHeaderSection
            // 
            this.reportHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1999998092651367D);
            this.reportHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TxtEncabezadoColumnaCodigo,
            this.TxtEncabezadoColumnaDescripcion,
            this.TxtEncabezadoColumnaPresentacion,
            this.TxtEncabezadoColumnaUM,
            this.TxtEncabezadoColumnaPiezasFacturadas,
            this.TxtEncabezadoColumnaPiezasAsignadas,
            this.TxtEncabezadoColumnaPiezasEncontradas,
            this.TxtEncabezadoColumnaPiezasNoConformes,
            this.TxtEncabezadoColumnaPiezasNoEncontradas});
            this.reportHeaderSection.Name = "reportHeaderSection";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.43562242388725281D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TxtDetalleColumnaCodigo,
            this.TxtDetalleColumnaDescripcion,
            this.TxtDetalleColumnaPresentacion,
            this.TxtDetalleColumnaUM,
            this.TxtDetalleColumnaPiezasFacturadas,
            this.TxtDetalleColumnaPiezasAsignadas,
            this.TxtDetalleColumnaPiezasEncontradas,
            this.TxtDetalleColumnaPiezasNoConformes,
            this.TxtDetalleColumnaPiezasNoEncontradas});
            this.detail.Name = "detail";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144D);
            this.pageFooter.Name = "pageFooter";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(2.600100040435791D);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.ShpPieLinea1,
            this.ShpPieLinea2,
            this.TxtPieEtiquetaFirma1,
            this.TxtPieEtiquetaFirma2,
            this.TxtPieEtiquetaTotal,
            this.TxtPieTotalPiezasAsignadas});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // Rep_PickingListConcentrado
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.reportHeaderSection,
            this.reportFooterSection1});
            this.Name = "Rep_PickingListConcentrado";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "Id_Emp";
            reportParameter2.Name = "Id_Cd_Ver";
            reportParameter3.Name = "Conexion";
            reportParameter4.Name = "PedidoInicial";
            reportParameter5.Name = "PedidoFinal";
            reportParameter6.Name = "FechaInicial";
            reportParameter7.Name = "FechaFinal";
            reportParameter8.Name = "ClienteInicial";
            reportParameter9.Name = "ClienteFinal";
            reportParameter10.Name = "SectorInicial";
            reportParameter11.Name = "SectorFinal";
            reportParameter12.Name = "RutaInicial";
            reportParameter13.Name = "RutaFinal";
            reportParameter14.Name = "Ids";
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
            this.ReportParameters.Add(reportParameter13);
            this.ReportParameters.Add(reportParameter14);
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(18.200006484985352D);
            this.NeedDataSource += new System.EventHandler(this.Rep_PickingListConcentrado_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.ReportHeaderSection reportHeaderSection;

        private Telerik.Reporting.TextBox TxtTituloPrincipal;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaCodigo;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaDescripcion;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaPresentacion;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaUM;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaPiezasFacturadas;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaPiezasAsignadas;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaPiezasEncontradas;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaPiezasNoConformes;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaPiezasNoEncontradas;

        private Telerik.Reporting.TextBox TxtDetalleColumnaCodigo;
        private Telerik.Reporting.TextBox TxtDetalleColumnaDescripcion;
        private Telerik.Reporting.TextBox TxtDetalleColumnaPresentacion;
        private Telerik.Reporting.TextBox TxtDetalleColumnaUM;
        private Telerik.Reporting.TextBox TxtDetalleColumnaPiezasFacturadas;
        private Telerik.Reporting.TextBox TxtDetalleColumnaPiezasAsignadas;
        private Telerik.Reporting.TextBox TxtDetalleColumnaPiezasEncontradas;
        private Telerik.Reporting.TextBox TxtDetalleColumnaPiezasNoConformes;
        private Telerik.Reporting.TextBox TxtDetalleColumnaPiezasNoEncontradas;

        private Telerik.Reporting.TextBox TxtPieEtiquetaTotal;
        private Telerik.Reporting.TextBox TxtPieTotalPiezasAsignadas;
        private Telerik.Reporting.TextBox TxtPieTotalPiezasEncontradas;
        private Telerik.Reporting.TextBox TxtPieTotalPiezasNoConformes;
        private Telerik.Reporting.TextBox TxtPieTotalPiezasNoEncontradas;
        private Telerik.Reporting.Shape ShpPieLinea1;
        private Telerik.Reporting.Shape ShpPieLinea2;
        private Telerik.Reporting.TextBox TxtPieEtiquetaFirma1;
        private Telerik.Reporting.TextBox TxtPieEtiquetaFirma2;

        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlConnection sqlConnection2;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
    }
}
