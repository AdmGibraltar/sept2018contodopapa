using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using System.Threading;
using System.IO;
using System.Text;

namespace SIANWEB.Busqueda
{
    public partial class Main : System.Web.UI.Page
    {
        public string strPagina = "";
        public string strpalabra = "";

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
                    Response.Redirect("../login.aspx" , false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        string palabra = Request.Params["Palabra"].ToString();
                        strpalabra = palabra;
                        BuscaEnHTML(palabra);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "Page_Load");
            }
            
        }

    #region Funciones

        private void BuscaEnHTML(string worrd)
        {
            try
            {
                List<string> listaArchivos = new List<string>();
                listaArchivos = obtenerArchivosDirectorio(Server.MapPath("~/Ayuda") + "\\*.html");
                foreach (var s in listaArchivos) BuscaEnArchivo(s, worrd);

                if (strPagina.Contains("<ul style="))
                {
                    strPagina = strPagina + "</ul>";
                }
                else
                {
                    strPagina = "No se encontraron coincidencias en la ayuda.";
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "BuscaEnHTML");
            }
        }

        public List<string> obtenerArchivosDirectorio(string rutaArchivo)
        {
            List<string> listaRuta = new List<string>();
            listaRuta = Directory.GetFiles(Path.GetDirectoryName(rutaArchivo), Path.GetFileName(rutaArchivo)).ToList();
            return listaRuta;
        }

        private void BuscaEnArchivo(string Archiv, string palabra)
            {
            try
                {
                // This text is added only once to the file.
                if (File.Exists(Archiv))
                    {
                        // Open the file to read from.
                        string readText = File.ReadAllText(Archiv);
                        if (readText.Contains(palabra))
                        {
                            if (strPagina.Length == 0)
                                { strPagina = "<ul style='font-size: 11px'>"; }
                            AgregaALaLista(ref strPagina, Archiv);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.ErrorManager(ex, "BuscaEnArchivo");
                }
            }

        private void AgregaALaLista(ref string listado, string pagina)
        {
            try
                {

                    //  pagina = pagina.Replace("C:\\SIANWEB\\_Fuentes\\SIANWEB\\Ayuda\\", "");
                    pagina = pagina.Replace(Server.MapPath("~/Ayuda\\"), "");
                    string modulo = "";
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();

                    CN_Citas.ObtieneModulo(pagina, Sesion.Emp_Cnx, ref modulo);
                    if (modulo != "")
                    {
                        listado = listado + "<li style='font-family: verdana; font-size: 12px; color: #3366FF'><a href='" + pagina +"' >" + modulo + "</a></li>";
                    }
                }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "AgregaALaLista");
            }
        }
    #endregion

        //string[] separadas;
        //string[] separada;
        //separadas = pagina.Split('/');
        //pagina = separadas[separadas.Count() - 1];
        //strPagina = "";
        //if (pagina == "inicio.aspx")
        //{
        //    strPagina = "<h1 style='font-family: verdana; font-size: xx-large; color: #3366FF'>Ayuda SIANWEB</h1>";
        //}
        //else
        //{
        //    CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();
        //    CN_Citas.GeneraPaginaAyuda(pagina, Sesion.Emp_Cnx, ref strPagina);
        //}         <%=strPagina%>      va en la aspx

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