using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SIANWEB
{
    public partial class CrmCampania_Lista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Campaña");
            dt.Columns.Add("UEN");
            dt.Columns.Add("Segmento");
            dt.Columns.Add("Aplicaciones");

            dt.Rows.Add(new object[] { "", "", "", ""});

            RadGrid1.DataSource = dt;
        }
    }
}