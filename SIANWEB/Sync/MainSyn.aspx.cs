using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CapaEntidad;
using CapaDatos;
using CapaNegocios;

namespace SIANWEB.MainSyn
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
                    Response.Redirect("../login.aspx" , false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        string pagina = Request.Params["Peich"].ToString();
                        string [] separadas;
                        string imagensita = "";
                        separadas=pagina.Split('/');
                        pagina = separadas[separadas.Count() - 1];

                        urll = pagina;
                        Random(ref imagensita);
                        this.Imagen1.ImageUrl = imagensita;
                        /*
                        if (pagina == "inicio.aspx")
                        {
                            urll = "Inicial.html";
                        }
                        else
                        {
                            CN_CatCriterioCitas CN_Citas = new CN_CatCriterioCitas();
                            CN_Citas.ObtienePaginaAyuda(pagina, Sesion.Emp_Cnx, ref urll);
                        }
                        */
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            
        }


        public void Random(ref string imagensit)
        {
            Random rnd = new Random();
            int numero = 0;
            numero = rnd.Next(3);
            if (numero == 0)
            {
                imagensit = "img/SyncAgenda.png";
            }
            if (numero == 1)
            {
                imagensit = "img/SyncContactos.png";
            }
            if (numero == 2)
            {
                imagensit = "img/SyncCorreos.jpg";
            }
            if (numero == 3)
            {
                imagensit = "img/SyncDirecciones.jpg";
            }
        }
    
    }
}