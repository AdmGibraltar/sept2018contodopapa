using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using CapaEntidad;
using CapaNegocios;
using System.Configuration;
using Telerik.Web.UI;
using System.Runtime.InteropServices;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Globalization;


namespace ReportesCentral
{

    public partial class _Default : System.Web.UI.Page
    {
        private string Emp_CnxCen
        {
            get { return ConfigurationManager.AppSettings.Get("strConnection"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
          
           
            if (!Page.IsPostBack)
            {
                CargarListadoReportes();
                CargarListadoAnio();
                CargarListadoMes();
            }
        }

        private static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }


        private static ExcelWorksheet CreateSheet(ExcelPackage p, string sheetName, int index)
        {
            p.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet ws = p.Workbook.Worksheets[index];
            ws.Name = sheetName; //Setting Sheet's name
            ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
            ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

            return ws;
        }


        private static ExcelWorksheet CreateSheet2(ExcelPackage p, string sheetName)
        {
            p.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet ws = p.Workbook.Worksheets[2];
            ws.Name = sheetName; //Setting Sheet's name
            ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
            ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

            return ws;
        }


        private static void SetWorkbookProperties(ExcelPackage p)
        {
            //Here setting some document properties
            p.Workbook.Properties.Author = "Rafael Mejía";
            p.Workbook.Properties.Title = "Reportes Centrales";


        }


        private void ExportarExcesoInventario()
        {
            System.IO.StreamWriter sw = null;
            string ruta = null;
            Random rnd = new Random();

            int nro = rnd.Next(0, 8);
            string tipo = "ExcesoInventario";
            ruta = Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xlsx";

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xls"))
                File.Delete(Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xls");



            List<RepExcesos> List = new List<RepExcesos>();
            RepExcesos exceso = new RepExcesos();
            CN_Rep_InvExceso CN_InvExceso = new CN_Rep_InvExceso();


            exceso.Id_Emp = 1;
            exceso.Id_Cd = 110;
            exceso.Id_U = 2;
            exceso.Indicador = 0;
            exceso.Proveedor = -1;
            exceso.Centro = -1;
            exceso.Dias = 30;
            exceso.Tproducto = -1;
            exceso.ProveedorVer = -1;
            exceso.DiasVer = -1;
            exceso.Salida = 3;
            exceso.Ano = Int32.Parse(RadComboAno.SelectedValue);
            exceso.Mes = Int32.Parse(RadComboMes.SelectedValue);
            
            CN_InvExceso.Consulta3(exceso, Emp_CnxCen, ref  List);
            

            if ((List != null))
            {
                if (!(List.Count() == 0))
                {


                    using (ExcelPackage p = new ExcelPackage())
                    {

                        //set the workbook properties and add a default sheet in it
                        SetWorkbookProperties(p);
                        //Create a sheet
                        

                        List<int> lCDI = List.Select(i => i.Id_Cd).Distinct().ToList();                        
                        Dictionary<int, double> lCDTOTAL = new Dictionary<int, double>();
                        int count = 1;

                        foreach (int a in lCDI)
                        {
                            DataTable dataTable = new DataTable();
                            DataColumn dcCDS = new DataColumn("Id_Cd", Type.GetType("System.String"));
                            DataColumn dcProveedor = new DataColumn("Id_Pvd", Type.GetType("System.String"));
                            DataColumn dcClave = new DataColumn("Id_Prd", Type.GetType("System.String"));
                            DataColumn dcArticulo = new DataColumn("Prd_Descripcion", Type.GetType("System.String"));
                            DataColumn dcCosto = new DataColumn("Prd_Costo", Type.GetType("System.Double"));
                            DataColumn dcCantidad = new DataColumn("Prd_Cantidad", Type.GetType("System.Int32"));
                            DataColumn dcDisponible = new DataColumn("Disponible", Type.GetType("System.Int32"));

                            dataTable.Columns.Add(dcCDS);
                            dataTable.Columns.Add(dcProveedor);
                            dataTable.Columns.Add(dcClave);
                            dataTable.Columns.Add(dcArticulo);
                            dataTable.Columns.Add(dcCosto);
                            dataTable.Columns.Add(dcCantidad);
                            dataTable.Columns.Add(dcDisponible);
                            ExcelWorksheet ws = CreateSheet(p, getCDIName(a), count);

                            double TOTAL = 0;
                            foreach (RepExcesos exc in List.FindAll(b => b.Id_Cd == a))
                            {                            
                                DataRow drFila = null;
                                drFila = dataTable.NewRow();
                                drFila["Id_Cd"] = exc.Id_Cd;
                                drFila["Id_Pvd"] = exc.Id_Pvd;
                                drFila["Id_Prd"] = exc.Id_Prd;
                                drFila["Prd_Descripcion"] = exc.Prd_Descripcion;
                                drFila["Prd_Costo"] = exc.Costo;
                                drFila["Prd_Cantidad"] = exc.Exceso;
                                drFila["Disponible"] = exc.Disponible;

                            
                                dataTable.Rows.Add(drFila);
                                dataTable.AcceptChanges();
                                TOTAL = TOTAL + exc.Costo;

                            }
                            lCDTOTAL.Add(a, Math.Round(TOTAL,2));


                            //Merging cells and create a center heading for out table
                            ws.Cells[1, 1].Value = "REPORTE EXCESO DE INVENTARIO QUE NO ROTA";
                            ws.Cells[1, 1, 1, dataTable.Columns.Count].Merge = true;
                            ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.Font.Bold = true;
                            ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            ws.Cells[2, 1].Value = "Costo de inventario de los Proveedores del centro " + a + " en los días Todos.";
                      
                            int rowIndex = 4;

                            CreateHeader(ws, ref rowIndex, dataTable);
                            CreateData(ws, ref rowIndex, dataTable);
                           // CreateFooter(ws, ref rowIndex, dt);*/

                            count++;
                        }


                        ExcelWorksheet wsGral = CreateSheet(p, "GENERAL", lCDTOTAL.Count() + 1);


                        DataTable dataTableGral = new DataTable();
                        DataColumn dcCDSGral = new DataColumn("Id_Cd", Type.GetType("System.String"));
                        DataColumn dcProveedorGral = new DataColumn("Id_Pvd", Type.GetType("System.String"));
                        DataColumn dcClaveGral = new DataColumn("Id_Prd", Type.GetType("System.String"));
                        DataColumn dcArticuloGral = new DataColumn("Prd_Descripcion", Type.GetType("System.String"));
                        DataColumn dcCostoGral = new DataColumn("Prd_Costo", Type.GetType("System.Double"));
                        DataColumn dcCantidadGral = new DataColumn("Prd_Cantidad", Type.GetType("System.Int32"));
                        DataColumn dcDisponibleGral = new DataColumn("Disponible", Type.GetType("System.Int32"));



                        dataTableGral.Columns.Add(dcCDSGral);
                        dataTableGral.Columns.Add(dcProveedorGral);
                        dataTableGral.Columns.Add(dcClaveGral);
                        dataTableGral.Columns.Add(dcArticuloGral);
                        dataTableGral.Columns.Add(dcCostoGral);
                        dataTableGral.Columns.Add(dcCantidadGral);
                        dataTableGral.Columns.Add(dcDisponibleGral);



                        //Merging cells and create a center heading for out table
                        wsGral.Cells[1, 1].Value = "REPORTE EXCESO DE INVENTARIO QUE NO ROTA";
                        wsGral.Cells[1, 1, 1, 4].Merge = true;
                        wsGral.Cells[1, 1, 1, 4].Style.Font.Bold = true;
                        wsGral.Cells[1, 1, 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        wsGral.Cells[2, 1].Value = "Costo de inventario de los Proveedores de Todos los centros  en los días Todos.";

                        
                        foreach (RepExcesos exc in List)
                        {
                            DataRow drFila = null;
                            drFila = dataTableGral.NewRow();
                            drFila["Id_Cd"] = exc.Id_Cd;
                            drFila["Id_Pvd"] = exc.Id_Pvd;
                            drFila["Id_Prd"] = exc.Id_Prd;
                            drFila["Prd_Descripcion"] = exc.Prd_Descripcion;
                            drFila["Prd_Costo"] = exc.Costo;
                            drFila["Prd_Cantidad"] = exc.Exceso;
                            drFila["Disponible"] = exc.Disponible;

                            dataTableGral.Rows.Add(drFila);
                            dataTableGral.AcceptChanges();

                        }

                        int rowIndex1 = 4;
                        CreateHeader(wsGral, ref rowIndex1, dataTableGral);
                        CreateData(wsGral, ref rowIndex1, dataTableGral);
                        

                        ExcelWorksheet wsResumen = CreateSheet(p, "RESUMEN", lCDTOTAL.Count() +2 );



                        //Merging cells and create a center heading for out table
                        wsResumen.Cells[1, 1].Value = "REPORTE EXCESO DE INVENTARIO QUE NO ROTA";
                        wsResumen.Cells[1, 1, 1, 4].Merge = true;
                        wsResumen.Cells[1, 1, 1, 4].Style.Font.Bold = true;
                        wsResumen.Cells[1, 1, 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        wsResumen.Cells[2, 1].Value = "Costo de inventario de los Proveedores de Todos los centros  en los días Todos.";

                        DataTable dataTableResumen = new DataTable();
                        DataColumn dcCDSResumen = new DataColumn("CDI", Type.GetType("System.String"));
                        DataColumn dcExcesoResumen = new DataColumn("Exceso Inv. que No Rota", Type.GetType("System.String"));
                        DataColumn dcMetaResumen = new DataColumn("Meta Rotación de Inventario", Type.GetType("System.String"));
                        DataColumn dcDiasResumen = new DataColumn("Dias que Afecta la Rotación de Inventarios", Type.GetType("System.String"));

                       
                        dataTableResumen.Columns.Add(dcCDSResumen);
                        dataTableResumen.Columns.Add(dcExcesoResumen);
                        dataTableResumen.Columns.Add(dcMetaResumen);
                        dataTableResumen.Columns.Add(dcDiasResumen);

                        int rowIndex2 = 4;
                        CreateHeader(wsResumen, ref rowIndex2, dataTableResumen);
                       
                        wsResumen.Cells[5, 2].Value = "MONTERREY";
                        wsResumen.Cells[5, 2].Style.Font.Bold = true;
                        wsResumen.Cells[5, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 110).Value;

                        wsResumen.Cells[6, 2].Value = "SALTILLO";
                        wsResumen.Cells[6, 2].Style.Font.Bold = true;
                        wsResumen.Cells[6, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 150).Value;

                        wsResumen.Cells[7, 2].Value = "MATAMOROS";
                        wsResumen.Cells[7, 2].Style.Font.Bold = true;
                        wsResumen.Cells[7, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 160).Value;

                        wsResumen.Cells[8, 2].Value = "TORREON";
                        wsResumen.Cells[8, 2].Style.Font.Bold = true;
                        wsResumen.Cells[8, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 170).Value;

                        wsResumen.Cells[9, 2].Value = "LAREDO";
                        wsResumen.Cells[9, 2].Style.Font.Bold = true;
                        wsResumen.Cells[9, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 180).Value;

                        wsResumen.Cells[10, 2].Value = "LEON";
                        wsResumen.Cells[10, 2].Style.Font.Bold = true;
                        wsResumen.Cells[10, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 190).Value;

                        wsResumen.Cells[11, 2].Value = "TIJUANA";
                        wsResumen.Cells[11, 2].Style.Font.Bold = true;
                        wsResumen.Cells[11, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 200).Value;

                        wsResumen.Cells[12, 2].Value = "CHIHUAHUA";
                        wsResumen.Cells[12, 2].Style.Font.Bold = true;
                        wsResumen.Cells[12, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 210).Value;

                        wsResumen.Cells[13, 2].Value = "SAN LUIS";
                        wsResumen.Cells[13, 2].Style.Font.Bold = true;
                        wsResumen.Cells[13, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 220).Value;

                        wsResumen.Cells[14, 2].Value = "JUAREZ";
                        wsResumen.Cells[14, 2].Style.Font.Bold = true;
                        wsResumen.Cells[14, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 230).Value;

                        wsResumen.Cells[15, 2].Value = "AGUASCALIENTES";
                        wsResumen.Cells[15, 2].Style.Font.Bold = true;
                        wsResumen.Cells[15, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 240).Value;

                        wsResumen.Cells[16, 2].Value = "MEXICO";
                        wsResumen.Cells[16, 2].Style.Font.Bold = true;
                        wsResumen.Cells[16, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 310).Value;

                        wsResumen.Cells[17, 2].Value = "VERACRUZ";
                        wsResumen.Cells[17, 2].Style.Font.Bold = true;
                        wsResumen.Cells[17, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 340).Value;

                        wsResumen.Cells[18, 2].Value = "CD CARMEN";
                        wsResumen.Cells[18, 2].Style.Font.Bold = true;
                        wsResumen.Cells[18, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 350).Value;

                        wsResumen.Cells[19, 2].Value = "MERIDA";
                        wsResumen.Cells[19, 2].Style.Font.Bold = true;
                        wsResumen.Cells[19, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 360).Value;

                        wsResumen.Cells[20, 2].Value = "CANCUN";
                        wsResumen.Cells[20, 2].Style.Font.Bold = true;
                        wsResumen.Cells[20, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 370).Value;

                        wsResumen.Cells[21, 2].Value = "RIVIERA";
                        wsResumen.Cells[21, 2].Style.Font.Bold = true;
                        wsResumen.Cells[21, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 380).Value;

                        wsResumen.Cells[22, 2].Value = "VALLARTA";
                        wsResumen.Cells[22, 2].Style.Font.Bold = true;
                        wsResumen.Cells[22, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 390).Value;

                        wsResumen.Cells[23, 2].Value = "LOS CABOS";
                        wsResumen.Cells[23, 2].Style.Font.Bold = true;
                        wsResumen.Cells[23, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 400).Value;


                        wsResumen.Cells[24, 2].Value = "QRO";
                        wsResumen.Cells[24, 2].Style.Font.Bold = true;
                        wsResumen.Cells[24, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 410).Value;

                        wsResumen.Cells[25, 2].Value = "GDL";
                        wsResumen.Cells[25, 2].Style.Font.Bold = true;
                        wsResumen.Cells[25, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 430).Value;

                        wsResumen.Cells[26, 2].Value = "PUEBLA";
                        wsResumen.Cells[26, 2].Style.Font.Bold = true;
                        wsResumen.Cells[26, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 510).Value;


                        wsResumen.Cells[27, 2].Value = "COATZACOALCOS";
                        wsResumen.Cells[27, 2].Style.Font.Bold = true;
                        wsResumen.Cells[27, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 610).Value;


                        wsResumen.Cells[28, 2].Value = "VILLAHERMOSA";
                        wsResumen.Cells[28, 2].Style.Font.Bold = true;
                        wsResumen.Cells[28, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 620).Value;

                        wsResumen.Cells[29, 2].Value = "TOLUCA";
                        wsResumen.Cells[29, 2].Style.Font.Bold = true;
                        wsResumen.Cells[29, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Key == 640).Value;


                         ExcelWorksheet wsCDI = CreateSheet(p, "CDI'S", lCDTOTAL.Count() + 3 );



                        //Merging cells and create a center heading for out table
                        wsCDI.Cells[1, 1].Value = "CENTROS DE DISTRIBUCION";
                        wsCDI.Cells[1, 1, 1, 4].Merge = true;
                        wsCDI.Cells[1, 1, 1, 4].Style.Font.Bold = true;
                        wsCDI.Cells[1, 1, 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                       

                        DataTable dataTableCDI = new DataTable();
                        DataColumn dcCDSCDI = new DataColumn("CODIGO", Type.GetType("System.String"));
                        DataColumn dcExcesoCDI = new DataColumn("CDS", Type.GetType("System.String"));



                        dataTableCDI.Columns.Add(dcCDSCDI);
                        dataTableCDI.Columns.Add(dcExcesoCDI);
                        

                        int rowIndex3 = 4;
                        CreateHeader(wsCDI, ref rowIndex3, dataTableCDI);

                        wsCDI.Cells[5, 2].Value = "110";
                        wsCDI.Cells[5, 3].Style.Font.Bold = true;
                        wsCDI.Cells[5, 3].Value = "MONTERREY";

                        wsCDI.Cells[6, 2].Value = "150";
                        wsCDI.Cells[6, 3].Style.Font.Bold = true;
                        wsCDI.Cells[6, 3].Value = "SALTILLO";

                        wsCDI.Cells[7, 2].Value = "160";
                        wsCDI.Cells[7, 3].Style.Font.Bold = true;
                        wsCDI.Cells[7, 3].Value = "MATAMOROS";

                        wsCDI.Cells[8, 2].Value = "170";
                        wsCDI.Cells[8, 3].Style.Font.Bold = true;
                        wsCDI.Cells[8, 3].Value = "TORREON";

                        wsCDI.Cells[9, 2].Value = "180";
                        wsCDI.Cells[9, 3].Style.Font.Bold = true;
                        wsCDI.Cells[9, 3].Value = "LAREDO";

                        wsCDI.Cells[10, 2].Value = "190";
                        wsCDI.Cells[10, 3].Style.Font.Bold = true;
                        wsCDI.Cells[10, 3].Value = "LEON";

                        wsCDI.Cells[11, 2].Value = "200";
                        wsCDI.Cells[11, 3].Style.Font.Bold = true;
                        wsCDI.Cells[11, 3].Value = "TIJUANA";

                        wsCDI.Cells[12, 2].Value = "210";
                        wsCDI.Cells[12, 3].Style.Font.Bold = true;
                        wsCDI.Cells[12, 3].Value = "CHIHUAHUA";

                        wsCDI.Cells[13, 2].Value = "220";
                        wsCDI.Cells[13, 3].Style.Font.Bold = true;
                        wsCDI.Cells[13, 3].Value = "SAN LUIS";

                        wsCDI.Cells[14, 2].Value = "230";
                        wsCDI.Cells[14, 3].Style.Font.Bold = true;
                        wsCDI.Cells[14, 3].Value = "JUAREZ";

                        wsCDI.Cells[15, 2].Value = "240";
                        wsCDI.Cells[15, 3].Style.Font.Bold = true;
                        wsCDI.Cells[15, 3].Value = "AGUASCALIENTES";

                        wsCDI.Cells[16, 2].Value = "310";
                        wsCDI.Cells[16, 3].Style.Font.Bold = true;
                        wsCDI.Cells[16, 3].Value = "MEXICO";

                        wsCDI.Cells[17, 2].Value = "340";
                        wsCDI.Cells[17, 3].Style.Font.Bold = true;
                        wsCDI.Cells[17, 3].Value = "VERACRUZ";

                        wsCDI.Cells[18, 2].Value = "350";
                        wsCDI.Cells[18, 3].Style.Font.Bold = true;
                        wsCDI.Cells[18, 3].Value = "CD CARMEN";

                        wsCDI.Cells[19, 2].Value = "360";
                        wsCDI.Cells[19, 3].Style.Font.Bold = true;
                        wsCDI.Cells[19, 3].Value = "MERIDA";

                        wsCDI.Cells[20, 2].Value = "370";
                        wsCDI.Cells[20, 3].Style.Font.Bold = true;
                        wsCDI.Cells[20, 3].Value = "CANCUN";

                        wsCDI.Cells[21, 2].Value = "380";
                        wsCDI.Cells[21, 3].Style.Font.Bold = true;
                        wsCDI.Cells[21, 3].Value = "RIVIERA";

                        wsCDI.Cells[22, 2].Value = "390";
                        wsCDI.Cells[22, 3].Style.Font.Bold = true;
                        wsCDI.Cells[22, 3].Value = "VALLARTA";

                        wsCDI.Cells[23, 2].Value = "400";
                        wsCDI.Cells[23, 3].Style.Font.Bold = true;
                        wsCDI.Cells[23, 3].Value = "LOS CABOS";


                        wsCDI.Cells[24, 2].Value = "410";
                        wsCDI.Cells[24, 3].Style.Font.Bold = true;
                        wsCDI.Cells[24, 3].Value = "QUERETARO";

                        wsCDI.Cells[25, 2].Value = "430";
                        wsCDI.Cells[25, 3].Style.Font.Bold = true;
                        wsCDI.Cells[25, 3].Value = "GUADALAJARA";

                        wsCDI.Cells[26, 2].Value = "510";
                        wsCDI.Cells[26, 3].Style.Font.Bold = true;
                        wsCDI.Cells[26, 3].Value = "PUEBLA";


                        wsCDI.Cells[27, 2].Value = "610";
                        wsCDI.Cells[27, 3].Style.Font.Bold = true;
                        wsCDI.Cells[27, 3].Value = "COATZACOALCOS";


                        wsCDI.Cells[28, 2].Value = "620";
                        wsCDI.Cells[28, 3].Style.Font.Bold = true;
                        wsCDI.Cells[28, 3].Value = "VILLAHERMOSA";

                        wsCDI.Cells[29, 2].Value = "640";
                        wsCDI.Cells[29, 3].Style.Font.Bold = true;
                        wsCDI.Cells[29, 3].Value = "TOLUCA";


                        Byte[] bin = p.GetAsByteArray();
                        File.WriteAllBytes(ruta, bin);


                        if (File.Exists(ruta))
                        {
                            string ruta2 = null;
                            ruta2 = Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xlsx";
                            // File.Move(ruta, Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xls");
                            Response.Redirect("Reportes\\Reporte" + tipo + nro + ".xlsx", false);
                        }

                    }  
                   
                }

            }
        }

        
        private void ExportarEstadisticaVenta()
        {
            System.IO.StreamWriter sw = null;
            string ruta = null;
            Random rnd = new Random();

            int nro = rnd.Next(0, 8);
            string tipo = "EstadisticaVenta";
            ruta = Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xlsx";

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xls"))
                File.Delete(Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xls");



            List<VenEstadisticaVentas> List = new List<VenEstadisticaVentas>();
            VenEstadisticaVentas ventas = new VenEstadisticaVentas();
            CN_RepVentas CN_RepVentas = new CN_RepVentas();


            ventas.Id_Emp = 1;
            ventas.Anio = Int32.Parse(RadComboAno.SelectedValue);
            ventas.Mes = Int32.Parse(RadComboMes.SelectedValue); ; 



            CN_RepVentas.Consulta(ventas, Emp_CnxCen, ref  List);


            if ((List != null))
            {
                if (!(List.Count() == 0))
                {


                    using (ExcelPackage p = new ExcelPackage())
                    {

                        //set the workbook properties and add a default sheet in it
                        SetWorkbookProperties(p);
                        //Create a sheet


                        List<int> lCDI = List.Select(i => i.Id_Cd).Distinct().ToList();
                        Dictionary<int, double> lCDTOTAL = new Dictionary<int, double>();
                        int count = 1;
                        /*
                        foreach (int a in lCDI)
                        {
                            DataTable dataTable = new DataTable();
                            DataColumn dcCDS = new DataColumn("Id_Cd", Type.GetType("System.String"));
                            DataColumn dcPrd= new DataColumn("Id_Prd", Type.GetType("System.String"));
                            DataColumn dcPrd_Descripcion = new DataColumn("Prd_Descripcion", Type.GetType("System.String"));
                            DataColumn dcEne = new DataColumn("Ene", Type.GetType("System.Double"));
                            DataColumn dcFeb = new DataColumn("Feb", Type.GetType("System.Double"));
                            DataColumn dcMar = new DataColumn("Mar", Type.GetType("System.Double"));
                            DataColumn dcAbr = new DataColumn("Abr", Type.GetType("System.Double"));
                            DataColumn dcMay = new DataColumn("May", Type.GetType("System.Double"));
                            DataColumn dcJun = new DataColumn("Jun", Type.GetType("System.Double"));
                            DataColumn dcJul = new DataColumn("Jul", Type.GetType("System.Double"));
                            DataColumn dcAgo = new DataColumn("Ago", Type.GetType("System.Double"));
                            DataColumn dcSep = new DataColumn("Sep", Type.GetType("System.Double"));
                            DataColumn dcOct = new DataColumn("Oct", Type.GetType("System.Double"));
                            DataColumn dcNov = new DataColumn("Nov", Type.GetType("System.Double"));
                            DataColumn dcDic = new DataColumn("Dic", Type.GetType("System.Double"));
                            DataColumn dcTotal = new DataColumn("Total", Type.GetType("System.Double"));
                        
                            dataTable.Columns.Add(dcCDS);
                            dataTable.Columns.Add(dcPrd);
                            dataTable.Columns.Add(dcPrd_Descripcion);
                            dataTable.Columns.Add(dcEne);
                            dataTable.Columns.Add(dcFeb);
                            dataTable.Columns.Add(dcMar);
                            dataTable.Columns.Add(dcAbr);
                            dataTable.Columns.Add(dcMay);
                            dataTable.Columns.Add(dcJun);
                            dataTable.Columns.Add(dcJul);
                            dataTable.Columns.Add(dcAgo);
                            dataTable.Columns.Add(dcSep);
                            dataTable.Columns.Add(dcOct);
                            dataTable.Columns.Add(dcNov);
                            dataTable.Columns.Add(dcDic);
                            dataTable.Columns.Add(dcTotal);
                            ExcelWorksheet ws = CreateSheet(p, getCDIName(a), count);

                        double TOTAL = 0;
                        foreach (VenEstadisticaVentas ven in List.FindAll(b => b.Id_Cd == a))
                        {
                            DataRow drFila = null;
                            drFila = dataTable.NewRow();
                            drFila["Id_Cd"] = ven.Id_Cd;
                            drFila["Id_Prd"] = ven.Id_Prd;
                            drFila["Prd_Descripcion"] = ven.Prd_Descripcion;
                            drFila["Ene"] = ven.Ene;
                            drFila["Feb"] = ven.Feb;
                            drFila["Mar"] = ven.Mar;
                            drFila["Abr"] = ven.Abr;
                            drFila["May"] = ven.May;
                            drFila["Jun"] = ven.Jun;
                            drFila["Jul"] = ven.Jul;
                            drFila["Ago"] = ven.Ago;
                            drFila["Sep"] = ven.Sep;
                            drFila["Oct"] = ven.Oct;
                            drFila["Nov"] = ven.Nov;
                            drFila["Dic"] = ven.Dic;
                            drFila["Total"] = ven.Total;



                            dataTable.Rows.Add(drFila);
                            dataTable.AcceptChanges();
                        }

                       

                            //Merging cells and create a center heading for out table
                            ws.Cells[1, 1].Value = "REPORTE ESTADISTICA DE VENTA";
                            ws.Cells[1, 1, 1, dataTable.Columns.Count].Merge = true;
                            ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.Font.Bold = true;
                            ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                               
                            int rowIndex = 4;

                            CreateHeader(ws, ref rowIndex, dataTable);
                            CreateData(ws, ref rowIndex, dataTable);
                            // CreateFooter(ws, ref rowIndex, dt);

                            count++;
                        }
                        */

                        ExcelWorksheet wsGral = CreateSheet(p, "GENERAL",  1);

                        DataTable dataTableGral = new DataTable();
                        DataColumn dcCDSGral = new DataColumn("Id_Cd", Type.GetType("System.String"));
                        DataColumn dcPrdGral = new DataColumn("Id_Prd", Type.GetType("System.String"));
                        DataColumn dcPrd_DescripcionGral = new DataColumn("Prd_Descripcion", Type.GetType("System.String"));
                        DataColumn dcEneGral = new DataColumn("Ene", Type.GetType("System.Double"));
                        DataColumn dcFebGral = new DataColumn("Feb", Type.GetType("System.Double"));
                        DataColumn dcMarGral = new DataColumn("Mar", Type.GetType("System.Double"));
                        DataColumn dcAbrGral = new DataColumn("Abr", Type.GetType("System.Double"));
                        DataColumn dcMayGral = new DataColumn("May", Type.GetType("System.Double"));
                        DataColumn dcJunGral = new DataColumn("Jun", Type.GetType("System.Double"));
                        DataColumn dcJulGral = new DataColumn("Jul", Type.GetType("System.Double"));
                        DataColumn dcAgoGral = new DataColumn("Ago", Type.GetType("System.Double"));
                        DataColumn dcSepGral = new DataColumn("Sep", Type.GetType("System.Double"));
                        DataColumn dcOctGral = new DataColumn("Oct", Type.GetType("System.Double"));
                        DataColumn dcNovGral = new DataColumn("Nov", Type.GetType("System.Double"));
                        DataColumn dcDicGral = new DataColumn("Dic", Type.GetType("System.Double"));
                        DataColumn dcTotalGral = new DataColumn("Total", Type.GetType("System.Double"));


                        dataTableGral.Columns.Add(dcCDSGral);
                        dataTableGral.Columns.Add(dcPrdGral);
                        dataTableGral.Columns.Add(dcPrd_DescripcionGral);
                        dataTableGral.Columns.Add(dcEneGral);
                        dataTableGral.Columns.Add(dcFebGral);
                        dataTableGral.Columns.Add(dcMarGral);
                        dataTableGral.Columns.Add(dcAbrGral);
                        dataTableGral.Columns.Add(dcMayGral);
                        dataTableGral.Columns.Add(dcJunGral);
                        dataTableGral.Columns.Add(dcJulGral);
                        dataTableGral.Columns.Add(dcAgoGral);
                        dataTableGral.Columns.Add(dcSepGral);
                        dataTableGral.Columns.Add(dcOctGral);
                        dataTableGral.Columns.Add(dcNovGral);
                        dataTableGral.Columns.Add(dcDicGral);
                        dataTableGral.Columns.Add(dcTotalGral);



                        //Merging cells and create a center heading for out table
                        wsGral.Cells[1, 1].Value = "REPORTE ESTADISTICA DE VENTA";
                        wsGral.Cells[1, 1, 1, dataTableGral.Columns.Count].Merge = true;
                        wsGral.Cells[1, 1, 1, dataTableGral.Columns.Count].Style.Font.Bold = true;
                        wsGral.Cells[1, 1, 1, dataTableGral.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                              



                        foreach (VenEstadisticaVentas ven in List)
                        {
                            DataRow drFila = null;
                            drFila = dataTableGral.NewRow();
                            drFila["Id_Cd"] = ven.Id_Cd;
                            drFila["Id_Prd"] = ven.Id_Prd;
                            drFila["Prd_Descripcion"] = ven.Prd_Descripcion;
                            drFila["Ene"] = ven.Ene;
                            drFila["Feb"] = ven.Feb;
                            drFila["Mar"] = ven.Mar;
                            drFila["Abr"] = ven.Abr;
                            drFila["May"] = ven.May;
                            drFila["Jun"] = ven.Jun;
                            drFila["Jul"] = ven.Jul;
                            drFila["Ago"] = ven.Ago;
                            drFila["Sep"] = ven.Sep;
                            drFila["Oct"] = ven.Oct;
                            drFila["Nov"] = ven.Nov;
                            drFila["Dic"] = ven.Dic;
                            drFila["Total"] = ven.Total;

                            dataTableGral.Rows.Add(drFila);
                            dataTableGral.AcceptChanges();

                        }


                        int rowIndex1 = 4;
                        CreateHeader(wsGral, ref rowIndex1, dataTableGral);
                        CreateData(wsGral, ref rowIndex1, dataTableGral);



                        ExcelWorksheet wsCDI = CreateSheet(p, "CDI'S", 2);



                        //Merging cells and create a center heading for out table
                        wsCDI.Cells[1, 1].Value = "CENTROS DE DISTRIBUCION";
                        wsCDI.Cells[1, 1, 1, 4].Merge = true;
                        wsCDI.Cells[1, 1, 1, 4].Style.Font.Bold = true;
                        wsCDI.Cells[1, 1, 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                       

                        DataTable dataTableCDI = new DataTable();
                        DataColumn dcCDSCDI = new DataColumn("CODIGO", Type.GetType("System.String"));
                        DataColumn dcExcesoCDI = new DataColumn("CDS", Type.GetType("System.String"));



                        dataTableCDI.Columns.Add(dcCDSCDI);
                        dataTableCDI.Columns.Add(dcExcesoCDI);
                        

                        int rowIndex2 = 4;
                        CreateHeader(wsCDI, ref rowIndex2, dataTableCDI);

                        wsCDI.Cells[5, 2].Value = "110";
                        wsCDI.Cells[5, 3].Style.Font.Bold = true;
                        wsCDI.Cells[5, 3].Value = "MONTERREY";

                        wsCDI.Cells[6, 2].Value = "150";
                        wsCDI.Cells[6, 3].Style.Font.Bold = true;
                        wsCDI.Cells[6, 3].Value = "SALTILLO";

                        wsCDI.Cells[7, 2].Value = "160";
                        wsCDI.Cells[7, 3].Style.Font.Bold = true;
                        wsCDI.Cells[7, 3].Value = "MATAMOROS";

                        wsCDI.Cells[8, 2].Value = "170";
                        wsCDI.Cells[8, 3].Style.Font.Bold = true;
                        wsCDI.Cells[8, 3].Value = "TORREON";

                        wsCDI.Cells[9, 2].Value = "180";
                        wsCDI.Cells[9, 3].Style.Font.Bold = true;
                        wsCDI.Cells[9, 3].Value = "LAREDO";

                        wsCDI.Cells[10, 2].Value = "190";
                        wsCDI.Cells[10, 3].Style.Font.Bold = true;
                        wsCDI.Cells[10, 3].Value = "LEON";

                        wsCDI.Cells[11, 2].Value = "200";
                        wsCDI.Cells[11, 3].Style.Font.Bold = true;
                        wsCDI.Cells[11, 3].Value = "TIJUANA";

                        wsCDI.Cells[12, 2].Value = "210";
                        wsCDI.Cells[12, 3].Style.Font.Bold = true;
                        wsCDI.Cells[12, 3].Value = "CHIHUAHUA";

                        wsCDI.Cells[13, 2].Value = "220";
                        wsCDI.Cells[13, 3].Style.Font.Bold = true;
                        wsCDI.Cells[13, 3].Value = "SAN LUIS";

                        wsCDI.Cells[14, 2].Value = "230";
                        wsCDI.Cells[14, 3].Style.Font.Bold = true;
                        wsCDI.Cells[14, 3].Value = "JUAREZ";

                        wsCDI.Cells[15, 2].Value = "240";
                        wsCDI.Cells[15, 3].Style.Font.Bold = true;
                        wsCDI.Cells[15, 3].Value = "AGUASCALIENTES";

                        wsCDI.Cells[16, 2].Value = "310";
                        wsCDI.Cells[16, 3].Style.Font.Bold = true;
                        wsCDI.Cells[16, 3].Value = "MEXICO";

                        wsCDI.Cells[17, 2].Value = "340";
                        wsCDI.Cells[17, 3].Style.Font.Bold = true;
                        wsCDI.Cells[17, 3].Value = "VERACRUZ";

                        wsCDI.Cells[18, 2].Value = "350";
                        wsCDI.Cells[18, 3].Style.Font.Bold = true;
                        wsCDI.Cells[18, 3].Value = "CD CARMEN";

                        wsCDI.Cells[19, 2].Value = "360";
                        wsCDI.Cells[19, 3].Style.Font.Bold = true;
                        wsCDI.Cells[19, 3].Value = "MERIDA";

                        wsCDI.Cells[20, 2].Value = "370";
                        wsCDI.Cells[20, 3].Style.Font.Bold = true;
                        wsCDI.Cells[20, 3].Value = "CANCUN";

                        wsCDI.Cells[21, 2].Value = "380";
                        wsCDI.Cells[21, 3].Style.Font.Bold = true;
                        wsCDI.Cells[21, 3].Value = "RIVIERA";

                        wsCDI.Cells[22, 2].Value = "390";
                        wsCDI.Cells[22, 3].Style.Font.Bold = true;
                        wsCDI.Cells[22, 3].Value = "VALLARTA";

                        wsCDI.Cells[23, 2].Value = "400";
                        wsCDI.Cells[23, 3].Style.Font.Bold = true;
                        wsCDI.Cells[23, 3].Value = "LOS CABOS";


                        wsCDI.Cells[24, 2].Value = "410";
                        wsCDI.Cells[24, 3].Style.Font.Bold = true;
                        wsCDI.Cells[24, 3].Value = "QUERETARO";

                        wsCDI.Cells[25, 2].Value = "430";
                        wsCDI.Cells[25, 3].Style.Font.Bold = true;
                        wsCDI.Cells[25, 3].Value = "GUADALAJARA";

                        wsCDI.Cells[26, 2].Value = "510";
                        wsCDI.Cells[26, 3].Style.Font.Bold = true;
                        wsCDI.Cells[26, 3].Value = "PUEBLA";


                        wsCDI.Cells[27, 2].Value = "610";
                        wsCDI.Cells[27, 3].Style.Font.Bold = true;
                        wsCDI.Cells[27, 3].Value = "COATZACOALCOS";


                        wsCDI.Cells[28, 2].Value = "620";
                        wsCDI.Cells[28, 3].Style.Font.Bold = true;
                        wsCDI.Cells[28, 3].Value = "VILLAHERMOSA";

                        wsCDI.Cells[29, 2].Value = "640";
                        wsCDI.Cells[29, 3].Style.Font.Bold = true;
                        wsCDI.Cells[29, 3].Value = "TOLUCA";

                                               


                        Byte[] bin = p.GetAsByteArray();
                        File.WriteAllBytes(ruta, bin);


                        if (File.Exists(ruta))
                        {
                            string ruta2 = null;
                            ruta2 = Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xlsx";
                            // File.Move(ruta, Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xls");
                            Response.Redirect("Reportes\\Reporte" + tipo + nro + ".xlsx", false);
                        }

                    }

                }

            }
        }
        


        private static void CreateHeader(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        {            
           
            
            int colIndex = 2;
            foreach (DataColumn dc in dt.Columns) //Creating Headings
            {
                var cell = ws.Cells[rowIndex, colIndex];

                //Setting the background color of header cells to Gray
                var fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.Gray);

                //Setting Top/left,right/bottom borders.
                var border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                //Setting Value in cell
                cell.Value =  dc.ColumnName;

                colIndex++;
            }
        }

        private static void CreateData(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        {
            int colIndex = 0;
            foreach (DataRow dr in dt.Rows) // Adding Data into rows
            {
                colIndex = 2;
                rowIndex++;

                foreach (DataColumn dc in dt.Columns)
                {
                    var cell = ws.Cells[rowIndex, colIndex];

                    //Setting Value in cell
                    if (dc.DataType == typeof(System.Double))
                    {
                        cell.Value =   (dr[dc.ColumnName].ToString());
                        cell.Style.Numberformat.Format =  "0.00" ;
                    }
                    else
                    {
                        cell.Value = dr[dc.ColumnName];
                    }


                  
                

                    //Setting borders of cell
                    var border = cell.Style.Border;
                    border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                    colIndex++;
                }
            }
        }

        private static void CreateFooter(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        {
            int colIndex = 0;
            foreach (DataColumn dc in dt.Columns) //Creating Formula in footers
            {
                colIndex++;
                var cell = ws.Cells[rowIndex, colIndex];

                //Setting Sum Formula
                cell.Formula = "Sum(" + ws.Cells[3, colIndex].Address + ":" + ws.Cells[rowIndex - 1, colIndex].Address + ")";

                //Setting Background fill color to Gray
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(Color.Gray);
            }
        }




        private static DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < 10; i++)
            {
                dt.Columns.Add(i.ToString());
            }

            for (int i = 0; i < 10; i++)
            {
                DataRow dr = dt.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    dr[dc.ToString()] = i;
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }




        private void ExportarControlRemisiones(int Id_Tm)
        {
            System.IO.StreamWriter sw = null;
            string ruta = null;
            string reporte = "";
            Random rnd = new Random();



            switch (Id_Tm)
            {
                case 62:
                    reporte = "CONSIGNACION";
                    break;
                case 72:
                    reporte = "EQUIPO ARRENDADO";
                    break;
                case 63:
                    reporte = "PENDIENTE POR FACTURAR";
                    break;
                case 65:
                    reporte = "PRODUCTO NO CONFORME";
                    break;
                case 64:
                    reporte = "PRUEBA";
                    break;
            }


            int nro = rnd.Next(0, 8);
            string tipo = "ControlRemisiones";
            ruta = Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xlsx";

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xls"))
                File.Delete(Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xls");



            List<Remision> List = new List<Remision>();
            Remision remision = new Remision();
            CN_CapRemision CN_Remision = new CN_CapRemision();


            remision.Id_Emp = 1;
            remision.Cal_Anio = Int32.Parse(RadComboAno.SelectedValue);
            remision.Cal_Mes = Int32.Parse(RadComboMes.SelectedValue);
            remision.Id_Tm = Id_Tm;


            CN_Remision.ConsultaEstadistica(remision, Emp_CnxCen, ref  List);


            if ((List != null))
            {
                if (!(List.Count() == 0))
                {


                    using (ExcelPackage p = new ExcelPackage())
                    {

                        //set the workbook properties and add a default sheet in it
                        SetWorkbookProperties(p);
                        //Create a sheet


                        List<int> lCDI = List.Select(i => i.Id_Cd).Distinct().ToList();
                        List<RemisionTotal> lCDTOTAL = new List<RemisionTotal>();
                        int count = 1;

                        foreach (int a in lCDI)
                        {
                            DataTable dataTable = new DataTable();
                            DataColumn dcCDS = new DataColumn("Id_Cd", Type.GetType("System.String"));
                            DataColumn dcId_Cte = new DataColumn("Id_Cte", Type.GetType("System.String"));
                            DataColumn dcCte_NomComercial = new DataColumn("Cte_NomComercial", Type.GetType("System.String"));
                            DataColumn dcId_Prd = new DataColumn("Id_Prd", Type.GetType("System.String"));
                            DataColumn dcPrd_Descripcion = new DataColumn("Prd_Descripcion", Type.GetType("System.String"));
                            DataColumn dcEst_Acumulado = new DataColumn("Acumulado Unidades", Type.GetType("System.Double"));
                            DataColumn dcEst_CAcumulado = new DataColumn("Acumulado Pesos", Type.GetType("System.String"));
                            DataColumn dcEst_Ene = new DataColumn("Ene Unidades", Type.GetType("System.Int32"));
                            DataColumn dcEst_CEne = new DataColumn("Ene Pesos", Type.GetType("System.Double"));
                            DataColumn dcEst_Feb = new DataColumn("Feb Unidades", Type.GetType("System.Int32"));
                            DataColumn dcEst_CFeb = new DataColumn("Feb Pesos", Type.GetType("System.Double"));
                            DataColumn dcEst_Mar = new DataColumn("Mar Unidades", Type.GetType("System.Int32"));
                            DataColumn dcEst_CMar = new DataColumn("Mar Pesos", Type.GetType("System.Double"));
                            DataColumn dcEst_Abr = new DataColumn("Abr Unidades", Type.GetType("System.Int32"));
                            DataColumn dcEst_CAbr = new DataColumn("Abr Pesos", Type.GetType("System.Double"));
                            DataColumn dcEst_May = new DataColumn("May Unidades", Type.GetType("System.Int32"));
                            DataColumn dcEst_CMay = new DataColumn("May Pesos", Type.GetType("System.Double"));
                            DataColumn dcEst_Jun = new DataColumn("Jun Unidades", Type.GetType("System.Int32"));
                            DataColumn dcEst_CJun = new DataColumn("Jun Pesos", Type.GetType("System.Double"));
                            DataColumn dcEst_Jul = new DataColumn("Jul Unidades", Type.GetType("System.Int32"));
                            DataColumn dcEst_CJul = new DataColumn("Jul Pesos", Type.GetType("System.Double"));
                            DataColumn dcEst_Ago = new DataColumn("Ago Unidades", Type.GetType("System.Int32"));
                            DataColumn dcEst_CAgo = new DataColumn("Ago Pesos", Type.GetType("System.Double"));
                            DataColumn dcEst_Sep = new DataColumn("Sep Unidades", Type.GetType("System.Int32"));
                            DataColumn dcEst_CSep = new DataColumn("Sep Pesos", Type.GetType("System.Double"));
                            DataColumn dcEst_Oct = new DataColumn("Oct Unidades", Type.GetType("System.Int32"));
                            DataColumn dcEst_COct = new DataColumn("Oct Pesos", Type.GetType("System.Double"));
                            DataColumn dcEst_Nov = new DataColumn("Nov Unidades", Type.GetType("System.Int32"));
                            DataColumn dcEst_CNov = new DataColumn("Nov Pesos", Type.GetType("System.Double"));
                            DataColumn dcEst_Dic = new DataColumn("Dic Unidades", Type.GetType("System.Int32"));
                            DataColumn dcEst_CDic = new DataColumn("Dic Pesos", Type.GetType("System.Double"));
                            DataColumn dcTotal = new DataColumn("Total Unidades", Type.GetType("System.Int32"));
                            DataColumn dcTotalCosto = new DataColumn("Total Pesos", Type.GetType("System.Double"));
                            DataColumn dcVigente = new DataColumn("Vigente", Type.GetType("System.Double"));
                            DataColumn dcVencido = new DataColumn("Vencido", Type.GetType("System.Double"));

                            dataTable.Columns.Add(dcCDS);
                            dataTable.Columns.Add(dcId_Cte);
                            dataTable.Columns.Add(dcCte_NomComercial);
                            dataTable.Columns.Add(dcId_Prd);
                            dataTable.Columns.Add(dcPrd_Descripcion);
                            dataTable.Columns.Add(dcEst_Acumulado);
                            dataTable.Columns.Add(dcEst_CAcumulado);
                            dataTable.Columns.Add(dcEst_Ene);
                            dataTable.Columns.Add(dcEst_CEne);
                            dataTable.Columns.Add(dcEst_Feb);
                            dataTable.Columns.Add(dcEst_CFeb);
                            dataTable.Columns.Add(dcEst_Mar);
                            dataTable.Columns.Add(dcEst_CMar);
                            dataTable.Columns.Add(dcEst_Abr);
                            dataTable.Columns.Add(dcEst_CAbr);
                            dataTable.Columns.Add(dcEst_May);
                            dataTable.Columns.Add(dcEst_CMay);
                            dataTable.Columns.Add(dcEst_Jun);
                            dataTable.Columns.Add(dcEst_CJun);
                            dataTable.Columns.Add(dcEst_Jul);
                            dataTable.Columns.Add(dcEst_CJul);
                            dataTable.Columns.Add(dcEst_Ago);
                            dataTable.Columns.Add(dcEst_CAgo);
                            dataTable.Columns.Add(dcEst_Sep);
                            dataTable.Columns.Add(dcEst_CSep);
                            dataTable.Columns.Add(dcEst_Oct);
                            dataTable.Columns.Add(dcEst_COct);
                            dataTable.Columns.Add(dcEst_Nov);
                            dataTable.Columns.Add(dcEst_CNov);
                            dataTable.Columns.Add(dcEst_Dic);
                            dataTable.Columns.Add(dcEst_CDic);
                            dataTable.Columns.Add(dcTotal);
                            dataTable.Columns.Add(dcTotalCosto);
                            dataTable.Columns.Add(dcVigente);
                            dataTable.Columns.Add(dcVencido);
                            ExcelWorksheet ws = CreateSheet(p, getCDIName(a), count);

                            double TOTAL = 0; double TOTAL_VIGENTE = 0; double TOTAL_VENCIDO = 0;
                            foreach (Remision rem in List.FindAll(b => b.Id_Cd == a))
                            {
                                DataRow drFila = null;
                                drFila = dataTable.NewRow();
                                drFila["Id_Cd"] = rem.Id_Cd;
                                drFila["Id_Cte"] = rem.Id_Cte;
                                drFila["Cte_NomComercial"] = rem.Cte_NomComercial;
                                drFila["Id_Prd"] = rem.Id_Prd;
                                drFila["Prd_Descripcion"] = rem.Prd_Descripcion;
                                drFila["Acumulado Unidades"] = rem.Est_Acumulado;
                                drFila["Acumulado Pesos"] = rem.Est_CAcumulado;
                                drFila["Ene Unidades"] = rem.Est_Ene;
                                drFila["Ene Pesos"] = rem.Est_CEne;
                                drFila["Feb Unidades"] = rem.Est_Feb;
                                drFila["Feb Pesos"] = rem.Est_CFeb;
                                drFila["Mar Unidades"] = rem.Est_Mar;
                                drFila["Mar Pesos"] = rem.Est_CMar;
                                drFila["Abr Unidades"] = rem.Est_Abr;
                                drFila["Abr Pesos"] = rem.Est_CAbr;
                                drFila["May Unidades"] = rem.Est_May;
                                drFila["May Pesos"] = rem.Est_CMay;
                                drFila["Jun Unidades"] = rem.Est_Jun;
                                drFila["Jun Pesos"] = rem.Est_CJun;
                                drFila["Jul Unidades"] = rem.Est_Jul;
                                drFila["Jul Pesos"] = rem.Est_CJul;
                                drFila["Ago Unidades"] = rem.Est_Ago;
                                drFila["Ago Pesos"] = rem.Est_CAgo;
                                drFila["Sep Unidades"] = rem.Est_Sep;
                                drFila["Sep Pesos"] = rem.Est_CSep;
                                drFila["Oct Unidades"] = rem.Est_Oct;
                                drFila["Oct Pesos"] = rem.Est_COct;
                                drFila["Nov Unidades"] = rem.Est_Nov;
                                drFila["Nov Pesos"] = rem.Est_CNov;
                                drFila["Dic Unidades"] = rem.Est_Dic;
                                drFila["Dic Pesos"] = rem.Est_CDic;
                                rem.Vigente = rem.Vigente > 0? rem.Vigente : 0;
                                rem.Vencido = rem.Vencido > 0? rem.Vencido : 0;
                                drFila["Total Unidades"] = rem.Total;
                                drFila["Total Pesos"] = rem.TotalCosto;
                                drFila["Vigente"] =  rem.Vigente;
                                drFila["Vencido"] = rem.Vencido;

                                TOTAL = TOTAL + rem.TotalCosto;
                                TOTAL_VIGENTE = TOTAL_VIGENTE + rem.Vigente;
                                TOTAL_VENCIDO = TOTAL_VENCIDO + rem.Vencido;

                                dataTable.Rows.Add(drFila);
                                dataTable.AcceptChanges();
                                

                            }

                            RemisionTotal RemisionT = new RemisionTotal();
                            RemisionT.Id_Cd = a;
                            RemisionT.Total = TOTAL;
                            RemisionT.TotalVigente = TOTAL_VIGENTE;
                            RemisionT.TotalVencido = TOTAL_VENCIDO;
                            lCDTOTAL.Add(RemisionT);


                            //Merging cells and create a center heading for out table
                            ws.Cells[1, 1].Value = "REPORTE DE CONTROL DE REMISIONES " + " "+ reporte;
                            ws.Cells[1, 1, 1, dataTable.Columns.Count].Merge = true;
                            ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.Font.Bold = true;
                            ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            

                            int rowIndex = 4;

                            CreateHeader(ws, ref rowIndex, dataTable);
                            CreateData(ws, ref rowIndex, dataTable);
                            // CreateFooter(ws, ref rowIndex, dt);*/

                            count++;
                        }


                        ExcelWorksheet wsGral = CreateSheet(p, "GENERAL", lCDTOTAL.Count() + 1);


                        DataTable dataTableGral = new DataTable();
                        DataColumn dcCDSGral = new DataColumn("Id_Cd", Type.GetType("System.String"));
                        DataColumn dcId_CteGral = new DataColumn("Id_Cte", Type.GetType("System.String"));
                        DataColumn dcCte_NomComercialGral = new DataColumn("Cte_NomComercial", Type.GetType("System.String"));
                        DataColumn dcId_PrdGral = new DataColumn("Id_Prd", Type.GetType("System.Int32"));
                        DataColumn dcPrd_DescripcionGral = new DataColumn("Prd_Descripcion", Type.GetType("System.String"));
                        DataColumn dcEst_AcumuladoGral = new DataColumn("Acumulado Unidades", Type.GetType("System.Double"));
                        DataColumn dcEst_CAcumuladoGral = new DataColumn("Acumulado Pesos", Type.GetType("System.String"));
                        DataColumn dcEst_EneGral = new DataColumn("Ene Unidades", Type.GetType("System.Int32"));
                        DataColumn dcEst_CEneGral = new DataColumn("Ene Pesos", Type.GetType("System.Double"));
                        DataColumn dcEst_FebGral = new DataColumn("Feb Unidades", Type.GetType("System.Int32"));
                        DataColumn dcEst_CFebGral = new DataColumn("Feb Pesos", Type.GetType("System.Double"));
                        DataColumn dcEst_MarGral = new DataColumn("Mar Unidades", Type.GetType("System.Int32"));
                        DataColumn dcEst_CMarGral = new DataColumn("Mar Pesos", Type.GetType("System.Double"));
                        DataColumn dcEst_AbrGral = new DataColumn("Abr Unidades", Type.GetType("System.Int32"));
                        DataColumn dcEst_CAbrGral = new DataColumn("Abr Pesos", Type.GetType("System.Double"));
                        DataColumn dcEst_MayGral = new DataColumn("May Unidades", Type.GetType("System.Int32"));
                        DataColumn dcEst_CMayGral = new DataColumn("May Pesos", Type.GetType("System.Double"));
                        DataColumn dcEst_JunGral = new DataColumn("Jun Unidades", Type.GetType("System.Int32"));
                        DataColumn dcEst_CJunGral = new DataColumn("Jun Pesos", Type.GetType("System.Double"));
                        DataColumn dcEst_JulGral = new DataColumn("Jul Unidades", Type.GetType("System.Int32"));
                        DataColumn dcEst_CJulGral = new DataColumn("Jul Pesos", Type.GetType("System.Double"));
                        DataColumn dcEst_AgoGral = new DataColumn("Ago Unidades", Type.GetType("System.Int32"));
                        DataColumn dcEst_CAgoGral = new DataColumn("Ago Pesos", Type.GetType("System.Double"));
                        DataColumn dcEst_SepGral = new DataColumn("Sep Unidades", Type.GetType("System.Int32"));
                        DataColumn dcEst_CSepGral = new DataColumn("Sep Pesos", Type.GetType("System.Double"));
                        DataColumn dcEst_OctGral = new DataColumn("Oct Unidades", Type.GetType("System.Int32"));
                        DataColumn dcEst_COctGral = new DataColumn("Oct Pesos", Type.GetType("System.Double"));
                        DataColumn dcEst_NovGral = new DataColumn("Nov Unidades", Type.GetType("System.Int32"));
                        DataColumn dcEst_CNovGral = new DataColumn("Nov Pesos", Type.GetType("System.Double"));
                        DataColumn dcEst_DicGral = new DataColumn("Dic Unidades", Type.GetType("System.Int32"));
                        DataColumn dcEst_CDicGral = new DataColumn("Dic Pesos", Type.GetType("System.Double"));
                        DataColumn dcTotalGral = new DataColumn("Total Unidades", Type.GetType("System.Int32"));
                        DataColumn dcTotalCostoGral = new DataColumn("Total Pesos", Type.GetType("System.Double"));
                        DataColumn dcVigenteGral = new DataColumn("Vigente", Type.GetType("System.Double"));
                        DataColumn dcVencidoGral = new DataColumn("Vencido", Type.GetType("System.Double"));



                        dataTableGral.Columns.Add(dcCDSGral);
                        dataTableGral.Columns.Add(dcId_CteGral);
                        dataTableGral.Columns.Add(dcCte_NomComercialGral);
                        dataTableGral.Columns.Add(dcId_PrdGral);
                        dataTableGral.Columns.Add(dcPrd_DescripcionGral);
                        dataTableGral.Columns.Add(dcEst_AcumuladoGral);
                        dataTableGral.Columns.Add(dcEst_CAcumuladoGral);
                        dataTableGral.Columns.Add(dcEst_EneGral);
                        dataTableGral.Columns.Add(dcEst_CEneGral);
                        dataTableGral.Columns.Add(dcEst_FebGral);
                        dataTableGral.Columns.Add(dcEst_CFebGral);
                        dataTableGral.Columns.Add(dcEst_MarGral);
                        dataTableGral.Columns.Add(dcEst_CMarGral);
                        dataTableGral.Columns.Add(dcEst_AbrGral);
                        dataTableGral.Columns.Add(dcEst_CAbrGral);
                        dataTableGral.Columns.Add(dcEst_MayGral);
                        dataTableGral.Columns.Add(dcEst_CMayGral);
                        dataTableGral.Columns.Add(dcEst_JunGral);
                        dataTableGral.Columns.Add(dcEst_CJunGral);
                        dataTableGral.Columns.Add(dcEst_JulGral);
                        dataTableGral.Columns.Add(dcEst_CJulGral);
                        dataTableGral.Columns.Add(dcEst_AgoGral);
                        dataTableGral.Columns.Add(dcEst_CAgoGral);
                        dataTableGral.Columns.Add(dcEst_SepGral);
                        dataTableGral.Columns.Add(dcEst_CSepGral);
                        dataTableGral.Columns.Add(dcEst_OctGral);
                        dataTableGral.Columns.Add(dcEst_COctGral);
                        dataTableGral.Columns.Add(dcEst_NovGral);
                        dataTableGral.Columns.Add(dcEst_CNovGral);
                        dataTableGral.Columns.Add(dcEst_DicGral);
                        dataTableGral.Columns.Add(dcEst_CDicGral);
                        dataTableGral.Columns.Add(dcTotalGral);
                        dataTableGral.Columns.Add(dcTotalCostoGral);
                        dataTableGral.Columns.Add(dcVigenteGral);
                        dataTableGral.Columns.Add(dcVencidoGral); 



                        //Merging cells and create a center heading for out table
                        wsGral.Cells[1, 1].Value = "REPORTE DE CONTROL DE REMISIONES " + " " + reporte;
                        wsGral.Cells[1, 1, 1, 4].Merge = true;
                        wsGral.Cells[1, 1, 1, 4].Style.Font.Bold = true;
                        wsGral.Cells[1, 1, 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;




                        foreach (Remision rem in List)
                        {
                         
                            DataRow drFila = null;
                            drFila = dataTableGral.NewRow();
                            drFila["Id_Cd"] = rem.Id_Cd;
                            drFila["Id_Cte"] = rem.Id_Cte;
                            drFila["Cte_NomComercial"] = rem.Cte_NomComercial;
                            drFila["Id_Prd"] = rem.Id_Prd;
                            drFila["Prd_Descripcion"] = rem.Prd_Descripcion;
                            drFila["Acumulado Unidades"] = rem.Est_Acumulado;
                            drFila["Acumulado Pesos"] = rem.Est_CAcumulado;
                            drFila["Ene Unidades"] = rem.Est_Ene;
                            drFila["Ene Pesos"] = rem.Est_CEne;
                            drFila["Feb Unidades"] = rem.Est_Feb;
                            drFila["Feb Pesos"] = rem.Est_CFeb;
                            drFila["Mar Unidades"] = rem.Est_Mar;
                            drFila["Mar Pesos"] = rem.Est_CMar;
                            drFila["Abr Unidades"] = rem.Est_Abr;
                            drFila["Abr Pesos"] = rem.Est_CAbr;
                            drFila["May Unidades"] = rem.Est_May;
                            drFila["May Pesos"] = rem.Est_CMay;
                            drFila["Jun Unidades"] = rem.Est_Jun;
                            drFila["Jun Pesos"] = rem.Est_CJun;
                            drFila["Jul Unidades"] = rem.Est_Jul;
                            drFila["Jul Pesos"] = rem.Est_CJul;
                            drFila["Ago Unidades"] = rem.Est_Ago;
                            drFila["Ago Pesos"] = rem.Est_CAgo;
                            drFila["Sep Unidades"] = rem.Est_Sep;
                            drFila["Sep Pesos"] = rem.Est_CSep;
                            drFila["Oct Unidades"] = rem.Est_Oct;
                            drFila["Oct Pesos"] = rem.Est_COct;
                            drFila["Nov Unidades"] = rem.Est_Nov;
                            drFila["Nov Pesos"] = rem.Est_CNov;
                            drFila["Dic Unidades"] = rem.Est_Dic;
                            drFila["Dic Pesos"] = rem.Est_CDic;
                            drFila["Total Unidades"] = rem.Total;
                            drFila["Total Pesos"] = rem.TotalCosto;
                            rem.Vigente = rem.Vigente > 0? rem.Vigente : 0;
                            rem.Vencido = rem.Vencido > 0? rem.Vencido : 0;
                            drFila["Vigente"] =  rem.Vigente ;
                            drFila["Vencido"] = rem.Vencido;


                            dataTableGral.Rows.Add(drFila);
                            dataTableGral.AcceptChanges();

                        }

                        int rowIndex1 = 4;
                        CreateHeader(wsGral, ref rowIndex1, dataTableGral);
                        CreateData(wsGral, ref rowIndex1, dataTableGral);


                        ExcelWorksheet wsResumen = CreateSheet(p, "RESUMEN", lCDTOTAL.Count() + 2);



                        //Merging cells and create a center heading for out table
                        wsResumen.Cells[1, 1].Value = "REPORTE DE CONTROL DE REMISIONES " + " " + reporte;
                        wsResumen.Cells[1, 1, 1, 4].Merge = true;
                        wsResumen.Cells[1, 1, 1, 4].Style.Font.Bold = true;
                        wsResumen.Cells[1, 1, 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        

                        DataTable dataTableResumen = new DataTable();
                        DataColumn dcCDSResumen = new DataColumn("CDI", Type.GetType("System.String"));
                        DataColumn dcTotalResumen = new DataColumn("Total", Type.GetType("System.String"));
                        DataColumn dcVencidoResumen = new DataColumn("Total Vencido", Type.GetType("System.Double"));
                        DataColumn dcVigenteResumen = new DataColumn("Total Vigente", Type.GetType("System.Double"));


                        dataTableResumen.Columns.Add(dcCDSResumen);
                        dataTableResumen.Columns.Add(dcTotalResumen);
                        dataTableResumen.Columns.Add(dcVencidoResumen);
                        dataTableResumen.Columns.Add(dcVigenteResumen);

                        int rowIndex2 = 4;
                        CreateHeader(wsResumen, ref rowIndex2, dataTableResumen);

                        
                            wsResumen.Cells[5, 2].Value = "MONTERREY";
                            wsResumen.Cells[5, 2].Style.Font.Bold = true;
                         if (lCDTOTAL.Count(a => a.Id_Cd == 110) > 0)
                         {
                            wsResumen.Cells[5, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 110).Total;
                            wsResumen.Cells[5, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 110).TotalVencido;
                            wsResumen.Cells[5, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 110).TotalVigente;
                         }
                                              
                            wsResumen.Cells[6, 2].Value = "SALTILLO";
                            wsResumen.Cells[6, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 150) > 0)
                        {
                            wsResumen.Cells[6, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 150).Total;
                            wsResumen.Cells[6, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 150).TotalVencido;
                            wsResumen.Cells[6, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 150).TotalVigente;
                        }

                      
                            wsResumen.Cells[7, 2].Value = "MATAMOROS";
                            wsResumen.Cells[7, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 160) > 0)
                        {
                            wsResumen.Cells[7, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 160).Total;
                            wsResumen.Cells[7, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 160).TotalVencido;
                            wsResumen.Cells[7, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 160).TotalVigente;
                        }



                       
                            wsResumen.Cells[8, 2].Value = "TORREON";
                            wsResumen.Cells[8, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 170) > 0)
                        {
                            wsResumen.Cells[8, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 170).Total;
                            wsResumen.Cells[8, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 170).TotalVencido;
                            wsResumen.Cells[8, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 170).TotalVigente;
                        }


                        
                            wsResumen.Cells[9, 2].Value = "LAREDO";
                            wsResumen.Cells[9, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 180) > 0)
                        {
                            wsResumen.Cells[9, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 180).Total;
                            wsResumen.Cells[9, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 180).TotalVencido;
                            wsResumen.Cells[9, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 180).TotalVigente;
                        }


                        
                            wsResumen.Cells[10, 2].Value = "LEON";
                            wsResumen.Cells[10, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 190) > 0)
                        {
                            wsResumen.Cells[10, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 190).Total;
                            wsResumen.Cells[10, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 190).TotalVencido;
                            wsResumen.Cells[10, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 190).TotalVigente;
                        }


                        
                            wsResumen.Cells[11, 2].Value = "TIJUANA";
                            wsResumen.Cells[11, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 200) > 0)
                        {
                            wsResumen.Cells[11, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 200).Total;
                            wsResumen.Cells[11, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 200).TotalVencido;
                            wsResumen.Cells[11, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 200).TotalVigente;
                        }


                       

                            wsResumen.Cells[12, 2].Value = "CHIHUAHUA";
                            wsResumen.Cells[12, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 210) > 0)
                        {
                            wsResumen.Cells[12, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 210).Total;
                            wsResumen.Cells[12, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 210).TotalVencido;
                            wsResumen.Cells[12, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 210).TotalVigente;
                        }


                      
                            wsResumen.Cells[13, 2].Value = "SAN LUIS";
                            wsResumen.Cells[13, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 220) > 0)
                        {
                            wsResumen.Cells[13, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 220).Total;
                            wsResumen.Cells[13, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 220).TotalVencido;
                            wsResumen.Cells[13, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 220).TotalVigente;
                        }


                        

                            wsResumen.Cells[14, 2].Value = "JUAREZ";
                            wsResumen.Cells[14, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 230) > 0)
                        {
                            wsResumen.Cells[14, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 230).Total;
                            wsResumen.Cells[14, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 230).TotalVencido;
                            wsResumen.Cells[14, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 230).TotalVigente;
                        }


                       
                            wsResumen.Cells[15, 2].Value = "AGUASCALIENTES";
                            wsResumen.Cells[15, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 240) > 0)
                        {
                            wsResumen.Cells[15, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 240).Total;
                            wsResumen.Cells[15, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 240).TotalVencido;
                            wsResumen.Cells[15, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 240).TotalVigente;
                        }


                       
                            wsResumen.Cells[16, 2].Value = "MEXICO";
                            wsResumen.Cells[16, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 310) > 0)
                        {
                            wsResumen.Cells[16, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 310).Total;
                            wsResumen.Cells[16, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 310).TotalVencido;
                            wsResumen.Cells[16, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 310).TotalVigente;
                        }


                       
                            wsResumen.Cells[17, 2].Value = "VERACRUZ";
                            wsResumen.Cells[17, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 340) > 0)
                        {
                            wsResumen.Cells[17, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 340).Total;
                            wsResumen.Cells[17, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 340).TotalVencido;
                            wsResumen.Cells[17, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 340).TotalVigente;
                        }


                        
                            wsResumen.Cells[18, 2].Value = "CD CARMEN";
                            wsResumen.Cells[18, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 350) > 0)
                        {
                            wsResumen.Cells[18, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 350).Total;
                            wsResumen.Cells[18, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 350).TotalVencido;
                            wsResumen.Cells[18, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 350).TotalVigente;
                        }


                        
                            wsResumen.Cells[19, 2].Value = "MERIDA";
                            wsResumen.Cells[19, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 360) > 0)
                        {
                            wsResumen.Cells[19, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 360).Total;
                            wsResumen.Cells[19, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 360).TotalVencido;
                            wsResumen.Cells[19, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 360).TotalVigente;
                        }


                        

                            wsResumen.Cells[20, 2].Value = "CANCUN";
                            wsResumen.Cells[20, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 370) > 0)
                        {
                            wsResumen.Cells[20, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 370).Total;
                            wsResumen.Cells[20, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 370).TotalVencido;
                            wsResumen.Cells[20, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 370).TotalVigente;
                        }


                        
                            wsResumen.Cells[21, 2].Value = "RIVIERA";
                            wsResumen.Cells[21, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 380) > 0)
                        {
                            wsResumen.Cells[21, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 380).Total;
                            wsResumen.Cells[21, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 380).TotalVencido;
                            wsResumen.Cells[21, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 380).TotalVigente;
                        }


                       
                            wsResumen.Cells[22, 2].Value = "VALLARTA";
                            wsResumen.Cells[22, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 390) > 0)
                        {
                            wsResumen.Cells[22, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 390).Total;
                            wsResumen.Cells[22, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 390).TotalVencido;
                            wsResumen.Cells[22, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 390).TotalVigente;
                        }


                        
                            wsResumen.Cells[23, 2].Value = "LOS CABOS";
                            wsResumen.Cells[23, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 400) > 0)
                        {
                            wsResumen.Cells[23, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 400).Total;
                            wsResumen.Cells[23, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 400).TotalVencido;
                            wsResumen.Cells[23, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 400).TotalVigente;
                        }


                      
                            wsResumen.Cells[24, 2].Value = "QRO";
                            wsResumen.Cells[24, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 410) > 0)
                        {
                            wsResumen.Cells[24, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 410).Total;
                            wsResumen.Cells[24, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 410).TotalVencido;
                            wsResumen.Cells[24, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 410).TotalVigente;
                        }


                        
                            wsResumen.Cells[25, 2].Value = "GDL";
                            wsResumen.Cells[25, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 430) > 0)
                        {
                            wsResumen.Cells[25, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 430).Total;
                            wsResumen.Cells[25, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 430).TotalVencido;
                            wsResumen.Cells[25, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 430).TotalVigente;
                        }


                       
                            wsResumen.Cells[26, 2].Value = "PUEBLA";
                            wsResumen.Cells[26, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 510) > 0)
                        {
                            wsResumen.Cells[26, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 510).Total;
                            wsResumen.Cells[26, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 510).TotalVencido;
                            wsResumen.Cells[26, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 510).TotalVigente;
                        }


                        
                            wsResumen.Cells[27, 2].Value = "COATZACOALCOS";
                            wsResumen.Cells[27, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 610) > 0)
                        {

                            wsResumen.Cells[27, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 610).Total;
                            wsResumen.Cells[27, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 610).TotalVencido;
                            wsResumen.Cells[27, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 610).TotalVigente;
                        }



                        wsResumen.Cells[28, 2].Value = "VILLAHERMOSA";
                        wsResumen.Cells[28, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 620) > 0)
                        {

                            wsResumen.Cells[28, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 620).Total;
                            wsResumen.Cells[28, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 620).TotalVencido;
                            wsResumen.Cells[28, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 620).TotalVigente;
                        }

                       

                            wsResumen.Cells[29, 2].Value = "TOLUCA";
                            wsResumen.Cells[29, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 640) > 0)
                        {
                            wsResumen.Cells[29, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 640).Total;
                            wsResumen.Cells[29, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 640).TotalVencido;
                            wsResumen.Cells[29, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 640).TotalVigente;
                        }




                         ExcelWorksheet wsCDI = CreateSheet(p, "CDI'S", lCDTOTAL.Count() + 3 );



                        //Merging cells and create a center heading for out table
                        wsCDI.Cells[1, 1].Value = "CENTROS DE DISTRIBUCION";
                        wsCDI.Cells[1, 1, 1, 4].Merge = true;
                        wsCDI.Cells[1, 1, 1, 4].Style.Font.Bold = true;
                        wsCDI.Cells[1, 1, 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                       

                        DataTable dataTableCDI = new DataTable();
                        DataColumn dcCDSCDI = new DataColumn("CODIGO", Type.GetType("System.String"));
                        DataColumn dcExcesoCDI = new DataColumn("CDS", Type.GetType("System.String"));



                        dataTableCDI.Columns.Add(dcCDSCDI);
                        dataTableCDI.Columns.Add(dcExcesoCDI);
                        

                        int rowIndex3 = 4;
                        CreateHeader(wsCDI, ref rowIndex3, dataTableCDI);

                        wsCDI.Cells[5, 2].Value = "110";
                        wsCDI.Cells[5, 3].Style.Font.Bold = true;
                        wsCDI.Cells[5, 3].Value = "MONTERREY";

                        wsCDI.Cells[6, 2].Value = "150";
                        wsCDI.Cells[6, 3].Style.Font.Bold = true;
                        wsCDI.Cells[6, 3].Value = "SALTILLO";

                        wsCDI.Cells[7, 2].Value = "160";
                        wsCDI.Cells[7, 3].Style.Font.Bold = true;
                        wsCDI.Cells[7, 3].Value = "MATAMOROS";

                        wsCDI.Cells[8, 2].Value = "170";
                        wsCDI.Cells[8, 3].Style.Font.Bold = true;
                        wsCDI.Cells[8, 3].Value = "TORREON";

                        wsCDI.Cells[9, 2].Value = "180";
                        wsCDI.Cells[9, 3].Style.Font.Bold = true;
                        wsCDI.Cells[9, 3].Value = "LAREDO";

                        wsCDI.Cells[10, 2].Value = "190";
                        wsCDI.Cells[10, 3].Style.Font.Bold = true;
                        wsCDI.Cells[10, 3].Value = "LEON";

                        wsCDI.Cells[11, 2].Value = "200";
                        wsCDI.Cells[11, 3].Style.Font.Bold = true;
                        wsCDI.Cells[11, 3].Value = "TIJUANA";

                        wsCDI.Cells[12, 2].Value = "210";
                        wsCDI.Cells[12, 3].Style.Font.Bold = true;
                        wsCDI.Cells[12, 3].Value = "CHIHUAHUA";

                        wsCDI.Cells[13, 2].Value = "220";
                        wsCDI.Cells[13, 3].Style.Font.Bold = true;
                        wsCDI.Cells[13, 3].Value = "SAN LUIS";

                        wsCDI.Cells[14, 2].Value = "230";
                        wsCDI.Cells[14, 3].Style.Font.Bold = true;
                        wsCDI.Cells[14, 3].Value = "JUAREZ";

                        wsCDI.Cells[15, 2].Value = "240";
                        wsCDI.Cells[15, 3].Style.Font.Bold = true;
                        wsCDI.Cells[15, 3].Value = "AGUASCALIENTES";

                        wsCDI.Cells[16, 2].Value = "310";
                        wsCDI.Cells[16, 3].Style.Font.Bold = true;
                        wsCDI.Cells[16, 3].Value = "MEXICO";

                        wsCDI.Cells[17, 2].Value = "340";
                        wsCDI.Cells[17, 3].Style.Font.Bold = true;
                        wsCDI.Cells[17, 3].Value = "VERACRUZ";

                        wsCDI.Cells[18, 2].Value = "350";
                        wsCDI.Cells[18, 3].Style.Font.Bold = true;
                        wsCDI.Cells[18, 3].Value = "CD CARMEN";

                        wsCDI.Cells[19, 2].Value = "360";
                        wsCDI.Cells[19, 3].Style.Font.Bold = true;
                        wsCDI.Cells[19, 3].Value = "MERIDA";

                        wsCDI.Cells[20, 2].Value = "370";
                        wsCDI.Cells[20, 3].Style.Font.Bold = true;
                        wsCDI.Cells[20, 3].Value = "CANCUN";

                        wsCDI.Cells[21, 2].Value = "380";
                        wsCDI.Cells[21, 3].Style.Font.Bold = true;
                        wsCDI.Cells[21, 3].Value = "RIVIERA";

                        wsCDI.Cells[22, 2].Value = "390";
                        wsCDI.Cells[22, 3].Style.Font.Bold = true;
                        wsCDI.Cells[22, 3].Value = "VALLARTA";

                        wsCDI.Cells[23, 2].Value = "400";
                        wsCDI.Cells[23, 3].Style.Font.Bold = true;
                        wsCDI.Cells[23, 3].Value = "LOS CABOS";


                        wsCDI.Cells[24, 2].Value = "410";
                        wsCDI.Cells[24, 3].Style.Font.Bold = true;
                        wsCDI.Cells[24, 3].Value = "QUERETARO";

                        wsCDI.Cells[25, 2].Value = "430";
                        wsCDI.Cells[25, 3].Style.Font.Bold = true;
                        wsCDI.Cells[25, 3].Value = "GUADALAJARA";

                        wsCDI.Cells[26, 2].Value = "510";
                        wsCDI.Cells[26, 3].Style.Font.Bold = true;
                        wsCDI.Cells[26, 3].Value = "PUEBLA";


                        wsCDI.Cells[27, 2].Value = "610";
                        wsCDI.Cells[27, 3].Style.Font.Bold = true;
                        wsCDI.Cells[27, 3].Value = "COATZACOALCOS";


                        wsCDI.Cells[28, 2].Value = "620";
                        wsCDI.Cells[28, 3].Style.Font.Bold = true;
                        wsCDI.Cells[28, 3].Value = "VILLAHERMOSA";

                        wsCDI.Cells[29, 2].Value = "640";
                        wsCDI.Cells[29, 3].Style.Font.Bold = true;
                        wsCDI.Cells[29, 3].Value = "TOLUCA";


                        Byte[] bin = p.GetAsByteArray();
                        File.WriteAllBytes(ruta, bin);


                        if (File.Exists(ruta))
                        {
                            string ruta2 = null;
                            ruta2 = Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xlsx";
                            // File.Move(ruta, Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xls");
                            Response.Redirect("Reportes\\Reporte" + tipo + nro + ".xlsx", false);
                         
                        }                       


                    }

                }

            }
        }
        

        private void ExportarRotacionConsignacion()
        {
            System.IO.StreamWriter sw = null;
            string ruta = null;
            Random rnd = new Random();

            int nro = rnd.Next(0, 8);
            string tipo = "RotacionInvConsignacion";
            ruta = Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xlsx";

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xls"))
                File.Delete(Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xls");



            List<InvRotacion> List = new List<InvRotacion>();
            InvRotacion InvRotacion = new InvRotacion();
            CN_InvRotacion CN_InvRotacion = new CN_InvRotacion();


            InvRotacion.Id_Emp = 1; 
            InvRotacion.Ano = Int32.Parse(RadComboAno.SelectedValue);
            InvRotacion.Mes = Int32.Parse(RadComboMes.SelectedValue);


            int Antepenultimo = (InvRotacion.Mes - 2 == -1) ? 11 : InvRotacion.Mes - 2;
            int Penultimo = (InvRotacion.Mes - 1 == 0) ? 12 : InvRotacion.Mes - 1;
            int Ultimo = InvRotacion.Mes;         
                                    
            CN_InvRotacion.Consulta(InvRotacion, Emp_CnxCen, ref  List);
            
            if ((List != null))
            {
                if (!(List.Count() == 0))
                {


                    using (ExcelPackage p = new ExcelPackage())
                    {

                        //set the workbook properties and add a default sheet in it
                        SetWorkbookProperties(p);
                        //Create a sheet


                        List<int> lCDI = List.Select(i => i.Id_Cd).Distinct().ToList();
                        List<RemisionTotal> lCDTOTAL = new List<RemisionTotal>();
                        int count = 1;

                        foreach (int a in lCDI)
                        {
                            DataTable dataTable = new DataTable();
                            DataColumn dcCDS = new DataColumn("Id_Cd", Type.GetType("System.Int32"));
                            DataColumn dcCte = new DataColumn("Id_Cte", Type.GetType("System.Int32"));
                            DataColumn dcCteNom = new DataColumn("Cte_NomComercial", Type.GetType("System.String"));
                            DataColumn dcIdPrd = new DataColumn("Id_Prd", Type.GetType("System.String"));
                            DataColumn dcDescripcion = new DataColumn("Prd_Descripcion", Type.GetType("System.String"));
                            DataColumn dcPresentacion = new DataColumn("Prd_Presentacion", Type.GetType("System.String"));
                            DataColumn dcUni = new DataColumn("Unidad", Type.GetType("System.String"));
                            DataColumn dcInvFinal = new DataColumn("Prd_InvFinal", Type.GetType("System.Int32"));
                            DataColumn dcPrecioAAA = new DataColumn("Prd_PrecioAAA", Type.GetType("System.Double"));
                            DataColumn dcImporteInventario = new DataColumn("ImporteInventario", Type.GetType("System.Double"));
                            DataColumn dcAntepenultimo = new DataColumn(getMonthName(Antepenultimo), Type.GetType("System.Int32"));
                            DataColumn dcPenultimo = new DataColumn(getMonthName(Penultimo), Type.GetType("System.Int32"));
                            DataColumn dcUltimo = new DataColumn(getMonthName(Ultimo), Type.GetType("System.Int32"));
                            DataColumn dcPromedio = new DataColumn("Promedio", Type.GetType("System.Double"));
                            DataColumn dcCostoPromedio = new DataColumn("CostoPromedio", Type.GetType("System.Double"));
                            DataColumn dcRotacion = new DataColumn("Rotacion", Type.GetType("System.String"));
                            DataColumn dcVigente = new DataColumn("Vigente", Type.GetType("System.Double"));
                            DataColumn dcVencido = new DataColumn("Vencido", Type.GetType("System.Double"));

                            dataTable.Columns.Add(dcCDS);
                            dataTable.Columns.Add(dcCte);
                            dataTable.Columns.Add(dcCteNom);
                            dataTable.Columns.Add(dcIdPrd);
                            dataTable.Columns.Add(dcDescripcion);
                            dataTable.Columns.Add(dcPresentacion);
                            dataTable.Columns.Add(dcUni);

                            dataTable.Columns.Add(dcInvFinal);
                            dataTable.Columns.Add(dcPrecioAAA);
                            dataTable.Columns.Add(dcImporteInventario);
                            dataTable.Columns.Add(dcAntepenultimo);
                            dataTable.Columns.Add(dcPenultimo);
                            dataTable.Columns.Add(dcUltimo);
                            dataTable.Columns.Add(dcPromedio);
                            dataTable.Columns.Add(dcCostoPromedio);
                            dataTable.Columns.Add(dcRotacion);

                            dataTable.Columns.Add(dcVigente);
                            dataTable.Columns.Add(dcVencido);

                            ExcelWorksheet ws = CreateSheet(p, getCDIName(a), count);

                            double TOTAL = 0; double TOTAL_VIGENTE = 0; double TOTAL_VENCIDO = 0; double TOTAL_IMPORTE = 0;
                            foreach (InvRotacion inv in List.FindAll(b => b.Id_Cd == a))
                            {
                                DataRow drFila = null;
                                drFila = dataTable.NewRow();
                                drFila["Id_Cd"] = inv.Id_Cd;
                                drFila["Id_Cte"] = inv.Id_Cte;
                                drFila["Cte_NomComercial"] = inv.Cte_NomComercial;
                                drFila["Id_Prd"] = inv.Id_Prd;
                                drFila["Prd_Descripcion"] = inv.Prd_Descripcion;
                                drFila["Prd_Presentacion"] = inv.Prd_Presentacion;
                                drFila["Unidad"] = inv.Id_Uni;
                                drFila["Prd_InvFinal"] = inv.Prd_InvFinal;
                                drFila["Prd_PrecioAAA"] = inv.Prd_PrecioAAA;
                                drFila["ImporteInventario"] = inv.ImporteInventario;
                                drFila[getMonthName(Antepenultimo)] = inv.Antepenultimo;
                                drFila[getMonthName(Penultimo)] = inv.Penultimo;
                                drFila[getMonthName(Ultimo)] = inv.Ultimo;
                                inv.Vigente = inv.Vigente > 0? inv.Vigente : 0;
                                inv.Vencido = inv.Vencido > 0? inv.Vencido : 0;
                                drFila["Promedio"] = inv.Promedio;
                                drFila["CostoPromedio"] = inv.CostoPromedio;
                                drFila["Rotacion"] = inv.Rotacion;
                                drFila["Vigente"] = inv.Vigente;
                                drFila["Vencido"] = inv.Vencido;


                                dataTable.Rows.Add(drFila);
                                dataTable.AcceptChanges();
                                TOTAL = TOTAL + inv.CostoPromedio;
                                TOTAL_IMPORTE = TOTAL_IMPORTE + inv.ImporteInventario;
                                TOTAL_VIGENTE = TOTAL_VIGENTE + inv.Vigente;
                                TOTAL_VENCIDO = TOTAL_VENCIDO + inv.Vencido;

                            }
                            RemisionTotal RemisionT = new RemisionTotal();
                            RemisionT.Id_Cd = a;
                            RemisionT.TotalImporte = TOTAL_IMPORTE;
                            RemisionT.Total = TOTAL;
                            RemisionT.TotalVigente = TOTAL_VIGENTE;
                            RemisionT.TotalVencido = TOTAL_VENCIDO;
                            lCDTOTAL.Add(RemisionT);



                            //Merging cells and create a center heading for out table
                            ws.Cells[1, 1].Value = "ROTACION DE INVENTARIOS DE PRODUCTO EN CONSIGNACION";
                            ws.Cells[1, 1, 1, dataTable.Columns.Count].Merge = true;
                            ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.Font.Bold = true;
                            ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                          

                            int rowIndex = 4;

                            CreateHeader(ws, ref rowIndex, dataTable);
                            CreateData(ws, ref rowIndex, dataTable);
                            // CreateFooter(ws, ref rowIndex, dt);*/

                            count++;
                        }


                        ExcelWorksheet wsGral = CreateSheet(p, "GENERAL", lCDTOTAL.Count() + 1);

                        DataTable dataTableGral = new DataTable();
                        DataColumn dcCDSGral = new DataColumn("Id_Cd", Type.GetType("System.Int32"));
                        DataColumn dcCteGral = new DataColumn("Id_Cte", Type.GetType("System.Int32"));
                        DataColumn dcCteNomGral = new DataColumn("Cte_NomComercial", Type.GetType("System.String"));
                        DataColumn dcIdPrdGral = new DataColumn("Id_Prd", Type.GetType("System.String"));
                        DataColumn dcDescripcionGral = new DataColumn("Prd_Descripcion", Type.GetType("System.String"));
                        DataColumn dcPresentacionGral = new DataColumn("Prd_Presentacion", Type.GetType("System.String"));
                        DataColumn dcUniGral = new DataColumn("Unidad", Type.GetType("System.String"));
                        DataColumn dcInvFinalGral = new DataColumn("Prd_InvFinal", Type.GetType("System.Int32"));
                        DataColumn dcPrecioAAAGral = new DataColumn("Prd_PrecioAAA", Type.GetType("System.Double"));
                        DataColumn dcImporteInventarioGral = new DataColumn("ImporteInventario", Type.GetType("System.Double"));
                        DataColumn dcAntepenultimoGral = new DataColumn(getMonthName(Antepenultimo), Type.GetType("System.Int32"));
                        DataColumn dcPenultimoGral = new DataColumn(getMonthName(Penultimo), Type.GetType("System.Int32"));
                        DataColumn dcUltimoGral = new DataColumn(getMonthName(Ultimo), Type.GetType("System.Int32"));
                        DataColumn dcPromedioGral = new DataColumn("Promedio", Type.GetType("System.Double"));
                        DataColumn dcCostoPromedioGral = new DataColumn("CostoPromedio", Type.GetType("System.Double"));
                        DataColumn dcRotacionGral = new DataColumn("Rotacion", Type.GetType("System.Double"));
                        DataColumn dcVigenteGral = new DataColumn("Vigente", Type.GetType("System.Double"));
                        DataColumn dcVencidoGral = new DataColumn("Vencido", Type.GetType("System.Double"));


                        dataTableGral.Columns.Add(dcCDSGral);
                        dataTableGral.Columns.Add(dcCteGral);
                        dataTableGral.Columns.Add(dcCteNomGral);
                        dataTableGral.Columns.Add(dcIdPrdGral);
                        dataTableGral.Columns.Add(dcDescripcionGral);
                        dataTableGral.Columns.Add(dcPresentacionGral);
                        dataTableGral.Columns.Add(dcUniGral);
                        dataTableGral.Columns.Add(dcInvFinalGral);
                        dataTableGral.Columns.Add(dcPrecioAAAGral);
                        dataTableGral.Columns.Add(dcImporteInventarioGral);
                        dataTableGral.Columns.Add(dcAntepenultimoGral);
                        dataTableGral.Columns.Add(dcPenultimoGral);
                        dataTableGral.Columns.Add(dcUltimoGral);
                        dataTableGral.Columns.Add(dcPromedioGral);
                        dataTableGral.Columns.Add(dcCostoPromedioGral);
                        dataTableGral.Columns.Add(dcRotacionGral);
                        dataTableGral.Columns.Add(dcVigenteGral);
                        dataTableGral.Columns.Add(dcVencidoGral);
                        


                        //Merging cells and create a center heading for out table
                        wsGral.Cells[1, 1].Value = "ROTACION DE INVENTARIOS DE PRODUCTO EN CONSIGNACION";
                        wsGral.Cells[1, 1, 1, 4].Merge = true;
                        wsGral.Cells[1, 1, 1, 4].Style.Font.Bold = true;
                        wsGral.Cells[1, 1, 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                        foreach (InvRotacion inv in List)
                        {
                            DataRow drFila = null;
                            drFila = dataTableGral.NewRow();
                            drFila["Id_Cd"] = inv.Id_Cd;
                            drFila["Id_Cte"] = inv.Id_Cte;
                            drFila["Cte_NomComercial"] = inv.Cte_NomComercial;
                            drFila["Id_Prd"] = inv.Id_Prd;
                            drFila["Prd_Descripcion"] = inv.Prd_Descripcion;
                            drFila["Prd_Presentacion"] = inv.Prd_Presentacion;
                            drFila["Unidad"] = inv.Id_Uni;
                            drFila["Prd_InvFinal"] = inv.Prd_InvFinal;
                            drFila["Prd_PrecioAAA"] = inv.Prd_PrecioAAA;
                            drFila["ImporteInventario"] = inv.ImporteInventario;
                            drFila[getMonthName(Antepenultimo)] = inv.Antepenultimo;
                            drFila[getMonthName(Penultimo)] = inv.Penultimo;
                            drFila[getMonthName(Ultimo)] = inv.Ultimo;
                            drFila["Promedio"] = inv.Promedio;
                            drFila["CostoPromedio"] = inv.CostoPromedio;
                            drFila["Rotacion"] = inv.Rotacion;
                            inv.Vigente = inv.Vigente > 0? inv.Vigente : 0;
                            inv.Vencido = inv.Vencido > 0? inv.Vencido : 0;
                            drFila["Vigente"] = inv.Vigente;
                            drFila["Vencido"] = inv.Vencido;


                            dataTableGral.Rows.Add(drFila);
                            dataTableGral.AcceptChanges();

                        }

                        int rowIndex1 = 4;
                        CreateHeader(wsGral, ref rowIndex1, dataTableGral);
                        CreateData(wsGral, ref rowIndex1, dataTableGral);


                        ExcelWorksheet wsResumen = CreateSheet(p, "RESUMEN", lCDTOTAL.Count() + 2);



                        wsGral.Cells[1, 1].Value = "ROTACION DE INVENTARIOS DE PRODUCTO EN CONSIGNACION";
                        wsGral.Cells[1, 1, 1, 4].Merge = true;
                        wsGral.Cells[1, 1, 1, 4].Style.Font.Bold = true;
                        wsGral.Cells[1, 1, 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                        DataTable dataTableResumen = new DataTable();
                        DataColumn dcCDSResumen = new DataColumn("CDI", Type.GetType("System.String"));
                        DataColumn dcTotalImporteResumen = new DataColumn("Total Importe del Inventario", Type.GetType("System.Double"));
                        DataColumn dcTotalResumen = new DataColumn("Total Costo Promedio", Type.GetType("System.Double"));
                        DataColumn dcVigenteResumen = new DataColumn("Total Vigente", Type.GetType("System.Double"));
                        DataColumn dcVencidoResumen = new DataColumn("Total Vencido", Type.GetType("System.Double"));
                        


                        dataTableResumen.Columns.Add(dcCDSResumen);
                        dataTableResumen.Columns.Add(dcTotalImporteResumen);
                        dataTableResumen.Columns.Add(dcTotalResumen);
                        dataTableResumen.Columns.Add(dcVigenteResumen);
                        dataTableResumen.Columns.Add(dcVencidoResumen);
                     

                        int rowIndex2 = 4;
                        CreateHeader(wsResumen, ref rowIndex2, dataTableResumen);


                        wsResumen.Cells[5, 2].Value = "MONTERREY";
                        wsResumen.Cells[5, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 110) > 0)
                        {
                            wsResumen.Cells[5, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 110).TotalImporte;
                            wsResumen.Cells[5, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 110).Total;
                            wsResumen.Cells[5, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 110).TotalVigente;
                            wsResumen.Cells[5, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 110).TotalVencido;
                            
                        }

                        wsResumen.Cells[6, 2].Value = "SALTILLO";
                        wsResumen.Cells[6, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 150) > 0)
                        {
                            wsResumen.Cells[6, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 150).TotalImporte;
                            wsResumen.Cells[6, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 150).Total;
                            wsResumen.Cells[6, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 150).TotalVigente;
                            wsResumen.Cells[6, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 150).TotalVencido;
                            
                        }


                        wsResumen.Cells[7, 2].Value = "MATAMOROS";
                        wsResumen.Cells[7, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 160) > 0)
                        {
                            wsResumen.Cells[7, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 160).TotalImporte;
                            wsResumen.Cells[7, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 160).Total;
                            wsResumen.Cells[7, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 160).TotalVigente;
                            wsResumen.Cells[7, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 160).TotalVencido;
                            
                        }
                        

                        wsResumen.Cells[8, 2].Value = "TORREON";
                        wsResumen.Cells[8, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 170) > 0)
                        {
                            wsResumen.Cells[8, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 170).TotalImporte;
                            wsResumen.Cells[8, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 170).Total;                            
                            wsResumen.Cells[8, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 170).TotalVigente;
                            wsResumen.Cells[8, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 170).TotalVencido;
                        }



                        wsResumen.Cells[9, 2].Value = "LAREDO";
                        wsResumen.Cells[9, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 180) > 0)
                        {
                            wsResumen.Cells[9, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 180).TotalImporte;
                            wsResumen.Cells[9, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 180).Total;
                            wsResumen.Cells[9, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 180).TotalVigente;
                            wsResumen.Cells[9, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 180).TotalVencido;
                            
                        }



                        wsResumen.Cells[10, 2].Value = "LEON";
                        wsResumen.Cells[10, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 190) > 0)
                        {
                            wsResumen.Cells[10, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 190).TotalImporte;
                            wsResumen.Cells[10, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 190).Total;
                            wsResumen.Cells[10, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 190).TotalVigente;
                            wsResumen.Cells[10, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 190).TotalVencido;
                            
                        }



                        wsResumen.Cells[11, 2].Value = "TIJUANA";
                        wsResumen.Cells[11, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 200) > 0)
                        {
                            wsResumen.Cells[11, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 200).TotalImporte;
                            wsResumen.Cells[11, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 200).Total;
                            wsResumen.Cells[11, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 200).TotalVigente;
                            wsResumen.Cells[11, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 200).TotalVencido;
                           
                        }

                        wsResumen.Cells[12, 2].Value = "CHIHUAHUA";
                        wsResumen.Cells[12, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 210) > 0)
                        {
                            wsResumen.Cells[12, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 210).TotalImporte;
                            wsResumen.Cells[12, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 210).Total;
                            wsResumen.Cells[12, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 210).TotalVigente;
                            wsResumen.Cells[12, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 210).TotalVencido;
                           
                        }



                        wsResumen.Cells[13, 2].Value = "SAN LUIS";
                        wsResumen.Cells[13, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 220) > 0)
                        {
                            wsResumen.Cells[13, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 220).TotalImporte;
                            wsResumen.Cells[13, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 220).Total;
                            wsResumen.Cells[13, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 220).TotalVigente;
                            wsResumen.Cells[13, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 220).TotalVencido;
                            
                        }




                        wsResumen.Cells[14, 2].Value = "JUAREZ";
                        wsResumen.Cells[14, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 230) > 0)
                        {
                            wsResumen.Cells[14, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 230).TotalImporte;
                            wsResumen.Cells[14, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 230).Total;
                            wsResumen.Cells[14, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 230).TotalVigente;
                            wsResumen.Cells[14, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 230).TotalVencido;                            
                        }



                        wsResumen.Cells[15, 2].Value = "AGUASCALIENTES";
                        wsResumen.Cells[15, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 240) > 0)
                        {
                            wsResumen.Cells[15, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 240).TotalImporte;
                            wsResumen.Cells[15, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 240).Total;                            
                            wsResumen.Cells[15, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 240).TotalVigente;
                            wsResumen.Cells[15, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 240).TotalVencido;
                        }



                        wsResumen.Cells[16, 2].Value = "MEXICO";
                        wsResumen.Cells[16, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 310) > 0)
                        {
                            wsResumen.Cells[16, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 310).TotalImporte;
                            wsResumen.Cells[16, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 310).Total;
                            wsResumen.Cells[16, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 310).TotalVigente;
                            wsResumen.Cells[16, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 310).TotalVencido;                            
                        }



                        wsResumen.Cells[17, 2].Value = "VERACRUZ";
                        wsResumen.Cells[17, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 340) > 0)
                        {
                            wsResumen.Cells[17, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 340).TotalImporte;
                            wsResumen.Cells[17, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 340).Total;
                            wsResumen.Cells[17, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 340).TotalVigente;
                            wsResumen.Cells[17, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 340).TotalVencido;                           
                        }



                        wsResumen.Cells[18, 2].Value = "CD CARMEN";
                        wsResumen.Cells[18, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 350) > 0)
                        {
                            wsResumen.Cells[18, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 350).TotalImporte;
                            wsResumen.Cells[18, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 350).Total;
                            wsResumen.Cells[18, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 350).TotalVigente;
                            wsResumen.Cells[18, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 350).TotalVencido;                           
                        }



                        wsResumen.Cells[19, 2].Value = "MERIDA";
                        wsResumen.Cells[19, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 360) > 0)
                        {
                            wsResumen.Cells[19, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 360).TotalImporte;
                            wsResumen.Cells[19, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 360).Total;
                            wsResumen.Cells[19, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 360).TotalVigente;
                            wsResumen.Cells[19, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 360).TotalVencido;                            
                        }
                        

                        wsResumen.Cells[20, 2].Value = "CANCUN";
                        wsResumen.Cells[20, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 370) > 0)
                        {
                            wsResumen.Cells[20, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 370).TotalImporte;
                            wsResumen.Cells[20, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 370).Total;
                            wsResumen.Cells[20, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 370).TotalVigente;
                            wsResumen.Cells[20, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 370).TotalVencido;
                           
                        }

                        wsResumen.Cells[21, 2].Value = "RIVIERA";
                        wsResumen.Cells[21, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 380) > 0)
                        {
                            wsResumen.Cells[21, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 380).TotalImporte;
                            wsResumen.Cells[21, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 380).Total;
                            wsResumen.Cells[21, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 380).TotalVigente;
                            wsResumen.Cells[21, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 380).TotalVencido;                            
                        }



                        wsResumen.Cells[22, 2].Value = "VALLARTA";
                        wsResumen.Cells[22, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 390) > 0)
                        {
                            wsResumen.Cells[22, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 390).TotalImporte;
                            wsResumen.Cells[22, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 390).Total;
                            wsResumen.Cells[22, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 390).TotalVigente;
                            wsResumen.Cells[22, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 390).TotalVencido;
                            
                        }



                        wsResumen.Cells[23, 2].Value = "LOS CABOS";
                        wsResumen.Cells[23, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 400) > 0)
                        {
                            wsResumen.Cells[23, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 400).TotalImporte;
                            wsResumen.Cells[23, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 400).Total;
                            wsResumen.Cells[23, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 400).TotalVigente;
                            wsResumen.Cells[23, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 400).TotalVencido;                            
                        }



                        wsResumen.Cells[24, 2].Value = "QRO";
                        wsResumen.Cells[24, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 410) > 0)
                        {
                            wsResumen.Cells[24, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 410).TotalImporte;
                            wsResumen.Cells[24, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 410).Total;
                            wsResumen.Cells[24, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 410).TotalVigente;
                            wsResumen.Cells[24, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 410).TotalVencido;                            
                        }



                        wsResumen.Cells[25, 2].Value = "GDL";
                        wsResumen.Cells[25, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 430) > 0)
                        {
                            wsResumen.Cells[25, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 430).TotalImporte;
                            wsResumen.Cells[25, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 430).Total;                            
                            wsResumen.Cells[25, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 430).TotalVigente;
                            wsResumen.Cells[25, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 430).TotalVencido;
                        }



                        wsResumen.Cells[26, 2].Value = "PUEBLA";
                        wsResumen.Cells[26, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 510) > 0)
                        {
                            wsResumen.Cells[26, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 510).TotalImporte;
                            wsResumen.Cells[26, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 510).Total;
                            wsResumen.Cells[26, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 510).TotalVigente;
                            wsResumen.Cells[26, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 510).TotalVencido;
                            
                        }


                        wsResumen.Cells[27, 2].Value = "COATZACOALCOS";
                        wsResumen.Cells[27, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 610) > 0)
                        {
                            wsResumen.Cells[27, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 610).TotalImporte;
                            wsResumen.Cells[27, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 610).Total;
                            wsResumen.Cells[27, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 610).TotalVigente;
                            wsResumen.Cells[27, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 610).TotalVencido;
                            
                        }

                        
                        wsResumen.Cells[28, 2].Value = "VILLAHERMOSA";
                        wsResumen.Cells[28, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 620) > 0)
                        {
                            wsResumen.Cells[28, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 620).TotalImporte;
                            wsResumen.Cells[28, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 620).Total;
                            wsResumen.Cells[28, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 620).TotalVigente;
                            wsResumen.Cells[28, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 620).TotalVencido;
                           
                        }
                        

                        wsResumen.Cells[29, 2].Value = "TOLUCA";
                        wsResumen.Cells[29, 2].Style.Font.Bold = true;
                        if (lCDTOTAL.Count(a => a.Id_Cd == 640) > 0)
                        {
                            wsResumen.Cells[29, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 640).TotalImporte;
                            wsResumen.Cells[29, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 640).Total;
                            wsResumen.Cells[29, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 640).TotalVigente;
                            wsResumen.Cells[29, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 640).TotalVencido;                           
                        }



                         ExcelWorksheet wsCDI = CreateSheet(p, "CDI'S", lCDTOTAL.Count() + 3 );



                        //Merging cells and create a center heading for out table
                        wsCDI.Cells[1, 1].Value = "CENTROS DE DISTRIBUCION";
                        wsCDI.Cells[1, 1, 1, 4].Merge = true;
                        wsCDI.Cells[1, 1, 1, 4].Style.Font.Bold = true;
                        wsCDI.Cells[1, 1, 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                       

                        DataTable dataTableCDI = new DataTable();
                        DataColumn dcCDSCDI = new DataColumn("CODIGO", Type.GetType("System.String"));
                        DataColumn dcExcesoCDI = new DataColumn("CDS", Type.GetType("System.String"));



                        dataTableCDI.Columns.Add(dcCDSCDI);
                        dataTableCDI.Columns.Add(dcExcesoCDI);
                        

                        int rowIndex3 = 4;
                        CreateHeader(wsCDI, ref rowIndex3, dataTableCDI);

                        wsCDI.Cells[5, 2].Value = "110";
                        wsCDI.Cells[5, 3].Style.Font.Bold = true;
                        wsCDI.Cells[5, 3].Value = "MONTERREY";

                        wsCDI.Cells[6, 2].Value = "150";
                        wsCDI.Cells[6, 3].Style.Font.Bold = true;
                        wsCDI.Cells[6, 3].Value = "SALTILLO";

                        wsCDI.Cells[7, 2].Value = "160";
                        wsCDI.Cells[7, 3].Style.Font.Bold = true;
                        wsCDI.Cells[7, 3].Value = "MATAMOROS";

                        wsCDI.Cells[8, 2].Value = "170";
                        wsCDI.Cells[8, 3].Style.Font.Bold = true;
                        wsCDI.Cells[8, 3].Value = "TORREON";

                        wsCDI.Cells[9, 2].Value = "180";
                        wsCDI.Cells[9, 3].Style.Font.Bold = true;
                        wsCDI.Cells[9, 3].Value = "LAREDO";

                        wsCDI.Cells[10, 2].Value = "190";
                        wsCDI.Cells[10, 3].Style.Font.Bold = true;
                        wsCDI.Cells[10, 3].Value = "LEON";

                        wsCDI.Cells[11, 2].Value = "200";
                        wsCDI.Cells[11, 3].Style.Font.Bold = true;
                        wsCDI.Cells[11, 3].Value = "TIJUANA";

                        wsCDI.Cells[12, 2].Value = "210";
                        wsCDI.Cells[12, 3].Style.Font.Bold = true;
                        wsCDI.Cells[12, 3].Value = "CHIHUAHUA";

                        wsCDI.Cells[13, 2].Value = "220";
                        wsCDI.Cells[13, 3].Style.Font.Bold = true;
                        wsCDI.Cells[13, 3].Value = "SAN LUIS";

                        wsCDI.Cells[14, 2].Value = "230";
                        wsCDI.Cells[14, 3].Style.Font.Bold = true;
                        wsCDI.Cells[14, 3].Value = "JUAREZ";

                        wsCDI.Cells[15, 2].Value = "240";
                        wsCDI.Cells[15, 3].Style.Font.Bold = true;
                        wsCDI.Cells[15, 3].Value = "AGUASCALIENTES";

                        wsCDI.Cells[16, 2].Value = "310";
                        wsCDI.Cells[16, 3].Style.Font.Bold = true;
                        wsCDI.Cells[16, 3].Value = "MEXICO";

                        wsCDI.Cells[17, 2].Value = "340";
                        wsCDI.Cells[17, 3].Style.Font.Bold = true;
                        wsCDI.Cells[17, 3].Value = "VERACRUZ";

                        wsCDI.Cells[18, 2].Value = "350";
                        wsCDI.Cells[18, 3].Style.Font.Bold = true;
                        wsCDI.Cells[18, 3].Value = "CD CARMEN";

                        wsCDI.Cells[19, 2].Value = "360";
                        wsCDI.Cells[19, 3].Style.Font.Bold = true;
                        wsCDI.Cells[19, 3].Value = "MERIDA";

                        wsCDI.Cells[20, 2].Value = "370";
                        wsCDI.Cells[20, 3].Style.Font.Bold = true;
                        wsCDI.Cells[20, 3].Value = "CANCUN";

                        wsCDI.Cells[21, 2].Value = "380";
                        wsCDI.Cells[21, 3].Style.Font.Bold = true;
                        wsCDI.Cells[21, 3].Value = "RIVIERA";

                        wsCDI.Cells[22, 2].Value = "390";
                        wsCDI.Cells[22, 3].Style.Font.Bold = true;
                        wsCDI.Cells[22, 3].Value = "VALLARTA";

                        wsCDI.Cells[23, 2].Value = "400";
                        wsCDI.Cells[23, 3].Style.Font.Bold = true;
                        wsCDI.Cells[23, 3].Value = "LOS CABOS";


                        wsCDI.Cells[24, 2].Value = "410";
                        wsCDI.Cells[24, 3].Style.Font.Bold = true;
                        wsCDI.Cells[24, 3].Value = "QUERETARO";

                        wsCDI.Cells[25, 2].Value = "430";
                        wsCDI.Cells[25, 3].Style.Font.Bold = true;
                        wsCDI.Cells[25, 3].Value = "GUADALAJARA";

                        wsCDI.Cells[26, 2].Value = "510";
                        wsCDI.Cells[26, 3].Style.Font.Bold = true;
                        wsCDI.Cells[26, 3].Value = "PUEBLA";


                        wsCDI.Cells[27, 2].Value = "610";
                        wsCDI.Cells[27, 3].Style.Font.Bold = true;
                        wsCDI.Cells[27, 3].Value = "COATZACOALCOS";


                        wsCDI.Cells[28, 2].Value = "620";
                        wsCDI.Cells[28, 3].Style.Font.Bold = true;
                        wsCDI.Cells[28, 3].Value = "VILLAHERMOSA";

                        wsCDI.Cells[29, 2].Value = "640";
                        wsCDI.Cells[29, 3].Style.Font.Bold = true;
                        wsCDI.Cells[29, 3].Value = "TOLUCA";



                        Byte[] bin = p.GetAsByteArray();
                        File.WriteAllBytes(ruta, bin);


                        if (File.Exists(ruta))
                        {
                            string ruta2 = null;
                            ruta2 = Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xlsx";
                            // File.Move(ruta, Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xls");
                            Response.Redirect("Reportes\\Reporte" + tipo + nro + ".xlsx", false);
                        }

                    }

                }

            }
        }



       

        private void CargarListadoReportes()
        {
            try
            {
                RadComboReporte.Items.Clear();
                RadComboReporte.Items.Add(new RadComboBoxItem("-- Seleccione --", "-1"));
                RadComboReporte.Items.Add(new RadComboBoxItem("Excesos De Inventario", "1"));
                RadComboReporte.Items.Add(new RadComboBoxItem("Estadistica de Ventas", "2"));
                RadComboReporte.Items.Add(new RadComboBoxItem("Estadistica de Remisiones Consignación", "3"));
                RadComboReporte.Items.Add(new RadComboBoxItem("Estadistica de Remisiones Equipo Arrendado", "4"));
                RadComboReporte.Items.Add(new RadComboBoxItem("Estadistica de Remisiones Pendiente Por Facturar", "5"));
                RadComboReporte.Items.Add(new RadComboBoxItem("Estadistica de Remisiones Producto No Conforme", "6"));
                RadComboReporte.Items.Add(new RadComboBoxItem("Estadistica de Remisiones Prueba", "7"));
                RadComboReporte.Items.Add(new RadComboBoxItem("Rotación de Inventario en Consignación", "8"));
                RadComboReporte.SortItems();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private void CargarListadoMes()
        {
            try
            {
                RadComboMes.Items.Clear();
                RadComboMes.Items.Add(new RadComboBoxItem("-- Seleccione --", "-1"));
                RadComboMes.Items.Add(new RadComboBoxItem("Enero", "1"));
                RadComboMes.Items.Add(new RadComboBoxItem("Febrero", "2"));
                RadComboMes.Items.Add(new RadComboBoxItem("Marzo", "3"));
                RadComboMes.Items.Add(new RadComboBoxItem("Abril", "4"));
                RadComboMes.Items.Add(new RadComboBoxItem("Mayo", "5"));
                RadComboMes.Items.Add(new RadComboBoxItem("Junio", "6"));
                RadComboMes.Items.Add(new RadComboBoxItem("Julio", "7"));
                RadComboMes.Items.Add(new RadComboBoxItem("Agosto", "8"));
                RadComboMes.Items.Add(new RadComboBoxItem("Septiembre", "9"));
                RadComboMes.Items.Add(new RadComboBoxItem("Octubre", "10"));
                RadComboMes.Items.Add(new RadComboBoxItem("Noviembre", "11"));
                RadComboMes.Items.Add(new RadComboBoxItem("Diciembre", "12"));

                RadComboMes.SortItems();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private string GetAutoNumberFormat(Type type)
        {
            string format = "General";
            if (type == typeof(int))
                format = "0";
            else if (type == typeof(uint))
                format = "0";
            else if (type == typeof(long))
                format = "0";
            else if (type == typeof(ulong))
                format = "0";
            else if (type == typeof(short))
                format = "0";
            else if (type == typeof(ushort))
                format = "0";
            else if (type == typeof(double))
                format = "0.00";
            else if (type == typeof(float))
                format = "0.00";
            else if (type == typeof(decimal))
                format = NumberFormatInfo.CurrentInfo.CurrencySymbol + " #,##0.00";
            else if (type == typeof(DateTime))
                format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern + " " + DateTimeFormatInfo.CurrentInfo.LongTimePattern;
            else if (type == typeof(string))
                format = "@";
            else if (type == typeof(bool))
                format = "\"" + bool.TrueString + "\";\"" + bool.TrueString + "\";\"" + bool.FalseString + "\"";

            return format;

        }


        private void CargarListadoAnio()
        {
            try
            {
                RadComboAno.Items.Clear();
                

                int anio = DateTime.Now.Year;
                int anioInicial = anio-5;

                int anioFinal = anio +1;
                RadComboAno.Items.Add(new RadComboBoxItem("-- Seleccione --", "-1"));
                for (int i = anioInicial; i <= anioFinal; i++)
                {
                    RadComboAno.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));                    
                }

                RadComboAno.SortItems();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {

            if (Int32.Parse(RadComboReporte.SelectedValue) == -1 )
            {
                Alerta("Seleccione un Reporte Por Favor");
                return;
            }


            if (Int32.Parse(RadComboMes.SelectedValue) == -1)
            {
                Alerta("Seleccione un Mes Por Favor");
                return;
            }

            if (Int32.Parse(RadComboAno.SelectedValue) == -1)
            {
                Alerta("Seleccione un Año Por Favor");
                return;
            }

            string accionError = string.Empty;
            string mensajeError = string.Empty;
           

            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "Exportar":
                        mensajeError = "Impresion_error";
                        if (Int32.Parse(RadComboReporte.SelectedValue) == 1)
                        {
                            this.ExportarExcesoInventario();
                            
                        }
                        if (Int32.Parse(RadComboReporte.SelectedValue) == 2)
                        {
                            this.ExportarEstadisticaVenta();
                        }
                        if (Int32.Parse(RadComboReporte.SelectedValue) == 3)
                        {
                            this.ExportarControlRemisiones(62);
                        }

                        if (Int32.Parse(RadComboReporte.SelectedValue) == 4)
                        {
                            this.ExportarControlRemisiones(72);
                        }

                        if (Int32.Parse(RadComboReporte.SelectedValue) == 5)
                        {
                            this.ExportarControlRemisiones(63);
                        }

                        if (Int32.Parse(RadComboReporte.SelectedValue) == 6)
                        {
                            this.ExportarControlRemisiones(65);
                        }

                        if (Int32.Parse(RadComboReporte.SelectedValue) == 7)
                        {
                            this.ExportarControlRemisiones(64);
                        }

                        if (Int32.Parse(RadComboReporte.SelectedValue) == 8)
                        {
                            this.ExportarRotacionConsignacion();
                        }

                        break;
                  
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, mensajeError);
                this.DisplayMensajeAlerta(mensaje);
            }
        }



        private string getCDIName(int CDI)
        {
            var name = "";
            switch (CDI)
            {
                case 110:
                    name = "MTY";
                    break;
                case 150:
                    name = "SAL";
                    break;
                case 170:
                    name = "TOR";
                    break;
                case 160:
                    name = "MAT";
                    break;
                case 180:
                    name = "LAR";
                    break;
                case 190:
                    name = "LEON";
                    break;
                case 200:
                    name = "TIJ";
                    break;
                case 210:
                    name = "CHI";
                    break;
                case 220:
                    name = "SLP";
                    break;
                case 230:
                    name = "JUA";
                    break;
                case 240:
                    name = "AGS";
                    break;
                case 310:
                    name = "MEX";
                    break;
                case 340:
                    name = "VER";
                    break;
                case 350:
                    name = "CAR";
                    break;
                case 360:
                    name = "MER";
                    break;
                case 370:
                    name = "CAN";
                    break;
                case 380:
                    name = "RIV";
                    break;
                case 390:
                    name = "VAL";
                    break;
                case 400:
                    name = "CAB";
                    break;
                case 410:
                    name = "QRO";
                    break;
                case 430:
                    name = "GDL";
                    break;
                case 510:
                    name = "PUE";
                    break;
                case 610:
                    name = "COA";
                    break;
                case 620:
                    name = "VIL";
                    break;
                case 640:
                    name = "TOL";
                    break;
                default: 
                     name = "NO";
                break;



            }
            return name;
        }




        private string getMonthName(int CDI)
        {
            var name = "";
            switch (CDI)
            {
                
                case 1:
                    name = "Ene";
                    break;
                case 2:
                    name = "Feb";
                    break;
                case 3:
                    name = "Mar";
                    break;
                case 4:
                    name = "Abr";
                    break;
                case 5:
                    name = "May";
                    break;
                case 6:
                    name = "Jun";
                    break;
                case 7:
                    name = "Jul";
                    break;
                case 8:
                    name = "Ago";
                    break;
                case 9:
                    name = "Sep";
                    break;
                case 10:
                    name = "Oct";
                    break;
                case 11:
                    name = "Nov";
                    break;
                case 12:
                    name = "Dic";
                    break;    


            }
            return name;
        }


        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("Impresion_error"))
                    Alerta(mensaje);
                else
                    Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }



        private void Alerta(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
              //  ErrorManager(ex, "Alerta");
            }
        }


           
        
    }
}
