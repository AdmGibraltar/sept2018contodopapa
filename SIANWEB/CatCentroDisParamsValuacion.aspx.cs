using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Collections;

namespace SIANWEB
{
    public partial class CatCentroDisParamsValuacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (!Page.IsPostBack)
                {
                    // ------------------------------------------------------------------------
                    // Consulta datos Gnerales, Nombre de Epmresa, C. Dist y Región
                    // ------------------------------------------------------------------------
                    CN_CapFactura fac = new CN_CapFactura();
                    string[] datosGen = fac.ConsultaFacturacion_DatosGeneralesFacturacion(sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver);
             
                    //Define variable de sesion con datos de impresion de reporte de valuacion de proyectos
                    this.HD_Folio.Value = string.Concat(
                        Page.Request.QueryString["Id_Emp"].ToString()
                        , ",", Page.Request.QueryString["Id_Cd"].ToString()
                        , ",", Page.Request.QueryString["Id_Vap"].ToString());
                    Session["ReporteValuacionProyecto" + Session.SessionID] = this.HD_Folio.Value;
                    //Define variable de sesion como indicativo de impreion de reporte de rentabilidad
                    Session["ReporteRentabilidadClientes" + Session.SessionID] = "SI";

                    CN_CatCliente cnCliente = new CN_CatCliente();
                    Clientes cte = new Clientes();
                    cte.Id_Cte = Convert.ToInt32(Request.QueryString["Id_Cte"]);
                    cte.Id_Emp = Convert.ToInt32(Request.QueryString["Id_Emp"]);
                    cte.Id_Cd = Convert.ToInt32(Request.QueryString["Id_Cd"]);
                    cnCliente.ConsultaClientes(ref cte, sesion.Emp_Cnx);
                    txtPlazoPago.Text = cte.Cte_CondPago.ToString();

                    //Datos del centro de distribución
                    CentroDistribucion cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd
                        , Convert.ToInt32(Page.Request.QueryString["Id_Cd"])
                        , Convert.ToInt32(Page.Request.QueryString["Id_Emp"])
                        , sesion.Emp_Cnx);

                    //Datos de valuación de proyectos del C. de Dist.
                    CentroDistribucion cdValProy = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultaCentroDistribucion_DatosValProyecto(ref cdValProy, sesion.Emp_Cnx);

