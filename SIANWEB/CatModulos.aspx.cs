using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaDatos;
using System.Data;
using Telerik.Web.UI;
using CapaNegocios;
using System.IO;
using System.Text;

namespace SIANWEB
{
    public partial class CatModulos : System.Web.UI.Page
    {
        #region Variables

        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        int nivel_absoluto = 0;
        CN_MenuItem CNMenu;
        public string strURL = "";
        #endregion
        #region Eventos
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
                        CargarCentros();
                        CargarTipos();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void CargarTipos()
        {
            cmbTipo.Items.Add(new RadComboBoxItem("-- Seleccionar --", "0"));
            cmbTipo.Items.Add(new RadComboBoxItem("Catálogos", "1"));
            cmbTipo.Items.Add(new RadComboBoxItem("Capturas", "2"));
            cmbTipo.Items.Add(new RadComboBoxItem("Procesos", "3"));
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            RadToolBarButton btn = e.Item as RadToolBarButton;
            try
            {
                ErrorManager();
                if (btn.CommandName == "save")
                {
                    Guardar();
                }
                else if (btn.CommandName == "up")
                {
                    UpDown(-1);
                    HF_Modificar.Value = "1";
                }
                else if (btn.CommandName == "down")
                {
                    UpDown(1);
                    HF_Modificar.Value = "1";
                }
                else if (btn.CommandName == "left")
                {
                    izquierda();
                    HF_Modificar.Value = "1";
                }
                else if (btn.CommandName == "right")
                {
                    derecha();
                    HF_Modificar.Value = "1";
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_ItemCommand");
            }

        }
        protected void RadToolBar2_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            RadToolBarButton btn = e.Item as RadToolBarButton;
            CNMenu = new CN_MenuItem();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            try
            {
                if (btn.CommandName == "save")
                {
                    guardar_item(Sesion.Emp_Cnx);
                }
                else if (btn.CommandName == "new")
                {
                    Nuevo();
                }
                else if (btn.CommandName == "delete")
                {
                    eliminar_item(Sesion.Emp_Cnx);
                }


            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadToolBar2_ButtonClick");
            }
        }
        protected void tvMenu_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            txtClave.Text = tvMenu.SelectedNode.Value; //value[0];
            txturl.Text = tvMenu.SelectedNode.ToolTip;//value[1];
            txtdescripcion.Text = tvMenu.SelectedNode.Text;
            txtImagen.Text = tvMenu.SelectedNode.LongDesc.Split(new string[] { "@" }, StringSplitOptions.None)[1];
            cmbTipo.SelectedIndex = cmbTipo.FindItemIndexByValue(tvMenu.SelectedNode.LongDesc.Split(new string[] { "@" }, StringSplitOptions.None)[0]);
            strURL = "'../Ayuda/CatAyuda.aspx?IdPag=" + tvMenu.SelectedNode.Value + "'";

