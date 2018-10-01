using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;

namespace SIANWEB.PortalRIK.GestionPromocion.Propuestas
{
    public partial class UCPropuestaTecnicaResultados : System.Web.UI.UserControl
    {
        public Sesion Sesion
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    return (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                }
                return null;
            }
        }

        public int IdCte
        {
            get
            {
                int idCte = 0;
                string strIdCte=Request["idCte"];
                try
                {
                    idCte = int.Parse(strIdCte);
                }
                catch (Exception ex)
                {
                }
                
                return idCte;
            }
        }

        public int IdVal
        {
            get
            {
                int idVal = 0;
                string strIdVal = Request["idVal"];
                try
                {
                    idVal = int.Parse(strIdVal);
                }
                catch (Exception ex)
                {
                }

                return idVal;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                System.Web.UI.WebControls.SessionParameter s = (System.Web.UI.WebControls.SessionParameter)ObjectDataSource1.SelectParameters["s"];
                s.SessionField = "Sesion" + HttpContext.Current.Session.SessionID;
                //HttpContext.Current.Session.Add("CustomSession", Sesion);
            }
        }

    }
}