using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SIANWEB
{
    public partial class ProFacturaRuta_Lista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("Estatus");
            DT.Columns.Add("Usuario");
            DT.Columns.Add("Embarque");
            DT.Columns.Add("Fecha");
            DT.Columns.Add("Dia");
            DT.Columns.Add("Chofer");
            DT.Columns.Add("Camion");
            DT.Rows.Add(new string[] { "", "", "", "", "", "", "" });
            rgFactura.DataSource = DT;
        }
    }
}