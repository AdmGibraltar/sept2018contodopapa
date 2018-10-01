using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SIANWEB
{
    public partial class ProAdmin_OrdCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("Estatus");
            DT.Columns.Add("Orden");
            DT.Columns.Add("Fecha");
            DT.Columns.Add("Num");
            DT.Columns.Add("Proveedor");
            DT.Rows.Add(new object[] { "", "", "", "", "" });
            RadGrid1.DataSource = DT;
        }
    }
}