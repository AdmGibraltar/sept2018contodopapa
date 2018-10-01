using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using System.Threading;
using System.IO;
using System.Text;


namespace SIANWEB.CatAyuda
{
    public partial class Main : System.Web.UI.Page
    {
        public string IdOpcionMenu = "";
        public string OpcionMenu = "";
        public string path = ".html";

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
                        if (Request.Params["IdPag"].ToString() != "")
                        {
                            IdOpcionMenu = Request.Params["IdPag"].ToString();
                            OpcionMenu = Request.Params["DescPag"].ToString();
                            this.HF_IdPag.Value = IdOpcionMenu;
                            this.HF_DescPag.Value = OpcionMenu;
                            this.HF_New.Value = "0";
                            CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();
                            CN_Citas.ObtienePaginaAyudaPorId(IdOpcionMenu, Sesion.Emp_Cnx, ref path);

                            string[] separadas;
                            string solopagina;
                            separadas = path.Split('/');
                            solopagina = separadas[separadas.Count() - 1];

                            if (solopagina == "Inicial.html")
                            {
                                path =  OpcionMenu.Replace(" ", "") + "/" + OpcionMenu.Replace(" ", "") + ".html";
                                GeneraCarpeta(OpcionMenu.Replace(" ", ""));
                                HF_Path.Value = path;
                                this.HF_New.Value = "1";
                                RadEditor1.ImageManager.ViewPaths = new string[] {"~/Ayuda/" + OpcionMenu.Replace(" ", "")};
                                RadEditor1.ImageManager.UploadPaths = new string[] {"~/Ayuda/" + OpcionMenu.Replace(" ", "")};
                                RadEditor1.ImageManager.DeletePaths = new string[] {"~/Ayuda/" + OpcionMenu.Replace(" ", "")};
                            }
                            else
                            {
                                HF_Path.Value = solopagina;
                                //  path = solopagina;
                                RadEditor1.Content = ReadFile(Server.MapPath(path));
                                //  RadEditor1.StripFormattingOptions = EditorStripFormattingOptions.ConvertWordLists | EditorStripFormattingOptions.None | EditorStripFormattingOptions.MSWord; 
                                RadEditor1.ImageManager.ViewPaths = new string[] {"~/Ayuda/" + OpcionMenu.Replace(" ", "")};
                                RadEditor1.ImageManager.UploadPaths = new string[] { "~/Ayuda/" + OpcionMenu.Replace(" ", "") };
                                RadEditor1.ImageManager.DeletePaths = new string[] {"~/Ayuda/" + OpcionMenu.Replace(" ", "")};
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
             {
                 this.ErrorManager(ex, "Page_Load");
            }
            
        }

        #region Funciones

        //protected void cmbSeccion_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    try
        //    {
        //        //  Sesion sesion = new Sesion();
        //        //  sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //        if (cmbSeccion.SelectedItem.Value == "6" || cmbSeccion.SelectedItem.Value == "0")
        //        {
        //            this.lblTexto.Enabled = false;
        //            this.txtTexto.Enabled = false;
        //            this.txtTexto.Text = "";
        //            this.lblImagen.Enabled = false;
                    
        //        }
        //        else
        //        {
        //            if (cmbSeccion.SelectedItem.Value == "7" || cmbSeccion.SelectedItem.Value == "8")
        //            {
        //                this.lblTexto.Enabled = false;
        //                this.txtTexto.Enabled = false;
        //                this.txtTexto.Text = "";
        //                this.lblImagen.Enabled = true;
        //                this.imgSubir.Enabled = true;
                        
        //            }
        //            else
        //            {
        //                this.lblTexto.Enabled = true;
        //                this.txtTexto.Enabled = true;
        //                this.txtTexto.Text = "";
        //                this.lblImagen.Enabled = false;
        //                this.imgSubir.Enabled = false;
                        
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                //Open file for writing and write content
                using (StreamWriter externalFile = new StreamWriter(this.MapPath(HF_Path.Value), false))
                {
                    externalFile.Write(RadEditor1.Content);
                }

                // hace la conexion entre la pagina y la opcion de menu
                if (this.HF_New.Value == "1")
                {
                    AsignaPagina();
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "btnGrabar_Click");
            }
        }

        #endregion


        #region Metodos


        private void GeneraCarpeta(string strnam)
        {
            try
            {
                //si no existe la carpeta temporal la creamos 
                if (!(Directory.Exists(Server.MapPath(strnam))))
                {
                    Directory.CreateDirectory(Server.MapPath(strnam));
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "GeneraCarpeta");
            }
        }


        protected string ReadFile(string path)
        {
           

                if (!System.IO.File.Exists(path))
                {
                    return string.Empty;
                }

                using (System.IO.StreamReader sr = new System.IO.StreamReader(path))
                {
                    return sr.ReadToEnd();
                }
            
        }
              
        private void AsignaPagina()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string path2 = "../Ayuda/" + this.HF_Path.Value;
                int ret = 0;
                CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();
                CN_Citas.AsignaPaginaAyudaPorId(Convert.ToInt32(this.HF_IdPag.Value), Sesion.Emp_Cnx, path2, ref ret);
                if (ret == 1)
                {
                    this.ErrorManager("Pagina Asignada Correctamente");
                    
                    IdOpcionMenu = this.HF_IdPag.Value;
                    OpcionMenu = this.HF_DescPag.Value;
                    path = "<b>" + this.HF_Path.Value + "</b>";

                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex,"AsignaPagina");
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