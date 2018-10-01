using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
 
namespace SIANWEB
{
    public partial class CapPago_Dif : System.Web.UI.Page
    {
        private DataTable dtGral
        {
            get { return (DataTable)Session["dtGralPagos" + Session.SessionID]; }
            set { Session["dtGralPagos" + Session.SessionID] = value; }
        }
        private DataTable dtDet
        {
            get
            {
                return (DataTable)Session["dtDetPagos" + Session.SessionID];
            }
            set
            {
                Session["dtDetPagos" + Session.SessionID] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt_Dif = new DataTable();
            dt_Dif.Columns.Add("Ficha");
            dt_Dif.Columns.Add("Gral");
            dt_Dif.Columns.Add("Det");
            dt_Dif.Columns.Add("Valido");

            DataRow[] Ar_Dr = null;
            double gral = 0;
            double sum = 0;
            for (int x = 0; x < dtGral.Rows.Count; x++)
            {
                sum = 0;
                gral = Convert.ToDouble(dtGral.Rows[x]["Pag_Importe"]);
                Ar_Dr = dtDet.Select("Pag_Numero='" + dtGral.Rows[x]["Pag_ficha"] + "'");
                for (int y = 0; y < Ar_Dr.Length; y++)
                {
                    sum += Convert.ToDouble(Ar_Dr[y]["Pag_Importe"]);
                }
                string valido = " ";
                if (gral != sum)
                    valido = "*";
                dt_Dif.Rows.Add(new object[] { dtGral.Rows[x]["Pag_ficha"], gral, sum, valido});
            }

            RadGrid1.DataSource = dt_Dif;
            RadGrid1.DataBind();
        }
    }
}