﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using System.Configuration;

namespace SIANWEB.MasterPage
{
    public partial class PortalRIK : System.Web.UI.MasterPage
    {
        public string[] CurrentPath
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {   
            if (Sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                Session["dir" + Session.SessionID] = Page.Request.Url.PathAndQuery; //pag[pag.Length - 1];
                Response.Redirect("~/login.aspx", true);
            }
        }

        public Sesion Sesion
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    return (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                }
                return null;
            }
        }

        /// <summary>
        /// Regresa la ubicación del recurso uniforme (URL) base de la aplicación.
        /// </summary>
        public String ApplicationUrl
        {
            get
            {
                return string.Format("{0}://{1}{2}", Page.Request.Url.Scheme, Page.Request.Url.Authority, Page.Request.ApplicationPath.TrimEnd('/'));
            }
        }

        public class ElementoNotificacion
        {
            public int IdNotificacion
            {
                get;
                set;
            }

            public string Contenido
            {
                get;
                set;
            }

            public string ClaseIcono
            {
                get;
                set;
            }
        }
    }
}