                    //Llenar Datos de valuación de proyecto del centro de distribucion
                    #region Llenar Datos de valuación de proyecto del centro de distribucion
                    txtCetes.Text = cd.Cd_TasaCetes == 0 ? cdValProy.Cd_TasaCetes.ToString() : cd.Cd_TasaCetes.ToString();
                    txtAdicionalCetes.Text = cd.Cd_TasaIncCostoCapital == 0 ? cdValProy.Cd_TasaIncCostoCapital.ToString() : cd.Cd_TasaIncCostoCapital.ToString();
                    txtInventarioKey.Text = cd.Cd_Dias == 0 ? cdValProy.Cd_Dias.ToString() : cd.Cd_Dias.ToString();
                    txtInventarioKeyConsignacion.Text = cd.Cd_DiasInv == 0 ? cdValProy.Cd_DiasInv.ToString() : cd.Cd_DiasInv.ToString();
                    txtIva.Text = cd.Cd_Iva == 0 ? cdValProy.Cd_Iva.ToString() : cd.Cd_Iva.ToString();
                    txtComision.Text = cd.Cd_ComisionRik == 0 ? cdValProy.Cd_ComisionRik.ToString() : cd.Cd_ComisionRik.ToString();
                    txtCostosFijosNoPapel.Text = cd.Cd_ContribucionGastosFijosOtros == 0 ? cdValProy.Cd_ContribucionGastosFijosOtros.ToString() : cd.Cd_ContribucionGastosFijosOtros.ToString();
                    txtCostosFijosPapel.Text = cd.Cd_ContribucionGastosFijosPapel == 0 ? cdValProy.Cd_ContribucionGastosFijosPapel.ToString() : cd.Cd_ContribucionGastosFijosPapel.ToString();
                    txtInversionActivosFijos.Text = cd.Cd_FactorConvActFijo == 0 ? cdValProy.Cd_FactorConvActFijo.ToString() : cd.Cd_FactorConvActFijo.ToString();
                    txtIsr.Text = cd.Cd_ISRyPTU == 0 ? cdValProy.Cd_ISRyPTU.ToString() : cd.Cd_ISRyPTU.ToString();
                    txtUcs.Text = cd.Cd_CargoUCS == 0 ? cdValProy.Cd_CargoUCS.ToString() : cd.Cd_CargoUCS.ToString();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            string mensajeError = string.Empty;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "save":
                        mensajeError = "CapValProyecto_ParamsRentabilidad_update_error";
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, mensajeError);
                this.DisplayMensajeAlerta(mensaje);
            }
        }

        #region Funciones

        private void Guardar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CentroDistribucion cd = new CentroDistribucion();
                this.LlenarObjetoCentroDistribucion(ref cd);

                ArrayList al = new ArrayList();
                al.Add(Request.QueryString["Id_Emp"].ToString());
                al.Add(Request.QueryString["Id_Cd"].ToString());
                al.Add(Request.QueryString["Id_Cte"].ToString());
                al.Add(Request.QueryString["Id_Vap"].ToString());

                al.Add(txtVigencia.Value.HasValue ? txtVigencia.Value.Value : 0);
                al.Add((txtComision.Value.HasValue ? txtComision.Value.Value : 0) / 100);
                al.Add(txtManoObra.Value.HasValue ? txtManoObra.Value.Value : 0);
                al.Add(txtAmortizacion.Value.HasValue ? txtAmortizacion.Value.Value : 0);
                al.Add(txtNumEntregas.Value.HasValue ? txtNumEntregas.Value.Value : 0);
                al.Add(txtCostoEntregas.Value.HasValue ? txtCostoEntregas.Value.Value : 0);
                al.Add((txtComisionFactoraje.Value.HasValue ? txtComisionFactoraje.Value.Value : 0) / 100);
                al.Add((txtComisionCruce.Value.HasValue ? txtComisionCruce.Value.Value : 0) / 100);
                al.Add((txtIva.Value.HasValue ? txtIva.Value.Value : 0) / 100);
                al.Add(txtPlazoPago.Value.HasValue ? txtPlazoPago.Value.Value : 0);
                al.Add(txtInventarioKey.Value.HasValue ? txtInventarioKey.Value.Value : 0);
                al.Add(txtInventarioKeyConsignacion.Value.HasValue ? txtInventarioKeyConsignacion.Value.Value : 0);
                al.Add(txtProveedorPapel.Value.HasValue ? txtProveedorPapel.Value.Value : 0);
                al.Add(txtProveedorPapelConsignacion.Value.HasValue ? txtProveedorPapelConsignacion.Value.Value : 0);
                al.Add(txtCreditoProveedor.Value.HasValue ? txtCreditoProveedor.Value.Value : 0);
                al.Add(txtCreditoProveedorPapel.Value.HasValue ? txtCreditoProveedorPapel.Value.Value : 0);
                al.Add((txtFleteLocales.Value.HasValue ? txtFleteLocales.Value.Value : 0) / 100);
                al.Add((txtCostosFijosNoPapel.Value.HasValue ? txtCostosFijosNoPapel.Value.Value : 0) / 100);
                al.Add((txtCostosFijosPapel.Value.HasValue ? txtCostosFijosPapel.Value.Value : 0) / 100);
                al.Add((txtGAdmitivos.Value.HasValue ? txtGAdmitivos.Value.Value : 0) / 100);
                al.Add((txtUcs.Value.HasValue ? txtUcs.Value.Value : 0) / 100);
                al.Add((txtIsr.Value.HasValue ? txtIsr.Value.Value : 0) / 100);
                al.Add(txtInversionActivosFijos.Value.HasValue ? txtInversionActivosFijos.Value.Value : 0);
                al.Add((txtCetes.Value.HasValue ? txtCetes.Value.Value : 0) / 100);
                al.Add((txtAdicionalCetes.Value.HasValue ? txtAdicionalCetes.Value.Value : 0) / 100);
                Session["ValProyectos" + Session.SessionID] = al;

                int verificador = 1;
                new CN_CatCentroDistribucion().ModificarCentroDistribucion_DatosValuacionProyectos(ref cd, sesion.Emp_Cnx, ref verificador);
                if (verificador != 0)
                {
                    string mensaje = "Los datos se modificaron correctamente";
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')")); //cerrar ventana radWindow de factura
                }              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LlenarObjetoCentroDistribucion(ref CentroDistribucion cd)
        {
            cd = new CentroDistribucion();

            cd.Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
            cd.Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
            cd.Cd_TasaCetes = txtCetes.Text == string.Empty ? (double?)null : Convert.ToDouble(txtCetes.Text);
            cd.Cd_Dias = txtInventarioKey.Text == string.Empty ? (int?)null : Convert.ToInt32(txtInventarioKey.Text);
            cd.Cd_DiasInv = txtInventarioKeyConsignacion.Text == string.Empty ? (int?)null : Convert.ToInt32(txtInventarioKeyConsignacion.Text);
            cd.Cd_FactorConvActFijo = txtInversionActivosFijos.Text == string.Empty ? (int?)null : Convert.ToInt32(txtInversionActivosFijos.Text);
            cd.Cd_TasaIncCostoCapital = txtAdicionalCetes.Text == string.Empty ? (double?)null : Convert.ToDouble(txtAdicionalCetes.Text);
            cd.Cd_Iva = txtIva.Text == string.Empty ? (double?)null : Convert.ToDouble(txtIva.Text);
            cd.Cd_ComisionRik = txtComision.Text == string.Empty ? (double?)null : Convert.ToDouble(txtComision.Text);
            cd.Cd_ContribucionGastosFijosOtros = txtCostosFijosNoPapel.Text == string.Empty ? (double?)null : Convert.ToDouble(txtCostosFijosNoPapel.Text);
            cd.Cd_ContribucionGastosFijosPapel = txtCostosFijosPapel.Text == string.Empty ? (double?)null : Convert.ToDouble(txtCostosFijosPapel.Text);
            cd.Cd_ISRyPTU = txtIsr.Text == string.Empty ? (double?)null : Convert.ToDouble(txtIsr.Text);
            cd.Cd_CargoUCS = txtUcs.Text == string.Empty ? (double?)null : Convert.ToDouble(txtUcs.Text);
        }

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("CapValProyecto_ParamsRentabilidad_update_error"))
                    Alerta("Error al momento de actualizar los datos de valuación de proyectos");
                else
                    Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
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