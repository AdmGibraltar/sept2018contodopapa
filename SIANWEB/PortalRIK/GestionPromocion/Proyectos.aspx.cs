using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;

namespace SIANWEB.PortalRIK.GestionPromocion
{
    public partial class Proyectos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SIANWEB.MasterPage.PortalRIK mp = Master as SIANWEB.MasterPage.PortalRIK;
            mp.CurrentPath = new List<string>() { "Gestion de la Promoción", "Proyectos" }.ToArray();

            if (!IsPostBack)
            {
                Session["activeMenu"] = 4;
            }
        }

        protected bool CargarDatosProyecto()
        {
            if (Id_Cliente != null && Id_Op != null)
            {
                return true;
            }
            return false;
        }

        protected string Id_Cliente
        {
            get
            {
                if (_idCte == null)
                {
                    _idCte = Request["Id_Cliente"];
                }
                return _idCte;
            }
        }

        protected string Id_Op
        {
            get
            {
                if (_idOp == null)
                {
                    _idOp = Request["Id_Op"];
                }
                return _idOp;
            }
        }

        protected string _idCte = null;
        protected string _idOp = null;

        protected Sesion EntidadSesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;
            }
        }
    }
}