using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SIANWEB
{
    public partial class ProOrdCompra_Gen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadGrid2_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("Codigo");
            DT.Columns.Add("Producto");
            DT.Columns.Add("Presen");
            DT.Columns.Add("Unc");
            DT.Columns.Add("Vta");
            DT.Columns.Add("prom");
            DT.Columns.Add("var");
            DT.Columns.Add("disp");
            DT.Columns.Add("mes1");
            DT.Columns.Add("mes2");
            DT.Columns.Add("mes3");
            DT.Columns.Add("sug");
            DT.Columns.Add("sel");
            DT.Columns.Add("sol");
            DT.Columns.Add("uni");
            DT.Columns.Add("Importe");

            DT.Rows.Add(new object[] { "", "", "", "", "", "", "", "", "", "", "", "", false, "", "", "" });
            rgOrden.DataSource = DT;
        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("Codigo");
            DT.Columns.Add("Proveedor");
            DT.Columns.Add("Seleccion");
            DT.Rows.Add(new object[] { "", "", false  });
            rgProveedor.DataSource = DT;
        }
    }
}