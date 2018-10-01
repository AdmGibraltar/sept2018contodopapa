using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CapaNegocios;
using CapaDatos;
using Telerik.Web.UI;
using SIANWEB.Utilerias;

namespace SIANWEB.CuentasCorporativas
{
    public partial class SolicitudesItem : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Int32.Parse(Request.QueryString["Id"]);

            var permisos = new Permisos(this.Page);
            permisos.ValidarSesion();

            if (!Page.IsPostBack )
            {
                 //permisos.ValidarPermisos(this.rtb);

                CN_CatCNac_Solicitudes cn = new CN_CatCNac_Solicitudes();
                var solic = cn.ConsultarItem(id);

                object objMatriz_Solic = solic;
                AsignacionCampos.AsignaCamposForma(ref objMatriz_Solic, "", this);

                object objMatriz_SolicDirFis = solic.CatCNac_Solicitudes_DirFiscal;
                AsignacionCampos.AsignaCamposForma(ref objMatriz_SolicDirFis, "", this);


                    btnAceptar.Visible= false;
                    btnRechazar.Visible = false;

                //this.cmbAsesorId.DataSource = cn.ComboAsesores(solic.Id_Matriz.Value);
                //this.cmbAsesorId.DataBind();

            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //CN_CatCNac_Solicitudes cn = new CN_CatCNac_Solicitudes(model);
            //int id = Int32.Parse(Request.QueryString["Id"]);
            //int estatus = 2;

            //cn.ActualizaSolicitud(id, estatus, txtComentariosAdministrador.Text);

            //RAM1.ResponseScripts.Add("CloseAlert('La solicitud ha sido aceptada');");

        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            //CN_CatCNac_Solicitudes cn = new CN_CatCNac_Solicitudes(model);
            //int id = Int32.Parse(Request.QueryString["Id"]);
            //int estatus = 3;

            //cn.ActualizaSolicitud(id, estatus, txtComentariosAdministrador.Text);

            //RAM1.ResponseScripts.Add("CloseAlert('La solicitud ha sido rechazada');");
        }



    }
}