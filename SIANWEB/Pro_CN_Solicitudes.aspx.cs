using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using SIANWEB.Utilerias;
using CapaEntidad;

namespace SIANWEB
{
    public partial class Pro_CN_Solicitudes : System.Web.UI.Page
    {

        private Sesion sesion { get { return (Sesion)Session["Sesion" + Session.SessionID]; } set { Session["Sesion" + Session.SessionID] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
           

            var permisos = new Permisos(this.Page);
            permisos.ValidarSesion();
            if (!Page.IsPostBack && sesion != null)
            {

              
                 //permisos.ValidarPermisos(this.rtb);

                CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();

                this.lblSucursal.Text = sesion.Cd_Nombre;
                lblUsuario.Text = sesion.Cu_User;

                this.dgSolicitudes.DataSource = cn.ConsultarSolicitudes(sesion.Cu_User);
                dgSolicitudes.DataBind();

            }

        }
    }
}