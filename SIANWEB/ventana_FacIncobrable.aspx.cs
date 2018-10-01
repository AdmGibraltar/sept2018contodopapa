using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Collections;
using System.Text;
using System.Xml;
using CapaDatos;
using System.Globalization;
using System.Threading;

namespace SIANWEB
{
    public partial class ventana_FacIncobrable : System.Web.UI.Page
    {

 #region Variables


    #endregion

 #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
          
            {
               
                ErrorManager();
                if (!Page.IsPostBack)
                {
                    ConsultarDepuracionFactura();
                }

            }
            catch (Exception ex)
            {

                ErrorManager(ex, "Page_Load");
            }
          
 
        }


        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "save":
                        Guardar();
                        break;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadToolBar1_ButtonClick");
            }

        }



        

    #endregion

 #region Funciones


        private void ConsultarDepuracionFactura()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Factura factura = new Factura();
                factura.Id_Cd = Convert.ToInt32(Request.Params["Id_Cd"]);
                factura.Id_Emp = Convert.ToInt32(Request.Params["Id_Emp"]);
                factura.Id_Fac = Convert.ToInt32(Request.Params["Id_Fac"]);

                CN_CapFactura CNCapFactura = new CN_CapFactura();
                CNCapFactura.Factura_DepuracionConsulta(ref factura, Sesion.Emp_Cnx);

                this.LblIdCdi.Text = Request.Params["Id_Cd"];
                this.LblId_Fac.Text = Request.Params["Id_Fac"];
                this.LblId_Cte.Text = Convert.ToString(factura.Id_Cte);
                this.lblCte_Nombre.Text = factura.Cte_NomComercial;
                this.chkDepuracion.Checked = Convert.ToBoolean(factura.Fac_Depuracion);
                this.txtMotivo.Text = factura.Fac_DepuracionMotivo;
                this.TxtAutorizo.Text = factura.Fac_DepuracionAutorizo;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }


        private void Guardar()
        {

            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int verificador = 0;
                Factura factura = new Factura();
                factura.Id_Cd = Convert.ToInt32(this.LblIdCdi.Text);
                factura.Id_Emp = Convert.ToInt32(Sesion.Id_Emp);
                factura.Id_Fac = Convert.ToInt32(this.LblId_Fac.Text);
                factura.Fac_Depuracion = this.chkDepuracion.Checked;
                factura.Fac_DepuracionMotivo = this.txtMotivo.Text;
                factura.Fac_DepuracionAutorizo = this.TxtAutorizo.Text;



                CN_CapFactura CNCapFactura = new CN_CapFactura();
                CNCapFactura.Factura_DepuracionActualiza (factura, Sesion.Emp_Cnx, ref verificador);

                if (verificador == 1)
                {
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "Los datos se guardaron correctamente", "')"));
                } 
                else
                {

                    Alerta("Error al intentar guardar los datos");
                }


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

 #endregion

 #region ErrorManager
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void AlertaFocus2(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus2('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 340, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta2(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 600, 150);");
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