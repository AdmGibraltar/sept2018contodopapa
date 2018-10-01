using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SIANWEB.Facturacion.Procesos
{
    public partial class ProVentInst_VentNueva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("Prod");
            DT.Columns.Add("Descripcion");
            DT.Columns.Add("Presen");
            DT.Columns.Add("Unidad");
            DT.Columns.Add("ventaanterior");
            DT.Columns.Add("ventacaptada");
            DT.Columns.Add("Precio");
            DT.Columns.Add("Importe");
            DT.Columns.Add("Docentrega");
            DT.Rows.Add(new string[] { "", "", "", "", "", "", "", "", "" });
            RadGrid1.DataSource = DT;
        }
    }
}