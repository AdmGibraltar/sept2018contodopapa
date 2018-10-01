using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Web.UI;

namespace SIANWEB.Utilerias
{
    public class Permisos : System.Web.UI.Page
    {

        public Permisos(Page pag)
        {
            pagOri = pag;
        }

        private Sesion sesion { get { return (Sesion)Session["Sesion" + Session.SessionID]; } set { Session["Sesion" + Session.SessionID] = value; } }
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + pagOri.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + pagOri.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + pagOri.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + pagOri.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + pagOri.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + pagOri.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + pagOri.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + pagOri.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        Page pagOri { get; set; }


       public void ValidarSesion()
        {
            try
            {
                if (sesion == null)
                {
                    string[] pag = pagOri.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    pagOri.Response.Redirect("login.aspx", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ValidarPermisos(RadToolBar rtb1)
        {
            try
            {

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (Sesion != null)
                {

                    Pagina pagina = new Pagina();
                    string[] pag = pagOri.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                    if (pag.Length > 1)
                        pagina.Url = (new System.IO.FileInfo(pagOri.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                    else
                        pagina.Url = (new System.IO.FileInfo(pagOri.Request.Url.AbsolutePath)).Name;
                    CN_Pagina CapaNegocio = new CN_Pagina();
                    CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                    Session["Head" + Session.SessionID] = pagina.Path;
                    this.Title = pagina.Descripcion;
                    Permiso Permiso = new Permiso();
                    Permiso.Id_U = Sesion.Id_U;
                    Permiso.Id_Cd = Sesion.Id_Cd;
                    Permiso.Sm_cve = pagina.Clave;
                    //Esta clave depende de la pantalla

                    CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                    CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                    if (Permiso.PAccesar == true)
                    {
                        _PermisoGuardar = Permiso.PGrabar;
                        _PermisoModificar = Permiso.PModificar;
                        _PermisoEliminar = Permiso.PEliminar;
                        _PermisoImprimir = Permiso.PImprimir;

                        //rtb1.Items[6].Visible = false;
                        ////Guardar
                        //rtb1.Items[5].Visible = false;
                        ////Regresar
                        //rtb1.Items[4].Visible = false;
                        ////Eliminar
                        //rtb1.Items[3].Visible = false;
                        ////Imprimir
                        //rtb1.Items[2].Visible = false;
                        ////Correo
                        //rtb1.Items[1].Visible = false;

                    }
                    else
                        pagOri.Response.Redirect("Inicio.aspx");
                }
                //else
                //    pagOri.Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}