            string path2 = "";
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();
            CN_Citas.ObtienePaginaAyudaPorId(txtClave.Text, Sesion.Emp_Cnx, ref path2);
            txthlp.Text = path2;
        }
        protected void tvMenu_NodeDrop(object sender, RadTreeNodeDragDropEventArgs e)
        {
            foreach (RadTreeNode node in e.DraggedNodes)
            {
                e.DestDragNode.Nodes.Add(node);
            }
            HF_Modificar.Value = "1";
        }
        #endregion
        #region Funciones
        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();


                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
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
               
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla
                CD_PermisosU CN_PermisosU = new CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                    if (!_PermisoGuardar)
                    {
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = _PermisoGuardar;
                        ((RadToolBarItem)RadToolBar2.Items.FindItemByValue("save")).Visible = _PermisoGuardar;
                    }
                    if (!_PermisoGuardar && !_PermisoModificar)
                    {
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = _PermisoModificar;
                        ((RadToolBarItem)RadToolBar2.Items.FindItemByValue("save")).Visible = _PermisoModificar;
                    }
                    ((RadToolBarItem)RadToolBar2.Items.FindItemByValue("delete")).Visible = _PermisoEliminar;
                }
                else
                    Response.Redirect("Inicio.aspx");               
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
            CNMenu.LlenarTreeMenu(Sesion.Emp_Cnx, ref DT);

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
                rtn.ToolTip = Nivel0[i]["href"].ToString();
                rtn.LongDesc = Nivel0[i]["tipo"].ToString() + "@" + Nivel0[i]["img"].ToString();
                tvMenu.Nodes.Add(rtn);
            }
            //Items nivel 1
            for (i = 0; i <= Nivel1.GetUpperBound(0); i++)
            {
                RadTreeNode rtn = new RadTreeNode(Nivel1[i]["Des"].ToString(), Nivel1[i]["Cve"].ToString());
                rtn.ToolTip = Nivel1[i]["href"].ToString();
                rtn.LongDesc = Nivel1[i]["tipo"].ToString() + "@" + Nivel1[i]["img"].ToString();
                if (tvMenu.FindNodeByValue(Nivel1[i]["Pad"].ToString()) != null)
                    tvMenu.FindNodeByValue(Nivel1[i]["Pad"].ToString()).Nodes.Add(rtn);
            }
            //Items nivel 2
            for (i = 0; i <= Nivel2.GetUpperBound(0); i++)
            {
                RadTreeNode rtn = new RadTreeNode(Nivel2[i]["Des"].ToString(), Nivel2[i]["Cve"].ToString());
                rtn.ToolTip = Nivel2[i]["href"].ToString();
                rtn.LongDesc = Nivel2[i]["tipo"].ToString() + "@" + Nivel2[i]["img"].ToString();
                if (tvMenu.FindNodeByValue(Nivel2[i]["Pad"].ToString()) != null)               
                    tvMenu.FindNodeByValue(Nivel2[i]["Pad"].ToString()).Nodes.Add(rtn);
            }
            //Items nivel 3
            for (i = 0; i <= Nivel3.GetUpperBound(0); i++)
            {
                RadTreeNode rtn = new RadTreeNode(Nivel3[i]["Des"].ToString(), Nivel3[i]["Cve"].ToString());
                rtn.ToolTip = Nivel3[i]["href"].ToString();
                rtn.LongDesc = Nivel3[i]["tipo"].ToString() + "@" + Nivel3[i]["img"].ToString();
                if (tvMenu.FindNodeByValue(Nivel3[i]["Pad"].ToString()) != null)               
                    tvMenu.FindNodeByValue(Nivel3[i]["Pad"].ToString()).Nodes.Add(rtn);               
            }
            //Items nivel 4
            for (i = 0; i <= Nivel4.GetUpperBound(0); i++)
            {
                RadTreeNode rtn = new RadTreeNode(Nivel4[i]["Des"].ToString(), Nivel4[i]["Cve"].ToString());
                rtn.ToolTip = Nivel4[i]["href"].ToString();
                rtn.LongDesc = Nivel4[i]["tipo"].ToString() + "@" + Nivel4[i]["img"].ToString();
                if (tvMenu.FindNodeByValue(Nivel4[i]["Pad"].ToString()) != null)                
                    tvMenu.FindNodeByValue(Nivel4[i]["Pad"].ToString()).Nodes.Add(rtn);              
            }
        }
        //TREEVIEW
        private void UpDown(int accion)
        {
            if ((accion == -1 & tvMenu.SelectedNode.Prev == null) | (accion == 1 & tvMenu.SelectedNode.Next == null))           
                return;           

            RadTreeNodeCollection SelectedParentNode = default(RadTreeNodeCollection);
            if (tvMenu.SelectedNode.ParentNode != null)           
                SelectedParentNode = tvMenu.SelectedNode.ParentNode.Nodes;          
            else          
                SelectedParentNode = tvMenu.Nodes;           

            RadTreeNode SelectedNode = tvMenu.SelectedNode;
            int SelectedIndex = SelectedNode.Index;
            SelectedParentNode.Remove(SelectedNode);
            SelectedParentNode.Insert(SelectedIndex + accion, SelectedNode);
        }
        protected void izquierda()
        {
            RadTreeNode selected_parent = tvMenu.SelectedNode.ParentNode;
            if (tvMenu.SelectedNode.ParentNode.ParentNode == null)
            {
                int i = tvMenu.SelectedNode.ParentNode.Index;
                tvMenu.Nodes.Insert(i + 1, tvMenu.SelectedNode);
            }
            else           
                tvMenu.SelectedNode.ParentNode.ParentNode.Nodes.Add(tvMenu.SelectedNode);           
            selected_parent.Expanded = false;
        }
        protected void derecha()
        {
            RadTreeNode SelectedNode = tvMenu.SelectedNode;
            tvMenu.SelectedNode.Prev.Nodes.Add(SelectedNode);
            tvMenu.SelectedNode.ParentNode.Expanded = true;
        }
        private void Guardar()
        {
            try
            {
                if (HF_Modificar.Value == "0")
                {
                    Alerta("No se ha realizado ningún cambio");
                    return;
                }
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permiso para guardar");
                    return;
                }
                if (!_PermisoModificar)
                {
                    Alerta("No tiene permiso para modificar");
                    return;
                }
                List<CapaEntidad.MenuItem> list = new List<CapaEntidad.MenuItem>();
                guardar_nodos(ref list, 0, "NULL", tvMenu.Nodes);
                CNMenu = new CN_MenuItem();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int verificador = 0;
                CNMenu.CatModulosInsertar(list, Sesion.Emp_Cnx, ref verificador);
                if (verificador == 1)
                {
                    Alerta("Los cambios se guardaron correctamente");
                    HF_Modificar.Value = "0";
                }
                else              
                    Alerta("Ocurrió un error al intentar guardar los cambios");               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void guardar_nodos(ref List<CapaEntidad.MenuItem> list, int nivel, string ipadre, RadTreeNodeCollection nodes)
        {
            try
            {
                CapaEntidad.MenuItem menuitem;
                string padre = "NULL";
                string url = "NULL";
                string tipo = "";
                string img = "";
                
                for (int i = 0; i <= nodes.Count - 1; i++)
                {
                    tipo = nodes[i].LongDesc.Split(new string[] { "@" }, StringSplitOptions.None)[0];
                    img = nodes[i].LongDesc.Split(new string[] { "@" }, StringSplitOptions.None)[1];
                    url = nodes[i].ToolTip == "" ? "NULL" : nodes[i].ToolTip;

                    if (nivel > 0)                   
                        padre = ipadre;                  

                    menuitem = new CapaEntidad.MenuItem();
                    menuitem.cve = nodes[i].Value;
                    menuitem.cve_p = padre;
                    nivel_absoluto += 1;
                    menuitem.ord = nivel_absoluto.ToString();
                    menuitem.desc = nodes[i].Text;
                    menuitem.href = url;
                    menuitem.nivel = nivel;
                    menuitem.Img = img;
                    menuitem.Tipo = Convert.ToInt32(tipo == "" ? "0" : tipo);

                    list.Add(menuitem);
                    if (nodes[i].Nodes.Count > 0)                   
                        guardar_nodos(ref list, nivel + 1, nodes[i].Value.ToString(), nodes[i].Nodes);                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void eliminar_item(string conexion)
        {
            if (txtClave.Text.Length == 0)           
                Alerta("No se ha seleccionado ningún módulo");          
            else
            {
                int verificador = 0;
                CNMenu.CatModulosEliminar(txtClave.Text, conexion, ref verificador);
                if (verificador == 1)
                {
                    Alerta("Los datos se eliminaron correctamente");
                    CargarTreePaginas();
                    Nuevo();
                }
                else               
                    Alerta("Ocurrió un error al intentar eliminar");             
            }
        }
        private void guardar_item(string conexion)
        {
            int verificador = 0;
            CapaEntidad.MenuItem menuitem = new CapaEntidad.MenuItem();
            menuitem.desc = txtdescripcion.Text;
            menuitem.href = txturl.Text == "" ? "NULL" : txturl.Text;
            menuitem.Img = txtImagen.Text;
            menuitem.nivel = 0;
            menuitem.ord = "0";
            menuitem.Tipo = Convert.ToInt32(cmbTipo.SelectedValue);
            if (txtClave.Text == "")
            {
                menuitem.cve = "NULL";
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permiso para guardar");
                    return;
                }              
                CNMenu.CatModulosInsertar(menuitem, conexion, ref verificador);
                if (verificador == 1)
                {
                    Alerta("Los datos se guardaron correctamente");
                    CargarTreePaginas();
                    Nuevo();
                }
                else               
                    Alerta("Ocurrió un error al intentar guardar los datos");               
            }
            else
            {
                menuitem.cve = txtClave.Text;               
                if (!_PermisoModificar)
                {
                    Alerta("No tiene permiso para modificar");
                    return;
                }
                CNMenu.CatModulosModificar(menuitem, conexion, ref verificador);
                if (verificador == 1)
                {
                    Alerta("Los datos se modificaron correctamente");
                    CargarTreePaginas();
                    Nuevo();
                }
                else              
                    Alerta("Ocurrió un error al intentar modificar los datos");             
            }
        }
        private void Nuevo()
        {
            txtClave.Text = string.Empty;

            txtdescripcion.Text = string.Empty;
            txtImagen.Text = string.Empty;
            txturl.Text = string.Empty;
            cmbTipo.SelectedIndex = 0;
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