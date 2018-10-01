using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CapaEntidad;
using CapaDatos;
using CapaNegocios;

namespace SIANWEB.Ayuda
{
    public partial class Main : System.Web.UI.Page
    {
        public string urll = "";

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
                        string pagina = Request.Params["Peich"].ToString();
                        string [] separadas;
//                          string[] separada;
                        separadas=pagina.Split('/');
                        pagina = separadas[separadas.Count() - 1];

                        if (pagina == "inicio.aspx")
                        {
                            urll = "Inicial.html";
                        }
                        else
                        {
                            CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();
                            CN_Citas.ObtienePaginaAyuda(pagina, Sesion.Emp_Cnx, ref urll);
                            //  separada = pagina.Split('.');   // +".aspx";
                            //  urll = separada[0] + ".html";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            
        }
    }
}