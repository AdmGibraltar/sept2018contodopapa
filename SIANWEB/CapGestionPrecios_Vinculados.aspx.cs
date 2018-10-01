using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Text;

namespace SIANWEB
{
    public partial class CapGestionPrecios_Vinculados : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
       
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
                    Response.Redirect("login.aspx", false);                  
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
         

                        HFId_PC.Value =  Page.Request.QueryString["Id_PC"].ToString();
                        HFId_Cd.Value = Page.Request.QueryString["Id_Cd"].ToString();
                        this.LblPC_NoConvenio.Text = Page.Request.QueryString["PC_NoConvenio"].ToString();
                        this.LblPC_Nombre.Text = Page.Request.QueryString["PC_Nombre"].ToString();
                        this.LblId_CatStr.Text = Page.Request.QueryString["Id_CatStr"].ToString();

                        rgVinculados.Rebind();
         
                        
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                    if (btn.CommandName == "print")
                    {
                            Imprimir();
                    }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgVinculados_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                    rgVinculados.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                   
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rgVinculados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (HFId_Cd.Value != "-1")
                {
                    item["Cancelar"].Visible = false;
                }

            }

            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem item = (GridHeaderItem)e.Item;
                if (HFId_Cd.Value != "-1")
                {
                    item["Cancelar"].Visible = false;
                  
                }
            }
           
        }
        protected void rgVinculados_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                
                    case "Cancelar":
                        Cancelar(e);
                        break;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
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

                    if (Permiso.PGrabar == false)
                        this.rtb1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");


                //if (ConsultarAutorizacionPrecio() == "True")
                //{

                //    this.rg1.Columns[11].Visible = true ;
                //}

                //else
                //{
                //    this.rg1.Columns[11].Visible = false;
                
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<SolConvenioDet> GetList()
        {
            try
            {
                List<SolConvenioDet> List = new List<SolConvenioDet>();
                CN_Convenio cn_conv = new CN_Convenio();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                cn_conv.Convenio_ConsultaVinculados(int.Parse(HFId_PC.Value), int.Parse(HFId_Cd.Value), ref List, Conexion);

                return List;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void Cancelar(GridCommandEventArgs e)
        {
            try
            {
                GridItem item = e.Item;
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                SolConvenioDet sol = new SolConvenioDet();
                CN_Convenio cn_conv = new CN_Convenio();
                int Verificador = 0;
                sol.Id_Sol = int.Parse(this.rgVinculados.MasterTableView.Items[e.Item.ItemIndex]["Id_Sol"].Text);
                sol.Id_PC = int.Parse(this.rgVinculados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                sol.Id_Cte = int.Parse(this.rgVinculados.MasterTableView.Items[e.Item.ItemIndex]["Id_Cte"].Text);
                sol.U_Nombre = sesion.U_Nombre;
                cn_conv.Convenio_DesvinculaUno(sol, ref Verificador, Conexion);


                if (Verificador == -1)
                {
                    cn_conv.ConvenioDesvinculo_EnviarCorreo(sol, sesion.Emp_Cnx);
                    Alerta("Se desvínculo el cliente de manera exitosa");
                    rgVinculados.Rebind();
                }
                else
                {
                    Alerta("Error al tratar de desvincular al cliente");
                }
               
                

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void Imprimir()
        {
            try
            {

                StringBuilder tabla = new StringBuilder();
                tabla.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'></head><body><table style='width:700px'>");
                EscribeEncabezado(ref tabla);
                EscribeDetalle(ref tabla);
                tabla.Append("</table></body></html>");
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.ExportarExcel("Clientes_Vinc_Conv_" + this.LblPC_NoConvenio.Text +  "", tabla.ToString());

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void EscribeEncabezado(ref StringBuilder Tabla)
        {
            try
            {
                

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Tabla.Append("<tr>");
                Tabla.Append("<td width='5px'><img src='http://" + HttpContext.Current.Request.Url.Authority + "/Imagenes/Logo.png'></td>");
                Tabla.Append("<td colspan ='5' style='width:400px; text-align:right; font-weight:bold'> Fecha impresión:  " + DateTime.Now.ToString() + " <br/> Usuario: " + sesion.U_Nombre + " </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td  colspan='6' style='width:400px; text-align:center; font-weight:bold'>&nbsp; Clientes vinculados </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td style='text-align:left;font-weight:bold' colspan='1'>  </td> <td style='text-align:left;font-weight:bold'> </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td style='text-align:left;font-weight:bold'  colspan='1'> No. Convenio: </td> <td style='text-align:left;font-weight:bold'> "+ this.LblPC_NoConvenio.Text +"</td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td style='text-align:left;font-weight:bold'  colspan='1'> Nombre: </td> <td style='text-align:left;font-weight:bold'> "+ this.LblPC_Nombre.Text +"</td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td style='text-align:left;font-weight:bold'  colspan='1'> Categoría:   </td> <td style='text-align:left;font-weight:bold'> "+this.LblId_CatStr.Text + " </td>");
                Tabla.Append("</tr>");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void EscribeDetalle(ref StringBuilder Tabla)
        {
            try
            {
                String width;

                System.Data.DataTable dt = new System.Data.DataTable();
                List<SolConvenioDet> List = new List<SolConvenioDet>();

                List = GetList();

                Funcion fn = new Funcion();
                dt = Funcion.Convertidor<SolConvenioDet>.ListaToDatatable(List);


                Tabla.Append("<tr>");

                //lectura y escritura de columnas
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    if (dt.Columns[i].ColumnName == "Id_Cte")
                    {
                        width = (i == 0) ? "70px" : "90px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Núm");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Sol_CteNombre")
                    {
                        width = (i == 0) ? "250px" : "280px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Cliente");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "SolTer_Nombre")
                    {
                        width = (i == 0) ? "220px" : "250px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Territorio");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Sol_UsuFinal")
                    {
                        width = (i == 0) ? "90px" : "120px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Usuario final");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "CDI")
                    {
                        width = (i == 0) ? "150px" : "180px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("CDI (Zona)");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Sol_UNombre")
                    {
                        width = (i == 0) ? "120px" : "150px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Usuario");
                        Tabla.Append("</th>");
                    }
  


                }
                Tabla.Append("</tr>");
                // lectura y escritura de filas
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Tabla.Append("<tr>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        if (dt.Columns[i].ColumnName == "Id_Cte" )
                        {
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Sol_CteNombre" )
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "SolTer_Nombre")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Sol_UsuFinal")
                        {
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "CDI")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Sol_UNombre")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }



                        //else
                        //{
                        //    //Domingos
                        //    if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -99)
                        //    {
                        //        Tabla.Append("<td bgcolor='#0066FF' style='text-align:center'>");
                        //        Tabla.Append("0");
                        //        Tabla.Append("</td>");

                        //    }
                        //    //Incumplimiento
                        //    else if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -98)
                        //    {
                        //        Tabla.Append("<td bgcolor='Yellow' style='text-align:center'>");
                        //        Tabla.Append("0");
                        //        Tabla.Append("</td>");
                        //    }
                        //    //Asueto
                        //    else if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -1)
                        //    {
                        //        Tabla.Append("<td bgcolor='Red' style='text-align:center'>");
                        //        Tabla.Append("0");
                        //        Tabla.Append("</td>");
                        //    }
                        //    //Curso
                        //    else if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -2)
                        //    {
                        //        Tabla.Append("<td bgcolor='Orange' style='text-align:center'>");
                        //        Tabla.Append("0");
                        //        Tabla.Append("</td>");
                        //    }
                        //    //Incapacidad
                        //    else if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -3)
                        //    {
                        //        Tabla.Append("<td bgcolor='Green' style='text-align:center'>");
                        //        Tabla.Append("0");
                        //        Tabla.Append("</td>");

                        //    }
                        //    //Permiso
                        //    else if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -4)
                        //    {
                        //        Tabla.Append("<td bgcolor='silver' style='text-align:center'>");
                        //        Tabla.Append("0");
                        //        Tabla.Append("</td>");
                        //    }


                        //    else
                        //    {
                        //        Tabla.Append("<td   style='text-align:center'>");
                        //        Tabla.Append(dt.Rows[j][i].ToString());
                        //        Tabla.Append("</td>");

                        //    }

                        //}






                    }
                }
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>");
                Tabla.Append("&nbsp; &nbsp;</td>");
                Tabla.Append("</tr>");



            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region ErrorManager
        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void AlertaCerrar(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RAM1.ResponseScripts.Add("CloseWindowA('" + mensaje + "');");
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