using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SIANWEB
{
    public partial class ventana_ProAsignProductoPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("Pedido");
            DT.Columns.Add("Fecha");
            DT.Columns.Add("Sap");
            DT.Columns.Add("Terr");
            DT.Columns.Add("Cte");
            DT.Columns.Add("Nom");
            DT.Columns.Add("Credito");
            DT.Rows.Add(new string[] { "", "", "", "", "", "", ""  });
            rgAsignacion.DataSource = DT;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}