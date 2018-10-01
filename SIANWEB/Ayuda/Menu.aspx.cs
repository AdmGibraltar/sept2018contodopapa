using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using Telerik.Web.UI;
using System.IO;
using System.Data;

namespace SIANWEB.Ayuda
{
    public partial class Menu : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        int nivel_absoluto = 0;
        CN_MenuItem CNMenu;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);                      
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];       
                    Response.Redirect("login.aspx" , false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        CargarTreePaginas();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        #region Treeview
        protected void tvMenu_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            //txtClave.Text = tvMenu.SelectedNode.Value; //value[0];
            //txturl.Text = tvMenu.SelectedNode.ToolTip;//value[1];
            //txtdescripcion.Text = tvMenu.SelectedNode.Text;
            //txtImagen.Text = tvMenu.SelectedNode.LongDesc.Split(new string[] { "@" }, StringSplitOptions.None)[1];
            //cmbTipo.SelectedIndex = cmbTipo.FindItemIndexByValue(tvMenu.SelectedNode.LongDesc.Split(new string[] { "@" }, StringSplitOptions.None)[0]);
        }

        protected void tvMenu_NodeDrop(object sender, RadTreeNodeDragDropEventArgs e)
        {
            foreach (RadTreeNode node in e.DraggedNodes)
            {
                e.DestDragNode.Nodes.Add(node);
            }
            //  HF_Modificar.Value = "1";
        }

        #endregion


        #region Funciones

        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

                this.Title = "Ayuda SIANWeb";    //  pagina.Descripcion
                
                //CN_Pagina CapaNegocio = new CN_Pagina();
                //CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                //Session["Head" + Session.SessionID] = pagina.Path;

                //Permiso Permiso = new Permiso();
                //Permiso.Id_U = Sesion.Id_U;
                //Permiso.Id_Cd = Sesion.Id_Cd;
                //Permiso.Sm_cve = pagina.Clave;
                ////Esta clave depende de la pantalla
                //CD_PermisosU CN_PermisosU = new CD_PermisosU();
                //CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                //if (Permiso.PAccesar == true)
                //{
                //    _PermisoGuardar = Permiso.PGrabar;
                //    _PermisoModificar = Permiso.PModificar;
                //    _PermisoEliminar = Permiso.PEliminar;
                //    _PermisoImprimir = Permiso.PImprimir;
                //    if (!_PermisoGuardar)
                //    {
                //        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = _PermisoGuardar;
                //        ((RadToolBarItem)RadToolBar2.Items.FindItemByValue("save")).Visible = _PermisoGuardar;
                //    }
                //    if (!_PermisoGuardar && !_PermisoModificar)
                //    {
                //        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = _PermisoModificar;
                //        ((RadToolBarItem)RadToolBar2.Items.FindItemByValue("save")).Visible = _PermisoModificar;
                //    }
                //    ((RadToolBarItem)RadToolBar2.Items.FindItemByValue("delete")).Visible = _PermisoEliminar;
                //}
                //else
                //    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void CargarTreePaginas()
        {
            tvMenu.Nodes.Clear();
            nivel_absoluto = 0;

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CNMenu = new CN_MenuItem();
            DataTable DT = new DataTable();
            CNMenu.LlenarTreeMenu2(Sesion.Emp_Cnx, ref DT);

            DataRow[] Nivel0 = DT.Select("Niv=0");
            DataRow[] Nivel1 = DT.Select("Niv=1");
            DataRow[] Nivel2 = DT.Select("Niv=2");
            DataRow[] Nivel3 = DT.Select("Niv=3");
            DataRow[] Nivel4 = DT.Select("Niv=4");
            int i = 0;
            string[] separador = new string[2];
            separador[0] = "@";

            //Items nivel raiz
            for (i = 0; i <= Nivel0.GetUpperBound(0); i++)
            {
                RadTreeNode rtn = new RadTreeNode(Nivel0[i]["Des"].ToString(), Nivel0[i]["Cve"].ToString());
                rtn.ToolTip = Nivel0[i]["hlp"].ToString();
                rtn.LongDesc = Nivel0[i]["tipo"].ToString() + "@" + Nivel0[i]["img"].ToString();
                tvMenu.Nodes.Add(rtn);
            }
            //Items nivel 1
            for (i = 0; i <= Nivel1.GetUpperBound(0); i++)
            {
                RadTreeNode rtn = new RadTreeNode(Nivel1[i]["Des"].ToString(), Nivel1[i]["Cve"].ToString());
                rtn.ToolTip = Nivel1[i]["hlp"].ToString();
                rtn.LongDesc = Nivel1[i]["tipo"].ToString() + "@" + Nivel1[i]["img"].ToString();
                if (tvMenu.FindNodeByValue(Nivel1[i]["Pad"].ToString()) != null)
                    tvMenu.FindNodeByValue(Nivel1[i]["Pad"].ToString()).Nodes.Add(rtn);
            }
            //Items nivel 2
            for (i = 0; i <= Nivel2.GetUpperBound(0); i++)
            {
                RadTreeNode rtn = new RadTreeNode(Nivel2[i]["Des"].ToString(), Nivel2[i]["Cve"].ToString());
                rtn.ToolTip = Nivel2[i]["hlp"].ToString();
                rtn.LongDesc = Nivel2[i]["tipo"].ToString() + "@" + Nivel2[i]["img"].ToString();
                if (tvMenu.FindNodeByValue(Nivel2[i]["Pad"].ToString()) != null)
                    tvMenu.FindNodeByValue(Nivel2[i]["Pad"].ToString()).Nodes.Add(rtn);
            }
            //Items nivel 3
            for (i = 0; i <= Nivel3.GetUpperBound(0); i++)
            {
                RadTreeNode rtn = new RadTreeNode(Nivel3[i]["Des"].ToString(), Nivel3[i]["Cve"].ToString());
                rtn.ToolTip = Nivel3[i]["hlp"].ToString();
                rtn.LongDesc = Nivel3[i]["tipo"].ToString() + "@" + Nivel3[i]["img"].ToString();
                if (tvMenu.FindNodeByValue(Nivel3[i]["Pad"].ToString()) != null)
                    tvMenu.FindNodeByValue(Nivel3[i]["Pad"].ToString()).Nodes.Add(rtn);
            }
            //Items nivel 4
            for (i = 0; i <= Nivel4.GetUpperBound(0); i++)
            {
                RadTreeNode rtn = new RadTreeNode(Nivel4[i]["Des"].ToString(), Nivel4[i]["Cve"].ToString());
                rtn.ToolTip = Nivel4[i]["hlp"].ToString();
                rtn.LongDesc = Nivel4[i]["tipo"].ToString() + "@" + Nivel4[i]["img"].ToString();
                if (tvMenu.FindNodeByValue(Nivel4[i]["Pad"].ToString()) != null)
                    tvMenu.FindNodeByValue(Nivel4[i]["Pad"].ToString()).Nodes.Add(rtn);
            }
        }
        
        #endregion

        #region ErrorManager

        private void Alerta(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void ErrorManager()
        {
            try
            {
                this.lblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorManager(string Message)
        {
            try
            {
                this.lblMensaje.Text = Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }

        #endregion
    }
}