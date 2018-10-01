using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.IO;
using System.Configuration;

namespace ReportesCentral
{
    public partial class About : System.Web.UI.Page
    {
        private string Emp_CnxCen
        {
            get { return ConfigurationManager.AppSettings.Get("strConnection"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ExportarExcesoInventario();
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

        private void ExportarExcesoInventario()
        {

            DataSet dsCE = new DataSet();
            string tipo = "";
            tipo = "ExcesoInventarioCentral";
            System.IO.StreamWriter sw = null;
            string ruta = null;

            Random rnd = new Random();

            int nro = rnd.Next(0, 8);

            ruta = Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xls";

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
            exceso.ProveedorVer = 100;
            exceso.DiasVer = -1;
            exceso.Salida = 3;


            CN_InvExceso.Consulta3(exceso, Emp_CnxCen, ref  List);



            Excel.Application oApp;
            Excel.Worksheet oSheet;
            Excel.Workbook oBook;


            oApp = new Microsoft.Office.Interop.Excel.Application();
            oBook = oApp.Workbooks.Add();



            if ((List != null))
            {
                if (!(List.Count() == 0))
                {

                    List<int> lCDI = List.Select(i => i.Id_Cd).Distinct().ToList();
                    var collection = new Microsoft.Office.Interop.Excel.Worksheet[lCDI.Count()];
                    Dictionary<int, double> lCDTOTAL = new Dictionary<int, double>();
                    int count = 0;

                    foreach (int a in lCDI)
                    {
                        DataTable dataTable = new DataTable();
                        DataColumn dcCDS = new DataColumn("Id_Cd", Type.GetType("System.String"));
                        DataColumn dcProveedor = new DataColumn("Id_Pvd", Type.GetType("System.String"));
                        DataColumn dcClave = new DataColumn("Id_Prd", Type.GetType("System.String"));
                        DataColumn dcArticulo = new DataColumn("Prd_Descripcion", Type.GetType("System.String"));
                        DataColumn dcCosto = new DataColumn("Prd_Costo", Type.GetType("System.String"));
                        DataColumn dcCantidad = new DataColumn("Prd_Cantidad", Type.GetType("System.String"));
                        DataColumn dcDisponible = new DataColumn("Disponible", Type.GetType("System.String"));



                        dataTable.Columns.Add(dcCDS);
                        dataTable.Columns.Add(dcProveedor);
                        dataTable.Columns.Add(dcClave);
                        dataTable.Columns.Add(dcArticulo);
                        dataTable.Columns.Add(dcCosto);
                        dataTable.Columns.Add(dcCantidad);
                        dataTable.Columns.Add(dcDisponible);

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

                            TOTAL = TOTAL + exc.Costo;
                            dataTable.Rows.Add(drFila);
                            dataTable.AcceptChanges();

                        }
                        lCDTOTAL.Add(a, TOTAL);

                        var columns = dataTable.Columns.Count;
                        var rows = dataTable.Rows.Count;


                        collection[count] = oBook.Worksheets.Add();
                        collection[count].Name = (" " + a + "");
                        oSheet = collection[count];

                        oSheet.Cells[1, 1] = "REPORTE EXCESO DE INVENTARIO QUE NO ROTA";
                        oSheet.Cells[2, 1] = "Costo de inventario del proveedor 100 del centro " + a + " en los días Todos.";
                        oSheet.Cells[4, 2].Value2 = "Id_Cd";
                        oSheet.Cells[4, 3].Value2 = "Prov.";
                        oSheet.Cells[4, 4].Value2 = "Clave";
                        oSheet.Cells[4, 5].Value2 = "Artículo";
                        oSheet.Cells[4, 6].Value2 = "Costo";
                        oSheet.Cells[4, 7].Value2 = "Cantidad";
                        oSheet.Cells[4, 8].Value2 = "Disponible";

                        Excel.Range range = oSheet.Range["B5", String.Format("{0}{1}", GetExcelColumnName(columns), rows)];

                        object[,] data = new object[rows + 2, columns];

                        for (int rowNumber = 0; rowNumber < rows; rowNumber++)
                        {
                            for (int columnNumber = 0; columnNumber < columns; columnNumber++)
                            {
                                data[rowNumber + 2, columnNumber] = dataTable.Rows[rowNumber][columnNumber].ToString();
                            }
                        }

                        range.Value = data;
                        count++;
                    }

                    oSheet = oApp.Worksheets.Add();
                    oSheet.Name = "Resumen";
                    /*DataTable dataTable = new DataTable();
                    DataColumn dcCDS = new DataColumn("Id_Cd", Type.GetType("System.String"));
                    DataColumn dcExceso = new DataColumn("Exceso", Type.GetType("System.String"));
                    DataColumn dcMontoDiario= new DataColumn("MontoDiario", Type.GetType("System.String"));
                    DataColumn dcMetaRotacion = new DataColumn("MetaRotacion", Type.GetType("System.String"));
                    DataColumn dcDiasAfecta = new DataColumn("DiasAfecta", Type.GetType("System.String"));*/

                    var thisRange = oSheet.Range["A1"];
                    thisRange.Value = "Exceso de Inventario que No Rota";

                    var thisRange1 = oSheet.Range["C2"];
                    thisRange1.Value = "CDI";
                    var thisRange2 = oSheet.Range["D2"];
                    thisRange2.Value = "Exceso Inv. que no Rota";
                    var thisRange3 = oSheet.Range["E2"];
                    thisRange3.Value = "Meta Rotación de Inventario";
                    var thisRange4 = oSheet.Range["F2"];
                    thisRange4.Value = "Dias que Afecta la Rotación de Inventarios";

                    var thisRange5 = oSheet.Range["C3"];
                    thisRange5.Value = "MONTERREY";
                    var thisRange6 = oSheet.Range["D3"];
                    thisRange6.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 110).Value;
                    var thisRange7 = oSheet.Range["C4"];
                    thisRange7.Value = "SALTILLO";
                    var thisRange8 = oSheet.Range["D4"];
                    thisRange8.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 150).Value;
                    var thisRange9 = oSheet.Range["C5"];
                    thisRange9.Value = "MATAMOROS";
                    var thisRange10 = oSheet.Range["D5"];
                    thisRange10.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 160).Value;
                    var thisRange11 = oSheet.Range["C6"];
                    thisRange11.Value = "TORREON";
                    var thisRange13 = oSheet.Range["D6"];
                    thisRange13.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 170).Value;
                    var thisRange14 = oSheet.Range["C7"];
                    thisRange14.Value = "LAREDO";
                    var thisRange15 = oSheet.Range["D7"];
                    thisRange15.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 180).Value;
                    var thisRange16 = oSheet.Range["C8"];
                    thisRange16.Value = "LEON";
                    var thisRange17 = oSheet.Range["D8"];
                    thisRange17.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 190).Value;
                    var thisRange18 = oSheet.Range["C9"];
                    thisRange18.Value = "TIJUANA";
                    var thisRange19 = oSheet.Range["D9"];
                    thisRange19.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 200).Value;
                    var thisRange20 = oSheet.Range["C10"];
                    thisRange20.Value = "CHIHUAHUA";
                    var thisRange21 = oSheet.Range["D10"];
                    thisRange21.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 210).Value;
                    var thisRange22 = oSheet.Range["C11"];
                    thisRange22.Value = "SAN LUIS";
                    var thisRange23 = oSheet.Range["D11"];
                    thisRange23.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 220).Value;
                    var thisRange24 = oSheet.Range["C12"];
                    thisRange24.Value = "JUAREZ";
                    var thisRange25 = oSheet.Range["D12"];
                    thisRange25.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 230).Value;
                    var thisRange26 = oSheet.Range["C13"];
                    thisRange26.Value = "AGUASCALIENTES";
                    var thisRange27 = oSheet.Range["D13"];
                    thisRange27.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 240).Value;
                    var thisRange28 = oSheet.Range["C14"];
                    thisRange28.Value = "MEXICO";
                    var thisRange29 = oSheet.Range["D14"];
                    thisRange29.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 310).Value;
                    var thisRange30 = oSheet.Range["C15"];
                    thisRange30.Value = "VERACRUZ";
                    var thisRange31 = oSheet.Range["D15"];
                    thisRange31.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 340).Value;
                    var thisRange32 = oSheet.Range["C16"];
                    thisRange32.Value = "CD CARMEN";
                    var thisRange33 = oSheet.Range["D16"];
                    thisRange33.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 350).Value;

                    var thisRange34 = oSheet.Range["C17"];
                    thisRange34.Value = "MERIDA";
                    var thisRange35 = oSheet.Range["D17"];
                    thisRange35.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 360).Value;


                    var thisRange36 = oSheet.Range["C18"];
                    thisRange36.Value = "CANCUN";
                    var thisRange37 = oSheet.Range["D18"];
                    thisRange37.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 370).Value;


                    var thisRange38 = oSheet.Range["C19"];
                    thisRange38.Value = "RIVIERA";
                    var thisRange39 = oSheet.Range["D19"];
                    thisRange39.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 380).Value;

                    var thisRange40 = oSheet.Range["C20"];
                    thisRange40.Value = "LOS CABOS";
                    var thisRange41 = oSheet.Range["D20"];
                    thisRange41.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 400).Value;


                    var thisRange42 = oSheet.Range["C21"];
                    thisRange42.Value = "QRO";
                    var thisRange43 = oSheet.Range["D21"];
                    thisRange43.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 410).Value;

                    var thisRange44 = oSheet.Range["C22"];
                    thisRange44.Font.Bold = true;
                    thisRange44.Value = "GDL";
                    
                    var thisRange45 = oSheet.Range["D22"];
                    thisRange45.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 430).Value;


                    var thisRange46 = oSheet.Range["C23"];
                    thisRange46.Value = "PUEBLA";
                    var thisRange47 = oSheet.Range["D23"];
                    thisRange47.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 510).Value;


                    var thisRange48 = oSheet.Range["C24"];
                    thisRange48.Value = "COATZA";
                    var thisRange49 = oSheet.Range["D24"];
                    thisRange49.Value = lCDTOTAL.FirstOrDefault(a => a.Key == 620).Value;


                    oBook.SaveAs(ruta);
                    oBook.Close();

                    oApp.Quit();

                }

            }
        }
    }
}
