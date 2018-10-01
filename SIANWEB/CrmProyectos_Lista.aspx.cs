using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SIANWEB
{
    public partial class CrmProyectos_Lista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Clave");
            dt.Columns.Add("IdCliente");
            dt.Columns.Add("Cliente");
            dt.Columns.Add("Proyecto");
            dt.Columns.Add("Potencial");
            dt.Columns.Add("A");
            dt.Columns.Add("P");
            dt.Columns.Add("N");
            dt.Columns.Add("C");
            dt.Columns.Add("X");
            dt.Columns.Add("Avances");
            dt.Rows.Add(new object[] { "", "", "", "", "", "", "", "", "", "" });

            RadGrid1.DataSource = dt;
        }
    }
}