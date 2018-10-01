namespace LibreriaReportes
{
    partial class Rep_ProgramaReparto
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
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            this.groupHeaderSection2 = new Telerik.Reporting.GroupHeaderSection();
            this.groupFooterSection2 = new Telerik.Reporting.GroupFooterSection();
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
            this.TxtEncabezadoColumnaRuta = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaSecuencia = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaNoFactura = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaNoRemision = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaNoPedido = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaCodigo = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaNombreCliente = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaDireccionEntrega = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaMonto = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaFechaEntrega = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoColumnaFechaPedido = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaRuta = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaSecuencia = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaNoFactura = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaNoRemision = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaNoPedido = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaCodigo = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaNombreCliente = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaDireccionEntrega = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaMonto = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaFechaEntrega = new Telerik.Reporting.TextBox();
            this.TxtDetalleColumnaFechaPedido = new Telerik.Reporting.TextBox();

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
            this.sqlSelectCommand1.CommandText = "dbo.spRep_ProgramaReparto";
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
            this.TxtTituloPrincipal.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(26.899797439575195D), Telerik.Reporting.Drawing.Unit.Cm(0.599999725818634D));
            this.TxtTituloPrincipal.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtTituloPrincipal.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtTituloPrincipal.Style.Font.Name = "Verdana";
            this.TxtTituloPrincipal.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtTituloPrincipal.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtTituloPrincipal.Value = "Programa de Reparto";
            // 
            // TxtEncabezadoColumnaRuta
            // 
            this.TxtEncabezadoColumnaRuta.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(1.899999737739563D));
            this.TxtEncabezadoColumnaRuta.Name = "TxtEncabezadoColumnaRuta";
            this.TxtEncabezadoColumnaRuta.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.0999000072479248D), Telerik.Reporting.Drawing.Unit.Cm(1.1999002695083618D));
            this.TxtEncabezadoColumnaRuta.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtEncabezadoColumnaRuta.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtEncabezadoColumnaRuta.Style.Font.Name = "Verdana";
            this.TxtEncabezadoColumnaRuta.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtEncabezadoColumnaRuta.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtEncabezadoColumnaRuta.Value = "Ruta";
            // 
            // TxtEncabezadoColumnaSecuencia
            // 
            this.TxtEncabezadoColumnaSecuencia.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.1002000570297241D), Telerik.Reporting.Drawing.Unit.Cm(1.899999737739563D));
            this.TxtEncabezadoColumnaSecuencia.Name = "TxtEncabezadoColumnaSecuencia";
            this.TxtEncabezadoColumnaSecuencia.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(1.1999002695083618D));
            this.TxtEncabezadoColumnaSecuencia.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtEncabezadoColumnaSecuencia.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtEncabezadoColumnaSecuencia.Style.Font.Name = "Verdana";
            this.TxtEncabezadoColumnaSecuencia.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtEncabezadoColumnaSecuencia.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtEncabezadoColumnaSecuencia.Value = "Secuencia";
            // 
            // TxtEncabezadoColumnaNoFactura
            // 
            this.TxtEncabezadoColumnaNoFactura.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.9001998901367188D), Telerik.Reporting.Drawing.Unit.Cm(1.899999737739563D));
            this.TxtEncabezadoColumnaNoFactura.Name = "TxtEncabezadoColumnaNoFactura";
            this.TxtEncabezadoColumnaNoFactura.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(1.1999002695083618D));
            this.TxtEncabezadoColumnaNoFactura.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtEncabezadoColumnaNoFactura.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtEncabezadoColumnaNoFactura.Style.Font.Name = "Verdana";
            this.TxtEncabezadoColumnaNoFactura.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtEncabezadoColumnaNoFactura.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtEncabezadoColumnaNoFactura.Value = "No Factura";
            // 
            // TxtEncabezadoColumnaNoRemision
            // 
            this.TxtEncabezadoColumnaNoRemision.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.700200080871582D), Telerik.Reporting.Drawing.Unit.Cm(1.899999737739563D));
            this.TxtEncabezadoColumnaNoRemision.Name = "TxtEncabezadoColumnaNoRemision";
            this.TxtEncabezadoColumnaNoRemision.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(1.1999002695083618D));
            this.TxtEncabezadoColumnaNoRemision.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtEncabezadoColumnaNoRemision.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtEncabezadoColumnaNoRemision.Style.Font.Name = "Verdana";
            this.TxtEncabezadoColumnaNoRemision.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtEncabezadoColumnaNoRemision.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtEncabezadoColumnaNoRemision.Value = "No Remisión";
            // 
            // TxtEncabezadoColumnaNoPedido
            // 
            this.TxtEncabezadoColumnaNoPedido.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.5001997947692871D), Telerik.Reporting.Drawing.Unit.Cm(1.899999737739563D));
            this.TxtEncabezadoColumnaNoPedido.Name = "TxtEncabezadoColumnaNoPedido";
            this.TxtEncabezadoColumnaNoPedido.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(1.1999002695083618D));
            this.TxtEncabezadoColumnaNoPedido.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtEncabezadoColumnaNoPedido.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtEncabezadoColumnaNoPedido.Style.Font.Name = "Verdana";
            this.TxtEncabezadoColumnaNoPedido.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtEncabezadoColumnaNoPedido.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtEncabezadoColumnaNoPedido.Value = "No Pedido";
            // 
            // TxtEncabezadoColumnaCodigo
            // 
            this.TxtEncabezadoColumnaCodigo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.3001995086669922D), Telerik.Reporting.Drawing.Unit.Cm(1.899999737739563D));
            this.TxtEncabezadoColumnaCodigo.Name = "TxtEncabezadoColumnaCodigo";
            this.TxtEncabezadoColumnaCodigo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(1.1999002695083618D));
            this.TxtEncabezadoColumnaCodigo.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtEncabezadoColumnaCodigo.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtEncabezadoColumnaCodigo.Style.Font.Name = "Verdana";
            this.TxtEncabezadoColumnaCodigo.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtEncabezadoColumnaCodigo.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtEncabezadoColumnaCodigo.Value = "Codigo";
            // 
            // TxtEncabezadoColumnaNombreCliente
            // 
            this.TxtEncabezadoColumnaNombreCliente.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.100199699401856D), Telerik.Reporting.Drawing.Unit.Cm(1.899999737739563D));
            this.TxtEncabezadoColumnaNombreCliente.Name = "TxtEncabezadoColumnaNombreCliente";
            this.TxtEncabezadoColumnaNombreCliente.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.6997990608215332D), Telerik.Reporting.Drawing.Unit.Cm(1.1999002695083618D));
            this.TxtEncabezadoColumnaNombreCliente.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtEncabezadoColumnaNombreCliente.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtEncabezadoColumnaNombreCliente.Style.Font.Name = "Verdana";
            this.TxtEncabezadoColumnaNombreCliente.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtEncabezadoColumnaNombreCliente.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtEncabezadoColumnaNombreCliente.Value = "Nombre Cliente";
            // 
            // TxtEncabezadoColumnaDireccionEntrega
            // 
            this.TxtEncabezadoColumnaDireccionEntrega.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.800199508666992D), Telerik.Reporting.Drawing.Unit.Cm(1.899999737739563D));
            this.TxtEncabezadoColumnaDireccionEntrega.Name = "TxtEncabezadoColumnaDireccionEntrega";
            this.TxtEncabezadoColumnaDireccionEntrega.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.6996989250183105D), Telerik.Reporting.Drawing.Unit.Cm(1.1999002695083618D));
            this.TxtEncabezadoColumnaDireccionEntrega.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtEncabezadoColumnaDireccionEntrega.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtEncabezadoColumnaDireccionEntrega.Style.Font.Name = "Verdana";
            this.TxtEncabezadoColumnaDireccionEntrega.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtEncabezadoColumnaDireccionEntrega.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtEncabezadoColumnaDireccionEntrega.Value = "Dirección Entrega";
            // 
            // TxtEncabezadoColumnaMonto
            // 
            this.TxtEncabezadoColumnaMonto.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(21.500099182128906D), Telerik.Reporting.Drawing.Unit.Cm(1.899999737739563D));
            this.TxtEncabezadoColumnaMonto.Name = "TxtEncabezadoColumnaMonto";
            this.TxtEncabezadoColumnaMonto.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(1.1999002695083618D));
            this.TxtEncabezadoColumnaMonto.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtEncabezadoColumnaMonto.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtEncabezadoColumnaMonto.Style.Font.Name = "Verdana";
            this.TxtEncabezadoColumnaMonto.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtEncabezadoColumnaMonto.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtEncabezadoColumnaMonto.Value = "Monto";
            // 
            // TxtEncabezadoColumnaFechaEntrega
            // 
            this.TxtEncabezadoColumnaFechaEntrega.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(23.300098419189453D), Telerik.Reporting.Drawing.Unit.Cm(1.899999737739563D));
            this.TxtEncabezadoColumnaFechaEntrega.Name = "TxtEncabezadoColumnaFechaEntrega";
            this.TxtEncabezadoColumnaFechaEntrega.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(1.1999002695083618D));
            this.TxtEncabezadoColumnaFechaEntrega.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtEncabezadoColumnaFechaEntrega.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtEncabezadoColumnaFechaEntrega.Style.Font.Name = "Verdana";
            this.TxtEncabezadoColumnaFechaEntrega.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtEncabezadoColumnaFechaEntrega.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtEncabezadoColumnaFechaEntrega.Value = "Fecha de Promesa de Entrega";
            // 
            // TxtEncabezadoColumnaFechaPedido
            // 
            this.TxtEncabezadoColumnaFechaPedido.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(25.10009765625D), Telerik.Reporting.Drawing.Unit.Cm(1.899999737739563D));
            this.TxtEncabezadoColumnaFechaPedido.Name = "TxtEncabezadoColumnaFechaPedido";
            this.TxtEncabezadoColumnaFechaPedido.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(1.1999002695083618D));
            this.TxtEncabezadoColumnaFechaPedido.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.TxtEncabezadoColumnaFechaPedido.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtEncabezadoColumnaFechaPedido.Style.Font.Name = "Verdana";
            this.TxtEncabezadoColumnaFechaPedido.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtEncabezadoColumnaFechaPedido.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtEncabezadoColumnaFechaPedido.Value = "Fecha de Pedido";
            // 
            // TxtDetalleColumnaRuta
            // 
            this.TxtDetalleColumnaRuta.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtDetalleColumnaRuta.Name = "TxtDetalleColumnaRuta";
            this.TxtDetalleColumnaRuta.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.1000000238418579D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtDetalleColumnaRuta.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtDetalleColumnaRuta.Style.Font.Name = "Verdana";
            this.TxtDetalleColumnaRuta.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtDetalleColumnaRuta.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtDetalleColumnaRuta.Value = "";
            //this.TxtDetalleColumnaRuta.Value = "=Fields.[Ruta]";
            // 
            // TxtDetalleColumnaSecuencia
            // 
            this.TxtDetalleColumnaSecuencia.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.1002000570297241D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtDetalleColumnaSecuencia.Name = "TxtDetalleColumnaSecuencia";
            this.TxtDetalleColumnaSecuencia.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7997997999191284D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtDetalleColumnaSecuencia.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtDetalleColumnaSecuencia.Style.Font.Name = "Verdana";
            this.TxtDetalleColumnaSecuencia.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtDetalleColumnaSecuencia.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtDetalleColumnaSecuencia.Value = "";
            //this.TxtDetalleColumnaSecuencia.Value = "=Fields.[Secuencia]";
            // 
            // TxtDetalleColumnaNoFactura
            // 
            this.TxtDetalleColumnaNoFactura.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.9001998901367188D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtDetalleColumnaNoFactura.Name = "TxtDetalleColumnaNoFactura";
            this.TxtDetalleColumnaNoFactura.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7997997999191284D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtDetalleColumnaNoFactura.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtDetalleColumnaNoFactura.Style.Font.Name = "Verdana";
            this.TxtDetalleColumnaNoFactura.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtDetalleColumnaNoFactura.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtDetalleColumnaNoFactura.Value = "=Fields.[Id_Fac]";
            // 
            // TxtDetalleColumnaNoRemision
            // 
            this.TxtDetalleColumnaNoRemision.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.700200080871582D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtDetalleColumnaNoRemision.Name = "TxtDetalleColumnaNoRemision";
            this.TxtDetalleColumnaNoRemision.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7997997999191284D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtDetalleColumnaNoRemision.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtDetalleColumnaNoRemision.Style.Font.Name = "Verdana";
            this.TxtDetalleColumnaNoRemision.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtDetalleColumnaNoRemision.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtDetalleColumnaNoRemision.Value = "=Fields.[Id_Rem]";
            // 
            // TxtDetalleColumnaNoPedido
            // 
            this.TxtDetalleColumnaNoPedido.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.5001997947692871D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtDetalleColumnaNoPedido.Name = "TxtDetalleColumnaNoPedido";
            this.TxtDetalleColumnaNoPedido.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7997997999191284D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtDetalleColumnaNoPedido.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtDetalleColumnaNoPedido.Style.Font.Name = "Verdana";
            this.TxtDetalleColumnaNoPedido.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtDetalleColumnaNoPedido.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtDetalleColumnaNoPedido.Value = "=Fields.[Id_Ped]";
            // 
            // TxtDetalleColumnaCodigo
            // 
            this.TxtDetalleColumnaCodigo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.3001995086669922D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtDetalleColumnaCodigo.Name = "TxtDetalleColumnaCodigo";
            this.TxtDetalleColumnaCodigo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7997997999191284D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtDetalleColumnaCodigo.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtDetalleColumnaCodigo.Style.Font.Name = "Verdana";
            this.TxtDetalleColumnaCodigo.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtDetalleColumnaCodigo.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtDetalleColumnaCodigo.Value = "=Fields.[Codigo]";
            // 
            // TxtDetalleColumnaNombreCliente
            // 
            this.TxtDetalleColumnaNombreCliente.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.100199699401856D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtDetalleColumnaNombreCliente.Name = "TxtDetalleColumnaNombreCliente";
            this.TxtDetalleColumnaNombreCliente.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.6997990608215332D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtDetalleColumnaNombreCliente.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtDetalleColumnaNombreCliente.Style.Font.Name = "Verdana";
            this.TxtDetalleColumnaNombreCliente.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtDetalleColumnaNombreCliente.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtDetalleColumnaNombreCliente.Value = "=Fields.[Cte_NomComercial]";
            // 
            // TxtDetalleColumnaDireccionEntrega
            // 
            this.TxtDetalleColumnaDireccionEntrega.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.800199508666992D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtDetalleColumnaDireccionEntrega.Name = "TxtDetalleColumnaDireccionEntrega";
            this.TxtDetalleColumnaDireccionEntrega.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.6996989250183105D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtDetalleColumnaDireccionEntrega.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtDetalleColumnaDireccionEntrega.Style.Font.Name = "Verdana";
            this.TxtDetalleColumnaDireccionEntrega.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtDetalleColumnaDireccionEntrega.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtDetalleColumnaDireccionEntrega.Value = "=Fields.[Cte_DireccionEntrega]";
            // 
            // TxtDetalleColumnaMonto
            // 
            this.TxtDetalleColumnaMonto.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(21.500099182128906D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtDetalleColumnaMonto.Name = "TxtEncabezadoColumnaMonto";
            this.TxtDetalleColumnaMonto.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtDetalleColumnaMonto.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtDetalleColumnaMonto.Style.Font.Name = "Verdana";
            this.TxtDetalleColumnaMonto.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtDetalleColumnaMonto.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtDetalleColumnaMonto.Value = "=Fields.[Ped_Importe]";
            // 
            // TxtDetalleColumnaFechaEntrega
            // 
            this.TxtDetalleColumnaFechaEntrega.Format = "{0:d}";
            this.TxtDetalleColumnaFechaEntrega.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(23.300098419189453D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtDetalleColumnaFechaEntrega.Name = "TxtDetalleColumnaFechaEntrega";
            this.TxtDetalleColumnaFechaEntrega.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtDetalleColumnaFechaEntrega.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtDetalleColumnaFechaEntrega.Style.Font.Name = "Verdana";
            this.TxtDetalleColumnaFechaEntrega.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtDetalleColumnaFechaEntrega.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtDetalleColumnaFechaEntrega.Value = "=Fields.[Ped_FechaEntrega]";
            // 
            // TxtDetalleColumnaFechaPedido
            // 
            this.TxtDetalleColumnaFechaPedido.Format = "{0:d}";
            this.TxtDetalleColumnaFechaPedido.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(25.10009765625D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TxtDetalleColumnaFechaPedido.Name = "TxtDetalleColumnaFechaPedido";
            this.TxtDetalleColumnaFechaPedido.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.799799919128418D), Telerik.Reporting.Drawing.Unit.Cm(0.43552190065383911D));
            this.TxtDetalleColumnaFechaPedido.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.TxtDetalleColumnaFechaPedido.Style.Font.Name = "Verdana";
            this.TxtDetalleColumnaFechaPedido.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.TxtDetalleColumnaFechaPedido.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtDetalleColumnaFechaPedido.Value = "=Fields.[Ped_Fecha]";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(3.1000001430511475D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TxtTituloPrincipal,
            this.TxtEncabezadoColumnaRuta,
            this.TxtEncabezadoColumnaSecuencia,
            this.TxtEncabezadoColumnaNoFactura,
            this.TxtEncabezadoColumnaNoRemision,
            this.TxtEncabezadoColumnaNoPedido,
            this.TxtEncabezadoColumnaCodigo,
            this.TxtEncabezadoColumnaNombreCliente,
            this.TxtEncabezadoColumnaDireccionEntrega,
            this.TxtEncabezadoColumnaMonto,
            this.TxtEncabezadoColumnaFechaEntrega,
            this.TxtEncabezadoColumnaFechaPedido});
            this.pageHeader.Name = "pageHeader";
            // 
            // reportHeaderSection
            // 
            this.reportHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(3.1000001430511475D);
            this.reportHeaderSection.Name = "reportHeaderSection";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.43562242388725281D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TxtDetalleColumnaRuta,
            this.TxtDetalleColumnaSecuencia,
            this.TxtDetalleColumnaNoFactura,
            this.TxtDetalleColumnaNoRemision,
            this.TxtDetalleColumnaNoPedido,
            this.TxtDetalleColumnaCodigo,
            this.TxtDetalleColumnaNombreCliente,
            this.TxtDetalleColumnaDireccionEntrega,
            this.TxtDetalleColumnaMonto,
            this.TxtDetalleColumnaFechaEntrega,
            this.TxtDetalleColumnaFechaPedido});
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
            this.TxtPieEtiquetaFirma2});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // ShpPieLinea1
            // 
            this.ShpPieLinea1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.9999995231628418D), Telerik.Reporting.Drawing.Unit.Cm(0.66437721252441406D));
            this.ShpPieLinea1.Name = "ShpPieLinea1";
            this.ShpPieLinea1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.ShpPieLinea1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.3997998237609863D), Telerik.Reporting.Drawing.Unit.Cm(0.46427720785140991D));
            // 
            // ShpPieLinea2
            // 
            this.ShpPieLinea2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.5D), Telerik.Reporting.Drawing.Unit.Cm(0.66437721252441406D));
            this.ShpPieLinea2.Name = "ShpPieLinea2";
            this.ShpPieLinea2.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.ShpPieLinea2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.3997998237609863D), Telerik.Reporting.Drawing.Unit.Cm(0.46427720785140991D));
            // 
            // TxtPieEtiquetaFirma1
            // 
            this.TxtPieEtiquetaFirma1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.9999995231628418D), Telerik.Reporting.Drawing.Unit.Cm(1.1288546323776245D));
            this.TxtPieEtiquetaFirma1.Name = "TxtPieEtiquetaFirma1";
            this.TxtPieEtiquetaFirma1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.3997993469238281D), Telerik.Reporting.Drawing.Unit.Cm(0.9999995231628418D));
            this.TxtPieEtiquetaFirma1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtPieEtiquetaFirma1.Value = "Firma Almacenista y/o Operador de Reparto";
            // 
            // TxtPieEtiquetaFirma2
            // 
            this.TxtPieEtiquetaFirma2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.5D), Telerik.Reporting.Drawing.Unit.Cm(1.1288546323776245D));
            this.TxtPieEtiquetaFirma2.Name = "TxtPieEtiquetaFirma2";
            this.TxtPieEtiquetaFirma2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.3997993469238281D), Telerik.Reporting.Drawing.Unit.Cm(1.0000003576278687D));
            this.TxtPieEtiquetaFirma2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtPieEtiquetaFirma2.Value = "Firma Coordinador de Almacen y Reparto";
            // 
            // Rep_ProgramaReparto
            // 
            this.groupHeaderSection2.Height = Telerik.Reporting.Drawing.Unit.Cm(0D);
            
            this.groupHeaderSection2.Name = "groupHeaderSection2";
            // 
            // groupFooterSection2
            // 
            this.groupFooterSection2.Height = Telerik.Reporting.Drawing.Unit.Cm(0D);
            
            this.groupFooterSection2.Name = "groupFooterSection2";

            group1.BookmarkId = null;
            group1.GroupFooter = this.groupFooterSection2;
            group1.GroupHeader = this.groupHeaderSection2;
            group1.GroupHeader.PageBreak = Telerik.Reporting.PageBreak.After;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.[Ruta]"));
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});

            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.pageFooter,
            this.reportFooterSection1});
            this.Name = "Rep_FacFacturasCanceladas";
            this.PageSettings.Landscape = true;
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(26.899999618530273D);
            this.NeedDataSource += new System.EventHandler(this.Rep_ProgramaReparto_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.ReportHeaderSection reportHeaderSection;

        private Telerik.Reporting.GroupHeaderSection groupHeaderSection2;
        private Telerik.Reporting.GroupFooterSection groupFooterSection2;

        private Telerik.Reporting.TextBox TxtTituloPrincipal;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaRuta;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaSecuencia;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaNoFactura;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaNoRemision;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaNoPedido;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaCodigo;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaNombreCliente;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaDireccionEntrega;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaMonto;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaFechaEntrega;
        private Telerik.Reporting.TextBox TxtEncabezadoColumnaFechaPedido;

        private Telerik.Reporting.TextBox TxtDetalleColumnaRuta;
        private Telerik.Reporting.TextBox TxtDetalleColumnaSecuencia;
        private Telerik.Reporting.TextBox TxtDetalleColumnaNoFactura;
        private Telerik.Reporting.TextBox TxtDetalleColumnaNoRemision;
        private Telerik.Reporting.TextBox TxtDetalleColumnaNoPedido;
        private Telerik.Reporting.TextBox TxtDetalleColumnaCodigo;
        private Telerik.Reporting.TextBox TxtDetalleColumnaNombreCliente;
        private Telerik.Reporting.TextBox TxtDetalleColumnaDireccionEntrega;
        private Telerik.Reporting.TextBox TxtDetalleColumnaMonto;
        private Telerik.Reporting.TextBox TxtDetalleColumnaFechaEntrega;
        private Telerik.Reporting.TextBox TxtDetalleColumnaFechaPedido;

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
