using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SIANWEB
{
    public partial class ProVentInst_PedidoNoCap : System.Web.UI.Page
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
            DT.Columns.Add("Frec");
            DT.Columns.Add("Sem");
            DT.Columns.Add("Ventaanterior");
            DT.Columns.Add("VentaCaptada");
            DT.Columns.Add("Precio");
            DT.Columns.Add("Importe");

            DT.Rows.Add(new object[] { "", "", "", "", "", "", "", "", "", "" });
            RadGrid1.DataSource = DT;
        }
    }
}