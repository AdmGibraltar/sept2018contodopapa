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
    public partial class MasterPage03 : System.Web.UI.MasterPage
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
                if (!Page.IsPostBack)
                {
                    if (!Page.ClientScript.IsClientScriptIncludeRegistered("prototype"))
                    {
                        Page.ClientScript.RegisterClientScriptInclude("prototype", ResolveUrl("../prototype.js"));
                    }
                    HiddenField1.Value = Session.SessionID;
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
