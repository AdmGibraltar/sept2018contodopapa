using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SIANWEB
{
    public partial class ProContratosPendientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("Acuerdo");
            DT.Columns.Add("Cliente");
            DT.Columns.Add("Nombre");
            DT.Columns.Add("Ter");
            DT.Columns.Add("Rik");
            DT.Columns.Add("Contrato");
            DT.Columns.Add("Folio");
            DT.Columns.Add("Fecha");
            DT.Rows.Add(new string[] { "", "", "", "", "", "", "",  "" });
            RadGrid1.DataSource = DT;
        }
    }
}