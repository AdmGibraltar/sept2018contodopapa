using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using Telerik.Web.UI;
using CapaModelo_CC.CuentasCoorporativas;
using CapaEntidad;
using SIANWEB.Utilerias;

namespace SIANWEB
{
    public partial class Pro_CN_Vinculacion_Lista : System.Web.UI.Page
    {

        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private Sesion sesion { get { return (Sesion)Session["Sesion" + Session.SessionID]; } set { Session["Sesion" + Session.SessionID] = value; } }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();

            var permisos = new Permisos(this.Page);
            permisos.ValidarSesion();


            if (!Page.IsPostBack && sesion!=null)
            {
               
               permisos.ValidarPermisos(rtb1);

                var matrices= cn.ConsultarTodos();
                var matricesEst= matrices.Where(x=>x.CatCNac_Estructura.Any(z=> z.Id_Matriz==x.Id));

                this.cmbMatriz.DataSource = matricesEst;
                this.cmbMatriz.DataBind();

                int idMatriz = 0;
                    if(cmbMatriz.Items.Count>0)
                     idMatriz = Int32.Parse(cmbMatriz.Items[0].Value);

                this.dgClienteMatriz.DataSource = cn.ConsultarEstructura(idMatriz, sesion.Id_Emp, sesion.Id_Cd);
                this.dgClienteMatriz.DataBind();

            }

        }


        protected void cmbMatriz_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

            if (Session["Sesion" + Session.SessionID] != null)
            {
                var objSession = ((CapaEntidad.Sesion)(Session["Sesion" + Session.SessionID]));

                CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();
                int idMatriz = Int32.Parse(cmbMatriz.SelectedValue);

                this.dgClienteMatriz.DataSource = cn.ConsultarEstructura(idMatriz, objSession.Id_Emp, objSession.Id_Cd);
                this.dgClienteMatriz.DataBind();
            }
        }



        protected void dgClienteMatriz_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var objSession = ((CapaEntidad.Sesion)(Session[1]));

            if (e.CommandName == "Cancelar")
            {
                int idEst = Int32.Parse(e.CommandArgument.ToString());
                CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();
                cn.CancelarSolicitud(idEst);

                int idMatriz = Int32.Parse(cmbMatriz.SelectedValue);

                this.dgClienteMatriz.DataSource = cn.ConsultarEstructura(idMatriz, objSession.Id_Emp, objSession.Id_Cd);
                this.dgClienteMatriz.DataBind();
            }
        }


        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                var objSession = ((CapaEntidad.Sesion)(Session[1]));
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                          CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();
                         
                          int idMatriz = Int32.Parse(cmbMatriz.SelectedValue);
                          this.dgClienteMatriz.DataSource = cn.ConsultarEstructura(idMatriz, objSession.Id_Emp, objSession.Id_Cd);
                          this.dgClienteMatriz.DataBind();
                          break;
                }
            }
            catch (Exception ex)
            {
               // ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //MiServicioSIAN.ServCentralClient client = new MiServicioSIAN.ServCentralClient();
            CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();
            var sol= cn.ConsultarSolicitudes("ogc");

            //List<MiServicioSIAN.CatCNac_Solicitudes> solicWS = new List<MiServicioSIAN.CatCNac_Solicitudes>();

            //foreach (CatCNac_Solicitudes s in sol)
            //{
            //    MiServicioSIAN.CatCNac_Solicitudes sws = new MiServicioSIAN.CatCNac_Solicitudes();
            //    sws.Id = s.Id;
            //    sws.Id_Estructura = s.Id_Estructura;
            //    sws.Id_Matriz = s.Id_Matriz;


            //    solicWS.Add(sws);
            //}

            //MiServicioSIAN.CatCNac_Solicitudes[] arrSolWs = solicWS.ToArray();
            //client.GetSolicitudes(arrSolWs);
        }


    }



}