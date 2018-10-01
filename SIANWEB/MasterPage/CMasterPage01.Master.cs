using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using System.Data;

namespace SIANWEB
{
    public partial class CMasterPage01 : System.Web.UI.MasterPage
    {
        #region "Propiedades"
        public int AñoInicio
        {
            get
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (sesion == null)
                {
                    return 1900;
                }
                else
                {
                    return ((Sesion)Session["Sesion" + Session.SessionID]).CalendarioIni.Year;
                }

            }
            set { }
        }
        public int MesInicio
        {
            get
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (sesion == null)
                {
                    return 1;
                }
                else
                {
                    return (((Sesion)Session["Sesion" + Session.SessionID]).CalendarioIni.Month - 1);
                }

            }
            set { }
        }
        public int DiaInicio
        {
            get
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (sesion == null)
                {
                    return 1;
                }
                else
                {
                    return ((Sesion)Session["Sesion" + Session.SessionID]).CalendarioIni.Day;
                }

            }
            set { }
        }
        public int AñoFin
        {
            get
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (sesion == null)
                {
                    return 1900;
                }
                else
                {
                    return ((Sesion)Session["Sesion" + Session.SessionID]).CalendarioFin.Year;
                }

            }
            set { }
        }
        public int MesFin
        {
            get
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (sesion == null)
                {
                    return 1;
                }
                else
                {
                    return (((Sesion)Session["Sesion" + Session.SessionID]).CalendarioFin.Month - 1);
                }

            }
            set { }
        }
        public int DiaFin
        {
            get
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (sesion == null)
                {
                    return 1;
                }
                else
                {
                    return ((Sesion)Session["Sesion" + Session.SessionID]).CalendarioFin.Day;
                }

            }
            set { }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsPostBack == false)
                {
                    HiddenField1.Value = Session.SessionID;
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    //Asigna los valores del encabezado
                    //this.RadSkinManager1.Skin = Sesion.Skin_Descripcion;
                    lblNombre.Text = "*" + Sesion.U_Nombre;

                    //CambiaLogo(Sesion.Emp_Cnx, Sesion.Emp_Pref);
                    //ValidarPermisos(Sesion.Emp_Cnx, Sesion.Id_U, Sesion.Id_Ofi);
                    //Carga el menú
                    DataTable DT = (DataTable)Session["DTMenu" + Session.SessionID];
                    this.RadMenu1.DataSource = DT;
                    this.RadMenu1.DataFieldID = "Sm_Cve";
                    this.RadMenu1.DataFieldParentID = "Sm_Sm_Cve";
                    this.RadMenu1.DataNavigateUrlField = "Sm_Href";
                    this.RadMenu1.DataTextField = "Sm_Desc";
                    this.RadMenu1.DataValueField = "Sm_Cve";

                    this.RadMenu1.DataBind();
                    if (RadMenu1.FindItemByValue("1") != null)
                    { RadMenu1.FindItemByValue("1").ImageUrl = @"~\Imagenes\" + DT.Select("Sm_Cve=2")[0]["Sm_Img"]; }
                    if (RadMenu1.FindItemByValue("2") != null)
                    { RadMenu1.FindItemByValue("2").ImageUrl = @"~\Imagenes\" + DT.Select("Sm_Cve=2")[0]["Sm_Img"]; }
                    if (RadMenu1.FindItemByValue("22") != null)
                    { RadMenu1.FindItemByValue("22").ImageUrl = @"~\Imagenes\" + DT.Select("Sm_Cve=22")[0]["Sm_Img"]; }
                    if (RadMenu1.FindItemByValue("33") != null)
                    { RadMenu1.FindItemByValue("33").ImageUrl = @"~\Imagenes\" + DT.Select("Sm_Cve=33")[0]["Sm_Img"]; }
                    if (RadMenu1.FindItemByValue("54") != null)
                    { RadMenu1.FindItemByValue("54").ImageUrl = @"~\Imagenes\" + DT.Select("Sm_Cve=54")[0]["Sm_Img"]; }
                    if (RadMenu1.FindItemByValue("72") != null)
                    { RadMenu1.FindItemByValue("72").ImageUrl = @"~\Imagenes\" + DT.Select("Sm_Cve=72")[0]["Sm_Img"]; }
                    if (RadMenu1.FindItemByValue("144") != null)
                    { RadMenu1.FindItemByValue("144").ImageUrl = @"~\Imagenes\" + DT.Select("Sm_Cve=144")[0]["Sm_Img"]; }
                    

                    if (Session["Head" + Session.SessionID] != null)
                    {
                        this.RadioButton1.Text = Session["Head" + Session.SessionID].ToString();
                    }
                }
                else
                {
                    if (HiddenField1.Value != Session.SessionID)
                    {
                        Response.Redirect("inicio.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #region ErrorManager

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
